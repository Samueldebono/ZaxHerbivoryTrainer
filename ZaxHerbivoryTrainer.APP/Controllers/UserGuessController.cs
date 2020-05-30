using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using ZaxHerbivoryTrainer.API.Models;
using ZaxHerbivoryTrainer.APP.Bindings;
using ZaxHerbivoryTrainer.APP.Business;
using ZaxHerbivoryTrainer.APP.Helpers;
using ZaxHerbivoryTrainer.APP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ZaxHerbivoryTrainer.APP.Controllers
{
    public class UserGuessController : Controller
    {

        private readonly ApiBusinessController _db;
        private readonly Session _session;

        public UserGuessController( IHttpClientFactory httpClientFactory)
        {
            _db = new ApiBusinessController(httpClientFactory);
            _session = Session.Instance;
        }

        /// <summary>
        /// Init Guess set up
        /// Get user details or creat them
        /// Old guesses
        /// New image
        /// </summary>
        /// <param name="userGuid">Can be null if so new user is created</param>
        /// <returns></returns>
        public async Task<ActionResult> Guess(string userGuid)
        {
            if (_session._isLoggedin)
            {
                //create if no guid
                var getUserRequest = new HttpRequestMessage(HttpMethod.Post, "/api/user");

                //Get if exists
                if (!string.IsNullOrEmpty(userGuid) && new Guid(userGuid) != Guid.Empty)
                    getUserRequest = new HttpRequestMessage(HttpMethod.Get, string.Format("/api/user/hash/{0}", userGuid));

                var getUserResponse = await _db.BuildApiResponse<UserModel>(getUserRequest, _session._accessToken);

                GuessViewModel model = new GuessViewModel();
                if (getUserResponse.Status == HttpStatusCode.OK)
                {
                    model.UserHash = getUserResponse.Content.HashUser.Value;
                    model.UserId = getUserResponse.Content.UserId;
                    model.Timer = 0;

                    //returning details
                    model.ReturningUser = (getUserResponse.Content.Guesses != null && getUserResponse.Content.Guesses.Any());
                    model.ReturningTimer = (getUserResponse.Content.Time.HasValue)
                        ? ((getUserResponse.Content.Time.Value.Hour * 3600) +
                           (getUserResponse.Content.Time.Value.Minute * 60) + (getUserResponse.Content.Time.Value.Second))
                        : 0;
                    
                    //Login logging
                    var updateLoginTokenRequest = new HttpRequestMessage(HttpMethod.Post, string.Format("/api/token/{0}/{1}", _session._accessId, model.UserHash.ToString())); 
                    var updateLoginTokenResponse = await _db.BuildApiResponse<Token>(updateLoginTokenRequest, _session._accessToken);

                    //Past guesses
                    var usersGuessRequest = new HttpRequestMessage(HttpMethod.Get,
                        string.Format("/api/usersGuess/{0}", getUserResponse.Content.UserId));
                    var usersGuessResponse = await _db.BuildApiResponse<List<UserGuessModel>>(usersGuessRequest, _session._accessToken);

                    model.FinalPercentage = getUserResponse.Content.Guesses != null && getUserResponse.Content.Guesses.Any() && usersGuessResponse.Content.Count >= 10
                        ? (decimal) FindDifference(usersGuessResponse.Content.OrderBy(x => x.UsersGuessId).Skip(usersGuessResponse.Content.Count - 10).ToList())
                        : (decimal) 0.0;

                    model.ImagesUsed = usersGuessResponse.Content.Count;

                    //Get random new image that hasn't been used
                    var binding = new RandomImageBinding
                    {
                        PreviousImageIds = getUserResponse.Content.Guesses != null && getUserResponse.Content.Guesses.Any()
                            ? getUserResponse.Content.Guesses.Select(x => x.ImageId).ToArray()
                            : new int[0],
                        ReturnRandom = true
                    };
                    var image = await GetRandomImage(binding);

                    
                    if (image != null)
                    {
                        model.CurrentCloudinaryUrl = Common.BuildCloudinaryUrl(image.FileName);
                        model.CurrentImageId = image.ImageId;
                        model.GuessResultPercent = 0;

                        return View(model);
                    }
                }

                return RedirectToAction("Errorpage", "Home");
            }

            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Check guess
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="guessPercent"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GuessCheck(int imageId, decimal guessPercent, int userId,
            int currentTime)
        {

            var getImageRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/image/{imageId}");
            var getImageResponse = await _db.BuildApiResponse<ImageModel>(getImageRequest, _session._accessToken);
            if (getImageResponse.Status == HttpStatusCode.OK)
            {
                var correctPercent = (double)getImageResponse.Content.DecayRate;
                bool result = false;

                //convert to percentage
                double userGuesdecimal = guessPercent == 0
                    ? 0.0
                    : ((double) guessPercent / 100);

                //if range sits between 10 then answer is correct
                if (userGuesdecimal >= (correctPercent - 0.05) && userGuesdecimal <= (correctPercent + 0.05))
                    result = true;
                else
                    result = false;

                var userGuessBinding = new
                {
                    GuessPercentage = (guessPercent / 100),
                    UserId = userId,
                    ImageId = imageId
                };

                var postGuessRequest = new HttpRequestMessage(HttpMethod.Post, "/api/usersGuess")
                {
                    Content = new StringContent(JsonSerializer.Serialize(userGuessBinding), Encoding.UTF8, "application/json")
                };
                var postGuessResponse = await _db.BuildApiResponse<UserGuessModel>(postGuessRequest, _session._accessToken);
                if (postGuessResponse.Status == HttpStatusCode.OK)
                {
                    //get users guesses list so there isn't any cheating via JS browser methods
                    var getUserGuessRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/usersGuess/{userId}");
                    var getUserGuessResponse = await _db.BuildApiResponse<List<UserGuessModel>>(getUserGuessRequest, _session._accessToken);
                    var dif = 0.0;
                    if (getUserGuessResponse.Status == HttpStatusCode.OK)
                    {
                        //find difference 
                        if (getUserGuessResponse.Content.Count >= 10)
                        {
                            var list = getUserGuessResponse.Content.OrderBy(x => x.UsersGuessId).Skip(getUserGuessResponse.Content.Count - 10)
                                .ToList();
                            dif = FindDifference(list);
                        }

                        //save data so if user exits/refreshes can come back to this point
                        var time = new TimeSpan(0, 0, currentTime);
                        var editUserBinding = new UpdateUserBinding()
                        {
                            FinishingPercent = ((decimal) dif),
                            PictureCycled = getUserGuessResponse.Content.Count,
                            Time = DateTime.UtcNow.Date.Add(time)
                        };

                        var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(editUserBinding));
                        var updateUserRequest = new HttpRequestMessage(HttpMethod.Post, $"/api/user/{userId}")
                        {
                            Content = new StringContent(stringPayload, Encoding.UTF8, "application/json")
                        };
                        var updateUserResponse = await _db.BuildApiResponse<UserModel>(updateUserRequest, _session._accessToken);


                        var returnValues = new
                        {
                            Answer = result,
                            Count = getUserGuessResponse.Content.Count,
                            CorrectPercent = Math.Round((correctPercent * 100)),
                            Total = Math.Round(dif, 2),
                            Error = "",
                            Success = true
                        };
                        return Json(returnValues);
                    }
                    else
                    {
                        return Json(new
                        {
                            Error = getUserGuessResponse.Status + ": " + getUserGuessResponse.Error,
                            Success = false
                        });
                    }
                }
                else
                {
                    return Json(new
                    {
                        Error = postGuessResponse.Status + ": " + postGuessResponse.Error,
                        Success = false
                    });
                }
            }

            return Json(new
            {
                Error = getImageResponse.Status + ": " + getImageResponse.Error,
                Success = false
            });
        }

        /// <summary>
        /// Get random new image 
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="guessPercent"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetNewImage(int[] preImageId)
        {
            var binding = new RandomImageBinding
            {
                PreviousImageIds = preImageId,
                ReturnRandom = true
            };

            var image = await GetRandomImage(binding);

            var returnValues = new
            {
                ImageId = image.ImageId,
                ImageUrl = Common.BuildCloudinaryUrl(image.FileName)
            };
            return Json(returnValues);
        }

        /// <summary>
        /// On Finish State
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Guess(GuessViewModel model)
        {
            if (ModelState.IsValid)
            {
                //save data with finish time
                var time = new TimeSpan(0, 0, (model.ReturningTimer + model.Timer));
                var binding = new UpdateUserBinding()
                {
                    FinishedUtc = DateTime.UtcNow,
                    FinishingPercent = model.FinalPercentage.Value,
                    PictureCycled = model.ImagesUsed.Value,
                    Time = DateTime.UtcNow.Date.Add(time)
                };
                var editUserRequest = new HttpRequestMessage(HttpMethod.Put, $"/api/user/hash/{model.UserHash}")
                {
                    Content = new StringContent(JsonSerializer.Serialize(binding), Encoding.UTF8, "application/json")
                };
                var editUserResponse = await _db.BuildApiResponse<UserModel>(editUserRequest, _session._accessToken);

                return RedirectToAction("Finish", "UserGuess", new {userGuid = model.UserHash});
            }

            return View(model);
        }
        /// <summary>
        /// GETs following data to display back to user
        /// number of images
        /// guess minus actual answer 
        /// average time (Time/images)
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public async Task<ActionResult> Finish(string userGuid)
        {
            if (_session._isLoggedin)
            {

                var getUserRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/user/hash/{userGuid}");
                var getUserResponse = await _db.BuildApiResponse<UserModel>(getUserRequest, _session._accessToken);
                if (getUserResponse.Status == HttpStatusCode.OK)
                {
                    var getUserGuessRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/usersGuess/{getUserResponse.Content.UserId}");
                    var getUserGuessResponse = await _db.BuildApiResponse<List<UserGuessModel>>(getUserGuessRequest, _session._accessToken);
                    if (getUserResponse.Status == HttpStatusCode.OK)
                    {
                        var imageNumberArray = new List<int>();
                        var guessResultsArray = new List<decimal>();
                        var time = getUserResponse.Content.Time.HasValue ? getUserResponse.Content.Time.Value : DateTime.Now.Date;

                        //convert time into seconds
                        var seconds = time.Second;
                        seconds += (time.Minute * 60);
                        seconds += (time.Hour * 3600);

                        //Get GuessPercentage - Actual Answer for table
                        int i = 1;
                        foreach (var item in getUserGuessResponse.Content)
                        {
                            var result = (item.GuessPercentage - item.Image.DecayRate) * 100;
                            guessResultsArray.Add(result);
                            imageNumberArray.Add(i);
                            i++;
                        }

                        //Total Time
                        TimeSpan t = TimeSpan.FromSeconds(seconds);
                        string totalTime = string.Format("{0:D2}:{1:D2}:{2:D2}s",
                            t.Hours,
                            t.Minutes,
                            t.Seconds);

                        //Average Time
                        TimeSpan t2 = TimeSpan.FromSeconds((seconds / imageNumberArray.Count));
                        string averageTime = string.Format("{0:D2}:{1:D2}:{2:D2}s",
                            t2.Hours,
                            t2.Minutes,
                            t2.Seconds);

                        var model = new FinishedModelView()
                        {
                            GuessResult = guessResultsArray.ToArray(),
                            NumberOfImages = imageNumberArray.ToArray(),
                            TotalTime = totalTime,
                            AverageTime = averageTime
                        };

                        return View(model);
                    }
                }

                return RedirectToAction("Errorpage", "Home");
            }

            return RedirectToAction("Login", "Account");

        }

        [HttpPost]
        public ActionResult Finish()
        {
            return RedirectToAction("Start", "Home");
        }

        #region Private Methods
        /// <summary>
        /// Get random image 
        /// </summary>
        /// <param name="binding"></param>
        /// <returns></returns>
        private async Task<ImageModel> GetRandomImage(RandomImageBinding binding)
        {
            var imageRequest = new HttpRequestMessage(HttpMethod.Get, "/api/image")
            {
                Content = new StringContent(JsonSerializer.Serialize(binding), Encoding.UTF8, "application/json")
            };
            var imageResponse = await _db.BuildApiResponse<ImageModel>(imageRequest, _session._accessToken);
            if (imageResponse.Status == HttpStatusCode.OK)
            {
                return imageResponse.Content;
            }

            return null;
        }


        /// <summary>
        /// returns the different for the last ten images
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private double FindDifference(List<UserGuessModel> list)
        {
            var dif = 0.0;
            foreach (var item in list)
            {
                var calc = (double) (item.GuessPercentage - item.Image.DecayRate);
                dif += calc < 0 ? calc * -1 : calc;
            }

            dif = (1 - (dif / 10)) * 100;
            return Math.Round(dif, 2);
        }


        #endregion
    }
}
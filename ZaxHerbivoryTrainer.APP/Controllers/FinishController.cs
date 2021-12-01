using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZaxHerbivoryTrainer.API.Models;
using ZaxHerbivoryTrainer.APP.Business;
using ZaxHerbivoryTrainer.APP.Helpers;
using ZaxHerbivoryTrainer.APP.Models;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ZaxHerbivoryTrainer.APP.Controllers
{
    public class FinishController : Controller
    {
        private readonly ApiBusinessController _db;
        private readonly Session _session;

        public FinishController(IHttpClientFactory httpClientFactory)
        {
            _db = new ApiBusinessController(httpClientFactory);
            _session = Session.Instance;
        }
        /// <summary>
        /// GETs following data to display back to user
        /// number of images
        /// guess minus actual answer 
        /// average time (Time/images)
        /// </summary>
        /// <param name="userGuid"></param>
        /// <returns></returns>
        public async Task<ActionResult> PhaseTwoFinish(string userGuid)
        {
            if (_session._isLoggedin)
            {

                var getUserRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/user/hash/{userGuid}");
                var getUserResponse = await _db.BuildApiResponse<UserModel>(getUserRequest, _session._accessToken);
                if (getUserResponse.Status == HttpStatusCode.OK)
                {
                    var getUserGuessRequest = new HttpRequestMessage(HttpMethod.Get, $"/api/usersGuess/{getUserResponse.Content.UserId}/{(byte)Phase.TWO}");
                    var getUserGuessResponse = await _db.BuildApiResponse<List<UserGuessModel>>(getUserGuessRequest, _session._accessToken);
                    if (getUserResponse.Status == HttpStatusCode.OK)
                    {
                        var imageNumberArray = new List<int>();
                        var guessResultsArray = new List<decimal>();
                        var time = getUserResponse.Content.TimePhase2.HasValue ? getUserResponse.Content.TimePhase2.Value : DateTime.Now.Date;

                        //convert time into seconds
                        var seconds = time.Second;
                        seconds += (time.Minute * 60);
                        seconds += (time.Hour * 3600);

                        //Get GuessPercentage - Actual Answer for table
                        int i = 1;
                        var guesses = getUserGuessResponse.Content.OrderBy(x => x.UsersGuessId);
                        foreach (var item in guesses)
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
                            AverageTime = averageTime,
                            UserGuid = userGuid
                        };

                        return View(model);
                    }
                }

                return RedirectToAction("Errorpage", "Home");
            }

            return RedirectToAction("Login", "Account");

        }

        [HttpPost]
        public ActionResult FinishPhase2(FinishedModelView model)
        {
            return RedirectToAction("GuessWithFeedback", "UserGuess", new { model.UserGuid, phase = 2 });
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
                        var imageNumberArrayP1 = new List<int>();
                        var imageNumberArrayP2 = new List<int>();
                        var imageNumberArrayP3 = new List<int>();
                        var guessResultsArrayP1 = new List<decimal>();
                        var guessResultsArrayP2 = new List<decimal>();
                        var guessResultsArrayP3 = new List<decimal>();
                        var time = getUserResponse.Content.TimePhase2.HasValue ? getUserResponse.Content.TimePhase2.Value : DateTime.Now.Date;

                        //convert time into seconds
                        var seconds = time.Second;
                        seconds += (time.Minute * 60);
                        seconds += (time.Hour * 3600);

                        //Get GuessPercentage - Actual Answer for table
                        int i = 1;
                        var guesses = getUserGuessResponse.Content.OrderBy(x => x.UsersGuessId);
                        var guessesP1 = guesses.Where(x => x.Phase == Phase.ONE);
                        var guessesP2 = guesses.Where(x => x.Phase == Phase.TWO);
                        var guessesP3 = guesses.Where(x => x.Phase == Phase.THREE);
                        foreach (var item in guessesP1)
                        {
                            var result = (item.GuessPercentage - item.Image.DecayRate) * 100;
                            guessResultsArrayP1.Add(result);
                            imageNumberArrayP1.Add(i);
                            i++;
                        }
                        foreach (var item in guessesP2)
                        {
                            var result = (item.GuessPercentage - item.Image.DecayRate) * 100;
                            guessResultsArrayP2.Add(result);
                            imageNumberArrayP2.Add(i);
                            i++;
                        }
                        foreach (var item in guessesP3)
                        {
                            var result = (item.GuessPercentage - item.Image.DecayRate) * 100;
                            guessResultsArrayP3.Add(result);
                            imageNumberArrayP3.Add(i);
                            i++;
                        }

                        //Total Time
                        TimeSpan t = TimeSpan.FromSeconds(seconds);
                        string totalTime = string.Format("{0:D2}:{1:D2}:{2:D2}s",
                            t.Hours,
                            t.Minutes,
                            t.Seconds);

                        //Average Time
                        TimeSpan t2 = TimeSpan.FromSeconds((seconds / imageNumberArrayP2.Count));
                        string averageTime = string.Format("{0:D2}:{1:D2}:{2:D2}s",
                            t2.Hours,
                            t2.Minutes,
                            t2.Seconds);

                        var model = new CompletedModelView()
                        {
                            GuessResultPhase1 = guessResultsArrayP1.ToArray(),
                            GuessResultPhase2 = guessResultsArrayP2.ToArray(),
                            GuessResultPhase3 = guessResultsArrayP3.ToArray(),
                            NumberOfImagesPhase1 = imageNumberArrayP1.ToArray(),
                            NumberOfImagesPhase2 = imageNumberArrayP2.ToArray(),
                            NumberOfImagesPhase3 = imageNumberArrayP3.ToArray(),
                            TotalTimePhase2 = totalTime,
                            AverageTimePhase2 = averageTime
                        };

                        return View(model);
                    }
                }

                return RedirectToAction("Errorpage", "Home");
            }

            return RedirectToAction("Login", "Account");

        }

        [HttpPost]
        public async Task<ActionResult> Finish(CompletedModelView model)
        {
            if (!string.IsNullOrEmpty(model.Email))
            {
                var createUserEmailBinding = new
                {
                    CreatedUtc = DateTime.UtcNow,
                    Email = model.Email,
                    PrizeSent = false
                };

                var postUserEmailRequest = new HttpRequestMessage(HttpMethod.Post, "/api/users/email")
                {
                    Content = new StringContent(JsonSerializer.Serialize(createUserEmailBinding), Encoding.UTF8,
                        "application/json")
                };
                var postGuessResponse =
                    await _db.BuildApiResponse<UserEmailsModel>(postUserEmailRequest, _session._accessToken);
                if (postGuessResponse.Status == HttpStatusCode.OK)
                {
                    return RedirectToAction("Start", "Home");
                }
            }

            return RedirectToAction("Start", "Home");
        }




    }
}

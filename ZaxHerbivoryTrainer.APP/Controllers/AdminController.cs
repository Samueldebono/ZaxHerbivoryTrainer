using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ZaxHerbivoryTrainer.APP.Business;
using ZaxHerbivoryTrainer.APP.Helpers;
using ZaxHerbivoryTrainer.APP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZaxHerbivoryTrainer.API.Helpers;
using ZaxHerbivoryTrainer.API.Models;

namespace ZaxHerbivoryTrainer.APP.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApiBusinessController _db;
        private readonly Session _session;

        public AdminController(IHttpClientFactory httpClientFactory)
        {
            _db = new ApiBusinessController(httpClientFactory);
            _session = Session.Instance;
        }

        /// <summary>
        /// Search and return all user guesses
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> UserGuessSearch()
        {
            if (_session._isLoggedin)
            {
                if (_session._userRole == Session.Role.Admin)
                {
                    var model = new UserGuessSearchModel()
                    {
                        UserGuessSearchResults = new List<UserGuessSearchResultsModel>()
                    };


                    var request = new HttpRequestMessage(HttpMethod.Get, "/api/users");
                    var response = await _db.BuildApiResponse<List<UserModel>>(request, _session._accessToken);

                    if (response.Status == HttpStatusCode.OK)
                    {
                        var users = new List<UserModel>();
                        users = response.Content;

                        List<UserGuessSearchResultsModel> UserResults = new List<UserGuessSearchResultsModel>();
                        foreach (var item in users)
                        {
                            var imageCountP1 = item.Guesses.Where(x => x.Phase == Phase.ONE).ToList();
                            var imageCountP2 = item.Guesses.Where(x => x.Phase == Phase.TWO).ToList();
                            var imageCountP3 = item.Guesses.Where(x => x.Phase == Phase.THREE).ToList();

                            var trimmedUserModel = new UserGuessSearchResultsModel()
                            {
                                CreatedUtc = item.CreatedUtc.Date.ToString("dd-MM-yyyy"),
                                HashUser = item.HashUser.Value.ToString(),
                                UserId = item.UserId,
                                RetentionUser = imageCountP1.Any() ? "No" : "Yes",

                                //PHASE 1
                                FinishedPhase1Utc = item.FinishedPhase1Utc.HasValue
                                    ? item.FinishedPhase1Utc.Value.Date.ToString("dd-MM-yyyy")
                                    : "",
                                FinishingPercentPhase1 = item.FinishingPercentPhase1.HasValue
                                    ? item.FinishingPercentPhase1.Value.ToString()
                                    : "0",
                                PictureCycledPhase1 = imageCountP1.Count.ToString(),
                                TimePhase1 = item.TimePhase1.HasValue ? item.TimePhase1.Value.TimeOfDay.ToString() : "",

                                //PHASE 2
                                FinishedPhase2Utc = item.FinishedPhase2Utc.HasValue
                                    ? item.FinishedPhase2Utc.Value.Date.ToString("dd-MM-yyyy")
                                    : "",
                                FinishingPercentPhase2 = item.FinishingPercentPhase2.HasValue
                                    ? item.FinishingPercentPhase2.Value.ToString()
                                    : "0",
                                PictureCycledPhase2 = imageCountP2.Count.ToString(),
                                TimePhase2 = item.TimePhase2.HasValue ? item.TimePhase2.Value.TimeOfDay.ToString() : "",

                                //PHASE 3
                                FinishedPhase3Utc = item.FinishedPhase3Utc.HasValue
                                    ? item.FinishedPhase3Utc.Value.Date.ToString("dd-MM-yyyy")
                                    : "",
                                FinishingPercentPhase3 = item.FinishingPercentPhase3.HasValue
                                    ? item.FinishingPercentPhase3.Value.ToString()
                                    : "0",
                                PictureCycledPhase3 = imageCountP3.Count.ToString(),
                                TimePhase3 = item.TimePhase3.HasValue ? item.TimePhase3.Value.TimeOfDay.ToString() : ""



                            };
                            UserResults.Add(trimmedUserModel);

                        }

                        model = new UserGuessSearchModel()
                        {
                            UserGuessSearchResults = UserResults
                        };

                        return View(model);
                    }

                    return View(model);
                }
                else
                {
                    return RedirectToAction("Start", "Home");
                }
            }

            return RedirectToAction("Login", "Account");
        }


        public async Task<ActionResult> UserCompletionDataSearch()
        {
            if (_session._isLoggedin)
            {
                if (_session._userRole == Session.Role.Admin)
                {

                    var model = new UserCompletionDataSearchModel()
                    {
                        UserCompletionDataSearchResults = new List<UserCompletionDataSearchResultsModel>()
                    };
                    var request = new HttpRequestMessage(HttpMethod.Get, "/api/usersGuess");
                    var response = await _db.BuildApiResponse<List<UserGuessModel>>(request, _session._accessToken);

                    if (response.Status == HttpStatusCode.OK)
                    {
                        var userGuesses = new List<UserGuessModel>();
                        userGuesses = response.Content.Where(x=>x.Phase==Phase.TWO).OrderBy(x=>x.UsersGuessId).ToList();
                        var distinctUserId = userGuesses.Select(x => x.UserId).Distinct().ToList(); 

                        List<UserCompletionDataSearchResultsModel> UserCompletionDataSearchResults =
                            new List<UserCompletionDataSearchResultsModel>();

                        var maxCols = 0;
                        foreach (var item in distinctUserId)
                        {
                            var trimmedGuesSearchResultsModel = new UserCompletionDataSearchResultsModel()
                            {
                                //GuessId = item.UsersGuessId,
                                UserId = item.ToString(),
                                GuessResult = Calculations.FindHerbivoryDifferenceoOfList(userGuesses.Where(x=>x.UserId==item).ToList())
                            };

                            if (trimmedGuesSearchResultsModel.GuessResult.Count > maxCols)
                                maxCols = trimmedGuesSearchResultsModel.GuessResult.Count;

                            UserCompletionDataSearchResults.Add(trimmedGuesSearchResultsModel);

                        }


                        model = new UserCompletionDataSearchModel()
                        {
                            UserCompletionDataSearchResults = UserCompletionDataSearchResults,
                            MaxGuess = maxCols
                        };



                        return View(model);
                    }
                    return View(model);
                }
                else
                {
                    return RedirectToAction("Start", "Home");
                }
            }

            return RedirectToAction("Login", "Account");
        }

        /// <summary>
        /// Search and return all user guesses
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> UserEmailSearch()
        {
            if (_session._isLoggedin)
            {
                if (_session._userRole == Session.Role.Admin)
                {
                    var model = new UserEmailSearchModel()
                    {
                        UserEmailSearchResults = new List<UserEmailSearchResultsModel>()
                    };


                    var request = new HttpRequestMessage(HttpMethod.Get, "/api/users/emails");
                    var response = await _db.BuildApiResponse<List<UserEmailsModel>>(request, _session._accessToken);

                    if (response.Status == HttpStatusCode.OK)
                    {
                        var userEmails = new List<UserEmailsModel>();
                        userEmails = response.Content;

                        List<UserEmailSearchResultsModel> UserEmailsResults = new List<UserEmailSearchResultsModel>();
                        foreach (var item in userEmails)
                        {
                            var trimmedUserModel = new UserEmailSearchResultsModel()
                            {
                                UserEmailId = item.UserEmailId,
                                CreatedUtc = item.CreatedUtc.Date.ToString("dd-MM-yyyy"),
                                Email = item.Email,
                                PrizeSent = item.PrizeSent

                            };

                            UserEmailsResults.Add(trimmedUserModel);

                        }

                        model = new UserEmailSearchModel()
                        {
                            UserEmailSearchResults = UserEmailsResults
                        };

                        return View(model);
                    }

                    return View(model);
                }
                else
                {
                    return RedirectToAction("Start", "Home");
                }
            }

            return RedirectToAction("Login", "Account");
        }
    }
}
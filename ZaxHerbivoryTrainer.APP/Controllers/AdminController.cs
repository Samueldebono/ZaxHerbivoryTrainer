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

namespace ZaxHerbivoryTrainer.APP.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApiBusinessController _db;
        private readonly Session _session;

        public AdminController( IHttpClientFactory httpClientFactory)
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
                            var trimmedUserModel = new UserGuessSearchResultsModel()
                            {
                                CreatedUtc = item.CreatedUtc.Date.ToString("dd-MM-yyyy"),
                                FinishedUtc = item.FinishedUtc.HasValue ? item.FinishedUtc.Value.Date.ToString("dd-MM-yyyy") : "",
                                FinishingPercent = item.FinishingPercent.HasValue
                                    ? item.FinishingPercent.Value.ToString()
                                    : "0",
                                HashUser = item.HashUser.Value.ToString(),
                                PictureCycled = item.Guesses.Count.ToString(),
                                Time = item.Time.HasValue ? item.Time.Value.TimeOfDay.ToString() : "",
                                UserId = item.UserId

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
    }
}
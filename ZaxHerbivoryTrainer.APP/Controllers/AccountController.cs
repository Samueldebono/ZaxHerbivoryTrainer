﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZaxHerbivoryTrainer.APP.Business;
using ZaxHerbivoryTrainer.APP.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZaxHerbivoryTrainer.APP.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ZaxHerbivoryTrainer.APP.Controllers
{
    public class AccountController : Controller
    {
        private readonly Session _session;
        private readonly ApiBusinessController _db;
        private readonly IOptions<Config> _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory,
            IOptions<Config> config)
        {

            _session = Session.Instance;
            _db = new ApiBusinessController(httpClientFactory);
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Post Login Details
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var client = _httpClientFactory.CreateClient();
            try
            {
                var binding = new
                    {
                        UserName = "Guest",
                        Password = "2021Participant!"                    };

                    var request = new HttpRequestMessage(HttpMethod.Post, "/api/authenticate")
                    {
                        Content = new StringContent(JsonSerializer.Serialize(binding), Encoding.UTF8,
                            "application/json")
                    };
                    var authResponse = await _db.BuildApiResponse<AuthenticateModel>(request);
                    if (authResponse.Status == HttpStatusCode.OK)
                    {
                        _session._accessToken = authResponse.Content.BearerToken;
                        _session._userRole = (Session.Role)authResponse.Content.RoleType;
                        _session._isLoggedin = true;
                        _session._expiryDate = authResponse.Content.ExpiryDate;
                        _session._accessId = authResponse.Content.AccessId;

                        if (_session._userRole == Session.Role.Guest)
                            return RedirectToAction("Start", "Home");
                        else if (_session._userRole == Session.Role.Admin)
                            return RedirectToAction("UserGuessSearch", "Admin");
                        else if (_session._userRole == Session.Role.ReturnGuest)
                            return RedirectToAction("GuessWithFeedback", "UserGuess");
                    }
                    else
                    {
                        ModelState.AddModelError("CustomError", "Username or password is incorrect");
                        return View();
                    }

                    //AccessTrainer
                    if (!model.AccessResults)
                        return RedirectToAction("Start", "Home");
                    else
                        return RedirectToAction("UserGuessSearch", "Admin");

            }
            catch (Exception)
            {
                ModelState.AddModelError("CustomError", "Make sure the reCAPTCHA is ticked");
                return View();
            }

            ModelState.AddModelError("CustomError", "Please contact support");
            return View();
        }

        /// <summary>
        /// Manage Login Screen
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {

            LoginModel model = new LoginModel();
            return View(model);

        }

        /// <summary>
        /// Manage Logoff
        /// </summary>
        /// <returns></returns>


        public ActionResult Logoff()
        {
            _session._accessToken = "";
            _session._userRole = 0;
            _session._isLoggedin = false;

            return RedirectToAction("Login", "Account");
        }
    }
}
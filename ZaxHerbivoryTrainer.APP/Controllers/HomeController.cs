using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ZaxHerbivoryTrainer.APP.Business;
using ZaxHerbivoryTrainer.APP.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ZaxHerbivoryTrainer.APP.Models;
using Microsoft.AspNetCore.Http;

namespace ZaxHerbivoryTrainer.APP.Controllers
{
    public class HomeController : Controller
    {
        private readonly Session _session;
        private readonly ApiBusinessController _db;

        public HomeController( IHttpClientFactory httpClientFactory)
        {
            _session = Session.Instance;
            _db = new ApiBusinessController(httpClientFactory);
        }

        /// <summary>
        /// Manage Instructions and start page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Start()
        {
            if (_session._isLoggedin)
            {
                StartModel model = new StartModel();
                return View(model);
            }
            else
                return RedirectToAction("Login", "Account");
        }
       
        /// <summary>
        /// Manage if User is returning or not
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Start(StartModel model)
        {
            if (ModelState.IsValid)
            {
                //if(model.Continue)
                //    return RedirectToAction("Guess", "UserGuess", new { userGuid = model.Hash });
                //else
                    return RedirectToAction("GuessNoFeedback", "UserGuess");
            }
            else
            {
                return View();

            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Errorpage()
        {
            return View();
        }

    }
}

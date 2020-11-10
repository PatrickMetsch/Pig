using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pig.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Pig.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            State state = new State();
            string stateString = JsonConvert.SerializeObject(state);
            HttpContext.Session.SetString("state", stateString);

            return View();
        }

        public IActionResult Roll()
        {
            string stateString = HttpContext.Session.GetString("state");
            State stateJson = JsonConvert.DeserializeObject<State>(stateString);

            Random random = new Random();
            int roll = random.Next(1, 7);

            int turnNumber = stateJson.TurnNumber;

            string newStateString;

            if (roll == 1)
            {
                if (turnNumber % 2 == 0)
                {
                    stateJson.P1Score = 0;
                }
                else
                {
                    stateJson.P2Score = 0;
                }
                stateJson.TurnNumber += 1;
                stateJson.CurrentDiceValue = 0;

                newStateString = JsonConvert.SerializeObject(stateJson);
                HttpContext.Session.SetString("state", newStateString);

            } else
            {
                stateJson.CurrentDiceValue = roll;

                newStateString = JsonConvert.SerializeObject(stateJson);
                HttpContext.Session.SetString("state", newStateString);
            }

            return View();
        }

        public IActionResult Hold()
        {
            string stateString = HttpContext.Session.GetString("state");
            State stateJson = JsonConvert.DeserializeObject<State>(stateString);

            int turnNumber = stateJson.TurnNumber;

            string newStateString;

            if (turnNumber % 2 == 0)
            {
                stateJson.P1Score += stateJson.CurrentDiceValue;
                if (stateJson.P1Score >= 20)
                {
                    newStateString = JsonConvert.SerializeObject(stateJson);
                    HttpContext.Session.SetString("state", newStateString);
                    TempData["Winner"] = "1";
                    return RedirectToAction("Winner");
                }
            }
            else
            {
                stateJson.P2Score += stateJson.CurrentDiceValue;

                if (stateJson.P2Score >= 20)
                {
                    newStateString = JsonConvert.SerializeObject(stateJson);
                    HttpContext.Session.SetString("state", newStateString);
                    TempData["Winner"] = "2";
                    return RedirectToAction("Winner");
                }
            }

            stateJson.TurnNumber += 1;
            stateJson.CurrentDiceValue = 0;

            newStateString = JsonConvert.SerializeObject(stateJson);
            HttpContext.Session.SetString("state", newStateString);

            return View();
        }

        public IActionResult Winner()
        {
            return View();
        }
    }
}

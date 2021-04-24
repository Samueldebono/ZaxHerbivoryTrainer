using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaxHerbivoryTrainer.APP.Models;

namespace ZaxHerbivoryTrainer.APP.Helpers
{
    public static class Calculations
    {
        /// <summary>
        /// returns the different for the last ten images
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static double FindAverageDifferenceOfList(List<UserGuessModel> list)
        {
            var dif = 0.0;
            foreach (var guess in list)
            {
                dif += FindSingleHerbivoryDifference(guess);
            }

            dif = (1 - (dif / list.Count)) * 100;
            return Math.Round(dif, 2);
        }        /// <summary>
        /// returns the different for the last ten images
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<double> FindHerbivoryDifferenceoOfList(List<UserGuessModel> list)
        {
            var guessHerbivoryList = new List<double>();
            foreach (var guess in list)
            {
                guessHerbivoryList.Add(FindSingleHerbivoryDifference(guess));
            }

            
            return guessHerbivoryList;
        }

        public static double FindSingleHerbivoryDifference(UserGuessModel guess)
        {
            var calc = (double)(guess.GuessPercentage - guess.Image.DecayRate);
            var dif = calc < 0 ? calc * -1 : calc;

            return dif;
        }


    }
}

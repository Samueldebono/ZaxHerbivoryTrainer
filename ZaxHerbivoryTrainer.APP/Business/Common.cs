using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ZaxHerbivoryTrainer.APP.Business
{
    public static class Common
    {
        public static string BuildCloudinaryUrl(string filename)
        {
            return $"https://res.cloudinary.com/pegleg/image/upload/v1585832588/UNSW/{filename}";
        }

        public static bool DownloadData<T>(List<T> datalist)
        {
            try
            {


            using (StreamWriter sw = File.CreateText("list.csv"))
            {
                for (int i = 0; i < datalist.Count; i++)
                {
                    sw.WriteLine(datalist[i]);
                }
            }

            return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }


        }

    }
}

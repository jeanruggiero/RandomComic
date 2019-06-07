using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Comics
{
    public interface IComicGenerator
    {
        Comic RandomComic();
    }

    public class xkcdGenerator : IComicGenerator
    {

        private int LatestComicNumber()
        {
            try
            {
                //Determine number of latest comic
                JObject comicData = RequestJsonResponse(
                    "http://xkcd.com/info.0.json");
                return (int)comicData["num"];
            }

            catch (System.Net.WebException ex)
            {
                throw ex;
            }
        }

        private int RandomNumber(int max)
        {
            //Generate a random number between 1 and the maximum value specified
            Random random = new Random();
            return random.Next(1, max);
        }

        private JObject RequestJsonResponse(String url)
        {
            try
            {
                //Make an htttp get request to the specified URL
                WebRequest request = WebRequest.Create(url);
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
                StreamReader reader = new StreamReader(stream, encode);

                //Convert response to json JObject
                var responseJson = reader.ReadToEnd();
                return JObject.Parse(((String)responseJson));
            }

            catch (System.Net.WebException)
            {
                throw new System.Net.WebException("Http request failed");
            }
        }

        private JObject GetComic(int comicNumber)
        {
            try
            {
                //Build URL for requested comic
                String url = "http://xkcd.com/" + comicNumber.ToString() + 
                    "/info.0.json";
                //Retrieve a random comic from xkcd
                return RequestJsonResponse(url);
            }

            catch (System.Net.WebException ex)
            {
                throw ex;
            }
        }

        public Comic RandomComic()
        {
            JObject comicData = GetComic(RandomNumber(LatestComicNumber()));
            return new Comic((string)comicData["title"],
                (string)comicData["month"] + '/' +
                (string)comicData["day"] + '/' + (string)comicData["year"],
                (string)comicData["transcript"]);
        }
    }

    public class Comic
    {
        public string title;
        public string published_date;
        public string transcript;

        public Comic(string ctitle, string cpublished_date, string ctranscript)
        {
            //Assign instance attributes
            title = ctitle;
            published_date = cpublished_date;
            transcript = ctranscript;
        }
    }
}
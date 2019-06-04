using System;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace xkcd
{
    public class Comic
    {

        public String title;
        public String published_date;
        public String transcript;

        public Comic()
        {
            //Determine number of latest comic
            WebRequest request = WebRequest.Create("http://xkcd.com/info.0.json");
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader reader = new StreamReader(stream, encode);

            //Convert response to json JObject
            var responseJson = reader.ReadToEnd();
            JObject comicData = JObject.Parse(((String)responseJson));
            int latest = (int)comicData["num"];

            //Generate a random number between 1 and the latest comic number
            Random random = new Random();
            int randomNumber = random.Next(1, latest);

            String url = "http://xkcd.com/" + randomNumber.ToString() + "/info.0.json";

            //Retrieve a random comic from xkcd
            request = WebRequest.Create(url);
            response = request.GetResponse();
            stream = response.GetResponseStream();
            reader = new StreamReader(stream, encode);

            //Convert response to json JObject
            responseJson = reader.ReadToEnd();
            comicData = JObject.Parse(((String)responseJson));

            //Assign instance attributes
            title = (string)comicData["title"];
            published_date = (string)comicData["month"] + '/' +
                (string)comicData["day"] + '/' + (string)comicData["year"];
            transcript = (string)comicData["transcript"];
        }
    }
}
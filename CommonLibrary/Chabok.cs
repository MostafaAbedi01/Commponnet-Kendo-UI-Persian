using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;

namespace CommonLibrary
{
    public class ChabokAllUser
    {
        public Target target { get; set; }
        public string channel { get; set; }
        public Notification notification { get; set; }
        public string content { get; set; }
    }

    public class Target
    {
        public List<And> and { get; set; }
    }

    public class And
    {
       // public List<string> tags { get; set; }
       // public Launchtime launchTime { get; set; }
        public string deviceType { get; set; }
       // public Created created { get; set; }
    }

    public class Launchtime
    {
        public long lte { get; set; }
    }

    public class Created
    {
        public long gte { get; set; }
    }


    public class Chabok
    {
        public Notification notification { get; set; }
        public string user { get; set; }
        public string content { get; set; }
    }

    public class Notification
    {
        public string title { get; set; }
        public string body { get; set; }
    }


    public class SendChabokNotificattion
    {
        public static string SenderAllUsers(ChabokAllUser pushe)
        {
            var serializer = new JavaScriptSerializer();

            var tRequest =
                WebRequest.Create(
                    "https://sandbox.push.adpdigital.com/api/push/notifyUsers?access_token=e69b618e9b55242773a2d847effaa617113533e8");
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";


            var json = serializer.Serialize(pushe);

            var byteArray = Encoding.UTF8.GetBytes(json);


            tRequest.ContentLength = byteArray.Length;

            using (var dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (var tResponse = tRequest.GetResponse())
                {
                    using (var dataStreamResponse = tResponse.GetResponseStream())
                    {
                        using (var tReader = new StreamReader(dataStreamResponse))
                        {
                            var sResponseFromServer = tReader.ReadToEnd();

                            var str = sResponseFromServer;

                            return str;
                        }
                    }
                }
            }
        }

        public static string SenderOneUser(Chabok pushe)
        {
            var serializer = new JavaScriptSerializer();

            var tRequest = WebRequest.Create(string.Format(
                "https://sandbox.push.adpdigital.com/api/push/toUsers?access_token={0}",
                "e69b618e9b55242773a2d847effaa617113533e8"));
            tRequest.Method = "post";
            tRequest.ContentType = "application/json";


            var json = serializer.Serialize(pushe);

            var byteArray = Encoding.UTF8.GetBytes(json);


            tRequest.ContentLength = byteArray.Length;

            using (var dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (var tResponse = tRequest.GetResponse())
                {
                    using (var dataStreamResponse = tResponse.GetResponseStream())
                    {
                        using (var tReader = new StreamReader(dataStreamResponse))
                        {
                            var sResponseFromServer = tReader.ReadToEnd();

                            var str = sResponseFromServer;

                            return str;
                        }
                    }
                }
            }
        }
    }
}
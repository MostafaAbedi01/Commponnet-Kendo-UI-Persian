using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using RestSharp;

namespace CommonLibrary
{
    public class WebHelper
    {
        public string HashSha1(string input)
        {
            var hash = new SHA1Managed().ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Join("", hash.Select(b => b.ToString("x2")).ToArray());
        }

        public bool CheckHeader(HttpRequestMessage request, string key)
        {
            IEnumerable<string> keys;
            if (!request.Headers.TryGetValues(key, out keys))
                return false;
            return ConfigurationManager.AppSettings[key] == keys.FirstOrDefault();
        }

        public static string HttpWebPost(string serviceUrl, string resourceUrl, string requestBody)
        {
            //var request = WebRequest.Create(string.Concat(serviceUrl.Trim(), resourceUrl)) as HttpWebRequest;

            var request = (HttpWebRequest)WebRequest.Create("https://mci.irpointcenter.com/mci/buytopupcharge");

           
            var data = Encoding.ASCII.GetBytes(requestBody);

            request.Method = "POST";
            request.ContentType = "application/json;";
            //request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();

            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
            //string responseMessage = null;
            //var request = WebRequest.Create(string.Concat(serviceUrl.Trim(), resourceUrl)) as HttpWebRequest;

            //if (request != null)
            //{
            //    request.ContentType = "application/json;";
            //    request.Method = "POST";
            //}

            //if (requestBody != null)
            //{
            //    var requestBodyBytes = JsonToByte(requestBody);
            //    if (request != null)
            //    {
            //        request.ContentLength = requestBodyBytes.Length;
            //        using (var postStream = request.GetRequestStream())
            //        {
            //            postStream.Write(requestBodyBytes, 0, requestBodyBytes.Length);
            //        }
            //    }
            //}

            //if (request != null)
            //    try
            //    {
            //        var response = request.GetResponse() as HttpWebResponse;
            //        if (response != null && response.StatusCode == HttpStatusCode.OK)
            //        {
            //            var responseStream = response.GetResponseStream();
            //            if (responseStream != null)
            //            {
            //                var reader = new StreamReader(responseStream);

            //                responseMessage = reader.ReadToEnd();
            //            }
            //        }
            //        else
            //        {
            //            if (response != null)
            //                responseMessage = response.StatusDescription;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        return ex.Message;
            //    }

            //return responseMessage;
        }

        public static byte[] JsonToByte(string @this)
        {
            byte[] bytes = null;
            var serializer1 = new DataContractJsonSerializer(typeof(string));
            var ms1 = new MemoryStream();
            serializer1.WriteObject(ms1, @this);
            ms1.Position = 0;
            var reader = new StreamReader(ms1);
            bytes = ms1.ToArray();
            return bytes;
        }

        public string HttpWebGet(string serviceUrl, string resourceUrl)
        {
            string responseMessage = null;
            var request = WebRequest.Create(string.Concat(serviceUrl.Trim(), resourceUrl)) as HttpWebRequest;
            if (request != null)
            {
                request.ContentType = "application/json;";
                request.Method = "GET";
            }

            if (request != null)
                try
                {
                    var response = request.GetResponse() as HttpWebResponse;
                    if (response != null && response.StatusCode == HttpStatusCode.OK)
                    {
                        var responseStream = response.GetResponseStream();
                        if (responseStream != null)
                        {
                            var reader = new StreamReader(responseStream);

                            responseMessage = reader.ReadToEnd();
                        }
                    }
                    else
                    {
                        if (response != null) responseMessage = response.StatusDescription;
                    }
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }

            return responseMessage;
        }

        public static string SendRequest(string resource, string Method, string url)
        {
            try
            {
                string result = "";
                HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url/*/" + MethodName*/);
                byte[] byteData = Encoding.UTF8.GetBytes(resource);
                Request.ContentType = "application/json";
                Request.ContentLength = byteData.Length;
                Request.Method = Method;
                //Request.Headers.Add("Authorization", "c5f670d332811e41da6e4acab8e1789bde29488c");
                Stream requestStream = Request.GetRequestStream();
                requestStream.Write(byteData, 0, byteData.Length);
                requestStream.Close();
                HttpWebResponse response = (HttpWebResponse)Request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)

                {
                    Stream responseStream = response.GetResponseStream();
                    result = new StreamReader(responseStream).ReadToEnd();
                }
                return result;
            }
            catch (Exception ex)
            {
                //ex.ExceptionResolve();
                return "";
            }
        }

        public static IRestResponse SendRequestAsBody<T>(string url, string methodName, Method sendMethod, object obj, TimeSpan? time = null) where T : new()
        {
            IRestResponse result = null;
            try
            {
                var client = new RestClient(string.Concat(url + methodName));
                RestRequest request = new RestRequest {Method = sendMethod};
                if (time != null)
                    request.Timeout = Convert.ToInt32(time.Value.TotalMilliseconds);
                request.AddHeader("ContentType", "application/json");
                if (typeof(T).Name.Trim().ToLower().Contains("nullable"))
                    request.AddJsonBody(obj);
                else
                    request.AddJsonBody((T)obj);
                result = client.Execute(request);
                if (string.IsNullOrEmpty(result.Content))
                    throw new Exception(result.Content + result?.ErrorMessage);
            }
            catch (Exception ex)
            {
               
            }
            return result;
        }


        public static IRestResponse SendRequestGet(string url, string methodName, Method sendMethod, object obj, TimeSpan? time = null) 
        {
            IRestResponse result = null;
            try
            {
                var client = new RestClient(string.Concat(url + methodName));
                RestRequest request = new RestRequest { Method = Method.GET };
                if (time != null)
                    request.Timeout = Convert.ToInt32(time.Value.TotalMilliseconds);
                request.AddHeader("ContentType", "application/json");
                
                result = client.Execute(request);
                if (string.IsNullOrEmpty(result.Content))
                    throw new Exception(result.Content + result?.ErrorMessage);
            }
            catch (Exception ex)
            {

            }
            return result;
        }

    }
}
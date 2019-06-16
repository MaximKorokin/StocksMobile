using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace StocksMobile
{
    internal static class HttpRequest
    {
        public const string BackendUrl = "http://anceag-001-site1.itempurl.com/";
        private const string BaseUrl = "http://anceag-001-site1.itempurl.com/api/";

        public static async Task<string> Get(string url)
        {
            var request = CreateRequest(url, "GET");
            return await GetResponse(request);
        }

        public static async Task<string> Post(string url, string obj)
        {
            var request = CreateRequest(url, "POST");
            await WriteRequestBody(request, obj);
            return await GetResponse(request);
        }

        private static HttpWebRequest CreateRequest(string url, string method)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(BaseUrl + url);
            request.Method = method;
            request.AutomaticDecompression = DecompressionMethods.GZip;
            request.Headers["Authorization"] = $"Bearer {CurrentUserManager.CurrentUser?.Token}";
            request.ContentType = "application/json";
            return request;
        }

        private static async Task<string> GetResponse(HttpWebRequest request)
        {
            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }
        }

        private static async Task WriteRequestBody(HttpWebRequest request, string data)
        {
            using (var stream = await request.GetRequestStreamAsync())
            using (var streamWriter = new StreamWriter(stream))
            {
                streamWriter.Write(data);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BigCBatchProvisioning
{
    public static class HttpClientPool
    {
        /// <summary>
        /// Singleton client
        /// </summary>
        private static HttpClient _client = null;

        /// <summary>
        /// Lock object
        /// </summary>
        private static readonly object _lock = new object();

        /// <summary>
        /// Single client pool for all HTTP requests
        /// </summary>
        public static HttpClient ClientPool
        {
            get
            {
                if (_client == null) {
                    lock (_lock) {
                        if (_client == null) {
                            _client = new HttpClient();
                            _client.DefaultRequestHeaders.ConnectionClose = true;
                            _client.Timeout = Timeout.InfiniteTimeSpan;
                        }
                    }
                }
                return _client;
            }
        }

        /// <summary>
        /// Set the URI for a request message as a shortcut
        /// </summary>
        /// <param name="request"></param>
        /// <param name="baseUri"></param>
        /// <param name="relativeUri"></param>
        public static void SetUri(this HttpRequestMessage request, string baseUri, string relativeUri)
        {
            request.RequestUri = new Uri(new Uri(baseUri), relativeUri);
        }

        /// <summary>
        /// Set the URI for a request message as a shortcut
        /// </summary>
        /// <param name="request"></param>
        /// <param name="baseUri"></param>
        public static void SetBaseUri(this HttpRequestMessage request, string baseUri)
        {
            request.RequestUri = new Uri(baseUri);
        }

        /// <summary>
        /// Add a relative path to an existing base URI
        /// </summary>
        /// <param name="request"></param>
        /// <param name="relativePath"></param>
        public static void AddRelativePath(this HttpRequestMessage request, string relativePath)
        {
            request.RequestUri = new Uri(request.RequestUri, relativePath);
        }

        /// <summary>
        /// Set the URI for a request message as a shortcut
        /// </summary>
        /// <param name="request"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void SetBasicAuth(this HttpRequestMessage request, string username, string password)
        {
            var combined = $"{username}:{password}";
            var bytes = System.Text.Encoding.UTF8.GetBytes(combined);
            var base64 = System.Convert.ToBase64String(bytes);
            request.Headers.Add("Authorization", "Basic " + base64);
        }
    }
}

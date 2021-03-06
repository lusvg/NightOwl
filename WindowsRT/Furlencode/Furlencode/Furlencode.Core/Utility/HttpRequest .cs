﻿using Furlencode.Core.Error;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Furlencode.Core.Utility
{
    /// <summary>
    /// Common HTTP Request Class
    /// Supports GET/POST methods
    /// Supports JSON and XML Serialization 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class HttpRequest<T>
    {
        private string uri = string.Empty;
        private RequestMethod requestMethod = RequestMethod.GET;
        private Dictionary<string, string> customHeaders = null;
        private Dictionary<string, string> urlParams = null;
        private bool enableCache = false;
        private HttpContent httpContent = null;
        private Dictionary<string, string> cookies = null;
        private string UUID { get; set; }




        /// <summary>
        /// Initialize a HTTPRequest helper. Works for both GET & POST methods
        /// </summary>
        /// <param name="uri">The web serive URL</param>
        /// <param name="requestMethod">GET/POST</param>
        /// <param name="urlParams">any query string parameters</param>
        /// <param name="cookies">cookies to be sent across</param>
        /// <param name="enableCache">Can we use server cache or request new?</param>
        /// <param name="content"></param>
        public HttpRequest(string uri, RequestMethod requestMethod, Dictionary<string, string> customHeaders = null, Dictionary<string, string> urlParams = null, Dictionary<string, string> cookies = null, bool enableCache = false, HttpContent content = null, string uuid = null)
        {
            this.uri = uri;
            this.requestMethod = requestMethod;
            this.customHeaders = customHeaders;
            this.urlParams = urlParams ?? new Dictionary<string, string>();
            this.enableCache = enableCache;
            this.httpContent = content;
            this.cookies = cookies ?? new Dictionary<string, string>();
            this.UUID = uuid;
        }

        /// <summary>
        /// Deserialize a request 
        /// Read JSON and populate object
        /// </summary>
        /// <returns>Object deserialzied</returns>

        public async Task<T> GetResponseForJSON()
        {

            string responseString = await GetResponseString();
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseString);
        }

        public async Task<string> GetResponseForString()
        {

            string responseString = await GetResponseString();
            return responseString;
        }
        /// <summary>
        /// Private function - called to make the necessary HTTP requests
        /// </summary>
        /// 
        /// <returns>String received in the response</returns>

        public async Task<string> GetResponseString()
        {
            try
            {
                HttpClient httpClient;
                string requestUri;
                SetupHTTPClientURI(out httpClient, out requestUri);

                HttpResponseMessage response;
                string responseString = string.Empty;
                try
                {
                    if (requestMethod == RequestMethod.GET)
                    {
                        response = await httpClient.GetAsync(requestUri);
                    }
                    else if (requestMethod == RequestMethod.PUT)
                    {
                        var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
                        request.Content = httpContent;
                        response = await httpClient.SendAsync(request);
                    }
                    else
                    {
                        var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
                        request.Content = httpContent;
                        response = await httpClient.SendAsync(request);
                    }
                    responseString = await response.Content.ReadAsStringAsync();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return responseString;
                    }

                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        //The user is using an invalid authorization code

                        //  ServiceError error = ExtractErrorCodeMessage(response, responseString);
                        //  error.IsAuthorizationError = true;

                        //IMvxMessenger _messenger = Mvx.Resolve<IMvxMessenger>();
                        //_messenger.Publish(new InvalidSessionMessage(this, error));

                        //   throw error;
                        throw new System.InvalidOperationException("");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden
                             || response.StatusCode == System.Net.HttpStatusCode.BadRequest
                             || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        //Some server side error
                        //    throw ExtractErrorCodeMessage(response, responseString);
                        throw new System.InvalidOperationException("");
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        //Unable to contact server
                        /*   var error = ExtractErrorCodeMessage(response, responseString);
                           error.IsConnectivityError = true;
                           throw error; */
                        throw new System.InvalidOperationException("");
                    }
                    else
                    {
                        //Other unhandled errors
                        //  throw ExtractErrorCodeMessage(response, responseString);
                        throw new System.InvalidOperationException("");
                    }
                }
                catch (ServiceError ce)
                {
                    throw ce;//Todo : Send custom exception

                }

            }
            catch (ServiceError ce)
            {
                throw ce;//Todo : Send custom exception
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw;//Todo : Send custom exception
            }
        }


        public async Task<bool> GetResponseAsFile(string filePath, string fileHash, bool encryptAndDeleteOriginal = false, bool allowOverwrite = false)
        {
            try
            {
                HttpClient httpClient;
                string requestUri;
                SetupHTTPClientURI(out httpClient, out requestUri);
                HttpResponseMessage response;
                string responseString = string.Empty;
                try
                {

                    response = await httpClient.GetAsync(requestUri);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        using (Stream stream = await response.Content.ReadAsStreamAsync())
                        {
                            //FileStorage.WriteFile(stream, filePath, fileHash, allowOverwrite);

                        }
                        return true;
                    }

                    else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        //The user is using an invalid authorization code

                        //   ServiceError error = ExtractErrorCodeMessage(response, responseString);
                        // error.IsAuthorizationError = true;

                        // throw error;
                        throw new System.InvalidOperationException("");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden
                             || response.StatusCode == System.Net.HttpStatusCode.BadRequest
                             || response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        //Some server side error
                        // throw ExtractErrorCodeMessage(response, responseString);
                        throw new System.InvalidOperationException("");
                    }
                    else if (response.StatusCode == HttpStatusCode.NotFound)
                    {
                        //Unable to contact server
                        //  var error = ExtractErrorCodeMessage(response, responseString);
                        //  error.IsConnectivityError = true;
                        //  throw error;
                        throw new System.InvalidOperationException("");
                    }
                    else
                    {
                        //Other unhandled errors
                        //  throw ExtractErrorCodeMessage(response, responseString);
                        throw new System.InvalidOperationException("");
                    }
                }
                catch (ServiceError ce)
                {
                    throw ce;//Todo : Send custom exception

                }

            }
            catch (ServiceError ce)
            {
                throw ce;//Todo : Send custom exception
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.StackTrace);
                throw;//Todo : Send custom exception
            }
        }




        private void SetupHTTPClientURI(out HttpClient httpClient, out string requestUri)
        {
            CookieContainer a = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler()
            {
                CookieContainer = a
            };

            httpClient = new HttpClient(handler);
            httpClient.Timeout = TimeSpan.FromMinutes(1);

            if (customHeaders != null)
            {
                // Add Custom headers to the request
                foreach (var key in customHeaders.Keys)
                {
                    httpClient.DefaultRequestHeaders.Add(key, customHeaders[key]);
                }
            }
            if (cookies.Keys.Count > 0)
            {
                // Add Cookies to the request
                foreach (var key in cookies.Keys)
                {
                    a.Add(new Uri(uri), new Cookie(key, cookies[key]));
                }
            }


            if (!enableCache)
            {
                urlParams.Add("nckey", Guid.NewGuid().ToString());
            }

            requestUri = uri;
            // Add URL Parameters to the request
            if (urlParams.Count > 0)
            {
                requestUri = string.Concat(requestUri, "?", string.Join("&", urlParams.Select(c => string.Concat(c.Key, "=", c.Value))));
            }
        }

        /* private static ServiceError ExtractErrorCodeMessage(HttpResponseMessage response, string responseString)
         {
                 ServiceError ObCustomsErrors = new ServiceError()
                 {
                     Error = new ErrorModel()
                     {                       
                         HttpStatusMessage = response.StatusCode.ToString(),
                         ReasonPhrase = response.ReasonPhrase
                     }
                 };
                 return ObCustomsErrors;
         } */
    }

    public enum RequestMethod
    {
        GET,
        POST,
        PUT
    }
}
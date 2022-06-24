using DohrniiFoundation.IServices;
//using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DohrniiFoundation.Helpers
{
    public static class RefitHelper
    {
        //public static IRestService GetService()
        //{
        //    string baseUrl = "http://10.0.2.2:8000/backend/api";
        //    IRestService restClient = null;

        //    var token = Preferences.Get("accessToken", string.Empty);
        //    if (string.IsNullOrEmpty(token))
        //    {
        //        restClient = RestService.For<IRestService>(baseUrl);
        //    }
        //    else
        //    {
        //        var httpClient = new HttpClient(new AuthHeaderHandler()) { BaseAddress = new Uri(baseUrl) };
        //        restClient = RestService.For<IRestService>(httpClient);
        //    }
        //    return restClient;
        //}
    }

    public class AuthHeaderHandler : DelegatingHandler
    {
        public AuthHeaderHandler()
        {
            InnerHandler = new HttpClientHandler();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = Preferences.Get("accessToken", string.Empty);

            //potentially refresh token here if it has expired etc.

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}

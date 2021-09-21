using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stripe_Test.Services
{
    public interface IGetResponseService
    {
        Task<IRestResponse> GetResponseWithAuth(string url, string auth);
    }
    public class GetResponseService : IGetResponseService
    {
        public GetResponseService()
        {
        }

        public async Task<IRestResponse> GetResponseWithAuth(string url, string auth)
        {
            var ApiBaseUrl = "https://api.sandbox.midtrans.com/";

            var client = new RestClient(ApiBaseUrl + url);
            var byteKey = System.Text.Encoding.UTF8.GetBytes("SB-Mid-server-NPQvXe4LtZJRa-v46eTuKN7k:");

            var request = new RestRequest(Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "BASIC " + Convert.ToBase64String(byteKey));
            var response = await client.ExecuteAsync(request);

            return response;
        }
    }
}

using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stripe_Test.Services
{
    public interface IPostResponseService
    {
        Task<IRestResponse> PostResponseWithAuth(string url, string obj);
    }

    public class PostResponseService : IPostResponseService
    {
        public async Task<IRestResponse> PostResponseWithAuth(string url, string obj)
        {
            //var ApiBaseUrl = "https://api.sandbox.midtrans.com/v2/";
            var ApiBaseUrl = "https://app.sandbox.midtrans.com/snap/v1/";
            var UserName = "SB-Mid-server-NPQvXe4LtZJRa-v46eTuKN7k";

            var byteArray = Encoding.ASCII.GetBytes($"{UserName}:");

            var client = new RestClient(ApiBaseUrl + url);
            //var byteKey = System.Text.Encoding.UTF8.GetBytes(UserName+ ":");

            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic " + Convert.ToBase64String(byteArray));
            request.AddJsonBody(obj);
            var response = await client.ExecuteAsync(request);

            return response;
        }
    }
}

using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace IdentityServerDemo.ClientSecret
{
    class Program
    {
        public static void Main(string[] args) => CallApiAsync().GetAwaiter().GetResult();

        private static async Task CallApiAsync()
        {
            var discovery = await DiscoveryClient.GetAsync("http://localhost:5000");

            if(discovery.IsError)
            {
                Console.WriteLine(discovery.Error);
                return;
            }
            Console.WriteLine("Discovery Success");
            Console.WriteLine("");

            var tokenClient = new TokenClient(discovery.TokenEndpoint, "ClientSecret", "clientSecret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("IdentityApi");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            Console.WriteLine("Token Response Success");
            Console.WriteLine("");
            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("");

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:5001/api/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return;
            }
            
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(JArray.Parse(content));
        }
    }
}

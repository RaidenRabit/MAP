using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using MAPeApi.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests
{
    public class IntegrationFakeServer
    {
        private HttpServer _server;
        private Token _token;
        protected HttpClient _client;
        public readonly Dictionary<TestUser, LoginInfo> _testUsers = new Dictionary<TestUser, LoginInfo>()
        {
            { TestUser.testUser1, new LoginInfo{Username = "testUser1", Password = "testUser1.test", Email = "fspthp+hwbc4c4p2yku9v@sharklasers.com", Nickname = "testUser1"} },
            { TestUser.testUser2, new LoginInfo{Username = "testUser2", Password = "testUser2.test", Email = "fsptin+cdctcgj3u3t1vk@sharklasers.com", Nickname = "testUser2"} },
        };

        [SetUp]
        protected void StartServer()
        {
            HttpConfiguration config = new HttpConfiguration();
            MAPeApi.WebApiConfig.Register(config);
            _server = new HttpServer(config);
            _client = new HttpClient(_server);
            _client.BaseAddress = new Uri("http://localhost:55537");
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [TearDown]
        protected void Dispose()
        {
            _server.Dispose();
            _client.Dispose();
        }

        protected async Task LoginAs(TestUser testUser)
        {
            if (_testUsers.ContainsKey(testUser))
            {
                var a = _testUsers[testUser];
                await GetToken(a.Username, a.Password);
            }
            else
            {
                throw (new Exception("No such user"));
            }
        }

        private async Task<bool> GetToken(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>( "grant_type", "password" ),
                new KeyValuePair<string, string>( "username", userName ),
                new KeyValuePair<string, string> ( "Password", password )
            };
            var content = new FormUrlEncodedContent(pairs);
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            using (_client)
            {
                var response = await _client.PostAsync("/token", content);
                if (response.IsSuccessStatusCode)
                {
                    _token = JsonConvert.DeserializeObject<Token>(await response.Content.ReadAsStringAsync());
                    return true;
                }
            }
            return false;
        }
    }

    class Token
    {
        public string access_token { get; set; }
        public string issued { get; set; }
        [JsonProperty(".expires")]
        public string expires { get; set; }
    }

    public enum TestUser
    {
        testUser1,
        testUser2,
    }

    public class LoginInfo
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nickname { get; set; }
        public string Email { get; set; }
    }
    
}

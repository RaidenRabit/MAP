using System.Net.Http;
using System.Web.Http;
using NUnit.Framework;

namespace Tests
{
    public class IntegrationFakeServer
    {
        private HttpServer _server;
        protected HttpClient _client;

        [SetUp]
        protected void StartServer()
        {
            HttpConfiguration config = new HttpConfiguration();
            MAPeApi.WebApiConfig.Register(config);
            _server = new HttpServer(config);
            _client = new HttpClient(_server);
        }

        [TearDown]
        protected void Dispose()
        {
            _server.Dispose();
            _client.Dispose();
        }
    }
}

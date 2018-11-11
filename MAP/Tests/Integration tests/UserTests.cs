using System.Threading.Tasks;
using MAPeApi.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Tests.Integration_tests
{
    /// <summary>
    /// Summary description for UserTests
    /// </summary>
    public class UserTests : IntegrationFakeServer
    {
        [Test]
        public async Task WhenLoggedInAsUser_ReadUser_ReturnsUserObject()
        {
            //set
            await LoginAs(TestUser.testUser1);
            //act
            var result = await _client.GetAsync("api/user/GetUser");
            var deserializedResult = JsonConvert.DeserializeObject<User>(await result.Content.ReadAsStringAsync());
            //assert
            Assert.AreEqual(_testUsers[TestUser.testUser1].Nickname, deserializedResult.Nickname);
        }
    }
}

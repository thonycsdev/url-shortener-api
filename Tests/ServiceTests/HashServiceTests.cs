using FluentAssertions;
using Service.Interfaces;
using Service.Services;
namespace Tests.ServiceTests.HashServiceTests
{
    public class HashServiceTests : BaseTests
    {

        [Fact]
        public async Task GivenAStringInput_ShouldReturnAHashedId()
        {
            IHashService hashService = new HashService();
            string url = faker.Internet.UrlWithPath();
            string hashId = await hashService.CreateHashId(url);
            hashId.Should().NotBeEmpty();
            hashId.Should().NotBeSameAs(url);
        }
        [Fact]
        public async Task GivenTheSameInput_ShouldCreateTheSameHashId()
        {
            IHashService hashService = new HashService();
            string url = faker.Internet.UrlWithPath();
            string hashId1 = await hashService.CreateHashId(url);
            string hashId2 = await hashService.CreateHashId(url);
            hashId1.Should().Be(hashId2);
        }
    }
}

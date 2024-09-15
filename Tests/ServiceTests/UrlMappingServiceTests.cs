using System.Linq.Expressions;
using AutoFixture;
using Domain.Entities;
using FluentAssertions;
using Moq;
using Repository.Interfaces;
using Service.Interfaces;
using Service.Services;
using Service.Utils;

namespace Tests.ServiceTests
{
    public class UrlMappingServiceTests : BaseTests
    {
        private readonly Mock<IUrlMappingRepository> _mockRepository;
        private readonly Mock<IHashService> _hashService;

        public UrlMappingServiceTests()
        {
            _mockRepository = new Mock<IUrlMappingRepository>();
            _hashService = new Mock<IHashService>();
        }

        [Fact]
        public async void GivenNoShortGuid_ShouldReturnStringUrlNotFound()
        {
            var service = new UrlMappingService(_mockRepository.Object, _hashService.Object);
            string shortGuid = CreateUtils.CreateRandomShortGuid();
            _mockRepository.Setup(x => x.GetBy(It.IsAny<Expression<Func<UrlMapping, bool>>>()));
            var result = await service.FindBaseUrlByShortGuid(shortGuid);
            result.URL.Should().Be("UrlNotFound");
        }

        [Fact]
        public async void GivenAShortGuid_ShouldReturnABaseUrlFromUrlMapping()
        {

            var service = new UrlMappingService(_mockRepository.Object, _hashService.Object);
            var urlMappingFixture = fixture.Create<UrlMapping>();
            _mockRepository.Setup(x => x.GetBy(It.IsAny<Expression<Func<UrlMapping, bool>>>())).ReturnsAsync(urlMappingFixture);
            var result = await service.FindBaseUrlByShortGuid(urlMappingFixture.RouteId);
            result.URL.Should().Be(urlMappingFixture.BaseUrl);
        }

        [Fact]
        public async void WhenUrlBaseAlreadyHasAHashIdInDatabase_ShouldReturnTheShortenedUrl_AndNotCallSave()
        {

            var service = new UrlMappingService(_mockRepository.Object, _hashService.Object);
            var urlMappingFixture = fixture.Create<UrlMapping>();

            _hashService.Setup(x => x.CreateHashId(It.IsAny<string>())).ReturnsAsync("hashId");
            _mockRepository.Setup(x => x.GetBy(It.IsAny<Expression<Func<UrlMapping, bool>>>())).ReturnsAsync(urlMappingFixture);
            var result = await service.MakeItShort(urlMappingFixture.BaseUrl);
            result.URL.Should().Be(urlMappingFixture.ShortenedUrl);
            _mockRepository.Verify(x => x.Save(It.IsAny<UrlMapping>()), Times.Never());
        }
    }
}

using Domain.Entities;
using Repository.Interfaces;
using Service.Interfaces;
using Service.Utils;

namespace Service.Services
{
    public class UrlMappingService : IUrlMappingService
    {
        private readonly IUrlMappingRepository _repository;
        private readonly IHashService _hashService;
        public UrlMappingService(IUrlMappingRepository repository, IHashService hashService)
        {
            _repository = repository;
            _hashService = hashService;
        }

        public async Task<string> FindBaseUrlByShortGuid(string shortGuid)
        {
            var result = await _repository.GetBy(x => x.RouteId == shortGuid);
            if (result is null) return "UrlNotFound";
            return result.BaseUrl;
        }

        public async Task<string> MakeItShort(string urlBase)
        {
            var hashId = await _hashService.CreateHashId(urlBase);
            var result = await _repository.GetBy(x => x._id == hashId);
            if (result is not null) return result.ShortenedUrl;

            UrlMapping urlMapping = BuildUrlMapping(urlBase, hashId);
            await _repository.Save(urlMapping);
            return urlMapping.ShortenedUrl;
        }

        private static UrlMapping BuildUrlMapping(string urlBase, string hashId)
        {
            var routeId = CreateUtils.CreateRandomShortGuid();
            var shortenedUrl = CreateUtils.CreateShortenedUrl(routeId);
            UrlMapping urlMapping = new() { BaseUrl = urlBase, _id = hashId, RouteId = routeId, ShortenedUrl = shortenedUrl };
            return urlMapping;
        }
    }
}

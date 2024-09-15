using Domain.Entities;
using Repository.Interfaces;
using Service.DTO;
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

        //Dado um short guid ( final da url reduzida ) deve procurar por essa chave, e retornar a url base 
        public async Task<UrlMappingResponse> FindBaseUrlByShortGuid(string shortGuid)
        {
            var result = await _repository.GetBy(x => x.RouteId == shortGuid);
            if (result is null) return new UrlMappingResponse { URL = "UrlNotFound" };
            return new UrlMappingResponse { URL = result.BaseUrl };
        }

        //verifica se a url foi reduzida
        // se ja foi reduzida retorna o valor reduzido
        // se nao
        // reduz e salva o valor o banco

        public async Task<UrlMappingResponse> MakeItShort(string urlBase)
        {
            var hashId = await _hashService.CreateHashId(urlBase);
            var result = await _repository.GetBy(x => x._id == hashId);
            if (result is not null) return new UrlMappingResponse { URL = result.ShortenedUrl };

            UrlMapping urlMapping = BuildUrlMapping(urlBase, hashId);
            await _repository.Save(urlMapping);
            return new UrlMappingResponse { URL = urlMapping.ShortenedUrl };
        }

        //constroi o valor do urlmapping

        private static UrlMapping BuildUrlMapping(string urlBase, string hashId)
        {
            var routeId = CreateUtils.CreateRandomShortGuid();
            var shortenedUrl = CreateUtils.CreateShortenedUrl(routeId);
            UrlMapping urlMapping = new() { BaseUrl = urlBase, _id = hashId, RouteId = routeId, ShortenedUrl = shortenedUrl };
            return urlMapping;
        }
    }
}

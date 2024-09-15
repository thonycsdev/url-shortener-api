using Service.DTO;

namespace Service.Interfaces
{
    public interface IUrlMappingService
    {
        Task<UrlMappingResponse> MakeItShort(string urlBase);
        Task<UrlMappingResponse> FindBaseUrlByShortGuid(string shortGuid);
    }
}

namespace Service.Interfaces
{
    public interface IUrlMappingService
    {
        Task<string> MakeItShort(string urlBase);
        Task<string> FindBaseUrlByShortGuid(string shortGuid);
    }
}

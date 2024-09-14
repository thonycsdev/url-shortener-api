using System.Text;

namespace Service.Utils
{
    public static class CreateUtils
    {
        public static string CreateRandomShortGuid()
        {
            var guidTest = Guid.NewGuid();
            var shortGuid = guidTest.ToString();
            return shortGuid.Substring(0, 6);
        }
        public static string CreateShortenedUrl(string routeId)
        {
            var domain = Environment.GetEnvironmentVariable("DOMAIN");
            var stringBuilder = new StringBuilder(domain);
            stringBuilder.Append(routeId);
            return stringBuilder.ToString();
        }
    }
}

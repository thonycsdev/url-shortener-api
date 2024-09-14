using AutoFixture;
using Bogus;
using System.Text.Json;

namespace Tests
{
    public class BaseTests
    {
        public Faker faker = new Faker("pt_BR");
        public Fixture fixture = new Fixture();
        public BaseTests()
        {

            var rootPath = Directory.GetCurrentDirectory();
            var dotenvLocation = Path.Combine(rootPath, ".env.tests");
            DotEnv.LoadEnvironmentVariables(dotenvLocation);
        }


        public void Log(dynamic obj)
        {
            string result = JsonSerializer.Serialize(obj);
            Console.WriteLine(result);
        }

    }
    public static class DotEnv
    {
        public static void LoadEnvironmentVariables(string filePath)
        {
            if (!File.Exists(filePath))
                return;


            foreach (var line in File.ReadAllLines(filePath))
            {
                var parts = line.Split(
                    '=',
                    StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length != 2)
                    continue;
                Environment.SetEnvironmentVariable(parts[0], parts[1]);
            }
        }
    }



}

using NewKinsTake4.Data.Models;
using NewKinsTake4.Data.Services;
using NewKinsTake4.Data.States;
using System.Text.Json;

namespace NewKinsTake4.Data.Helper
{
    public class DomainStructureJsonLoader
    {
        public static void LoadDomainStructure(DomainStructureState domainStructureState)
        {

            var basePath = AppContext.BaseDirectory;

            string filePath = Path.Combine(basePath, "wwwroot", "data", "DomainStructure.json");
            try
            {
                // Read the JSON file
                string jsonContent = File.ReadAllText(filePath);

                // Deserialize the JSON content to the WorkflowRoot object
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                JsonRootModel? data = JsonSerializer.Deserialize<JsonRootModel>(jsonContent, options);
                domainStructureState.DomainStructure = data;

                WorkflowJsonLoader.LoadWorkflow(domainStructureState);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading or deserializing JSON file: {ex.Message}");
            }
        }
    }
}
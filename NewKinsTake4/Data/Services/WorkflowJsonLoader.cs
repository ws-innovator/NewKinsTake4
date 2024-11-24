using NewKinsTake4.Data.Models;
using NewKinsTake4.Data.States;
using System.Text.Json;

namespace NewKinsTake4.Data.Services
{
    public class WorkflowJsonLoader
    {
        public static void LoadWorkflow(DomainStructureState domainStructureState)
        {
            if (domainStructureState?.DomainStructure?.Workflow == null)
            {
                throw new ArgumentNullException(nameof(domainStructureState), "DomainStructureState or its Workflow property cannot be null.");
            }

            var basePath = AppContext.BaseDirectory;
            string dataFolderPath = Path.Combine(basePath, "wwwroot", "data");

            if (!Directory.Exists(dataFolderPath))
            {
                throw new DirectoryNotFoundException($"Data folder path not found: {dataFolderPath}");
            }

            var workflowFiles = Directory.GetFiles(dataFolderPath, "workflow_??_??.json");

            foreach (var file in workflowFiles)
            {
                // Extract the domain and subdomain codes from the file name
                string fileName = Path.GetFileNameWithoutExtension(file); // e.g., "workflow_XX_YY"
                var fileNameParts = fileName.Split('_');
                if (fileNameParts.Length != 3)
                {
                    Console.WriteLine($"Invalid file format: {fileName}");
                    continue;
                }

                string domainCode = fileNameParts[1]; // Extract "XX"
                string subDomainCode = fileNameParts[2]; // Extract "YY"

                // Read the content of the JSON file
                string jsonContent = File.ReadAllText(file);

                // Deserialize JSON content into a list of WorkflowModel
                List<WorkflowModel>? workflows = null;
                try
                {
                    var workflowJson = JsonDocument.Parse(jsonContent);
                    if (workflowJson.RootElement.TryGetProperty("Workflow", out var workflowElement))
                    {
                        workflows = JsonSerializer.Deserialize<List<WorkflowModel>>(workflowElement.ToString());
                    }
                    else
                    {
                        Console.WriteLine($"Workflow property not found in JSON file: {file}");
                        continue;
                    }
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error parsing JSON file {file}: {ex.Message}");
                    continue;
                }

                if (workflows == null)
                {
                    Console.WriteLine($"No workflows found in file: {file}");
                    continue;
                }

                // Find the domain and subdomain objects
                var domain = domainStructureState.DomainStructure.Workflow
                    .FirstOrDefault(x => x.DomainCode == domainCode);

                if (domain == null)
                {
                    Console.WriteLine($"Domain not found for DomainCode: {domainCode}");
                    continue;
                }

                var subDomain = domain.SubDomainNames
                    ?.FirstOrDefault(x => x.SubDomainCode == subDomainCode);

                if (subDomain == null)
                {
                    Console.WriteLine($"SubDomain not found for SubDomainCode: {subDomainCode}");
                    continue;
                }

                // Assign workflows to the SubDomain
                subDomain.Workflows = workflows;
            }
        }
    }
}

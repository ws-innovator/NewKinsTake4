namespace NewKinsTake4.Data.Models
{
    public class ScenarioModel
    {
        public string? ScenarioCode { get; set; }

        public string? ScenarioName { get; set; }

        public List<WorkflowModel>? Workflows { get; set; }
    }
}

namespace NewKinsTake4.Data.Models
{
    public class SubDomainModel
    {
        public string? SubDomainCode { get; set; }

        public string? SubDomainName { get; set; }

        public List<WorkflowModel>? Workflows { get; set; }
    }
}

namespace NewKinsTake4.Data.Models
{
    public class DomainModel
    {
        public string? DomainCode { get; set; }

        public string? DomainName { get; set; }

        public List<SubDomainModel>? SubDomainNames { get; set; }
    }
}

namespace EmployeeManagement.Models
{
    public class Job
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public string Title { get; set; }
        public int SalaryMax { get; set; }
        public int SalaryMin { get; set; }
    }
}

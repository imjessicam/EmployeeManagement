namespace EmployeeManagement.Models
{
    public class Salary
    {
        public int Id { get; set; }
        public Guid ExternalId { get; set; }
        public int SalaryAmount { get; set; }
        public string From { get; set; }
        public string To { get; set; }

    }
}

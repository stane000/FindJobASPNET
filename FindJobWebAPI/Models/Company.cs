namespace FindJobWebAPI.Models
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal AvgSalary { get; set; }

        public ICollection<Worker>? Workers { get; set; }

        public bool Hiring { get; set; }

        public ICollection<Job>? Jobs { get; set; }

    }
}

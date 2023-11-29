namespace FindJobWebAPI.Dto
{
    public class CompanyDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal AvgSalary { get; set; }

        public bool Hiring { get; set; }

        public ICollection<JobDto> Jobs { get; set; }

        public ICollection<WorkerDto> Workers { get; set; }
    }

}

namespace FindJobWebAPI.Models
{
    public class Job
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public Company? Company { get; set; }

        public Position Position { get; set; }

        public int Experience { get; set; }

        public College College { get; set; }

    }
}

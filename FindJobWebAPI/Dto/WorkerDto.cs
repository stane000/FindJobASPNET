namespace FindJobWebAPI.Dto
{
    public class WorkerDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string College { get; set; }

        public string Position { get; set; }

        public int Experience { get; set; }

        public bool LookingForJob { get; set; }

        public int? CompanyId { get; set; }

        public string? CompanyName { get; set; }


    }

}

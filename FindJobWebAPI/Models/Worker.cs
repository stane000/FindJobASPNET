using System.ComponentModel.DataAnnotations;

namespace FindJobWebAPI.Models
{
    public class Worker
    {
        public int Id { get; set; }

        public int? CompanyId { get; set; }

        public Company? Company { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Range(18, 65, ErrorMessage = "The value must be between 18 and 65.")]
        public int Age { get; set; }

        public College College { get; set; }

        public Position Position { get; set; }

        [Range(0, 35, ErrorMessage = "The value must be between 0 and 35.")]
        public int Experience { get; set; }

        public bool LookingForJob { get; set; }

    }
}

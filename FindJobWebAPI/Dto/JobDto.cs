using FindJobWebAPI.Models;
using System.ComponentModel;

namespace FindJobWebAPI.Dto
{
    public class JobDto
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public decimal CompanyAvgSalary { get; set; }

        public string Position { get; set; }

        public int Experience { get; set; }

        public string College { get; set; }

    }
}

public class JobFilter
{
    [DefaultValue("")]
    public string? Company { get; set; }

    [DefaultValue("")]
    public string? Position { get; set; }

    [DefaultValue(0)]
    public int? Experience { get; set; }

    [DefaultValue("")]
    public string? College { get; set; }
}

public enum JobSort
{
    expSort,
    exoSortDesc,
    AvgSalary,
    AvgSalaryDesc
}
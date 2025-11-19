public class UserDto
{
    public string Role { get; set; } = "";          // "Student" or "Teacher"

    public string UserName { get; set; } = "";
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string Password { get; set; } = "";      // plain for now (you can hash later)

    // Student-specific
    public int? Grade { get; set; }
    public string? Major { get; set; }
    public double? GPA { get; set; }

    // Teacher-specific
    public string? Subject { get; set; }
    public int? YearsOfExperience { get; set; }
    public bool? IsFullTime { get; set; }
}

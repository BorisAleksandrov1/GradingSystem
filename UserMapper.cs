public static class UserMapper
{
    public static UserDto ToDto(User user)
    {
        var dto = new UserDto
        {
            UserName = user.UserName,
            Name = user.Name,
            LastName = user.LastName,
            Password = user.Password
        };

        if (user is Student s)
        {
            dto.Role = "Student";
            dto.Grade = s.Grade;
            dto.Major = s.Major;
            dto.GPA = s.GPA;
        }
        else if (user is Teacher t)
        {
            dto.Role = "Teacher";
            dto.Subject = t.Subject;
            dto.YearsOfExperience = t.YearsOfExperience;
            dto.IsFullTime = t.IsFullTime;
        }

        return dto;
    }

    public static User FromDto(UserDto dto)
    {
        if (dto.Role == "Student")
        {
            var s = new Student
            {
                UserName = dto.UserName,
                Name = dto.Name,
                LastName = dto.LastName,
                Grade = dto.Grade ?? 0,
                Major = dto.Major,
                GPA = dto.GPA ?? 0
            };

            s.Password = dto.Password; // triggers validation
            return s;
        }
        else if (dto.Role == "Teacher")
        {
            var t = new Teacher
            {
                UserName = dto.UserName,
                Name = dto.Name,
                LastName = dto.LastName,
                Subject = dto.Subject,
                YearsOfExperience = dto.YearsOfExperience ?? 0,
                IsFullTime = dto.IsFullTime ?? false
            };

            t.Password = dto.Password;
            return t;
        }

        throw new InvalidOperationException($"Unknown role: {dto.Role}");
    }
}

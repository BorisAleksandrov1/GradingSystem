using System;
using System.Collections.Generic;

public enum UserRole
{
    Student,
    Teacher
}

public static class RegisterService
{
    // Example in-memory store
    private static readonly List<User> _users = new List<User>();

    public static User Register()
    {
        Console.WriteLine("=== Registration ===");

        // 1. Choose role
        UserRole role = AskForRole();

        // 2. Common user data
        int userName = AskForInt("Enter numeric username: ");
        string name = AskForString("Enter first name: ");
        string lastName = AskForString("Enter last name: ");
        string password = AskForPassword();

        // 3. Create specific user type
        User newUser;
        if (role == UserRole.Student)
        {
            newUser = CreateStudent(userName, name, lastName, password);
        }
        else
        {
            newUser = CreateTeacher(userName, name, lastName, password);
        }

        // 4. Store user (optional)
        _users.Add(newUser);

        Console.WriteLine("User registered successfully!\n");
        return newUser;
    }

    private static UserRole AskForRole()
    {
        while (true)
        {
            Console.Write("Are you registering as a Student or Teacher? (s/t): ");
            string? input = Console.ReadLine()?.Trim().ToLower();

            if (input == "s" || input == "student")
                return UserRole.Student;
            if (input == "t" || input == "teacher")
                return UserRole.Teacher;

            Console.WriteLine("Invalid input. Please type 's' for Student or 't' for Teacher.");
        }
    }

    private static int AskForInt(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (int.TryParse(input, out int result))
                return result;

            Console.WriteLine("Please enter a valid integer.");
        }
    }

    private static string AskForString(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();

            Console.WriteLine("This field cannot be empty.");
        }
    }

    private static string AskForPassword()
    {
        while (true)
        {
            Console.Write("Enter password: ");
            string? pwd = Console.ReadLine() ?? string.Empty;

            try
            {
                // Use a temp user to validate logic (or validate later when setting on real user)
                var temp = new DummyUser();
                temp.Password = pwd;
                return pwd; // if no exception, password is valid
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Invalid password: {ex.Message}");
            }
        }
    }

    // Simple dummy just for password validation
    private class DummyUser : User { }

    private static Student CreateStudent(int userName, string name, string lastName, string password)
    {
        Console.WriteLine("--- Student details ---");
        int grade = AskForInt("Enter grade (e.g., 1–12): ");
        string major = AskForString("Enter major (or field of study): ");
        double gpa = AskForDouble("Enter GPA (e.g., 3.5): ");

        var student = new Student
        {
            UserName = userName,
            Name = name,
            LastName = lastName,
            Grade = grade,
            Major = major,
            GPA = gpa
        };

        student.Password = password;
        return student;
    }

    private static Teacher CreateTeacher(int userName, string name, string lastName, string password)
    {
        Console.WriteLine("--- Teacher details ---");
        string subject = AskForString("Enter subject taught: ");
        int years = AskForInt("Enter years of experience: ");
        bool isFullTime = AskForYesNo("Is full-time? (y/n): ");

        var teacher = new Teacher
        {
            UserName = userName,
            Name = name,
            LastName = lastName,
            Subject = subject,
            YearsOfExperience = years,
            IsFullTime = isFullTime
        };

        teacher.Password = password;
        return teacher;
    }

    private static double AskForDouble(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (double.TryParse(input, out double result))
                return result;

            Console.WriteLine("Please enter a valid number.");
        }
    }

    private static bool AskForYesNo(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine()?.Trim().ToLower();

            if (input == "y" || input == "yes")
                return true;
            if (input == "n" || input == "no")
                return false;

            Console.WriteLine("Please answer 'y' or 'n'.");
        }
    }
}

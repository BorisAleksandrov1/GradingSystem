using System;
using System.Collections.Generic;
using System.Linq;

public enum UserRole
{
    Student,
    Teacher
}

public static class AccountService
{
    // ⛔ REMOVE this:
    // private static readonly List<User> _users = new List<User>();

    public static User Register(List<User> users)
    {
        Console.WriteLine("=== SIGN UP ===");

        UserRole role = AskForRole();
        string userName = AskForString("Enter username: ");
        string name = AskForString("Enter first name: ");
        string lastName = AskForString("Enter last name: ");
        string password = AskForPassword();

        User newUser = (role == UserRole.Student)
            ? CreateStudent(userName, name, lastName, password)
            : CreateTeacher(userName, name, lastName, password);

        // ✅ Add to the list that came from Program.cs
        users.Add(newUser);

        Console.WriteLine("Registration successful!\n");
        return newUser;
    }

    public static User? Login(List<User> users)
    {
        Console.WriteLine("=== LOG IN ===");

        while (true)
        {
            string userName = AskForString("Username: ");
            Console.Write("Password: ");
            string password = Console.ReadLine() ?? "";

            // ✅ Search in the list that came from Program.cs
            User? found = users.FirstOrDefault(u =>
                u.UserName == userName &&
                u.Password == password
            );

            if (found != null)
            {
                Console.WriteLine($"Welcome back, {found.Name} {found.LastName} ({found.GetType().Name})!\n");
                return found;
            }

            Console.WriteLine("Invalid username or password.");
            if (!AskForYesNo("Try again? (y/n): "))
                return null;
        }
    }

    // ------------------- Helpers -------------------

    private static UserRole AskForRole()
    {
        while (true)
        {
            Console.Write("Are you registering as Student or Teacher? (s/t): ");
            string? input = Console.ReadLine()?.Trim().ToLower();

            if (input == "s" || input == "student") return UserRole.Student;
            if (input == "t" || input == "teacher") return UserRole.Teacher;

            Console.WriteLine("Please type 's' or 't'.");
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
            string pwd = Console.ReadLine() ?? "";

            try
            {
                var dummy = new DummyUser();
                dummy.Password = pwd;
                return pwd;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Invalid password: {ex.Message}");
            }
        }
    }

    private class DummyUser : User { }

    private static Student CreateStudent(string userName, string name, string lastName, string password)
    {
        Console.WriteLine("--- Student details ---");

        int grade = AskForInt("Enter grade (1-12): ");
        string major = AskForString("Enter major: ");
        double gpa = AskForDouble("Enter GPA: ");

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

    private static Teacher CreateTeacher(string userName, string name, string lastName, string password)
    {
        Console.WriteLine("--- Teacher details ---");

        string subject = AskForString("Enter subject: ");
        int years = AskForInt("Enter years of experience: ");
        bool fullTime = AskForYesNo("Full-time? (y/n): ");

        var teacher = new Teacher
        {
            UserName = userName,
            Name = name,
            LastName = lastName,
            Subject = subject,
            YearsOfExperience = years,
            IsFullTime = fullTime
        };

        teacher.Password = password;
        return teacher;
    }

    private static int AskForInt(string msg)
    {
        while (true)
        {
            Console.Write(msg);
            if (int.TryParse(Console.ReadLine(), out int value))
                return value;

            Console.WriteLine("Please enter a valid number.");
        }
    }

    private static double AskForDouble(string msg)
    {
        while (true)
        {
            Console.Write(msg);
            if (double.TryParse(Console.ReadLine(), out double value))
                return value;

            Console.WriteLine("Please enter a valid number.");
        }
    }

    private static bool AskForYesNo(string msg)
    {
        while (true)
        {
            Console.Write(msg);
            string? response = Console.ReadLine()?.Trim().ToLower();

            if (response == "y" || response == "yes") return true;
            if (response == "n" || response == "no") return false;

            Console.WriteLine("Please answer y/n.");
        }
    }
}

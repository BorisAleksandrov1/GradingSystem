using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<User> users = UserRepository.LoadUsers();

        AccountManagement(users);

        // 2. Save to JSON before exit
        UserRepository.SaveUsers(users);
    }

    public static void AccountManagement(List<User> users)
    {
        Console.WriteLine("=== Welcome to the System ===");

        bool hasAccount = AskForYesNo("Do you have an account? (y/n): ");

        User? currentUser;

        if (hasAccount)
        {
            // LOG IN – uses the SAME list
            currentUser = AccountService.Login(users);

            if (currentUser == null)
            {
                Console.WriteLine("Login cancelled. Exiting...");
                return;
            }
        }
        else
        {
            // SIGN UP / REGISTER – adds to the SAME list
            currentUser = AccountService.Register(users);
        }

        Console.WriteLine($"You are logged in as: {currentUser.GetType().Name}");
        Console.WriteLine($"Username: {currentUser.UserName}, Name: {currentUser.Name} {currentUser.LastName}");
        // here you can continue with your app logic...
    }

    private static bool AskForYesNo(string message)
    {
        while (true)
        {
            Console.Write(message);
            string? input = Console.ReadLine()?.Trim().ToLower();

            if (input == "y" || input == "yes") return true;
            if (input == "n" || input == "no") return false;

            Console.WriteLine("Please answer with 'y' or 'n'.");
        }
    }
}

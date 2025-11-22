using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

public static class UserRepository
{
    private const string FilePath = @"C:\Users\Admin\Codes\CS\Projects\GradingSystem\Users.json";

    public static List<User> LoadUsers()
    {
        if (!File.Exists(FilePath))
            return new List<User>();

        var json = File.ReadAllText(FilePath);
        var dtos = JsonSerializer.Deserialize<List<UserDto>>(json) ?? new List<UserDto>();

        return dtos.Select(UserMapper.FromDto).ToList();
    }

    public static void SaveUsers(List<User> users)
    {
        var dtos = users.Select(UserMapper.ToDto).ToList();

        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var json = JsonSerializer.Serialize(dtos, options);
        File.WriteAllText(FilePath, json);
    }
}

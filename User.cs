using System.Text.RegularExpressions;

public abstract class User
{
    private string _password = string.Empty;

    public string Password
    {
        get { return _password; }
        set
        {
            if (value.Length < 8)
            {
                throw new ArgumentException("Password must be at least 8 characters long.");
            }

            // At least one number
            if (!Regex.IsMatch(value, @"\d"))
            {
                throw new ArgumentException("Password must contain at least one number.");
            }

            // At least one uppercase letter
            if (!Regex.IsMatch(value, @"[A-Z]"))
            {
                throw new ArgumentException("Password must contain at least one uppercase letter.");
            }

            _password = value;
        }
    }

    public string? UserName { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
}
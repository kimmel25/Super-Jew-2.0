using System.Security.Cryptography;

namespace Super_Jew_2._0.Backend.Services;

public static class PasswordHasher
{
    private static byte[]? _salt;
    
    // Generates a salt
    public static string GenerateSalt()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            _salt = new byte[16];
            rng.GetBytes(_salt);
            return Convert.ToBase64String(_salt);
            
        }
    }

    // Hashes a password using SHA256
    public static string HashPassword(string password, string salt)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] saltedPassword = System.Text.Encoding.UTF8.GetBytes(password + salt);
            byte[] hash = sha256.ComputeHash(saltedPassword);
            return Convert.ToBase64String(hash) + ":" + salt;
        }
    }
}
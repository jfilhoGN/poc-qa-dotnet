using System.Data.SqlClient;
using VulnerableApi.Models;

namespace VulnerableApi.Services;

public class DatabaseService
{
    // VULNERABILITY: Hardcoded connection string with credentials
    private const string ConnectionString = "Server=localhost;Database=VulnerableDB;User Id=sa;Password=P@ssw0rd123;";

    public User? GetUserById(string userId)
    {
        // VULNERABILITY: SQL Injection - concatenating user input directly
        string query = $"SELECT * FROM Users WHERE Id = {userId}";

        try
        {
            using var connection = new SqlConnection(ConnectionString);
            using var command = new SqlCommand(query, connection);
            
            connection.Open();
            using var reader = command.ExecuteReader();
            
            if (reader.Read())
            {
                return new User
                {
                    Id = reader.GetInt32(0),
                    Username = reader.GetString(1),
                    Password = reader.GetString(2), // VULNERABILITY: Reading plain text password
                    Email = reader.GetString(3),
                    Role = reader.GetString(4),
                    CreatedAt = reader.GetDateTime(5)
                };
            }
        }
        catch (Exception ex)
        {
            // VULNERABILITY: Exposing internal error details
            Console.WriteLine($"Database error: {ex.Message}");
            throw;
        }

        return null;
    }

    public void ExecuteRawQuery(string query)
    {
        // VULNERABILITY: Allows arbitrary SQL execution
        using var connection = new SqlConnection(ConnectionString);
        using var command = new SqlCommand(query, connection);
        
        connection.Open();
        command.ExecuteNonQuery();
    }
}

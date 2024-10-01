using Microsoft.Data.SqlClient;

namespace DevTdd.Course.UnitTests.Day5;

public class UserByIdQuery
{
    public UserDbRecord? GetUserById(int id)
    {
        var connectionString = "YourConnectionStringHere";
        var query = "SELECT Id, Name, ConfidentialProperty FROM Users WHERE Id = @Id";
        using var connection = new SqlConnection(connectionString);
        var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return new UserDbRecord
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                Name = reader.GetString(reader.GetOrdinal("Name")),
                ConfidentialProperty = reader.GetString(reader.GetOrdinal("ConfidentialProperty"))
            };
        }

        return null;
    }
}
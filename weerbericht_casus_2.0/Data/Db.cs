using MySqlConnector;

namespace WeatherApp;

public class Db
{
    private readonly string _connectionString;

    public Db()
    {
        _connectionString = "Server=localhost;Database=stationgegevens;User ID=rulian;Password=Reawake*Deduct*Mumbling1;";
    }

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connectionString);
    }

    public async Task<MySqlConnection> GetOpenConnectionAsync()
    {
        var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}
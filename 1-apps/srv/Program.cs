// Example usage:
//     dotnet run

using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.MapGet("/", Handler);
app.Run();

async Task<string> Handler(ILogger<Program> logger)
{
    await ExecuteSql("SELECT 1");

    var waitTime = Random.Shared.NextDouble(); // max 1 seconds
    await Task.Delay(TimeSpan.FromSeconds(waitTime));

    logger.LogInformation("Success! Today is: {Date:MMMM dd, yyyy}", DateTimeOffset.UtcNow);

    return "Hello there";
}

async Task ExecuteSql(string sql)
{
    var databasePort = "1433";
    var databasePassword = "yourStrong(!)Password";
    var connectionString = $"Server=127.0.0.1,{databasePort};User=sa;Password={databasePassword};TrustServerCertificate=True;";

    using var connection = new SqlConnection(connectionString);
    await connection.OpenAsync();
    using var command = new SqlCommand(sql, connection);
    using var reader = await command.ExecuteReaderAsync();
}

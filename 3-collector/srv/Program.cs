// Example usage:
//     dotnet run

using Microsoft.Data.SqlClient;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using System.Diagnostics;
using System.Diagnostics.Metrics;

var builder = WebApplication.CreateBuilder(args);

// OTEL: common resource info
var resourceBuilder = ResourceBuilder.CreateDefault().AddService(
    serviceName: "my web service",
    serviceVersion: "0.1.0"
);

// OTEL: setup traces provider
builder.Services.AddOpenTelemetryTracing((configure) => configure
    .SetResourceBuilder(resourceBuilder)
    .AddAspNetCoreInstrumentation()
    .AddSqlClientInstrumentation()
    .AddSource("MyCompany.MyWebService")
    .AddOtlpExporter()
);

// .NET Diagnostics: create the span factory
using var activitySource = new ActivitySource("MyCompany.MyWebService");

// OTEL: setup metrics provider
builder.Services.AddOpenTelemetryMetrics((configure) => configure
    .SetResourceBuilder(resourceBuilder)
    .AddMeter("MyCompany.MyWebService")
    .AddRuntimeInstrumentation()
    .AddOtlpExporter()
);

// OTEL: setup logger provider
builder.Logging.AddOpenTelemetry((configure) => configure
    .SetResourceBuilder(resourceBuilder)
    .AddOtlpExporter()
);

// .NET Diagnostics: create a metric
using var meter = new Meter("MyCompany.MyWebService", "1.0");
var successCounter = meter.CreateCounter<long>("srv.successes.count", description: "Number of successful responses");

var app = builder.Build();
app.MapGet("/", Handler);

app.Run();

async Task<string> Handler(ILogger<Program> logger)
{
    await ExecuteSql("SELECT 1");

    // .NET Diagnostics: create a manual span
    using (var activity = activitySource.StartActivity("SayHello"))
    {
        activity?.SetTag("foo", 1);
        activity?.SetTag("bar", "Hello, World!");
        activity?.SetTag("baz", new int[] { 1, 2, 3 });

        var waitTime = Random.Shared.NextDouble(); // max 1 seconds
        await Task.Delay(TimeSpan.FromSeconds(waitTime));

        activity?.SetStatus(ActivityStatusCode.Ok);

        // .NET Diagnostics: update the metric
        successCounter.Add(1);
    }

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

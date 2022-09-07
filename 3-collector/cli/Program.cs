// Example usage:
//     dotnet run http://localhost:5200

using OpenTelemetry.Trace;
using OpenTelemetry.Resources;

using var tracerProvider = OpenTelemetry.Sdk.CreateTracerProviderBuilder()
    .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(
        serviceName: "my cli app",
        serviceVersion: "0.1.0"
    ))
    .AddHttpClientInstrumentation()
    .AddOtlpExporter()
    .Build();

if (args.Length != 1)
{
    Console.WriteLine(@"URL missing");
    return 2;
}

var uri = args[0];

using var httpClient = new HttpClient();
var content = await httpClient.GetStringAsync(uri);
Console.WriteLine(content);
return 0;
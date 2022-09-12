# Automatic Instrumentation magic

Thanks to the OTel .NET auto-instrumentation we could remove
all `OpenTelemetry` code and package references. Everything is
configurable using env vars.

1. Download appropriate version of
[OpenTelemetry .NET Automatic Instrumentation](https://github.com/open-telemetry/opentelemetry-dotnet-instrumentation/releases/tag/v0.3.1-beta.1)

2. Extract it to the `bin` directory.

3. Run `source envvars.sh` in Bash to initialize auto-instrumentation
in current terminal session.

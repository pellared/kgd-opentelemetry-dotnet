# Automatic Instrumentation magic

Thanks to the OTel .NET auto-instrumentation we could remove
all `OpenTelemetry` code and package references. Everything is
configurable using env vars.

Put the OTel .NET auto-instrumentation under `bin` directory
and run `source envvars.sh` in Bash to initialize the it.
Remember to do so before running the instrumented app.

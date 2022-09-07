# Enter the OTel Collector

We are using OTel Collector as a proxy between to apps
and the backend.

It makes it easier to change the signals ingestion pipeline
in the collector configuration than in the application code.
Especially if you have apps written in many lanagues. Moreover
you can use OTel Collector signal processing capabilities which
are often not implemented in the SDKs.

Now you can see the spans, metrics, logs in the collector stdout
as well as in the backends (Jaeger, Prometheus, log file).

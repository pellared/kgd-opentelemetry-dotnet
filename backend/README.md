# Backend

Content:

- **SQL Server** used by the `srv` app.
- **Jaeger** where the spans are collected.
- **Prometheus** that scraps the metrics.
- **OTel Collector** which can be used as proxy for the signals.

Usage:

- Start: `docker-compose up` (stop: CTRL+C)
- Traces: <http://localhost:16686/search>
- Example metric in Prometheus: [`otelcol_process_runtime_total_alloc_bytes`](http://localhost:9090/graph?g0.expr=otelcol_process_runtime_total_alloc_bytes&g0.tab=0&g0.stacked=0&g0.show_exemplars=0&g0.range_input=5m)
- `srv` app metrics collected by the collector: <http://localhost:8889/metrics>
- Collector metrics: <http://localhost:8888/metrics>
- Collector health: <http://localhost:13133>
- Delete backend: `docker-compose down`

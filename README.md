# .NET Diagnostics for Applications: Best Practices 
![GitHub Actions Workflow Status](https://img.shields.io/github/actions/workflow/status/wodsonluiz/DiagnosticExamples/dotnet.yml)


Repositório para explorar os recursos das APIs de observabilidade do .NET: `EventSource`, `DiagnosticSource`, `OpenTelemetry` e `Distributed Tracing`.([GitHub][1])


#### 🧰 Tecnologias e Conceitos Abordados

* **EventSource**: API para geração de eventos de diagnóstico de alta performance.
* **DiagnosticSource**: Mecanismo para instrumentação leve e coleta de dados de diagnóstico.
* **OpenTelemetry**: Framework para observabilidade, oferecendo suporte a métricas, logs e traces.
* **Distributed Tracing**: Técnicas para rastrear requisições através de múltiplos serviços.
* **Jaeger**: Ferramenta de código aberto para rastreamento distribuído.

##### _Os projetos nesse repositório vai possibilitar esse laboratório_:
![Screenshot 2025-05-19 at 22 20 36](https://github.com/user-attachments/assets/2a90eb0f-9191-41cf-a658-f6a89b59ee0b)


### 📁 Estrutura do Repositório

```plaintext
DiagnosticExamples/
├── .github/workflows/      # Configurações de CI/CD
├── resources/              # Arquivos de apoio e documentação
├── src/                    # Código-fonte dos exemplos
│   ├── EventSourceDemo/        # Exemplo utilizando EventSource
│   ├── DiagnosticSourceDemo/   # Exemplo utilizando DiagnosticSource
│   ├── OpenTelemetryDemo/      # Exemplo utilizando OpenTelemetry
│   └── DistributedTracingDemo/ # Exemplo de rastreamento distribuído
├── DiagnosticExamples.sln  # Solução do Visual Studio
└── README.md               # Este arquivo
```


### 📚 Referencias

* [Documentação do EventSource](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.tracing.eventsource)
* [Documentação do DiagnosticSource](https://learn.microsoft.com/en-us/dotnet/api/system.diagnostics.diagnosticsource)
* [OpenTelemetry .NET](https://opentelemetry.io/docs/instrumentation/net/)
* [Jaeger Tracing](https://www.jaegertracing.io/)

---


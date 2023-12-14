<div align="center">
    <img alt='aspire logo' src='./docs/images/aspire.svg' />
</div>

# .NET Aspire

.NET Aspire is based on a previous experiment from Microsoft called [`Project Tye`](https://devblogs.microsoft.com/dotnet/introducing-project-tye/) which was release around May 2020.

The goal of .NET Aspire is to improve the local development experience for Distributed Applications (aka microservices). In a nutshell, .NET Aspire helps to improve:

* Orchestration - one-click to start *all-the-things*
  * [Distributed Application Host orchestration](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/app-host-overview)
* Observability - we get logs, traces and metrics all in one place for all apps
  * [Telemetry](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/telemetry)
* Health Checks and Resiliency
  * [Health Checks](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/health-checks)
  * [HTTP resiliency](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/service-defaults#add-service-defaults-functionality)
* Components - facilitate the integration of cloud-native applications with prominent services and platforms such as Redis, Azure SQL, CosmosDb, Azure ServiceBus, RabbitMQ, Postgresql, etc.
  * [Full list of Available components](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/components-overview?tabs=dotnet-cli#available-components)

The great thing about a .NET Aspire orchestrated application is that we can add all the Projects / Services and Components that make up our overall microservices system into one project and have a way to describe how the applications are connected or related to each other.

```csharp
var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedisContainer("cache");
var heroApi = builder.AddProject<Projects.AspiringHeroes_Api>("heroesRestAPI");
var heroGrpc = builder.AddProject<Projects.AspiringHeroes_Grpc>("heroesGrpcService");

builder.AddProject<Projects.AspiringHeroes_Web>("webUI")
    .WithReference(cache)
    .WithReference(heroApi)
    .WithReference(heroGrpc);

builder.Build().Run();
```

The `AppHost` and [`ServiceDefaults`](https://learn.microsoft.com/en-us/dotnet/aspire/fundamentals/service-defaults) is responsible for all configuration that enables the health checking, resiliency, service discovery, telemetry and more.

*Figure: Using C# to describe the projects and components in the distributed system and how they are related to each other.*

![.NET Aspire Dashboard](/docs/images/aspire-dashboard.png)
*Figure: Aspire Dashboard - Projects View*

![.NET Aspire Dashboard - Traces](/docs/images/aspire-dashboard-traces.png)
*Figure: Aspire Dashboard - Traces View*

![.NET Aspire Dashboard - Traces - details](/docs/images/aspire-dashboard-traces-details.png)
*Figure: Aspire Dashboard - Trace Details View*

## ✅ Pros

* The orchestration is great - one project to start and connect everything is a massive time saver
* .NET Aspire tooling in Visual Studio is great
  * Quickly add Aspire Components (via nuget package manager)
  * Couple of clicks to add Orchestration support (generates some code for us)
  * All the logs in one place
  * Traces - wow... now we can see exactly how our applications were interacting with each other
* Deployments with Azure Dev CLI (AZD) is very easy

## ❌ Cons

* For local development only (for now?)
  * Would be great to have the same dashboard deployed to Azure
  * Although, with ACA, Azure Monitor, Promethius, Graphana etc. we still get the awesome observability we are used to
* All projects need to be part of the same solution
* Couldn't find any way to add .NET Aspire Components or Orchestration from the dotnet CLI
* No "restart" option available on individual projects
* No easy scriptable functionality (like Project Tye) to define the startup arguments for individual services - currently discovered via `launchSettings.json`
* No deployment / publishing options from Visual Studio (for now it is on purpose)
* Application Insights not provided OOTB, we need to add it ourselves (see [Use Application Insights for .NET Aspire telemetry](https://learn.microsoft.com/en-us/dotnet/aspire/deployment/azure/application-insights))

## Deployment

.NET Aspire allows for flexible deployment of applications across platforms that support .NET and containers. It generates a manifest file to help deployment tools understand the app's structure, which includes information about the projects and dependant services along with the necessary properties for deployment like environment variables and network bindings.

In summary, .NET Aspire simplifies app deployment across various platforms. It works well with containerized environments and integrates with tools like [AZD](https://learn.microsoft.com/en-us/azure/developer/azure-developer-cli/install-azd) and [Aspirate](https://github.com/prom3theu5/aspirational-manifests) for a streamlined deployment process.

## Deploy to Azure Container Apps (ACA)

[Azure Container Apps (ACA)](https://learn.microsoft.com/en-us/dotnet/aspire/deployment/azure/aca-deployment-azd-in-depth) is a great choice for hosting .NET Aspire applications. ACA is a managed environment for running containerised applications on a serverless platform.

The Azure Developer CLI (AZD) is used to generate an Azure Bicep, a language for describing and deploying Azure resources. This is used to provision Azure Container Apps resources needed to host .NET Aspire applications. AZD can also directly deploy the application to Azure Container Apps.

* [Deploying .NET Aspire Applications to Azure Container Apps (in-depth)]((https://learn.microsoft.com/en-us/dotnet/aspire/deployment/azure/aca-deployment-azd-in-depth))
* [Manifest Format](https://learn.microsoft.com/en-us/dotnet/aspire/deployment/manifest-format)

Check out the [in-depth guide](https://learn.microsoft.com/en-us/dotnet/aspire/deployment/azure/aca-deployment-azd-in-depth?tabs=windows#generate-bicep-from-net-aspire-app-model) for how to synthesize the Azure Bicep file that you can use as part of your GitHub workflow / Azure DevOps pipeline to automate everything.

### Quick deployment from local to ACA

Using AZD, the steps to get a .NET Aspire application deployed from local to Azure Container Apps can be distilled to the following 3 commands:

```sh
azd auth login
azd init
azd up
```

Quite simple right? Ofcourse we would really want to take it further and use automated CI/CD workflows to do the work for us.

## Deploy to Azure Kubernetes Service (AKS)

Azure Kubernetes Service (AKS) is an managed environment for running container applications with advanced networking and orchestration capabilities.

Using the [Aspirate](https://github.com/prom3theu5/aspirational-manifests) tool we can generate Kubernetes Manifest files for each component in our .NET Aspire application and eventually push the application containers to a container registry and then deploy to pods on a AKS/K8s cluster.

For detailed steps on how to deploy your application to AKS/K8s, check out the [Deploy .NET Aspire apps to a Kubernetes cluster](https://learn.microsoft.com/en-us/dotnet/aspire/deployment/k8s-deployment) page from MS Learn (its a lot easier than you think).

## Links / Resources

* .NET Aspire announcement: https://devblogs.microsoft.com/dotnet/announcing-dotnet-8

* Aspire Samples on GitHub: https://github.com/dotnet/aspire-samples

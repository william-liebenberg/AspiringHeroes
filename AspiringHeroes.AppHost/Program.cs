var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedisContainer("cache");
var heroApi = builder.AddProject<Projects.AspiringHeroes_Api>("heroesRestAPI");
var heroGrpc = builder.AddProject<Projects.AspiringHeroes_Grpc>("heroesGrpcService");

builder.AddProject<Projects.AspiringHeroes_Web>("webUI")
    .WithReference(cache)
    .WithReference(heroApi)
    .WithReference(heroGrpc);

builder.Build().Run();

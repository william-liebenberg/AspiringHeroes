using AspiringHeroes.Grpc.Services;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.Services.AddGrpc();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();

app.UseRouting();

app.UseResponseCompression();

app.MapDefaultEndpoints();

app.UseGrpcWeb(new GrpcWebOptions() {  DefaultEnabled = true });

app.MapGrpcService<HeroScheduleService>();

app.Run();

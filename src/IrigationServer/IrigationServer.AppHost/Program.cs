var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.IrigationServer_ApiService>("apiservice");

builder.AddProject<Projects.IrigationServer_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();

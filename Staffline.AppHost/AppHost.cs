var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Staffline_Api>("staffline-api");

builder.Build().Run();

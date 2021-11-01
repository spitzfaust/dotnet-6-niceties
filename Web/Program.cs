using Microsoft.AspNetCore.Mvc;
using WorkOrganization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<WorkOrganizer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/work-items",
        ([FromServices] WorkOrganizer workOrganizer) => workOrganizer.Backlog)
    .WithName("GetWorkItems");

app.MapPost("/work-items",
    ([FromBody] CreateWorkItemRequest request, [FromServices] WorkOrganizer workOrganizer)
        => workOrganizer.AddNew(request.WorkCategory));

app.MapGet("/work-items/{id:int}",
    (int id, [FromServices] WorkOrganizer workOrganizer) =>
    {
        var item = workOrganizer.GetById(id);
        if (item is null)
        {
            return Results.NotFound();
        }

        return item;
    });

app.Run();

record CreateWorkItemRequest(WorkOrganizer.WorkCategory WorkCategory);

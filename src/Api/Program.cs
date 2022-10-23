using System.Text.Json.Serialization;
using Api.Filters;
using Application.ErrorBag;
using Application.TodoAggregate;
using Application.TodoAggregate.Request;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers(options => {
    options.Filters.Add<ErrorFilter>(int.MaxValue -10);
}).AddJsonOptions(opts => {
        var enumConverter = new JsonStringEnumConverter();
        opts.JsonSerializerOptions.Converters.Add(enumConverter);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTodo(builder.Configuration);
builder.Services.AddTransient<IValidator<TodoRaiseRequest>, TodoRaiseValidator>();
builder.Services.AddTransient<IValidator<TodoUpdateRequest>, TodoUpdateValidator>();
builder.Services.AddScoped<IErrorBagHandler, ErrorBagHandler> ();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

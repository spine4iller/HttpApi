using Api.Infra.DataProviders;
using Api.Infra.HttpClients;
using Api.Infra.Interfaces;
using Api.Infra.Options;
using Api.Infra.Profiles;
using Api.Infra.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddApiVersioning();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile))
    .AddSingleton<ICacheDataProvider, InMemoryCacheDataProvider>()
    .AddScoped<ISearchService, SearchService>()
    .AddScoped<FirstApiDataProvider>()
    .AddScoped<SecondApiDataProvider>()
    .AddScoped<FirstApiClient>()
    .AddScoped<SecondApiClient>();

builder.Services.Configure<FirstApiClientOptions>(builder.Configuration.GetSection(
    nameof(FirstApiClientOptions)));
builder.Services.Configure<SecondApiClientOptions>(builder.Configuration.GetSection(
    nameof(SecondApiClientOptions)));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();

using DAL.EntityModels;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.IRepos;
using Repository.Repos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var provider = builder.Services.BuildServiceProvider();
var config = provider.GetService<IConfiguration>();
builder.Services.AddDbContext<EMS_ITCContext>(item => item.UseSqlServer(config.GetConnectionString("DC")));
builder.Services.AddScoped<IAccountRepos,AccountRepos>();
builder.Services.AddScoped<IDistributorRepos, DistributorRepos>();
builder.Services.AddScoped<IProductRepos, ProductRepos>();
builder.Services.AddScoped<IWarehouseRepos,WarehouseRepos>();
builder.Services.AddScoped<ICustomerRepos, CustomerRepos>();
builder.Services.AddScoped<IBranchrepos, BranchRepos>();
var app = builder.Build();





// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

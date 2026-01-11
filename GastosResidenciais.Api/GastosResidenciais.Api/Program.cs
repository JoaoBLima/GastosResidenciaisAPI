using GastosResidenciais.Api.DataContext;
using GastosResidenciais.Api.Service.CategoriaService;
using GastosResidenciais.Api.Service.PessoaService;
using GastosResidenciais.Api.Service.RelatorioCategoriaService;
using GastosResidenciais.Api.Service.RelatorioService;
using GastosResidenciais.Api.Service.TransacaoService;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPessoaInterface, PessoaService>();
builder.Services.AddScoped<ICategoriaInterface, CategoriaService>();
builder.Services.AddScoped<ITransacaoInterface, TransacaoService>();
builder.Services.AddScoped<IRelatorioInterface, RelatorioService>();
builder.Services.AddScoped<IRelatorioCategoriaInterface, RelatorioCategoriaService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();
app.UseCors("AllowReact");

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

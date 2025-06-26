using System.Text.Json.Serialization;
using backend.DTO;
using backend.Helpers;
using backend.Interfaces;
using backend.Repository;
using backend.Validators;
using Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Default")));

// TODO: Configurar de forma correta no momento de mover para produção
builder.Services.AddCors(options =>
{
    options.AddPolicy("Default", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSingleton<IPaginationHelper, PaginationHelper>();

builder.Services.AddScoped<IValidator<ChamadoPostDTO>, ChamadoValidator>();
builder.Services.AddScoped<IValidator<ChamadoAcompanhamentoPostDTO>, ChamadoAcompanhamentoValidator>();
builder.Services.AddScoped<IValidator<EquipamentoPostDTO>, EquipamentoValidator>();
builder.Services.AddScoped<IValidator<EstabelecimentoPostDTO>, EstabelecimentoValidator>();
builder.Services.AddScoped<IValidator<SetorPostDTO>, SetorValidator>();
builder.Services.AddScoped<IValidator<UsuarioPostDTO>, UsuarioValidator>();

builder.Services.AddScoped<IChamadoAcompanhamentoRepository, ChamadoAcompanhamentoRepository>();
builder.Services.AddScoped<IChamadoAtendimentoRepository, ChamadoAtendimentoRepository>();
builder.Services.AddScoped<IChamadoRepository, ChamadoRepository>();
builder.Services.AddScoped<IEquipamentoRepository, EquipamentoRepository>();
builder.Services.AddScoped<IEstabelecimentoRepository, EstabelecimentoRepository>();
builder.Services.AddScoped<ISetorRepository, SetorRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddControllers().AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Default");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

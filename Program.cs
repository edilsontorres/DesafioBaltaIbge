using Desafio_Balta.Data;
using Desafio_Balta.Models;
using Desafio_Balta.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddDbContext<DadosIbgeContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DataBaseConnetion")));
builder.Services.AddScoped<TokenService>();
builder.AddAuthJWT();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();



app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

//Registro usuário
app.MapPost("/user/register", [AllowAnonymous] async (User user, DadosIbgeContext context) =>
{
    if(user != null)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return Results.Ok("Usuário cadastrado com sucesso");
    }
    return Results.BadRequest("Dados incorretos!");

}).WithTags("Usuário");

//Login usuário
app.MapPost("/user/login", [AllowAnonymous] async (User user, TokenService tokenService, DadosIbgeContext context) =>
{
    var users = await context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
    if(users != null)
    {
        if (user.Password == users.Password)
        {
            var token = await tokenService.CreateToken(users);
            return Results.Ok(token);
        }   
    }
    return Results.NotFound("Usuário e/ou senha inválidos!");

}).WithTags("Usuário");



//Listando todos os dados do DB
app.MapGet("/ibge", [AllowAnonymous] async (DadosIbgeContext context) =>
{
    return await context.Ibges.ToListAsync();

}).WithTags("IBGE");

//Pesquisa por cidade
app.MapGet("ibge/pesqCidade/{cidade}", [AllowAnonymous] async (string cidade, DadosIbgeContext context) =>
{
    var cidades = context.Ibges.Where(c => c.City.ToLower().Contains(cidade.ToLower())).ToList();
    if (cidades != null)
    {
        return Results.Ok(cidades);
    }
    return Results.NotFound("Dados não encontrado");

}).WithTags("IBGE");

//Pesquisa por Estado
app.MapGet("/ibge/pesqEstado/{estado}", [AllowAnonymous] (string estado, DadosIbgeContext context) => 
{
    var estados = context.Ibges.Where(e => e.State.ToLower().Contains(estado.ToLower())).ToList();
    if(estados != null)
    {
        return Results.Ok(estados);
    }
    return Results.NotFound("Dados não encontrado");

}).WithTags("IBGE");

//Pesquisa por código IBGE
app.MapGet("/ibge/pesqIbge/{ibge}", [AllowAnonymous] async (string ibge, DadosIbgeContext context) => 
{
    var dados = await context.Ibges.FirstOrDefaultAsync(x => x.Id == ibge);
    if (dados != null)
    {
        return Results.Ok(dados);
    }
    return Results.NotFound("Dados não encontrados");

}).WithTags("IBGE");

//Adicionando um novo registro IBGE (rota protegida, somente usuários autenticados podem acessar) 
app.MapPost("/ibge/create", async (Ibge ib, DadosIbgeContext context, ClaimsPrincipal user) =>
{
    context.Ibges.Add(ib);
    await context.SaveChangesAsync();
    return Results.Ok(await context.Ibges.ToListAsync());

}).RequireAuthorization()
  .WithTags("IBGE");


//Atualizando dados do IBGE (rota protegida, somente usuários autenticados podem acessar)
app.MapPut("ibge/{id}", async (string id, DadosIbgeContext context, Ibge ib) =>
{
    var ibge = await context.Ibges.FindAsync(id);
    if (ibge != null)
    {
        ibge.State = ib.State;
        ibge.City = ib.City;
        await context.SaveChangesAsync();
        return Results.Ok(ib);
    }
    return Results.NotFound("Dados não encontrado");

}).RequireAuthorization()
  .WithTags("IBGE");

//Deletando dados do IBGE (rota protegida, somente usuários administradores autenticados podem acessar)
app.MapDelete("ibge/{id}", async (string id, DadosIbgeContext context) =>
{
    var ibge = await context.Ibges.FindAsync(id);
    if (ibge != null)
    {
        context.Ibges.Remove(ibge);
        await context.SaveChangesAsync();
        return Results.Ok("Registro deletado com sucesso!");
    }

    return Results.NotFound("Dados não encontrado!");

}).RequireAuthorization("Admin")
  .WithTags("IBGE");

app.Run();



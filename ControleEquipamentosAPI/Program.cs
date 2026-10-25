
using ControleEquipamentosAPI.Data;
using ControleEquipamentosAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=equipamentos.db"));

var app = builder.Build();

// Página inicial
app.MapGet("/", () => Results.Ok(new
{
    mensagem = "API de Controle de Equipamentos e Manutenções",
    status = "Funcionando"
}));

// ==========================================
// EQUIPAMENTOS
// ==========================================

// POST: cadastrar equipamento
app.MapPost("/equipamentos", async (
    Equipamento equipamento,
    AppDbContext db) =>
{
    equipamento.Id = 0;

    db.Equipamentos.Add(equipamento);
    await db.SaveChangesAsync();

    return Results.Created(
        $"/equipamentos/{equipamento.Id}",
        equipamento);
});

// GET: listar equipamentos
app.MapGet("/equipamentos", async (
    AppDbContext db) =>
{
    var equipamentos = await db.Equipamentos
        .AsNoTracking()
        .ToListAsync();

    return Results.Ok(equipamentos);
});

// GET: consultar equipamento por ID
app.MapGet("/equipamentos/{id:int}", async (
    int id,
    AppDbContext db) =>
{
    var equipamento = await db.Equipamentos
        .AsNoTracking()
        .Include(e => e.Manutencoes)
        .FirstOrDefaultAsync(e => e.Id == id);

    if (equipamento is null)
        return Results.NotFound();

    return Results.Ok(equipamento);
});

// PUT: alterar equipamento
app.MapPut("/equipamentos/{id:int}", async (
    int id,
    Equipamento dados,
    AppDbContext db) =>
{
    var equipamento = await db.Equipamentos.FindAsync(id);

    if (equipamento is null)
        return Results.NotFound();

    equipamento.Nome = dados.Nome;
    equipamento.NumeroPatrimonio = dados.NumeroPatrimonio;
    equipamento.Localizacao = dados.Localizacao;
    equipamento.Status = dados.Status;

    await db.SaveChangesAsync();

    return Results.Ok(equipamento);
});

// DELETE: excluir equipamento
app.MapDelete("/equipamentos/{id:int}", async (
    int id,
    AppDbContext db) =>
{
    var equipamento = await db.Equipamentos.FindAsync(id);

    if (equipamento is null)
        return Results.NotFound();

    db.Equipamentos.Remove(equipamento);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

// ==========================================
// MANUTENÇÕES
// ==========================================

// POST: cadastrar manutenção
app.MapPost("/manutencoes", async (
    Manutencao manutencao,
    AppDbContext db) =>
{
    var existe = await db.Equipamentos
        .AnyAsync(e => e.Id == manutencao.EquipamentoId);

    if (!existe)
    {
        return Results.BadRequest(new
        {
            mensagem = "O equipamento informado não existe."
        });
    }

    manutencao.Id = 0;

    db.Manutencoes.Add(manutencao);
    await db.SaveChangesAsync();

    return Results.Created(
        $"/manutencoes/{manutencao.Id}",
        manutencao);
});

// GET: listar manutenções
app.MapGet("/manutencoes", async (
    AppDbContext db) =>
{
    var manutencoes = await db.Manutencoes
        .AsNoTracking()
        .ToListAsync();

    return Results.Ok(manutencoes);
});

// GET: consultar manutenção por ID
app.MapGet("/manutencoes/{id:int}", async (
    int id,
    AppDbContext db) =>
{
    var manutencao = await db.Manutencoes
        .AsNoTracking()
        .FirstOrDefaultAsync(m => m.Id == id);

    if (manutencao is null)
        return Results.NotFound();

    return Results.Ok(manutencao);
});

// PUT: alterar manutenção
app.MapPut("/manutencoes/{id:int}", async (
    int id,
    Manutencao dados,
    AppDbContext db) =>
{
    var manutencao = await db.Manutencoes.FindAsync(id);

    if (manutencao is null)
        return Results.NotFound();

    var existe = await db.Equipamentos
        .AnyAsync(e => e.Id == dados.EquipamentoId);

    if (!existe)
    {
        return Results.BadRequest(new
        {
            mensagem = "O equipamento informado não existe."
        });
    }

    manutencao.Descricao = dados.Descricao;
    manutencao.Data = dados.Data;
    manutencao.Status = dados.Status;
    manutencao.EquipamentoId = dados.EquipamentoId;

    await db.SaveChangesAsync();

    return Results.Ok(manutencao);
});

// DELETE: excluir manutenção
app.MapDelete("/manutencoes/{id:int}", async (
    int id,
    AppDbContext db) =>
{
    var manutencao = await db.Manutencoes.FindAsync(id);

    if (manutencao is null)
        return Results.NotFound();

    db.Manutencoes.Remove(manutencao);
    await db.SaveChangesAsync();

    return Results.NoContent();
});

// GET: histórico de manutenções por equipamento
app.MapGet("/equipamentos/{id:int}/manutencoes", async (
    int id,
    AppDbContext db) =>
{
    var existe = await db.Equipamentos
        .AnyAsync(e => e.Id == id);

    if (!existe)
        return Results.NotFound();

    var historico = await db.Manutencoes
        .AsNoTracking()
        .Where(m => m.EquipamentoId == id)
        .ToListAsync();

    return Results.Ok(historico);
});

app.Run();



using System;
using System.Text.Json.Serialization;

namespace ControleEquipamentosAPI.Models;

public class Manutencao
{
    public int Id { get; set; }

    public string Descricao { get; set; } = string.Empty;

    public DateTime Data { get; set; } = DateTime.UtcNow;

    public string Status { get; set; } = "Pendente";

    public int EquipamentoId { get; set; }

    [JsonIgnore]
    public Equipamento? Equipamento { get; set; }
}

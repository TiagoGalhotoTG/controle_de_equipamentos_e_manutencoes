
namespace ControleEquipamentosAPI.Models;

public class Equipamento
{
    public int Id { get; set; }

    public string Nome { get; set; } = string.Empty;

    public string NumeroPatrimonio { get; set; } = string.Empty;

    public string Localizacao { get; set; } = string.Empty;

    public string Status { get; set; } = "Ativo";

    public ICollection<Manutencao> Manutencoes { get; set; }
        = new List<Manutencao>();
}

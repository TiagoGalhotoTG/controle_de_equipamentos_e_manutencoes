namespace ControleEquipamentosAPI.Models;

public class Equipamento
{
    /*Identificador único do equipamento.*/
    public int Id {get; set;}

    /*Nome, como notebook ou impressora.*/
    public string Nome {get; set;} = string.Empty;

    /*Código patrimonial.*/
    public string NumeroPatrimonio {get; set;} = string.Empty;

    /*local onde o equipamento está instalado.*/
    public string Localização {get; set;} = string.Empty;

    /*Status situação atual do equipamento.*/
    public string Status {get; set;} = "Ativo";

    /*Manutenções coleção dos registros relacionados ao equipamento.*/
    public ICollection<Manutencao> manutencaos {get; set;} = new List<Manutencao>();
}

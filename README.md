Controle de Equipamentos e Manutenções
1. Descrição do projeto
Projeto acadêmico desenvolvido em C# com .NET 8 Minimal API para controlar equipamentos e registrar as manutenções realizadas ou pendentes.

A API permite cadastrar, consultar, atualizar e excluir equipamentos e manutenções, além de consultar o histórico de manutenções de um equipamento específico.
2. Tecnologias utilizadas
- C#
- .NET 8 Minimal API
- ASP.NET Core
- Entity Framework Core
- SQLite
- REST e JSON
- Git e GitHub
- Postman para testes das requisições HTTP
3. Entidades e relacionamento
Equipamento
Representa um equipamento cadastrado no sistema.

Campos:
- Id: identificador do equipamento.
- Nome: nome do equipamento.
- NumeroPatrimonio: número de patrimônio.
- Localizacao: local onde o equipamento está instalado.
- Status: situação atual do equipamento.
- Manutencoes: coleção de manutenções relacionadas.
Manutencao
Representa uma manutenção relacionada a um equipamento.

Campos:
- Id: identificador da manutenção.
- Descricao: descrição do serviço.
- Data: data e hora da manutenção.
- Status: situação da manutenção.
- EquipamentoId: chave estrangeira que identifica o equipamento associado.
Relacionamento
O sistema utiliza um relacionamento um para muitos (1:N):
- Um equipamento pode possuir várias manutenções.
- Cada manutenção pertence a um único equipamento.
- EquipamentoId é a chave estrangeira na tabela de manutenções.
A configuração atual utiliza exclusão em cascata: ao excluir um equipamento, suas manutenções relacionadas também são excluídas.

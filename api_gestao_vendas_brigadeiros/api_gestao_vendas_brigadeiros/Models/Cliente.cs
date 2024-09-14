using System.Data.SqlClient;

namespace api_gestao_vendas_brigadeiros.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco {  get; set; }
        public string Email { get; set; }
        public DateTime DataCadastro { get; set; }


        public Cliente() { }

        public Cliente(SqlDataReader reader)
        {
            Id = reader.GetInt32(reader.GetOrdinal("IdCliente"));
            Nome = reader.GetString(reader.GetOrdinal("Nome"));
            Telefone = reader.GetString(reader.GetOrdinal("Telefone"));
            Endereco = reader.GetString(reader.GetOrdinal("Endereco"));
            Email = reader.GetString(reader.GetOrdinal("Email"));
            DataCadastro = reader.GetDateTime(reader.GetOrdinal("DataCadastro"));
        }
    }
}

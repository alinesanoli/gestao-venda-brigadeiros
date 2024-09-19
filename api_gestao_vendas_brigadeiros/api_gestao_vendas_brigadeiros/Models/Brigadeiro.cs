using System.Data.SqlClient;

namespace api_gestao_vendas_brigadeiros.Models
{
    public class Brigadeiro
    {
        public int IdBrigadeiro { get; set; }
        public string Sabor { get; set; }

        public Brigadeiro(SqlDataReader reader)
        {
            IdBrigadeiro = reader.GetInt32(reader.GetOrdinal("IdBrigadeiro"));
            Sabor = reader.GetString(reader.GetOrdinal("Sabor"));
        }
    }
}

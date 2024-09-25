using System.Data.SqlClient;

namespace api_gestao_vendas_brigadeiros.Models
{
    public class EncomendaBrigadeiro
    {
        public int IdEncomenda { get; set; }
        public int IdBrigadeiro { get; set; }
        public int Quantidade { get; set; }


        public EncomendaBrigadeiro() { }

        public EncomendaBrigadeiro(SqlDataReader reader)
        {
            IdEncomenda = reader.GetInt32(reader.GetOrdinal("IdEncomenda"));
            IdBrigadeiro = reader.GetInt32(reader.GetOrdinal("IdBrigadeiro"));
            Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"));
        }
    }
}

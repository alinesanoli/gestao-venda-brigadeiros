using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace api_gestao_vendas_brigadeiros.Models
{
    public class VendaBrigadeiro
    {
        public int IdVenda { get; set; }
        public int IdBrigadeiro { get; set; }
        public int Quantidade { get; set; }
        

        public VendaBrigadeiro() { }

        public VendaBrigadeiro(SqlDataReader reader)
        {
            IdVenda = reader.GetInt32(reader.GetOrdinal("IdVenda"));
            IdBrigadeiro = reader.GetInt32(reader.GetOrdinal("IdBrigadeiro"));
            Quantidade = reader.GetInt32(reader.GetOrdinal("Quantidade"));
        }
    }
}

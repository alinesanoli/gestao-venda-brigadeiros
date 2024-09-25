using System.Data.SqlClient;

namespace api_gestao_vendas_brigadeiros.Models
{
    public class Encomenda
    {
        public int IdEncomenda { get; set; }
        public int IdCliente { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public DateTime DataEntrega { get; set; }
        public decimal ValorTotal { get; set; }
        public string FormaPagamento {get; set;}
        public List<EncomendaBrigadeiro> EncomendaBrigadeiro { get; set; }

        public Encomenda() { }

        public Encomenda(SqlDataReader reader)
        {
            IdEncomenda = reader.GetInt32(reader.GetOrdinal("IdEncomenda"));
            IdCliente = reader.GetInt32(reader.GetOrdinal("IdCliente"));
            DataSolicitacao = reader.GetDateTime(reader.GetOrdinal("DataSolicitacao"));
            DataEntrega = reader.GetDateTime(reader.GetOrdinal("DataEntrega"));
            ValorTotal = reader.GetDecimal(reader.GetOrdinal("ValorTotal"));
            FormaPagamento = reader.GetString(reader.GetOrdinal("FormaPagamento"));
            EncomendaBrigadeiro = new List<EncomendaBrigadeiro>();

        }
    }
}

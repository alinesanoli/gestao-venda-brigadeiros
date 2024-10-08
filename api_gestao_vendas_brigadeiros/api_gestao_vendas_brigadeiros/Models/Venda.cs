﻿

using System.Data;
using System.Data.SqlClient;


namespace api_gestao_vendas_brigadeiros.Models
{
    public class Venda
    {
        public int IdVenda { get; set; }
        public int IdCliente { get; set; }
        public DateTime DataVenda { get; set; }
        public decimal ValorTotal { get; set; }
        public string FormaPagamento { get; set; }
        public List<VendaBrigadeiro> VendaBrigadeiro { get; set; }


        public Venda() { }

        public Venda(SqlDataReader reader)
        {
            IdVenda = reader.GetInt32(reader.GetOrdinal("IdVenda"));
            IdCliente = reader.GetInt32(reader.GetOrdinal("IdCliente"));
            DataVenda = reader.GetDateTime(reader.GetOrdinal("DataVenda"));
            ValorTotal = reader.GetDecimal(reader.GetOrdinal("ValorTotal"));
            FormaPagamento = reader.GetString(reader.GetOrdinal("FormaPagamento"));

            VendaBrigadeiro = new List<VendaBrigadeiro>();
        }
    }
}

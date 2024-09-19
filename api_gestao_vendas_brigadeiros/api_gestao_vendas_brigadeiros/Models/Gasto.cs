using System.Data.SqlClient;

namespace api_gestao_vendas_brigadeiros.Models
{
    public class Gasto
    {
        public int IdGasto { get; set; }
        public string Insumo { get; set; }
        public decimal ValorUnitario { get; set; }
        public int QuantidadeComprada {  get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataCompra { get; set; }

        public Gasto() { }

        public Gasto(SqlDataReader reader)
        {
            IdGasto = reader.GetInt32(reader.GetOrdinal("IdGasto"));
            Insumo = reader.GetString(reader.GetOrdinal("Insumo"));
            ValorUnitario = reader.GetDecimal(reader.GetOrdinal("ValorUnitario"));
            QuantidadeComprada = reader.GetInt32(reader.GetOrdinal("QuantidadeComprada"));
            ValorTotal = reader.GetDecimal(reader.GetOrdinal("ValorTotal"));
            DataCompra = reader.GetDateTime(reader.GetOrdinal("DataCompra"));


        }

    }
}

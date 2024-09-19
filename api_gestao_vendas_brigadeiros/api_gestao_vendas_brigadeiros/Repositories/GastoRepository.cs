using api_gestao_vendas_brigadeiros.Models;
using System.Data;
using System.Data.SqlClient;

namespace api_gestao_vendas_brigadeiros.Repositories
{
    public class GastoRepository
    {

        private readonly SqlConnection connection;

        public GastoRepository(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DbGestaoBrigadeiros") ??
                throw new Exception("ConnectionString sem valor válido");
            connection = new SqlConnection(connectionString);
        }
        public int InserirGasto(Gasto gasto)
        {
            try
            {
                connection.Open();
                string sql = "INSERT INTO Gastos (Insumo, ValorUnitario, QuantidadeComprada, DataCompra)" +
                    " VALUES (@Insumo, @ValorUnitario, @QuantidadeComprada, @DataCompra)";
                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@Insumo", gasto.Insumo);
                command.Parameters.AddWithValue("@ValorUnitario", gasto.ValorUnitario);
                command.Parameters.AddWithValue("@QuantidadeComprada", gasto.QuantidadeComprada);
                command.Parameters.AddWithValue("@DataCompra", gasto.DataCompra);


                return command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public IEnumerable<Gasto>? BuscarTodosOsGastos()
        {
            try
            {
                connection.Open();

                string sql = @"SELECT * FROM Gastos";

                using SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;

                using SqlDataReader reader = command.ExecuteReader();

                List<Gasto> listaRetorno = new List<Gasto>();

                while (reader.Read())
                {
                    Gasto gasto = new Gasto(reader);
                    listaRetorno.Add(gasto);
                }
                return listaRetorno;

            }
            finally
            {
                connection.Close();
            }
        }
    }
}

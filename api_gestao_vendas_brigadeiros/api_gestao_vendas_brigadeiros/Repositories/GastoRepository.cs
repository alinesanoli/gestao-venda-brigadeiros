using api_gestao_vendas_brigadeiros.Models;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

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

        public int AtualizarGasto(Gasto gasto, int id)
        {
            try
            {
                using TransactionScope transaction = new TransactionScope();

                connection.Open();

                string sqlUpd = "UPDATE Gastos SET Insumo = @Insumo, ValorUnitario = @ValorUnitario, QuantidadeComprada = @QuantidadeComprada, DataCompra = @DataCompra" +
                    "WHERE IdGasto = @Id";
                using SqlCommand commandUpd = new SqlCommand(sqlUpd, connection);
                commandUpd.Parameters.AddWithValue("@Insumo", gasto.Insumo);
                commandUpd.Parameters.AddWithValue("@ValorUnitario", gasto.ValorUnitario);
                commandUpd.Parameters.AddWithValue("@QuantidadeComprada", gasto.QuantidadeComprada);
                commandUpd.Parameters.AddWithValue("@DataCompra", gasto.DataCompra);
                commandUpd.Parameters.AddWithValue("@Id", id);

                int numLinhas = commandUpd.ExecuteNonQuery();

                transaction.Complete();
                return numLinhas;

            }
            finally
            {
                connection.Close();
            }

        }

        #region | Método de Exclusão | 
        public int ExcluirGasto(int id)
        {
            try
            {
                using TransactionScope transaction = new TransactionScope();

                connection.Open();

                string sqlUpd = "DELETE FROM Gastos WHERE IdGasto = @Id";
                using SqlCommand commandUpd = new SqlCommand(sqlUpd, connection);
                commandUpd.Parameters.AddWithValue("@Id", id);

                int numLinhas = commandUpd.ExecuteNonQuery();

                transaction.Complete();
                return numLinhas;

            }
            finally
            {
                connection.Close();
            }

        }
        #endregion

    }
}

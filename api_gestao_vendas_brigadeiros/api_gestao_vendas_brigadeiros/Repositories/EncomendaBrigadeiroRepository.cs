using api_gestao_vendas_brigadeiros.Models;
using System.Data.SqlClient;
using System.Data;

namespace api_gestao_vendas_brigadeiros.Repositories
{
    public class EncomendaBrigadeiroRepository
    {
        private readonly SqlConnection connection;

        public EncomendaBrigadeiroRepository(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DbGestaoBrigadeiros") ??
                throw new Exception("ConnectionString sem valor válido");
            connection = new SqlConnection(connectionString);
        }


        public IEnumerable<EncomendaBrigadeiro> BuscarBrigadeiroPorEncomenda(int idEncomenda)
        {
            try
            {
                connection.Open();

                string sql = @"SELECT * FROM EncomendaBrigadeiros WHERE IdEncomenda = @IdEncomenda";

                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdEncomenda", idEncomenda);

                using SqlDataReader reader = command.ExecuteReader();

                List<EncomendaBrigadeiro> brigadeiros = new List<EncomendaBrigadeiro>();

                while (reader.Read())
                {
                    EncomendaBrigadeiro encomendaBrigadeiro = new EncomendaBrigadeiro(reader);
                    brigadeiros.Add(encomendaBrigadeiro);

                }
                return brigadeiros;

            }
            finally
            {
                connection.Close();
            }
        }

        public int AdicionarBrigadeiroAEncomenda(int idEncomenda, int idBrigadeiro, int quantidade)
        {
            try
            {
                string sql = @"INSERT INTO EncomendaBrigadeiros (idEncomenda, idBrigadeiro, Quantidade)
                             VALUES (@IdEncomenda, @IdBrigadeiro, @Quantidade);";

                using SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@idEncomenda", idEncomenda);
                command.Parameters.AddWithValue("@idBrigadeiro", idBrigadeiro);
                command.Parameters.AddWithValue("@Quantidade", quantidade);

                connection.Open();

                int linhasAfetadas = command.ExecuteNonQuery();
                return linhasAfetadas;

            }
            finally
            {
                connection.Close();
            }


        }
    }
}

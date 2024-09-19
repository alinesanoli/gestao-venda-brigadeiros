using System.Data;
using System.Data.SqlClient;
using api_gestao_vendas_brigadeiros.Models;

namespace api_gestao_vendas_brigadeiros.Repositories
{
    public class VendaBrigadeiroRepository
    {

        private readonly SqlConnection connection;

        public VendaBrigadeiroRepository(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DbGestaoBrigadeiros") ??
                throw new Exception("ConnectionString sem valor válido");
            connection = new SqlConnection(connectionString);
        }

        public IEnumerable<Brigadeiro>? BuscarBrigadeiros()
        {
            try
            {
                connection.Open();

                string sql = @"SELECT * FROM Brigadeiros";

                using SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;

                using SqlDataReader reader = command.ExecuteReader();

                List<Brigadeiro> listaRetorno = new List<Brigadeiro>();

                while (reader.Read())
                {
                    Brigadeiro brigadeiro = new Brigadeiro(reader);
                    listaRetorno.Add(brigadeiro);
                }
                return listaRetorno;

            }
            finally
            {
                connection.Close();
            }
        }

        public IEnumerable<VendaBrigadeiro> BuscarBrigadeiroPorVenda(int idVenda)
        {
            try
            {
                connection.Open();

                string sql = @"SELECT * FROM VendaBrigadeiros WHERE IdVenda = @IdVenda";

                using SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@IdVenda", idVenda);

                using SqlDataReader reader = command.ExecuteReader();

                List<VendaBrigadeiro> brigadeiros = new List<VendaBrigadeiro>();

                while (reader.Read()) 
                {
                    VendaBrigadeiro brigadeiro = new VendaBrigadeiro(reader);
                    brigadeiros.Add(brigadeiro);
                                        
                }
                return brigadeiros;

            }
            finally
            {
                connection.Close();
            }
        }

        public int AdicionarBrigadeiroAVenda(int idVenda, int idBrigadeiro, int quantidade)
        {
            try
            {
                string sql = @"INSERT INTO VendaBrigadeiros (idVenda, idBrigadeiro, Quantidade)
                             VALUES (@IdVenda, @IdBrigadeiro, @Quantidade);";

                using SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@idVenda", idVenda);
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

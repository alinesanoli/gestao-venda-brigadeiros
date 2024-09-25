using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace gestao_vendas_brigadeiro
{
    public partial class Gastos : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DbGestaoBrigadeiros"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarGastos();
            }
        }

        private void CarregarGastos()
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {

                    conn.Open();

                    string query = "SELECT IdGastos, Insumo, ValorUnitario, QuantidadeComprada, ValorTotal, DataCompra FROM Gastos";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Associar o DataTable ao GridView
                    GastosGrid.DataSource = dt;
                    GastosGrid.DataBind();
                }
                catch (Exception ex)
                {

                    Response.Write("Erro: " + ex.Message);
                }
            }
        }

        protected void btnAdicionarGasto_Click(object sender, EventArgs e)
        {
            string insumo = txtInsumo.Text;
            decimal valorUnitario = decimal.Parse(txtValorUnitario.Text);
            int quantidadeComprada = int.Parse(txtQuantidadeComprada.Text);
            DateTime dataCompra = DateTime.Parse(txtDataCompra.Text);
            decimal valorTotal = valorUnitario * quantidadeComprada;

            InserirGasto(insumo, valorUnitario, quantidadeComprada, dataCompra);
            CarregarGastos(); // Recarregar a lista de gastos após a inserção

            // Limpar os campos após inserção
            txtInsumo.Text = string.Empty;
            txtValorUnitario.Text = string.Empty;
            txtQuantidadeComprada.Text = string.Empty;
            txtDataCompra.Text = string.Empty;
        }

        private void InserirGasto(string insumo, decimal valorUnitario, int quantidadeComprada, DateTime dataCompra)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Gastos (Insumo, ValorUnitario, QuantidadeComprada, DataCompra) VALUES (@Insumo, @ValorUnitario, @QuantidadeComprada, @DataCompra)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Insumo", insumo);
                        cmd.Parameters.AddWithValue("@ValorUnitario", valorUnitario);
                        cmd.Parameters.AddWithValue("@QuantidadeComprada", quantidadeComprada);
                        cmd.Parameters.AddWithValue("@DataCompra", dataCompra);

                        cmd.ExecuteNonQuery(); // Executar a inserção
                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Erro ao inserir gasto: " + ex.Message);
                }
            }
        }
    }
}
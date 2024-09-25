using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace gestao_vendas_brigadeiro
{
    public partial class Venda : Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["DbGestaoBrigadeiros"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                    CarregarVendas();
            }
        }

         private void CarregarVendas()
         {
                           
              using (SqlConnection conn = new SqlConnection(connectionString))
              {
                  try
                  {
                       
                      conn.Open();

                      string query = "SELECT IdVenda, IdCliente, DataVenda, ValorTotal, FormaPagamento FROM Vendas";

                      SqlDataAdapter da = new SqlDataAdapter(query, conn);                                  
                      DataTable dt = new DataTable();                      
                      da.Fill(dt);

                      // Associar o DataTable ao GridView
                      VendasGrid.DataSource = dt;
                      VendasGrid.DataBind();
                  }
                  catch (Exception ex)
                  {
                   
                      Response.Write("Erro: " + ex.Message);
                  }
                }
         }
        protected void btnCadastrarVenda_Click(object sender, EventArgs e)
        {
            // Valida os campos (exemplo simples)
            if (string.IsNullOrEmpty(txtIdCliente.Text) || string.IsNullOrEmpty(txtDataVenda.Text) ||
                string.IsNullOrEmpty(txtValorTotal.Text) || string.IsNullOrEmpty(txtFormaPagamento.Text))
            {
                lblMensagem.Text = "Por favor, preencha todos os campos.";
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    // Comando SQL para inserir uma nova venda
                    string query = "INSERT INTO Vendas (IdCliente, DataVenda, ValorTotal, FormaPagamento) " +
                                   "VALUES (@IdCliente, @DataVenda, @ValorTotal, @FormaPagamento)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdCliente", txtIdCliente.Text);
                    cmd.Parameters.AddWithValue("@DataVenda", DateTime.Parse(txtDataVenda.Text));
                    cmd.Parameters.AddWithValue("@ValorTotal", decimal.Parse(txtValorTotal.Text));
                    cmd.Parameters.AddWithValue("@FormaPagamento", txtFormaPagamento.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                    lblMensagem.Text = "Venda cadastrada com sucesso!";
                    // Limpa os campos
                    txtIdCliente.Text = "";
                    txtDataVenda.Text = "";
                    txtValorTotal.Text = "";
                    txtFormaPagamento.Text = "";

                    // Recarrega a lista de vendas
                    CarregarVendas();
                }
            }
            catch (Exception ex)
            {
                lblMensagem.Text = "Erro ao cadastrar venda: " + ex.Message;
            }
        }

        protected void VendasGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteVenda")
            {
                int IdVenda = Convert.ToInt32(e.CommandArgument);
                DeletarVenda(IdVenda);
            }
        }

        private void DeletarVenda(int vendaId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "DELETE FROM Vendas WHERE IdVenda = @IdVenda";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdVenda", vendaId);
                    cmd.ExecuteNonQuery();

                    lblMensagem.Text = "Venda deletada com sucesso!";
                 
                    CarregarVendas();
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Erro ao deletar venda: " + ex.Message;
                }
            }
        }
    }

}

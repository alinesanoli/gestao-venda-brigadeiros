using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;

namespace gestao_vendas_brigadeiro
{
    public partial class VendaBrigadeiros : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DbGestaoBrigadeiros"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CarregarBrigadeiros();
                CarregarBrigadeirosDaVenda();
            }
        }

        // Carregar brigadeiros no DropDownList para inserção
        private void CarregarBrigadeiros()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT IdBrigadeiro, Sabor FROM Brigadeiros";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    ddlBrigadeiro.DataSource = reader;
                    ddlBrigadeiro.DataTextField = "Sabor"; // Sabor exibido no DropDownList
                    ddlBrigadeiro.DataValueField = "IdBrigadeiro"; // Valor do DropDownList
                    ddlBrigadeiro.DataBind();
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Erro ao carregar brigadeiros: " + ex.Message;
                }
            }
        }

        // Carregar os brigadeiros de uma venda específica
        private void CarregarBrigadeirosDaVenda()
        {
            //int idVenda = Convert.ToInt32(txtIdVenda.Text);
            int idVenda;
            if (int.TryParse(txtIdVenda.Text, out idVenda))
            {
                // O valor foi convertido com sucesso
                
            }
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"
                    SELECT vb.IdVenda, b.Sabor, vb.Quantidade
                    FROM VendaBrigadeiros vb
                    JOIN Brigadeiros b ON vb.IdBrigadeiro = b.IdBrigadeiro
                    WHERE vb.IdVenda = @IdVenda";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdVenda", idVenda);

                try
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    // Verificar as colunas retornadas
                    foreach (DataColumn column in dt.Columns)
                    {
                        Console.WriteLine(column.ColumnName); 
                    }

                    VendaBrigadeirosGrid.DataSource = dt;
                    VendaBrigadeirosGrid.DataBind();
                }
                catch (Exception ex)
                {
                    lblMensagem.Text = "Erro ao carregar brigadeiros da venda: " + ex.Message;
                }
            }
        }

        // Inserir brigadeiro na venda
        protected void btnAdicionarBrigadeiro_Click(object sender, EventArgs e)
        {
            int idVenda = Convert.ToInt32(txtIdVenda.Text);
            int idBrigadeiro = Convert.ToInt32(ddlBrigadeiro.SelectedValue);
            int quantidade = Convert.ToInt32(txtQuantidade.Text);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Verificar se o brigadeiro já existe para essa venda
                string queryCheck = "SELECT Quantidade FROM VendaBrigadeiros WHERE IdVenda = @IdVenda AND IdBrigadeiro = @IdBrigadeiro";
                SqlCommand cmdCheck = new SqlCommand(queryCheck, conn);
                cmdCheck.Parameters.AddWithValue("@IdVenda", idVenda);
                cmdCheck.Parameters.AddWithValue("@IdBrigadeiro", idBrigadeiro);

                // Executa o comando para verificar a quantidade existente
                object result = cmdCheck.ExecuteScalar();

                if (result != null)  // Se já existir um registro
                {
                    int quantidadeAtual = Convert.ToInt32(result);

                    // Atualizar a quantidade somando o valor novo
                    string queryUpdate = "UPDATE VendaBrigadeiros SET Quantidade = @NovaQuantidade WHERE IdVenda = @IdVenda AND IdBrigadeiro = @IdBrigadeiro";
                    SqlCommand cmdUpdate = new SqlCommand(queryUpdate, conn);
                    cmdUpdate.Parameters.AddWithValue("@NovaQuantidade", quantidadeAtual + quantidade);
                    cmdUpdate.Parameters.AddWithValue("@IdVenda", idVenda);
                    cmdUpdate.Parameters.AddWithValue("@IdBrigadeiro", idBrigadeiro);
                    cmdUpdate.ExecuteNonQuery();

                    lblMensagem.Text = "Quantidade atualizada com sucesso!";
                }
                else  //Caso não exista, faz a inserção
                {
                    string queryInsert = "INSERT INTO VendaBrigadeiros (IdVenda, IdBrigadeiro, Quantidade) VALUES (@IdVenda, @IdBrigadeiro, @Quantidade)";
                    SqlCommand cmdInsert = new SqlCommand(queryInsert, conn);
                    cmdInsert.Parameters.AddWithValue("@IdVenda", idVenda);
                    cmdInsert.Parameters.AddWithValue("@IdBrigadeiro", idBrigadeiro);
                    cmdInsert.Parameters.AddWithValue("@Quantidade", quantidade);
                    cmdInsert.ExecuteNonQuery();

                    lblMensagem.Text = "Brigadeiro adicionado com sucesso!";
                }

                // Atualizar a lista de brigadeiros da venda
                CarregarBrigadeirosDaVenda();
            }
        }
    

        // Deletar brigadeiro da venda
        protected void VendaBrigadeirosGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteBrigadeiro")
            {
                //dividir em um array de strings as informações de Venda e Brigadeiro
                string[] args = e.CommandArgument.ToString().Split(',');
                int idVenda = Convert.ToInt32(args[0]);
                int idBrigadeiro = Convert.ToInt32(args[1]);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM VendaBrigadeiros WHERE IdVenda = @IdVenda AND IdBrigadeiro = @IdBrigadeiro";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@IdVenda", idVenda);
                    cmd.Parameters.AddWithValue("@IdBrigadeiro", idBrigadeiro);

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        lblMensagem.Text = "Brigadeiro removido com sucesso!";
                        CarregarBrigadeirosDaVenda();
                    }
                    catch (Exception ex)
                    {
                        lblMensagem.Text = "Erro ao deletar brigadeiro: " + ex.Message;
                    }
                }
            }
        }
    }
}

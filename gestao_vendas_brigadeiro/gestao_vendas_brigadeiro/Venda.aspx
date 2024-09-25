<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="Venda.aspx.cs" Inherits="gestao_vendas_brigadeiro.Venda" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <h1>Cadastro de Vendas</h1>

    <asp:Label runat="server" ID="lblMensagem" ForeColor="Red" />

    <!-- Formulário de cadastro de vendas -->
    <table class="table-venda">
        <tr>
            <td>ID Cliente:</td>
            <td><asp:TextBox ID="txtIdCliente" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Data da Venda:</td>
            <td><asp:TextBox ID="txtDataVenda" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Valor Total:</td>
            <td><asp:TextBox ID="txtValorTotal" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td>Forma de Pagamento:</td>
            <td><asp:TextBox ID="txtFormaPagamento" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnCadastrarVenda" runat="server" Text="Cadastrar Venda" OnClick="btnCadastrarVenda_Click" />
            </td>
        </tr>
    </table>

    <hr />
    
    <h1>Lista de Vendas</h1>
    <asp:GridView ID="VendasGrid" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="IdVenda" HeaderText="ID Venda" />
            <asp:BoundField DataField="IdCliente" HeaderText="ID Cliente" />
            <asp:BoundField DataField="DataVenda" HeaderText="Data da Venda" DataFormatString="{0:dd/MM/yyyy}" />
            <asp:BoundField DataField="ValorTotal" HeaderText="Valor Total" DataFormatString="{0:C}" />
            <asp:BoundField DataField="FormaPagamento" HeaderText="Forma de Pagamento" />
            <asp:TemplateField HeaderText="Ações">
                <ItemTemplate>
                    <asp:Button ID="btnDelete" runat="server" CommandName="DeleteVenda" CommandArgument='<%# Eval("IdVenda") %>' Text="Deletar" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
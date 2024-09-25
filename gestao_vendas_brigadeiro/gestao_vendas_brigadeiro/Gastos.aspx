<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Gastos.aspx.cs" Inherits="gestao_vendas_brigadeiro.Gastos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

       <h2>Adicionar Gastos</h2>

    <asp:Label runat="server" ID="lblMensagem" ForeColor="Red" />

        <!-- Formulário de Inserção de Gastos -->
        <div>
            
            <table class="table-venda">
                <tr>
                    <td>Insumo:</td>
                    <td><asp:TextBox ID="txtInsumo" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Valor Unitário:</td>
                    <td><asp:TextBox ID="txtValorUnitario" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Quantidade Comprada:</td>
                    <td><asp:TextBox ID="txtQuantidadeComprada" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Data da Compra:</td>
                    <td><asp:TextBox ID="txtDataCompra" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnAdicionarGasto" runat="server" Text="Adicionar Gasto" OnClick="btnAdicionarGasto_Click" />
                    </td>
                </tr>
            </table>
        </div>
   
       <h2>Lista de Gastos</h2>
        <asp:GridView ID="GastosGrid" runat="server" AutoGenerateColumns="False" OnRowCommand="GastosGrid_RowCommand">
            <Columns>
                <asp:BoundField DataField="IdGastos" HeaderText="ID" />
                <asp:BoundField DataField="Insumo" HeaderText="Insumo" />
                <asp:BoundField DataField="ValorUnitario" HeaderText="Valor Unitário" DataFormatString="{0:C}" />
                <asp:BoundField DataField="QuantidadeComprada" HeaderText="Quantidade Comprada" />
                <asp:BoundField DataField="ValorTotal" HeaderText="Valor Total" DataFormatString="{0:C}" />
                <asp:BoundField DataField="DataCompra" HeaderText="Data da Compra" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:TemplateField HeaderText="Ações">
                <ItemTemplate>
                    <asp:Button ID="btnDelete" runat="server" CommandName="DeleteGasto" CommandArgument='<%# Eval("IdGastos") %>' Text="Deletar" />
                </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>

</asp:Content>

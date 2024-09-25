<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="VendaBrigadeiros.aspx.cs" Inherits="gestao_vendas_brigadeiro.VendaBrigadeiros" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Adicionar Brigadeiros à Venda</h2>

    <asp:Label runat="server" ID="lblMensagem" ForeColor="Red" />

    <div>
        <h3>Inserir Brigadeiro</h3>
        <table class="table-venda">
            <tr>
                <td>ID da Venda:</td>
                <td><asp:TextBox ID="txtIdVenda" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Brigadeiro:</td>
                <td>
                    <asp:DropDownList ID="ddlBrigadeiro" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Quantidade:</td>
                <td><asp:TextBox ID="txtQuantidade" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnAdicionarBrigadeiro" runat="server" Text="Adicionar Brigadeiro" OnClick="btnAdicionarBrigadeiro_Click" />
                </td>
            </tr>
        </table>
    </div>

    <h2>Brigadeiros na Venda</h2>
    <asp:GridView ID="VendaBrigadeirosGrid" runat="server" AutoGenerateColumns="False" OnRowCommand="VendaBrigadeirosGrid_RowCommand">
        <Columns>
            <asp:BoundField DataField="IdVenda" HeaderText="ID da Venda" />
            <asp:BoundField DataField="Sabor" HeaderText="Brigadeiro" />
            <asp:BoundField DataField="Quantidade" HeaderText="Quantidade" />
            <asp:BoundField DataField="IdBrigadeiro" HeaderText="Id Brigadeiro"  />
            <asp:TemplateField HeaderText="Ações">
                <ItemTemplate>
                    <asp:Button ID="btnDelete" runat="server" CommandName="DeleteBrigadeiro" CommandArgument='<%# Eval("IdVenda") + "," + Eval("IdBrigadeiro") %>' Text="Deletar" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="Site.Master" CodeFile="Sobre.aspx.cs" Inherits="gestao_vendas_brigadeiro.Sobre" %>

    <asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" type="text/css" href="../StyleSheet.css" />
    </asp:Content> 


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
        <header>
            <h1>Sobre os Nossos Brigadeiros</h1>
            
        </header>
        
        <section class="brigadeiros">
            <div class="brigadeiro-item">
                <h2>Brigadeiro meio amargo</h2>
                <p>Brigadeiro feito com leite condensado, creme de leite, chocolate em pó 50%, chocolate em gotas 70% e granulado. Uma verdadeira delícia!</p>
            </div>

            <div class="brigadeiro-item">
                <h2>Brigadeiro de chocolate branco</h2>
                <p>Brigadeiro feito com leite condensado, creme de leite, chocolate branco e granulado, que derrete na boca.</p>
            </div>

            <div class="brigadeiro-item">
                <h2>Brigadeiro de café</h2>
                <p>Brigadeiro feito com leite condensado, creme de leite, chocolate em pó 50%, café e granulado, para os amantes de chocolate com café.</p>
            </div>
            
            <div class="brigadeiro-item">
                <h2>Brigadeiro de casadinhho</h2>
                <p>Brigadeiro de chocolate meio amargo com brigadeiro de leite em pó (feito com leite condensado, creme de leite e leite em pó).</p>
            </div>
            <div class="brigadeiro-item">
                <h2>Brigadeiro de Romeu e Julieta</h2>
                <p>Brigadeiro feito com leite condensado, creme de leite, queijo parmesão e chocolate branco.</p>
            </div>
            <div class="brigadeiro-item">
                <h2>Brigadeiro de Tapioca com doce de leite</h2>
                <p>Brigadeiro de tapioca com brigadeiro de doce de leite.</p>
                <p>- Brigadeiro de tapioca</p>
                <ul>
                    <li>Leite condensado</li>
                    <li>Creme de leite</li>
                    <li>Leite</li>
                    <li>Leite de coco</li>
                    <li>Coco</li>
                    <li>Tapioca granulada</li>
                </ul>
                <p>- Brigadeiro de doce de leite</p>
                <ul>
                    <li>Leite condensado</li>
                    <li>Creme de leite</li>
                    <li>Doce de leite</li>
                </ul>
            </div>

        </section>

    </div>
</asp:Content>

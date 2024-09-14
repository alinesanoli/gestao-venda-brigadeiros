namespace api_gestao_vendas_brigadeiros.Models
{
    public class VendaBrigadeiro
    {
        public int IdVenda { get; set; }
        public int IdBrigadeiro { get; set; }
        public int Quantidade { get; set; }

        public Venda Venda { get; set; }
        public required Brigadeiro Brigadeiro { get; set; }
    }
}

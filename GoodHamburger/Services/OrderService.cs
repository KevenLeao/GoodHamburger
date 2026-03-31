using GoodHamburger.Models;

namespace GoodHamburger.Services;

public class OrderService
{
    public List<ItemOrder> Carrinho { get; private set; } = new();
    public List<PedidoFinalizado> HistoricoPedidos { get; private set; } = new();
    public string MensagemErro { get; private set; } = "";

    public event Action? OnChange;

    public void Adicionar(ItemOrder item)
    {
        MensagemErro = "";

        // REGRA 1: Permitir 3 itens no order
        if (Carrinho.Count >= 3)
        {
            MensagemErro = "Maximum of 3 items reached!";
            NotifyStateChanged();
            return;
        }

        // REGRA 2: Não permitir duplicar o mesmo item
        if (Carrinho.Any(i => i.Nome == item.Nome))
        {
            MensagemErro = $"You already added {item.Nome}!";
            NotifyStateChanged();
            return;
        }

        // REGRA 3: Permitir apenas um sanduiche por vez
        if (item.Categoria == "Sandwich" && Carrinho.Any(i => i.Categoria == "Sandwich"))
        {
            MensagemErro = "You can only select one burger per combo!";
            NotifyStateChanged();
            return;
        }

        Carrinho.Add(item);
        NotifyStateChanged();
    }

    public void Remover(ItemOrder item)
    {
        Carrinho.Remove(item);
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();

    public void FinalizarPedido()
    {
        if (Carrinho.Count == 0) return;

        var novoPedido = new PedidoFinalizado
        {
            Id = HistoricoPedidos.Count + 1,
            Itens = new List<ItemOrder>(Carrinho),
            Total = TotalFinal,
            DataHora = DateTime.Now
        };

        HistoricoPedidos.Add(novoPedido);
        Carrinho.Clear();
        NotifyStateChanged();
    }

    public void AlterarStatus(int pedidoId, string novoStatus)
    {
        var pedido = HistoricoPedidos.FirstOrDefault(p => p.Id == pedidoId);
        if (pedido != null)
        {
            pedido.Status = novoStatus;
            NotifyStateChanged();
        }
    }

    public void ExcluirPedido(int pedidoId)
    {
        var pedido = HistoricoPedidos.FirstOrDefault(p => p.Id == pedidoId);
        if (pedido != null)
        {
            HistoricoPedidos.Remove(pedido);
            NotifyStateChanged();
        }
    }

    // Calculo de desconto
    public decimal CalcularDesconto()
    {
        bool temSanduba = Carrinho.Any(i => i.Categoria == "Sandwich");
        bool temFritas = Carrinho.Any(i => i.Nome == "Fries");
        bool temRefri = Carrinho.Any(i => i.Nome == "Soft Drink");

        if (temSanduba && temFritas && temRefri) return 0.20m; // 20%
        if (temSanduba && temRefri) return 0.15m;             // 15%
        if (temSanduba && temFritas) return 0.10m;            // 10%
        return 0;
    }

    public decimal Subtotal => Carrinho.Sum(i => i.Preco);
    public decimal ValorDesconto => Subtotal * CalcularDesconto();
    public decimal TotalFinal => Subtotal - ValorDesconto;

    public class PedidoFinalizado
    {
        public int Id { get; set; }
        public List<ItemOrder> Itens { get; set; } = new();
        public decimal Total { get; set; }
        public DateTime DataHora { get; set; }
        public string Status { get; set; } = "Pending";
    }
}
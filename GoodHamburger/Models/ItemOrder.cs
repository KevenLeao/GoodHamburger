namespace GoodHamburger.Models;

public class ItemOrder
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public decimal Preco { get; set; }
    public string Categoria { get; set; } = ""; //Sanduba ou extras
    public string Emoji { get; set; } = "";
    public string Descricao { get; set; } = "";
}
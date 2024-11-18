using System.ComponentModel.DataAnnotations.Schema;

namespace gestiondette.Models;

public class DetailDette() : AbstractEntity()
{
    public int Id { get; set; }
    public double Qte { get; set; }
    public Dette? Dette { get; set; }

    public int ArticleId { get; set; }

    [NotMapped]
    public Article? Article { get; set; }

    [NotMapped]
    public double Total { get; set; } = 0;

    public void setTotal(double total)
    {
        Total = Article.Prix * total;
    }



    public override string ToString()
    {
        return "Client[" +
                "id=" + Id +
                ", qte=" + Qte +
                ", article=" + Article.Libelle +
                ']';

    }
}

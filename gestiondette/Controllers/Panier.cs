using gestiondette.Models;
using Microsoft.IdentityModel.Tokens;
namespace gestiondette.Controllers;

public class Panier
{
    public List<DetailDette> detailDettes { get; set; } = new List<DetailDette>();
    public Client client { get; set; }
    public void AjouterArticles(Article article, int qte)
    {
        // Vérifier si l'article existe déjà dans le panier
        DetailDette existingDetail = detailDettes.FirstOrDefault(d => d.Article.Id == article.Id);

        if (existingDetail != null)
        {
            // Si l'article existe, mettre à jour la quantité et le total
            existingDetail.Qte += qte;
            existingDetail.Total = article.Prix * existingDetail.Qte;
        }
        else
        {
            // Si l'article n'existe pas, l'ajouter comme nouveau
            detailDettes.Add(new DetailDette { Article = article, Qte = qte, Total = article.Prix * qte });
        }
    }

}
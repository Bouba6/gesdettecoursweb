using System.ComponentModel.DataAnnotations.Schema;

namespace gestiondette.Models
{
    public class Client : AbstractEntity
    {
        private List<Dette> listDette = new List<Dette>(); // Correction : initialiser la liste correctement

        public string? Surnom { get; set; }
        public string? Telephone { get; set; }
        public string? Adresse { get; set; }
        public double? Solde { get; set; }
        public User? User { get; set; }

        public List<Dette> ListDette // Correction de la propriété pour retourner la liste de Dettes
        {
            get => listDette;
        }

        public void SetDette(Dette dette)
        {
            listDette.Add(dette);
        }

        public override string ToString()
        {
            return $"Client[id={Id}, surnom='{Surnom}', telephone='{Telephone}', adresse='{Adresse}']";
        }
    }
}

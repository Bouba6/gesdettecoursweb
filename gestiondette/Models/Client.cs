using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace gestiondette.Models
{
    public class Client : AbstractEntity
    {
        private List<Dette> listDette = new List<Dette>();
        [Required]
        public string? Surnom { get; set; }
        [Required]
        public string? Telephone { get; set; }
        [Required]
        public string? Adresse { get; set; }
        public double? Solde { get; set; }
        public User? User { get; set; }

        public List<Dette> ListDette
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

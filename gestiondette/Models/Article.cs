namespace gestiondette.Models
{
    public class Article : AbstractEntity
    {

        public string? Libelle { get; set; }
        public double Prix { get; set; }
        public double QteStock { get; set; }



        public override string ToString()
        {
            return "Client[" +
                    "id=" + Id +
                    ", libelle='" + Libelle + '\'' +
                    ", prix='" + Prix + '\'' +
                    ", qteStock='" + QteStock + ']';

        }


    }
}
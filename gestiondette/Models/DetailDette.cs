namespace gestiondette.Models
{
    public class DetailDette() : AbstractEntity()
    {

        public int Id { get; set; }
        public double Qte { get; set; }
        public Dette Dette { get; set; }
        public Article Article { get; set; }


        public override string ToString()
        {
            return "Client[" +
                    "id=" + Id +
                    ", qte=" + Qte +
                    ", article=" + Article.Libelle +
                    ']';

        }
    }
}
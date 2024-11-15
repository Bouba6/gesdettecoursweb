
namespace gestiondette.Models
{
    public class Paiement : AbstractEntity
    {




        public double Montant { get; set; }
        public DateTime? DatePaiement { get; set; }

        public Dette? Dette { get; set; }


        public override string ToString()
        {
            return "Client[" +
                    "id=" + Id +
                    ", montant='" + Montant + '\'' +
                    ", date='" + DatePaiement + '\'';
        }

    }



}

namespace gestiondette.Models
{
    public class Paiement : AbstractEntity
    {


        private double montant;

        private DateTime datePaiement;

        private Dette dette;



        public double Montant { get => montant; set => montant = value; }
        public DateTime DatePaiement { get => datePaiement; set => datePaiement = value; }

        public Dette Dette { get => dette; set => dette = value; }






        public override string ToString()
        {
            return "Client[" +
                    "id=" + Id +
                    ", montant='" + montant + '\'' +
                    ", date='" + datePaiement + '\'';
        }

    }



}
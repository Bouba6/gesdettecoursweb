using gestiondette.Enum;

namespace gestiondette.Models
{
    public class Dette : AbstractEntity
    {





        private List<DetailDette>? detailDettes = new List<DetailDette>();
        private List<Paiement>? paiements = new List<Paiement>();

        private StateDette stateDette;

        // Constructor
        public Dette()
        {

        }

        // Properties

        public Client? Client { get; set; }
        public double MontantRestant { get; set; }
        public double MontantVerser { get; set; }
        public List<DetailDette>? DetailDettes { get; set; }
        public EtatDette EtatDette { get; set; }
        public double Montant { get; set; }
        public List<Paiement>? Paiements { get; set; } = new List<Paiement>();

        public StateDette StateDette { get; set; }

        // Methods




        public override string ToString()
        {
            return "Dette[" +
                    "id=" + Id +
                    ", montant=" + Montant +
                    ", montantRestant=" + MontantRestant +
                    ", montantVerser=" + MontantVerser +
                    ", etatDette=" + EtatDette +
                    ']';
        }
    }
}

using gestiondette.Enum;

namespace gestiondette.Models
{
    public class Dette : AbstractEntity
    {





        private List<DetailDette> detailDettes = new List<DetailDette>();
        private List<Paiement> paiements = new List<Paiement>();

        private StateDette stateDette;

        // Constructor
        public Dette()
        {

        }

        // Properties

        public Client Client { get; set; }
        public double MontantRestant { get; set; }
        public double MontantVerser { get; set; }
        public List<DetailDette> DetailDettes { get => detailDettes; }
        public EtatDette EtatDette { get; set; }
        public double Montant { get; set; }
        public List<Paiement> Paiements { get => paiements; }

        public StateDette StateDette { get; set; }

        // Methods
        public void SetdetailDettes(DetailDette detailDette)
        {
            Montant += detailDette.Qte * detailDette.Article.Prix;
            detailDettes.Add(detailDette);
        }

        public void SetPaiements(Paiement paiement)
        {
            MontantVerser += paiement.Montant;
            MontantRestant -= paiement.Montant;
            paiements.Add(paiement);
        }

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

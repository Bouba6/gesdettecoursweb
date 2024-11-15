using gestiondette.Models;
using gestiondette.Enum;
namespace gestiondette.Data.Fixtures
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider, ApplicationDbContext context)
        {
            // Vérifier si des données existent déjà dans la table Clients
            if (context.dette.Any())
            {
                return;   // La base de données est déjà peuplée
            }

            // Ajouter des clients par défaut
            var articles = new List<Article>();
            var clients = new List<Client>();
            var users = new List<User>();
            var dettes = new List<Dette>();

            for (int i = 0; i < 3; i++)
            {
                articles.Add(new Article
                {

                    Libelle = $"Libelle{i + 1}",
                    Prix = i + 1.99,
                    QteStock = i + 1
                });
                clients.Add(new Client
                {
                    Surnom = $"Surnom{i + 1}",
                    Telephone = $"Telephone{i + 1}",
                    Adresse = $"Adresse{i + 1}"

                });
                users.Add(new User
                {
                    Email = $"Email{i + 1}@gmail.com",
                    Login = $"Login{i + 1}",
                    Password = $"Password{i + 1}"
                });

                dettes.Add(new Dette
                {
                    Client = clients[i],
                    MontantRestant = (i + 1) * 100.0,
                    MontantVerser = (i + 1) * 50.0,
                    EtatDette = EtatDette.ENCOURS,
                    Montant = (i + 1) * 150.0,
                    StateDette = StateDette.ARCHIVER
                });

            }

            // Ajouter tous les clients en même temps dans la base de données
            context.article.AddRange(articles);
            context.client.AddRange(clients);
            context.users.AddRange(users);
            context.dette.AddRange(dettes);


            // Sauvegarder les changements dans la base de données
            context.SaveChanges();
        }
    }
}

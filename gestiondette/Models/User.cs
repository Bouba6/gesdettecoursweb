
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gestiondette.Enum;

namespace gestiondette.Models
{
    public class User : AbstractEntity
    {











        private bool state;

        public User()
        {

            state = true;
        }

        [Required]

        public string? Login { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
        public Role? Role { get; set; }

        [NotMapped]
        public Client? Client { get; set; }


        public bool State { get => state; set => state = value; }



        public override string ToString()
        {
            return "User[" +
                    "id=" + Id +
                    ", login='" + Login + '\'' +
                    ", password='" + Password + '\'' +
                    ", role=" + Role +
                    ']';

        }

    }
}
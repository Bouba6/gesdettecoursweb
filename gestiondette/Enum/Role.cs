namespace gestiondette.Enum
{
    public enum Role
    {
        ADMIN,
        CLIENT,
        BOUTIQUIER,


    }
    public class RoleHelper
    {
        public static Array GetRoles()
        {
            return Role.GetValues(typeof(Role));
        }
    }
}
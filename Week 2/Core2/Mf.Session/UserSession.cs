using Data.Entities;

namespace Core.Session
{
    public class UserSession
    {
        public static User loggedInUser { get; set; }
    }
}

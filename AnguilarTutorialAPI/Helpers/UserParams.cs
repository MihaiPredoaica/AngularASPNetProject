using System.Security.Cryptography.X509Certificates;

namespace AnguilarTutorialAPI.Helpers
{
    public class UserParams : PaginationParams
    {
        public string CurrentUsername { get; set; }
        public string Gender { get; set; }
        public int MinAge { get; set; } = 18;
        public int MaxAge { get; set; } = Int32.MaxValue;
        public string OrderBy { get; set; } = "lastActive";

    }
}

namespace Identity.API.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public List<string> services { get; set; }
    }
}

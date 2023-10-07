namespace Identity.Core.Entites;
public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public Service Role { get; set; }
}

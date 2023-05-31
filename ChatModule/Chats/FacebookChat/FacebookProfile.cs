namespace FacebookChat;

public class FacebookProfile
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? ProfilePic { get; set; }
    public required string Gender { get; set; }
    public required string Locale { get; set; }
}
namespace API.Helpers;

public class JWT
{
    public string HasKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public double DurationInMinutes { get; set; }
}

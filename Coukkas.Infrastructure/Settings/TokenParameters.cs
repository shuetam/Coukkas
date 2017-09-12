namespace Coukkas.Infrastructure.Settings
{
    public class TokenParameters
    {
    public string Issuer {get; set;}
    public string SigningKey {get; set;}
    public int ExpiryMinutes {get; set;}


    }
}
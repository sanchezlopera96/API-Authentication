namespace Authentication.Domain.AppSettings
{
    public class SecuritySettings
    {
        public string Secret { get; set; }
        public int TokenExpirationTime { get; set; }
        public bool ValidateAudience { get; set; }
        public bool ValidateIssuer { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

    }
}

namespace Authentication.Domain.Model
{
    public class AuthenticationRequest
    {
        public string User { get; set; }
        public string UserPassword { get; set; }
        public int AuthenticationType { get; set; }
        public int? AplicationId { get; set; }
        public string Secret { get; set; }

    }
}

namespace Authentication.Domain.Model
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }

    }
}

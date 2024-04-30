namespace Authentication.Domain.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string UserIdentification { get; set; }
        public string UserPassword { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int UserType { get; set; }
        public bool UserState { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? ModificationDate { get; set; }
    }
}
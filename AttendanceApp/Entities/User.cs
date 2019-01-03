namespace AttendanceApp.Entities
{
    public abstract class User
    {

        public int UserId { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        
        public User(int userId, string name, string email, string password)
        {
            this.UserId = userId;
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }

        public bool CheckEmail(string email)
        {
            if (this.Email.Equals(email))
            {
                return true;
            }
            return false;
        }

        public bool CheckPassword(string password)
        {
            if (this.Password.Equals(password))
            {
                return true;
            }
            return false;
        }

    }
}

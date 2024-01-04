using SQLite;
using System.Security.Cryptography;
using System.Text;

namespace C971.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Boolean IsAdmin { get; set; }
        public Boolean IsInstructor { get; set; }

        public User() {
            ID = 0;
            Username = "Username";
            Password = HashPassword("Password");
            Email = "Email";
            Phone = "Phone";
            FirstName = "First Name";
            LastName = "Last Name";
            IsAdmin = false;
            IsInstructor = false;
        }
        public User(int ID = 0, string Username = "Username", string Password = "Password", string Email = "Email", string Phone = "Phone", 
            string FirstName = "First Name", string LastName = "Last Name", Boolean isAdmin = false, Boolean isInstructor = false)
        {
            this.ID = ID;
            this.Username = Username;
            this.Password = HashPassword(Password);
            this.Email = Email;
            this.Phone = Phone;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.IsAdmin = isAdmin;
            this.IsInstructor = isInstructor;
        }

        public User(string Username = "Username", string Password = "Password", string Email = "Email", string Phone = "Phone", 
            string FirstName = "First Name", string LastName = "Last Name", Boolean isAdmin = false, Boolean isInstructor = false)
        {
            this.Username = Username;
            this.Password = HashPassword(Password);
            this.Email = Email;
            this.Phone = Phone;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.IsAdmin = isAdmin;
            this.IsInstructor = isInstructor;
        }
        public string HashPassword(string password)
        {
            //HashAlgorithmName alg = HashAlgorithmName.SHA512;
            //var hash = Rfc2898DeriveBytes.Pbkdf2(
            //    Encoding.UTF8.GetBytes(password),
            //    RandomNumberGenerator.GetBytes(32), 1000, alg, 32);
            //return Convert.ToHexString(hash);
            return password;
        }
    }
}

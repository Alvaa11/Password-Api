using System.ComponentModel.DataAnnotations;
using Requests;
using Data;

namespace Model.UsersModel
{
    public class UsersModel
    {
        public UsersModel(string username, string password)
        {
            Username = username;
            Password = password;
        }

    [Key]
    public int Id { get; set; }
    public string Username { get; private set; }
    public string Password { get; private set; }

    public void SetUsername(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Please provide a username or password.");
        } else
        {
            Username = username;
            Password = password;
        }
    }
    // Verify if the user already exists in the database
    // If it does, return true, else return false
    public bool VerifyUser(string username, string password = "")
        {
            var context = new UsersContext();
            bool user = context.Users.Any(u => u.Username == username);

            return user;                                
        }
    };

}
namespace ShoppingList.Models;

//Model for UserAccount database
// properties for creating a new account to the database
public class UserAccount
{
    public string username { get; set; }
    public string password { get; set; }
    public string email { get; set; }

    public string userKey { get; set; }

    //default constructor to set values to properties for create new account
    public UserAccount(string username, string password, string email)
    {
        this.username = username;
        this.password = password;
        this.email = email;
    }
    
    //overload method of default constructor to set values to properties for login
    public UserAccount(string username, string password)
    {
        this.username = username;
        this.password = password;
        
    }
    
    //overload method of default constructor to set values to properties for logout
    public UserAccount(string userKey)
    {
        this.username = userKey;
        
    }
    
}
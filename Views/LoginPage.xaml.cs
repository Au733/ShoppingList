using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingList.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        Title = "Login";
    }

    private void Login_OnClicked(object sender, EventArgs e)
    {
        App.SessionKey = "aaa";//If user's username and password are good the sever will assign the user a sessionkey to allow user to use the page.
        Navigation.PopModalAsync();//pop away the modal page if user was able to login
    }

    private void CreateAccount_OnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new NewAccountPage()); // When create button is pushed, navigate to NewAccountPage.
    }
}
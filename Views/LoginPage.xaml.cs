using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShoppingList.Models;

namespace ShoppingList.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
        Title = "Login";
    }

    async void Login_OnClicked(object sender, EventArgs e)
    {
        //User info
        //username:joew2
        //password:aaa
        
        var data = JsonConvert.SerializeObject(new UserAccount(txtUser.Text, txtPassword.Text));
        
        var client = new HttpClient();
        var response = await client.PostAsync(new Uri("https://joewetzel.com/fvtc/account/login"),
            new StringContent(data, Encoding.UTF8, "application/json"));

        var SKey = response.Content.ReadAsStringAsync().Result;

        if (!string.IsNullOrEmpty(SKey) || SKey.Length < 50)
        {
            App.SessionKey = SKey;//If user's username and password are good the sever will assign the user a sessionkey to allow user to use the page.
            Navigation.PopModalAsync();//pop away the modal page
        }
        else
        {
            await DisplayAlert("Error", "Sorry invalid username or password!", "OK");
            return;
        }
        
    }

    private void CreateAccount_OnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new NewAccountPage()); // When create button is pushed, navigate to NewAccountPage.
    }
}
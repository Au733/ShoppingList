using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingList.Models;
using Newtonsoft.Json;

namespace ShoppingList.Views;

public partial class MainPage : ContentPage
{
    private LoginPage LP = new LoginPage();
    
    public MainPage()
    {
        InitializeComponent();
        Title = "Shopping List Pro";
        this.Loaded += MainPage_Loaded;
        LP.Unloaded += LP_Unloaded;
    }

    private void LP_Unloaded(object sender, EventArgs e)
    {
        OnAppearing1();
    }

    private void MainPage_Loaded(object sender, EventArgs e)
    {
        OnAppearing1();
    }

    public void OnAppearing1()
    {
        if (string.IsNullOrEmpty(App.SessionKey))//if SessionKey is null or empty user needs to login and will fire off login page for user.
        {
            Navigation.PushModalAsync(new NavigationPage(LP));//On appearing, creating a modal login page over main page with navigation to a create account page if the user does not have an account yet.
        }
        else
        {
            txtInput.Text = App.SessionKey;
        }
        
        
    }

    async void Logout_OnClicked(object sender, EventArgs e)
    {
        var data = JsonConvert.SerializeObject(new UserAccount(App.SessionKey));
        
        var client = new HttpClient();
        await client.PostAsync(new Uri("https://joewetzel.com/fvtc/account/logout"),
            new StringContent(data, Encoding.UTF8, "application/json"));
        
        App.SessionKey = "";//set sessionkey to empty
        OnAppearing1();
        
    }
}
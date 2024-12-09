using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ShoppingList.Models;

namespace ShoppingList.Views;

public partial class NewAccountPage : ContentPage
{
    public NewAccountPage()
    {
        InitializeComponent();
        Title = "Create New Account";
    }

    async void CreateAccount_OnClicked(object sender, EventArgs e)
    {
        //do passwords match
        if (txtPassword1.Text != txtPassword2.Text)
        {
            await DisplayAlert("Error", "Passwords do not match!", "OK");
        }
        
        
        //is a valid email address, check for "@" sign and a "."
        if (string.IsNullOrWhiteSpace(txtEmail.Text) || !txtEmail.Text.Contains("@") || !txtEmail.Text.Contains("."))
        {
            await DisplayAlert("Error", "Please enter valid email address", "OK");
        }
        
        //API stuff
        //serializes user input data into a JSON format and sends it to a server using HttpClient
        var data = JsonConvert.SerializeObject(new UserAccount(txtUser.Text, txtPassword1.Text, txtEmail.Text));
        
        var client = new HttpClient();
        var response = await client.PostAsync(new Uri("https://joewetzel.com/fvtc/account/createuser"),
            new StringContent(data, Encoding.UTF8, "application/json"));

        var AccountStatus = response.Content.ReadAsStringAsync().Result;

        //AccountStatus = AccountStatus; used to test serializing user input data, hitting the server, and response of api
        
        //does the user exist?
        if (AccountStatus == "user exists")
        {
            await DisplayAlert("Error", "Sorry this username has been taken!", "OK");
            return;
        }
        
        
        //is the email in use?
        if (AccountStatus == "email exists")
        {
            await DisplayAlert("Error", "Sorry this email has already been used!", "OK");
            return;
        }
        
        
        if (AccountStatus == "complete")
        {
            
            response = await client.PostAsync(new Uri("https://joewetzel.com/fvtc/account/login"),
                new StringContent(data, Encoding.UTF8, "application/json"));

            var SKey = response.Content.ReadAsStringAsync().Result;

            if (!string.IsNullOrEmpty(SKey) || SKey.Length < 50)
            {
                App.SessionKey = SKey;//sessionkey for login of new user
                Navigation.PopModalAsync();//pop away the modal page
            }
            else
            {
                await DisplayAlert("Error", "Sorry there was an issue logging you in!", "OK");
                return;
            }
            
        }
        
        
        
    }
}
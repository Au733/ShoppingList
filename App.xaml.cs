using ShoppingList.Views;

namespace ShoppingList;

public partial class App : Application
{
    public static string SessionKey = "";// SessionKey to enable login to app
    
    public App()
    {
        InitializeComponent();

        MainPage = new NavigationPage(new MainPage());
    }
}
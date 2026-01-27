using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Utils;
using AvaloniaApplication1.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AuthCommand))]
    private string login;
    
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AuthCommand))]
    private string password;
    
    [ObservableProperty]
    private string windowCaption;

    [RelayCommand(CanExecute = nameof(CanExecuteLoginCommand))]
    public async void Auth()
    {
        var client = Http.GetHttpClient();
        var data = new LoginData { Login = this.Login, Password = this.Password };
        var result = await client.PostAsync("Login", new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"));

        if (!result.IsSuccessStatusCode)
        {
            WindowCaption = "Ошибка входа";
            return;
        }

        ActiveUser.Token = await result.Content.ReadAsStringAsync();

        WindowCaption = "Успешный вход";

        var win = new Users();
        win.Show();
        
        close();
    }

    bool CanExecuteLoginCommand()
    {
        return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
    }

    private Action close;
    public void SetCloseAction(Action close)
    {
        this.close = close;
    }
}
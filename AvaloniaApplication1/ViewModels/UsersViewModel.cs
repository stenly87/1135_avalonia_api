using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AvaloniaApplication1.Models;
using AvaloniaApplication1.Utils;
using AvaloniaApplication1.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaApplication1.ViewModels;

public partial class UsersViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<User> users;

    HttpClient _client;

    public UsersViewModel()
    {
        _client = Http.GetHttpClient();

        Task.Run(async () => await LoadUsers());
    }

    async Task LoadUsers()
    {
        var result = await _client.GetAsync("Users");
        if (result.IsSuccessStatusCode)
        {
            Users = new(await  result.Content.ReadFromJsonAsync<List<User>>());
        }
    }

    [RelayCommand]
    public void CreateUser()
    {
        var win = new CreateUser();
        win.Show();
        win.Closed += async (s, e) =>
        {
            await LoadUsers();
        };
    }
}
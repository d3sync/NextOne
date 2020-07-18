using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NextOne.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "d3sync's Tools";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://tools.idle.gr"));
        }

        public ICommand OpenWebCommand { get; }
    }
}
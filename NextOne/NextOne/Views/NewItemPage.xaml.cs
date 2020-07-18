using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NextOne.Models;

using LahoreSocketAsync;

using Environment = System.Environment;


namespace NextOne.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class NewItemPage : ContentPage
    {

        public LahoreSocketClient client = null; 
        public Item Item { get; set; }
        public static LahoreSocketClient SocketInstance { get; set; }

        public static NewItemPage ItemPage { get; set; }
        static NewItemPage self = null;
        public NewItemPage()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "192.168.0.4",
                Description = "This is an item description."
            };

            BindingContext = this;
            NewItemPage self = this;
        }
        private static void HandleTextReceived(object sender, TextReceivedEventArgs trea)
        {
            string msg = string.Format(
                "{0} - Received: {1}{2}",
                DateTime.Now,
                trea.TextReceived,
                Environment.NewLine);
            //Console.WriteLine(msg);

        self.DisplayAlert("Transmission", msg,"OK");
        }

        private static void HandleServerDisconnected(object sender, ConnectionDisconnectedEventArgs cdea)
        {
            string msg = string.Format(
                "{0} - Disconnected from server: {1}{2}",
                DateTime.Now,
                cdea.DisconnectedPeer,
                Environment.NewLine);
            //System.Console.ReadLine();
            //Environment.Exit(1);
            //Console.WriteLine(msg);
            self.DisplayAlert("Connection Status", msg, "OK");
            //connectIt.Text = "Connect";
        }

        private static void HandleServerConnected(object sender, ConnectionDisconnectedEventArgs cdea)
        {

            string msg = string.Format(
                "{0} - Connected to server: {1}{2}",
                DateTime.Now,
                cdea.DisconnectedPeer,
                Environment.NewLine);
            //Console.WriteLine(msg);
            //Toast.MakeText(self, msg, ToastLength.Short).Show();
            self.DisplayAlert("Connection Status", msg, "OK");
            //connectIt.Text = "CONNECTED!!!";

        }

        public void Socket_Handler(object sender, EventArgs e)
        {
            client = new LahoreSocketClient();
            client.RaiseTextReceivedEvent += HandleTextReceived;
            client.RaiseServerDisconnected += HandleServerDisconnected;
            client.RaiseServerConnected += HandleServerConnected;
            string strIPAddress = ipAddress.Text;
            string strPortInput = "23000";

            if (!client.SetServerIPAddress(strIPAddress) ||
                !client.SetPortNumber(strPortInput))
            {
                /* MessageBox.Show(
                     string.Format(
                         "Wrong IP Address or port number supplied - {0} - {1} - Press a key to exit",
                         strIPAddress,
                         strPortInput));

                 return;*/
            }

            client.ConnectToServer();

            Navigation.PopModalAsync();
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopModalAsync();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
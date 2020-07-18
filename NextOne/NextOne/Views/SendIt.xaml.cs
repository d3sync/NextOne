using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NextOne.Models;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace NextOne.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendIt : ContentPage
    {
        private NewItemPage lol = NewItemPage.ItemPage;

        public NewItemPage Lol
        {
            get => lol;
            set => lol = value;
        }
        public Item Item { get; set; }

        public SendIt()
        {
            InitializeComponent();

            Item = new Item
            {
                Text = "6942907212",
                Description = "This is an item description."
            };

        }

        private void Toggle_OnToggled(object sender, ToggledEventArgs e)
        {
            ToggleLabel.Text = Toggle.IsToggled ? "PLUS4U" : "E-SHOP";
        }

        private void SendIt_OnClicked(object sender, EventArgs e)
        {
            string toadd = Phone.Text + "|" + Name.Text + "|" + ToggleLabel.Text;
            Item = new Item();
            Item.Text = Phone.Text;
            Item.Description = Name.Text + "|" + ToggleLabel.Text;
            MessagingCenter.Send(this, "AddItem", Item);
            //Lol.client.SendToServer(toadd);
        }
    }
}
using System;
using AddressSearchApp.api;
using AddressSearchApp.entity;
using Xamarin.Forms;

namespace AddressSearchApp
{
    public partial class AddressSearch : ContentPage
    {
        public AddressSearch()
        {
            InitializeComponent();
            this.Title = "住所検索アプリ";
            this.GetAddressSearchBtn.Clicked += GetAddressSearchBtn_Clicked;
            this.BindingContext = new Address();
        }

        private async void GetAddressSearchBtn_Clicked(object sender, EventArgs e) {
            if (!string.IsNullOrEmpty(AddressEntry.Text)
                && ((string)AddressEntry.Text).Length == 7) {
                Address Ad = await APIAddress.GetAddressSearchResult(AddressEntry.Text);

                if (Ad == null) {
                    await DisplayAlert("通知", "検索に失敗しました。検索条件を変更して再度検索してください。", "OK");
                    return;
                }
                this.BindingContext = Ad;
            } else {
                await DisplayAlert("警告", "郵便番号は7桁で入力いてください。", "OK");
            }
        }
    }
}

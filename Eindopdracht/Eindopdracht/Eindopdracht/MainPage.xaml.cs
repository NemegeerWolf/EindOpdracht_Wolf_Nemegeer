using Eindopdracht.Models;
using Eindopdracht.Repositories;
using Eindopdracht.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Eindopdracht
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            //test();
            setup();
        }

        
        public async void test()
        {
            List<Book> lstcities = await BooksRepositorie.GetBooksAsync();
            //foreach (Book city in lstcities)
            //{
            //    Debug.WriteLine(city.Title);
            //    Debug.WriteLine(city.Authors);
            //}

            
        }

        private async void setup()
        {
            List<Book> lstbooks = await BooksRepositorie.GetBooksAsync();
            lsvBooks.ItemsSource = lstbooks;

            lsvBooks.ItemSelected += lsvBooks_Clicked;
            
        }

        private void lsvBooks_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            var book = (Book)lsvBooks.SelectedItem;
            if(book != null)
            {
                Navigation.PushAsync(new BookPage(book));
            }
        }
    }
}

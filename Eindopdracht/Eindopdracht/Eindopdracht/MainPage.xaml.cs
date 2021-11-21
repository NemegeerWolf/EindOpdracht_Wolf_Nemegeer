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
        private int pageNumber = 1;

        public MainPage()
        {
            InitializeComponent();
            //test();
            setup();
        }

        
        public async void test()
        {
            List<Book> lstcities = await BooksRepositorie.GetBooksAsync(pageNumber);
            //foreach (Book city in lstcities)
            //{
            //    Debug.WriteLine(city.Title);
            //    Debug.WriteLine(city.Authors);
            //}

            
        }

        private async void setup()
        {
            //imgBack.Source = ImageSource.FromResource("Eindopdracht.Assets.baseline_arrow_back_white_24dp.png");

            List<Book> lstbooks = await BooksRepositorie.GetBooksAsync(pageNumber);
            lsvBooks.ItemsSource = lstbooks;

            lsvBooks.ItemSelected += lsvBooks_Clicked;
            
        }

        private void lsvBooks_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            var book = (Book)lsvBooks.SelectedItem;
            if(book != null)
            {
                Navigation.PushAsync(new BookPage(book));
                lsvBooks.SelectedItem = null;
            }
        }

        private async void btnPreviousPage_Clicked(object sender, EventArgs e)
        {
            pageNumber--;
            if (pageNumber < 1) pageNumber = 1;
            else
            {
                List<Book> lstbooks = await BooksRepositorie.GetBooksAsync(pageNumber);
                lsvBooks.ItemsSource = lstbooks;
            }
            

            lblPageNumber.Text = pageNumber.ToString();
        }

        private async void btnNextPage_Clicked(object sender, EventArgs e)
        {
            pageNumber++;
            if (pageNumber > 2084) pageNumber = 2084;
            else 
            {
                List<Book> lstbooks = await BooksRepositorie.GetBooksAsync(pageNumber);
                lsvBooks.ItemsSource = lstbooks;
            }
            

            lblPageNumber.Text = pageNumber.ToString();
        }
    }
}

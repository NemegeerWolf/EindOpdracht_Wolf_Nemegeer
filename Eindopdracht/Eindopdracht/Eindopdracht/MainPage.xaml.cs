using Eindopdracht.Models;
using Eindopdracht.Repositories;
using Eindopdracht.Services;
using Eindopdracht.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Eindopdracht
{
    public partial class MainPage : ContentPage
    {
        private int pageNumber = 1;
        private string filter = "";
        private string searchCategorie = "";
        private string[] searchCategories = { "", "&search=", "&languages=", "&topic=" };

        private string[] talen = { "En", "Fr", "Nl","De", "Ar", "El (greek)", "Es (Spaans)", "Et (Estonian)" , "Fi (Finnish)", "Fy (Frisian)", "Ga (Gaelic)", "GL (Garlician)", "He (Hebrew)", "Hu (Hungarian)" };

        public MainPage()
        {
            InitializeComponent();
            //test();
            if (Network.NetworkControle() == true)
            {
                setup();
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
                lsvBooks.SelectedItem = null;
            }
        } 

        
        //public async void test()
        //{
        //    List<Book> lstcities = await BooksRepositorie.GetBooksAsync(pageNumber);
        //    //foreach (Book city in lstcities)
        //    //{
        //    //    Debug.WriteLine(city.Title);
        //    //    Debug.WriteLine(city.Authors);
        //    //}

            
        //}

        public async void setup()
        {
            //imgBack.Source = ImageSource.FromResource("Eindopdracht.Assets.baseline_arrow_back_white_24dp.png");

            List<Book> lstbooks = await BooksRepositorie.GetBooksAsync(pageNumber, "");
            lsvBooks.ItemsSource = lstbooks;

            lsvBooks.ItemSelected += lsvBooks_Clicked;
            string[] searchCategoriesDisplay = { "All", "Search", "Languages (2 Letters/ , for multiple)", "Subject", "Liked/Loved" };
            pkSearchCategorie.ItemsSource = searchCategoriesDisplay;
            
    }

        

        private void lsvBooks_Clicked(object sender, SelectedItemChangedEventArgs e)
        {
            // NetworkControle();
            if (Network.NetworkControle() == true)
            {
                var book = (Book)lsvBooks.SelectedItem;
                if (book != null)
                {
                    Navigation.PushAsync(new BookPage(book));
                    lsvBooks.SelectedItem = null;
                }
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
            
        }

        private async void btnPreviousPage_Clicked(object sender, EventArgs e)
        {
            if (Network.NetworkControle() == true)
            {
                pageNumber--;
                if (pageNumber < 1) pageNumber = 1;
                else
                {
                    // NetworkControle();
                    List<Book> lstbooks = await BooksRepositorie.GetBooksAsync(pageNumber, filter);
                    if (lstbooks != null)
                        lsvBooks.ItemsSource = lstbooks;
                    else
                        pageNumber++;
                }
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }


            lblPageNumber.Text = pageNumber.ToString();
        }

        private async void btnNextPage_Clicked(object sender, EventArgs e)
        {
            if (Network.NetworkControle() == true)
            {
                pageNumber++;
            if (pageNumber > 2084) pageNumber = 2084;
            else 
            {
                // NetworkControle();
                List<Book> lstbooks = await BooksRepositorie.GetBooksAsync(pageNumber, filter);
                if (lstbooks != null)
                    lsvBooks.ItemsSource = lstbooks;
                else
                    pageNumber--;


            }
            

            lblPageNumber.Text = pageNumber.ToString();
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
        }

        private async void pkSearchCategorie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Network.NetworkControle() == true)
            {
                // NetworkControle();
                txtSearchTerm.IsEnabled = true;
            txtSearchTerm.Text = "";
            if (pkSearchCategorie.SelectedIndex == 4)
            {
                pageNumber = 1;
                lblPageNumber.Text = pageNumber.ToString();
                List<int> ids = await ReviewRepositorie.GetHartsIdAsync();
                string terms = "";
                foreach(int id in ids)
                {
                    terms += id + ",";
                }

                terms = terms.Substring(0, terms.Length - 1); // verwijder de laatste komma
                List<Book> lstbooks = await BooksRepositorie.GetBooksAsync(pageNumber, "&ids=" + terms) ;
                lsvBooks.ItemsSource = lstbooks;
                filter = "&ids=" + terms;
                txtSearchTerm.IsEnabled = false;
            }
            
            else
            {
            searchCategorie = searchCategories[pkSearchCategorie.SelectedIndex];
            
                if (pkSearchCategorie.SelectedIndex == 0)
                {
                    filter = "";
                    List<Book> lstbooks = await BooksRepositorie.GetBooksAsync(pageNumber, filter);
                    if (lstbooks != null)
                        lsvBooks.ItemsSource = lstbooks;
                }
            }
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            } 
        }

        private async void txtSearchTerm_BindingContextChanged(object sender, EventArgs e)
        {
            //pageNumber = 1;
            //lblPageNumber.Text = pageNumber.ToString();

            //if (searchCategorie == "") { searchCategorie = searchCategories[1]; pkSearchCategorie.SelectedIndex = 0; }

            //filter = searchCategorie + txtSearchTerm.Text;
            
            //List<Book> lstbooks = await BooksRepositorie.GetBooksAsync(pageNumber, filter);
            
            //lsvBooks.ItemsSource = lstbooks; 
        }

        private void txtSearchTerm_Focused(object sender, FocusEventArgs e)
        {
            pkSearchCategorieColumn.Width = 0;
        }

        private async void txtSearchTerm_Unfocused(object sender, FocusEventArgs e)
        {
            if (Network.NetworkControle() == true)
            {
                // NetworkControle();
                pkSearchCategorieColumn.Width = 100;
            pageNumber = 1;
            lblPageNumber.Text = pageNumber.ToString();


            if (searchCategorie == "") 
            { 
                searchCategorie = searchCategories[1];  
            }
            filter = searchCategorie + txtSearchTerm.Text;

            List<Book> lstbooks = await BooksRepositorie.GetBooksAsync(pageNumber, filter);

            lsvBooks.ItemsSource = lstbooks;


            if (pkSearchCategorie.SelectedIndex == 0)
            {
                string t = txtSearchTerm.Text;
                pkSearchCategorie.SelectedIndex = 1;
                txtSearchTerm.Text = t;
            }
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }

        }

        private async void txtSearchTerm_Completed(object sender, EventArgs e)
        {
            if (Network.NetworkControle() == true)
            {
                // NetworkControle();
                pkSearchCategorieColumn.Width = 100;
            pageNumber = 1;
            lblPageNumber.Text = pageNumber.ToString();

              
            if (searchCategorie == "")
            {
                searchCategorie = searchCategories[1];
            }
            filter = searchCategorie + txtSearchTerm.Text;

            List<Book> lstbooks = await BooksRepositorie.GetBooksAsync(pageNumber, filter);

            lsvBooks.ItemsSource = lstbooks;

            if (pkSearchCategorie.SelectedIndex == 0)
                pkSearchCategorie.SelectedIndex = 1;
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
        }
    }
}

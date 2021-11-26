using Eindopdracht.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Eindopdracht.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookPage : ContentPage
    {
        Book book { get; set; }

        public BookPage(Book pbook)
        {
            InitializeComponent();
            book = pbook;
            setup();
        }

        private void setup()
        {
            TapGestureRecognizer recognizer = new TapGestureRecognizer();
            recognizer.Tapped += Recognizer_Tapped;
            imgBack.GestureRecognizers.Add(recognizer);
            imgBack.Source = ImageSource.FromResource("Eindopdracht.Assets.baseline_arrow_back_white_24dp.png");
            imgHard.Source = ImageSource.FromResource("Eindopdracht.Assets.hart_Empty.png");


            imgBook.Source = book.Image_url;
            lblTitle.Text = book.Title;


            lblAuthors.Text = book.Authors_string;
            lblSubjects.Text = book.Subjects_string;
            lblBookshelves.Text = book.Bookshelves_string;
            lblLanguages.Text = book.Languages_string;

            btnReadBook.Clicked += btnReadBook_IsClicked;
            btnWriteReview.Clicked += btnWriteReview_IsClicked;
        }

        private void Recognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnReadBook_IsClicked(object sender, EventArgs e)
        {



            Navigation.PushAsync(new IntoPage(book));

        }
        
        private void btnWriteReview_IsClicked(object sender, EventArgs e)
        {



            Navigation.PushAsync(new ReviewPage(book));

        }


    }
}
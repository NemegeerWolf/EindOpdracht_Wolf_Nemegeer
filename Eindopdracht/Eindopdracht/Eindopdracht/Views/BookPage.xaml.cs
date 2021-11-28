using Eindopdracht.Models;
using Eindopdracht.Repositories;
using Eindopdracht.Services;
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
        Hart hart { get; set; }
        public BookPage(Book pbook)
        {
            InitializeComponent();
            book = pbook;
            setup();
        }

        private async void setup()
        {

            Network.NetworkControle();
            TapGestureRecognizer recognizer = new TapGestureRecognizer();
            recognizer.Tapped += Recognizer_Tapped;
            imgBack.GestureRecognizers.Add(recognizer);
            imgBack.Source = ImageSource.FromResource("Eindopdracht.Assets.baseline_arrow_back_white_24dp.png");

            TapGestureRecognizer recognizer2 = new TapGestureRecognizer();
            recognizer2.Tapped += Like_Tapped;
            imgHard.GestureRecognizers.Add(recognizer2);

            hart = await ReviewRepositorie.GetHartAsync(book.Id);
            if (hart.Like == true)
            {

                imgHard.Source = ImageSource.FromResource("Eindopdracht.Assets.Hart_Full.png");
            }
            else
            {

                imgHard.Source = ImageSource.FromResource("Eindopdracht.Assets.Hart_Empty.png");
            }

            if (hart.id == "" || hart.id == null)
            {
                await ReviewRepositorie.PutHartAsync(book.Id, true, hart.id);
                hart = await ReviewRepositorie.GetHartAsync(book.Id);
            }




            imgBook.Source = book.Image_url;
            lblTitle.Text = book.Title;


            lblAuthors.Text = book.Authors_string;
            lblSubjects.Text = book.Subjects_string;
            lblBookshelves.Text = book.Bookshelves_string;
            lblLanguages.Text = book.Languages_string;

            btnReadBook.Clicked += btnReadBook_IsClicked;
            btnWriteReview.Clicked += btnWriteReview_IsClicked;
        }

        private async void Like_Tapped(object sender, EventArgs e)
        {
            if (Network.NetworkControle() == true)
            {
                if (hart.Like == false)
                {

                    hart.Like = true;
                    await ReviewRepositorie.PutHartAsync(book.Id, true, hart.id);
                    imgHard.Source = ImageSource.FromResource("Eindopdracht.Assets.Hart_Full.png");

                }
                else
                {
                    hart.Like = false;
                    await ReviewRepositorie.PutHartAsync(book.Id, false, hart.id);
                    imgHard.Source = ImageSource.FromResource("Eindopdracht.Assets.Hart_Empty.png");
                }
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());

            }
        }

        private void Recognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void btnReadBook_IsClicked(object sender, EventArgs e)
        {

            if (Network.NetworkControle() == true)
            {

                Navigation.PushAsync(new IntoPage(book));
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());

            }

        }

        private void btnWriteReview_IsClicked(object sender, EventArgs e)
        {
            if (Network.NetworkControle() == true)
            {


                Navigation.PushAsync(new ReviewPage(book));
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());

            }

        }


    }
}
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
    public partial class ReviewPage : ContentPage
    {
        private int rating = 1;
        Book book { get; set; }
        Image[] arrStars = new Image[5];
        public ReviewPage(Book pbook)
        {
            InitializeComponent();
            book = pbook;


            setup();

        }

        private async void setup()
        {
            Network.NetworkControle();
            List<Review> lstReviews = await ReviewRepositorie.GetReviewsAsync(book.Id);

            lsvReviews.ItemsSource = lstReviews;

            TapGestureRecognizer recognizer = new TapGestureRecognizer();
            recognizer.Tapped += Recognizer_Tapped;
            imgBack.GestureRecognizers.Add(recognizer);
            imgBack.Source = ImageSource.FromResource("Eindopdracht.Assets.baseline_arrow_back_white_24dp.png");


            arrStars[0] = imgStar1;
            arrStars[1] = imgStar2;
            arrStars[2] = imgStar3;
            arrStars[3] = imgStar4;
            arrStars[4] = imgStar5;



            foreach (Image star in arrStars)
            {
                TapGestureRecognizer recognizer2 = new TapGestureRecognizer();
                recognizer2.Tapped += Star;
                star.GestureRecognizers.Add(recognizer2);
                star.Source = ImageSource.FromResource("Eindopdracht.Assets.Star_empty.png");
            }
            imgStar1.Source = ImageSource.FromResource("Eindopdracht.Assets.Star_Full.png");
        }

        private void Star(object sender, EventArgs e)
        {
            foreach (Image star in arrStars)
            {

                star.Source = ImageSource.FromResource("Eindopdracht.Assets.Star_empty.png");
            }
            int i = 0;
            Image checkstar = arrStars[0];
            do
            {
                checkstar = arrStars[i];
                checkstar.Source = ImageSource.FromResource("Eindopdracht.Assets.Star_Full.png");
                i++;

            } while (checkstar != sender);
            rating = i;
        }

        private void Recognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void btnSaveReview_Clicked(object sender, EventArgs e)
        {
            if (Network.NetworkControle() == true)
            {
                Review rev = new Review
                {
                    BookId = book.Id,
                    Message = txtReview.Text,
                    Stars = rating
                };

                await ReviewRepositorie.PostReviewsAsync(rev);
                List<Review> lstReviews = await ReviewRepositorie.GetReviewsAsync(book.Id);

                lsvReviews.ItemsSource = lstReviews;
                txtReview.Text = "";
            }
            else
            {
                Navigation.PushAsync(new NoNetworkPage());

            }
            }
        }
    }
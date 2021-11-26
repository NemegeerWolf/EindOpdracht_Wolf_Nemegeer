using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Eindopdracht.Models
{
    class Review
    {
        public DateTime Date { get; set; }
        public int BookId { get; set; }
        public string Message { get; set; }
        public int Stars { get; set; }
        public string Id { get; set; }

        public ImageSource Star1 { get
            {
                return Star(1);
            } 
        }

        public ImageSource Star2
        {
            get
            {
                return Star(2);
            }
        }
        public ImageSource Star3
        {
            get
            {
                return Star(3);
            }
        }
        public ImageSource Star4
        {
            get
            {
                return Star(4);
            }
        }

        public ImageSource Star5
        {
            get
            {
                return Star(5);
            }
        }


        private ImageSource Star(int star)
        {
           if(Stars >= star)
            {
                return ImageSource.FromResource("Eindopdracht.Assets.Star_Full.png");
            }
            else
            {
                return ImageSource.FromResource("Eindopdracht.Assets.Star_empty.png");
            }
           
        }



    }
}

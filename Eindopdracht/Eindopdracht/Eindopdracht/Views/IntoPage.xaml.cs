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
    public partial class IntoPage : ContentPage
    {
        Book book { get; set; }

        public IntoPage(Book pbook)
        {
            InitializeComponent();
            book = pbook;
            Browser.Source = book.Formats["text/html; charset=utf-8"];
        }
    }
}
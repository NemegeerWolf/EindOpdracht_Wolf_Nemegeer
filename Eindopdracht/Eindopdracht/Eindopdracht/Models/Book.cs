using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eindopdracht.Models
{
    public class Book
    {

        
            public int Id { get; set; }
            public string Title { get; set; }
            public Person[] Authors { get; set; } // classe maken -- done
            public Person[] Translators { get; set; } // classe maken -- done
            public string[] Subjects { get; set; }
            public string[] Bookshelves { get; set; }
            public string[] Languages { get; set; }
            public bool Copyright { get; set; }
            
            public IDictionary<string,string> Formats { get; set; }

        public string Authors_string { 
            get 
            {
                string authors = "";
                foreach (Person p in Authors)
                {
                    authors += p.Name + ".  ";
                    
                }
                return authors;
            }
            }

        public string Subjects_string
        {
            get
            {
                string subjects = "";
                foreach (string p in Subjects)
                {
                    subjects += p + ".  ";

                }
                return subjects;
            }
        }

        public string Bookshelves_string
        {
            get
            {
                string bookshelves = "";
                foreach (string p in Bookshelves)
                {
                    bookshelves += p + ".  ";

                }
                return bookshelves;
            }
        }

        public string Languages_string
        {
            get
            {
                string languages = "";
                foreach (string p in Languages)
                {
                    languages += p + ".  ";

                }
                return languages;
            }
        }

        public string Text_Url
        {
            get
            {
                if( Formats.ContainsKey("text/plain;charset=utf-8")){
                    return Formats["text/plain;charset=utf-8"];
                }
                else if (Formats.ContainsKey("text/html"))
                {
                    return Formats["text/html"];
                }
                else
                {
                    return Formats["text/plain"];
                }
                
            }
        } // enkel text/plain; of (text/html)

        public string Image_url
        {
            get
            {
                return Formats["image/jpeg"];
            }
        }
        public int download_count { get; set; }



    }
}

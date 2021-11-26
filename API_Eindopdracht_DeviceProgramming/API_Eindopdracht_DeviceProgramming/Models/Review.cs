using System;
using System.Collections.Generic;
using System.Text;

namespace API_Eindopdracht_DeviceProgramming.Models
{
    class Review
    {
        public int BookId { get; set; }
        public string Message { get; set; }
        public int Stars { get; set; }
        public string Id { get; set; }
    }
}

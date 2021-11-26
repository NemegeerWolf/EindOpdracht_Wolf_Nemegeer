using System;
using System.Collections.Generic;
using System.Text;

namespace API_Eindopdracht_DeviceProgramming.Models
{
    class Review: IComparable<Review>
    {
        public DateTime Date { get; set; }
        public int BookId { get; set; }
        public string Message { get; set; }
        public int Stars { get; set; }
        public string Id { get; set; }

        public int CompareTo(Review comparePart)
        {
            // A null value means that this object is greater.
            if (comparePart == null)
                return 1;

            else
                return comparePart.Date.CompareTo(this.Date);
        }



    }
}

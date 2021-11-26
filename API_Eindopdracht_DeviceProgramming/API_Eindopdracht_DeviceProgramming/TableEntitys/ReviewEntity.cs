using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace API_Eindopdracht_DeviceProgramming.TableEntitys
{
    class ReviewEntity : TableEntity
    {
        
        public string Message { get; set; }
        public int Stars { get; set; }

        public ReviewEntity()
        { }

            public ReviewEntity(string BookId, string RowKey)
        {

            this.PartitionKey = BookId;
            
            this.RowKey = RowKey;

        }
    }
}


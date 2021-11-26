using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eindopdracht.Models
{
    class HartEntity:TableEntity
    {
        public bool Like { get; set; }

        public HartEntity()
        { }

        public HartEntity(string BookId, string RowKey)
        {

            this.PartitionKey = BookId;

            this.RowKey = RowKey;

            this.ETag = "*";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuotesManager.Models
{
    public class Quotes
    {
        public int quoteId { get; set; }
        public string quoteText { get; set; }
        public string author { get; set; }
    }
}
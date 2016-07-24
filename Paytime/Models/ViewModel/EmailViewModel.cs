using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paytime.Models.ViewModel
{
    public class EmailViewModel
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Footer { get; set; }
        public string ApiKey { get; set; }
    }
}
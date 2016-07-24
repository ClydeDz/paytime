using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Paytime.Models.ViewModel
{
    public class AuditViewModel
    {
        public int Id { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public string LastModifiedOn { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
    }
}
using System;

namespace Paytime.Models
{
    public class Audit
    {
        public int Id { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
    }
}

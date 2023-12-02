using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToShare.Models
{
    public class Apply
    {
        public int? Id { get; set; }
        public int? UserId { get; set; }
        public int? PostId { get; set; }
        public DateTime? ApplyTime { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsAproved { get; set; }
    }
}

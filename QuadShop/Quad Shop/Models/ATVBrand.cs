using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Quad_Shop.Models
{
    public class ATVBrand
    {
        [Key]
        public int ATVBrandID { get; set; }
        public string Brand { get; set; }
        public ICollection<ATVDescription> ATVDescriptions { get; set; }
        public ATVBrand()
        {
            ATVDescriptions = new List<ATVDescription>();
        }
    }
}
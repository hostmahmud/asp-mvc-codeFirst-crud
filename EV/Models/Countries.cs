using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EV.Models
{
    public class Countries
    {
        [Key, Column(Order = 0),DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int cId { get; set; }

        [Display(Name ="Country")]
        public string cName { get; set; }

        //nav
        [InverseProperty("nCountry")]
        public ICollection<Immigrants> _nCountry { get; set; }

        [InverseProperty("dCountry")]
        public ICollection<Immigrants> _dCountry { get; set; }

        [InverseProperty("aCountry")]
        public ICollection<Immigrants> _aCountry { get; set; }
    }
}
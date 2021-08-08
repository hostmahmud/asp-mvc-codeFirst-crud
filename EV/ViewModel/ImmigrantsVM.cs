using EV.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EV.ViewModel
{
    public class ImmigrantsVM
    {
        public int iId { get; set; }
        public string iName { get; set; }
        public string iNationality { get; set; }
        public string iPassNo { get; set; }
        public DateTime iDob { get; set; }
        public string iGender { get; set; }
        public string iPurpose { get; set; }
        public string iImmiType { get; set; }
        public string dCountry { get; set; }
        public string aCountry { get; set; }
        public DateTime iDate { get; set; }
        public string iImage { get; set; }
    }
}
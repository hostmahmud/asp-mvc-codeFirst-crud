using mvcCodeFirstEV2.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EV.Models
{
    public class Immigrants
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int iId { get; set; }
        [Required, StringLength(50)]
        [DisplayName("Full Name")]
        public string iName { get; set; }
        [Required, DataType(DataType.Text)]
        [DisplayName("Nationality")]
        [ForeignKey("nCountry")]
        public int? nCountryId { get; set; }
        public Countries nCountry { get; set; }
        [Required]
        [DisplayName("Passport No.")]
        public string iPassNo { get; set; }
        [Required, DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MMMM dd, yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of Birth")]
        [ValidateBirthDay(ErrorMessage = "Date of birth must be less than Today's Date")]
        public DateTime iDob { get; set; }
        [Required, DisplayName("Gender")]
        public string iGender { get; set; }
        [Required, DisplayName("Purpose")]
        public string iPurpose { get; set; }
        [Required, DisplayName("Immigration Type")]
        public string iImmiType { get; set; }

        [DisplayName("Departure Country")]
        [ForeignKey("dCountry")]
        public int? dCountryId { get; set; }
        public Countries dCountry { get; set; }

        [DisplayName("Arrival Country")]
        [ForeignKey("aCountry")]
        public int? aCountryId { get; set; }
        public Countries aCountry { get; set; }

        [Required, DataType(DataType.Date)]
        [DisplayName("Date")]
        public DateTime iDate { get; set; }

        [DisplayName("Image")]
        public string iImage { get; set; }

        [NotMapped]
        public HttpPostedFileBase File { get; set; }

    }
}
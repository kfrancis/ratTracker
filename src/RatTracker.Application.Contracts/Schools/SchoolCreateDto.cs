using System;
using System.ComponentModel.DataAnnotations;

namespace RatTracker.Schools
{
    public class SchoolCreateDto
    {
        [Required]
        [StringLength(SchoolConsts.NameMaxLength, MinimumLength = SchoolConsts.NameMinLength)]
        public string Name { get; set; }
        [Required]
        [StringLength(SchoolConsts.Address1MaxLength, MinimumLength = SchoolConsts.Address1MinLength)]
        public string Address1 { get; set; }
        [StringLength(SchoolConsts.Address2MaxLength, MinimumLength = SchoolConsts.Address2MinLength)]
        public string Address2 { get; set; }
        [StringLength(SchoolConsts.Address3MaxLength, MinimumLength = SchoolConsts.Address3MinLength)]
        public string Address3 { get; set; }
        [Required]
        [StringLength(SchoolConsts.CityMaxLength, MinimumLength = SchoolConsts.CityMinLength)]
        public string City { get; set; }
        [Required]
        [RegularExpression(@"^[ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ]( )?\d[ABCEGHJKLMNPRSTVWXYZ]\d$")]
        public string PostalCode { get; set; }
    }
}
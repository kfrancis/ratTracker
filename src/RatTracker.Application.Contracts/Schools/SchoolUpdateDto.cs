using System.ComponentModel.DataAnnotations;

namespace RatTracker.Schools
{
    public class SchoolUpdateDto
    {
        public SchoolUpdateDto()
        {
            Name = string.Empty;
            Address1 = string.Empty;
            Address2 = string.Empty;
            Address3 = string.Empty;
            City = string.Empty;
            PostalCode = string.Empty;
        }

        public SchoolUpdateDto(string name, string address1, string address2, string address3, string city, string postalCode)
        {
            Name = name;
            Address1 = address1;
            Address2 = address2;
            Address3 = address3;
            City = city;
            PostalCode = postalCode;
        }

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
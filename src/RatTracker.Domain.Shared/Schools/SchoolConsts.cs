using System.Globalization;

namespace RatTracker.Schools
{
    public static class SchoolConsts
    {
        private const string DefaultSorting = "{0}Name asc,{0}City asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(CultureInfo.CurrentCulture, DefaultSorting, withEntityName ? "School." : string.Empty);
        }

        public const int NameMinLength = 5;
        public const int NameMaxLength = 500;
        public const int Address1MinLength = 10;
        public const int Address1MaxLength = 100;
        public const int Address2MinLength = 0;
        public const int Address2MaxLength = 100;
        public const int Address3MinLength = 0;
        public const int Address3MaxLength = 100;
        public const int EmailMinLength = 5;
        public const int EmailMaxLength = 100;
        public const int PhoneMinLength = 12;
        public const int PhoneMaxLength = 12;
        public const string PhoneFormat = "###-###-####";
        public const int CityMinLength = 0;
        public const int CityMaxLength = 100;
        public const string PostalCodeRegex = @"/^[ABCEGHJ-NPRSTVXY]\d[ABCEGHJ-NPRSTV-Z][ -]?\d[ABCEGHJ-NPRSTV-Z]\d$/i";
    }
}
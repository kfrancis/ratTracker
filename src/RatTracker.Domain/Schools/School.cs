using System;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace RatTracker.Schools
{
    public class School : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Name { get; set; }

        [NotNull]
        public virtual string Address1 { get; set; }

        [CanBeNull]
        public virtual string Address2 { get; set; }

        [CanBeNull]
        public virtual string Address3 { get; set; }

        [NotNull]
        public virtual string City { get; set; }

        [NotNull]
        public virtual string PostalCode { get; set; }

        [CanBeNull]
        public virtual string Email { get; set; }

        [CanBeNull]
        public virtual string Phone { get; set; }

        [CanBeNull]
        public virtual string SchoolFamily { get; set; }

        [CanBeNull]
        public virtual string SchoolType { get; set; }

        [CanBeNull]
        public virtual string Website { get; set; }

        public School()
        {

        }

        public School(Guid id, string name, string address1, string address2, string address3, string city, string postalCode, string email, string phone, string schoolFamily, string schoolType, string website)
        {
            Id = id;
            Check.NotNull(name, nameof(name));
            Check.Length(name, nameof(name), SchoolConsts.NameMaxLength, SchoolConsts.NameMinLength);
            Check.NotNull(address1, nameof(address1));
            Check.Length(address1, nameof(address1), SchoolConsts.Address1MaxLength, SchoolConsts.Address1MinLength);
            if (!string.IsNullOrEmpty(address2)) Check.Length(address2, nameof(address2), SchoolConsts.Address2MaxLength, SchoolConsts.Address2MinLength);
            if (!string.IsNullOrEmpty(address3)) Check.Length(address3, nameof(address3), SchoolConsts.Address3MaxLength, SchoolConsts.Address3MinLength);
            Check.NotNull(city, nameof(city));
            Check.Length(city, nameof(city), SchoolConsts.CityMaxLength, SchoolConsts.CityMinLength);
            Check.NotNull(postalCode, nameof(postalCode));
            if (!string.IsNullOrEmpty(email))
            {
                Check.Length(email, nameof(email), SchoolConsts.EmailMaxLength, SchoolConsts.EmailMinLength);
            }
            if (!string.IsNullOrEmpty(phone))
            {
                //Check.Length(phone, nameof(phone), SchoolConsts.PhoneMaxLength, SchoolConsts.PhoneMinLength);
            }
            Name = name;
            Address1 = address1;
            Address2 = address2;
            Address3 = address3;
            City = city;
            PostalCode = postalCode;
            Email = email;
            Phone = phone;
            SchoolFamily = schoolFamily;
            SchoolType = schoolType;
            Website = website;
        }
    }
}
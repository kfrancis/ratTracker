﻿using CsvHelper;
using CsvHelper.Configuration;
using RatTracker.Schools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace RatTracker.DbMigrator
{
    public class TenantDataSeedContributer : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<School, Guid> _schoolRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;

        public TenantDataSeedContributer(
            IRepository<School, Guid> schoolRepository,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant)
        {
            _schoolRepository = schoolRepository;
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            using (_currentTenant.Change(context?.TenantId))
            {
                if (await _schoolRepository.GetCountAsync() > 0)
                {
                    return;
                }

                await ProcessFileAsync("Data\\Alternative_Learning_Centres.csv");
                await ProcessFileAsync("Data\\Elementary_Schools.csv");
                await ProcessFileAsync("Data\\Secondary_Schools.csv");

                var newCount = await _schoolRepository.GetCountAsync();
                await Console.Out.WriteLineAsync($"Imported {newCount} records");
            }
        }

        private async Task ProcessFileAsync(string filePath)
        {
            var minTypeDef = new
            {
                gid = default(int),
                Name = string.Empty,
                description = string.Empty,
                Address = string.Empty,
                Phone_Number = string.Empty,
                Website = string.Empty,
                Email = string.Empty
            };

            //var medTypeDef = new
            //{
            //    gid = default(int),
            //    Name = string.Empty,
            //    description = string.Empty,
            //    Address = string.Empty,
            //    Phone_Number = string.Empty,
            //    Website = string.Empty,
            //    Email = string.Empty,
            //    School_Type = string.Empty,
            //};

            //var lgTypeDef = new
            //{
            //    gid = default(int),
            //    Name = string.Empty,
            //    description = string.Empty,
            //    Address = string.Empty,
            //    Phone_Number = string.Empty,
            //    Website = string.Empty,
            //    Email = string.Empty,
            //    School_Type = string.Empty,
            //    Family_of_School = string.Empty
            //};

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var schoolRecords = csv.GetRecords(minTypeDef);
                foreach (var schoolRecord in schoolRecords)
                {

                    var book = new School(
                        id: _guidGenerator.Create(),
                        name: schoolRecord.Name,
                        address1: schoolRecord.Address,
                        address2: string.Empty,
                        address3: string.Empty,
                        city: "Kingston",
                        postalCode: string.Empty,
                        email: schoolRecord.Email,
                        phone: schoolRecord.Phone_Number,
                        schoolFamily: string.Empty, //schoolRecord.Family_of_School,
                        schoolType: string.Empty, //schoolRecord.School_Type,
                        website: schoolRecord.Website
                    );

                    await _schoolRepository.InsertAsync(book);
                }
            }
        }
    }
}
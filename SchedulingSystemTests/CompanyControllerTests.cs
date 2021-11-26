using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace SchedulingSystemTests
{
    public class CompanyControllerTests
    {
        private static DbContextOptions<DataContext> dbContextOptions = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "SchedulingSystem")
            .Options;

        DataContext context;
        IRepository<Company> companyRepository;
        IRepository<Schedule> scheduleRepository;
        CompanyService companyService;
        CompanyController companyController;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new DataContext(dbContextOptions);
            companyRepository = new Repository<Company>(context);
            scheduleRepository = new Repository<Schedule>(context);
            context.Database.EnsureCreated();
            SeedDatabase();
            companyService = new CompanyService(companyRepository, scheduleRepository);
            companyController = new CompanyController(companyService);


        }

        [Test, Order(1)]
        public void HTTPGET_GetCompanyById_ReturnsOk_Test()
        {
            Guid id = Guid.Parse("3c951ae2-1d5b-4cae-b409-e02ba8f9cd2f");
            IActionResult actionResult = companyController.GetCompanyById(id);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
            var companyData = (actionResult as OkObjectResult).Value as CompanyNotifications;
            Assert.That(companyData.CompanyId, Is.EqualTo(Guid.Parse("3c951ae2-1d5b-4cae-b409-e02ba8f9cd2f")));
            Assert.That(companyData.Notifications[0], Is.EqualTo(DateTime.Now.AddDays(1).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
            Assert.That(companyData.Notifications[1], Is.EqualTo(DateTime.Now.AddDays(5).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
            Assert.That(companyData.Notifications[2], Is.EqualTo(DateTime.Now.AddDays(10).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
            Assert.That(companyData.Notifications[3], Is.EqualTo(DateTime.Now.AddDays(15).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)));
        }

        [Test, Order(2)]
        public void HTTPGET_GetCompanyById_ReturnsNotFound_Test()
        {
            Guid id = Guid.Parse("3c951ae2-1d5b-4cae-b409-e02ba8f9cd2c");
            IActionResult actionResult = companyController.GetCompanyById(id);
            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());

        }

        [Test, Order(3)]
        public void HTTPPOST_InsertCompany_ReturnsOk_Test()
        {
            var newCompany = new InsertCompanyDto()
            {
                CompanyId = Guid.Parse("d17c62b5-64cd-49f6-8d11-b524475b6e0e"),
                CompanyName = "Company 3",
                CompanyNumber = 123,
                CompanyType = "Medium",
                Market = "Norway"
            };
            IActionResult actionResult = companyController.InsertCompany(newCompany);
            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

        }

        [Test, Order(4)]
        public void HTTPPOST_InsertCompany_BadRequest_Test()
        {
            var newCompany = new InsertCompanyDto()
            {
                CompanyId = Guid.Parse("7c8631b3-b0b6-4cf5-ac60-9685acd122ee"),
                CompanyName = "Company 3",
                CompanyNumber = 123,
                CompanyType = "Extra Large",
                Market = "Spain"
            };
            IActionResult actionResult = companyController.InsertCompany(newCompany);
            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());

        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();

        }

        private void SeedDatabase()
        {
            var company = new Company()
            {
                CompanyId = Guid.Parse("3c951ae2-1d5b-4cae-b409-e02ba8f9cd2f"),
                CompanyName = "Company 1",
                CompanyNumber = 1,
                CompanyType = "Large",
                Market = "Finland",
                Schedule = new Schedule
                {
                    Notifications = new List<Notification>()
                        {
                            new Notification {SendingDate = DateTime.Now.AddDays(1)},
                            new Notification {SendingDate = DateTime.Now.AddDays(5)},
                            new Notification {SendingDate = DateTime.Now.AddDays(10)},
                            new Notification {SendingDate = DateTime.Now.AddDays(15)}
                        }
                }
            };
            context.Companies.Add(company);
            context.SaveChanges();
        }



    }
}

using DataAccess;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CompanyService : ICompanyService
    {
        readonly IRepository<Company> _companyRepository;
        readonly IRepository<Schedule> _scheduleRepository;

        public CompanyService(IRepository<Company> companyRepository, IRepository<Schedule> scheduleRepository)
        {
            _companyRepository = companyRepository;
            _scheduleRepository = scheduleRepository;
        }

        public ReturnResult InsertCompany(InsertCompanyDto companyDto)
        {
            ReturnResult result = new ReturnResult();
            var existingCompany = _companyRepository.GetSingle(c => c.CompanyId == companyDto.CompanyId);
            if (existingCompany != null)
            {
                result.Message = "Company already exists";
                return result;
            }

            Company company = new Company();
            company.CompanyId = companyDto.CompanyId;
            company.CompanyName = companyDto.CompanyName;
            company.CompanyNumber = companyDto.CompanyNumber;
            company.CompanyType = companyDto.CompanyType;
            company.Market = companyDto.Market;

            List<string> validCompanyTypes = new List<string>() { "SMALL", "MEDIUM", "LARGE" };
            var companyType = company.CompanyType.Trim().ToUpper();
            var isValidCompanyType = validCompanyTypes.Contains(companyType);
            if (!isValidCompanyType)
            {
                result.Message = "Company type is not valid";
                return result;
            }

            List<string> validCompanyMarket = new List<string>() { "FINLAND", "NORWAY", "SWEDEN", "DENMARK" };
            var companyMarket = company.Market.Trim().ToUpper();
            var isValidCompanyMarket = validCompanyMarket.Contains(companyMarket);
            if (!isValidCompanyMarket)
            {
                result.Message = "Company type is not valid";
                return result;
            }

            if (companyMarket == "DENMARK" && isValidCompanyType)
            {
                company.Schedule = new Schedule()
                {
                    Notifications = new List<Notification>
                    {
                        new Notification{ SendingDate = DateTime.Now.AddDays(1) },
                        new Notification{ SendingDate = DateTime.Now.AddDays(5) },
                        new Notification{ SendingDate = DateTime.Now.AddDays(10) },
                        new Notification{ SendingDate = DateTime.Now.AddDays(15) },
                        new Notification{ SendingDate = DateTime.Now.AddDays(20) }
                    }
                };
            }

            if (companyMarket == "NORWAY" && isValidCompanyType)
            {
                company.Schedule = new Schedule()
                {
                    Notifications = new List<Notification>
                    {
                        new Notification{ SendingDate = DateTime.Now.AddDays(1) },
                        new Notification{ SendingDate = DateTime.Now.AddDays(5) },
                        new Notification{ SendingDate = DateTime.Now.AddDays(10) },
                        new Notification{ SendingDate = DateTime.Now.AddDays(20) }
                    }
                };
            }

            if (companyMarket == "SWEDEN" && (companyType == "SMALL" || companyType == "MEDIUM"))
            {
                company.Schedule = new Schedule()
                {
                    Notifications = new List<Notification>
                    {
                        new Notification{ SendingDate = DateTime.Now.AddDays(1) },
                        new Notification{ SendingDate = DateTime.Now.AddDays(7) },
                        new Notification{ SendingDate = DateTime.Now.AddDays(14) },
                        new Notification{ SendingDate = DateTime.Now.AddDays(28) }
                    }
                };
            }

            if (companyMarket == "FINLAND" && companyType == "LARGE")
            {
                company.Schedule = new Schedule()
                {
                    Notifications = new List<Notification>
                    {
                        new Notification{ SendingDate = DateTime.Now.AddDays(1) },
                        new Notification{ SendingDate = DateTime.Now.AddDays(5) },
                        new Notification{ SendingDate = DateTime.Now.AddDays(10) },
                        new Notification{ SendingDate = DateTime.Now.AddDays(15) },
                        new Notification{ SendingDate = DateTime.Now.AddDays(20) }
                    }
                };
            }

            _companyRepository.Add(company);
            int affected = _companyRepository.Save();
            if (affected > 0)
            {
                result.IsSuccess = true;
                result.Message = "Company was created successfully";
            }
            else
            {
                result.Message = "Error occurred during creation of company";
            }

            return result;
        }

        public CompanyNotifications GetCompanyById(Guid id)
        {
            var result = new CompanyNotifications();

            var company = _companyRepository.GetSingle(c => c.CompanyId == id, c => c.Schedule);
            if (company != null)
            {
                result.CompanyId = company.CompanyId;
                var schedule = _scheduleRepository.GetSingle(s => s.ScheduleId == company.ScheduleId, s => s.Notifications);
                if (schedule != null)
                {
                    foreach (var item in schedule.Notifications)
                    {
                        string sendingDate = item.SendingDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                        result.Notifications.Add(sendingDate);
                    }
                }
            }
            return result;
        }

    }
}

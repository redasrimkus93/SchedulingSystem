using DataAccess;
using System;

namespace Services
{
    public interface ICompanyService
    {
        CompanyNotifications GetCompanyById(Guid id);
        ReturnResult InsertCompany(InsertCompanyDto company);
    }
}
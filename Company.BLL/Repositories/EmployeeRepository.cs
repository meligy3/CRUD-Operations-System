using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class EmployeeRepository :GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly CompanyDbContext dbcontext;

        public EmployeeRepository(CompanyDbContext dbcontext) : base(dbcontext) 
        {
            this.dbcontext = dbcontext;
        }

        public IQueryable<Employee> GetEmployeesByAddress(string address)
        {
            return dbcontext.Employees.Where(e => e.Address == address);
        }

        public IQueryable<Employee> GetEmployeesByName(string name)
        => dbcontext.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower()));
    }
}

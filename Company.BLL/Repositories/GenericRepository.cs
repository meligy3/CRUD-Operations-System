using Company.BLL.Interfaces;
using Company.DAL.Contexts;
using Company.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Repositories
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class  
    {
        private readonly CompanyDbContext dbContext;
        // DI
        public GenericRepository(CompanyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Add(T iteam)
            =>await dbContext.Set<T>().AddAsync(iteam);
          
        

        public void Delete(T iteam)
        => dbContext.Set<T>().Remove(iteam);




        public async Task<T> Get(int id)
         =>await dbContext.Set<T>().FindAsync(id);

        public async Task<IEnumerable<T>> GetAll()
        {
            if(typeof(T) == typeof (Employee))
            {
                return (IEnumerable<T>)await dbContext.Employees.Include(e => e.Department).ToListAsync();
            }
            return await dbContext.Set<T>().ToListAsync();
        }

        public void Update(T iteam)
        => dbContext.Set<T>().Update(iteam);


    
    }
}

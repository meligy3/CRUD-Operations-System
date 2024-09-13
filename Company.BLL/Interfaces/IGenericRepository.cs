using Company.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.BLL.Interfaces
{
    public interface IGenericRepository<T>
    { // Task 
       Task<IEnumerable<T>> GetAll();

        Task<T> Get(int id);

        Task Add(T iteam);

        void Delete(T iteam);

        void Update(T iteam);


    }
}

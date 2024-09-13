using AutoMapper;
using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Company.PL.Helpers;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
	[Authorize]

	public class EmployeeController : Controller
    { 
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

      

        public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper) // ask clr to create object from Employee repository
        {
       
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IActionResult> Index(string SearchValue)
        {
            IEnumerable<Employee> employees;
            if (string.IsNullOrEmpty(SearchValue))
                employees =await unitOfWork.EmployeeRepository.GetAll();
            else
                employees = unitOfWork.EmployeeRepository.GetEmployeesByName(SearchValue);

            // var employees = EmployeeRepository.GetAll();

            var MappedEmp = mapper.Map<IEnumerable<Employee>,IEnumerable<EmployeeViewModel>>(employees);


            return View(MappedEmp);
        }

        public IActionResult Create()
        {
           // ViewBag.Departments = DepartmentRepository.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeeViewModel EmployeeVM)
        {
            if (ModelState.IsValid) // serverv side validation 
            {   // maping || manual mapping
                //Employee mappEmployee = new Employee()
                //{ 
                // Name = EmployeeVM.Name,
                // Age = EmployeeVM.Age,
                // Salary = EmployeeVM.Salary,    
                // Email = EmployeeVM.Email,
                // PhoneNumber = EmployeeVM.PhoneNumber,
                // Address = EmployeeVM.Address,
                // IsActive = EmployeeVM.IsActive,
                // HireDate = EmployeeVM.HireDate,
                // DepartmentId = EmployeeVM.DepartmentId,
                //};

                // auto mapper
                var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);
                MappedEmp.ImageName = DocumentSettings.UploadImage(EmployeeVM.Image,"Images");
                await unitOfWork.EmployeeRepository.Add(MappedEmp);
                int Result =await unitOfWork.Complete(); 
                if (Result > 0)
                {
                    TempData["Message"] = "Employee Is Created!";
                }
                //3. temp data => dictionary object 
                // transfer data from action to action 
              
                return RedirectToAction(nameof(Index));
            }
            return View(EmployeeVM);
        }

        // new details ((((((((( Employee ? employee ? ))))))))) 
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var employee =await unitOfWork.EmployeeRepository.Get(id.Value);

            if (employee is null)
                return NotFound();
            var MappedEmp = mapper.Map<Employee, EmployeeViewModel>(employee);

            return View(ViewName, MappedEmp);
        }
        [HttpGet]
        // new details
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
            //if (id is null)
            //    return BadRequest();
            //var Employee = EmployeeRepository.Get(id.Value);

            //if (Employee is null)
            //    return NotFound();

            //return View(Employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel EmployeeVM)
        {
            if (id != EmployeeVM.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);

                    unitOfWork.EmployeeRepository.Update(MappedEmp);
                  await  unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    //1. log execption
                    //2. friendly message
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }
            return View(EmployeeVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] int id, EmployeeViewModel EmployeeVM)
        {
            if (id != EmployeeVM.Id)
                return BadRequest();
            try
            {
                var MappedEmp = mapper.Map<EmployeeViewModel, Employee>(EmployeeVM);

                unitOfWork.EmployeeRepository.Delete(MappedEmp);
               await unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(EmployeeVM);
            }

        }
    }
}

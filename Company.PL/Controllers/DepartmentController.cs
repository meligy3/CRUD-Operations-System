using Company.BLL.Interfaces;
using Company.BLL.Repositories;
using Company.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
	[Authorize]

	public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public DepartmentController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            // get all 
            var Departments = await unitOfWork.DepartmentRepository.GetAll();
            return View(Departments);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Department department)
        {
            if (ModelState.IsValid) // serverv side validation 
            {

            await unitOfWork.DepartmentRepository.Add(department);
                int Result =await unitOfWork.Complete();
                if (Result > 0)
                {
                    TempData["Message"] = "Department Is Created!";
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // new details
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id is null)
                return BadRequest();
            var Department =await unitOfWork.DepartmentRepository.Get(id.Value);

            if (Department is null)
                return NotFound();

            return View(ViewName, Department);
        }
        [HttpGet]
        // new details
        public async Task<IActionResult> Edit(int? id)
        {
            return await Details(id, "Edit");
            //if (id is null)
            //    return BadRequest();
            //var Department = departmentRepository.Get(id.Value);

            //if (Department is null)
            //    return NotFound();     

            //return View(Department); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, Department department)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.DepartmentRepository.Update(department);
                    await unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }

                catch (Exception ex)
                {
                    //1. log execption
                    //2. friendly message
                    ModelState.AddModelError(string.Empty, ex.Message);

                }
            }
            return View(department);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute]int id, Department department)
        {
            if (id != department.Id)    
                return BadRequest();
            try
            {
                unitOfWork.DepartmentRepository.Delete(department);
                await unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
                return View(department);    
            }

        }
    
    
    }
}

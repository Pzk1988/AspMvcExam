using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PANExam.DAL;
using PANExam.DAL.DTO;
using PANExam.DAL.Models;
using PANExam.Models;

namespace PANExam.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly DataAccess _dataAccess;

        public DepartmentsController()
        {
            _dataAccess = new DataAccess();
        }

        // GET: Departments
        public ActionResult Index()
        {
            var model = _dataAccess.GetAllDepartments();
            return View(Mapper.Map<List<DepartmentViewModel>>(model));
        }

        // GET: Departments/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentDTO department = _dataAccess.GetDepartment(id.GetValueOrDefault());
            if (department == null)
            {
                return HttpNotFound();
            }

            DepartmentViewModel model = Mapper.Map<DepartmentViewModel>(department);
            model.NumberOfCourses = _dataAccess.GetNumberOfCoursesInDepartment(id.GetValueOrDefault());
            model.NumberOfStudents = _dataAccess.GetNumberOfStudentsInDepartment(id.GetValueOrDefault());
            return View(model);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentViewModel department, HttpPostedFileBase imageFile)
        {
            if (imageFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    imageFile.InputStream.CopyTo(ms);
                    department.Emblem = ms.ToArray();
                }
            }

            if (ModelState.IsValid)
            {
                var departmentDto = Mapper.Map<DepartmentViewModel, DepartmentDTO>(department);
                departmentDto.Id = Guid.NewGuid();
                _dataAccess.AddNewDeprtment(departmentDto);
                return RedirectToAction("Index");
            }

            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentDTO department = _dataAccess.GetDepartment(id.GetValueOrDefault());
            if (department == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<DepartmentViewModel>(department);
            return View(model);
        }

        // POST: Departments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DepartmentViewModel department, HttpPostedFileBase imageFile)
        {
            if (imageFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    imageFile.InputStream.CopyTo(ms);
                    department.Emblem = ms.ToArray();
                }
            }

            if (ModelState.IsValid)
            {
                var departmentDto = Mapper.Map<DepartmentDTO>(department);

                if (imageFile == null)
                {
                    var temp = _dataAccess.GetDepartment(department.Id);
                    departmentDto.Emblem = temp.Emblem;
                }

                _dataAccess.UpdateDepartment(departmentDto);
                return RedirectToAction("Index");
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var department = _dataAccess.GetDepartment(id.GetValueOrDefault());

            if (department == null)
            {
                return HttpNotFound();
            }


            DepartmentViewModel model = Mapper.Map<DepartmentViewModel>(department);
            model.NumberOfCourses = _dataAccess.GetNumberOfCoursesInDepartment(id.GetValueOrDefault());
            model.NumberOfStudents = _dataAccess.GetNumberOfStudentsInDepartment(id.GetValueOrDefault());
            return View(model);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            _dataAccess.DeleteDepartment(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataAccess.Dispose();
            }
            base.Dispose(disposing);
        }

        public FileContentResult GetEmblem(Guid? id)
        {

            byte[] _dummyImage = System.IO.File.ReadAllBytes(Server.MapPath("~/Content/dummy-emblem.jpg"));


            var department = _dataAccess.GetDepartment(id.GetValueOrDefault());
            var model = Mapper.Instance.Map<DepartmentViewModel>(department);

            if (model != null && model.Id == id.GetValueOrDefault())
            {

                FileContentResult fcr = model.Emblem.Length == 0 ?
                    new FileContentResult(_dummyImage, "image/jpg") :
                    new FileContentResult(model.Emblem, "image/jpg");
                return fcr;
            }

            return new FileContentResult(_dummyImage, "image/jpg");
        }
    }
}

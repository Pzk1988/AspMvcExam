using AutoMapper;
using PANExam.DAL;
using PANExam.DAL.DTO;
using PANExam.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PANExam.Controllers
{
    public class StudentsController : Controller
    {
        private DataAccess dataAccess;

        public StudentsController()
        {
            dataAccess = new DataAccess();
        }

        // GET: Students
        public ActionResult Index()
        {
            var model = Mapper.Map<List<StudentViewModel>>(dataAccess.GetAllStudents());
            return View(model);
        }

        //GET:
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        //GET:
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StudentViewModel model)
        {
            if(ModelState.IsValid)
            {
                var modelDTO = Mapper.Map<StudentDTO>(model);
                dataAccess.AddNewStudent(modelDTO);
                return RedirectToAction("Index");
            }
            
            return View(model);
        }

        // GET: Student/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentDTO student = dataAccess.GetStudent(id.GetValueOrDefault());
            if (student == null)
            {
                return HttpNotFound();
            }

            StudentViewModel model = Mapper.Map<StudentViewModel>(student);
            model.TotalEnrollment = dataAccess.GetStudentEnrollmentsAmount(model.Id);
            return View(model);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var student = dataAccess.GetStudent(id.GetValueOrDefault());
            if (student == null)
            {
                return HttpNotFound();
            }

            var model = Mapper.Map<StudentViewModel>(student);
            return View(model);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StudentViewModel student)
        {
            if (ModelState.IsValid)
            { 
                dataAccess.UpdateStudent(Mapper.Map<StudentDTO>(student));
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Student/Delete/5
        [HttpGet]
        public ActionResult Delete(Guid id)
        {
            dataAccess.DeleteStudent(id);
            return RedirectToAction("Index");
        }
    }
}
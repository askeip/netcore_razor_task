using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Razor.Models;

namespace Razor
{
    public class StudentController : Controller
    {
        static List<StudentModel> Students = new List<StudentModel>
        {
            new StudentModel{StudentID = 1, Name = "Evgeny", Surname = "Petrosyan", Course = 4, Group = "FIIT"}
        };

        public IActionResult All()
        {
            return View(Students);
        }

        [HttpGet]
        public IActionResult Profile(int studentID)
        {
            var student = GetStudentModel(studentID);
            if (student != null)
                return View(student);

            return BadRequest();
        }

        [HttpGet]
        public IActionResult Edit(int studentID)
        {
            return View(GetStudentModel(studentID));
        }

        [HttpPost]
        public IActionResult Edit(StudentModel student)
        {
            if (ModelState.IsValid)
            {
                UpdateStudent(student);
                return RedirectToAction("Profile", new {studentID = student.StudentID});
            }

            return View(student);
        }

        [HttpPost]
        public IActionResult Create([FromBody]StudentModel student)
        {
            if (ModelState.IsValid)
            {
                Students.Add(student);
                return RedirectToAction("All");
            }

            return BadRequest();
        }

        [HttpDelete]
        public IActionResult Delete(int studentID)
        {
            var student_index = Students.FindIndex(x => x.StudentID == studentID);
            if (student_index != -1)
            {
                Students.RemoveAt(student_index);
                return RedirectToAction("All");
            }

            return BadRequest();
        }

        private StudentModel GetStudentModel(int studentID)
        {
            return Students.FirstOrDefault(x =>
                x.StudentID == studentID);
        }

        public void UpdateStudent(StudentModel student)
        {
            Students[Students.FindIndex(x => x.StudentID == student.StudentID)] = student;
        }
    }
}

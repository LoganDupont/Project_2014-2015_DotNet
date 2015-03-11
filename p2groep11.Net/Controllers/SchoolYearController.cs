﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using p2groep11.Net.Models;
using p2groep11.Net.Models.DAL;
using p2groep11.Net.Models.Domain;
using p2groep11.Net.ViewModels;

namespace p2groep11.Net.Controllers
{
    public class SchoolYearController : Controller
    {
        private IGradeRepository gradeRepository;


        public SchoolYearController(IGradeRepository gradeRepository)
        {
            this.gradeRepository = gradeRepository;
        }

        // GET: SchoolYear
        [HttpGet]
        public ActionResult Index()
        {
            return View(new SchoolYearFormViewModel(GetYears()));
        }
    
        [HttpPost]
        public ActionResult Index(int SelectedYear)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    Grade grade = gradeRepository.FindById((SelectedYear + 1) / 2);
                    //SchoolYear schoolYear = new SchoolYear(SelectedYear);
                    //Grade grade = gradeRepository.FindBySchoolYear(schoolYear);
                    //List<SchoolYear> lijst = grade.SchoolYears.ToList();
                    switch (grade.GradeId)
                    {
                        case 1:
                            return RedirectToAction("ListCountries", "Continent", new { SelectedYear, continentId = 6});
                        case 2:
                            return RedirectToAction("ListContinents", "Continent", new { SelectedYear });
                        default:
                            return RedirectToAction("Error");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("",e.Message);
                }
               
            }
                return View(new SchoolYearFormViewModel());
        }

        public ActionResult Error()
        {
            return View();
        }
            private List<SelectListItem> GetYears()
       {
           List<SelectListItem> years = new List<SelectListItem>();
           years.Add(new SelectListItem {Value = "1", Text = "1ste leerjaar"});
            for (int i = 2; i < 7; i++)
           {
                years.Add(new SelectListItem{Value = i+"",Text = i+"de leerjaar"});
           }            return years;
        }

    }
}
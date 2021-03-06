﻿using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using p2groep11.Net.Controllers;
using p2groep11.Net.Models;
using p2groep11.Net.Models.DAL;
using p2groep11.Net.Models.Domain;
using p2groep11.Net.ViewModels;


namespace p2groep11.Net.Tests.Controllers
{
    [TestClass]
    public class ClimateChartControllerTest
    {
        private ClimateChartController controller;
        private Mock<IGradeRepository> gradeRepository;
        private Continent continent;
        private LocationListViewModel model;
        private DummyDataContext context;
        private ClimateChartViewModel chartModel;

        [TestInitialize]
        public void Initialize()
        {
            context = new DummyDataContext();
            
            gradeRepository = new Mock<IGradeRepository>();
            gradeRepository.Setup(c => c.FindBySchoolyear(1)).Returns(context.Graad);

            continent = context.Europa;
            controller = new ClimateChartController(gradeRepository.Object);
            //model = new KlimatogramViewModel(controller.GetContinents(), controller.GetCountrys(),
            //    controller.GetLocations());
            

        }

        [TestMethod]
        public void ErrorInShowClimateChartRedirectToSelectSchoolyear()
        {
            controller.ModelState.AddModelError("key", "error");
            RedirectToRouteResult result = controller.ShowExercises(1,1,1,1) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["Action"]);
        }
        //crasht bij viewmodel constructor foreach
        [TestMethod]
        public void ShowClimatogramPassesViewmodelToView()
        {
            ViewResult result =controller.ShowExercises(1, 1, 1, 1)as ViewResult; 
            ClimateChartViewModel model = result.Model as ClimateChartViewModel;
            Assert.AreEqual(model.Months,context.Gent.Months);
        }
    }
}

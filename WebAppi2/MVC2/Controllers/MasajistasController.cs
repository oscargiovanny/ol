﻿using MVC2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC2.Controllers
{
    public class MasajistasController : Controller
    {
        // GET: Masajistas
        public ActionResult Index()
        {

            IEnumerable<mvcMasajistasmodel> empList;
            HttpResponseMessage response = GlobalVariables.WedApiClient.GetAsync("Masajistas").Result;
            empList = response.Content.ReadAsAsync<IEnumerable<mvcMasajistasmodel>>().Result;
            return View(empList);
        }

        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcMasajistasmodel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WedApiClient.GetAsync("Masajistas/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcMasajistasmodel>().Result);
            }
        }
        [HttpPost]

        public ActionResult AddOrEdit(mvcMasajistasmodel emp)
        {
            if (emp.id == 0)
            {
                HttpResponseMessage response = GlobalVariables.WedApiClient.PostAsJsonAsync("Masajistas", emp).Result;
                TempData["SuccesMessage"] = "Saved Succesfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WedApiClient.PutAsJsonAsync("Masajistas/" + emp.id, emp).Result;
                TempData["SuccesMessage"] = "Update Succesfully";
            }

            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WedApiClient.DeleteAsync("Masajistas/" + id.ToString()).Result;
            TempData["SuccesMessage"] = "Delete Succesfully";
            return RedirectToAction("Index");
        }
    }
    }

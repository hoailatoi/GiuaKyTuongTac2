using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class PhonesController : Controller
    {
        // GET: Phones
        public ActionResult Index()
        {
            IEnumerable<mvcPhone> empList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Phones").Result;
            empList = response.Content.ReadAsAsync<IEnumerable<mvcPhone>>().Result;
            return View(empList);
        }
        public ActionResult AddOrEdit(int id = 0)
        {
            if (id == 0)
                return View(new mvcPhone());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("Phones/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcPhone>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEdit(mvcPhone emp)
        {
            if (emp.ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("Phones", emp).Result;
                TempData["SuccessMessage"] = "Saved Successfully";
            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("Phones/" + emp.ID, emp).Result;
                TempData["SuccessMessage"] = "Updated Successfully";
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("Phones/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using LOB.BLL;
using LOB.Core;
using LOB.Web.Models;

namespace LOB.Web.Controllers
{
    public class AccountController : Controller
    {
        //[AllowAnonymous]
        //[ChildActionOnly]
        //public PartialViewResult LoginPartial()
        //{
        //    DataModel model = new DataModel();
        //    model.SelectedUser = new User();
        //    model.SelectedUser.IsAuthenticated = User.Identity.IsAuthenticated;
        //    model.SelectedUser.Email = User.Identity.Name;
        //    return PartialView("LoginPartial", model);
        //}





        public ActionResult UserProfile()
        {
            DataModel model = new DataModel();
            model.SelectedUser = Users.GetUserByEmail(User.Identity.Name);
            if (model.SelectedUser == null)
            {
                return RedirectToAction("Index", "Public");
            }
            model.Roles = Users.GetRoles();
            model.SelectedUser.Email = model.SelectedUser.Email;
            model.SelectedUser.Phone = model.SelectedUser.Phone;
            model.SelectedRole = model.Roles.FirstOrDefault(x => x.Id == model.SelectedUser.Id);
            ViewBag.Title = String.Format("Профиль - {0}", model.SelectedUser.Name);
            return View(model);
        }

        [HttpPost]
        public ActionResult UserProfile(DataModel model)
        {
            model.SelectedUser = Users.GetUserByEmail(User.Identity.Name);
            model.SelectedUser.Role = (LOB.Core.RoleType)model.SelectedRole.Id;
            model.SelectedUser.Phone = model.SelectedUser.Phone;
            model.SelectedUser.Email = model.SelectedUser.Email;
            //Users.UpdateUser(model.SelectedUser);
            return RedirectToAction("UserProfile", "Account");
        }



        public ActionResult Example()
        {
            return View();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Index Page";

            return View();
        }

        

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

        
    }
}
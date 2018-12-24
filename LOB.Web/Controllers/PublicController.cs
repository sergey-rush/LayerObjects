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
    public class PublicController : Controller
    {
        public ActionResult Index()
        {
            var req = Request;
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public PartialViewResult LoginPartial()
        {
            DataModel model = new DataModel();
            model.SelectedUser = new User();
            model.SelectedUser.IsAuthenticated = User.Identity.IsAuthenticated;
            model.SelectedUser.Email = User.Identity.Name;
            return PartialView("LoginPartial", model);
        }

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
            //model.SelectedRole = model.Roles.FirstOrDefault(x => x.Id == model.SelectedUser.Id);
            ViewBag.Title = String.Format("Профиль - {0}", model.SelectedUser.Name);
            return View(model);
        }

        [HttpPost]
        public ActionResult UserProfile(DataModel model)
        {
            model.SelectedUser = Users.GetUserByEmail(User.Identity.Name);
            //model.SelectedUser.Role = (LOB.Core.RoleType)model.SelectedRole.Id;
            model.SelectedUser.Phone = model.SelectedUser.Phone;
            model.SelectedUser.Email = model.SelectedUser.Email;
            //Users.UpdateUser(model.SelectedUser);
            return RedirectToAction("UserProfile", "Account");
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            DataModel model = new DataModel();
            model.SelectedUser = new User();
            model.SelectedUser.RememberMe = true;
            if (User.Identity.IsAuthenticated)
            {
                model.SelectedUser.IsAuthenticated = true;
                model.SelectedUser.Email = User.Identity.Name;
            }
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(DataModel model, string returnUrl)
        {
            if (ModelState.IsValid && BLL.Membership.ValidateUser(model.SelectedUser.Email, model.SelectedUser.Pass))
            {
                FormsAuthentication.SetAuthCookie(model.SelectedUser.Email, model.SelectedUser.RememberMe);
                return RedirectToLocal(returnUrl);
            }
            ModelState.AddModelError("", "Логин или пароль введены неверно");
            return View(model);
        }

        public ActionResult Register()
        {
            ViewBag.Title = "Регистрация";
            DataModel model = new DataModel();
            model.Roles = Users.GetRoles();
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register(DataModel model)
        {
            string email = model.SelectedUser.Email;
            string password = model.SelectedUser.Pass;
            if (ModelState.IsValid)
            {
                RoleType role = RoleType.Users;
                
                User user = BLL.Membership.CreateUser(email, password, role);
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.SelectedUser.Email, model.SelectedUser.RememberMe);
                    return RedirectToAction("UserProfile", "Account");
                }
            }
            ModelState.AddModelError("Registration error", "Некорректный адрес электронной почты или пароль");
            model.Roles = Users.GetRoles();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Public");
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;

using Microsoft.Owin.Security;
using MongoDB.AspNet.Identity;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using System.Collections;
using eWallet.CardSystem.Models;
using System.Web.Security;
using Data;

namespace eWallet.CardSystem.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        CMSDBDataContext context = new CMSDBDataContext();
        //private ApplicationSignInManager _signInManager;
        //private ApplicationUserManager _userManager;

        public AccountController()
        //: this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
            this.UserManager = new UserManager<ApplicationUser>
                (
                new UserStore<ApplicationUser>("CoreDB_Server")
                );
        }
        
        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
        }

        //public ApplicationSignInManager SignInManager
        //{
        //    get
        //    {
        //        return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
        //    }
        //    private set 
        //    { 
        //        _signInManager = value; 
        //    }
        //}

        public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        //public UserManager<ApplicationUser> UserManager { get; private set; }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        public ActionResult UserProfile(string id)
        {
            ViewBag.profile = (from e in context.Users where e.UserId.Equals(id) select e).SingleOrDefault();
            return View();
        }

        public ActionResult Update(string profile_id, string full_name, string card, string role, bool active)
        {
            return View();
        }
        public ActionResult List()
        {
            return View(context.Users.ToArray());
        }
        //
        // POST: /Account/Login
        [HttpPost]
        //[Authorize(Roles = "SYSTEM, MERCHANT, GNC, CUSTOMER")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            dynamic profile = Helper.DataHelper.Get("users", Query.EQ("UserName", model.UserName));

            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.UserName, model.Password);
                if (user != null)
                {
                    if (user.Roles[0] == "SYSTEM" || user.Roles[0] == "MERCHANT" || user.Roles[0] == "GNC")
                    {
                        if (profile.Status != "LOCKED")
                        {
                            await SignInAsync(user, model.RememberMe);
                            return RedirectToLocal(returnUrl);
                        }
                        else
                        {
                            ModelState.AddModelError("", "tài khoản của bạn đang bị khóa vui lòng liên hệ admin để được hỗ trợ !");
                        }
                    }
                    else
                        ModelState.AddModelError("", "tài khoản của bạn không thể đăng nhâp vui lòng liên hệ admin để được hỗ trợ !");
                }
                else
                    ModelState.AddModelError("", "Invalid username or password.");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (CheckIphone(model.Mobile) == true)
                {
                    if (CheckPhoneSupport(model.Mobile) == true)
                    {
                        var user = new ApplicationUser() { UserName = model.UserName };
                        var result = await UserManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            //add Roles to User
                            string[] roles = new string[] { "CUSTOMER" };
                            var roleResult = await UserManager.AddToRoleAsync(user.Id, roles[0]);
                            await SignInAsync(user, isPersistent: false);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            AddErrors(result);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Số điện thoại không chính xác !");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Số điện thoại đã tồn tại!");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }
        // kiem tra sdt
        public static bool CheckIphone(string iphone)
        {
            string a = iphone.Insert(0, "84");
            a = a.Remove(2, 1);
            dynamic profile = Helper.DataHelper.Get("profile", Query.EQ("mobile", a));
            if (profile != null)
            {
                return false;
            }
            return true;
        }
        public static bool CheckPhoneSupport(string phone_number)
        {
            const int RegionConuntryCode = 84;

            if (phone_number.Length == 10)
            {
                if (phone_number.StartsWith("+"))
                {
                    phone_number = phone_number.Replace("+", "0");
                }

                if (phone_number.StartsWith("0" + RegionConuntryCode))
                {
                    phone_number = phone_number.Replace("0" + RegionConuntryCode, "0");
                }
                string[] networkSupport_2 = { "096", "097", "098", "090", "093", "091", "094", "092", "099" };
                const int networkLength = 3;
                var startphone_number = phone_number.Substring(0, networkLength);
                return networkSupport_2.Any(startphone_number.Equals);

            }
            if (phone_number.Length == 11)
            {
                if (phone_number.StartsWith("+"))
                {
                    phone_number = phone_number.Replace("+", "0");
                }

                if (phone_number.StartsWith("0" + RegionConuntryCode))
                {
                    phone_number = phone_number.Replace("0" + RegionConuntryCode, "0");
                }

                string[] networkSupport_1 = {"0162", "0163", "0164", "0165", "0166", "0167", "0168", "0169",
            "0120", "0121", "0122","0126","0128",
            "0123","0124","0125","0127","0129",
            "0188", "0186",
            "0199"};
                const int networkLength_2 = 4;
                var startphone_number_2 = phone_number.Substring(0, networkLength_2);
                return networkSupport_1.Any(startphone_number_2.Equals);
            }
            return false;
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        TempData["alertMessage"] = "Mat khau thay doi thanh cong !";
                        return RedirectToAction("Manage");
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //


        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }





        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult RemoveAccountList()
        {
            var linkedAccounts = UserManager.GetLogins(User.Identity.GetUserId());
            ViewBag.ShowRemoveButton = HasPassword() || linkedAccounts.Count > 1;
            return (ActionResult)PartialView("_RemoveAccountPartial", linkedAccounts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && UserManager != null)
            {
                UserManager.Dispose();
                UserManager = null;
            }
            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        private class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties() { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}
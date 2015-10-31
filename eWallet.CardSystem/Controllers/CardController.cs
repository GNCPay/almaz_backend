using Data;
using eWallet.CardSystem.Models;
using Microsoft.AspNet.Identity;
using MongoDB.AspNet.Identity;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eWallet.CardSystem.Controllers
{
    public class CardController : Controller
    {
        CMSDBDataContext context = new CMSDBDataContext();
        // GET: Card
        [Authorize]
        public ActionResult Index()
        {
            return View(context.Cards.ToArray());
        }

        public ActionResult ExportList()
        {
            return View(context.Cards.ToArray());
        }
        public JsonResult CardListFilter(string keyword, string from_date, string to_date, string counter)
        {
            var list = (from c in context.Cards
                        where c.CardId.Contains(keyword)
                            || c.CustomerCIF.Contains(keyword)
                            || c.CardNumber.Contains(keyword)
                            || c.CardOwnerName.Contains(keyword)
                        select c).ToArray();
            if (!String.IsNullOrEmpty(from_date))
            {
                try
                {
                    DateTime df = DateTime.ParseExact(from_date, "yyyyMMdd", null);
                    list = (from c in list where c.Valid.Value.Date >= df select c).ToArray();
                }
                catch { }
            }

            if (!String.IsNullOrEmpty(to_date))
            {
                try
                {
                    DateTime dt = DateTime.ParseExact(from_date, "yyyyMMdd", null);
                    list = (from c in list where c.Valid.Value.Date <= dt select c).ToArray();
                }
                catch { }
            }
            if (!String.IsNullOrEmpty(counter))
            {
                try
                {
                    list = (from c in list where c.RegisterAt.Equals(counter) select c).ToArray();
                }
                catch { }
            }
            var list_return = (from e in list select e).Select(e => new
            {
                CardId = e.CardId,
                CardNumber = e.CardNumber,
                CardOwnerName = e.CardOwnerName,
                CustomerCIF = e.CustomerCIF,
                PrepaidAmount = e.PrepaidAmount,
                OriginalAmount = e.CardType1.PrepaidValue,
                IsActived = e.IsActived,
                IsLockout = e.IsLockout,
                CardType = e.CardType1.TypeName,
                Valid = e.Valid,
                Expire = e.Expire,
                Balance = e.Balance
            }
                ).ToArray();
            return Json(new { data = list_return },JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult Profile(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                ViewBag.Profile = null;
            }
            else
            {
                var profile_info = Helper.DataHelper.Get("profile", Query.EQ("_id", long.Parse(id)));
                ViewBag.profile = profile_info;
                ViewBag.isLockout = (from e in context.Cards where e.CustomerCIF.Equals(id) select e.IsLockout).SingleOrDefault();
            }
            return View();
        }
        public ActionResult ChangeLock(string card_id)
        {

            //var id = new ObjectId(_id);
            dynamic user = Helper.DataHelper.Get("users", Query.EQ("UserName", card_id));
            ViewBag.Action = (user.Status == "LOCKED") ? "Mở khóa" : "Khóa";
            user.Status = (user.Status == "LOCKED") ? "ACTIVED" : "LOCKED";
            CMSDBDataContext db = new CMSDBDataContext();
            var card = (from element in db.Cards where element.CardId.Equals(card_id) select element).SingleOrDefault();
            if (card != null)
            {
                card.IsLockout = (user.Status == "LOCKED") ? true : false;
                db.SubmitChanges();
                db.Dispose();
            }
            Helper.DataHelper.SaveUpdate("users", user);
            return View();
        }
        public ActionResult ResetPin(string card_id)
        {
            var UserManager = new UserManager<ApplicationUser>
                (
                new UserStore<ApplicationUser>("CoreDB_Server")
                );
            string newPassword = DateTime.Now.ToString("ssHHmm");
            if (!String.IsNullOrEmpty(Helper.DEFAULT_PIN)) newPassword = Helper.DEFAULT_PIN;
            dynamic current_user = Helper.DataHelper.Get("users", Query.EQ("UserName", card_id));
            Helper.DataHelper.Delete("users", current_user._id);
            var new_user = new ApplicationUser() { UserName = card_id, };
            var result = UserManager.Create(new_user, newPassword);
            if (result.Succeeded)
            {
                //add Roles to User
                string[] roles = new string[] { "CUSTOMER" };
                var roleResult = UserManager.AddToRole(new_user.Id, roles[0]);
                dynamic current_profile = Helper.DataHelper.Get("profile", Query.EQ("user_name", card_id));
                //CMSDBDataContext db = new CMSDBDataContext();
                //var card = (from element in db.Cards where element.CardId.Equals(card_id) select element).SingleOrDefault();
                //if (card != null)
                //{
                //    card.IsActived = false;
                //    db.SubmitChanges();
                //    db.Dispose();
                //}
                if (current_profile != null)
                {
                    current_profile.Pin = 1;
                    Helper.DataHelper.Save("profile", current_profile);
                }
            }
            //string token = UserManager.GenerateUserToken("ResetPassword", card_id);

            //bool succeded = UserManager.ResetPassword(card_id, token, newPassword).Succeeded;
            ViewBag.Result = "thành công. Mã PIN mới là " + newPassword;
            return View();
        }
        [Authorize]
        public JsonResult UpdateProfile(string profile_id, string full_name, string mobile, string email, string address, string physical_id)
        {
            try
            {
                dynamic profile_info = Helper.DataHelper.Get("profile", Query.EQ("_id", long.Parse(profile_id)));
                profile_info.full_name = full_name;
                profile_info.mobile = mobile;
                profile_info.email = email;
                profile_info.address = address;
                profile_info.personal_id = physical_id;
                Helper.DataHelper.Save("profile", profile_info);

                CMSDBDataContext db = new CMSDBDataContext();
                Card card = (from e in db.Cards where e.CustomerCIF.Equals(profile_id) select e).SingleOrDefault();
                card.CardOwnerName = full_name;
                db.SubmitChanges();

                return Json(new { error_code = "00", error_message = "Cập nhật thành công" }, JsonRequestBehavior.AllowGet);
            }
            catch { return Json(new { error_code = "96", error_message = "Cập nhật không thành công" }, JsonRequestBehavior.AllowGet); }
        }
        [Authorize]
        public JsonResult CashIn(string card_id, string profile_id, long amount, string note)
        {
            dynamic balance = Helper.GetAccountInfo(card_id);
            dynamic result = Helper.CashIn(profile_id, amount);
            CardActionLog log = new CardActionLog();
            log.ActionAt = "backend";
            log.ActionBy = User.Identity.Name;
            log.ActionCode = "CASHIN";
            log.ActionTime = DateTime.Now;
            log.Amount = amount;
            log.CardId = profile_id;
            log.StartBalance = balance.available_balance;
            log.Note = note;

            if (result.error_code.ToString() == "00")
            {
                balance = Helper.GetAccountInfo(card_id);
                log.EndBalance = balance.available_balance;
                CMSDBDataContext db = new CMSDBDataContext();
                Card card = (from c in db.Cards where c.CustomerCIF.Equals(card_id) select c).Single();
                card.Balance = decimal.Parse("0" + log.EndBalance.ToString());
                db.CardActionLogs.InsertOnSubmit(log);
                db.SubmitChanges();
            }

            return Json(new { error_code = result.error_code, error_message = result.error_message }, JsonRequestBehavior.AllowGet);
        }
        [Authorize]
        public ActionResult ActionLogs()
        {
            return View(context.CardActionLogs.ToArray());
        }
        public ActionResult ExportActionLogs()
        {
            return View(context.CardActionLogs.ToArray());
        }
        [Authorize]
        public ActionResult CardType()
        {
            return View(context.CardTypes.ToArray());
        }


    }
}
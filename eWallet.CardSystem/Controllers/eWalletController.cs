using Data;
using MongoDB.Driver.Builders;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace eWallet.CardSystem.Controllers
{
    public class eWalletController : ApiController
    {
        static CMSDBDataContext db = null;
        public eWalletController()
        {
            if (db == null)
                db = new CMSDBDataContext();
        }
        public dynamic GetProfile(string card_track)
        {
            card_track = card_track.Replace("%", "").Replace("?", "");
            card_track = Regex.Replace(card_track, "[^0-9A-Za-z]+", "");
            try
            {
                CMSDBDataContext db2 = new CMSDBDataContext();
                CardTrace trace = new CardTrace();
                trace.CardTrace1 = card_track;
                trace.CustomerFullName = "TELLER 01";
                trace.CardNumber = "1234679";
                db2.CardTraces.InsertOnSubmit(trace);
                db2.SubmitChanges();
            }
            catch { }
            var user = (from e in db.Users where e.CardID.Equals(card_track) select e).SingleOrDefault();
            if (user == null)
            {
                //card_track = "00187";
                //user = (from e in db.Users where e.CardID.Equals(card_track) select e).SingleOrDefault();
                return null;
            }
            var profile_info = new { Id = user.UserId, FullName = user.FullName, Counter = "Counter 1" , CardId = card_track};
            return profile_info;
        }
        public string GetUpdateProfile(string profile_id, string full_name, string mobile, string email, string address, string physical_id, int Pin)
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

                return "OK";
            }
            catch { return String.Empty; }
        }
        public dynamic Get(string card_id)
        {
            var card_info = (from e in db.Cards where e.CardId.Equals(card_id)
                             select new {
                                 CardId = e.CardId,
                                 CardNumber = e.CardNumber,
                                 CustomerCIF = e.CustomerCIF,
                                 PrepaidAmount = e.PrepaidAmount,
                                 OriginalAmount = e.CardType1.PrepaidValue,
                                 IsActived = e.IsActived,
                                 IsLockout = e.IsLockout,
                                 CardType = e.CardType1.TypeName,
                                 CardOwnerName = e.CardOwnerName
                             }).Single();
            
            //if (card_info == null) return String.Empty;
            //return String.Join("|",
            //    card_info.CardId,
            //    card_info.CardNumber,
            //    card_info.CustomerCIF,
            //    card_info.PrepaidAmount,
            //    card_info.IsActived,
            //    card_info.CardType1.TypeName,
            //    card_info.CardOwnerName);
            return card_info;
        }
        public string GetActive(string card_id, string full_name, string customer_cif)
        {
            using (CMSDBDataContext db = new CMSDBDataContext())
            {
                var card_info = (from e in db.Cards where e.CardId.Equals(card_id) select e).Single();
                if (card_info == null) return String.Empty;
                card_info.CardOwnerName = full_name;
                card_info.CustomerCIF = customer_cif;
                card_info.ActivedDate = DateTime.Now;
                card_info.Valid = card_info.ActivedDate;
                card_info.Expire = ((DateTime)card_info.ActivedDate).AddMonths(6);
                card_info.IsActived = true;
                db.SubmitChanges();
                return "OK";
            }
        }

        public dynamic GetCheckReference(string action_at, string reference_id, string date)
        {
            try {
                DateTime log_date = DateTime.ParseExact(date, "yyyyMMdd",null);
                var action_log = (from e in db.CardActionLogs
                        where e.ActionAt.Equals(action_at)
&& e.ReferenceID.Equals(reference_id)
&& e.ActionTime.Date.Equals(log_date)
                        select e).SingleOrDefault();
                if (action_log == null) return null;
                return new
                {
                    card_id = action_log.Card.CardId,
                    card_number = action_log.Card.CardNumber,
                    action_time = action_log.ActionTime.ToString("HH:mm:ss dd/MM/yyyy"),
                    amount = action_log.Amount
                };
            }
            catch {
                return null;
            }
        }
        public string GetLogAction(string card_id, string start_balance, string end_balance, string reference_id, string amount, string action_code, string action_by, string action_at, string note)
        {
            try
            {
                using (CMSDBDataContext db = new CMSDBDataContext())
                {
                    CardActionLog log = new CardActionLog();
                    log.ActionBy = action_by;
                    log.ActionCode = action_code;
                    log.ActionTime = DateTime.Now;
                    log.CardId = card_id;
                    log.Note = note;
                    log.ActionAt = action_at;
                    log.StartBalance = long.Parse("0" + start_balance);
                    log.EndBalance = long.Parse("0" + end_balance);
                    log.Amount = long.Parse("0" + amount);
                    log.ReferenceID = reference_id;
                    db.CardActionLogs.InsertOnSubmit(log);
                    try {
                        Card card = (from c in db.Cards where c.CardId.Equals(card_id) select c).Single();
                        card.Balance = decimal.Parse("0" + end_balance);
                    }
                    catch { }
                    db.SubmitChanges();
                    return "OK";
                }
            }
            catch
            {
                return String.Empty;
            }
        }

        public dynamic[] GetLogActions(string card_id, int last_number)
        {
            try
            {
                CMSDBDataContext db = new CMSDBDataContext();
                var list = (from e in db.CardActionLogs
                            orderby e.ActionTime descending
                            select (new 
                            {
                                ActionTime = e.ActionTime,
                                ActionCode = e.ActionCode,
                                ActionBy = e.ActionBy,
                                Note = e.Note,
                                CardId = e.CardId,
                                CardNumber = e.Card.CardNumber,
                                StartBalance = e.StartBalance,
                                EndBalance = e.EndBalance,
                                Amount = e.Amount
                            })).Take(last_number).ToArray();

                return list;

            }
            catch
            {
                return null;
            }
        }
    }
}

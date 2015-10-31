using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace eWallet.CardSystem
{
    public class Helper
    {

        public static string DEFAULT_PIN = String.Empty;
            public static eWallet.Data.MongoHelper DataHelper;
        private static CoreAPI.ChannelAPIClient client = new CoreAPI.ChannelAPIClient();
        public static void Init()
        {
            DataHelper = new Data.MongoHelper(
                ConfigurationSettings.AppSettings["CoreDB_Server"],
                ConfigurationSettings.AppSettings["CoreDB_Database"]
                );
            try {
                DEFAULT_PIN = ConfigurationSettings.AppSettings["DEFAULT_PIN"];
            }
            catch { }
        }

        public static dynamic GetAccountInfo(string card_id)
        {
            string request = @"{system:'app_pos_counter', module:'finance', function:'get_balance', type:'two_way', request:{profile_id:"
+ card_id
+ "}}";
            return Helper.RequestToServer(request).response;
        }
        public static dynamic RequestToServer(string request)
        {
            string response = @"{error_code:'96',error_message:'Có lỗi trong quá trình xử lý. Vui lòng thử lại sau'}";
            if (client.State != System.ServiceModel.CommunicationState.Opened)
            {
                try
                {
                    client.Abort();
                    client = new CoreAPI.ChannelAPIClient();
                    client.Open();
                }
                catch
                {

                }
            }
            try
            {
                response = client.Process(request);
            }
            catch
            {
            }
            if (String.IsNullOrEmpty(response))
            {
                response = @"{error_code:'96',error_message:'Có lỗi trong quá trình xử lý. Vui lòng thử lại sau'}";
            }
            return new eWallet.Data.DynamicObj(response);
        }
        public static dynamic CashIn(string profile_id, long amount)
        {
            string request = @"{system:'web_frontend', module:'transaction',type:'two_way', function:'CASHIN',request:{channel:'WEB', profile:'"
               + profile_id + "',service:'GNCP', provider:'VINHOME',payment_provider:'VINHOME',amount: " + amount +
         ", note: '" + "NAP TIEN CHO THE" +
         "'}}";
            dynamic cashin = Helper.RequestToServer(request);
            string trans_id;
            if (cashin.error_code == "00")
            {
                trans_id = cashin.response.trans_id;
                string confirm = @"{system:'web_frontend', module:'transaction',type:'two_way',function:'confirm',request:{user_id:'" + profile_id
                   + "',transaction_type:'" + "CASHIN" + "', trans_id:'" + trans_id + "', amount: " + amount + "}}";
                dynamic confirm_result = Helper.RequestToServer(confirm);
                confirm_result.trans_id = trans_id;
                confirm_result.amount = amount;
                confirm_result.transaction_type = "CASHIN";
                return confirm_result;
            }
            return cashin;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class _Default : Page
    {
        // These are default transaction keys.
        // You can create your own keys in seconds by signing up for a sandbox account here: https://developer.authorize.net/sandbox/
        const string apiLoginId = "89FVdsGYb7f";
        const string transactionKey = "5Lr795D69RH9b7c7";

        const string shippingAddressId = "1223213";
        const decimal amount = 12.34m;
        const string subscriptionId = "1223213";
        const short day = 45;
        const string emailId = "test@test.com";
        string token = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(this.Request.ToString());

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //processTransactionRedirectForm(this);
            processTransaction(this);

        }


        void processTransaction(Page p) {


            var retValue = GetAnAcceptPaymentPage.Run(apiLoginId, transactionKey, 12.34m, p, out token);

            var descriptor = "COMMON.ACCEPT.INAPP.PAYMENT";

            postDataRedirected(token, descriptor, token);
            //postDataIFrame(token, descriptor, token);

        }

        void postDataIFrame(string dataValue, string dataDescriptor, string token)
        {
            dataValue = "";
            dataDescriptor = "";
            NameValueCollection collections = new NameValueCollection();
            collections.Add("token", token);
            string remoteUrl = "https://test.authorize.net/payment/payment";
                       string html = "<html><head>";
            html += "</head><body onload='document.forms[0].submit()'>";
            html += string.Format("< form method = 'post' action = '{0}' id = 'formAuthorizeNetPopup' name = 'formAuthorizeNetPopup' target = 'iframeAuthorizeNet' style = 'display:none;' >", remoteUrl);
            foreach (string key in collections.Keys)
            {
                html += string.Format("<input name='{0}' type='hidden' value='{1}'>", key, collections[key]);
            }
            html += "</form></body></html>";
            Response.Clear();
            Response.ContentEncoding = Encoding.GetEncoding("ISO-8859-1");
            Response.HeaderEncoding = Encoding.GetEncoding("ISO-8859-1");
            Response.Charset = "ISO-8859-1";
            Response.Write(html);
            Response.End();
        }



        void postDataRedirected(string dataValue, string dataDescriptor, string token) {
            NameValueCollection collections = new NameValueCollection();
            collections.Add("dataValue", dataValue);
            collections.Add("dataDescriptor", dataDescriptor);
            collections.Add("token", token);
            string remoteUrl = "https://test.authorize.net/payment/payment";

            string html = "<html><head>";
            html += "</head><body onload='document.forms[0].submit()'>";
            html += string.Format("<form name='PostForm' method='POST' action='{0}'>", remoteUrl);
            foreach (string key in collections.Keys)
            {
                html += string.Format("<input name='{0}' type='hidden' value='{1}'>", key, collections[key]);
            }
            html += "</form></body></html>";
            Response.Clear();
            Response.ContentEncoding = Encoding.GetEncoding("ISO-8859-1");
            Response.HeaderEncoding = Encoding.GetEncoding("ISO-8859-1");
            Response.Charset = "ISO-8859-1";
            Response.Write(html);
            Response.End();
        }
    }
}
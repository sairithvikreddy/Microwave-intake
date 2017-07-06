using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btn_feedback_Click(object sender, EventArgs e)
    {
        string email = "WestRegionRANEngineering@DL.t-mobile.com";
        email = "alok.jani@T-Mobile.com;";
        string url = HttpContext.Current.Request.Url.AbsoluteUri;

        //email = "shaque2@t-mobile.com";

        Page.ClientScript.RegisterStartupScript(this.GetType(), "mailto",
           "<script type = 'text/javascript'>parent.location='mailto:" + email + "?subject=Microwave Intake Form Feedback &body= URL : " + url +
           "'</script>");  
    }
}

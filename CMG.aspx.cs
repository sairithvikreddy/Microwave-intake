using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Telerik.Web.UI;
using System.IO;
using System.Security.Principal;
using System.Data.SqlClient;
using System.Drawing;
using System.Data.SqlTypes;
using System.Data;
using System.Net.Mail;
using MWIntakeCommon;

public partial class CMG : System.Web.UI.Page
{
    string s_id = String.Empty;
    ArrayList al_Error = new ArrayList();

    //System.Configuration.ConnectionStringSettingsCollection conStringSettingsCollection = System.Web.Configuration.WebConfigurationManager.ConnectionStrings;
    string conString = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        String currurl = HttpContext.Current.Request.RawUrl;
        var parsed = HttpUtility.ParseQueryString(currurl);
        string siteid = parsed["siteid"];
        string action = parsed["action"]; 
        string requester = String.Empty;
        string status = String.Empty;
        string structuretype = String.Empty;
        string market = String.Empty;
        string s_cmd = String.Empty;
        string timestamp = String.Empty;
        string aa = Request.LogonUserIdentity.Name;
        WindowsIdentity id = System.Security.Principal.WindowsIdentity.GetCurrent();

        conString = MWIntakeCommon.Utility.ConnectionString;
        //if (0 < conStringSettingsCollection.Count)
        //{
        //    foreach (System.Configuration.ConnectionStringSettings conSettings in conStringSettingsCollection)
        //    {
        //        if (conSettings.Name.Equals("ConcordConnectionString_Report"))
        //            conString = conSettings.ConnectionString;
        //    }
        //}

        string userName = id.Name;
        s_id = (userName.Substring(aa.IndexOf('\\') + 1));

        if (!IsPostBack)
        {
            #region Action Create
            //if (action.Equals("create"))
            //{
            //    market = parsed["market"];
            //    requester = parsed["requester"];
            //    structuretype = parsed["structuretype"];
            //    market = parsed["market"];
            //    txt_Requester.Text = requester;
            //    txt_ProjectType.Text = "Desktop Analysis (CMG Only)";
            //}
            #endregion 

            #region Action View
            //if (action.Equals("view"))
            //{
            //    requester = parsed["requester"];
            //    structuretype = parsed["structuretype"];
            //    market = parsed["market"];
            //    timestamp = parsed["timestamp"];
            //    status = parsed["status"];
            //    txt_Requester.Text = requester;
            //    txt_ProjectType.Text = "Desktop Analysis (CMG Only)";
            //    //lbl_ProjectStatus.Text = status;
            //}
            #endregion
        }
        else
        { 
        
        }
    }

    protected void AsyncFileUpload_Donor_Confirmed_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        //System.Threading.Thread.Sleep(5000);

        //if (AsyncFileUpload_Donor_Confirmed.HasFile)
        //{
        //    string[] sa_file = Path.GetFileName(e.FileName).Split('.');
        //    string s_TimeStamp = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        //    string m_AppDir = HttpContext.Current.Request.PhysicalApplicationPath.ToString();

        //    if (!Directory.Exists(m_AppDir + "/Uploads/" + s_id + "/"))
        //        Directory.CreateDirectory(m_AppDir + "/Uploads/" + s_id + "/");

        //    string strPath = MapPath("~/Uploads/" + s_id + "/") + sa_file[0] + "_" + s_TimeStamp + "_Donor_Confirmed" + "." + sa_file[sa_file.Length - 1];

        //    AsyncFileUpload_Donor_Confirmed.SaveAs(strPath);
        //}

        var AsyncFileUpload = AsyncFileUpload_Donor_Confirmed;
        if (AsyncFileUpload.HasFile)
        {
            if (Path.GetExtension(AsyncFileUpload.FileName) == ".xlsx")
            {
                var dt = ExcelPackageExtensions.GetDataTable(AsyncFileUpload.FileContent);

            }
        }
    }

    protected void Candidate_Site1_AsyncFileUpload_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
    {

    }
    protected void Candidate_Site2_AsyncFileUpload_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
    {

    }
    protected void Candidate_Site3_AsyncFileUpload_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
    {

    }
    protected void MicrowaveTopology_AsyncFileUpload_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
    {

    }
}
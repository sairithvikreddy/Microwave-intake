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
using System.Configuration;
using MWIntakeCommon;

public partial class Intake : System.Web.UI.Page
{
    string s_id = String.Empty;
    ArrayList al_Error = new ArrayList();

    System.Configuration.ConnectionStringSettingsCollection conStringSettingsCollection = System.Web.Configuration.WebConfigurationManager.ConnectionStrings;
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

        if (0 < conStringSettingsCollection.Count)
        {
            foreach (System.Configuration.ConnectionStringSettings conSettings in conStringSettingsCollection)
            {
                if (conSettings.Name.Equals("ConcordConnectionString_Report"))
                    conString = conSettings.ConnectionString;
            }
        }

        string userName = id.Name;
        s_id = (userName.Substring(aa.IndexOf('\\') + 1));

        if (!IsPostBack)
        {
            //#region Load DDL            
            //try
            //{
            //    SqlConnection sqlcn = new SqlConnection(conString);
            //    sqlcn.Open();

            //    SqlDataAdapter adapter = new SqlDataAdapter();
            //    DataTable dt_table3 = new DataTable();
            //    adapter.SelectCommand = new SqlCommand("SELECT [Requester] FROM [MW].[dbo].[Requester]", sqlcn);
            //    adapter.Fill(dt_table3);

            //    DDL_Requester.DataSource = dt_table3;
            //    DDL_Requester.DataTextField = "Requester";
            //    DDL_Requester.DataValueField = "Requester";
            //    DDL_Requester.DataBind();

            //    DDL_Requester_SelectedIndexChanged(null, null);

            //    //DDL_Requester.SelectedIndex = 1;
            //}
            //finally
            //{

            //}
            //#endregion 


            



            #region Action Create
            if (action.Equals("create"))
            {
                market = parsed["market"];
                requester = parsed["requester"];
                structuretype = parsed["structuretype"];
                market = parsed["market"];
                DDL_Requester.Text = requester;
                DDL_Market.Text = market;
                Pending_Donor_SiteID.Text = siteid;
                Pending_Donor_Structure_Type.Text = structuretype;
                if (Session["region"] != null)
                    DDL_Region.Text = Session["region"].ToString();

                Pending_Donor_Submit.Enabled= true;
                LOS_Survey_Submit.Enabled = false;
                PRELIM_Design_Submit.Enabled = false;
                Donor_Confirmed_Submit.Enabled = false;
            }
            #endregion 

            #region Action View
            if (action.Equals("view"))
            {
                requester = parsed["requester"];
                structuretype = parsed["structuretype"];
                market = parsed["market"];
                timestamp = parsed["timestamp"];
                status = parsed["status"];
                //populate data from 4 tables
                populate_4_tables(siteid,requester,timestamp);

                lbl_ProjectStatus.Text = status;
            }
            #endregion

            var selectedRequestor = DDL_Requester.Text;
            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlConnection sqlcn = new SqlConnection(conString);
            sqlcn.Open();

            DataTable dt_table3 = new DataTable();
            adapter.SelectCommand = new SqlCommand("SELECT [ProjectType],[Requester] FROM [MW].[dbo].[ProjectType_Requester] where [Requester] ='" + selectedRequestor + "'", sqlcn);
            adapter.Fill(dt_table3);

            DDL_ProjectType.DataSource = dt_table3;
            DDL_ProjectType.DataTextField = "ProjectType";
            DDL_ProjectType.DataValueField = "ProjectType";
            DDL_ProjectType.DataBind();
            DDL_ProjectType_SelectedIndexChanged(null, null);
        }
        else
        { 
        
        }
    }

    protected void DDL_Requester_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedRequestor = DDL_Requester.Text;
        SqlDataAdapter adapter = new SqlDataAdapter();

        SqlConnection sqlcn = new SqlConnection(conString);
        sqlcn.Open();

        DataTable dt_table3 = new DataTable();
        adapter.SelectCommand = new SqlCommand("SELECT [ProjectType],[Requester] FROM [MW].[dbo].[ProjectType_Requester] where [Requester] ='"+selectedRequestor+"'", sqlcn);
        adapter.Fill(dt_table3);

        DDL_ProjectType.DataSource = dt_table3;
        DDL_ProjectType.DataTextField = "ProjectType";
        DDL_ProjectType.DataValueField = "ProjectType";
        DDL_ProjectType.DataBind();
        DDL_ProjectType_SelectedIndexChanged(null, null);
    }

    #region AsyncFileUpload
    protected void AsyncFileUpload_1A2C_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        System.Threading.Thread.Sleep(5000);

        if (AsyncFileUpload_1A2C.HasFile)
        {
            MWIntakeUpload(AsyncFileUpload_1A2C, FileCategory._1A2C);

            //string[] sa_file = Path.GetFileName(e.FileName).Split('.');

            ////if (sa_file[sa_file.Length - 1].ToUpper().Equals("PDF"))
            ////{
            //    string s_TimeStamp = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            //    string m_AppDir = HttpContext.Current.Request.PhysicalApplicationPath.ToString();

            //    if (!Directory.Exists(m_AppDir + "/Uploads/" + s_id + "/"))
            //        Directory.CreateDirectory(m_AppDir + "/Uploads/" + s_id + "/");

            //    string strPath = MapPath("~/Uploads/" + s_id + "/") + sa_file[0] + "_" + s_TimeStamp + "_1A2C" + "." + sa_file[sa_file.Length - 1];
            //    //string strPath = MapPath(m_AppDir + "\\Uploads\\\\" + i_ticket + "\\\\") + Path.GetFileName(e.filename);

            //    AsyncFileUpload_1A2C.SaveAs(strPath);
                //l_Test.Text = "Finished!";
            //}
            //else
            //{
             //    lbl_Error_1.Text = "You could only upload pdf file!";
            //}
        }
    }
    protected void AsyncFileUpload_FAAFCC_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        System.Threading.Thread.Sleep(5000);

        if (AsyncFileUpload_FAAFCC.HasFile)
        {
            MWIntakeUpload(AsyncFileUpload_FAAFCC, FileCategory._FAAFCC);
           // string[] sa_file = Path.GetFileName(e.FileName).Split('.');
           //// if (sa_file[sa_file.Length - 1].ToUpper().Equals("PDF"))
           // //{
           //     string s_TimeStamp = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
           //     string m_AppDir = Utility.MWIntakeUploads;

           //     if (!Directory.Exists(m_AppDir + "/Uploads/" + s_id + "/"))
           //         Directory.CreateDirectory(m_AppDir + "/Uploads/" + s_id + "/");

           //     string strPath = MapPath("~/Uploads/" + s_id + "/") + sa_file[0] + "_" + s_TimeStamp + "_FAAFCC" + "." + sa_file[sa_file.Length - 1];
           //     //string strPath = MapPath(m_AppDir + "\\Uploads\\\\" + i_ticket + "\\\\") + Path.GetFileName(e.filename);

           //     AsyncFileUpload_1A2C.SaveAs(strPath);
                //l_Test.Text = "Finished!";
            //}
           // else
            //{
            //    lbl_Error_1.Text = "You could only upload pdf file!";
           // }
        }
    }
    protected void AsyncFileUpload_Donor_Study_Status_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        //System.Threading.Thread.Sleep(5000);

        //if (AsyncFileUpload_Donor_Study_Status.HasFile)
        //{
        //    string[] sa_file = Path.GetFileName(e.FileName).Split('.');

        //   // if (sa_file[sa_file.Length - 1].ToUpper().Equals("PDF"))
        //   // {
        //        string s_TimeStamp = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
        //        string m_AppDir = HttpContext.Current.Request.PhysicalApplicationPath.ToString();

        //        if (!Directory.Exists(m_AppDir + "/Uploads/" + s_id + "/"))
        //            Directory.CreateDirectory(m_AppDir + "/Uploads/" + s_id + "/");

        //        string strPath = MapPath("~/Uploads/" + s_id + "/") + sa_file[0] + "_" + s_TimeStamp + "_Donor_Study_Status" + "." + sa_file[sa_file.Length - 1];
        //        //string strPath = MapPath(m_AppDir + "\\Uploads\\\\" + i_ticket + "\\\\") + Path.GetFileName(e.filename);

        //        AsyncFileUpload_Donor_Study_Status.SaveAs(strPath);
        //        //l_Test.Text = "Finished!";
        //    //}
        //   // else
        //    //{
        //    //    lbl_Error_1.Text = "You could only upload pdf file!";
        //    //}
        //}
    }

    protected void MicrowaveTopology_AsyncFileUpload_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if(MicrowaveTopology_AsyncFileUpload.HasFile)
        {
            MWIntakeUpload(MicrowaveTopology_AsyncFileUpload, FileCategory._topology);
        }
    }

    protected void Candidate_Site1_AsyncFileUpload_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if (Candidate_Site1_AsyncFileUpload.HasFile)
        {
            MWIntakeUpload(Candidate_Site1_AsyncFileUpload, FileCategory._donor1);
        }
    }

    protected void Candidate_Site2_AsyncFileUpload_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if (Candidate_Site2_AsyncFileUpload.HasFile)
        {
            MWIntakeUpload(Candidate_Site2_AsyncFileUpload, FileCategory._donor2);
        }
    }

    protected void Candidate_Site3_AsyncFileUpload_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if (Candidate_Site3_AsyncFileUpload.HasFile)
        {
            MWIntakeUpload(Candidate_Site3_AsyncFileUpload, FileCategory._donor3);
        }
    }

    protected void AsyncFileUpload_LOS_Survey_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        System.Threading.Thread.Sleep(5000);

        if (AsyncFileUpload_LOS_Survey.HasFile)
        {
            MWIntakeUpload(AsyncFileUpload_LOS_Survey, FileCategory._lossurvey);
            //string[] sa_file = Path.GetFileName(e.FileName).Split('.');

            ////if (sa_file[sa_file.Length - 1].ToUpper().Equals("PDF"))
            ////{
            //    string s_TimeStamp = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            //    string m_AppDir = HttpContext.Current.Request.PhysicalApplicationPath.ToString();

            //    if (!Directory.Exists(m_AppDir + "/Uploads/" + s_id + "/"))
            //        Directory.CreateDirectory(m_AppDir + "/Uploads/" + s_id + "/");

            //    string strPath = MapPath("~/Uploads/" + s_id + "/") + sa_file[0] + "_" + s_TimeStamp + "_LOS_Survey" + "." + sa_file[sa_file.Length - 1];
            //    //string strPath = MapPath(m_AppDir + "\\Uploads\\\\" + i_ticket + "\\\\") + Path.GetFileName(e.filename);

            //    AsyncFileUpload_LOS_Survey.SaveAs(strPath);
                //l_Test.Text = "Finished!";
            //}
            //else
            //{
           //     lbl_Error_1.Text = "You could only upload pdf file!";
            //}
        }
    }
    protected void AsyncFileUpload_Donor_Confirmed_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        System.Threading.Thread.Sleep(5000);

        if (AsyncFileUpload_Donor_Confirmed.HasFile)
        {
            MWIntakeUpload(AsyncFileUpload_Donor_Confirmed, FileCategory._prelimdesign);
            //string[] sa_file = Path.GetFileName(e.FileName).Split('.');

            ////if (sa_file[sa_file.Length - 1].ToUpper().Equals("PDF"))
            ////{
            //    string s_TimeStamp = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            //    string m_AppDir = HttpContext.Current.Request.PhysicalApplicationPath.ToString();

            //    if (!Directory.Exists(m_AppDir + "/Uploads/" + s_id + "/"))
            //        Directory.CreateDirectory(m_AppDir + "/Uploads/" + s_id + "/");

            //    string strPath = MapPath("~/Uploads/" + s_id + "/") + sa_file[0] + "_" + s_TimeStamp + "_Donor_Confirmed" + "." + sa_file[sa_file.Length - 1];
            //    //string strPath = MapPath(m_AppDir + "\\Uploads\\\\" + i_ticket + "\\\\") + Path.GetFileName(e.filename);

            //    AsyncFileUpload_Donor_Confirmed.SaveAs(strPath);
                //l_Test.Text = "Finished!";
            //}
            //else
            //{
            //    lbl_Error_1.Text = "You could only upload pdf file!";
           // }
        }
    }
    #endregion 

    #region Submit
    protected void Pending_Donor_Submit_Click(object sender, EventArgs args)
    {
        l_Submitted_1.Text = "";

        if (Pending_Donor_Cluster_Type.SelectedValue  == "Existing")
        {
            if (string.IsNullOrEmpty(Pending_Donor_Cluster_ID.Text))
            {
                l_Submitted_1.Text = "Cluster_ID cant be blank. ";
                return;
            }

            //try
            //{
            //    var int_temp = Convert.ToInt16(Pending_Donor_Cluster_ID.Text);
            //}
            //catch (Exception ex)
            //{
            //    l_Submitted_1.Text = "Cluster ID should be integer.";
            //    return;
            //}
        }

        //validation
        try
        {
            var int_temp = Convert.ToInt16(Pending_Donor_Structure_Height.Text);
        }
        catch (Exception ex)
        {
            l_Submitted_1.Text = "Structure_Height should be integer.";
            return;
        }

        try
        {
            var int_temp = Convert.ToInt16(Pending_Donor_Known_Available_Center_Line.Text);
        }
        catch (Exception ex)
        {
            l_Submitted_1.Text = "Known Available Center Line should be integer.";
            return;
        }

        try
        {
            var int_temp = Convert.ToInt16(Pending_Donor_Requested_Throughput.Text);
        }
        catch (Exception ex)
        {
            l_Submitted_1.Text = "Requested Throughput should be integer.";
            return;
        }

        if (
            DDL_ProjectType.SelectedItem != null && 
            !string.IsNullOrEmpty(DDL_ProjectType.SelectedItem.Value) && 
            (DDL_ProjectType.SelectedItem.Value.Contains("Modification") || DDL_ProjectType.SelectedItem.Value.Contains("Upgrade"))
           )
        {
            try
            {
                var int_temp = Convert.ToInt16(Pending_Donor_Existing_CIR_Value.Text);
            }
            
            catch (Exception ex)
            {
                l_Submitted_1.Text = "Existing CIR Value should be integer.";
                return;
            }
        }

        if (Pending_Donor_Existing_CIR_Value.Text == "") {
            var inter = 2;
        }

        try
        {
            var int_temp = Convert.ToInt16(Pending_Donor_New_CIR_Value.Text);
        }
        catch (Exception ex)
        {
            l_Submitted_1.Text = "New CIR Value should be integer.";
            return;
        }

        //submit the inputs
        DateTime datetime_now = DateTime.Now;
        string s_cmd = String.Empty;

        Pending_Donor_Desktop_Analysis_Request_Date.Text = datetime_now.ToString("MM/dd/yyyy");

        SqlConnection sqlcn = new SqlConnection(conString);
        sqlcn.Open();

        s_cmd = "insert into [MW].[dbo].MW_Pending_Donor (Requester,Project_Type,State_Date,End_Date,Licensed,Region,Market,Site_ID,Structure_Type,Structure_Height,Center_Line,Requested_Throughput,Proposed_On_Air_Date,Cluster_Type,Cluster_ID,Cluster_Topology,Existing_CIR_Value,New_CIR_Value,Desktop_Analysis_Request_Date,Notes_Comments,Date_Requested_Key,Updated_By,Status,DoD_56Kms,Market_Recommended_Donor) values('" 
            + DDL_Requester.Text + "','" + 
            DDL_ProjectType.SelectedItem.Text + "'," +
            (string.IsNullOrEmpty(ProjectType_StartTime.Text) ? "NULL" : (object)"'" + ProjectType_StartTime.Text + "'") + "," +
            (string.IsNullOrEmpty(ProjectType_EndTime.Text) ? "NULL" : (object)"'" + ProjectType_EndTime.Text + "'") + "," + 
            (RBL_ProjectTypeLicensedUnlicensed.SelectedItem == null ? (-1) : (RBL_ProjectTypeLicensedUnlicensed.SelectedIndex)) + ",'" + DDL_Region.Text + "','" + DDL_Market.Text + "','" + Pending_Donor_SiteID.Text + "','" + Pending_Donor_Structure_Type.Text + "',"
            + (Pending_Donor_Structure_Height.Text == "" ? ("null") : (Pending_Donor_Structure_Height.Text)) + "," + (Pending_Donor_Known_Available_Center_Line.Text == "" ? ("null") : (Pending_Donor_Known_Available_Center_Line.Text)) + "," + (Pending_Donor_Requested_Throughput.Text == "" ? ("null") : (Pending_Donor_Requested_Throughput.Text)) + ", "
            + (string.IsNullOrEmpty(Pending_Donor_Proposed_On_Air_Date.Text) ? "NULL" : (object)"'"+Pending_Donor_Proposed_On_Air_Date.Text+"'") + ", '" + Pending_Donor_Cluster_Type.SelectedItem.Value + "','" + Pending_Donor_Cluster_ID.Text + "', '" + Pending_Donor_Cluster_Topology.Text + "', " + (Pending_Donor_Existing_CIR_Value.Text == "" ? ("null") : (Pending_Donor_Existing_CIR_Value.Text)) + "," + (Pending_Donor_New_CIR_Value.Text == "" ? ("null") : (Pending_Donor_New_CIR_Value.Text)) + ",'" + Pending_Donor_Desktop_Analysis_Request_Date.Text + "','" + Pending_Donor_Notes_Comments.Text + "','" + datetime_now + "', '" + s_id + "','Pending Donor','" + Pending_Donor_DoD_56.SelectedValue + "','" + Pending_Donor_Market_Recommended_Donner.Text + "')";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            string jkdfksdfs = "";
        }
        finally
        { }

        s_cmd = "insert into [MW].[dbo].MW_LOS_Survey values('" + Pending_Donor_SiteID.Text + "','" + DDL_Requester.Text + "','" + DDL_ProjectType.SelectedItem.Text + "',null,null,null,-1,'" + datetime_now + "', '" + s_id + "','Pending Donor')";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "insert into [MW].[dbo].MW_Prelim_Design_Pending values('" + Pending_Donor_SiteID.Text + "','" + DDL_Requester.Text + "','" + DDL_ProjectType.SelectedItem.Text + "',null,'" + datetime_now + "', '" + s_id + "','Pending Donor')";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "insert into [MW].[dbo].MW_Donor_Confirmed values('" + Pending_Donor_SiteID.Text + "','" + DDL_Requester.Text + "','" + DDL_ProjectType.SelectedItem.Text + "',null,null,null,null,null,null,null,null,'" + datetime_now + "', '" + s_id + "','Pending Donor')";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "insert into [MW].[dbo].MW_Final_Design_BOM_Request (Site_ID,Requester,Project_Type,Final_Design_Requested_Date,Proposed_On_Air_Date,Recipient_Information_SiteID,Final_Center_Line,Final_Dish_Size,Final_Radio_Make,Final_Radio_Model,Radio_Configuration_Type,Date_Requested_Key,Updated_By,Status) values('" + Pending_Donor_SiteID.Text + "','" + DDL_Requester.Text + "','" + DDL_ProjectType.SelectedItem.Text + "',null,null,null,null,null,null,null,null,'" + datetime_now + "', '" + s_id + "','Pending Donor')";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "insert into [MW].[dbo].MW_FCC601Filed values('" + Pending_Donor_SiteID.Text + "','" + DDL_Requester.Text + "','" + DDL_ProjectType.SelectedItem.Text + "',null,null,null,null,null,null,null,null,null,null,null,null,'" + datetime_now + "', '" + s_id + "','Pending Donor',null,null)";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "insert into [MW].[dbo].MW_Final values('" + Pending_Donor_SiteID.Text + "','" + DDL_Requester.Text + "','" + DDL_ProjectType.SelectedItem.Text + "',null,'0','" + datetime_now + "', '" + s_id + "','Pending Donor')";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        //carry over siteid
        Donor_Candidates_SiteID.Text = Pending_Donor_SiteID.Text;

        //disable step 1 button
        Pending_Donor_Submit.Enabled = false;
        LOS_Survey_Submit.Enabled = true;
        Session["timestamp"] = datetime_now;

        sqlcn.Close();

        l_Submitted_1.Text = "Saved !";


        if (true)
        {
            //Send email
            string emailBody = "Intake received for <br/><br/><table><tr><td>Market :</td><td>" + DDL_Market.Text + "</td></tr>";
            emailBody += "<tr><td>Site Id :</td><td>" + Pending_Donor_SiteID.Text + "</td></tr>";
            emailBody += "<tr><td>Requestor :</td><td>" + DDL_Requester.Text + "</td></tr>";
            emailBody += "<tr><td>Project Type :</td><td>" + DDL_ProjectType.SelectedValue + "</td></tr>";
            emailBody += "</table>";
            try
            {


                new Email().Send(DDL_Region.Text, "Intake received - " + Pending_Donor_SiteID.Text, emailBody);

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    protected void LOS_Survey_Submit_Click(object sender, EventArgs e)
    {
        lbl_Error_LOS_Survey.Text = "";
        //validation
        //if (Donor_Candidates_DonorStudyStatus.SelectedIndex == -1)
        //{
        //    lbl_Error_LOS_Survey.Text = "Choose A Donor Study Status.";
        //    return;
        //}

        DateTime datetime_now = DateTime.Now;
        //submit the inputs
        if (Session["timestamp"] != null && Session["timestamp"] != DBNull.Value)
            datetime_now = Convert.ToDateTime(Session["timestamp"]);

        string s_cmd = String.Empty;

        SqlConnection sqlcn = new SqlConnection(conString);
        sqlcn.Open();

        s_cmd = "update [MW].[dbo].MW_Pending_Donor set Status='Los Survey', Updated_By='" + s_id + "'  where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }
        //s_cmd = "update [MW].[dbo].MW_LOS_Survey set Status='Los Survey', Site_ID1='" + Donor_Candidates_SiteID1.Text + "', Site_ID2='" + Donor_Candidates_SiteID2.Text + "', Site_ID3='" + Donor_Candidates_SiteID3.Text + "',Available_Donor_Study_Status=" + Donor_Candidates_DonorStudyStatus.SelectedIndex + ", Updated_By='" + s_id + "'  where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        s_cmd = "update [MW].[dbo].MW_LOS_Survey set Status='Los Survey', Site_ID1='" + Donor_Candidates_SiteID1.Text + "', Site_ID2='" + Donor_Candidates_SiteID2.Text + "', Site_ID3='" + Donor_Candidates_SiteID3.Text + "',Available_Donor_Study_Status=" + "''" + ", Updated_By='" + s_id + "'  where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            string k = "";
        }
        finally
        { }

        s_cmd = "update [MW].[dbo].MW_Prelim_Design_Pending set Status='Los Survey', Updated_By='" + s_id + "'  where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "update [MW].[dbo].MW_Donor_Confirmed set Status='Los Survey', Updated_By='" + s_id + "'  where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "update [MW].[dbo].MW_Final_Design_BOM_Request set Status='Los Survey', Updated_By='" + s_id + "'  where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "update [MW].[dbo].MW_FCC601Filed set Status='Los Survey', Updated_By='" + s_id + "'  where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        
        s_cmd = "update [MW].[dbo].MW_Final set Status='Los Survey' where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            string h = "";
        }
        finally
        { }
        
        //string dt_now_MMDDYYYY = DateTime.Now.ToString("MM/dd/yyyy");
        //LOS_Survey_Upload_Date.Text = dt_now_MMDDYYYY;

        lbl_ProjectStatus.Text = "LOS Survey";

        //disable step 1 button
        Pending_Donor_Submit.Enabled = false;
        LOS_Survey_Submit.Enabled = false;
        PRELIM_Design_Submit.Enabled  = true;
        Donor_Confirmed_Submit.Enabled = false;
      

        sqlcn.Close();
    }
    protected void PRELIM_Design_Submit_Click(object sender, EventArgs e)
    {
        string dt_now_MMDDYYYY = DateTime.Now.ToString("MM/dd/yyyy");
        LOS_Survey_Upload_Date.Text = dt_now_MMDDYYYY;

        lbl_Error_PRELIM_Design.Text = "";
        //validation
        if (LOS_Survey_Upload_Date.Text.Equals(""))
        {
            lbl_Error_PRELIM_Design.Text = "Choose A LOS Survey Upload Date.";
            return;
        }

        DateTime datetime_now = DateTime.Now;

        //submit the inputs
        if (Session["timestamp"] != null && Session["timestamp"] != DBNull.Value)
            datetime_now = Convert.ToDateTime(Session["timestamp"]);

        string s_cmd = String.Empty;

        SqlConnection sqlcn = new SqlConnection(conString);
        sqlcn.Open();

        s_cmd = "update [MW].[dbo].MW_Pending_Donor set Status='PRELIM Design Pending', Updated_By='" + s_id + "'  where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "update [MW].[dbo].MW_LOS_Survey set Status='PRELIM Design Pending', Updated_By='" + s_id + "'  where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "update [MW].[dbo].MW_Prelim_Design_Pending set Status='PRELIM Design Pending', Los_Upload_Date='" + LOS_Survey_Upload_Date.Text + "', Updated_By='" + s_id + "' where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "update [MW].[dbo].MW_Donor_Confirmed set Status='PRELIM Design Pending', Updated_By='" + s_id + "'  where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "update [MW].[dbo].MW_Final_Design_BOM_Request set Status='PRELIM Design Pending', Updated_By='" + s_id + "'   where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "update [MW].[dbo].MW_FCC601Filed set Status='PRELIM Design Pending', Updated_By='" + s_id + "'   where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        
        s_cmd = "update [MW].[dbo].MW_Final set Status='PRELIM Design Pending' where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }
        

        lbl_ProjectStatus.Text = "PRELIM Design Pending";

        //disable step 1 button
        Pending_Donor_Submit.Enabled = false;
        LOS_Survey_Submit.Enabled = false;
        PRELIM_Design_Submit.Enabled = false;
        Donor_Confirmed_Submit.Enabled = true;
        Prelim_Design_Site_ID.Text = Pending_Donor_SiteID.Text;
        sqlcn.Close();
        
        if (true)
        {
            string emailBody = "LOS Survey uploaded for <br/><br/><table><tr><td>Market :</td><td>" + DDL_Market.Text + "</td></tr>";
            emailBody += "<tr><td>Site Id :</td><td>" + Pending_Donor_SiteID.Text + "</td></tr>";
            emailBody += "<tr><td>Requestor :</td><td>" + DDL_Requester.Text + "</td></tr>";
            emailBody += "<tr><td>Project Type :</td><td>" + DDL_ProjectType.SelectedValue + "</td></tr>";
            emailBody += "</table>";

            try
            {
                new Email().Send(DDL_Region.Text, "LOS Survey uploaded - " + Pending_Donor_SiteID.Text, emailBody);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
    protected void Donor_Confirmed_Submit_Click(object sender, EventArgs e)
    {
        string dt_now_MMDDYYYY = DateTime.Now.ToString("MM/dd/yyyy");
        Prelim_Design_Date.Text = dt_now_MMDDYYYY;
        lbl_Error_Donor_Confirmed.Text = "";
        //validation
        try
        {
            var int_temp = Convert.ToInt16(Prelim_Design_Structure_Height.Text);
        }
        catch (Exception ex)
        {
            lbl_Error_Donor_Confirmed.Text = "Structure Height should be integer.";
            return;
        }

        try
        {
            var int_temp = Convert.ToInt16(Prelim_Design_Confirmed_Centerline.Text);
        }
        catch (Exception ex)
        {
            lbl_Error_Donor_Confirmed.Text = "Centerline should be integer.";
            return;
        }
        
        try
        {
            var int_temp = Convert.ToInt16(Prelim_Design_Confirmed_Dish_Size.Text);
        }
        catch (Exception ex)
        {
            lbl_Error_Donor_Confirmed.Text = "Dish Size should be integer.";
            return;
        }

        if (Prelim_Design_Date.Text.Equals(""))
        {
            lbl_Error_Donor_Confirmed.Text = "Choose A Design Sent to Market Date.";
            return;
        }

        DateTime datetime_now = DateTime.Now;

        //submit the inputs
        if (Session["timestamp"] != null && Session["timestamp"] != DBNull.Value)
            datetime_now = Convert.ToDateTime(Session["timestamp"]);

        string s_cmd = String.Empty;

        SqlConnection sqlcn = new SqlConnection(conString);
        sqlcn.Open();

        s_cmd = "update [MW].[dbo].MW_Pending_Donor set Status='Donor Confirmed', Updated_By='" + s_id + "'  where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "update [MW].[dbo].MW_LOS_Survey set Status='Donor Confirmed', Updated_By='" + s_id + "'  where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "update [MW].[dbo].MW_Prelim_Design_Pending set Status='Donor Confirmed', Los_Upload_Date='" + LOS_Survey_Upload_Date.Text + "', Updated_By='" + s_id + "' where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "update [MW].[dbo].MW_Donor_Confirmed set Status='Donor Confirmed', Prelim_Design_Date='" + Prelim_Design_Date.Text + "',Structure_Height='" + Prelim_Design_Structure_Height.Text + "',Structure_Type='" + Prelim_Design_Structure_Type.Text + "',Confirmed_Centerline='" + Prelim_Design_Confirmed_Centerline.Text + "',Confirmed_Dish_Size='" + Prelim_Design_Confirmed_Dish_Size.Text + "',Confirmed_Radio_Model='" + Prelim_Design_Confirmed_Radio_Model.Text + "',Confirmed_Radio_Make='" + Prelim_Design_Confirmed_Radio_Make.Text + "', Comments='" + Prelim_Design_Comments.Text+ "',Updated_By='" + s_id + "'  where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "update [MW].[dbo].MW_Final_Design_BOM_Request set Status='Donor Confirmed', Updated_By='" + s_id + "'   where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }

        s_cmd = "update [MW].[dbo].MW_FCC601Filed set Status='Donor Confirmed', Updated_By='" + s_id + "'   where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }


        s_cmd = "update [MW].[dbo].MW_Final set Status='Donor Confirmed' where Site_ID='" + Donor_Candidates_SiteID.Text + "' and Requester='" + DDL_Requester.Text + "' and Project_Type='" + DDL_ProjectType.SelectedItem.Text + "' and Date_Requested_Key='" + datetime_now + "'";
        try
        {
            SqlCommand sqlcmd = new SqlCommand(s_cmd, sqlcn);
            sqlcmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
        }
        finally
        { }
        

        lbl_ProjectStatus.Text = "Donor Confirmed";

        //disable step 1 button
        Pending_Donor_Submit.Enabled = false;
        LOS_Survey_Submit.Enabled = false;
        PRELIM_Design_Submit.Enabled = false;
        Donor_Confirmed_Submit.Enabled = false;

        sqlcn.Close();

        Response.Redirect("Final.aspx?&action=create&siteid=" + Session["siteid"].ToString() + "&requester=" + DDL_Requester.Text + "&structuretype=" + Session["structuretype"].ToString() + "&market=" + Session["market"].ToString() + "&projecttype=" + DDL_ProjectType.SelectedItem.Text + "&timestamp=" + datetime_now + "&status=Donor Confirmed");
       
    }
    #endregion 

    private void populate_4_tables(string s_SiteID, string s_Requester, string s_timestamp)
    {
        DataTable dt_table1 = new DataTable();
        DataTable dt_table2 = new DataTable();
        DataTable dt_table3 = new DataTable();
        DataTable dt_table4 = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();
        string s_cmd = String.Empty;

        SqlConnection sqlcn = new SqlConnection(conString);
        //1
        s_cmd = "select * from [MW].[dbo].MW_Pending_Donor where Site_ID='" + s_SiteID + "' and Requester='" + s_Requester + "' and Date_Requested_Key='" + s_timestamp + "'";
        adapter.SelectCommand = new SqlCommand(s_cmd, sqlcn);
        sqlcn.Open();

        try
        {
            adapter.Fill(dt_table1);
        }
        finally
        {

        }

        //2
        s_cmd = "select * from [MW].[dbo].MW_LOS_Survey where Site_ID='" + s_SiteID + "' and Requester='" + s_Requester + "' and Date_Requested_Key='" + s_timestamp + "'";
        adapter.SelectCommand = new SqlCommand(s_cmd, sqlcn);

        try
        {
            adapter.Fill(dt_table2);
        }
        finally
        {

        }

        //3
        s_cmd = "select * from [MW].[dbo].MW_Prelim_Design_Pending where Site_ID='" + s_SiteID + "' and Requester='" + s_Requester + "' and Date_Requested_Key='" + s_timestamp + "'";
        adapter.SelectCommand = new SqlCommand(s_cmd, sqlcn);

        try
        {
            adapter.Fill(dt_table3);
        }
        finally
        {

        }

        //4
        s_cmd = "select * from [MW].[dbo].MW_Donor_Confirmed where Site_ID='" + s_SiteID + "' and Requester='" + s_Requester + "' and Date_Requested_Key='" + s_timestamp + "'";
        adapter.SelectCommand = new SqlCommand(s_cmd, sqlcn);

        try
        {
            adapter.Fill(dt_table4);
        }
        finally
        {

        }
        sqlcn.Close();


        //update the page
        if (dt_table1.Rows.Count > 0)
        { 
            DDL_Requester.Text = dt_table1.Rows[0][0].ToString();
            //DDL_ProjectType.SelectedItem.Text = dt_table1.Rows[0][1].ToString();
            string projex = dt_table1.Rows[0][1].ToString();
            DDL_ProjectType.SelectedValue = dt_table1.Rows[0][1].ToString();
            ProjectType_StartTime.Text = dt_table1.Rows[0][2].ToString();
            ProjectType_EndTime.Text = dt_table1.Rows[0][3].ToString();
            RBL_ProjectTypeLicensedUnlicensed.SelectedIndex = (dt_table1.Rows[0][4] == DBNull.Value ? (-1) : (Convert.ToInt16(dt_table1.Rows[0][4])));
            DDL_Region.Text =  dt_table1.Rows[0][5].ToString();
            DDL_Market.Text =  dt_table1.Rows[0][6].ToString();
            Pending_Donor_SiteID.Text =  dt_table1.Rows[0][7].ToString();
            Pending_Donor_Structure_Type.Text =  dt_table1.Rows[0][8].ToString();
            Pending_Donor_Structure_Height.Text =  dt_table1.Rows[0][9].ToString();
            Pending_Donor_Known_Available_Center_Line.Text =  dt_table1.Rows[0][10].ToString();
            Pending_Donor_Requested_Throughput.Text =  dt_table1.Rows[0][11].ToString();
            //Pending_Donor_Proposed_On_Air_Date.Text =  dt_table1.Rows[0][12].ToString();
            Pending_Donor_Proposed_On_Air_Date.Text = (Convert.ToString(dt_table1.Rows[0][12]) != null && dt_table1.Rows[0][12] != DBNull.Value ? Convert.ToDateTime(dt_table1.Rows[0][12]).ToString("MM/dd/yyyy") : string.Empty);
            Pending_Donor_Cluster_Type.SelectedItem.Text =  dt_table1.Rows[0][13].ToString();
            Pending_Donor_Cluster_ID.Text =  dt_table1.Rows[0][14].ToString();
            Pending_Donor_Cluster_Topology.Text =  dt_table1.Rows[0][15].ToString();
            Pending_Donor_Existing_CIR_Value.Text =  dt_table1.Rows[0][16].ToString();
            Pending_Donor_New_CIR_Value.Text =  dt_table1.Rows[0][17].ToString();
            //Pending_Donor_Desktop_Analysis_Request_Date.Text = dt_table1.Rows[0][18].ToString();
            Pending_Donor_Desktop_Analysis_Request_Date.Text = (Convert.ToString(dt_table1.Rows[0][18]) != null && dt_table1.Rows[0][18] != DBNull.Value ? Convert.ToDateTime(dt_table1.Rows[0][18]).ToString("MM/dd/yyyy") : string.Empty);
            Pending_Donor_Notes_Comments.Text = dt_table1.Rows[0][19].ToString();

            switch (dt_table1.Rows[0][22].ToString())
            { 
                case "Pending Donor":
                    Pending_Donor_Submit.Enabled = false;
                    LOS_Survey_Submit.Enabled = true;
                    PRELIM_Design_Submit.Enabled = false;
                    Donor_Confirmed_Submit.Enabled = false;
                    break;
                case "Los Survey":
                    Pending_Donor_Submit.Enabled = false;
                    LOS_Survey_Submit.Enabled = false;
                    PRELIM_Design_Submit.Enabled = true;
                    Donor_Confirmed_Submit.Enabled = false;
                    break;
                case "PRELIM Design Pending":
                    Pending_Donor_Submit.Enabled = false;
                    LOS_Survey_Submit.Enabled = false;
                    PRELIM_Design_Submit.Enabled = false;
                    Donor_Confirmed_Submit.Enabled = true;
                    break;
                default:
                    Pending_Donor_Submit.Enabled = false;
                    LOS_Survey_Submit.Enabled = false;
                    PRELIM_Design_Submit.Enabled = false;
                    Donor_Confirmed_Submit.Enabled = false;
                    break;
            }
        }
        if (dt_table2.Rows.Count > 0)
        {
            Donor_Candidates_SiteID.Text = dt_table2.Rows[0][0].ToString();
            Donor_Candidates_SiteID1.Text = dt_table2.Rows[0][3].ToString();
            Donor_Candidates_SiteID2.Text = dt_table2.Rows[0][4].ToString();
            Donor_Candidates_SiteID3.Text = dt_table2.Rows[0][5].ToString();
            //Donor_Candidates_DonorStudyStatus.SelectedIndex = (dt_table2.Rows[0][6] == DBNull.Value ? (-1) : (Convert.ToInt16(dt_table2.Rows[0][6])));

        }

        if (dt_table3.Rows.Count > 0)
        {
            LOS_Survey_Upload_Date.Text = dt_table3.Rows[0][3].ToString();

        }

        if (dt_table4.Rows.Count > 0)
        {
            Prelim_Design_Date.Text = dt_table4.Rows[0][3].ToString();
            if (string.IsNullOrEmpty(Prelim_Design_Date.Text))
            {
                Prelim_Design_Date.Text = "N/A";
            }
            Prelim_Design_Site_ID.Text = dt_table4.Rows[0][0].ToString();
            Prelim_Design_Structure_Height.Text = dt_table4.Rows[0][4].ToString();
            Prelim_Design_Structure_Type.Text = dt_table4.Rows[0][5].ToString();
            Prelim_Design_Confirmed_Centerline.Text = dt_table4.Rows[0][6].ToString();
            Prelim_Design_Confirmed_Dish_Size.Text = dt_table4.Rows[0][7].ToString();
            Prelim_Design_Confirmed_Radio_Model.Text = dt_table4.Rows[0][8].ToString();
            Prelim_Design_Confirmed_Radio_Make.Text = dt_table4.Rows[0][9].ToString();
            Prelim_Design_Comments.Text = dt_table4.Rows[0][10].ToString();
        }


    }
    protected void DDL_ProjectType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sv = DDL_ProjectType.SelectedValue.Replace(" ","");

        //Desktop Analysis (CMG Only)
        if (sv.ToUpper() == ("Desktop Analysis (CMG Only)").Replace(" ", "").ToUpper())
        {
        }
        else
        {
            this.ProjectType_StartTime.Visible = false;
            this.ProjectType_EndTime.Visible = false;
            this.Label2.Visible = false;
            this.Label3.Visible = false;

            if (sv.ToUpper() == ("Temporary/Event").ToUpper())
            {
                this.ProjectType_StartTime.Visible = true;
                this.ProjectType_EndTime.Visible = true;
                this.Label2.Visible = true;
                this.Label3.Visible = true;
            }
        }
    }
    //protected void Candidate_Site1_AsyncFileUpload_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
    //{

    //}
    //protected void Candidate_Site2_AsyncFileUpload_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
    //{

    //}
    //protected void Candidate_Site3_AsyncFileUpload_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
    //{

    //}
    //protected void MicrowaveTopology_AsyncFileUpload_UploadedComplete(object sender, AsyncFileUploadEventArgs e)
    //{

    //}

    

    public void MWIntakeUpload(AsyncFileUpload fileUpload, MWIntakeCommon.FileCategory fileCat)
    {
        string region = DDL_Region.Text;
        string market = DDL_Market.Text;
        string siteId = Pending_Donor_SiteID.Text;
        string requestor = DDL_Requester.Text;
        if (!Directory.Exists(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region))
            Directory.CreateDirectory(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region);
        if (!Directory.Exists(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region + "\\" + market))
            Directory.CreateDirectory(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region + "\\" + market);
        if (!Directory.Exists(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region + "\\" + market + "\\" + siteId))
            Directory.CreateDirectory(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region + "\\" + market + "\\" + siteId);
        if (!Directory.Exists(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region + "\\" + market + "\\" + siteId + "\\Requestor_" + requestor))
            Directory.CreateDirectory(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region + "\\" + market + "\\" + siteId + "\\Requestor_" + requestor);
        fileUpload.SaveAs(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region + "\\" + market + "\\" + siteId + "\\Requestor_" + requestor + "\\" + Path.GetFileNameWithoutExtension(fileUpload.FileName) + fileCat + Path.GetExtension(fileUpload.FileName));
    }
}
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
using MWIntakeCommon;

public partial class Final : System.Web.UI.Page
{
    #region Var
    string s_id = String.Empty;

    System.Configuration.ConnectionStringSettingsCollection conStringSettingsCollection = System.Web.Configuration.WebConfigurationManager.ConnectionStrings;
    string conString = String.Empty;

    string siteid = String.Empty;
    string action = String.Empty;
    string requester = String.Empty;
    string projecttype = String.Empty;
    string timestamp = String.Empty;
    string status = String.Empty;
    #endregion 
    
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        String currurl = HttpContext.Current.Request.RawUrl;
        var parsed = HttpUtility.ParseQueryString(currurl);
        string structuretype = String.Empty;
        string region = string.Empty;
        string market = String.Empty;
        string s_cmd = String.Empty;
        string aa = Request.LogonUserIdentity.Name;
        WindowsIdentity id = System.Security.Principal.WindowsIdentity.GetCurrent();

        siteid = parsed["siteid"];
        action = parsed["action"];
        projecttype = parsed["projecttype"];
        timestamp = parsed["timestamp"];
        status = parsed["status"];
        requester = parsed["requester"];
        structuretype = parsed["structuretype"];
        market = parsed["market"];


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

        txt_Final_Design_and_BOM_Request_Requester.Text = requester;
        txt_Final_Design_and_BOM_Request_ProjectType.Text = projecttype;

        if (!IsPostBack)
        {
            if (action.Equals("create"))
            {
                txt_Final_Design_and_BOM_Request_SiteID.Text = siteid;

                if (status.Equals("Donor Confirmed"))
                {
                    btn_Final_Design_and_BOM_Request_Submit.Enabled = true;
                    btn_Final_Donor_Information_Submit.Enabled = false;
                    btn_Project_Status_Submit.Enabled = false;
                    l_Status.Text = "Donor Confirmed";
                }

            }

            if (action.Equals("view"))
            {

                timestamp = parsed["timestamp"];
                //populate data from 3 tables
                populate_3_tables(siteid, requester, timestamp);

                txt_Final_Design_and_BOM_Request_SiteID.Text = siteid;
                //Final_Design_And_BOM_Recipient_Information.Text = siteid;

                l_Status.Text = status;
            }
        }
        else
        {

        }

        //Final_Donor_Information_Final_Center_Line.Text = Final_Design_And_BOM_Final_Center_Line.Text;
        //Final_Donor_Information_Final_Dish_Size.Text = Final_Design_And_BOM_Final_Dish_Size.Text;
        //Final_Donor_Information_Final_Radio_Make.Text = Final_Design_And_BOM_Final_Radio_Make.Text;
        //Final_Donor_Information_Final_Radio_Model.Text = Final_Design_And_BOM_Final_Radio_Model.Text;
        //Final_Donor_Information_Radio_Configuration_Type.Text = Final_Design_And_BOM_Radio_configuration_Type.Text;
        txt_Final_Donor_Information_SiteID.Text = txt_Final_Design_and_BOM_Request_Donor_SiteId.Text;
        lbl_Final_Donor_Information_Final_Center_Line.Text = txt_Final_Design_and_BOM_Request_Final_Center_Line.Text;
        lbl_Final_Donor_Information_Final_Dish_Size.Text = txt_Final_Design_and_BOM_Request_Donor_Dish_Size.Text;
        lbl_Final_Donor_Information_Final_Radio_Make.Text = txt_Final_Design_and_BOM_Request_Donor_Radio_Make.Text;
        lbl_Final_Donor_Information_Final_Radio_Model.Text = txt_Final_Design_and_BOM_Request_Donor_Radio_Model.Text;
        lbl_Final_Donor_Information_Radio_Configuration_Type.Text = txt_Final_Design_and_BOM_Request_Radio_configuration_Type.Text;
    }
    #endregion 
    
    #region Upload
    protected void AFU_Final_Donor_Information_PCN_Document_OnUploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        System.Threading.Thread.Sleep(5000);
        string region="", market="";
        GetRegionMarket(ref region, ref market);

        if (AFU_Final_Donor_Information_PCN_Document.HasFile)
        {
            MWIntakeUpload(AFU_Final_Donor_Information_PCN_Document, MWIntakeCommon.FileCategory._pcndoc, region, market);

           // string[] sa_file = Path.GetFileName(e.FileName).Split('.');

           //// if (sa_file[sa_file.Length - 1].ToUpper().Equals("PDF"))
           //// {
           //     string s_TimeStamp = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
           //     string m_AppDir = HttpContext.Current.Request.PhysicalApplicationPath.ToString();

           //     if (!Directory.Exists(m_AppDir + "/Uploads/" + s_id + "/")),
           //         Directory.CreateDirectory(m_AppDir + "/Uploads/" + s_id + "/");

           //     string strPath = MapPath("~/Uploads/" + s_id + "/") + sa_file[0] + "_" + s_TimeStamp + "_PCN_Document" + "." + sa_file[sa_file.Length - 1];
           //     //string strPath = MapPath(m_AppDir + "\\Uploads\\\\" + i_ticket + "\\\\") + Path.GetFileName(e.filename);

           //     AsyncFileUpload_PCN_Document.SaveAs(strPath);
                //l_Test.Text = "Finished!";
            //}
            //else
            //{
           //     lbl_Error_1.Text = "You could only upload pdf file!";
           // }
        }
    }

    protected void AFU_Final_Donor_Information_BOM_Upload_OnUploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        string region = "", market = "";
        GetRegionMarket(ref region, ref market);
        if (AFU_Final_Donor_Information_BOM_Upload.HasFile)
            MWIntakeUpload(AFU_Final_Donor_Information_BOM_Upload, MWIntakeCommon.FileCategory._bom, region, market);      
    }

    protected void AFU_Final_Donor_Information_PCN_Warning_Document_OnUploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        string region = "", market = "";
        GetRegionMarket(ref region, ref market);
        if (AFU_Final_Donor_Information_PCN_Warning_Document.HasFile)
        {
            MWIntakeUpload(AFU_Final_Donor_Information_PCN_Warning_Document, MWIntakeCommon.FileCategory._pcnwarn, region, market);

           // string[] sa_file = Path.GetFileName(e.FileName).Split('.');

           //// if (sa_file[sa_file.Length - 1].ToUpper().Equals("PDF"))
           //// {
           //     string s_TimeStamp = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
           //     string m_AppDir = HttpContext.Current.Request.PhysicalApplicationPath.ToString();

           //     if (!Directory.Exists(m_AppDir + "/Uploads/" + s_id + "/"))
           //         Directory.CreateDirectory(m_AppDir + "/Uploads/" + s_id + "/");

           //     string strPath = MapPath("~/Uploads/" + s_id + "/") + sa_file[0] + "_" + s_TimeStamp + "_Warning_Document" + "." + sa_file[sa_file.Length - 1];
           //     //string strPath = MapPath(m_AppDir + "\\Uploads\\\\" + i_ticket + "\\\\") + Path.GetFileName(e.filename);

           //     AsyncFileUpload_Warning_Document.SaveAs(strPath);
                //l_Test.Text = "Finished!";
            //}
            //else
            //{
            //    lbl_Error_1.Text = "You could only upload pdf file!";
            //}
        }
    }
    #endregion 

    #region Submit
    protected void Final_Design_And_BOM_Submit_Click(object sender, EventArgs e)
    {
        string dt_now_MMDDYYYY = DateTime.Now.ToString("MM/dd/yyyy");
        lbl_Final_Design_and_BOM_Request_Date.Text = dt_now_MMDDYYYY;
        l_Submitted_1.Text = "";

        string msg = "";
        string s_cmd = String.Empty;

        SqlConnection sqlcn = new SqlConnection(conString);
        sqlcn.Open();

        #region validation
        try
        {
            var int_temp = Convert.ToInt16(txt_Final_Design_and_BOM_Request_Final_Center_Line.Text);
        }
        catch (Exception ex)
        {
            l_Submitted_1.Text = "Center_Line should be integer.";
            return;
        }

        try
        {
            var int_temp = Convert.ToInt16(txt_Final_Design_and_BOM_Request_Final_Dish_Size.Text);
        }
        catch (Exception ex)
        {
            l_Submitted_1.Text = "Dish Size should be integer.";
            return;
        }
        #endregion 


        #region Get last user
        string last_user = "";
        string last_Status = "";

        string us_cmd = " select top 1 [Updated_By],[Status] from [MW].[dbo].[MW_Pending_Donor] where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
        var udt = MWIntakeCommon.Utility.ExecuteFillQuery(us_cmd, sqlcn);
        if (udt != null && udt.Rows.Count != 0)
        {
            last_user = udt.Rows[0]["Updated_By"].ToString();
            last_Status = udt.Rows[0]["Status"].ToString();
        }
        #endregion

        #region Status
        string status = "";
        string proposed_status = "Final Design and BOM Request";
        #endregion

        try
        {
            #region MW_Pending_Donor
            s_cmd = "update [MW].[dbo].MW_Pending_Donor set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_1.Text = msg;
                throw new Exception(" UPDATE MW_Pending_Donor Failed : " + msg);
            }
            #endregion

            #region MW_LOS_Survey
            s_cmd = "update [MW].[dbo].MW_LOS_Survey set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_1.Text = msg;
                throw new Exception(" UPDATE MW_LOS_Survey Failed : " + msg);
            }
            #endregion

            #region MW_Prelim_Design_Pending
            s_cmd = "update [MW].[dbo].MW_Prelim_Design_Pending set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_1.Text = msg;
                throw new Exception(" UPDATE MW_Prelim_Design_Pending Failed : " + msg);
            }
            #endregion

            #region MW_Donor_Confirmed
            s_cmd = "update [MW].[dbo].MW_Donor_Confirmed set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_1.Text = msg;
                throw new Exception(" UPDATE MW_Donor_Confirmed Failed : " + msg);
            }
            #endregion

            #region MW_Final_Design_BOM_Request
            s_cmd = "update [MW].[dbo].MW_Final_Design_BOM_Request set Status='" + proposed_status + 
                "', Final_Design_Requested_Date='" + lbl_Final_Design_and_BOM_Request_Date.Text + "'," +
                "Proposed_On_Air_Date="+ (string.IsNullOrEmpty(txt_Final_Design_and_BOM_Request_Proposed_On_Air_Date.Text) ? "NULL" : (object)"'"+txt_Final_Design_and_BOM_Request_Proposed_On_Air_Date.Text+"'")+

                ", Recipient_Information_SiteID='" + "" + 
                "', Final_Center_Line=" + txt_Final_Design_and_BOM_Request_Final_Center_Line.Text + 
                ",  Final_Dish_Size=" + txt_Final_Design_and_BOM_Request_Final_Dish_Size.Text + 
                ",  Final_Radio_Make='" + txt_Final_Design_and_BOM_Request_Final_Radio_Make.Text + 
                "', Final_Radio_Model='" + txt_Final_Design_and_BOM_Request_Final_Radio_Model.Text + 
                "', Radio_Configuration_Type='" + txt_Final_Design_and_BOM_Request_Radio_configuration_Type.Text + 
                "', Updated_By='" + s_id + 
                "', Donor_SiteId='" + this.txt_Final_Design_and_BOM_Request_Donor_SiteId.Text + 
                "', Donor_Dish_Size='" + this.txt_Final_Design_and_BOM_Request_Donor_Dish_Size.Text + 
                "', Donor_Radio_Make='" + this.txt_Final_Design_and_BOM_Request_Donor_Radio_Make.Text+
                "', Donor_Radio_Model='" + this.txt_Final_Design_and_BOM_Request_Donor_Radio_Model.Text+ 

                "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_1.Text = msg;
                throw new Exception(" UPDATE MW_Final_Design_BOM_Request Failed : " + msg);
            }
            #endregion

            #region MW_FCC601Filed
            s_cmd = "update [MW].[dbo].MW_FCC601Filed set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_1.Text = msg;
                throw new Exception(" UPDATE MW_FCC601Filed Failed : " + msg);
            }
            #endregion

            #region MW_Final
            s_cmd = "update [MW].[dbo].MW_Final set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_1.Text = msg;
                throw new Exception(" UPDATE MW_Final Failed : " + msg);
            }
            #endregion

            status = proposed_status;
        }
        catch (Exception exp)
        {
            l_Submitted_1.Text = exp.Message;

            try
            {
                #region Rollback Code
                UpdateStamp(sqlcn, "MW_Pending_Donor", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_LOS_Survey", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_Prelim_Design_Pending", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_Donor_Confirmed", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_Final_Design_BOM_Request", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_FCC601Filed", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_Final", last_user, last_Status, siteid, requester, projecttype, timestamp);
                #endregion
            }
            catch (Exception exp2)
            {
            }
        }
        finally
        {
            if (string.IsNullOrEmpty(status))//Failed
            {
                btn_Final_Design_and_BOM_Request_Submit.Enabled = true;
                btn_Final_Donor_Information_Submit.Enabled = true;
                btn_Project_Status_Submit.Enabled = false;
                l_Submitted_1.Text = "Update Failed";
            }
            else
            {
                txt_Final_Donor_Information_SiteID.Text = txt_Final_Design_and_BOM_Request_Donor_SiteId.Text;
                lbl_Final_Donor_Information_Final_Center_Line.Text = txt_Final_Design_and_BOM_Request_Final_Center_Line.Text;
                lbl_Final_Donor_Information_Final_Dish_Size.Text = txt_Final_Design_and_BOM_Request_Donor_Dish_Size.Text;
                lbl_Final_Donor_Information_Final_Radio_Make.Text = txt_Final_Design_and_BOM_Request_Donor_Radio_Make.Text;
                lbl_Final_Donor_Information_Final_Radio_Model.Text = txt_Final_Design_and_BOM_Request_Donor_Radio_Model.Text;
                lbl_Final_Donor_Information_Radio_Configuration_Type.Text = txt_Final_Design_and_BOM_Request_Radio_configuration_Type.Text;

                if (true)
                {
                    #region Email
                    string region = "", market = "";
                    GetRegionMarket(ref region, ref market);

                    string emailBody = "Final Design and BOM Request Submitted for <br/><br/><table><tr><td>Market :</td><td>" + market + "</td></tr>";
                    emailBody += "<tr><td>Site Id :</td><td>" + siteid + "</td></tr>";
                    emailBody += "<tr><td>Requestor :</td><td>" + requester + "</td></tr>";
                    emailBody += "<tr><td>Project Type :</td><td>" + projecttype + "</td></tr>";
                    emailBody += "</table>";

                    try
                    {
                        new Email().Send(region, "Final Design and BOM Request Submitted - " + siteid, emailBody);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    #endregion
                }

                btn_Final_Design_and_BOM_Request_Submit.Enabled = false;
                btn_Final_Donor_Information_Submit.Enabled = true;
                btn_Project_Status_Submit.Enabled = false;
                l_Status.Text = status;
                l_Submitted_1.Text = "Saved !";
            }
        }
    }
    protected void Final_Donor_Information_Submit_Click(object sender, EventArgs e)
    {
        string msg = "";
        string s_cmd = String.Empty;
        l_Submitted_2.Text = "";

        #region Validation 
        try
        {
            var int_temp = Convert.ToInt16(txt_Final_Donor_Information_Designed_CIR_Value.Text);
        }
        catch (Exception ex)
        {
            l_Submitted_2.Text = "CIR Value should be integer.";
            return;
        }
        #endregion 

        #region Junk
        ////submit the inputs
        //DateTime datetime_now = new DateTime();
        //if (Session["timestamp"] != null)
        //    datetime_now = Convert.ToDateTime(Session["timestamp"]);
        #endregion 

        SqlConnection sqlcn = new SqlConnection(conString);
        sqlcn.Open();

        #region Get last user
        string last_user = "";
        string last_Status = "";

        string us_cmd = " select top 1 [Updated_By],[Status] from [MW].[dbo].[MW_Pending_Donor] where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
        var udt = MWIntakeCommon.Utility.ExecuteFillQuery(us_cmd, sqlcn);
        if (udt != null && udt.Rows.Count!= 0 )
        {
            last_user = udt.Rows[0]["Updated_By"].ToString();
            last_Status = udt.Rows[0]["Status"].ToString();
        }
        #endregion 
        
        #region Status
        string status = "";
        string proposed_status = "";
        //Final Design Completed
        //PCN Filed
        //PCN Cleared
        //BOM Uploaded
        //FCC601 Filed

        if(!string.IsNullOrEmpty(txt_Final_Donor_Information_Final_Design_Completed_Date.Text))
        {
            proposed_status = "Final Design Completed";
            if (!string.IsNullOrEmpty(txt_Final_Donor_Information_PCN_Filed_Date.Text))
            {
                proposed_status = "PCN Filed";
                if (!string.IsNullOrEmpty(txt_Final_Donor_Information_PCN_Cleared_Date.Text))
                {
                    proposed_status = "PCN Cleared";
                    if (!string.IsNullOrEmpty(txt_Final_Donor_Information_BOM_Sent_to_Market_Date.Text))
                    {
                        proposed_status = "BOM Uploaded";
                        if (!string.IsNullOrEmpty(txt_Final_Donor_Information_FCC_601_Filed_Date.Text))
                        {
                            proposed_status = "FCC601 Filed";
                        }
                    }
                }
            }
        }

        if (string.IsNullOrEmpty(proposed_status))
        {
            l_Submitted_2.Text = "Data not set properly";
            return;
        }
        #endregion 

        #region Update

        try
        {
            #region MW_Pending_Donor
            s_cmd = "update [MW].[dbo].MW_Pending_Donor set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_2.Text = msg;
                throw new Exception(" UPDATE MW_Pending_Donor Failed : " + msg);
            }
            #endregion

            #region MW_LOS_Survey
            s_cmd = "update [MW].[dbo].MW_LOS_Survey set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_2.Text = msg;
                throw new Exception(" UPDATE MW_LOS_Survey Failed : " + msg);
            }
            #endregion

            #region MW_Prelim_Design_Pending
            s_cmd = "update [MW].[dbo].MW_Prelim_Design_Pending set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_2.Text = msg;
                throw new Exception(" UPDATE MW_Prelim_Design_Pending Failed : " + msg);
            }
            #endregion

            #region MW_Donor_Confirmed
            s_cmd = "update [MW].[dbo].MW_Donor_Confirmed set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_2.Text = msg;
                throw new Exception(" UPDATE MW_Donor_Confirmed Failed : " + msg);
            }
            #endregion

            #region MW_Final_Design_BOM_Request
            s_cmd = "update [MW].[dbo].MW_Final_Design_BOM_Request set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_2.Text = msg;
                throw new Exception(" UPDATE MW_Final_Design_BOM_Request Failed : " + msg);
            }
            #endregion

            #region MW_Final
            s_cmd = "update [MW].[dbo].MW_Final set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_2.Text = msg;
                throw new Exception(" UPDATE MW_Final Failed : " + msg);
            }
            #endregion

            #region MW_FCC601Filed
            s_cmd = "update [MW].[dbo].MW_FCC601Filed set Status='" + proposed_status + "',Final_Center_Line=" + lbl_Final_Donor_Information_Final_Center_Line.Text +
                ", Final_Dish_Size=" + lbl_Final_Donor_Information_Final_Dish_Size.Text +
                ", Final_Radio_Make='" + lbl_Final_Donor_Information_Final_Radio_Make.Text +
                "',Final_Radio_Model='" + lbl_Final_Donor_Information_Final_Radio_Model.Text +
                "',Radio_Configuration_Type='" + lbl_Final_Donor_Information_Radio_Configuration_Type.Text +
                "',Designed_CIR_Value='" + txt_Final_Donor_Information_Designed_CIR_Value.Text +
                "',PCN_Filed_date=" + (string.IsNullOrEmpty(txt_Final_Design_and_BOM_Request_Proposed_On_Air_Date.Text) ? "NULL" : (object)"'" + txt_Final_Donor_Information_PCN_Filed_Date.Text + "'") +//
                " ,PCN_Warning=" + Final_Donor_Information_PCN_Warning.SelectedIndex +
                ", PCN_Cleared_Date="+ (string.IsNullOrEmpty(txt_Final_Donor_Information_PCN_Cleared_Date.Text) ? "NULL" : (object)"'" + txt_Final_Donor_Information_PCN_Cleared_Date.Text + "'") +//               

                " ,BOM_Sent_to_Market_Status=" + Final_Donor_Information_BOM_Sent_to_Market_Status.SelectedIndex +
                ", BOM_Sent_to_Market_Date=" + (string.IsNullOrEmpty(txt_Final_Donor_Information_BOM_Sent_to_Market_Date.Text) ? "NULL" : (object)"'" + txt_Final_Donor_Information_BOM_Sent_to_Market_Date.Text + "'") +// 
                " ,FCC_601_Filed_Date=" + (string.IsNullOrEmpty(txt_Final_Donor_Information_FCC_601_Filed_Date.Text) ? "NULL" : (object)"'" + txt_Final_Donor_Information_FCC_601_Filed_Date.Text + "'") +// 

                " ,Donor_Siteid='" + txt_Final_Donor_Information_SiteID.Text +
                "',Updated_By='" + s_id +
                "',Final_Design_Completed_Date=" + (string.IsNullOrEmpty(txt_Final_Donor_Information_Final_Design_Completed_Date.Text) ? "NULL" : (object)"'" + txt_Final_Donor_Information_Final_Design_Completed_Date.Text + "'") +//
                "  where Site_ID='" + siteid +
                "' and Requester='" + requester +
                "' and Project_Type='" + projecttype +
                "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_2.Text = msg;
                throw new Exception(" UPDATE MW_FCC601Filed Failed : " + msg);
            }
            #endregion

            status = proposed_status;
        }
        catch (Exception exp)
        {
            l_Submitted_2.Text = exp.Message;

            try
            {
                #region Rollback Code
                UpdateStamp(sqlcn, "MW_Pending_Donor", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_LOS_Survey", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_Prelim_Design_Pending", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_Donor_Confirmed", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_Final_Design_BOM_Request", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_FCC601Filed", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_Final", last_user, last_Status, siteid, requester, projecttype, timestamp);
                #endregion
            }
            catch (Exception exp2)
            {
            }
        }
        finally
        {
            if (string.IsNullOrEmpty(status))
            {
                l_Submitted_2.Text = "Update Failed";
            }
            if (status != "FCC601 Filed")
            {
                btn_Final_Design_and_BOM_Request_Submit.Enabled = false;
                btn_Final_Donor_Information_Submit.Enabled = true;
                btn_Project_Status_Submit.Enabled = false;
                l_Submitted_2.Text = "Saved !";
                l_Status.Text = status;
            }
            if (status == "FCC601 Filed")
            {
                btn_Final_Design_and_BOM_Request_Submit.Enabled = false;
                btn_Final_Donor_Information_Submit.Enabled = false;
                btn_Project_Status_Submit.Enabled = true;
                l_Status.Text = "FCC 601 Filed";
                l_Submitted_2.Text = "Saved !";
            }
        }
        
        #endregion         
    }    
    protected void Project_Status_Submit_Click(object sender, EventArgs e)
    {
        string dt_now_MMDDYYYY = DateTime.Now.ToString("MM/dd/yyyy");
        lbl_Project_Status_Date.Text = dt_now_MMDDYYYY;
        l_Submitted_3.Text = "";

        string msg = "";
        string s_cmd = String.Empty;

        SqlConnection sqlcn = new SqlConnection(conString);
        sqlcn.Open();

        #region Get last user
        string last_user = "";
        string last_Status = "";

        string us_cmd = " select top 1 [Updated_By],[Status] from [MW].[dbo].[MW_Pending_Donor] where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
        var udt = MWIntakeCommon.Utility.ExecuteFillQuery(us_cmd, sqlcn);
        if (udt != null && udt.Rows.Count != 0)
        {
            last_user = udt.Rows[0]["Updated_By"].ToString();
            last_Status = udt.Rows[0]["Status"].ToString();
        }
        #endregion 
        
        #region Status
        string status = "";
        string proposed_status = Project_Status.SelectedItem.Text;
        #endregion 

        #region Update
        try
        {
            #region MW_Pending_Donor
            s_cmd = "update [MW].[dbo].MW_Pending_Donor set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_3.Text = msg;
                throw new Exception(" UPDATE MW_Pending_Donor Failed : " + msg);
            }
            #endregion

            #region MW_LOS_Survey
            s_cmd = "update [MW].[dbo].MW_LOS_Survey set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_3.Text = msg;
                throw new Exception(" UPDATE MW_LOS_Survey Failed : " + msg);
            }
            #endregion

            #region MW_Prelim_Design_Pending
            s_cmd = "update [MW].[dbo].MW_Prelim_Design_Pending set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_3.Text = msg;
                throw new Exception(" UPDATE MW_Prelim_Design_Pending Failed : " + msg);
            }
            #endregion

            #region MW_Donor_Confirmed
            s_cmd = "update [MW].[dbo].MW_Donor_Confirmed set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_3.Text = msg;
                throw new Exception(" UPDATE MW_Donor_Confirmed Failed : " + msg);
            }
            #endregion

            #region MW_Final_Design_BOM_Request
            s_cmd = "update [MW].[dbo].MW_Final_Design_BOM_Request set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_3.Text = msg;
                throw new Exception(" UPDATE MW_Final_Design_BOM_Request Failed : " + msg);
            }
            #endregion

            #region MW_FCC601Filed
            s_cmd = "update [MW].[dbo].MW_FCC601Filed set Status='" + proposed_status + "', Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_3.Text = msg;
                throw new Exception(" UPDATE MW_FCC601Filed Failed : " + msg);
            }
            #endregion 

            #region MW_Final
            s_cmd = "update [MW].[dbo].MW_Final set Project_Status_Date='" + lbl_Project_Status_Date.Text + "', Project_Status='" + Project_Status.SelectedItem.Value + "',Status='" + proposed_status + "',Updated_By='" + s_id + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
            msg = MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
            if (!string.IsNullOrEmpty(msg))
            {
                l_Submitted_3.Text = msg;
                throw new Exception(" UPDATE MW_Final Failed : " + msg);
            }
            #endregion 

            status = proposed_status;
        }
        catch (Exception exp)
        {
            l_Submitted_3.Text = exp.Message;

            try
            {
                #region Rollback Code
                UpdateStamp(sqlcn, "MW_Pending_Donor", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_LOS_Survey", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_Prelim_Design_Pending", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_Donor_Confirmed", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_Final_Design_BOM_Request", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_FCC601Filed", last_user, last_Status, siteid, requester, projecttype, timestamp);
                UpdateStamp(sqlcn, "MW_Final", last_user, last_Status, siteid, requester, projecttype, timestamp);
                #endregion
            }
            catch (Exception exp2)
            {
            }
        }
        finally
        {
            if (string.IsNullOrEmpty(status))//Failed
            {
                btn_Final_Design_and_BOM_Request_Submit.Enabled = false;
                btn_Final_Donor_Information_Submit.Enabled = false;
                btn_Project_Status_Submit.Enabled = true;
                l_Submitted_3.Text = "Update Failed";
            }
            else
            {
                btn_Final_Design_and_BOM_Request_Submit.Enabled = false;
                btn_Final_Donor_Information_Submit.Enabled = false;
                btn_Project_Status_Submit.Enabled = true;
                l_Status.Text = status;
                l_Submitted_3.Text = "Saved !";
            }
        }
        #endregion 
    }
    #endregion 

    #region Helpers
    void GetRegionMarket(ref string region, ref string market)
    {
        DataTable dt = new DataTable();
        SqlCommand comm = new SqlCommand("select Region,Market from [MW].[dbo].[MW_Pending_Donor] where Site_ID='" + siteid + "'", new SqlConnection(MWIntakeCommon.Utility.ConnectionString));
        SqlDataAdapter adapter = new SqlDataAdapter();
        adapter.SelectCommand = comm;
        adapter.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            region = Convert.ToString(dt.Rows[0][0]);
            market = Convert.ToString(dt.Rows[0][1]);
        }

    }

    private void populate_3_tables(string s_SiteID, string s_Requester, string s_timestamp)
    {
        DataTable dt_table1 = new DataTable();
        DataTable dt_table2 = new DataTable();
        DataTable dt_table3 = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();
        string s_cmd = String.Empty;

        SqlConnection sqlcn = new SqlConnection(conString);
        //1
        s_cmd = "select * from [MW].[dbo].MW_Final_Design_BOM_Request where Site_ID='" + s_SiteID + "' and Requester='" + s_Requester + "' and Date_Requested_Key='" + s_timestamp + "'";
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
        s_cmd = "select * from [MW].[dbo].MW_FCC601Filed where Site_ID='" + s_SiteID + "' and Requester='" + s_Requester + "' and Date_Requested_Key='" + s_timestamp + "'";
        adapter.SelectCommand = new SqlCommand(s_cmd, sqlcn);

        try
        {
            adapter.Fill(dt_table2);
        }
        finally
        {

        }

        //3
        s_cmd = "select * from [MW].[dbo].MW_Final where Site_ID='" + s_SiteID + "' and Requester='" + s_Requester + "' and Date_Requested_Key='" + s_timestamp + "'";
        adapter.SelectCommand = new SqlCommand(s_cmd, sqlcn);

        try
        {
            adapter.Fill(dt_table3);
        }
        finally
        {

        }
        sqlcn.Close();



        //update the page
        if (dt_table1.Rows.Count > 0)
        {
            txt_Final_Design_and_BOM_Request_SiteID.Text = dt_table1.Rows[0][0].ToString();
            txt_Final_Design_and_BOM_Request_Proposed_On_Air_Date.Text = (Convert.ToString(dt_table1.Rows[0][4]) != null && dt_table1.Rows[0][4] != DBNull.Value ? Convert.ToDateTime(dt_table1.Rows[0][4]).ToString("MM/dd/yyyy") : string.Empty);
            txt_Final_Design_and_BOM_Request_Final_Center_Line.Text = dt_table1.Rows[0][6].ToString();
            txt_Final_Design_and_BOM_Request_Final_Dish_Size.Text = dt_table1.Rows[0][7].ToString();
            txt_Final_Design_and_BOM_Request_Final_Radio_Make.Text = dt_table1.Rows[0][8].ToString();
            txt_Final_Design_and_BOM_Request_Final_Radio_Model.Text = dt_table1.Rows[0][9].ToString();
            txt_Final_Design_and_BOM_Request_Radio_configuration_Type.Text = dt_table1.Rows[0][10].ToString();

            txt_Final_Design_and_BOM_Request_Donor_SiteId.Text = dt_table1.Rows[0]["Donor_SiteId"].ToString();
            txt_Final_Design_and_BOM_Request_Donor_Dish_Size.Text= dt_table1.Rows[0]["Donor_Dish_Size"].ToString();
            txt_Final_Design_and_BOM_Request_Donor_Radio_Make.Text= dt_table1.Rows[0]["Donor_Radio_Make"].ToString();
            txt_Final_Design_and_BOM_Request_Donor_Radio_Model.Text= dt_table1.Rows[0]["Donor_Radio_Model"].ToString();

            lbl_Final_Design_and_BOM_Request_Date.Text = (Convert.ToString(dt_table1.Rows[0][3]) != null && dt_table1.Rows[0][3] != DBNull.Value ? Convert.ToDateTime(dt_table1.Rows[0][3]).ToString("MM/dd/yyyy") : string.Empty);

            switch (dt_table1.Rows[0][13].ToString())
            {
                case "Donor Confirmed":
                    btn_Final_Design_and_BOM_Request_Submit.Enabled = true;
                    btn_Final_Donor_Information_Submit.Enabled = false;
                    btn_Project_Status_Submit.Enabled = false;
                    break;
                case "Final Design and BOM Request":
                    btn_Final_Design_and_BOM_Request_Submit.Enabled = false;
                    btn_Final_Donor_Information_Submit.Enabled = true;
                    btn_Project_Status_Submit.Enabled = false;
                    break;
                case "BOM Uploaded" :
                    btn_Final_Design_and_BOM_Request_Submit.Enabled = false;
                    btn_Final_Donor_Information_Submit.Enabled = true;
                    btn_Project_Status_Submit.Enabled = false;
                    break;
                case "PCN Cleared" :
                    btn_Final_Design_and_BOM_Request_Submit.Enabled = false;
                    btn_Final_Donor_Information_Submit.Enabled = true;
                    btn_Project_Status_Submit.Enabled = false;
                    break; 
                case "PCN Filed": 
                    btn_Final_Design_and_BOM_Request_Submit.Enabled = false;
                    btn_Final_Donor_Information_Submit.Enabled = true;
                    btn_Project_Status_Submit.Enabled = false;
                    break; 
                case "Final Design Completed":
                    btn_Final_Design_and_BOM_Request_Submit.Enabled = false;
                    btn_Final_Donor_Information_Submit.Enabled = true;
                    btn_Project_Status_Submit.Enabled = false;
                    break;
                case "FCC601 Filed":
                    btn_Final_Design_and_BOM_Request_Submit.Enabled = false;
                    btn_Final_Donor_Information_Submit.Enabled = false;
                    btn_Project_Status_Submit.Enabled = true;
                    break;
                default:
                    btn_Final_Design_and_BOM_Request_Submit.Enabled = false;
                    btn_Final_Donor_Information_Submit.Enabled = false;
                    btn_Project_Status_Submit.Enabled = false;
                    break;
            }
        }
        if (dt_table2.Rows.Count > 0)
        {
            txt_Final_Donor_Information_SiteID.Text = dt_table2.Rows[0][18].ToString();
            lbl_Final_Donor_Information_Final_Center_Line.Text = dt_table2.Rows[0][3].ToString();
            lbl_Final_Donor_Information_Final_Dish_Size.Text = dt_table2.Rows[0][4].ToString();
            lbl_Final_Donor_Information_Final_Radio_Make.Text = dt_table2.Rows[0][5].ToString();
            lbl_Final_Donor_Information_Final_Radio_Model.Text = dt_table2.Rows[0][6].ToString();
            lbl_Final_Donor_Information_Radio_Configuration_Type.Text = dt_table2.Rows[0][7].ToString();
            txt_Final_Donor_Information_Designed_CIR_Value.Text = dt_table2.Rows[0][8].ToString();                
            Final_Donor_Information_PCN_Warning.SelectedIndex = (dt_table2.Rows[0][10] == DBNull.Value ? (-1) : (Convert.ToInt16(dt_table2.Rows[0][10])));                
            Final_Donor_Information_BOM_Sent_to_Market_Status.SelectedIndex = (dt_table2.Rows[0][12] == DBNull.Value ? (-1) : (Convert.ToInt16(dt_table2.Rows[0][12])));
            txt_Final_Donor_Information_FCC_601_Filed_Date.Text = (Convert.ToString(dt_table2.Rows[0][14]) != null && dt_table2.Rows[0][14] != DBNull.Value ? Convert.ToDateTime(dt_table2.Rows[0][14]).ToString("MM/dd/yyyy") : string.Empty);

            //Pending_Donor_Proposed_On_Air_Date.Text = (Convert.ToString(dt_table1.Rows[0][12]) != null && dt_table1.Rows[0][12] != DBNull.Value ? Convert.ToDateTime(dt_table1.Rows[0][12]).ToString("MM/dd/yyyy") : string.Empty);
            //if(dt_table2.Rows[0][15] != DBNull.Value)
            //    txt_Final_Donor_Information_Final_Design_Completed_Date.Text = Convert.ToDateTime(dt_table2.Rows[0][15]).ToString("mm/dd/yyyy");

            //if (dt_table2.Rows[0][9] != DBNull.Value)
            //    txt_Final_Donor_Information_PCN_Filed_Date.Text = Convert.ToDateTime(dt_table2.Rows[0][9]).ToString("mm/dd/yyyy");
            txt_Final_Donor_Information_Final_Design_Completed_Date.Text = (Convert.ToString(dt_table2.Rows[0][19]) != null && dt_table2.Rows[0][19] != DBNull.Value ? Convert.ToDateTime(dt_table2.Rows[0][19]).ToString("MM/dd/yyyy") : string.Empty);
            txt_Final_Donor_Information_PCN_Filed_Date.Text = (Convert.ToString(dt_table2.Rows[0][9]) != null && dt_table2.Rows[0][9] != DBNull.Value ? Convert.ToDateTime(dt_table2.Rows[0][9]).ToString("MM/dd/yyyy") : string.Empty);

            //if (dt_table2.Rows[0][13] != DBNull.Value)
            //    txt_Final_Donor_Information_BOM_Sent_to_Market_Date.Text = Convert.ToDateTime(dt_table2.Rows[0][13]).ToString("mm/dd/yyyy");

            //if (dt_table2.Rows[0][11] != DBNull.Value)
            //    txt_Final_Donor_Information_PCN_Cleared_Date.Text = Convert.ToDateTime(dt_table2.Rows[0][11]).ToString("mm/dd/yyyy");

            txt_Final_Donor_Information_BOM_Sent_to_Market_Date.Text = (Convert.ToString(dt_table2.Rows[0][13]) != null && dt_table2.Rows[0][13] != DBNull.Value ? Convert.ToDateTime(dt_table2.Rows[0][13]).ToString("MM/dd/yyyy") : string.Empty);
            txt_Final_Donor_Information_PCN_Cleared_Date.Text = (Convert.ToString(dt_table2.Rows[0][11]) != null && dt_table2.Rows[0][11] != DBNull.Value ? Convert.ToDateTime(dt_table2.Rows[0][11]).ToString("MM/dd/yyyy") : string.Empty);

        }

        if (dt_table3.Rows.Count > 0)
        {
            lbl_Project_Status_Date.Text  = dt_table3.Rows[0][3].ToString();
            Project_Status.SelectedIndex = (dt_table3.Rows[0][4] == DBNull.Value ? (-1) : (Convert.ToInt16(dt_table3.Rows[0][4])));
        }
    }

    public void MWIntakeUpload(AsyncFileUpload fileUpload, MWIntakeCommon.FileCategory fileCat,string region, string market)
    {
        if (!Directory.Exists(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region))
            Directory.CreateDirectory(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region);
        if (!Directory.Exists(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region + "\\" + market))
            Directory.CreateDirectory(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region + "\\" + market);
        if (!Directory.Exists(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region + "\\" + market + "\\" + siteid))
            Directory.CreateDirectory(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region + "\\" + market + "\\" + siteid);
        if (!Directory.Exists(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region + "\\" + market + "\\" + siteid + "\\Requestor_" + requester))
            Directory.CreateDirectory(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region + "\\" + market + "\\" + siteid + "\\Requestor_" + requester);
        fileUpload.SaveAs(MWIntakeCommon.Utility.MWIntakeUploads + "\\" + region + "\\" + market + "\\" + siteid + "\\Requestor_" + requester + "\\" + Path.GetFileNameWithoutExtension(fileUpload.FileName) + fileCat + Path.GetExtension(fileUpload.FileName));
    }

    public string UpdateStamp(SqlConnection sqlcn, string table, string user, string status, string siteid, string requester, string projecttype, string timestamp)
    {
        var s_cmd = "update [MW].[dbo]." + table + " set Status='" + status + "', Updated_By='" + user + "'  where Site_ID='" + siteid + "' and Requester='" + requester + "' and Project_Type='" + projecttype + "' and Date_Requested_Key='" + timestamp + "'";
        return MWIntakeCommon.Utility.ExecuteNonQuery(s_cmd, sqlcn);
    }
    #endregion 
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Telerik.Web.UI;
using System.Data.SqlClient;
using System.Drawing;
using System.Data.SqlTypes;
using System.IO;
using System.Security.Principal;
using System.Data.OleDb;
using System.Web.Services;
using OfficeOpenXml.Style;

public partial class MainPage : System.Web.UI.Page
{
    System.Configuration.ConnectionStringSettingsCollection conStringSettingsCollection = System.Web.Configuration.WebConfigurationManager.ConnectionStrings;
    string conString = MWIntakeCommon.Utility.ConnectionString;
    //string s_id = String.Empty;
    //string s_type = String.Empty;
    string s_Status = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        //string aa = Request.LogonUserIdentity.Name;
        //s_id = (aa.Substring(aa.IndexOf('\\') + 1));

        if (!Page.IsPostBack)
        {
            OnSelectedIndexChanged_Changed(sender, e);
        }
        
    }


    protected void Submit_Click(object sender, EventArgs e)
    {
        this.txt_SiteID.Text = this.txt_SiteID.Text.ToUpper().Trim();

        if (DDL_Requester.SelectedItem.Text == "--Select--")
        {
            lbl_Status.Text = "Please Select Requestor!";
            Button_View.Visible = false;
            Button_CreateNewProject.Visible = false;
            trSite2.Visible = false;
        }

        else
        {
            trSite2.Visible = true;
            DataTable dt_result_project = new DataTable();
            DataTable dt_result_status = new DataTable();
            DataTable dt_result_type = new DataTable();
            String ConnString = ConfigurationManager.ConnectionStrings["ConcordConnectionString_Report"].ConnectionString;
            SqlConnection conn = new SqlConnection(ConnString);
            SqlDataAdapter adapter = new SqlDataAdapter();





            string s_cmd = String.Empty;
            DataTable dt = new DataTable();
            s_cmd = " select siteid from [dbo].[edw_insite_allinfo_west] where siteid = '" + txt_SiteID.Text.Trim() + "'";
            adapter.SelectCommand = new SqlCommand(s_cmd, conn);
            conn.Open();
            adapter.Fill(dt);
            conn.Close();
            if (dt != null && dt.Rows.Count > 0)
            {
                s_cmd = " select top 1 site_id, requester,status,Date_Requested_Key,Project_Status from [MW].[dbo].[MW_Final] where site_id='" + txt_SiteID.Text.Trim() + "' and requester='" + DDL_Requester.SelectedItem.Text + "'" + " and (status<> 'killed' or status <> 'complete')";
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(s_cmd, conn);
                conn.Open();

                try
                {
                    adapter.Fill(dt_result_project);
                }
                finally
                {

                }

                if (dt_result_project.Rows.Count > 0)
                {
                    if (dt_result_project.Rows[0][4].ToString() == "0")
                    //if (dt_result_project.Rows[0][4].ToString().ToUpper().Equals("ACTIVE"))
                    {
                        //pull 7 tables data 
                        Session["timestamp"] = dt_result_project.Rows[0][3].ToString();
                        lbl_Status.Text = dt_result_project.Rows[0][2].ToString().ToUpper();
                        Button_View.Visible = true;
                        Button_CreateNewProject.Visible = false;
                    }
                    else
                    {
                        if (dt_result_project.Rows[0][4].ToString() != "0")                        
                        //if (dt_result_project.Rows[0][4].ToString().ToUpper().Equals("COMPLETE") || dt_result_project.Rows[0][4].ToString().ToUpper().Equals("KILLED") || dt_result_project.Rows[0][4].ToString().ToUpper().Equals("HOLD"))
                        {
                            Session["timestamp"] = dt_result_project.Rows[0][3].ToString();
                            lbl_Status.Text = dt_result_project.Rows[0][2].ToString().ToUpper();
                            Button_View.Visible = true;
                            Button_CreateNewProject.Visible = false;
                        }
                    }
                }
                else
                {
                    //create a new project
                    lbl_Status.Text = "No Record Found!";
                    Button_View.Visible = false;
                    Button_CreateNewProject.Visible = true;
                }
                //conn.Close();


                if (Session["timestamp"] != null)
                    s_cmd = " select top 1 status,project_type from [MW].[dbo].[MW_Pending_Donor] where site_id='" + txt_SiteID.Text.Trim() + "' and requester='" + DDL_Requester.SelectedItem.Text + "' and Date_Requested_Key='" + Session["timestamp"].ToString() + "'";
                else
                { s_cmd = " "; }
                adapter.SelectCommand = new SqlCommand(s_cmd, conn);

                try
                {
                    adapter.Fill(dt_result_status);
                }
                finally
                {

                }

                if (dt_result_status.Rows.Count > 0)
                {
                    //s_Status = dt_result_status.Rows[0][0].ToString();
                    //Session["status"] = s_Status;

                    Session["status"] = dt_result_status.Rows[0][0].ToString();
                    Session["projecttype"] = dt_result_status.Rows[0][1].ToString();
                }

                s_cmd = "SELECT distinct siteid,[class],[market_name],region FROM [LTE_CIQ].[dbo].[edw_insite_allinfo_west] where siteid='" + txt_SiteID.Text.Trim() + "' ";
                adapter.SelectCommand = new SqlCommand(s_cmd, conn);

                try
                {
                    adapter.Fill(dt_result_type);
                }
                finally
                {

                }

                if (dt_result_type.Rows.Count > 0)
                {
                    Session["structuretype"] = dt_result_type.Rows[0][1].ToString();
                    Session["market"] = dt_result_type.Rows[0][2].ToString();
                    Session["siteid"] = dt_result_type.Rows[0][0].ToString();
                    Session["region"] = dt_result_type.Rows[0][3].ToString();
                }

                try { conn.Close(); }
                catch (Exception ex) { }

            }
            else
            {
                //create a new project
                lbl_Status.Text = "Please check Site Id (Site Id should include candidate letter)!";
                Button_View.Visible = false;
                Button_CreateNewProject.Visible = false;
            }
        }
    }


    protected void Submit_View(object sender, EventArgs args)
    {
        if (Session["timestamp"] != null)
        {
            if (Session["status"] != null)
            {
                var s_Status = Session["status"].ToString();
                if (s_Status.Equals("Pending Donor") || s_Status.Equals("Los Survey") || s_Status.Equals("PRELIM Design Pending"))
                {
                    if (DDL_Requester.SelectedItem.Text == "CMG")
                    {
                        Response.Redirect("CMG.aspx?&action=view&siteid=" + Session["siteid"].ToString() + "&requester=" + DDL_Requester.SelectedItem.Text + "&timestamp=" + Session["timestamp"].ToString() + "&status=" + s_Status);
                    }
                    else
                    {
                        Response.Redirect("Intake.aspx?&action=view&siteid=" + Session["siteid"].ToString() + "&requester=" + DDL_Requester.SelectedItem.Text + "&timestamp=" + Session["timestamp"].ToString() + "&status=" + s_Status);                    
                    }
                }

                if (Session["siteid"] != null)
                {
                    if (Session["projecttype"] != null)
                    {
                        if (Session["timestamp"] != null)
                            if (s_Status.Equals("Donor Confirmed") || s_Status.Equals("Final Design and BOM Request") || s_Status.Equals("FCC601 Filed") || s_Status.Equals("HOLD") || s_Status.Equals("KILLED") || s_Status.Equals("COMPLETE") || s_Status.Equals("ACTIVE") || s_Status.Equals("BOM Uploaded") || s_Status.Equals("PCN Cleared") || s_Status.Equals("PCN Filed") || s_Status.Equals("Final Design Completed"))
                                Response.Redirect("Final.aspx?&action=view&siteid=" + Session["siteid"].ToString() + "&requester=" + DDL_Requester.SelectedItem.Text + "&timestamp=" + Session["timestamp"].ToString() + "&status=" + s_Status + "&projecttype=" + Session["projecttype"].ToString());
                        //Response.Redirect("Final.aspx?&action=create&currentStatus=FinalDesignAndBOMRequest&siteid=" + Session["siteid"].ToString() + "&requester=" + DDL_Requester.SelectedItem.Text + "&structuretype=" + Session["structuretype"].ToString() + "&market=" + Session["market"].ToString() + "&projecttype=" + DDL_ProjectType.SelectedItem.Text + "&timestamp=" + datetime_now);
                    }
                }
            }
        }
    }

    protected void Submit_Create(object sender, EventArgs args)
    {
        if (Session["structuretype"] != null)
            Response.Redirect("Intake.aspx?&action=create&siteid=" + Session["siteid"].ToString() + "&requester=" + DDL_Requester.SelectedItem.Text + "&structuretype=" + Session["structuretype"].ToString() + "&market=" + Session["market"].ToString() + "&status=" + s_Status);
        else
            Response.Redirect("Intake.aspx?&siteid=&structuretype=null");
    }

    protected void OnSelectedIndexChanged_Changed(object sender, EventArgs e)
    {
        this.txt_SiteID.Text = "";
        this.lbl_Status.Text = "";

        DropDownList ddl = sender as DropDownList;
        if (ddl != null)
        {
            if (ddl.SelectedValue == "Market" || ddl.SelectedValue == "National Transport")
            {
                trSite.Visible = true;
                trSite2.Visible = false;


            }
            else
            {
                trSite.Visible = false;
                trSite2.Visible = false;
            }
        }
        else
        {
            trSite.Visible = false;
            trSite2.Visible = false;
        }
    }

    protected void OnddlRegionSelectedIndex_Changed(object sender, EventArgs e)
    {
        DropDownList ddlRegionTmp = sender as DropDownList;
        if (ddlRegionTmp != null)
        {
            String ConnString = ConfigurationManager.ConnectionStrings["ConcordConnectionString_Report"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(ConnString))
            {
                DataTable dt = new DataTable();
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    string s_cmd = " select MarketMapping from MW.[dbo].[Market] where region = '" + ddlRegionTmp.SelectedValue + "'";
                    adapter.SelectCommand = new SqlCommand(s_cmd, conn);
                    conn.Open();
                    adapter.Fill(dt);
                    ddlMarket.DataTextField = "MarketMapping";
                    ddlMarket.DataValueField = "MarketMapping";
                    var dr = dt.NewRow();
                    dr["MarketMapping"] = "-- Select --";
                    dt.Rows.InsertAt(dr, 0);
                    ddlMarket.DataSource = dt;
                    ddlMarket.DataBind();
                }
                conn.Close();
            }
            
        }
    }

    protected void btnDownloadSummary_Click(object sender, EventArgs e)
    {
        String ConnString = ConfigurationManager.ConnectionStrings["ConcordConnectionString_Report"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(ConnString))
        {
            DataTable dt = new DataTable();
            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                using (SqlCommand command = new SqlCommand("DownloadSummaryReport", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@region", ddlRegion.SelectedValue);
                    if (!string.IsNullOrEmpty(ddlMarket.SelectedValue) && ddlMarket.SelectedValue != "-- Select --")
                        command.Parameters.AddWithValue("@market", ddlMarket.SelectedValue);
                    adapter.SelectCommand = command;
                    conn.Open();
                    adapter.Fill(dt);                   
                }
            }
            conn.Close();

            
                using (MemoryStream xlFileStream = new MemoryStream())
                {
                    using (OfficeOpenXml.ExcelPackage xlPkg = new OfficeOpenXml.ExcelPackage())
                    {
                        int xlRowCnt = 1;
                        int xlColCnt = 1;
                        string sheetName = "Download Intake Summary";

                        OfficeOpenXml.ExcelWorksheet xlWrkSht1 = xlPkg.Workbook.Worksheets.Add(sheetName);
                        xlWrkSht1.Cells[xlRowCnt, xlColCnt++].Value = "Region";
                        xlWrkSht1.Cells[xlRowCnt, xlColCnt++].Value = "Market";
                        xlWrkSht1.Cells[xlRowCnt, xlColCnt++].Value = "Requestor ";
                        xlWrkSht1.Cells[xlRowCnt, xlColCnt++].Value = "Site_ID ";
                        xlWrkSht1.Cells[xlRowCnt, xlColCnt++].Value = "Status";
                        xlWrkSht1.Cells[xlRowCnt, xlColCnt++].Value = "Desktop_Analysis_Request_Date";
                        xlWrkSht1.Cells[xlRowCnt, xlColCnt++].Value = "Los_Upload_Date";
                        xlWrkSht1.Cells[xlRowCnt, xlColCnt++].Value = "Prelim_Design_Date";
                        xlWrkSht1.Cells[xlRowCnt, xlColCnt++].Value = "Final_Design_Requested_Date";

                        xlWrkSht1.Cells[xlRowCnt, xlColCnt++].Value = "Final_Design_Completed_Date";
                        xlWrkSht1.Cells[xlRowCnt, xlColCnt++].Value = "PCN_Filed_date";
                        xlWrkSht1.Cells[xlRowCnt, xlColCnt++].Value = "PCN_Cleared_Date";

                        xlWrkSht1.Cells[xlRowCnt, xlColCnt++].Value = "BOM_Sent_to_Market_Date";
                        xlWrkSht1.Cells[xlRowCnt, xlColCnt++].Value = "FCC_601_Filed_Date";

                        xlWrkSht1.Select("A1:N1");
                        xlWrkSht1.SelectedRange.Style.Font.Bold = true;
                        xlWrkSht1.SelectedRange.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        xlWrkSht1.SelectedRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.ColorTranslator.FromHtml("#B4C6E7"));

                        if (dt != null)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                xlRowCnt++;
                                xlWrkSht1.Cells[xlRowCnt, 1].Value = Convert.ToString(dt.Rows[i]["Region"]);
                                xlWrkSht1.Cells[xlRowCnt, 2].Value = Convert.ToString(dt.Rows[i]["Market"]);
                                xlWrkSht1.Cells[xlRowCnt, 3].Value = Convert.ToString(dt.Rows[i]["Requester"]);
                                xlWrkSht1.Cells[xlRowCnt, 4].Value = Convert.ToString(dt.Rows[i]["Site_ID"]);
                                xlWrkSht1.Cells[xlRowCnt, 5].Value = Convert.ToString(dt.Rows[i]["Status"]);
                                xlWrkSht1.Cells[xlRowCnt, 6].Value = ((Convert.ToString(dt.Rows[i]["Desktop_Analysis_Request_Date"]) != null && Convert.ToString(dt.Rows[i]["Desktop_Analysis_Request_Date"]) != "") ? Convert.ToDateTime(dt.Rows[i]["Desktop_Analysis_Request_Date"]).ToString("MM/dd/yyyy") : string.Empty);
                                xlWrkSht1.Cells[xlRowCnt, 7].Value = ((Convert.ToString(dt.Rows[i]["Los_Upload_Date"]) != null && Convert.ToString(dt.Rows[i]["Los_Upload_Date"]) != "") ? Convert.ToDateTime(dt.Rows[i]["Los_Upload_Date"]).ToString("MM/dd/yyyy") : string.Empty);
                                xlWrkSht1.Cells[xlRowCnt, 8].Value = ((Convert.ToString(dt.Rows[i]["Prelim_Design_Date"]) != null && Convert.ToString(dt.Rows[i]["Prelim_Design_Date"]) != "") ? Convert.ToDateTime(dt.Rows[i]["Prelim_Design_Date"]).ToString("MM/dd/yyyy") : string.Empty);
                                xlWrkSht1.Cells[xlRowCnt, 9].Value = ((Convert.ToString(dt.Rows[i]["Final_Design_Requested_Date"]) != null && Convert.ToString(dt.Rows[i]["Final_Design_Requested_Date"]) != "") ? Convert.ToDateTime(dt.Rows[i]["Final_Design_Requested_Date"]).ToString("MM/dd/yyyy") : string.Empty);

                                xlWrkSht1.Cells[xlRowCnt, 10].Value = ((Convert.ToString(dt.Rows[i]["Final_Design_Completed_Date"]) != null && Convert.ToString(dt.Rows[i]["Final_Design_Completed_Date"]) != "") ? Convert.ToDateTime(dt.Rows[i]["Final_Design_Completed_Date"]).ToString("MM/dd/yyyy") : string.Empty);
                                xlWrkSht1.Cells[xlRowCnt, 11].Value = ((Convert.ToString(dt.Rows[i]["PCN_Filed_date"]) != null && Convert.ToString(dt.Rows[i]["PCN_Filed_date"]) != "") ? Convert.ToDateTime(dt.Rows[i]["PCN_Filed_date"]).ToString("MM/dd/yyyy") : string.Empty);
                                xlWrkSht1.Cells[xlRowCnt, 12].Value = ((Convert.ToString(dt.Rows[i]["PCN_Cleared_Date"]) != null && Convert.ToString(dt.Rows[i]["PCN_Cleared_Date"]) != "") ? Convert.ToDateTime(dt.Rows[i]["PCN_Cleared_Date"]).ToString("MM/dd/yyyy") : string.Empty);
                                                                
                                
                                xlWrkSht1.Cells[xlRowCnt, 13].Value = ((Convert.ToString(dt.Rows[i]["BOM_Sent_to_Market_Date"]) != null && Convert.ToString(dt.Rows[i]["BOM_Sent_to_Market_Date"]) != "") ? Convert.ToDateTime(dt.Rows[i]["BOM_Sent_to_Market_Date"]).ToString("MM/dd/yyyy") : string.Empty);
                                xlWrkSht1.Cells[xlRowCnt, 14].Value = ((Convert.ToString(dt.Rows[i]["FCC_601_Filed_Date"]) != null && Convert.ToString(dt.Rows[i]["FCC_601_Filed_Date"]) != "") ? Convert.ToDateTime(dt.Rows[i]["FCC_601_Filed_Date"]).ToString("MM/dd/yyyy") : string.Empty);
                            }
                        }
                        for (int colCnt = 1; colCnt <= xlColCnt; colCnt++)
                        {
                            xlWrkSht1.Column(colCnt).AutoFit();
                        }
                        xlWrkSht1.Column(xlColCnt).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        xlWrkSht1.Select("A1:N" + xlRowCnt);
                        xlWrkSht1.SelectedRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        xlWrkSht1.SelectedRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        xlWrkSht1.SelectedRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        xlWrkSht1.SelectedRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

                                              
                        
                        
                       

                        xlPkg.SaveAs(xlFileStream);
                        xlWrkSht1.Dispose();
                    }


                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx");
                    xlFileStream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();

                }




            
        }
    }

    protected void txt_SiteID_TextChanged(object sender, EventArgs e)
    {        
        if (!string.IsNullOrEmpty(this.txt_SiteID.Text))
        {
            this.txt_SiteID.Text = this.txt_SiteID.Text.ToUpper().Trim();
            Submit_Click(sender, e);
        }
    }
}
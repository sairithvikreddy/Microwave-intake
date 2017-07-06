<%@ Page Language="C#" AutoEventWireup="true" Inherits="CMG" CodeFile="CMG.aspx.cs"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePageMethods="true"
        AsyncPostBackTimeout="100000">
    </asp:ToolkitScriptManager>--%>
    <div id="content">
        <script type="text/javascript" language="javascript">
            function uploadError_Donor_Confirmed(sender, args) {
            }
            function startUpload_Donor_Confirmed(sender, args) {
            }
            function uploadComplete_Donor_Confirmed(sender, args) {
            }
        </script>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <p></p>
                <h2>AVAILABLE DONOR STUDY (Market / National Transport)</h2>
   
        <table>
            <tr>
                <td>
                    
                </td>
                <td></td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Status</td>
                <td>
                    <asp:TextBox ID="txt_Status" runat="server" AutoPostBack="True" 
                        CssClass="myTestBox2" ReadOnly="True" Width="300px">Pending Donor</asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Requester
                </td>
                <td>
                    <asp:TextBox ID="txt_Requester" runat="server" AutoPostBack="True" 
                        CssClass="myTestBox2" ReadOnly="True" Width="300px"></asp:TextBox>
                    </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Project Type</td>
                <td>
                    <asp:TextBox ID="txt_ProjectType" runat="server" AutoPostBack="True" 
                        CssClass="myTestBox2" ReadOnly="True" Width="300px"></asp:TextBox>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Upload Data</td>
                <td>
                    <asp:AsyncFileUpload ID="AsyncFileUpload_Donor_Confirmed" runat="server" 
                        CompleteBackColor="LightGreen" ErrorBackColor="Red" 
                        OnClientUploadComplete="uploadComplete_Donor_Confirmed" 
                        OnClientUploadError="uploadError_Donor_Confirmed" 
                        OnClientUploadStarted="startUpload_Donor_Confirmed" 
                        OnUploadedComplete="AsyncFileUpload_Donor_Confirmed_UploadedComplete" 
                        ThrobberID="Throbber" UploaderStyle="Modern" UploadingBackColor="#66CCFF" 
                        Visible="true" Width="538px" />
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="sidebar">
      <h3>Links</h3>
      <ul class="side">      
        <li>
           <asp:LinkButton ID="ib_ExportTemplate" runat="server">Get Input Template (Excel)</asp:LinkButton>            
        </li>
        <li>
           <asp:LinkButton ID="ib_Download" runat="server">Download Existing Data</asp:LinkButton>            
        </li>
      </ul>
    </div> 
</asp:Content>

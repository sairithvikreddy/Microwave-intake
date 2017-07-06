<%@ Page Language="C#" AutoEventWireup="true" Inherits="Intake" CodeFile="Intake.aspx.cs" MasterPageFile="~/MasterPage.master"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePageMethods="true"
        AsyncPostBackTimeout="100000">
    </asp:ToolkitScriptManager>--%>
    <div id="content">
            <script type="text/javascript" language="javascript">
                function uploadError_1A2C(sender, args) {
                    document.getElementById('<% =lbl_Error_1.ClientID %>').innerText = args.get_fileName() + ": " + args.get_errorMessage() + " ";
                }

                function startUpload_1A2C(sender, args) {
                    document.getElementById('<% =lbl_Status_1.ClientID %>').innerText = 'Uploading Started.';
                }

                function uploadComplete_1A2C(sender, args) {

                    var filename = args.get_fileName();
                    var contentType = args.get_contentType();
                    var text = "Size of " + filename + " is " + args.get_length() + " bytes. Uploading is done.";
                   
                    document.getElementById('<% =lbl_Error_1.ClientID %>').innerText = " ";
                    document.getElementById('<% =lbl_Status_1.ClientID %>').innerText = text;

                    document.getElementById('<% =Pending_Donor_Submit.ClientID %>').disabled = false;

                    document.getElementById('<% =l_Submitted_1.ClientID %>').innerText = "";

                    //window.location.href("ExportToExcel_DT.aspx");
                }

                function uploadError_FAAFCC(sender, args) {
                    document.getElementById('<% =lbl_Error_1.ClientID %>').innerText = args.get_fileName() + ": " + args.get_errorMessage() + " ";
                }

                function startUpload_FAAFCC(sender, args) {
                    document.getElementById('<% =lbl_Status_1.ClientID %>').innerText = 'Uploading Started.';
                }

                function uploadComplete_FAAFCC(sender, args) {

                    var filename = args.get_fileName();
                    var contentType = args.get_contentType();
                    var text = "Size of " + filename + " is " + args.get_length() + " bytes. Uploading is done.";
                    
                    document.getElementById('<% =lbl_Error_1.ClientID %>').innerText = " ";
                    document.getElementById('<% =lbl_Status_1.ClientID %>').innerText = text;
                    document.getElementById('<% =l_Submitted_1.ClientID %>').innerText = "";
                }


                function uploadError_Donor_Study_Status(sender, args) {
                    document.getElementById('<% =lbl_Error_2.ClientID %>').innerText = args.get_fileName() + ": " + args.get_errorMessage() + " ";
                }

                function startUpload_Donor_Study_Status(sender, args) {
                    document.getElementById('<% =lbl_Status_2.ClientID %>').innerText = 'Uploading Started.';
                }

                function uploadComplete_Donor_Study_Status(sender, args) {

                    var filename = args.get_fileName();
                    var contentType = args.get_contentType();
                    var text = "Size of " + filename + " is " + args.get_length() + " bytes. Uploading is done.";
                    
                    document.getElementById('<% =lbl_Error_2.ClientID %>').innerText = " ";
                    document.getElementById('<% =lbl_Status_2.ClientID %>').innerText = text;
                    document.getElementById('<% =lbl_Error_LOS_Survey.ClientID %>').innerText = "";
                }

                function uploadError_LOS_Survey(sender, args) {
                    document.getElementById('<% =lbl_Error_4.ClientID %>').innerText = args.get_fileName() + ": " + args.get_errorMessage() + " ";
                }

                function startUpload_LOS_Survey(sender, args) {
                    document.getElementById('<% =lbl_Status_4.ClientID %>').innerText = 'Uploading Started.';
                }

                function uploadComplete_LOS_Survey(sender, args) {

                    var filename = args.get_fileName();
                    var contentType = args.get_contentType();
                    var text = "Size of " + filename + " is " + args.get_length() + " bytes. Uploading is done.";
                    
                    document.getElementById('<% =lbl_Error_4.ClientID %>').innerText = " ";
                    document.getElementById('<% =lbl_Status_4.ClientID %>').innerText = text;
                    document.getElementById('<% =lbl_Error_PRELIM_Design.ClientID %>').innerText = "";
                }


                function uploadError_Donor_Confirmed(sender, args) {
                    document.getElementById('<% =lbl_Error_3.ClientID %>').innerText = args.get_fileName() + ": " + args.get_errorMessage() + " ";
                }

                function startUpload_Donor_Confirmed(sender, args) {
                    document.getElementById('<% =lbl_Status_3.ClientID %>').innerText = 'Uploading Started.';
                }

                function uploadComplete_Donor_Confirmed(sender, args) {

                    var filename = args.get_fileName();
                    var contentType = args.get_contentType();
                    var text = "Size of " + filename + " is " + args.get_length() + " bytes. Uploading is done.";
                    
                    document.getElementById('<% =lbl_Error_3.ClientID %>').innerText = " ";
                    document.getElementById('<% =lbl_Status_3.ClientID %>').innerText = text;
                    document.getElementById('<% =lbl_Error_Donor_Confirmed.ClientID %>').innerText = "";
                }



                function MicrowaveTopology_Client_Error(sender, args) {
                    document.getElementById('<% =lbl_Error_2.ClientID %>').innerText = args.get_fileName() + ": " + args.get_errorMessage() + " ";
                }

                function MicrowaveTopology_Client_Started(sender, args) {
                    document.getElementById('<% =lbl_Status_2.ClientID %>').innerText = 'Uploading Started.';
                }

                function MicrowaveTopology_Client_Complete(sender, args) {

                    var filename = args.get_fileName();
                    var contentType = args.get_contentType();
                    var text = "Size of " + filename + " is " + args.get_length() + " bytes. Uploading is done.";
                    
                    document.getElementById('<% =lbl_Error_2.ClientID %>').innerText = " ";
                    document.getElementById('<% =lbl_Status_2.ClientID %>').innerText = text;
                    document.getElementById('<% =lbl_Error_LOS_Survey.ClientID %>').innerText = "";
                }


                function Candidate_Site1_Client_Error(sender, args) {
                    document.getElementById('<% =lbl_Error_2.ClientID %>').innerText = args.get_fileName() + ": " + args.get_errorMessage() + " ";
                }

                function Candidate_Site1_Client_Started(sender, args) {
                    document.getElementById('<% =lbl_Status_2.ClientID %>').innerText = 'Uploading Started.';
                }

                function Candidate_Site1_Client_Complete(sender, args) {

                    var filename = args.get_fileName();
                    var contentType = args.get_contentType();
                    var text = "Size of " + filename + " is " + args.get_length() + " bytes. Uploading is done.";
                    
                    document.getElementById('<% =lbl_Error_2.ClientID %>').innerText = " ";
                    document.getElementById('<% =lbl_Status_2.ClientID %>').innerText = text;
                    document.getElementById('<% =lbl_Error_LOS_Survey.ClientID %>').innerText = "";
                }

                function Candidate_Site2_Client_Error(sender, args) {
                    document.getElementById('<% =lbl_Error_2.ClientID %>').innerText = args.get_fileName() + ": " + args.get_errorMessage() + " ";
                }

                function Candidate_Site2_Client_Started(sender, args) {
                    document.getElementById('<% =lbl_Status_2.ClientID %>').innerText = 'Uploading Started.';
                }

                function Candidate_Site2_Client_Complete(sender, args) {

                    var filename = args.get_fileName();
                    var contentType = args.get_contentType();
                    var text = "Size of " + filename + " is " + args.get_length() + " bytes. Uploading is done.";
                    
                    document.getElementById('<% =lbl_Error_2.ClientID %>').innerText = " ";
                    document.getElementById('<% =lbl_Status_2.ClientID %>').innerText = text;
                    document.getElementById('<% =lbl_Error_LOS_Survey.ClientID %>').innerText = "";
                }

                function Candidate_Site3_Client_Error(sender, args) {
                    document.getElementById('<% =lbl_Error_2.ClientID %>').innerText = args.get_fileName() + ": " + args.get_errorMessage() + " ";
                }

                function Candidate_Site3_Client_Started(sender, args) {
                    document.getElementById('<% =lbl_Status_2.ClientID %>').innerText = 'Uploading Started.';
                }

                function Candidate_Site3_Client_Complete(sender, args) {

                    var filename = args.get_fileName();
                    var contentType = args.get_contentType();
                    var text = "Size of " + filename + " is " + args.get_length() + " bytes. Uploading is done.";
                    
                    document.getElementById('<% =lbl_Error_2.ClientID %>').innerText = " ";
                    document.getElementById('<% =lbl_Status_2.ClientID %>').innerText = text;
                    document.getElementById('<% =lbl_Error_LOS_Survey.ClientID %>').innerText = "";
                }

                   </script>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <p>
                </p>
                <h2>AVAILABLE DONOR STUDY (Market / National Transport)</h2>
                <table>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Status
                        </td>
                        <td>
                            <asp:TextBox ID="lbl_ProjectStatus" runat="server" AutoPostBack="True" CssClass="myTestBox2"
                                ReadOnly="True" Width="300px">Pending Donor</asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Requestor
                        </td>
                        <td>
                            <asp:Label ID="DDL_Requester" runat="server"></asp:Label>                           
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Project Type
                        </td>
                        <td>
                            <asp:DropDownList ID="DDL_ProjectType" runat="server" AutoPostBack="True" 
                                OnSelectedIndexChanged="DDL_ProjectType_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:RadioButtonList ID="RBL_ProjectTypeLicensedUnlicensed" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Selected="False" Text="Licensed"></asp:ListItem>
                                <asp:ListItem Selected="False" Text="Unlicensed"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="Start Date"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="ProjectType_StartTime" runat="server" Width="69px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="End Date"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="ProjectType_EndTime" runat="server" Width="68px"></asp:TextBox>
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
                <br />
                <h2>Recipient Information (Market / National Transport)</h2>
                <table>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Region</td>
                        <td>                            
                            <asp:Label ID="DDL_Region" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Market </td>
                        <td>
                            <asp:Label ID="DDL_Market" runat="server"></asp:Label>
                            
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Recipient_SiteId </td>
                        <td>
                            <asp:TextBox ID="Pending_Donor_SiteID" ReadOnly="true" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Structure Type</td>
                        <td>
                            <asp:TextBox ID="Pending_Donor_Structure_Type" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Structure Height (Feet)</td>
                        <td>
                            <asp:TextBox ID="Pending_Donor_Structure_Height" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;Known Available Center Line (Feet)
                        </td>
                        <td>
                            <asp:TextBox ID="Pending_Donor_Known_Available_Center_Line" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Requested Throughput (Mbps)</td>
                        <td>
                            <asp:TextBox ID="Pending_Donor_Requested_Throughput" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Site Accuracy Report- 1A/2C Survey </td>
                        <td>
                            <asp:AsyncFileUpload ID="AsyncFileUpload_1A2C" runat="server" 
                                CompleteBackColor="LightGreen" ErrorBackColor="Red" 
                                OnClientUploadComplete="uploadComplete_1A2C" 
                                OnClientUploadError="uploadError_1A2C" OnClientUploadStarted="startUpload_1A2C" 
                                OnUploadedComplete="AsyncFileUpload_1A2C_UploadedComplete" 
                                ThrobberID="Throbber" UploaderStyle="Modern" UploadingBackColor="#66CCFF" 
                                Visible="true" Width="350px" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Regulatory Compliance FAA/FCC/ASR
                        </td>
                        <td>
                            <asp:AsyncFileUpload ID="AsyncFileUpload_FAAFCC" runat="server" 
                                CompleteBackColor="LightGreen" ErrorBackColor="Red" 
                                OnClientUploadComplete="uploadComplete_FAAFCC" 
                                OnClientUploadError="uploadError_FAAFCC" 
                                OnClientUploadStarted="startUpload_FAAFCC" 
                                OnUploadedComplete="AsyncFileUpload_FAAFCC_UploadedComplete" 
                                ThrobberID="Throbber" UploaderStyle="Modern" UploadingBackColor="#66CCFF" 
                                Visible="true" Width="350px" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            DoD or 56Kms off the border</td>
                        <td>
                            <asp:DropDownList ID="Pending_Donor_DoD_56" runat="server">
                                <asp:ListItem Text="No"></asp:ListItem>
                                <asp:ListItem Text="Yes"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Label ID="lbl_Status_1" runat="server" 
                                Style="font-family: Arial; font-size: small;"></asp:Label>
                            <asp:Label ID="lbl_Error_1" runat="server" ForeColor="Red" 
                                Style="font-family: Arial; font-size: small;"></asp:Label>
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
                    <tr>
                        <td>
                            Market Recommended Donor</td>
                        <td>
                            <asp:TextBox ID="Pending_Donor_Market_Recommended_Donner" runat="server"></asp:TextBox>                            
                            
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Proposed On Air Date</td>
                        <td>
                            <asp:TextBox ID="Pending_Donor_Proposed_On_Air_Date" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender_TB_Datefrom" runat="server" 
                                CssClass="cal_Theme1" Format="MM/dd/yyyy" 
                                TargetControlID="Pending_Donor_Proposed_On_Air_Date">
                            </asp:CalendarExtender>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Cluster Type </td>
                        <td>
                            <asp:DropDownList ID="Pending_Donor_Cluster_Type" runat="server">
                                <asp:ListItem Text="New"></asp:ListItem>
                                <asp:ListItem Text="Existing"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Cluster ID </td>
                        <td>
                            <asp:TextBox ID="Pending_Donor_Cluster_ID" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Microwave Topology
                        </td>
                        <td>
                            <asp:TextBox ID="Pending_Donor_Cluster_Topology" runat="server" Width="397px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Upload Microwave Topology</td>
                        <td>
                            <asp:AsyncFileUpload ID="MicrowaveTopology_AsyncFileUpload" runat="server" 
                                CompleteBackColor="LightGreen" ErrorBackColor="Red" 
                                OnClientUploadComplete="MicrowaveTopology_Client_Complete" 
                                OnClientUploadError="MicrowaveTopology_Client_Error" OnClientUploadStarted="MicrowaveTopology_Client_Started" 
                                OnUploadedComplete="MicrowaveTopology_AsyncFileUpload_UploadedComplete" 
                                style="text-align: left" ThrobberID="Throbber" UploaderStyle="Modern" 
                                UploadingBackColor="#66CCFF" Visible="true" Wdth="350px" />
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Microwave Topology Template</td>
                        <td>                           
                            \\devwsrfds002\MWIntakeUploads
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
                    <tr>
                        <td>
                            Existing CIR Value</td>
                        <td>
                            <asp:TextBox ID="Pending_Donor_Existing_CIR_Value" runat="server" Width="159px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            New CIR Value
                        </td>
                        <td>
                            <asp:TextBox ID="Pending_Donor_New_CIR_Value" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Desktop Analysis Request Date
                        </td>
                        <td>
                            <asp:TextBox ID="Pending_Donor_Desktop_Analysis_Request_Date" runat="server" 
                                ReadOnly="True"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Notes/Comments
                        </td>
                        <td>
                            <asp:TextBox ID="Pending_Donor_Notes_Comments" runat="server" 
                                TextMode="MultiLine" Width="502px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="l_Submitted_1" runat="server" ForeColor="Red" 
                                Style="font-family: Arial; font-size: small;" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="Pending_Donor_Submit" runat="server" Enabled="false" 
                                OnClick="Pending_Donor_Submit_Click" Text="Submit" Width="272px" />
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
                <br />
                <h2>DONOR CANDIDATES (Region)</h2>
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
                            DONOR CANDIDATES </td>
                        <td>
                            <asp:Label ID="Donor_Candidates_SiteID" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Candidates SITE ID1</td>
                        <td>
                            <asp:TextBox ID="Donor_Candidates_SiteID1" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Upload</td>
                        <td>
                            <asp:AsyncFileUpload ID="Candidate_Site1_AsyncFileUpload" runat="server" 
                                CompleteBackColor="LightGreen" ErrorBackColor="Red" 
                                OnClientUploadComplete="Candidate_Site1_Client_Complete" 
                                OnClientUploadError="Candidate_Site1_Client_Error" OnClientUploadStarted="Candidate_Site1_Client_Started" 
                                OnUploadedComplete="Candidate_Site1_AsyncFileUpload_UploadedComplete" 
                                style="text-align: left" ThrobberID="Throbber" UploaderStyle="Modern" 
                                UploadingBackColor="#66CCFF" Visible="true" Wdth="350px" />
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
                    <tr>
                        <td>
                            Candidates SITE ID2
                        </td>
                        <td>
                            <asp:TextBox ID="Donor_Candidates_SiteID2" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Upload</td>
                        <td>
                            <asp:AsyncFileUpload ID="Candidate_Site2_AsyncFileUpload" runat="server" 
                                CompleteBackColor="LightGreen" ErrorBackColor="Red" 
                                OnClientUploadComplete="Candidate_Site2_Client_Complete" 
                                OnClientUploadError="Candidate_Site2_Client_Error" OnClientUploadStarted="Candidate_Site2_Client_Started" 
                                OnUploadedComplete="Candidate_Site2_AsyncFileUpload_UploadedComplete" 
                                style="text-align: left" ThrobberID="Throbber" UploaderStyle="Modern" 
                                UploadingBackColor="#66CCFF" Visible="true" Wdth="350px" />
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
                    <tr>
                        <td>
                            Candidates SITE ID3</td>
                        <td>
                            <asp:TextBox ID="Donor_Candidates_SiteID3" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Upload</td>
                        <td>
                            <asp:AsyncFileUpload ID="Candidate_Site3_AsyncFileUpload" runat="server" 
                                CompleteBackColor="LightGreen" ErrorBackColor="Red" 
                                OnClientUploadComplete="Candidate_Site3_Client_Complete" 
                                OnClientUploadError="Candidate_Site3_Client_Error" OnClientUploadStarted="Candidate_Site3_Client_Started" 
                                OnUploadedComplete="Candidate_Site3_AsyncFileUpload_UploadedComplete" 
                                style="text-align: left" ThrobberID="Throbber" UploaderStyle="Modern" 
                                UploadingBackColor="#66CCFF" Visible="true" Wdth="350px" />
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
                    <tr>
                        <td>
                            <asp:Label ID="lbl_Error_LOS_Survey" runat="server" ForeColor="Red" 
                                Style="font-family: Arial; font-size: small;" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="LOS_Survey_Submit" runat="server" Enabled="false" 
                                OnClick="LOS_Survey_Submit_Click" Text="Submit" Width="272px" />
                        </td>
                        <td>
                            <asp:Label ID="lbl_Status_2" runat="server" 
                                Style="font-family: Arial; font-size: small;"></asp:Label>
                            <asp:Label ID="lbl_Error_2" runat="server" ForeColor="Red" 
                                Style="font-family: Arial; font-size: small;"></asp:Label>
                                                            <asp:Label ID="Throbber" runat="server" Style="display: none">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <img src="./Images/upload-progress-animation.gif" align="middle" alt="loading" />
                            
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </asp:Label>
                            
                        </td>
                    </tr>
                 </table>

                 <h2>LOS SURVEY (Market / National Transport)</h2>
                 <table>
                    <tr>
                        <td>                    
                        </td>
                        <td>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                        <tr>
                        <td>                    
                            LOS/Site Survey</td>
                        <td>
                            <asp:AsyncFileUpload ID="AsyncFileUpload_LOS_Survey" runat="server" 
                                CompleteBackColor="LightGreen" ErrorBackColor="Red" 
                                OnClientUploadComplete="uploadComplete_LOS_Survey" 
                                OnClientUploadError="uploadError_LOS_Survey" 
                                OnClientUploadStarted="startUpload_LOS_Survey" 
                                OnUploadedComplete="AsyncFileUpload_LOS_Survey_UploadedComplete" 
                                ThrobberID="Throbber" UploaderStyle="Modern" UploadingBackColor="#66CCFF" 
                                Visible="true" Width="350px" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                                        <tr>
                        <td>                    
                            LOS Survey Upload Date                    
                        </td>
                        <td>
                            <asp:Label ID="LOS_Survey_Upload_Date" runat="server">Not submited yet</asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                                        <tr>
                        <td>                    
                            <asp:Label ID="lbl_Status_4" runat="server" 
                                Style="font-family: Arial; font-size: small;"></asp:Label>
                        </td>
                        <td>
                            <asp:Button ID="PRELIM_Design_Submit" runat="server" Enabled="false" 
                                OnClick="PRELIM_Design_Submit_Click" Text="Submit" Width="272px" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                                        <tr>
                        <td>                    
                        </td>
                        <td>
                            <asp:Label ID="lbl_Error_PRELIM_Design" runat="server" ForeColor="Red" Style="font-family: Arial;
                                font-size: small;" Text=""></asp:Label>
                            <asp:Label ID="lbl_Error_4" runat="server" ForeColor="Red" 
                                Style="font-family: Arial; font-size: small;"></asp:Label>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                                        <tr>
                        <td>                    
                        </td>
                        <td>
                        </td>
                        <td>&nbsp;</td>
                    </tr>

                </table>

                <h2>&nbsp;PRELIM DESIGN&nbsp;(<strong>Region</strong>)</h2>
                 <table>
                    <tr>
                        <td>                    
                        </td>
                        <td>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                        <tr>
                        <td>                    
                            PRELIM DESIGN SENT TO MARKET</td>
                        <td>
                            <asp:AsyncFileUpload ID="AsyncFileUpload_Donor_Confirmed" runat="server" 
                                CompleteBackColor="LightGreen" ErrorBackColor="Red" 
                                OnClientUploadComplete="uploadComplete_Donor_Confirmed" 
                                OnClientUploadError="uploadError_Donor_Confirmed" 
                                OnClientUploadStarted="startUpload_Donor_Confirmed" 
                                OnUploadedComplete="AsyncFileUpload_Donor_Confirmed_UploadedComplete" 
                                ThrobberID="Throbber" UploaderStyle="Modern" UploadingBackColor="#66CCFF" 
                                Visible="true" Width="350px" />
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                            <asp:Label ID="lbl_Status_3" runat="server" Style="font-family: Arial; font-size: small;"></asp:Label>
                                                <asp:Label ID="lbl_Error_3" runat="server" ForeColor="Red" 
                                                    Style="font-family: Arial; font-size: small;"></asp:Label>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                     </tr>
                                        <tr>
                        <td>                    
                            Recipient_SiteId</td>
                        <td>
                            <asp:TextBox ID="Prelim_Design_Site_ID" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                                        <tr>
                        <td>                    
                            Structure Height&nbsp;
                        </td>
                        <td>
                            <asp:TextBox ID="Prelim_Design_Structure_Height" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                                        <tr>
                        <td>                    
                            Structure Type                    
                        </td>
                        <td>
                            <asp:TextBox ID="Prelim_Design_Structure_Type" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                                        <tr>
                        <td>                    
                            Confirmed Centerline</td>
                        <td>
                            <asp:TextBox ID="Prelim_Design_Confirmed_Centerline" runat="server"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>

                     <tr>
                         <td>
                             Confirmed Dish Size</td>
                         <td>
                             <asp:TextBox ID="Prelim_Design_Confirmed_Dish_Size" runat="server"></asp:TextBox>
                         </td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td>
                             Confirmed Radio Model
                         </td>
                         <td>
                             <asp:TextBox ID="Prelim_Design_Confirmed_Radio_Model" runat="server"></asp:TextBox>
                         </td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td>
                             Confirmed Radio Make</td>
                         <td>
                             <asp:TextBox ID="Prelim_Design_Confirmed_Radio_Make" runat="server"></asp:TextBox>
                         </td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td>
                             PRELIM Design sent to market date</td>
                         <td>
                             <asp:Label ID="Prelim_Design_Date" runat="server">Not submitted yet</asp:Label>
                         </td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td valign="top">
                             Comments</td>
                         <td>
                             <asp:TextBox ID="Prelim_Design_Comments" runat="server" TextMode="MultiLine" 
                                 Width="502px"></asp:TextBox>
                         </td>
                         <td>
                             &nbsp;</td>
                     </tr>
                     <tr>
                         <td>
                             <asp:Label ID="lbl_Error_Donor_Confirmed" runat="server" ForeColor="Red" Style="font-family: Arial;
                                font-size: small;" Text=""></asp:Label>
                         </td>
                         <td>
                             <asp:Button ID="Donor_Confirmed_Submit" runat="server" Enabled="false" 
                                 OnClick="Donor_Confirmed_Submit_Click" Text="Submit" Width="272px" />
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
        <h3>
            Links</h3>
        <ul class="side">
            <li>
                <%--           <asp:LinkButton ID="ib_ExportTemplate" runat="server">Get Input Template (Excel)</asp:LinkButton>  --%>
            </li>
            <li>
                <%--           <asp:LinkButton ID="ib_Download" runat="server">Download Existing Data</asp:LinkButton>  --%>
            </li>
        </ul>
    </div>
</asp:Content>

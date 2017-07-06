<%@ Page Language="C#" AutoEventWireup="true" Inherits="Final" CodeFile="Final.aspx.cs"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--    <asp:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server" EnablePageMethods="true"
        AsyncPostBackTimeout="100000">
    </asp:ToolkitScriptManager>--%>
    <div id="content">
        <script type="text/javascript" language="javascript">

            function uploadError_PCN_Document(sender, args) {
                document.getElementById('<% =lbl_Error_1.ClientID %>').innerText = args.get_fileName() + ": " + args.get_errorMessage() + " ";
            }

            function startUpload_PCN_Document(sender, args) {
                document.getElementById('<% =lbl_Status_1.ClientID %>').innerText = 'Uploading Started.';
            }

            function uploadComplete_PCN_Document(sender, args) {

                var filename = args.get_fileName();
                var contentType = args.get_contentType();
                var text = "Size of " + filename + " is " + args.get_length() + " bytes. Uploading is done.";
                
                document.getElementById('<% =lbl_Error_1.ClientID %>').innerText = " ";
                document.getElementById('<% =lbl_Status_1.ClientID %>').innerText = text;
            }


            
            function uploadError_Warning_Document(sender, args) {
                document.getElementById('<% =lbl_Error_2.ClientID %>').innerText = args.get_fileName() + ": " + args.get_errorMessage() + " ";
            }

            function startUpload_Warning_Document(sender, args) {
                document.getElementById('<% =lbl_Status_2.ClientID %>').innerText = 'Uploading Started.';
            }

            function uploadComplete_Warning_Document(sender, args) {

                var filename = args.get_fileName();
                var contentType = args.get_contentType();
                var text = "Size of " + filename + " is " + args.get_length() + " bytes. Uploading is done.";
               
                document.getElementById('<% =lbl_Error_2.ClientID %>').innerText = " ";
                document.getElementById('<% =lbl_Status_2.ClientID %>').innerText = text;
            }


            

            function fileUploadBOM_Client_Error(sender, args) {
                document.getElementById('<% =Label2.ClientID %>').innerText = args.get_fileName() + ": " + args.get_errorMessage() + " ";
            }

            function fileUploadBOM_Client_Started(sender, args) {
                document.getElementById('<% =Label1.ClientID %>').innerText = 'Uploading Started.';
            }

            function fileUploadBOM_Client_Complete(sender, args) {

                var filename = args.get_fileName();
                var contentType = args.get_contentType();
                var text = "Size of " + filename + " is " + args.get_length() + " bytes. Uploading is done.";

                document.getElementById('<% =Label2.ClientID %>').innerText = " ";
                document.getElementById('<% =Label1.ClientID %>').innerText = text;
            }


        </script>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <p>
                </p>
                <h2>Final Design and BOM Request (Market)</h2>
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
                            <asp:TextBox ID="l_Status" runat="server" AutoPostBack="True" CssClass="myTestBox2"
                                ReadOnly="True" Width="300px">Final Design and BOM Request</asp:TextBox>
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
                            <asp:TextBox ID="txt_Final_Design_and_BOM_Request_Requester" runat="server" 
                                AutoPostBack="True" CssClass="myTestBox2"
                                ReadOnly="True" Width="300px"></asp:TextBox>
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
                            <asp:TextBox ID="txt_Final_Design_and_BOM_Request_ProjectType" runat="server" 
                                AutoPostBack="True" CssClass="myTestBox2"
                                ReadOnly="True" Width="300px"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Proposed On Air Date</td>
                        <td>
                            <asp:TextBox ID="txt_Final_Design_and_BOM_Request_Proposed_On_Air_Date" 
                                runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="txt_Final_Design_and_BOM_Request_Proposed_On_Air_Date_CalendarExtender" 
                                runat="server" CssClass="cal_Theme1" Format="MM/dd/yyyy" 
                                TargetControlID="txt_Final_Design_and_BOM_Request_Proposed_On_Air_Date">
                            </asp:CalendarExtender>
<%--                            <asp:MaskedEditExtender ID="txt_Final_Design_and_BOM_Request_Proposed_On_Air_Date_MaskedEditExtender" 
                                runat="server" Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="Invalid" 
                                TargetControlID="txt_Final_Design_and_BOM_Request_Proposed_On_Air_Date">
                            </asp:MaskedEditExtender>--%>
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
                            <strong>Recipient Information</strong></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Recipient_SiteId
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Final_Design_and_BOM_Request_SiteID" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Final Center line (Feet)</td>
                        <td>
                            <asp:TextBox ID="txt_Final_Design_and_BOM_Request_Final_Center_Line" 
                                runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Final Dish Size (Feet)
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Final_Design_and_BOM_Request_Final_Dish_Size" 
                                runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Final Radio Make
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Final_Design_and_BOM_Request_Final_Radio_Make" 
                                runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Final Radio Model
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Final_Design_and_BOM_Request_Final_Radio_Model" 
                                runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Radio configuration Type
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Final_Design_and_BOM_Request_Radio_configuration_Type" 
                                runat="server"></asp:TextBox>
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
                            <strong>Donor Information</strong></td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Donor SiteId</td>
                        <td>
                            <asp:TextBox ID="txt_Final_Design_and_BOM_Request_Donor_SiteId" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Donor Dish Size (Feet)
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Final_Design_and_BOM_Request_Donor_Dish_Size" 
                                runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Donor Radio Make</td>
                        <td>
                            <asp:TextBox ID="txt_Final_Design_and_BOM_Request_Donor_Radio_Make" 
                                runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Donor Radio Model
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Final_Design_and_BOM_Request_Donor_Radio_Model" 
                                runat="server"></asp:TextBox>
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
                            Final Design Request Date
                        </td>
                        <td>
                            <asp:Label ID="lbl_Final_Design_and_BOM_Request_Date" runat="server">Not 
                            Requested yet</asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="btn_Final_Design_and_BOM_Request_Submit" runat="server" Enabled="false" 
                                OnClick="Final_Design_And_BOM_Submit_Click" Text="Submit" Width="272px" />
                        </td>
                        <td>
                            <asp:Label ID="l_Submitted_1" runat="server" ForeColor="Red" 
                                Style="font-family: Arial; font-size: small;"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <h2>Final Donor information (Region)</h2>
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
                            Donor_SiteId
                        </td>
                        <td>
                            <asp:TextBox ID="txt_Final_Donor_Information_SiteID" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Final Design Completed</td>
                        <td>
                            <asp:TextBox ID="txt_Final_Donor_Information_Final_Design_Completed_Date" 
                                runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender7" runat="server" 
                                CssClass="cal_Theme1" Format="MM/dd/yyyy" 
                                TargetControlID="txt_Final_Donor_Information_Final_Design_Completed_Date">
                            </asp:CalendarExtender>
<%--                            <asp:MaskedEditExtender ID="MaskedEditExtender7" runat="server" 
                                Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="Invalid" 
                                TargetControlID="txt_Final_Donor_Information_Final_Design_Completed_Date">
                            </asp:MaskedEditExtender>--%>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Final Center Line (Feet) </td>
                        <td>
                            <asp:Label ID="lbl_Final_Donor_Information_Final_Center_Line" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Final Dish Size (Feet) </td>
                        <td>
                            <asp:Label ID="lbl_Final_Donor_Information_Final_Dish_Size" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Final Radio Make</td>
                        <td>
                            <asp:Label ID="lbl_Final_Donor_Information_Final_Radio_Make" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Final Radio Model</td>
                        <td>
                            <asp:Label ID="lbl_Final_Donor_Information_Final_Radio_Model" runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Radio Configuration Type
                        </td>
                        <td>
                            <asp:Label ID="lbl_Final_Donor_Information_Radio_Configuration_Type" 
                                runat="server"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            Designed CIR Value (Mbps)</td>
                        <td>
                            <asp:TextBox ID="txt_Final_Donor_Information_Designed_CIR_Value" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            PCN Filed Date</td>
                        <td>
                            <asp:TextBox ID="txt_Final_Donor_Information_PCN_Filed_Date" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" 
                                CssClass="cal_Theme1" Format="MM/dd/yyyy" 
                                TargetControlID="txt_Final_Donor_Information_PCN_Filed_Date">
                            </asp:CalendarExtender>
<%--                            <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server" 
                                Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="Invalid" 
                                TargetControlID="txt_Final_Donor_Information_PCN_Filed_Date">
                            </asp:MaskedEditExtender>--%>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            PCN Document
                        </td>
                        <td>
                            <asp:AsyncFileUpload ID="AFU_Final_Donor_Information_PCN_Document" runat="server" 
                                CompleteBackColor="LightGreen" ErrorBackColor="Red" 
                                OnClientUploadComplete="uploadComplete_PCN_Document" 
                                OnClientUploadError="uploadError_PCN_Document" 
                                OnClientUploadStarted="startUpload_PCN_Document" 
                                OnUploadedComplete="AFU_Final_Donor_Information_PCN_Document_OnUploadedComplete" 
                                ThrobberID="Throbber" UploaderStyle="Modern" UploadingBackColor="#66CCFF" 
                                Visible="true" Width="350px" />
                        </td>
                        <td>
                            <asp:Label ID="lbl_Status_1" runat="server" 
                                Style="font-family: Arial; font-size: small;"></asp:Label>
                            <asp:Label ID="lbl_Error_1" runat="server" ForeColor="Red" 
                                Style="font-family: Arial; font-size: small;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            PCN Warning</td>
                        <td>
                            <asp:RadioButtonList ID="Final_Donor_Information_PCN_Warning" runat="server" 
                                RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            PCN Warning Document</td>
                        <td>
                            <asp:AsyncFileUpload ID="AFU_Final_Donor_Information_PCN_Warning_Document" runat="server" 
                                CompleteBackColor="LightGreen" ErrorBackColor="Red" 
                                OnClientUploadComplete="uploadComplete_Warning_Document" 
                                OnClientUploadError="uploadError_Warning_Document" 
                                OnClientUploadStarted="startUpload_Warning_Document" 
                                OnUploadedComplete="AFU_Final_Donor_Information_PCN_Warning_Document_OnUploadedComplete" 
                                ThrobberID="Throbber" UploaderStyle="Modern" UploadingBackColor="#66CCFF" 
                                Visible="true" Width="350px" />
                        </td>
                        <td>
                            <asp:Label ID="lbl_Status_2" runat="server" 
                                Style="font-family: Arial; font-size: small;"></asp:Label>
                            <asp:Label ID="lbl_Error_2" runat="server" ForeColor="Red" 
                                Style="font-family: Arial; font-size: small;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            PCN Cleared Date</td>
                        <td>
                            <asp:TextBox ID="txt_Final_Donor_Information_PCN_Cleared_Date" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" 
                                CssClass="cal_Theme1" Format="MM/dd/yyyy" 
                                TargetControlID="txt_Final_Donor_Information_PCN_Cleared_Date">
                            </asp:CalendarExtender>
<%--                            <asp:MaskedEditExtender ID="MaskedEditExtender3" runat="server" 
                                Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="Invalid" 
                                TargetControlID="txt_Final_Donor_Information_PCN_Cleared_Date">
                            </asp:MaskedEditExtender>--%>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            BOM Upload
                        </td>
                        <td>
                            <asp:AsyncFileUpload ID="AFU_Final_Donor_Information_BOM_Upload" runat="server" 
                                CompleteBackColor="LightGreen" ErrorBackColor="Red" 
                                OnClientUploadComplete="fileUploadBOM_Client_Complete" 
                                OnClientUploadError="fileUploadBOM_Client_Error" 
                                OnClientUploadStarted="fileUploadBOM_Client_Started" 
                                OnUploadedComplete="AFU_Final_Donor_Information_BOM_Upload_OnUploadedComplete" 
                                ThrobberID="Throbber" UploaderStyle="Modern" UploadingBackColor="#66CCFF" 
                                Visible="true" Width="350px" />
                        </td>
                        <td>
                            <asp:Label ID="Label1" runat="server" 
                                Style="font-family: Arial; font-size: small;"></asp:Label>
                            <asp:Label ID="Label2" runat="server" ForeColor="Red" 
                                Style="font-family: Arial; font-size: small;"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            BOM Sent to Market Status</td>
                        <td>
                            <asp:RadioButtonList ID="Final_Donor_Information_BOM_Sent_to_Market_Status" 
                                runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Yes"></asp:ListItem>
                                <asp:ListItem Selected="True" Text="No"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            BOM Sent to Market Date</td>
                        <td>
                            <asp:TextBox ID="txt_Final_Donor_Information_BOM_Sent_to_Market_Date" 
                                runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender4" runat="server" 
                                CssClass="cal_Theme1" Format="MM/dd/yyyy" 
                                TargetControlID="txt_Final_Donor_Information_BOM_Sent_to_Market_Date">
                            </asp:CalendarExtender>
<%--                            <asp:MaskedEditExtender ID="MaskedEditExtender4" runat="server" 
                                Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="Invalid" 
                                TargetControlID="txt_Final_Donor_Information_BOM_Sent_to_Market_Date">
                            </asp:MaskedEditExtender>--%>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            FCC 601 Filed Date</td>
                        <td>
                            <asp:TextBox ID="txt_Final_Donor_Information_FCC_601_Filed_Date" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" 
                                CssClass="cal_Theme1" Format="MM/dd/yyyy" 
                                TargetControlID="txt_Final_Donor_Information_FCC_601_Filed_Date">
                            </asp:CalendarExtender>
<%--                            <asp:MaskedEditExtender ID="MaskedEditExtender5" runat="server" 
                                Mask="99/99/9999" MaskType="Date" OnInvalidCssClass="Invalid" 
                                TargetControlID="txt_Final_Donor_Information_FCC_601_Filed_Date">
                            </asp:MaskedEditExtender>--%>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            <asp:Button ID="btn_Final_Donor_Information_Submit" runat="server" Enabled="false" 
                                OnClick="Final_Donor_Information_Submit_Click" Text="Submit" 
                                Width="272px" />
                        </td>
                        <td>
                            <asp:Label ID="l_Submitted_2" runat="server" ForeColor="Red" 
                                Style="font-family: Arial; font-size: small;" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <h2>Project Status (Region)</h2>
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
                    <asp:RadioButtonList ID="Project_Status" runat="server" 
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Text="ACTIVE" Value='0'></asp:ListItem>
                        <asp:ListItem Text="HOLD" Value='1'></asp:ListItem>
                        <asp:ListItem Text="KILLED" Value='2'></asp:ListItem>
                        <asp:ListItem Text="COMPLETE" Value='3'></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Project Status Date</td>
                <td>
                    <asp:Label ID="lbl_Project_Status_Date" runat="server"></asp:Label>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btn_Project_Status_Submit" runat="server" Enabled="false" 
                        OnClick="Project_Status_Submit_Click" Text="Submit" Width="272px" />
                </td>
                <td>
                    <asp:Label ID="l_Submitted_3" runat="server" ForeColor="Red" 
                        Style="font-family: Arial; font-size: small;" Text=""></asp:Label>
                </td>
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

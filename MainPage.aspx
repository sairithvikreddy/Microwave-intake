<%--<%@ page language="C#" autoeventwireup="true" inherits="MainPage, App_Web_04vnpcm2" %>--%>

<%@ Page Language="C#" AutoEventWireup="true" Inherits="MainPage" CodeFile="MainPage.aspx.cs" MasterPageFile="~/MasterPage.master"%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="content">   
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
        <p></p>
        <h2>Search Current Records</h2>
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
                    Requester
                </td>
                <td>
                    <asp:DropDownList ID="DDL_Requester" OnSelectedIndexChanged="OnSelectedIndexChanged_Changed" runat="server" AutoPostBack="true">
                        <asp:ListItem Selected="True">--Select--</asp:ListItem>
                       <%-- <asp:ListItem >CMG</asp:ListItem>--%>
                        <asp:ListItem >Market</asp:ListItem>
                        <asp:ListItem >National Transport</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    </td>
                <td>
                    <asp:Label ID="lbl_Status" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr id="trSite" runat="server">
                <td>
                    Recipient_SiteId
                </td>
                <td>
                    <asp:TextBox ID="txt_SiteID" runat="server" Width="135px" ></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Submit" runat="server" OnClick="Submit_Click" Text="Search" 
                        Width="160px" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr  id="trSite2" runat="server">
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="Button_CreateNewProject" runat="server" OnClick="Submit_Create" 
                        Text="Create New Project" Visible="False" Width="160px" />
                    <asp:Button ID="Button_View" runat="server" OnClick="Submit_View" Text="View" 
                        Visible="False" Width="160px" />
                </td>
            </tr>



        </table>
        <h2>Download Intake Summary</h2>
            <table>
            
            <tr>
                <td>
                    Region
                </td>
                <td>
                    <asp:DropDownList ID="ddlRegion" AutoPostBack="true"  OnSelectedIndexChanged="OnddlRegionSelectedIndex_Changed" runat="server">
                        <asp:ListItem Selected="True">--Select--</asp:ListItem>
                        <asp:ListItem >West</asp:ListItem>
                        <asp:ListItem >NorthEast</asp:ListItem>
                        <asp:ListItem >South</asp:ListItem>
                        <asp:ListItem >Central</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    Market
                </td>
                <td>
                    <asp:DropDownList ID="ddlMarket" DataMember="MarketMapping" runat="server">
                        <asp:ListItem Text="-- Select --"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnDownloadSummary" runat="server" OnClick="btnDownloadSummary_Click" Text="Download Intake Summary" 
                        Width="160px" />
                </td>
            </tr>
            
            </table>
        </ContentTemplate>
        <Triggers>
        <asp:PostBackTrigger ControlID="btnDownloadSummary" />
        </Triggers>
        </asp:UpdatePanel>


    </div>
</asp:Content>

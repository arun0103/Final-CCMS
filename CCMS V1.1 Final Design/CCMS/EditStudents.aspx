<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditStudents.aspx.cs" Inherits="CCMS.EditStudents" %>
<asp:Content ID="pageContent" ContentPlaceHolderID="pageContent" runat="server">

<h2>Upgrade Student Class</h2>
    <asp:Label ID="lblFrom" runat="server" Text=" From : "></asp:Label>
    <asp:DropDownList ID="ddlFrom" runat="server" AutoPostBack="True" 
        onselectedindexchanged="ddlFrom_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:RequiredFieldValidator ID="rvfClassFrom" runat="server" ErrorMessage="You Must Choose Class to Upgrade" ControlToValidate="ddlFrom" InitialValue="-1" ForeColor="Red"></asp:RequiredFieldValidator>
    <asp:Label ID="lblTo" runat="server" Text="  To : "></asp:Label>
    <asp:DropDownList ID="ddlTo" runat="server">
    </asp:DropDownList>
    <br />
    <asp:Button ID="btnUpgrade" runat="server" Text="Upgrade" 
        onclick="btnUpgrade_Click" />
    <br />
    <asp:Label ID="lblsuccssMsg" runat="server" Text="" Visible="false"></asp:Label>


</asp:Content>


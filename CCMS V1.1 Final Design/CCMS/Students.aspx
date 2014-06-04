<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Students.aspx.cs" Inherits="CCMS.Students" %>
<asp:Content ID="pageContent" ContentPlaceHolderID="pageContent" runat="server">

    <br />
    <asp:Panel ID="FileUploadPanel" runat="server">
        <asp:FileUpload ID="FileUploadStudent" runat="server" />
        <asp:Button ID="ButtonUpload" runat="server" onclick="ButtonUpload_Click" 
        Text="Upload" /><br /><br />
    </asp:Panel>

    <asp:Panel ID="FileInfoPanel" runat="server" Visible="False">
        <asp:Label ID="lblFileName" runat="server" Text="File Name: "/>
        <asp:Label ID="lblFileNameValue" runat="server" Text=""/><br />
        <asp:Label ID="lblSelectSheet" runat="server" Text="Select Sheet: " />
        <asp:DropDownList ID="ddlSheets" runat="server"
                            AppendDataBoundItems = "true">
        </asp:DropDownList> <br />
        <asp:Label ID="lblHDR" runat="server" Text="Does your Excel file has Header?" /> <br />
        <asp:RadioButtonList ID="RadioButtonListHDR" runat="server">
            <asp:ListItem Selected="True" Value="yes">Yes</asp:ListItem>
            <asp:ListItem Value="no">No</asp:ListItem>
        </asp:RadioButtonList> <br /><br />
        <asp:Button ID="btnShow" runat="server" Text="Show Data" OnClick="btnShow_Click" />
      
    </asp:Panel>
    
    <asp:Panel ID="GridViewPanel" runat="server">
        <asp:GridView ID="GridViewStudents" runat="server" CellPadding="4" 
        ForeColor="#333333" GridLines="None" AllowPaging="True" 
            OnPageIndexChanging="PageIndexChanging" PageSize="20" >
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerSettings Mode="NumericFirstLast" Position="TopAndBottom" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>

         <br />
        <asp:Button ID="btnSaveStudents" runat="server" onclick="SaveStudents_Click" 
            Text="Save in Database" Visible="False" />
        <br />
        <asp:Label ID="successMsg" runat="server" Visible="False"></asp:Label>

    </asp:Panel>
        <asp:GridView ID="tempGridView" runat="server" Visible="false">
        </asp:GridView>
</asp:Content>

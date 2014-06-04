<%@ Page Title="AddFacultyDetail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddFacultyDetail.aspx.cs" Inherits="CCMS.AddFacultyDetails" %>

<asp:Content runat="server" ID="pageContent" ContentPlaceHolderID="pageContent">
    <div class="main-content">
        <fieldset>
            <legend><b>Add Faculty</b></legend>
            <table class="add_faculty">
                <tr>
                    <td class="tdLegend">Email
                    </td>
                    <td>
                        <asp:DropDownList ID="EmailList" runat="server"  Width="200px" Font-Size="14px"
                            Height="24px" BorderColor="#3366ff" AutoPostBack="true" OnSelectedIndexChanged="EmailIndexChanged">
                            
                        </asp:DropDownList>
                        
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="emailValidator" runat="server" ControlToValidate="EmailList"
                ErrorMessage="Please Choose Email" InitialValue="---Select Email---"></asp:RequiredFieldValidator>

                    </td>
                    
                </tr>
                <tr>
                    <td class="tdLegend">First Name
                    </td>
                    <td>
                        <asp:TextBox ID="firstName" runat="server" Width="200px" Font-Size="14px"
                            Height="24px" BorderColor="#3366ff"></asp:TextBox>
                    </td>
                    
                </tr>
                <tr>
                    <td class="tdLegend">Last Name
                    </td>
                    <td>
                        <asp:TextBox ID="lastName" runat="server" Width="200px" Font-Size="14px"
                            Height="24px" BorderColor="#3366ff"></asp:TextBox>
                    </td>
                    
                </tr>
                

                <tr>
                    <td class="tdLegend">Contact
                    </td>
                    <td>
                        <asp:TextBox ID="contact" runat="server" Width="200px" Font-Size="14px"
                            Height="24px" BorderColor="#3366ff"></asp:TextBox>
                    </td>
                    <td class="tdValidator">
                        <asp:RegularExpressionValidator runat="server" id="NumberValidation" ForeColor="Red" controltovalidate="contact" validationexpression="9(7|8)\d\d\d\d\d\d\d\d" errormessage="Nepal's Cell Number are of 10 digit like 9841XXXXXX!" />
                    </td>
                </tr>
                <tr>
                    <td class="tdLegend">Active
                    </td>
                    <td>
                        <asp:CheckBox ID="activeCB" runat="server" />
                    </td>
                </tr>
                </table>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="addFacultyBtn" runat="server" Text="Add" Width="76px"                         
                            CssClass="allbutton" OnClick="addBtn_Click" />
                    </td>
                     <%--OnClientClick="return confirm('Are you sure, you want to ADD Faculty Data?')"--%>
                    <td>
                        <asp:Button ID="clearBtn" runat="server" Text="Clear" Width="76px"
                            CssClass="allbutton" OnClick="clearBtn_Click1" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="updateMsg" runat="server" Text="Record is added successfully" ForeColor="Green" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
</asp:Content>
<%--<asp:Content ID="ViewFaculty" ContentPlaceHolderID="pageContent2" runat="server" Visible="false">
    <div class="all-texts">
        <asp:GridView ID="FacultyGridView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="875px">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
    </div>
</asp:Content>--%>

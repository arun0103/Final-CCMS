<%@ Page Language="C#" Title="Edit User Details" AutoEventWireup="true" CodeBehind="EditUserDetails.aspx.cs" Inherits="DWIT_HRM_System.EditUserDetails" MasterPageFile="~/Site.Master" %>

<asp:Content ID="EditPage" runat="server" ContentPlaceHolderID="pageContent">
    <div class="main-content">
        <fieldset>
            <legend><b>Edit User Details</b></legend>
            <table style="font-size: 14px;">
                <tr>
                    <td class="row_break"></td>
                </tr>
                <tr>
                    <td>Enter User Email
                    </td>
                    <td>
                        <asp:TextBox ID="searchBox" runat="server" CssClass="box" BorderColor="#3366FF"
                            OnTextChanged="searchBox_TextChanged" Width="220px" Font-Size="14px"
                            Height="24px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="searchButton" runat="server" ImageAlign="AbsBottom"
                            ImageUrl="~/Images/search_button.png" OnClick="searchButton_Click" />
                    </td>
                </tr>
            </table>
            <div style="height: 30px">
                <asp:Label ID="notFoundMsg" runat="server" ForeColor="Red">Sorry!! No records found with such User Email, Type Correct User Email</asp:Label>
            </div>

            <asp:Panel ID="panelWrapper" runat="server" Visible="False">

                <div id="left-part-user-info" style="float: left; width: 300px;">
                    <table  style="font-size: 14px" class="all-texts">

                        <tr>
                            <td>User Email
                            </td>
                            <td>
                                <asp:Label ID="userEmailV" runat="server" Text="Label"></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td>First Name
                            </td>
                            <td>
                                <asp:TextBox ID="firstNameV" runat="server" Font-Size="14px" Height="24px" BorderColor="#3366FF"
                                    Width="150px" CssClass="box"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Last Name
                            </td>
                            <td>
                                <asp:TextBox ID="lastNameV" runat="server" Font-Size="14px" Height="24px" BorderColor="#3366FF"
                                    Width="150px" CssClass="box"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>Status
                            </td>
                            <td>
                                <asp:DropDownList ID="statusV" runat="server" Font-Size="14px" Height="24px" CssClass="box" Width="155px">
                                    <asp:ListItem Text="Active" Value="True"></asp:ListItem>
                                    <asp:ListItem Text="Inactive" Value="False"></asp:ListItem>
                                </asp:DropDownList>



                            </td>
                        </tr>
                        <tr>
                            <td>Role
                            </td>
                            <td>
                                <asp:DropDownList ID="roleV" runat="server" Font-Size="14px" Height="24px" CssClass="box" Width="155px">
                                    <asp:ListItem>--- Select ---</asp:ListItem>
                                    <asp:ListItem Text="User" Value="User"></asp:ListItem>
                                    <asp:ListItem Text="Admin" Value="Admin"></asp:ListItem>
                                    <asp:ListItem Text="Faculty" Value="Faculty"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="updateButton" runat="server" Text="Update"
                                    OnClick="updateButton_Click"
                                    CssClass="allbutton" Height="33px" Width="76px"
                                    OnClientClick="return confirm('Are you sure, you want to Update the Data?')"
                                    Font-Size="14px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="updateMsg" runat="server" Text="Record is updated successfully" ForeColor="Green" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </asp:Panel>
        </fieldset>
    </div>
</asp:Content>

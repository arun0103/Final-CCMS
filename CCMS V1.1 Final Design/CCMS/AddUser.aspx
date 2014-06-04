<%@ Page Language="C#" Title="Add User" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="MyCCMS.addUser" MasterPageFile="~/Site.Master" %>

<asp:Content ContentPlaceHolderID="pageContent" ID="cntAddUser" runat="server">
    <div class="main-content">
        <fieldset>
        <legend><b>Add User</b></legend>
        <table class="add_user_table">
            <tr>
                <td class="tdLegend">Email</td>

                <td class="tdInput">
                    <asp:TextBox ID="txtEmail" runat="server" BackColor="White"
                        BorderColor="#3366FF" MaxLength="50" Width="200px" Height="27px">
                    </asp:TextBox>
                </td>

                <td class="tdValidator">
                    <asp:RequiredFieldValidator ID="AU_email" runat="server" ErrorMessage="You must enter Email" ForeColor="Red" ControlToValidate="txtEmail" EnableClientScript="False"></asp:RequiredFieldValidator>
                </td>
            </tr>
            
            <tr>
                <td class="tdLegend">First Name </td>
                <td class="tdInput">
                    <asp:TextBox ID="txtFirstname" runat="server" BackColor="White"
                        BorderColor="#3366FF" MaxLength="50" Width="200px" Height="27px"></asp:TextBox>
                </td>
                <td class="tdValidator">
                    <asp:RequiredFieldValidator ID="AU_firstname" runat="server" ErrorMessage="You must enter First Name" ForeColor="Red" ControlToValidate="txtFirstname" EnableClientScript="False"></asp:RequiredFieldValidator>
                </td>
            </tr>
            
            <tr>
                <td class="tdLegend">Last Name</td>
                <td class="tdInput">
                    <asp:TextBox ID="txtLastname" runat="server" BackColor="White"
                        BorderColor="#3366FF" MaxLength="50" Width="200px" Height="27px"></asp:TextBox>
                </td>
                <td class="tdValidator">
                    <asp:RequiredFieldValidator ID="AU_lastname" runat="server" ErrorMessage="You must enter Last Name" ForeColor="Red" ControlToValidate="txtLastname" EnableClientScript="False"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="tdLegend">Contact
                </td>
                <td class="tdInput">
                    <asp:TextBox ID="txtContact" runat="server" Width="200px" Font-Size="14px"
                        Height="24px" BorderColor="#3366ff"></asp:TextBox>
                </td>
                <td class="tdValidator">
                    <asp:RequiredFieldValidator ID="AU_contact" runat="server" ErrorMessage="You must enter Contact No." ForeColor="Red" ControlToValidate="txtContact" EnableClientScript="False"></asp:RequiredFieldValidator>
                </td>
            </tr>
            
            <tr>
                <td class="tdLegend">Role</td>
                <td class="tdInput">
                    <asp:DropDownList ID="DropDownRole" runat="server" BorderColor="#3366FF" Width="100px">
                        <asp:ListItem>--- Select ---</asp:ListItem>
                        <asp:ListItem>Admin</asp:ListItem>
                        <asp:ListItem>User</asp:ListItem>
                        <asp:ListItem>Faculty</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="tdValidator">
                    <asp:RequiredFieldValidator ID="AU_role" runat="server" ErrorMessage="You must enter role " ForeColor="Red" ControlToValidate="DropDownRole" EnableClientScript="False" InitialValue="--- Select ---"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="tdLegend">Active</td>
                <td class="tdInput">
                    <asp:CheckBox ID="chkActive" runat="server" Text="" />
                </td>

            </tr>
            </table>
            <table>

            <tr>
                 <td>
                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="allbutton" Width="76px" OnClick="Button1_Click" />
                 </td>
                 <td>
                    <asp:Button ID="btnReset" runat="server" Text="Reset"  Width="76px" CssClass="allbutton" OnClick="btnReset_Click" />
                 </td>
                <td></td>
             </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="updateMsg" runat="server" Text="Record is added successfully" ForeColor="Green" Visible="false"></asp:Label>
                    </td>
                </tr>
        </table>
    </fieldset>
    </div>
</asp:Content>


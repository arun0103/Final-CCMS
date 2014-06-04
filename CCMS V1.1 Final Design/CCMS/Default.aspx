<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CCMS.Login" %>

<asp:Content runat="server" ID="pageContent" ContentPlaceHolderID="pageContent">
    <div class="all-texts">
        <table style="margin-left:30%">
            <tr>
                <td style="height: 280px">
                    <p class="pageName" style="font-family: 'Arial Rounded MT', helvetica, sans-serif; font-size: 30px">
                        Login
                    </p>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="labels">User Name</td>
                        </tr>
                        <tr>
                            <td class="style10">
                                <asp:TextBox ID="txtUserName" runat="server" BackColor="White"
                                    MaxLength="50" Width="200px" Height="27px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ErrorMessage="You must enter username....!!" ForeColor="Red" ControlToValidate="txtUserName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="labels">Password</td>
                        </tr>
                        <tr>
                            <td class="style10">
                                <asp:TextBox ID="txtPassword" runat="server" BackColor="White"
                                    MaxLength="50" Width="200px" TextMode="Password"
                                    Height="27px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ErrorMessage="You must enter Password....!!" ForeColor="Red" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnLogin" runat="server" CssClass="allbutton" align="left" Text="Sign In" Width="206px" OnClick="UserLogin" />
                    <asp:LinkButton ID="lnkForgotPassword" runat="server" Text="Forgot your password?" OnClick="forgotPasswordClick" Font-Size="12px" />

                </td>
            </tr>
        </table>
    </div>
</asp:Content>

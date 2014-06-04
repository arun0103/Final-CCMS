<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="CCMS.ForgotPassword" %>--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="CCMS.ForgotPassword" MasterPageFile="~/Site.Master" EnableEventValidation="false" %>

<asp:Content ID="EditPage" runat="server" ContentPlaceHolderID="pageContent">
    
        <fieldset>
            Email:<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            <asp:RegularExpressionValidator ID="revEmailId" runat="server"
                ErrorMessage="Please enter a valid Email Address"
                ControlToValidate="txtEmail" Display="Dynamic"
                ForeColor="Red" SetFocusOnError="True"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            <asp:Button ID="Button1" runat="server" Text="submit" OnClick="checkEmail" />
        </fieldset>
    <div style="margin:15px"><br />
    <asp:Label ID="lblText" runat="server"></asp:Label>
        </div>
</asp:Content>

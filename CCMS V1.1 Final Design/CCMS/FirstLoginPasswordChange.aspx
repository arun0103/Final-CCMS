<%@ Page Language="C#" Title="FirstLoginPasswordChange" AutoEventWireup="true" CodeBehind="FirstLoginPasswordChange.aspx.cs" Inherits="CCMS.FirstLoginPasswordChange" MasterPageFile ="~/Site.Master"%>


            <asp:Content runat="server" ID="pageContent" ContentPlaceHolderID="pageContent">
                <style>
                    .txtNotify
                    {
                        color:red;
                    }

                    .errorMsg
                    {
                        color:red;
                    }

                    table
                    {
                        height:100px;
                    }

                    

                </style>
                  <div class="main-content">
                <fieldset>
                      <p> purpose is to change the password in first login. From next login you will not be redirected here.</p>
                            <p> password must have at least 8 characters, at least 1 digit(s), at least 1 lower case letter(s), at least 1 upper case letter(s)
                            </p>
                       <table>  
                      <tr>
                        <td></td>
                        <td class="errorMsg"><asp:Label ID ="lblMessage" runat ="server" Visible ="false"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="txtNotify">
                                Original Password*:
                            </td>
                            <td>
                                <asp:TextBox ID="originalPassword" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="OriginalPasswordValidator" runat="server" ForeColor="Red" ErrorMessage="Please enter your original password." ControlToValidate="originalPassword"></asp:RequiredFieldValidator>
                            </td>

                        </tr>

                        <tr>
                            <td class="txtNotify">
                                New Password*:
                            </td>
                            <td>
                                <asp:TextBox ID="newPassword" runat="server" TextMode="Password" ></asp:TextBox>
                                
                                <asp:RegularExpressionValidator ID="regExpressionValidator" runat="server"  ForeColor="Red" ControlToValidate="newPassword" ValidationExpression="^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$" Display="Dynamic" ErrorMessage="Please follow above password guideline. "/>

                            </td>

                        </tr>

                        <tr>
                            <td class="txtNotify">
                                Confirm Password*:
                            </td>
                            <td>
                                <asp:TextBox ID="confirmedPassword" runat="server" TextMode="Password" ></asp:TextBox>    
                                
                                <asp:RegularExpressionValidator ID="regExpressionValidator2" runat="server" ForeColor="Red" ControlToValidate="confirmedPassword" ValidationExpression="^(?=.{8,})(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$" Display="Dynamic" ErrorMessage="Please follow above password guideline. "/>
                                
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="updatePassword" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td class="txtNotify">There are required field in form marked *</td>
                        </tr>
 
                    </table>
                
                <asp:Panel runat="server" ID="showPanel" Visible="false" >
                      Password has been changed.
                      <asp:Button ID="btnContinue" runat="server" Text="Continue" OnClick="passwordChange" />

                </asp:Panel>
                    </fieldset>
                    </div>
            
            </asp:Content>
        

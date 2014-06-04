<%@ Page Language="C#" Title="Edit Time Entry" AutoEventWireup="true" CodeBehind="EditTimeEntry.aspx.cs" Inherits="DWIT_HRM_System.EditTimeEntry" MasterPageFile="~/Site.Master" %>

<asp:Content ID="EditTimePage" runat="server" ContentPlaceHolderID="pageContent">
    <div class="main-content">
        <fieldset>
            <legend><b>Edit Time Entry</b></legend>
            <table style="font-size: 14px">
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
                    <td style="width:40%"></td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="notFoundMsg" runat="server" ForeColor="Red">Sorry!! No records found with such User Email. Please Type in the Correct User Email</asp:Label>
                    </td>
                </tr>
            </table>

            <asp:Panel ID="panelWrapper" runat="server" Visible="False">

                <div id="right-part-timesheet-info" style="float: left">
                    <div style="position: absolute; margin-left: 290px; margin-top: 35px;">
                    </div>
                    <table cellspacing="10px;" style="font-size: 14px; padding-left: 19px;">
                        <tr>
                            <td class="auto-style2">Record Date
                            </td>
                            <td>
                                <asp:TextBox ID="recordDateV" runat="server" BorderColor="#3366FF" Font-Size="14px" Height="24px" CssClass="box" Width="220px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:ImageButton ID="Calendar_Button" runat="server"
                                    ImageUrl="~/Images/button-calendar.gif" OnClick="Calendar_Button_Click" />
                            </td>
                            
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" OnSelectionChanged="Calendar1_SelectionChanged1" Visible="False" VisibleDate="2013-05-20" Width="220px">
                                    <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                    <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                    <OtherMonthDayStyle ForeColor="#999999" />
                                    <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                    <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                    <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                    <TodayDayStyle BackColor="#99CCCC" BorderColor="Black" ForeColor="White" />
                                    <WeekendDayStyle BackColor="#CCCCFF" />
                                </asp:Calendar>
                                format: mm/dd/yy
                            </td>
                            <td class="auto-style2"></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="height: 75px">Check-In Time
                            </td>
                            <td class="auto-style2" style="height: 75px">

                                <asp:DropDownList ID="ddlcinhour" runat="server" Font-Size="14px" Height="24px" CssClass="box" Width="55px">
                                    <asp:ListItem Text="hh" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp&nbsp
                            <asp:DropDownList ID="ddlcinminute" runat="server" Font-Size="14px" Height="24px" CssClass="box" Width="55px">
                                <asp:ListItem Text="mm" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                                &nbsp&nbsp
                            <asp:DropDownList ID="ddlcinperiod" runat="server" Font-Size="14px" Height="24px" CssClass="box" Width="95px">
                                <asp:ListItem Text="am/pm" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                            </asp:DropDownList>
                            </td>
                            <td style="height: 75px">
                                <asp:Label ID="checkInStatus" runat="server" ForeColor="Red">No records found</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>format: hour, minute, period
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Check-Out Time
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlcouthour" runat="server" Font-Size="14px" Height="24px" CssClass="box" Width="55px">
                                    <asp:ListItem Text="hh" Value="-1"></asp:ListItem>
                                </asp:DropDownList>
                                &nbsp&nbsp
                            <asp:DropDownList ID="ddlcoutminute" runat="server" Font-Size="14px" Height="24px" CssClass="box" Width="55px">
                                <asp:ListItem Text="mm" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                                &nbsp&nbsp
                            <asp:DropDownList ID="ddlcoutperiod" runat="server" Font-Size="14px" Height="24px" CssClass="box" Width="95px">
                                <asp:ListItem Text="am/pm" Value="-1"></asp:ListItem>
                                <asp:ListItem Text="AM" Value="AM"></asp:ListItem>
                                <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                            </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="checkOutStatus" runat="server" ForeColor="Red">No records found</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>format: hour, minute, period
                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="updateButton" runat="server" Text="Update"
                                    OnClick="updateButton_Click"
                                    OnClientClick="return confirm('Are you sure, you want to Update the Data?')"
                                    CssClass="allbutton" Font-Size="14px" Height="33px" Width="76px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="updateMsg" runat="server" Text="Record is updated successfully" ForeColor="Green" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <asp:Label ID="useridV" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="tempcin" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="tempcout" runat="server" Visible="false"></asp:Label>

            </asp:Panel>

        </fieldset>
    </div>
</asp:Content>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AttendanceReport.aspx.cs" Inherits="CCMS.AttendanceReport" MasterPageFile="~/Site.Master" EnableEventValidation="false" %>


<asp:Content ID="EditPage" runat="server" ContentPlaceHolderID="pageContent">

    <style>
        .CSSTableGenerator table {
            width: 70%;
            padding: 1px 13px 173px 10px;
        }

            .CSSTableGenerator table a:link {
                color: #666;
                font-weight: bold;
                text-decoration: none;
            }

            .CSSTableGenerator table a:visited {
                color: #999999;
                font-weight: bold;
                text-decoration: none;
            }

            .CSSTableGenerator table a:active,
            .CSSTableGenerator table a:hover {
                color: #bd5a35;
                text-decoration: underline;
            }

        .CSSTableGenerator table {
            position: relative;
            top: -164px;
            font-family: Arial, Helvetica, sans-serif;
            color: #666;
            font-size: 12px;
            text-shadow: 1px 1px 0px #fff;
            border-width: 0px 1px 1px 0px;
            text-align: left;
            padding: 7px;
            padding-top: 7px;
            padding-right: 7px;
            padding-bottom: 7px;
            padding-left: 7px;
            /* background: #eaebec; */
            margin: -10px;
            border: #ccc 1px solid;
            -moz-border-radius: 3px;
            -webkit-border-radius: 3px;
            border-radius: 3px;
            -moz-box-shadow: 0 1px 2px #d1d1d1;
            -webkit-box-shadow: 0 1px 2px #d1d1d1;
        }
            .CSSTableGenerator table th{
            padding: 21px 25px 22px 25px;
            border-top: 1px solid /*#fafafa*/;
            border-bottom: 1px solid #e0e0e0;
            background: #ededed;
            background: -webkit-gradient(linear, left top, left bottom, from(#ededed), to(#ebebeb));
            background: -moz-linear-gradient(top, #ededed, #ebebeb);
        }

        .CSSTableGenerator table th:first-child {
            text-align: left;
            padding-left: 20px;
        }

        .CSSTableGenerator table tr:first-child th:first-child {
            -moz-border-radius-topleft: 3px;
            -webkit-border-top-left-radius: 3px;
            border-top-left-radius: 3px;
        }

        .CSSTableGenerator table tr:first-child th:last-child {
            -moz-border-radius-topright: 3px;
            -webkit-border-top-right-radius: 3px;
            border-top-right-radius: 3px;
        }

        .CSSTableGenerator table tr {
            text-align: center;
            padding-left: 20px;
        }

        .CSSTableGenerator table td:first-child {
            text-align: left;
            padding-left: 20px;
            border-left: 0;
        }

        .CSSTableGenerator table td {
            padding: 18px;
            border-top: 1px solid #ffffff;
            border-bottom: 1px solid #e0e0e0;
            border-left: 1px solid #e0e0e0;
            background: #fafafa;
            background: -webkit-gradient(linear, left top, left bottom, from(#fbfbfb), to(#fafafa));
            background: -moz-linear-gradient(top, #fbfbfb, #fafafa);
        }

        .CSSTableGenerator table tr.even td {
            background: #f6f6f6;
            background: -webkit-gradient(linear, left top, left bottom, from(#f8f8f8), to(#f6f6f6));
            background: -moz-linear-gradient(top, #f8f8f8, #f6f6f6);
        }

        .CSSTableGenerator table tr:last-child td {
            border-bottom: 0;
        }

            .CSSTableGenerator table tr:last-child td:first-child {
                -moz-border-radius-bottomleft: 3px;
                -webkit-border-bottom-left-radius: 3px;
                border-bottom-left-radius: 3px;
            }

            .CSSTableGenerator table tr:last-child td:last-child {
                -moz-border-radius-bottomright: 3px;
                -webkit-border-bottom-right-radius: 3px;
                border-bottom-right-radius: 3px;
            }
        .auto-style9 {
            width: 199px;
            height: 35px;
        }
        .auto-style10 {
            width: 199px;
        }
        .arReportColumn {
            width: 200px;
            height: 35px;
        }
        .auto-style12 {
            width: 200px;
        }
        .attendanceReportTable {
            font-size: 14px;
        }
        .attendanceReportTable td {
            width:150px;
        }
    </style>

    <div class="main-content">
        <fieldset>
            <legend><b>Attendance Report</b></legend>
            <asp:Panel ID="panelWrapper" runat="server" Visible="True">

                <div>
                    <table> <%--style="font-size: 14px"--%>
                        <tr>
                            <td class="tdLegend">Faculty Name
                            </td>
                            <td class="tdInput">
                                <asp:DropDownList ID="facultyV" runat="server" Font-Size="14px" Height="24px" CssClass="box" BorderColor="#3366FF" Width="155px" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLegend">Student's Name
                            </td>
                            <td class="tdInput">
                                <asp:DropDownList ID="studentV" runat="server" Font-Size="14px" Height="24px" CssClass="box" BorderColor="#3366FF" Width="155px">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="tdLegend">Start Date
                            </td>
                            <td class="tdInput">
                                <asp:TextBox ID="startDate" runat="server" Width="150px" Font-Size="14px"
                                    Height="24px" BorderColor="#3366ff"></asp:TextBox>
                                &nbsp;&nbsp;
                            </td>
                            <td style="height: 35px">

                                <div style="position: absolute; margin-left: 50px;">
                                    <asp:Calendar ID="startDateCalendar" runat="server"
                                        BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1"
                                        DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                        ForeColor="#003399" Height="200px" VisibleDate="2013-05-20" Width="220px"
                                        OnSelectionChanged="startDateCalendar_SelectionChanged1"
                                        Visible="False">
                                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                        <OtherMonthDayStyle ForeColor="#999999" />
                                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px"
                                            Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                        <TodayDayStyle BackColor="#99CCCC" BorderColor="Black" ForeColor="White" />
                                        <WeekendDayStyle BackColor="#CCCCFF" />
                                    </asp:Calendar>
                                </div>


                                <asp:ImageButton ID="startDateCalendar_Button" runat="server"
                                    ImageUrl="~/Images/button-calendar.gif" OnClick="startDateCalendar_Button_Click" />
                            </td>
                            <td class="tdValidator">
                            <div class ="control-validator-box">
                                 <asp:RequiredFieldValidator ID="AR_startDate" runat="server" ErrorMessage="You must enter Start Date" 
                                    ForeColor="Red" ControlToValidate ="startDate" EnableClientScript="False" ></asp:RequiredFieldValidator>
                             </div>

                            </td>
                        </tr>
                        <tr>
                            <td class="tdLegend">End Date
                            </td>
                            <td class="tdInput">
                                <asp:TextBox ID="endDate" runat="server" Width="150px" Font-Size="14px"
                                    Height="24px" BorderColor="#3366ff"></asp:TextBox>
                                &nbsp;&nbsp;
                            </td>

                            <td class="inputColumn">
                                <div style="position: absolute; margin-left: 50px;">
                                    <asp:Calendar ID="endDateCalendar" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" OnSelectionChanged="endDateCalendar_SelectionChanged2" Visible="False" VisibleDate="2013-05-20" Width="220px">
                                        <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                                        <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                                        <OtherMonthDayStyle ForeColor="#999999" />
                                        <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                        <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                                        <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                                        <TodayDayStyle BackColor="#99CCCC" BorderColor="Black" ForeColor="White" />
                                        <WeekendDayStyle BackColor="#CCCCFF" />
                                    </asp:Calendar>
                                </div>


                                <asp:ImageButton ID="endDateCalendar_Button" runat="server" ImageUrl="~/Images/button-calendar.gif" OnClick="endDateCalendar_Button_Click" />
                            </td>
                            <td>
                            <div class ="control-validator-box">
                                 <asp:RequiredFieldValidator ID="AR_endDate" runat="server" ErrorMessage="You must enter End Date" 
                                    ForeColor="Red" ControlToValidate ="endDate" EnableClientScript="False" ></asp:RequiredFieldValidator>
                             </div>
                            </td>
                        </tr>
                        </table>
                    <table>
                        <tr>

                            <td>
                                <asp:Button ID="showReport" CssClass="allbutton" runat="server" BackColor="" Font-Size="14px" Height="33px" 
                                    OnClick="showReport_Click" Text="Show" Width="76px" />
                            </td>

                            <td class="auto-style12">
                                <asp:Button ID="clearBtn" CssClass="allbutton" runat="server" Text="Clear" Width="76px"
                                    Font-Size="14px" Height="34px" OnClick="clearBtn_Click" />
                            </td>
                        </tr>
                    </table>


                </div>
            </asp:Panel>
        </fieldset>

        <div class="CSSTableGenerator">

            <div>

                <asp:Label ID="lblText" runat="server" Visible="false" />
                <%--commented for not responding--%>
                <asp:Panel ID="panel1" runat="server" Visible="True" CssClass="panel">
                    <asp:GridView ID="reportGrid" runat="server"
                         CellPadding="4" ForeColor="#333333" AutoGenerateColumns="False" AllowPaging="True" PageSize="10" 
                        OnPageIndexChanging="PageIndexChanging" AllowSorting="true" OnSorting="GridView1_Sorting">
                        <%--//commented for remaining feature--%>
                         <%--OnSelectedIndexChanged="reportGrid_SelectedIndexChanged">--%>

                        <Columns>
                            <asp:BoundField DataField="Subject_Name" DataFormatString="{0}" HeaderText="Subject" SortExpression="Subject_Name" />
                            <asp:BoundField DataField="FacultyName" DataFormatString="{0}" HeaderText="Faculty" SortExpression="FacultyName" />
                            <asp:BoundField DataField="StudentName" DataFormatString="{0}" HeaderText="Student" SortExpression="StudentName" />
                            <asp:BoundField DataField="TotalClass" DataFormatString="{0}" HeaderText="Total Class" SortExpression="TotalClass" />
                            <asp:BoundField DataField="Present" DataFormatString="{0}" HeaderText="Total present" SortExpression="Present" />
                            <asp:BoundField DataField="Missedclass" DataFormatString="{0}" HeaderText="Total Missed Class" SortExpression="Missedclass" />
                            <asp:BoundField DataField="AttendancePercent" DataFormatString="{0}" HeaderText="Total Attendance %" SortExpression="AttendancePercent" />
                        </Columns>

                    </asp:GridView>
                </asp:Panel>
            </div>


            <br />
            <div>
                <asp:Button ID="btnExport" CssClass="allbutton" runat="server" Text="Export to Excel" OnClick="btnExport_Click" Style="height: 26px" Width="288px" Visible="false" />
                <asp:DropDownList ID="drpAttendees" runat="server" AutoPostBack="true" Visible=" false"></asp:DropDownList>
            </div>



        </div>

          
    </div>

</asp:Content>


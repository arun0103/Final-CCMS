<%@ Page Language="C#" Title="Attendance Entry" AutoEventWireup="true" CodeBehind="AttendanceEntry.aspx.cs" Inherits="CCMS.AttendanceEntry"  MasterPageFile ="~/Site.Master"%>

<asp:Content runat="server" ID="pageContent" ContentPlaceHolderID="pageContent">
    <style type="text/css">
    
        .clsTable
        {
            width:233%;
            height:100%;
            text-align:left;
            font-family:'Arial Rounded MT';
            font-size:large;
            padding:5px; 
            background-color:#e2e2e2;
           -moz-border-radius:20px;
            border-radius:20px
        }
    
      .body
      {
             
         background-image:url(spotlight.jpg);
         background-size: cover;
         background-repeat: no-repeat;
         text-decoration-color:white;
      }
      
      .layout1
      {
          padding:50px 0px 0px 50px;
      }

        .clsTd
        {
            float:right;
            height:30px;
        }

        .clsTd1
        {
            float:left;
            margin-left:18px;
        }

        .clsTd2
        {
            text-align:left;
            font-family:'Arial Rounded MT';
            font-size:large;
            margin-left:5px;
        }

        .panel
        {
            height:auto;
        }      

    </style>
    
    <div class="layout1">
        <asp:Panel ID="panelWrapper" runat="server" Visible="True" CssClass="panel">
            <div style="position:absolute; margin-left:290px; margin-top:35px;">

                <asp:Calendar ID="calendar" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" onselectionchanged="Calendar1_SelectionChanged1" Visible="False" Width="220px" >
                            <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                            <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
                            <OtherMonthDayStyle ForeColor="#999999" />
                            <SelectedDayStyle BackColor="#339966" Font-Bold="True" ForeColor="#CCFF99" />
                            <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                            <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
                            <TodayDayStyle BackColor="#99CCCC" BorderColor="Black" ForeColor="White" />
                            <WeekendDayStyle BackColor="#CCCCFF" />
                 </asp:Calendar>
            </div>
            <table>
                <tr>
                      <td class ="clsTd2">
                               Attendance Date : <asp:TextBox ID="recordDateV" runat="server" BorderColor="#3366FF" Font-Size="14px" Height="24px" CssClass="box" Width="220px"></asp:TextBox>
                      </td>
              
                      <td >
                           <asp:ImageButton ID="Calendar_Button" runat="server" 
                           ImageUrl="~/Images/button-calendar.gif" onclick="Calendar_Button_Click" />
                      </td>    
                 </tr>
            </table>
        </asp:Panel>
    </div>

    <table style= "padding:50px 0px 0px 50px;">
        <tr>
            <td class ="clsTd2" >Subject :</td>
            <td>
                <asp:Label ID="LblSubject" runat="server"></asp:Label>
            </td>
        </tr>
    </table>

    <div class="layout1">
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" 
        ForeColor="#333333" GridLines="None" style="margin-right: 0px; text-align:center;" 
        AutoGenerateColumns="False">
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

            <Columns>
                <asp:BoundField DataField="RollNo" DataFormatString="{0}" HeaderText="Roll No" />
                <asp:BoundField DataField="Name" DataFormatString="{0}" HeaderText="Name"/>
                <asp:TemplateField HeaderText="Attendance">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBoxAttendance" runat="server" checked="true" AutoPostBack="true"/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
        <asp:GridView ID="GridView2" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" style="margin-right: 0px; text-align:center" AutoGenerateColumns="False">
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

            <Columns>
                <asp:BoundField DataField="RollNo" DataFormatString="{0}" HeaderText="Roll No" />
                <asp:BoundField DataField="Name" DataFormatString="{0}" HeaderText="Name"/>
                <asp:TemplateField HeaderText="Attendance">
                    <ItemTemplate>
                        <asp:CheckBox ID="chkbox" runat="server" Checked='<%# Convert.ToBoolean(Eval("Attendance")) %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
    </div>

        <div style="padding:20px 0px 0px 50px;">
            <asp:Button ID="ButtonSave" runat="server" Text="Save" OnClick="saveAttendance" />
        </div>

</asp:Content>
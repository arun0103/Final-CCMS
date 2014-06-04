<%@ Page Language="C#" Title="Attendance" AutoEventWireup="true" CodeBehind="FacultyPage.aspx.cs" Inherits="CCMS.FacultyPage" MasterPageFile ="~/Site.Master"%>

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
            height:400px;
        }


        

    </style>

    <table class="clsTable">
        <tr>
            <td></td>
            <td class ="clsTd">
            <asp:Label ID="LblWelcome" runat="server"></asp:Label>
            </td >

        </tr>
        <tr>
            <td class ="clsTd1">
            <asp:Label ID="LblDate" runat="server" ></asp:Label> 
           </td>
            
        </tr>
        
    </table>

    <asp:Panel ID="panelWrapper" runat="server" Visible="True" CssClass="panel">
        <div style="position:absolute; margin-left:290px; margin-top:35px;">

            <asp:Calendar ID="calendar" runat="server" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" onselectionchanged="Faculty_Calendar_SelectionChanged" Visible="False" VisibleDate="2013-12-04" Width="220px">
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
        <table
            >
            <tr>
                  <td class ="clsTd2">
                           Attendance Date : <asp:TextBox ID="recordDateV" runat="server" BorderColor="#3366FF" Font-Size="14px" Height="24px" CssClass="box" Width="220px"  ></asp:TextBox>
                  </td>
              
                  <td >
                       <asp:ImageButton ID="Calendar_Button" runat="server" 
                       ImageUrl="~/Images/button-calendar.gif" onclick="Faculty_Calendar_Button_Click" />
                  </td>    
             </tr>
        </table>
        <asp:Label ID="Label1" runat="server">

        </asp:Label>
        <br />
            <%--<asp:LinkButton ID="lnksubject" runat="server" onclick="sublink"></asp:LinkButton>--%>
        <asp:hiddenfield id="ValueHiddenField" runat="server" Value="" Visible ="false"/>

            <asp:PlaceHolder ID="PlaceHolder1" runat="server" >
                
            </asp:PlaceHolder>
        <br />
        </asp:Panel>
        
     


    


</asp:Content>

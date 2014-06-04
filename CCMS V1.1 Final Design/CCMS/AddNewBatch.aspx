<%@ Page Title="Add Class" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddNewBatch.aspx.cs" Inherits="CCMS.NewClass" %>

<asp:Content runat="server" ID="pageContent" ContentPlaceHolderID="pageContent">
    <div class="main-content">
        <fieldset>
            <legend><b>Add Class</b></legend>
             <table>
                 <tr>
                     <td></td>
                     <td>
                         <div class ="control-validator-box">
                                <asp:RequiredFieldValidator ID="ANB_batch" runat="server" ErrorMessage="You must enter Batch" 
                            ForeColor="Red" ControlToValidate="batchName" EnableClientScript="False" ></asp:RequiredFieldValidator>
                            </div>
                     </td>
                     <td></td>
                     <td>
                          <div class ="control-validator-box">
                                 <asp:RequiredFieldValidator ID="ANB_semester" runat="server" ErrorMessage="You must enter Semester" 
                            ForeColor="Red" ControlToValidate="ddl_semester" EnableClientScript="False" ></asp:RequiredFieldValidator>
                             </div>
                     </td>
                 </tr>
                    <tr>
                        <td class="tdLegend">Batch
                        </td>
                        <td class="tdInput">
                            
                            <asp:TextBox ID="batchName" runat="server" Width="150px" Font-Size="14px" Text=""
                                Height="24px" BorderColor="#3366ff"></asp:TextBox>
                        </td>
                                 

                        <td class="tdLegend">Semester</td>
                        <td class="tdInput">
                            
                        <asp:DropDownList ID="ddl_semester" runat="server" Width="155px" BorderColor="#3366FF" Height="24px">
                            <asp:ListItem Value ="">--- Select Semester ---</asp:ListItem>
                            <asp:ListItem Value ="I">I</asp:ListItem>
                            <asp:ListItem Value ="II">II</asp:ListItem>
                            <asp:ListItem Value ="III">III</asp:ListItem>
                            <asp:ListItem Value ="IV">IV</asp:ListItem>
                            <asp:ListItem Value ="V">V</asp:ListItem>
                            <asp:ListItem Value ="VI">VI</asp:ListItem>
                            <asp:ListItem Value ="VII">VII</asp:ListItem>
                            <asp:ListItem Value ="VIII">VIII</asp:ListItem>
                        </asp:DropDownList>
                        </td>
                    
                    </tr>
                 <tr>
                     <td></td>
                     <td>
                        <%-- <div class ="control-validator-box">
                                <asp:RequiredFieldValidator ID="ANB_section" runat="server" ErrorMessage="You must enter Section" 
                                    ForeColor="Red" ControlToValidate="ddl_section" EnableClientScript="False" ></asp:RequiredFieldValidator>
                           </div>--%>
                     </td>
                     <td></td>
                     <td>

                     </td>
                 </tr>
                    <tr>
                        <td class="tdLegend">Section</td>
                        <td class="tdInput">
                            
                        <asp:DropDownList ID="ddl_section" runat="server" Width="155px" BorderColor="#3366FF" Height="24px" OnTextChanged="DropDownSection_TextChanged" >
                            <asp:ListItem Value ="">--- Select Section ---</asp:ListItem>
                            <asp:ListItem Value ="A">A</asp:ListItem>
                            <asp:ListItem Value ="B">B</asp:ListItem>
                        </asp:DropDownList>
                       </td>                   

                        <td class="tdLegend"></td>
                        <td class="tdInput"></td>
                    </tr>
                 <tr>
                     <td></td>
                     <td>
                        <div class ="control-validator-box">
                                 <asp:RequiredFieldValidator ID="ANB_startDate" runat="server" ErrorMessage="You must enter Start Date" 
                                    ForeColor="Red" ControlToValidate ="startDate" EnableClientScript="False" ></asp:RequiredFieldValidator>
                             </div>
                     </td>
                     <td></td>
                     <td>
                         <div class ="control-validator-box">
                                     <asp:RequiredFieldValidator ID="ANB_endDate" runat="server" ErrorMessage="You must enter End Date" ForeColor="Red" ControlToValidate ="endDate" EnableClientScript="False" ></asp:RequiredFieldValidator>
                                   </div>
                     </td>
                 </tr>
                    <tr>                  
                        <td class="tdLegend">Start Date
                        </td>
                        <td class="tdInput">
                             
                            <asp:TextBox ID="startDate" runat="server" Width="150px" Font-Size="14px"
                                Height="24px" BorderColor="#3366ff"></asp:TextBox>
                            
                        <asp:ImageButton ID="startDateCalendar_Button" runat="server"
                            ImageUrl="~/Images/button-calendar.gif" OnClick="startDateCalendar_Button_Click" Height="17px" />
                        </td>

                        <td class="tdLegend">End Date
                        </td>
                            <td class="tdInput">
                                 
                                <asp:TextBox ID="endDate" runat="server" Width="150px" Font-Size="14px"
                                    Height="24px" BorderColor="#3366ff"></asp:TextBox>
                                
                            <asp:ImageButton ID="endDateCalendar_Button" runat="server"
                                ImageUrl="~/Images/button-calendar.gif" OnClick="endDateCalendar_Button_Click" />
                            </td>
                        </tr>
                 <tr>
                     <td></td>
                     <td>
                         <div style="position:absolute;">
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
                     </td>
                     <td>
                         
                     </td>
                     <td>
                         <div style="position:absolute;">
                            <asp:Calendar ID="endDateCalendar" runat="server"
                                BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1"
                                DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt"
                                ForeColor="#003399" Height="200px" VisibleDate="2013-05-20" Width="220px"
                                OnSelectionChanged="endDateCalendar_SelectionChanged2"
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
                     </td>

                 </tr>
                    <tr>

                        <td class="tdLegend">Active
                        </td>
                        <td>
                            <asp:CheckBox ID="activeCB" runat="server" />
                        </td>
                    
                    </tr>
                    <tr>            
                        <td class="tdInput" colspan="2">
                            
                        </td>
                    
                        <td class="tdInput" colspan="2">
                            
                        </td>
                    </tr>
                 </table>
            <table>
                    <tr>
                        <td ></td>
                        <td>
                            <asp:Button ID="addBtn" runat="server" Text="Add" Width="76px" CssClass="allbutton"
                                Font-Size="14px" Height="34px" OnClick="addBtn_Click" ControlToValidate ="addBtn" />
                            </td>
                        <td>
                        <asp:Button ID="clearBtn" runat="server" Text="Clear" Width="76px" CssClass="allbutton"
                            Font-Size="14px" Height="34px" OnClick="clearBtn_Click" />
                        </td>
                        <td></td>
                    </tr>
             </table>
        </fieldset>
        </div>
</asp:Content>

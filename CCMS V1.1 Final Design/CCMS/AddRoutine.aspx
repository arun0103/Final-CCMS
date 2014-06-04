<%@ Page Title="AddRoutine" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddRoutine.aspx.cs" Inherits="CCMS.AddRoutine" %>

<asp:Content runat="server" ID="pageContent" ContentPlaceHolderID="pageContent">
    <div class="main-content">
    <fieldset>
        <legend><b>Add Routine</b></legend>
        
            <table style ="width:100%">
                <tr>
                    <td class="auto-style1">Faculty Name
                    </td>
                    <td class="inputColumn">
                        <asp:DropDownList ID="FacultyList" runat="server" Width="200px" Font-Size="14px"
                            Height="24px" BorderColor="#3366ff" AutoPostBack="true">
                            <asp:ListItem>--- Select Faculty ---</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdValidator float-left">
                        <asp:RequiredFieldValidator ID="RFV_FacultyList" runat="server" ErrorMessage="You must choose a faculty member " ForeColor="Red" ControlToValidate="FacultyList" EnableClientScript="False" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Class
                    </td>
                    <td class="inputColumn">
                        <asp:DropDownList ID="ClassList" runat="server" Width="200px" Font-Size="14px"
                            Height="24px" BorderColor="#3366ff" AutoPostBack="true" OnSelectedIndexChanged="ClassList_SelectedIndexChanged">
                            <asp:ListItem>--- Select Class ---</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdValidator float-left">
                        <asp:RequiredFieldValidator ID="RFV_ClassList" runat="server" ErrorMessage="You must choose a batch " ForeColor="Red" ControlToValidate="ClassList" EnableClientScript="False" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Year
                    </td>
                    <td class="inputColumn">
                        <asp:DropDownList ID="YearList" runat="server" Width="200px" Font-Size="14px"
                            Height="24px" BorderColor="#3366ff" AutoPostBack="true">
                            <asp:ListItem>--- Select Year ---</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdValidator float-left">
                        <asp:RequiredFieldValidator ID="RFV_YearList" runat="server" ErrorMessage="You must choose a year " ForeColor="Red" ControlToValidate="YearList" EnableClientScript="False" InitialValue="--- Select Year ---"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Semester
                    </td>
                    <td class="inputColumn">
                        <asp:DropDownList ID="Semester_drp" runat="server" Width="200px" Font-Size="14px"
                            Height="24px" BorderColor="#3366ff" AutoPostBack="true" OnSelectedIndexChanged="Semester_drp_SelectedIndexChanged">
                            <asp:ListItem>--- Select Semester ---</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdValidator float-left">
                        <asp:RequiredFieldValidator ID="RFV_Semester_drp" runat="server" ErrorMessage="You must choose a semester " ForeColor="Red" ControlToValidate="Semester_drp" EnableClientScript="False" InitialValue="0"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Section
                    </td>
                    <td class="inputColumn">
                        <asp:DropDownList ID="section_drp" runat="server" Width="200px" Font-Size="14px"
                            Height="24px" BorderColor="#3366ff" AutoPostBack="true">
                            
                            <asp:ListItem>A</asp:ListItem>
                            <asp:ListItem>B</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdValidator float-left">
                        <%--<asp:RequiredFieldValidator ID="RFV_section_drp" runat="server" ErrorMessage="You must choose a section " ForeColor="Red" ControlToValidate="section_drp" EnableClientScript="False" InitialValue="--- Select Section ---"></asp:RequiredFieldValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style1">Subject
                    </td>
                    <td class="inputColumn">
                        <asp:DropDownList ID="subjectlist_drp" runat="server" Width="200px" Font-Size="14px" Height="24px"
                            BorderColor="#3366ff" AutoPostBack="True">
                            <asp:ListItem>--- Select Subject ---</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="tdValidator float-left">
                        <asp:RequiredFieldValidator ID="RFV_subjectlist_drp" runat="server" ErrorMessage="You must choose a Subject " ForeColor="Red" ControlToValidate="subjectlist_drp" EnableClientScript="False" InitialValue="--- Select Subject ---"></asp:RequiredFieldValidator>
                    </td>
                </tr>

                <tr>
                    <td class="auto-style1"></td>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="RoutineAddBtn" runat="server" Text="Add" Width="76px"
                            CssClass="allbutton" OnClick="RoutineAddBtn_Click" />
                    </td>
                    <td>
                        <asp:Button ID="clearBtn1" runat="server" Text="Clear" Width="76px"
                            CssClass="allbutton" OnClick="clearBtn_Routine" />
                    </td>
                </tr>
            </table>
        
    </fieldset>

</div>
</asp:Content>

<asp:Content ID="FacultyRoutine" ContentPlaceHolderID="pageContent2" runat="server" Visible="true">
    <fieldset>
        <legend>List Of Classes</legend>
            <asp:GridView ID="RoutineGridView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" 
        DataKeyNames="routineId" OnRowDeleting="deleteRow">
        
        <Columns>

            <asp:BoundField HeaderText="ID" DataField="routineId" ReadOnly="true" />
            
            <asp:TemplateField HeaderText="Faculty Name">
                 <ItemTemplate>
                     <%# Eval("FacultyName") %>
                </ItemTemplate>

              
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Class">
                <ItemTemplate>
                    <%# Eval("ClassName") %>
                
                    <%# Eval("SectionName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Subject">
                <ItemTemplate>
                    <%# Eval("Subject_Name") %>
                </ItemTemplate>
            </asp:TemplateField>
            
            
           <asp:CommandField HeaderText="Delete" ButtonType="Link" ShowEditButton="false" ShowDeleteButton ="true" />

            
        </Columns>
         
        
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
    </asp:GridView>
    </fieldset>
</asp:Content>


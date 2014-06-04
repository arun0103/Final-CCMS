<%@ Page Language="C#" Title="Check In/Out" AutoEventWireup="true" CodeBehind="TimeEntry.aspx.cs" Inherits="CCMS.WebForm1" MasterPageFile="~/Site.Master" %>

<asp:Content runat="server" ID="pageContent" ContentPlaceHolderID="pageContent">

    <div class="main-content">
        <fieldset >
            <legend><b>Time Entry</b></legend>
            <table class="controlcolumn" style="text-align: left; margin-top: -14px; width: 478px;">
                <tr style="height: 100px">
                    <td style="width: 50px">&nbsp;</td>
                    <td style="width: 96px">
                        <br />
                        <br />
                    </td>
                    <td style="text-align: left;">
                        <br />
                        <asp:Label ID="LblWelcome" runat="server"></asp:Label>
                        <asp:Timer ID="TimerTime" runat="server" Interval="1000" OnTick="TimerTime_Tick">
                            <br />
                        </asp:Timer>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>

                                <asp:Label ID="LblDate" runat="server"></asp:Label>

                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                </asp:ScriptManager>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="TimerTime" EventName="Tick" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>

                </tr>
                <tr style="height: 100px">
                    <td class="allbutton" style="text-align: right;">
                        <asp:Button ID="btncheckin" runat="server" Text="Check In" BackColor="#1c4877" OnClick="checkIn_Click" ForeColor="White" Width="200px" Height="52px" />
                        <asp:Label ID="Lblcheckin" runat="server"></asp:Label>
                    </td>
                    <td style="width: 96px"></td>
                    <td class="allbutton" style="text-align: right;">
                        <asp:Button ID="btncheckout" runat="server" Text="Check Out" BackColor="#1c4877" OnClick="checkOut_Click" ForeColor="White" Enabled="false" Width="200px" Height="52px" />
                        <asp:Label ID="Lblcheckout" runat="server"></asp:Label>
                    </td>

                </tr>
                <tr>
                    <td style="height: 50px;"></td>
                    <td style="height: 50px; width: 96px;"></td>
                    <td style="height: 50px"></td>

                </tr>
            </table>
        </fieldset>
    </div>

</asp:Content>

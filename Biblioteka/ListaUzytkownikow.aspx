<%@ Page Title="Lista Użytkowników" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaUzytkownikow.aspx.cs" Inherits="Biblioteka.ListaUzytkownikow" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="IfAdmin" runat="server">
    <h2><%: Title %>.</h2>
<div style="text-align:center"> 
    <asp:Label ID="Label1" runat="server" Text="Aktywacja Użytkowników"  Font-Bold="true" Font-Size="XX-Large"></asp:Label>
</div><br />

<div style="text-align:center">
    <asp:Label  style="text-align:left" Width="200px" runat="server"  Text="Użytkownik" Font-Size="X-Large" Font-Bold="true"></asp:Label> 
    <asp:DropDownList ID="usersDropDown" DataSourceID="SqlDataSource1" DataTextField="UserName" DataValueField="UserName" runat="server" forecolor="Blue" Font-Bold="true" Font-Size="X-Large" AutoPostBack="True" OnSelectedIndexChanged="usersDropDown_SelectedIndexChanged"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [UserName] FROM [AspNetUsers] WHERE IsActivated=0"></asp:SqlDataSource>
</div>

<div style="text-align:center">
    <asp:Label style="text-align:left;" Width="200px" runat="server"  Text="Rola" Font-Size="X-Large" Font-Bold="true"></asp:Label> 
    <asp:DropDownList ID="RoleDropDown" runat="server" forecolor="Blue" Font-Bold="true" Font-Size="X-Large" AutoPostBack="True" OnSelectedIndexChanged="RoleDropDown_SelectedIndexChanged">
    <asp:ListItem Text="Pracownik" Value="Pracownik" />
    <asp:ListItem Text="Czytelnik" Value="Czytelnik" />
        </asp:DropDownList>
</div>

    <div style="text-align:center">
        <asp:Button ID="Button1" runat="server" Text="Aktywuj" OnClick="Button1_Click" />
        </div>

    <div style="border-bottom-style:solid"></div>

    <div style="text-align:center"> 
    <asp:Label ID="Label2" runat="server" Text="Usuwanie Użytkowników"  Font-Bold="true" Font-Size="XX-Large"></asp:Label>
</div><br />

<div style="text-align:center">
    <asp:Label  style="text-align:left" Width="200px" runat="server"  Text="Użytkownik" Font-Size="X-Large" Font-Bold="true"></asp:Label> 
    <asp:DropDownList ID="DeleteUserDropDownList" DataSourceID="SqlDataSource3" DataTextField="UserName" DataValueField="UserName" runat="server" forecolor="Blue" Font-Bold="true" Font-Size="X-Large" AutoPostBack="True" OnSelectedIndexChanged="DeleteUsersDropDown_SelectedIndexChanged"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [UserName] FROM [AspNetUsers] where Role !='Administrator'"></asp:SqlDataSource>
</div>
    <div style="text-align:center">
        <asp:Button ID="Button2" runat="server" Text="Usuń Konto" OnClick="Button2_Click" />
        </div>
        <asp:Panel ID="IfMaWypozyczenia" runat="server">
        <div style="text-align:center">
        <asp:Label ID="Label5" runat="server" Text="Nie możesz usunąć konta gdyż użytkownik ma wypożyczone książki" Font-Bold="true" Font-Size="X-Large"></asp:Label>
    </div>
            </asp:Panel>

    <div style="border-bottom-style:solid"></div>

    <div style="text-align:center"> 
    <asp:Label ID="Label3" runat="server" Text="Zatwierdzanie modyfikacji danych osobowych Użytkowników"  Font-Bold="true" Font-Size="XX-Large"></asp:Label>
</div><br />
    <div style="text-align:center">
    <asp:Label  style="text-align:left" Width="200px" runat="server"  Text="Użytkownik" Font-Size="X-Large" Font-Bold="true"></asp:Label> 
    <asp:DropDownList ID="ModyfikacjaDropDown" DataSourceID="SqlDataSource2" DataTextField="Email" DataValueField="Email" runat="server" forecolor="Blue" Font-Bold="true" Font-Size="X-Large" AutoPostBack="True" OnSelectedIndexChanged="ModyfikacjaDropDown_SelectedIndexChanged"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Email] FROM [ZmodyfikowaneDane]"></asp:SqlDataSource>
         <asp:GridView ID="GridView1" AutoGenerateColumns="False" runat="server" BackColor="White" 
            BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4" 
            GridLines="Horizontal" Height="168px" Width="452px">
            <FooterStyle BackColor="White" ForeColor="#333333" />
            <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="White" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F7F7F7" />
            <SortedAscendingHeaderStyle BackColor="#487575" />
            <SortedDescendingCellStyle BackColor="#E5E5E5" />
            <SortedDescendingHeaderStyle BackColor="#275353" />
             <Columns>
             <asp:BoundField DataField="Imie" HeaderText="Imie" />
             <asp:BoundField DataField="Nazwisko" HeaderText="Nazwisko" />
             <asp:BoundField DataField="Data_Urodzenia" HeaderText="Data_Urodzenia" />
                 </Columns>
        </asp:GridView>
</div>
    <div style="text-align:center">
        <asp:Button ID="Button3" runat="server" Text="Zatwierdź" OnClick="Button3_Click" />
        </div>
        </asp:Panel>
    <asp:Panel ID="IfNotAdmin" runat="server">
        <div style="text-align:center">
        <asp:Label ID="Label4" runat="server" Text="Nie jesteś Administratorem. Nie masz uprawnień by tu być." Font-Bold="true" Font-Size="X-Large"></asp:Label>
    </div>
            </asp:Panel>
</asp:Content>

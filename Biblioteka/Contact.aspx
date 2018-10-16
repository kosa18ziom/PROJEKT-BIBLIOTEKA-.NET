<%@ Page Title="Panel Administratora" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Biblioteka.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="IfAdmin" runat="server">
    <h2><%: Title %>.</h2>
   <asp:Label ID="UserList" runat="server" Text="Przejdź do listy użytkowników"></asp:Label>
    <asp:Button ID="Button1" runat="server" Text="Przejdź" OnClick="Button1_Click" />
        <div style="border-bottom-style:solid"></div>
                    <div style="text-align:center">
        <asp:Label ID="Label6" runat="server" Text="Aktualne wypożyczenia w bibliotece użytkownika" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                    </div>
        <asp:DropDownList ID="AktualneDropDown" DataSourceID="SqlDataSource4" DataTextField="UserName" DataValueField="UserName" runat="server" forecolor="Blue" Font-Bold="true" Font-Size="X-Large" AutoPostBack="True" OnSelectedIndexChanged="AktualneDropDown_SelectedIndexChanged"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [UserName] FROM [Wypozyczenie]"></asp:SqlDataSource>
        <asp:Button ID="Button5" runat="server" Text="Zobacz" OnClick="Button5_Click" />
                <div style="margin-left:auto; margin-right:auto; width:700px;">
        <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="GridView4_PageIndexChanging" OnSelectedIndexChanged="GridView4_SelectedIndexChanged"
    DataKeyNames="Id_Wypozyczenia" EmptyDataText = "Brak wypożyczeń w bibliotece" >
            <HeaderStyle BackColor="#4acabb" Font-Bold="True" ForeColor="Red" />            
                <RowStyle BackColor="#effc09" Font-Bold="True" ForeColor="Red" />
    <Columns>
        <asp:BoundField DataField="UserName" HeaderText="Użytkownik"/>
        <asp:BoundField DataField="Tytul" HeaderText="Tytuł"/>
        <asp:BoundField DataField="Autor" HeaderText="Autor"/>
        <asp:BoundField DataField="Data_Wydania" HeaderText="Data Wydania" DataFormatString="{0:d}"/>
        <asp:BoundField DataField="Wydawnictwo" HeaderText="Wydawnictwo"/>
        <asp:BoundField DataField="Data_Wypozyczenia" HeaderText="Data Wypożyczenia" DataFormatString="{0:d}" ReadOnly="True"/>
        <asp:BoundField DataField="ZwrotDo" HeaderText="Zwrot książki do" DataFormatString="{0:d}" ReadOnly="True"/>
    </Columns>

</asp:GridView>
                    </div>
        <div style="border-bottom-style:solid"></div>
                    <div style="text-align:center">
        <asp:Label ID="Label7" runat="server" Text="Historia wypożyczeń w bibliotece użytkownika" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                    </div>
                <asp:DropDownList ID="HistoriaDropDown" DataSourceID="SqlDataSource3" DataTextField="UserName" DataValueField="UserName" runat="server" forecolor="Blue" Font-Bold="true" Font-Size="X-Large" AutoPostBack="True" OnSelectedIndexChanged="HistoriaDropDown_SelectedIndexChanged"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [UserName] FROM [Historia_Wypozyczen]"></asp:SqlDataSource>
        <asp:Button ID="Button4" runat="server" Text="Zobacz" OnClick="Button4_Click" />
        <div style="margin-left:auto; margin-right:auto; width:500px;">
        <asp:GridView ID="GridView5" runat="server" OnPageIndexChanging="GridView5_PageIndexChanging" OnSelectedIndexChanged="GridView5_SelectedIndexChanged"
             AutoGenerateColumns="False" AllowPaging="true" PageSize="10"
    DataKeyNames="ID_Zwroconej" EmptyDataText = "Brak wypożyczeń w historii biblioteki" >
            <HeaderStyle BackColor="#4acabb" Font-Bold="True" ForeColor="Red" />            
                <RowStyle BackColor="#effc09" Font-Bold="True" ForeColor="Red" />
    <Columns>
        <asp:BoundField DataField="UserName" HeaderText="Użytkownik"/>
        <asp:BoundField DataField="Tytul" HeaderText="Tytuł"/>
        <asp:BoundField DataField="Autor" HeaderText="Autor"/>
        <asp:BoundField DataField="Data_Wydania" HeaderText="Data Wydania" DataFormatString="{0:d}"/>
        <asp:BoundField DataField="Wydawnictwo" HeaderText="Wydawnictwo"/>
        <asp:BoundField DataField="Data_Wypozyczenia" HeaderText="Data_Wypozyczenia"/>
        <asp:BoundField DataField="Data_Zwrotu" HeaderText="Data_Zwrotu"/>
        <asp:BoundField DataField="Kara" HeaderText="Kara w zł za przekroczenie okresu wypożyczenia"/>
    </Columns>

</asp:GridView>
                    </div>
        </asp:Panel>
    <asp:Panel ID="IfNotAdmin" runat="server">
        <div style="text-align:center">
        <asp:Label ID="Label1" runat="server" Text="Nie jesteś Administratorem. Nie masz uprawnień by tu być." Font-Bold="true" Font-Size="X-Large"></asp:Label>
    </div>
            </asp:Panel>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DodajPozycje.aspx.cs" Inherits="Biblioteka.DodajPozycje" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="IfPracownik" runat="server">
    <h2><%: Title %>.</h2>
    <div style="text-align:center">
    <asp:Label  style="text-align:left" Width="300px" runat="server"  Text="Tytuł" Font-Size="X-Large" Font-Bold="true"></asp:Label> 
        <asp:TextBox ID="Tytul" runat="server"
            ToolTip="Podaj tytuł książki">
        </asp:TextBox>
    </div>
    <div style="text-align:center">
    <asp:Label  style="text-align:left" Width="300px" runat="server"  Text="Autor" Font-Size="X-Large" Font-Bold="true"></asp:Label> 
        <asp:TextBox ID="Autor" runat="server"
            ToolTip="Podaj Autora książki">
        </asp:TextBox>
    </div>
    <div style="text-align:center">
    <asp:Label  style="text-align:left" Width="300px" runat="server"  Text="Data Wydania" Font-Size="X-Large" Font-Bold="true"></asp:Label> 
        <asp:TextBox ID="DataWydania" runat="server"
            ToolTip="Podaj datę wydania książki w formacie mm-dd-yyyy">
        </asp:TextBox>
        
        </div>
    <div style="text-align:center">
    <asp:Label  style="text-align:left" Width="300px" runat="server"  Text="Wydawnictwo" Font-Size="X-Large" Font-Bold="true"></asp:Label> 
        <asp:TextBox ID="Wydawnictwo" runat="server"
            ToolTip="Podaj nazwę wydawnictwa które wydało książkę">
        </asp:TextBox>
    </div>
    <div style="text-align:center">
    <asp:Label  style="text-align:left" Width="300px" runat="server"  Text="Liczba Egzemplarzy" Font-Size="X-Large" Font-Bold="true"></asp:Label> 
        <asp:TextBox ID="LiczbaEgzemplarzy" runat="server"
            ToolTip="Podaj liczbę egzemplarzy danej książki">
        </asp:TextBox>
        
        </div>
    <div style="text-align:center">
        <asp:Button ID="Button1" runat="server" Text="Dodaj" OnClick="Button1_Click" />
    </div>
        </asp:Panel>
    <asp:Panel ID="IfNotPracownik" runat="server">
        <div style="text-align:center">
        <asp:Label ID="Label1" runat="server" Text="Nie jesteś Pracownikiem. Nie masz uprawnień by tu być." Font-Bold="true" Font-Size="X-Large"></asp:Label>
    </div>
        </asp:Panel>
                   <asp:RequiredFieldValidator ErrorMessage="*Tytuł nie został podany" ForeColor="Red" Font-Size="Large" Font-Bold="true" ControlToValidate="Tytul" runat="server" /><br />
                    <asp:RequiredFieldValidator ErrorMessage="*Autor nie został podany" ForeColor="Red" Font-Size="Large" Font-Bold="true" ControlToValidate="Autor" runat="server" /><br />
    <div><asp:RequiredFieldValidator ErrorMessage="*Data Wydania nie została wpisana!" ForeColor="Red" Font-Size="Large" Font-Bold="true"  ControlToValidate="DataWydania" runat="server" /><br />
        <asp:RegularExpressionValidator runat="server"
            ControlToValidate="DataWydania" 
            ErrorMessage="*Niepoprawny format daty wydania, poprawny format to na przykład: 01-25-1990"  ForeColor="Red" Font-Size="Large" Font-Bold="true"
            ValidationExpression="^((0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])[\/\-]\d{4})" />
    </div>
            <asp:RequiredFieldValidator ErrorMessage="*Nazwa Wydawnicta nie została podana!" ForeColor="Red" Font-Size="Large" Font-Bold="true" ControlToValidate="Wydawnictwo" runat="server" /><br />
    <div><asp:RequiredFieldValidator ErrorMessage="*Liczba egzemplarzy nie została wpisana!" ForeColor="Red" Font-Size="Large" Font-Bold="true" ControlToValidate="LiczbaEgzemplarzy" runat="server" /><br />
           <asp:RegularExpressionValidator runat="server"
            ControlToValidate="LiczbaEgzemplarzy" 
            ErrorMessage="*Wpisz Liczbę" Font-Size="Large" Font-Bold="true"  ForeColor="Red"
            ValidationExpression="^[0-9]+$" />
    </div>
</asp:Content>

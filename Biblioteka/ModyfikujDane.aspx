<%@ Page Title="Modyfikuj Dane Osobowe" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModyfikujDane.aspx.cs" Inherits="Biblioteka.ModyfikujDane" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <h2><%: Title %>.</h2>
    <asp:Panel ID="Panel1" runat="server">
    <div style="text-align:center">
    <asp:Label  style="text-align:left" Width="200px" runat="server"  Text="Imię" Font-Size="X-Large" Font-Bold="true"></asp:Label> 
        <asp:TextBox ID="Imie" runat="server"
            ToolTip="Wpisz swoje Imię z wielkiej litery np.Konrad">
        </asp:TextBox>
    </div>
    <div style="text-align:center">
    <asp:Label  style="text-align:left" Width="200px" runat="server"  Text="Nazwisko" Font-Size="X-Large" Font-Bold="true"></asp:Label> 
        <asp:TextBox ID="Nazwisko" runat="server"
            ToolTip="Wpisz swoje nazwisko z wielkiej litery np.Kosiński">
        </asp:TextBox>
    </div>
    <div style="text-align:center">
    <asp:Label  style="text-align:left" Width="200px" runat="server"  Text="Data Urodzenia" Font-Size="X-Large" Font-Bold="true"></asp:Label> 
        <asp:TextBox ID="DataUrodzenia" runat="server"
            ToolTip="Wpisz swoją Datę Urodzenia w formacie mm-dd-yyyy">
        </asp:TextBox>
    </div>
    <div style="text-align:center"><asp:Button ID="Button1" runat="server" Text="Zapisz" OnClick="Button1_Click" /></div>

    <div><asp:RequiredFieldValidator ErrorMessage="*Imie nie zostało wpisane!" ForeColor="Red" Font-Size="Large" Font-Bold="true" ControlToValidate="Imie" runat="server" /><br />
           <asp:RegularExpressionValidator runat="server"
            ControlToValidate="Imie" 
            ErrorMessage="*Niepoprawny format Imienia,poprawne formaty to na przykład: Jan lub Jan Marian w przypadku dwóch imion" Font-Size="Large" Font-Bold="true"  ForeColor="Red"
            ValidationExpression="^([A-ZĘÓĄŚŁŻŹĆŃ][a-zęóąśłżźćń]+)( [A-ZĘÓĄŚŁŻŹĆŃ][a-zęóąśłżźćń]+){0,1}$" />
       </div>
    <div><asp:RequiredFieldValidator ErrorMessage="*Nazwisko nie zostało wpisane!" ForeColor="Red" Font-Size="Large" Font-Bold="true" ControlToValidate="Nazwisko" runat="server" /><br />
           <asp:RegularExpressionValidator runat="server"
            ControlToValidate="Nazwisko" 
            ErrorMessage="*Niepoprawny format Nazwiska, poprawne format to nazwisko pisane z dużej litery" Font-Size="Large" Font-Bold="true"  ForeColor="Red"
            ValidationExpression="^([A-ZĘÓĄŚŁŻŹĆŃ-][a-zęóąśłżźćń-]+)([A-ZĘÓĄŚŁŻŹĆŃ][a-zęóąśłżźćń]+){0,1}$" />
       </div>
    <div><asp:RequiredFieldValidator ErrorMessage="*Data Urodzenia nie została wpisana!" ForeColor="Red" Font-Size="Large" Font-Bold="true"  ControlToValidate="DataUrodzenia" runat="server" /><br />
        <asp:RegularExpressionValidator runat="server"
            ControlToValidate="DataUrodzenia" 
            ErrorMessage="*Niepoprawny format daty urodzenia, poprawny format to na przykład: 10-02-1990"  ForeColor="Red" Font-Size="Large" Font-Bold="true"
            ValidationExpression="^((0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])[\/\-]\d{4})" />
      </div>
        </asp:Panel>
    <asp:Panel ID="Panel3" runat="server">
        <asp:Label ID="Label1" runat="server" Text="Poczekaj na zatwierdzenie zmian przez Admina"  Font-Bold="true" Font-Size="XX-Large"></asp:Label>
    </asp:Panel>

</asp:Content>

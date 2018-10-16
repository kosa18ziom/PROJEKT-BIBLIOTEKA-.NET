<%@ Page Title="Wypożycz/Zwróć Książkę" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WypozyczZwroc.aspx.cs" Inherits="Biblioteka.WypozyczZwroc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
<div style="text-align:center"> 
    <asp:Label ID="Label1" runat="server" Text="Wypożycz Książkę"  Font-Bold="true" Font-Size="XX-Large"></asp:Label>
</div><br />

<div style="text-align:center">
    <asp:Label  style="text-align:left" Width="200px" runat="server"  Text="Książka" Font-Size="X-Large" Font-Bold="true"></asp:Label> 
    <asp:DropDownList ID="KsiazkaDropDown" DataSourceID="SqlDataSource1" DataTextField="Tytul" DataValueField="Tytul" runat="server" forecolor="Blue" Font-Bold="true" Font-Size="X-Large" AutoPostBack="True" OnSelectedIndexChanged="KsiazkaDropDown_SelectedIndexChanged"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Tytul] FROM [Ksiazka] WHERE Liczba_Egzemplarzy>0"></asp:SqlDataSource>
    <asp:Button ID="Button1" runat="server" Text="Wypożycz" OnClick="Button1_Click" />
                            

</div>
    <div style="border-bottom-style:solid"></div>
    <div style="text-align:center"> 
    <asp:Label ID="Label2" runat="server" Text="Rezerwuj Książkę"  Font-Bold="true" Font-Size="XX-Large"></asp:Label>
</div><br />

<div style="text-align:center">
    <asp:Label  style="text-align:left" Width="200px" runat="server"  Text="Książka" Font-Size="X-Large" Font-Bold="true"></asp:Label> 
    <asp:DropDownList ID="RezerwujDropDown" DataSourceID="SqlDataSource2" DataTextField="Tytul" DataValueField="Tytul" runat="server" forecolor="Blue" Font-Bold="true" Font-Size="X-Large" AutoPostBack="True" OnSelectedIndexChanged="RezerwujDropDown_SelectedIndexChanged"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [Tytul] FROM [Ksiazka] WHERE Liczba_Egzemplarzy=0"></asp:SqlDataSource>
    <asp:Button ID="Button2" runat="server" Text="Rezerwuj" OnClick="Button2_Click" />
                            

</div>
    <div style="border-bottom-style:solid"></div>
    <div style="text-align:center"> 
    <asp:Label ID="Label3" runat="server" Text="Anuluj Rezerwację Książki"  Font-Bold="true" Font-Size="XX-Large"></asp:Label>
</div><br />

<div style="text-align:center">
    <asp:Label  style="text-align:left" Width="200px" runat="server"  Text="Książka" Font-Size="X-Large" Font-Bold="true"></asp:Label> 
    <asp:DropDownList ID="AnulujRezerwacjeDropDown" DataSourceID="SqlDataSource3" DataTextField="Tytul" DataValueField="Tytul" runat="server" forecolor="Blue" Font-Bold="true" Font-Size="X-Large" AutoPostBack="True" OnSelectedIndexChanged="AnulujRezerwacjeDropDown_SelectedIndexChanged"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" ></asp:SqlDataSource>
    <asp:Button ID="Button3" runat="server" Text="Anuluj" OnClick="Button3_Click" />
                            

</div>
</asp:Content>

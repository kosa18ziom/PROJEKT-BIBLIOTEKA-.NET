<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Biblioteka._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div style="text-align:center">
        <asp:Label ID="tytul" runat="server" Text="BIBLIOTEKA"  Font-Bold="true" Font-Size="XX-Large"></asp:Label>
        <table style="text-align: center; background-color:antiquewhite" class="table table-bordered">
            <tr>
              <td><asp:Label ID="Naglowek" runat="server" Text="Wyszukiwarka książek" Font-Bold="true" Font-Size="X-Large"></asp:Label></td>
            <td><asp:Label ID="Naglowek2" runat="server" Text="Wpisz autora lub tytuł książki i kliknij szukaj" Font-Bold="true" Font-Size="X-Large"></asp:Label></td>
            </tr>
            <tr>
        <td><asp:Label ID="Autor" runat="server" Text="Wyszukaj wg Autora" Font-Bold="true"></asp:Label></td>
            <td><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox> <asp:Button ID="Button1" runat="server" Text="Szukaj" OnClick="Button1_Click" /></td> 
        </tr>
            <tr>
        <td><asp:Label ID="Tytuł" runat="server" Text="Wyszukaj wg Tytułu" Font-Bold="true"></asp:Label></td>
            <td><asp:TextBox ID="TextBox2" runat="server"></asp:TextBox> <asp:Button ID="Button2" runat="server" Text="Szukaj" OnClick="Button2_Click" /></td> 
        </tr>
        </table>
    </div>
                    <div style="margin-left:auto; margin-right:auto; width:500px;">
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" EmptyDataText = "Brak Książek" AllowPaging="true"
     DataKeyNames="ID_Ksiazki">
                <HeaderStyle BackColor="#4acabb" Font-Bold="True" ForeColor="Red" />            
                <RowStyle BackColor="#effc09" Font-Bold="True" ForeColor="Red" />
                <Columns>
                    <asp:BoundField DataField="Tytul" HeaderText="Tytuł" ReadOnly="True"/>
                    <asp:BoundField DataField="Autor" HeaderText="Autor" ReadOnly="True"/>
                    <asp:BoundField DataField="Data_Wydania" HeaderText="Data Wydania" DataFormatString="{0:d}" HtmlEncode=false ReadOnly="True"/>
                    <asp:BoundField DataField="Wydawnictwo" HeaderText="Wydawnictwo" ReadOnly="True"/>
                    <asp:BoundField DataField="Liczba_Egzemplarzy" HeaderText="Liczba Egzemplarzy" ReadOnly="True" />
                    </Columns>
            </asp:GridView>
                        </div>
    <div style="border-bottom-style:solid"></div>
    <div style="text-align:center"> 
    <asp:Label ID="Label4" runat="server" Text="Twoje Dane Osobowe"  Font-Bold="true" Font-Size="XX-Large"></asp:Label>
</div>
                        <div style="margin-left:auto; margin-right:auto; width:400px;">
    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="false" AllowPaging="true"
     DataKeyNames="Email" >
                <HeaderStyle BackColor="#4acabb" Font-Bold="True" ForeColor="Red" />            
                <RowStyle BackColor="#effc09" Font-Bold="True" ForeColor="Red" />
                <Columns>
                    <asp:BoundField DataField="Email" HeaderText="Email" ReadOnly="True"/>
                    <asp:BoundField DataField="Imie" HeaderText="Imie" ReadOnly="True"/>
                    <asp:BoundField DataField="Nazwisko" HeaderText="Nazwisko" ReadOnly="True"/>
                    <asp:BoundField DataField="Data_Urodzenia" HeaderText="Data Urodzenia" DataFormatString="{0:d}" HtmlEncode=false ReadOnly="True"/>
                    </Columns>
            </asp:GridView>
                            </div>
                        <div style="border-bottom-style:solid"></div>
    <div style="text-align:center"> 
    <asp:Label ID="Label2" runat="server" Text="Aktualne Wypożyczenia"  Font-Bold="true" Font-Size="XX-Large"></asp:Label>
</div>
                        <div style="margin-left:auto; margin-right:auto; width:800px;">
    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" EmptyDataText = "Brak Książek wypożyczonych przez ciebie" AllowPaging="true"
     DataKeyNames="ID_Wypozyczenia" OnPageIndexChanging="GridView2_PageIndexChanging" PageSize="10" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                <HeaderStyle BackColor="#4acabb" Font-Bold="True" ForeColor="Red" />            
                <RowStyle BackColor="#effc09" Font-Bold="True" ForeColor="Red" />
                <Columns>
                    <asp:BoundField DataField="ID_Wypozyczenia" HeaderText="ID" ReadOnly="True"/>
                    <asp:BoundField DataField="Tytul" HeaderText="Tytuł" ReadOnly="True"/>
                    <asp:BoundField DataField="Autor" HeaderText="Autor" ReadOnly="True"/>
                    <asp:BoundField DataField="Data_Wydania" HeaderText="Data Wydania" DataFormatString="{0:d}" HtmlEncode=false ReadOnly="True"/>
                    <asp:BoundField DataField="Wydawnictwo" HeaderText="Wydawnictwo" ReadOnly="True"/>
                    <asp:BoundField DataField="Data_Wypozyczenia" HeaderText="Data Wypożyczenia" DataFormatString="{0:d}" ReadOnly="True"/>
                    <asp:BoundField DataField="ZwrotDo" HeaderText="Zwrot książki do" DataFormatString="{0:d}" ReadOnly="True"/>
                    </Columns>
            </asp:GridView>
                            </div>
    <asp:DropDownList ID="ZwrocDropDown" DataSourceID="SqlDataSource1" DataTextField="ID_Wypozyczenia" DataValueField="ID_Wypozyczenia" runat="server" forecolor="Blue" Font-Bold="true" Font-Size="X-Large" AutoPostBack="True" OnSelectedIndexChanged="ZwrocDropDown_SelectedIndexChanged"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>"></asp:SqlDataSource>
    <asp:Button ID="Button3" runat="server" Text="Zwróć książkę" OnClick="Button3_Click" />
                            <asp:Panel ID="Panel1" runat="server">
                            <div style="text-align:center"> 
    <asp:Label ID="Label1" runat="server" Text="Biblioteka jest w trakcie zatwierdzenia jednego z twoich zwrotów"  Font-Bold="true" Font-Size="XX-Large"></asp:Label>
</div>
                            </asp:Panel>
   
     <div style="border-bottom-style:solid"></div>
    <div style="text-align:center"> 
    <asp:Label ID="Label3" runat="server" Text="Twoja Historia Wypożyczeń"  Font-Bold="true" Font-Size="XX-Large"></asp:Label>
</div>
                        <div style="margin-left:auto; margin-right:auto; width:500px;">
    <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" EmptyDataText = "Brak Książek zwróconych przez ciebie" AllowPaging="true"
     DataKeyNames="ID_Zwroconej" OnPageIndexChanging="GridView3_PageIndexChanging" PageSize="10" OnSelectedIndexChanged="GridView3_SelectedIndexChanged">
                <HeaderStyle BackColor="#4acabb" Font-Bold="True" ForeColor="Red" />            
                <RowStyle BackColor="#effc09" Font-Bold="True" ForeColor="Red" />
                <Columns>
                    <asp:BoundField DataField="Tytul" HeaderText="Tytuł" ReadOnly="True"/>
                    <asp:BoundField DataField="Autor" HeaderText="Autor" ReadOnly="True"/>
                    <asp:BoundField DataField="Data_Wydania" HeaderText="Data Wydania" DataFormatString="{0:d}" HtmlEncode=false ReadOnly="True"/>
                    <asp:BoundField DataField="Wydawnictwo" HeaderText="Wydawnictwo" ReadOnly="True"/>
                    <asp:BoundField DataField="Data_Wypozyczenia" HeaderText="Data_Wypozyczenia"/>
                    <asp:BoundField DataField="Data_Zwrotu" HeaderText="Data_Zwrotu"/>
                    <asp:BoundField DataField="Kara" HeaderText="Kara w zł za przekroczenie okresu wypożyczenia"/>

                    </Columns>
            </asp:GridView>
                 </div>           
</asp:Content>

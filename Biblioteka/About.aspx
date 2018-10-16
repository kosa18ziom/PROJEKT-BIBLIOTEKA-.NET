<%@ Page Title="Panel Pracownika" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="Biblioteka.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %>.</h2>
    <asp:Panel ID="IfNotPracownik" runat="server">
        <div style="text-align:center">
        <asp:Label ID="Label1" runat="server" Text="Nie jesteś Pracownikiem. Nie masz uprawnień by tu być." Font-Bold="true" Font-Size="X-Large"></asp:Label>
    </div>
            </asp:Panel>
    <asp:Panel ID="IfPracownik" runat="server">
        <div style="text-align:center">
        <asp:Label ID="Label2" runat="server" Text="Dodaj nową pozycję" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                    <div style="text-align:center">
                        <asp:Button ID="Button1" runat="server" Text="Dodaj" OnClick="Button1_Click" CausesValidation=false/>
                        </div>
    </div>
        <div style="text-align:center">
        <asp:Label ID="Label3" runat="server" Text="Lista Książek dostępnych w bibliotece" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                    </div>
        <div style="margin-left:auto; margin-right:auto; width:800px;">
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" EmptyDataText = "Brak Książek" OnRowCommand="GridView1_RowCommand" OnRowDeleting="GridView1_RowDeleting" AllowPaging="true"
    OnPageIndexChanging="OnPageIndexChanging" DataKeyNames="ID_Ksiazki" OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating" >
                <HeaderStyle BackColor="#4acabb" Font-Bold="True" ForeColor="Red" />            
                <RowStyle BackColor="#effc09" Font-Bold="True" ForeColor="Red" />
                <Columns>
                    <asp:BoundField DataField="ID_Ksiazki" HeaderText="ID_Ksiazki" ReadOnly="True"/>
                    <asp:BoundField DataField="Tytul" HeaderText="Tytuł" ReadOnly="True"/>
                    <asp:BoundField DataField="Autor" HeaderText="Autor" ReadOnly="True"/>
                    <asp:BoundField DataField="Data_Wydania" HeaderText="Data Wydania" DataFormatString="{0:d}" HtmlEncode=false ReadOnly="True"/>
                    <asp:BoundField DataField="Wydawnictwo" HeaderText="Wydawnictwo" ReadOnly="True"/>
                    <asp:TemplateField HeaderText="Liczba Egzemplarzy">
          <EditItemTemplate>
            <asp:TextBox ID="textBox1" Width="100%" Height="100%" runat="server" Text='<%# Bind("Liczba_Egzemplarzy") %>'>
</asp:TextBox>
          </EditItemTemplate>

          <ItemTemplate>
            <asp:Label ID="wszystkie" runat="server" Text='<%# Bind("Liczba_Egzemplarzy") %>' Font-Size="Medium"></asp:Label>
          </ItemTemplate>
     </asp:TemplateField>
                    <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
       <asp:LinkButton ID="LinkButton1" 
         CommandArgument='<%# Eval("ID_Ksiazki") %>' 
         CommandName="Delete" runat="server"
           CausesValidation=false>
         Delete</asp:LinkButton>
     </ItemTemplate> 
   </asp:TemplateField>
        <asp:TemplateField HeaderText="Select">
        <ItemTemplate>
         <asp:LinkButton ID="LkB1"  runat="server" CausesValidation=false CommandName="Edit">Edit</asp:LinkButton>
        </ItemTemplate>
        <EditItemTemplate>
         <asp:LinkButton ID="LB2" runat="server" CommandName="Update" CausesValidation=false>Update</asp:LinkButton>
         <asp:LinkButton ID="LB3" runat="server" CommandName="Cancel" CausesValidation=false>Cancel</asp:LinkButton>
        </EditItemTemplate>
    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <asp:Panel ID="IfWypozyczone" runat="server">
        <div style="text-align:center">
        <asp:Label ID="Label8" runat="server" Text="Nie możesz usunąć ksiazki gdyż są wypożyczone jej egzemplarze" Font-Bold="true" Font-Size="X-Large"></asp:Label>
    </div>
            </asp:Panel>
            <div style="border-bottom-style:solid"></div>
        <div style="text-align:center">
        <asp:Label ID="Label4" runat="server" Text="Wypożyczenia do zatwierdzenia" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                    </div>

                <div style="margin-left:auto; margin-right:auto; width:800px;">
        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="Id_DoZatwierdzenia" EmptyDataText = "Brak wypożyczeń do zatwierdzenia w bibliotece" >
            <HeaderStyle BackColor="#4acabb" Font-Bold="True" ForeColor="Red" />            
                <RowStyle BackColor="#effc09" Font-Bold="True" ForeColor="Red" />
    <Columns>
        <asp:BoundField DataField="ID_DoZatwierdzenia" HeaderText="ID" ReadOnly="True"/>
        <asp:BoundField DataField="UserName" HeaderText="Użytkownik"/>
        <asp:BoundField DataField="Tytul" HeaderText="Tytuł"/>
        <asp:BoundField DataField="Autor" HeaderText="Autor"/>
        <asp:BoundField DataField="Data_Wydania" HeaderText="Data Wydania" DataFormatString="{0:d}"/>
        <asp:BoundField DataField="Wydawnictwo" HeaderText="Wydawnictwo"/>
    </Columns>

</asp:GridView>
                    </div>
        <asp:DropDownList ID="ZatwierdzWypDropDown" DataSourceID="SqlDataSource1" DataTextField="ID_DoZatwierdzenia" DataValueField="Tytul" runat="server" forecolor="Blue" Font-Bold="true" Font-Size="X-Large" AutoPostBack="True" OnSelectedIndexChanged="ZatwierdzWypDropDown_SelectedIndexChanged"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [DoZatwierdzeniaWyp]"></asp:SqlDataSource>
        <div><asp:Label  style="text-align:left" Width="300px" runat="server"  Text="Okres Wypożyczenia" Font-Size="X-Large" Font-Bold="true"></asp:Label> 
        <asp:TextBox ID="ZwrotDo" runat="server"
            ToolTip="Wpisz liczbę dni po której książka ma być zwrócona">
        </asp:TextBox></div>
        <div><asp:RequiredFieldValidator ErrorMessage="*Ilość Dni na jaką książka zostanie wypożyczona nie została wpisana!" ForeColor="Red" Font-Size="Large" Font-Bold="true" ControlToValidate="ZwrotDo" runat="server" /><br />
           <asp:RegularExpressionValidator runat="server"
            ControlToValidate="ZwrotDo" 
            ErrorMessage="*Wpisz Liczbę" Font-Size="Large" Font-Bold="true"  ForeColor="Red"
            ValidationExpression="^[0-9]+$" />
       </div>
    <asp:Button ID="Button2" runat="server" Text="Zatwierdź" OnClick="Button2_Click"  />
        <asp:Panel ID="NieMozna" runat="server">
        <asp:Label ID="NieMoznaLabel" runat="server" Text="Nie możesz zatwierdzić wypożyczenia gdyż nie ma wolnych egzemplarzy, poczekaj aż się zwolnią" Font-Bold="true" Font-Size="X-Large"></asp:Label>
            </asp:Panel>

                
        <div style="border-bottom-style:solid"></div>
                    <div style="text-align:center">
        <asp:Label ID="Label5" runat="server" Text="Zwroty do zatwierdzenia" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                    </div>
        <div style="margin-left:auto; margin-right:auto; width:800px;">
        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" 
    DataKeyNames="Id_DoZatwierdzeniaZwrot" EmptyDataText = "Brak wypożyczeń do zatwierdzenia w bibliotece" >
            <HeaderStyle BackColor="#4acabb" Font-Bold="True" ForeColor="Red" />            
                <RowStyle BackColor="#effc09" Font-Bold="True" ForeColor="Red" />
    <Columns>
        <asp:BoundField DataField="Id_DoZatwierdzeniaZwrot" HeaderText="ID" ReadOnly="True"/>
        <asp:BoundField DataField="UserName" HeaderText="Użytkownik"/>
        <asp:BoundField DataField="Tytul" HeaderText="Tytuł"/>
        <asp:BoundField DataField="Autor" HeaderText="Autor"/>
        <asp:BoundField DataField="Data_Wydania" HeaderText="Data Wydania" DataFormatString="{0:d}"/>
        <asp:BoundField DataField="Wydawnictwo" HeaderText="Wydawnictwo"/>
    </Columns>

</asp:GridView>
                    </div>
         
                <asp:DropDownList ID="ZatwierdzZwrotDropDown" DataSourceID="SqlDataSource2" DataTextField="Id_DoZatwierdzeniaZwrot" DataValueField="Id_DoZatwierdzeniaZwrot" runat="server" forecolor="Blue" Font-Bold="true" Font-Size="X-Large" AutoPostBack="True" OnSelectedIndexChanged="ZatwierdzZwrotDropDown_SelectedIndexChanged"></asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [DoZatwierdzeniaZwrot]"></asp:SqlDataSource>
    <asp:Button ID="Button3" runat="server" Text="Zatwierdź" OnClick="Button3_Click" CausesValidation=false/>


        <div style="border-bottom-style:solid"></div>
                    <div style="text-align:center">
        <asp:Label ID="Label6" runat="server" Text="Aktualne wypożyczenia w bibliotece użytkownika" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                    </div>
        <asp:DropDownList ID="AktualneDropDown" DataSourceID="SqlDataSource4" DataTextField="UserName" DataValueField="UserName" runat="server" forecolor="Blue" Font-Bold="true" Font-Size="X-Large" AutoPostBack="True" OnSelectedIndexChanged="AktualneDropDown_SelectedIndexChanged"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [UserName] FROM [Wypozyczenie]"></asp:SqlDataSource>
        <asp:Button ID="Button5" runat="server" Text="Zobacz" OnClick="Button5_Click" CausesValidation=false/>
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
        <asp:Button ID="Button4" runat="server" Text="Zobacz" OnClick="Button4_Click" CausesValidation=false/>
        <div style="margin-left:auto; margin-right:auto; width:800px;">
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
    
</asp:Content>

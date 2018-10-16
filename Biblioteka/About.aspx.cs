using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Biblioteka
{
    public partial class About : Page
    {
        static string connString = "Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\aspnet-Biblioteka-20170316082419.mdf;Initial Catalog=aspnet-Biblioteka-20170316082419;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connString);
        protected void Page_Load(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            String userName = HttpContext.Current.User.Identity.Name;
            cmd.CommandText = "SELECT Role FROM AspNetUsers WHERE username ='" + userName + "'"; //pobieramy rolę aktualnie zalogowanego użytkownika
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                if (reader.GetString(reader.GetOrdinal("Role")) == "Pracownik")//jeśli jest to pracownik to wysietli mu się panel pracownika
                {
                    IfPracownik.Visible = true;
                    IfNotPracownik.Visible = false;
                }
                else                                                            // jeśli to nie pracownik to dostanie komunikat że nie jest uprawniony do przebywania w tym panelu
                {
                    IfPracownik.Visible = false;
                    IfNotPracownik.Visible = true;
                }

            }
            conn.Close();
            if (!IsPostBack)
            {
                BindGrid(); //wyświetla gridview z informacjami o ksiązkach w bibliotece
                ZatwierdzWypDropDown.DataBind(); //pokazuje aktualne dane w dropdownlist(czy jest jakieś wypożyczenie do zatwierdzenia)
                ZatwierdzZwrotDropDown.DataBind(); //pokazuje aktualne dane w dropdownlist(czy jest jakiś zwrot do zatwierdzenia)
            }
            IfButton2Active();//sprawdza czy jest coś w dropdownlist(zatwierdz wypozyczenie). Jeśli tak to przycisk jest aktywny
            IfButton3Active();//sprawdza czy jest coś w dropdownlist(zatwierdz zwrot). Jeśli tak to przycisk jest aktywny


            BindGrid2();//wyświetla gridview z informacjami o książkach które czekają na zatwierdzenie wypozyczenia
            IfWypozyczone.Visible = false;
            BindGrid3();//wyświetla gridview z informacjami o książkach które czekają na zwrot wypozyczenia
            NieMozna.Visible = false;


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("DodajPozycje.aspx"); //kliknięcie przycisku powoduje do przejscia do webformu DodajPozycje.aspx
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)// gridview jest domyslnie ustalony na pokazywanie 10 pozycji, ta metoda pozwala na przejscie do kolejnej strony gridview jesli pozycji jest ponad 10
        {
            BindGrid();
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        protected void CancelEdit(object sender, GridViewCancelEditEventArgs e)//działa na kliknięcie cancel w gridview
        {
            GridView1.EditIndex = -1;
            BindGrid();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                conn.Open();
                SqlCommand cmd2 = conn.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                int ID_Ksiazki = Convert.ToInt32(e.CommandArgument);
                cmd2.CommandText = "SELECT Count(*) as ct FROM Wypozyczenie where ID_Ksiazki = '" + ID_Ksiazki + "'";// sprawdza czy są wypożyczenia ksiażki ktorą chcemy usunać
                using (SqlDataReader reader = cmd2.ExecuteReader())
                {
                    reader.Read();
                    if (reader.GetInt32(reader.GetOrdinal("ct")) >= 1)
                    {
                        IfWypozyczone.Visible = true;// jeśli jest jakaś książka wypożyczona to pojawi się komunikat że ksiażki nie można usunąć
                    }
                    else
                    {
                        conn.Close();
                        DeleteRecordByID(ID_Ksiazki);// jeśli książka nie jest wypożyczona to zostanie usunięta
                    }
                }
                conn.Close();
            }
        }


        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
           conn.Open();
            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT Count(*) as ct FROM Wypozyczenie where ID_Ksiazki = "+GridView1.DataKeys[e.RowIndex].Value +"";
            using (SqlDataReader reader = cmd2.ExecuteReader())
            {
                reader.Read();
                if (reader.GetInt32(reader.GetOrdinal("ct")) >= 1)
                {
                    IfWypozyczone.Visible = true;
                }
                else
                {
                    int ID_Ksiazki = (int)GridView1.DataKeys[e.RowIndex].Value;
                    DeleteRecordByID(ID_Ksiazki);
                }
            }
        }
        protected void DeleteRecordByID(int ID_Ksiazki)//metoda która usuwa książkę z biblioteki i jej ewentualne rezerwacje
        {
            SqlCommand command = new SqlCommand();
            SqlCommand cmd = new SqlCommand();
            command.CommandText = "DELETE FROM Ksiazka where ID_Ksiazki ='" + ID_Ksiazki + "'";
            cmd.CommandText = "DELETE FROM Rezerwacja where ID_Ksiazki ='" + ID_Ksiazki + "'";
            conn.Open();
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            command.Connection = conn;
            command.ExecuteNonQuery();
            conn.Close();
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        
        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)//metoda pozwalajaca na rozpoczęcie edycji w gridview
        {

            GridView1.EditIndex = e.NewEditIndex;
            BindGrid();
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)//metoda ktora zatwierdza edycję w gridview
        {
            int ID_Ksiazki = (int)GridView1.DataKeys[e.RowIndex].Value;//pobieramy ID_Ksiazki z konkretnego rzędu gridview
            TextBox TextBox1 = (TextBox)GridView1.Rows[e.RowIndex].FindControl("textBox1");//pobieranie wpisanej liczby egzemplarzy w textbox ktory pojawia sie po kliknięciu edit
            SqlCommand cmd = new SqlCommand();
            SqlCommand cm = new SqlCommand();
            SqlCommand cm1 = new SqlCommand();


            cmd.Connection = conn;
            cm.Connection = conn;
            cm1.Connection = conn;


            cm.CommandText = "UPDATE Ksiazka SET tmp  = Liczba_Egzemplarzy where ID_Ksiazki='" + ID_Ksiazki + "'";// Wstaiwamy wartosc kolumny Liczba_Egzemplarzy do kolumny tmp
            cmd.CommandText = "UPDATE Ksiazka SET Liczba_Egzemplarzy  = @i where ID_Ksiazki='" + ID_Ksiazki + "'";// ustawia liczbę egzemplarzy taką jaką wpiszemy w textbox
            cmd.Parameters.AddWithValue("@i", TextBox1.Text);// parametr do cmd
            cm1.CommandText = "UPDATE Ksiazka SET Roznica  = Liczba_Egzemplarzy-tmp where ID_Ksiazki='" + ID_Ksiazki + "'";// Do kolumny różnica wprowadzamy wynik odejmowania kolumny Liczba_Egzemplarzy od tmp
            conn.Open();
            cm.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
            cm1.ExecuteNonQuery();


            GridView1.EditIndex = -1;

            BindGrid();//pokazuje uaktualniony gridview po edycji

            conn.Close();
            conn.Open();
            SqlCommand comd = conn.CreateCommand();
            comd.CommandType = CommandType.Text;
            comd.CommandText = "SELECT Count(*) as ct FROM Rezerwacja where ID_Ksiazki='"+ID_Ksiazki+"'";// sprawdzanie liczby rezereacji w tabeli rezerwacja
            using (SqlDataReader reader = comd.ExecuteReader())
            {
                reader.Read();
                if (reader.GetInt32(reader.GetOrdinal("ct")) >= 1)// jesli jest jakaś rezerwacja
                {
                    SqlCommand cmd2 = conn.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "SELECT roznica FROM Ksiazka  where ID_Ksiazki = " + ID_Ksiazki + "";// pobieramy warotsc z kolumny roznica bo jest to wartosc o jaką zmieniła się liczba egzemplarzy od ostatniego stanu
                    conn.Close();
                    conn.Open();
                    using (SqlDataReader reader2 = cmd2.ExecuteReader())
                    {
                        reader2.Read();
                        int liczba = reader2.GetInt32(reader2.GetOrdinal("roznica"));// liczba o jaką zmieniła się liczba wolnych egzemplarzy
                        for (int i = 0; i < liczba; i++)  //wywolujemy fora tyle razy ile zmieniła się liczba egzemplarzy
                        {
                            SqlCommand cmd3 = conn.CreateCommand();
                            cmd3.CommandType = CommandType.Text;
                            cmd3.CommandText = "SELECT TOP(1) ID_Rezerwacji as ID FROM Rezerwacja R inner join Ksiazka K on R.ID_Ksiazki=K.ID_Ksiazki where R.ID_Ksiazki="+ID_Ksiazki+" order by Data_Rezerwacji ASC ;";// pobieramy najstarszą rezerwację
                            conn.Close();
                            conn.Open();
                            using (SqlDataReader reader3 = cmd3.ExecuteReader())
                            {
                                reader3.Read();
                                int ID_Rezerwacji = reader3.GetInt32(reader3.GetOrdinal("ID"));//zapisujemy ID Rezerwacji do Inta
                                SqlCommand cmd4 = new SqlCommand("INSERT INTO DoZatwierdzeniaWyp(ID_Usera,Tytul,ID_Ksiazki,UserName,Autor,Data_Wydania,Wydawnictwo) SELECT Rezerwacja.ID_Usera,Rezerwacja.Tytul,Rezerwacja.ID_Ksiazki,Rezerwacja.UserName,Rezerwacja.Autor,Rezerwacja.Data_Wydania,Rezerwacja.Wydawnictwo from Rezerwacja where Rezerwacja.ID_Rezerwacji=" + ID_Rezerwacji + "", conn);//Do tabeli DoZatwierdzeniaWyp wprowadzamy dane z rezerwacji
                                SqlCommand cmd5 = new SqlCommand("DELETE FROM Rezerwacja where ID_Rezerwacji = " + ID_Rezerwacji + "", conn);// usuwamy rząd z tabeli rezerwacji dla pobranego ID_Rezerwacji
                                conn.Close();
                                conn.Open();
                                cmd4.ExecuteNonQuery();
                                cmd5.ExecuteNonQuery();
                                conn.Close();
                            }
                            BindGrid();
                            BindGrid2();
                        }
                    }
                    conn.Close();
                }
            }
            BindGrid2();// pokazuje uaktualniony gridview z ksiązkami czekającymi do zatwierdzenia
            ZatwierdzWypDropDown.DataBind();//uaktualnia dropdownlist
            IfButton2Active();//sprawdza czy przycisk jest aktywny(zależne od tego czy jest coś w drodpown)
        }
        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)//anulowanie edytowania
        {
            GridView1.EditIndex = -1;
            BindGrid();
        }

        private void BindGrid()// metoda do pozyskiwania informacji do gridview z tabeli ksiazka
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Tytul, Autor, Data_Wydania, Wydawnictwo, Liczba_Egzemplarzy,ID_Ksiazki FROM Ksiazka"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView1.DataSource = dt;
                            GridView1.DataBind();
                        }
                    }
                }
            }
        }
        private void BindGrid2()// metoda do pozyskiwania informacji do gridview z tabeli DoZatwierdzeniaWyp
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ID_DoZatwierdzenia,UserName,Tytul, Autor, Data_Wydania, Wydawnictwo,ID_Ksiazki FROM DoZatwierdzeniaWyp"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView2.DataSource = dt;
                            GridView2.DataBind();
                        }
                    }
                }
            }
        }
        private void BindGrid3()// metoda do pozyskiwania informacji do gridview z tabeli DoZatwierdzeniaZwrot
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ID_DoZatwierdzeniaZwrot,UserName,Tytul, Autor, Data_Wydania, Wydawnictwo,ID_Ksiazki FROM DoZatwierdzeniaZwrot"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView3.DataSource = dt;
                            GridView3.DataBind();
                        }
                    }
                }
            }
        }
        private void BindGrid4() // metoda do pozyskiwania informacji do gridview z tabeli Wypozyczenie, dla wybranego w drodpownlist użytkownika
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ID_Wypozyczenia,UserName,Tytul, Autor, Data_Wydania, Wydawnictwo,ID_Ksiazki,Data_Wypozyczenia,ZwrotDo FROM Wypozyczenie where UserName='"+AktualneDropDown.SelectedValue+"'"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView4.DataSource = dt;
                            GridView4.DataBind();
                        }
                    }
                }
            }
        }
        private void BindGrid5()// metoda do pozyskiwania informacji do gridview z tabeli Historia_Wypozyczen, dla wybranego w drodpownlist użytkownika
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ID_Zwroconej,UserName,Tytul, Autor, Data_Wydania, Wydawnictwo,Data_Wypozyczenia,Data_Zwrotu,Kara FROM Historia_Wypozyczen where UserName='"+HistoriaDropDown.SelectedValue+"'"))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.Connection = conn;
                        sda.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            sda.Fill(dt);
                            GridView5.DataSource = dt;
                            GridView5.DataBind();
                        }
                    }
                }
            }
        }


        protected void ZatwierdzWypDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void Button2_Click(object sender, EventArgs e)// przycisk do zatwierdzania wypozyczen
        {

            conn.Open();
            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT Liczba_Egzemplarzy FROM Ksiazka  where Tytul = @Tytul";// sprawdzamy liczbę egzemplarzy
            cmd2.Parameters.AddWithValue("@Tytul", ZatwierdzWypDropDown.SelectedItem.Value);
            conn.Close();
            conn.Open();
            using (SqlDataReader reader2 = cmd2.ExecuteReader())
            {
                reader2.Read();
                if (reader2.GetInt32(reader2.GetOrdinal("Liczba_Egzemplarzy")) >= 1)// jeśli są wolne egzemplarze
                {

                    SqlCommand cmd1 = new SqlCommand("INSERT TOP(1) INTO Wypozyczenie(ID_Ksiazki,ID_Usera,Autor,Tytul,Data_Wydania,Wydawnictwo,UserName,Data_Wypozyczenia,ZwrotDo) Select DoZatwierdzeniaWyp.ID_Ksiazki,DoZatwierdzeniaWyp.ID_Usera,DoZatwierdzeniaWyp.Autor,DoZatwierdzeniaWyp.Tytul,DoZatwierdzeniaWyp.Data_Wydania,DoZatwierdzeniaWyp.Wydawnictwo,DoZatwierdzeniaWyp.UserName,@Data_Wypozyczenia,@Data_Zwrotu from DoZatwierdzeniaWyp where DoZatwierdzeniaWyp.Tytul=@Tytul", conn);// wprowadzamy do tabeli wypożyczenie na ksiażkę wybraną w dropdownlist
                    cmd1.Parameters.AddWithValue("@Tytul", ZatwierdzWypDropDown.SelectedItem.Value);// parametr do cmd1
                    DateTime dataWyp = DateTime.Now;//pobranie obecnej daty do Daty Wypozyczenia
                    cmd1.Parameters.AddWithValue("@Data_Wypozyczenia", dataWyp);// parametr do cmd1
                    cmd1.Parameters.AddWithValue("@Data_Zwrotu", dataWyp.AddDays(Int32.Parse(ZwrotDo.Text)));//parametr do cmd1
                    conn.Close();
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    conn.Close();
                    SqlCommand com = new SqlCommand();
                    com.CommandText = "UPDATE Ksiazka SET Liczba_Egzemplarzy = Liczba_Egzemplarzy-@i WHERE Tytul = @Tytul";// zmniejszanie wolnej liczby egzemplarzy
                    com.Parameters.AddWithValue("@Tytul", ZatwierdzWypDropDown.SelectedItem.Value);//parametr com
                    com.Parameters.AddWithValue("@i", "1");//parametr com
                    conn.Open();
                    com.Connection = conn;
                    com.ExecuteNonQuery();
                    conn.Close();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "DELETE Top(1) FROM DoZatwierdzeniaWyp where Tytul = @Tytul";// usuwa rząd który wprowadzilismy do wypozyczenia z tabeli DoZatwierdzeniaWyp
                    command.Parameters.AddWithValue("@Tytul", ZatwierdzWypDropDown.SelectedItem.Value);//command parametr
                    conn.Open();
                    command.Connection = conn;
                    command.ExecuteNonQuery();
                    conn.Close();
                    ZatwierdzWypDropDown.DataBind();//uaktualnia dropdownlist z ksiazkami czekajacymy do zatwierdzenia wypozyczenia
                    BindGrid();//pokazuje aktualne dane z gridview1
                    BindGrid2();//pokazuje aktualne dane z gridview2
                    AktualneDropDown.DataBind();//uaktualnia dropdown list z użytkownikami ktorzy mają wypozyczenia
                    IfButton2Active();//sprawdza czy jest cos w dropdownlist do zatwierdzenia wypozyczen, jesli tak to przycisk jest aktywny
                }
                else
                {
                    NieMozna.Visible = true;// jesli nie ma juz wolnych egzemplarzy a są ksiazki czekajace na zatwierdzenie to pokaże się informacja o braku wolnych egzemplarzy
                }
            }

        }

        protected void ZatwierdzZwrotDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)// przycisk ktory zatwierdza zwrot
        {
            SqlCommand cmd1 = new SqlCommand("INSERT  INTO Historia_Wypozyczen(ID_Usera,Autor,Tytul,Data_Wydania,Wydawnictwo,UserName,Data_Wypozyczenia,Data_Zwrotu,ZwrotDo,Roznica,Id_DoZatwierdzeniaZwrot) Select DoZatwierdzeniaZwrot.ID_Usera,DoZatwierdzeniaZwrot.Autor,DoZatwierdzeniaZwrot.Tytul,DoZatwierdzeniaZwrot.Data_Wydania,DoZatwierdzeniaZwrot.Wydawnictwo,DoZatwierdzeniaZwrot.UserName,DoZatwierdzeniaZwrot.Data_Wypozyczenia,@Data_Zwrotu,DoZatwierdzeniaZwrot.ZwrotDo,(Select DATEDIFF(DAY,DoZatwierdzeniaZwrot.ZwrotDo,@Data_Zwrotu)),@Id_DoZatwierdzeniaZwrot from DoZatwierdzeniaZwrot where DoZatwierdzeniaZwrot.Id_DoZatwierdzeniaZwrot=@Id_DoZatwierdzeniaZwrot", conn);// wprowadza dane z DoZatwierdzeniaZwrot do Historii WYpozyczen
            cmd1.Parameters.AddWithValue("@Id_DoZatwierdzeniaZwrot", ZatwierdzZwrotDropDown.SelectedItem.Value);//cmd1 parametr
            DateTime dataWyp = DateTime.Now;//obecna data w ktorej odbyl sie zwrot
            cmd1.Parameters.AddWithValue("@Data_Zwrotu", dataWyp);//parametr cmd1
            conn.Open();
            cmd1.Connection = conn;
            cmd1.ExecuteNonQuery();
            conn.Close();
           conn.Open();
            SqlCommand comm = conn.CreateCommand();
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT Roznica FROM Historia_Wypozyczen where Id_DoZatwierdzeniaZwrot =  @Id_DoZatwierdzeniaZwrot ";// pobiera różnicę między Datą zwrotu a datą do której należało zwrocić ksiażkę
            comm.Parameters.AddWithValue("@Id_DoZatwierdzeniaZwrot", ZatwierdzZwrotDropDown.SelectedItem.Value);
            using (SqlDataReader reader = comm.ExecuteReader())
            {

                reader.Read();
                if (reader.GetInt32(reader.GetOrdinal("Roznica")) >= 1)// jeśli różnica jest wieksza lub równa 1(książka została zwrócona po określonym terminie)
                {
                    conn.Close();

                    conn.Open();

                    SqlCommand comm1 = new SqlCommand();
                    comm1.CommandText = "UPDATE Historia_Wypozyczen SET Kara = 1*Roznica where Id_DoZatwierdzeniaZwrot =  @Id_DoZatwierdzeniaZwrot ";// Ustawia karę taką jaka jest różnica(kara za dzień zwłoki to złotówka)
                    comm1.Parameters.AddWithValue("@Id_DoZatwierdzeniaZwrot", ZatwierdzZwrotDropDown.SelectedItem.Value);

                    comm1.Connection = conn;
                    comm1.ExecuteNonQuery();
                    conn.Close();

                }
                else
                {
                    conn.Close();

                    conn.Open();

                    SqlCommand comm2 = new SqlCommand();
                    comm2.CommandText = "UPDATE Historia_Wypozyczen SET Kara = 0 where Id_DoZatwierdzeniaZwrot =  @Id_DoZatwierdzeniaZwrot ";//jeśli nie przekroczono czasu to ustawia karę jako 0
                    comm2.Parameters.AddWithValue("@Id_DoZatwierdzeniaZwrot", ZatwierdzZwrotDropDown.SelectedItem.Value);
                    comm2.Connection = conn;
                    comm2.ExecuteNonQuery();
                    conn.Close();

                }

            }
            conn.Close();
            SqlCommand cmd = new SqlCommand();
            SqlCommand cm2 = new SqlCommand();
            SqlCommand cm1 = new SqlCommand();
            cm2.CommandText = "UPDATE K SET K.tmp  = Liczba_Egzemplarzy FROM KSIAZKA AS K INNER JOIN DoZatwierdzeniaZwrot as DZZ on K.ID_Ksiazki=DZZ.ID_Ksiazki WHERE Id_DoZatwierdzeniaZwrot = @Id_DoZatwierdzeniaZwrot";// do tmp wstawiamy aktualny stan liczby egzemplarzy
            cm2.Parameters.AddWithValue("@ID_DoZatwierdzeniaZwrot", ZatwierdzZwrotDropDown.SelectedItem.Value);
            cmd.CommandText = "UPDATE K SET K.Liczba_Egzemplarzy = Liczba_Egzemplarzy+@i FROM KSIAZKA AS K INNER JOIN DoZatwierdzeniaZwrot as DZZ on K.ID_Ksiazki=DZZ.ID_Ksiazki WHERE Id_DoZatwierdzeniaZwrot = @Id_DoZatwierdzeniaZwrot";//zwiekszamy liczbę wolnych egzemplarzy
            cmd.Parameters.AddWithValue("@i", "1");
            cmd.Parameters.AddWithValue("@ID_DoZatwierdzeniaZwrot", ZatwierdzZwrotDropDown.SelectedItem.Value);
            cm1.CommandText = "UPDATE K SET K.roznica  = Liczba_Egzemplarzy-tmp FROM KSIAZKA AS K INNER JOIN DoZatwierdzeniaZwrot as DZZ on K.ID_Ksiazki=DZZ.ID_Ksiazki WHERE Id_DoZatwierdzeniaZwrot = @Id_DoZatwierdzeniaZwrot";// do roznica wstawiamy wynik odejmowania
            cm1.Parameters.AddWithValue("@ID_DoZatwierdzeniaZwrot", ZatwierdzZwrotDropDown.SelectedItem.Value);
            conn.Open();
            cm2.Connection = conn;
            cm2.ExecuteNonQuery();
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            cm1.Connection = conn;
            cm1.ExecuteNonQuery();
            conn.Close();
            conn.Open();
            SqlCommand comd = conn.CreateCommand();
            comd.CommandType = CommandType.Text;
            comd.CommandText = "SELECT Count(*) as ct FROM Rezerwacja R Inner Join DoZatwierdzeniaZwrot as DZZ on R.ID_Ksiazki=DZZ.ID_Ksiazki WHERE Id_DoZatwierdzeniaZwrot = @Id_DoZatwierdzeniaZwrot";// sprawdza czy są rezerwację na zwróconą książkę
            comd.Parameters.AddWithValue("@ID_DoZatwierdzeniaZwrot", ZatwierdzZwrotDropDown.SelectedItem.Value);
            using (SqlDataReader reader = comd.ExecuteReader())
            {
                reader.Read();
                if (reader.GetInt32(reader.GetOrdinal("ct")) >= 1)// jeśli są
                {
                    SqlCommand cm = conn.CreateCommand();
                    cm.CommandType = CommandType.Text;
                    cm.CommandText = "SELECT K.ID_Ksiazki FROM KSIAZKA K INNER JOIN DoZatwierdzeniaZwrot DZZ on K.ID_Ksiazki=DZZ.ID_Ksiazki WHERE Id_DoZatwierdzeniaZwrot = @Id_DoZatwierdzeniaZwrot";// pobieramy ID Ksiazki właśnie zwróconej
                    cm.Parameters.AddWithValue("@Id_DoZatwierdzeniaZwrot", ZatwierdzZwrotDropDown.SelectedItem.Value);
                    conn.Close();
                    conn.Open();
                    using (SqlDataReader reader1 = cm.ExecuteReader())
                    {
                        reader1.Read();
                        int ID_Ksiazki = reader1.GetInt32(reader1.GetOrdinal("ID_Ksiazki"));// zapisujemy ID_Ksiazki
                        SqlCommand cmd2 = conn.CreateCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "SELECT roznica FROM Ksiazka  where ID_Ksiazki = " + ID_Ksiazki + "";// pobieramy wartosc o jaką zmienila sie liczba egzemplarzy od ostatniego stanu
                        cmd2.Parameters.AddWithValue("@Id_DoZatwierdzeniaZwrot", ZatwierdzZwrotDropDown.SelectedItem.Value);
                        conn.Close();
                        conn.Open();
                        using (SqlDataReader reader2 = cmd2.ExecuteReader())
                        {
                            reader2.Read();
                            int liczba = reader2.GetInt32(reader2.GetOrdinal("roznica"));// zapisujemy liczbę z kolumny roznica
                            for (int i = 0; i < liczba; i++)  //wywolujemy fora tyle razy co wartosc w kolumnie roznica(czyli o ile zmienila sie wartosc wolnych egzemplarzy)
                            {
                                SqlCommand cmd3 = conn.CreateCommand();
                                cmd3.CommandType = CommandType.Text;
                                cmd3.CommandText = "SELECT TOP(1) ID_Rezerwacji as ID FROM Rezerwacja R inner join DoZatwierdzeniaZwrot DZ on R.ID_Ksiazki=DZ.ID_Ksiazki where R.ID_Ksiazki=" + ID_Ksiazki + " order by Data_Rezerwacji ASC ;";// pobieramy najstarszą rezerwację
                                conn.Close();
                                conn.Open();
                                using (SqlDataReader reader3 = cmd3.ExecuteReader())
                                {
                                    reader3.Read();
                                    int ID_Rezerwacji = reader3.GetInt32(reader3.GetOrdinal("ID"));
                                    SqlCommand cmd4 = new SqlCommand("INSERT INTO DoZatwierdzeniaWyp(ID_Usera,Tytul,ID_Ksiazki,UserName,Autor,Data_Wydania,Wydawnictwo) SELECT Rezerwacja.ID_Usera,Rezerwacja.Tytul,Rezerwacja.ID_Ksiazki,Rezerwacja.UserName,Rezerwacja.Autor,Rezerwacja.Data_Wydania,Rezerwacja.Wydawnictwo from Rezerwacja where Rezerwacja.ID_Rezerwacji=" + ID_Rezerwacji + "", conn);//wstawiamy z rezerwacji do zatwierdzeniaWYP
                                    SqlCommand cmd5 = new SqlCommand("DELETE FROM Rezerwacja where ID_Rezerwacji = " + ID_Rezerwacji + "", conn);// usuwamy z rezerwacji
                                    conn.Close();
                                    conn.Open();
                                    cmd4.ExecuteNonQuery();
                                    cmd5.ExecuteNonQuery();
                                    conn.Close();
                                }
                            }
                        }
                        conn.Close();
                        conn.Open();
                        SqlCommand command = new SqlCommand();
                        command.CommandText = "DELETE  FROM DoZatwierdzeniaZwrot where Id_DoZatwierdzeniaZwrot = @Id_DoZatwierdzeniaZwrot";//usuwamy z DoZatwierdzeniaZwrot
                        command.Parameters.AddWithValue("@Id_DoZatwierdzeniaZwrot", ZatwierdzZwrotDropDown.SelectedItem.Value);
                        command.Connection = conn;
                        command.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                else
                {
                    conn.Close();
                    conn.Open();
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "DELETE  FROM DoZatwierdzeniaZwrot where Id_DoZatwierdzeniaZwrot = @Id_DoZatwierdzeniaZwrot";// jesli nie ma rezerwacji to odrazu usuwa z doZatwierdzeniaZwrot
                    command.Parameters.AddWithValue("@Id_DoZatwierdzeniaZwrot", ZatwierdzZwrotDropDown.SelectedItem.Value);
                    command.Connection = conn;
                    command.ExecuteNonQuery();
                    conn.Close();
                }
                ZatwierdzZwrotDropDown.DataBind();
                HistoriaDropDown.DataBind();
                BindGrid();
                IfButton3Active();
                BindGrid3();
                BindGrid2();
                ZatwierdzWypDropDown.DataBind();
                IfButton2Active();
            }
        }
           

        protected void GridView5_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView5.PageIndex = e.NewPageIndex;
            GridView5.DataBind();
            BindGrid5();

        }

        protected void GridView5_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void HistoriaDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button4_Click(object sender, EventArgs e)// klikniecie przycisku powoduje pokazanie gridview z aktualnymi wypozyczeniami uzytkownika wybranego w dropdownlist
        {
            BindGrid5();
        }

        protected void AktualneDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)// mozliwosc zmiany stron w gridview
        {
            GridView4.PageIndex = e.NewPageIndex;
            GridView4.DataBind();
            BindGrid4();

        }

        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void Button5_Click(object sender, EventArgs e)// klikniecie przycisku powoduje pokazanie gridview z Historią wypozyczen uzytkownika wybranego w dropdownlist
        {
            BindGrid4();
        }
        protected void IfButton2Active()//metoda sprawdza czy są jakies dane w dropdownlist. Jeśli tak to przycisk2 jest aktywny.
        {
            if (ZatwierdzWypDropDown.SelectedIndex < 0)
            {
                Button2.Enabled = false;
            }
            else
            {
                Button2.Enabled = true;
            }
        }
        protected void IfButton3Active()//metoda sprawdza czy są jakies dane w dropdownlist. Jeśli tak to przycisk3 jest aktywny.
        {
            if (ZatwierdzZwrotDropDown.SelectedIndex < 0)
            {
                Button3.Enabled = false;
            }
            else
            {
                Button3.Enabled = true;
            }
        }

    }
}
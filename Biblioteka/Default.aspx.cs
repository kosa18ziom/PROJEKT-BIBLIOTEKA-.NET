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
    public partial class _Default : Page
    {
        static string connString = "Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\aspnet-Biblioteka-20170316082419.mdf;Initial Catalog=aspnet-Biblioteka-20170316082419;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connString);
        protected void Page_Load(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            String userName = HttpContext.Current.User.Identity.Name;
            cmd.CommandText = "SELECT Count(*) as ct FROM DoZatwierdzeniaZwrot where UserName = '" + userName + "'";//sprawdzamy czy aktualnie zalogowany użytkownik zwrócił książki które nie zostały jescze zatwierdzone
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                if (reader.GetInt32(reader.GetOrdinal("ct")) >= 1)//jeśli tak to pokaże się informacja o tym że Bibliotek jest w trakcie zatwierdzania zwrotu
                {
                    Panel1.Visible = true;
                }
                else                                             //jeśli nie to nie pokaze sie informacja
                {
                    Panel1.Visible = false;
                }

            }
            conn.Close();
            BindGrid3();//pokazuje gridview( aktualnie wypożyczone)
            BindGrid4();//pokazuje gridview(historia wypozyczen)
            BindGrid5();//pokazuje gridview(dane osobowe)


                String myQuery = String.Format("SELECT ID_Wypozyczenia FROM  Wypozyczenie where UserName = '" + userName + "'");//pobieranie ksiazek wypozyczonych przez użytkownika do dropdownlist
                SqlDataSource1.SelectCommand = myQuery;
                SqlDataSource1.DataBind();
            if (!IsPostBack)//W momencie kiedy nie występuje PostBack, czyli gdy strona jest wczytywana po raz pierwszy
            {
                ZwrocDropDown.DataBind();//pokazuje dane w dropdown
            }
            IfButton3Active();
        }
        private void BindGrid1()//pobiera dane do gridview bazując na wpisanym do textobxa autorze
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
               using (SqlCommand cmd = new SqlCommand("SELECT Tytul, Autor, Data_Wydania, Wydawnictwo, Liczba_Egzemplarzy,ID_Ksiazki FROM Ksiazka where Autor Like '%" + TextBox1.Text+"%'"))
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
        private void BindGrid2()//pobiera dane do gridview bazując na wpisanym do textobxa tytule
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (
                SqlCommand cmd = new SqlCommand("SELECT Tytul, Autor, Data_Wydania, Wydawnictwo, Liczba_Egzemplarzy,ID_Ksiazki FROM Ksiazka where Tytul Like '%" + TextBox2.Text + "%'"))
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

        private void BindGrid3()//pobiera aktualne wypozyczenia do gridview
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            String userName = HttpContext.Current.User.Identity.Name;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ID_Wypozyczenia,Tytul, Autor, Data_Wydania, Wydawnictwo,Data_Wypozyczenia,ZwrotDo FROM Wypozyczenie where UserName='" + userName + "'"))
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
        private void BindGrid4()//pobiera historię wypozyczen do gridview
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            String userName = HttpContext.Current.User.Identity.Name;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ID_Zwroconej,Tytul, Autor, Data_Wydania, Wydawnictwo,Data_Wypozyczenia,Data_Zwrotu,Kara FROM Historia_Wypozyczen where UserName='" + userName + "'"))
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
        private void BindGrid5()//pobiera dane do gridview o danych osobowych użytkownika
        {
            String userName = HttpContext.Current.User.Identity.Name;
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Email, Imie, Nazwisko, Data_Urodzenia FROM AspNetUsers where UserName = '" +userName+ "'"))
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

        protected void Button1_Click(object sender, EventArgs e)// klikniecie przycisku spowoduje pokazanie sie gridview z ksiazkami szukanymi po autorze
        {
            BindGrid1();
        }

        protected void Button2_Click(object sender, EventArgs e)// klikniecie przycisku spowoduje pokazanie sie gridview z ksiazkami szukanymi po tytule
        {
            BindGrid2();
        }

        protected void ZwrocDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void IfButton3Active()//sprawdza czy są jakieś ksiazki do zwrotu, jesli tak to przycisk aktywny
        {
            if (ZwrocDropDown.SelectedIndex < 0)
            {
                Button3.Enabled = false;
            }
            else
            {
                Button3.Enabled = true;
            }
        }
        protected void Button3_Click(object sender, EventArgs e)// przycisk zwracający książkę
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT TOP(1) INTO DoZatwierdzeniaZwrot(ID_Ksiazki,ID_Usera,Autor,Tytul,Data_Wydania,Wydawnictwo,UserName,Data_Wypozyczenia,ZwrotDo) Select Wypozyczenie.ID_Ksiazki,Wypozyczenie.ID_Usera,Wypozyczenie.Autor,Wypozyczenie.Tytul,Wypozyczenie.Data_Wydania,Wypozyczenie.Wydawnictwo,Wypozyczenie.UserName,Wypozyczenie.Data_Wypozyczenia,Wypozyczenie.ZwrotDo from Wypozyczenie where Wypozyczenie.ID_Wypozyczenia=@ID_Wypozyczenia", conn);//dane z wypożyczenia wstawiamy do tabeli DoZatwierdzeniaZwrot
            cmd.Parameters.AddWithValue("@ID_Wypozyczenia", ZwrocDropDown.SelectedItem.Value);
            cmd.ExecuteNonQuery();
            conn.Close();
            SqlCommand command = new SqlCommand();
            command.CommandText = "DELETE TOP(1) FROM Wypozyczenie where ID_Wypozyczenia =@ID_Wypozyczenia";//Usuwamy z wypożyczenia
            command.Parameters.AddWithValue("@ID_Wypozyczenia", ZwrocDropDown.SelectedItem.Value);
            conn.Open();
            command.Connection = conn;
            command.ExecuteNonQuery();
            conn.Close();
            ZwrocDropDown.DataBind();
            BindGrid3();
            Panel1.Visible = true;// pojawia się informacja o tym że biblioteka jest w trakcie zatwierdzania zwrotu

        }
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView2.PageIndex = e.NewPageIndex;
            GridView2.DataBind();
        }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView3.PageIndex = e.NewPageIndex;
            GridView3.DataBind();
        }

        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
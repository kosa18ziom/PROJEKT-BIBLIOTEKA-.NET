using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Biblioteka
{
    public partial class DodajPozycje : System.Web.UI.Page
    {
        static string connString = "Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\aspnet-Biblioteka-20170316082419.mdf;Initial Catalog=aspnet-Biblioteka-20170316082419;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connString);
        protected void Page_Load(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            String userName = HttpContext.Current.User.Identity.Name;
            cmd.CommandText = "SELECT Role FROM AspNetUsers WHERE username ='" + userName + "'";//pobieramy rolę aktualnie zalogowanego użytkownika
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                if (reader.GetString(reader.GetOrdinal("Role")) == "Pracownik")// jeśli jest to pracownik to pojawi się panel z textobxami to wpisania danych ksiazki
                {
                    IfPracownik.Visible = true;
                    IfNotPracownik.Visible = false;
                }
                else                                                            //jeśli nie to pojawią się dane o braku uprawnień
                {
                    IfPracownik.Visible = false;
                    IfNotPracownik.Visible = true;
                }

            }
            conn.Close();
        }

        protected void Button1_Click(object sender, EventArgs e)//Przycisk powoduje wstawienie wpisanych danych w textboxach do tabeli Ksiazka
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("INSERT INTO Ksiazka(Tytul,Autor,Data_Wydania,Wydawnictwo,Liczba_Egzemplarzy) VALUES (@Tytul,@Autor,@DataWydania,@Wydawnictwo,@LiczbaEgzemplarzy)", conn);
            cmd.Parameters.AddWithValue("@Tytul", Tytul.Text);
            cmd.Parameters.AddWithValue("@Autor", Autor.Text);
            cmd.Parameters.AddWithValue("@DataWydania", DataWydania.Text);
            cmd.Parameters.AddWithValue("@Wydawnictwo", Wydawnictwo.Text);
            cmd.Parameters.AddWithValue("@LiczbaEgzemplarzy", LiczbaEgzemplarzy.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
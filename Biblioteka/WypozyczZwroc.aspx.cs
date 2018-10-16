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
    public partial class WypozyczZwroc : System.Web.UI.Page
    {
        static string connString = "Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\aspnet-Biblioteka-20170316082419.mdf;Initial Catalog=aspnet-Biblioteka-20170316082419;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connString);
        protected void Page_Load(object sender, EventArgs e)
        {
            String userName = HttpContext.Current.User.Identity.Name;
            String myQuery = String.Format("SELECT Tytul FROM  Rezerwacja where UserName = '" + userName + "'");//sprawdzamy czy użytkownik zalogowany ma jakieś rezerwację, jeśli tak to będzie miał możliwosć ich anulowania
            SqlDataSource3.SelectCommand = myQuery;
            SqlDataSource3.DataBind();
            AnulujRezerwacjeDropDown.DataBind();
                SqlDataSource1.DataBind();
            
        }

        protected void KsiazkaDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)// kliknięcie przycisku spowoduje wstawienie danych o ksiazce do tabeli DoZatwierdzeniaWyp
        {

            conn.Open();
            String userName = HttpContext.Current.User.Identity.Name;
            SqlCommand cmd = new SqlCommand("INSERT INTO DoZatwierdzeniaWyp(ID_Usera,Tytul,UserName) SELECT AspNetUsers.Id,@Tytul,Email from AspNetUsers where AspNetUsers.UserName='" + userName + "'", conn);//Do tabeli wprowadzamy wybrany w dropdown Tytul a takze ID i Username aktualnego uzytkownika
            SqlCommand cmd1 = new SqlCommand("Update DoZatwierdzeniaWyp SET ID_Ksiazki=(SELECT Ksiazka.ID_Ksiazki),Autor=(Select Ksiazka.Autor),Data_Wydania=(Select Ksiazka.Data_Wydania),Wydawnictwo=(Ksiazka.Wydawnictwo) from Ksiazka where DoZatwierdzeniaWyp.Tytul=Ksiazka.Tytul", conn);//Dopisujemy do tabeli resztę informacji o ksiażce bazując na wstawionym wczesniej tytule
            cmd.Parameters.AddWithValue("@Tytul", KsiazkaDropDown.SelectedItem.Value);
            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            conn.Close();
            KsiazkaDropDown.DataBind();
            RezerwujDropDown.DataBind();

        }

        protected void RezerwujDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)// klikniecie przycisku powoduje wstawienie danych ksiażki wybranej w dropdownlist do tabeli rezerwacja
        {
            conn.Open();
            String userName = HttpContext.Current.User.Identity.Name;
            SqlCommand cmd = new SqlCommand("INSERT INTO Rezerwacja(ID_Usera,Tytul,UserName) SELECT AspNetUsers.Id,@Tytul,Email from AspNetUsers where AspNetUsers.UserName='" + userName + "'", conn);
            SqlCommand cmd1 = new SqlCommand("Update Rezerwacja SET ID_Ksiazki=(SELECT Ksiazka.ID_Ksiazki),Autor=(Select Ksiazka.Autor),Data_Wydania=(Select Ksiazka.Data_Wydania),Wydawnictwo=(Ksiazka.Wydawnictwo) from Ksiazka where Rezerwacja.Tytul=Ksiazka.Tytul", conn);
            cmd.Parameters.AddWithValue("@Tytul", RezerwujDropDown.SelectedItem.Value);
            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            conn.Close();
            AnulujRezerwacjeDropDown.DataBind();

        }

        protected void AnulujRezerwacjeDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button3_Click(object sender, EventArgs e)//Przycisk anulujący rezerwację
        {
            conn.Open();
            String userName = HttpContext.Current.User.Identity.Name;
            SqlCommand cmd = new SqlCommand("Delete TOP(1) from Rezerwacja where Tytul=@Tytul and Rezerwacja.UserName='" + userName + "'", conn);
            cmd.Parameters.AddWithValue("@Tytul", AnulujRezerwacjeDropDown.SelectedItem.Value);
            cmd.ExecuteNonQuery();
            conn.Close();
            AnulujRezerwacjeDropDown.DataBind();

        }
    }
}
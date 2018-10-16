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
    public partial class ModyfikujDane : System.Web.UI.Page
    {
        static string connString = "Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\aspnet-Biblioteka-20170316082419.mdf;Initial Catalog=aspnet-Biblioteka-20170316082419;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connString);
        protected void Page_Load(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            String userName = HttpContext.Current.User.Identity.Name;
            cmd.CommandText = "SELECT Count(*) as ct FROM ZmodyfikowaneDane where UserName = '" + userName + "'";//sprawdzamy czy są jakieś modyfikacje przeprowadzone przez obecnego użytkownika ktore jeszcze nie zostały zaakceptowane
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                if (reader.GetInt32(reader.GetOrdinal("ct")) == 1)//jeśli są to nie można modyfikować
                {
                    Panel3.Visible = true;
                    Panel1.Visible = false;
                }
                else                                                //jeśli nie to można modyfikować
                {
                    Panel3.Visible = false;
                    Panel1.Visible = true;
                }

            }
            conn.Close();

        }

        protected void Button1_Click(object sender, EventArgs e)// przycisk którego naciśnięćie powoduje wstawienie danych z textboxów do tabeli ZmodyfikowaneDane i zmiany czekają na zatwierdzenie
        {
            
            conn.Open();
            String userName = HttpContext.Current.User.Identity.Name;
            SqlCommand cmd = new SqlCommand("INSERT INTO ZmodyfikowaneDane(User_Id,Email,UserName,Imie,Nazwisko,Data_Urodzenia) SELECT AspNetUsers.Id,AspNetUsers.Email,AspNetUsers.UserName,@Imie,@Nazwisko,@Data_Urodzenia from AspNetUsers where AspNetUsers.UserName='" + userName + "'", conn);
            cmd.Parameters.AddWithValue("@Imie", Imie.Text);
            cmd.Parameters.AddWithValue("@Nazwisko", Nazwisko.Text);
            cmd.Parameters.AddWithValue("@Data_Urodzenia", DataUrodzenia.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            Panel3.Visible = true;
            Panel1.Visible = false;
        }
    }
}
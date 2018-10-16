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
    public partial class ListaUzytkownikow : System.Web.UI.Page
    {
        static string connString = "Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\aspnet-Biblioteka-20170316082419.mdf;Initial Catalog=aspnet-Biblioteka-20170316082419;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connString);
        protected void Page_Load(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            String userName = HttpContext.Current.User.Identity.Name;
            cmd.CommandText = "SELECT Role FROM AspNetUsers WHERE username ='" + userName + "'";//pobieramy rolę dla aktualnie zalogowanego użytkownika
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                if (reader.GetString(reader.GetOrdinal("Role")) == "Administrator")// jeśli admin to zobaczy panel admina
                {
                    IfAdmin.Visible = true;
                    IfNotAdmin.Visible = false;
                }
                else                                                                //jeśli nie to dostanie informację o braku uprawnień
                {
                    IfAdmin.Visible = false;
                    IfNotAdmin.Visible = true;
                }

            }
            conn.Close();
            IfMaWypozyczenia.Visible = false;

        }

        protected void usersDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void RoleDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)//przycisk aktywujący i przydzielajacy rolę użytkownikowi wybrenmu w dropdownlist
        {
            SqlCommand cmd = new SqlCommand("UPDATE AspNetUsers SET IsActivated=1,Role=@Role where UserName=@UserName" , conn);//zmienia flagę isActivated z 0 na 1 i wstawia rolę wybraną w dropdownlist dla użytkownika wybranego w dropdownlist
            cmd.Parameters.AddWithValue("@Role", RoleDropDown.SelectedItem.Value);
            cmd.Parameters.AddWithValue("@UserName", usersDropDown.SelectedItem.Value);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            DeleteUserDropDownList.DataBind();//aktualizuje dane w dropdownlist z użytkownikami do usunięcia
            usersDropDown.DataBind();
        }

        protected void DeleteUsersDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)//przycisk służący do usuwania użytkownika wybranego w dropdownlist
        {
            conn.Open();
            SqlCommand cmd2 = conn.CreateCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.CommandText = "SELECT Count(*) as ct FROM Wypozyczenie where UserName = @UserName";// sprawdzamy czy użytkownik którego chcemy usunąć ma wypożyczenia
            cmd2.Parameters.AddWithValue("@UserName", DeleteUserDropDownList.SelectedItem.Value);
            using (SqlDataReader reader = cmd2.ExecuteReader())
            {
                reader.Read();
                if (reader.GetInt32(reader.GetOrdinal("ct")) >= 1)
                {
                    IfMaWypozyczenia.Visible = true;// jeśli ma wypożyczenia to pojawia się komunikat że użytkownik nie może zostać usunięty
                }
                else                                //jeśli nie ma to usuwamy użytkownika
                {
                    SqlCommand cmd1 = new SqlCommand("Delete From ZmodyfikowaneDane where Email=@UserName", conn);
                    SqlCommand cmd = new SqlCommand("Delete From AspNetUsers where UserName=@UserName", conn);
                    cmd.Parameters.AddWithValue("UserName", DeleteUserDropDownList.SelectedItem.Value);
                    cmd1.Parameters.AddWithValue("@UserName", DeleteUserDropDownList.SelectedItem.Value);
                    conn.Close();
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    DeleteUserDropDownList.DataBind();
                    usersDropDown.DataBind();
                    ModyfikacjaDropDown.DataBind();
                }
            }
          }

        protected void ModyfikacjaDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        

        protected void Button3_Click(object sender, EventArgs e) // przycisk po naciśnięciu ktorego admin akcpetuje modyfikację danych zrobioną przez użytkownika
        {
            SqlCommand cmd = new SqlCommand("UPDATE AspNetUsers SET Imie=(Select ZmodyfikowaneDane.Imie),Nazwisko=(Select ZmodyfikowaneDane.Nazwisko),Data_Urodzenia=(Select ZmodyfikowaneDane.Data_Urodzenia) from ZmodyfikowaneDane where AspNetUsers.Email=@Email", conn);
            SqlCommand cmd1 = new SqlCommand("Delete From ZmodyfikowaneDane where Email=@Email", conn);
            cmd.Parameters.AddWithValue("@Email", ModyfikacjaDropDown.SelectedItem.Value);
            cmd1.Parameters.AddWithValue("@Email", ModyfikacjaDropDown.SelectedItem.Value);
            conn.Open();
            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            conn.Close();
            ModyfikacjaDropDown.DataBind();
        }
       
    }
}
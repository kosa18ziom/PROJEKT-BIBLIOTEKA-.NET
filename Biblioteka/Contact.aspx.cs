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
    public partial class Contact : Page
    {
        static string connString = "Data Source=(LocalDb)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\aspnet-Biblioteka-20170316082419.mdf;Initial Catalog=aspnet-Biblioteka-20170316082419;Integrated Security=True";
        SqlConnection conn = new SqlConnection(connString);
        protected void Page_Load(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.Text;
            String userName = HttpContext.Current.User.Identity.Name;
            cmd.CommandText = "SELECT Role FROM AspNetUsers WHERE username ='" + userName + "'";//pobieramy rolę aktualnie zalogowanego użytkownia
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                reader.Read();
                if (reader.GetString(reader.GetOrdinal("Role")) == "Administrator")// jeśli to admin to zobaczy panel admina
                {
                    IfAdmin.Visible = true;
                    IfNotAdmin.Visible = false;
                }
                else                                                                // jeśli nie admin to zobaczy informację o braku uprawnień
                {
                    IfAdmin.Visible = false;
                    IfNotAdmin.Visible = true;
                }

            }
            conn.Close();
                BindGrid4();//Pokazuje gridview z aktualnymi wypożyczeniami w bibliotece
                BindGrid5();//pokazuje gridview z historią wypożyczeń w bibliotece
        }
        private void BindGrid5()//pobiera dane do gridview z Historii Wypozyczen
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ID_Zwroconej,UserName,Tytul, Autor, Data_Wydania, Wydawnictwo,Data_Wypozyczenia,Data_Zwrotu,Kara FROM Historia_Wypozyczen where UserName='" + HistoriaDropDown.SelectedValue + "'"))
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

        protected void GridView5_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView5.PageIndex = e.NewPageIndex;
            GridView5.DataBind();
        }

        protected void GridView5_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void BindGrid4()//pobiera dane do gridview z Wypozyczen
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT ID_Wypozyczenia,UserName,Tytul, Autor, Data_Wydania, Wydawnictwo,ID_Ksiazki,Data_Wypozyczenia,ZwrotDo FROM Wypozyczenie where UserName='" + AktualneDropDown.SelectedValue + "'"))
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
        protected void Button4_Click(object sender, EventArgs e)// klikniecie przycisku pokazuje historię wypozyczen
        {
            BindGrid5();
        }
        protected void Button5_Click(object sender, EventArgs e)// klikniecie przycisku pokazuje aktualne wypozyczenia
        {
            BindGrid4();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaUzytkownikow.aspx");
        }
        protected void GridView4_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView4.PageIndex = e.NewPageIndex;
            GridView4.DataBind();
            BindGrid4();

        }

        protected void GridView4_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        protected void AktualneDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void HistoriaDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
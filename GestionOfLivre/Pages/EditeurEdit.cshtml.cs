using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using GestionOfLivre.Pages.Editeur;

namespace GestionOfLivre.Pages.EditeurEdit
{
    public class EditeurEditModel : PageModel
    {
        private readonly ILogger<EditeurEditModel> _logger;
        public EditeurEditModel(ILogger<EditeurEditModel> logger)
        {
            _logger = logger;
        }

        public EditeurInfo Editeurinfo = new EditeurInfo();

        public void OnGet()
        {
            string IDEditeur = Request.Query["IDEditeur"];
            try
            {
                string connectionString = "Data Source=DESKTOP-4GIP9VN\\sqlexpress;Initial Catalog=GestionDeLivre;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "select * from Editeur where IDEditeur=@IDEditeur";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@IDEditeur", IDEditeur);
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                Editeurinfo.IDEditeur = rd.GetInt32(0);
                                Editeurinfo.NomEditeur = rd.GetString(1);
                                Editeurinfo.DescripEditeur = rd.GetString(2);
                                Editeurinfo.EmailEdit = rd.GetString(3);
                                Editeurinfo.telephonrEdi = rd.GetString(4);
                                Editeurinfo.AddressEdit = rd.GetString(5);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching data for editing the editor.");
            }
        }

        public void OnPost()
        {
            Editeurinfo.IDEditeur = Convert.ToInt32(Request.Form["IDEditeur"]);
            Editeurinfo.NomEditeur = Request.Form["NomEditeur"];
            Editeurinfo.DescripEditeur = Request.Form["DescripEditeur"];
            Editeurinfo.EmailEdit = Request.Form["EmailEdit"];
            Editeurinfo.telephonrEdi = Request.Form["telephonrEdi"];
            Editeurinfo.AddressEdit = Request.Form["AddressEdit"];

            try
            {
                string connectionString = "Data Source=DESKTOP-4GIP9VN\\sqlexpress;Initial Catalog=GestionDeLivre;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sql = "update Editeur set NomEditeur=@NomEditeur, DescripEditeur=@DescripEditeur, EmailEdit=@EmailEdit, telephonrEdi=@telephonrEdi, AddressEdit=@AddressEdit where IDEditeur=@IDEditeur";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@NomEditeur", Editeurinfo.NomEditeur);
                        cmd.Parameters.AddWithValue("@DescripEditeur", Editeurinfo.DescripEditeur);
                        cmd.Parameters.AddWithValue("@EmailEdit", Editeurinfo.EmailEdit);
                        cmd.Parameters.AddWithValue("@telephonrEdi", Editeurinfo.telephonrEdi);
                        cmd.Parameters.AddWithValue("@AddressEdit", Editeurinfo.AddressEdit);
                        cmd.Parameters.AddWithValue("@IDEditeur", Editeurinfo.IDEditeur);

                        cmd.ExecuteNonQuery();
                        con.Close();
                        _logger.LogInformation("Update operation completed successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the update operation.");
            }
            Response.Redirect("/Editeur");
        }
    }
}

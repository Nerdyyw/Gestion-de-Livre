using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using GestionOfLivre.Pages.Auteur;

namespace GestionOfLivre.Pages.Auteur.AuteurEdit
{
    public class AuteurEditModel : PageModel
    {
        private readonly ILogger<AuteurEditModel> _logger;
        public AuteurEditModel(ILogger<AuteurEditModel> logger)
        {
            _logger = logger;
        }

        public AuteurInfo Auteurinfo = new AuteurInfo();

        public void OnGet()
        {
            string IDAuteur = Request.Query["IDAuteur"];
            try
            {
                string connectionString = "Data Source=DESKTOP-4GIP9VN\\sqlexpress;Initial Catalog=GestionDeLivre;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "select * from Auteur where IDAuteur=@IDAuteur";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@IDAuteur", IDAuteur);
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                Auteurinfo.IDAuteur = rd.GetInt32(0);
                                Auteurinfo.NomAuteur = rd.GetString(1);
                                Auteurinfo.EmailAuteur = rd.GetString(2);
                                Auteurinfo.telephoneAut = rd.GetString(3);
                                Auteurinfo.AdressAut = rd.GetString(4);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching data for editing the author.");
            }
        }

        public void OnPost()
        {
            Auteurinfo.IDAuteur = Convert.ToInt32(Request.Form["IDAuteur"]);
            Auteurinfo.NomAuteur = Request.Form["NomAuteur"];
            Auteurinfo.EmailAuteur = Request.Form["EmailAuteur"];
            Auteurinfo.telephoneAut = Request.Form["telephoneAut"];
            Auteurinfo.AdressAut = Request.Form["AdressAut"];

            try
            {
                string connectionString = "Data Source=DESKTOP-4GIP9VN\\sqlexpress;Initial Catalog=GestionDeLivre;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sql = "update Auteur set NomAuteur=@NomAuteur, EmailAuteur=@EmailAuteur, telephoneAut=@telephoneAut, AdressAut=@AdressAut where IDAuteur=@IDAuteur";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@NomAuteur", Auteurinfo.NomAuteur);
                        cmd.Parameters.AddWithValue("@EmailAuteur", Auteurinfo.EmailAuteur);
                        cmd.Parameters.AddWithValue("@telephoneAut", Auteurinfo.telephoneAut);
                        cmd.Parameters.AddWithValue("@AdressAut", Auteurinfo.AdressAut);
                        cmd.Parameters.AddWithValue("@IDAuteur", Auteurinfo.IDAuteur);

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
            Response.Redirect("/Auteur/Auteur");
        }
    }
}

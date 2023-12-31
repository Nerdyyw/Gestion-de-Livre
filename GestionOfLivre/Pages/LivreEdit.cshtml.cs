using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;
using GestionOfLivre.Pages.Livre;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GestionOfLivre.Pages.LivreEdit
{
    public class LivreEditModel : PageModel
    {
        private readonly ILogger<LivreEditModel> _logger;

        public LivreEditModel(ILogger<LivreEditModel> logger)
        {
            _logger = logger;
        }

        public LivreInfo Livreinfo = new LivreInfo();
        public List<SelectListItem> Editeurs { get; set; }
        public List<SelectListItem> Auteurs { get; set; }
        public List<SelectListItem> Categories { get; set; }

        public void OnGet()
        {
            string IDLivre = Request.Query["IDLivre"];

            // Fetch Livreinfo data
            try
            {
                string connectionString = "Data Source=DESKTOP-4GIP9VN\\sqlexpress;Initial Catalog=GestionDeLivre;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "select * from Livre where IDLivre=@IDLivre";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@IDLivre", IDLivre);
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                Livreinfo.IDLivre = rd.GetInt32(0);
                                Livreinfo.Titre = rd.GetString(1);
                                Livreinfo.SPM = rd.GetString(2);
                                Livreinfo.IDEditeur = rd.GetInt32(3);
                                Livreinfo.IDAuteur = rd.GetInt32(4);
                                Livreinfo.IDCat = rd.GetInt32(5);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching data for editing the book.");
            }

            // Fetch Editeurs data for the dropdown
            Editeurs = FetchDropdownData("Editeur", "IDEditeur");

            // Fetch Auteurs data for the dropdown
            Auteurs = FetchDropdownData("Auteur", "IDAuteur");

            // Fetch Categories data for the dropdown
            Categories = FetchDropdownData("Categorie", "IDCat");
        }

        public void OnPost()
        {
            Livreinfo.IDLivre = Convert.ToInt32(Request.Form["IDLivre"]);
            Livreinfo.Titre = Request.Form["Titre"];
            Livreinfo.SPM = Request.Form["SPM"];
            Livreinfo.IDEditeur = Convert.ToInt32(Request.Form["IDEditeur"]);
            Livreinfo.IDAuteur = Convert.ToInt32(Request.Form["IDAuteur"]);
            Livreinfo.IDCat = Convert.ToInt32(Request.Form["IDCat"]);

            try
            {
                string connectionString = "Data Source=DESKTOP-4GIP9VN\\sqlexpress;Initial Catalog=GestionDeLivre;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sql = "update Livre set Titre=@Titre, SPM=@SPM, IDEditeur=@IDEditeur, IDAuteur=@IDAuteur, IDCat=@IDCat where IDLivre=@IDLivre";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Titre", Livreinfo.Titre);
                        cmd.Parameters.AddWithValue("@SPM", Livreinfo.SPM);
                        cmd.Parameters.AddWithValue("@IDEditeur", Livreinfo.IDEditeur);
                        cmd.Parameters.AddWithValue("@IDAuteur", Livreinfo.IDAuteur);
                        cmd.Parameters.AddWithValue("@IDCat", Livreinfo.IDCat);
                        cmd.Parameters.AddWithValue("@IDLivre", Livreinfo.IDLivre);

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
            Response.Redirect("/Livre");
        }

        // Helper method to fetch data for dropdowns
        private List<SelectListItem> FetchDropdownData(string tableName, string valueField)
        {
            List<SelectListItem> dropdownData = new List<SelectListItem>();

            try
            {
                string connectionString = "Data Source=DESKTOP-4GIP9VN\\sqlexpress;Initial Catalog=GestionDeLivre;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = $"select {valueField} from {tableName}";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            while (rd.Read())
                            {
                                dropdownData.Add(new SelectListItem
                                {
                                    Value = rd[valueField].ToString(),
                                    Text = rd[valueField].ToString()  // Utilisez la même valeur pour le texte
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while fetching data for {tableName} dropdown.");
            }

            return dropdownData;
        }
    }
}

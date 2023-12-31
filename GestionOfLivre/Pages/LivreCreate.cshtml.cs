using GestionOfLivre.Pages.Livre;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace GestionOfLivre.Pages.LivreCreate
{
    public class LivreCreateModel : PageModel
    {
        private readonly ILogger<LivreCreateModel> _logger; // Injection du service de journalisation

        public LivreCreateModel(ILogger<LivreCreateModel> logger)
        {
            _logger = logger;
        }
        public LivreInfo Livreinfo = new LivreInfo();
        public string errorMessage = "";
        public string SuccessMessage = "";

        public List<SelectListItem> Editeurs { get; set; }
        public List<SelectListItem> Auteurs { get; set; }
        public List<SelectListItem> Categories { get; set; }


        public void OnGet()
        {
            // Remplir les listes déroulantes avec les valeurs des clés étrangères
            Editeurs = GetDropDownList("Editeur", "IDEditeur", "NomEditeur");
            Auteurs = GetDropDownList("Auteur", "IDAuteur", "NomAuteur");
            Categories = GetDropDownList("Categorie", "IDCat", "NomCat");
        }

        public void OnPost()
        {
            Livreinfo.Titre = Request.Form["Titre"];
            Livreinfo.SPM = Request.Form["SPM"];
            Livreinfo.IDEditeur = Convert.ToInt32(Request.Form["IDEditeur"]);
            Livreinfo.IDAuteur = Convert.ToInt32(Request.Form["IDAuteur"]);
            Livreinfo.IDCat = Convert.ToInt32(Request.Form["IDCat"]);

            if (string.IsNullOrEmpty(Livreinfo.Titre) || string.IsNullOrEmpty(Livreinfo.SPM))
            {
                errorMessage = "Les champs Titre et SPM sont obligatoires";
                return;
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-4GIP9VN\\sqlexpress;Initial Catalog=GestionDeLivre;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sql = "INSERT INTO Livre(Titre, SPM, IDEditeur, IDAuteur, IDCat) " +
                                 "VALUES (@Titre, @SPM, @IDEditeur, @IDAuteur, @IDCat)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@Titre", Livreinfo.Titre);
                        cmd.Parameters.AddWithValue("@SPM", Livreinfo.SPM);
                        cmd.Parameters.AddWithValue("@IDEditeur", Livreinfo.IDEditeur);
                        cmd.Parameters.AddWithValue("@IDAuteur", Livreinfo.IDAuteur);
                        cmd.Parameters.AddWithValue("@IDCat", Livreinfo.IDCat);

                        cmd.ExecuteNonQuery();
                        _logger.LogInformation("Le Livre a été créé avec succès!");
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Une erreur s'est produite lors de la création du livre.");
                Console.WriteLine("Exception " + ex.ToString);
            }

            SuccessMessage = "Le Livre a été créé avec succès!";
        }
        // Méthode pour récupérer les données de la base de données et les transformer en SelectListItems
        private List<SelectListItem> GetDropDownList(string tableName, string valueField, string textField)
        {
            List<SelectListItem> items = new List<SelectListItem>();
            string connectionString = "Data Source=DESKTOP-4GIP9VN\\sqlexpress;Initial Catalog=GestionDeLivre;Integrated Security=True";

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string sql = $"SELECT {valueField}, {textField} FROM {tableName}";

                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            items.Add(new SelectListItem
                            {
                                Value = reader[valueField].ToString(),
                                Text = reader[textField].ToString()
                            });
                        }
                    }
                }
            }

            return items;
        }
    }
}

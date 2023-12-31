using GestionOfLivre.Pages.Categorie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace GestionOfLivre.Pages.CatCreate
{
    public class CatCreateModel : PageModel
    {
        public CategorieInfo Categorieinfo = new CategorieInfo();
        public string errorMessage = "";
        public string SuccessMessage = "";

        public void OnGet()
        {
            // If you have any initial logic for GET requests, you can add it here.
        }

        public void OnPost()
        {
            Categorieinfo.NomCat = Request.Form["NomCat"];
            Categorieinfo.DescriptionCat = Request.Form["DescriptionCat"];

            if (string.IsNullOrEmpty(Categorieinfo.NomCat) || string.IsNullOrEmpty(Categorieinfo.DescriptionCat))
            {
                errorMessage = "Tous les champs sont obligatoires";
                return;
            }
            try
            {
                string connectionString = "Data Source=DESKTOP-4GIP9VN\\sqlexpress;Initial Catalog=GestionDeLivre;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    string sql = "INSERT INTO Categorie(NomCat,DescriptionCat) VALUES (@NomCat, @DescriptionCat)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@NomCat", Categorieinfo.NomCat);
                        cmd.Parameters.AddWithValue("@DescriptionCat", Categorieinfo.DescriptionCat);

                        cmd.ExecuteNonQuery();
                    }
                }

                
            }
            catch (Exception ex)
            {
                // Utilisez un système de journalisation approprié ici
                Console.WriteLine("Exception " + ex.ToString);
            }
            SuccessMessage = "La Categorie a été créé avec succès!";
        }
    }
}


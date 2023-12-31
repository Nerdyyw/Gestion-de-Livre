using GestionOfLivre.Pages.Auteur;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;

namespace GestionOfLivre.Pages.Auteur.AuteurCreate
{
    public class AuteurCreateModel : PageModel
    {
        public AuteurInfo Auteurinfo = new AuteurInfo();
        public string errorMessage = "";
        public string SuccessMessage = "";

        public void OnGet()
        {
            // Si vous avez une logique initiale pour les requêtes GET, vous pouvez l'ajouter ici.
        }

        public void OnPost()
        {
            Auteurinfo.NomAuteur = Request.Form["NomAuteur"];
            Auteurinfo.EmailAuteur = Request.Form["EmailAuteur"];
            Auteurinfo.telephoneAut = Request.Form["telephoneAut"];
            Auteurinfo.AdressAut = Request.Form["AdressAut"];

            if (string.IsNullOrEmpty(Auteurinfo.NomAuteur) || string.IsNullOrEmpty(Auteurinfo.EmailAuteur) || string.IsNullOrEmpty(Auteurinfo.telephoneAut) || string.IsNullOrEmpty(Auteurinfo.AdressAut))
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

                    string sql = "INSERT INTO Auteur(NomAuteur, EmailAuteur, telephoneAut, AdressAut) " +
                                 "VALUES (@NomAuteur, @EmailAuteur, @telephoneAut, @AdressAut)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        
                        cmd.Parameters.AddWithValue("@NomAuteur", Auteurinfo.NomAuteur);
                        cmd.Parameters.AddWithValue("@EmailAuteur", Auteurinfo.EmailAuteur);
                        cmd.Parameters.AddWithValue("@telephoneAut", Auteurinfo.telephoneAut);
                        cmd.Parameters.AddWithValue("@AdressAut", Auteurinfo.AdressAut);

                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                // Utilisez un système de journalisation approprié ici
                Console.WriteLine("Exception " + ex.ToString);
            }
            SuccessMessage = "L'Auteur a été créé avec succès!";
        }
    }
}

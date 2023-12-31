using GestionOfLivre.Pages.Editeur;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace GestionOfLivre.Pages.EditeurCreate
{
    public class EditeurCreateModel : PageModel
    {
        public EditeurInfo Editeurinfo = new EditeurInfo();
        public string errorMessage = "";
        public string SuccessMessage = "";

        public void OnGet()
        {
            // Si vous avez une logique initiale pour les requêtes GET, vous pouvez l'ajouter ici.
        }

        public void OnPost()
        {
            Editeurinfo.NomEditeur = Request.Form["NomEditeur"];
            Editeurinfo.DescripEditeur = Request.Form["DescripEditeur"];
            Editeurinfo.EmailEdit = Request.Form["EmailEdit"];
            Editeurinfo.telephonrEdi = Request.Form["telephonrEdi"];
            Editeurinfo.AddressEdit = Request.Form["AddressEdit"];

            if (string.IsNullOrEmpty(Editeurinfo.NomEditeur) || string.IsNullOrEmpty(Editeurinfo.DescripEditeur) || string.IsNullOrEmpty(Editeurinfo.EmailEdit) || string.IsNullOrEmpty(Editeurinfo.telephonrEdi)|| string.IsNullOrEmpty(Editeurinfo.AddressEdit))
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

                    string sql = "INSERT INTO Editeur(NomEditeur, DescripEditeur, EmailEdit, telephonrEdi, AddressEdit) " +
                                 "VALUES (@NomEditeur, @DescripEditeur, @EmailEdit, @telephonrEdi, @AddressEdit)";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@NomEditeur", Editeurinfo.NomEditeur);
                        cmd.Parameters.AddWithValue("@DescripEditeur", Editeurinfo.DescripEditeur);
                        cmd.Parameters.AddWithValue("@EmailEdit", Editeurinfo.EmailEdit);
                        cmd.Parameters.AddWithValue("@telephonrEdi", Editeurinfo.telephonrEdi);
                        cmd.Parameters.AddWithValue("@AddressEdit", Editeurinfo.AddressEdit);

                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                // Utilisez un système de journalisation approprié ici
                Console.WriteLine("Exception " + ex.ToString);
            }
            SuccessMessage = "L'Editeur a été créé avec succès!";
        }
    }
}

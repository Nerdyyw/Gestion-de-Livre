using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace GestionOfLivre.Pages.Categorie
{
    public class CategorieModel : PageModel
    {
        public List<CategorieInfo> listCategorie = new List<CategorieInfo>();
        public void OnGet()
        {
            // connection vers la base de données
            try
            {
                string connectionString = "Data Source=DESKTOP-4GIP9VN\\SQLEXPRESS;Initial Catalog = GestionDeLivre; Integrated Security = True";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from Categorie";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    CategorieInfo Categorieinf = new CategorieInfo();
                    Categorieinf.IDCat = rd.GetInt32(0);
                    Categorieinf.NomCat = rd.GetString(1);
                    Categorieinf.DescriptionCat = rd.GetString(2);
                    listCategorie.Add(Categorieinf);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }
        }
    }
    public class CategorieInfo
    {
        public int IDCat;
        public string? NomCat;
        public string? DescriptionCat;
    }
}

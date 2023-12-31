using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace GestionOfLivre.Pages.Livre
{
    public class LivreModel : PageModel
    {
        public List<LivreInfo> listLivre = new List<LivreInfo>();
        public void OnGet()
        {
            // connection vers la base de données
            try
            {
                string connectionString = "Data Source=DESKTOP-4GIP9VN\\SQLEXPRESS;Initial Catalog = GestionDeLivre; Integrated Security = True";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from Livre";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    LivreInfo Livreinf = new LivreInfo();
                    Livreinf.IDLivre = rd.GetInt32(0);
                    Livreinf.Titre = rd.GetString(1);
                    Livreinf.SPM = rd.GetString(2);
                    Livreinf.IDEditeur = rd.GetInt32(3);
                    Livreinf.IDAuteur = rd.GetInt32(4);
                    Livreinf.IDCat = rd.GetInt32(5);
                    listLivre.Add(Livreinf);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }
        }
    }
    public class LivreInfo
    {
        public int IDLivre;
        public string? Titre;
        public string? SPM;
        public int IDEditeur;
        public int IDAuteur;
        public int IDCat;
    }
}

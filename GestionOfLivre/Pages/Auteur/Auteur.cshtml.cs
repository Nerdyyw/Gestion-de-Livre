using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace GestionOfLivre.Pages.Auteur
{
    public class AuteurModel : PageModel
    {
        public List<AuteurInfo> listAuteur = new List<AuteurInfo>();
        public void OnGet()
        {
            // connection vers la base de données
            try
            {
                string connectionString = "Data Source=DESKTOP-4GIP9VN\\SQLEXPRESS;Initial Catalog = GestionDeLivre; Integrated Security = True";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from Auteur";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    AuteurInfo Auteurinf = new AuteurInfo();
                    Auteurinf.IDAuteur = rd.GetInt32(0);
                    Auteurinf.NomAuteur = rd.GetString(1);
                    Auteurinf.EmailAuteur = rd.GetString(2);
                    Auteurinf.telephoneAut = rd.GetString(3);
                    Auteurinf.AdressAut = rd.GetString(4);
                    listAuteur.Add(Auteurinf);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }
        }
    }
    public class AuteurInfo
    {
        public int IDAuteur;
        public string? NomAuteur;
        public string? EmailAuteur;
        public string? telephoneAut;
        public string? AdressAut;

    }
}

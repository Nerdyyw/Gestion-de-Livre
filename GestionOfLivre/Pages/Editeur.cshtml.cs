using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
namespace GestionOfLivre.Pages.Editeur
{
    public class EditeurModel : PageModel
    {
        public List<EditeurInfo> listEditeur = new List<EditeurInfo>();
        public void OnGet()
        {
            // connection vers la base de données
            try
            {
                string connectionString = "Data Source=DESKTOP-4GIP9VN\\SQLEXPRESS;Initial Catalog = GestionDeLivre; Integrated Security = True";
                SqlConnection con = new SqlConnection(connectionString);
                con.Open();
                string sql = "select * from Editeur";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    EditeurInfo Editeurinf = new EditeurInfo();
                    Editeurinf.IDEditeur = rd.GetInt32(0);
                    Editeurinf.NomEditeur = rd.GetString(1);
                    Editeurinf.DescripEditeur = rd.GetString(2);
                    Editeurinf.EmailEdit = rd.GetString(3);
                    Editeurinf.telephonrEdi = rd.GetString(4);
                    Editeurinf.AddressEdit = rd.GetString(5);
                    listEditeur.Add(Editeurinf);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception " + ex.ToString());
            }
        }
    }
    public class EditeurInfo
    {
        public int IDEditeur;
        public string? NomEditeur;
        public string? DescripEditeur;
        public string? EmailEdit;
        public string? telephonrEdi;
        public string? AddressEdit;
    }
}

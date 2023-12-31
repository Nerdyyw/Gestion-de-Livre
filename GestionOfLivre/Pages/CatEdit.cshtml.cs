using GestionOfLivre.Pages.Categorie;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Logging;


namespace GestionOfLivre.Pages.CatEdit
{
    public class CatEditModel : PageModel
    {
        private readonly ILogger<CatEditModel> _logger;
        public CatEditModel(ILogger<CatEditModel> logger)
        {
            _logger = logger;
        }

        public CategorieInfo Categorieinfo = new CategorieInfo();

        public void OnGet()
        {
            string IDCat = Request.Query["IDCat"];
            try
            {
                string connectionString = "Data Source=DESKTOP-4GIP9VN\\sqlexpress;Initial Catalog=GestionDeLivre;Integrated Security=True";

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sql = "select * from Categorie where IDCat=@IDCat";
                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@IDCat", IDCat);
                        using (SqlDataReader rd = cmd.ExecuteReader())
                        {
                            if (rd.Read())
                            {
                                Categorieinfo.IDCat = rd.GetInt32(0);
                                Categorieinfo.NomCat = rd.GetString(1);
                                Categorieinfo.DescriptionCat = rd.GetString(2);

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching data for editing the category.");
            }
        }
        public void OnPost()
        {
            Categorieinfo.IDCat = Convert.ToInt32(Request.Form["IDCat"]);
            Categorieinfo.NomCat = Request.Form["NomCat"];
            Categorieinfo.DescriptionCat = Request.Form["DescriptionCat"];
        
            try
            {
                string connectionString = "Data Source=DESKTOP-4GIP9VN\\sqlexpress;Initial Catalog=GestionDeLivre;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                { 
                    con.Open();


                    string sql = "update Categorie set NomCat=@NomCat,DescriptionCat=@DescriptionCat where IDCat=@IDCat";

                    using (SqlCommand cmd = new SqlCommand(sql, con))
                    {
                        cmd.Parameters.AddWithValue("@NomCat", Categorieinfo.NomCat);
                        cmd.Parameters.AddWithValue("@DescriptionCat", Categorieinfo.DescriptionCat);
                        cmd.Parameters.AddWithValue("@IDCat", Categorieinfo.IDCat);


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
            Response.Redirect("/Categorie");


        }
    }
}



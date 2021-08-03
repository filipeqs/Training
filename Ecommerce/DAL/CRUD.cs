using Ecommerce.Models;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Ecommerce.DAL
{
    public class CRUD
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["EcomContext"].ToString());

        public Category FetchCategory(int? id)
        {
            var data = new DataTable();
            var command = new SqlCommand("spFetchCategory", con);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
            var adapter = new SqlDataAdapter(command);
            adapter.Fill(data);

            var category = new Category
            {
                Id = id.Value,
                Name = data.Rows[0]["name"].ToString(),
                Description = data.Rows[0]["Description"].ToString(),
                PhotoPath = data.Rows[0]["PhotoPath"].ToString(),
            };

            return category;
        }
    }
}
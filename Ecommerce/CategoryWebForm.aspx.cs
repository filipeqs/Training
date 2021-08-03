using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecommerce
{
    public partial class CategoryWebForm : System.Web.UI.Page
    {
        EcomContext contect = new EcomContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCategories.DataSource = contect.Categories.ToList();
                ddlCategories.DataTextField = "name";
                ddlCategories.DataValueField = "id";
                ddlCategories.DataBind();
            }
        }

        protected void ProductBtn_Click(object sender, EventArgs e)
        {
            Menu.SetActiveView(ProductView);
        }

        protected void CategoryBtn_Click(object sender, EventArgs e)
        {
            Menu.SetActiveView(CategoryView);
        }
    }
}
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ecommerce
{
    public partial class ProductGridView : System.Web.UI.Page
    {
        EcomContext context = new EcomContext();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGrid();
            }
        }

        public void FillGrid()
        {
            ProductsView.DataSource = context.Products.ToList();
            ProductsView.DataBind();
        }

        protected void ProductsView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            ProductsView.EditIndex = e.NewEditIndex;
            FillGrid();
        }

        protected void ProductsView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            ProductsView.EditIndex = -1;
            FillGrid();
        }

        protected void ProductsView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label labelId = (Label)ProductsView.Rows[e.RowIndex].FindControl("lblID");
            var id = int.Parse(labelId.Text);
            var product = context.Products.SingleOrDefault(c => c.Id == id);
            context.Products.Remove(product);
            context.SaveChanges();
            FillGrid();
        }

        protected void ProductsView_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label labelId = (Label)ProductsView.Rows[e.RowIndex].FindControl("lblID");
            TextBox name = (TextBox)ProductsView.Rows[e.RowIndex].FindControl("txtName");
            TextBox categoryId = (TextBox)ProductsView.Rows[e.RowIndex].FindControl("txtCategoryId");

            var product = new Product
            {
                Id = int.Parse(labelId.Text),
                Name = name.Text,
                CategoryId = int.Parse(categoryId.Text)
            };

            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges();

            ProductsView.EditIndex = -1;
            FillGrid();
        }
    }
}
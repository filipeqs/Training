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
    public partial class GrtidViewExample : System.Web.UI.Page
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
            gvCategories.DataSource = context.Categories.ToList();
            gvCategories.DataBind();
        }

        protected void gvCategoris_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCategories.EditIndex = e.NewEditIndex;
            FillGrid();
        }

        protected void gvCategoris_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCategories.EditIndex = -1;
            FillGrid();
        }

        protected void gvCategoris_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            Label labelId = (Label)gvCategories.Rows[e.RowIndex].FindControl("lblID");
            var id = int.Parse(labelId.Text);
            var category = context.Categories.SingleOrDefault(c => c.Id == id);
            context.Categories.Remove(category);
            context.SaveChanges();
            FillGrid();
        }

        protected void gvCategoris_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Label labelId = (Label)gvCategories.Rows[e.RowIndex].FindControl("lblID");
            TextBox name = (TextBox)gvCategories.Rows[e.RowIndex].FindControl("txtName");
            TextBox description = (TextBox)gvCategories.Rows[e.RowIndex].FindControl("txtDescription");

            var category = new Category
            {
                Id = int.Parse(labelId.Text),
                Name = name.Text,
                Description = description.Text
            };

            context.Entry(category).State = EntityState.Modified;
            context.SaveChanges();

            gvCategories.EditIndex = -1;
            FillGrid();
        }

        protected void gvCategoris_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Insert")
            {
                TextBox name = (TextBox)gvCategories.FooterRow.FindControl("txtName");
                TextBox description = (TextBox)gvCategories.FooterRow.FindControl("txtDescription");

                var category = new Category
                {
                    Name = name.Text,
                    Description = description.Text
                };

                context.Categories.Add(category);
                context.SaveChanges();

                FillGrid();
            }
        }
    }
}
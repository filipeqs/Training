<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CategoryWebForm.aspx.cs" Inherits="Ecommerce.CategoryWebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:DropDownList ID="ddlCategories" runat="server"></asp:DropDownList>
        <asp:CheckBoxList ID="CheckBoxList1" runat="server"></asp:CheckBoxList>
        <asp:Button ID="ProductBtn" runat="server" Text="Product" OnClick="ProductBtn_Click" />
        <asp:Button ID="CategoryBtn" runat="server" Text="Category" OnClick="CategoryBtn_Click" />
        <asp:MultiView ID="Menu" runat="server">
            <asp:View ID="ProductView" runat="server">
                Product
            </asp:View>
            <asp:View ID="CategoryView" runat="server">
                Category
            </asp:View>
        </asp:MultiView>
    </form>
</body>
</html>

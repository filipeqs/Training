<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="WebFormExample.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    Server Side <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    Client Side <input type="text" />
    <asp:Button ID="Button1" runat="server" Text="Button" />
    <asp:DropDownList ID="DropDownList1" runat="server">
        <asp:ListItem Selected="True">Select</asp:ListItem>
        <asp:ListItem>USA</asp:ListItem>
        <asp:ListItem>Brasil</asp:ListItem>
    </asp:DropDownList>
</asp:Content>

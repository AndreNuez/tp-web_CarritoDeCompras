<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="TPWeb.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="text-center">
        <h2>Productos en Carrito</h2>
    </div>
    <div class="col">
        <asp:GridView ID="dgvCarrito" runat="server" CssClass="table">

        </asp:GridView>
    </div>
</asp:Content>

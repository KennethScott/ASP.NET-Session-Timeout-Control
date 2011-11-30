<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationMenu.ascx.cs" Inherits="Demo.UserControls.NavigationMenu" %>
<asp:Menu ID="NavigationMenu1" runat="server" CssClass="menu" EnableViewState="false" IncludeStyleBlock="false" Orientation="Horizontal">
    <Items>
        <asp:MenuItem NavigateUrl="~/ColorBox.aspx" Text="ColorBox"/>
        <asp:MenuItem NavigateUrl="~/Div.aspx" Text="Div"/>
        <asp:MenuItem NavigateUrl="~/EasyUI.aspx" Text="EasyUI"/>
        <asp:MenuItem NavigateUrl="~/jQueryUI.aspx" Text="jQueryUI"/>
        <asp:MenuItem NavigateUrl="~/SimpleModal.aspx" Text="SimpleModal"/>
        <asp:MenuItem NavigateUrl="~/ThickBox.aspx" Text="ThickBox"/>
    </Items>
</asp:Menu>

<%@ Page Title="" Language="C#" MasterPageFile="~/jQueryUI.Master" AutoEventWireup="true" CodeBehind="jQueryUiMultipleDialogs.aspx.cs" Inherits="Demo.jQueryUiMultipleDialogs" %>
<%@ MasterType VirtualPath="~/jQueryUI.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ph1" runat="server">

    <h2>
        Welcome to the Timeout Demo app!
    </h2>
    <p>
        To learn more about the Timeout control, visit the project page on <a href="https://github.com/kennethscott/ASP.NET-Session-Timeout-Control" title="GitHub">GitHub</a>.
    </p>  
    
    <div id="somediv"></div>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#somediv").load("StandalonePage.aspx").dialog({ modal: true, autoOpen: true });
        });
    </script>
</asp:Content>

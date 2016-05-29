<%@ Page Title="" Language="C#" MasterPageFile="~/Div.Master" AutoEventWireup="true" CodeBehind="Div.aspx.cs" Inherits="Demo.Div1" %>
<%@ MasterType VirtualPath="~/Div.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ph1" runat="server">

    <h2>
        Welcome to the Timeout Demo app!
    </h2>
    <p>
        To learn more about the Timeout control, visit the project page on <a href="https://github.com/kennethscott/ASP.NET-Session-Timeout-Control" title="GitHub">GitHub</a>.
    </p>  

    <div>Wait until the warning div becomes visible and then click this Reset button to demonstrate Resetting the control externally (in this case from a content page)</div>
    <input id="Button1" type="button" value="Reset" onclick="resetMe();" />

    <div>Or you can click the jQuery Reset button and demonstrate Resetting the control (also externally) via jQuery instead of $find</div>
    <input id="Button2" type="button" value="jQuery Reset" onclick="jQueryResetMe();" />

    <script type="text/javascript">
        function resetMe() {
            $find('<%= Master.TimeoutControlClientId %>').reset();
        }
        function jQueryResetMe() {
            // a little uglier but still works..
            $('#<%=Master.TimeoutControlClientId%>')[0].control.reset();
        } 
    </script>

</asp:Content>

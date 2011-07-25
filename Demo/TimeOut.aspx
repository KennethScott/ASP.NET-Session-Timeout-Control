<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeOut.aspx.cs" Inherits="Demo.TimeOut" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding-top: 100px; text-align: center;">
            Timed Out
        </div>
        <script type="text/javascript">
            TEST = {};
            TEST.myClass = function () {
                this.Kenneth = "testing";
                var parent = this;
                function something() {
                    alert(parent.Kenneth);
                }
                this.somepublic = function () {
                    alert(this.Kenneth);
                }
            };
            var xxx = new TEST.myClass();
            //xxx.somepublic();
        </script>
    </form>
</body>
</html>

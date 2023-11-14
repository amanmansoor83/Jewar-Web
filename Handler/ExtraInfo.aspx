<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExtraInfo.aspx.cs" Inherits="Broadway_New.Handler.ExtraInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <div class="abc" id="abcd">


        <%=ItemOptions %>
    </div>
    

    
    <div class="cartbutton" id="Div11111">


        <%=CartButton %>
    </div>
    
    <div class="cartbuttonmobile" id="Div222222">


        <%=CartButtonMobile %>
    </div>

    
    <div class="dvdeliverytime1" id="Div1"> 
        <%=DeliveryTime %>
    </div>
    
    <div class="dvOperationalHour" id="Div2"> 
        <%=OperationalHour %>
    </div>



    
    <div class="dvAreas" id="Div3">
      
         
            <%=Areas %>
         
    </div>


</body>
</html>

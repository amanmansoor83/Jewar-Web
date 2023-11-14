<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreditCard.aspx.cs" Inherits="Jewar.Handler.CreditCard" %>


<%--<form id="payment_form" action="https://testsecureacceptance.cybersource.com/pay" method="post">--%>
<form id="payment_form" action="https://secureacceptance.cybersource.com/pay" method="post">
    <%
        IDictionary<string, string> parameters = new Dictionary<string, string>();
        //reference_number = GenerateRandom();
        parameters.Add("access_key", access_key);
        parameters.Add("profile_id", profile_id);
        parameters.Add("transaction_uuid", NewGUID);
        parameters.Add("bill_to_address_country", bill_to_address_country);
        parameters.Add("signed_field_names", signed_field_names);
        parameters.Add("unsigned_field_names", unsigned_field_names);
        parameters.Add("signed_date_time", signed_date_time);
        parameters.Add("locale", locale);
        parameters.Add("transaction_type", transaction_type);
        parameters.Add("reference_number", reference_number);
        parameters.Add("amount", amount);
        parameters.Add("currency", currency);
        parameters.Add("bill_to_phone", bill_to_phone);
        parameters.Add("bill_to_address_line1", bill_to_address_line1);
        parameters.Add("bill_to_address_city", bill_to_address_city);
        parameters.Add("bill_to_email", bill_to_email);
        parameters.Add("bill_to_forename", bill_to_forename);
        parameters.Add("bill_to_surname", bill_to_surname);        
        parameters.Add("customer_ip_address", customer_ip_address);
        parameters.Add("consumer_id", consumer_id);
        parameters.Add("merchant_defined_data1", merchant_defined_data1);
        parameters.Add("merchant_defined_data2", merchant_defined_data2);
        parameters.Add("merchant_defined_data3", merchant_defined_data3);
        parameters.Add("merchant_defined_data4", merchant_defined_data4);
        parameters.Add("merchant_defined_data5", merchant_defined_data5);
        parameters.Add("merchant_defined_data6", merchant_defined_data6);
        parameters.Add("merchant_defined_data7", merchant_defined_data7);
        parameters.Add("merchant_defined_data8", merchant_defined_data8);
        parameters.Add("merchant_defined_data20", merchant_defined_data20);
        //parameters.Add("payment_token", payment_token);
        parameters.Add("device_fingerprint_id", transaction_uuid);


        parameters.Add("ship_to_address_city", ship_to_address_city);
        parameters.Add("ship_to_address_country", ship_to_address_country);
        parameters.Add("ship_to_address_line1", ship_to_address_line1);
        parameters.Add("ship_to_address_postal_code", ship_to_address_postal_code);
        parameters.Add("ship_to_address_state", ship_to_address_state);
        parameters.Add("ship_to_forename", ship_to_forename);
        parameters.Add("ship_to_phone", ship_to_phone);
        parameters.Add("ship_to_surname", ship_to_surname);
        parameters.Add("ship_to_email", ship_to_email);
         //parameters.Add("submit", submit);      
    %>

    

    <input type="hidden" id="access_key" name="access_key" value="<% Response.Write(access_key); %>">
    <input type="hidden" id="profile_id" name="profile_id" value="<% Response.Write(profile_id); %>">
    <input type="hidden" id="transaction_uuid" name="transaction_uuid" value="<% Response.Write(NewGUID); %>">    
    <input type="hidden" id="bill_to_address_country" name="bill_to_address_country" value="<% Response.Write(bill_to_address_country); %>">
    <input type="hidden" id="signed_field_names" name="signed_field_names" value="<% Response.Write(signed_field_names); %>">
    <input type="hidden" id="unsigned_field_names" name="unsigned_field_names" value="<% Response.Write(unsigned_field_names); %>">
    <input type="hidden" id="signed_date_time" name="signed_date_time" value="<% Response.Write(signed_date_time); %>">
    <input type="hidden" id="locale" name="locale" value="<% Response.Write(locale); %>">
    <input type="hidden" id="transaction_type" name="transaction_type" value="<% Response.Write(transaction_type); %>">
    <input type="hidden" id="reference_number" name="reference_number" value="<% Response.Write(reference_number); %>">
    <input type="hidden" id="amount" name="amount" value="<% Response.Write(amount); %>">
    <input type="hidden" id="currency" name="currency" value="<% Response.Write(currency); %>">

    
    <input type="hidden" id="bill_to_phone" name="bill_to_phone" value="<% Response.Write(bill_to_phone); %>">
    <input type="hidden" id="bill_to_address_line1" name="bill_to_address_line1" value="<% Response.Write(bill_to_address_line1); %>">
    <input type="hidden" id="bill_to_address_city" name="bill_to_address_city" value="<% Response.Write(bill_to_address_city); %>">
    <input type="hidden" id="bill_to_email" name="bill_to_email" value="<% Response.Write(bill_to_email); %>">
    <input type="hidden" id="bill_to_forename" name="bill_to_forename" value="<% Response.Write(bill_to_forename); %>">
    <input type="hidden" id="bill_to_surname" name="bill_to_surname" value="<% Response.Write(bill_to_surname); %>">
    <input type="hidden" id="customer_ip_address" name="customer_ip_address" value="<% Response.Write(customer_ip_address); %>">
    <input type="hidden" id="consumer_id" name="consumer_id" value="<% Response.Write(consumer_id); %>">
    <input type="hidden" id="merchant_defined_data1" name="merchant_defined_data1" value="<% Response.Write(merchant_defined_data1); %>">
    <input type="hidden" id="merchant_defined_data2" name="merchant_defined_data2" value="<% Response.Write(merchant_defined_data2); %>">
    <input type="hidden" id="merchant_defined_data3" name="merchant_defined_data3" value="<% Response.Write(merchant_defined_data3); %>">
    <input type="hidden" id="merchant_defined_data4" name="merchant_defined_data4" value="<% Response.Write(merchant_defined_data4); %>">
    <input type="hidden" id="merchant_defined_data5" name="merchant_defined_data5" value="<% Response.Write(merchant_defined_data5); %>">
    <input type="hidden" id="merchant_defined_data6" name="merchant_defined_data6" value="<% Response.Write(merchant_defined_data6); %>">
    <input type="hidden" id="merchant_defined_data7" name="merchant_defined_data7" value="<% Response.Write(merchant_defined_data7); %>">
    <input type="hidden" id="merchant_defined_data8" name="merchant_defined_data8" value="<% Response.Write(merchant_defined_data8); %>">
    <input type="hidden" id="merchant_defined_data20" name="merchant_defined_data20" value="<% Response.Write(merchant_defined_data20); %>">
    
    <%--<input type="hidden" id="payment_token" name="payment_token" value="<% Response.Write(payment_token); %>">--%>

    
    <input type="hidden" id="ship_to_address_city" name="ship_to_address_city" value="<% Response.Write(ship_to_address_city); %>">
    <input type="hidden" id="ship_to_address_country" name="ship_to_address_country" value="<% Response.Write(ship_to_address_country); %>">
    <input type="hidden" id="ship_to_address_line1" name="ship_to_address_line1" value="<% Response.Write(ship_to_address_line1); %>">
    <input type="hidden" id="ship_to_address_postal_code" name="ship_to_address_postal_code" value="<% Response.Write(ship_to_address_postal_code); %>">
    <input type="hidden" id="ship_to_address_state" name="ship_to_address_state" value="<% Response.Write(ship_to_address_state); %>">
    <input type="hidden" id="ship_to_forename" name="ship_to_forename" value="<% Response.Write(ship_to_forename); %>">
    <input type="hidden" id="ship_to_phone" name="ship_to_phone" value="<% Response.Write(ship_to_phone); %>">
    <input type="hidden" id="ship_to_surname" name="ship_to_surname" value="<% Response.Write(ship_to_surname); %>">
    <input type="hidden" id="ship_to_email" name="ship_to_email" value="<% Response.Write(ship_to_email); %>">

    <p style="background: url(https://h.online-metrix.net/fp/clear.png?org_id=k8vif92e&amp;session_id=hbl_unitedking<% Response.Write(transaction_uuid); %>&amp; m=1 )"></p>
    <img src=" https://h.online-metrix.net/fp/clear.png?org_id=k8vif92e&amp;session_id=hbl_unitedking<% Response.Write(transaction_uuid); %>&amp;m=2" alt="">

    <object type="application/x-shockwave-flash" data="https://h.online-metrix.net/fp/fp.swf?org_id=k8vif92e&amp;session_id=hbl_unitedking<% Response.Write(transaction_uuid); %>" width="1" height="1" id="thm_fp">
        <param name="movie" value="https://h.online-metrix.net/fp/fp.swf?org_id=k8vif92e&amp;session_id=hbl_unitedking<% Response.Write(transaction_uuid); %>">
        <div></div>
    </object>

    <script src="https://h.online-metrix.net/fp/check.js?org_id=k8vif92e&amp;session_id=hbl_unitedking<% Response.Write(transaction_uuid); %>" type="text/javascript"> </script>


    <input type="hidden" id="device_fingerprint_id" name="device_fingerprint_id" value="<% Response.Write(transaction_uuid); %>">



    <input type="hidden" id="signature" name="signature" value="<%Response.Write(secureacceptance.Security.sign(parameters)); %>">


    <%
        int aaa = Jewar.CodeLibrary.DBHandler.InsertDataWithID(string.Format("update orders set signature = '{0}' where id ='{1}' ", secureacceptance.Security.sign(parameters), Request.QueryString["OrderID"]));

         %>

    <%--<input type="submit" id="submit1" name="submit" value="Submit" />--%>
</form>

<script type="text/javascript" language="JavaScript">
    document.forms[0].submit();
</script>

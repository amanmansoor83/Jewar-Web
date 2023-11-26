<%@ Page Language="C#"  MasterPageFile="~/Agent/Agent.Master" AutoEventWireup="true" CodeBehind="Message.aspx.cs" Inherits="Jewar_API.Message" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
<div class="row mb40">
  <div class="col-lg-6 col-xl-5 col-xxl-4">
    <div class="message_container">
      <div class="inbox_user_list">
        <div class="iu_heading pr35">
          <div class="chat_user_search">
            <form class="d-flex align-items-center">
              <button class="btn" type="submit"><span class="flaticon-search"></span></button>
              <input class="form-control" type="search" placeholder="Serach" aria-label="Search">
            </form>
          </div>
        </div>
        <div class="chat-member-list pr20">

    <asp:Repeater ID="rptMessages" runat="server" OnItemCommand="rptMessages_ItemCommand">
        <ItemTemplate>
            <div class="list-item pt5">
               <%-- <a href="#" CommandName="update">--%>
                    <asp:LinkButton   ID="lnkUpdate" CssClass="btn btn-primary" ForeColor="White"  runat="server" CommandName="update" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>'>Update


                    <div class="d-flex align-items-center position-relative">
                        <img class="img-fluid float-start rounded-circle mr10" src="<%#Eval("Image") %>" alt="ms1.png">
                        <div class="d-sm-flex">
                            <div class="d-inline-block">
                                <div class="fz14 fw600 dark-color ff-heading mb-0"><%#Eval("FirstName") + " " + Eval("LastName") %></div>
                                <p class="preview"><%#Eval("Position") %></p>
                            </div>
                            <div class="iul_notific">
                                <small><%#Eval("Created") %> mins</small>
                            </div>
                        </div>
                    </div>
                        </asp:LinkButton>
               <%-- </a>--%>
            </div>
        </ItemTemplate>
    </asp:Repeater>

 
        </div>
      </div>
    </div>
  </div>
  <div class="col-lg-6 col-xl-7 col-xxl-8">
    <div class="message_container mt30-md">
      <div class="user_heading px-0 mx30">
        <div class="wrap">
          <span class="contact-status online"></span>
          <img class="img-fluid mr10" src="images/inbox/ms3.png" alt="ms3.png">
          <div class="meta d-sm-flex justify-content-sm-between align-items-center">
            <div class="authors">
              <h6 class="name mb-0">Arlene McCoy</h6>
              <p class="preview">Active</p>
            </div>
            <div>
              <a class="text-decoration-underline fz14 fw600 dark-color ff-heading" href="#">Delete Conversation</a>
            </div>
          </div>
        </div>
      </div>
      <div class="inbox_chatting_box" style="">
        <ul class="chatting_content">
             <asp:Repeater ID="rtpMessages" runat="server">
     <ItemTemplate>


         
            <%if(Convert.ToInt32(Eval("SenderID")) > 0)
                {%>

          <li class="sent float-start">
            <div class="d-flex align-items-center mb15">
              <img class="img-fluid rounded-circle align-self-start mr10" src="images/inbox/ms4.png" alt="ms4.png">
              <div class="title fz14"><%#Eval("SenderID") %> <small class="ml10"><%#Eval("Created") %> mins</small></div>
            </div>
            <p><%#Eval("Message") %></p>
          </li>
         <%}else{ %>

          <li class="reply float-end">
            <div class="d-flex align-items-center justify-content-end mb15">
              <div class="title fz14"><small class="mr10"><%#Eval("Created") %> mins</small> You</div>
              <img class="img-fluid rounded-circle align-self-end ml10" src="images/inbox/ms5.png" alt="ms5.png">
            </div>
            <p><%#Eval("Message") %></p>
          </li>
         <%} %>

                </ItemTemplate>
</asp:Repeater>


             
          
        </ul>
      </div>
      <div class="mi_text">
        <div class="message_input">
          <form class="d-flex align-items-center">
            <input class="form-control" type="search" placeholder="Type a Message" aria-label="Search">
            <button class="btn ud-btn btn-thm">Send Message<i class="fal fa-arrow-right-long"></i></button>
          </form>
        </div>
      </div>
    </div>
  </div>
</div>

    </asp:Content>
﻿<%@ Page Language="C#"  MasterPageFile="~/Agent/Agent.Master" AutoEventWireup="true" CodeBehind="MyProperties.aspx.cs" Inherits="Jewar_API.MyProperties" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
 <div class="row align-items-center pb40">
   <div class="col-xxl-3">
     <div class="dashboard_title_area">
       <h2>My Properties</h2>
       <p class="text">We are glad to see you again!</p>
     </div>
   </div>
   <div class="col-xxl-9">
     <div class="dashboard_search_meta d-md-flex align-items-center justify-content-xxl-end">
       <div class="item1 mb15-sm">
         <div class="search_area">
           <input type="text" class="form-control bdrs12" placeholder="Search">
           <label><span class="flaticon-search"></span></label>
         </div>
       </div>
       <div class="page_control_shorting bdr1 bdrs12 py-2 ps-3 pe-2 mx-1 mx-xxl-3 bgc-white mb15-sm maxw140">
         <div class="pcs_dropdown d-flex align-items-center"><span class="title-color">Sort by:</span>
           <select class="selectpicker show-tick">
             <option>New</option>
             <option>Best Seller</option>
             <option>Best Match</option>
             <option>Price Low</option>
             <option>Price High</option>
           </select>
         </div>
       </div>
       <a href="AddProperty" class="ud-btn btn-thm">Add New Property<i class="fal fa-arrow-right-long"></i></a>
     </div>
   </div>
 </div>
 <div class="row">
   <div class="col-xl-12">
     <div class="ps-widget bgc-white bdrs12 default-box-shadow2 p30 mb30 overflow-hidden position-relative">
       <div class="packages_table table-responsive">
         <table class="table-style3 table at-savesearch">
           <thead class="t-head">
             <tr>
               <th scope="col">Listing title</th>
               <th scope="col">Date Published</th>
               <th scope="col">Status</th>
               <th scope="col">View</th>
               <th scope="col">Action</th>
             </tr>
           </thead>
           <tbody class="t-body">
     <asp:Repeater ID="rptProperties" runat="server">
         <ItemTemplate>
             <tr>
                 <th scope="row">
                     <div class="listing-style1 dashboard-style d-xxl-flex align-items-center mb-0">
                         <div class="list-thumb">
                             <img class="w-100" src="images/listings/list-1.jpg" alt="">
                         </div>
                         <div class="list-content py-0 p-0 mt-2 mt-xxl-0 ps-xxl-4">
                             <div class="h6 list-title"><a href="page-property-single-v1.html"><%#Eval("Title") %></a></div>
                             <p class="list-text mb-0"><%#Eval("City") %>, <%#Eval("State") %>, <%#Eval("Country") %></p>
                             <div class="list-price"><a href="#">$<%#Eval("Price") %>/<span>mo</span></a></div>
                         </div>
                     </div>
                 </th>
                 <td class="vam"><%#Eval("Created") %></td>
                 <td class="vam"><span class="pending-style style1"><%#Eval("Status") %></span></td>
                 <td class="vam"><%#Eval("Created") %></td>
                 <td class="vam">
                     <div class="d-flex">
                         <a href="#" class="icon" data-bs-toggle="tooltip" data-bs-placement="top" title="Edit"><span class="fas fa-pen fa"></span></a>
                         <a href="#" class="icon" data-bs-toggle="tooltip" data-bs-placement="top" title="Delete"><span class="flaticon-bin"></span></a>
                     </div>
                 </td>
             </tr>
         </ItemTemplate>
     </asp:Repeater>
           </tbody>
         </table>
         <div class="mbp_pagination text-center mt30">
           <ul class="page_navigation">
             <li class="page-item">
               <a class="page-link" href="#"> <span class="fas fa-angle-left"></span></a>
             </li>
             <li class="page-item"><a class="page-link" href="#">1</a></li>
             <li class="page-item active" aria-current="page">
               <a class="page-link" href="#">2 <span class="sr-only">(current)</span></a>
             </li>
             <li class="page-item"><a class="page-link" href="#">3</a></li>
             <li class="page-item"><a class="page-link" href="#">4</a></li>
             <li class="page-item"><a class="page-link" href="#">5</a></li>
             <li class="page-item"><a class="page-link" href="#">...</a></li>
             <li class="page-item"><a class="page-link" href="#">20</a></li>
             <li class="page-item">
               <a class="page-link" href="#"><span class="fas fa-angle-right"></span></a>
             </li>
           </ul>
           <p class="mt10 pagination_page_count text-center">1 – 20 of 300+ property available</p>
         </div>
       </div>
     </div>
   </div>
 </div>

    </asp:Content>
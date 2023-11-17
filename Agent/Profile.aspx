<%@ Page Language="C#"  MasterPageFile="~/Agent/Agent.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Jewar_API.Profile" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row align-items-center pb40">
        <div class="col-lg-12">
            <div class="dashboard_title_area">
                <h2>My Profile</h2>
                <p class="text">We are glad to see you again!</p>
            </div>
        </div>
    </div>
     

    <div class="row">
  <div class="col-xl-12">
    <div class="ps-widget bgc-white bdrs12 default-box-shadow2 p30 mb30 overflow-hidden position-relative">
      <div class="col-xl-7">
        <div class="profile-box position-relative d-md-flex align-items-end mb50">
          <div class="profile-img position-relative overflow-hidden bdrs12 mb20-sm">
            <img class="w-100" src="images/listings/profile-1.jpg" alt="">
            <a href="#" class="tag-del" data-bs-toggle="tooltip" data-bs-placement="top" title="" data-bs-original-title="Delete Image" aria-label="Delete Item"><span class="fas fa-trash-can"></span></a>
          </div>
          <div class="profile-content ml30 ml0-sm">
            <a href="#" class="ud-btn btn-white2 mb30">Upload Profile Files<i class="fal fa-arrow-right-long"></i></a>
               <label class="ud-btn btn-white">Browse Files<input id="fuPropertyImages" type="file" multiple="" class="ud-btn btn-white" style="display: none;"></label>


            <p class="text">Photos must be JPEG or PNG format and least 2048x768</p>
          </div>
        </div>
      </div>
      <div class="col-lg-12">
        <form class="form-style1">
          <div class="row">
            <div class="col-sm-6 col-xl-4">
              <div class="mb20">
                <label class="heading-color ff-heading fw600 mb10">Username</label>
                <input type="text" class="form-control" placeholder="Your Name">
              </div>
            </div>
            <div class="col-sm-6 col-xl-4">
              <div class="mb20">
                <label class="heading-color ff-heading fw600 mb10">Email</label>
                <input type="email" class="form-control" placeholder="Your Name">
              </div>
            </div>
            <div class="col-sm-6 col-xl-4">
              <div class="mb20">
                <label class="heading-color ff-heading fw600 mb10">Phone</label>
                <input type="text" class="form-control" placeholder="Your Name">
              </div>
            </div>
            <div class="col-sm-6 col-xl-4">
              <div class="mb20">
                <label class="heading-color ff-heading fw600 mb10">First Name</label>
                <input type="text" class="form-control" placeholder="Your Name">
              </div>
            </div>
            <div class="col-sm-6 col-xl-4">
              <div class="mb20">
                <label class="heading-color ff-heading fw600 mb10">Last Name</label>
                <input type="text" class="form-control" placeholder="Your Name">
              </div>
            </div>
            <div class="col-sm-6 col-xl-4">
              <div class="mb20">
                <label class="heading-color ff-heading fw600 mb10">Position</label>
                <input type="text" class="form-control" placeholder="Your Name">
              </div>
            </div>
            <div class="col-sm-6 col-xl-4">
              <div class="mb20">
                <label class="heading-color ff-heading fw600 mb10">Language</label>
                <input type="text" class="form-control" placeholder="Your Name">
              </div>
            </div>
            <div class="col-sm-6 col-xl-4">
              <div class="mb20">
                <label class="heading-color ff-heading fw600 mb10">Company Name</label>
                <input type="text" class="form-control" placeholder="Your Name">
              </div>
            </div>
            <div class="col-sm-6 col-xl-4">
              <div class="mb20">
                <label class="heading-color ff-heading fw600 mb10">Tax Number</label>
                <input type="text" class="form-control" placeholder="Your Name">
              </div>
            </div>
            <div class="col-xl-12">
              <div class="mb20">
                <label class="heading-color ff-heading fw600 mb10">Address</label>
                <input type="text" class="form-control" placeholder="Your Name">
              </div>
            </div>
            <div class="col-md-12">
              <div class="mb10">
                <label class="heading-color ff-heading fw600 mb10">About me</label>
                <textarea cols="30" rows="4" placeholder="There are many variations of passages."></textarea>
              </div>
            </div>
            <div class="col-md-12">
              <div class="text-end">
                <a class="ud-btn btn-dark" href="page-contact.html">Update Profile<i class="fal fa-arrow-right-long"></i></a>
              </div>
            </div>
          </div>
        </form>
      </div>
    </div>
    <div class="ps-widget bgc-white bdrs12 default-box-shadow2 p30 mb30 overflow-hidden position-relative">
      <h4 class="title fz17 mb30">Social Media</h4>
      <form class="form-style1">
        <div class="row">
          <div class="col-sm-6 col-xl-4">
            <div class="mb20">
              <label class="heading-color ff-heading fw600 mb10">Facebook Url</label>
              <input type="text" class="form-control" placeholder="Your Name">
            </div>
          </div>
          <div class="col-sm-6 col-xl-4">
            <div class="mb20">
              <label class="heading-color ff-heading fw600 mb10">Pinterest Url</label>
              <input type="text" class="form-control" placeholder="Your Name">
            </div>
          </div>
          <div class="col-sm-6 col-xl-4">
            <div class="mb20">
              <label class="heading-color ff-heading fw600 mb10">Instagram Url</label>
              <input type="text" class="form-control" placeholder="Your Name">
            </div>
          </div>
          <div class="col-sm-6 col-xl-4">
            <div class="mb20">
              <label class="heading-color ff-heading fw600 mb10">Twitter Url</label>
              <input type="text" class="form-control" placeholder="Your Name">
            </div>
          </div>
          <div class="col-sm-6 col-xl-4">
            <div class="mb20">
              <label class="heading-color ff-heading fw600 mb10">Linkedin Url</label>
              <input type="text" class="form-control" placeholder="Your Name">
            </div>
          </div>
          <div class="col-sm-6 col-xl-4">
            <div class="mb20">
              <label class="heading-color ff-heading fw600 mb10">Website Url (without http)</label>
              <input type="text" class="form-control" placeholder="Your Name">
            </div>
          </div>
          <div class="col-md-12">
            <div class="text-end">
              <a class="ud-btn btn-dark" href="page-contact.html">Update Social<i class="fal fa-arrow-right-long"></i></a>
            </div>
          </div>
        </div>
      </form>
    </div>
    <div class="ps-widget bgc-white bdrs12 default-box-shadow2 p30 mb30 overflow-hidden position-relative">
      <h4 class="title fz17 mb30">Change password</h4>
      <form class="form-style1">
        <div class="row">
          <div class="col-sm-6 col-xl-4">
            <div class="mb20">
              <label class="heading-color ff-heading fw600 mb10">Old Password</label>
              <input type="text" class="form-control" placeholder="Your Name">
            </div>
          </div>
        </div>
        <div class="row">
          <div class="col-sm-6 col-xl-4">
            <div class="mb20">
              <label class="heading-color ff-heading fw600 mb10">New Password</label>
              <input type="text" class="form-control" placeholder="Your Name">
            </div>
          </div>
          <div class="col-sm-6 col-xl-4">
            <div class="mb20">
              <label class="heading-color ff-heading fw600 mb10">Confirm New Password</label>
              <input type="text" class="form-control" placeholder="Your Name">
            </div>
          </div>
          <div class="col-md-12">
            <div class="text-end">
              <a class="ud-btn btn-dark" href="page-contact.html">Change Password<i class="fal fa-arrow-right-long"></i></a>
            </div>
          </div>
        </div>
      </form>
    </div>
  </div>
</div>

    </asp:Content>
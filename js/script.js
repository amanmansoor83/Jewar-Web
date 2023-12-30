(function($) {
    
    "use strict";
    function preloaderLoad() {
        if($('.preloader').length){
            $('.preloader').delay(200).fadeOut(300);
        }
        $(".preloader_disabler").on('click', function() {
            $("#preloader").hide();
        });
    }

    /* ----- Navbar Scroll To Fixed ----- */
    function navbarScrollfixed() {
        $('.navbar-scrolltofixed').scrollToFixed();
        var summaries = $('.summary');
        summaries.each(function(i) {
            var summary = $(summaries[i]);
            var next = summaries[i + 1];
            summary.scrollToFixed({
                marginTop: $('.navbar-scrolltofixed').outerHeight(true) + 10,
                limit: function() {
                    var limit = 0;
                    if (next) {
                        limit = $(next).offset().top - $(this).outerHeight(true) - 10;
                    } else {
                        limit = $('.footer').offset().top - $(this).outerHeight(true) - 10;
                    }
                    return limit;
                },
                zIndex: 999
            });
        });
    }

    /** Main Menu Custom Script Start **/
    $(document).on('ready', function() {
        $("#respMenu").aceResponsiveMenu({
            resizeWidth: '768', // Set the same in Media query
            animationSpeed: 'fast', //slow, medium, fast
            accoridonExpAll: false //Expands all the accordion menu on click
        });
    });    

    function mobileNavToggle() {
        if ($('#main-nav-bar .navbar-nav .sub-menu').length) {
            var subMenu = $('#main-nav-bar .navbar-nav .sub-menu');
            subMenu.parent('li').children('a').append(function () {
                return '<button class="sub-nav-toggler"> <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span> <span class="icon-bar"></span> <span class="icon-bar"></span> </button>';
            });
            var subNavToggler = $('#main-nav-bar .navbar-nav .sub-nav-toggler');
            subNavToggler.on('click', function () {
                var Self = $(this);
                Self.parent().parent().children('.sub-menu').slideToggle();
                return false;
            });

        };
    }

    /* ----- This code for menu ----- */
    $(window).on('scroll', function() {
        if ($('.scroll-to-top').length) {
            var strickyScrollPos = 100;
            if ($(window).scrollTop() > strickyScrollPos) {
                $('.scroll-to-top').fadeIn(500);
            } else if ($(this).scrollTop() <= strickyScrollPos) {
                $('.scroll-to-top').fadeOut(500);
            }
        };
        if ($('.stricky').length) {
            var headerScrollPos = $('.header-navigation').next().offset().top;
            var stricky = $('.stricky');
            if ($(window).scrollTop() > headerScrollPos) {
                stricky.removeClass('slideIn animated');
                stricky.addClass('stricky-fixed slideInDown animated');
            } else if ($(this).scrollTop() <= headerScrollPos) {
                stricky.removeClass('stricky-fixed slideInDown animated');
                stricky.addClass('slideIn animated');
            }
        };
    });
    
    $(".mouse_scroll").on('click', function() {
        $('html, body').animate({
            scrollTop: $("#feature-property, #property-city").offset().top
        }, 1000);
    });
    /** Main Menu Custom Script End **/
    
    /* ----- Blog innerpage sidebar according ----- */
    $(document).on('ready', function() {
        $('.collapse').on('show.bs.collapse', function () {
            $(this).siblings('.card-header').addClass('active');
        });
        $('.collapse').on('hide.bs.collapse', function () {
            $(this).siblings('.card-header').removeClass('active');
        });
        
    });
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function(tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    })

    /* ----- Menu Cart Button Dropdown ----- */
    $(document).on('ready', function() {
        // Loop through each nav item
        $('.cart_btn').children('ul.cart').children('li').each(function(indexCount){
            // loop through each dropdown, count children and apply a animation delay based on their index value
            $(this).children('ul.dropdown_content').children('li').each(function(index){
                // Turn the index value into a reasonable animation delay
                var delay = 0.1 + index*0.03;
                // Apply the animation delay
                $(this).css("animation-delay", delay + "s")
            });
        });
    });

    /* ----- Mobile Nav ----- */
    document.addEventListener('DOMContentLoaded', () => {
      new Mmenu("#menu", {
        navbar: {
        title: "Menu"
      },
      searchfield: {
        add: false,
        addTo: "#contacts"
      },
      offCanvas: {
        position: "left-front"
      }},
      {});        
    });

    /* ----- Custom Search Dropdown Script Start ----- */
    var showSuggestions = function() {
      $( ".advance-search-field form.form-search .box-search" ).each(function() {
        $( "form.form-search .box-search input" ).on('focus', (function() {
          $(this).closest('.boxed').children('.overlay').css({
            opacity: '1',
            display: 'block'
          });
          $(this).parent('.box-search').children('.search-suggestions').css({
            opacity: '1',
            visibility: 'visible',
            top: '55px'
          });
        }));
        $( "form.form-search .box-search input" ).on('blur', (function() {
          $(this).closest('.boxed').children('.overlay').css({
            opacity: '0',
            display: 'block'
          });
          $(this).parent('.box-search').children('.search-suggestions').css({
            opacity: '0',
            visibility: 'hidden',
            top: '100px'
          });
        }));
      });
    };
    $(function() {
      showSuggestions();
    });
    /* ----- Custom Search Dropdown Script End ----- */
    

    /* ----- Price Range Slider Desktop Style ----- */
    $(document).on('ready', function() {
        $(".slider-range").slider({
            range: true,
            min: 0,
            max: 100000,
            values: [ 20, 70987 ],
            slide: function( event, ui ) {
                $( ".amount"  ).val( ui.values[ 0 ] );
                $( ".amount2"  ).val( ui.values[ 1 ] );
            }
        });
        $(".amount").change(function() {
            $(".slider-range").slider('values',0,$(this).val());
        });
        $(".amount2").change(function() {
            $(".slider-range").slider('values',1,$(this).val());
        });
    });


    /* ----- MagnificPopup ----- */
    if (($(".popup-img").length > 0) || ($(".popup-iframe").length > 0) || ($(".popup-img-single").length > 0)) {
        $(".popup-img").magnificPopup({
            type:"image",
            gallery: {
                enabled: true,
            }
        });
        $(".popup-img-single").magnificPopup({
            type:"image",
            gallery: {
                enabled: false,
            }
        });
        $('.popup-iframe').magnificPopup({
            disableOn: 700,
            type: 'iframe',
            preloader: false,
            fixedContentPos: false
        });
        $('.popup-youtube, .popup-vimeo, .popup-gmaps').magnificPopup({
            disableOn: 700,
            type: 'iframe',
            mainClass: 'mfp-fade',
            removalDelay: 160,
            preloader: false,
            fixedContentPos: false
        });
    };

    /*** ====  Right Side Hidden Sidebar Start ==== ***/
    //Side Content Toggle
    if($('.filter-btn-right').length){
      //Show Form
      $('.filter-btn-right').on('click', function(e) {
        e.preventDefault();
        $('body').addClass('signin-hidden-sidebar-content');
      });
      //Hide Form
      $('.sidebar-close-icon,.hiddenbar-body-ovelay').on('click', function(e) {
        e.preventDefault();
        $('body').removeClass('signin-hidden-sidebar-content');
      });
    }
    if($('.filter-btn-left').length){
      //Show Form
      $('.filter-btn-left').on('click', function(e) {
        e.preventDefault();
        $('body').addClass('menu-hidden-sidebar-content');
      });
      //Hide Form
      $('.sidebar-close-icon,.hiddenbar-body-ovelay').on('click', function(e) {
        e.preventDefault();
        $('body').removeClass('menu-hidden-sidebar-content');
      });
    }
    /*** ====  Right Side Hidden Sidebar END ==== ***/

    /*** ====  Perspective Hover Animation Code Start ==== ***/
    var perspectiveHover = function() {
      var $animate_content               = $('.animate_content'),
          $animate_thumb          = $('.animate_thumb'),
          xAngle              = 0,
          yAngle              = 0,
          zValue              = 0,
          xSensitivity        = 15,
          ySensitivity        = 25,
          restAnimSpeed       = 300,
          perspective         = 500;

      $animate_content.on('mousemove', function(element) {
          var $item = $(this),
              // Get cursor coordinates
              XRel = element.pageX - $item.offset().left,
              YRel = element.pageY - $item.offset().top,
              width = $item.width();

          // Build angle val from container width and cursor value
          xAngle = (0.5 - (YRel / width)) * xSensitivity;
          yAngle = -(0.5 - (XRel / width)) * ySensitivity;

          // Container isn't manipulated, only child elements within
          updateElement($item.children($animate_thumb));
      }); 
      // Move element around
      function updateElement(modifyLayer) {
          modifyLayer.css({
              'transform': 'perspective('+ perspective + 'px) translateZ(' + zValue + 'px) rotateX(' + xAngle + 'deg) rotateY(' + yAngle + 'deg)',
              'transition': 'none'
          });
      }
      // Reset element to default state
      $animate_content.on('mouseleave', function() {
          modifyLayer = $(this).children($animate_thumb);
          modifyLayer.css({
              'transform': 'perspective(' + perspective + 'px) translateZ(0) rotateX(0) rotateY(0)',
              'transition': 'transform ' + restAnimSpeed + 'ms linear'
          });
      });
    };
    perspectiveHover();
    /*** ====  Perspective Hover Animation Code End ==== ***/

    /* ----- Scroll To top ----- */
    function scrollToTop() {
        var btn = $('.scrollToHome');
        $(window).on('scroll', function () {
            if ($(window).scrollTop() > 300) {
                btn.addClass('show');
            } else {
                btn.removeClass('show');
            }
        });
        btn.on('click', function (e) {
            e.preventDefault();
            $('html, body').animate({
                scrollTop: 0
            }, '300');
        });
    }

    $(".mouse_scroll").on('click', function() {
      $('html, body').animate({
          scrollTop: $("#explore-property").offset().top
      }, 1200);
    });
    
    /* ----- Mega Dropdown Content ----- */
    $(document).on('ready', function(){
        $(".drop_btn").on('click',function(){
            $(".drop_content").toggle();
        });
        $(".drop_btn2").on('click',function(){
            $(".drop_content2").toggle();
        });
        $(".drop_btn3").on('click',function(){
            $(".drop_content3").toggle();
        }); 
        $(".drop_btn4").on('click',function(){
            $(".drop_content4").toggle();
        });        
    });
    

    /*----------- Addclass Remove Class for Home 2 Accordion ----------*/
    (function( $ ) {
      $(".accordion-style1 .accordion-item, .accordion-style1.style2 .accordion-item, .accordion-style2 .accordion-item").on("click",function() {
        $(this).toggleClass( "active", 1000)
      });
    })(jQuery);


/* ======
   When document is ready, do
   ====== */
    $(document).on('ready', function() {
        // add your functions
        navbarScrollfixed();
        scrollToTop();
        mobileNavToggle();
    });
    
/* ======
   When document is loading, do
   ====== */
    // window on Load function
    $(window).on('load', function() {
        // add your functions
        preloaderLoad();
    });
    // window on Scroll function
    $(window).on('scroll', function() {
        // add your functions
    });



    $('body').on('click', '#btnSignIn', function (e) {
        e.preventDefault();

        //alert('111');

        var Validate = true;
        if ($('#txtEmail').val() == '') {
            Validate = false;
            $('#txtEmail').addClass('field-error');
        }
        if ($('#txtPassword').val() == '') {
            Validate = false;
            $('#txtPassword').addClass('field-error');
        }

        if (Validate) {
            $('#btnSignIn').html('Processing..');
            $('#btnSignIn').attr('disabled', 'true');

            var Email = $('#txtEmail').val(),
                Password = $('#txtPassword').val();

            //alert(txtEmail);

            //alert(txtPassword);



            $.ajax({
                url: '/Handler/Actions.aspx/Login',
                type: "POST",
                //data: '{"OutletID":"' + OutletID + '","OutletName":"' + outletname + '","customerName":"' + username + '","customerPhone":"' + usernum + '","DeliveryAddress":"' + deliveryaddress + '","DeliveryFee":"' + deliveryfee + '","OrderType":"' + ordertype + '","DeliveryArea":"' + DeliveryArea + '","Notes":"' + notes + '","discount":"' + outletdiscount + '","otherdiscount":"0","tax":"' + outlettax + '","deliverytime":"' + deliverytime + '","PreOrderDeliveryTime":"' + PreOrderDeliveryTime + '"}',
                data: '{"Email":"' + Email + '","Password":"' + Password + '"}',   //totalamount
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    data = $.parseJSON(data.d);
                    //alert(data);
                    // Success
                    if (data.success) {

                        window.location.href = "/Agent/Dashboard";
                        $('#btnSignIn').html('Sign in');
                        $('#btnSignIn').removeAttr('disabled');
                    }

                    else {
                        //alert(data.message);
                        //M.toast({ html: data.message });

                        alert(data.message);
                        $('#btnSignIn').html('Sign in');
                        $('#btnSignIn').removeAttr('disabled');
                    }
                }
            });


        }
        else {
            $('#btnSignIn').html('Sign in');
            $('#btnSignIn').removeAttr('disabled');
        }


    });


    $("#txtPassword").keyup(function (event) {
        if (event.keyCode === 13) {
            $("#btnSignIn").click();
        }
    });


    $("#txtPassword1").keyup(function (event) {
        if (event.keyCode === 13) {
           $("#btnSignIn1").click();
           //alert('22');
        }
    });


    $('body').on('click', '#btnHomeBuySearch', function (e) {
        e.preventDefault();

        //alert($('#txtSearchBuyText').val());
        SearchHomePage('Buy', $('#txtSearchBuyText').val());

    });
    $('body').on('click', '#btnHomeRentSearch', function (e) {
        e.preventDefault();

        //alert($('#txtSearchBuyText').val());
        SearchHomePage('Rent', $('#txtSearchRentText').val());

    });
    $('body').on('click', '#btnHomeSoldSearch', function (e) {
        e.preventDefault();

        //alert($('#txtSearchBuyText').val());
        SearchHomePage('Sold', $('#txtSearchSoldText').val());

    });

    function SearchHomePage(searchtype, searchtext) {
        //alert(searchtype);
        //alert(searchtext);


        //alert($('#chkSearchAttic').val())

        //alert($('input[name=xbeds]:checked').val());

        //'slider-range-value2'

        var Price1 = $('#slider-range-value1').html(),
            Price2 = $('#slider-range-value2').val(),
            PropertyType = $('#drpPropertyType').val(),
            PropertyID = $('#txtPropertyID').val(),
            Location = $('#drpLocation').val(),
            MinSqFeet = $('#txtMinSqFeet').val(),
            MaxSqFeet = $('#txtMaxSqFeet').val(),
            Bedrooms = $('input[name=xbeds]:checked').val(),
            Bathrooms = $('input[name=ybath]:checked').val(),
            SearchAttic = $('#chkSearchAttic').val(),
            SearchAirConditioning = $('#chkSearchAirConditioning').val(),
            SearchLawn = $('#chkSearchLawn').val(),
            SearchTVCable = $('#chkSearchTVCable').val(),
            SearchDryer = $('#chkSearchDryer').val(),
            SearchOutdoorShower = $('#chkSearchOutdoorShower').val(),
            SearchWasher = $('#chkSearchWasher').val(),
            SearchLakeview = $('#chkSearchLakeview').val(),
            SearchWinecellar = $('#chkSearchWinecellar').val(),
            SearchFrontyard = $('#chkSearchFrontyard').val(),
            SearchRefrigerator = $('#chkSearchRefrigerator').val();



      //  window.location.href = "listing";

         





        $.ajax({
            url: '/Handler/Actions.aspx/SearchHome',
            type: "POST",
            //data: '{"OutletID":"' + OutletID + '","OutletName":"' + outletname + '","customerName":"' + username + '","customerPhone":"' + usernum + '","DeliveryAddress":"' + deliveryaddress + '","DeliveryFee":"' + deliveryfee + '","OrderType":"' + ordertype + '","DeliveryArea":"' + DeliveryArea + '","Notes":"' + notes + '","discount":"' + outletdiscount + '","otherdiscount":"0","tax":"' + outlettax + '","deliverytime":"' + deliverytime + '","PreOrderDeliveryTime":"' + PreOrderDeliveryTime + '"}',
            data: '{"SearchType":"' + searchtype + '","SearchText":"' + searchtext + '","Price1":"' + Price1 + '","Price2":"' + Price2 + '","PropertyType":"' + PropertyType + '","PropertyID":"' + PropertyID + '","Location":"' + Location + '","MinSqFeet":"' + MinSqFeet + '","MaxSqFeet":"' + MaxSqFeet + '","Bedrooms": "' + Bedrooms + '","Bathrooms": "' + Bathrooms + '","SearchAttic": "' + SearchAttic + '","SearchAirConditioning": "' + SearchAirConditioning + '","SearchLawn": "' + SearchLawn + '","SearchTVCable": "' + SearchTVCable + '","SearchDryer": "' + SearchDryer + '","SearchOutdoorShower": "' + SearchOutdoorShower + '","SearchWasher": "' + SearchWasher + '","SearchLakeview": "' + SearchLakeview + '","SearchWinecellar": "' + SearchWinecellar + '","SearchFrontyard": "' + SearchFrontyard + '","SearchRefrigerator": "' + SearchRefrigerator + '"}',  
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                data = $.parseJSON(data.d);
                //alert(data);
                // Success
                if (data.success) {
                    alert(data.message);
                    window.location.href = "listing?p=" + data.message;
                    $('#btnSignIn1').html('Sign in');
                    $('#btnSignIn1').removeAttr('disabled');
                }

                else {
                    //alert(data.message);
                    //M.toast({ html: data.message });

                    alert(data.message);
                    $('#btnSignIn1').html('Sign in');
                    $('#btnSignIn1').removeAttr('disabled');
                }
            }
        });



    }



    $('body').on('click', '#btnSignIn1', function (e) {
        e.preventDefault();

        //alert('111');

        var Validate = true;
        if ($('#txtEmail1').val() == '') {
            Validate = false;
            $('#txtEmail1').addClass('field-error');
        }
        if ($('#txtPassword1').val() == '') {
            Validate = false;
            $('#txtPassword1').addClass('field-error');
        }

        if (Validate) {
            $('#btnSignIn1').html('Processing..');
            $('#btnSignIn1').attr('disabled', 'true');

            var Email = $('#txtEmail1').val(),
                Password = $('#txtPassword1').val();

            //alert(txtEmail);

            //alert(txtPassword);



            $.ajax({
                url: '/Handler/Actions.aspx/Login',
                type: "POST",
                //data: '{"OutletID":"' + OutletID + '","OutletName":"' + outletname + '","customerName":"' + username + '","customerPhone":"' + usernum + '","DeliveryAddress":"' + deliveryaddress + '","DeliveryFee":"' + deliveryfee + '","OrderType":"' + ordertype + '","DeliveryArea":"' + DeliveryArea + '","Notes":"' + notes + '","discount":"' + outletdiscount + '","otherdiscount":"0","tax":"' + outlettax + '","deliverytime":"' + deliverytime + '","PreOrderDeliveryTime":"' + PreOrderDeliveryTime + '"}',
                data: '{"Email":"' + Email + '","Password":"' + Password + '"}',   //totalamount
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    data = $.parseJSON(data.d);
                    //alert(data);
                    // Success
                    if (data.success) {

                        window.location.href = "/Agent/Dashboard";
                        $('#btnSignIn1').html('Sign in');
                        $('#btnSignIn1').removeAttr('disabled');
                    }

                    else {
                        //alert(data.message);
                        //M.toast({ html: data.message });

                        alert(data.message);
                        $('#btnSignIn1').html('Sign in');
                        $('#btnSignIn1').removeAttr('disabled');
                    }
                }
            });


        }
        else {
            $('#btnSignIn').html('Sign in');
            $('#btnSignIn').removeAttr('disabled');
        }


    });



    $("#fuPropertyImages").on('change', function () {
        $(".filearray").empty();//you can remove this code if you want previous user input
        for (let i = 0; i < this.files.length; ++i) {
            let filereader = new FileReader();
            //let $img = jQuery.parseHTML(@"<img src=''>");
            //let $img1 =  "<div class='profile-img position-relative overflow-hidden bdrs12 ml20 ml0-sm'>";

            //let $img = jQuery.parseHTML("<img class='w-100' src='' alt=''>");
            //let $img2 =  "</div>";


            // let img = "<div class='col-2'><div class='profile-img mb20 position-relative'>" ;
            // let img1 = "<img class='w-100 bdrs12 cover'  src='images/listings/profile-1.jpg' alt='Uploaded Image 1'/>";
            // let img2 = "<button class='tag-del' title='Delete Image' type='button' data-tooltip-id='delete-0' style='border: none;' fdprocessedid='6ujarj'><span class='fas fa-trash-can'></span></button></div></div>";

            let img = jQuery.parseHTML(`<div class='col-2'><div class='profile-img mb20 position-relative'><img class='w-100 bdrs12 cover'  src='' alt='Uploaded Image 1'/><button class='tag-del' title='Delete Image' id='btndeletepic' type='button' data-tooltip-id='delete-0' style='border: none;' fdprocessedid='6ujarj'><span class='fas fa-trash-can'></span></button></div></div>`);
            filereader.onload = function () {
                //img[0].src = this.result;
                //const item = $(img).find('img')[0]
                //console.log(item)
                $(img).find('img')[0].src = this.result;
            };
            filereader.readAsDataURL(this.files[i]);
            $(".filearray").append(img);

        }
    });
     
    $('body').on('click', '#btndeletepic', function (e) {
        e.preventDefault();
        //alert(1);
        //console.log(this);
        //const col2 = $(this).parent().parent();
        //console.log(col2)
        $(this).parent().parent().remove();
    });


    const s3 = new AWS.S3();
    AWS.config.update({
        accessKeyId: 'AKIA2MSTGSWEPW4ZT4U5',
        secretAccessKey: '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',
        region: 'ap-southeast-1'
    });



    $('#upload').on('click', function () {
      

        //const file = $('#fileSelect1111')[0].files[0];
        //const fileName = file.name;
        //const fileKey =  fileName; // The S3 key where you want to store the file
        ////alert(fileName + '123');
        //const params = {
        //    Bucket: 'jewarcdn',
        //    Key: fileKey,
        //    Body: file,
        //    ACL: 'public-read' // Set appropriate permissions for your use case
        //};

        //// Upload file to S3
        //s3.upload(params, function (err, data) {
        //    if (err) {
        //        console.log('Error uploading:', err);
        //    } else {
        //        console.log('Upload successful! File location:', data.Location);
        //        // Here, you can do something with the uploaded file's URL (data.Location)
        //    }
        //});


        const files = $('#fileSelect1111')[0].files;
        for (let i = 0; i < files.length; i++) {
            const file = files[i];
            const fileName = file.name;
            const fileKey =  fileName; // The S3 key where you want to store the file

            const params = {
                Bucket: 'jewarcdn',
                Key: fileKey,
                Body: file,
                ACL: 'public-read', // Set ACL to 'public-read' for public access
                ContentType: file.type
            };

            // Upload file to S3
            s3.upload(params, function (err, data) {
                if (err) {
                    console.log('Error uploading:', err);
                } else {
                    console.log('Upload successful! File location:', data.Location);
                    // Handle success - you might want to store the URLs or perform other actions
                }
            });
        }


         



    });




    $('body').on('click', '#btnSubmitProperty', function (e) {
        e.preventDefault();

       
            $('#btnSubmitProperty').html('Processing..');
            $('#btnSubmitProperty').attr('disabled', 'true');

         
        var Title = $('#txtTitle').val(),
            Description = $('#txtDescription').val(),
            Category = $('#drpCategory').val(),
            Listed = $('#drpListed').val(),
            Status = $('#drpStatus').val(),
            Price = $('#txtPrice').val(),
            YearlyTaxRate = $('#txtYearlyTaxRate').val(),
            AfterPriceLabel = $('#txtAfterPriceLabel').val(),
            VideoFrom = $('#drpVideoFrom').val(),
            EmbedVideoid = $('#txtEmbedVideoid').val(),
            VirtualTour = $('#txtVirtualTour').val(),
            Address = $('#txtAddress').val(),
            State = $('#drpState').val(),
            City = $('#drpCity').val(),
            Neighborhood = $('#txtNeighborhood').val(),
            Zip = $('#txtZip').val(),
            Country = $('#drpCountry').val(),
            Latitude = $('#txtLatitude').val(),
            Longitude = $('#txtLongitude').val(),
            Size = $('#txtSize').val(),
            LotSize = $('#txtLotSize').val(),
            Rooms = $('#txtRooms').val(),
            Bedrooms = $('#txtBedrooms').val(),
            Bathrooms = $('#txtBathrooms').val(),
            CustomID = $('#txtCustomID').val(),
            Garages = $('#txtGarages').val(),
            GarageSize = $('#txtGarageSize').val(),
            YearBuilt = $('#txtYearBuilt').val(),
            AvailableFrom = $('#txtAvailableFrom').val(),
            Basement = $('#txtBasement').val(),
            ExtraDetails = $('#txtExtraDetails').val(),
            Roofing = $('#txtRoofing').val(),
            ExteriorMaterial = $('#txtExteriorMaterial').val(),
            Structure = $('#drpStructure').val(),
            Floors = $('#drpFloors').val(),
            AgentNotes = $('#txtAgentNotes').val(),
            EnergyClass = $('#drpEnergyClass').val(),
            EnergyIndex = $('#drpEnergyIndex').val(),
            Attic = $('#chkAttic').is(':checked'),
            BasketballCourt = $('#chkBasketballCourt').is(':checked'),
            AirConditioning = $('#chkAirConditioning').is(':checked'),
            Lawn = $('#chkLawn').is(':checked'),
            SwimmingPool = $('#chkSwimmingPool').is(':checked'),
            Barbeque = $('#chkBarbeque').is(':checked'),
            Microwave = $('#chkMicrowave').is(':checked'),
            TVCable = $('#chkTVCable').is(':checked'),
            Dryer = $('#chkDryer').is(':checked'),
            OutdoorShower = $('#chkOutdoorShower').is(':checked'),
            Washer = $('#chkWasher').is(':checked'),
            Gym = $('#chkGym').is(':checked'),
            OceanView = $('#chkOceanView').is(':checked'),
            PrivateSpace = $('#chkPrivateSpace').is(':checked'),
            LakeView = $('#chkLakeView').is(':checked'),
            WineCellar = $('#chkWineCellar').is(':checked'),
            FrontYard = $('#chkFrontYard').is(':checked'),
            Refrigerator = $('#chkRefrigerator').is(':checked'),
            WiFi = $('#chkWiFi').is(':checked'),
            Laundry = $('#chkLaundry').is(':checked'),
            Sauna = $('#chkSauna').is(':checked');


        var ImageLinks = '';

        getImageURL().then(function (url) {
            console.log('Uploaded image URL:', url);
            // Use the image URL or perform further actions here

            ImageLinks = url;
        }).catch(function (error) {
            // Handle any errors here
        });



        //const files = $('#fileSelect1111')[0].files;
        //for (let i = 0; i < files.length; i++) {
        //    const file = files[i];
        //    const fileName = file.name;
        //    const fileKey = fileName; // The S3 key where you want to store the file

        //    const params = {
        //        Bucket: 'jewarcdn',
        //        Key: fileKey,
        //        Body: file,
        //        ACL: 'public-read', // Set ACL to 'public-read' for public access
        //        ContentType: file.type
        //    };

        //    // Upload file to S3
        //    s3.upload(params, function (err, data) {
        //        if (err) {
        //            console.log('Error uploading:', err);
        //        } else {
        //            console.log('Upload successful! File location:', data.Location);
        //            // Handle success - you might want to store the URLs or perform other actions

        //            ImageLinks += data.Location + ",";
        //        }
        //    });
        //}

        //alert(ImageLinks);


        $.ajax({
            url: '/Handler/Actions.aspx/AddProperty',
            type: "POST",
            //data: '{"OutletID":"' + OutletID + '","OutletName":"' + outletname + '","customerName":"' + username + '","customerPhone":"' + usernum + '","DeliveryAddress":"' + deliveryaddress + '","DeliveryFee":"' + deliveryfee + '","OrderType":"' + ordertype + '","DeliveryArea":"' + DeliveryArea + '","Notes":"' + notes + '","discount":"' + outletdiscount + '","otherdiscount":"0","tax":"' + outlettax + '","deliverytime":"' + deliverytime + '","PreOrderDeliveryTime":"' + PreOrderDeliveryTime + '"}',
        
            data: '{"Title":"' + Title + '","Description":"' + Description + '","Category":"' + Category + '","Listed":"' + Listed + '","Status":"' + Status + '","Price":"' + Price + '","YearlyTaxRate":"' + YearlyTaxRate + '","AfterPriceLabel":"' + AfterPriceLabel + '","VideoFrom":"' + VideoFrom + '","EmbedVideoid":"' + EmbedVideoid + '","VirtualTour":"' + VirtualTour + '","Address":"' + Address + '","State":"' + State + '","City":"' + City + '","Neighborhood":"' + Neighborhood + '","Zip":"' + Zip + '","Country":"' + Country + '","Latitude":"' + Latitude + '","Longitude":"' + Longitude + '","Size":"' + Size + '","LotSize":"' + LotSize + '","Rooms":"' + Rooms + '","Bedrooms":"' + Bedrooms + '","Bathrooms":"' + Bathrooms + '","CustomID":"' + CustomID + '","Garages":"' + Garages + '","GarageSize":"' + GarageSize + '","YearBuilt":"' + YearBuilt + '","AvailableFrom":"' + AvailableFrom + '","Basement":"' + Basement + '","ExtraDetails":"' + ExtraDetails + '","Roofing":"' + Roofing + '","ExteriorMaterial":"' + ExteriorMaterial + '","Structure":"' + Structure + '","Floors":"' + Floors + '","AgentNotes":"' + AgentNotes + '","EnergyClass":"' + EnergyClass + '","EnergyIndex":"' + EnergyIndex + '","Attic":"' + Attic + '","BasketballCourt":"' + BasketballCourt + '","AirConditioning":"' + AirConditioning + '","Lawn":"' + Lawn + '","SwimmingPool":"' + SwimmingPool + '","Barbeque":"' + Barbeque + '","Microwave":"' + Microwave + '","TVCable":"' + TVCable + '","Dryer":"' + Dryer + '","OutdoorShower":"' + OutdoorShower + '","Washer":"' + Washer + '","Gym":"' + Gym + '","OceanView":"' + OceanView + '","PrivateSpace":"' + PrivateSpace + '","LakeView":"' + LakeView + '","WineCellar":"' + WineCellar + '","FrontYard":"' + FrontYard + '","Refrigerator":"' + Refrigerator + '","WiFi":"' + WiFi + '","Laundry":"' + Laundry + '","Sauna":"' + Sauna + '","ImageLinks":"' + ImageLinks + '"}',


            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                data = $.parseJSON(data.d);
                //alert(data);
                // Success
                if (data.success) {
                    $('#btnSubmitProperty').html('Submit Property');
                    $('#btnSubmitProperty').removeAttr('disabled');
                    window.location.href = "/Agent/Dashboard";
                }

                else {
                    //alert(data.message);
                    M.toast({ html: data.message });


                    $('#btnSubmitProperty').html('Submit Property');
                    $('#btnSubmitProperty').removeAttr('disabled');
                }
            }
        });
    });

    // Function that needs to wait for image upload and returns image URL
    function getImageURL() {
        const files = $('#fileSelect1111')[0].files;

        for (let i = 0; i < files.length; i++) {
            // Upload image to S3 and wait for the upload to complete
            return uploadImageToS3(files[i])
                .then(function (imageURL) {
                    // Return the image URL once the image upload is complete
                    return imageURL;
                })
                .fail(function (error) {
                    // Handle errors if the image upload fails
                    console.error('Error uploading image:', error);
                    throw error; // Propagate the error
                });
        }
    }


    function uploadImageToS3(imageFile) {
        const deferred = $.Deferred();

        const s3 = new AWS.S3();
        const fileKey = imageFile.name; // Modify as needed

        const params = {
            Bucket: 'jewarcdn',
            Key: fileKey,
            Body: imageFile,
            ACL: 'public-read', // Set ACL as needed
            ContentType: imageFile.type // Set ContentType based on the uploaded file type
        };

        // Upload file to S3
        s3.upload(params, function (err, data) {
            if (err) {
                deferred.reject(err); // Reject the deferred object if there's an error
            } else {
                deferred.resolve(data.Location); // Resolve the deferred object with the uploaded file's URL
            }
        });

        return deferred.promise(); // Return the promise from the deferred object
    }
    


    $('body').on('click', '#btnUpdateProfile', function (e) {
        e.preventDefault();


        $('#btnUpdateProfile').html('Processing..');
        $('#btnUpdateProfile').attr('disabled', 'true');


        var Username = $('#txtUsername').val(),
            Email = $('#txtEmail').val(),
            FirstName = $('#txtFirstName').val(),
            LastName = $('#txtLastName').val(),
            Position = $('#txtPosition').val(),
            Language = $('#txtLanguage').val(),
            CompanyName = $('#txtCompanyName').val(),
            TaxNumber = $('#txtTaxNumber').val(),
            Address = $('#txtAddress').val(),
            Aboutme = $('#txtAboutme').val();
            //FacebookUrl = $('#txtFacebookUrl').val(),
            //PinterestUrl = $('#txtPinterestUrl').val(),
            //InstagramUrl = $('#txtInstagramUrl').val(),
            //TwitterUrl = $('#txtTwitterUrl').val(),
            //LinkedinUrl = $('#txtLinkedinUrl').val(),
            //WebsiteUrl = $('#txtWebsiteUrl').val();


        $.ajax({
            url: '/Handler/Actions.aspx/UpdateProfile',
            type: "POST",
            //data: '{"OutletID":"' + OutletID + '","OutletName":"' + outletname + '","customerName":"' + username + '","customerPhone":"' + usernum + '","DeliveryAddress":"' + deliveryaddress + '","DeliveryFee":"' + deliveryfee + '","OrderType":"' + ordertype + '","DeliveryArea":"' + DeliveryArea + '","Notes":"' + notes + '","discount":"' + outletdiscount + '","otherdiscount":"0","tax":"' + outlettax + '","deliverytime":"' + deliverytime + '","PreOrderDeliveryTime":"' + PreOrderDeliveryTime + '"}',

            data: '{"Username":"' + Username + '","Email":"' + Email + '","FirstName":"' + FirstName + '","LastName":"' + LastName + '","Position":"' + Position + '","Language":"' + Language + '","CompanyName":"' + CompanyName + '", "TaxNumber": "' + TaxNumber + '", "Address": "' + Address + '", "Aboutme": "' + Aboutme + '"}',


            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                data = $.parseJSON(data.d);
                //alert(data);
                // Success
                if (data.success) {
                    $('#btnUpdateProfile').html('Update Profile');
                    $('#btnUpdateProfile').removeAttr('disabled');
                    //window.location.href = "/Agent/Dashboard";
                }

                else {
                    //alert(data.message);
                    M.toast({ html: data.message });


                    $('#btnUpdateProfile').html('Update Profile');
                    $('#btnUpdateProfile').removeAttr('disabled');
                }
            }
        });
    });

    $('body').on('click', '#btnUpdateSocial', function (e) {
        e.preventDefault();


        $('#btnUpdateSocial').html('Processing..');
        $('#btnUpdateSocial').attr('disabled', 'true');


        var  FacebookUrl = $('#txtFacebookUrl').val(),
        PinterestUrl = $('#txtPinterestUrl').val(),
        InstagramUrl = $('#txtInstagramUrl').val(),
        TwitterUrl = $('#txtTwitterUrl').val(),
        LinkedinUrl = $('#txtLinkedinUrl').val(),
        WebsiteUrl = $('#txtWebsiteUrl').val();


        $.ajax({
            url: '/Handler/Actions.aspx/UpdateSocial',
            type: "POST",
            //data: '{"OutletID":"' + OutletID + '","OutletName":"' + outletname + '","customerName":"' + username + '","customerPhone":"' + usernum + '","DeliveryAddress":"' + deliveryaddress + '","DeliveryFee":"' + deliveryfee + '","OrderType":"' + ordertype + '","DeliveryArea":"' + DeliveryArea + '","Notes":"' + notes + '","discount":"' + outletdiscount + '","otherdiscount":"0","tax":"' + outlettax + '","deliverytime":"' + deliverytime + '","PreOrderDeliveryTime":"' + PreOrderDeliveryTime + '"}',

            data: '{"FacebookUrl":"' + FacebookUrl + '","PinterestUrl":"' + PinterestUrl + '","InstagramUrl":"' + InstagramUrl + '","TwitterUrl":"' + TwitterUrl + '","LinkedinUrl":"' + LinkedinUrl + '","WebsiteUrl":"' + WebsiteUrl + '"}',


            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                data = $.parseJSON(data.d);
                //alert(data);
                // Success
                if (data.success) {
                    $('#btnUpdateSocial').html('Update Social');
                    $('#btnUpdateSocial').removeAttr('disabled');
                    //window.location.href = "/Agent/Dashboard";
                }

                else {
                    //alert(data.message);
                    M.toast({ html: data.message });


                    $('#btnUpdateSocial').html('Update Social');
                    $('#btnUpdateSocial').removeAttr('disabled');
                }
            }
        });
    });

    $('body').on('click', '#btnChangePassword', function (e) {
        e.preventDefault();


        $('#btnChangePassword').html('Processing..');
        $('#btnChangePassword').attr('disabled', 'true');


        var OldPassword = $('#txtOldPassword').val(),
            NewPassword = $('#txtNewPassword').val(),
            ConfirmNewPassword = $('#txtConfirmNewPassword').val();


        $.ajax({
            url: '/Handler/Actions.aspx/ChangePassword',
            type: "POST",
            //data: '{"OutletID":"' + OutletID + '","OutletName":"' + outletname + '","customerName":"' + username + '","customerPhone":"' + usernum + '","DeliveryAddress":"' + deliveryaddress + '","DeliveryFee":"' + deliveryfee + '","OrderType":"' + ordertype + '","DeliveryArea":"' + DeliveryArea + '","Notes":"' + notes + '","discount":"' + outletdiscount + '","otherdiscount":"0","tax":"' + outlettax + '","deliverytime":"' + deliverytime + '","PreOrderDeliveryTime":"' + PreOrderDeliveryTime + '"}',

            data: '{"OldPassword":"' + OldPassword + '","NewPassword":"' + NewPassword + '","ConfirmNewPassword":"' + ConfirmNewPassword + '"}',


            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                data = $.parseJSON(data.d);
                //alert(data);
                // Success
                if (data.success) {
                    $('#btnChangePassword').html('Change Password');
                    $('#btnChangePassword').removeAttr('disabled');
                    //window.location.href = "/Agent/Dashboard";
                }

                else {
                    //alert(data.message);
                    M.toast({ html: data.message });


                    $('#btnChangePassword').html('Change Password');
                    $('#btnChangePassword').removeAttr('disabled');
                }
            }
        });
    });

})(window.jQuery);
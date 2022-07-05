<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dashboard_1.aspx.cs" Inherits="PrjLavalifeWebServer.dashboard"  EnableViewState="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <style type="text/css">
        .cbo-box {
            margin-top: 10px;
        }

        .radio-list td span {
            vertical-align: text-bottom;
        }

        #radGender .icr{
            margin: 15px 10px 10px 0px !important; 
        }

        #search_option .col-lg-1,
        #search_option .col-lg-2,
        #search_option .col-lg-3,
        #search_option .col-lg-4,
        #search_option .col-lg-5,
        #search_option .col-lg-6,
        #search_option .col-lg-7,
        #search_option .col-lg-7,
        #search_option .col-lg-9 {
            padding-right: 1px !important;
            padding-left: 1px !important;
        }

        .search-btn{
            margin-top:15px;
            width:100%;
        }

        #search_option label {
            font-weight: bold !important;
            color: black;
            text-transform: capitalize;
            font-size: 14px;
        }

        .selectric .label {
            padding: 7px 0px 0px 0px !important;
            margin: 0px !important;
        }


        @charset "UTF-8";

        [ng\:cloak],
        [ng-cloak],
        [data-ng-cloak],
        [x-ng-cloak],
        .ng-cloak,
        .x-ng-cloak,
        .ng-hide {
            display: none !important;
        }

        ng\:form {
            display: block;
        }

        .ng-animate-block-transitions {
            transition: 0s all !important;
            -webkit-transition: 0s all !important;
        }

        .ng-hide-add-active,
        .ng-hide-remove {
            display: block !important;
        }
    </style>

    <title>Dashboard</title>


    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=1000" />
    <link rel="shortcut icon" type="image/png" href="favicon.ico" />
    <meta name="description" content="Match interests and make connections with Lavalife’s online dating site. Browse profiles, send messages and meet new people today." />


    <!-- Styles -->
    <link href="css/finalbuild.css" rel="stylesheet" />
    <link href="css/bootstrap-tour.min.css" rel="stylesheet" />

    <script type="text/javascript" async="" src="http://cdn.mplxtms.com/s/MasterTMS.min.js#1614"></script>
    <script src="js/angular.min.js"></script>
    <script src="js/directives.js"></script>
    <script src="js/angulartics.js"></script>
    <script src="js/angulartics-ga.js"></script>
    <script src="js/dashboard.js"></script>




</head>
<body data-new-gr-c-s-check-loaded="14.1056.0" data-gr-ext-installed="" class="" style="">




    <div class="ng-scope">

        <!-- navbar -->

        <div class="navbar navbar-default navbar-static-top container ng-scope" role="navigation" style="margin-bottom: 0px; border-left: 1px solid #ccc; border-right: 1px solid #ccc;">

            <!-- header logo -->

            <div class="col-xs-3" style="padding-left: 40px;">
                <a href="dashboard.aspx" id="logo" cj-affiliate=""></a>
            </div>


            <div class="col-xs-3 test" style="padding: 0px;">

                <ul class="main-nav">

                    <!-- Home -->

                    <li>
                        <a ng-href="dashboard.aspx" cj-affiliate="" href="dashboard.html">Home</a>
                        <span></span>
                        <span></span>
                        <span></span>
                    </li>

                    <!-- Discussion -->

                    <li>
                        <a id="formLink" style="cursor: pointer;">Discussion</a>
<%--                        <span></span>
                        <span></span>
                        <span></span>--%>
                    </li>

                    <!-- Blog -->

                    <li><%--<a href="//blog.lavalife.com/" target="_blank">Blog</a>--%></li>
                </ul>

            </div>

            <form method="POST" class="forum-form ng-pristine ng-valid has-validation-callback" action="http://discussion.lavalife.com/auto_login.php" style="display: none;">
                <input type="hidden" name="jsessionId">
                <input type="hidden" name="userId">
                <input type="hidden" name="email_address">
                <input type="hidden" name="username">
                <input type="hidden" name="cj">
            </form>

            <!-- profile in navbar -->

            <div class="col-xs-3 my-nav" style="padding-left: 30px;">
                <div class="pull-left devider-horizontal"></div>
                <div class="pull-left" style="padding-left: 10px;">
                    <img class="pull-left avatar-me" ng-src="/images/pictures/1649286673290.jpeg" err-src="images/ll_profile_blank_img.png" src="/images/pictures/1649286673290.jpeg"/>

                    <p class="pull-right user-info ng-binding">
                        <asp:Literal ID="litUsername" runat="server"></asp:Literal>
                        <span class="ng-binding">23, Montreal</span></p>
                </div>

                <ul>
                    <li><a href="dashboard.html#/myProfile/" class="profile" cj-affiliate="">My Profile</a></li>
<%--                    <li><a href="dashboard.html#/account/" class="account" cj-affiliate="">My Account</a></li>
                    <li><a href="help-center.html" target="_blank" class="help">Help</a></li>--%>
                    <li><a ng-click="clearCookie();" href="LogOut.aspx" class="logout">Logout</a></li>
                </ul>

                <div class="caret" style="right: -5px;"></div>

            </div>




        </div>

    </div>

    <div class="main ng-scope">
        <section class="container container-padding ng-scope">

            <div class="clearfix"></div>
            <form runat="server">
                <div class="regular-section username-search col-lg-6">
                    <label style="margin-top: 0px;" onclick="openUsernameSearch()">USERNAME SEARCH +</label>
                </div>

                <div class="modal" data-backdrop="static" id="username-search-modal"></div>

                <div id="search_option" class="search advanced-open col-xs-12">

                    <!-- Gender -->
                    <div class="regular-section col-lg-2">
                        <label class="col-lg-4">Gender: </label>
                        <div class="col-lg-3">

                            <asp:RadioButtonList CssClass="radio-list" ID="radGender" runat="server" RepeatDirection="Horizontal" AutoPostBack="True">
                                <asp:ListItem Value="Male" Text="Male"></asp:ListItem>
                                <asp:ListItem Value="Female" Text="Female"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>

                    <!-- looking -->
                    <div class="regular-section col-lg-2">
                        <label style="text-align: center;" class="col-lg-5">Looking: </label>
                        <div class="col-lg-7" style="padding: 0px; line-height: 60px;" id="looking-for-search">
                            <div class="btn-group cbo-box">

                                <asp:DropDownList ID="cboLooking" runat="server" Height="12px" AutoPostBack="True" OnSelectedIndexChanged="search_SelectedIndexChanged">

                                    <asp:ListItem Value="">Select</asp:ListItem>

                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <%-- Age --%>
                    <div class="regular-section col-lg-2">

                        <label class="col-lg-3" style="text-align: center;">Age: </label>

                        <div class="col-lg-3" style="padding: 0px; line-height: 60px;" id="min-age-search">

                            <div class="btn-group cbo-box">

                                <asp:DropDownList ID="cboAgeFrom" runat="server" AppendDataBoundItems="True" Font-Bold="True" AutoPostBack="True" OnSelectedIndexChanged="search_SelectedIndexChanged">
                               
                                    <asp:ListItem Value="">Any</asp:ListItem>

                                    </asp:DropDownList>
                            </div>
                        </div>

                        <label class="col-lg-3" style="text-align: center; padding: 0px;">To</label>

                        <div class="col-lg-3" style="padding: 0px; line-height: 60px;" id="max-age-search">

                            <div class="btn-group cbo-box">
                                <asp:DropDownList ID="cboAgeTo" runat="server" AutoPostBack="True" OnSelectedIndexChanged="search_SelectedIndexChanged">
                                    <asp:ListItem Value="">Any</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                    <%-- Ethinicity --%>

                    <div class="regular-section col-lg-2">
                        <label style="text-align: center;" class="col-lg-6">Ethinicity: </label>
                        <div class="col-lg-6" style="padding: 0px; line-height: 60px;">
                            <div class="btn-group cbo-box">

                                <asp:DropDownList ID="cboEthinicity" runat="server" AutoPostBack="True" OnSelectedIndexChanged="search_SelectedIndexChanged">
                                    <asp:ListItem Value="">Select</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>

                    <%-- Religion --%>

                    <div class="regular-section col-lg-2">
                        <label style="text-align: center;" class="col-lg-6">Religion: </label>
                        <div class="col-lg-6" style="padding: 0px; line-height: 60px;">
                            <div class="btn-group cbo-box">

                                <asp:DropDownList ID="cboReligion" runat="server" AutoPostBack="True" OnSelectedIndexChanged="search_SelectedIndexChanged">
                                    <asp:ListItem Value="">Select</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>


                    <!-- search button -->

                    <p class="col-lg-2">
                        <!--<a href="dashboard.html#/" style="text-decoration:none;">-->

                        <asp:Button ID="btnSearch" CssClass="search-btn" runat="server" Text="Search" />
               
                    </p>




                    <div class="modal" data-backdrop="static" id="advanced-search-modal" aria-hidden="true" style="display: none;"></div>

                    <div style="position: absolute; width: 100%;">
                        <div id="username-search-popup" class="username-search-popup ng-hide" ng-show="searchUsername" style="padding-bottom: 80px;">

                            <div class="col-xs-6 col-xs-6 col-xs-6">
                                <h3>Username Search</h3>
                            </div>

                            <div class="formgroup col-xs-12" style="padding: 0 120px;">
                                <label class="col-xs-12" style="padding-left: 0px;">Username</label>
                                <div class="col-xs-12" style="padding: 0px;" id="username-search">
                                    <input name="nickname" class="col-xs-6" type="text" />
                                    <div class="col-xs-6">
                                        <div id="nickname-search" style="height: 35px; line-height: 35px;" class="redBtn save saveProfile">
                                            SEARCH
                                        <img style="position: absolute; right: 30px; top: 8px;" src="images/icons/ll_button_ic_search.png" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="clearfix"></div>

                            <div onclick="closeUsernameSearch()" class="close-modal"></div>

                        </div>
                    </div>

                </div>
            </form>
            <%--<div id="search-holder" class="col-xs-8 col-xs-8 col-xs-8" style="padding: 25px; padding-top: 0px;">--%>

            <div class="col-lg-12" style="padding: 25px; padding-top: 0px;">
                <div id="search-holder" class="col-xs-12" style="position: relative;">
                    <div ng-show="searchResults.length == 0 &amp;&amp; loadGif == false &amp;&amp; searchId != -1" style="text-align: center; margin-top: 44%;" class="ng-hide">
                        <img src="images/ll_error_ic_search_with_text.png" />
                    </div>

                    <div ng-show="searchResults.length == 0 &amp;&amp; loadGif == false &amp;&amp; searchId == -1" style="text-align: center; margin-top: 44%;" class="ng-hide">
                        <h4 style="margin-bottom: 20px;">Your previous search result has expired.</h4>
                        <button style="width: 180px; margin: auto;" class="redBtn toggle" ng-click="advancedSearch(undefined, 'Advanced Search')">REFRESH</button>
                    </div>


                    <div id="no-results" style="text-align: center; margin-top: 50%;" ng-hide="loadGif == false" class="ng-hide">
                        <img src="images/search-pre.gif" />
                    </div>





                    <asp:Repeater ID="repeatUserSearchResult" runat="server">

                        <ItemTemplate>
                            <a href="UserProfile.aspx?username=<%# Eval("username") %>">
                                <div class="user-holder col-lg-3 ng-scope">
                                    <div class="user-search col-xs-12">

                                        <div class="user-search-avatar">



                                            <%--<img src="<%# Eval("image_src") %>" alt="images/ll_profile_female_img.png" class="img-responsive" />--%>
                                            <img src="<%# Eval("ImageUrl").ToString() == "" 
                                                ? Eval("gender").ToString() == "Female" 
                                                    ? "images/ll_profile_female_img.png"
                                                    : "images/ll_profile_male_img.png"
                                                : Eval("imageurl").ToString()%>"
                                                class="img-responsive" />
                                        </div>

                                        <div class="user-name-location col-xs-12" style="padding: 0px;">

                                            <p>
                                                <%# Eval("firstname") %>&nbsp;<%# Eval("lastname")%>
                                                <%--<span class="ng-binding">address</span>--%>
                                            </p>

                                        </div>

                                    </div>
                                </div>
                            </a>
                        </ItemTemplate>

                    </asp:Repeater>


                    <div class="search-nav" ng-show="searchResults.length > 0">
                        <div class="search-left ng-hide" ng-show="prevPage > 0">
                            <a ng-click="advancedSearch( prevPage, 'Grid Previous' )"></a>
                        </div>
                        <div class="search-right ng-hide" ng-show="nextPage > 0">
                            <a ng-click="advancedSearch( nextPage, 'Grid Next' )"></a>
                        </div>
                    </div>


                </div>
            </div>
        </section>
    </div>

    <div ng-include="'partials/footer.html'" class="ng-scope">
        <style class="ng-scope">
            footer.container {
                padding: 0px;
                border-left: 1px solid #ccc;
                border-right: 1px solid #ccc;
            }

            .footer-holder {
                padding: 0px;
                position: relative;
                margin: auto;
                background-color: #f0f0f0;
                padding: 15px;
            }

                .footer-holder p {
                    color: #bdbdbd;
                    font-size: 9px;
                    margin-bottom: 0px;
                    margin-top: 20px;
                    line-height: 120%;
                    font-weight: normal;
                    text-align: center;
                }

            .footer-links p {
                text-align: center;
                background: none;
                margin-top: 0px;
            }

                .footer-links p a {
                    font-weight: 500;
                    color: #b2b2b2;
                    font-size: 14px;
                    margin: 0 10px;
                }

                    .footer-links p a:hover {
                        color: #ff0000;
                    }

        </style>


        <footer class="container ng-scope">
            <div class="footer-holder col-xs-12">

                <div class="col-xs-3">
                    <img style="margin-top: 5px;" src="images/icons/ll_logo_footer_gray.png">
                </div>

                <div class="col-xs-7">
                    <div class="footer-links col-lg-12 col-md-12 col-sm-12">
                        <p><a href="contact-us.html" target="_blank">Contact Us</a> * <a href="privacy-policy.html" target="_blank">Privacy Policy</a> * <a href="terms-of-use.html" target="_blank">Terms of Use</a> * <a href="FAQ.html" target="_blank">Help</a></p>
                    </div>
                    <p>
                        ALL IMAGES DESIGN AND OTHER INTELLECTUAL MATERIALS AND COPYRIGHTS © 2015 LAVALIFE LTD. ALL RIGHTS RESERVED. THIS IS AN ADULT SERVICE. BY SELECTING ANY OF THE OPTIONS ABOVE AND/OR CREATING YOUR LAVALIFE PROFILE, YOU ARE CONFIRMING THAT
                        YOU ARE 18 YEARS OF AGE OR OLDER. PLEASE BE SURE YOU HAVE READ AND AGREE TO OUR PRIVACY POLICY AND TERMS OF USE.
                    </p>
                </div>

                <div class="col-xs-2"></div>

            </div>

        </footer>

        <div class="chat-box col-lg-2 col-md-3 ng-scope ng-hide" ng-show="chatBoxOpen == true">

            <input type="checkbox" checked="">
            <div class="fake-label" data-expanded="Close Chatbox" data-collapsed="Open Chatbox"><span ng-click="functions.closeChatBox()" stop-event="click">X</span></div>

            <div class="chat-box-content col-lg-12 col-md-12 col-sm-12" scroll-glue="">

                <!-- ngRepeat: message in chatBoxConvo -->


                <div class="col-lg-12 col-md-12 col-sm-12 chat-send-holder">
                    <textarea class="col-lg-12 col-md-12 col-sm-12 ng-pristine ng-valid" placeholder="Reply..." ng-model="chatBoxMessage" id="reply-chatbox" autogrow="" style="overflow: hidden; overflow-wrap: break-word; height: 40px;"></textarea>
                    <div class="col-lg-4 col-md-4 col-sm-4 pull-right" style="margin-bottom: 12px; padding: 0px;">
                        <button id="reply-message" class="sign-up" style="margin: 0px;" ng-click="functions.sendChatBox(chatBoxConvo[0].userId,chatBoxMessage,chatBoxConvo[0].imageUser,avatar)">SEND</button>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <!-- jQuery (necessary for Bootstrap's JavaScript plugins) -->
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.0/jquery-ui.min.js"></script>
    <script src="//platform.twitter.com/oct.js" type="text/javascript"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/plugins.js"></script>
    <script src="js/app.js?v=2.0"></script>
    <script src="js/controllers.js?v=2.0"></script>
    <script src="js/functions.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('.multiselect').multiselect({
                numberDisplayed: 1,
                selectedClass: 'multi-active',
                maxHeight: 200
            });
            $('select:not(.multiselect)').selectric({
                disableOnMobile: false
            });
            $("input[type='radio']:not(label.radio input), input[type='checkbox']:not(label.checkbox input, .chat-box input) ").ionCheckRadio();
        });

        $("body").backstretch("images/backgrounds/lavalife_regist_welcome_fullscreen_blur.jpg");
    </script>
    <div class="backstretch" style="left: 0px; top: 0px; overflow: hidden; margin: 0px; padding: 0px; height: 625px; width: 1349px; z-index: -999999; position: fixed;">
        <img src="images/backgrounds/lavalife_regist_welcome_fullscreen_blur.jpg" style="position: absolute; margin: 0px; padding: 0px; border: none; width: 1349px; height: 758.812px; max-height: none; max-width: none; z-index: -999999; left: 0px; top: -66.9062px;">
    </div>

    <!--No User found Modal -->
    <div class="modal fade" id="no-user" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                    <h4 class="modal-title" style="text-align: center;">Sorry this user does not exist.</h4>
                </div>
                <div class="modal-footer">
                    <a style="margin: auto;" class="redBtn save" data-dismiss="modal">OKAY</a>
                </div>
            </div>
        </div>
    </div>



    <!--Account Modal -->
    <div class="modal fade" id="account-modal" tabindex="-1" role="dialog" aria-labelledby="basicModal" aria-hidden="true" style="display: none;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" style="text-align: center;"></h4>
                    <h3 style="text-align: center;">Lavalife is live!</h3>
                    We have finished our beta period and we want to thank all of you for your feedback and suggestions. As a New Year bonus we will be giving you an additional month free! Check out the new account page so that you
                    know how much time you have left, and where to go when it is time to purchase a package.

                </div>
                <div class="modal-footer">
                    <a ng-href="https://www.lavalife.com/subscription.html#membership/subscription" style="margin: auto;" class="redBtn save" href="https://www.lavalife.com/subscription.html#membership/subscription">Let’s See it!</a>
                </div>
            </div>
        </div>
    </div>





    <script src="js/cj-lavalife.js"></script>

</body>
</html>

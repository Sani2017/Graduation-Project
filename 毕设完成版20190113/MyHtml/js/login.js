// window.onload = function () {
    // outLoginBox();
    // loginBox();
    //outWorksList();
    //outActivityList();
    // $('#container').pinto();
    //登录模块效果
    // $(".xs_login_mark").on('click',
    //     function loginStyle(){
    //     $(".xs_login_mark").removeClass('xs_content_current');
    //     $(this).addClass('xs_content_current');
    //     if($('.xs_login_display').hasClass('dsno')){
    //         $('.xs_login_display').removeClass('dsno');
    //         $(".xs_login_display").addClass('dsbl');
    //     }
    //     else if($('.xs_login_display').hasClass('dsbl')){
    //         $('.xs_login_display').removeClass('dsbl');
    //         $(".xs_login_display").addClass('dsno');
    //     }
    //     if($('.password').hasClass('dsbl')){//手机登录
    //         $('.password').removeClass('dsbl');
    //         $(".password").addClass('dsno');//不显示
    //         $("#xs_password").val("");
    //         $('#login_btn').attr({"disabled":"disabled"});//禁用
    //         $('.xs_error_tips').addClass('dsno');
    //     }else{
    //         $('.password').removeClass('dsno');//密码登录
    //         $("#xs_password").val("");
    //         $(".password").addClass('dsbl');//显示
    //         $('.xs_error_tips').addClass('dsno');
    //         $('#login_btn').attr({"disabled":"disabled"});//禁用
    //     }
    //     $('#drag').drag();//滑动要放在登录模块是否显示之后
    // });//
    //$('.btn-loading-example').click(
    function namePwd_login (userName,password) {
        //此处仅为保险，一般不会调用到。
        $.ajax({
            url: "http://localhost:6992/api/UserInfo/UserLogin",
            type: "Post",
            async: true,
            //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
            //dataType: "json",
            // contentType: "charset=utf-8",  ,"cat":Search
            data:{UserName:""+userName+"",PassWord:""+password+""},
            //cache: false,
            success: function (returndata) {
                alert(returndata.mes+"正确");
                // $('#xs_login_output').css('display','none');
                $('#login_btn').attr({"disabled":"disabled"});//按钮禁用
                $('#xs_username').val("");//用户名或手机
                $('#xs_password').val("");//用户密码
                $('#sms-cd').val("");//验证码

                if(!returndata.suc){//false
                    $('.xs_errpr_text ').text(""+returndata.mes+"");
                    $('.xs_errpr_text ').removeClass("dsno")

                }else{
                    //是否勾选自动登录
                    var state_check=$("#auto-login-cd").is(':checked');
                    if(state_check){
                        close_login();
                        //sessionStorage 切换新页面，关闭会删除
                        localStorage.setItem("Token",""+returndata.token+"");//存token
                        localStorage.setItem("loginState","1");//更改用户登录状态
                        display_outUserLoging(returndata.extra);
                        // loginBox();
                        alert("true");
                    }else{
                        close_login();
                        //sessionStorage 切换,刷新页面，关闭会删除，不能用来储存用户登录信息
                        // sessionStorage.setItem("Token",""+returndata.token+"");//存token
                        // sessionStorage.setItem("loginState","1");//更改用户登录状态
                        $.cookie("Token",""+returndata.token+"");//,{ expires:1 }
                        localStorage.setItem("loginState","2");//更改用户登录状态
                        //$.cookie("loginState","1");
                        display_outUserLoging(returndata.extra);

                        alert("false");
                    }

                }
            },
            error: function (returndata) {
                alert(returndata.mes+"错误");
            }

        })
    }
    function phone_login(userName,smsCd)
    {
        $.ajax({//验证码校验
            data:{CacheName:""+userName+"",smsCd:""+smsCd+"",intoState:0},
            url: "http://localhost:6992/api/UserInfo/SmsCache",
            type: "get",
            async: true,
            //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
            //dataType: "json",
            // contentType: "charset=utf-8",  ,"cat":Search
            cache: false,
            success: function (returndata) {
                alert(returndata.mes+"正确");
                // $('#xs_login_output').css('display','none');
                $('#login_btn').attr({"disabled":"disabled"});//按钮禁用
                $('#xs_username').val("");//用户名或手机
                $('#sms-cd').val("");//用户验证码

                if(!returndata.suc){//false
                    $('.xs_errpr_text ').text(""+returndata.mes+"");
                    $('.xs_errpr_text ').removeClass("dsno")
                }else{
                    //是否勾选自动登录
                    var state_check=$("#auto-login-cd").is(':checked');
                    if(state_check){
                        close_login();
                        //sessionStorage 切换新页面，关闭会删除
                        localStorage.setItem("Token",""+returndata.token+"");//存token
                        localStorage.setItem("loginState","1");//更改用户登录状态
                        display_outUserLoging(returndata.extra);
                        // loginBox();
                    }else{
                        close_login();
                        //sessionStorage 切换,刷新页面，关闭会删除，不能用来储存用户登录信息
                        // sessionStorage.setItem("Token",""+returndata.token+"");//存token
                        // sessionStorage.setItem("loginState","1");//更改用户登录状态
                        $.cookie("Token",""+returndata.token+"");//,{ expires:1 }
                        localStorage.setItem("loginState","2");//更改用户登录状态
                        //$.cookie("loginState","1");
                        display_outUserLoging(returndata.extra);

                        alert("false");
                    }
                }
                //取token存，调用header登录dom
                //display_outWorksList(returndata,lookState);
            },
            error: function (returndata) { alert(returndata+"错误"); }
        });

    }
        //输出保存登录后头部的用户头像。
    function display_outUserLoging(data) {
        var json=data;
        var outStr="";
        var state_check=$("#auto-login-cd").is(':checked');
        $.each(json,function (index,item) {
            var userName=item.UserName;
            var userId=item.Id;
            var userImg=item.UserImg;
            if(state_check){
                localStorage.setItem("userImg",""+userImg+"");//头像
                localStorage.setItem("userInfo",""+userName+"");//信息用户名
                localStorage.setItem("userId",""+userId+"");//id
            }else{
                $.cookie("userImg",""+userImg+"");
                    //{
                      //  expires:7,  
                       // path:'/',
                    //}
                    
                $.cookie("userInfo",""+userName+"");//信息用户名
                localStorage.setItem("userId",""+userId+"");//id
                //  {
                //     expires:7,  
                //     path:'/',
                // }
                
                //$.cookie("cookie_kogin","1")
            }
            outStr="<ul class=\"logining xs_a_opacity\">" +
                "<li><span class=\"am-icon-search am-icon-sm am-btn am-btn-sm xs_header_btn_style xs_search_icon\" onclick=\"clk()\"></span>\n" +
                "<a href='uploadWork.html?userId="+userId+"'><span class=\"am-icon-upload am-icon-sm am-btn am-btn-sm xs_header_btn_style\" id=\"xs_info_btn\"></span></a>" +
                "<span class=\"xs_user_img\">" +
                "<a href=\"User_personal.html?userId="+userId+"\">" +
                "<img src=\"http://localhost:6992/"+userImg+"\" title=\""+userName+"_"+userId+"\">" +
                "</a><a href=\"#\" class=\"font_size xs_Exit\" onclick=\"Exit_login();\">退出</a></span></li></ul>"
        });
        var obj=$('#xs_login_box');
        obj.html(outStr);
    }

    //);
    //登录模块关闭
    //$('.xs_login_close').click(
    function close_login() {
        $('#xs_login_output').css('display','none');
        $(document.body).css('overflow','auto');
    };//);
    //登录模块显示
    //$('#xs_login_btn').click(
    function block_login() {
        $('#xs_login_output').css('display','block');
        $(document.body).css('overflow','hidden');
        login_in();
    };//);
//登录按钮的启动与禁用
    function login_in(){
        var userName=$('#xs_username').val();//用户名或手机
        var password=$('#xs_password').val();//用户密码
        var smsCd=$('#sms-cd').val();//验证码
        var login_btn=$('#login_btn');
        if(userName!=""&&password!=""){//密码登录
           login_btn.removeAttr("disabled");//启用
            return
        }else if(userName!=""&&smsCd!=""){//手机登录
            login_btn.removeAttr("disabled");//启用
            return
        }else{
             login_btn.attr({"disabled":"disabled"});
             return
        }
    }
    //退出登录
    function Exit_login(){
        localStorage.clear();
        localStorage.setItem("loginState","0");
        loginBox();
    }
    //验证码发送按钮倒计时
    var countdown=30;
    function settime(obj) {
    if (countdown == 0) {
        obj.removeAttribute("disabled");
        obj.value="免费获取验证码";
        countdown = 30;
        return;
    } else {
        obj.setAttribute("disabled", true);//禁用发送按钮
        obj.value="重新发送(" + countdown + ")";
        countdown--;
    }
    setTimeout(function() {
            settime(obj) }
        ,1000)
    }
    //发送验证码
    function sms_login(state){
        var userName=$('#xs_username').val();//用户名或手机
        var sms_cd= $('#sms-cd').val();//验证码
        var intState=state;//这是注册状态
        if(!userName==""){
            if((/^1(3|4|5|7|8)\d{9}$/.test(userName))){
                var arr = [""+userName+""];
                $.ajax({
                    data:{phone:""+userName+"",intState:""+intState+""},
                    url: "http://localhost:6992/api/UserInfo/SmsOut",
                    type: "get",
                    async: true,
                    //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
                    //dataType: "json",
                    // contentType: "charset=utf-8",  ,"cat":Search
                    cache: false,
                    success: function (returndata) {
                        if(returndata.suc==false){
                            $('.xs_error_tips').removeClass('dsno');
                            $('.xs_errpr_text ').text(returndata.mes);
                        }
                        alert(returndata+"正确");

                    },
                    error: function (returndata) { alert(returndata+"错误"); }
                });
            }else{
                $('.xs_error_tips').removeClass('dsno');
                $('.xs_errpr_text ').text("手机号有误，请重填");
            }
        }else{
            $('.xs_error_tips').removeClass('dsno');
            $('.xs_errpr_text ').text("手机号不能为空");
        }

    }
// };

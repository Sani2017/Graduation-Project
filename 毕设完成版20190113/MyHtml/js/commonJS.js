//导航特效
var bool = true;
function clk(){
    bool = !bool;
    if(!bool){//搜索图标可以跟随显示当无效果
        $('#xs_serch_input').removeClass('dsno');
        $('#xs_serch_input').addClass('dsind');
        $('#xs_search_input').focus() ;//获取焦点
        //css('display','inline-block');//搜索显示
        // $('#xs_nav_content ').css('display');//导航隐藏
        // $('#xs_nav_content').css('display','none');
        setTimeout(function(){
            $('#xs_serch_input').addClass('active');//搜索出现效果添加
            $('#xs_serch_input').removeClass('nuactive');//搜索消失效果去除
            $('#xs_nav_content').addClass('nuactive');//导航隐藏效果添加
            $('#xs_nav_content').removeClass('active');//导航出现效果去除

            $('#xs_nav_content').addClass('dsno');// .css('display');//导航隐藏
            $('#xs_nav_content').removeClass('dsind');

            $('.xs_search_icon').removeClass('dsind');//这是搜索icon
            $('.xs_search_icon').addClass('dsno');

            // $('#xs_nav_content').css('display','none');//
        },0);
    }else{
        $('#xs_nav_content').removeClass('dsno');//移除消失样式
        $('#xs_nav_content').addClass('dsind');//.css('display','inline-block');//导航显示

        $('.xs_search_icon').removeClass('dsno');//这是搜索icon
        $('.xs_search_icon').addClass('dsind');

        // $('#xs_serch_input').css('display');//搜索隐藏
        // $('#xs_serch_input').css('display','none');//
        setTimeout(function(){
            $('#xs_nav_content').removeClass('nuactive');//导航隐藏效果去除
            $('#xs_nav_content').addClass('active');//导航出现效果添加
            $('#xs_serch_input').removeClass('active');//搜索出现效果去除
            $('#xs_serch_input').addClass('nuactive');//搜索隐藏效果添加

            $('#xs_serch_input').addClass('dsno');//搜索隐藏
            //.css('display');
            $('#xs_serch_input').removeClass('dsind');

            //$('#xs_serch_input').css('display','none');//

        },0)
    }
}
//从url取值，判断是否进行邮箱密码重置功能
function GetQueryStringByUrl(name)
{
    var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);//色arch,查询？后面的参数，并匹配正则
    if(r!=null) return  decodeURI(r[2]); return null;
}
// loginState:0：未登录；1：下次自动登录；2：下次不自动登录
//userName:储存用户名与token
function loginBox(){
    var outStr="";
    var cookState= $.cookie("userImg");
    var localState= localStorage.getItem("loginState");
    var obj=$('#xs_login_box');
    if ( localState==0) {
        outStr="<ul class=\"unlogin xs_a_opacity\" >"//href=\"login.html\"
            +"<li><span class=\"xs_header_btn_style am-icon-search am-icon-sm am-btn am-btn-sm  xs_search_icon\" onclick=\"clk()\" style=\"margin-right: 13px;\"></span>"
            +"<a onclick=\"block_login();\"  class=\"xs_header_btn_style font_size\" id=\"xs_login_btn\">登录</a>"
            +"<i></i><a href=\"registered.html\" class=\"xs_header_btn_style font_size\" id=\"xs_reg_btn\">注册</a>"
            +"</li></ul>";
    }else {
        var userImg="";
        var userInfo="";
        //判断用户是否勾选自动登录
        if(localState==1){//勾选
            userImg=localStorage.getItem("userImg");
            userInfo=localStorage.getItem("userInfo");
        }
        else if(cookState!=null && localState==2){
            userImg=$.cookie("userImg");
            userInfo=$.cookie("userInfo");
        }else{
            outStr="<ul class=\"unlogin xs_a_opacity\" >"//href=\"login.html\"
                +"<li><span class=\"xs_header_btn_style am-icon-search am-icon-sm am-btn am-btn-sm  xs_search_icon\" onclick=\"clk()\" style=\"margin-right: 13px;\"></span>"
                +"<a onclick=\"block_login();\"  class=\"xs_header_btn_style font_size\" id=\"xs_login_btn\">登录</a>"
                +"<i></i><a href=\"registered.html\" class=\"xs_header_btn_style font_size\" id=\"xs_reg_btn\">注册</a>"
                +"</li></ul>";
            obj.html(outStr);
            return;
        }
        var userId=localStorage.getItem("userId");
        outStr="<ul class=\"logining xs_a_opacity\">" +
            "<li><span class=\"am-icon-search am-icon-sm am-btn am-btn-sm xs_header_btn_style xs_search_icon\" onclick=\"clk()\"></span>\n" +
            "<a href='uploadWork.html?userId="+userId+"'><span class=\"am-icon-upload am-icon-sm am-btn am-btn-sm xs_header_btn_style\" id=\"xs_info_btn\"></span></a>\n" +
            "<span class=\"xs_user_img\">" +
            "<a href=\"User_personal.html?userId="+userId+"\">" +
            "<img class='userImg_personal' src=\"http://localhost:6992/"+userImg+"\" title=\""+userInfo+"\">\n" +
            "</a><a class=\"font_size xs_Exit \" href=\"#\" onclick=\"Exit_login();\">退出</a></span></li></ul>"
    }
    obj.html(outStr);
}


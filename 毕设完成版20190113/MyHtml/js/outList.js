    // 用于输出各种网站所需列表
    //作品类型全输出
    function outSort(sortList){
        $.ajax({  
            url: "http://localhost:6992/api/Sort/GetAllSort",
            type: "Get",  
            async: true,  
            //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
            //dataType: "json",  
            // contentType: "charset=utf-8",  ,"cat":Search
            //data:, 
            cache: false,  
            success: function (returndata) {  
                // alert(returndata+"正确");
                //display_outSortList(returndata);
                var outStr="";
                if(sortList==null){
                    outStr="<li><a id=\"sort_0\" class=\"sortSelect allSortList newSelect\" href=\"javacript:void(0);\">全部</a></li>";
                    $.each(returndata,function (index,item) {
                        var SortName=item.SortName;//分類名稱
                        var SortId=item.Id;//分類Id
                        outStr+="<li><a id=\"sort_"+SortId+"\" class=\"sortSelect\" href=\"javacript:void(0);\">"+SortName+"</a></li>"
                    });
                }else if(sortList==1){
                    outStr="<li class=\"xs_sortList_title\">\n" +
                        "<span>作品榜单</span></li>\n" +
                        "<li class=\"sort_click color\" id=\"sort_0\">\n" +
                        "<a href=\"javascript:void (0);\">\n" +
                        "<i></i><p>全部</p></a></li>"
                    $.each(returndata,function (index,item) {
                        var SortName = item.SortName;//分類名稱
                        var SortId = item.Id;//分類Id
                        outStr += "<li class=\"sort_click\" id=\"sort_"+SortId+"\">\n" +
                            "<a href=\"javascript:void (0);\">\n" +
                            "<i></i>\<p>"+SortName+"</p></a></li>";
                    });
                }else if(sortList==2){//用户私人页输出
                    outStr="<a href=\"javascript:void (0);\" class=\"work_sort current\" id=\"sort_0\">全部</a>"
                    $.each(returndata,function (index,item) {
                        var SortName = item.SortName;//分類名稱
                        var SortId = item.Id;//分類Id
                        outStr += "<a href=\"javascript:void (0);\" class=\"work_sort\" id=\"sort_"+SortId+"\">"+SortName+"</a>"
                    });
                    var obj_user_personal=$(".work_sort_list");
                    obj_user_personal.html(outStr);
                    return false;
                }else if(sortList==3){//上传页作品类型
                    $.each(returndata,function (index,item) {
                        var SortName = item.SortName;//分類名稱
                        var SortId = item.Id;//分類Id
                        outStr += "<option value=\""+SortId+"\">"+SortName+"</option>"
                    });
                    var obj_user_upload=$(".outputWorkSort");
                    obj_user_upload.html(outStr);
                    return false;
                }
                var obj=$("#sortList");
                obj.html(outStr);
            },
            error: function (returndata) { alert(returndata+"错误"); }  
        }); 
    }

    // function display_outSortList(data){

    // }
    // 输出作品列表
    function outWorksList(lookState){
        if(lookState==null){
            lookState=0;
        }
        var searchValue= "    ";
        var page=1;
        var pageName=1;
        var sortId=0;
        $.ajax({  
            url: "http://localhost:6992/api/Works/GetWorkAndUserInfoList",
            type: "Get",  
            async: true,  
            //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
            //dataType: "json",  
            // contentType: "charset=utf-8",  ,"cat":Search
            data:{lookState:""+lookState+"",searchValue:""+searchValue+"",page:""+page+"",pageName:""+pageName+"",sortId:""+sortId+""}, 
            //cache: false,  
            success: function (returndata) {  
                // alert(returndata+"正确");
                var sdf=returndata.Data;
                display_outWorksList(returndata.Data,lookState);
            },
            error: function (returndata) { alert(returndata+"错误"); }  
        }); 
    }
    function display_outWorksList(data,lookState) {
        var json=data;
        var outStr="";
        var topWorkList=" ";
        if(lookState===0){
            topWorkList="最新作品";
        }else if(lookState===1){
            topWorkList="赞数最多";
        }else if(lookState===2){
            topWorkList="浏览最多";
        }
        if(json.length  ==0){
            //    无内容输出
            outStr="<h2 class='noneText'>什么都没找到啊T T</h2>";
            var obj_noneText=$(".outNoneText");
            obj_noneText.html(outStr);
            return false;
        }
        $.each(json,function (index,item) {
            var WorkImg = item.WorkImg;//作品封面
            var WorksId = item.WorksId;//作品id
            var WorksTitle = item.WorksTitle;//标题
            var LikesCount = item.LikesCount;//点赞量
            var Hits = item.Hits;//浏览量
            var PublishedAt = item.PublishedAt;//发布时间
            var UserId = item.UserId;//用户id
            var UserName = item.UserName;//用户名
            var UserImg = item.UserImg;//用户头像
            var WorksSort = item.WorksSort;//作品分类
            var TimeDifference=item.TimeDifference;//时间差
            outStr+="<div class=\"xs_card_box xs_a_opacity\">" +
            "<div class=\"xs_card_img\">" +
            "<a href=\"works_Details.html?workId="+WorksId+"\">" +
            "<img class=\"xs_work_img\" src=\"http://localhost:6992/"+WorkImg+"\">" +
            "</a></div><div class=\"xs_card_info\">" +
            "<p class=\"xs_card_info_title\">" +
            "<a href=\"works_Details.html?workId="+WorksId+"\">"+WorksTitle+"</a></p><p class=\"xs_card_info_type\">"+WorksSort+"</p>" +
            "<p class=\"xs_card_info_item\">" +
            "<span class=\"statistics-view\" title=\"共"+LikesCount+"人气\"><i class=\" am-icon-thumbs-up am-icon-fw\"></i>"+LikesCount+"</span>" +
            "<span class=\"statistics-comment\" title=\"点击查看"+Hits+"次\"><i class=\"am-icon-eye am-icon-fw\"></i>"+Hits+"</span>" +
            "<span class=\"statistics-tuijian\" title=\""+topWorkList+"\"><i class=\"am-icon-tencent-weibo am-icon-fw\"></i></span>" +
            "</p></div><div class=\"xs_card_item\">" +
            "<span class=\"xs_card_user_info\">" +
            "<img src=\"http://localhost:6992/"+UserImg+"\" alt=\"头像\">" +
            "<a href=\"User_public.html?userId="+UserId+"\" id='"+UserId+"'>"+UserName+"</a>" +
            "<span class=\"works_time\" title=\"审核通过时间:"+PublishedAt+"\">"+TimeDifference+"</span>" +
            "<div></div></span></div></div>";
        });
        var obj=$("#container");
        obj.html(outStr);
        $('#container').pinto();//图片排版js效果，在输出的时候就要调用不然会出样式问题

    }
    //输出活动列表
    //outState输出状态,lookState查看状态,page分页页数,searchId搜索活动的Id，0为不搜索
    function outActivityList(outState,lookState,page,outPages,searchId){
        if(outState==null){
            outState=1;
        }
        if(lookState==null){
            lookState=0;
        }
        if(page==null){
            page=0;
        }
     
        $.ajax({
            url: "http://localhost:6992/api/Activity/GetAllActivityInfo",
            type: "Get",
            async: true,
            //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
            //dataType: "json",
            // contentType: "charset=utf-8",  ,"cat":Search
            data:{outState:""+outState+"",lookState:""+lookState+"",page:""+page+"",searchId:""+searchId+""},
            //cache: false,
            success: function (returndata) {
                // alert(returndata+"正确");。
                switch(outState){
                    case 1://输出前五条活动
                        display_outActivityListTop5(returndata);
                    break;
                    case 2://输出活动页的带分页的列表
                    display_outActivityListByPage(returndata.Data);
                    var pages=returndata.Total;
                        if(outPages==pages){
                            return false;
                        }else{
                            outPage(pages);//输出页码

                            var outStr="<div id=\"page\" class=\"dsno\">"+pages+"</div>";
                            var obj=$("#allPage");
                            obj.html(outStr);
                        }
                }
                
            },
            error: function (returndata) { alert(returndata+"错误"); }
        });
    }
    function display_outActivityListTop5(data) {
        var json=data;
        var outStr="";
        $.each(json,function (index,item) {
            var Id = item.Id;//作品id
            var ActivityImg = item.ActivityImg;//作品封面
            var ActivityName = item.ActivityName;//标题
            var ActivityContent = item.ActivityContent;//点赞量
            var ActivityDate = item.ActivityDate;//浏览量
            outStr+="<li class='xs_a_opacity xs_index_actitle'><a href=\"activity_Details.html?activity="+Id+"\">" +
                "<img class=\"am-thumbnail\" src=\"http://localhost:6992/"+ActivityImg+"\" title=\""+ActivityName+"发布日期："+ActivityDate+"\" />" +
                "</a></li>";
        });
        var obj=$("#xs_acticity_list_content");
        obj.html(outStr);
        //$('#container').pinto();//图片排版js效果，在输出的时候就要调用不然会出样式问题

    }
    //关于活动页输出带分页的列表(未测试……)
    function display_outActivityListByPage(data){
        var json=data;
        var outStr="";
        $.each(json,function (index,item) {
            var ActivityId = item.ActivityId;//活动ID
            var ActivityImg = item.ActivityImg;//活动封面
            var ActivityTitle = item.ActivityTitle;//活动标题
            var ActivityDate = item.ActivityDate.substring(0, 10);//活动发布时间
            var EndTime = item.EndTime.substring(0, 10);//活动结束时间
            var ActivityTimeDifference = item.ActivityTimeDifference;//距活动结束的時間差
            var LikesCount = item.LikesCount;//点赞量
            var NewTimeDifference = item.NewTimeDifference;//发布至今的時間差
            var Hits = item.Hits;//活动点击浏览量
            var NumberOfWorks = item.NumberOfWorks;//参加作品数量
            var ActivityState=item.ActivityState;//作品状态
            outStr+="<div class=\"xs_card_box xs_a_opacity\">"+
            "<div class=\"xs_card_img\">"+
                "<a href=\"activity_Details.html?activity="+ActivityId+"\">"+
                "<img class=\"xs_work_img\" src=\"http://localhost:6992/"+ActivityImg+"\"></a></div>"+
            "<div class=\"xs_activity_card_info\">"+
                "<p class=\"xs_card_info_title\"><a href=\"activity_Details.html?activity="+ActivityId+"\">"+ActivityTitle+"</a></p>"+
                "<p class=\"xs_card_info_time\"><span>"+ActivityDate+"-"+EndTime+"</span><span class=\"works_time\">"+ActivityTimeDifference+"</span></p></div>"+
            "<div class=\"xs_card_item\"><p>"+
                "<span class=\"statistics-view\" title=\"共"+LikesCount+"人气\"><i class=\" am-icon-thumbs-up am-icon-fw\"></i>"+LikesCount+"</span>"+
                "<span class=\"statistics-comment\" title=\"点击查看"+Hits+"次\"><i class=\"am-icon-eye am-icon-fw\"></i>"+Hits+"</span>"+
                "<span class=\"statistics-comment\" title=\"参加作品"+NumberOfWorks+"个\"><i class=\"am-icon-image am-icon-fw\"></i>"+NumberOfWorks+"</span>"+
                "<span class=\"works_time\" title=\"审核通过时间:2018-12-24T17:20:47\" >"+NewTimeDifference+"</span>"+
            "</p></div></div>";
        });
        var obj=$("#container");
        obj.html(outStr);
        $('#container').pinto();//图片排版js效果，在输出的时候就要调用不然会出样式问题

    }
    //查询输出作品名与作者名
    function outSelectList(seleName){
        $.ajax({
            url: "http://localhost:6992/api/Works/SelectInput",
            type: "Get",
            async: true,
            //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
            //dataType: "json",
            // contentType: "charset=utf-8",  ,"cat":Search
            data:{selectName:""+seleName+""},
            cache: false,
            success: function (returndata) {
                //alert(returndata+"正确");
                display_outSelectList(returndata.UserName,returndata.WorkName);
            },
            error: function (returndata) { alert(returndata+"错误"); }
        });
    }
    function display_outSelectList(UserNameList,WorkNameList){
        var json_users=UserNameList;
        var json_works=WorkNameList;
        var outStr_users="";
        var outStr_works="";
        var outStr="";
        $.each(json_works,function (index,item) {
            var Id=item.Id;
            var Title=item.Title;
            outStr_works+="<div class=\"hot-list search-l\"><a href=\"works_Details.html?workId="+Id+"\">"+Title+"</a></div>"
        });
        $.each(json_users,function (index,item) {
            var Id=item.Id;
            var UserName=item.UserName;
            outStr_users+="<div class=\"hot-list search-l\"><a href=\"User_public.html?userId="+Id+"\">"+UserName+"</a></div>"
        });
        outStr= outStr_works+"<div class=\"search-title\">搜索大佬<span></span></div>"+outStr_users;

        var obj=$(".search-content-list");
        obj.html(outStr);
    }

    //输出登录dom
    function outLoginBox(){
        var outStr="";
        outStr="<div class=\"xs_user_login\"></div>"+
                "<div class=\"xs_user_login_box am-panel am-panel-default\">"+
                "<a href=\"#\" class=\"am-close xs_login_close\" onclick='close_login();' >&times;</a>"+
                "<ul class=\"xs_user_login_select\">"+
                "<li class=\"xs_login_mark xs_content_current\" id='login_0'><a href=\"#\">密码登录</a></li>"+
                "<li class=\"xs_login_mark\" id='login_1'><a href=\"#\">手机登录</a></li></ul>"+
                "<div class=\"xs_error_tips dsno\" id=\"sms-error-tips\" >"+
                "<span class=\"ipt-tips-default ipt-default-current js-err-l\">"+
                "<i class=\"am-icon-minus-circle\" style=\"padding-right: 5px;color:red\"></i>"+
                "<span class=\"font_size xs_errpr_text \">请输入正确的手机号</span></span></div>"+
                "<div class=\"username\">"+
                "<input type=\"text\" id=\"xs_username\" onkeyup=\"login_in();\" placeholder=\"用户名或手机号\" autocomplete=\"off\" class=\"xs_login_style\"></div>"+
                "<div class=\"password  dsbl\">"+
                "<input type=\"password\" id=\"xs_password\" onkeyup=\"login_in();\" placeholder=\"密码\" autocomplete=\"off\" class=\"xs_login_style\"></div>"+
                "<div class=\"xs_login_display dsno\">"+
                "<div id=\"wrapper\" ><div id=\"drag\">"+
                "<div class=\"drag_bg\"></div>"+
                "<div class=\"drag_text slidetounlock\" onselectstart=\"return false;\" unselectable=\"on\">"+
                "请按住滑块，拖动到最右边</div><div class=\"handler handler_bg am-icon-angle-double-right\"></div></div></div>"+
                "<div class=\"l-sms-code js-ipt-h dsno\"> "+
                "<input type=\"text\" autocomplete=\"off\" class=\"text-style ipt-short-current xs_sms_input xs_login_style\" id=\"sms-cd\" onkeyup=\"login_in();\" maxlength=\"6\">"+
                "<input type=\"button\" class=\"xs_sms_btn xs_login_btn sms-cd-btn btn-default-main\" id=\"sms-send-cd-btn\" value=\"发送验证码\" onclick=\"sms_login();settime(this);\" ></div></div>"+
                "<button type=\"button\"   class=\" xs_login_btn am-btn  btn-loading-example\" id=\"login_btn\" style=\"outline:none;\" >登&nbsp;&nbsp;录</button>"+
                "<div class=\"xs_auto_login\"><label class=\"auto-login-cd\">"+
                "<input type=\"checkbox\" name=\"autolog\" id=\"auto-login-cd\" checked=\"\" class=\"auto-login-cd \">下次自动登录"+
                "<div class=\"show-box\"></div></label><div class=\"login-links\"><a href=\"getPassword.html\"class=\"\">忘记密码</a>"+
                "<span>|</span><a href=\"registered.html\">注册</a></div></div></div>";// target=\"_blank\" 
              var obj=$("#xs_login_output");
              obj.html(outStr);
              //$('#container').pinto();//图片排版js效果，在输出的时候就要调用不然会出样式问题 onclick='login_btn();'

            //登录模块效果以及相关功能
            // $(".xs_login_mark").on('click',function(){
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
            // });
            // $('.btn-loading-example').click(function () {
            //     //此处仅为保险，一般不会调用到。
            //     var userName=$('#xs_username').val();//用户名或手机
            //     var password=$('#xs_password').val();//用户密码
            //     var smsCd=$('#sms-cd').val();//验证码
            //     var login_btn=$('#login_btn');
            //     if(userName==""||password==""){
            //         $('.xs_errpr_text ').text("用户或密码不能为空");
            //         $('.xs_error_tips').removeClass('dsno');
            //         login_btn.attr({"disabled":"disabled"});
            //         return;
            //     }else if(userName!=""&&smsCd!=""){//手机登录
            //         $('.xs_errpr_text ').text("手机或验证码不能为空");
            //         $('.xs_error_tips').removeClass('dsno');
            //         login_btn.attr({"disabled":"disabled"});
            //         return;
            //     }else{
            //         var $btn = $(this)
            //         $btn.button('loading');
            //         setTimeout(function(){
            //             $btn.button('reset');
            //         }, 500);
            //         $.ajax({
            //             url: "http://localhost:6992/api/UserInfo/UserLogin",
            //             type: "Post",
            //             async: true,
            //             //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
            //             //dataType: "json",
            //             // contentType: "charset=utf-8",  ,"cat":Search
            //             data:{UserName:""+userName+"",PassWord:""+password+""},
            //             //cache: false,
            //             success: function (returndata) {
            //                 alert(returndata.mes+"正确");
            //                 // $('#xs_login_output').css('display','none');
            //                 $('#xs_username').val("");//用户名或手机
            //                 $('#xs_password').val("");//用户密码
            //                 $('#sms-cd').val("");//验证码
            //                 if(!returndata.suc){//false
            //                     $('.xs_errpr_text ').text(""+returndata.mes+"");
            //                 }else{
            //                     //sessionStorage 切换新页面，关闭会删除
            //                     localStorage.setItem("Token",""+returndata.token+"");//存token
            //                     localStorage.setItem("loginState","1");//更改用户登录状态
            //                     display_outUserLoging(eval(returndata.extra))
            //                     // loginBox();
            //
            //                 }
            //                 //取token存，调用header登录dom
            //                 //display_outWorksList(returndata,lookState);
            //             },
            //             error: function (returndata) {
            //                 alert(returndata.mes+"错误");
            //             }
            //
            //         })
            //     }
            //     function display_outUserLoging(data) {
            //         var json=data;
            //         var outStr="";
            //         $.each(json,function (index,item) {
            //             var userName=item.UserName;
            //             var userId=item.Id;
            //             var userImg=item.UserImg;
            //             localStorage.setItem("userImg",""+userImg+"");
            //             localStorage.setItem("userInfo",""+userName+"_"+userId+"");
            //             outStr="<ul class=\"logining\">" +
            //                 "<li><span class=\"am-icon-search am-icon-sm am-btn am-btn-sm xs_header_btn_style xs_search_icon\" onclick=\"clk()\"></span>\n" +
            //                 "<span class=\"am-icon-home am-icon-sm am-btn am-btn-sm xs_header_btn_style\" id=\"xs_info_btn\"></span>\n" +
            //                 "<span class=\"xs_user_img\">" +
            //                 "<a href=\"#\">" +
            //                 "<img src=\"http://localhost:6992/"+userImg+"\" title=\""+userName+"_"+userId+"\">" +
            //                 "</a><a href=\"#\">退出</a></span></li></ul>"
            //         });
            //         var obj=$('#xs_login_box');
            //         obj.html(outStr);
            //     }
            // });
            // //登录模块关闭
            // $('.xs_login_close').click(function close_login() {
            //     $('#xs_login_output').css('display','none');
            // });
            // //登录模块显示
            // $('#xs_login_btn').click(function () {
            //     $('#xs_login_output').css('display','block');
            //     login_in();
            //
            // });
    }
//发现页,搜索頁输出作品列表
// lookState:查看状态,sortId：类型id,
// suerSearchValue：搜索内容,clickPage分页页码,outPages总页数
function outWorksListByPase(lookState,sortId,
                            suerSearchValue,clickPage,outPages){
    var searchValue=" ";//搜索內容
    var page=1;//頁碼
    var selectSortId;//分类的id
    if(lookState==null){
        lookState=0;
    }
    if(sortId==null){
        selectSortId=0;
    }else{
        selectSortId=sortId;
    }
    if(suerSearchValue!=null){
        searchValue=suerSearchValue;
    }else {
        searchValue= "    ";
    }
    if(clickPage!=null){
        page= clickPage;
    }else{
        page=1;
    }
    var pageName="Find";//是否分頁的标示
    $.ajax({  
        url: "http://localhost:6992/api/Works/GetWorkAndUserInfoList",
        type: "Get",  
        async: true,  
        //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
        //dataType: "json",  
        // contentType: "charset=utf-8",  ,"cat":Search
        data:{lookState:""+lookState+"",searchValue:""+searchValue+"",
        page:""+page+"",pageName:""+pageName+"",sortId:""+selectSortId+""}, 
        //cache: false,  
        success: function (returndata) {  
            // alert(returndata+"正确");
            //display_outActivityListByPage  //var sdf=returndata.Data;
            display_outWorksList(returndata.Data,lookState);
            var pages=returndata.Total;
            if(outPages==pages){
                return false;
            }else{
                outPage(pages);//输出页码

                var outStr="<div id=\"page\" class=\"dsno\">"+pages+"</div>";
                var obj=$("#allPage");
                obj.html(outStr);
            }

            //$("#page").html(pages); 
            //把从后台获取的页码保存在放在dom中，当点击当个页码时获取并与的新页码进行比对
            //如果相同则return false 结束，else渲染新的分页页码。
        },
        error: function (returndata) { alert(returndata+"错误"); }  
    }); 
}
    //发现页,搜索頁输出作品列表
    // lookState:查看状态,sortId：类型id,
    // suerSearchValue：搜索内容,clickPage分页页码,outPages总页数
    function outUserListByPase(suerSearchValue,clickPage,outPages) {
        if(suerSearchValue!=null){
            searchValue=suerSearchValue;
        }else {
            searchValue= "    ";
        }
        if(clickPage!=null){
            page= clickPage;
        }else{
            page=1;
        }
        $.ajax({
            url: "http://localhost:6992/api/UserInfo/selectUserAndWorkSum",
            type: "Get",
            async: true,
            //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
            //dataType: "json",
            // contentType: "charset=utf-8",  ,"cat":Search
            data:{searchValue:""+searchValue+"",page:""+page+""},
            //cache: false,
            success: function (returndata) {
                // alert(returndata+"正确");
                //var sdf=returndata.Data;
                display_outUsersList(returndata.Data);
                var pages=returndata.Total;
                if(outPages==pages){
                    return false;
                }else{
                    outPage(pages);//输出页码

                    var outStr="<div id=\"page\" class=\"dsno\">"+pages+"</div>";
                    var obj=$("#allPage");
                    obj.html(outStr);
                }

                //$("#page").html(pages);
                //把从后台获取的页码保存在放在dom中，当点击当个页码时获取并与的新页码进行比对
                //如果相同则return false 结束，else渲染新的分页页码。
            },
            error: function (returndata) { alert(returndata+"错误"); }
        });
    }
    function display_outUsersList(data) {
    var outStr="";
        $.each(data,function (index,item) {
            var UserId = item.UserId;//用户id
            var UserImg = item.UserImg;//用户头像
            var UserName = item.UserName;//用户名称
            var UserWorksum = item.UserWorksum;//点赞量
            outStr+="<div class=\"xs_card_box xs_a_opacity\">" +
                "        <div class=\"xs_card_user_img\">\n" +
                "            <a href=\"User_public.html?userId="+UserId+"\">" +
                "                <img class=\"xs_SuerImg\" src=\"http://localhost:6992/"+UserImg+"\">" +
                "            </a>" +
                "        </div>" +
                "        <div>" +
                "            <p class=\"xs_card_info_title\">" +
                "                <a href=\"User_public.html?userId="+UserId+"\" >"+UserName+"</a>" +
                "            </p>" +
                "            <p class=\"xs_card_userWork_sum\">" +
                "                <span class=\"statistics-view\" title=\"共"+UserWorksum+"个作品\">" +
                "                    <i class=\" am-icon-image am-icon-fw\"></i>创作" +
                "                    <b>"+UserWorksum+"</b>" +
                "                </span>" +
                "            </p>" +
                "        </div>" +
                "        <a class=\"user_btn font_size\" href='User_public.html?userId="+UserId+"'>" +
                "            查看" +
                "        </a>" +
                "    </div>";
        });
        var obj=$("#container");
        obj.html(outStr);
        $('#container').pinto();//图片排版js效果，在输出的时候就要调用不然会出样式问题

    }
    ///输出榜单列表
    function outPopularList(SortId) {
        $.ajax({
            url: "http://localhost:6992/api/Works/PopularList",
            type: "Get",
            async: true,
            //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
            //dataType: "json",
            // contentType: "charset=utf-8",  ,"cat":Search
            data:{SortId:""+SortId+""},
            //cache: false,
            success: function (returndata) {
                display_outPopularList(returndata)
            },
            error: function (returndata) { alert(returndata+"错误"); }
        });
    }
    function display_outPopularList(data) {
        var outStr="";
        if(data.length>0) {
            $.each(data,function (index,item) {
                    var UserId = item.UserId;//用户id
                    var UserImg = item.UserImg;//用户头像
                    var UserName = item.UserName;//用户名称
                    var WorkId = item.WorkId;//作品id
                    var WorkImg = item.WorkImg;//作品封面
                    var WorkTitle = item.WorkTitle;//作品标题
                    var WorkSort = item.WorkSort;//作品类型
                    var PublishedAt = item.PublishedAt.substring(0, 10);//作品时间
                    var TotalScore = item.TotalScore;//作品总分
                    var LikesCount = item.LikesCount;//点赞数
                    var Hits = item.Hits;//浏览量
                    var newIndex = index + 1;
                    outStr +=
"                            <li class=\"contentInfo_li\"><!--等级排名-->\n" +
"                                <div class=\"rank\">\n" +
"                                    <div class=\"rank_content\">\n" +
"                                        <div class=\"rank_num\">" + newIndex + "</div>\n" +
"                                        <div class=\"score\">分数</div>\n" +
"                                        <div class=\"score_num\">" + TotalScore + "</div>\n" +
"                                    </div>\n" +
"                                </div>\n" +
"                                <!--展示图片-->\n" +
"                                <div class=\"showImg\">\n" +
"                                    <a href=\"works_Details.html?workId="+WorkId+"\">\n" +
"                                        <img src=\"http://localhost:6992/" + WorkImg + "\" alt=\"img\" title=\"作品名:" + WorkTitle + ";浏览量:" + Hits + ";点赞数:" + LikesCount + "\">\n" +
"                                    </a>\n" +
"                                </div>\n" +
"                                <!--作者相关信息-->\n" +
"                                <div class=\"author\">\n" +
"                                    <div class=\"workInfo\">\n" +
"                                        <div class=\"workTitle\">\n" +
"                                            <a href=\"works_Details.html?workId="+WorkId+"\">" + WorkTitle + "</a>\n" +
"                                        </div>\n" +
"                                        <div class=\"workSort\">" + WorkSort + "</div>\n" +
"                                    </div>\n" +
"                                    <div class=\"userInfo\">\n" +
"                                        <div class=\"userImg\">\n" +
"                                            <a href=\"User_public.html?userId="+UserId+"\">\n" +
"                                                <img src=\"http://localhost:6992/" + UserImg + "\" alt=\"\" title=\"用户名\">\n" +
"                                            </a></div>\n" +
"                                        <div class=\"userName\">\n" +
"                                            <a href=\"User_public.html?userId="+UserId+"\">" + UserName + "</a></div>\n" +
"                                        <div class=\"date\">" + PublishedAt + "</div>\n" +
"                                    </div></div>\n" +
"                                <a class=\"pick\" href=\"works_Details.html?workId="+WorkId+"\">查看详情>></a></li>\n";

            });
        }else {
        //    无内容输出
            outStr="<h2 class='noneText'>什么都没找到啊T T</h2>";
            var obj_noneText=$(".contentInfo");
            obj_noneText.html(outStr);
            return false;
        }
        var obj=$(".contentInfo");
        obj.html(outStr);
    }
    function outPopularList_user(pages,outPages) {
        $.ajax({
            url: "http://localhost:6992/api/UserInfo/PopularListUser",
            type: "Get",
            async: true,
            //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
            //dataType: "json",
            // contentType: "charset=utf-8",  ,"cat":Search
            data:{page:""+pages+""},
            //cache: false,
            heads : {
                'content-type' : 'application/x-www-form-urlencoded'
            },
            success: function (returndata) {
                display_outPopularList_user(returndata.Data)
                var pages=returndata.Total;
                if(outPages==pages){
                    return false;
                }else {
                    outPage(pages);

                    var outStr="<div id=\"page\" class=\"dsno\">"+pages+"</div>";
                    var obj=$("#allPage");
                    obj.html(outStr);
                }
            },
            error: function (returndata) { alert(returndata+"错误"); }
        });
    }
    function display_outPopularList_user(data) {
    var outStr="";
        $.each(data,function (index,item) {
            var UserId = item.UserId;//用户id
            var UserImg = item.UserImg;//用户头像
            var UserName = item.UserName;//用户名称
            var Userlabel=item.Userlabel;//用户个性标签
            var UserWorksum=item.UserWorksum;//创作总数
            var UserWorkImg=item.UserWorkImg;
            outStr+=
            "<div class=\"userList\">\n" +
            "<div class=\"card_user_list\">\n" +
            "    <span class=\"popularUserImg\">\n" +
            "        <a href=\"User_public.html?userId="+UserId+"\" class=\"avatar\"><img src=\"http://localhost:6992/"+UserImg+"\" alt=\"\"></a>\n" +
            "    </span>\n" +
            "    <div class=\"author_info\">\n" +
            "        <p class=\"author_info_name\">\n" +
            "            <a href=\"User_public.html?userId="+UserId+"\">"+UserName+"</a>\n" +
            "        </p>\n" +
            "        <p class=\"author_info_workCount\">创作 <strong>"+UserWorksum+"</strong></p>\n" +
            "        <p class=\"author_info_label\">"+Userlabel+"</p>\n" +
            "        <span class=\"author_info_btn\">\n" +
            "            <a href=\"User_public.html?userId="+UserId+"\" target=\"\" rel=\"noopener noreferrer\">查&nbsp;看</a>\n" +
            "        </span>\n" +
            "    </div>\n" +
            "    <div class=\"card_work_list\">\n" +
            "        <ul class=\"work_con_box\">\n";
            $.each(UserWorkImg,function (indexWork,itemWork) {
                var UserWorkImglist=itemWork.UserWorkImg;
                var asd=itemWork.WorkId;
                    var WorkId = itemWork.WorkId;//作品id
                    var WorkImg = itemWork.WorksImg;//作品封面
                    var WorkTitle = itemWork.Title;//作品标题
                    var LikesCount=itemWork.LikesCount;//点赞数
                    var Hits=itemWork.Hits;//浏览量
                    outStr+=
"                                <li class=\"work_item\" title='"+WorkTitle+";赞数"+LikesCount+";浏览量"+Hits+"'>\n" +
"                                    <a href=\"works_Details.html?workId="+WorkId+"\"><img src=\"http://localhost:6992/"+WorkImg+"\" height=\"155\" width=\"200\" alt=\"\"></a>\n" +
"                                </li>\n";
                });
            outStr+=
"                                <div class=\"seize_box\">\n" +
"                                    <div class=\"work_show_more\">\n" +
"                                        <a href=\"User_public.html?userId="+UserId+"\">\n" +
"                                            <div class=\"more_icon\">\n" +
"                                                <span></span>\n" +
"                                                <span></span>\n" +
"                                                <span></span>\n" +
"                                            </div></a></div></div></ul></div></div>\n";

        });
        var obj=$(".userList");
        obj.html(outStr);
    }
    //活动详情页，活动部分
function outActivity_Details(ActivityId,userId) {
    $.ajax({
    url: "http://localhost:6992/api/Activity/GetActivityInfoById",
        type: "Get",
        async: true,
        //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
        //dataType: "json",
        // contentType: "charset=utf-8",  ,"cat":Search
        data:{id:""+ActivityId+""},

        success: function (returndata) {
            display_outActivity_Details(returndata,userId)
        },
        error: function (returndata) { alert(returndata+"错误"); }
    });
}
function display_outActivity_Details(data,userId) {
    var outStr="";
    if(data!=""||data!=null){
        $.each(data,function (index,item) {
            var ActivityId = item.ActivityId;//活动id
            var ActivityImg = item.ActivityImg;//活动头像
            var ActivityTitle = item.ActivityTitle;//活动名称
            var ActivityDate=item.ActivityDate.substring(0, 10);//开始日期
            var EndTime=item.EndTime.substring(0, 10);//结束日期
            var LikesCount=item.LikesCount;//点赞
            var Hits=item.Hits;//浏览量
            var NumberOfWorks=item.NumberOfWorks;//总数
            var ActivityContent=item.ActivityContent;
            var ActivityState=item.ActivityState;//作品状态
            outStr+="            <div class=\"activity_content_top\">\n" +
                "                <div class=\"content_center_box\">\n" +
                "                    <div class=\"content_details_box\">\n" +
                "                        <span class=\"details_picture\">\n" +
                "                            <img src=\"http://localhost:6992/"+ActivityImg+"\" alt=\"\" width=\"440\" height=\"330\">\n" +
                "                        </span>\n" +
                "                        <h2 class=\"details_title\">"+ActivityTitle+"</h2>\n" +
                "                        <div class=\"activity_lie\">\n" +
                "                            开始时间:<span class=\"other-color\">"+ActivityDate+" </span><br>\n" +
                "                            结束时间:<span class=\"other-color\">"+EndTime+"</span><br>\n" +
                "                        </div>\n" +
                "                        <div class=\"details_describe_box\">\n" +
                "                            <div class=\"details_describe\">"+ActivityContent+"</div>\n" +
                "                        </div>\n" +
                "                        <div class=\"details_main_btn\">\n"
                if(ActivityState==0){
                outStr+=
                "                            <a href=\"javascript:void(0);\" class=\"details_uplode_btn over_zan\">上传活动作品</a>" +
                "                            <a href=\"javascript:void(0)\" class=\"details_uplode_btn details_zan_btn over_zan\" id=\" \"><i class=\"am-icon-thumbs-up am-icon-fw am-icon-md \"></i></a>\n"

                }else {
                    outStr+=
                "                            <a href=\"uploadWork.html?userId="+userId+"&sortId="+ActivityId+"\" class=\"details_uplode_btn \">上传活动作品</a>" +
                "                            <a href=\"javascript:void(0)\" class=\"details_uplode_btn details_zan_btn\" id=\"LikesCount_btn\"><i class=\"am-icon-thumbs-up am-icon-fw am-icon-md\"></i></a>\n"
                }
                outStr+=
                "                           </div>\n" +
                "                        <div class=\"details_performers\">\n" +
                "                            <a href=\"javascript:void(0);\" title=\"人气:"+LikesCount+"\" class=\"active-card-partake active-card-glance LikesCount\">\n" +
                "                                <i class=\" am-icon-thumbs-up am-icon-fw\"></i>"+LikesCount+"</a>\n" +
                "                            <a href=\"javascript:void(0);\" title=\"浏览量:"+Hits+"\" class=\"active-card-partake active-card-glance\">\n" +
                "                                <i class=\"am-icon-eye am-icon-fw\"></i>"+Hits+"</a>\n" +
                "                            <a href=\"javascript:void(0);\" title=\"总数:"+NumberOfWorks+"\" class=\"active-card-partake active-card-glance\">\n" +
                "                                <i class=\"am-icon-image am-icon-fw\"></i>"+NumberOfWorks+"</a>\n" +
                "                        </div>\n" +
                "                    </div>\n" +
                "                </div>\n" +
                "            </div>\n"
        });
    }else{
        window.location.href="activity.html";
        window.history.back(-1);
    }
    var obj=$("#xs_slides_box");
    obj.html(outStr);

}
//根据活动id输出作品
function outWorkByActivityId(ActivityId,page,outPages,lookState) {
    $.ajax({
        url: "http://localhost:6992/api/Works/GetWorkByActivityId",
        type: "Get",
        async: true,
        //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
        //dataType: "json",
        // contentType: "charset=utf-8",  ,"cat":Search
        data:{activityId:""+ActivityId+"",page:""+page+"",lookState:""+lookState+""},

        success: function (returndata) {
            display_outActivity_Works_list(returndata.Data);
            var pages=returndata.Total;
            if(outPages==pages){
                return false;
            }else{
                outPage(pages);//输出页码

                var outStr="<div id=\"page\" class=\"dsno\">"+pages+"</div>";
                var obj=$("#allPage");
                obj.html(outStr);
            }
        },
        error: function (returndata) { alert(returndata+"错误"); }
    });
}
function display_outActivity_Works_list(data) {
    var outStr="";
    if(data.length>0){
    $.each(data,function (index,item) {
        var WorkImg = item.WorkImg;//作品封面
        var WorksId = item.WorksId;//作品id
        var WorksTitle = item.WorksTitle;//标题
        var LikesCount = item.LikesCount;//点赞量
        var Hits = item.Hits;//浏览量
        var PublishedAt = item.PublishedAt;//发布时间
        var UserId = item.UserId;//用户id
        var UserName = item.UserName;//用户名
        var UserImg = item.UserImg;//用户头像
        var WorksSort = item.WorksSort;//作品分类
        var TimeDifference=item.TimeDifference;//时间差
        outStr+="<div class=\"xs_card_box xs_a_opacity\">" +
            "<div class=\"xs_card_img\">" +
            "<a href=\"works_Details.html?workId="+WorksId+"\">" +
            "<img class=\"xs_work_img\" src=\"http://localhost:6992/"+WorkImg+"\">" +
            "</a></div><div class=\"xs_card_info\">" +
            "<p class=\"xs_card_info_title\">" +
            "<a href=\"works_Details.html?workId="+WorksId+"\">"+WorksTitle+"</a></p><p class=\"xs_card_info_type\">"+WorksSort+"</p>" +
            "<p class=\"xs_card_info_item\">" +
            "<span class=\"statistics-view\" title=\"共"+LikesCount+"人气\"><i class=\" am-icon-thumbs-up am-icon-fw\"></i>"+LikesCount+"</span>" +
            "<span class=\"statistics-comment\" title=\"点击查看"+Hits+"次\"><i class=\"am-icon-eye am-icon-fw\"></i>"+Hits+"</span>" +
            //"<span class=\"statistics-tuijian\" title=\""+topWorkList+"\"><i class=\"am-icon-tencent-weibo am-icon-fw\"></i></span>" +
            "</p></div><div class=\"xs_card_item\">" +
            "<span class=\"xs_card_user_info\">" +
            "<img src=\"http://localhost:6992/"+UserImg+"\" alt=\"头像\">" +
            "<a href=\"User_public.html?userId="+UserId+"\" id='"+UserId+"'>"+UserName+"</a>" +
            "<span class=\"works_time\" title=\"审核通过时间:"+PublishedAt+"\">"+TimeDifference+"</span>" +
            "<div></div></span></div></div>";
    });
    }else {
        //    无内容输出
        outStr="<h2 class='noneText'>什么都没找到啊T T</h2>";
        var obj_noneText=$(".outNoneText");
        obj_noneText.html(outStr);
        return false;
    }
    var obj=$("#container");
    obj.html(outStr);
    $('#container').pinto();//图片排版js效果，在输出的时候就要调用不然会出样式问题

}
    //根据作品id输出作品信息
    function GetWorkInfoByWorkId(workId) {
        $.ajax({
            url: "http://localhost:6992/api/Works/GetWorkInfoByWorkId",
            type: "Get",
            async: true,
            //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
            //dataType: "json",
            // contentType: "charset=utf-8",  ,"cat":Search
            data:{workId:""+workId+""},
            success: function (returndata) {
                if(returndata.length>0){
                    display_Works_info(returndata);
                }else {//如果查到的值小于1
                    window.location.href="index.html";
                    window.history.back(-1);
                }
            },
            error: function (returndata) { alert(returndata+"错误"); }
        });
    }
    function display_Works_info(data) {

            var outStr_header_left="";//顶部左
        var outStr_header_right="";//顶部右
        var outStr_work_content="";//内容
        var zan_number_content="";//赞数
            $.each(data,function (index,item) {
                var WorkImg = item.WorkImg;//作品封面
                var WorksId = item.WorksId;//作品id
                var WorksTitle = item.WorksTitle;//标题
                var Content=item.Content;//内容
                var LikesCount = item.LikesCount;//点赞量
                var Hits = item.Hits;//浏览量
                var PublishedAt = item.PublishedAt;//发布时间
                var FileAddress=item.FileAddress;//作品附件
                var UserId = item.UserId;//用户id
                var UserName = item.UserName;//用户名
                var UserImg = item.UserImg;//用户头像
                var Userlabel=item.Userlabel;//个性签名
                var WorksSort = item.WorksSort;//作品分类
                var TimeDifference=item.TimeDifference;//时间差
                outStr_header_left+=
"                    <div class=\"details_contitle_box\">\n" +
"                        <h2 class=\"details_contitle_title\">"+WorksTitle+"</h2>\n" +
"                        <p title=\"发布时间："+PublishedAt+"；\" class=\"title-time\">\n" +
"                            <span>"+TimeDifference+"前</span>发布\n" +
"                        </p>\n" +
"                        <div class=\"work_head_box\">\n" +
"                            <div class=\"head_left\">\n" +
"                                <span class=\"head_index\">\n" +
"                                    <span><a href=\"\">"+WorksSort+"</a></span>\n" +
"                                    <i></i>\n" +
"                                </span>\n" +
"                            </div>\n" +
"                            <div class=\"head_right\">\n" +
"                                <span class=\"head_data_show\">\n" +
"                                    <a href=\"javascript:;\" title=\"共2571人气\" class=\"see vertical-line\">\n" +
"                                        <span>\n" +
"                                            <i class=\" am-icon-thumbs-up am-icon-fw\"></i>"+LikesCount+"\n" +
"                                        </span>\n" +
"                                        <span>\n" +
"                                            <i class=\"am-icon-eye am-icon-fw\"></i>"+Hits+"\n" +
"                                        </span>\n" +
"                                    </a>\n" +
"                                </span>\n" +
"                            </div>\n" +
"                        </div>\n" +
"                    </div>\n";
                outStr_header_right+=
"                    <div class=\"avatar-container\">\n" +
"                        <a href=\"User_public.html?userId="+UserId+"\" class=\"avatar\">\n" +
"                            <img src=\"http://localhost:6992/"+UserImg+"\" alt=\"\">\n" +
"                        </a>\n" +
"                    </div>\n" +
"                    <div class=\"author_info\">\n" +
"                        <p class=\"author_info_title\"><a href=\"User_public.html?userId="+UserId+"\">"+UserName+"</a></p>\n" +
"                        <div class=\"position_info\">\n" +
"                            <span>"+Userlabel+"</span>\n" +
"                        </div>\n" +
"                        <div class=\"btn_area\">\n" +
"                            <a href='User_public.html?userId="+UserId+"' class=\"btn_default_main\">\n" +
"                                详情\n" +
"                            </a>\n" +
"                        </div>\n" +
"                    </div>\n";
                if(FileAddress!=null){
                    outStr_work_content+=Content+
                        "<a href=\"javascript:void(0);\" class=\"FileDownload\" id=\""+FileAddress+"\">下载附件</a>";
                }else {
                    outStr_work_content+=Content
                }
                zan_number_content=LikesCount;
            });
        var obj_left=$(".left_details_head");
        var obj_right=$(".right_details_head");
        var obj_content=$(".cotent_work");
        var obj_zan_number=$(".recommend_number");
        obj_left.html(outStr_header_left);
        obj_right.html(outStr_header_right);
        obj_content.html(outStr_work_content);
        obj_zan_number.html(zan_number_content);

    }
    //根据作品id输出作品留言与回复
    function GetOnlineAndReplyByWorkId(placeId) {
        $.ajax({
            url: "http://localhost:6992/api/Online/GetOnlineInfoByPlaceId",
            type: "Get",
            async: true,
            data:{placeId:""+placeId+""},
            success: function (returndata) {
                display_Works_online(returndata);
            },
            error: function (returndata) { alert(returndata+"错误"); }
        });
    }
    //留言回复功能输出
    function display_Works_online(data) {
        var outStr="";
        if(data.length>0){
            $.each(data,function (index,item) {
            //留言部分
            var OnlineId = item.OnlineId;//留言id
            var PlaceId = item.PlaceId;//留言地点id
            var OnlineContent = item.OnlineContent;//留言内容
            var Creatime=item.Creatime.substring(0, 10);//留言时间
            var LikesCount = item.LikesCount;//留言点赞量
            var OnlineUserId = item.OnlineUserId;//留言人Id
            var OnlineUserName = item.OnlineUserName;//留言人姓名
            var OnlineUserImg = item.OnlineUserImg;//留言人头像
            var OnlineRelistCount = item.OnlineRelistCount;//留言下回复的条数

            var idlist=PlaceId+"_"+OnlineId+"_"+OnlineUserId;
            outStr+=
"                <li class=\"comment_list\">\n" +
"                    <div class=\"comment_list_box\">" +
"                        <div class=\"comment_list_userImg\">\n" +
"                            <a href=\"User_public.html?userId="+OnlineUserId+"\" class=\"userImg\">\n" +
"                                <img src=\"http://localhost:6992/"+OnlineUserImg+"\" alt=\"\">\n" +
"                            </a>\n" +
"                        </div>\n" +
"                        <!--用户名-->\n" +
"                        <div class=\"timebox\">\n" +
"                            <a href=\"User_public.html?userId="+OnlineUserId+"\" class=\"usernick\">"+OnlineUserName+"</a>\n" +
"                            <span class=\"issuetimer\">\n" +
"                                <span class=\"time\">"+Creatime+"</span>\n" +
"                            </span>\n" +
"                        </div>\n" +
"                        <!--留言-->\n" +
"                        <p class=\"commoncon\">\n" +
"                            <span class=\"commontxt\">"+OnlineContent+"</span>\n" +
"                        </p>";

            //回复部分
            var ReplyList = item.ReplyList;//回复列表
            if(ReplyList!=null){
                $.each(ReplyList,function (indexReply,itemReply) {

                    var ReplyId=itemReply.ReplyId;//回复id
                    var OnlineId = itemReply.OnlineId;//留言id
                    var Recontent=itemReply.Recontent;//回复内容
                    var Retime=itemReply.Retime.substring(0, 10);//回复时间
                    var ReplyUserId=itemReply.ReplyUserId;//回复用户id
                    var ReplyUserName=itemReply.ReplyUserName;//回复人姓名
                    outStr+=
    "                        <div class=\"quote_content_wrap\">\n" +
    "                            <i></i>\n" +
    "                            <div class=\"usernicks\">\n" +
    "                                <div class=\"usernick_box\">\n" +
    "                                    <a href=\"User_public.html?userId="+ReplyUserId+"\" class=\"designer_name\">"+ReplyUserName+"</a>\n" +
                                            "<span class=\"issuetimer\"><span class=\"time\">"+Retime+"</span></span>"+
    "                                </div>\n" +
    "                                <span class=\"userreply\">"+Recontent+"</span>\n" +
    "                            </div>\n" +
    "                        </div>";
                });
            }
            outStr+=
"                        <div class=\"commenticons\">\n" +
"                            <div class=\"ricons\">\n" +//"+ReplyId+"_"+OnlineId+"_"+ReplyUserId+"
"                                <a href=\"javascript:void(0)\" class=\"comment_news reply_btn\" id='"+idlist+"'>回复<span>"+OnlineRelistCount+"</span></a>\n" +
"                                <a href=\"javascript:void(0)\" class=\"comment_zan OnlineId_"+OnlineId+"\"><i class='am-icon-thumbs-up am-icon-fw'></i><span>"+LikesCount+"</span></a>\n" +
"                            </div>\n" +
"                        </div>\n" +
"                        <!--输出时把留言的id付给回复按钮和回复宽，点击回复按年显示相应的回复框-->\n" +
"                        <div class=\"reply_text "+idlist+"\" style=\"display: none;\">\n" +
"                            <input type=\"text\" class=\"workName_text OnlineIdText_"+OnlineId+"\" >" +
                "           <a href=\"javacript:void(0)\" class='upBtn OnlineId_"+OnlineId+"'>发送</a>\n" +
"                        </div></div></li>"
        });

        }else {
            //    无内容输出
            outStr="<h2 class='noneText'>什么都没找到啊T T</h2>";
            var obj_noneText=$(".commentcon");
            obj_noneText.html(outStr);
            return false;
        }
        var obj=$(".commentcon");
        obj.html(outStr);

    }
//用户个人信息公共页/私人页输出
    function GetUserInfoById(userId,selectId) {
        $.ajax({
            url: "http://localhost:6992/api/UserInfo/GetUserInfoByID",
            type: "Get",
            async: true,
            data:{Id:""+userId+""},
            success: function (returndata) {
                if(selectId==1){
                    display_UserInfoByUserId_personal(returndata);//私人页
                }else {
                display_UserInfoByUserId(returndata);
                }
            },
            error: function (returndata) { alert(returndata+"错误"); }
        });
    }
    function display_UserInfoByUserId(data) {
        var outStr="";
        if(data!=null){
            $.each(data,function (index,item) {
                var UserId = item.UserId;//用户id
                var UserImg = item.UserImg;//用户封面
                var UserName = item.UserName;//用户名
                var UserWorksum = item.UserWorksum;//点赞量
                var Userlabel = item.Userlabel;//浏览量
                outStr+=
                    "<div class=\"home_center\">\n" +
"                            <div class=\"information_headimg_box\">\n" +
"                                <img src=\"http://localhost:6992/"+UserImg+"\" alt=\"\">\n" +
"                            </div>\n" +
"                        </div>\n" +
"                        <div class=\"text_center\">\n" +
"                            <p class=\"people_nick_name\">"+UserName+"</p>\n" +
"                            <p class=\"user_background\">创作:"+UserWorksum+"</p>\n" +
"                            <p class=\"user-signature\">"+Userlabel+"</p>\n" +
"                            <!--把数据库中能输出的都输出-->\n" +
"                        </div>";
            });
        }else {
        //    为空
            window.location.href="index.html";
            window.history.back(-1);
        }
        var obj=$(".home_information_box");
        obj.html(outStr);
        // $('#container').pinto();//图片排版js效果，在输出的时候就要调用不然会出样式问题
    }
    function display_UserInfoByUserId_personal(data) {
        var outStr="";
        if(data.length>0){
            $.each(data,function (index,item) {
                var UserId = item.UserId;//用户id
                var UserImg = item.UserImg;//用户封面
                var UserName = item.UserName;//用户名
                var Userlabel=item.Userlabel;//个性签名
                outStr+=
"                        <div class=\"card_center\">\n" +
"                            <a href=\"User_personal.html?userId="+UserId+"\"><img class='userImg_personal' src=\"http://localhost:6992/"+UserImg+"\" alt=\"\"></a>\n" +
"                        </div>\n" +
"                        <div class=\"presonal_info\">\n" +
"                            <a href=\"\">"+UserName+"</a>\n" +
"                        </div>\n" +
"                        <div class=\"position_info\">\n" +
"                            <a href=\"\">"+Userlabel+"</a>\n" +
"                        </div>";
            });
        }else {
            //    为空
            window.location.href="index.html";
            window.history.back(-1);
        }
        var obj=$(".card_media");
        obj.html(outStr);
    }
    //根据用户id输出作品列表
    function GetWorkListByUserId(lookState,page,userId,outPages) {
        $.ajax({
            url: "http://localhost:6992/api/Works/GetWorkInfoByUserId",
            type: "Get",
            async: true,
            data:{
                lookState:""+lookState+"",
                page:""+page+"",
                userId:""+userId+"",
            },
            success: function (returndata) {
                display_Works_listByUserId(returndata.Data);
                var pages=returndata.Total;
                if(outPages==pages){
                    return false;
                }else{
                    outPage(pages);//输出页码

                    var outStr="<div id=\"page\" class=\"dsno\">"+pages+"</div>";
                    var obj=$("#allPage");
                    obj.html(outStr);
                }
            },
            error: function (returndata) { alert(returndata+"错误"); }
        });
    }

    //根据用户Id输出作品信息,用于用户私人页的作品输出
    //页码，用户id，类型id，是否展示，是否软删除，当前总页码
    function GetWorkInfoByUserIdForUserPersonal
    (page, userId, sortId, if_show, if_Deleted,outPages,TorF) {
        $.ajax({
            url: "http://localhost:6992/api/Works/GetWorkInfoByUserIdForUserPersonal",
            type: "Get",
            async: true,
            data:{
                page:""+page+"",
                userId:""+userId+"",
                sortId:""+sortId+"",
                if_show:""+if_show+"",
                if_Deleted:""+if_Deleted+"",
            },
            success: function (returndata) {
                display_Works_listByUserId(returndata.Data,TorF);
                var pages=returndata.Total;
                if(outPages==pages){
                    return false;
                }else{
                    outPage(pages);//输出页码

                    var outStr="<div id=\"page\" class=\"dsno\">"+pages+"</div>";
                    var obj=$("#allPage");
                    obj.html(outStr);
                }
            },
            error: function (returndata) { alert(returndata+"错误"); }
        });
    }

    function display_Works_listByUserId(data,TorF) {
    var outStr="";
    if(data.length>0){
        $.each(data,function (index,item) {
            var WorkImg = item.WorkImg;//作品封面
            var WorksId = item.WorksId;//作品id
            var WorksTitle = item.WorksTitle;//标题
            var LikesCount = item.LikesCount;//点赞量
            var Hits = item.Hits;//浏览量
            var PublishedAt = item.PublishedAt;//发布时间
            var UserId = item.UserId;//用户id
            var UserName = item.UserName;//用户名
            var UserImg = item.UserImg;//用户头像
            var WorksSort = item.WorksSort;//作品分类
            var TimeDifference=item.TimeDifference;//时间差
            outStr+="<div class=\"xs_card_box xs_a_opacity\">"
            if(TorF===1){
                outStr+="<a class='am-close delWork' id='delWorkId_"+WorksId+"' onclick='delWork(this.id);'><span>X</span></a>"
                        +"<input class=\"checkboxes \" value='"+WorksId+"' type=\"checkbox\"   name=\"checkName\" >"

            }else {//删除
                outStr+="<a class='am-close delWork revertWork' id='delWorkId_"+WorksId+"' onclick='revertWork(this.id);'><span>还原</span></a>"
                    +"<input class=\"checkboxes delInput\"  type=\"checkbox\" value='"+WorksId+"'  name=\"checkName\" >"

            }
            outStr+="<div class=\"xs_card_img\">" +
                "<a href=\"works_Details.html?workId="+WorksId+"\">" +
                "<img class=\"xs_work_img\" src=\"http://localhost:6992/"+WorkImg+"\">" +
                "</a></div><div class=\"xs_card_info\">" +
                "<p class=\"xs_card_info_title\">" +
                "<a href=\"works_Details.html?workId="+WorksId+"\">"+WorksTitle+"</a></p><p class=\"xs_card_info_type\">"+WorksSort+"</p>" +
                "<p class=\"xs_card_info_item\">" +
                "<span class=\"statistics-view\" title=\"共"+LikesCount+"人气\"><i class=\" am-icon-thumbs-up am-icon-fw\"></i>"+LikesCount+"</span>" +
                "<span class=\"statistics-comment\" title=\"点击查看"+Hits+"次\"><i class=\"am-icon-eye am-icon-fw\"></i>"+Hits+"</span>" +
                //"<span class=\"statistics-tuijian\" title=\""+topWorkList+"\"><i class=\"am-icon-tencent-weibo am-icon-fw\"></i></span>" +
                "</p></div><div class=\"xs_card_item\">" +
                "<span class=\"xs_card_user_info\">" +
                "<img src=\"http://localhost:6992/"+UserImg+"\" alt=\"头像\">" +
                "<a href=\"User_public.html?userId="+UserId+"\" id='"+UserId+"'>"+UserName+"</a>" +
                "<span class=\"works_time\" title=\"审核通过时间:"+PublishedAt+"\">"+TimeDifference+"</span>" +
                "<div></div></span></div></div>";
        });
    }
    else {
        //    无内容输出
        outStr="<h2 class='noneText'>什么都没找到啊T T</h2>";
        var obj_noneText=$(".outNoneText");
        obj_noneText.html(outStr);
        return false;
    }
        var obj=$("#container");
        obj.html(outStr);
        $('#container').pinto();//图片排版js效果，在输出的时候就要调用不然会出样式问题
        return false;
}
    //根据输出用户所有信息
    function GetAllUserInfoById(UserId){
        $.ajax({
            url: "http://localhost:6992/api/UserInfo/GetAllUserInfoById",
            type: "Get",
            async: true,
            data:{Id:UserId},
            success: function (returndata){
                display_GetAllUserInfoById(returndata.extra)
            },
            error: function (returndata) { alert(returndata+"错误"); }
        });
    }
    function display_GetAllUserInfoById(data){
        var outImg="";
        var outName="";
        //$.each(data,function (index,item) {
            var WorkId = data.Id;//用户id
            var UserName = data.UserName;//用户名
            var ActualName = data.ActualName;//用户真实姓名
            var Userlabel = data.Userlabel;//用户个性签名
            var UserPhone = data.UserPhone;//用户手机号
            var Email = data.Email;//用户邮箱
            var UserImg = data.UserImg;//用户封面
            outImg="<img class='userImg_personal' id='userImg' src=\"http://localhost:6992/"+UserImg+"\" alt=\"\">";
            outName="<span class=\"user_text_center\" id='username_update'>"+UserName+"</span>" +
                "<a href=\"javascript:void(0);\" class=\"user_name_modify\">修改</a>";
            var obj_Img=$(".userImg_box");
            obj_Img.html(outImg);
            var obj_Name=$(".user_name_box");
            obj_Name.html(outName);
            $(".input_userPhon").val(UserPhone);
            $(".input_userPhon_hidden").val(UserPhone);
            $(".input_userEmail").val(Email);
            $(".input_userEmail_hidden").val(Email);
            $(".input_actualName").val(ActualName);
            $(".input_actualName_hidden").val(ActualName);
            $(".input_userlabel").val(Userlabel);
            $(".input_userlabel_hidden").val(Userlabel);

        //})
    }
















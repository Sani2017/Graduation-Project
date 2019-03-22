//用于admin的用户列表输出
// lookState:查看状态,sortId：类型id,
// suerSearchValue：搜索内容,clickPage分页页码,outPages总页数，userState用户状态
function outUserListByPase(suerSearchValue,clickPage,outPages,newOutPage,userState) {
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
        url: "http://localhost:6992/api/Admin/selectUserAndWorkSum",
        type: "Get",
        async: true,
        //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
        //dataType: "json",
        // contentType: "charset=utf-8",  ,"cat":Search
        data:{searchValue:""+searchValue+"",
            page:""+page+"",
            userState:""+userState+""},
        //cache: false,
        success: function (returndata) {
            // alert(returndata+"正确");
            //var sdf=returndata.Data;
            display_outUsersList(returndata.Data);
            var pages=returndata.Total;
            if(newOutPage==1){
                outPage(pages,newOutPage);//输出页码

                var outStr="<div id=\"page\" class=\"dsno\">"+pages+"</div>";
                var obj=$("#allPage");
                obj.html(outStr);
            }else {
                if(outPages==pages){
                    return false;
                }else{
                    outPage(pages);//输出页码

                    var outStr="<div id=\"page\" class=\"dsno\">"+pages+"</div>";
                    var obj=$("#allPage");
                    obj.html(outStr);
                }
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
        var UserState = item.UserState;//用户状态(1.启用，0.不启用)默认1
        if(UserState==1){//启用
            if(index%2!=0){
                outStr+="<tr class=\"gradeX\">"
            }else {
                outStr+="<tr class=\"even gradeC\">"

            }
            outStr+=
                "                                        <td><input class=\"checkboxes \" value=\""+UserId+"\" type=\"checkbox\" name=\"checkName\"></td>\n" +
                "                                        <td>"+UserName+"</td>\n" +
                "                                        <td>"+UserWorksum+"</td>\n" +
                "                                        <td>\n" +
                "                                            <div class=\"tpl-table-black-operation\">\n"
                outStr+=
                    "                                                <a href=\"javascript:;\" id='none_"+UserId+"' onclick='delUserF(this.id)'>\n" +
                    "                                                    <i class=\"am-icon-pencil\"></i> 禁用\n" ;
                "                                                </a>\n" ;

            outStr+=
                "                                            </div>\n" +
                "                                        </td>\n" +
                "                                    </tr>\n";
        }else {
            if(index%2!=0){
                outStr+="<tr class=\"gradeX\">"
            }else {
                outStr+="<tr class=\"even gradeC\">"

            }
            outStr+=
                "                                        <td><input class=\"checkboxes \" value=\""+UserId+"\" type=\"checkbox\" name=\"checkName\"></td>\n" +
                "                                        <td>"+UserName+"</td>\n" +
                "                                        <td>"+UserWorksum+"</td>\n" +
                "                                        <td>\n" +
                "                                            <div class=\"tpl-table-black-operation\">\n"
            outStr+=
                "                                                <a href=\"javascript:;\" id='none_"+UserId+"' onclick='delUserT(this.id)'>\n" +
                "                                                    <i class=\"am-icon-pencil\"></i> 启用\n"+
            "                                                </a>\n" ;
            outStr+=
                "                                            </div>\n" +
                "                                        </td>\n" +
                "                                    </tr>\n";
        }

});
    var obj=$("#outputUserList");
    obj.html(outStr);

}
// 用于输出各种网站所需列表
//作品类型全输出api/Sort/GetAllSort",
function outSort(){
    $.ajax({
        url: "http://localhost:6992/api/Admin/getSortNameAndCount",
        type: "Get",
        async: true,
        //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
        //dataType: "json",
        // contentType: "charset=utf-8",  ,"cat":Search
        //data:,
        cache: false,
        success: function (returndata) {
            var outStr="";
            $.each(returndata,function (index,item) {
                var SortWorkCount=item.SortWorkCount;
                var SortName=item.SortName;//分類名稱
                var SortId=item.Id;//分類Id
                if(index%2!=0){
                    outStr+="<tr class=\"gradeX\">"
                }else {
                    outStr+="<tr class=\"even gradeC\">"

                }
                outStr+=
"                      <td>"+SortId+"</td>\n" +
"                      <td class='sortName_"+SortId+"'>"+SortName+"</td>\n" +
"                      <td>"+SortWorkCount+"</td>\n" +
"                      <td>\n" +
"                          <div class=\"tpl-table-black-operation\">" +
                    "    <a href=\"javascript:;\" onclick='updateText(this.id);' id='upSort_"+SortId+"'>\n" +
                    "        <i class=\"am-icon-pencil\"></i> 编辑</a>"+
"                              <a href=\"javascript:;\"  onclick='delSort(this.id);' id='sort_"+SortId+"' class=\"tpl-table-black-operation-del\">\n" +
"                                  <i class=\"am-icon-trash\"></i> 删除\n" +
"                              </a>\n" +
"                          </div>\n" +
"                      </td>\n" +
"                  </tr>";
            });
            var obj=$("#outputUserList");
            obj.html(outStr);
        },
        error: function (returndata) { alert(returndata+"错误"); }
    });
}
function getAllowShowWorkAndUserInfo(page,outPages,selectVal) {
    $.ajax({
        url: "http://localhost:6992/api/Admin/getAllowShowWorkAndUserInfo",
        type: "Get",
        async: true,
        //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
        //dataType: "json",
        // contentType: "charset=utf-8",  ,"cat":Search
        data:{
            page:""+page+"",selectVal:selectVal},
        //cache: false,
        success: function (returndata) {
            // alert(returndata+"正确");
            //var sdf=returndata.Data;
            display_getAllowShowWorkAndUserInfo(returndata.Data);
            var pages=returndata.Total;
            // if(newOutPage==1){
            //     outPage(pages,newOutPage);//输出页码
            //
            //     var outStr="<div id=\"page\" class=\"dsno\">"+pages+"</div>";
            //     var obj=$("#allPage");
            //     obj.html(outStr);
            // }else {
                if(outPages==pages){
                    return false;
                }else{
                    outPage(pages);//输出页码

                    var outStr="<div id=\"page\" class=\"dsno\">"+pages+"</div>";
                    var obj=$("#allPage");
                    obj.html(outStr);
                }
            // }
            //$("#page").html(pages);
            //把从后台获取的页码保存在放在dom中，当点击当个页码时获取并与的新页码进行比对
            //如果相同则return false 结束，else渲染新的分页页码。
        },
        error: function (returndata) { alert(returndata+"错误"); }
    });

}
function display_getAllowShowWorkAndUserInfo(data) {
    var outStr="";
    $.each(data,function (index,item) {
        var WorkImg = item.WorkImg;//作品封面
        var WorksId = item.WorksId;//作品id
        var WorksTitle = item.WorksTitle;//作品名称
        var Content = item.Content;//作品内容
        var CreatedAt = item.CreatedAt.substring(0, 10);//用户发布时间
        var UserId = item.UserId;//用户id
        var UserImg = item.UserImg;//用户头像
        var UserName = item.UserName;//用户名称
        var WorksSort = item.WorksSort;//类型
        outStr+=
"                            <div class=\"am-u-sm-12 am-u-md-6 am-u-lg-4\">\n" +
"                                <div class=\"tpl-table-images-content\">\n" +
"                                    <div class=\"tpl-table-images-content-i-time\">发布时间："+CreatedAt+"</div>\n" +
"                                    <div class=\"tpl-i-title\">\n" +
"                                        "+WorksTitle+"\n" +
"                                    </div>\n" +
"                                    <a href=\"javascript:;\" class=\"tpl-table-images-content-i\">\n" +
"                                        <div class=\"tpl-table-images-content-i-info\">\n" +
"                                            <span class=\"ico\">\n" +
"                                    <img src=\"http://localhost:6992/"+UserImg+"\" alt=\"\">"+UserName+"\n" +
"                                 </span>\n" +
"                                        </div>\n" +
"                                        <span class=\"tpl-table-images-content-i-shadow\"></span>\n" +
"                                        <span style=\"height: 215px!important;width:auto ;display:block;overflow: hidden\">\n" +
"                            <img src=\"http://localhost:6992/"+WorkImg+"\" alt=\"\"></span>\n"+
"                                    </a>\n" +
"                                    <div class=\"tpl-table-images-content-block\">\n" +
"                                        <div class=\"tpl-i-font\">\n" +
"                                            "+Content+"\n" +
"                                        </div>\n" +
"                                        <div class=\"am-btn-toolbar\">\n" +
"                                            <div class=\"am-btn-group am-btn-group-xs tpl-edit-content-btn\">\n" +
            "<button onclick='upWork("+WorksId+");' type=\"button\" class=\"am-btn am-btn-default am-btn-warning\"><span class=\"am-icon-archive\"></span> 审核</button>\n" +
"             <button onclick='delWork("+WorksId+");'type=\"button\" class=\"am-btn am-btn-default am-btn-danger\"><span class=\"am-icon-trash-o\"></span> 删除</button>\n" +
"                                            </div>\n" +
"                                        </div>\n" +
"                                    </div>\n" +
"                                </div>\n" +
"                            </div>\n";
});
    // outStr+="<div class=\"am-u-lg-12\">\n" +
    //     "                                <div class=\"am-cf\">\n" +
    //     "                                    <div class=\"am-fr\"><div id=\"page3\"><!--分页页码的显示-->\n" +
    //     "                        </div>\n" +
    //     "                        <div id=\"tips3\" class=\"dsno\"></div>\n" +
    //     "                        <div id=\"allPage\"></div></div></div></div>";
var obj=$("#outputWorksList");
obj.html(outStr);
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
            "                                    <span><a href=\"javascript:void(0);\">"+WorksSort+"</a></span>\n" +
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
            "                        <a href=\"javascript:void(0);\" class=\"avatar\">\n" +
            "                            <img src=\"http://localhost:6992/"+UserImg+"\" alt=\"\">\n" +
            "                        </a>\n" +
            "                    </div>\n" +
            "                    <div class=\"author_info\">\n" +
            "                        <p class=\"author_info_title\"><a href=\"javascript:void(0);\">"+UserName+"</a></p>\n" +
            "                        <div class=\"position_info\">\n" +
            "                            <span>"+Userlabel+"</span>\n" +
            "                        </div>\n" +
            "                        <div class=\"btn_area\">\n" +
            "                            <a href='javascript:void(0);' class=\"btn_default_main\">\n" +
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

function GetActivityInfo(lookState, page, searchId, selectVal,outPages) {
    $.ajax({
        url: "http://localhost:6992/api/Admin/GetActivityInfo",
        type: "Get",
        async: true,
        //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
        //dataType: "json",
        // contentType: "charset=utf-8",  ,"cat":Search
        data:{
            lookState:""+lookState+"",
            page:page,
            searchId:""+searchId+"",
            selectVal:selectVal,
        },
        //cache: false,
        success: function (returndata) {
            // alert(returndata+"正确");
            //var sdf=returndata.Data;
            display_GetActivityInfo(returndata.Data);
            var pages=returndata.Total;
            // if(newOutPage==1){
            //     outPage(pages,newOutPage);//输出页码
            //
            //     var outStr="<div id=\"page\" class=\"dsno\">"+pages+"</div>";
            //     var obj=$("#allPage");
            //     obj.html(outStr);
            // }else {
            if(outPages==pages){
                return false;
            }else{
                outPage(pages);//输出页码

                var outStr="<div id=\"page\" class=\"dsno\">"+pages+"</div>";
                var obj=$("#allPage");
                obj.html(outStr);
            }
            // }
            //$("#page").html(pages);
            //把从后台获取的页码保存在放在dom中，当点击当个页码时获取并与的新页码进行比对
            //如果相同则return false 结束，else渲染新的分页页码。
        },
        error: function (returndata) { alert(returndata+"错误"); }
    });

}
function display_GetActivityInfo(data) {
    var outStr="";
    $.each(data,function (index,item) {
        var ActivityId = item.ActivityId;//活动id
        var ActivityImg = item.ActivityImg;//活动图片
        var ActivityTitle = item.ActivityTitle;//活动名称
        var ActivityDate = item.ActivityDate.substring(0, 10);//活动开始时间
        var EndTime = item.EndTime.substring(0, 10);//活动结束时间
        var ActivityTimeDifference = item.ActivityTimeDifference;//活动状态
        var LikesCount = item.LikesCount;//赞
        var NewTimeDifference = item.NewTimeDifference;//距今时间
        var Hits = item.Hits;//浏览
        var NumberOfWorks = item.NumberOfWorks;//作品数
        var ActivityContent = item.ActivityContent;//活动内容
        outStr+=
"                            <div class=\"am-u-sm-12 am-u-md-6 am-u-lg-4\">\n" +
"                                <div class=\"tpl-table-images-content\">\n" +
"                                    <div class=\"tpl-table-images-content-i-time\" title='开始时间："+ActivityDate+"，结束时间："+EndTime+"'>"+ActivityTimeDifference+"</div>\n" +
"                                    <div class=\"tpl-i-title\">\n" +
"                                        "+ActivityTitle+"\n" +
"                                    </div>\n" +
"                                    <a style=\"height: 215px!important;width:auto ;display:block;overflow: hidden\" href=\"javascript:;\" class=\"tpl-table-images-content-i\">\n" +
"                                        <span class=\"tpl-table-images-content-i-shadow\" ></span>\n" +
"                                        <img src=\"http://localhost:6992/"+ActivityImg+"\" alt=\"\">\n" +
"                                    </a>\n" +
"                                    <div class=\"tpl-table-images-content-block\">\n" +
"                                        <div class=\"tpl-i-font\">\n" +
"                                            "+ActivityContent+"\n" +
"                                        </div>\n" +
"                                        <div class=\"tpl-i-more\">\n" +
"                                            <ul>\n" +
"                                                <li><span class=\"am-icon-thumbs-up am-text-warning\"> "+LikesCount+"</span></li>\n" +
"                                                <li><span class=\"am-icon-eye am-text-success\"> "+Hits+"</span></li>\n" +
"                                                <li><span class=\"am-icon-img font-green\"> "+NumberOfWorks+"</span></li>\n" +
"                                            </ul>\n" +
"                                        </div>\n" +
"                                        <div class=\"am-btn-toolbar\">\n" +
"                                            <div class=\"am-btn-group am-btn-group-xs tpl-edit-content-btn\">\n" +
"                                                <button onclick='activityContent("+ActivityId+");' type=\"button\" class=\"am-btn am-btn-default am-btn-secondary\"><span class=\"am-icon-edit\"></span> 编辑</button>\n" +
"                                                <button onclick='del("+ActivityId+");'  type=\"button\" class=\"am-btn am-btn-default am-btn-danger\"><span class=\"am-icon-trash-o\"></span> 删除</button>\n" +
"                                            </div>\n" +
"                                        </div>\n" +
"                                    </div>\n" +
"                                </div>\n" +
"                            </div>\n";
    });
    // outStr+="<div class=\"am-u-lg-12\">\n" +
    //     "                                <div class=\"am-cf\">\n" +
    //     "                                    <div class=\"am-fr\"><div id=\"page3\"><!--分页页码的显示-->\n" +
    //     "                        </div>\n" +
    //     "                        <div id=\"tips3\" class=\"dsno\"></div>\n" +
    //     "                        <div id=\"allPage\"></div></div></div></div>";
    var obj=$("#outputWorksList");
    obj.html(outStr);
}

function outActivityInfoById(WorkIdValueUrl) {
    $.ajax({
        url: "http://localhost:6992/api/Activity/GetActivityInfoById",
        type: "Get",
        async: true,
        //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
        //dataType: "json",
        // contentType: "charset=utf-8",  ,"cat":Search
        data:{
            id:WorkIdValueUrl
        },
        //cache: false,
        success: function (returndata) {

            display_outActivityInfoById(returndata);
            
        },
        error: function (returndata) { alert(returndata+"错误"); }
    });

}
function display_outActivityInfoById(data) {
    var outStr="";
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
        $("#activity_name").val(ActivityTitle);

        // outStr=
        // "<input type=\"text\" class=\"tpl-form-input\" id=\"activity_name\" placeholder=\"请输入标题文字\" value=\""+EndTime+"\">"+
        // "<small class=\"error_text \">请填写标题文字10-20字左右。</small>";


        $("#endTime").val(EndTime);
        $("#img_preview").attr("src",
            "http://localhost:6992/"+ActivityImg);
        $("#none_imgSrc").text(ActivityImg);
        $(".w-e-text").html(ActivityContent);
        $(".openTime").text(ActivityDate);
        //editor.cmd.do('insertHTML', ActivityContent)
    });
    // var obj=$(".timeInput");
    // obj.html(outStr);
}




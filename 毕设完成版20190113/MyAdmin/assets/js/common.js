//页面进入时判断当前用户是否有效
function Judge_login(userName,token,fun) {
    var a="t";
    var b="f";
    $.ajax({
        url: "http://localhost:6992/api/LoginToken/token",
        type: "post",
        async: false,
        //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
        //dataType: "json",
        // contentType: "charset=utf-8",  ,"cat":Search
        data:{UserName:""+userName+"",Token:""+token+""},
        //cache: false,
        success: function (returndata) {
                if(typeof fun==='function'){
                    fun(returndata.suc);
                }
        },
        error: function (returndata) { alert(returndata+"错误"); }
    });
}

//从url取值，判断是否进行邮箱密码重置功能
function GetQueryStringByUrl(name)
{
    var reg = new RegExp("(^|&)"+ name +"=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);//色arch,查询？后面的参数，并匹配正则
    if(r!=null) return  decodeURI(r[2]); return null;
}
function exit() {

    var AdminName=$.cookie("AdminName");
    $.ajax({
        url: "http://localhost:6992/api/UserInfo/SafetyExit",
        type: "get",
        async: false,
        //data: _json, //不能直接写成 {id:"123",code:"tomcat"}
        //dataType: "json",
        // contentType: "charset=utf-8",  ,"cat":Search
        data:{UserName:""+AdminName+""},
        //cache: false,
        success: function (returndata) {
            window.location.href="Login.html";
            window.history.back(-1);
        },
        error: function (returndata) { alert(returndata+"错误"); }
    });
}
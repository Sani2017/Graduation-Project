//上下键 选择事件 keydown document searchBackgroud 为样式，只做标记，无实质样式，因为和其他样式不兼容，只能添加CSS
$('#xs_search_input').keyup(function (event) {
    var upDownClickNum = $(".search-content-list .searchBackgroud ").length;

    if (event.keyCode != 38 && event.keyCode != 40 && event.keyCode!=13) {
        var seleName=$('#xs_search_input').val();
        outSelectList(seleName);
        return;
    }

    //if ($(".search-content-list").css("display") == "block") {
      //  alert(event.keyCode)
        //38:上  40:下
    if (event.keyCode == 38) {
        if (upDownClickNum < 1) {
            $(".search-l:last a")
                .addClass('active')
                //.css({ "background": "#f0f0f0" })
                .addClass("searchBackgroud");

                $('#xs_search_input').val($('.searchBackgroud').text());
        } else {
            $(".searchBackgroud ")
                .removeClass("searchBackgroud active")
                .css({ "background": "" })
                .parent().prev().children()//.next()////
                .addClass("searchBackgroud")
                .addClass('active')
                $('#xs_search_input').val($('.searchBackgroud').text());
            //.css({ "background": "#f0f0f0" });
        }

        stopDefault(event);//不阻止光标户向前移动
    } else if (event.keyCode == 40) {
        if (upDownClickNum < 1) {
            $(".search-l:first a")//li:first a"
            //$(".search-content-list .search-l:first a")//li:first a"
                .addClass('active')
                //.css({ "background": "#f0f0f0" })
                .addClass("searchBackgroud");
                $('#xs_search_input').val($('.searchBackgroud').text());

        } else {
            $(".searchBackgroud")
                .removeClass("searchBackgroud active")
                .css({ "background": "" })
                .parent().next().children()
                .addClass("searchBackgroud")
                .addClass('active')
            //.css({ "background": "#f0f0f0" });
            $('#xs_search_input').val($('.searchBackgroud').text());


        }
        stopDefault(event);
    }
    upDownClickNum++;

    if(event.keyCode ==13){ // enter 键
        var searchValue=$("#xs_search_input").val();
        selectContent(searchValue);
    }

});
$("#xs_input_search").click(function(){
    var searchValue=$("#xs_search_input").val();
    selectContent(searchValue);
});
//阻止事件执行
function stopDefault(e) {

    //阻止默认浏览器动作(W3C)
    if (e && e.preventDefault) {
        //火狐的 事件是传进来的e
        e.preventDefault();
    }
    //IE中阻止函数器默认动作的方式
    else {
        //ie 用的是默认的event
        event.returnValue = false;
    }
}
//用户查询的值，并跳转到发现页面
function selectContent(selectValue){
    if(selectValue!=""){
        //window.location.replace("http://127.0.0.1:5500/""+selectValue+""");//跳转到首页，且不能回退
        window.location.href="search.html?word="+selectValue+"";

//         window.history.back(-1);
    }else{
        clk();//关闭查询框，显示导航
    }
}
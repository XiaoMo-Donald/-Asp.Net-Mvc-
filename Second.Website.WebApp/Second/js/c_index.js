var MenuState=0;
window.onresize=function(){
    //手机端和Pad端菜单栏
    $(".head .m-inner-nav").css({"display":"none"});
    MenuState=0;
    $(".head .m-nav img").attr("src","/Content/Second/images/head_nav_menu_o.png");
};

$(function(){
    //手机端菜单js
    //手机端的菜单显示状态0为默认不打开 1 为打开
    $(".head .m-nav").click(function(){
        if(MenuState==0){
            $(".head .m-nav img").attr("src","/Content/Second/images/head_nav_menu_c.png");
            $(".head .m-inner-nav").css({"display":"block"});
            MenuState=1;
        }
        else{
            $(".head .m-inner-nav").css({"display":"none"});
            $(".head .m-nav img").attr("src","/Content/Second/images/head_nav_menu_o.png");
            MenuState=0;
        }
    })
    //菜单栏菜单列表效果
    $(".head .m-inner-nav a").each(function( index ) {
        $( this ).css({'animation-delay': (index/10)+'s'});
        $( this).click(function () {
            $(".head .m-inner-nav").css({"display":"none"});
            $(".head .m-nav img").attr("src","/Content/Second/images/head_nav_menu_o.png");
            MenuState=0;
        });
    });

});
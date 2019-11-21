/**
 * Created by 小小莫先森 on 2016/12/16.
 */
    //当浏览器窗口大小改变时，设置显示内容的高度
var MenuState=0;
window.onresize=function(){
        changeBannerHeight();
        //手机端和Pad端菜单栏
        $(".head .m-inner-nav").css({"display":"none"});
        MenuState = 0;
        $(".head .m-nav img").attr("src", "/Second/images/head_nav_menu_o.png");
}
function changeBannerHeight(){
        var h = document.documentElement.clientHeight;//获取页面可见高度
        document.getElementById("wrap").style.height=h+"px";
};

$(function(){
    changeBannerHeight();
    //轮播图函数开始
    var size= $(".banner li").size();
    for(var i=1;i<=size;i++){
        var li="<li></li>";
        $(".menu").append(li);
    }
    $(".banner li").eq(0).show();
    $(".menu li").eq(0).addClass("color");

    // 手动轮播
    $(".menu li").mouseover(function(){
        $(this).addClass("color").siblings().removeClass("color");
        var index=$(this).index();
        i=index;
        $('.banner li').eq(i).stop().fadeIn(300).siblings().stop().fadeOut(300);
    })

    // 自动轮播
    var i=0;
    var timer=setInterval(move,3000);
    function move(){
        i++;
        if(i==size){
            i=0;
        }
        $(".banner li").eq(i).fadeIn(300).siblings().fadeOut(300);
        $(".menu li").eq(i).addClass("color").siblings().removeClass("color");
    }

    $(".menu li").hover(function(){
        clearInterval(timer);
    },function(){
        timer=setInterval(move,3000);
    })
    //轮播图函数结束

    //创建和初始化地图函数：
    function initMap(){
        createMap();//创建地图
        setMapEvent();//设置地图事件
        addMapControl();//向地图添加控件
        addMarker();//向地图中添加marker
    }

    //创建地图函数：
    function createMap(){
        var map = new BMap.Map("dituContent");//在百度地图容器中创建一个地图
        var point = new BMap.Point(109.456063,24.302648);//定义一个中心点坐标
        map.centerAndZoom(point,17);//设定地图的中心点和坐标并将地图显示在地图容器中
        window.map = map;//将map变量存储在全局
    }

    //地图事件设置函数：
    function setMapEvent(){
        map.enableDragging();//启用地图拖拽事件，默认启用(可不写)
        map.enableScrollWheelZoom();//启用地图滚轮放大缩小
        map.enableDoubleClickZoom();//启用鼠标双击放大，默认启用(可不写)
        map.enableKeyboard();//启用键盘上下左右键移动地图
    }

    //地图控件添加函数：
    function addMapControl(){
        //向地图中添加缩放控件
        var ctrl_nav = new BMap.NavigationControl({anchor:BMAP_ANCHOR_TOP_LEFT,type:BMAP_NAVIGATION_CONTROL_LARGE});
        map.addControl(ctrl_nav);
        //向地图中添加缩略图控件
        var ctrl_ove = new BMap.OverviewMapControl({anchor:BMAP_ANCHOR_BOTTOM_RIGHT,isOpen:1});
        map.addControl(ctrl_ove);
        //向地图中添加比例尺控件
        var ctrl_sca = new BMap.ScaleControl({anchor:BMAP_ANCHOR_BOTTOM_LEFT});
        map.addControl(ctrl_sca);
    }

    //标注点数组
    var markerArr = [{title:"02小组&nbsp;工作室（TWO）",content:"02小组&nbsp;工作室（TWO）- 程序猿们的天堂。",point:"109.457311|24.301537",isOpen:1,icon:{w:21,h:21,l:0,t:0,x:6,lb:5}}
    ];
    //创建marker
    function addMarker(){
        for(var i=0;i<markerArr.length;i++){
            var json = markerArr[i];
            var p0 = json.point.split("|")[0];
            var p1 = json.point.split("|")[1];
            var point = new BMap.Point(p0,p1);
            var iconImg = createIcon(json.icon);
            var marker = new BMap.Marker(point,{icon:iconImg});
            var iw = createInfoWindow(i);
            var label = new BMap.Label(json.title,{"offset":new BMap.Size(json.icon.lb-json.icon.x+10,-20)});
            marker.setLabel(label);
            map.addOverlay(marker);
            label.setStyle({
                borderColor:"#808080",
                color:"#333",
                cursor:"pointer"
            });

            (function(){
                var index = i;
                var _iw = createInfoWindow(i);
                var _marker = marker;
                _marker.addEventListener("click",function(){
                    this.openInfoWindow(_iw);
                });
                _iw.addEventListener("open",function(){
                    _marker.getLabel().hide();
                })
                _iw.addEventListener("close",function(){
                    _marker.getLabel().show();
                })
                label.addEventListener("click",function(){
                    _marker.openInfoWindow(_iw);
                })
                if(!!json.isOpen){
                    label.hide();
                    _marker.openInfoWindow(_iw);
                }
            })()
        }
    }
    //创建InfoWindow
    function createInfoWindow(i){
        var json = markerArr[i];
        var iw = new BMap.InfoWindow("<b class='iw_poi_title' title='" + json.title + "'>" + json.title + "</b><div class='iw_poi_content'>"+json.content+"</div>");
        return iw;
    }
    //创建一个Icon
    function createIcon(json){

        //http://api.map.baidu.com/img/markers.png
        var icon = new BMap.Icon("/Second/images/marker.png", new BMap.Size(json.w, json.h), { imageOffset: new BMap.Size(-json.l, -json.t), infoWindowOffset: new BMap.Size(json.lb + 5, 1), offset: new BMap.Size(json.x, json.h) })
        return icon;
    }
    initMap();//创建和初始化地图


    //手机端菜单js
    //手机端的菜单显示状态0为默认不打开 1 为打开
    $(".head .m-nav").click(function(){
        if(MenuState==0){
            $(".head .m-nav img").attr("src", "/Second/images/head_nav_menu_c.png");
            $(".head .m-inner-nav").css({"display":"block"});
            MenuState=1;
        }
        else{
            $(".head .m-inner-nav").css({"display":"none"});
            $(".head .m-nav img").attr("src", "/Second/images/head_nav_menu_o.png");
            MenuState=0;
        }
    })
    //菜单栏菜单列表效果
    $(".head .m-inner-nav a").each(function( index ) {
        $( this ).css({'animation-delay': (index/10)+'s'});
        $( this).click(function () {
            $(".head .m-inner-nav").css({"display":"none"});
            $(".head .m-nav img").attr("src", "/Second/images/head_nav_menu_o.png");
            MenuState=0;
        });
    });
});
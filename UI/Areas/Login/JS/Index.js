$(function () {
    $.ajax({
        url: '/Login/Login/GetMenuData',
        type: 'get',
        success: function (jsonMenu) {
            ResizeWindow();
            InitLeftMenu(jsonMenu);
        }
    })
})

///窗体resize事件响应
$(function () {
    $(window).resize(
    function () {
        ResizeWindow();
    }
    )
})

$(function () {
    $("#left-shrink-btn").click(function () {
        LeftMenuShrink();
    });
});


//初始化左侧导航面板
function InitLeftMenu(jsonMenu)
{
    var jsonMenus = jQuery.parseJSON(jsonMenu);
    var $menu = $("#menu");

    $menu.empty();//删除选中元素所有子节点

    var menuList = "";
    menuList += '<ul>';
    var bgx = 0;
    console.info(jsonMenus.menus);

    $.each(jsonMenus.menus, function (i, n) {
        console.info(n.menuId);
        console.info(n.icon.imgPath);
        console.info(n.menuName);

        menuList += '<li>';
        menuList += '<div  class="menu_column_title" onclick="SlideToggleChildMenu(this,' + n.menuId + ')" id="menu-column-title-' + n.menuId + '">';
        menuList += '<div class="menu_column_title_border" >';
        menuList += '<div id="menu-column-title-logo-' + n.menuId + '" class="menu_column_title_logo" style="background-image:url(\'' + n.icon.imgPath + '\');background-position:' + bgx + 'px ' + 0 + 'px;"></div>';
        menuList += '<span class="menu_column_title_text">' + n.menuName + '</span>';
        menuList += '<div id="menu-column-title-state-'+n.menuId+'" class="menu_column_title_state" style="background-image:url(\'Images/index_icon.png\');background-position-x:-240px;background-position-y:0px;"></div>';
        menuList += '</div>';
        menuList += '</div>';

        menuList += '<div class="child_menu" id="child-menu-' + n.menuId + '">';
        menuList += '<ul>';
        $.each(n.childMenus, function (j, o) {
            
            menuList += '<li  onclick="AddIframe('+o.menuId+',\''+o.url+'\',\''+o.menuName+'\')">';
            menuList += '<div class="child_menu_column_border">';
            menuList += '<div class="child_menu_column_logo"  ></div>';
            menuList += '<span class="child_menu_column_text" >' + o.menuName + '</span>';
            menuList += '</div>';
            menuList += '</li>';

        });
        menuList += '</ul>';
        menuList += '</div>';
        menuList += '</li>';
        bgx = bgx - 20;
        
    });

    menuList += "</ul>"
    $menu.append(menuList);
}

//根据浏览器高度改变nav和main的高度
function ResizeWindow()
{
    var windowHeight = $(window).outerHeight(); //获取浏览器高度
    var windowWidth = $(window).outerWidth(); //获取浏览器宽度

    var headerHeight = $("#header").outerHeight();//获取header高度
    var footerHeight = $("#footer").outerHeight();//获取footer高度

    //设置nav和main的高度
    $("#left").css('height', windowHeight - headerHeight - footerHeight + 'px');
    $("#main").css('height', windowHeight - headerHeight - footerHeight + 'px');
    $("#main").css('width', windowWidth - $("#left").outerWidth() + 'px');
    $("#main-panel").css('height', $("#main").outerHeight() - $('#top-nav').outerHeight() + 'px');

    ResizeChildMenu();
    
}

//根据浏览器高度改变子列表的高度
function ResizeChildMenu()
{
    var $childMenu = $(".child_menu");

    var $menuColumn = $(".menu_column_title");

    $childMenu.css('height', $("#left").outerHeight() - $menuColumn.outerHeight() * $menuColumn.length + 'px');
}

//Show子菜单事件
function SlideToggleChildMenu(childMenu,id)
{
    var $childMenus = $(".child_menu");
    var $childMenu = $(childMenu);
    var $states = $(".menu_column_title_state");
    var $state = $("#menu-column-title-state-" + id);

    if ($state.css('background-position') == "-240px 0px")
    {
        $state.css('background-position-x', '-260px'); 
    } else
    {
        $state.css('background-position-x', '-240px');
    }
    $childMenus.each(function (i, n) {
        if (n.style.display == "block" && $childMenu.next().css("display") != "block")
        {
            $($states[i]).css('background-position-x', '-240px');
            $(n).slideToggle(1000);
        }
    })
    ResizeChildMenu();

    $childMenu.next().slideToggle(1000);

    
}

//菜单滑动事件
function LeftMenuShrink()
{
    if ($(".menu").css("width") == '0px')
    {
        $("#left").animate({ width: 185 }, "slow");
        $(".menu").animate({ width: 185 }, "slow");
        $("#left-shrink-btn").animate({ left: 185 }, "slow");
        $("#left-shrink-btn").css('background-position-x','0px');
        $("#main").animate({ left: 185, width: $(window).outerWidth() - 185 }, "slow");

    } else
    {

        $("#left").animate({ width: 0 }, "slow");
        $(".menu").animate({ width: 0 }, "slow");
        $("#left-shrink-btn").animate({ left: 0 }, "slow");
        $("#left-shrink-btn").css('background-position-x', '-20px');
        $("#main").animate({ left: 0, width: $(window).outerWidth() - 0 }, "slow");
        
    }
}

/*AddFrame*/
function AddIframe(id, url, name) {

    var strLi = "";
    var strIframe = "";

    var $ul = $("#nav-body ul");

    var $mainPanel = $("#main-panel");

    var $mainPanelChilds = $mainPanel.children();

    $mainPanelChilds.each(function (i, n) {
            n.style.display = "none";
        });
    
    if (document.getElementById("nav-column-" + id)) {
        document.getElementById("panel-body-" + id).style.display = 'block';
    } else
    {
        
        strLi += '<li id="nav-column-' + id + '">';
        strLi += '<div class="main_nav_li_interlayer" onclick="ChangeFrame(' + id + ')" >'
        strLi += '<div class="mian_nav_li_content">';
        strLi += '<span class="mian_nav_li_content_title">' + name + '</span>';
        strLi += '</div>';
        strLi += '</div>'
        strLi += '<div onclick="SubIframe('+id+')" onmouseover ="NavCloseBtnMouseOver(' + id + ')" onmouseout ="NavCloseBtnMouseOut(' + id + ')" class="mian_nav_li_close_btn" id="nav-li-close-btn-' + id + '"></div>';
        strLi += '</li>';

        $ul.append(strLi);

    
        strIframe += '<div id="panel-body-'+id+'" style="width:inherit;height:inherit">';
        strIframe += '<iframe src="'+url+'" style="border:0px none;width:100%;height:100%;">';
        strIframe += '</iframe>';
        strIframe += '</div>';

        $mainPanel.append(strIframe);

    }

}

function NavCloseBtnMouseOver(navId)
{
    $li = $("#nav-column-" + navId);
    $li.css('background-position-y', '-60px');
}

function NavCloseBtnMouseOut(navId) {
    $li = $("#nav-column-" + navId);
    $li.css('background-position-y', '-20px');
}

function SubIframe(id)
{
    var $navLi = $("#nav-column-" + id);
    $navLi.remove();
    var $iframe = $("#panel-body-" + id);
    var $nextIframe = $iframe.next();

    //若$nextIframe不存在length = 0 ，否则为length = 1
    if ($nextIframe.length < 1) {
        $nextIframe = $iframe.prev();
        if ($nextIframe.length < 1) {
            $nextIframe = $iframe;
        }
    }

    if ($iframe.css("display") == "block") {
        ChangeFrame($nextIframe.attr('id').substring(11, 13));//截取id号
    }
    $iframe.remove();
}

/*iframe   title onclick事件*/
function ChangeFrame(id)
{
    var $mainPanel = $("#main-panel");
    var $mainPanelChilds = $mainPanel.children();
    $mainPanelChilds.each(function (i, n) {
        n.style.display = "none";
    });

    document.getElementById("panel-body-" + id).style.display = 'block';
}
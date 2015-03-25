﻿

$.extend({
    Entry: function (stunums,role) {
        $.ajax({
            url: '/PersonalManger/CheckMember/EntryPositionEx',
            type: 'post',
            data: { "stunums": stunums, "role": role },
            success: function (json) {
                //转换json，很奇怪............
                var jsonObj = JSON.parse(json);
                if (jsonObj.Statu == "ok") {
                    $("#body").empty();
                    PreCreateTable(jsonObj);
                }
                else { }
            }
        })
    }
})


/*录入职位*/
function entry() {
    var stunums = "";
    var role = $('#input-text option:selected').val()
    $("input[type='checkbox']").each(function () {
        if ($(this).attr("checked")) {
            stunums = stunums + $(this).attr("data-stunum") + ";";
        }
    });
    /*r进行判断，每个职位录入的人数是固定的*/
    if (role == 10001) {
        if ($("input[type='checkbox']:checked").length > 1) {
            alert("只能录入一位");
        } else {
            $.Entry(stunums, role);
        }
    } else {
        alert(stunums);
        $.Entry(stunums, role);
    }

}
//编辑方法
function Edit(stuNum) {}
    //选中方法
    function choose() {

    }
    //删除方法
    function Delete(stuNum) {
        if (confirm("确定要删除吗？")) {
            $.ajax({
                url: '/PersonnalManger/CheckMember/DeleteBy',
                type: 'post',
                data: { "stuNum": stuNum },
                success: function (data) {
                    var jsonObj = JSON.parse(data);
                    if (jsonObj.Statu == "ok") {
                        alert(jsonObj.Msg);
                        $("#body").empty();
                        $.Init();
                    } else {
                        alert(jsonObj.Msg);
                    }
                }
            })
        }
        else { }
    }



    /*分页定义的变量*/
    var json = "";//json数据
    var pageSize = 10;//页的容量
    var pageIndex = 1;//记录翻到了第几页
    var rowSum;//记录总行数
    var pageSum;//记录总页数
    var rowCurrentSum;//当前请求得到的总记录数
    var rowForm;//开始的记录数
    var rowTO;//结束的记录数

    $(function () {
        //关闭Jquery的浏览器缓存
        $.ajaxSetup({ cache: false });
    })


    //生成表格前预处理工作
    function PreCreateTable(jsonObj) {
        var data = jsonObj.Data.data;//获得数据data对象
        rowSum = jsonObj.Data.TotalRecord;//总记录数
        pageSum = Math.floor((jsonObj.Data.TotalRecord / (pageSize + 1))) + 1;//总页数    
        rowCurrentSum = data.length;//当前请求得到的总记录数
        rowForm = (pageIndex - 1) * pageSize + 1;//开始的记录数
        rowTO = rowForm + rowCurrentSum - 1;//结束的记录数
        $(".lable-text-right").html("显示" + rowForm + "到" + rowTO + "，总计" + rowSum + "条记录");//从开始纪录到结束纪录显示
        $(".lable-page").html(pageIndex);//当前页显示
        $("#lable-page-sum").html("共" + pageSum + "页");//总页数显示
        $.creatTable(data);
    }

    $.extend({
        Init: function (role) {
            $.ajax({
                url: '/PersonalManger/CheckMember/GetEntryData',
                type: 'post',
                data: { "pageindex": pageIndex,"role": role },
                success: function (json) {
                    //转换json，很奇怪............
                    var jsonObj = JSON.parse(json);
                    if (jsonObj.Statu == "ok") {
                        $("#body").empty();
                        PreCreateTable(jsonObj);
                    }
                    else { }
                }
            })
        }
    })

    //第一次请求初始化
    $(function () {
        //清空表格
        $.Init("10001");
    })

    //各种点击事件
    $(function () {
        //点击第一页按钮触发事件
        $(".a-first").click(function () {
            $("#body").empty();
            pageIndex = 1;
            $.Init();
        });

        //点击最后一页按钮触发事件
        $(".a-last").click(function () {
            $("#body").empty();
            console.info(pageSum);
            pageIndex = pageSum;
            $.Init();
        });

        //点击向前翻页按钮触发事件
        $(".a-pre").click(function () {
            if (pageIndex == 1) { }//判断是否到了第一页
            else {
                $("#body").empty();
                pageIndex = pageIndex - 1;
                $.Init();
            }
        });
        //点击向后翻页按钮触发事件
        $(".a-next").click(function () {
            if (pageIndex == pageSum) { }//判断是否到了最后一页
            else {
                $("#body").empty();
                pageIndex = pageIndex + 1;
                $.Init();
            }
        });
        //点击刷新按钮触发事件
        $(".a-refresh").click(function () {
            $("#body").empty();
            $.Init();
        });
    })

    //生成表格
    $.extend({
        creatTable: function (data) {
            var $body = $("#body");
            for (var i = 0; i < data.length; i++) {
                var $tr = $("<tr class='tbody-tr'></tr>");
                /*生成第一列*/
                var $td = $("<td id='body-td-first'></td>");
                var $div = $("<input type='checkbox'  data-stunum=" + data[i].StuNum + ">");
                $td.append($div);
                $tr.append($td);
                /*生成其他七列*/
                var $content = $("<td><a href='#' class='a-stum'>" + data[i].StuNum + "</a></td>" + "<td>" + data[i].StuName + "</td>" +
                    "<td>" + data[i].TelephoneNumber + "</td>" + "<td>" + data[i].Major + "</td>" + "<td>" + data[i].Department + "</td>" +
                    "<td style='width:300px'>" + data[i].roles + "</td>");
                $tr.append($content);
                $body.append($tr);
            }
        }
    });
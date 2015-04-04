window.onload = function () {
    document.getElementById('bgOne').onmouseover = function () {
        document.getElementById('bgOne').style.backgroundColor = "#D4D4D4";
        document.getElementById('bgTwo').style.backgroundColor = "#F4F4F4";
        document.getElementById('bgThree').style.backgroundColor = "#F4F4F4";
        document.getElementById('BackGroundDiv').style.backgroundImage = url('../Images/background.png');
    }

    document.getElementById('bgTwo').onmouseover = function () {
        document.getElementById('bgOne').style.backgroundColor = "#F4F4F4";
        document.getElementById('bgTwo').style.backgroundColor = "#D4D4D4";
        document.getElementById('bgThree').style.backgroundColor = "#F4F4F4";
        document.getElementById('BackGroundDiv').style.backgroundImage = url('http://localhost:30923/Images/登陆界面图标.png');
    }

    document.getElementById('bgThree').onmouseover = function () {
        document.getElementById('bgOne').style.backgroundColor = "#F4F4F4";
        document.getElementById('bgTwo').style.backgroundColor = "#F4F4F4";
        document.getElementById('bgThree').style.backgroundColor = "#D4D4D4";
        document.getElementById('BackGroundDiv').style.backgroundImage = url('../Images/background.png');
    }
}


function login() {
    var userName = document.getElementById("LoginName").value;
    var pwd = document.getElementById("Pwd").value;
    var vCode = document.getElementById("VCode").value;
    var matchResult = true;
    if (LoginName == "" || pwd == "" || vCode == "") {
        alert("请确认是否有空缺项！");
        matchResult = false;
    } else if (userName.length < 6 || userName.length > 20) {
        alert("用户名长度应在6到20个字符之间！");
        matchResult = false;
    } else if (userName == pwd) {
        alert("密码不能和用户名相同！");
        matchResult = false;
    } else if (pwd.length < 6 || pwd.length > 20) {
        alert("密码或重复密码长度应在6到20个字符之间！");
        matchResult = false;
    }
    if (matchResult == true) {
        document.getElementById("submitForm").submit();
        alert("sss");
    }
}

$(function () {
    $("#ValiCode").bind("click", function () {
        this.src = "GetVCode?time=" + (new Date()).getTime();
    });
});
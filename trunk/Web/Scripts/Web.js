/*
*   登录
*/
function doLogin(form) {
    var username = form.txtUserName.value;
    if (username == "") {
        document.getElementById("message").innerText = "* 用户名不能为空！";
        return false;
    }
    var password = form.txtPassword.value;
    if (password == "") {
        document.getElementById("message").innerText = "* 密码不能为空！";
        return false;
    }
    form.txtPassword.value = $.base64.encode(form.txtPassword.value);
}
/*
*   专辑管理->checkbox事件
*/
function doClickCheck(sender,ctrid,value) {
    for (var i = 0; i < sender.form.length; i++) {
        var element = sender.form[i];
        if (element.type == "checkbox" && element != sender) {
            element.checked = false;
        }
    }
    document.getElementById(ctrid).value = value;
    document.getElementById("cmd").value = "Check";
}
/*
*   专辑管理-> 按钮事件
*/
function doAction(type) {
    var aid = document.getElementById("aid").value;
    if (aid == "") {
        alert("请先选择专辑！");
        return false;
    }
    document.getElementById("cmd").value = type;
    return true;
}
/*
*   专辑管理-> 选择图片
*/
function selectImage() {
    var list = document.getElementsByName("chkImage");
    var selected = "";
    for (var i = 0; i < list.length; i++) {
        if (list[i].checked) {
            selected += list[i].title + ",";
        }
    }
    document.getElementById("selectedImage").value = selected;
}
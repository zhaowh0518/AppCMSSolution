function ValidateInput() {
    var userName = document.getElementById("UserName");
    if (userName == "") {
        alert("用户名不能为空！");
        return false;
    }
}
function ClickImage(img) {
    alert("got image's url");
    window.clipboardData.setData("Text", img.title);
}

var captcha = new function () {
    
    this.add = function (ele, referenceEle) {
        ele.src = "/Content/Stageplan/Images/Captcha/" + getSrc();

        //referenceEle is when another element needs to know the URL
        if (referenceEle != null)
        referenceEle.value = ele.src;
    };

    function getSrc() {
        var num = Math.floor(Math.random() * 6) + 1;
        return "Captcha-0" + num + ".jpg";
    }
}
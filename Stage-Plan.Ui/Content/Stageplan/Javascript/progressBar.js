
var progressBar = new function () {

    var ele;

    this.setUp = function (progressBarId) {
        ele = document.getElementById(progressBarId);
    },
    this.progress = function (current, max) {
        var p = (current / max) * 100;
        ele.style.width = p + "%";
    };
    }
"use strict"
var closePopUpFunction = new function () {
    this.register = function (eleId, closeCallBack, showClose) {

        if (!showClose)
            return;

        var closeDiv = document.createElement("div");
        closeDiv.className = "closeButton";
        closeDiv.innerHTML = "x";

        closeDiv.addEventListener("click", closeCallBack);
        var element = document.getElementById(eleId);
        if(element != null) {
            element.insertBefore(closeDiv, element.childNodes[0]);
        }
    }
};
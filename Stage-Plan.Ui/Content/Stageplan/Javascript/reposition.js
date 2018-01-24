var reposition = new function () {
    var _zIndex = 0;
    this.increase = function (ele) {
        _zIndex++;
        ele.style.zIndex = _zIndex;
    };
}
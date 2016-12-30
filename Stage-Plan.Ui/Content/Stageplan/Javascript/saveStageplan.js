

var saveStageplan = new function () {
    
    var _stageplanHtmlId;
    var _successAction;
    var _failAction;
    var _progressBarAction;

    this.setUp = function (saveStageplanButtonId, bandNameId, stageplanHtmlId, successAction, failAction, progressBarAction, captchaSrc, captchaTextId, captchaFailAction) {

        _stageplanHtmlId = stageplanHtmlId;
        _successAction = successAction;
        _failAction = failAction;
        _progressBarAction = progressBarAction;
        var saveButton = document.getElementById(saveStageplanButtonId);
        saveButton.addEventListener("click", function () {

            var bandName = document.getElementById(bandNameId).value;
            if (isVerified(captchaSrc, captchaTextId, captchaFailAction, bandName)) {
                save(bandName, stageplanHtmlId, successAction, failAction, progressBarAction);
            }            
        });
    };

    function save(bandName) {
        ajaxCall.postNow("../../Stageplan/SaveNewStagePlan", JSON.stringify({ 'name': bandName }), getStageplanIdSuccess, fail, fail);
    }

    function getStageplanIdSuccess(returnId) {

        var stagePlanId = returnId;

        var stageplanHtml = document.getElementById(stageplanHtmlId);

        //loop through each instrument
        for (var i = 0; i < stageplanHtml.children.length; i++) {
            //left, top, data-text,data-description, img
            var child = stageplanHtml.children[i];
            var txt = child.getAttribute("data-text");
            var detail = child.getAttribute("data-detail");
            var left = child.offsetLeft;
            var top = child.offsetTop;
            var src = child.getAttribute("data-src");

            progressBarAction(i + 1, stageplanHtml.children.length);

        }

        //    ajaxCall.postNow("../../SaveNewStagePlan", JSON.stringify({ 'name': 'myName' }, getStageplanIdSuccess, _failAction, _failAction));
    }

    function saveInstrument() {

    }


    function isVerified(captchaSrc, captchaTextId, captchaFailAction, bandName) {
        var captchaText = document.getElementById(captchaTextId).value;
        var splity = captchaSrc.split("/");
        var src = splity[splity.length -1];

        if (helpers.isNullOrEmpty(bandName))
            return false;

        return true;
    }

    function fail(d) {
        _failAction("Failed in save...");
    }
};
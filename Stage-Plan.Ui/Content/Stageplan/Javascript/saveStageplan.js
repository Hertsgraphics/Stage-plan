

var saveStageplan = new function () {

    this.setUp = function (saveStageplanButtonId, bandNameId, stageplanHtmlId, successAction, failAction, captchaSrc, captchaTextId, captchaFailAction) {

        var saveButton = document.getElementById(saveStageplanButtonId);
        saveButton.addEventListener("click", function () {

            var bandName = document.getElementById(bandNameId).value;
            if (isVerified(captchaSrc, captchaTextId, captchaFailAction, bandName)) {
                save(bandName, stageplanHtmlId, successAction, failAction);
            }
        });
    };

    function save(bandName, stageplanHtmlId, successAction, failAction) {
      
       
        var stageplanHtml = document.getElementById(stageplanHtmlId);

        var AllInstruments = [];
        //loop through each instrument
        for (var i = 0; i < stageplanHtml.children.length; i++) {
            var child = stageplanHtml.children[i];
            var txt = child.getAttribute("data-text");
            var detail = child.getAttribute("data-detail");
            var left = child.offsetLeft;
            var top = child.offsetTop;
            var src = child.getAttribute("data-src");

            AllInstruments.push({
                'Text': txt,
                'Detail': detail,
                'Left': left,
                'Top': top,
                'Src': src
            });
        }

        ajaxCall.postNow("../../Stageplan/SaveNewStagePlan", JSON.stringify({
            'bandName' : bandName,
            'AllInstruments': AllInstruments
        })
        , successAction, failAction, failAction);



    }

    function saveInstrumentSuccess(returnObj) {
        alert(returnObj.url);
    }


    function isVerified(captchaSrc, captchaTextId, captchaFailAction, bandName) {
        var captchaText = document.getElementById(captchaTextId).value;
        var splity = captchaSrc.split("/");
        var src = splity[splity.length - 1];

        if (helpers.isNullOrEmpty(bandName))
            return false;

        return true;
    }

    function fail(d) {
        _failAction("Failed in save...");
    }
};
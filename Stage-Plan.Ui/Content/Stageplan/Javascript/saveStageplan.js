

var saveStageplan = new function () {
    
    this.setUp = function (saveStageplanButtonId, bandNameId, stageplanHtmlId, successAction, failAction, progressBarAction, captchaSrc, captchaTextId, captchaFailAction) {
        var saveButton = document.getElementById(saveStageplanButtonId);
        saveButton.addEventListener("click", function () {
            if (isVerified(captchaSrc, captchaTextId, captchaFailAction)) {
                save(bandNameId, stageplanHtmlId, successAction, failAction, progressBarAction);
            }
            
        });
    };

    function save(bandNameId, stageplanHtmlId, successAction, failAction, progressBarAction) {

        var stagePlanId = getStageplanId(bandNameId);
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
            var startTime = new Date().getTime(); // get the current time
            while (new Date().getTime() < startTime + 1000); // hog cpu//todo remove.. used to simulate database save

            //update [progress

            //save
        }
        //alert("todo save");
        //ajax.postNow("../../SaveNewStagePlan", JSON.stringify({ 'name': 'myName' }, success, fail, fail));

    }

    function saveInstrument() {

    }

    function getStageplanId(bandNameId) {
        return 1;//todo
    }

    function isVerified(captchaSrc, captchaTextId, captchaFailAction) {
        var captchaText = document.getElementById(captchaTextId).value;
        var splity = captchaSrc.split("/");
        var src = splity[splity.length -1];

        return true;
    }

};
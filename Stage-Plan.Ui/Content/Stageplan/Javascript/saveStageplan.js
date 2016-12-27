

var saveStageplan = new function () {
    
    this.save = function (bandName, stageplanHtml, fail) {

        
        ajax.postNow("../../SaveNewStagePlan", JSON.stringify({ 'name': 'myName' }, success, fail, fail));

    };

};
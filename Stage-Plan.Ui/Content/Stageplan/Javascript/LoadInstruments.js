var loadInstruments = new function () {
    var _screenWidth;
    var _screenHeight;
    var _currentWidth;
    var _currentHeight;
    this.setup = function (screenWidth, screenHeight, currentWidth, currentHeight) {
        _screenHeight = screenHeight;
        _screenWidth = screenWidth;
        _currentHeight = currentHeight;
        _currentWidth = currentWidth;
    },

    //'left', 'top':', 'detail':', 'text', 'src':, 'instrumentId' 
    this.loadPlan = function (model, resetCallBack, editCallback) {
        
        //x, y, instrumentName, addInstrumentExtraDetail, selectedImage, instrumentsOnStage
        var instrumentOnStageEle = document.getElementById(model.instrumentId);
        var leftPercentage = (model.left / _screenWidth) * 100;
        var heightPercentage = (model.top / _screenHeight) * 100;

        var newLeft = (leftPercentage / 100) * _currentWidth;
        var newHeight = (heightPercentage / 100) * _currentHeight;

        var newInstrumentWidth = null;
        var newInstrumentHeight = null;

        if (model.width != null && model.height != null) {
            var instrumentWidthPercentage = (model.width / _screenWidth) * 100;
            var instrumentHeightPercentage = (model.height / _screenHeight) * 100;


            newInstrumentWidth = (instrumentWidthPercentage / 100) * _currentWidth;
            newInstrumentHeight = (instrumentHeightPercentage / 100) * _currentHeight;
        }
        var isFixedPosition = false;

        model.addInstrumentCallback.add(model.selectedInstrument, newLeft, newHeight, model.text, model.detail, model.src, instrumentOnStageEle, editCallback, model.bandMemberName, model.isFixedPosition != "True", resetCallBack, isFixedPosition, false, false, newInstrumentWidth, newInstrumentHeight, null, model.zIndex, model.rotateAngle);
    };
};
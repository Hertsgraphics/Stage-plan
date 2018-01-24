"use strict"

var saveStageplan = new function () {

    this.setUp = function (saveStageplanButtonId, stageplanHtmlId, successAction, failAction, captcha, captchaText, captchaFailAction, bandNameEle, bandWebUrlEle, bandSocialMediaUrlEle, bandGenreEle, SaveBandDetailToHomePageEle, bandCountryEle, saveStagePlanBandEmailEle, willJoinMailingListEle) {

        var saveButton = document.getElementById(saveStageplanButtonId);
        saveButton.addEventListener("click", function () {
            var bandName = bandNameEle.value;
            var error = validation.isSaveStageplanVerified(bandName);
            if (helpers.isNullOrEmpty(error)) {
                save(bandName, stageplanHtmlId, successAction, failAction, captcha.src, captchaText.value, bandWebUrlEle.value, bandSocialMediaUrlEle.value, bandGenreEle.value, SaveBandDetailToHomePageEle.checked, bandCountryEle.value, saveStagePlanBandEmailEle.value, willJoinMailingListEle.checked);
            } else {
                alert(error);
            }
        })
    };

    function save(bandName, stageplanHtmlId, successAction, failAction, captchaSrc, captchaText, bandWebUrl, bandSocialUrl, bandGenre, willSaveBandDetailToHomePage, bandCountry, bandEmail, willJoinMailingList) {

        if (willJoinMailingList && helpers.isNullOrEmpty(bandEmail)) {
            alert("Please enter an email to join the mailing list.");
            return;
        }

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
            var bandMemberName = child.getAttribute("data-bandmember");
            var isFixedPosition = child.getAttribute("data-is-fixed-position");
            var selectedInstrument = child.getAttribute("data-img-id");

            //var rotateAngle = 0;
            //var imgForRotateParent = child.children[0];
            //var imgForRotate = null;

            //if (imgForRotateParent != null)
            //    imgForRotate = imgForRotateParent.children[0];

            //if (imgForRotate != null)
            //    rotateAngle = imgForRotate.getAttribute("data-rotate");

            var rotateAngle = child.getAttribute("data-rotate") || 0;


            var width = "100px";
            var height = "100px";
            var zIndex = child.style.zIndex;
            if (child.hasChildNodes()) {
                var inner = child.children[0];
                if (inner != null) {
                    width = inner.style.width;
                    height = inner.style.height;
                }
            }

            width = width.split(".")[0];
            height = height.split(".")[0];


            AllInstruments.push({
                'Text': txt,
                'Detail': detail,
                'Left': left,
                'Top': top,
                'Src': src,
                'BandMemberName': bandMemberName,
                'Isfixedposition': isFixedPosition,
                'SelectedInstrument': selectedInstrument,
                'Width': width.replace("px", ""),
                'Height': height.replace("px", ""),
                'Zindex': zIndex,
                'RotateAngle': rotateAngle

            });
        }

        ajaxCall.postNow("../../Stageplan/SaveNewStagePlan", JSON.stringify({
            'BandName': bandName,
            'Width': document.documentElement.clientWidth,
            'Height': document.documentElement.clientHeight,
            'AllInstruments': AllInstruments,
            'CaptchaPath': captchaSrc,
            'CaptchaCode': captchaText,
            'BandWebAddress': bandWebUrl,
            'BandSocialMediaAddress': bandSocialUrl,
            'BandGenre': bandGenre,
            'Country': bandCountry,
            'WillShowInRecentBands': willSaveBandDetailToHomePage,
            'TemporaryEmailAddress': bandEmail,
            'WillSignUp': willJoinMailingList
        })
        , successAction, failAction, failAction);

    }

    function saveInstrumentSuccess(returnObj) {
        alert(returnObj.url);
    }

    function fail(d) {
        _failAction("Failed in save...");
    }
};
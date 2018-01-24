"use strict"
var enterVenueDetails = new function () {
    var _venueDetails;

    this.setUp = function (venueNameId, captchaImgId, captchaCodeEnteredId, saveButtonId, emailId, venueTemplateUrlId, passwordId, helpersCallback, validationCallback, ajaxResponseCallback, ajaxErrorCallback) {
        var enterVenueDetails_Submit = document.getElementById(saveButtonId);

        enterVenueDetails_Submit.addEventListener("click", function () {
            onSubmit(
                document.getElementById(venueNameId).value,
                document.getElementById(captchaImgId).src,
                document.getElementById(captchaCodeEnteredId).value,
                null,
                document.getElementById(emailId).value,
                null,
                null,
                null,
                document.getElementById(venueTemplateUrlId).value,
                document.getElementById(passwordId).value,
                null,
                null,
                null,
                helpersCallback,
                validationCallback,
                ajaxResponseCallback,
                ajaxErrorCallback)
        });
    };


    this.update = function (venueNameId, captchaImgId, captchaCodeEnteredId, saveButtonId, contactId, emailId, phoneId, websiteId, socialMediaId, venueTemplateUrlId, passwordId, addressId, stageWidthId, stageDepthId, helpersCallback, validationCallback, ajaxResponseCallback, ajaxErrorCallback) {
        var enterVenueDetails_Submit = document.getElementById(saveButtonId);

        enterVenueDetails_Submit.addEventListener("click", function () {
            onSubmit(
                document.getElementById(venueNameId).value,
                document.getElementById(captchaImgId).src,
                document.getElementById(captchaCodeEnteredId).value,
                document.getElementById(contactId).value,
                document.getElementById(emailId).value,
                document.getElementById(phoneId).value,
                document.getElementById(websiteId).value,
                document.getElementById(socialMediaId).value,
                document.getElementById(venueTemplateUrlId).value,
                document.getElementById(passwordId).value,
                document.getElementById(addressId).value,
                document.getElementById(stageWidthId).value,
                document.getElementById(stageDepthId).value,
                helpersCallback,
                validationCallback,
                ajaxResponseCallback,
                ajaxErrorCallback)
        });
    };

    function onSubmit(venueName, captchaImg, captchaCodeEntered, contact, email, phone, website, socialMedia, venueTemplateUrl, password, address, stageWidth, stageDepth, helpersCallback, validationCallback, ajaxResponseCallback, ajaxErrorCallback) {

        var Venue = {
            'ContactName': contact,
            'Phone': phone,
            'Website': website,
            'SocialMedia': socialMedia,
            'VenueTemplateUrl': venueTemplateUrl,
            'VenueName': venueName,
            'Address': address,
            'StageWidth': stageWidth,
            'StageDepth': stageDepth             
        }

        var Account = {
            'EmailAddress': email,
            'TempPassword': password
        }

        var Captcha = { 'CaptchaCode': captchaCodeEntered , 'CaptchaPath': captchaImg };

        var NewVenueAndAccountVm = { "Venue": Venue, "Account": Account, 'Captcha' : Captcha };
        
        var error = validationCallback.isValidVenueDetail(email, venueTemplateUrl, password);

        if (!helpersCallback.isNullOrEmpty(error)) {
            ajaxErrorCallback(error);
            return;
        }
        //var error = validationCallback.hasValidBandMembers(_venueDetails);
        //if (helpersCallback.isNullOrEmpty(error)) {
        //    registerBandDetailsCallback.add(bandName.value, bandMembers.value);
        //    closeCurrentPopUpCallback();
        //} else {
        //    alert(error);
        //};

        save(ajaxResponseCallback, NewVenueAndAccountVm);
    }

    function save(ajaxResponseCallback, data) {
        var json = JSON.stringify(data);
        ajaxCall.postNow("../../CreateTemplate/Create", JSON.stringify(data), ajaxResponseCallback, ajaxResponseCallback, ajaxResponseCallback);
    };
}
"use strict"
var validation = new function () {

    this.validateSaveInstument = function (x, y, instrumentImage, instrumentName) {

        var errorMessage = "";

        if (x == null || y == null) {
            errorMessage += "There was a fault with the program, you cannot save. Please contact us direct. ";
        }

        if (instrumentImage == null) {
            errorMessage += "Please select an instrument image. ";
        }

        if (instrumentName == null) {
            errorMessage += "Please enter an instrument name. ";
        }

        return errorMessage;
    },

    this.isValidVenueDetail = function (email, venueTemplateUrl, password) {
        var errorMessage = "";
        errorMessage += validation.isValidEmail(email);

        if (helpers.isNullOrEmpty(venueTemplateUrl)) {
            errorMessage += "Please enter a valid URL. ";
        }

        if (helpers.contains(venueTemplateUrl, " ")) {
            errorMessage += "Please don't use spaces in the venue URL. ";
        }

        if (helpers.isNullOrEmpty(password)) {
            errorMessage += "Please enter a valid password. ";
        }

        if (!validation.isValidPassword(password)) {
            errorMessage += "The password is invalid and requires at least 8 characters, numbers and special characters. ";
        }

        return errorMessage;
    },

    this.isValidPassword = function (pwrd) {
        var pattern = /^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*\d)(?=.*[!#$%&? "]).*$/;
        new RegExp(pattern).test(pwrd);
        var result = new RegExp(pattern).test(pwrd);;//pwrd.match(pattern);
        return result;
    }

    this.isValidEmail = function (email) {
        if (helpers.isNullOrEmpty(email)) {
            return "Please enter a valid email address. ";
        }

        var pattern = /^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$/;
        if (!new RegExp(pattern).test(email)) {
            return "Please enter a valid email address. ";
        }
        return "";
    },

    this.validateAddInstument = function (instrumentImage, instrumentName, allInstrumentsOnStage) {

        var errorMessage = "";

        for (var i = 0; i < allInstrumentsOnStage.length; i++) {
            if (allInstrumentsOnStage[i].name == instrumentName) {
                errorMessage += "Please enter a unique name because " + instrumentName + " is already in use. ";
                break;
            }
        }

        if (instrumentImage == null) {
            errorMessage += "Please select an instrument image. ";
        }

        if (instrumentName == null) {
            errorMessage += "Please enter an instrument name. ";
        }

        return errorMessage;
    },

    this.isSaveStageplanVerified = function (bandName) {
        var errorMessage = "";

        if (helpers.isNullOrEmpty(bandName)) {
            errorMessage += "Please enter a band name. ";
        }
        return errorMessage;
    },

    this.isInstrumentsBeenAdded = function (isFirstInstrument) {
        var errorMessage = "";

        if (isFirstInstrument) {
            errorMessage += "Please add an instrument. ";
        }
        return errorMessage;
    };

    // expects all elements to have data-required attribute
    //this.hasValidBandMembers = function (elements) {
    //    var errorMessage = null;
    //    for (var i = 0; i < elements.length; i++) {
    //        if (elements.getAttribute("data-required") == "true") {
    //            var mem = helpers.replaceAll(bandMembers, "\n", "");
    //            var mem = helpers.replaceAll(bandMembers, "\r", "");
    //            var mem = helpers.replaceAll(bandMembers, "\r\n", "");
    //            var mem = helpers.replaceAll(bandMembers, "\n\r", "");
    //            var mem = helpers.replaceAll(bandMembers, " ", "");
    //            if (helpers.isNullOrEmpty(mem)) {
    //                errorMessage += "Please enter your band member(s).";
    //            }
    //        }

    //        return errorMessage;
    //    }
    //},


    this.areBandMemberDetailsValid = function (bandName, bandMembers) {
        var errorMessage = "";
        if (helpers.isNullOrEmpty(bandName)) {
            errorMessage += "Please enter band name. ";
        }

        var mem = helpers.replaceAll(bandMembers, "\n", "");
        mem = helpers.replaceAll(mem, "\r", "");
        mem = helpers.replaceAll(mem, "\r\n", "");
        mem = helpers.replaceAll(mem, "\n\r", "");
        mem = helpers.replaceAll(mem, " ", "");
        if (helpers.isNullOrEmpty(mem)) {
            errorMessage += "Please enter your band member(s).";
        }

        var allMembers = [];
        var mem2 = helpers.replaceAll(bandMembers, " ", "");
        var memSplit = mem2.split('\n');

        var unique = helpers.distinct(memSplit);

        if (memSplit.length != unique.length) {
            errorMessage += "Each band member must have a unique name.";
        }

        return errorMessage;
    };

};
"use strict"

var stagePlanButton = new function () {
    this.getCss = function () {
        switch (helpers.getRandomNumber(0, 5)) { // return the name of the css class
            case 0:
                return "drummer";
            case 1:
                return "bassist";
            case 2:
                return "guitarist";
            case 3:
                return "vocalist";
            case 4:
                return "keyboardist";
            default:

        }

    };
}
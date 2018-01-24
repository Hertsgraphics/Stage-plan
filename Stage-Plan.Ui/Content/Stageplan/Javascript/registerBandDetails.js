

"use strict"
var registerBandDetails = new function () {

    var _bandName;
   // var _bandMembers = [];


    this.add = function (enterBandName, enterBandMembers, setBandMembersCallback) {
        _bandName = enterBandName;
        var split = enterBandMembers.split("\n");
        var bandMembers = [];
        for (var i = 0; i < split.length; i++) {
            if (!helpers.isNullOrEmpty(split[i])) {
                bandMembers.push(split[i].trim());
            }
        }

        setBandMembersCallback(bandMembers);
    }
}
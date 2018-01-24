"use strict"
var enterBandDetails = new function () {
    var _bandMembersUnique = [];
    this.setUp = function (saveButtonId, bandName, bandMembers, helpersCallback, validationCallback, registerBandDetailsCallback, closeCurrentPopUpCallback, setBandDetailsCallback) {
        var enterBandDetails_Submit = document.getElementById(saveButtonId);
   
        enterBandDetails_Submit.addEventListener("click", function () {
            onSubmit(bandName, bandMembers, helpersCallback, validationCallback, registerBandDetailsCallback, closeCurrentPopUpCallback, setBandDetailsCallback)
        });
    };

    function onSubmit(bandNameId, bandMembersId, helpersCallback, validationCallback, registerBandDetailsCallback, closeCurrentPopUpCallback, setBandDetailsCallback) {
        var bandName = document.getElementById(bandNameId);
        var bandMembers = document.getElementById(bandMembersId);
        var error = validationCallback.areBandMemberDetailsValid(bandName.value, bandMembers.value);
        if (helpersCallback.isNullOrEmpty(error)) {
            registerBandDetailsCallback.add(bandName.value, bandMembers.value, setBandMembers);
            closeCurrentPopUpCallback();
        } else {
            alert(error);
        };
        setBandDetailsCallback(bandName.value, _bandMembersUnique);
    }
    function setBandMembers(members){
        _bandMembersUnique = members;
    }
}
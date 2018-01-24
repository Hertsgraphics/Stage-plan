"use strict"

var _websiteUrl = "https://stage-plan.com";



/* create the nav links */

var _newPlan = document.getElementById("showNewPlanLink");
var _showAddInstrument = document.getElementById("showAddInstrumentLink");
var _showAddVenueInstrument = document.getElementById("showAddVenueInstrumentLink");
var _showSaveOptions = document.getElementById("showSaveOptionsLink");
var _loadExistingStageplanPopUp = document.getElementById("showLoadExistingStageplanLink");
var _signInPopUp = document.getElementById("showSignInLink");
var _showSignOut = document.getElementById("showSignOutLink");
var _showSignUp = document.getElementById("showSignUpLink");
var _showSuggestionsLink = document.getElementById("showSuggestionsLink");
var _showEnterBandDetails = document.getElementById("enterBandDetails");

/* end nav links */

//global
var _allInstruments = 0;
var _addedInstrumentId = 0;
var _allInstrumentDetails = [];
var _currentPopUp;
var _popUp = document.getElementById("addInstrumentPopUp");
var _instrumentName = document.getElementById("instrumentName");
var _isFixedPositionEle = document.getElementById("isFixedPosition");
var _noInstruments = document.getElementById("noInstruments");
var _bandMemberNames = [];
var _addInstrumentPopUp_bandMemberNames = document.getElementById("addInstrumentPopUp_bandMemberNames");
var _addInstrumentPopUp_bandName;
var _popUpMask = document.getElementById("popUpMask");
var _instruementsPanel = document.getElementById("instruementsPanel")
var _stagePlanType = null;
//end global



//Buttons
var _saveEdit = document.getElementById("saveEditButton");
var _addNewInstrumentButton = document.getElementById("addNewInstrumentButton");
var _addInstrumentExtraDetail = document.getElementById("addInstrumentExtraDetail");
var _deleteButton = document.getElementById("deleteButton");

var _popUpUl = _instruementsPanel.children[1];
var _instrumentsOnStage = document.getElementById("instrumentsOnStage");
var _stage = document.getElementById("stage");
var _selectedInstrument = null;
var _selectedImage = null;
var _isFirstInstrument = true;
//var _isMove = false;
var _stageLeft = _stage.offsetLeft;
var _stageTop = _stage.offsetTop;
var _stageRight = _stageLeft + _stage.clientWidth;
var _stageBottom = _stageTop + _stage.clientHeight;
var _selectedWidth = 0;
var _selectedHeight = 0;
var _cssSelected = "selected";

/*register close pop up*/

closePopUpFunction.register("addInstrumentPopUp", closeCurrentPopUp, true);
closePopUpFunction.register("signUpPopUp", closeCurrentPopUp, true);
closePopUpFunction.register("showSelectedInstrumentInfo", closeCurrentPopUp, true);
closePopUpFunction.register("signInPopUp", closeCurrentPopUp, true);
closePopUpFunction.register("loadExistingStageplanPopUp", closeCurrentPopUp, true);
closePopUpFunction.register("readOnlyStageplanUrl", closeCurrentPopUp, true);
closePopUpFunction.register("saveStagePlanPopUp", closeCurrentPopUp, true);
closePopUpFunction.register("showYouTubePopUp", closeCurrentPopUp, true);
closePopUpFunction.register("showSuggestionsPopUp", closeCurrentPopUp, true);
closePopUpFunction.register("enterBandDetails", closeCurrentPopUp, false);


/*end register close pop up*/

/*cancel  buttons*/
var _cancelSaveStagePlanPopUp = document.getElementById("cancelSaveStagePlanPopUp");
var _cancelLoadExistingProject = document.getElementById("cancelLoadExistingProject");
var _cancelSignIn = document.getElementById("cancelSignIn");
var _cancelSignUp = document.getElementById("cancelSignUp");
var _cancelShowSelectedInstrumentInfo = document.getElementById("cancelShowSelectedInstrumentInfo");
var _cancelSuggestionsPopUp = document.getElementById("cancelSuggestionsPopUp");
var _cancelEditButton = document.getElementById("cancelEditButton");
var _cancelSaveUrlPopUp = document.getElementById("cancelSaveUrlPopUp");
/*end cancel buttons*/
var _debug = document.getElementById("debug");


_instrumentName.addEventListener("keyup", function () {
    helpers.setUpCharacterLimit(this, 20);
});

/*Nav events*/

if (_newPlan != null)
    _newPlan.addEventListener("click", createNewPlan);

if (_showAddInstrument != null)
    _showAddInstrument.addEventListener("click", showInstrumentClick);

if (_showAddVenueInstrument != null)
    _showAddVenueInstrument.addEventListener("click", showAddVenueInstrumentClick);


var stagePlan = new function () {
    this.setUp = function (stagePlanType) {
        if (stagePlanType == null)
            alert("Fault - need to pass parameter");

        _stagePlanType = stagePlanType;
    }
}


_showSaveOptions.addEventListener("click", showSaveStageplanPopUp);
//_signInPopUp.addEventListener("click", signInPopUp);
//_showSignUp.addEventListener("click", signUpPopUp);
//_showSuggestionsLink.addEventListener("click", showSuggestionPopUp);
/*end Nav events*/



_saveEdit.addEventListener("click", saveEdit);
_addNewInstrumentButton.addEventListener("click", addInstrumentToStageplan);


saveStageplan.setUp("saveStageplanButton",
                    "instrumentsOnStage",
                    saveForUrl,
                    fail,
                    document.getElementById("captcha_saveStageplan"),
                    document.getElementById("captchaSaveStagePlanTextBox"),
                    null,
                    document.getElementById("enterBandDetails_BandName"),
                    document.getElementById("saveStagePlanBandWebAddress"),
                    document.getElementById("saveStagePlanBandSocialMedia"),
                    document.getElementById("saveStagePlanBandGenre"),
                    document.getElementById("toggleAdvert"),
                    document.getElementById("saveStagePlanBandCountry"),
                    document.getElementById("saveStagePlanBandEmail"),
                    document.getElementById("WillSignUp")
                ); //todo
_deleteButton.addEventListener("click", deleteSelectedInstrument);

//_loadExistingStageplanPopUp.addEventListener("click", loadExistingStageplanPopUp);

/*cancel events*/
if (_cancelEditButton != null) {
    _cancelEditButton.addEventListener("click", function () { closePopUp(_popUp); });
}
if (_cancelSaveStagePlanPopUp != null) {
    _cancelSaveStagePlanPopUp.addEventListener("click", function () { closePopUp(document.getElementById("saveStagePlanPopUp")); });
}
if (_cancelLoadExistingProject != null) {
    _cancelLoadExistingProject.addEventListener("click", function () { closePopUp(document.getElementById("loadExistingStageplanPopUp")); });
}
if (_cancelSignIn != null) {
    _cancelSignIn.addEventListener("click", function () { closePopUp(document.getElementById("signInPopUp")); });
}
if (_cancelSignUp != null) {
    _cancelSignUp.addEventListener("click", function () { closePopUp(document.getElementById("signUpPopUp")); });
}
if (_cancelShowSelectedInstrumentInfo != null) {
    _cancelShowSelectedInstrumentInfo.addEventListener("click", function () { closePopUp(document.getElementById("showSelectedInstrumentInfo")); });
}
if (_cancelSuggestionsPopUp != null) {
    _cancelSuggestionsPopUp.addEventListener("click", function () { closePopUp(document.getElementById("showSuggestionsPopUp")); });
}
if (_cancelSaveUrlPopUp != null) {
    _cancelSaveUrlPopUp.addEventListener("click", function () { closePopUp(document.getElementById("saveUrlPopUp")); });
}

/*end cancel events*/


for (var i = 0; i < _popUpUl.children.length; i++) {
    var image = _popUpUl.children[i].children[0];
    image.addEventListener("click", selectInstrument);
}

function setBandDetails(bandName, members) {
    _addInstrumentPopUp_bandName = bandName;
    _bandMemberNames = members;
}

function setStartupPopup() {
    _currentPopUp = _showEnterBandDetails;
}

function selectCurrentInstrument(e, u) {
    _selectedInstrument = e.target;
}

function addInstrumentToStageplan(sender) {
    var errorMessage = validation.validateAddInstument(_selectedImage, _instrumentName.value, _allInstrumentDetails);
    if (!helpers.isNullOrEmpty(errorMessage)) {
        alert(errorMessage);
        return;
    }

    var bandMember = _addInstrumentPopUp_bandMemberNames.options[_addInstrumentPopUp_bandMemberNames.selectedIndex].value;
    //if (_addInstrumentPopUp_bandMemberNames.selectedOptions.length > 0) {


    var angle = 0;
    var zIndex = 0;

   var isVenueTemplateChecked = _isFixedPositionEle.checked;


    var id = addInstrument.add(_selectedImage.id, null, null, _instrumentName.value, encodeURI(_addInstrumentExtraDetail.value), _selectedImage.src, _instrumentsOnStage, editInstrument, bandMember, true, selectCurrentInstrument, false, _stagePlanType == g_stagePlanType.EditVenue, isVenueTemplateChecked, null, null,reposition.increase, zIndex, angle);
    closeCurrentPopUp();
    _allInstruments++;

    _addedInstrumentId++;
    addNewInstrumentToAllInstruments(id, _instrumentName.value, _addInstrumentExtraDetail.value, _selectedImage.src, _selectedImage.id);


    isFirstTimeUse();

    if (_addedInstrumentId == 1)
        _noInstruments.innerHTML = "Please drag the instrument to where you want it! Then keep adding more instruments to set up the band.";
}

function addNewInstrumentToAllInstruments(id, name, description, image, dataImgId) {
    _allInstrumentDetails.push({ 'id': id, 'name': name, 'description': description, 'image': image, 'dataImgId': dataImgId })
}

function clearSelectedImage() {
    for (var i = 0; i < _popUpUl.children.length; i++) {
        _popUpUl.children[i].children[0].className = null;
    }
    _selectedImage = null;
}

function saveForUrl(data) {
    if (!helpers.isNullOrEmpty(data.error)) {
        alert(data.error);
        return;
    }

    closeCurrentPopUp();
    showPopUp("saveUrlPopUp");
    var url = data.url;
    var a = document.createElement("a");
    a.href = url;
    a.setAttribute("target", "_blank");
    var txt = document.createTextNode(url);
    a.appendChild(txt);
    var savedStagePlanUrl = document.getElementById("savedStagePlanUrl");
    savedStagePlanUrl.innerHTML = "";
    savedStagePlanUrl.appendChild(a);
}


function showSuggestionPopUp() {
    showPopUp("showSuggestionsPopUp");
}

function loadExistingStageplanPopUp() {
    showPopUp("loadExistingStageplanPopUp");
}

function showSaveStageplanPopUp() {
    var errorMessage = validation.isInstrumentsBeenAdded(_isFirstInstrument);
    if (helpers.isNullOrEmpty(errorMessage)) {
        showPopUp("saveStagePlanPopUp");
    } else {
        alert(errorMessage);
    }

}

function signInPopUp() {
    showPopUp("signInPopUp");
}

function signUpPopUp() {
    showPopUp("signUpPopUp");
}

function showPopUp(id) {
    var ele = document.getElementById(id);
    ele.scrollTop = 0;
    closeCurrentPopUp();
    helpers.replaceCssClass(_popUpMask, "hide", "");

    _currentPopUp = ele;
    helpers.replaceCssClass(ele, "hide", "");
}

function closeCurrentPopUp() {
    if (_currentPopUp != null)
        closePopUp(_currentPopUp);
}

function createNewPlan() {
    var willCreateNewPlan = confirm("Are you sure you want to create a new plan? This will re-set your current plan.");
    if (willCreateNewPlan == true)
        location.reload();
}

function fail(msg) {
    alert("There was a fault. Sorry, please contact us direct. " + msg)
}

function showAddInstrument(sender, instrumentDetail, instrumentName, isEdit, bandMemberName, isFixedPosition, isFixedInstrumentFixedForVenueEdit) {
    closeCurrentPopUp();

    helpers.replaceCssClass(_popUpMask, "hide", "");

    if (!isEdit) {
        clearSelectedImage();
    }

    helpers.clearNodes(_addInstrumentPopUp_bandMemberNames);

    var bandMemberNames = [];

    setUpForStageplanType(_stagePlanType);

    _bandMemberNames.forEach(function (item) {
        bandMemberNames.push(item);
    });

    dropDownList.add(_addInstrumentPopUp_bandMemberNames, bandMemberNames, bandMemberName);

    _currentPopUp = _popUp;
    _instrumentName.value = instrumentName || "";
    _addInstrumentExtraDetail.value = instrumentDetail || "";


    if (isFixedInstrumentFixedForVenueEdit == "true") {
        _isFixedPositionEle.checked = true;
    } else {
        _isFixedPositionEle.checked = false;
    }

    helpers.replaceCssClass(_popUp, "hide", "");
    _instrumentsOnStage.className = "blur";

    if (isEdit) {
        helpers.replaceCssClass(_saveEdit, "hide", "");
        _addNewInstrumentButton.className = "hide";

        if (_stagePlanType)
            _deleteButton.className += " hide";
        else
            helpers.replaceCssClass(_deleteButton, "hide", "");


    } else {
        _saveEdit.className = "hide";
        helpers.replaceCssClass(_addNewInstrumentButton, "hide", "");
        _deleteButton.className += " hide";
    }

    _currentPopUp.scrollTop = 0;
}


function setUpForStageplanType(stagePlanType) {

    switch (stagePlanType) {
        case g_stagePlanType.EditVenue:
            _bandMemberNames.push("Not used");
            var stageplanExtraInformation = document.getElementById("stageplanExtraInformation");
            stageplanExtraInformation.className += " hide";
            var selectBandMemberName = document.getElementById("selectBandMemberName");
            selectBandMemberName.className += " hide";
            break;

        case g_stagePlanType.Stageplan:
            var fixedPositionWrapper = document.getElementById("fixedPositionWrapper");
            fixedPositionWrapper.className += " hide";
            var venueExtraInformation = document.getElementById("venueExtraInformation");
            venueExtraInformation.className += " hide";
            break;

        default:
            alert("Unexpected type. SP12342");
            break;

    }


}

function clearBlur() {
    instrumentsOnStage.className = "";
}


function selectInstrument(e) {
    clearSelectedImage();
    var image = e.currentTarget;
    _selectedImage = image;
    clearInstrumentCss();
    image.className = _cssSelected;
    var dataText = image.getAttribute("data-text");
    instrumentName.value = dataText;
    _addInstrumentExtraDetail.setAttribute("placeholder", image.getAttribute("data-helper"));
}

function clearInstrumentCss() {
    for (var i = 0; i < _popUpUl.children.length; i++) {
        var image = _popUpUl.children[i].children[0];
        helpers.replaceCssClass(image, _cssSelected, "");
    }
}

function deleteSelectedInstrument() {
    deleteInstrument(_selectedInstrument);
}

function deleteInstrument(ele) {
    var id = ele.id;

    ele.parentNode.removeChild(ele);
    closePopUp(_popUp);
    _allInstruments--;
    isFirstTimeUse();
    removeFromAllInstruments(id);
}

function removeFromAllInstruments(id) {
    var y = _allInstrumentDetails;
    for (var i = 0; i < _allInstrumentDetails.length; i++) {
        if (_allInstrumentDetails[i].id == id) {
            _allInstrumentDetails.splice(i, 1);
            break;
        }
    }
};

function isFirstTimeUse() {
    if (_allInstruments <= 0) {
        _noInstruments.innerHTML = "Please click on Menu and then add an instrument to the stageplan to get started.";
        _isFirstInstrument = true;
    }
    else {
        noInstruments.innerHTML = "";
        _isFirstInstrument = false;
    }
}


function saveEdit(e) {
    var x = _selectedInstrument.offsetLeft;
    var y = _selectedInstrument.offsetTop;

    var errorMessages = validation.validateSaveInstument(x, y, _selectedImage.src, _instrumentName.value); //todo
    if (!helpers.isNullOrEmpty(errorMessages)) {
        alert(errorMessages);
        return;
    }

    var currentInstrument = e.currentTarget;

    var inst = _selectedImage.id;//.getAttribute("data-img-id");
    if (inst == "")
        inst = _selectedImage.parentElement.parentElement.getAttribute("data-img-id");

    var bandMember = _addInstrumentPopUp_bandMemberNames.options[_addInstrumentPopUp_bandMemberNames.selectedIndex].value;

    var isFixed = _selectedInstrument.getAttribute("data-is-fixed-position") || false;
    var canDrag = isFixed != "true";

    var isFixedForVenueChecked = _isFixedPositionEle.checked;



    var instrumentToDelete = document.getElementById(_selectedInstrument.id);
    deleteInstrument(instrumentToDelete);


    var id = addInstrument.add(inst, x, y, _instrumentName.value, encodeURI(_addInstrumentExtraDetail.value), _selectedImage.src, _instrumentsOnStage, editInstrument, bandMember, canDrag, selectCurrentInstrument, isFixed, _stagePlanType == g_stagePlanType.EditVenue, isFixedForVenueChecked, null, null, reposition.increase, 0);

    addNewInstrumentToAllInstruments(id, _instrumentName.value, _addInstrumentExtraDetail.value, _selectedImage.src, _selectedImage.id);

    closePopUp(_popUp);

    _isFirstInstrument = false;

    _selectedImage = null;

    _allInstruments++;
    isFirstTimeUse();
}


function showInfoForInstrument(e) {
    var parentDivInfo = getInstrumentDiv(e.currentTarget.parentNode);
    var detail = parentDivInfo.getAttribute("data-detail");
    document.getElementById("showSelectedInstrumentInfoText").innerHTML = detail || "There is no additional information";
    showPopUp("showSelectedInstrumentInfo");
}

function getInstrumentDiv(selectedElement) {
    if (selectedElement.getAttribute("data-parent") == "parent") {
        return selectedElement;
    }
    var parent = selectedElement.parentElement;

    if (parent.getAttribute("data-parent") == "parent") {
        return parent;
    }
    alert("didn't work woops sp13247");
}

function closePopUp(ele) {
    ele.className += " hide";
    _popUpMask.className += " hide";
    clearInstrumentCss();
    clearBlur();
}

function editInstrument(e) {
    var button = e.currentTarget;
    var currentInstrument = getInstrumentDiv(button);
    var id = currentInstrument.getAttribute("data-img-id");

    _selectedInstrument = currentInstrument;
    _selectedImage = currentInstrument.children[0].children[0];
    var instrumentDetail = currentInstrument.getAttribute("data-detail");
    var instrumentName = currentInstrument.getAttribute("data-text");
    var chosenBandMemberName = currentInstrument.getAttribute("data-bandmember");
    var isFixedVenueEdit = currentInstrument.getAttribute("data-is-fixed-position-venue-edit");

    var parent = button.parentNode;
    var isFixedPosition = parent.getAttribute("data-is-fixed-position");
    showAddInstrument(null, decodeURI(instrumentDetail), instrumentName, true, chosenBandMemberName, isFixedPosition, isFixedVenueEdit);

    document.getElementById(id).className = _cssSelected;

    if (_stagePlanType == g_stagePlanType.VenueStageplan && isFixedPosition)
        _instruementsPanel.className += " hide";
}

function showAddVenueInstrumentClick() {
    showAddInstrument(null, null, null, false, null, false, false);
}

function showInstrumentClick() {
    showAddInstrument(null, null, null, false, null, false, false);
}

isFirstTimeUse();


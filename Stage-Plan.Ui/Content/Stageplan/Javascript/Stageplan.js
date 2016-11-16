

lmsResponsiveMenu.setUp({ "parentId": "menu", "navId": "mainNav", "responsiveCssClosed": "responsiveCssClosed", "text": "Menu" });

/* create the nav links */

var _newPlan = document.getElementById("showNewPlanLink");
var _showAddInstrument = document.getElementById("showAddInstrumentLink");
var _showSaveOptions = document.getElementById("showSaveOptionsLink");
var _loadExistingStageplanPopUp = document.getElementById("showLoadExistingStageplanLink");
var _signInPopUp = document.getElementById("showSignInLink");
var _showSignOut = document.getElementById("showSignOutLink");
var _showSignUp = document.getElementById("showSignUpLink");
var _showSuggestionsLink = document.getElementById("showSuggestionsLink");
/* end nav links */

var _currentPopUp;

var _saveEdit = document.getElementById("saveEditButton");
var _popUp = document.getElementById("addInstrumentPopUp");
var _instrumentName = document.getElementById("instrumentName");
var _addNewInstrumentButton = document.getElementById("addButton");
var _cancelEditButton = document.getElementById("cancelEditButton");
var _addInstrumentExtraDetail = document.getElementById("addInstrumentExtraDetail");
var _deleteButton = document.getElementById("deleteButton");
var _popUpUl = _popUp.children[1];
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

/*cancel  buttons*/
var _cancelSaveStagePlanPopUp = document.getElementById("cancelSaveStagePlanPopUp");
var _cancelLoadExistingProject = document.getElementById("cancelLoadExistingProject");
var _cancelSignIn = document.getElementById("cancelSignIn");
var _cancelSignUp = document.getElementById("cancelSignUp");
var _cancelShowSelectedInstrumentInfo = document.getElementById("cancelShowSelectedInstrumentInfo");
var _cancelSuggestionsPopUp = document.getElementById("cancelSuggestionsPopUp");
/*end cancel buttons*/
var _debug = document.getElementById("debug");


var _isMouseDown = false;

document.addEventListener("mousemove", moveEquipment);
document.addEventListener('mouseup', mouseUpWindow);

/*Nav events*/
_newPlan.addEventListener("click", createNewPlan);
_showAddInstrument.addEventListener("click", showAddInstrument);
_showSaveOptions.addEventListener("click", showSaveStageplanPopUp);
//_signInPopUp.addEventListener("click", signInPopUp);
//_showSignUp.addEventListener("click", signUpPopUp);
//_showSuggestionsLink.addEventListener("click", showSuggestionPopUp);
/*end Nav events*/

_saveEdit.addEventListener("click", saveEdit);
_addNewInstrumentButton.addEventListener("click", addNewInstrument);

_deleteButton.addEventListener("click", deleteInstrument);

//_loadExistingStageplanPopUp.addEventListener("click", loadExistingStageplanPopUp);

/*cancel events*/
_cancelEditButton.addEventListener("click", function () { closePopUp(_popUp); });
_cancelSaveStagePlanPopUp.addEventListener("click", function () { closePopUp(document.getElementById("saveStagePlanPopUp")); });
_cancelLoadExistingProject.addEventListener("click", function () { closePopUp(document.getElementById("loadExistingStageplanPopUp")); });
_cancelSignIn.addEventListener("click", function () { closePopUp(document.getElementById("signInPopUp")); });
_cancelSignUp.addEventListener("click", function () { closePopUp(document.getElementById("signUpPopUp")); });
_cancelShowSelectedInstrumentInfo.addEventListener("click", function () { closePopUp(document.getElementById("showSelectedInstrumentInfo")); });
_cancelSuggestionsPopUp.addEventListener("click", function () { closePopUp(document.getElementById("showSuggestionsPopUp")); });
/*end cancel events*/


for (var i = 0; i < _popUpUl.children.length; i++) {
    var image = _popUpUl.children[i].children[0];
    image.addEventListener("click", selectInstrument);
}

function showSuggestionPopUp() {
    showPopUp("showSuggestionsPopUp");
}

function loadExistingStageplanPopUp() {
    showPopUp("loadExistingStageplanPopUp");
}

function showSaveStageplanPopUp() {
    showPopUp("saveStagePlanPopUp");
}

function signInPopUp() {
    showPopUp("signInPopUp");
}

function signUpPopUp() {
    showPopUp("signUpPopUp");
}

function showPopUp(id) {
    var ele = document.getElementById(id);
    if (_currentPopUp != null)
        closeCurrentPopUp();

    _currentPopUp = ele;
    replaceCssClass(ele, "hide", "");
}

function closeCurrentPopUp() {
    if (_currentPopUp != null)
        closePopUp(_currentPopUp);
}

function createNewPlan() {
    location.reload();
}

function showAddInstrument(e, instrumentDetail, instrumentName, isEdit) {
    closeCurrentPopUp();

    _currentPopUp = _popUp;
    _instrumentName.value = instrumentName || "";
    _addInstrumentExtraDetail.value = instrumentDetail || "";
    replaceCssClass(_popUp, "hide", "");
    _instrumentsOnStage.className = "blur";

    if (isEdit) {
        replaceCssClass(_saveEdit, "hide", "");
        _addNewInstrumentButton.className = "hide";
        replaceCssClass(_deleteButton, "hide", "");


    } else {
        _saveEdit.className = "hide";
        replaceCssClass(_addNewInstrumentButton, "hide", "");
        _deleteButton.className += " hide";
    }
}



function clearBlur() {
    instrumentsOnStage.className = "";
}


function selectInstrument(e) {
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
        replaceCssClass(image, _cssSelected, "");
    }
}

function deleteInstrument() {
    _selectedInstrument.parentNode.removeChild(_selectedInstrument);
    closePopUp(_popUp);
}


function saveEdit() {
    var x = _selectedInstrument.offsetLeft;
    var y = _selectedInstrument.offsetTop;
    deleteInstrument();
    addNewInstrument(x, y);
}

function addNewInstrument(x, y) {
    var instrumentInfo = document.createElement("div");
    instrumentInfo.setAttribute("class", "instrumentInfo");
    instrumentInfo.setAttribute("data-parent", "parent");
    instrumentInfo.style.left = "300px";
    instrumentInfo.style.position = "absolute";

    if (x && y) {
        instrumentInfo.style.left = x;
        instrumentInfo.style.top = y;
    }

    var image = document.createElement("img");
    instrumentInfo.setAttribute("data-text", _instrumentName.value);
    instrumentInfo.setAttribute("data-detail", _addInstrumentExtraDetail.value);

    image.setAttribute("src", _selectedImage.src);

    var br = document.createElement("br");

    var text = document.createTextNode(_instrumentName.value);

    var br2 = document.createElement("br");

    var editButton = document.createElement("input");
    editButton.setAttribute("type", "button");
    editButton.setAttribute("value", "Edit");
    editButton.setAttribute("id", "");
    editButton.setAttribute("class", "editButton");
    editButton.addEventListener("click", editInstrument);



    instrumentInfo.appendChild(image);
    instrumentInfo.appendChild(br);
    instrumentInfo.appendChild(text);
    instrumentInfo.appendChild(br2);
    instrumentInfo.appendChild(editButton);
    _instrumentsOnStage.appendChild(instrumentInfo);

    instrumentInfo.addEventListener("mousedown", mouseDown);


    closePopUp(_popUp);

    _isFirstInstrument = false;

    _selectedImage = null;
}


var tempX = 0;
var tempY = 0;

function showInfoForInstrument(e) {
    var parentDivInfo = getInstrumentDiv(e.currentTarget.parentNode);
    var detail = parentDivInfo.getAttribute("data-detail");
    document.getElementById("showSelectedInstrumentInfoText").innerHTML = detail || "There is no additional information";
    showPopUp("showSelectedInstrumentInfo");
}

function getMouseXY(e) {
    tempX = e.pageX;
    tempY = e.pageY;


    if (tempX < 0) { tempX = 0; }
    if (tempY < 0) { tempY = 0; }


    return true;
}



function moveEquipment(event) {

    getMouseXY(event);

    _debug.innerHTML = "origX: " + event.clientX + "<br /> origY: " + event.clientY + "<br />tempX:" + tempX + "<br />tempY:" + tempY + "<br />";

    if (_selectedInstrument != null)
        _debug.innerHTML += "<br />selectInstrument left: " + _selectedInstrument.style.left + "<br />top:" + _selectedInstrument.style.top;
    if (!_isMouseDown)
        return;

    var x = event.clientX;
    var y = event.clientY;

    if (x < _stageLeft)
        x = _stageLeft;

    if (x > _stageRight)
        x = _stageRight;

    if (y < _stageTop)
        y = _stageTop;

    if (y > _stageBottom)
        y = _stageBottom;


    _debug.innerHTML += "<br />X: " + x + "<br />Y:" + y + "<br />";

    _selectedInstrument.style.left = x + "px";
    _selectedInstrument.style.top = y + "px";

}

function mouseDown(event) {
    _isMouseDown = true;
    _selectedWidth = event.target.clientWidth;
    _selectedHeight = event.target.clientHeight;
    _stageRight -= _selectedWidth;
    _stageBottom -= _selectedHeight;
    _selectedInstrument = getInstrumentDiv(event.target);
    event.preventDefault();
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

function mouseUpWindow(event) {
    if (_isMouseDown) {
        _isMouseDown = false;
        _stageRight += _selectedWidth;
        _stageBottom += _selectedHeight;
    }

}



function closePopUp(ele) {
    ele.className += " hide";
    clearInstrumentCss();
    clearBlur();
}

function editInstrument(e) {
    var button = e.currentTarget;
    var currentInstrument = getInstrumentDiv(button);
    _selectedImage = currentInstrument.children[0];
    var instrumentDetail = currentInstrument.getAttribute("data-detail");
    var instrumentName = currentInstrument.getAttribute("data-text");
    showAddInstrument(null, instrumentDetail, instrumentName, true);
    document.getElementById(instrumentName.replace(" ", "")).className = _cssSelected;
}






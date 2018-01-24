"use strict"

//Requires a list, set up as 
//
//<ul id="someId">
//  <li>
//    <div>First Child Is Header</div>
//    <div>
//        <h2>Second main child is the body</h2>
//        <p>The body content</p>
//    </div>   
//  </li>
//</ul>
//<div id="bodyId"></div> <!-- optional -->



var lmsTabControl = new function () {
    var _selectedCssClass;
    var _parentEle;
    var _bodies = [];
    // var _lmsTabControlLi = "lmsTabControlLi";
    var _lmsTabControlBodyEle;
    var _datalmstabcontrol = "data-lmstabcontrol";
    //defaultOpenTab                   The 0 based number to show. If you don't want any tab to be selected, don't pass a number. If you want the first tab to be shown, pass 0
    //bodyId                           Where you want the content to actually be placed. If nothing passed, it will create a new element 
    //{selectedCssClass:'', parentId:'', bodyCssClass:'', defaultOpenTab:'', bodyId:'' }
    this.setUp = function (param) {
        _selectedCssClass = param.selectedCssClass;
        _parentEle = document.getElementById(param.parentId);

        for (var i = 0; i < _parentEle.children.length; i++) {
            validateChildren(2, _parentEle.children[i].children); //gaurented to have only 2 children.
            _parentEle.children[i].children[0].setAttribute(_datalmstabcontrol, i);
            _parentEle.children[i].children[1].setAttribute(_datalmstabcontrol, i);

            _parentEle.children[i].children[0].addEventListener("click", function (sender) {

                var ele = sender.currentTarget;
                var item = ele.getAttribute(_datalmstabcontrol);
                lmsTabControl.switchTab(item);
            });

            var wrapper = document.createElement("div");
            wrapper.appendChild(_parentEle.children[i].children[1]);

            _bodies.push({ 'body': wrapper, 'head': _parentEle.children[i].children[0], 'index': i });
            helpers.clearNodes(_parentEle.children[i].children[1]);


        }

        hideAll();


        if (helpers.isNullOrEmpty(param.bodyId))
            createBodyElement(_parentEle, param.bodyCssClass);
        else
            _lmsTabControlBodyEle = document.getElementById(params.bodyId);


        for (var i = 0; i < _bodies.length; i++) {
            _lmsTabControlBodyEle.appendChild(_bodies[i].body);
        }

        for (var i = 0; i < _parentEle.children.length; i++) {
            if (param.defaultOpenTab == i) {
                selectTab(_parentEle.children[i].children[0]);
                showBody(i);
                break;
            }
        }
    },


    this.switchTab = function (tabIndex) {
        deselectHead();
        hideAll();
        showBody(tabIndex);
        selectTab(_bodies[tabIndex].head);
    };

    function createBodyElement(ele, css){
        _lmsTabControlBodyEle = document.createElement("div");
        _lmsTabControlBodyEle.className += " " + css;
        _lmsTabControlBodyEle.id = "tempTab";
        helpers.insertAfter(ele, _lmsTabControlBodyEle);
    }

    function hideAll() {
        for (var i = 0; i < _bodies.length; i++) {
            hide(_bodies[i].body);
        }
    }

    function deselectHead() {
        for (var i = 0; i < _bodies.length; i++) {
            _bodies[i].head.className = helpers.replaceAll(_bodies[i].head.className, _selectedCssClass, "");
        }
    }

    function showBody(i) {
        removeHide(_bodies[i].body);
        _lmsTabControlBodyEle.appendChild(_bodies[i].body);
    }


    //function hideBody() {
    //    //helpers.clearNodes(_lmsTabControlBodyEle);
    //    //helpers.replaceAll(_lmsTabControlBodyEle.className, "hide", "");
    //    _lmsTabControlBodyEle.className = " hide";
    //}

    function selectTab(ele) {
        if (helpers.isNullOrEmpty(_selectedCssClass))
            Console.write("No selectedCssClass")

        ele.className += " " + _selectedCssClass;

    }

    
    function hide(ele){
        ele.style.display = "none";
        ele.style.visibility = "collapsed";
    }

    function removeHide(ele) {
        ele.style.display = "block";
        ele.style.visibility = "visible";
    }

    function validateChildren(childrenAmount, ele){
        if (ele.length > childrenAmount) {
            alert("Should only have "+childrenAmount+" children");
        }
    }
}
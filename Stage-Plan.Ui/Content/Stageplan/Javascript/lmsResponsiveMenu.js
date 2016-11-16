
/*
//Usage
//
//Assumes you have only 1 navigation and will use the 1 navigation for both normal and 
responsive menus
//
//no need to use jquery 
//after reference the lmsResponsiveMenu.js file
//in the head, reference the lmsResponsiveMenu.css file
//
// at bottom of page, call the function in the jquery onload
//
//    <script>
//        lmsResponsiveMenu.setUp();
//    </script>
//
//and with parameters
//
//    <script>
//        $(function () {
//        lmsResponsiveMenu.setUp({ text: "Menu", parentId: "menu", linkLiCss: "myClass" });
//        });
//    </script>
//
//
//lmsResponsveMenuVisible = true, false or only
//true is default, it's not needed to be set (leaving it empty is implicitly setting it to true
//false means it will not appear in the responsive menu
//only means it is removed from the non-responsive menu but shown in the responsive menu
//
//
//    <li data-lmsResponsiveMenuVisible="false"><a href="#">link</a></li>
//html structure could be
//
//<div class="showIfSmall hideIfMedium hideIfLarge paddingLeft10px" id="menu"></div>
//<nav id = "lmsResponsiveMenu"> 
//<ul class="hideIfSmall">
//<li></li>
//<li></li>
//<li></li>
//</ul>
//</nav>
//


//text - default is Menu
//parentId - the parent ID where the menu will show
//linkLiCss - the css to use on each list item (li)
//navId - the ID of the nav 
//responsiveCssClosed - the CSS to use for the closed menu in responsive mode

*/

var lmsResponsiveMenu = new function () {

    var lmsResponsiveMenuInnerUlId = "lmsResponsiveMenuInner";
    
    this.setUp = function (property) {
        var text = "Menu";
        var parentId = "";
        var linkLiCss = ""; 
		var navId = "";
		var responsiveCssClosed="";

        if (property != null) {
            text = property.text || text;
            parentId = property.parentId || parentId;
            linkLiCss = property.linkLiCss || linkLiCss;
			navId = property.navId || navId;
			responsiveCssClosed = property.responsiveCssClosed || responsiveCssClosed; 
        }

        var lmsMenu = document.getElementById(navId);

        var lmsUl;
        //loop through until we find the ul

        var i = 0;
        var total = lmsMenu.children.length;
        for (i = 0; i < total; i++) {
            if (lmsMenu.children[i].nodeName == "UL") {
                lmsUl = lmsMenu.children[i];
                break;
            }
        }


        var frag = document.createDocumentFragment();

        var aLink = document.createElement("a");
        aLink.setAttribute("id", "lmsResponsiveMenuLink");
        aLink.setAttribute("class", "lmsResponsiveViewCursor ");
        var aLinkText = document.createTextNode(text);
        aLink.appendChild(aLinkText);


        var ul = document.createElement("ul");
        ul.setAttribute("id", lmsResponsiveMenuInnerUlId);
        ul.setAttribute("class", "lmsResponsiveMenuHide");

        i = 0;//already declared above
        var totalChildren = lmsUl.children.length;
        for (i = 0; i < totalChildren; i++) {
            if (lmsUl.children[i] == null)
                continue;

            var willShow = lmsUl.children[i].attributes["data-lmsResponsiveMenuVisible"]; //["data-lmsResponsiveMenuVisible"];

            if (willShow != null) {
                if (willShow.value.toLowerCase() == "false")
                    continue;

                if (willShow.value.toLowerCase() == "only") {
                    lmsUl.children[i].className += " lmsResponsiveMenuHide";
                }
            }




            var li = document.createElement("li");
            li.className = linkLiCss;
            li.innerHTML = lmsUl.children[i].innerHTML;
            ul.appendChild(li);
        }

        //toggleVisibility("lmsResponsiveMenu", parentId);


        frag.appendChild(aLink);
        frag.appendChild(ul);

        var menu = document.getElementById(parentId);
		menu.appendChild(frag);
		menu.className += " "  + responsiveCssClosed;

        document.getElementById(aLink.id).addEventListener("click", function () {
            toggleVisibility(lmsResponsiveMenuInnerUlId);
        });
    };

    function toggleVisibility(lmsId) {

        var isVisible = containsClass(lmsId, "lmsResponsiveMenuShow");

        if (isVisible) {
            removeClass(lmsId, "lmsResponsiveMenuShow");
            addClass(lmsId, "lmsResponsiveMenuHide");
        } else {
            removeClass(lmsId, "lmsResponsiveMenuHide");
            addClass(lmsId, "lmsResponsiveMenuShow");
        }
    }


    function containsClass(lmsId, cssClass) {
        var classes = document.getElementById(lmsId).className;

        if (classes == null || classes == "")
            return false;

        var currentClasses = classes.split(" ");

        var i = 0;
        for (i = 0; i < currentClasses.length; i++) {
            if (currentClasses[i].toLowerCase() == cssClass.toLowerCase())
                return true;
        }

        return false;
    }


    function addClass(lmsId, cssClass) {

        document.getElementById(lmsId).className = document.getElementById(lmsId).className + " " + cssClass;
        var debug = "";
    }

    function removeClass(lmsId, cssClass) {

        document.getElementById(lmsId).className = document.getElementById(lmsId).className.replace(cssClass, "");
    }

}
var rating = new function () {

    this.setUp = function (parentEle, currentScore) {

        var frag = document.createDocumentFragment();

        var splitScore = String(currentScore).split(".");
        var wholeNumber = splitScore[0];

        var fullStars = getFullStars(wholeNumber, currentScore);

        var partNumber = splitScore[1][0];
        var partStars = null;
        if (partNumber >= 8) {
            partStars = getPartStars(8, currentScore);
        }
        if (partNumber < 8) {
            partStars = getPartStars(6, currentScore);
        }
        if (partNumber < 6) {
            partStars = getPartStars(4, currentScore);
        }

        if (partNumber < 4) {
            partStars = getPartStars(2, currentScore);
        }

        frag.appendChild(fullStars);
        frag.appendChild(partStars);
        var parent = document.getElementById(parentEle);
        parent.appendChild(frag);

        //<img src="~/Content/MainSite/Images/Star40.png" />
    };

    function getFullStars(wholeNumber, title) {
        var frag = document.createDocumentFragment();

        for (var i = 0; i < wholeNumber; i++) {
            var img = document.createElement("img");
            img.alt = title;
            img.title = title;
            img.src = "/Content/MainSite/Images/Star.png";
            frag.appendChild(img);
        }

        return frag;
    }

    function getPartStars(partNumber, title) {
        var frag = document.createDocumentFragment();

        var img = document.createElement("img");
        img.alt = title;
        img.title = title;
        img.src = "/Content/MainSite/Images/Star" + partNumber + "0.png";
        frag.appendChild(img);

        return frag;
    }

}
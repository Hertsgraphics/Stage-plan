var lmsRating = new function () {
    var _starNotSelectedPath;
    var _starSelectedPath;
    var _rating;
    this.setUp = function (eleId, starSelectedPath, starNotSelectedPath, defaultSelectedStars) {

        _starNotSelectedPath = starNotSelectedPath;
        _starSelectedPath = starSelectedPath;
        _rating = defaultSelectedStars || 0;
        var divWrapper = document.createElement("div");
        divWrapper.id = "lmsRating";
        var star1 = document.createElement("img");
        var star2 = document.createElement("img");
        var star3 = document.createElement("img");
        var star4 = document.createElement("img");
        var star5 = document.createElement("img");

        setProperties(star1, 1);
        setProperties(star2, 2);
        setProperties(star3, 3);
        setProperties(star4, 4);
        setProperties(star5, 5);


        divWrapper.appendChild(star1);
        divWrapper.appendChild(star2);
        divWrapper.appendChild(star3);
        divWrapper.appendChild(star4);
        divWrapper.appendChild(star5);

        var parent = document.getElementById(eleId);
        parent.appendChild(divWrapper);

        if (defaultSelectedStars > 0)
            showSelected(defaultSelectedStars, true);
    },

    this.getRating = function () {
        return _rating > 0 ? _rating : 0;
    };

    function setProperties(ele, score) {
        ele.src = _starNotSelectedPath;
        ele.id = "lmsRating" + score;
        ele.alt = score + " stars";
        ele.setAttribute("data-star", score)
        ele.title = score + " stars";
        ele.style.paddingRight = "10px";

        ele.addEventListener("mouseover", function (e) {
            clear(false);
            var currentScore = e.currentTarget.getAttribute("data-star");
            showSelected(currentScore, false);

        });

        ele.addEventListener("mouseout", function () { clear(false); });


        ele.addEventListener("click", function (e) {
            clear(true);
            var currentScore = e.currentTarget.getAttribute("data-star");
            showSelected(currentScore, true);
            _rating = currentScore;
        });
    }
    function showSelected(index, isFixed) {
        for (var i = 0; i < index; i++) {
            var id = "lmsRating" + Number(i + 1);
            var str = document.getElementById(id);
            str.src = _starSelectedPath;

            if (isFixed)
                str.setAttribute("data-fixed", "yup");
        }

    }

    function clear(willClearFixed) {
        for (var i = 0; i < 5; i++) {
            var id = "lmsRating" + Number(i + 1);
            var str = document.getElementById(id);

            if (willClearFixed) {
                str.setAttribute("data-fixed", "nope");
            }

            var atty = str.getAttribute("data-fixed");
            if (willClearFixed || atty != "yup")
                str.src = _starNotSelectedPath;

        }
    }

}
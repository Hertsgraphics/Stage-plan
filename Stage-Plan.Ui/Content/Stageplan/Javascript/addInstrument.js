"use strict"
var addInstrument = new function () {
    var _index = 0;
    var _isDragging;
    this.add = function (dataImgId, x, y, instrumentName, addInstrumentExtraDetail, selectedImage, instrumentsOnStageEle, editInstrumentCallback, bandMember, canDrag, onSelectCallback, isFixedPosition, isVenueEditTemplate, isVenueEditFixedPosition, instrumentWidth, instrumentHeight, repositionCallback, zIndex, rotateAngle) {

        if (isVenueEditTemplate == null)
            console.log("Need to pass isFixedPosition");

        var id = "stagePlanInstrument_" + _index;
        var instrumentInfo = document.createElement("div");
        //instrumentInfo.setAttribute("class", "instrumentInfo");
        instrumentInfo.setAttribute("data-parent", "parent");
        instrumentInfo.style.top = 100 + helpers.getRandomNumber(0, 200) + "px";
        instrumentInfo.style.left = 200 + helpers.getRandomNumber(0, 100) + "px";
        instrumentInfo.style.position = "absolute";
        instrumentInfo.id = id;

        if (x != null && y != null) {
            instrumentInfo.style.left = x + "px";
            instrumentInfo.style.top = y + "px";
        }




        instrumentInfo.setAttribute("data-text", instrumentName);
        instrumentInfo.setAttribute("data-detail", addInstrumentExtraDetail);
        instrumentInfo.setAttribute("data-img-id", dataImgId);
        instrumentInfo.setAttribute("data-src", selectedImage);
        instrumentInfo.setAttribute("data-bandmember", bandMember);
        instrumentInfo.setAttribute("data-is-fixed-position", isFixedPosition);
        instrumentInfo.setAttribute("data-is-fixed-position-venue-edit", isVenueEditFixedPosition);
        instrumentInfo.style.zIndex = zIndex;
        instrumentInfo.title = "Band member: " + bandMember;
        instrumentInfo.alt = "Band member: " + bandMember;


        var imgTemp = document.getElementById(dataImgId);

        if (imgTemp != null) {
            var limit = imgTemp.getAttribute("data-rotate-limit");
            instrumentInfo.setAttribute("data-rotate-limit", limit);
        }

        if (!canDrag) {
            instrumentInfo.className += " fixedPosition ";
        }

        var image = document.createElement("img");
        image.setAttribute("src", selectedImage);

        //image.setAttribute("data-rotate", rotateAngle);

        if (instrumentWidth && instrumentHeight) {
            image.style.width = instrumentWidth + "px";
            image.style.height = instrumentHeight + "px";
        }

        if (repositionCallback != null)
            instrumentInfo.addEventListener("click", function () {
                repositionCallback(instrumentInfo);
                //_isDragging = false;
            });

        if (editInstrumentCallback != null)
            instrumentInfo.addEventListener("dblclick", editInstrumentCallback);

        instrumentInfo.appendChild(image);

        instrumentsOnStageEle.appendChild(instrumentInfo);

        if (rotateAngle != 0) {
            if (rotateAngle === -1)
                flipImage(instrumentInfo);
            else
                $(instrumentInfo).rotate(rotateAngle);
        }

        var ratio = "1/2";

        $(image).resizable({
            aspectRatio: ratio,
            ghost: false,
            animate: false,
            minHeight: 20,
            minWidth: 20,
            start: function (event, ui) {
                _isDragging = true;
            },
            stop: function (event, ui) {
                resetDrag();
            },
        });

        if (canDrag) {// && isVenueEditTemplate) {
            $(instrumentInfo).draggable({
                drag: function () {
                    if (repositionCallback != null)
                        repositionCallback(instrumentInfo);

                    onSelectCallback(this);
                },
                start: function (event, ui) {
                    _isDragging = true;
                },
                stop: function (event, ui) {
                    resetDrag();
                },
                containment: "#stage",
                scroll: false
            });
        }



        instrumentInfo.addEventListener("click", function () {
            if (_isDragging) {
                _isDragging = false;
                return;
            }

            var rotateRule = instrumentInfo.getAttribute("data-rotate-limit");
            var rotateAngle = 0;

            if (rotateRule === "horizontal" || rotateRule === -1) {
                flipImage(this);
                return;
            }

            if (rotateRule === null || rotateRule === "none")
                return;

            if (rotateRule === "full")
                rotateAngle = $(this).getRotateAngle()[0] + 90;



            //var angle = $(this).getRotateAngle()[0];

            $(this).rotate(rotateAngle);
            this.setAttribute("data-rotate", rotateAngle);

        });


        _index++;

        return id;
    }

    function flipImage(img) {
        if (helpers.contains(img.className, "flipImage"))
            helpers.replaceCssClass(img, "flipImage", "");
        else
            img.className += " flipImage";

        var rotate = img.getAttribute("data-rotate");

        if (rotate == null) {
            img.setAttribute("data-rotate", -1);
            return;
        }
        
        if(rotate != null && rotate == 0)
            img.setAttribute("data-rotate", -1);
        
        if (rotate != null && rotate == -1)
            img.setAttribute("data-rotate", 0);

    }

    function resetDrag() {
        setTimeout(function () { _isDragging = false; }, 250);
    }
}
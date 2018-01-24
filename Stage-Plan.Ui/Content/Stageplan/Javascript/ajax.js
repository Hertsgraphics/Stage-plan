var ajaxCall = new function () {
    var _spinner = null;
    this.postNow = function (url, data, successDelegate, failDelegate, errorDelegate) {

        if (successDelegate == "" || successDelegate == null)
            alert("No success function passed!");

        setSpinner();       

        toDatabase("POST", url, data, successDelegate, failDelegate, errorDelegate);

    };

    this.getNow = function (url, data, successDelegate, failDelegate, errorDelegate) {

        if (successDelegate == "" || successDelegate == null)
            alert("No success function passed!");

        setSpinner();

        toDatabase("GET", url, data, successDelegate, failDelegate, errorDelegate);

    };

    function toDatabase(type, url, data, successDelegate, failDelegate, errorDelegate) {
            $.ajax({
                type: type.toUpperCase(),
                url: url,
                contentType: "application/json;",
                data: data,
                dataType: "json",
                success: function (response) {
                    successDelegate(response); removeSpinner();
                },
                failure: function (e) {
                    failDelegate(e.statusText); removeSpinner();
                },
                error: function (e) {
                    errorDelegate(e.statusText); removeSpinner();
                }
            })
        }

        function setSpinner() {
            _spinner = document.getElementById("lmsSpinnerId");
            var spinnerImg = document.getElementById("lmsSpinnerImgId");
            if (_spinner == null) {
                _spinner = document.createElement("div");
                _spinner.id = "lmsSpinnerId";
                _spinner.style.position = "absolute";
                _spinner.style.top = "0";
                _spinner.style.left = "0";
                _spinner.style.backgroundColor = "#fff";
                _spinner.style.border = "solid 1px #444";
                _spinner.style.opacity = 9;
                _spinner.style.minWidth = "100%";
                _spinner.style.minHeight = "100%";
                _spinner.style.zIndex = 99999;
                _spinner.className = "spinnerWrapper";

                spinnerImg = document.createElement("img");
                spinnerImg.id = "lmsSpinnerImgId";
                spinnerImg.src = "/Content/Stageplan/Images/EQ-Spinner.gif";
                spinnerImg.className = "spinner";
                spinnerImg.style.maxWidth = "300px";
                spinnerImg.style.maxHeight= "300px";
                _spinner.appendChild(spinnerImg);
                var body = document.body;
                body.appendChild(_spinner);
            }
        }

        function removeSpinner() {
            if (_spinner == null)
                return;

            _spinner.className = "hide";
            var parent = _spinner.parentNode;

            if (parent)
                parent.removeChild(_spinner);

        }
    };
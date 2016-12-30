var ajaxCall = new function () {
    
    this.postNow = function (url, data, successDelegate, failDelegate, errorDelegate) {

        if (successDelegate == "" || successDelegate == null)
            alert("No success function passed!");

        var spinner = document.getElementById("lmsSpinnerId");
        var spinnerImg = document.getElementById("lmsSpinnerImgId");
        if (spinner == null) {
            spinner = document.createElement("div");
            spinner.id = "lmsSpinnerId";
            spinner.style.position = "absolute";
            spinner.style.top = "0";
            spinner.style.left = "0";
            spinner.style.backgroundColor = "#fff";
            spinner.style.border = "solid 1px #444";
            spinner.style.opacity = 9;
            spinner.style.minWidth = "100%";
            spinner.style.minHeight = "100%";
            spinner.style.zIndex = 99999;
            spinner.className = "spinnerWrapper";

            spinnerImg = document.createElement("img");
            spinnerImg.id = "lmsSpinnerImgId";
            spinnerImg.src = "../Content/Images/spinner.gif";
            spinnerImg.className = "spinner";
            spinner.appendChild(spinnerImg);
            var body = document.body;
            body.appendChild(spinner);
        }



        $.ajax({
            type: "POST",
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
        });

        function removeSpinner() {
            if (spinner == null)
                return;

            spinner.className = "hide";
            var parent = spinner.parentNode;

            if (parent)
                parent.removeChild(spinner);

        }
    };
}
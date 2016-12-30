var helpers = new function () {

    var _previousLength = 0;

    this.setUpCharacterLimit = function (ele, maxChar) {
        ele.addEventListener("keyup", function () {
            limitMaxChar(maxChar, ele);
        });
    },


    this.setUpNumbersOnly = function (eleId) {
        var ele = document.getElementById(eleId);
        ele.addEventListener("keyup", function () {
            numbersOnly(ele);
        });

    },

    this.isNullOrEmpty = function (string) {
        return string == "" || string == null;
    };

    function limitMaxChar(maxChar, textBox) {
        if (_previousLength != textBox.value.length && textBox.value.length > maxChar)
            textBox.value = textBox.value.substring(0, maxChar);

        _previousLength = textBox.value.length;
    }

    function numbersOnly(textBox) {
        if (isNaN(textBox.value)) {
            textBox.value = "";
            textBox.placeholder = "Numbers and . only"
        }
    }



}
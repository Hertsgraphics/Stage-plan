"use strict"

var helpers = new function () {

    var _previousLength = 0;

    this.setUpCharacterLimit = function (ele, maxChar) {
        ele.addEventListener("keyup", function () {
            limitMaxChar(maxChar, ele);
        });
    },

    this.clearNodes = function (ele) {
        if(ele == null) {
            return;
        }

        while (ele.firstChild){
            ele.removeChild(ele.firstChild);
        }
    },

    this.setUpNumbersOnly = function (eleId) {
        var ele = document.getElementById(eleId);
        ele.addEventListener("keyup", function () {
            numbersOnly(ele);
        });

    },

    this.contains = function (wordToSearchIn, wordToSearchFor) {
        if (wordToSearchFor == " ") {
            var result = wordToSearchIn.indexOf(wordToSearchFor) > 0;
            return result;
        }
        return wordToSearchIn.toLowerCase().indexOf(wordToSearchFor.toLowerCase()) > 0;
    }

    this.isNullOrEmpty = function (string) {
        if (string == null || string == undefined)
            return true;

        string = helpers.replaceAll(string, " ", "");
        return string == "" || string == null;
    };

    this.copy = function (ele) {
        if (document.selection) { //IE
            var range = document.body.createTextRange();
            range.moveToElementText(ele);
            range.select();
        } else if (window.getSelection) { //others
            var range = document.createRange();
            range.selectNode(ele);
            window.getSelection().empty();
            window.getSelection().addRange(range);
        }
        var didCopy = document.execCommand('copy');
        if (didCopy)
            alert("Copied. You can now paste your unique URL");
        else
            alert("Failed to copy, sorry, you'll need to select the URL and copy it manually");
    }


    this.insertAfter = function(referenceNode, newNode) {
        referenceNode.parentNode.insertBefore(newNode, referenceNode.nextSibling);
    }

    this.replaceCssClass = function (ele, cssToRemove, replaceWith) {

        if (ele.className == null) {
            ele.className = replaceWith;
            return;
        }

        while (true) {
            var length = ele.className.length;
            ele.className = ele.className.replace(cssToRemove, replaceWith);

            if (length == ele.className.length)
                break;
        }

        while (true) {
            var length = ele.className.length;
            ele.className = ele.className.replace("  ", " ");

            if (length == ele.className.length)
                break;
        }
    },

     this.getRandomNumber = function(min, max) {
         return Math.floor(Math.random() * max) + min
     },

    this.distinct = function (list) {
        var result = [];

        for (var i = 0; i < list.length; i++) {
            if (i == 0) {
                result.push(list[i]);
                continue;
            }

            var willAdd = true;
            for (var j = 0; j < result.length; j++) {
                if (result[j] == list[i]) {
                    willAdd = false;
                    break;
                }
            }

            if (willAdd)
                result.push(list[i]);
        }

        return result;
    },

    this.replaceAll = function (string, replaceWhat, replaceWith) {
        var i = 0;
        while (string.length != i) {
            i = string.length;
            string = string.replace(replaceWhat, replaceWith);
        }
        return string;
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
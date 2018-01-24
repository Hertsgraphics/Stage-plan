
var dropDownList = new function () {



    this.add = function (ele, items, selectedBandMember) {

        if (!helpers.isNullOrEmpty(selectedBandMember)) {
            var op = document.createElement("option");
            op.value = selectedBandMember;
            op.text = selectedBandMember;
            ele.appendChild(op);
        }

        for (var i = 0; i < items.length; i++) {
            if (items[i] == selectedBandMember)
                continue;

            var op = document.createElement("option");
            op.value = items[i];
            op.text = items[i];
            ele.appendChild(op);
        }
    };

}
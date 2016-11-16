function replaceCssClass(ele, cssToRemove, replaceWith) {

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
}

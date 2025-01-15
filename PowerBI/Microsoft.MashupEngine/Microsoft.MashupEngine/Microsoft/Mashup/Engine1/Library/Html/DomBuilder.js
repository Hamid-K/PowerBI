// Copyright (c) Microsoft Corporation.  All rights reserved.

function getDataExplorerDom() {

    function writeInt32(x, output) {
        var digits = [];
        x = (x + 1) >>> 0
        while (x >= 10) {
            digits.push(x % 10);
            x /= 10;
            x = Math.floor(x);
        }
        digits.push(x);
        var aCharCode = "A".charCodeAt(0);
        var zeroCharCode = "0".charCodeAt(0);
        var numLen = digits.length;
        for (var i = 0; i < numLen - 1; i++) {
            output.value += String.fromCharCode(digits[numLen - i - 1] + zeroCharCode);
        }
        output.value += String.fromCharCode(digits[0] + aCharCode);
    }

    function writeString(str, output) {
        if (!str && str !== "") {
            writeInt32(0, output);
        } else {
            writeInt32(1, output);
            writeInt32(str.length, output);
            output.value += str;
        }
    }

    function visitAttributes(node, output) {

        function IsHidden(node) {
            return node.style && (node.style.display === "none" ||
                node.style.visibility === "hidden");
        }

        var attributes = [];
        switch (node.tagName) {
            case "SPAN":
            case "DIV":
                attributes = ["hidden", IsHidden(node)];
                break;
            case "TR":
                attributes = [
                    "bgColor", node.bgColor,
                    "hidden", IsHidden(node)];
                break;
            case "TD":
            case "TH":
                attributes = [
                    "colSpan", node.colSpan,
                    "rowSpan", node.rowSpan,
                    "hidden", IsHidden(node)];
                break;
            case "A":
                // If the link has a space and apostrophe it will return an unspecified exception. Returning an empty link instead.
                try {
                    if (node.href) {
                        attributes = [
                            "href", node.href,
                            "hidden", IsHidden(node)];
                    }
                } catch (e) {
                    attributes = [
                        "href", "",
                        "hidden", true];
                }
                break;
            case "IMG":
                if (node.alt) {
                    attributes = [
                        "alt", node.alt];
                }
                break;
        }

        switch (node.tagName) {
            case "DIV":
            case "DL":
            case "OL":
            case "TABLE":
            case "TR":
            case "UL":
                if (node.className) {
                    // Only pass one argument at a time, since some pages don't support multi-arg push calls.
                    attributes.push("class");
                    attributes.push(node.className);
                }
                if (node.id) {
                    attributes.push("id");
                    attributes.push(node.id);
                }
                break;
        }

        writeInt32(attributes.length / 2, output);
        for (var i = 0; i < attributes.length; i += 2) {
            writeString(attributes[i], output);
            var value = attributes[i + 1];
            switch (typeof value) {
                case "object":
                case "undefined":
                    writeInt32(0, output);
                    break;
                case "boolean":
                    writeInt32(1, output);
                    output.value += value ? '+' : '-';
                    break;
                case "number":
                    writeInt32(2, output);
                    writeInt32(value, output);
                    break;
                case "string":
                    writeInt32(3, output);
                    writeString(value === "" ? null : value, output);
                    break;
                default:
                    throw "InvalidOperationException";
            }
        }
    }

    function visitDom(root) {
        var TEXT_NODE_TYPE = 3;
        var ELEMENT_NODE_TYPE = 1;
        var SUCCESS_TOKEN = "0";
        var DATAEXPLORER_SCRIPT_ID = "dataExplorerScript";
        var output = { value: SUCCESS_TOKEN, count: 0 };
        var levels = [{ node: root, childIndex: 0, children: []}];
        while (levels.length > 0) {
            var level = levels[levels.length - 1];
            var node = level.node;
            if (level.childIndex >= node.childNodes.length) {
                levels.pop();
                var childNodes = level.children;
                var childNodesLength = childNodes.length;
                writeString(node.tagName, output);
                visitAttributes(node, output);
                writeInt32(childNodesLength, output);
                for (var i = 0; i < childNodesLength; i++) {
                    var childNode = childNodes[i];
                    writeInt32(childNode.nodeType, output);
                    if (childNode.nodeType === ELEMENT_NODE_TYPE) {
                        if (childNode.dataExplorerId == undefined) {
                            writeInt32(-1, output);
                        } else {
                            writeInt32(childNode.dataExplorerId, output);
                        }
                    } else {
                        writeString(childNode.nodeValue, output);
                    }
                }
                node.dataExplorerId = output.count++;
            } else {
                var child = node.childNodes[level.childIndex++];
                var isElementNode = child.nodeType === ELEMENT_NODE_TYPE && child.id !== DATAEXPLORER_SCRIPT_ID;
                if (isElementNode || child.nodeType === TEXT_NODE_TYPE) {
                    level.children.push(child);
                    if (isElementNode && !child.dataExplorerId) {
                        levels.push({ node: child, childIndex: 0, children: [] });
                    }
                }
            }
        }
        return output.value;
    }
    try {
        return visitDom(document.documentElement);
    } catch (e) {
        var FAILURE_TOKEN = "1";
        return FAILURE_TOKEN + e.message;
    }
}
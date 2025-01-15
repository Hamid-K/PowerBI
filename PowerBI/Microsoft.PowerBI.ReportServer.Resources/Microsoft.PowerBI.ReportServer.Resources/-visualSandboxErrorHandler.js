// Copyright (c) Microsoft Corporation.  All rights reserved.
/// <dictionary> plugin, erroring </dictionary>
var BI;
(function (BI) {
    var Shell;
    (function (Shell) {
        var InterOp;
        (function (InterOp) {
            var VisualSandboxErrorHandler;
            (function (VisualSandboxErrorHandler) {
                function initialize() {
                    setupErrorHandler(new WebView2Internals());
                    return Promise.resolve();
                }
                VisualSandboxErrorHandler.initialize = initialize;
                var WebView2Internals = /** @class */ (function () {
                    function WebView2Internals() {
                        //@ts-ignore
                        this.webview2 = window["chrome"].webview;
                        //@ts-ignore
                        delete window["chrome"];
                    }
                    WebView2Internals.prototype.onAlert = function (message, stack) {
                        this.webview2.postMessage({ objectId: "internals", method: "onAlert", args: [message, stack] });
                    };
                    WebView2Internals.prototype.onError = function (message, stack, additionalInfo, suppressErrorDialog) {
                        this.webview2.postMessage({ objectId: "internals", method: "onError", args: [message, stack, additionalInfo, suppressErrorDialog] });
                    };
                    return WebView2Internals;
                }());
                function setupErrorHandler(internals) {
                    function prepareToReport(error) {
                        if (error.stack) {
                            // Putting untrusted strings in a regular expression can can lead to regex DoS attacks (https://github.com/gkouziik/eslint-plugin-security-node/blob/master/docs/rules/non-literal-reg-expr.md)
                            // As a mitigation, we escape the untrusted string to ensure it's always treated as a literal and not a pattern
                            var regexEscapedMessage = error.message.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'); // https://developer.mozilla.org/en-US/docs/Web/JavaScript/Guide/Regular_Expressions#escaping
                            error.stack = error.stack.replace(new RegExp("^\\w*Error: ".concat(regexEscapedMessage)), '').trim();
                        }
                        return error;
                    }
                    function ensureError(obj, basicStack) {
                        if (obj instanceof Error) {
                            return obj;
                        }
                        var stringified = JSON.stringify(obj);
                        if (obj && obj.constructor && obj.constructor.name) {
                            stringified = "".concat(obj.constructor.name, " ").concat(stringified);
                        }
                        var error = new Error("ErrorHandler caught non-Error object: ".concat(stringified));
                        if (basicStack)
                            error.stack = basicStack;
                        return error;
                    }
                    window.alert = function (messageObject) {
                        var error = prepareToReport(ensureError(messageObject));
                        internals.onAlert(error.message, error.stack);
                    };
                    window.onerror = function (messageObject, source, lineNumber, columnNumber, errorObject) {
                        /// <disable>JS2043.RemoveDebugCode</disable>
                        var basicStack = "(at ".concat(source, ":line:").concat(lineNumber, ":").concat(columnNumber || "", ")");
                        var error = prepareToReport(ensureError(errorObject, basicStack));
                        var topFrame = {
                            lineNumber: lineNumber,
                            columnNumber: columnNumber,
                            fileName: "",
                            functionName: ""
                        };
                        try {
                            // Parse window location to set topFrame.source to the customer visual's plugin name
                            var location_1 = window.location;
                            var customVisualName = /plugin=([^&]+)/.exec(location_1.search);
                            if (customVisualName && customVisualName[0])
                                topFrame.fileName = location_1.origin + location_1.pathname + "?" + customVisualName;
                        }
                        catch (e) {
                            // Swallow our failure to parse the custom visual name
                        }
                        try {
                            // Parse the stack to determine an erroring function name. Expected formats:
                            // at HTMLDivElement.<anonymous> 
                            // at <anonymous>
                            // at functionName.methodName
                            var classDotFunctionName = / at (?:<anonymous>)?((?:\w)+[.]?(?:<anonymous>)?(?:\w)*) /.exec(error.stack);
                            if (classDotFunctionName && classDotFunctionName.length >= 2)
                                topFrame.functionName = classDotFunctionName[1];
                        }
                        catch (e) {
                            // Swallow our failure to parse the stack for more info
                        }
                        var additionalInfo = {
                            topFrame: topFrame,
                            type: (errorObject && errorObject.name) || "Error"
                        };
                        try {
                            internals.onError(error.message, error.stack, JSON.stringify(additionalInfo), true);
                        }
                        catch (e) {
                            internals.onAlert(error.message, error.stack);
                        }
                        return true;
                    };
                }
            })(VisualSandboxErrorHandler = InterOp.VisualSandboxErrorHandler || (InterOp.VisualSandboxErrorHandler = {}));
        })(InterOp = Shell.InterOp || (Shell.InterOp = {}));
    })(Shell = BI.Shell || (BI.Shell = {}));
})(BI || (BI = {}));

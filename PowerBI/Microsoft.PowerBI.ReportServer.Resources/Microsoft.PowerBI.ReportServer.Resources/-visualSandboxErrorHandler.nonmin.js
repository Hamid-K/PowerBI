var BI;

!function(BI) {
    !function(Shell) {
        !function(InterOp) {
            !function(VisualSandboxErrorHandler) {
                VisualSandboxErrorHandler.initialize = function() {
                    function prepareToReport(error) {
                        var regexEscapedMessage;
                        return error.stack && (regexEscapedMessage = error.message.replace(/[.*+?^${}()|[\]\\]/g, "\\$&"), 
                        error.stack = error.stack.replace(new RegExp("^\\w*Error: ".concat(regexEscapedMessage)), "").trim()), 
                        error;
                    }
                    function ensureError(obj, basicStack) {
                        if (obj instanceof Error) return obj;
                        var error = JSON.stringify(obj);
                        obj && obj.constructor && obj.constructor.name && (error = "".concat(obj.constructor.name, " ").concat(error));
                        error = new Error("ErrorHandler caught non-Error object: ".concat(error));
                        return basicStack && (error.stack = basicStack), error;
                    }
                    var internals;
                    return internals = new WebView2Internals(), window.alert = function(error) {
                        error = prepareToReport(ensureError(error));
                        internals.onAlert(error.message, error.stack);
                    }, window.onerror = function(messageObject, error, lineNumber, topFrame, additionalInfo) {
                        error = prepareToReport(ensureError(additionalInfo, "(at ".concat(error, ":line:").concat(lineNumber, ":").concat(topFrame || "", ")"))), 
                        topFrame = {
                            lineNumber: lineNumber,
                            columnNumber: topFrame,
                            fileName: "",
                            functionName: ""
                        };
                        try {
                            var location_1 = window.location, customVisualName = /plugin=([^&]+)/.exec(location_1.search);
                            customVisualName && customVisualName[0] && (topFrame.fileName = location_1.origin + location_1.pathname + "?" + customVisualName);
                        } catch (e) {}
                        try {
                            var classDotFunctionName = / at (?:<anonymous>)?((?:\w)+[.]?(?:<anonymous>)?(?:\w)*) /.exec(error.stack);
                            classDotFunctionName && 2 <= classDotFunctionName.length && (topFrame.functionName = classDotFunctionName[1]);
                        } catch (e) {}
                        additionalInfo = {
                            topFrame: topFrame,
                            type: additionalInfo && additionalInfo.name || "Error"
                        };
                        try {
                            internals.onError(error.message, error.stack, JSON.stringify(additionalInfo), !0);
                        } catch (e) {
                            internals.onAlert(error.message, error.stack);
                        }
                        return !0;
                    }, Promise.resolve();
                };
                var WebView2Internals = function() {
                    function WebView2Internals() {
                        this.webview2 = window.chrome.webview, delete window.chrome;
                    }
                    return WebView2Internals.prototype.onAlert = function(message, stack) {
                        this.webview2.postMessage({
                            objectId: "internals",
                            method: "onAlert",
                            args: [ message, stack ]
                        });
                    }, WebView2Internals.prototype.onError = function(message, stack, additionalInfo, suppressErrorDialog) {
                        this.webview2.postMessage({
                            objectId: "internals",
                            method: "onError",
                            args: [ message, stack, additionalInfo, suppressErrorDialog ]
                        });
                    }, WebView2Internals;
                }();
            }(InterOp.VisualSandboxErrorHandler || (InterOp.VisualSandboxErrorHandler = {}));
        }(Shell.InterOp || (Shell.InterOp = {}));
    }(BI.Shell || (BI.Shell = {}));
}(BI = BI || {});
"use strict";

(self.webpackChunkdesktop = self.webpackChunkdesktop || []).push([ [ "visual-datapoint-annotations-container.component" ], {
    33416: function(__unused_webpack_module, __webpack_exports__, __webpack_require__) {
        __webpack_require__.r(__webpack_exports__), __webpack_require__.d(__webpack_exports__, {
            VisualDatapointAnnotationsContainerComponent: function() {
                return VisualDatapointAnnotationsContainerComponent;
            }
        });
        var _TickScheduler, _AnimationFrameTickScheduler, _RenderScheduler, _LetDirective, _LetModule, _PushPipe, _PushModule, core = __webpack_require__(50423), common = __webpack_require__(77476), visual_annotations_service = __webpack_require__(56797), item_picker_service = __webpack_require__(4877), src = __webpack_require__(32238), color = __webpack_require__(64067), colorUtility = __webpack_require__(1521), VisualDatapointAnnotationsService = function() {
            function VisualDatapointAnnotationsService() {
                this.visualAnnotationsService = (0, core.inject)(visual_annotations_service.u);
            }
            return VisualDatapointAnnotationsService.prototype.menuClicked = function(data) {
                this.visualAnnotationsService.setAnnotationsRequest(data.visualName, [ {
                    text: "",
                    lastEditUTCTimestamp: Date.now(),
                    key: data.key
                } ]);
            }, VisualDatapointAnnotationsService.\u0275prov = core["\u0275\u0275defineInjectable"]({
                token: VisualDatapointAnnotationsService,
                factory: VisualDatapointAnnotationsService.\u0275fac = function(t) {
                    return new (t || VisualDatapointAnnotationsService);
                }
            }), VisualDatapointAnnotationsService;
        }(), tslib_es6 = __webpack_require__(81337), a11y = __webpack_require__(51193), drag_drop = __webpack_require__(558), Subject = __webpack_require__(30794), combineLatest = __webpack_require__(59017), map = __webpack_require__(14172), startWith = __webpack_require__(22318), createClass = __webpack_require__(90341), esm_extends = __webpack_require__(84229), inheritsLoose = __webpack_require__(37419), isObservable = __webpack_require__(38860), from = __webpack_require__(82762), Observable = __webpack_require__(58417), ReplaySubject = __webpack_require__(33554), pipe = __webpack_require__(41497), Subscription = __webpack_require__(34763), distinctUntilChanged = __webpack_require__(77526), tap = __webpack_require__(17653), switchMap = __webpack_require__(81905), TickScheduler = function() {};
        (_TickScheduler = TickScheduler).\u0275fac = function(t) {
            return new (t || _TickScheduler);
        }, _TickScheduler.\u0275prov = core["\u0275\u0275defineInjectable"]({
            token: _TickScheduler,
            factory: function() {
                return (0, core.inject)(core.NgZone) instanceof core.NgZone ? new NoopTickScheduler : (0, 
                core.inject)(AnimationFrameTickScheduler);
            },
            providedIn: "root"
        });
        var AnimationFrameTickScheduler = function(_TickScheduler2) {
            function AnimationFrameTickScheduler(appRef) {
                var _this;
                return (_this = _TickScheduler2.call(this) || this).appRef = appRef, _this.isScheduled = !1, 
                _this;
            }
            return (0, inheritsLoose.Z)(AnimationFrameTickScheduler, _TickScheduler2), AnimationFrameTickScheduler.prototype.schedule = function() {
                var _this2 = this;
                this.isScheduled || (this.isScheduled = !0, requestAnimationFrame(function() {
                    _this2.appRef.tick(), _this2.isScheduled = !1;
                }));
            }, AnimationFrameTickScheduler;
        }(TickScheduler);
        (_AnimationFrameTickScheduler = AnimationFrameTickScheduler).\u0275fac = function(t) {
            return new (t || _AnimationFrameTickScheduler)(core["\u0275\u0275inject"](core.ApplicationRef));
        }, _AnimationFrameTickScheduler.\u0275prov = core["\u0275\u0275defineInjectable"]({
            token: _AnimationFrameTickScheduler,
            factory: _AnimationFrameTickScheduler.\u0275fac,
            providedIn: "root"
        });
        var NoopTickScheduler = function(_TickScheduler3) {
            function NoopTickScheduler() {
                return _TickScheduler3.apply(this, arguments) || this;
            }
            return (0, inheritsLoose.Z)(NoopTickScheduler, _TickScheduler3), NoopTickScheduler.prototype.schedule = function() {}, 
            NoopTickScheduler;
        }(TickScheduler), RenderScheduler = function() {
            function RenderScheduler(cdRef, tickScheduler) {
                this.cdRef = cdRef, this.tickScheduler = tickScheduler;
            }
            return RenderScheduler.prototype.schedule = function() {
                this.cdRef.markForCheck(), this.tickScheduler.schedule();
            }, RenderScheduler;
        }();
        function createRenderEventManager(handlers) {
            var handleRenderEvent = function(handlers) {
                return function(event) {
                    var _handlers$event$type;
                    return null == (_handlers$event$type = handlers[event.type]) ? void 0 : _handlers$event$type.call(handlers, event);
                };
            }(handlers), potentialObservable$ = new ReplaySubject.t(1);
            return {
                nextPotentialObservable: function(potentialObservable) {
                    potentialObservable$.next(potentialObservable);
                },
                handlePotentialObservableChanges: function() {
                    return potentialObservable$.pipe((0, distinctUntilChanged.x)(), (0, pipe.z)((0, 
                    switchMap.w)(function(potentialObservable) {
                        var observable$ = function(potentialObservable) {
                            return (0, isObservable.b)(potentialObservable) ? potentialObservable : function(value) {
                                return !!value && "object" == typeof value && !Array.isArray(value);
                            }(value = potentialObservable) && Object.keys(value).length > 0 && Object.values(value).every(isObservable.b) ? (0, 
                            combineLatest.aj)((obsDictionary = potentialObservable, Object.keys(obsDictionary).reduce(function(acc, key) {
                                var _extends2;
                                return (0, esm_extends.Z)({}, acc, ((_extends2 = {})[key] = obsDictionary[key].pipe((0, 
                                distinctUntilChanged.x)()), _extends2));
                            }, {}))) : function(value) {
                                return "function" == typeof (null == value ? void 0 : value.then);
                            }(potentialObservable) ? (0, from.D)(potentialObservable) : new Observable.y(function(subscriber) {
                                subscriber.next(potentialObservable);
                            });
                            var obsDictionary, value;
                        }(potentialObservable), reset = !0, synchronous = !0;
                        return new Observable.y(function(subscriber) {
                            var subscription = (0, core.untracked)(function() {
                                return observable$.subscribe({
                                    next: function(value) {
                                        subscriber.next({
                                            type: "next",
                                            value,
                                            reset,
                                            synchronous
                                        }), reset = !1;
                                    },
                                    error: function(_error) {
                                        subscriber.next({
                                            type: "error",
                                            error: _error,
                                            reset,
                                            synchronous
                                        }), reset = !1;
                                    },
                                    complete: function() {
                                        subscriber.next({
                                            type: "complete",
                                            reset,
                                            synchronous
                                        }), reset = !1;
                                    }
                                });
                            });
                            return reset && (subscriber.next({
                                type: "suspense",
                                reset,
                                synchronous: !0
                            }), reset = !1), synchronous = !1, subscription;
                        });
                    })), (0, distinctUntilChanged.x)(renderEventComparator), (0, tap.b)(handleRenderEvent));
                }
            };
        }
        function renderEventComparator(previous, current) {
            return previous.type === current.type && previous.reset === current.reset && ("next" === current.type ? previous.value === current.value : "error" !== current.type || previous.error === current.error);
        }
        (_RenderScheduler = RenderScheduler).\u0275fac = function(t) {
            return new (t || _RenderScheduler)(core["\u0275\u0275inject"](core.ChangeDetectorRef), core["\u0275\u0275inject"](TickScheduler));
        }, _RenderScheduler.\u0275prov = core["\u0275\u0275defineInjectable"]({
            token: _RenderScheduler,
            factory: _RenderScheduler.\u0275fac
        });
        var LetDirective = function() {
            function LetDirective(mainTemplateRef, viewContainerRef, errorHandler, renderScheduler) {
                var _this3 = this;
                this.mainTemplateRef = mainTemplateRef, this.viewContainerRef = viewContainerRef, 
                this.errorHandler = errorHandler, this.renderScheduler = renderScheduler, this.isMainViewCreated = !1, 
                this.isSuspenseViewCreated = !1, this.viewContext = {
                    $implicit: void 0,
                    ngrxLet: void 0,
                    error: void 0,
                    complete: !1
                }, this.renderEventManager = createRenderEventManager({
                    suspense: function() {
                        _this3.viewContext.$implicit = void 0, _this3.viewContext.ngrxLet = void 0, _this3.viewContext.error = void 0, 
                        _this3.viewContext.complete = !1, _this3.renderSuspenseView();
                    },
                    next: function(event) {
                        _this3.viewContext.$implicit = event.value, _this3.viewContext.ngrxLet = event.value, 
                        event.reset && (_this3.viewContext.error = void 0, _this3.viewContext.complete = !1), 
                        _this3.renderMainView(event.synchronous);
                    },
                    error: function(event) {
                        _this3.viewContext.error = event.error, event.reset && (_this3.viewContext.$implicit = void 0, 
                        _this3.viewContext.ngrxLet = void 0, _this3.viewContext.complete = !1), _this3.renderMainView(event.synchronous), 
                        _this3.errorHandler.handleError(event.error);
                    },
                    complete: function(event) {
                        _this3.viewContext.complete = !0, event.reset && (_this3.viewContext.$implicit = void 0, 
                        _this3.viewContext.ngrxLet = void 0, _this3.viewContext.error = void 0), _this3.renderMainView(event.synchronous);
                    }
                }), this.subscription = new Subscription.w;
            }
            LetDirective.ngTemplateContextGuard = function(dir, ctx) {
                return !0;
            };
            var _proto4 = LetDirective.prototype;
            return _proto4.ngOnInit = function() {
                this.subscription.add(this.renderEventManager.handlePotentialObservableChanges().subscribe());
            }, _proto4.ngOnDestroy = function() {
                this.subscription.unsubscribe();
            }, _proto4.renderMainView = function(isSyncEvent) {
                this.isSuspenseViewCreated && (this.isSuspenseViewCreated = !1, this.viewContainerRef.clear()), 
                this.isMainViewCreated || (this.isMainViewCreated = !0, this.viewContainerRef.createEmbeddedView(this.mainTemplateRef, this.viewContext)), 
                isSyncEvent || this.renderScheduler.schedule();
            }, _proto4.renderSuspenseView = function() {
                this.isMainViewCreated && (this.isMainViewCreated = !1, this.viewContainerRef.clear()), 
                this.suspenseTemplateRef && !this.isSuspenseViewCreated && (this.isSuspenseViewCreated = !0, 
                this.viewContainerRef.createEmbeddedView(this.suspenseTemplateRef));
            }, (0, createClass.Z)(LetDirective, [ {
                key: "ngrxLet",
                set: function(potentialObservable) {
                    this.renderEventManager.nextPotentialObservable(potentialObservable);
                }
            } ]), LetDirective;
        }();
        (_LetDirective = LetDirective).\u0275fac = function(t) {
            return new (t || _LetDirective)(core["\u0275\u0275directiveInject"](core.TemplateRef), core["\u0275\u0275directiveInject"](core.ViewContainerRef), core["\u0275\u0275directiveInject"](core.ErrorHandler), core["\u0275\u0275directiveInject"](RenderScheduler));
        }, _LetDirective.\u0275dir = core["\u0275\u0275defineDirective"]({
            type: _LetDirective,
            selectors: [ [ "", "ngrxLet", "" ] ],
            inputs: {
                ngrxLet: "ngrxLet",
                suspenseTemplateRef: [ "ngrxLetSuspenseTpl", "suspenseTemplateRef" ]
            },
            standalone: !0,
            features: [ core["\u0275\u0275ProvidersFeature"]([ RenderScheduler ]) ]
        }), (_LetModule = function() {}).\u0275fac = function(t) {
            return new (t || _LetModule);
        }, _LetModule.\u0275mod = core["\u0275\u0275defineNgModule"]({
            type: _LetModule
        }), _LetModule.\u0275inj = core["\u0275\u0275defineInjector"]({}), (_PushPipe = function() {
            function PushPipe(errorHandler) {
                var _this4 = this;
                this.errorHandler = errorHandler, this.renderScheduler = new RenderScheduler((0, 
                core.inject)(core.ChangeDetectorRef), (0, core.inject)(TickScheduler)), this.renderEventManager = createRenderEventManager({
                    suspense: function(event) {
                        return _this4.setRenderedValue(void 0, event.synchronous);
                    },
                    next: function(event) {
                        return _this4.setRenderedValue(event.value, event.synchronous);
                    },
                    error: function(event) {
                        event.reset && _this4.setRenderedValue(void 0, event.synchronous), _this4.errorHandler.handleError(event.error);
                    },
                    complete: function(event) {
                        event.reset && _this4.setRenderedValue(void 0, event.synchronous);
                    }
                }), this.subscription = this.renderEventManager.handlePotentialObservableChanges().subscribe();
            }
            var _proto5 = PushPipe.prototype;
            return _proto5.transform = function(potentialObservable) {
                return this.renderEventManager.nextPotentialObservable(potentialObservable), this.renderedValue;
            }, _proto5.ngOnDestroy = function() {
                this.subscription.unsubscribe();
            }, _proto5.setRenderedValue = function(value, isSyncEvent) {
                value !== this.renderedValue && (this.renderedValue = value, isSyncEvent || this.renderScheduler.schedule());
            }, PushPipe;
        }()).\u0275fac = function(t) {
            return new (t || _PushPipe)(core["\u0275\u0275directiveInject"](core.ErrorHandler, 16));
        }, _PushPipe.\u0275pipe = core["\u0275\u0275definePipe"]({
            name: "ngrxPush",
            type: _PushPipe,
            pure: !1,
            standalone: !0
        }), (_PushModule = function() {}).\u0275fac = function(t) {
            return new (t || _PushModule);
        }, _PushModule.\u0275mod = core["\u0275\u0275defineNgModule"]({
            type: _PushModule
        }), _PushModule.\u0275inj = core["\u0275\u0275defineInjector"]({});
        var rx_component = __webpack_require__(73221), BehaviorSubject = __webpack_require__(35510), tri_svg_icon = __webpack_require__(17177), tri_menu = __webpack_require__(98759), localization_module = __webpack_require__(79061), localization_service = __webpack_require__(65181), tri_svg_icon_component = __webpack_require__(74913), tri_menu_directive = __webpack_require__(76688), localize_pipe = __webpack_require__(85102), _c0 = [ "annotationTextarea" ];
        function DatapointAnnotationContentComponent_ng_container_0_Template(rf, ctx) {
            if (1 & rf) {
                var _r4 = core["\u0275\u0275getCurrentView"]();
                core["\u0275\u0275elementContainerStart"](0), core["\u0275\u0275elementStart"](1, "div", 2), 
                core["\u0275\u0275listener"]("dblclick", function() {
                    core["\u0275\u0275restoreView"](_r4);
                    var ctx_r3 = core["\u0275\u0275nextContext"]();
                    return core["\u0275\u0275resetView"](ctx_r3.startEdit());
                }), core["\u0275\u0275text"](2), core["\u0275\u0275elementEnd"](), core["\u0275\u0275elementStart"](3, "div", 3), 
                core["\u0275\u0275listener"]("dblclick", function() {
                    core["\u0275\u0275restoreView"](_r4);
                    var ctx_r5 = core["\u0275\u0275nextContext"]();
                    return core["\u0275\u0275resetView"](ctx_r5.startEdit());
                }), core["\u0275\u0275text"](4), core["\u0275\u0275pipe"](5, "date"), core["\u0275\u0275elementEnd"](), 
                core["\u0275\u0275elementStart"](6, "button", 4), core["\u0275\u0275listener"]("triMenuButtonClick", function($event) {
                    core["\u0275\u0275restoreView"](_r4);
                    var ctx_r6 = core["\u0275\u0275nextContext"]();
                    return core["\u0275\u0275resetView"](ctx_r6.onMenuItemClicked($event));
                }), core["\u0275\u0275element"](7, "tri-svg-icon", 5), core["\u0275\u0275elementEnd"](), 
                core["\u0275\u0275elementContainerEnd"]();
            }
            if (2 & rf) {
                var ctx_r0 = core["\u0275\u0275nextContext"]();
                core["\u0275\u0275advance"](2), core["\u0275\u0275textInterpolate1"](" ", null == ctx_r0.annotation ? null : ctx_r0.annotation.text, " "), 
                core["\u0275\u0275advance"](2), core["\u0275\u0275textInterpolate1"](" ", core["\u0275\u0275pipeBind2"](5, 4, null == ctx_r0.annotation ? null : ctx_r0.annotation.lastEditUTCTimestamp, "short"), " "), 
                core["\u0275\u0275advance"](2), core["\u0275\u0275property"]("triMenu", ctx_r0.menu), 
                core["\u0275\u0275advance"](1), core["\u0275\u0275property"]("name", "more_horizontal_16_regular");
            }
        }
        function DatapointAnnotationContentComponent_ng_template_2_Template(rf, ctx) {
            if (1 & rf) {
                var _r9 = core["\u0275\u0275getCurrentView"]();
                core["\u0275\u0275elementStart"](0, "div", 6)(1, "textarea", 7, 8), core["\u0275\u0275listener"]("focusout", function() {
                    core["\u0275\u0275restoreView"](_r9);
                    var ctx_r8 = core["\u0275\u0275nextContext"]();
                    return core["\u0275\u0275resetView"](ctx_r8.onEditCompleted());
                }), core["\u0275\u0275pipe"](3, "localize"), core["\u0275\u0275text"](4), core["\u0275\u0275elementEnd"]()();
            }
            if (2 & rf) {
                var ctx_r2 = core["\u0275\u0275nextContext"]();
                core["\u0275\u0275advance"](1), core["\u0275\u0275property"]("placeholder", core["\u0275\u0275pipeBind1"](3, 2, "Annotation_placeholder")), 
                core["\u0275\u0275advance"](3), core["\u0275\u0275textInterpolate"](ctx_r2.annotation.text);
            }
        }
        var DatapointAnnotationContentComponent = function() {
            function DatapointAnnotationContentComponent(localizationService) {
                this.localizationService = localizationService, this.outputAnnotation = new core.EventEmitter, 
                this.onDeleted = new core.EventEmitter, this.isEditMode$ = new BehaviorSubject.X(!1), 
                this.menu = {
                    items: [ {
                        id: "edit",
                        type: tri_menu.fz.Button,
                        text: this.localizationService.get("Edit"),
                        styles: {
                            border: "none"
                        }
                    }, {
                        id: "delete",
                        type: tri_menu.fz.Button,
                        text: this.localizationService.get("Delete"),
                        styles: {
                            border: "none"
                        }
                    } ]
                };
            }
            return DatapointAnnotationContentComponent.prototype.ngOnInit = function() {
                var _a;
                "" === (null === (_a = this.annotation) || void 0 === _a ? void 0 : _a.text) && this.isEditMode$.next(!0);
            }, DatapointAnnotationContentComponent.prototype.ngAfterViewInit = function() {
                var _this = this;
                this.isEditMode$.subscribe(function(isEditMode) {
                    isEditMode && setTimeout(function() {
                        var _a;
                        null === (_a = _this.annotationTextareaRef) || void 0 === _a || _a.nativeElement.focus();
                    });
                });
            }, DatapointAnnotationContentComponent.prototype.onMenuItemClicked = function(event) {
                switch (event.id) {
                  case "edit":
                    this.isEditMode$.next(!0);
                    break;

                  case "delete":
                    this.onDeleted.emit();
                }
            }, DatapointAnnotationContentComponent.prototype.startEdit = function() {
                this.isEditMode$.next(!0);
            }, DatapointAnnotationContentComponent.prototype.onEditCompleted = function() {
                this.annotation.text = this.annotationTextareaRef.nativeElement.value, "" !== this.annotation.text ? (this.annotation.lastEditUTCTimestamp = Date.now(), 
                this.outputAnnotation.emit(this.annotation), this.isEditMode$.next(!1)) : this.onDeleted.emit();
            }, DatapointAnnotationContentComponent.\u0275fac = function(t) {
                return new (t || DatapointAnnotationContentComponent)(core["\u0275\u0275directiveInject"](localization_service.o));
            }, DatapointAnnotationContentComponent.\u0275cmp = core["\u0275\u0275defineComponent"]({
                type: DatapointAnnotationContentComponent,
                selectors: [ [ "datapoint-annotation-content" ] ],
                viewQuery: function(rf, ctx) {
                    var _t;
                    1 & rf && core["\u0275\u0275viewQuery"](_c0, 5), 2 & rf && core["\u0275\u0275queryRefresh"](_t = core["\u0275\u0275loadQuery"]()) && (ctx.annotationTextareaRef = _t.first);
                },
                inputs: {
                    annotation: "annotation"
                },
                outputs: {
                    outputAnnotation: "outputAnnotation",
                    onDeleted: "onDeleted"
                },
                standalone: !0,
                features: [ core["\u0275\u0275StandaloneFeature"] ],
                decls: 4,
                vars: 4,
                consts: [ [ 4, "ngIf", "ngIfElse" ], [ "edit", "" ], [ 1, "content", 3, "dblclick" ], [ 1, "last-updated", 3, "dblclick" ], [ "tri-button", "", "appearance", "subtle", 1, "icon-button", "menu-button", 3, "triMenu", "triMenuButtonClick" ], [ 3, "name" ], [ 1, "edit" ], [ 1, "content", 3, "placeholder", "focusout" ], [ "annotationTextarea", "" ] ],
                template: function(rf, ctx) {
                    if (1 & rf && (core["\u0275\u0275template"](0, DatapointAnnotationContentComponent_ng_container_0_Template, 8, 7, "ng-container", 0), 
                    core["\u0275\u0275pipe"](1, "async"), core["\u0275\u0275template"](2, DatapointAnnotationContentComponent_ng_template_2_Template, 5, 4, "ng-template", null, 1, core["\u0275\u0275templateRefExtractor"])), 
                    2 & rf) {
                        var _r1 = core["\u0275\u0275reference"](3);
                        core["\u0275\u0275property"]("ngIf", !core["\u0275\u0275pipeBind1"](1, 2, ctx.isEditMode$))("ngIfElse", _r1);
                    }
                },
                dependencies: [ common.CommonModule, common.NgIf, common.AsyncPipe, common.DatePipe, a11y.rt, tri_svg_icon.T6, tri_svg_icon_component.M, tri_menu.up, tri_menu_directive.rX, localization_module.v, localize_pipe.F ],
                styles: [ "[_nghost-%COMP%]{background-color:var(--annotation-background-color);border-radius:2px}.content[_ngcontent-%COMP%]{font-size:12px;line-height:12px;padding:10px 16px 20px 10px}textarea.content[_ngcontent-%COMP%]{width:calc(100% - 26px);height:100%;background:none;border:none;outline:none}.last-updated[_ngcontent-%COMP%]{font-size:8px;color:var(--globalColorGrey38);position:absolute;inset-block-end:3px;inset-inline-start:10px;display:none}button[tri-button].menu-button[_ngcontent-%COMP%]{position:absolute;inset-block-start:5px;inset-inline-end:5px;display:none;height:13px;padding:1px;background:none}button[tri-button].menu-button[_ngcontent-%COMP%]:hover{background-color:var(--gray-88)}button[tri-button].menu-button[_ngcontent-%COMP%]   tri-svg-icon[_ngcontent-%COMP%]{height:13px;width:13px;transform:translateY(-6px)}[_nghost-%COMP%]:hover   .last-updated[_ngcontent-%COMP%], [_nghost-%COMP%]:has(.menu-button.tri-menu-expanded)   .last-updated[_ngcontent-%COMP%], [_nghost-%COMP%]:hover   .menu-button[_ngcontent-%COMP%], [_nghost-%COMP%]:has(.menu-button.tri-menu-expanded)   .menu-button[_ngcontent-%COMP%]{display:block}" ],
                changeDetection: 0
            }), DatapointAnnotationContentComponent;
        }();
        function DatapointAnnotationComponent_ng_container_0_Template(rf, ctx) {
            if (1 & rf) {
                var _r3 = core["\u0275\u0275getCurrentView"]();
                core["\u0275\u0275elementContainerStart"](0), core["\u0275\u0275namespaceSVG"](), 
                core["\u0275\u0275elementStart"](1, "svg"), core["\u0275\u0275element"](2, "line")(3, "circle", 1), 
                core["\u0275\u0275elementEnd"](), core["\u0275\u0275namespaceHTML"](), core["\u0275\u0275elementStart"](4, "div", 2), 
                core["\u0275\u0275listener"]("cdkDragEnded", function($event) {
                    core["\u0275\u0275restoreView"](_r3);
                    var ctx_r2 = core["\u0275\u0275nextContext"]();
                    return core["\u0275\u0275resetView"](ctx_r2.onDragEnd($event));
                })("cdkDragMoved", function($event) {
                    core["\u0275\u0275restoreView"](_r3);
                    var ctx_r4 = core["\u0275\u0275nextContext"]();
                    return core["\u0275\u0275resetView"](ctx_r4.onDragMove($event));
                }), core["\u0275\u0275pipe"](5, "async"), core["\u0275\u0275elementStart"](6, "datapoint-annotation-content", 3), 
                core["\u0275\u0275listener"]("onDeleted", function() {
                    core["\u0275\u0275restoreView"](_r3);
                    var ctx_r5 = core["\u0275\u0275nextContext"]();
                    return core["\u0275\u0275resetView"](ctx_r5.onDeleted.emit());
                }), core["\u0275\u0275elementEnd"]()(), core["\u0275\u0275elementContainerEnd"]();
            }
            if (2 & rf) {
                var vm_r1 = ctx.ngrxLet, ctx_r0 = core["\u0275\u0275nextContext"]();
                core["\u0275\u0275advance"](2), core["\u0275\u0275attribute"]("x1", vm_r1.lineX1)("x2", vm_r1.lineX2)("y1", vm_r1.lineY1)("y2", vm_r1.lineY2), 
                core["\u0275\u0275advance"](1), core["\u0275\u0275attribute"]("cx", ctx_r0.annotation.x)("cy", ctx_r0.annotation.y), 
                core["\u0275\u0275advance"](1), core["\u0275\u0275styleProp"]("width", vm_r1.width, "px")("height", vm_r1.height, "px"), 
                core["\u0275\u0275property"]("cdkDragFreeDragPosition", core["\u0275\u0275pipeBind1"](5, 13, ctx_r0.startPosition$))("cdkDragBoundary", ctx_r0.dragBoundary), 
                core["\u0275\u0275advance"](2), core["\u0275\u0275property"]("annotation", ctx_r0.annotation);
            }
        }
        var DatapointAnnotationComponent = function(_super) {
            function DatapointAnnotationComponent() {
                var _this = null !== _super && _super.apply(this, arguments) || this;
                return _this.outputAnnotation = new core.EventEmitter, _this.onDeleted = new core.EventEmitter, 
                _this.dragMove = new Subject.xQ, _this.defaultOffset = {
                    offsetX: 50,
                    offsetY: -50
                }, _this;
            }
            return (0, tslib_es6.__extends)(DatapointAnnotationComponent, _super), DatapointAnnotationComponent.prototype.ngOnInit = function() {
                var _this = this;
                this.startPosition$ = this.changes$("annotation").pipe((0, map.U)(function(annotation) {
                    var _a, _b;
                    return {
                        x: annotation.x + (null !== (_a = annotation.offsetX) && void 0 !== _a ? _a : _this.defaultOffset.offsetX),
                        y: annotation.y + (null !== (_b = annotation.offsetY) && void 0 !== _b ? _b : _this.defaultOffset.offsetY)
                    };
                })), this.vm$ = (0, combineLatest.aj)([ this.changes$("annotation"), this.dragMove.pipe((0, 
                startWith.O)(null)) ]).pipe((0, map.U)(function(_a) {
                    var _b, _c, offset, annotation = _a[0], event = _a[1];
                    if (event) {
                        var dragPosition = event.source.getFreeDragPosition();
                        offset = {
                            x: dragPosition.x - annotation.x,
                            y: dragPosition.y - annotation.y
                        };
                    } else offset = {
                        x: null !== (_b = annotation.offsetX) && void 0 !== _b ? _b : _this.defaultOffset.offsetX,
                        y: null !== (_c = annotation.offsetY) && void 0 !== _c ? _c : _this.defaultOffset.offsetY
                    };
                    var vm = {
                        x: annotation.x + offset.x,
                        y: annotation.y + offset.y,
                        width: annotation.width || 160,
                        height: annotation.height || 70,
                        lineX1: annotation.x,
                        lineY1: annotation.y,
                        lineX2: 0,
                        lineY2: 0
                    }, boxCenterAngle = Math.atan2(offset.x + vm.width / 2, offset.y + vm.height / 2);
                    return boxCenterAngle > .25 * Math.PI && boxCenterAngle <= .75 * Math.PI ? (vm.lineY2 = vm.y + vm.height / 2, 
                    vm.lineX2 = vm.x) : boxCenterAngle > -.25 * Math.PI && boxCenterAngle <= .25 * Math.PI ? (vm.lineY2 = vm.y, 
                    vm.lineX2 = vm.x + vm.width / 2) : boxCenterAngle > -.75 * Math.PI && boxCenterAngle <= -.25 * Math.PI ? (vm.lineY2 = vm.y + vm.height / 2, 
                    vm.lineX2 = vm.x + vm.width) : (vm.lineY2 = vm.y + vm.height, vm.lineX2 = vm.x + vm.width / 2), 
                    vm;
                }));
            }, DatapointAnnotationComponent.prototype.onDragMove = function(event) {
                this.dragMove.next(event);
            }, DatapointAnnotationComponent.prototype.onDragEnd = function(event) {
                var dragPosition = event.source.getFreeDragPosition();
                this.outputAnnotation.emit((0, tslib_es6.__assign)((0, tslib_es6.__assign)({}, this.annotation), {
                    offsetX: dragPosition.x - this.annotation.x,
                    offsetY: dragPosition.y - this.annotation.y
                }));
            }, DatapointAnnotationComponent.\u0275fac = function() {
                var \u0275DatapointAnnotationComponent_BaseFactory;
                return function(t) {
                    return (\u0275DatapointAnnotationComponent_BaseFactory || (\u0275DatapointAnnotationComponent_BaseFactory = core["\u0275\u0275getInheritedFactory"](DatapointAnnotationComponent)))(t || DatapointAnnotationComponent);
                };
            }(), DatapointAnnotationComponent.\u0275cmp = core["\u0275\u0275defineComponent"]({
                type: DatapointAnnotationComponent,
                selectors: [ [ "datapoint-annotation" ] ],
                inputs: {
                    annotation: "annotation",
                    dragBoundary: "dragBoundary"
                },
                outputs: {
                    outputAnnotation: "outputAnnotation",
                    onDeleted: "onDeleted"
                },
                standalone: !0,
                features: [ core["\u0275\u0275InheritDefinitionFeature"], core["\u0275\u0275StandaloneFeature"] ],
                decls: 1,
                vars: 1,
                consts: [ [ 4, "ngrxLet" ], [ "r", "1.5" ], [ "cdkDrag", "", 1, "annotation-container", 3, "cdkDragFreeDragPosition", "cdkDragBoundary", "cdkDragEnded", "cdkDragMoved" ], [ 3, "annotation", "onDeleted" ] ],
                template: function(rf, ctx) {
                    1 & rf && core["\u0275\u0275template"](0, DatapointAnnotationComponent_ng_container_0_Template, 7, 15, "ng-container", 0), 
                    2 & rf && core["\u0275\u0275property"]("ngrxLet", ctx.vm$);
                },
                dependencies: [ common.CommonModule, common.AsyncPipe, a11y.rt, drag_drop.Zt, LetDirective, DatapointAnnotationContentComponent ],
                styles: [ ".annotation-container[_ngcontent-%COMP%]{position:fixed;inset-block-start:0;inset-inline-start:0;pointer-events:auto;color:var(--annotation-text-color)}.annotation-container.cdk-drag-dragging[_ngcontent-%COMP%]{cursor:move}.annotation-container[_ngcontent-%COMP%]   datapoint-annotation-content[_ngcontent-%COMP%]{display:block;width:100%;height:100%}svg[_ngcontent-%COMP%]{position:fixed;top:0;right:0;bottom:0;left:0;width:100%;height:100%}svg[_ngcontent-%COMP%]   line[_ngcontent-%COMP%]{stroke:var(--annotation-stroke-color);stroke-width:1px}svg[_ngcontent-%COMP%]   circle[_ngcontent-%COMP%]{stroke:var(--annotation-stroke-color);fill:var(--annotation-stroke-color)}" ],
                changeDetection: 0
            }), DatapointAnnotationComponent;
        }(rx_component.w);
        function VisualDatapointAnnotationsContainerComponent_datapoint_annotation_0_Template(rf, ctx) {
            if (1 & rf) {
                var _r3 = core["\u0275\u0275getCurrentView"]();
                core["\u0275\u0275elementStart"](0, "datapoint-annotation", 1), core["\u0275\u0275listener"]("onDeleted", function() {
                    var annotation_r1 = core["\u0275\u0275restoreView"](_r3).$implicit, ctx_r2 = core["\u0275\u0275nextContext"]();
                    return core["\u0275\u0275resetView"](ctx_r2.onAnnotationDeleted(annotation_r1));
                }), core["\u0275\u0275elementEnd"]();
            }
            if (2 & rf) {
                var annotation_r1 = ctx.$implicit, ctx_r0 = core["\u0275\u0275nextContext"]();
                core["\u0275\u0275styleProp"]("--annotation-stroke-color", ctx_r0.annotationStyle.strokeColor)("--annotation-background-color", ctx_r0.annotationStyle.backgroundColor)("--annotation-text-color", ctx_r0.annotationStyle.textColor), 
                core["\u0275\u0275property"]("annotation", annotation_r1);
            }
        }
        var VisualDatapointAnnotationsContainerComponent = function() {
            function VisualDatapointAnnotationsContainerComponent() {
                var _this = this;
                this.visualDatapointAnnotationsService = (0, core.inject)(VisualDatapointAnnotationsService), 
                this.visualAnnotationsService = (0, core.inject)(visual_annotations_service.u), 
                this.annotationStyle = {
                    backgroundColor: color.s1.white,
                    strokeColor: "#212121",
                    textColor: color.s1.black
                };
                var itemPickerService = (0, core.inject)(item_picker_service.a);
                (0, core.inject)(src.Ve).initializeTheme().then(function() {
                    var theme = itemPickerService.getThemes();
                    _this.annotationStyle.backgroundColor = theme[2].children[0].value, _this.annotationStyle.strokeColor = theme[2].children[4].value, 
                    _this.annotationStyle.textColor = _this.isColorLight(_this.annotationStyle.backgroundColor) ? color.s1.black : color.s1.white;
                });
            }
            return VisualDatapointAnnotationsContainerComponent.prototype.ngOnInit = function() {
                this.annotationsResponse$ = this.visualAnnotationsService.getAnnotationsResponse$(this.visualName), 
                this.visualAnnotationsService.annotationsHidden$.subscribe(function(hiddenEvent) {});
            }, VisualDatapointAnnotationsContainerComponent.prototype.menuClicked = function(data) {
                this.visualDatapointAnnotationsService.menuClicked(data);
            }, VisualDatapointAnnotationsContainerComponent.prototype.isColorLight = function(color) {
                return (0, colorUtility.um)(color) > .5;
            }, VisualDatapointAnnotationsContainerComponent.prototype.onAnnotationDeleted = function(annotation) {
                this.visualAnnotationsService.deleteAnnotation(this.visualName, annotation);
            }, VisualDatapointAnnotationsContainerComponent.\u0275fac = function(t) {
                return new (t || VisualDatapointAnnotationsContainerComponent);
            }, VisualDatapointAnnotationsContainerComponent.\u0275cmp = core["\u0275\u0275defineComponent"]({
                type: VisualDatapointAnnotationsContainerComponent,
                selectors: [ [ "visual-datapoint-annotations-container" ] ],
                inputs: {
                    visualName: "visualName"
                },
                standalone: !0,
                features: [ core["\u0275\u0275ProvidersFeature"]([ VisualDatapointAnnotationsService, {
                    provide: src.Ve,
                    useFactory: function() {
                        return new src.Ve("powerbi");
                    }
                } ]), core["\u0275\u0275StandaloneFeature"] ],
                decls: 2,
                vars: 3,
                consts: [ [ "dragBoundary", "visual-datapoint-annotations-container", 3, "annotation", "--annotation-stroke-color", "--annotation-background-color", "--annotation-text-color", "onDeleted", 4, "ngFor", "ngForOf" ], [ "dragBoundary", "visual-datapoint-annotations-container", 3, "annotation", "onDeleted" ] ],
                template: function(rf, ctx) {
                    1 & rf && (core["\u0275\u0275template"](0, VisualDatapointAnnotationsContainerComponent_datapoint_annotation_0_Template, 1, 7, "datapoint-annotation", 0), 
                    core["\u0275\u0275pipe"](1, "async")), 2 & rf && core["\u0275\u0275property"]("ngForOf", core["\u0275\u0275pipeBind1"](1, 1, ctx.annotationsResponse$));
                },
                dependencies: [ common.CommonModule, common.NgForOf, common.AsyncPipe, DatapointAnnotationComponent ],
                styles: [ "[_nghost-%COMP%]{position:absolute;top:0;right:0;bottom:0;left:0;pointer-events:none}" ],
                changeDetection: 0
            }), VisualDatapointAnnotationsContainerComponent;
        }();
    }
} ]);
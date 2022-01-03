module.exports =
/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};

/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {

/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId])
/******/ 			return installedModules[moduleId].exports;

/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			exports: {},
/******/ 			id: moduleId,
/******/ 			loaded: false
/******/ 		};

/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);

/******/ 		// Flag the module as loaded
/******/ 		module.loaded = true;

/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}


/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;

/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;

/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";

/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(0);
/******/ })
/************************************************************************/
/******/ ({

/***/ 0:
/***/ (function(module, exports, __webpack_require__) {

	module.exports = __webpack_require__(1101);


/***/ }),

/***/ 3:
/***/ (function(module, exports) {

	module.exports = function() { throw new Error("define cannot be used indirect"); };


/***/ }),

/***/ 1050:
/***/ (function(module, exports) {

	module.exports = require("./kendo.core");

/***/ }),

/***/ 1051:
/***/ (function(module, exports) {

	module.exports = require("./kendo.popup");

/***/ }),

/***/ 1091:
/***/ (function(module, exports) {

	module.exports = require("./kendo.slider");

/***/ }),

/***/ 1092:
/***/ (function(module, exports) {

	module.exports = require("./kendo.textbox");

/***/ }),

/***/ 1101:
/***/ (function(module, exports, __webpack_require__) {

	var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_ARRAY__, __WEBPACK_AMD_DEFINE_RESULT__;(function(f, define){
	    !(__WEBPACK_AMD_DEFINE_ARRAY__ = [
	        __webpack_require__(1050),
	        __webpack_require__(1103),
	        __webpack_require__(1051),
	        __webpack_require__(1091),
	        __webpack_require__(1104),
	        __webpack_require__(1102),
	        __webpack_require__(1105),
	        __webpack_require__(1092),
	        __webpack_require__(1106),

	        __webpack_require__(1107),
	        __webpack_require__(1108)
	    ], __WEBPACK_AMD_DEFINE_FACTORY__ = (f), __WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ? (__WEBPACK_AMD_DEFINE_FACTORY__.apply(exports, __WEBPACK_AMD_DEFINE_ARRAY__)) : __WEBPACK_AMD_DEFINE_FACTORY__), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));
	})(function(){

	var __meta__ = { // jshint ignore:line
	    id: "colorpicker",
	    name: "Color tools",
	    category: "web",
	    description: "Color selection widgets",
	    depends: [ "core", "color", "popup", "slider", "userevents", "button", "binder", "textbox", "numerictextbox" ]
	};

	(function($, undefined){
	    // WARNING: removing the following jshint declaration and turning
	    // == into === to make JSHint happy will break functionality.
	    /*jshint eqnull:true  */
	    var kendo = window.kendo,
	        ui = kendo.ui,
	        Widget = ui.Widget,
	        Color = kendo.Color,
	        parseColor = kendo.parseColor,
	        KEYS = kendo.keys,
	        BACKGROUNDCOLOR = "background-color",
	        WHITE = "#ffffff",
	        MESSAGES = {
	            apply  : "Apply",
	            cancel : "Cancel",
	            noColor: "no color",
	            clearColor: "Clear color",
	            previewInput: null,
	            contrastRatio: "Contrast ratio:",
	            fail: "Fail",
	            pass: "Pass",
	            hex: "HEX",
	            toggleFormat: "Toggle format",
	            red: "Red",
	            green: "Green",
	            blue: "Blue",
	            alpha: "Alpha",
	            gradient: "Gradient view",
	            palette: "Palette view"
	        },
	        NS = ".kendoColorTools",
	        CLICK_NS = "click" + NS,
	        KEYDOWN_NS = "keydown" + NS,
	        ColorSelector = ui.colorpicker.ColorSelector,
	        FlatColorPicker = ui.FlatColorPicker;

	    /* -----[ The ColorPicker widget ]----- */

	    var ColorPicker = Widget.extend({
	        init: function(element, options) {
	            var that = this;

	            // Legacy support for the cases where only palette is defined
	            if(options && options.palette && !options.view){
	                options.view ="palette";
	            }

	            Widget.fn.init.call(that, element, options);
	            options = that.options = kendo.deepExtend({}, that.options, options);
	            element = that.element;

	            var value = element.attr("value") || element.val();
	            if (value) {
	                value = parseColor(value, true);
	            } else {
	                value = parseColor(options.value, true);
	            }
	            that._value = options.value = value;

	            var content = that.wrapper = $(that._template(options));
	            element.hide().after(content);

	            that._inputWrapper = $(that.wrapper[0].firstChild);

	            if (element.is("input")) {
	                element.appendTo(content);

	                // if there exists a <label> associated with this
	                // input field, we must catch clicks on it to prevent
	                // the built-in color picker from showing up.
	                // https://github.com/telerik/kendo-ui-core/issues/292

	                var label = element.closest("label");
	                var id = element.attr("id");
	                if (id) {
	                    label = label.add('label[for="' + id + '"]');
	                }
	                label.on("click", function(ev){
	                    that.open();
	                    ev.preventDefault();
	                });
	            }

	            that._tabIndex = element.attr("tabIndex") || 0;

	            that.enable(!element.attr("disabled"));

	            var accesskey = element.attr("accesskey");
	            if (accesskey) {
	                element.attr("accesskey", null);
	                content.attr("accesskey", accesskey);
	            }

	            that.bind("activate", function(ev){
	                if (!ev.isDefaultPrevented()) {
	                    that.toggle();
	                }
	            });

	            that._updateUI(value);
	        },
	        destroy: function() {
	            this.wrapper.off(NS).find("*").off(NS);
	            if (this._popup) {
	                this._selector.destroy();
	                this._popup.destroy();
	            }
	            this._selector = this._popup = this.wrapper = null;
	            Widget.fn.destroy.call(this);
	        },
	        enable: function(enable) {
	            var that = this,
	                wrapper = that.wrapper,
	                innerWrapper = wrapper.children(".k-picker-wrap"),
	                arrow = innerWrapper.find(".k-select");

	            if (arguments.length === 0) {
	                enable = true;
	            }

	            that.element.attr("disabled", !enable);
	            wrapper.attr("aria-disabled", !enable);

	            arrow.off(NS).on("mousedown" + NS, preventDefault);

	            wrapper.addClass("k-state-disabled")
	                .removeAttr("tabIndex")
	                .add("*", wrapper).off(NS);

	            if (enable) {
	                wrapper.removeClass("k-state-disabled")
	                    .attr("tabIndex", that._tabIndex)
	                    .on("mouseenter" + NS, function () { innerWrapper.addClass("k-state-hover"); })
	                    .on("mouseleave" + NS, function () { innerWrapper.removeClass("k-state-hover"); })
	                    .on("focus" + NS, function () { innerWrapper.addClass("k-state-focused"); })
	                    .on("blur" + NS, function () { innerWrapper.removeClass("k-state-focused"); })
	                    .on(KEYDOWN_NS, bind(that._keydown, that))
	                    .on(CLICK_NS, ".k-select", bind(that.toggle, that))
	                    .on(CLICK_NS, that.options.toolIcon ? ".k-tool-icon" : ".k-selected-color", function () {
	                        that.trigger("activate");
	                    });
	            } else {
	                that.close();
	            }
	        },

	        _template: kendo.template(
	            '<div role="textbox" aria-haspopup="true" class="k-colorpicker">' +
	                '<span  class="k-picker-wrap">' +
	                    '# if (toolIcon) { #' +
	                        '<span class="k-icon k-tool-icon #= toolIcon #">' +
	                            '<span class="k-selected-color"></span>' +
	                        '</span>' +
	                    '# } else { #' +
	                        '<span class="k-selected-color"></span>' +
	                    '# } #' +
	                    '<span role="button" class="k-select" unselectable="on" aria-label="select">' +
	                        '<span class="k-icon k-i-arrow-s"></span>' +
	                    '</span>' +
	                '</span >' +
	            '</div>'
	        ),

	        options: {
	            name: "ColorPicker",
	            closeOnSelect: false,
	            contrastTool: false,
	            palette: null,
	            columns: 10,
	            toolIcon: null,
	            value: null,
	            messages: MESSAGES,
	            opacity: false,
	            buttons: true,
	            preview: true,
	            clearButton: false,
	            input      : true,
	            format: "hex",
	            formats: ["rgb", "hex"],
	            view: "gradient",
	            views: ["gradient", "palette"],
	            backgroundColor: null,
	            ARIATemplate: 'Current selected color is #=data || ""#'
	        },

	        events: [ "activate", "change", "select", "open", "close" ],

	        open: function () {
	            if (!this.element.prop("disabled")) {
	                this._getPopup().open();
	            }
	        },
	        close: function () {
	            var selOptions = (this._selector && this._selector.options) || {};
	            selOptions._closing = true;
	            this._getPopup().close();

	            delete selOptions._closing;
	        },
	        toggle: function () {
	            if (!this.element.prop("disabled")) {
	                this._getPopup().toggle();
	            }
	        },
	        setBackgroundColor: function (color) {
	            var that = this,
	                handler = function () { that._selector.setBackgroundColor(color); };

	            that.options.contrastTool.backgroundColor = color;

	            if(that._selector && (that._popup && that._popup.visible())) {
	                that._selector.setBackgroundColor(color);
	            } else if (that._popup) {
	                that._popup.unbind("activate", handler);
	                that._popup.bind("activate", handler);
	            }
	        },
	        _noColorIcon: function(){
	            return this.wrapper.find(".k-picker-wrap > .k-selected-color");
	        },
	        color: ColorSelector.fn.color,
	        value: ColorSelector.fn.value,
	        _select: ColorSelector.fn._select,
	        _triggerSelect: ColorSelector.fn._triggerSelect,
	        _isInputTypeColor: function() {
	            var el = this.element[0];
	            return (/^input$/i).test(el.tagName) && (/^color$/i).test(el.type);
	        },

	        _updateUI: function(value, dontChangeSelector) {
	            var formattedValue = "";

	            if (value) {
	                if (this._isInputTypeColor() || value.a == 1) {
	                    // seems that input type="color" doesn't support opacity
	                    // in colors; the only accepted format is hex #RRGGBB
	                    formattedValue = value.toCss();
	                } else {
	                    formattedValue = value.toCssRgba();
	                }

	                this.element.val(formattedValue);
	            }

	            if (!this._ariaTemplate) {
	                this._ariaTemplate = kendo.template(this.options.ARIATemplate);
	            }

	            this.wrapper.attr("aria-label", this._ariaTemplate(formattedValue));

	            this._triggerSelect(value);
	            this.wrapper.find(".k-selected-color").css(
	                BACKGROUNDCOLOR,
	                value ? value.toDisplay() : WHITE
	            );

	            this._noColorIcon().toggleClass("k-no-color", !formattedValue);

	            if (this._selector && !dontChangeSelector) {
	                this._selector.value(value);
	            }
	        },
	        _keydown: function(ev) {
	            var key = ev.keyCode;
	            if (this._getPopup().visible()) {
	                if (key == KEYS.ESC) {
	                    this._selector._cancel();
	                } else {
	                    this._selector._keydown(ev);
	                }
	                preventDefault(ev);
	            }
	            else if (key == KEYS.ENTER || key == KEYS.DOWN) {
	                this.open();
	                preventDefault(ev);
	            }
	        },
	        _getPopup: function() {
	            var that = this, popup = that._popup;

	            if (!popup) {
	                var options = that.options;
	                var selectorType;

	                selectorType = FlatColorPicker;

	                options.autoupdate = options.buttons !== true;
	                delete options.select;
	                delete options.change;
	                delete options.cancel;

	                var id = kendo.guid();

	                var selectorWrapper = $('<div id="' + id +'" class="k-colorpicker-popup"></div>').appendTo(document.body);
	                var selector = that._selector = new selectorType($('<div></div>').appendTo(selectorWrapper), options);

	                that.wrapper.attr("aria-owns", id);

	                that._popup = popup = selectorWrapper.kendoPopup({
	                    anchor: that.wrapper,
	                    adjustSize: { width: 5, height: 0 }
	                }).data("kendoPopup");

	                selector.bind({
	                    select: function(ev){
	                        that._updateUI(parseColor(ev.value), true);
	                    },
	                    change: function(ev){
	                        if (that.options.buttons) {
	                            that._select(selector.color());
	                        } else {
	                            that._updateUI(parseColor(ev.value), true);
	                        }

	                        if (that.options.buttons || (that._selector.options.view === "palette" && that.options.closeOnSelect)) {
	                            that.close();
	                        }
	                    },
	                    cancel: function() {
	                        that.close();
	                    }
	                });
	                popup.bind({
	                    close: function(ev){
	                        if (that.trigger("close")) {
	                            ev.preventDefault();
	                            return;
	                        }
	                        that.wrapper.children(".k-picker-wrap").removeClass("k-state-focused");

	                        var color = selector.color();

	                        if (!that.options.buttons) {
	                            that._select(color);
	                        } else {
	                            that._select(that.color());
	                        }

	                        color = that.color();

	                        if (color && color.h) {
	                            that._cachedHue = color.h;
	                        }

	                        setTimeout(function(){
	                            if (that.wrapper && !that.wrapper.is("[unselectable='on']")) {
	                                that.wrapper.trigger("focus");
	                            }
	                        },0);
	                    },
	                    open: function(ev) {
	                        if (that.trigger("open")) {
	                            ev.preventDefault();
	                        } else {
	                            that.wrapper.children(".k-picker-wrap").addClass("k-state-focused");
	                        }
	                    },
	                    activate: function(){
	                        var hsvColor,
	                            selectedColor = that.color();

	                        if (selectedColor) {
	                            selectedColor = selectedColor.toHSV();
	                            hsvColor = Color.fromHSV(that._cachedHue || 0, selectedColor.s, selectedColor.v, selectedColor.a);
	                            selectedColor = selectedColor.equals(hsvColor) ? hsvColor : selectedColor;
	                        }

	                        selector.value(selectedColor);
	                        selector.focus();
	                        that.wrapper.children(".k-picker-wrap").addClass("k-state-focused");
	                    }
	                });
	            }
	            return popup;
	        }
	    });

	    function preventDefault(ev) { ev.preventDefault(); }

	    function bind(callback, obj) {
	        return function() {
	            return callback.apply(obj, arguments);
	        };
	    }

	    ui.plugin(ColorPicker);

	})(window.kendo.jQuery);

	return window.kendo;

	}, __webpack_require__(3));


/***/ }),

/***/ 1102:
/***/ (function(module, exports) {

	module.exports = require("./kendo.button");

/***/ }),

/***/ 1103:
/***/ (function(module, exports) {

	module.exports = require("./kendo.color");

/***/ }),

/***/ 1104:
/***/ (function(module, exports) {

	module.exports = require("./kendo.userevents");

/***/ }),

/***/ 1105:
/***/ (function(module, exports) {

	module.exports = require("./kendo.binder");

/***/ }),

/***/ 1106:
/***/ (function(module, exports) {

	module.exports = require("./kendo.numerictextbox");

/***/ }),

/***/ 1107:
/***/ (function(module, exports) {

	module.exports = require("./colorpicker/colorselector");

/***/ }),

/***/ 1108:
/***/ (function(module, exports) {

	module.exports = require("./colorpicker/flatcolorpicker");

/***/ })

/******/ });
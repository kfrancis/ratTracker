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

	module.exports = __webpack_require__(1085);


/***/ }),

/***/ 3:
/***/ (function(module, exports) {

	module.exports = function() { throw new Error("define cannot be used indirect"); };


/***/ }),

/***/ 1050:
/***/ (function(module, exports) {

	module.exports = require("./kendo.core");

/***/ }),

/***/ 1085:
/***/ (function(module, exports, __webpack_require__) {

	var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_ARRAY__, __WEBPACK_AMD_DEFINE_RESULT__;(function (f, define) {
	    !(__WEBPACK_AMD_DEFINE_ARRAY__ = [__webpack_require__(1050), __webpack_require__(1086)], __WEBPACK_AMD_DEFINE_FACTORY__ = (f), __WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ? (__WEBPACK_AMD_DEFINE_FACTORY__.apply(exports, __WEBPACK_AMD_DEFINE_ARRAY__)) : __WEBPACK_AMD_DEFINE_FACTORY__), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));
	})(function () {

	    var __meta__ = { // jshint ignore:line
	        id: "button",
	        name: "Button",
	        category: "web",
	        description: "The Button widget displays styled buttons.",
	        depends: ["core", "badge"]
	    };

	    (function ($, undefined) {
	        var kendo = window.kendo,
	            Widget = kendo.ui.Widget,
	            ui = kendo.ui,
	            proxy = $.proxy,
	            keys = kendo.keys,
	            CLICK = "click",
	            MOUSEDOWN = kendo.support.mousedown,
	            MOUSEUP = kendo.support.mouseup,
	            MOUSEOUT = "mouseout",
	            KBUTTON = "k-button",
	            KBUTTONICON = "k-button-icon",
	            KBUTTONICONTEXT = "k-button-icontext",
	            NS = ".kendoButton",
	            DISABLED = "disabled",
	            DISABLEDSTATE = "k-state-disabled",
	            FOCUSEDSTATE = "k-state-focused",
	            SELECTEDSTATE = "k-state-active";

	        var BUTTON_DEFAULTS = {
	            icon: "",
	            iconClass: "",
	            spriteCssClass: "",
	            imageUrl: "",
	            badge: null
	        };
	        kendo.setDefaults("button", BUTTON_DEFAULTS);

	        var Button = Widget.extend({
	            init: function (element, options) {
	                var that = this;

	                Widget.fn.init.call(that, element, options);

	                element = that.wrapper = that.element;
	                options = that.options;

	                element.addClass(KBUTTON).attr("role", "button");

	                options.enable = options.enable && options.enabled && !element.attr(DISABLED);
	                that.enable(options.enable);

	                if (options.enable) {
	                    that._tabindex();
	                }

	                that._badge();

	                that.iconElement();

	                element
	                    .on(CLICK + NS, proxy(that._click, that))
	                    .on("focus" + NS, proxy(that._focus, that))
	                    .on("blur" + NS, proxy(that._blur, that))
	                    .on("keydown" + NS, proxy(that._keydown, that))
	                    .on("keyup" + NS, proxy(that._removeActive, that))
	                    .on(MOUSEDOWN + NS, proxy(that._addActive, that))
	                    .on(MOUSEUP + NS  + " " + MOUSEOUT + NS, proxy(that._removeActive, that));

	                kendo.notify(that);
	            },

	            destroy: function() {
	                var that = this;

	                that.wrapper.off(NS);

	                if (that.badge){
	                    that.badge.destroy();
	                }

	                Widget.fn.destroy.call(that);
	            },

	            events: [
	                CLICK
	            ],

	            options: {
	                name: "Button",
	                enable: true,
	                enabled: true,
	                icon: "",
	                iconClass: "",
	                spriteCssClass: "",
	                imageUrl: "",
	                badge: null
	            },

	            _isNativeButton: function() {
	                return this.element.prop("tagName").toLowerCase() == "button";
	            },

	            _click: function(e) {
	                if (this.options.enable) {
	                    if (this.trigger(CLICK, { event: e })) {
	                        e.preventDefault();
	                    }
	                }
	            },

	            _focus: function() {
	                if (this.options.enable) {
	                    this.element.addClass(FOCUSEDSTATE);
	                }
	            },

	            _blur: function() {
	                var that = this;
	                that.element.removeClass(FOCUSEDSTATE);
	                setTimeout(function() {
	                    that.element.removeClass(SELECTEDSTATE);
	                });
	            },

	            _keydown: function(e) {
	                var that = this;
	                if (e.keyCode == keys.ENTER || e.keyCode == keys.SPACEBAR) {
	                    that._addActive();

	                    if (!that._isNativeButton()) {
	                        if (e.keyCode == keys.SPACEBAR) {
	                            e.preventDefault();
	                        }
	                        that._click(e);
	                    }
	                }
	            },

	            _removeActive: function() {
	                this.element.removeClass(SELECTEDSTATE);
	            },

	            _addActive: function() {
	                if (this.options.enable) {
	                    this.element.addClass(SELECTEDSTATE);
	                }
	            },

	            iconElement: function() {
	                var that = this,
	                    element = that.element,
	                    options = that.options,
	                    icon = options.icon,
	                    iconClass = options.iconClass,
	                    spriteCssClass = options.spriteCssClass,
	                    imageUrl = options.imageUrl,
	                    span, img, isEmpty;

	                if (spriteCssClass || imageUrl || icon || iconClass) {
	                    isEmpty = true;

	                    element.contents().filter(function() {
	                        return (!$(this).hasClass("k-sprite") && !$(this).hasClass("k-icon") && !$(this).hasClass("k-image"));
	                    }).each(function(idx, el){
	                        if (el.nodeType == 1 || el.nodeType == 3 && kendo.trim(el.nodeValue).length > 0) {
	                            isEmpty = false;
	                        }
	                    });

	                    if (isEmpty) {
	                        element.addClass(KBUTTONICON);
	                    } else {
	                        element.addClass(KBUTTONICONTEXT);
	                    }
	                }

	                if (imageUrl) {
	                    img = element.children("img.k-image").first();
	                    if (!img[0]) {
	                        img = $('<img alt="icon" class="k-image" />').prependTo(element);
	                    }
	                    img.attr("src", imageUrl);
	                } else if (icon || iconClass) {
	                    span = element.children("span.k-icon").first();
	                    if (!span[0]) {
	                        span = $('<span></span>').prependTo(element);
	                    }
	                    span.attr("class", icon ? "k-icon k-i-" + icon : iconClass);
	                } else if (spriteCssClass) {
	                    span = element.children("span.k-sprite").first();
	                    if (!span[0]) {
	                        span = $('<span class="k-sprite"></span>').prependTo(element);
	                    }
	                    span.addClass(spriteCssClass);
	                }
	            },

	            enable: function(enable) {
	                var that = this,
	                    element = that.element;

	                if (enable === undefined) {
	                    enable = true;
	                }

	                enable = !!enable;
	                that.options.enable = enable;
	                element.toggleClass(DISABLEDSTATE, !enable)
	                    .attr("aria-disabled", !enable)
	                    .attr(DISABLED, !enable);

	                if (enable) {
	                    that._tabindex();
	                }

	                // prevent 'Unspecified error' in IE when inside iframe
	                try {
	                    element.trigger("blur");
	                } catch (err) {}
	            },

	            _badge: function() {
	                var that = this;
	                var badgeOptions = that.options.badge;
	                var badgeEelement;

	                if (badgeOptions === null || badgeOptions === undefined) {
	                    return;
	                }

	                if (badgeOptions.constructor !== Object) {
	                    badgeOptions = { text: badgeOptions };
	                }

	                if (badgeOptions.position === undefined || badgeOptions.position === "") {
	                    badgeOptions.position = "edge";

	                    if (badgeOptions.align === undefined || badgeOptions.align === "") {
	                        badgeOptions.align = "top end";
	                    }
	                }

	                badgeOptions._classNames = ["k-button-badge"];

	                that.element.addClass("k-badge-container");

	                badgeEelement = $('<span />').appendTo(that.element);
	                that.badge = new ui.Badge(badgeEelement, badgeOptions);
	            }
	        });

	        if (Button.fn.hasOwnProperty("defaults") === false) {
	            Object.defineProperty(Button.fn, "defaults", {
	                get: function() {
	                    return kendo.defaults.button;
	                }
	            });
	        }

	        kendo.ui.plugin(Button);

	    })(window.kendo.jQuery);

	    return window.kendo;

	}, __webpack_require__(3));


/***/ }),

/***/ 1086:
/***/ (function(module, exports) {

	module.exports = require("./kendo.badge");

/***/ })

/******/ });
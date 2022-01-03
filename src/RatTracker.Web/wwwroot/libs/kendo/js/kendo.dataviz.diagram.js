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

	module.exports = __webpack_require__(1148);


/***/ }),

/***/ 3:
/***/ (function(module, exports) {

	module.exports = function() { throw new Error("define cannot be used indirect"); };


/***/ }),

/***/ 1056:
/***/ (function(module, exports) {

	module.exports = require("./kendo.drawing");

/***/ }),

/***/ 1069:
/***/ (function(module, exports) {

	module.exports = require("./kendo.data");

/***/ }),

/***/ 1079:
/***/ (function(module, exports) {

	module.exports = require("./kendo.mobile.scroller");

/***/ }),

/***/ 1104:
/***/ (function(module, exports) {

	module.exports = require("./kendo.userevents");

/***/ }),

/***/ 1128:
/***/ (function(module, exports) {

	module.exports = require("./kendo.draganddrop");

/***/ }),

/***/ 1148:
/***/ (function(module, exports, __webpack_require__) {

	var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_ARRAY__, __WEBPACK_AMD_DEFINE_RESULT__;(function(f, define){
	    !(__WEBPACK_AMD_DEFINE_ARRAY__ = [
	        __webpack_require__(1069), __webpack_require__(1128), __webpack_require__(1104), __webpack_require__(1079),
	        __webpack_require__(1056),

	       __webpack_require__(1149),
	       __webpack_require__(1150),
	       __webpack_require__(1151),
	       __webpack_require__(1152),
	       __webpack_require__(1153),
	       __webpack_require__(1154)
	    ], __WEBPACK_AMD_DEFINE_FACTORY__ = (f), __WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ? (__WEBPACK_AMD_DEFINE_FACTORY__.apply(exports, __WEBPACK_AMD_DEFINE_ARRAY__)) : __WEBPACK_AMD_DEFINE_FACTORY__), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));
	})(function(){

	    var __meta__ = { // jshint ignore:line
	        id: "dataviz.diagram",
	        name: "Diagram",
	        category: "dataviz",
	        description: "The Kendo DataViz Diagram ",
	        depends: [ "data", "userevents", "mobile.scroller", "draganddrop", "drawing", "dataviz.core", "dataviz.themes", "toolbar" ],
	        features: [{
	            id: "dataviz.diagram-pdf-export",
	            name: "PDF export",
	            description: "Export Diagram as PDF",
	            depends: [ "pdf" ]
	        },{
	            id: "dataviz.diagram-editing",
	            name: "Editing",
	            description: "Support for model editing",
	            depends: [ "editable", "window", "dropdownlist" ]
	        }]
	    };

	    return window.kendo;

	}, __webpack_require__(3));


/***/ }),

/***/ 1149:
/***/ (function(module, exports) {

	module.exports = require("./dataviz/diagram/utils");

/***/ }),

/***/ 1150:
/***/ (function(module, exports) {

	module.exports = require("./dataviz/diagram/math");

/***/ }),

/***/ 1151:
/***/ (function(module, exports) {

	module.exports = require("./dataviz/diagram/svg");

/***/ }),

/***/ 1152:
/***/ (function(module, exports) {

	module.exports = require("./dataviz/diagram/services");

/***/ }),

/***/ 1153:
/***/ (function(module, exports) {

	module.exports = require("./dataviz/diagram/layout");

/***/ }),

/***/ 1154:
/***/ (function(module, exports) {

	module.exports = require("./dataviz/diagram/dom");

/***/ })

/******/ });
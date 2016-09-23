/**
 * AutoScroll for jQuery
 * By: Jeremy Pyne
 * https://github.com/pynej/jquery.autoScroll.js
 * v1.1
 */

/**
 * Usage:
 *
 * // using default options
 * $("#element").autoScroll();
 *
 * // using some custom options
 * $("#element").autoScroll({
 *   paddingTop: 10,
 *   paddingBottom: 30
 * });
 *
 * Configuration options:
 *
 * paddingTop    integer  padding relative to the top of the viewport.  (Top will be start position or viewport top + padding.)
 * paddingBottom integer  padding relative to the bottom of the viewport. (Top will max out at item outer height + current top + padding bottem to prevent page from growing as you scroll.)
 * animate       boolean  animate transitions of assign directly.  (Disable if poor preformance.)
 * scrollMode	padding|margin Will this item be scrolled by adding padding or margins.  May be affected by surrounding layout elements.
 */

(function($) {
  $.fn.autoScroll = function(options) {

    // Default options.
    var defaults = {
      paddingTop: 0,
      paddingBottom: 0,
      animate: true,
      scrollMode: "margin"
    };

    var options = $.extend(defaults, options);
    
    // Setup each element seperatly.
    return this.each(function() {
      // Store this element.
      var $scrollingDiv = $(this);
      // Store the original position offset.
      var offset = $scrollingDiv.offset();
		
      // When the window is scrolled.
      $(window).scroll(function() {
		  
          console.log("scroll");
		  var scrollBy;
		  switch(options.scrollMode) {
				  
			  case "padding":
				  // Check that the new position wouldn't make the bottom of the item + the bottom padding go past the height of the parent element.  We dont want it to grow automaticaly.
        		  if (Math.max($(window).scrollTop() - offset.top + options.paddingTop, 0) + $scrollingDiv.outerHeight() - parseInt($scrollingDiv.css("padding-top")) + options.paddingBottom > $scrollingDiv.parent().outerHeight()) return;
				
				  scrollBy = {"paddingTop": (Math.max($(window).scrollTop() - offset.top + options.paddingTop, 0)) + "px"};
				  break;
			  case "margin":
			  default:
				  // Check that the new position wouldn't make the bottom of the item + the bottom padding go past the height of the parent element.  We dont want it to grow automaticaly.
        		  if (Math.max($(window).scrollTop() - offset.top + options.paddingTop, 0) + $scrollingDiv.outerHeight() + options.paddingBottom > $scrollingDiv.parent().outerHeight()) return;
		
				  scrollBy = {"marginTop": (Math.max($(window).scrollTop() - offset.top + options.paddingTop, 0)) + "px"};
				  break;  
		  }
        if (options.animate) {
          // Smoothly scroll the element to the new height.
          $scrollingDiv.stop().animate(scrollBy, "fast");
        } else {
          // Imediatly move the element to the new position without animation.  (Better preformance.)
          $scrollingDiv.css(scrollBy);
        }
      }).scroll();
    });
  };
})(jQuery);

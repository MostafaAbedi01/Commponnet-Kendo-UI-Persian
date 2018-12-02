// <![CDATA[
$(function() {

  // Slider
  $('#coin-slider').coinslider({width:960,height:220,opacity:1});

  // Radius Box
  $('.menu_nav ul li a, .content .mainbar img.fl, p.infopost a.com, .content p.pages span, .content p.pages a').css({"border-radius":"6px", "-moz-border-radius":"6px", "-webkit-border-radius":"6px"});
  $('.content .sidebar .gadget, .fbg_resize').css({"border-radius":"12px", "-moz-border-radius":"12px", "-webkit-border-radius":"12px"});
  //$('.content p.pages span, .content p.pages a').css({"border-radius":"16px", "-moz-border-radius":"16px", "-webkit-border-radius":"16px"});
  $('.content .sidebar h2').css({"border-top-left-radius":"12px", "border-top-right-radius":"12px", "-moz-border-radius-topleft":"12px", "-moz-border-radius-topright":"12px", "-webkit-border-top-left-radius":"12px", "-webkit-border-top-right-radius":"12px"});

});	

// Cufon
//Cufon.replace('h1, h2, h3, h4, h5, h6, .menu_nav ul li a', { hover: true });
//Cufon.replace('h1', { color: '-linear-gradient(#fff, #ffaf02)'});
//Cufon.replace('h1 small', { color: '#8a98a5'});

// ]]>
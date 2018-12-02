/*
Limit and Count TextArea characters using jQuery
By: Bryian Tan (bryian.tan at ysatech.com)
05/20/2010 - v01.00.00
02/16/2013 - v02.00.00

Description:
Support more than one TextArea objects on the page.
 
Limit/Count number of characters in TextArea/TextBox control on 
- copy-paste events using mouse
- copy-paste events using keyboard keys
- cut event using mouse
- keyup event

Usage:
$("[id$='TextBox1']").limit_textarea({ maxLength: 100, displayText: ' Letters left.' });
$("[id$='TextBox2']").limit_textarea({ maxLength: 200, displayTextPosition: 'bottomleft' });
$("[id$='TextBox3']").limit_textarea({ displayTextPosition: 'bottomright' });
$("[id$='TextBox4']").limit_textarea();
$("[id$='TextBox5']").limit_textarea({ maxLength: 125 });
*/
(function ($) {

    var limit_textarea = new function () {

        this.createLabel = function (txtArea, options) {

            var txtAreaID = txtArea.attr("id");
            var labelID = txtAreaID + "_lbl";

            //textarea position
            var txtLeftPos = $("[id='" + txtAreaID + "']").position().left;
            var txtTopPos = $("[id='" + txtAreaID + "']").position().top;

            var displayLabel = $('<span id=' + labelID + '>' + options.displayText + '</span>')
            .css({ position: 'absolute', height: 15, padding: 2,
                'font-size': '75%', display: 'inline-block'
            });

            //attach the label to text area
            txtArea.after(displayLabel);

            //position
            switch (options.displayTextPosition) {
                case 'topRight':
                    displayLabel.css({ top: txtTopPos - 18, left: txtArea.width() - $("[id='" + labelID + "']").width() - 5 });
                    break;
                case 'bottomLeft':
                    displayLabel.css({ top: txtTopPos + txtArea.height() + 5, left: txtLeftPos });
                    break;
                case 'bottomRight':
                    displayLabel.css({ top: txtTopPos + txtArea.height() + 5, left: txtArea.width() - $("[id='" + labelID + "']").width() - 5 });
                    break;
                default:
                    displayLabel.css({ top: txtTopPos - 18, left: txtLeftPos });
                    break;
            }

            //initialize
            limit_textarea.validateLimit(txtArea, options);

            return true;
        }

        //remove paragraph break from the textarea
        this.trimEnter = function (dataStr) {
            return dataStr.replace(/(\r\n|\r|\n)/g, "");
        }

        this.validateLimit = function (txtArea, options) {

            var txtValue = txtArea.val();
            var txtAreaID = txtArea.attr("id");
            var labelID = txtAreaID + "_lbl";

            //get the paragraph break count
            var lineBreakMatches = txtValue.match(/(\r\n|\r|\n)/g);
            var lineBreakCount = lineBreakMatches ? lineBreakMatches.length : 0;

            //remaining character left
            var remaningChar = options.maxLength - limit_textarea.trimEnter(txtValue).length;

            if ($("#" + labelID).length) {
                $("#" + labelID).html(remaningChar + options.displayText);

                if (Number(remaningChar) <= Number(0)) {

                    txtArea.val(txtValue.substring(0, options.maxLength + lineBreakCount));

                    $("#" + labelID).html("0" + options.displayText);

                    return false;
                }
                else
                { return true; }
            }
            return true;
        }
    }

    //limit_textarea plugin 
    $.fn.limit_textarea = function (options) {
        // merge default and user parameters
        options = $.extend({ maxLength: 500, displayText: ' characters left', displayTextPosition: 'topLeft' }, options);

        //create label
        limit_textarea.createLabel($(this), options);

        //mouse right click cut/paste event
        $(this).bind('cut paste', null, function (e) {
            if (!e.keyCode) {
                var ctrl = $(this);
                setTimeout(function () {
                    limit_textarea.validateLimit(ctrl, options);
                }, 250);
            }
        });

        //make sure the count is up-to-date
        $(this).mousedown(function (e) {
            if (!e.keyCode) {
                //left mouse click
                if (e.which === 1) {
                    var ctrl = $(this);
                    setTimeout(function () {
                        limit_textarea.validateLimit(ctrl, options);
                    }, 250);
                }
            }
        });

        $(this).keyup(function (e) {
            limit_textarea.validateLimit($(this), options);
        });
    };
})(jQuery);

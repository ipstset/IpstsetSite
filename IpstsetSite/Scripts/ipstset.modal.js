//namespace alisnet_common
var ipstset_modal = function () { };

//Modal popup
ipstset_modal.Modal = function (pageDiv, noClose) {

    this.div = pageDiv;
    this.showClose = !noClose;

    this.show = function () {
        init(this);
        this.div.show();
    };

    this.hide = function () {
        this.div.hide();

        //remove back
        $(".modal-back").remove();
        $(".modal-window").remove();

        this.div.detach();

        //reset body
        $('body').removeAttr('style');
    };

    this.resize = function () {
        formatDivs(this);
    };

    //private method
    var init = function (modal) {

        //$('body').css({ overflow: 'hidden' });
        var backDiv = '<div class="modal-back" style="height: ' + $(document).height() + 'px; width: ' + $(document).width() + 'px;"></div>';
        $('body').prepend(backDiv);

        //popup container
        var closeHtml = "";

        if (modal.showClose)
            closeHtml = '<div class="modal-close"></div>';

        var contDiv = '<div class="modal-window">' + closeHtml + '</div>';


        //add container to page
        $('body').prepend(contDiv);
        $(".modal-window").append(modal.div);

        //attach the modal's hide function to the close button
        if (modal.showClose) {
            $(".modal-close").on('click', function () {
                modal.hide();
            });
        }

        //this will handle resizing
        $(window).resize(function () {
            modal.resize();
        });

        formatDivs(modal);
    };

    var formatDivs = function (modal) {
        $(".modal-back").css({ width: $(document).width(), height: $(document).height() });

        //hardcoded padding from class modal-popup-window...need to change
        var toppos = ((($(window).height() / 2) - (modal.div.height() / 2)) - 30);
        if (toppos < 0) toppos = 0;
        var leftpos = ((($(window).width() / 2) - (modal.div.width() / 2)) - 40);
        $(".modal-window").css({ 'top': toppos + 'px', 'left': leftpos + 'px' });
    };
};




Type.registerNamespace("AjaxControls");

AjaxControls.Timeout = function (element) {

    AjaxControls.Timeout.initializeBase(this, [element]);

    // self reference
    var myself = this;
    // countdown
    var countDownSeconds = null;
    var countDownDelegate = null;
    // timers
    var timerTimeout = null;
    var timerAboutToTimeout = null;
    var timerCountDown = null;
    // properties
    this._timeoutMinutes = null;
    this._aboutToTimeoutMinutes = null;
    this._timeoutUrl = null;
    this._clientId = null;
    this._countDownSpanId = null;

    function countDown(e) {
        // countDownSeconds was computed originally in showDialog
        var secs = countDownSeconds % 60;
        // we want to format it as minutes : seconds
        $('#' + myself._countDownSpanId).text((parseInt(countDownSeconds / 60)) + ':' + ((secs < 10) ? '0' + secs : secs));

        // subtract one and loop back here in 1 second
        countDownSeconds -= 1;
        timerCountDown = setTimeout(countDownDelegate, 1000);
    }

    function resetTimers() {
        // clear all timers
        clearTimeout(timerAboutToTimeout);
        clearTimeout(timerTimeout);
        clearTimeout(timerCountDown);

        // setup the timer that controls when the warning dialog appears
        var showDialogDelegate = Function.createDelegate(myself, showDialog);
        timerAboutToTimeout = setTimeout(showDialogDelegate, myself._aboutToTimeoutMinutes * 60 * 1000);
        // setup the timer that controls when the redirect occurs (when session actually times out)
        var timeoutDelegate = Function.createDelegate(myself, timeout);
        timerTimeout = setTimeout(timeoutDelegate, myself._timeoutMinutes * 60 * 1000);
    }

    function showDialog(e) {
        // on open, the countdown will always start at exactly this._timeoutMinutes - this._aboutToTimeoutMinutes
        countDownSeconds = ((myself._timeoutMinutes - myself._aboutToTimeoutMinutes) * 60) - 1;
        $('#' + myself._countDownSpanId).text((myself._timeoutMinutes - myself._aboutToTimeoutMinutes) + ':00');

        // now start our countdown
        countDownDelegate = Function.createDelegate(myself, countDown);
        myself._timerCountDown = setTimeout(countDownDelegate, 1000);

        // open the warning dialog and focus the window
        myself.openDialog();
        window.focus();
    }

    function timeout(e) {
        // redirect to the specified timeout url
        window.location = myself._timeoutUrl;
    }

    // public functions
    this.initialize = function() {
        // ensure requirements are met
        if (typeof jQuery == 'undefined')
            alert('Error:  jQuery not found.');

        AjaxControls.Timeout.callBaseMethod(this, 'initialize');

        // make sure timers are reset on partial postbacks
        Sys.Application.add_load(Function.createDelegate(this, resetTimers));

        this.initDialog();
    }

    this.dispose = function() {
        $clearHandlers(this.get_element());
        AjaxControls.Timeout.callBaseMethod(this, 'dispose');
    }

    // publicly accessible in case you want to reset from outside the control - use $find('whateverid').reset()
    this.reset = function () {
        // make sure dialog is closed
        this.closeDialog();
        // reset session and update timers
        CallServer();
        resetTimers();
    }
}

AjaxControls.Timeout.prototype =
{
    get_timeoutMinutes: function () {
        return this._timeoutMinutes;
    },

    set_timeoutMinutes: function (value) {
        if (this._timeoutMinutes !== value) {
            this._timeoutMinutes = value;
            this.raisePropertyChanged('timeoutMinutes');
        }
    },

    get_aboutToTimeoutMinutes: function () {
        return this._aboutToTimeoutMinutes;
    },

    set_aboutToTimeoutMinutes: function (value) {
        if (this._aboutToTimeoutMinutes !== value) {
            this._aboutToTimeoutMinutes = value;
            this.raisePropertyChanged('aboutToTimeoutMinutes');
        }
    },

    get_timeoutUrl: function () {
        return this._timeoutUrl;
    },

    set_timeoutUrl: function (value) {
        if (this._timeoutUrl !== value) {
            this._timeoutUrl = value;
            this.raisePropertyChanged('timeoutUrl');
        }
    },

    get_clientId: function () {
        return this._clientId;
    },

    set_clientId: function (value) {
        if (this._clientId !== value) {
            this._clientId = value;
            this.raisePropertyChanged('clientId');
        }
    },

    get_countDownSpanId: function () {
        return this._countDownSpanId;
    },

    set_countDownSpanId: function (value) {
        if (this._countDownSpanId !== value) {
            this._countDownSpanId = value;
            this.raisePropertyChanged('countDownSpanId');
        }
    },

    // this is the default dialog (override if not using jquery UI)
    initDialog: function (e) {
        var ctl = this;
        if (typeof $("#" + this._clientId).dialog != 'undefined') {
            $("#" + this._clientId).dialog({
                autoOpen: false,
                modal: true,
                buttons: {
                    "Ok": function () {
                        ctl.reset();
                    }
                }
            });
        }
    },

    // close the dialog (override if not using jquery UI)
    closeDialog: function () {
        if (typeof $("#" + this._clientId).dialog != 'undefined') {
            $("#" + this._clientId).dialog('close');
        }
    },

    // open the dialog (override if not using jquery UI)
    openDialog: function () {
        if (typeof $("#" + this._clientId).dialog != 'undefined') {
            $("#" + this._clientId).dialog('open');
        }
    }
}

AjaxControls.Timeout.registerClass('AjaxControls.Timeout', Sys.UI.Control);

if (typeof (Sys) !== 'undefined')
    Sys.Application.notifyScriptLoaded();

function ReceiveServerData(rValue) { }

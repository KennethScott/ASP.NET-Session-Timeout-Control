Type.registerNamespace("AjaxControls");

AjaxControls.Timeout = function (element) {

    AjaxControls.Timeout.initializeBase(this, [element]);

    // self reference
    var parent = this;
    // countdown
    var countDownSeconds = null;
    var countDownDelegate = null;
    // timers
    var timerTimeout = null;
    var timerAboutToTimeout = null;
    var timerCountDown = null;
    // properties
    var timeoutMinutes = null;
    var aboutToTimeoutMinutes = null;
    var timeoutUrl = null;
    var clientId = null;
    var countDownSpanId = null;

    function countDown(e) {
        // countDownSeconds was computed originally in showDialog
        var secs = countDownSeconds % 60;
        // we want to format it as minutes : seconds
        $('#' + countDownSpanId).text((parseInt(countDownSeconds / 60)) + ':' + ((secs < 10) ? '0' + secs : secs));

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
        var showDialogDelegate = Function.createDelegate(parent, showDialog);
        timerAboutToTimeout = setTimeout(showDialogDelegate, aboutToTimeoutMinutes * 60 * 1000);
        // setup the timer that controls when the redirect occurs (when session actually times out)
        var timeoutDelegate = Function.createDelegate(parent, timeout);
        timerTimeout = setTimeout(timeoutDelegate, timeoutMinutes * 60 * 1000);
    }

    function showDialog(e) {
        // on open, the countdown will always start at exactly timeoutMinutes - aboutToTimeoutMinutes
        countDownSeconds = ((timeoutMinutes - aboutToTimeoutMinutes) * 60) - 1;
        $('#' + countDownSpanId).text((timeoutMinutes - aboutToTimeoutMinutes) + ':00');

        // now start our countdown
        countDownDelegate = Function.createDelegate(parent, countDown);
        timerCountDown = setTimeout(countDownDelegate, 1000);

        // open the warning dialog and focus the window
        $("#" + clientId).dialog('open');
        window.focus();
    }

    function timeout(e) {
        // redirect to the specified timeout url
        window.location = timeoutUrl;
    }

    // public functions
    this.initialize = function () {
        // ensure requirements are met
        if (typeof jQuery == 'undefined')
            alert('Error:  jQuery not found.');
        if (typeof jQuery.ui == 'undefined')
            alert('Error:  jQuery UI not found.');

        AjaxControls.Timeout.callBaseMethod(this, 'initialize');

        // make sure timers are reset on partial postbacks
        Sys.Application.add_load(Function.createDelegate(this, resetTimers));

        this.initDialog();
    }

    this.dispose = function () {
        $clearHandlers(this.get_element());
        AjaxControls.Timeout.callBaseMethod(this, 'dispose');
    }

    this.get_timeoutMinutes = function () {
        return timeoutMinutes;
    }

    this.set_timeoutMinutes = function (value) {
        if (timeoutMinutes !== value) {
            timeoutMinutes = value;
            this.raisePropertyChanged('timeoutMinutes');
        }
    }

    this.get_aboutToTimeoutMinutes = function () {
        return aboutToTimeoutMinutes;
    }

    this.set_aboutToTimeoutMinutes = function (value) {
        if (aboutToTimeoutMinutes !== value) {
            aboutToTimeoutMinutes = value;
            this.raisePropertyChanged('aboutToTimeoutMinutes');
        }
    }

    this.get_timeoutUrl = function () {
        return timeoutUrl;
    }

    this.set_timeoutUrl = function (value) {
        if (timeoutUrl !== value) {
            timeoutUrl = value;
            this.raisePropertyChanged('timeoutUrl');
        }
    }

    this.get_clientId = function () {
        return clientId;
    }

    this.set_clientId = function (value) {
        if (clientId !== value) {
            clientId = value;
            this.raisePropertyChanged('clientId');
        }
    }

    this.get_countDownSpanId = function () {
        return countDownSpanId;
    }

    this.set_countDownSpanId = function (value) {
        if (countDownSpanId !== value) {
            countDownSpanId = value;
            this.raisePropertyChanged('countDownSpanId');
        }
    }

    // this is essentially the default dialog if not overriden
    this.initDialog = function (e) {
        var ctl = this;
        $("#" + clientId).dialog({
            autoOpen: false,
            modal: true,
            buttons: {
                "Ok": function () {
                    ctl.reset();
                }
            }
        });
    }

    // publicly accessible in case you want to reset from outside the control - use $find('whateverid').reset()
    this.reset = function () {
        // make sure dialog is closed
        $("#" + clientId).dialog('close');
        // reset session and update timers
        CallServer();
        resetTimers();
    }
}

AjaxControls.Timeout.registerClass('AjaxControls.Timeout', Sys.UI.Control);

if (typeof (Sys) !== 'undefined')
    Sys.Application.notifyScriptLoaded();

function ReceiveServerData(rValue) { }

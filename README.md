The ASP.NET Session Timeout Control provides the following:

* User notification that their session is about to expire (includes countdown timer)
* AJAX-enabled session update (enabling users to extend their session without leaving the page)
* Redirect to the designated timeout page upon session expiration.

The Control itself is a .NET 4.5 server control with embedded javascript that provides these capabilities via a 
mixture of client and serverside properties and methods.  The control requires jQuery for interaction with the DOM, 
but can work with any desired notification mechanism such as a simple element or a modal dialog.

By default the control works with the jQuery UI Dialog and whatever jQuery UI theme you have installed - and the only thing needed
in the app is a webmethod or service that can be called to extend session.  User notification defaults to your site's Session Timeout minus one minute.
Note that Session Timeout is set using the standard .NET sessionState tag in the web.config.

The control is template based and you can layout your desired notification interface within the template or it 
may exist elsewhere on the page.

The required **timeout.js** file is embedded in the control and by default is automatically sent to the client.  In cases where you'd
rather serve the file your way (combined/minified/etc), you may disable the embedded copy by setting **UseEmbeddedJavascript=false** and 
then either specifying the path to your timeout.js file in the **JavascriptUrl** property OR simply adding your own script tag to the page.

| Property				| Description |
| --------------------- | ----------- |
| TimeoutMinutes		| Minutes until the user's session will timeout.  Defaults to site's Session Timeout value. |
| AboutToTimeoutMinutes | Minutes until the user is notified that their session is about to timeout.  Defaults to the TimeoutMinutes minus one. |
| TimeoutUrl            | The page to redirect to upon timeout. |
| CountDownSpanId       | The span element to show the countdown timer in. |
| UseEmbeddedJavascript | Controls whether the embedded timeout.js file is automatically sent to the client.  Defaults to true. |
| JavascriptUrl		    | Optionally specifies the location of the timeout.js file (if not using the embedded version).  Defaults to "". |
| SessionRefreshURL     | Specifies the location of the page/service/method to be called via $.ajax to extend session. |


For using with other notification mechanisms there are 5 key public clientside methods available on the control:

| Method       | Description |
| ------------ | ----------- |
| setup()      | used for any one-time setup/configuration that may be needed by your notification mechanism |
| show()       | show the notification to the user |
| hide()       | hide the notification from the user |
| reset()      | called by the user to extend their session (also hides the notification, resets timers, etc) |
| timeout()    | occurs upon session timeout.  Redirects by default. |

Examples in the included project demonstrate using the control with:

* [jQuery UI Dialog](https://jqueryui.com/dialog/)
* [SimpleModal](http://www.ericmmartin.com/projects/simplemodal/)
* [ThickBox](http://codylindley.com/thickbox/)
* [ColorBox](http://www.jacklmoore.com/colorbox/)
* Div (also demonstrates resetting control from content page)
* [jQuery EasyUI](http://www.jeasyui.com/)



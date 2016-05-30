1.13
---
* Breaking changes.  Removed the old crusty Callback approach to resetting session.  It's 2016 and there is just no reason to force the current page 
  through a full lifecycle just to refresh session.  Instead the control now requires a SessionRefreshUrl that will be called via ajax to update session.

1.10
---
* Added code to reset session and timers on completion of any ajax calls

1.9
---
* Added two new server properties:  UseEmbeddedJavascript (default=true) and JavascriptUrl (default="") used to control how the required timeout
  javascript file is sent to the client.

1.8
---
* Added new event OnRaisingCallbackEvent that can be subscribed to for notification of when the control's callback fires.  Example implementation
  on jQueryUI demo.

1.7
---
* Fixed bogus references to old jQuery 1.6.4
* Moved jQuery reference into UserControl and updated Master pages accordingly
* Created NavigationMenu UserControl with links to all demo pages and added to all Master pages

1.6
---
* Upgraded to jQuery 1.7 and jQuery UI 1.8.16
* Added new content page jQueryUiMultipleDialogs which demonstrates having a jQuery UI modal dialog up and visible (with content from a separate
  page) prior to the Timeout popup being shown (and redirecting).

1.5
---
* Added jQuery EasyUI demo

1.4
---
* Added TimeoutControlClientId public property to all Master pages and added MasterType declarations to all content pages
* Modified Div pages to demonstrate using the new TimeoutControlClientId property to reset the control externally from the content page
  using both MS Ajax $find And jQuery examples.
  
1.3
---
* Reworked class (again) trying to expose *only* relevant properties and methods
* Removed all references to "dialog" as control now works with any element based mechanism
* Exposed the timeout method (for situations where you need to do more than just redirect)
* Renamed the public methods for clarity.  'showDialog' now 'show';  'hideDialog' now 'hide';  'initDialog' now 'setup';
* Added ColorBox demo

1.2
---
* Reworked class to a mixture of closures + prototypes for maximum flexibility
* jQuery UI no longer required (but is the default) as the control can work with any dialog window via overriden methods
* Demos added for SimpleModal, ThickBox, and Div (in addition to existing jQueryUI demo)

1.1
---
* Default the TimeoutMinutes to HttpContext.Current.Session.Timeout
* Default the AboutToTimeoutMinutes to TimeoutMinutes - 1
* Add changelog.txt

1.0
---
* Initial release of reworked control.  
* jQuery and jQuery UI now required.
* Countdown timer no longer optional
* Partial postbacks now always reset (no longer optional)
* New public reset() method that can be called from outside the control if needed

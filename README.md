ExRep is a file explorer / browser for Windows. It is very crude, and a work in progress with lots left to do.

Fire it up and it will be default start in C:\. Navigate with Backspace and Enter. F2 to rename a file or directory.


* todo

- copy/move - i separat exe?
- delete - i sep exe?

- search
- lazy loading of icons
	- spawn thread upon entering dir that fetches icons; show default icon meanwhile
- handle column re-ordering
- properties window for file
- change: if current dir is deleted; show message and "stay" in dir
x handle UNC paths
	- handle only "\\server"; show list of shares
- handle permissions
x column sorting
	- highlight current sort column
	- show arrow on currently sorted column
	- retain focused item on sorting
- details/list/icons view modes
	- handle rename textbox location - use built-in BeginEdit method on listview?
	- tiles: set appropriate columns to display info
	- smallicons: don't support
- context menus
	- drag and drop context menus
- suspend filesystemwatchers when listview isn't visible (tabbed mode)
- show logical drives when going up beyond drive root
- back/forward history, keep selection
- status bar
- one-shot undo/redo
- read column setup, keyboard shortcuts, etc. from config file

* refactor

- use DataGridView instead of ListView
- FileTypeIconHandler.AddIconToImageList

* toggle settings

- full row select
- confirm on delete
- hide/show system+hidden files
- always/never auto-sort upon changes in dir
- sort dirs first

* bugs / known issues

- listview icons are always shown in the first column - must be Name
- a few files can't fetch an icon - ex: e:\RECYCLER\S-1-5-21-117609710-1897051121-839522115-1003\desktop.ini


* links

network drive/share mapping - http://www.aejw.com/default.aspx?page=codeviewer&file=cNetworkDrive.cs&ret=dev/cnetw

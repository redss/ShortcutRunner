This repository is not maintained! It's better to find a different, similar app.

# ShortcutRunner #

A minimalistic tool for defining global shortcuts for Windows. It doesn't need installation, is configured in a text file, has no GUI and can be easily turned on and off.

## Usage ##

ShortcutRunner consists of two files:

 * `ShortcutRunner.exe`
 * `shortcuts.txt`

Define your shortcuts in `shortcuts.txt` file:

    Ctrl + Win + H -> mshta javascript:alert("Hello world!");close();

Now run `ShortcutRunner.exe`. When you click Ctrl + Win + H, a greeting message will appear.

To close ShorcutRunner, right-click on its tray icon and select "Exit ShortcutRunner".

## Defining shortcuts ##

Lines starting with `#` or empty lines will be ignored. Shortcut is defined as follows:

    # Opens up downloads folder.
    Ctrl + Win + H -> explorer %userprofile%\Downloads

Left-hand-side of the arrow describes shortcut.
Valid shortcut can contain multuple modifier keys (Ctrl, Alt, Shift or Win)
and must contain exactly one non-modifier key (e. g. H). 
Full list of keys can be found [here](http://msdn.microsoft.com/en-us/library/system.windows.forms.keys%28v=vs.110%29.aspx#enumerationSection).

Right-hand-side of the arrow is a command.
Command is sent do cmd.exe when shortcut is triggered, so it can do pretty much everything.

## Requirements ##

ShortcutRunner requires .NET Framework 4.5.

## License ##

ShortcutRunner is licensed under MIT. See LICENSE.txt.

## Credits ##

- [TinyIoC](https://github.com/grumpydev/TinyIoC)
- [Freecns Cumulus](https://www.iconfinder.com/icons/183254/arrow_forward_right_icon)

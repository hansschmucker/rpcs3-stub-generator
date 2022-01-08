# rpcs3-stub-generator
A simple C#/WinForms utility to find games in RPCS3 and generate EXE stubs that launch the games directly.

A simple little utility that was born of the need to have Steam launch different PS3 games with different controller configs.

Features:
* Creates EXE files for PS3 games
* Finds disc games added to RPCS3 from games.yml
* Finds installed games by looking through the emulated HDD (SFO parsing included)
* Creates correct icon for each game
* Uses game title for filename
* Includes GUI application for setup and filtering
* Commandline application for automatic creation


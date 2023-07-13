@setlocal enableextensions
@cd /d "%~dp0"

@echo off

REM Create MyTheme theme symlink to companion module
REM This must be excuted on the server after the module/theme was delpoyed
mklink /j "%CD%\..\..\Themes\MyTheme" "%CD%\..\..\Modules\Smartstore.Themes.MyTheme"


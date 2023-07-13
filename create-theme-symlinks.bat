@setlocal enableextensions
@cd /d "%~dp0"

@echo off

set linkedmodules=Smartstore.Themes.MyTheme

set linksrc=%CD%\..\Smartstore\src\Smartstore.Modules
set linktarget=%CD%\themes

FOR %%A IN (%linkedmodules%) DO (
	mklink /j "%linksrc%\%%A-sym" "%linktarget%\%%A"
)

REM Create theme symlinks to companion module
mklink /j "%CD%\..\Smartstore\src\Smartstore.Web\Themes\MyTheme" "%CD%\..\Smartstore\src\Smartstore.Web\Modules\Smartstore.Themes.MyTheme"

pause

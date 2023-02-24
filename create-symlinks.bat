@setlocal enableextensions
@cd /d "%~dp0"

@echo off

set linkedmodules=MyOrg.HelloWorld,MyOrg.TabsTutorial,MyOrg.MenuTutorial,MyOrg.WidgetTutorial,MyOrg.BlockTutorial,MyOrg.BlockTutorialAdvanced,MyOrg.ExportTutorial,MyOrg.DomainTutorial

set linksrc=%CD%\..\Smartstore\src\Smartstore.Modules
set linktarget=%CD%\src

FOR %%A IN (%linkedmodules%) DO (
	mklink /j "%linksrc%\%%A-sym" "%linktarget%\%%A"
)

REM Create solution file symlink
mklink "%CD%\..\Smartstore\Smartstore.CodeExamples-sym.sln" "%CD%\Smartstore.CodeExamples.sln"

pause

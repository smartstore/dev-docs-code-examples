# dev-docs-code-examples

Contains all the code examples used in the [technical documentation](https://smartstore.gitbook.io/developer-guide/compose/modules/examples).

## Introduction

This repository contains all the tutorial examples used in the SmartStore developer documentation.
They can be included using the **.sln** solution file.

## Installation

1. Copy this repository into the same directory as your SmartStore repository.
2. Run `create-symlinks.bat`.
3. Open the `Smartstore.CodeExamples.sln` solution file.

## Directory structure

Place this repository in the same directory as your SmartStore repository.

Example:
- SmartStore repository: _/home/www/Smartstore/_
- Place in _/home/www/_

| Folder | Description                              |
| ------ | ---------------------------------------- |
| [root] | **Solution file** and **symlinks**       |
| _/src_ | All **module** examples can be found here.|

### Symlinks

Creates symlinks, so that Smartstore can use the solution file and all of the modules.
- The modules are each linked to _/Smartstore/src/Smartstore.Modules/_.
- The solution file is linked to _/Smartstore/Smartstore.CodeExamples-sym.sln_.

## Tutorials

### "Hello World"

Start your journey here and build your first Smartstore module.  
&rarr; Source code for [Hello World](./src/MyOrg.HelloWorld)  
&rarr; Developer documentation for [Building a simple "Hello World" module](https://smartstore.gitbook.io/developer-guide/compose/modules/examples/building-a-simple-hello-world-module)

### Tabs

Add a tab to the product editing page in the backend.  
&rarr; Source code for [Tabs Tutorial](./src/MyOrg.TabsTutorial)  
&rarr; Developer documentation for [Adding tabs](https://smartstore.gitbook.io/developer-guide/compose/modules/examples/adding-tabs)

### Widgets

Create a widget, that displays custom content in the frontend.  
&rarr; Source code for [Widget Tutorial](./src/MyOrg.WidgetTutorial)  
&rarr; Developer documentation for [Creating a Widget provider](https://smartstore.gitbook.io/developer-guide/compose/modules/examples/creating-a-widget-provider)

### Menus

Access the menu bar in the backend and add a menu item and a submenu.  
&rarr; Source code for [Menu Tutorial](./src/MyOrg.MenuTutorial)  
&rarr; Developer documentation for [Adding menu items](https://smartstore.gitbook.io/developer-guide/compose/modules/examples/adding-menu-items)

### Blocks

Create your own PageBuilder-Block to display in stories.  
&rarr; Source code for [BlockTutorial Tutorial](./src/MyOrg.BlockTutorialTutorial)  
&rarr; Developer documentation for [Creating a Block](https://smartstore.gitbook.io/developer-guide/compose/modules/examples/creating-a-block)

#### Blocks (Advanced)

Tweak the Story view modes, to provide a better user experience and render a widget.  
&rarr; Source code for [BlockTutorialAdvanced Tutorial](./src/MyOrg.BlockTutorialAdvancedTutorial)  
&rarr; Developer documentation for [Creating a Block](https://smartstore.gitbook.io/developer-guide/compose/modules/examples/creating-a-block#advanced-topics)

### Export

Provide a configurable export for products, store information, etc.  
&rarr; Source code for [Export Tutorial](./src/MyOrg.ExportTutorial)  
&rarr; Developer documentation for [Creating an Export provider](https://smartstore.gitbook.io/developer-guide/compose/modules/examples/creating-a-export-provider)

### Entities

Create a small messaging system, using your own database table.  
&rarr; Source code for [Domain Tutorial](./src/MyOrg.DomainTutorial)  
&rarr; Developer documentation for [Creating a Domain entity](https://smartstore.gitbook.io/developer-guide/compose/modules/examples/creating-a-domain-entity)
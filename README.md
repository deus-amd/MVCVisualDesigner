MVC Visual Designer
===============

An open source visual designer for ASP.NET MVC framework.

[ASP.NET MVC Framework](http://www.asp.net/mvc) is Microsoft's web application framework, and it provides a lot of advantages compare to traditional Web Form framework, but one disadvantage is that there is no graphical designer for it.

This project will try to provide a graphical designer, but it's **NOT** a WYSIWYG style designer such as DreamWeaver, or Web Form Designer.

This project is based on such observation that pages in a specific applications are composed of some UI componets with predefined styles for that application (styles are defined by using classes and css). In addition to predefined styles, these UI components may contain client side logic written in javascript, and may interact with web server by ajax.

## This graphical designer is supposed to be used in this way:
 1. For a specific web application, add new UI components or customize existing UI components used in this designer (will provide some predefined UI components)
 2. Compose pages by using the UI components in designer graphically
 3. Generate code files (\*.cs, \*.cshtml, \*.js, \*.css etc.) from the designer
 4. Customzie generated code files if necessary.

## The target feature list:
 * Create pages in designer graphically, and only present basic layout in the designer, not in WYSIWYG style.
 * Be able to generate code files, not only \*.cshtml files, but also javascript files, \*.cs files for view model definition etc.
 * The designer itself can be extented by adding new UI components or customizing existing UI components.
 * Manage the relationship of layout views, views, partital views and related javascript, stylesheet files graphically.

This tool is based on [Visual Studio Visualization and Modeling SDK (was DSL SDK)](http://archive.msdn.microsoft.com/vsvmsdk), and provides a Vistual Studio style UI.

So far, this project is at its very beginning stage, and more features will be implemented soon, and welcome any suggestions and comments.

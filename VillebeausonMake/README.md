# VillebeausonMake

This project (this program, a console application) builds the HTML files in the WebSite.

The contents of the web site are defined by the text files in the `Pages` folder.

This program processes those text files, and merges them into the HTML template (which is the [template.html](template.html) file).

## Text files in the Pages folder

### !pages.txt

The [!pages.txt](Pages/!pages.txt) file defines the list of pages.

Each line contains two tab-separated parts:

1. The first is the human-readable page title
2. The second is the machine-readable filename (ASCII only because non-ASCII characters in an URL must be escaped, which is ugly).

The sequence of lines is significant:

- The first line should define the `index` page
- There must be an even number of lines (and so, of pages)
- The lines are in pairs, so each French-language page is followed is followed by the corresponding English-language page
- The default home page (i.e. the `index` page) is French-language.

### !fragments.txt

The [!fragments.txt](Pages/!fragments.txt) file defines the annuouncements at the top of each page.

This file has the following format:

- A separate section for each page (the fragments for all pages are in this one file)
- Each new section starts with a `#` followed by the page ID.
The page ID must exist in the `!pages.txt` file, and every page must have a section in this `!fragments.txt` (though a section may be empty).
- Each section contains HTML fragments (i.e. text with HTML tag markup).
Expecting and allowing HTML tags in this file makes it more difficult for a non-developer to edit, but allows complex content (e.g. embedded maps or whatever).

### Other files

All other files (without a `!` in the filename) define text inserted at the end of (i.e. the main body of) each page.

The format of these files is relatively simple so that anyone can edit them fairly easily.

- `#` for a section title
- a separate paragraph for each line of text (optionally multiple sentences per line/paragraph)
- empty lines ignored

## template.html

The [template.html](template.html) defines the overall format of every HTML page.

Text in `{ }` braces is replaced with page-specific content.

`bootstrap.min.css` is a copy of one of the [Bootstrap v4](https://getbootstrap.com/docs/4.0/getting-started/download/) style sheets.
This file exists (under source control) in the WebSite folder.

Other CSS is defined inline the template (not in a separately-included style-sheet file) -- that is in order to:

- Minimize the number of downloaded style-sheet files (to improve performance)
- Avoid the possibility of browsers' cacheing old versions of a style-sheet file.

The template is (hopefully) optimized for viewing on a small-screen device (i.e. a mobile phone).

## Software

The `*.cs` files contain C# source code (which you can build and run using Visual Studio 2015).

There's one C# `class` per source file (as usual).

Four classes encapsulate (i.e. they know to how to read and are responsible for processing) the four types of input data file:

- [Pages.cs](Pages.cs) reads the [!pages.txt](Pages/!pages.txt) file
- [Fragments.cs](Fragments.cs) reads the [!fragments.txt](Pages/!fragments.txt) file
- [Page.cs](Page.cs) reads the other `*.txt` text files in the `Pages` folder
- [Template.cs](Template.cs) reads the [template.html](template.html) file

Other classes:

- [Navbar.cs](Navbar.cs) is called from [Template.cs](Template.cs) -- it creates the navigation bar at the top of each page.
I experimented with different versions/implementations of navigation bar.
The current one is a home-grown version which doesn't depend on Bootstrap.
- [PageText.cs](PageText.cs) is called from [Page.cs](Page.cs) and from [Fragments.cs](Fragments.cs) to help parse the text files and convert it to HTML.
- [Output.cs](Output.cs) is called from [Template.cs](Template.cs) -- all it knows is where to write the output (i.e. the `WebSite` folder), not what to write.
- [Program.cs](Program.cs) contains the `Main` method -- it gets all the `Page` instances, and passes them into the `output` method of the `Template` class.

One peculiarity is that, in the text files, any text in `[ ]` square brackets must be a page ID (e.g. `[index]`) and is replaced with a relative URL.

Another peculiarity is that the program includes a hard-coded `debug` flag -- when this flag is `true` it generates URLs which end with `.html`:

- This helps me develope (test) the web pages within Visual Studio on my machine -- apparently the mini-webserver that's built-in to Visual Studio
doesn't support URL rewriting, so I need the complete/correct URLs to navigate between pages.
- For the live site I generate files with the `debug` flag set `false`, to generate URLs like `/music` instead of `/music.html`
(because the URLs are prettier without the file extension).
This is supported on the live site by an URL-rewriting statement that's built-in to the [.htaccess](../WebSite/.htaccess) file
(which is used by the Apache server which runs the live site).

## Live site

After HTML files are built, I upload them from the WebSite folder to the live site via FTP.

FYI I use WinSCP as my FTP client software.

The credentials (username, password, etc.) of the live site are *not* shown in this public README (they were detailed elsewhere).
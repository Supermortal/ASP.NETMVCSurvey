MVCSurvey
=========

A drop-in survey system for ASP.NET MVC 4

Contained in the repository are a fully working example solution (some of the javascript components don't work in IE; this needs to be fixed), and a Controllers, Models
Views, Abstract and Concrete folders. The Controllers, Models and Views folders will give you the MVC components you need, and the Abstract and Concrete folders will give
you the underlying repositories and services. The service can be copy and pasted to a new implementation with minimal changes, and the repository interfaces can be used to create
your own implementations, or you can use the Entity Framework solutions included. If you need to return an IQueryable<> from your implementations, you can fill a list from a reader
or other source, and return the list .AsQueryable().

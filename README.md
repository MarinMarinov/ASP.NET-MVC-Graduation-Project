# ASP.NET-MVC-Graduation-Project
###Telerik Academy 2016 - ASP.NET MVC Course - Graduation Project

On-line Auction Application "Live Auction"

##The Idea

App for managing on-line instant live auctions(continuing no more than several minutes) for luxury goods like pictures, statues, porcelain, jewellery, post stamps, retro cars etc.

###Using **SignalR** technology implements the fastest possible real-time communication true web-sockets.

The functionality is as described:

###Admin functionality:
*	Full CRUD operations on Item with upload of pictures
	*	The pictures are saved in the Database
*	Full CRUD operations on Auction for an Item
*	Manage Auction:
	*	Set Auction as Active
	*	Close Auction(set it as Inactive)
	*	Make bids
* Includes all User and free functionality

###User functionality:
*	Create, Read, Edit of User
	*	Uploading avatar image
	*	Avatar images are saved on the file system
*	Join to active Auction
*	Make bids

###Free functionality
* See all Items and details of Items
* See all Auctions and details of Auctions

###Used frameworks and libraries
*	ASP.NET MVC 5.0
	*	Including all default libraries and frameworks
*	ASP.NET SignalR 2.2.0
*	Entity Framework 6.1.3
	*	Code-first approach
* jQuery - 2.0.3
	* jQuery UI - 1.11.4
	* Unobtrusive Ajax support library for jQuery
	* jQuery Validation Plugin 1.11.1
	* Unobtrusive validation support library for jQuery and jQuery Validate
* Autofac.Mvc 5 - 3.3.3
* Automapper - 4.2.0
* Grid.Mvc - 2.3.0
* Bootstrap CSS library
	* Spacelab free theme

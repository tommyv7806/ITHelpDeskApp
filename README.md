# ITHelpDeskApp
In this application, users can either be IT Users or Non-IT Users. Depending on which type of user they are, they will have access to different functionalities.

### Non-IT User Index Page
After a Non-IT user logs in, the Index page that they will see will look like the one in the below image.
* A table will display all of the user's curent tickets.
* By default, it will only display Open tickets, but users can check the box above the table to also show any tickets with a Closed status.
* The 'Create New' button will open a modal where users can enter a new ticket (see further below for an example).
![Non-IT User](https://github.com/tommyv7806/ITHelpDeskApp/assets/67933601/60af3cd4-d20a-4e20-bf92-3e5b3e19ab37)

### Create New Ticket Window
When a Non-IT User clicks the 'Create New' button from their Index page, a modal will open that will allow them to enter a new ticket.
* Basic modal that allows the user to enter a Title, Descripton, and select a Priority for the ticket.
![New Ticket Modal](https://github.com/tommyv7806/ITHelpDeskApp/assets/67933601/53802134-0515-4259-bfa4-4872e011b482)

### Ticket Summary Window
When a user - Non-IT or IT - clicks on a ticket, this ticket summary window will open to show the details of the ticket. This window will look the same to both Non-IT and IT users. The only difference is that an IT user will be able to configure the status of the ticket from here to close out a ticket.
![Ticket Summary Modal](https://github.com/tommyv7806/ITHelpDeskApp/assets/67933601/6f8be9bf-9d5a-4f6d-bc44-403681d31aa6)

### IT User Index Page
Whenever an IT User logs in, this is the first page they will see. 
* There are two tables - one for displaying unassigned tickets and one for displaying assigned tickets.
* Users can click the 'Pick Up' button in the Unassigned table in order to pick up a ticket and assign themselves to it.
* Users can change the value in the dropdown above the Assigned table to filter the list of assigned tickets to either show only tickets assigned to the user, or to display all tickets.
![IT User Index Page](https://github.com/tommyv7806/ITHelpDeskApp/assets/67933601/4c2dd589-7ac9-411b-8e65-32e103d14ddb)

### Initial Project Description 
For this project, I would like to try and build an IT help desk web application. It would function similarly to applications like JIRA. In a nutshell, users can create and submit tickets when they encounter IT related issues. A member of the IT team will then be assigned to the ticket, and they will be responsible for resolving the issue and controlling the flow of the ticket. 

The application would first begin with a login screen where users can either log in or register. When the users register, they will be able to select their Role - either an 'IT Team Member' or 'End User'. Once registered and logged in, the initial view will be different depending on the user's Role. The view for the IT Team Members will allow them to see all open tickets, manage the status of tickets, and assign tickets. On the other hand, the initial view for the End Users would allow them to see only their open tickets as well as create new ones. 

On each ticket, the End Users would provide a Summary, Description, Priority and Type for their issue. Depending on the Type selected (General IT Help, Password Reset, Purchase Request), additional fields may need to be filled out. Once the ticket is created, there will be a Comments area where the End User and the IT Team Member can discuss the ticket with each other. The IT Team Member who is assigned to the ticket will be able to change the Status of the ticket and will eventually close the ticket once the issue has been resolved. Ideally, each End User and IT Team Member will also be able to see the historical record of all closed tickets that they have been a part of. There will also be a special IT Admin user login that will be able to view and edit any fields on any tickets.

Overall, this IT help desk application will provide hands-on experience working with registration/login systems for creating and maintaining users, interfacing with databases for storing/retrieving user and ticket information, and working with an MVC structure to display certain views and allow certain actions based on the logged in user's role.

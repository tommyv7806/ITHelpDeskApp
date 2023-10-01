# ITHelpDeskApp
### Intro: 
For this project, I would like to try and build an IT help desk web application. It would function similarly to applications like JIRA. In a nutshell, users can create and submit tickets when they encounter IT related issues. A member of the IT team will then be assigned to the ticket, and they will be responsible for resolving the issue and controlling the flow of the ticket. 

### Body: 
The application would first begin with a login screen where users can either log in or register. When the users register, they will be able to select their Role - either an 'IT Team Member' or 'End User'. Once registered and logged in, the initial view will be different depending on the user's Role. The view for the IT Team Members will allow them to see all open tickets, manage the status of tickets, and assign tickets. On the other hand, the initial view for the End Users would allow them to see only their open tickets as well as create new ones. 

On each ticket, the End Users would provide a Summary, Description, Priority and Type for their issue. Depending on the Type selected (General IT Help, Password Reset, Purchase Request), additional fields may need to be filled out. Once the ticket is created, there will be a Comments area where the End User and the IT Team Member can discuss the ticket with each other. The IT Team Member who is assigned to the ticket will be able to change the Status of the ticket and will eventually close the ticket once the issue has been resolved. Ideally, each End User and IT Team Member will also be able to see the historical record of all closed tickets that they have been a part of. There will also be a special IT Admin user login that will be able to view and edit any fields on any tickets.

### Conclusion: 
Overall, this IT help desk application will provide hands-on experience working with registration/login systems for creating and maintaining users, interfacing with databases for storing/retrieving user and ticket information, and working with an MVC structure to display certain views and allow certain actions based on the logged in user's role.

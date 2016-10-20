Add the following line before StaticFile handlers in the IIS express config file (the file is located at .vs\config\applicationhost.config) (5th line from the bottom)
<add name="HtmlFileHandler" path="*.html" verb="GET" type="System.Web.Handlers.TransferRequestHandler" preCondition="integratedMode,runtimeVersionv4.0" />
Source: http://weblogs.asp.net/jongalloway/asp-net-mvc-routing-intercepting-file-requests-like-index-html-and-what-it-teaches-about-how-routing-works

Change the SnapConnection in Web.config of the SnapChallenge project
Run the SnapChallenge project, the database will be created and seeded automatically
A webpage will be shown with the following functionality:
- Three kind of teacher reports
- Individual student report
- Seeding progress (if running)
- Search student by studentnumber

If the seeding has been interrupted (e.g. by changing the IIS express settings), delete the database and run the SnapChallenge project again

Snappet Challenge by:
Tim Theeuwes
tim@inexpro.nl
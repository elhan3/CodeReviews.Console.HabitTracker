<h1>Console Habit Logger</h1>
<p>This is a basic CRUD console application for runners. It tracks what distance has a person run on a specific date.
  Appication was developed using C# and SQLite. In order to make UI a bit more interactive, I used Specter.
</p>
<h2>Requirements</h2>
<ul>
  <li>The habit can only be tracked by quantity (in this case app tracks distance).</li>
  <li>When the application starts a new table in a database should be created if it doesn't exist.</li>
  <li>User should be able to view, insert, update and delete data from the database.</li>
  <li>Errors should be handled so that the app doesn't crash</li>
  <li>Application can interact with the database only using ADO.NET.</li>
</ul>
<h2>How does the app work</h2>
When the program is started, if table doesn't already exist, new table is created. Then, the following UI is displayed. 

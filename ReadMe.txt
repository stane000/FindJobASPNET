1. Connection string: if you use PostgreSql, just change the database name and password in the connection string "DefaultConnection"
 		      If you use another database, change the connection string according to the database specifications
 			and in program.cs change UseNpgsql to Use..databsename, you must also install a server for this database

2. Migrations: If you use PostgreSql, open the package manager console and run the command: Update-Database
 		If you are using another database, you need to create new migrations in the package manager console and then run update-database
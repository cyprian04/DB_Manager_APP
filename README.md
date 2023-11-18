# DB_Manager_APP
It's a desktop app in C# that allows to interfere with databases from accounts located on the databases server, For now tested only od MariaDB databases


App requaries installed database server. On that server you should have user with privileges to default databases and databases you want to manage remontly or localy.
short procedure to create user: 
`CREATE USER 'username'@'ip';`  - if you want a user without password
if you want user to has password use below fragment:
`CREATE USER 'username'@'ip' IDENTIFIED BY 'password';`

Now that you have user created add privileges:
`GRANT ALL PRIVILEGES ON database.table TO 'root'@'ip';` - if you are using this statement you have to add privileges to all default databases and databases you want to manage
or
`GRANT ALL PRIVILEGES ON *.* TO 'root'@'ip';` - use for all databases managment

then `FLUSH PRIVILEGES;`

ip - is ip of a host from you are running the app

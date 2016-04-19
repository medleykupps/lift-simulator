# lift-simulator
Simulating a lift system

Running in Windows Azure at
http://lift-simulator.azurewebsites.net/

Can be run locally - will require a new Sql Server database be created.

The script at 'Sql\Create database.sql' can do this.  A connection string will be required in the web.config file for the new database.

Connection string name: LiftSimulator

Lift movement is tracked in the LiftMovements table

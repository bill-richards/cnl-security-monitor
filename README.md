# CNL Technical Exercise

This solution requires `Docker Engine 1.8+` because it makes use of a Docker image.

- bitnami/rabbitmq:latest

  This container provides a RabbitMQ server for a publisher/subscriber implementation
  
---

## Setup

- Run the Powershell script to prepare the required containers

```PowerShell
.\container-setup.ps1 'D:\container-persistance'
```

The script will prepare the RabbitMQ container whose data will be persisted to the specified persistence directory on the host.

Once the container is running, you can open the RabbitMQ administration pages by navigating to [`http://localhost:15672`](http://localhost:15672)

- username `cnl_user`
- password `cnl_Password1`

---

## Running the solution

- open and build the solution within Visual Studio (2019)
- the order in which you fire up the executables is not important
  - run the Security.Server (**just one instance**)

    either in debug from with VS or by navigating to `cnl-security-monitor\Security.Server\bin\Debug\netcoreapp3.0` and running `Security.Server.exe`
  - run the Security.Monitor (**as many instances as you like**)

    either in debug from with VS or by navigating to `cnl-security-monitor\Security.Monitor\bin\Debug\netcoreapp3.0` and running `Security.Monitor.exe`

The application is not completely polished, and for the time being you should:

- run the Server on the same machine as the RabbitMQ container
- run the Monitor instances (however many you choose) on the same machine as the RabbitMQ container

This is because the connection for RabbitMQ is looking at `localhost` and has not been made configurable.

### Using the application

#### The Server

The server window, provides the ability to register doors, change the state of those doors open/closed, and it also provides a view of the messages which are being passed via RabbitMQ. When changing the state of the doors using the Server window, those state changes will be reflected on all of the Monitor instances which you have ruunning.

#### The Monitor

The monitor instances will be blank WPF windows until doors have been registered with the server;however if you have any instances of the Monitor running when you register doors on the Server, those monitored doors will appear on the monitor instances. Also, if you should fir up an Monitor instances whilst the Server is running, and doors have been registered, those doors will appear on thoses instances when you fire them up.

When changing the state of the doors using the Monitor windows, those state changes will be reflected on all of the Monitor instances which you have running, and in the Server window. You will also be able to see the messages in the Server window.

### The database

The database is an SQLite database called `security.db` and can be found in the `Security.Server` directory, you can view this from within Visual Studio

The database was created **_code first_** and when you inspect the solution, you will notice that within the `Security.Data` project, there is a Migration file which was used to construct the database structure.

The database only contains two tables, one for the registered door data (id, label, door state), and the other for recording events (door registration, opening/closing of doors).

---

## Take note

You may notice that when you close down the Server application the process remains running for a short while after the window has closed, this is due to closing of the RabbitMQ channels and connections. I believe it is due to the queue running in a docker container on the local machine.

---

## When you have finished running the solution

If you want to remove the containers and images from your Docker server run the *teardown* PowerShell script

```PowerShell
.\container-teardown.ps1
```

The script will **not** remove your persistence directory, so **_you will have to delete this manually_**.

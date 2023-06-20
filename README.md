## Protato Online

A simple online game created with Unity and ExpressJS, where players can engage in battles against AI-controlled monsters or engage in multiplayer combat with other players.

Built with:
* Game Engine: Unity
* Server: ExpressJS, MongoDB, mongoose, UDP Socket, JWT, express-validator, Swagger 
* Deployment and Infrastructure: GCP VM, Docker, NGINX

### Installation

To install and run the application, follow the steps below:

1. Clone the repository to your local machine:
   ```sh
   git clone https://github.com/Sweetloveinyourheart/protato
   ```
2. Go to server folder:
   ```sh
   cd express-server
   ```
3. Build the application using docker-compose:
   ```sh
   docker-compose build
   ```
3. Run the application using docker-compose:
   ```sh
   docker-compose up -d
   ```
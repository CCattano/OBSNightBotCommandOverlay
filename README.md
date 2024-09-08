# OBSNightBotCommandOverlay
Full-stack multi-tenant websocket-driven web app and server for displaying the output of a NightBot command in OBS and refreshing output based on external signaling from Twitch chat.

Repository contains source code for the backend API server code and the frontend Web App code.

API Server endpoints for websocket functionality are configured to support being hosted as an AWS Lambda on top of an AWS Websocket Gateway.

If your hosting methodology of choice differs you would need to fork this and modify as needed. Given it is multi-tenanted by design so I can share it with multiple friends if you'd like to operate on my hosted version of this application you can reach out to ask and I can see if it would pose a problem.

Web App is an Angular app that can be run directly out of the index.html file or via a hosting solution of your choice.

Web App source code is configured such that it makes web requests to the backend via a fixed URL configuration that is NOT committed to VCS. You will need to provide your own backend FQDN to be used during runtime based on how/where you host the backend API server code.

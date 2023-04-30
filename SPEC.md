

# Project Specification Document - Torrent Downloader Web Application

## Overview

This ASP<span>.</span>NET Core project aims to provide a web application for users to search and download torrent files without requiring an account on the ncore website. The application utilizes a separate microservice to provide this functionality through gRPC communication. The frontend of the application is developed using Angular, which provides a Single Page Application for users to interact with. The backend of the application is developed using ASP<span>.</span>NET Core and provides the necessary APIs for communication between the frontend and the microservice. The application also utilizes Bootstrap and Angular Material for styling and user interface components.

## Features

-   User authentication and authorization will not be required to access the application's search and download functionalities.
-   The application will allow users to search for torrent files based on various criteria such as name and category.
-   Users will be able to download torrent files directly from the application.
-   The microservice will provide a search function that accepts user input and returns a list of matching torrent files.
-   The microservice will also provide a download function that takes in a torrent file and returns the file data to the user.
-   The backend will provide RESTful API endpoints for the microservice to communicate with the frontend. These endpoints will include search and download functionalities.
-   The frontend will utilize Angular Material and Bootstrap to provide an aesthetically pleasing and responsive user interface.

## API endpoints for normal users
-	/api/torrents/search [POST]: This endpoint will allow users to search for torrent files.
-	/api/torrents/download [POST]: This endpoint will allow users to download torrent files.
-	/api/user/users [GET]: This endpoint will allow users to get all other users profile's.


## Technologies and Frameworks Used

-   **Angular** will be used to develop the frontend of the application.
-  **ASP<span>.</span>NET CORE** will be used to implement the backend of the application.
-   **gRPC** will be used to facilitate communication between the frontend and the microservice.
-   **Bootstrap** and **Angular Material** will be used to enhance the user interface of the application.

##  Simples:
[![example-Image.png](https://i.postimg.cc/NfgzZvDQ/example-Image.png)](https://postimg.cc/23KGBJgt)
[![example-Image-Users.png](https://i.postimg.cc/j5tHCtL5/example-Image-Users.png)](https://postimg.cc/gwtXT9yF)
[![example-Image-Torrents.png](https://i.postimg.cc/zXVnydXn/example-Image-Torrents.png)](https://postimg.cc/F7XfqVSR)
# Baby Sleep Application with White Noise Sounds

This repository contains the source code and documentation for a Baby Sleep Application that plays white noise sounds to help soothe babies and promote better sleep. This README provides an overview of the system architecture, frontend and backend development details, and information about the data models used in the application.

## System Architecture Overview

The Baby Sleep Application follows a client-server architecture, where the client refers to the user-facing interface, and the server handles the processing and storage of data. The architecture is designed to ensure scalability, maintainability, and reliability. The client and server communicate via API calls.

The system architecture can be summarized as follows:
- The client (frontend) is responsible for providing a user-friendly interface, allowing users to interact with the application, select white noise sounds, and control playback.
- The server (backend) is responsible for serving the frontend, processing user requests, managing sound playback, and storing relevant data.

## Frontend Development (Blazor)

The frontend of the Baby Sleep Application is developed using Blazor, a framework for building interactive web user interfaces. Blazor allows us to create rich and responsive UI components using C# and HTML. It enables a seamless development experience by allowing developers to write both client-side and server-side code in C#. Blazor's components-based architecture promotes code reusability and maintainability.

To run the frontend, ensure you have the .NET 7 SDK installed. Then, follow these steps:

1. Clone the repository.
2. Navigate to the `frontend` directory.
3. Open a terminal and run the command `dotnet run`.
4. Access the application by opening a web browser and visiting `http://localhost:5000`.

## Backend Development (.NET 7)

The backend of the Baby Sleep Application is developed using .NET 7, a versatile and powerful framework for building scalable and high-performance applications. The backend is responsible for processing requests, managing sound playback, and performing any necessary business logic.

To run the backend, make sure you have the .NET 7 SDK installed. Follow these steps:

1. Clone the repository.
2. Navigate to the `.\src\server` directory.
3. Open a terminal and run the command `dotnet run`.
4. The backend will start running on `http://localhost:5131/swagger/index.html`.

The backend utilizes an audio playback library to handle playing white noise sounds. It provides APIs for managing the user session and controlling sound playback, such as starting, stopping, and adjusting volume. The backend also manages user preferences and sound selection.

## Data Models Used (Relational)

The Baby Sleep Application uses a relational database to store and manage data. The application employs the following data models:

1. **User**: Represents a user of the application. It includes attributes such as `id`, `username`, `email`, and `password`.
2. **Track**: Represents a white noise sound available in the application. It includes attributes such as `id`, `title`, `audioPath`, and `duration`.
3. Playlist: Stores the user-created playlists in the Baby Sleep Application. A playlist is a collection of white noise sounds that users can create and customize according to their preferences. It includes attributes such as `id`, `name`, and `tracks`.
4. **PlaybackHistory**: Stores the playback history of white noise sounds for a user. It includes attributes such as `id`, `user_id`, `tracks`.

The use of a relational database allows for efficient data storage, retrieval, and querying. The application utilizes SQL queries and database transactions to ensure data consistency and integrity.

---

The Baby Sleep Application repository provides a comprehensive solution for managing a baby's sleep patterns by playing white noise sounds. The system architecture overview explains the client-server model, the frontend development section covers the usage of Blazor, the backend development section discusses the implementation using .NET 7, and the data models section provides information about the relational database structure.
Feel free to explore the repository, contribute, and customize the application to suit your

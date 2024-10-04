# Rehab Management System

## Overview

The **Rehab Management System** is a comprehensive solution for managing rehabilitation processes, including employee and client management. This system is built using a microservices architecture, leveraging ASP.NET Core for the backend and React TypeScript for the frontend.

## Features

- **Employee Management**: Manage employee records, roles, and responsibilities.
- **Client Management**: Track client progress, appointments, and treatment plans.
- **gRPC Services**: Efficient communication between microservices.
- **Authentication and Authorization**: Secure access using JWT and role-based permissions.
- **Responsive Frontend**: Built with React and TypeScript for a modern user experience.

## Architecture

The project is structured as follows:

RehabManagementSystem/

├── RehabManagementSystem.GrpcServices/ # gRPC Services for inter-service communication
│
├── RehabManagementSystem.Database/    # Database layer
│
└── frontend/                          # React TypeScript frontend application
    ├── src/                           # Source files
    ├── public/                        # Public assets
    └── package.json                   # Project metadata


## Technologies Used

- **Backend**: ASP.NET Core, Entity Framework Core
- **Frontend**: React, TypeScript
- **Database**: Sqlite
- **gRPC**: For inter-service communication
- **Authentication**: ASP.NET Identity, JWT

## Getting Started

### Prerequisites

Make sure you have the following installed:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (for React frontend)
- [Database](#) 

### Installation

1. **Clone the repository**:

   ```bash
   git clone https://github.com/Xia-Hui-Liu/RehabManagementSystem.git
   cd RehabManagementSystem
   backend: cd RehabManagementSystem.API
     dotnet restore
     dotnet run
   frontend: cd frontend
     npm install
     npm start





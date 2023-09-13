# HNGx Backend Project 2

![.NET Version](https://img.shields.io/badge/.NET-7-blueviolet)
![Docker Version](https://img.shields.io/badge/Docker-latest-brightgreen)
![Render Hosted](https://img.shields.io/badge/Hosted%20on-Render-008BDC)

## Overview

This is the HNGx Backend Project 2, a RESTful API built using C# and .NET 7 Minimal API framework. It provides endpoints to manage a list of persons, allowing you to perform basic CRUD operations (Create, Read, Update, Delete) on person records. The API is containerized with Docker and hosted on Render, making it easy to deploy and scale.

## Getting Started

To run this project locally or deploy it on your own server, follow these steps:

1. **Prerequisites:**

   - [.NET 7 SDK](https://dotnet.microsoft.com/download/dotnet/7.0)
   - [Docker](https://www.docker.com/get-started)
   - Render account for deployment (optional)
2. Configure the PostgreSQL database connection:
   Update the database connection string in the appsettings.json file to point to your PostgreSQL database.
3. **Clone the Repository:**

   ```bash
   git clone https://github.com/richkazz/HNGx.git
   cd HNGx
   ```

4. **Build and Run Locally:**

   ```bash
   dotnet build
   dotnet run
   ```

5. **Access the API:**

   The API should now be running locally at `http://localhost:8080/api`.

## API Documentation

For detailed information on the API endpoints and how to use them, please refer to the [API Documentation](DOCUMENTATION.md).

## Docker Containerization

This project is Dockerized, allowing for easy deployment and scaling. To build a Docker image and run it locally, follow these steps:

1. **Build the Docker Image:**

   ```bash
   docker build -t hngx.
   ```

2. **Run the Docker Container:**

   ```bash
   docker run -p 8080:80 hngx
   ```

   The API will be accessible at `http://localhost:8080/api`.

## Deployment on Render

To deploy this project on Render, follow these steps:

1. **Create a Render Account:**

   If you haven't already, sign up for a Render account at [https://render.com/](https://render.com/).

2. **Create a New Web Service:**

   - Choose "Docker" as the environment.
   - Link your GitHub repository.
   - Configure the build settings and environment variables as needed.

3. **Deploy the Service:**

   Render will automatically build and deploy your Docker container. Once deployed, your API will be accessible at the provided URL.
---

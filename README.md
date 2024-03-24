
Movie Search Web Application

This project is a web application that utilizes the OMDb API service to provide various movie-related functionalities. It allows users to search for movies by title, view search results, save the 5 latest search queries, and display extended information about a selected movie, including its poster, description, and IMDB score.

Technologies Used
Backend Framework: .NET Core
Frontend Framework: Razor
Database: MSSQL
API Integration: OMDb API (http://www.omdbapi.com)
Unit Testing: Included, using standard .NET Core testing libraries

Table of Contents
Installation
Usage
Features
API Documentation



Installation

To get started with the project, follow these steps:

Clone the repository from GitHub:

git clone https://github.com/your-username/your-project.git

Ensure you have .NET 8 installed. You can download it from Microsoft's official website.

Install Visual Studio 2022 or any other IDE of your choice for development.



Usage

This project requires .NET 8. Ensure you have it installed on your system. You can download it from here.



Features

FluentValidation: Used for elegant and fluent validation of requests, ensuring data integrity and accuracy.

MediatR: Implements the mediator pattern for simplified communication between components, enhancing modularity and maintainability.

Microsoft.EntityFrameworkCore: Employs Entity Framework Core as the Object-Relational Mapper (ORM) for seamless database interactions and efficient data management.

MSSQL: Utilizes Microsoft SQL Server as the relational database management system, providing robust data storage and retrieval capabilities.

Moq: Integrates Moq, a popular mocking library for .NET, to facilitate unit testing by creating mock objects and defining their behavior.


API Documentation


Movie Endpoints
POST /api/Movie/add
Description: Add a new movie.

DELETE /api/Movie/{id}
Description: Delete a movie by its ID.

PUT /api/Movie/{id}
Description: Update a movie by its ID.

GET /api/Movie
Description: Get a list of all movies.

GET /api/Movie/savetopfivesearch
Description: Save top five search results.

OmdbQuery Endpoints
GET /api/OmdbQuery/searchmoviesbytitle
Description: Search movies by title.

GET /api/OmdbQuery/getmoviebytitle
Description: Get a movie by its title.

GET /api/OmdbQuery/getmoviebyid/{id}
Description: Get a movie by its ID.



![Alt Text](https://github.com/VS-GitHub-001/Omdbapi.Movie.Sample/blob/master/UI/RazorWebUI/wwwroot/1.png)



![Alt Text](https://github.com/VS-GitHub-001/Omdbapi.Movie.Sample/blob/master/UI/RazorWebUI/wwwroot/2.png)



![Alt Text](https://github.com/VS-GitHub-001/Omdbapi.Movie.Sample/blob/master/UI/RazorWebUI/wwwroot/3.png)



![Alt Text](https://github.com/VS-GitHub-001/Omdbapi.Movie.Sample/blob/master/UI/RazorWebUI/wwwroot/4.png)


![Alt Text](https://github.com/VS-GitHub-001/Omdbapi.Movie.Sample/blob/master/UI/RazorWebUI/wwwroot/5.png)


![Alt Text](https://github.com/VS-GitHub-001/Omdbapi.Movie.Sample/blob/master/UI/RazorWebUI/wwwroot/6.png)


# Project: Build a Movie Ticket Booking System

## Objective:
Build a movie ticket booking system using Angular frontend and .NET core backend. You can use any database of your choice to store data.

## Requirements:

### User Management
- Create a user registration and login functionality using JWT
- Allow users to view and edit their profile information
- The user profile information should include fields like first name, last name, email, and password.
- Implement role-based authorization

### Movie Management
- Allow admins to add, edit, and delete movies
- The movie information should be obtained through a third-party API or database. Please use any API of your choice to get movie details like title, description, release date, and image. Some examples of movie APIs are The Movie Database (https://www.themoviedb.org/documentation/api) and OMDB (http://www.omdbapi.com/).
- Display movie information such as title, description, release date, and image

### Theatre Management
- Allow admins to add, edit, and delete theaters
- The seating capacity should be specified when the theatre is created.
- Display theatre information such as name, location, and seating capacity

### Showtime Management
- Allow admins to add, edit, and delete showtimes for each movie in each theatre
- The showtimes should be created by the admin and should be associated with a specific movie and theatre.
- The date, time, and price should be specified when the showtime is created.
- Display showtime information such as date, time, and price

### Ticket Booking
- The booking should be implemented using a shopping cart design.
- Users should be able to select the desired movie, theatre, and showtime from dropdown menus.
- Once a showtime is selected, the available seats for that showtime should be displayed on a seating chart.
- Users should be able to select their desired seats and add them to the cart.
- The total price should be calculated based on the number of seats selected.
- You are not required to implement the payment gateway integration. Feel free to mock the API.

## Deliverables:
- Source code of the project with proper documentation and comments
- A README file that contains instructions on how to run the application, the design decisions, technical choices, and any trade-offs made during the project

## Evaluation:
The candidate will be evaluated based on the following criteria:

- Quality of code and adherence to best practices
- Implementation of functionality as per the requirements
- Design and architecture of the system
- Documentation and clarity of communication
- User experience and interface design
# Lettercaixa

This is the back-end part of a full-stack application using `asp.net core` (web api) and `angular`.

Lettercaixa is a web application for users to navigate throught a vast catalog of movies, selecting their favorite movies and writing and reading reviews.


This `asp.net core` is a web api that exposes multiple endpoints that receive user http requests and sends back responses to the angular client, related to accounts, movies and posts.
It connects with a `SQL Server` database to persist data, with a Database First Approach. The DB constains three tables with: Profile, Post and FavoriteMovie;

It has three major parts in the api that exposes this endpoints: 
- Profile: log in, creating, recieving, removing and updating profile fields (full crud).
- Favorite Movies: adding and removing movies from user favorites lists.
- Posts: adding and removing comments made by users from movies favorites.

~ This project is based on the concept of Letterboxd.



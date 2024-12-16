---
title: _Chirp!_ Project Report
subtitle: ITU BDSA 2024 Group 31
author:
  - "Adam Assi <aass@itu.dk>"
  - "David Friis Jensen <davj@itu.dk>"
  - "Frederik Harboe Hjørtdal Andersen <frean@itu.dk>"
  - "Yacob Bakiz <yaba@itu.dk>"
  - "Youssef Wassim Noureddine <yono@itu.dk>"
numbersections: true
---

# Design and Architecture of _Chirp!_

### **Domain Model**


<div align="center">
    <img src="./images/domain_model.png" alt="Domain Model">
</div>

The domain model for the Chirp application consists of two main entities: **Author** and **Cheep**, which represent users and their posts, respectively. The **Author** entity inherits from `IdentityUser<int>` to integrate with ASP.NET Core Identity for authentication and user management. Each Author has a unique `Id`, a `Name` (required), and an optional `Email`. An Author can also have a list of **Cheeps** that they have posted.

The **Cheep** entity represents a short post authored by a user. It contains a unique identifier `CheepId`, a foreign key `AuthorId` referencing the Author, the content of the post in the `Text` field (limited to 160 characters), and a `Timestamp` indicating when the post was created. Each Cheep is associated with exactly one Author, establishing a **one-to-many relationship** between Authors and their Cheeps.

## Architecture — In the small

## Architecture of deployed application

## User activities

## Sequence of functionality/calls trough _Chirp!_

# Process

## Build, test, release, and deployment

## Team work

## How to make _Chirp!_ work locally

## How to run test suite locally

# Ethics

## License

## LLMs, ChatGPT, CoPilot, and others

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

## Domain Model

<div align="center">
    <img src="./images/domain_model.png" alt="Domain Model">
</div>

The domain model for the Chirp application consists of two main entities: **Author** and **Cheep**, which represent users and their posts, respectively. The **Author** entity inherits from `IdentityUser<int>` to integrate with ASP.NET Core Identity for authentication and user management. Each Author has a unique `Id`, a `Name` (required), and an optional `Email`. An Author can also have a list of **Cheeps** that they have posted.

The **Cheep** entity represents post authored by a user. It contains a unique identifier `CheepId`, a foreign key `AuthorId` referencing the Author, the content of the post in the `Text` field (limited to 160 characters), and a `Timestamp` indicating when the post was created. Each Cheep is associated with exactly one Author, establishing a **one-to-many relationship** between Authors and their Cheeps.

### Changes to the Domain Model

As of writing this report, it is not possible to follow other users, but when this feature is implemented, the domain model will be extended to include functionality for **user relationships**.

This change introduces a new entity: **Follower**, which enables a **many-to-many relationship** between Authors.

The **Follower** entity will include:

- `Id`: The unique identifier for the relationship.
- `FollowerId`: A reference to the Author who is following another Author.
- `FolloweeId`: A reference to the Author being followed.

Each Author will gain two new collections:

- `Followers`: A list of **Follower** entities representing the Authors who are following them.
- `Following`: A list of **Follower** entities representing the Authors they are following.

This change will enable the system to track follower-followee relationships while maintaining data integrity. The **Follower** entity will act as a bridge table, ensuring scalability and flexibility for managing user relationships. Once this functionality is completed, the domain model will support features like displaying followers, private timelines, and more.

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

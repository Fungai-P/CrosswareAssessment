# Likes API -- Local Setup

## Prerequisites

-   .NET 8 SDK
-   Docker (with Docker Compose)

------------------------------------------------------------------------

## 1. Start Dependencies

From the project root (where `docker-compose.yml` is located):

    docker compose up -d

This will start: - MongoDB (port 27017) - Redis (port 6379)

------------------------------------------------------------------------

## 2. Run the API

From the project directory:

    dotnet restore
    dotnet run

Once running, open Swagger:

    http://localhost:<port>/swagger

------------------------------------------------------------------------

## 3. Using the X-User-Id Header

Swagger is configured to use a demo authentication header:

    X-User-Id

Click **Authorize** in Swagger and enter any string (e.g. `user-1`).

⚠️ Important: To create unique likes for the same post, you must change
the `X-User-Id` value.\
Each unique user ID can like a post once.

Example: - `user-1` → like count becomes 1\
- `user-1` again → count stays 1 (duplicate prevented)\
- `user-2` → like count becomes 2

This is enforced by a unique MongoDB index on `(PostId, UserId)`.

------------------------------------------------------------------------

## 4. Stop Services

    docker compose down

To remove database data:

    docker compose down -v

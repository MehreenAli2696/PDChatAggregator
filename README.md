# ChatAggregator

This is a .NET 8 + ReactJS application that provides chat room interface in which the user can view chat history at varying levels of time-based aggregation in a given time period. It uses an in memory load of chat events that can be replaced with a Database fetch.

## Features

- View chat events aggregated by minute, hour or none in descending chronological order.
- Events types supported: "enter-the-room", "leave-the-room", "comment", and "high-five-another-user".
- Built using Clean Architecture principles.
- Unit testing 

## Project Structure

- `ChatAggregator.Domain`: This project contains the business entities related to the chat functionality.
- `ChatAggregator.Application`: This project contains the business logic and use cases of the system. It depends on the `Domain` project.
- `ChatAggregator.Infrastructure`: This project contains chat history fetching logic in descending chronological order. Right now it simply loads it from memory but it can be modified to plugin Database service and chat events can be loaded from there. It depends on `Application` project
- `ChatAggregator.Web.Server`: This is the Web API project. It depends on the `Application` and `Infrastructure` projects.
- `ChatAggregator.Web.client`: This is the UI project. Which uses `ChatAggregator.Web.Server` public APIs to fetch aggregated chat. 

## How to Run

1. Clone the repository.
2. Open the solution file (.sln) in Visual Studio (2022 recommended).
3. Build the solution (Build -> Build Solution).
4. After running the `src\ChatAggregator.Web` project, use the endpoint `GET /ChatAggregator?granularity=xxx&startTime=yyy-mm-dd+hh:mm:ms&endTime=yyy-mm-dd+hh:mm:ms'` where `xxx` can be "None", "Minute" or "Hour" and time interval of `startTime` and `endTime` can be in format `yyy-mm-dd+hh:mm:ms` to fetch the chat history aggregated at the specified level and interval.
5. For UI, open command prompt at the project directory of `ChatAggregator.Web.client`
6. Run commands `npm install`
   `npm run build`
   `npm run dev` 
7. Navigate to the local endpoint shown on command prompt. It is configured to be `https://localhost:5173/`

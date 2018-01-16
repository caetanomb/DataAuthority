# DataAuthority
Data Authority API - Request content of endpoint A and B are stored, diff-ed and the result is available throught a third endpoint

## Context

There are 3 endpoints Asp.net Web Api phisycally independent so that them can be scale out.
  - Endpoint A - Store request contents into Microsoft Sql LocalDb
  - Endpoint B - Store request contents into Microsoft Sql LocalDb
  - Endpoint C - Read the differ result from Microsoft Sql LocalDb
### Business rules
 The results shall provide the following info in JSON format
  - If Endpoint A's content equal Endpoint B's content return that
  - If contents from A and B not of equal size just return that
  - If contents of same size provide insight in where the diffs are, actual diffs are not needed.
    - So mainly offsets + length in the data
### Non-functional requirements
  - Scalabiity
  - Solution written in C#
  - Internal logic shall be under unit test
  - Functionality shall be under integration test
  - Documentation in code
  - Clear and to the point readme on usage
### Architecure Overview
![alt tag](https://github.com/caetanomb/DataAuthority/blob/master/Architecture%20Overview.png)

### Design Patterns and principles
  - CQRS - Command Query Responsibility Segregation
  - Repository
  - Domain Model
  - DDD principles (Domain Event, rules kept inside Entities)
  - Inversion of Control
  - Dependency Injection
## Requirements to run

 - Visual Studio community 2017
 - .Net Core
 - Complemeting packages are downloaded in first build
 

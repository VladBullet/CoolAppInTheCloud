# CoolAppInTheCloud
HMH interview exercise

**The code can run on localhost.**

**Run the API (dotnet run) and make sure that the rootUrl in *CoolAppInTheCloud.UI/api.js* has the same *port* as the API's instance** 

**Open the index.html page in a browser. Have fun trying to find bugs 😅**

These features work for now: 
- Login
- Logout
- Show people list
- Search/filter people
- Add person
- Delete person

### Website preview: 
Login:
![loginImage](https://github.com/VladBullet/CoolAppInTheCloud/blob/master/GitHub_Resources/login.JPG?raw=true)

Main content:
![contentImage](https://github.com/VladBullet/CoolAppInTheCloud/blob/master/GitHub_Resources/content.JPG?raw=true)

Search: *Quick tip: If you hit search with an empty input, it will return the whole list.*  
![image](https://github.com/VladBullet/CoolAppInTheCloud/blob/master/GitHub_Resources/search.JPG?raw=true)

Add person: 
![image](https://github.com/VladBullet/CoolAppInTheCloud/blob/master/GitHub_Resources/addPerson.JPG?raw=true)

Delete person: *(known bug -> if you delete the person, the table doesn't refresh yet, so please refresh the page to see the changes)*
![image](https://github.com/VladBullet/CoolAppInTheCloud/blob/master/GitHub_Resources/delete.JPG?raw=true)



# Requirements
## Overview of the exercise
Here are some hints in terms of what we are looking for:
- Some code, but not overly complex
- Choice of tooling and frameworks if necessary
- Architectural thinking (Cloud infrastructure and code).
You're not expected to run the code in an actual cloud environment as part of the exercise, but demonstrate your thoughts and process around cloud development as far as this point is concerned
Your exercise will be reviewed before our interview and we can have a walkthrough together so you can explain your solution.

## Exercise instructions
Hey! I hear you're good with computers. You know, I actually have an idea for an app. You should definitely create it - we can split the profits!

Your mission (should you accept it)
You see, I have this list of people. They're all in this text file. I've collected names, ages, and what they do. And more! But I have too many people now. I can't keep control of them anymore just using notepad. I need this to be an app! In the CLOUD!

You should be able to come up with something in a few hours, right? I've googled up some made-up words to help you get started:
- frontend
- typescript
- backend
- dotnet

plan for a database, but don't implement. In memory database/mock database is OK.
 
I have no idea what these words mean, but my app should let me see all the people I've collected in a list. There are a lot of people, so I need to be able to find them quickly. And let me add new ones of course! Gotta catch 'em all. :D
Here's my list of people (don't share this with anyone). *(This list was much bigger. I only included 2 people in this readme as an example)*

| Name | identifies as | age | city | country | occupation | favorite food | shoe size | hair color | hair color (real) | eye color | watch brand | cell phone brand | favorite drink | have they ever been in Kristiansand | do they like baguettes | coffee from glass or cup |
| ------------------:| ------------------:| ------------------:| ------------------:| ------------------:| ------------------:| ------------------:| ------------------:| ------------------:| ------------------:| ------------------:| ------------------:| ------------------:| ------------------:| ------------------:| ------------------:| ------------------:| 
| Chloe Miller | female | 26 | Sydney | Australia | graphic designer | Sushi, Pizza | 8 | brown | brown | brown | Michael Kors | Samsung | Margarita | No | Yes | Cup |
| Dylan Price | male | 34 | San Francisco | USA | photographer | Pizza, Apple | 11 | blonde | blonde | blue | Rolex | Gin and Tonic | Pasta | Yes | No | Glass |

# Exercise solution
My choice of tooling, frameworks and architecture : 
- .net Core 6.0 (long term support)
- Docker 
- EntityFrameworkCore (Code First Approach) -> I planed for a database, meaning I created a DbContext and the 2 models used. I didn't run the migrations knowing that I will only use the Mock database.
- Passwords were hashed with MD5 to keep them secure.
- Architectural design -> I chose to keep frontend and backend separated. I implemented an API secured with JWT authentication, and a frontend website. 
Initially i wanted my frontend to be React with Typescript, but i didn't manage to remember the syntax as fast as I thought , so I switched to the good old stack (HTML, Javascript & JQuery, CSS)
 
 - The mock database is a singleton instance containing 2 lists : 
    - People list (read from a text file)
    - Users list

| Username | Password | Role |
| ------------- |:-------------:| :-----:|
| Admin | pass | admin |
| User | pass | regular user |

  Even though this website was simple and didn't require Roles, I implemented Policy authorization based on roles. In the controller we could use these policies or authorize based on roles ([Authorize(Roles="Admin")])

## Exercise walk-through
### Backend
  Firstly I started working on the backend. I chose how the models and architecture should look, implemented the "database", the services, controllers and the authentication and authorization.
  
  The API is based on a N-layer simple architecture. Controllers -> Services -> Data Layer (here I would have also implemented a repository pattern in order to be able to quickly switch between databases if necessary)
  
  The API's file structure can be seen bellow: 
  
  ![image](https://github.com/VladBullet/CoolAppInTheCloud/blob/master/GitHub_Resources/fileStructure.JPG?raw=true)
  
### Frontend
  When I finished with the backend I tried to quickly spin-up a React project, but beeing that I didn't remember the syntax as fast as I thought, I created another project with the simple approach (HTML, CSS and Javascript). 
  
  I kept the React project in the solution, in case I had more time to start working on it again.
  
  On the CSS side, I used bootstrap to quickly style the html page. 
  
  I used only one html page, where I show and hide the "modules" as I use them. 
  There is a login "module" and the "content" module. I also have a modal that pops up when you want to add a new Person in the database.
  In the content "module" there is a search bar. Here you could quickly filter the people list by any of the following: Name, City, Country, CellPhoneBrand, EyeColor, FavoriteDrink, HairColor, RealHairColor
   
   🐜💸 There is a known **bug** right now, when deleting a person, the table doesn't refresh yet.
  

## msa2022-backend
 
# Configuration Files
2 configuration files allow the developer to start the project with different variables or configurations. Stuff that we want in the production but maybe not in the development phase. In the development configuration file we can declare variables of the same key with a different value, this will allow developers to control the parameters they want to run the project with.

# Dependency Injections
Dependency Injection simplifies our code by reducing redundancy in our code, in my code I use it to inject HttpClient into my controllers with BaseAddress to the external api. This reduces duplicate code in my program and makes unit testing easier.

# Unit Testing
Unit Testing allow developers to have automatic testing in their projects, this is not only faster but is also way more convientent as I don't have to manual test every time I change my program. I can run these unit tests and see if all the tests still pass, and can let me know early in development when there is an error.

# Middleware Testing
Many middleware testing libraries make my code easier to test, as it has better support of exception. For example, FluentAssertions letting me more easily compare response types, espicially the Okay and NotFound Result. This made it more easier to implement as well as makes it easier to understand. 


-Vince Guan
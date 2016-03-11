# EF6-Generic
A Generic Entity Framework 6 SuperClass with CRUD operation for your Model classes

# What it does ?
This two classes allow your EF Models to have all the CRUD operations inherited from the base class (ModelEntity).
Class Methods: Your Class will have static methods for retrive, delete and update object from/to DataBase
Instance Methods: Your Object will have a Save() method to save it to the DataBase

# What I need to do ?
You just need to:
   - add the two files (IBaseEntity & ModelEntity) into your project
   - adjust the namespaces of the files
   - in ModelEntity adjust the name of the Context class (The one that inherits from DbContext)
   - your model classes need to inherits from ModelEntity and implements IBaseEntity (see example below)

# Example
As we live/die by examples, here's one:

## Model class for User
```C#
public class User : ModelEntity<User>, IBaseEntity
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public int Age { get; set; }
}
```

## Usage of the User class
```C#
User usr = new User();
usr.Name = "Homer Simpson" ;
usr.Age = 36;
usr.Save(); // that's all, the object will be saved

// how to retrieve objects 
var specificUser = User.Get(1); // the User hows id is 1
var allUsers = User.GetAll() ; // list them all
var allUsersAsync = User.GetAllAsyc(); // same but async

```

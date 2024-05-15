# Lab02_UnitTesting

# CarFactoryAPI Tests

## OwnersServiceTests

This repository contains unit tests for the OwnersService class in the CarFactoryAPI project. The OwnersService class handles operations related to buying cars for owners.

### Table of Contents
1. [Introduction](#introduction)
2. [Project Structure](#project-structure)
3. [Test Descriptions](#test-descriptions)
4. [Setup Instructions](#setup-instructions)
5. [Contributing](#contributing)

### Introduction
The OwnersService class in the CarFactoryAPI project is responsible for managing the buying process of cars for owners. This includes checking car availability, owner existence, sufficient funds, and assigning cars to owners.

This repository contains unit tests written using xUnit and Moq to ensure the functionality of the OwnersService class is working as expected.

### Project Structure
The project is structured as follows:

- OwnersServiceTests.cs: Contains unit tests for the OwnersService class.

### Test Descriptions

**BuyCar_CarNotExist_NotExist**
Verifies that the correct error message is returned when attempting to buy a car that doesn't exist.

**BuyCar_CarNotExist_NotExist2**
Another test to verify the behavior of attempting to buy a non-existing car.

**BuyCar_CarwithOwner_Sold**
Verifies that the correct error message is returned when attempting to buy a car that has already been sold.

**BuyCar_OwnerNotExist_NotExist**
Verifies that the correct error message is returned when attempting to buy a car for a non-existing owner.

**BuyCar_OwnerAlreadHaveCar_AlreadHaveCar**
Verifies that the correct error message is returned when attempting to buy a car for an owner who already has a car.

**BuyCar_CarPrice_Insufficientfunds**
Verifies that the correct error message is returned when an owner has insufficient funds to buy a car.

**BuyCar_AssignToOwner_SomethingWentWrong**
Verifies that the correct error message is returned when something unexpected happens during the process of assigning a car to an owner.

**BuyCar_AssignToOwner_Successfull**
Verifies that a car is successfully bought and assigned to an owner.

### Setup Instructions
To run these tests locally, follow these steps:

1. Clone this repository to your local machine.
2. Open the solution in Visual Studio or your preferred IDE.
3. Build the solution to restore dependencies.
4. Run the tests using the test runner in your IDE.

### Contributing
Contributions are welcome! If you have any ideas, suggestions, or improvements, feel free to open an issue or create a pull request.

---

## CarRepositoryTests

This repository contains unit tests for the CarRepository class in the CarFactoryAPI project. The CarRepository class handles operations related to car data persistence.

### Table of Contents
1. [Introduction](#introduction)
2. [Project Structure](#project-structure)
3. [Test Descriptions](#test-descriptions)
4. [Setup Instructions](#setup-instructions)
5. [Contributing](#contributing)

### Introduction
The CarRepository class in the CarFactoryAPI project is responsible for database operations related to cars, such as fetching car data, adding new cars, assigning cars to owners, and removing cars.

This repository contains unit tests written using xUnit and Moq.EntityFrameworkCore to ensure the functionality of the CarRepository class is working as expected.

### Project Structure
The project is structured as follows:

- CarRepositoryTests.cs: Contains unit tests for the CarRepository class.

### Test Descriptions

**GetCarById_AskForCarId10_CarObject**
Verifies that a car object is returned when requesting a car by its ID.

**GetCarById_AskForCarId50_CarObjectNull**
Verifies that null is returned when requesting a car with a non-existing ID.

**GetAllCar_AskForCars_CarsObjectsNotEmpty**
Verifies that a list of car objects is returned and not empty.

**GetAllCar_AskForCars_CarsObjectsEmpty**
Verifies that an empty list is returned when there are no cars in the database.

**AddCar_ValidCar_CarAddedSuccessfully**
Verifies that a car is added successfully to the database.

**AssignToOwner_ValidCarandOwner_True**
Verifies that a car is successfully assigned to an owner.

**AssignToOwner_ValidCarandOwner_false**
Verifies that a car cannot be assigned to an owner if it doesn't exist.

**Remove_ExistingCar_CarRemovedSuccessfully**
Verifies that a car is removed successfully from the database.

**Remove_ExistingCar_CarRemovedfail**
Verifies that a car cannot be removed from the database if it doesn't exist.

### Setup Instructions
To run these tests locally, follow these steps:

1. Clone this repository to your local machine.
2. Open the solution in Visual Studio or your preferred IDE.
3. Build the solution to restore dependencies.
4. Run the tests using the test runner in your IDE.

### Contributing
Contributions are welcome! If you have any ideas, suggestions, or improvements, feel free to open an issue or create a pull request.

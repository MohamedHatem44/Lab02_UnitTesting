using CarAPI.Entities;
using CarFactoryAPI.Entities;
using CarFactoryAPI.Repositories_DAL;
using Moq;
using Moq.EntityFrameworkCore;

namespace CarFactoryAPI_Tests
{
    public class CarRepositoryTests
    {
        /*-----------------------------------------------------------------*/
        #region Fields
        // Create Mock of Dependencies
        Mock<FactoryContext> contextMock;

        // use fake object as dependency
        CarRepository carRepository;
        #endregion
        /*-----------------------------------------------------------------*/
        #region Ctor
        public CarRepositoryTests()
        {
            // Create Mock of Dependencies
            contextMock = new();

            // use fake object as dependency
            carRepository = new(contextMock.Object);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 01 NotNull
        [Fact]
        public void GetCarById_AskForCarId10_CarObject()
        {
            // Arrange
            // Build the mock data
            List<Car> cars = new List<Car>() 
            {
                new Car() { Id = 10 },
                new Car() { Id = 20 },
                new Car() { Id = 30 }
            };

            // setup called Dbsets
            contextMock.Setup(o => o.Cars).ReturnsDbSet(cars);

            // Act
            Car result = carRepository.GetCarById(10);

            // Assert
            Assert.NotNull(result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 02 Null
        [Fact]
        public void GetCarById_AskForCarId50_CarObjectNull()
        {
            // Arrange
            // Build the mock data
            List<Car> cars = new List<Car>()
            {
                new Car() { Id = 10 },
                new Car() { Id = 20 },
                new Car() { Id = 30 }
            };

            // setup called Dbsets
            contextMock.Setup(o => o.Cars).ReturnsDbSet(cars);

            // Act
            Car result = carRepository.GetCarById(50);

            // Assert
            Assert.Null(result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 03 NotEmpty
        [Fact]
        public void GetAllCar_AskForCars_CarsObjectsNotEmpty()
        {
            // Arrange
            // Build the mock data
            List<Car> cars = new List<Car>()
            {
                new Car() { Id = 10 },
                new Car() { Id = 20 },
                new Car() { Id = 30 }
            };

            // setup called Dbsets
            contextMock.Setup(o => o.Cars).ReturnsDbSet(cars);

            // Act
            var result = carRepository.GetAllCars();

            // Assert
            Assert.NotEmpty(result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 04 Empty
        [Fact]
        public void GetAllCar_AskForCars_CarsObjectsEmpty()
        {
            // Arrange
            // Build the mock data
            List<Car> cars = new List<Car>();

            // setup called Dbsets
            contextMock.Setup(o => o.Cars).ReturnsDbSet(cars);

            // Act
            var result = carRepository.GetAllCars();

            // Assert
            Assert.Empty(result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 05 AddCar_Successful
        [Fact]
        public void AddCar_ValidCar_CarAddedSuccessfully()
        {
            // Arrange
            Car car = new Car() { Id = 40 };
            var cars = new List<Car>();

            // setup called Dbsets
            contextMock.Setup(c => c.Cars).ReturnsDbSet(cars);

            // Act
            bool result = carRepository.AddCar(car);

            // Assert
            Assert.True(result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 06 AssignToOwner_Successful
        [Fact]
        public void AssignToOwner_ValidCarandOwner_True()
        {
            // Arrange
            int carId = 1;
            int ownerId = 10;

            // Create a mock car
            Car car = new Car() { Id = carId };

            // setup called Dbsets
            var cars = new List<Car>() { car };
            contextMock.Setup(c => c.Cars).ReturnsDbSet(cars);

            // Act
            bool result = carRepository.AssignToOwner(carId, ownerId);

            // Assert
            Assert.True(result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 07 AssignToOwner_false
        [Fact]
        public void AssignToOwner_ValidCarandOwner_false()
        {
            // Arrange
            int car1Id = 1;
            int car2Id = 2;
            int ownerId = 10;

            // Create a mock car
            Car car1 = new Car() { Id = car1Id };
            Car car2 = new Car() { Id = car2Id };

            // setup called Dbsets
            var cars = new List<Car>() { car2 };
            contextMock.Setup(c => c.Cars).ReturnsDbSet(cars);

            // Act
            bool result = carRepository.AssignToOwner(car1Id, ownerId);

            // Assert
            Assert.False(result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 08 Remove_Successful
        [Fact]
        public void Remove_ExistingCar_CarRemovedSuccessfully()
        {
            // Arrange
            int carId = 1;

            // Create a mock car
            Car car = new Car() { Id = carId };

            // setup called Dbsets
            var cars = new List<Car>() { car };
            contextMock.Setup(c => c.Cars).ReturnsDbSet(cars);

            // Act
            bool result = carRepository.Remove(carId);

            // Assert
            Assert.True(result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 09 Remove_fail
        [Fact]
        public void Remove_ExistingCar_CarRemovedfail()
        {
            // Arrange
            int car1Id = 1;
            int car2Id = 2;

            // Create a mock car
            Car car1 = new Car() { Id = car1Id };
            Car car2 = new Car() { Id = car2Id };

            // setup called Dbsets
            var cars = new List<Car>() { car2 };
            contextMock.Setup(c => c.Cars).ReturnsDbSet(cars);

            // Act
            bool result = carRepository.Remove(car1Id);

            // Assert
            Assert.False(result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
    }
}

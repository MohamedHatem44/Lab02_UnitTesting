using CarAPI.Entities;
using CarAPI.Models;
using CarAPI.Payment;
using CarAPI.Repositories_DAL;
using CarAPI.Services_BLL;
using CarFactoryAPI.Entities;
using CarFactoryAPI.Repositories_DAL;
using Moq;
using Xunit.Abstractions;

namespace CarFactoryAPI_Tests
{
    public class OwnersServiceTests : IDisposable
    {
        /*-----------------------------------------------------------------*/
        #region Fields
        private readonly ITestOutputHelper testOutputHelper;

        // Create Mock Of Dependencies
        Mock<ICarsRepository> carRepoMock;
        Mock<IOwnersRepository> OwnerRepoMock;
        Mock<ICashService> cashMock;

        // Use fake object as a dependency
        OwnersService ownersService;
        #endregion
        /*-----------------------------------------------------------------*/
        #region Ctor
        public OwnersServiceTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
            // test setup
            testOutputHelper.WriteLine("Test setup");
            // Create Mock Of Dependencies
            carRepoMock = new();
            OwnerRepoMock = new();
            cashMock = new();

            // use fake object as a dependency
            ownersService = new OwnersService(
                carRepoMock.Object, OwnerRepoMock.Object, cashMock.Object);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Dispose
        public void Dispose()
        {
            // Test clean up
            testOutputHelper.WriteLine("Test clean up");
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 01 Car doesn't exist
        [Fact(Skip = "Fail due to fail in dependencies working on isolating Unit")]
        [Trait("Author", "Hatem")]
        public void BuyCar_CarNotExist_NotExist()
        {

            // Arrange
            FactoryContext factoryContext = new FactoryContext();

            CarRepository carRepository = new CarRepository(factoryContext);
            OwnerRepository ownerRepository = new OwnerRepository(factoryContext);
            CashService cashService = new CashService();

            OwnersService ownersService = new OwnersService(carRepository, ownerRepository, cashService);

            BuyCarInput buyCarInput = new() { CarId = 100, OwnerId = 10, Amount = 5000 };

            // Act
            string result = ownersService.BuyCar(buyCarInput);

            // Assert
            Assert.Contains("Car doesn't exist", result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 02 Car doesn't exist
        [Fact]
        [Trait("Author", "Hatem")]
        public void BuyCar_CarNotExist_NotExist2()
        {
            testOutputHelper.WriteLine("Test 2");

            // Arrange
            BuyCarInput buyCarInput = new() { CarId = 200, OwnerId = 20, Amount = 5000 };

            // Act
            string result = ownersService.BuyCar(buyCarInput);

            // Assert
            Assert.Contains("Car doesn't exist", result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 03 Already sold
        [Fact]
        [Trait("Author", "Hatem")]
        public void BuyCar_CarwithOwner_Sold()
        {
            testOutputHelper.WriteLine("Test 3");

            // Build the mock Data
            Car car = new Car() { Id = 10, Price = 1000, OwnerId = 5, Owner = new Owner() };

            // Setup the called methods
            carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);

            BuyCarInput carInput = new() { CarId = 10, OwnerId = 10, Amount = 1000 };

            // Act
            string result = ownersService.BuyCar(carInput);

            // Assert
            Assert.Contains("Already sold", result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 04 Owner doesn't exist
        [Fact]
        [Trait("Author", "Hatem")]
        public void BuyCar_OwnerNotExist_NotExist()
        {
            testOutputHelper.WriteLine("Test 4");

            // Arrange

            // Build the mock Data
            Car car = new Car() { Id = 10, Price = 1000 };
            Owner owner = null!;

            // Setup called methods
            carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            OwnerRepoMock.Setup(o => o.GetOwnerById(100)).Returns(owner!);

            BuyCarInput carInput = new() { CarId = 10, OwnerId = 100, Amount = 1000 };

            // Act
            string result = ownersService.BuyCar(carInput);

            // Assert
            Assert.Contains("Owner doesn't exist", result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 05 Already have car
        [Fact]
        [Trait("Author", "Hatem")]
        public void BuyCar_OwnerAlreadHaveCar_AlreadHaveCar()
        {
            testOutputHelper.WriteLine("Test 5");

            // Arrange

            // Build the mock Data
            Car car = new Car() { Id = 10, Price = 1000 };
            Owner owner = new Owner() { Id = 10, Name = "Hatem", Car = car };

            // Setup called methods
            carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            OwnerRepoMock.Setup(o => o.GetOwnerById(10)).Returns(owner);

            BuyCarInput carInput = new() { CarId = 10, OwnerId = 10, Amount = 1000 };

            // Act
            string result = ownersService.BuyCar(carInput);

            // Assert
            Assert.Contains("Already have car", result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 06 Insufficient funds
        [Fact]
        [Trait("Author", "Hatem")]
        public void BuyCar_CarPrice_Insufficientfunds()
        {
            testOutputHelper.WriteLine("Test 6");

            // Arrange

            // Build the mock Data
            Car car = new Car() { Id = 10, Price = 2000 };
            Owner owner = new Owner() { Id = 10, Name = "Hatem" };

            // Setup called methods
            carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            OwnerRepoMock.Setup(o => o.GetOwnerById(10)).Returns(owner);

            BuyCarInput carInput = new() { CarId = 10, OwnerId = 10, Amount = 1000 };

            // Act
            string result = ownersService.BuyCar(carInput);

            // Assert
            Assert.Contains("Insufficient funds", result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 07 Something went wrong
        [Fact]
        [Trait("Author", "Hatem")]
        public void BuyCar_AssignToOwner_SomethingWentWrong()
        {
            testOutputHelper.WriteLine("Test 7");

            // Arrange

            // Build the mock Data
            Car car = new Car() { Id = 10, Price = 1000 };
            Owner owner = new Owner() { Id = 20, Name = "Hatem" };

            // Setup called methods
            carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            OwnerRepoMock.Setup(o => o.GetOwnerById(20)).Returns(owner);
            cashMock.Setup(o => o.Pay(1000)).Returns("Successfull");
            // Set up AssignToOwner to return false
            carRepoMock.Setup(c => c.AssignToOwner(10, 20)).Returns(false);

            BuyCarInput carInput = new() { CarId = 10, OwnerId = 20, Amount = 1000 };

            // Act
            string result = ownersService.BuyCar(carInput);

            // Assert
            Assert.Contains("Something went wrong", result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
        #region Test 08 Successfull
        [Fact]
        [Trait("Author", "Hatem")]
        public void BuyCar_AssignToOwner_Successfull()
        {
            testOutputHelper.WriteLine("Test 7");

            // Arrange

            // Build the mock Data
            Car car = new Car() { Id = 10, Price = 1000 };
            Owner owner = new Owner() { Id = 20, Name = "Hatem" };

            // Setup called methods
            carRepoMock.Setup(o => o.GetCarById(10)).Returns(car);
            OwnerRepoMock.Setup(o => o.GetOwnerById(20)).Returns(owner);
            cashMock.Setup(o => o.Pay(1000)).Returns("Successfull");
            // Set up AssignToOwner to return false
            carRepoMock.Setup(c => c.AssignToOwner(10, 20)).Returns(true);

            BuyCarInput carInput = new() { CarId = 10, OwnerId = 20, Amount = 1000 };

            // Act
            string result = ownersService.BuyCar(carInput);

            // Assert
            Assert.Contains("is bought by", result);
        }
        #endregion
        /*-----------------------------------------------------------------*/
    }
}

using System;
using System.Collections.Generic;
using Application.UserCase.Hoodie;
using Application.UserCase.Music;
using AutoMapper;
using Domain;
using Infrastructure.SqlServer.Repositories.Hoodies;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Profile;
using Xunit;

namespace UnitTests
{
    public class HoodieControllerTest:IDisposable
    {
        Mock<IHoodiesRepository> mockRepo;
        HoodieProfile realProfile;
        MapperConfiguration configuration; 
        IMapper mapper;

        public HoodieControllerTest()
        {
            mockRepo = new Mock<IHoodiesRepository>();
            realProfile = new HoodieProfile();
            configuration = new MapperConfiguration(cfg => cfg.AddProfile(realProfile));
            mapper = new Mapper(configuration);
        }
        public void Dispose()
        {
            mockRepo = null;
            mapper = null;
            configuration = null;
            realProfile = null;
        }
          //**************************
        //GET api/Hoodie   Unit Tests
        //**************************
        
        //Test 1.1

        [Fact]
        public void GetAllHoodie_ReturnZeroResources_WhenDBIsEmpty()
        {
            //Arrange 
            mockRepo.Setup(repo =>
                repo.GetAllHoodies()).Returns(GetHoodies(0));
            var controller = new HoodieController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetAllHoodies();
            
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        
        // Test 1.2
        [Fact]
        public void GetAllHoodie_ReturnOneResources_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllHoodies()).Returns(GetHoodies(1));

            var controller = new HoodieController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetAllHoodies();
            
            //Assert
            var okResult = result.Result as OkObjectResult;
            var hoodies = okResult.Value as List<HoodieReadDto>;
            Assert.Single(hoodies);
        }
        
        //Test 1.3
        [Fact]
        public void GetAllHoodie_Return200_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllHoodies()).Returns(GetHoodies(1));
            var controller = new HoodieController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetAllHoodies();
            
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        
        //Test 1.4
        [Fact]
        public void GetAllHoodie_ReturnCorrectType_WhenDBHasOneResource()
        {
            //Arrange 
            mockRepo.Setup(repo => repo.GetAllHoodies()).Returns(GetHoodies(1));
            var controller = new HoodieController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetAllHoodies();
            
            //Assert
            Assert.IsType<ActionResult<IEnumerable<HoodieReadDto>>>(result);
        }
        
        //*********************************
        //GET   api/Hoodie/{id} Unit Test
        //*********************************
        
        //Test 2.1
        [Fact]
        public void GetHoodieById_Returns404_WhenNonIdProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetHoodieById(Guid.NewGuid())).Returns(() => null);
            var controller = new HoodieController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetHoodieById(Guid.NewGuid());
            
            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        //Test 2.2
        [Fact]
        public void GetHoodieById_Returns200_WhenValidIdProvided()
        {
            //Arrange 
            mockRepo.Setup(repo => repo.GetHoodieById(new Guid())).Returns(new Hoodies()
            {
                Id=Guid.NewGuid(),
                Price =12,
                Name = "pull",
                Description = "super",
                Size = 12,
                Color = "blue"
            });
            var controller = new HoodieController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetHoodieById(new Guid());
            
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        
        //Test 2.3
        [Fact]
        public void GetHoodieById_ReturnsCorrectResourceType_WhenValidIdProvided()
        {
            //Arrange 
            mockRepo.Setup(repo => repo.GetHoodieById(Guid.NewGuid())).Returns(new Hoodies
            {
                Id=Guid.NewGuid(),
                Price =12,
                Name = "pull",
                Description = "super",
                Size = 12,
                Color = "blue"
            });
            var controller = new HoodieController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetHoodieById(Guid.NewGuid());
            
            //Assert
            Assert.IsType<ActionResult<HoodieReadDto>>(result);
        }
        //******************************
        // POST   api/Hoodie  Unit Test
        //******************************
        //Test 3.1
        [Fact]
        public void CreateHoodie_ReturnsCorrectResourceType_WhenValidSubmit()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetHoodieById(Guid.NewGuid())).Returns(new Hoodies()
            {
                Id=Guid.NewGuid(),
                Price =12,
                Name = "pull",
                Description = "super",
                Size = 12,
                Color = "blue"
            });
            var controller = new HoodieController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.CreateHoodie(new HoodieCreateDto() { });
            //Assert
            Assert.IsType<ActionResult<HoodieReadDto>>(result);
        }
        //Test 3.2
        [Fact]
        public void CreateHoodie_Returns200e_WhenValidSubmit()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetHoodieById(Guid.NewGuid())).Returns(new Hoodies()
            {
                Id=Guid.NewGuid(),
                Price =12,
                Name = "pull",
                Description = "super",
                Size = 12,
                Color = "blue"
            });
            var controller = new HoodieController(mockRepo.Object, mapper);
            //Act
            var result = controller.CreateHoodie(new HoodieCreateDto() { });
            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }
        
        //*******************************
        // PUT api/Hoodie/{id}
        //*******************************
        //Test 4.1
        [Fact]
        public void UpdateHoodie_Returns204_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetHoodieById(Guid.NewGuid())).Returns(new Hoodies()
            {
                Id=Guid.NewGuid(),
                Price =12,
                Name = "pull",
                Description = "super",
                Size = 12,
                Color = "blue"
            });
            var controller = new HoodieController(mockRepo.Object, mapper);
            //Act 
            var result = controller.UpdateHoodie(Guid.NewGuid(), new HoodieUpdateDto() { });
            //Assert
            Assert.IsType<NoContentResult>(result);

        }
        //Test 4.2
        [Fact]
        public void UpdateHoodie_Returns404_WhenNonExistingIdSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetHoodieById(Guid.NewGuid())).Returns(() => null);
            var controller = new HoodieController(mockRepo.Object, mapper);
            //Act 
            var result = controller.UpdateHoodie(Guid.NewGuid(), new HoodieUpdateDto() { });
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        //******************************
        //Delete  api/Hoodie/{id} Unit test
        //*******************************
        
        //Test 5.1
        [Fact]
        public void DeleteHoodie_Returns200_WhenValidIdSubmitted()
        {
            mockRepo.Setup(repo => repo.GetHoodieById(Guid.NewGuid())).Returns(new Hoodies()
            {
                Id=Guid.NewGuid(),
                Price =12,
                Name = "pull",
                Description = "super",
                Size = 12,
                Color = "blue"
            });
            var controller = new HoodieController(mockRepo.Object, mapper);
            //Act 
            var result = controller.DeleteHoodie(Guid.NewGuid());
            //Assert
            Assert.IsType<NoContentResult>(result);
        }
        //Test 5.2
        [Fact]
        public void DeleteHoodie_Returns404_WhenNonExistingIdSubmitted()
        {
            mockRepo.Setup(repo => repo.GetHoodieById(Guid.Empty)).Returns(() => null);
            var controller = new HoodieController(mockRepo.Object, mapper);
            //Act
            var result = controller.DeleteHoodie(Guid.Empty);
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        //*******************************
        // private Support Methods
        //*******************************
        private List<Hoodies> GetHoodies(int num)
        {
            var hoodie = new List<Hoodies>();
            if (num > 0)
            {
                hoodie.Add(new Hoodies
                    {
                        Id=Guid.NewGuid(),
                        Price =12,
                        Name = "pull",
                        Description = "super",
                        Size = 12,
                        Color = "blue"
                    }
                );
            }

            return hoodie;
        }

    }
}
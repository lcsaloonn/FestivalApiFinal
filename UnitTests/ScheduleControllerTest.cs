using System;
using System.Collections.Generic;
using Application.UserCase.Schedules;
using AutoMapper;
using Domain;
using Infrastructure.SqlServer.Repositories.Music;
using Infrastructure.SqlServer.Repositories.Schedules;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Profile;
using Xunit;

namespace UnitTests
{
    public class ScheduleControllerTest:IDisposable
    {
        
        Mock<IScheduleRepository> mockRepo;
        ScheduleProfile realProfile;
        MapperConfiguration configuration; 
        IMapper mapper;
        Random gen = new Random();

        public ScheduleControllerTest()
        {
            mockRepo = new Mock<IScheduleRepository>();
            realProfile = new ScheduleProfile();
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
        //GET api/Schedule   Unit Tests
        //**************************
        
        //Test 1.1

        [Fact]
        public void GetAllSchedule_ReturnZeroResources_WhenDBIsEmpty()
        {
            //Arrange 
            mockRepo.Setup(repo =>
                repo.GetAllSchedule()).Returns(GetSchedule(0));
            var controller = new ScheduleController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetAllSchedules();
            
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        
        // Test 1.2
        [Fact]
        public void GetAllSchedule_ReturnOneResources_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllSchedule()).Returns(GetSchedule(1));

            var controller = new ScheduleController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetAllSchedules();
            
            //Assert
            var okResult = result.Result as OkObjectResult;
            var shedules = okResult.Value as List<ScheduleReadDto>;
            Assert.Single(shedules);
        }
        //Test 1.3
        [Fact]
        public void GetAllSchedule_Return200_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllSchedule()).Returns(GetSchedule(1));
            var controller = new ScheduleController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetAllSchedules();
            
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        //Test 1.4
        [Fact]
        public void GetAllSchedule_ReturnCorrectType_WhenDBHasOneResource()
        {
            //Arrange 
            mockRepo.Setup(repo => repo.GetAllSchedule()).Returns(GetSchedule(1));
            var controller = new ScheduleController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetAllSchedules();
            
            //Assert
            Assert.IsType<ActionResult<IEnumerable<ScheduleReadDto>>>(result);
        }
        
        //*********************************
        //GET   api/Schedule/{id} Unit Test
        //*********************************
        
        //Test 2.1
        [Fact]
        public void GetScheduleById_Returns404_WhenNonIdProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetScheduleById(0)).Returns(() => null);
            var controller = new ScheduleController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetScheduleById(1);
            
            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        //Test 2.2
        [Fact]
        public void GetScheduleById_Returns200_WhenValidIdProvided()
        {
            //Arrange 
            mockRepo.Setup(repo => repo.GetScheduleById(1)).Returns(new Schedules
            {
                Id=1,
                ScheduleStart = RandomDay(),
                ScheduleEnd = RandomDay(),
            });
            var controller = new ScheduleController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetScheduleById(1);
            
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        
        //Test 2.3
        [Fact]
        public void GetScheduleById_ReturnsCorrectResourceType_WhenValidIdProvided()
        {
            //Arrange 
            mockRepo.Setup(repo => repo.GetScheduleById(1)).Returns(new Schedules
            {
                Id = 1,
                ScheduleStart = RandomDay(),
                ScheduleEnd = RandomDay(),
            });
            var controller = new ScheduleController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetScheduleById(1);
            
            //Assert
            Assert.IsType<ActionResult<ScheduleReadDto>>(result);
        }
        //******************************
        // POST   api/Schedule  Unit Test
        //******************************
        //Test 3.1
        [Fact]
        public void CreateSchedule_ReturnsCorrectResourceType_WhenValidSubmit()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetScheduleById(1)).Returns(new Schedules
            {
                Id = 1,
                ScheduleStart = RandomDay(),
                ScheduleEnd = RandomDay()
            });
            var controller = new ScheduleController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.CreateSchedule(new ScheduleCreateDto() { });
            //Assert
            Assert.IsType<ActionResult<ScheduleReadDto>>(result);
        }
        //Test 3.2
        [Fact]
        public void CreateSchedule_Returns200e_WhenValidSubmit()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetScheduleById(1)).Returns(new Schedules()
            {
                Id = 1,
                ScheduleStart = RandomDay(),
                ScheduleEnd = RandomDay()
            });
            var controller = new ScheduleController(mockRepo.Object, mapper);
            //Act
            var result = controller.CreateSchedule(new ScheduleCreateDto() { });
            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }
        
        //*******************************
        // PUT api/Schedule/{id}
        //*******************************
        //Test 4.1
        [Fact]
        public void UpdateSchedule_Returns204_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetScheduleById(1)).Returns(new Schedules
            {
                Id = 1,
                ScheduleStart = RandomDay(),
                ScheduleEnd = RandomDay()
            });
            var controller = new ScheduleController(mockRepo.Object, mapper);
            //Act 
            var result = controller.UpdateSchedule(1, new ScheduleUpdateDto() { });
            //Assert
            Assert.IsType<NoContentResult>(result);

        }
        //Test 4.2
        [Fact]
        public void UpdateSchedule_Returns404_WhenNonExistingIdSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetScheduleById(0)).Returns(() => null);
            var controller = new ScheduleController(mockRepo.Object, mapper);
            //Act 
            var result = controller.UpdateSchedule(0, new ScheduleUpdateDto() { });
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        
        //******************************
        //Delete  api/schedule/{id} Unit test
        //*******************************
        
        //Test 5.1
        [Fact]
        public void DeleteSchedule_Returns200_WhenValidIdSubmitted()
        {
            mockRepo.Setup(repo => repo.GetScheduleById(1)).Returns(new Schedules()
            {
                Id = 1,
                ScheduleStart = RandomDay(),
                ScheduleEnd = RandomDay()
            });
            var controller = new ScheduleController(mockRepo.Object, mapper);
            //Act 
            var result = controller.DeleteSchedule(1);
            //Assert
            Assert.IsType<NoContentResult>(result);
        }
        //Test 5.2
        [Fact]
        public void DeleteSchedule_Returns404_WhenNonExistingIdSubmitted()
        {
            mockRepo.Setup(repo => repo.GetScheduleById(0)).Returns(() => null);
            var controller = new ScheduleController(mockRepo.Object, mapper);
            //Act
            var result = controller.DeleteSchedule(0);
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        //*******************************
        // private Support Methods
        //*******************************
        private List<Schedules> GetSchedule(int num)
        {
            var schedules = new List<Schedules>();
            if (num > 0)
            {
                schedules.Add(new Schedules
                    {
                        Id=0,
                        ScheduleStart = RandomDay(),
                        ScheduleEnd = RandomDay(),
                    }
                );
            }

            return schedules;
        }
        
       
        DateTime RandomDay()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;           
            return start.AddDays(gen.Next(range));
        }
        
        
    }
}
using System;
using System.Collections.Generic;
using Application.UserCase.Music;
using AutoMapper;
using Domain;
using FluentAssertions;
using Infrastructure.SqlServer.Repositories.Music;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebAPI.Controllers;
using WebAPI.Profile;
using Xunit;

namespace UnitTests
{
    public class MusicControllerTest:IDisposable
    {
         Mock<IMusicRepository> mockRepo;
         MusicProfile realProfile;
        MapperConfiguration configuration; 
        IMapper mapper;

        public MusicControllerTest()
        {
            mockRepo = new Mock<IMusicRepository>();
            realProfile = new MusicProfile();
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
        //GET api/Music   Unit Tests
        //**************************
        
        //Test 1.1

        [Fact]
        public void GetAllMusics_ReturnZeroResources_WhenDBIsEmpty()
        {
            //Arrange 
            mockRepo.Setup(repo =>
                repo.GetAllMusic()).Returns(GetMusics(0));
            var controller = new MusicController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetAllMusics();
            
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        
        // Test 1.2
        [Fact]
        public void GetAllMusics_ReturnOneResources_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllMusic()).Returns(GetMusics(1));

            var controller = new MusicController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetAllMusics();
            
            //Assert
            var okResult = result.Result as OkObjectResult;
            var musics = okResult.Value as List<MusicReadDto>;
            Assert.Single(musics);
        }
        
        //Test 1.3
        [Fact]
        public void GetAllMusics_Return200_WhenDBHasOneResource()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetAllMusic()).Returns(GetMusics(1));
            var controller = new MusicController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetAllMusics();
            
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        
        //Test 1.4
        [Fact]
        public void GetAllMusics_ReturnCorrectType_WhenDBHasOneResource()
        {
            //Arrange 
            mockRepo.Setup(repo => repo.GetAllMusic()).Returns(GetMusics(1));
            var controller = new MusicController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetAllMusics();
            
            //Assert
            Assert.IsType<ActionResult<IEnumerable<MusicReadDto>>>(result);
        }
        
        //*********************************
        //GET   api/Music/{id} Unit Test
        //*********************************
        
        //Test 2.1
        [Fact]
        public void GetMusicById_Returns404_WhenNonIdProvided()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetMusicById(Guid.Empty)).Returns(() => null);
            var controller = new MusicController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetMusicById(Guid.NewGuid());
            
            //Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        //Test 2.2
        [Fact]
        public void GetMusicById_Returns200_WhenValidIdProvided()
        {
            //Arrange 
            mockRepo.Setup(repo => repo.GetMusicById(Guid.NewGuid())).Returns(new Music
            {
                Id = Guid.NewGuid(),
                Title = "thomas",
                Link = "/music/mucis",
            });
            var controller = new MusicController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetMusicById(Guid.NewGuid());
            
            //Assert
            Assert.IsType<OkObjectResult>(result.Result);
        }
        
        //Test 2.3
        [Fact]
        public void GetMusicById_ReturnsCorrectResourceType_WhenValidIdProvided()
        {
            //Arrange 
            mockRepo.Setup(repo => repo.GetMusicById(Guid.NewGuid())).Returns(new Music
            {
                Id = Guid.NewGuid(),
                Title = "Thomas",
                Link = "music/musics"
            });
            var controller = new MusicController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.GetMusicById(Guid.NewGuid());
            
            //Assert
            Assert.IsType<ActionResult<MusicReadDto>>(result);
        }
        //******************************
        // POST   api/Music  Unit Test
        //******************************
        //Test 3.1
        [Fact]
        public void CreateMusic_ReturnsCorrectResourceType_WhenValidSubmit()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetMusicById(Guid.NewGuid())).Returns(new Music
            {
                Id = Guid.NewGuid(),
                Title = "thomas",
                Link = "music/musics"
            });
            var controller = new MusicController(mockRepo.Object, mapper);
            
            //Act
            var result = controller.CreateMusic(new MusicCreateDto() { });
            //Assert
            Assert.IsType<ActionResult<MusicReadDto>>(result);
        }
        //Test 3.2
        [Fact]
        public void CreateMusic_Returns200e_WhenValidSubmit()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetMusicById(Guid.NewGuid())).Returns(new Music
            {
                Id = Guid.NewGuid(),
                Title = "thomas",
                Link = "music/musics"
            });
            var controller = new MusicController(mockRepo.Object, mapper);
            //Act
            var result = controller.CreateMusic(new MusicCreateDto() { });
            //Assert
            Assert.IsType<CreatedAtRouteResult>(result.Result);
        }
        //*******************************
        // PUT api/Music/{id}
        //*******************************
        //Test 4.1
        [Fact]
        public void UpdateMusic_Returns204_WhenValidObjectSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetMusicById(Guid.NewGuid())).Returns(new Music
            {
                Id = Guid.NewGuid(),
                Title = "thomas",
                Link = "music/musics"
            });
            var controller = new MusicController(mockRepo.Object, mapper);
            //Act 
            var result = controller.UpdateMusic(Guid.NewGuid(), new MusicUpdateDto { });
            //Assert
            Assert.IsType<NoContentResult>(result);

        }
        //Test 4.2
        [Fact]
        public void UpdateMusic_Returns404_WhenNonExistingIdSubmitted()
        {
            //Arrange
            mockRepo.Setup(repo => repo.GetMusicById(Guid.Empty)).Returns(() => null);
            var controller = new MusicController(mockRepo.Object, mapper);
            //Act 
            var result = controller.UpdateMusic(Guid.Empty, new MusicUpdateDto { });
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }

        //******************************
        //Delete  api/music/{id} Unit test
        //*******************************
        
        //Test 5.1
        [Fact]
        public void DeleteMusic_Returns200_WhenValidIdSubmitted()
        {
            mockRepo.Setup(repo => repo.GetMusicById(Guid.NewGuid())).Returns(new Music
            {
                Id = Guid.NewGuid(),
                Title = "thomas",
                Link = "music/musics"
            });
            var controller = new MusicController(mockRepo.Object, mapper);
            //Act 
            var result = controller.DeleteMusic(Guid.NewGuid());
            //Assert
            Assert.IsType<NoContentResult>(result);
        }
        //Test 5.2
        [Fact]
        public void DeleteMusic_Returns404_WhenNonExistingIdSubmitted()
        {
            mockRepo.Setup(repo => repo.GetMusicById(Guid.Empty)).Returns(() => null);
            var controller = new MusicController(mockRepo.Object, mapper);
            //Act
            var result = controller.DeleteMusic(Guid.Empty);
            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        

        //*******************************
        // private Support Methods
        //*******************************
        private List<Music> GetMusics(int num)
        {
            var musics = new List<Music>();
            if (num > 0)
            {
                musics.Add(new Music
                    {
                        Id=Guid.Empty,
                        Title = "thomas",
                        Link = "music/music"
                    }
                );
            }

            return musics;
        }

    }
}
using AutoMapper;
using CollegeProject.Controllers;
using CollegeProject.Models.Roles;
using CollegeProject.Repositories.Interfaces;
using CollegeProject.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollegeProject.Tests.Controllers
{
    public class RolesControllerTests
    {
        private readonly Mock<IRoleRepository> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly RolesController _controller;

        public RolesControllerTests()
        {
            _mockRepo = new Mock<IRoleRepository>();
            _mockMapper = new Mock<IMapper>();
            _controller = new RolesController(_mockRepo.Object, _mockMapper.Object);
        }
        [Fact]
        public void AddRole_TestCase()
        {
            var dto = new RolesDto { RoleName = "Test", Status = true }; 
            var role = new Roles { RoleName = "Test", Status = true };

            _mockMapper.Setup(m=>m.Map<Roles>(dto)).Returns(role);
            _mockRepo.Setup(r => r.CreateRoleAsync(role)).ReturnsAsync(new ApiResponse { Success = true, Message = "Role created successfully" });

            var result = _controller.CreateRole(dto).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var response = result.Value as ApiResponse;
            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.Equal("Role created successfully", response.Message);
        }
        [Fact]
        public void AddRole_DuplicateTestCase()
        {
            var dto = new RolesDto { RoleName="Admin",Status=true };
            var role = new Roles { RoleName = "Admin", Status = true };

            _mockMapper.Setup(m=>m.Map<Roles>(dto)).Returns(role);
            _mockRepo.Setup(r=>r.CreateRoleAsync(It.Is<Roles>(r=>r.RoleName == "Admin" && r.Status== true))).ReturnsAsync(new ApiResponse { Success = false, Message = "Role Already Exists. Please Check" });

            var result = _controller.CreateRole(dto).Result as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            var response = result.Value as ApiResponse;
            Assert.NotNull(response);
            Assert.False(response.Success);
            Assert.Equal("Role Already Exists. Please Check", response.Message);

        }
        public void AddRole_MultipleTestCase()
        {

        }
    }
}

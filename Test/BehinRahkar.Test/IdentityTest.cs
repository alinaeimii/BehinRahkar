using BehinRahkar.Test.abstraction;
using Identity.API.Controllers;
using Identity.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BehinRahkar.Test;
public class IdentityTest : Base
{
    [Fact]
    public async void Get_Token_Should_Return_Jwt_Token()
    {
        // Arrange
        var controller = new IdentityController();
        var userMock = new UserDTO { Username = "user1", Password = "123456user" };
        // Act
        var result = await controller.GetToken(userMock);
        var okResult = Assert.IsType<OkObjectResult>(result);
        var token = Assert.IsType<TokenDTO>(okResult.Value);

        //  Assert
        Assert.NotEmpty(token.Value);
    }



}

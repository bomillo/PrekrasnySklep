using Microsoft.Data.Sqlite;
using PrekrasnyDomainLayer.Context;
using PrekrasnyDomainLayer.Services;
using PrekrasnyDomainLayer.State;
using Tests.Util;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Tests;

public class UserServiceTests : IDisposable
{
    private readonly PrekrasnyContext context;
    private readonly UserService userService;

    private readonly InMemoryDbConecttion dbConnection = new();

    public UserServiceTests(ITestOutputHelper testOutputHelper)
    {
        this.context = new PrekrasnyContext(dbConnection.DbConnection);
        context.Database.EnsureCreated();

        AppState.SharedContext = context;
        AppState.CurrentUser = null;

        userService = new UserService(context);
    }

    public void Dispose()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }

    [Fact]
    public void CreatingNewUser()
    {
        string userName = "testUser";

        Assert.False(context.Users.Any(u => u.UserName == userName));

        userService.RegisterNewUser(userName, "testPassword");

        Assert.True(context.Users.Any(u => u.UserName == userName));
    }

    [Fact]
    public void LoginingIn()
    {
        string userName = "testUser";

        Assert.False(context.Users.Any(u => u.UserName == userName));
        Assert.Null(AppState.CurrentUser);

        userService.RegisterNewUser(userName, "testPassword");
        userService.Login(userName, "testPassword");

        Assert.True(context.Users.Any(u => u.UserName == userName));
        Assert.NotNull(AppState.CurrentUser);
    }

    [Fact]
    public void ModifyingCurrentUser()
    {
        string userName = "testUser";

        userService.RegisterNewUser(userName, "testPassword");
        userService.Login(userName, "testPassword");

        Assert.Equal(AppState.CurrentUser!.UserName, userName);
        Assert.False(context.Users.Any(u => u.UserName == "new" + userName));

        userService.ModifyCurrentUser(newUserName: "new" + userName);

        Assert.Equal(AppState.CurrentUser.UserName, "new" + userName);
        Assert.True(context.Users.Any(u => u.UserName == "new" + userName));

    }
}
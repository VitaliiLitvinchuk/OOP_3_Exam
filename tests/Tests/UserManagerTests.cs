using Application.Factories.Users;
using Application.Factories.Users.Abstractions;
using Application.Managers;
using Domain.Models.Roles;
using Domain.Models.Users;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Tests.Common;
using Tests.Data;
using Xunit;
using static Infrastructure.Persistence.DefaultDbData;

namespace Tests;

public class UserManagerTests(TestFactory factory) : BaseTest(factory), IAsyncLifetime
{
    private const string TestName = "user name";
    private readonly UserManager _userManager = factory.ServiceProvider.GetRequiredService<UserManager>();
    private readonly UserFactory _userFactory = factory.ServiceProvider.GetRequiredService<UserFactory>();
    private readonly GuestFactory _guestFactory = factory.ServiceProvider.GetRequiredService<GuestFactory>();
    private readonly AdminFactory _adminFactory = factory.ServiceProvider.GetRequiredService<AdminFactory>();

    private readonly List<Role> _roles = [];

    private User _user;

    [Theory]
    [InlineData(nameof(UserFactory))]
    [InlineData(nameof(GuestFactory))]
    [InlineData(nameof(AdminFactory))]
    public async Task ShouldCreateUser_FromFactory(string factoryName)
    {
        // Arrange
        var factory = Substitute.For<IUserFactory>();
        if (factoryName == nameof(UserFactory))
        {
            factory = _userFactory;
        }
        else if (factoryName == nameof(GuestFactory))
        {
            factory = _guestFactory;
        }
        else if (factoryName == nameof(AdminFactory))
        {
            factory = _adminFactory;
        }
        else
        {
            throw new ArgumentException($"Factory with name {factoryName} is not supported", nameof(factoryName));
        }

        // Act
        var user = await factory.CreateUser(TestName, CancellationToken.None);

        var createdUser = await _userManager.CreateUser(user, CancellationToken.None);

        // Assert
        createdUser.Id.Should().NotBeNull().And.Be(user.Id);
        createdUser.Name.Should().NotBeNull().And.Be(user.Name);
        createdUser.RoleId.Should().NotBeNull().And.Be(user.RoleId);
    }


    [Fact]
    public async Task ShouldCreateUser()
    {
        // Arrange
        var user = new User(UserId.New(), TestName, _roles.First(r => r.Name == nameof(User)).Id);

        // Act
        var createdUser = await _userManager.CreateUser(user, CancellationToken.None);

        // Assert
        createdUser.Id.Should().NotBeNull().And.Be(user.Id);
        createdUser.Name.Should().NotBeNull().And.Be(user.Name);
        createdUser.RoleId.Should().NotBeNull().And.Be(user.RoleId);
    }

    [Fact]
    public async Task ShouldUpdateUser()
    {
        // Arrange
        var user = new User(UserId.New(), TestName, _roles.First(r => r.Name == nameof(User)).Id);

        // Act
        await _userManager.CreateUser(user, CancellationToken.None);

        user.UpdateDetails("updated user name");

        await _userManager.UpdateUser(user, CancellationToken.None);

        var updatedUser = await _userManager.GetUserById(user.Id, CancellationToken.None);

        // Assert
        updatedUser!.Name.Should().Be(user.Name);
        updatedUser.RoleId.Should().Be(user.RoleId);
    }

    [Fact]
    public async Task ShouldUpdateUserRole()
    {
        // Arrange
        var user = new User(_user.Id, _user.Name, _roles.Last().Id);

        // Act
        var updatedUser = await _userManager.UpdateUserRole(user, CancellationToken.None);

        // Assert
        updatedUser.RoleId.Should().Be(user.RoleId);
    }

    [Fact]
    public async Task ShouldDeleteUser()
    {
        // Arrange
        var user = new User(UserId.New(), TestName, _roles.First(r => r.Name == nameof(User)).Id);

        // Act
        await _userManager.CreateUser(user, CancellationToken.None);
        await _userManager.DeleteUser(user, CancellationToken.None);
        var deletedUser = await _userManager.GetUserById(user.Id, CancellationToken.None);

        // Assert
        deletedUser.Should().BeNull();
    }

    [Fact]
    public async Task Should_ThrowException_WhenUserDoesNotExist()
    {
        // Arrange
        var user = new User(UserId.New(), TestName, _roles.First(r => r.Name == nameof(User)).Id);

        // Act
        Func<Task> act = async () => await _userManager.DeleteUser(user, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<DbUpdateConcurrencyException>(act);
    }

    public async Task DisposeAsync()
    {
        Context.Users.RemoveRange(Context.Users);
        Context.Roles.RemoveRange(Context.Roles);
        await SaveChangesAsync();
    }

    public async Task InitializeAsync()
    {
        _roles.AddRange(GetRoles());

        _user = UserData.CreateUser(_roles.First().Id);

        await Context.Roles.AddRangeAsync(_roles);
        await Context.Users.AddRangeAsync(_user);

        await SaveChangesAsync();
    }
}

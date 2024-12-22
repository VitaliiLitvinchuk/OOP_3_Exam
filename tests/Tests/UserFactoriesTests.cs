using Application.Factories.Users;
using Application.Managers;
using Domain.Models.Roles;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tests.Common;
using Xunit;
using static Infrastructure.Persistence.DefaultDbData;

namespace Tests;

public class UserFactoriesTests(TestFactory factory) : BaseTest(factory), IAsyncLifetime
{
    private readonly UserFactory _userFactory = factory.ServiceProvider.GetRequiredService<UserFactory>();
    private readonly GuestFactory _guestFactory = factory.ServiceProvider.GetRequiredService<GuestFactory>();
    private readonly AdminFactory _adminFactory = factory.ServiceProvider.GetRequiredService<AdminFactory>();

    private readonly List<Role> _roles = [];

    [Fact]
    public async Task ShouldCreateUser()
    {
        const string testName = "John Wick";

        // Arrange & Act
        var user = await _userFactory.CreateUser(testName, CancellationToken.None);

        // Assert
        user.Should().NotBeNull();
        user.Name.Should().Be(testName);

        user.RoleId.Should().Be(_roles.First(r => r.Name == Enum.GetName(typeof(Roles), Roles.User)).Id);
    }

    [Fact]
    public async Task ShouldCreateGuestUser()
    {
        const string testName = "John Wick";

        // Arrange & Act
        var user = await _guestFactory.CreateUser(testName, CancellationToken.None);

        // Assert
        user.Should().NotBeNull();
        user.Name.Should().Be(testName);
        user.RoleId.Should().Be(_roles.First(r => r.Name == Enum.GetName(typeof(Roles), Roles.Guest)).Id);
    }

    [Fact]
    public async Task ShouldCreateAdminUser()
    {
        const string testName = "John Wick";

        // Arrange & Act
        var user = await _adminFactory.CreateUser(testName, CancellationToken.None);

        // Assert
        user.Should().NotBeNull();
        user.Name.Should().Be(testName);
        user.RoleId.Should().Be(_roles.First(r => r.Name == Enum.GetName(typeof(Roles), Roles.Admin)).Id);
    }

    public async Task InitializeAsync()
    {
        _roles.AddRange(GetRoles());

        await Context.Roles.AddRangeAsync(_roles);

        await SaveChangesAsync();
    }

    public async Task DisposeAsync()
    {
        Context.Users.RemoveRange(Context.Users);
        Context.Roles.RemoveRange(Context.Roles);

        await SaveChangesAsync();
    }
}

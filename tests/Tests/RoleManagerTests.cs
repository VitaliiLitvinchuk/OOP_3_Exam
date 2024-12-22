using Application.Managers;
using Domain.Models.Roles;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tests.Common;
using Tests.Data;
using Xunit;

namespace Tests;

public class RoleManagerTests(TestFactory factory) : BaseTest(factory), IAsyncLifetime
{
    private readonly RoleManager _roleManager = factory.ServiceProvider.GetRequiredService<RoleManager>();
    private readonly Role _role = RoleData.Guest;

    [Fact]
    public async Task ShouldCreateRole()
    {
        // Arrange
        Role role = Role.New(RoleId.New(), "Admin");

        // Act
        await _roleManager.CreateRole(role, CancellationToken.None);

        // Assert
        var role1 = await Context.Roles.FirstAsync(r => r.Id == role.Id);

        role1.Name.Should().Be(role.Name);
        role1.Id.Should().Be(role.Id);
    }

    [Fact]
    public async Task Should_Not_CreateRole_With_ExistingName()
    {
        // Arrange
        Role role = Role.New(RoleId.New(), RoleData.Guest.Name);

        // Act
        Func<Task> act = async () => await _roleManager.CreateRole(role, CancellationToken.None);

        // Assert
        var exception = await Assert.ThrowsAsync<Exception>(act);

        exception.Message.Should().Be(RoleManager.ErrorMessages[1]);
    }

    public async Task DisposeAsync()
    {
        Context.RemoveRange(Context.Roles);

        await SaveChangesAsync();
    }

    public async Task InitializeAsync()
    {
        Context.Roles.Add(_role);

        await SaveChangesAsync();
    }
}
using System.Collections;
using Domain.Models.Users;
using Infrastructure.Persistence.Repositories.Abstractions.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository(ExamDbContext context) : IUserQueries, IUserRepository, IEnumerable<User>
{
    private readonly DbSet<User> _users = context.Users;

    public async Task<User> Create(User user, CancellationToken cancellationToken)
    {
        await _users.AddAsync(user, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<User> Delete(User user, CancellationToken cancellationToken)
    {
        _users.Remove(user);

        await context.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<User> Update(User user, CancellationToken cancellationToken)
    {
        _users.Update(user);

        await context.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<User?> GetUserById(UserId id, CancellationToken cancellationToken)
    {
        return await _users
            .Include(u => u.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<User>> GetUsers(CancellationToken cancellationToken)
    {
        return await _users
            .Include(u => u.Role)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public IEnumerator<User> GetEnumerator()
    {
        return _users.AsEnumerable().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

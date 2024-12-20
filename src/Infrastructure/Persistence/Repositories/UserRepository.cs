using Domain.Models.Users;
using Infrastructure.Persistence.Repositories.Abstractions.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository<T>(ExamDbContext context) : IUserQueries<T>, IUserRepository<T> where T : User
{
    private readonly DbSet<User> _users = context.Users;

    public async Task<T> Create(T user, CancellationToken cancellationToken)
    {
        await _users.AddAsync(user, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<T> Delete(T user, CancellationToken cancellationToken)
    {
        _users.Remove(user);

        await context.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<T> Update(T user, CancellationToken cancellationToken)
    {
        _users.Update(user);

        await context.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<T?> GetUserById(UserId id, CancellationToken cancellationToken)
    {
        return await _users.OfType<T>().FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<T>> GetUsers(CancellationToken cancellationToken)
    {
        return await _users.OfType<T>().ToListAsync(cancellationToken);
    }
}

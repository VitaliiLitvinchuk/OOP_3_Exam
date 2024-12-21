using Application.Loggers.Abstractions;
using Domain.Models.Users;
using Infrastructure.Persistence.Repositories.Abstractions.Users;

namespace Application.Managers;

public class UserManager
{
    public static Dictionary<ILogger.LogLevel, List<string>> Messages { get; } = new Dictionary<ILogger.LogLevel, List<string>>
    {
        { ILogger.LogLevel.Info, new List<string> {

            "Initializing user manager", "User manager initialized", // 0, 1 - UserManager.Initialize
            "Creating user", "User created", // 2, 3 - CreateUser
            "Updating user role", "User role updated", // 4, 5 - UpdateUserRole
            "Updating user", "User updated", // 6, 7 - UpdateUser
            "Deleting user", "User deleted", // 8, 9 - DeleteUser
            "Getting user by id", "User by id retrieved", // 10, 11 - GetUserById
            "Getting users", "Users retrieved" // 12, 13 - GetUsers

        } },

        { ILogger.LogLevel.Warning, new List<string> {

            "User has the same role", // 0 - UpdateUserRole

        } },

        { ILogger.LogLevel.Error, new List<string> {

            "User manager already initialized", // 0 - UserManager.Initialize
            "User not found" // 1 - UpdateUserRole

        } }
    };

    public static List<string> InfoMessages { get; } = Messages[ILogger.LogLevel.Info];
    public static List<string> ErrorMessages { get; } = Messages[ILogger.LogLevel.Error];
    public static List<string> WarningMessages { get; } = Messages[ILogger.LogLevel.Warning];

    // Nullable ignored for simplicity
    public static UserManager Instance { get; private set; }

    public static void Initialize(IUserQueries queries, IUserRepository repository, ILogger logger)
    {
        logger.Log(InfoMessages[0]);

        if (Instance != null)
        {
            string errorMessage = ErrorMessages[0];

            logger.LogError(errorMessage);

            throw new Exception(errorMessage);
        }

        new UserManager(queries, repository, logger);
    }

    private UserManager(IUserQueries queries, IUserRepository repository, ILogger logger)
    {
        Instance = this;
        _queries = queries;
        _repository = repository;
        _logger = logger;
        _logger.Log(InfoMessages[1]);
    }

    private readonly IUserQueries _queries;
    private readonly IUserRepository _repository;
    private readonly ILogger _logger;

    public async Task<User> CreateUser(User user, CancellationToken cancellationToken)
    {
        await _logger.Log(InfoMessages[2]);
        try
        {
            var createdUser = await _repository.Create(user, cancellationToken);

            await _logger.Log(InfoMessages[3]);

            return createdUser;
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex.Message);
            throw new Exception("An error occurred while creating the user", ex);
        }
    }

    public async Task<User> UpdateUserRole(User user, CancellationToken cancellationToken)
    {
        await _logger.Log(InfoMessages[4]);
        try
        {
            var existingUser = await _queries.GetUserById(user.Id, cancellationToken) ?? throw new Exception(ErrorMessages[1]);

            if (existingUser.RoleId == user.RoleId)
            {
                await _logger.LogWarning(WarningMessages[0]);

                return existingUser;
            }

            existingUser.UpdateRole(user.RoleId);

            var updatedUser = await _repository.Update(existingUser, cancellationToken);

            await _logger.Log(InfoMessages[5]);

            return updatedUser;
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex.Message);
            throw new Exception("An error occurred while updating the user role", ex);
        }
    }

    public async Task<User> UpdateUser(User user, CancellationToken cancellationToken)
    {
        await _logger.Log(InfoMessages[6]);
        try
        {
            var updatedUser = await _repository.Update(user, cancellationToken);

            await _logger.Log(InfoMessages[7]);

            return updatedUser;
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex.Message);
            throw new Exception("An error occurred while updating the user", ex);
        }
    }

    public async Task<User> DeleteUser(User user, CancellationToken cancellationToken)
    {
        await _logger.Log(InfoMessages[8]);
        try
        {
            var deletedUser = await _repository.Delete(user, cancellationToken);

            await _logger.Log(InfoMessages[9]);

            return deletedUser;
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex.Message);
            throw new Exception("An error occurred while deleting the user", ex);
        }
    }

    public async Task<User?> GetUserById(UserId id, CancellationToken cancellationToken)
    {
        await _logger.Log(InfoMessages[10]);
        try
        {
            var user = await _queries.GetUserById(id, cancellationToken);

            await _logger.Log(InfoMessages[11]);

            return user;
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex.Message);
            throw new Exception("An error occurred while retrieving the user", ex);
        }
    }

    public async Task<IEnumerable<User>> GetUsers(CancellationToken cancellationToken)
    {
        await _logger.Log(InfoMessages[12]);
        try
        {
            var users = await _queries.GetUsers(cancellationToken);

            await _logger.Log(InfoMessages[13]);

            return users;
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex.Message);
            throw new Exception("An error occurred while retrieving the users", ex);
        }
    }
}

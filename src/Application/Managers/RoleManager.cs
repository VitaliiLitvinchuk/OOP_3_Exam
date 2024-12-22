using Application.Loggers.Abstractions;
using Domain.Models.Roles;
using Infrastructure.Persistence.Repositories.Abstractions.Roles;

namespace Application.Managers;

public class RoleManager
{
    public static Dictionary<ILogger.LogLevel, List<string>> Messages { get; } = new Dictionary<ILogger.LogLevel, List<string>>
    {
        { ILogger.LogLevel.Info, new List<string> {

            "Initializing role manager", "Role manager initialized", // 0, 1 - RoleManager.Initialize
            "Getting role by id", "Role by id retrieved", // 2, 3 - GetRoleById
            "Getting role by name", "Role by name retrieved", // 4, 5 - GetRoleByName
            "Getting roles", "Roles retrieved", // 6, 7 - GetRoles
            "Creating role", "Role created", // 8, 9 - CreateRole
            "Updating role", "Role updated", // 10, 11 - UpdateRole
            "Deleting role", "Role deleted", // 12, 13 - DeleteRole

        } },

        { ILogger.LogLevel.Warning, new List<string> {


        } },

        { ILogger.LogLevel.Error, new List<string> {

            "Role manager already initialized", // 0 - RoleManager.Initialize
            "Role already exists", // 1 - CreateRole
            "Role not found" // 2 - UpdateRole

        } }
    };

    public static List<string> InfoMessages { get; } = Messages[ILogger.LogLevel.Info];
    public static List<string> ErrorMessages { get; } = Messages[ILogger.LogLevel.Error];
    public static List<string> WarningMessages { get; } = Messages[ILogger.LogLevel.Warning];

    // Nullable ignored for simplicity
    public static RoleManager Instance { get; private set; }

    public static void Initialize(IRoleQueries roleQueries, IRoleRepository roleRepository, ILogger logger)
    {
        logger.Log(InfoMessages[0]);

        if (Instance != null)
        {
            string errorMessage = ErrorMessages[0];

            logger.LogError(errorMessage);

            throw new Exception(errorMessage);
        }

        new RoleManager(roleQueries, roleRepository, logger);
    }

    private RoleManager(IRoleQueries roleQueries, IRoleRepository roleRepository, ILogger logger)
    {
        Instance = this;
        _roleQueries = roleQueries;
        _roleRepository = roleRepository;
        _logger = logger;
        _logger.Log(InfoMessages[1]);
    }

    private readonly IRoleQueries _roleQueries;
    private readonly IRoleRepository _roleRepository;
    private readonly ILogger _logger;

    public async Task<Role?> GetRoleById(RoleId id, CancellationToken cancellationToken)
    {
        await _logger.Log(InfoMessages[2]);
        try
        {
            var role = await _roleQueries.GetRoleById(id, cancellationToken);

            await _logger.Log(InfoMessages[3]);

            return role;
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<Role?> GetRoleByName(string name, CancellationToken cancellationToken)
    {
        await _logger.Log(InfoMessages[4]);
        try
        {
            var role = await _roleQueries.GetRoleByName(name, cancellationToken);

            await _logger.Log(InfoMessages[5]);

            return role;
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<Role>> GetRoles(CancellationToken cancellationToken)
    {
        await _logger.Log(InfoMessages[6]);
        try
        {
            var roles = await _roleQueries.GetRoles(cancellationToken);

            await _logger.Log(InfoMessages[7]);

            return roles;
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<Role> CreateRole(Role role, CancellationToken cancellationToken)
    {
        await _logger.Log(InfoMessages[8]);
        try
        {
            var existingRole = await GetRoleByName(role.Name, cancellationToken);

            if (existingRole != null)
            {
                string errorMessage = ErrorMessages[1];

                await _logger.LogError(errorMessage);

                throw new Exception(errorMessage);
            }

            var newRole = await _roleRepository.Create(role, cancellationToken);

            await _logger.Log(InfoMessages[9]);

            return newRole;
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<Role> UpdateRole(Role role, CancellationToken cancellationToken)
    {
        await _logger.Log(InfoMessages[10]);
        try
        {
            var existingRole = await GetRoleById(role.Id, cancellationToken);

            if (existingRole == null)
            {
                string errorMessage = ErrorMessages[2];

                await _logger.LogError(errorMessage);

                throw new Exception(errorMessage);
            }

            existingRole.UpdateDetails(role.Name);

            var updatedRole = await _roleRepository.Update(existingRole, cancellationToken);

            await _logger.Log(InfoMessages[11]);

            return updatedRole;
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex.Message);
            throw;
        }
    }

    public async Task<Role> DeleteRole(Role role, CancellationToken cancellationToken)
    {
        await _logger.Log(InfoMessages[12]);
        try
        {
            var deletedRole = await _roleRepository.Delete(role, cancellationToken);

            await _logger.Log(InfoMessages[13]);

            return deletedRole;
        }
        catch (Exception ex)
        {
            await _logger.LogError(ex.Message);
            throw;
        }
    }
}

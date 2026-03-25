using System.Reflection;
using BepInEx.Logging;

namespace MoreMiraRoles.Roles;

public sealed class RoleBootstrapper
{
    private readonly RoleRegistry _registry;
    private readonly ManualLogSource _logger;

    public RoleBootstrapper(RoleRegistry registry, ManualLogSource logger)
    {
        _registry = registry;
        _logger = logger;
    }

    public void LoadFromAssembly(Assembly assembly)
    {
        var roleTypes = assembly
            .GetTypes()
            .Where(type => !type.IsAbstract && typeof(IRoleDefinition).IsAssignableFrom(type))
            .OrderBy(type => type.Name, StringComparer.Ordinal)
            .ToArray();

        foreach (var roleType in roleTypes)
        {
            if (roleType.GetConstructor(Type.EmptyTypes) is null)
            {
                _logger.LogWarning($"Skipping role {roleType.FullName}: missing parameterless constructor.");
                continue;
            }

            if (Activator.CreateInstance(roleType) is not IRoleDefinition role)
            {
                _logger.LogWarning($"Skipping role {roleType.FullName}: could not instantiate.");
                continue;
            }

            _registry.TryRegister(role);
        }
    }
}

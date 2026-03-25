using BepInEx.Logging;

namespace MoreMiraRoles.Roles;

public sealed class RoleRegistry
{
    private readonly Dictionary<string, IRoleDefinition> _roles = new(StringComparer.OrdinalIgnoreCase);
    private readonly RoleContext _context;

    public RoleRegistry(ManualLogSource logger)
    {
        _context = new RoleContext(logger);
    }

    public int Count => _roles.Count;

    public IReadOnlyCollection<IRoleDefinition> All => _roles.Values
        .OrderBy(role => role.Priority)
        .ThenBy(role => role.DisplayName, StringComparer.OrdinalIgnoreCase)
        .ToArray();

    public bool TryRegister(IRoleDefinition role)
    {
        if (string.IsNullOrWhiteSpace(role.Id))
        {
            _context.Logger.LogWarning("Skipping role registration because Id is empty.");
            return false;
        }

        if (_roles.ContainsKey(role.Id))
        {
            _context.Logger.LogWarning($"Skipping duplicate role Id: {role.Id}");
            return false;
        }

        _roles.Add(role.Id, role);
        role.OnRegister(_context);
        _context.Logger.LogInfo($"Registered role: {role.DisplayName} ({role.Id})");
        return true;
    }

    public bool TryUnregister(string roleId)
    {
        if (!_roles.Remove(roleId, out var role))
        {
            return false;
        }

        role.OnUnregister(_context);
        _context.Logger.LogInfo($"Unregistered role: {role.DisplayName} ({role.Id})");
        return true;
    }

    public bool TryGet(string roleId, out IRoleDefinition role) => _roles.TryGetValue(roleId, out role!);
}

using BepInEx.Logging;

namespace MoreMiraRoles.Roles;

public sealed class RoleContext
{
    public RoleContext(ManualLogSource logger)
    {
        Logger = logger;
    }

    public ManualLogSource Logger { get; }
}

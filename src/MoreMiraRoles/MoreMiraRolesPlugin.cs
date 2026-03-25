using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using MoreMiraRoles.Roles;

namespace MoreMiraRoles;

[BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Version)]
public sealed class MoreMiraRolesPlugin : BaseUnityPlugin
{
    internal static ManualLogSource Log { get; private set; } = null!;

    private readonly Harmony _harmony = new(PluginInfo.Guid);
    private RoleRegistry? _roleRegistry;

    private void Awake()
    {
        Log = Logger;

        _roleRegistry = new RoleRegistry(Log);
        var bootstrapper = new RoleBootstrapper(_roleRegistry, Log);
        bootstrapper.LoadFromAssembly(typeof(MoreMiraRolesPlugin).Assembly);

        _harmony.PatchAll();

        Log.LogInfo($"{PluginInfo.Name} loaded with {_roleRegistry.Count} registered roles.");
    }

    private void OnDestroy()
    {
        _harmony.UnpatchSelf();
        Log.LogInfo($"{PluginInfo.Name} unloaded.");
    }
}

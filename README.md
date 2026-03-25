# MoreMiraRoles

Not affiliated with Innersloth or Town Of Us Mira Mod. This plugin is a **role framework** for Among Us + Town Of Us Mira `1.5.4` on BepInEx, with no custom roles enabled yet.

## Current status

- ✅ BepInEx plugin entrypoint is set up.
- ✅ Harmony patch bootstrap is wired.
- ✅ Role system architecture is ready (registry, base role class, assembly auto-loader).
- ✅ Zero gameplay roles are registered by default.

## Project layout

- `src/MoreMiraRoles/MoreMiraRolesPlugin.cs`: plugin startup/shutdown.
- `src/MoreMiraRoles/Roles/IRoleDefinition.cs`: role contract.
- `src/MoreMiraRoles/Roles/RoleDefinition.cs`: base class for new roles.
- `src/MoreMiraRoles/Roles/RoleRegistry.cs`: central role registration and lifecycle.
- `src/MoreMiraRoles/Roles/RoleBootstrapper.cs`: automatic role discovery.

## Build

```bash
dotnet restore src/MoreMiraRoles/MoreMiraRoles.csproj
dotnet build src/MoreMiraRoles/MoreMiraRoles.csproj -c Release
```

Output DLL will be in:

`src/MoreMiraRoles/bin/Release/net6.0/MoreMiraRoles.dll`

Copy that DLL into your BepInEx plugins folder.

## How to add a role later

1. Create a new class in `src/MoreMiraRoles/Roles` that inherits `RoleDefinition`.
2. Implement at minimum: `Id`, `DisplayName`, and `Team`.
3. Optionally add lifecycle logic in `OnRegister` and `OnUnregister`.
4. Rebuild and drop the DLL into BepInEx plugins.

### Example role skeleton

```csharp
using MoreMiraRoles.Roles;

namespace MoreMiraRoles.Roles;

public sealed class ExampleRole : RoleDefinition
{
    public override string Id => "example_role";
    public override string DisplayName => "Example Role";
    public override TeamAlignment Team => TeamAlignment.Crewmate;
    public override string Description => "Describe the role here.";
    public override int Priority => 100;

    public override void OnRegister(RoleContext context)
    {
        context.Logger.LogInfo("ExampleRole registered.");
    }
}
```

When you give me a role idea, I can add the actual behavior on top of this framework.

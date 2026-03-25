namespace MoreMiraRoles.Roles;

public interface IRoleDefinition
{
    string Id { get; }
    string DisplayName { get; }
    string Description { get; }
    TeamAlignment Team { get; }
    int Priority { get; }

    void OnRegister(RoleContext context);
    void OnUnregister(RoleContext context);
}

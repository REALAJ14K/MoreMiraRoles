namespace MoreMiraRoles.Roles;

public abstract class RoleDefinition : IRoleDefinition
{
    public abstract string Id { get; }
    public abstract string DisplayName { get; }
    public abstract TeamAlignment Team { get; }

    public virtual string Description => string.Empty;
    public virtual int Priority => 0;

    public virtual void OnRegister(RoleContext context)
    {
    }

    public virtual void OnUnregister(RoleContext context)
    {
    }
}

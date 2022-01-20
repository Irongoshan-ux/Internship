namespace CarMarket.Core.User.Domain
{
    public class Permission
    {
        public enum PermissionType
        {
            Create,
            Edit,
            Delete
        }

        public int Id { get; set; }
        public PermissionType PermissionName { get;  set; }
    }
}
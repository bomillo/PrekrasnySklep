namespace PrekrasnyDomainLayer.Models.Enums
{
    [Flags]
    public enum UserRoleAccessRule
    {
        Admin = UserRole.Admin,
        Shipping = UserRole.Admin | UserRole.Shipping,
        CustomerService = UserRole.Admin | UserRole.CustomerService,
        Any = UserRole.Admin | UserRole.Shipping | UserRole.CustomerService
    }
}
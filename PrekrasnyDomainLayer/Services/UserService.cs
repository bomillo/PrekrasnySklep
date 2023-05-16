using PrekrasnyDomainLayer.Context;
using PrekrasnyDomainLayer.Models;
using PrekrasnyDomainLayer.Models.Enums;
using PrekrasnyDomainLayer.State;
using System.Security.Cryptography;
using System.Text;

namespace PrekrasnyDomainLayer.Services;

public sealed class UserService
{
    private readonly PrekrasnyContext context;
    private readonly SHA256 Sha256 = SHA256.Create();

    public UserService() : this(AppState.SharedContext) { }

    public UserService(PrekrasnyContext context)
    {
        this.context = context;
    }


    public bool RegisterNewUser(string userName, string password, UserRole userRole)
    {
        if (context.Users.Any(u => u.UserName == userName))
        {
            return false;
        }

        context.Users.Add(new User { UserName = userName, HashedPassword = HashPassword(password), UserRole=userRole });
        context.SaveChanges();
        return true;
    }

    public bool Login(string userName, string password)
    {
        if (!context.Users.Any(u => u.UserName == userName && u.HashedPassword == HashPassword(password)))
        {
            return false;
        }

        AppState.CurrentUser = context.Users.First(u => u.UserName == userName && u.HashedPassword == HashPassword(password));
        return true;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Bo nie, po co drążyć temat?")]
    public void Logout()
    {
        AppState.CurrentUser = null;
    }

    public void ModifyCurrentUser(string? newUserName=null, string? newPassword=null)
    {
        if (AppState.CurrentUser is null)
        {
            return;
        }

        ModifyUser(AppState.CurrentUser, newUserName, newPassword);
    }

    public void ModifyUser(User user, string? newUserName = null, string? newPassword = null)
    {
        user.UserName = newUserName ?? user.UserName;
        user.HashedPassword = newPassword is not null ? HashPassword(newPassword) : user.HashedPassword;

        context.Update(user);
        context.SaveChanges();
    }

    public List<User> GetAllUsers()
    {
        return context.Users.ToList();
    }
    public bool ValidateOldPassword(string oldPassword)
    {
        if(!string.IsNullOrWhiteSpace(oldPassword))
        {
            return HashPassword(oldPassword).Equals(AppState.CurrentUser!.HashedPassword);
        }

        return false;
    }
    private string HashPassword(string password)
    {
        return string.Concat(Sha256.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("x2")));
    }
}

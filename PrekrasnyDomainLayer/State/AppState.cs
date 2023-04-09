using PrekrasnyDomainLayer.Context;
using PrekrasnyDomainLayer.Models;

namespace PrekrasnyDomainLayer.State;

public static class AppState
{
    public static User? CurrentUser { get; set; }

    public static PrekrasnyContext SharedContext { get; set; } = null!;
}
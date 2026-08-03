using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace LanguaForge.Web.Services;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly AuthStateService _authStateService;

    public JwtAuthenticationStateProvider(AuthStateService authStateService)
    {
        _authStateService = authStateService;
        _authStateService.OnChange += () =>
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
        => Task.FromResult(new AuthenticationState(_authStateService.CurrentUser));
}

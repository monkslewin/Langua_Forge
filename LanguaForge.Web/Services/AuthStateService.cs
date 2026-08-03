using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace LanguaForge.Web.Services;

public class AuthStateService
{
    private ClaimsPrincipal _currentUser = new(new ClaimsIdentity());

    public ClaimsPrincipal CurrentUser => _currentUser;

    public event Action? OnChange;

    public void SetUserFromToken(string token)
    {
        _currentUser = ParseJwt(token);
        OnChange?.Invoke();
    }

    public void Logout()
    {
        _currentUser = new ClaimsPrincipal(new ClaimsIdentity());
        OnChange?.Invoke();
    }

    private static ClaimsPrincipal ParseJwt(string token)
    {
        var parts = token.Split('.');
        if (parts.Length != 3)
            return new ClaimsPrincipal(new ClaimsIdentity());

        var payload = parts[1];
        payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');

        var json = Encoding.UTF8.GetString(Convert.FromBase64String(payload));
        var data = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(json);

        var claims = new List<Claim>();
        foreach (var (key, value) in data ?? [])
        {
            var strValue = value.ValueKind == JsonValueKind.String
                ? value.GetString()!
                : value.ToString();
            claims.Add(new Claim(key, strValue));
        }

        return new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
    }
}

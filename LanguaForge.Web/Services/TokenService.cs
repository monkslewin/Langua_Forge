using System.Text;
using System.Text.Json;

namespace LanguaForge.Web.Services;

public class TokenService
{
    private string? _token;
    private string? _firstName;

    public void SetToken(string token)
    {
        _token = token;
        _firstName = ParseClaim(token, "FirstName");
    }

    public string? GetToken() => _token;
    public string? GetFirstName() => _firstName;

    public void ClearToken()
    {
        _token = null;
        _firstName = null;
    }

    // JWT payload is Base64url-encoded JSON — no library needed
    private static string? ParseClaim(string token, string claimType)
    {
        var parts = token.Split('.');
        if (parts.Length != 3) return null;

        var payload = parts[1].Replace('-', '+').Replace('_', '/');
        payload = payload.PadRight(payload.Length + (4 - payload.Length % 4) % 4, '=');

        try
        {
            var json = Encoding.UTF8.GetString(Convert.FromBase64String(payload));
            using var doc = JsonDocument.Parse(json);
            return doc.RootElement.TryGetProperty(claimType, out var el) ? el.GetString() : null;
        }
        catch { return null; }
    }
}
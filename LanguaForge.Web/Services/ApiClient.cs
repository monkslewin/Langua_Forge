using System.Net.Http.Headers;
namespace LanguaForge.Web.Services;

public class ApiClient
{
    private readonly IHttpClientFactory _factory;
    private readonly TokenService _tokenService;

    public ApiClient(IHttpClientFactory factory, TokenService tokenService)
    {
        _factory = factory;
        _tokenService = tokenService;
    }

    public HttpClient Create()
    {
        var client = _factory.CreateClient("API");
        var token = _tokenService.GetToken();
        if (!string.IsNullOrEmpty(token))
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        return client;
    }
}

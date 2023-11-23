using System.Security.AccessControl;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using System.Threading.Tasks;

// IApiService.cs
public interface IApiService
{
    Task<V1MentionsResponse> GetV1Mentions();
    Task<V1MentionsResponse> GetV1Mentions(long from, long to, int count);
}

// ApiService.cs
public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<V1MentionsResponse> GetV1Mentions()
    {
        return await GetV1Mentions(1697872914, 1700558566, 10);
    }

    public async Task<V1MentionsResponse> GetV1Mentions(long from, long to, int count)
    {
        try
        {
            var requestUri = $"https://api.mediatoolkit.com/organizations/160996/groups/215519/mentions?access_token=3d6u7eqx5anw9v72dxkr9imz8vwkl3ye73dsgv427q3lu7npua&from_time={from}&to_time={to}&sort=time&count={count}";

            using (var response = await _httpClient.GetAsync(requestUri))
            {
                response.EnsureSuccessStatusCode();
                var responseData = await response.Content.ReadAsAsync<V1MentionsResponse>();
                return responseData;
            }
        }
        catch (HttpRequestException ex)
        {
            // Handle exceptions (log, rethrow, etc.)
            // You might want to define a custom exception type for better error handling
            throw new ApiException("Error occurred while fetching V1 mentions.", ex);
        }
    }
}

public class ApiException : Exception
{
    public ApiException(string message, HttpRequestException ex) : base(message)
    {
    }
}
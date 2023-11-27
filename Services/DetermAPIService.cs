using System.Security.AccessControl;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Json;
using System.Text.Json;

using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

// IApiService.cs
public interface IApiService
{
    Task<V1MentionsResponse> GetV1Mentions(long from, long to, int count);
    Task<V2MentionsResponse> GetV2Mentions(long from, long to, int count);
}

// ApiService.cs
public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private readonly string AccessToken = "3d6u7eqx5anw9v72dxkr9imz8vwkl3ye73dsgv427q3lu7npua";
    private readonly string SourceToken = "130fd26e789be8219d0a52bc937f082e";
    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<V1MentionsResponse> GetV1Mentions(long from, long to, int count)
    {
        try
        {
            var requestUri = $"https://api.mediatoolkit.com/organizations/160996/groups/215519/mentions?access_token={AccessToken}&from_time={from}&to_time={to}&sort=time&count={count}";

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

     public async Task<V2MentionsResponse> GetV2Mentions(long from, long to, int count)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.mediatoolkit.com/v2/organization/160996/group/215519/mentions/scroll?sourceToken={SourceToken}");
            request.Headers.Add("Authorization", $"Bearer {AccessToken}");
            var content = new StringContent
            (
               $@"
                    {{
                        ""query"": {{
                            ""publishedTime"": {{
                                ""from"": { from * 1000 },
                                ""to"": { to * 1000 }
                            }}
                        }},
                        ""paged"": {{
                            ""count"": { count },
                            ""sorted"": {{
                                ""direction"": ""DESC"",
                                ""property"": ""PUBLISHED_TIME""
                            }}
                        }}
                    }}",
                null,
                "application/json"
            );
            request.Content = content;


            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var responseData = await response.Content.ReadAsAsync<V2MentionsResponse>();
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
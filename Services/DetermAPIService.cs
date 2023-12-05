using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

// IApiService.cs
public interface IApiService
{
    Task<Result<List<PostTableViewModel>, string>> GetV1Mentions(long from, long to, int groupId, int keywordId, int tagId, int count, int offset);
    Task<Result<List<PostTableViewModel>, string>> GetV2Mentions(long from, long to, int groupId, int keywordId, int tagId, int count);
    Task<GroupResponse> GetKeywords(int groupId);
}

// ApiService.cs
public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private const string AccessToken = "3d6u7eqx5anw9v72dxkr9imz8vwkl3ye73dsgv427q3lu7npua";
    private const string SourceToken = "130fd26e789be8219d0a52bc937f082e";
    private string ScrollToken = "";
    private bool areV1TaggedPostsFetched = true;
    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<GroupResponse> GetKeywords(int groupId)
    {
        try
        {
            var requestUri = $"https://api.mediatoolkit.com/organizations/160996/groups/{groupId}?access_token={AccessToken}";

            using (var response = await _httpClient.GetAsync(requestUri))
            {
                response.EnsureSuccessStatusCode();
                var responseData = await response.Content.ReadAsAsync<GroupResponse>();
                return responseData;
            }
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException("Error occurred while fetching group keywords.", ex);
        }
    }

    public async Task<Result<List<PostTableViewModel>, string>> GetV1Mentions(long from, long to, int groupId, int keywordId, int tagId, int count, int offset = 0)
    {
        if (tagId != 0)
        {
            areV1TaggedPostsFetched = false;
            return await GetTaggedV1Mentions(new List<PostTableViewModel>(), from, to, groupId, keywordId, tagId, count);
        }
        try
        {
            var requestUri = $"https://api.mediatoolkit.com/organizations/160996/groups/{groupId}/{((keywordId == 0) ? "" : $"keywords/{keywordId}/")}mentions?access_token={AccessToken}&from_time={from}&to_time={to}&sort=time&count={count}&offset={offset}";
            using (var response = await _httpClient.GetAsync(requestUri))
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<V1MentionsResponse>(responseString, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                });
                return Result<List<PostTableViewModel>, string>.Success(responseData.Data.Response.Select(p => new PostTableViewModel(p)).ToList());
            }
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException("Error occurred while fetching V1 mentions.", ex);
        }
    }

    public async Task<Result<List<PostTableViewModel>, string>> GetTaggedV1Mentions(List<PostTableViewModel> initialPosts, long from, long to, int groupId, int keywordId, int tagId, int count, int offset = 0)
    {
        try
        {
            var requestUri = $"https://api.mediatoolkit.com/organizations/160996/groups/{groupId}/{((keywordId == 0) ? "" : $"keywords/{keywordId}/")}mentions?access_token={AccessToken}&from_time={from}&to_time={to}&sort=time&count=1000&offset={offset}";
            using (var response = await _httpClient.GetAsync(requestUri))
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<V1MentionsResponse>(responseString, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new SnakeCaseNamingStrategy()
                    }
                });

                // If no more posts, return
                if (responseData?.Data.Response.Count == 0) {
                    return Result<List<PostTableViewModel>, string>.Success(initialPosts);
                }

                // Fiter out DFL/EPL tagged posts
                var posts = responseData?.Data.Response
                    .Where(p => p.TagFeedLocations?.FirstOrDefault()?.TagId == tagId)
                    .Select(p => new PostTableViewModel(p))
                    .ToList();
                initialPosts.AddRange(posts);
                Console.WriteLine($"==========================    YES TAGGED POSTS   >> {initialPosts.Count} <<  ============================");
                if (initialPosts.Count < count && !areV1TaggedPostsFetched)
                {
                    return await GetTaggedV1Mentions(initialPosts, from, to, groupId, keywordId, tagId, count, offset + 1000);
                }  

                areV1TaggedPostsFetched = true;
                return Result<List<PostTableViewModel>, string>.Success(initialPosts.GetRange(0, count));
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine(ex.Message);
            if (offset > 9999)
            {
                return Result<List<PostTableViewModel>, string>.Failure("Reached Determ V1 offset limit of 10,000 posts.");
            }
            return Result<List<PostTableViewModel>, string>.Failure(ex.Message);
            throw new ApiException("Error occurred while fetching V1 mentions.", ex);
        }
    }


     public async Task<Result<List<PostTableViewModel>, string>> GetV2Mentions(long from, long to, int groupId, int keywordId, int tagId, int count)
    {
        try
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.mediatoolkit.com/v2/organization/160996/group/{groupId}/{((keywordId == 0) ? "" : $"keyword/{ keywordId}/")}mentions/scroll?sourceToken={SourceToken}");
            request.Headers.Add("Authorization", $"Bearer {AccessToken}");

            var content = new StringContent
            (
               $@"
                {{
                    ""query"": {{
                        { (
                        (tagId == 0) 
                            ? "" 
                            : $@"""mentionFilter"": {{ 
                                ""tag"": {{
                                    ""all"": [ { tagId } ] 
                                }} 
                            }}," 
                        ) }
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
                    { (string.IsNullOrEmpty(ScrollToken) ? "" : $@",\nscrollToken : { ScrollToken }") }
                }}
                ",
                null,
                "application/json"
            );
            request.Content = content;

            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var responseString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<V2MentionsResponse>(responseString, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                });


                return Result<List<PostTableViewModel>, string>.Success(responseData.Mentions.Select(p => new PostTableViewModel(p)).ToList());
            }
        }
        catch (HttpRequestException ex)
        {
            throw new ApiException("Error occurred while fetching V2 mentions.", ex);
        }
    }
}

public class ApiException : Exception
{
    public ApiException(string message, HttpRequestException ex) : base(message)
    {
    }
}
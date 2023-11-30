using System;
using System.Collections.Generic;
using Newtonsoft.Json;


public interface IPost
{
    long Id { get; set; }
    string Title { get; set; }
    long InsertTime { get; set; }
    string Type { get; set; }
    string From { get; set; }
    string Url { get; set; }
}

public class V1MentionsResponse
{
    public string Type { get; set; }
    public int Code { get; set; }
    public V1Posts Data { get; set; }
    public string Message { get; set; }
    public int Duration { get; set; }
}

public class V2MentionsResponse
{
    public List<V2Post> Mentions { get; set; }
    public string ScrollToken { get; set; }
}

public class V1Posts
{
    public List<V1Post> Response { get; set; }
}

public class V1Post : IPost
{
    [JsonProperty("comment_count")]
    public int CommentCount { get; set; }
    public List<string> Keywords { get; set; }
    public int Reach { get; set; }
    [JsonProperty("insert_time")]
    public long InsertTime { get; set; }
    public string Description { get; set; }
    public string Type { get; set; }
    public string Title { get; set; }
    public List<string> OriginalPhotos { get; set; }
    public List<string> Photos { get; set; }
    public string Mention { get; set; }
    public string OriginalPhoto { get; set; }
    public double Score { get; set; }
    [JsonProperty("from")]
    public string From { get; set; }
    public long Id { get; set; }
    [JsonProperty("auto_sentiment")]
    public string AutoSentiment { get; set; }
    public long DatabaseInsertTime { get; set; }
    [JsonProperty("keyword_name")]
    public string KeywordName { get; set; }
    public string Image { get; set; }
    [JsonProperty("like_count")]
    public int LikeCount { get; set; }
    public List<string> Languages { get; set; }
    public string GroupName { get; set; }
    public string Author { get; set; }
    public string Photo { get; set; }
    [JsonProperty("influence_score")]
    public int InfluenceScore { get; set; }
    public string Url { get; set; }
    public double Virality { get; set; }
    [JsonProperty("share_count")]
    public int ShareCount { get; set; }
    [JsonProperty("source_reach")]
    public int SourceReach { get; set; }
    public int KeywordId { get; set; }
    public int GroupId { get; set; }
    public string Domain { get; set; }
    [JsonProperty("tag_feed_locations")]
    public List<V1Tag>? TagFeedLocations { get; set; }
    public int Interaction { get; set; }
    public List<string> Locations { get; set; }
    public List<string> KeywordNames { get; set; }
}

public interface ITag
{
    public int CategoryId { get; set; }
    public int TagId { get; set; }
}

public class V1Tag : ITag
{
    [JsonProperty("category_id")]
    public int CategoryId { get; set; }
    [JsonProperty("tag_id")]
    public int TagId { get; set; }
}

public class V2Post : IPost
{
    public long Id { get; set; }
    public string Type { get; set; }
    public string Mention { get; set; }
    public List<string> Languages { get; set; }
    public string From { get; set; }
    public string Author { get; set; }
    public long InsertTime { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
    public string Image { get; set; }
    public string Photo { get; set; }
    public string OriginalPhoto { get; set; }
    public List<string> Photos { get; set; }
    public List<string> OriginalPhotos { get; set; }
    public int Reach { get; set; }
    public double Virality { get; set; }
    public long DatabaseInsertTime { get; set; }
    public List<string> Keywords { get; set; }
    public List<string> Locations { get; set; }
    public string AutoSentiment { get; set; }
    public int SourceReach { get; set; }
    public int Interaction { get; set; }
    public int InfluenceScore { get; set; }
    public double Score { get; set; }
    public string Domain { get; set; }
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    public int ShareCount { get; set; }
    public string Description { get; set; }
    public List<V2Tag>? TagFeedLocations { get; set; }
    public int KeywordId { get; set; }
    public string KeywordName { get; set; }
    public int GroupId { get; set; }
    public string GroupName { get; set; }
    public List<string> KeywordNames { get; set; }
}

public class V2Tag : ITag
{
    public int CategoryId { get; set; }
    public int TagId { get; set; }
}

public class PostTableViewModel
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Thumbnail { get; set; }
    public long InsertTime { get; set; }
    public string Type { get; set; }
    public string From { get; set; }
    public string Url { get; set; }
    public string KeywordName { get; set; }
    public int Reach { get; set; }
    public int SourceReach { get; set; }
    public int Interaction { get; set; }
    public int InfluenceScore { get; set; }
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    public int ShareCount { get; set; }
    public ITag? Tag { get; set; }
    public double Virality { get; set; }
    public string AutoSentiment { get; set; }
}

public enum DetermTab
{
    One,
    Two
}

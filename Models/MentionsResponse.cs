using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class V1MentionsResponse
{
    public string Type { get; set; }
    public int Code { get; set; }
    public V1Posts Data { get; set; }
    public string Message { get; set; }
    public int Duration { get; set; }
}

public class V1Posts
{
    public List<Post> Response { get; set; }
}

public class V2MentionsResponse
{
    public List<Post> Mentions { get; set; }
    public string ScrollToken { get; set; }
}

public class Post
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
    public int ViewCount { get; set; }
    public int Reach { get; set; }
    public double Virality { get; set; }
    public long DatabaseInsertTime { get; set; }
    public List<string> Keywords { get; set; }
    public List<string> Locations { get; set; }
    public string AutoSentiment { get; set; }
    public int SourceReach { get; set; }
    public int Interaction { get; set; }
    public int InfluenceScore { get; set; }
    public int AuthorFollowerCount { get; set; }
    public double Score { get; set; }
    public string Domain { get; set; }
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    public int ShareCount { get; set; }
    public string Description { get; set; }
    public int Duration { get; set; }
    public List<Tag>? TagFeedLocations { get; set; }
    public int KeywordId { get; set; }
    public string KeywordName { get; set; }
    public int GroupId { get; set; }
    public string GroupName { get; set; }
    public List<string> KeywordNames { get; set; }
}

public class Tag
{
    public int CategoryId { get; set; }
    public int TagId { get; set; }
}

public class PostTableViewModel
{
    public PostTableViewModel(Post post)
    {
        Id = post.Id;
        Title = post.Title;
        Thumbnail = post.Image;
        InsertTime = post.InsertTime;
        Type = post.Type;
        From = post.From;
        Url = post.Url;
        KeywordName = post.KeywordName;
        ViewCount = post.ViewCount;
        Reach = post.Reach;
        SourceReach = post.SourceReach;
        Interaction = post.Interaction;
        InfluenceScore = post.InfluenceScore;
        AuthorFollowerCount = post.AuthorFollowerCount;
        LikeCount = post.LikeCount;
        CommentCount = post.CommentCount;
        ShareCount = post.ShareCount;
        Duration = post.Duration;
        Tag = post.TagFeedLocations?.FirstOrDefault();
        Virality = post.Virality;
        AutoSentiment = post.AutoSentiment;
    }

    public long Id { get; set; }
    public string Title { get; set; }
    public string Thumbnail { get; set; }
    public long InsertTime { get; set; }
    public string Type { get; set; }
    public string From { get; set; }
    public string Url { get; set; }
    public string KeywordName { get; set; }
    public Tag? Tag { get; set; }
    public int ViewCount { get; set; }
    public int Reach { get; set; }
    public int SourceReach { get; set; }
    public int Interaction { get; set; }
    public int InfluenceScore { get; set; }
    public int AuthorFollowerCount { get; set; }
    public int LikeCount { get; set; }
    public int CommentCount { get; set; }
    public int ShareCount { get; set; }
    public int Duration { get; set; }
    public double Virality { get; set; }
    public string AutoSentiment { get; set; }
}
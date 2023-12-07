

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
    public int? ViewCount { get; set; }
    public int? PlayCount { get; set; }
    public int? Views { get; set; }
    public int Reach { get; set; }
    public long DatabaseInsertTime { get; set; }
    public List<string> Keywords { get; set; }
    public List<string>? Locations { get; set; }
    public string AutoSentiment { get; set; }
    public int SourceReach { get; set; }
    public int Interaction { get; set; }
    public int InfluenceScore { get; set; }
    public int FollowersCount { get; set; }
    public double Score { get; set; }
    public string Domain { get; set; }
    public int? LikeCount { get; set; }
    public int? DiggCount { get; set; }
    public int? TotalReactionsCount { get; set; }
    public int? FavoriteCount { get; set; }
    public int CommentCount { get; set; }
    public double? Virality { get; set; }
    public int? ShareCount { get; set; }
    public int? RetweetCount { get; set; }
    public string Description { get; set; }
    public int? Duration { get; set; }
    public int? VideoDurationSeconds { get; set; }
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
        Location = getLocation(post.Locations);
        Url = post.Url;
        KeywordName = Keyword.RemoveGuid(post.KeywordName);
        VideoViews = post.ViewCount ?? post.PlayCount ?? post.Views ?? 0;
        Reach = post.Reach;
        SourceReach = post.SourceReach;
        Interaction = post.Interaction;
        InfluenceScore = post.InfluenceScore;
        FollowersCount = post.FollowersCount;
        Endorsement = post.TotalReactionsCount ?? post.LikeCount ?? post.DiggCount ?? post.FavoriteCount ?? post.Virality ?? 0;
        CommentCount = post.CommentCount;
        Shares = post.RetweetCount ?? post.ShareCount ?? 0;
        Duration = post.Duration ?? post.VideoDurationSeconds ?? 0;
        Tag = post.TagFeedLocations?.FirstOrDefault();
        AutoSentiment = post.AutoSentiment;
    }

    public long Id { get; set; }
    public string Title { get; set; }
    public string Thumbnail { get; set; }
    public long InsertTime { get; set; }
    public string Type { get; set; }
    public string From { get; set; }
    public string? Location { get; set; }
    public string Url { get; set; }
    public string KeywordName { get; set; }
    public Tag? Tag { get; set; }
    public int VideoViews { get; set; }
    public int Reach { get; set; }
    public int SourceReach { get; set; }
    public int Interaction { get; set; }
    public int InfluenceScore { get; set; }
    public int FollowersCount { get; set; }
    public double Endorsement { get; set; }
    public int CommentCount { get; set; }
    public int Shares { get; set; }
    public int Duration { get; set; }
    public string AutoSentiment { get; set; }

    private string getLocation(List<string>? values)
    {
        var locale = values?.FirstOrDefault(); 
        if (locale == null) { return "N/A"; }
        return locale;
    }
    //new CultureInfo(post.Locations.FirstOrDefault()).DisplayName;
}

using System.Text.RegularExpressions;
// TabContentController.cs

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class TabContentController : Controller
{
    private readonly IApiService _apiService;
    private readonly int TwoWeeksAgoTimestamp = (int)DateTimeOffset.UtcNow.AddDays(-14).ToUnixTimeSeconds();
    private readonly int NowTimestamp = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
    private readonly int DefaultCount = 100;
    private readonly int DFLGroupId = 215519;
    private readonly int AllKeywords = 0;
    private List<PostTableViewModel> V1ViewModel = new List<PostTableViewModel>();
    private List<PostTableViewModel> V2ViewModel = new List<PostTableViewModel>();

    public TabContentController(IApiService apiService)
    {
        _apiService = apiService;
    }

    public async Task<IActionResult> Keywords(int groupId)
    {
        var keywords = await _apiService.GetKeywords(groupId);
        return Json(keywords.data.Keywords);
    }

    [HttpPost]
    public async Task<IActionResult> LoadData(int id) {
        return await UpdateData(TwoWeeksAgoTimestamp, NowTimestamp, DefaultCount, DFLGroupId, AllKeywords,  id);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateData(int from, int to, int count, int groupId, int keywordId, int id)
    {
        if (id == 1) {
            var V1Mentions = await _apiService.GetV1Mentions(from, to, groupId, keywordId, count);
            V1ViewModel = new List<PostTableViewModel>();
            V1ViewModel = V1Mentions.Data.Response.Select(post => new PostTableViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Thumbnail = post.Image,
                InsertTime = post.InsertTime,
                Type = post.Type,
                From = post.From,
                Url = post.Url,
                KeywordName = post.KeywordName,
                Reach = post.Reach,
                SourceReach = post.SourceReach,
                Interaction = post.Interaction,
                InfluenceScore = post.InfluenceScore,
                LikeCount = post.LikeCount,
                CommentCount = post.CommentCount,
                ShareCount = post.ShareCount,
                Tag = post.TagFeedLocations?.FirstOrDefault(),
                Virality = post.Virality,
                AutoSentiment = post.AutoSentiment

            }).ToList();
            return Json(V1ViewModel);
        } else {
            var V2Mentions = await _apiService.GetV2Mentions(from, to, groupId, keywordId, count);
            V2ViewModel = new List<PostTableViewModel>();
            V2ViewModel = V2Mentions.Mentions.Select(post => new PostTableViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Thumbnail = post.Image,
                InsertTime = post.InsertTime,
                Type = post.Type,
                From = post.From,
                Url = post.Url,
                KeywordName = post.KeywordName,
                Reach = post.Reach,
                SourceReach = post.SourceReach,
                Interaction = post.Interaction,
                InfluenceScore = post.InfluenceScore,
                LikeCount = post.LikeCount,
                CommentCount = post.CommentCount,
                ShareCount = post.ShareCount,
                Tag = post.TagFeedLocations?.FirstOrDefault(),
                Virality = post.Virality,
                AutoSentiment = post.AutoSentiment
            }).ToList();
            return Json(V2ViewModel);
        }
    }
}

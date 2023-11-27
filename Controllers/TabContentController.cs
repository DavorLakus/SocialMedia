// TabContentController.cs

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

public class TabContentController : Controller
{
    private readonly IApiService _apiService;

    public TabContentController(IApiService apiService)
    {
        _apiService = apiService;
    }

    [HttpPost]
    public async Task<IActionResult> TabContent(int from, int to, int count)
    {
         // Calculate Unix timestamps for two weeks ago and now
        var twoWeeksAgoTimestamp = DateTimeOffset.UtcNow.AddDays(-14).ToUnixTimeSeconds();
        var nowTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        // Fetch mentions for two weeks ago, now, and 100
        var mentions = await _apiService.GetV1Mentions(twoWeeksAgoTimestamp, nowTimestamp, count);
        // return PartialView(mentions);

        return PartialView("TabView", mentions);
    }

    [HttpPost]
    public async Task<IActionResult> LoadData(int from, int to, int count, int id)
    {
         // Calculate Unix timestamps for two weeks ago and now
        var twoWeeksAgoTimestamp = DateTimeOffset.UtcNow.AddDays(-14).ToUnixTimeSeconds();
        var nowTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        List<PostTableViewModel> mentions = new List<PostTableViewModel>();
        if (id == 1) {
            var v1mentions = await _apiService.GetV1Mentions(twoWeeksAgoTimestamp, nowTimestamp, count);
            mentions = v1mentions.Data.Response.Select(post => new PostTableViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Thumbnail = post.Image,
                InsertTime = post.InsertTime,
                Type = post.Type,
                From = post.From,
                Url = post.Url
            }).ToList();
        } else if (id == 2) {
            var v2mentions = await _apiService.GetV2Mentions(twoWeeksAgoTimestamp, nowTimestamp, count);
            mentions = v2mentions.Mentions.Select(post => new PostTableViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Thumbnail = post.Image,
                InsertTime = post.InsertTime,
                Type = post.Type,
                From = post.From,
                Url = post.Url
            }).ToList();
        }

        return Json(mentions);
    }
}

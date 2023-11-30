using System.Reflection.Metadata.Ecma335;
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
    private readonly int DFLTagId = 14549;
    private readonly int EPLTagId = 16800;
    private readonly int EPLGroupId = 230416;
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
    public async Task<IActionResult> LoadData(int id)
    {
        return await UpdateData(TwoWeeksAgoTimestamp, NowTimestamp, DefaultCount, DFLGroupId, AllKeywords, false, id);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateData(int from, int to, int count, int groupId, int keywordId, bool tag, int id)
    {
        int tagId = tag ? getTagId(groupId) : 0;
        if (id == 1)
        {
            return Json(await _apiService.GetV1Mentions(from, to, groupId, keywordId, tagId, count));
        }
        else
        {
            return Json(await _apiService.GetV2Mentions(from, to, groupId, keywordId, tagId, count));
        }
    }

    private int getTagId(int groupId)
    {
        if (groupId == DFLGroupId) {
            return DFLTagId;
        } else if (groupId == EPLGroupId) {
            return EPLTagId;
        } else {
            return 0;
        }
    }
}

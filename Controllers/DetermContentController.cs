// TabContentController.cs

using Microsoft.AspNetCore.Mvc;

public class DetermContentController : Controller
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

    public DetermContentController(IApiService apiService)
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
    public async Task<IActionResult> UpdateData(int from, int to, int count, int groupId, int keywordId, bool tag, int id, int offset = 0)
    {
        int tagId = tag ? getTagId(groupId) : 0;
        if (id == 1)
        {
            return Json(await _apiService.GetV1Mentions(from, to, groupId, keywordId, tagId, count, offset));
        }
        else
        {
            return Json(await _apiService.GetV2Mentions(from, to, groupId, keywordId, tagId, count, offset));
        }
    }

    private int getTagId(int groupId)
    {
        if (groupId == DFLGroupId)
        {
            return DFLTagId;
        }
        else if (groupId == EPLGroupId)
        {
            return EPLTagId;
        }
        else
        {
            return 0;
        }
    }
}

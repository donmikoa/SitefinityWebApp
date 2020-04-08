using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SitefinityWebApp.Mvc.Models;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace SitefinityWebApp.Mvc.Controllers
{
    public class RecentVideosController : Controller
    {

        public static List<RecentVideos> GetVideoList()
        {
            List<RecentVideos> lstYtb = new List<RecentVideos>();
            try
            {
                var yt = new YouTubeService(new BaseClientService.Initializer() { ApiKey = "AIza66T1nNkMGy-1rdXZ9nkSOPPoO851a6i79o" });
                var channelsListRequest = yt.Channels.List("contentDetails");
                channelsListRequest.ForUsername = "UCIpaQzkTUSPsovR2lb1nFog";
                var srchListRqst = yt.Search.List("snippet");
                srchListRqst.ChannelId = "UCIpaQzkTUSPsovR2lb1nFog";
                var channelsListResponse = channelsListRequest.Execute();  
                foreach (var channel in channelsListResponse.Items)
                {
                    // of videos uploaded to the authenticated user's channel.
                    var uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;
                    var nextPageToken = "";
                    while (nextPageToken != null)
                    {
                        var playlistItemsListRequest = yt.PlaylistItems.List("snippet");
                        playlistItemsListRequest.PlaylistId = uploadsListId;
                        playlistItemsListRequest.MaxResults = 8;
                        playlistItemsListRequest.PageToken = nextPageToken;
                        // Retrieve the list of videos uploaded to the authenticated user's channel.
                        var playlistItemsListResponse = playlistItemsListRequest.Execute();
                        foreach (var playlistItem in playlistItemsListResponse.Items)
                        {
                            RecentVideos objYouTubeData = new RecentVideos();
                            objYouTubeData.VideoId = "https://www.youtube.com/embed/" + playlistItem.Snippet.ResourceId.VideoId;
                            objYouTubeData.Title = playlistItem.Snippet.Title;
                            objYouTubeData.Descriptions = playlistItem.Snippet.Description;
                            objYouTubeData.ImageUrl = playlistItem.Snippet.Thumbnails.High.Url;
                            objYouTubeData.IsValid = true;
                            lstYtb.Add(objYouTubeData);
                           
                        }
                        nextPageToken = playlistItemsListResponse.NextPageToken;
                    }
                }
            }
            catch (Exception e)
            {
                string ErrorMessage = "Some exception occured" + e.Data.ToString() + e.Message.ToString() + e.GetBaseException().ToString();

            }

            return lstYtb;

        }
        // GET: RecentVideos
        public ActionResult Index()
        {
            return View("Default");
        }

       
    }
}
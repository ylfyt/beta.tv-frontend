using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;


    public class Constants
    {
        public static string BASE_URL { get; } = "http://10.0.2.2:5000/api";
        public static string LOGIN_END_POINT { get; } = BASE_URL + "/User/login";
        public static string REGISTER_END_POINT { get; } = BASE_URL + "/User/register";
        public static string SEARCH_VIDEO_ENDPOINT { get; } = BASE_URL + "/video/search";
        public static string ME_END_POINT { get; } = BASE_URL + "/User/me";
        public static string CHANGE_PROFILE_DATA_END_POINT { get; } = BASE_URL + "/User/changeProfile";
        public static string CHANGE_PASS_END_POINT { get; } = BASE_URL + "/User/changePass";
        public static string CHANGE_PROFILE_IMAGE_END_POINT { get; } = BASE_URL + "/User/changeProfilePic";
        public static string LOGOUT_END_POINT { get; } = BASE_URL + "/User/logout";
        public static string HOMEPAGE_ALL_VIDEO_ENDPOINT { get; } = BASE_URL + "/Video";
        public static string VIDEO_ENDPOINT { get; } = BASE_URL + "/video";
        public static string COMMENT_ENDPOINT { get; } = BASE_URL + "/comment";
        public static string COMMENT_LIKE_ENDPOINT { get; } = BASE_URL + "/commentlike";
        public static string BOOKMARK_END_POINT { get; } = BASE_URL + "/Bookmark";
        public static string IS_BOOKMARKED_END_POINT { get; } = BASE_URL + "/Bookmark/isBookmarked";
        public static string NotInternetText { get; } = "No internet, please reconnect!";

        public static Color color0 = Color.FromHex("3F72AF");
    }


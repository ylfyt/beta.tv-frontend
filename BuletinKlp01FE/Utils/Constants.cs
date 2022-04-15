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
    public static string HOMEPAGE_ALL_VIDEO_ENDPOINT { get; } = BASE_URL + "/Video";
    public static string VIDEO_ENDPOINT { get; } = BASE_URL + "/video";
    public static string COMMENT_ENDPOINT { get; } = BASE_URL + "/comment";
    public static string COMMENT_LIKE_ENDPOINT { get; } = BASE_URL + "/commentlike";
    public static string CATEGORY_ENDPOINT { get; } = BASE_URL + "/Category";

    public static string ENDPOINT_USER_ME = "/user/me";
    public static string ENDPOINT_USER_LOGIN = "/user/login";
    public static string ENDPOINT_USER_REGISTER = "/user/register";

    public static string ENDPOINT_VIDEO_GET = "/video";
    public static string ENDPOINT_VIDEO_SEARCH = "/video/search";
    public static string ENDPOINT_COMMENT = "/comment";
    public static string ENDPOINT_COMMENT_LIKE = "/commentlike";
    public static string ENDPOINT_CATEGORY = "/category";

    public static string NotInternetText { get; } = "No internet, please reconnect!";

    public static Color color0 = Color.FromHex("3F72AF");
}


using System;
using System.Collections.Generic;
using System.Text;

namespace ProbForm.Alexa.Helpers
{
    public static class UrlKeys
    {
        public const string BASE_URL = "https://localhost:44337/api/values";

        public static string GET_ALL_MATCHES = BASE_URL + "/full";
        public static string GET_TEAMPLAYERS = BASE_URL + "/teamplayers/{0}";
        public static string GET_TEAMS = BASE_URL + "/teams";
        public static string GET_MATCH = BASE_URL + "/match?team1={0}&team2={1}";
        public static string GET_LIST_MATCHES = BASE_URL + "/";

        public static string SearchByDay(int day, string url)
        {
            return url.Contains("?") 
                    ? url + $"&day={day}" 
                    : url + $"?day={day}";
        }
    }
}

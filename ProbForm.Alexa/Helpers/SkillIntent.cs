using System;
using System.Collections.Generic;
using System.Text;

namespace ProbForm.Alexa.Helpers
{
    public static class SkillIntent
    {
        #region INTENT
        public const string PLAYERS                 = "players";
        public const string MATCH                   = "match";
        public const string MATCHES                 = "matches";
        public const string FULLMATCHES             = "full_matches";
        public const string HELPINTENT              = "AMAZON.HelpIntent";
        #endregion

    }
    public static class SkillSlots
    {
        #region SLOTS
        public const string PLAYER                  = "player";
        public const string FIRSTTEAM               = "first_team";
        public const string SECONDTEAM              = "second_team";
        public const string DAY                     = "day";
        #endregion

    }
}

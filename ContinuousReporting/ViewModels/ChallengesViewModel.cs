using System;
using System.Collections.Generic;

namespace ContinuousReporting.ViewModels
{
    public class ChallengesViewModel
    {
        public IEnumerable<MonthChallenge> MonthChallenges { get; set; }
    }

    public class MonthChallenge
    {
        public DateTime Date { get; set; }
        public IEnumerable<ChallengeSummary> Summary { get; set; }
    }
}
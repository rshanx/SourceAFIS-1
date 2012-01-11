using System;
using System.Collections.Generic;
using System.Text;
using SourceAFIS.Meta;
using SourceAFIS.General;

namespace SourceAFIS.Matching
{
    public sealed class MatchScoring
    {
        [Parameter(Upper = 10)]
        public float PairCountFactor = 0.48f;
        [Parameter(Upper = 100)]
        public float PairFractionFactor = 6.7f;
        [Parameter(Upper = 10)]
        public float CorrectTypeFactor = 0.07f;
        [Parameter(Upper = 10)]
        public float SupportedCountFactor = 0.4f;
        [Parameter(Upper = 10, Precision = 3)]
        public float EdgeCountFactor = 0.208f;

        public DetailLogger.Hook Logger = DetailLogger.Null;

        public float Compute(MatchAnalysis analysis)
        {
            float score = 0;
            
            score += PairCountFactor * analysis.PairCount;
            score += CorrectTypeFactor * analysis.CorrectTypeCount;
            score += SupportedCountFactor * analysis.SupportedCount;
            score += PairFractionFactor * analysis.PairFraction;
            score += EdgeCountFactor * analysis.EdgeCount;
            
            if (Logger.IsActive)
                Logger.Log(score);

            return score;
        }
    }
}

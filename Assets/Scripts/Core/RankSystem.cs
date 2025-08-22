using UnityEngine;

public static class RankSystem
{
    public struct RankEvaluation
    {
        public Rank rank;
        public float multiplier;

        public RankEvaluation(Rank rank, float multiplier)
        {
            this.rank = rank;
            this.multiplier = multiplier;
        }
    }

    static readonly RankEvaluation[] ranks = new RankEvaluation[]
    {
        new RankEvaluation(Rank.FMinus, 0.5f),
        new RankEvaluation(Rank.F, 0.55f),
        new RankEvaluation(Rank.FPlus, 0.6f),
        new RankEvaluation(Rank.DMinus, 0.65f),
        new RankEvaluation(Rank.D, 0.7f),
        new RankEvaluation(Rank.DPlus, 0.75f),
        new RankEvaluation(Rank.CMinus, 0.8f),
        new RankEvaluation(Rank.C, 0.9f),
        new RankEvaluation(Rank.CPlus, 1.0f),
        new RankEvaluation(Rank.BMinus, 1.1f),
        new RankEvaluation(Rank.B, 1.2f),
        new RankEvaluation(Rank.BPlus, 1.3f),
        new RankEvaluation(Rank.AMinus, 1.4f),
        new RankEvaluation(Rank.A, 1.5f),
        new RankEvaluation(Rank.APlus, 1.6f),
        new RankEvaluation(Rank.SMinus, 1.7f),
        new RankEvaluation(Rank.S, 1.8f),
        new RankEvaluation(Rank.SPlus, 1.9f),
        new RankEvaluation(Rank.SSMinus, 2.0f),
        new RankEvaluation(Rank.SS, 2.2f),
        new RankEvaluation(Rank.SSPlus, 2.4f),
        new RankEvaluation(Rank.SSSMinus, 2.6f),
        new RankEvaluation(Rank.SSS, 2.8f),
        new RankEvaluation(Rank.SSSPlus, 3.0f),
    };

    // Evaluate qualityScore (0-100) and return rank with multiplier
    public static RankEvaluation Evaluate(float qualityScore)
    {
        RankEvaluation result = ranks[0];
        float step = 100f / (ranks.Length);
        int index = Mathf.Clamp(Mathf.FloorToInt(qualityScore / step), 0, ranks.Length - 1);
        result = ranks[index];
        return result;
    }
}

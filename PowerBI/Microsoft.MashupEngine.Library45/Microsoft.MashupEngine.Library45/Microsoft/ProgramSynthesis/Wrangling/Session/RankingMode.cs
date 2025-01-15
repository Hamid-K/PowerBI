using System;

namespace Microsoft.ProgramSynthesis.Wrangling.Session
{
	// Token: 0x02000124 RID: 292
	public abstract class RankingMode
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0001522E File Offset: 0x0001342E
		public static RankingMode MostLikely { get; } = RankingMode.MostLikelyMode.Instance;

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x00015235 File Offset: 0x00013435
		public static RankingMode NoRanking { get; } = RankingMode.NoRankingMode.Instance;

		// Token: 0x02000125 RID: 293
		private class MostLikelyMode : RankingMode
		{
			// Token: 0x0600067E RID: 1662 RVA: 0x00015252 File Offset: 0x00013452
			private MostLikelyMode()
			{
			}

			// Token: 0x0600067F RID: 1663 RVA: 0x0001525A File Offset: 0x0001345A
			public override string ToString()
			{
				return "MostLikely";
			}

			// Token: 0x040002E0 RID: 736
			public static readonly RankingMode.MostLikelyMode Instance = new RankingMode.MostLikelyMode();
		}

		// Token: 0x02000126 RID: 294
		private class NoRankingMode : RankingMode
		{
			// Token: 0x06000681 RID: 1665 RVA: 0x00015252 File Offset: 0x00013452
			private NoRankingMode()
			{
			}

			// Token: 0x06000682 RID: 1666 RVA: 0x0001526D File Offset: 0x0001346D
			public override string ToString()
			{
				return "NoRanking";
			}

			// Token: 0x040002E1 RID: 737
			public static readonly RankingMode.NoRankingMode Instance = new RankingMode.NoRankingMode();
		}
	}
}

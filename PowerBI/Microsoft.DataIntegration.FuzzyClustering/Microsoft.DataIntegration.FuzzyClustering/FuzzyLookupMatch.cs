using System;
using System.Diagnostics;

namespace Microsoft.DataIntegration.FuzzyClustering
{
	// Token: 0x0200000A RID: 10
	[DebuggerDisplay("Value={Value}, Match={Match}, Sim={Similarity}")]
	internal class FuzzyLookupMatch
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002E12 File Offset: 0x00001012
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002E1A File Offset: 0x0000101A
		public int DedupId { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002E23 File Offset: 0x00001023
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002E2B File Offset: 0x0000102B
		public int MatchDedupId { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002E34 File Offset: 0x00001034
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002E3C File Offset: 0x0000103C
		public float Similarity { get; private set; }

		// Token: 0x06000036 RID: 54 RVA: 0x00002E45 File Offset: 0x00001045
		public FuzzyLookupMatch(int dedupId, int matchDedupId, float similarity)
		{
			this.DedupId = dedupId;
			this.MatchDedupId = matchDedupId;
			this.Similarity = similarity;
		}
	}
}

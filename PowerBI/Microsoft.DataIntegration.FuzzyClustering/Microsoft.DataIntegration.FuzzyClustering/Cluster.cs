using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyClustering
{
	// Token: 0x0200000B RID: 11
	internal class Cluster
	{
		// Token: 0x06000037 RID: 55 RVA: 0x00002E62 File Offset: 0x00001062
		internal Cluster()
		{
			this.matches = new List<FuzzyLookupMatch>();
			this.minSimilarity = 1f;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002E80 File Offset: 0x00001080
		internal float MinSimilarity
		{
			get
			{
				return this.minSimilarity;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002E88 File Offset: 0x00001088
		internal void AddSingleColumnFuzzyLookupMatch(FuzzyLookupMatch match)
		{
			this.matches.Add(match);
			this.minSimilarity = Math.Min(match.Similarity, this.minSimilarity);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002EAD File Offset: 0x000010AD
		internal IEnumerable<FuzzyLookupMatch> Matches
		{
			get
			{
				return this.matches;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002EB5 File Offset: 0x000010B5
		internal void Merge(Cluster other)
		{
			if (this != other)
			{
				this.matches.AddRange(other.matches);
				this.minSimilarity = Math.Min(other.minSimilarity, this.minSimilarity);
			}
		}

		// Token: 0x0400001B RID: 27
		private List<FuzzyLookupMatch> matches;

		// Token: 0x0400001C RID: 28
		private float minSimilarity;
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.DataIntegration.FuzzyClustering
{
	// Token: 0x02000010 RID: 16
	internal class RepresentativeChooser
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003A9C File Offset: 0x00001C9C
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00003AA4 File Offset: 0x00001CA4
		public DedupContext Context { get; private set; }

		// Token: 0x06000053 RID: 83 RVA: 0x00003AAD File Offset: 0x00001CAD
		public RepresentativeChooser(DedupContext context)
		{
			this.Context = context;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003ABC File Offset: 0x00001CBC
		public int GetRepresentativeId(Cluster cluster)
		{
			foreach (FuzzyLookupMatch fuzzyLookupMatch in cluster.Matches)
			{
				int dedupId = fuzzyLookupMatch.DedupId;
				if (this.Context.IsRepresentativeIdInConstraints(dedupId))
				{
					return dedupId;
				}
				int matchDedupId = fuzzyLookupMatch.MatchDedupId;
				if (this.Context.IsRepresentativeIdInConstraints(matchDedupId))
				{
					return matchDedupId;
				}
			}
			int num = -1;
			int num2 = 0;
			foreach (FuzzyLookupMatch fuzzyLookupMatch2 in cluster.Matches)
			{
				int dedupId2 = fuzzyLookupMatch2.DedupId;
				int frequency = this.Context.GetFrequency(dedupId2);
				if (frequency > num2)
				{
					num = dedupId2;
					num2 = frequency;
				}
				int matchDedupId2 = fuzzyLookupMatch2.MatchDedupId;
				int frequency2 = this.Context.GetFrequency(matchDedupId2);
				if (frequency2 > num2)
				{
					num = matchDedupId2;
					num2 = frequency2;
				}
			}
			return num;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003BC4 File Offset: 0x00001DC4
		public int GetRepresentative(int valueId)
		{
			return this.Context.DedupIdToInputRows[valueId].RepreRowId;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003BDC File Offset: 0x00001DDC
		private DedupRecord GetRepresentative(IEnumerable<DedupRecord> rows)
		{
			DedupRecord dedupRecord = null;
			int num = -1;
			foreach (DedupRecord dedupRecord2 in rows)
			{
				if (dedupRecord2.Freq >= num)
				{
					dedupRecord = dedupRecord2;
					num = dedupRecord2.Freq;
				}
			}
			return dedupRecord;
		}
	}
}

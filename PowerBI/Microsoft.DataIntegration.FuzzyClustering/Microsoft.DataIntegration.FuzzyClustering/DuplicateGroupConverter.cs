using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyClustering
{
	// Token: 0x02000011 RID: 17
	internal class DuplicateGroupConverter
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003C34 File Offset: 0x00001E34
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003C3C File Offset: 0x00001E3C
		public DedupContext Context { get; private set; }

		// Token: 0x06000059 RID: 89 RVA: 0x00003C45 File Offset: 0x00001E45
		public DuplicateGroupConverter(DedupContext context, RepresentativeChooser representativeChooser)
		{
			this.Context = context;
			this.representativeChooser = representativeChooser;
			int maxValueId = context.MaxValueId;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003C64 File Offset: 0x00001E64
		private int GetOrInsert(int id)
		{
			int count;
			if (this.idToIndexMap.TryGetValue(id, ref count))
			{
				return count;
			}
			count = this.idToIndexMap.Count;
			this.idToIndexMap.Add(id, count);
			return count;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003C9D File Offset: 0x00001E9D
		private static int CompareMatch(FuzzyLookupMatch x, FuzzyLookupMatch y)
		{
			return Math.Sign(y.Similarity - x.Similarity);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003CB4 File Offset: 0x00001EB4
		private void Initialize(Cluster cluster)
		{
			this.idToIndexMap = new SortedList<int, int>();
			foreach (FuzzyLookupMatch fuzzyLookupMatch in cluster.Matches)
			{
				this.GetOrInsert(fuzzyLookupMatch.DedupId);
				this.GetOrInsert(fuzzyLookupMatch.MatchDedupId);
			}
			int count = this.idToIndexMap.Count;
			this.parents = new List<int>(count);
			this.costs = new List<float>(count);
			this.matchesById = new List<List<FuzzyLookupMatch>>(count);
			for (int i = 0; i < count; i++)
			{
				this.parents.Add(-1);
				this.costs.Add(-1f);
				this.matchesById.Add(new List<FuzzyLookupMatch>());
			}
			foreach (FuzzyLookupMatch fuzzyLookupMatch2 in cluster.Matches)
			{
				this.matchesById[this.idToIndexMap[fuzzyLookupMatch2.DedupId]].Add(fuzzyLookupMatch2);
				this.matchesById[this.idToIndexMap[fuzzyLookupMatch2.MatchDedupId]].Add(fuzzyLookupMatch2);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003E08 File Offset: 0x00002008
		public DuplicateGroup ToDuplicateGroup(Cluster cluster)
		{
			int representativeId = this.representativeChooser.GetRepresentativeId(cluster);
			DuplicateGroup duplicateGroup = this.CreateDuplicateGroup(representativeId);
			if (Enumerable.Count<FuzzyLookupMatch>(cluster.Matches) == 1)
			{
				FuzzyLookupMatch fuzzyLookupMatch = Enumerable.First<FuzzyLookupMatch>(cluster.Matches);
				int num = ((fuzzyLookupMatch.DedupId == representativeId) ? fuzzyLookupMatch.MatchDedupId : fuzzyLookupMatch.DedupId);
				this.AddDuplicate(duplicateGroup, num, fuzzyLookupMatch.Similarity);
				return duplicateGroup;
			}
			this.Initialize(cluster);
			int num2 = this.idToIndexMap[representativeId];
			this.parents[num2] = num2;
			this.costs[num2] = 1f;
			int count = this.idToIndexMap.Count;
			int num3 = 1;
			Heap<FuzzyLookupMatch> heap = new Heap<FuzzyLookupMatch>(new Comparison<FuzzyLookupMatch>(DuplicateGroupConverter.CompareMatch));
			int num4 = num2;
			bool flag;
			do
			{
				foreach (FuzzyLookupMatch fuzzyLookupMatch2 in this.matchesById[num4])
				{
					heap.Add(fuzzyLookupMatch2);
				}
				flag = false;
				while (heap.Count > 0)
				{
					FuzzyLookupMatch fuzzyLookupMatch3 = heap.Pop();
					int num5 = this.idToIndexMap[fuzzyLookupMatch3.DedupId];
					int num6 = this.idToIndexMap[fuzzyLookupMatch3.MatchDedupId];
					if (this.parents[num5] < 0 || this.parents[num6] < 0)
					{
						if (this.parents[num5] >= 0)
						{
							this.parents[num6] = num5;
							this.costs[num6] = Math.Min(fuzzyLookupMatch3.Similarity, this.costs[num5]);
							num4 = num6;
							num3++;
							flag = true;
							break;
						}
						this.parents[num5] = num6;
						this.costs[num5] = Math.Min(fuzzyLookupMatch3.Similarity, this.costs[num6]);
						num4 = num5;
						num3++;
						flag = true;
						break;
					}
				}
			}
			while (num3 != count && flag);
			foreach (KeyValuePair<int, int> keyValuePair in this.idToIndexMap)
			{
				int key = keyValuePair.Key;
				int value = keyValuePair.Value;
				if (key != representativeId && this.costs[value] > 0f)
				{
					this.AddDuplicate(duplicateGroup, key, this.costs[value]);
				}
			}
			return duplicateGroup;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000040A8 File Offset: 0x000022A8
		internal DuplicateGroup GetDuplicateGroupByCase(int representativeId)
		{
			return this.CreateDuplicateGroup(representativeId);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000040B4 File Offset: 0x000022B4
		private DuplicateGroup CreateDuplicateGroup(int representativeId)
		{
			GroupWithRepresentative groupWithRepresentative = this.Context.DedupIdToInputRows[representativeId];
			int repreRowId = groupWithRepresentative.RepreRowId;
			DuplicateGroup duplicateGroup = new DuplicateGroup(groupWithRepresentative.RepreRowId);
			foreach (int num in groupWithRepresentative.RowIds)
			{
				if (num != repreRowId)
				{
					duplicateGroup.Duplicates[num] = 1f;
				}
			}
			return duplicateGroup;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000413C File Offset: 0x0000233C
		private void AddDuplicate(DuplicateGroup group, int duplicateId, float similarity)
		{
			foreach (int num in this.Context.DedupIdToInputRows[duplicateId].RowIds)
			{
				group.Duplicates[num] = similarity;
			}
		}

		// Token: 0x0400002B RID: 43
		private RepresentativeChooser representativeChooser;

		// Token: 0x0400002D RID: 45
		private SortedList<int, int> idToIndexMap;

		// Token: 0x0400002E RID: 46
		private List<List<FuzzyLookupMatch>> matchesById;

		// Token: 0x0400002F RID: 47
		private List<int> parents;

		// Token: 0x04000030 RID: 48
		private List<float> costs;
	}
}

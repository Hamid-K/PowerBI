using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatching;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyClustering
{
	// Token: 0x02000012 RID: 18
	internal class HierarchicalFuzzyGrouper
	{
		// Token: 0x06000061 RID: 97 RVA: 0x000041A8 File Offset: 0x000023A8
		internal List<DuplicateGroup> Group(DataTable input, FuzzyLookupEntry.FuzzyLookupParameters parameters, DataTable transformTable, FuzzyDedupEntry.DedupConstraints constraints = null)
		{
			DedupContext dedupContext = new DedupContext(input, constraints);
			FuzzyLookupBuilder fuzzyLookupBuilder = FuzzyLookupEntry.CreateFuzzyLookupBuilder(transformTable, parameters);
			List<string> list = new List<string>();
			int count = input.Columns.Count;
			for (int i = 1; i < count; i++)
			{
				list.Add(input.Columns[i].ColumnName);
			}
			int num = count + count;
			FuzzyLookup fuzzyLookup = fuzzyLookupBuilder.CreateFuzzyLookup(dedupContext.DedupTable, parameters, list.ToArray());
			FuzzyQuery fuzzyQuery = fuzzyLookupBuilder.CreateFuzzyQuery(fuzzyLookup, parameters);
			List<FuzzyLookupMatch> list2 = new List<FuzzyLookupMatch>();
			FastInt64HashSet fastInt64HashSet = new FastInt64HashSet();
			using (IDataReader dataReader = dedupContext.DedupTable.CreateDataReader())
			{
				while (dataReader.Read())
				{
					int @int = dataReader.GetInt32(0);
					int num2 = -1;
					if (dedupContext.RowIdToDedupId.TryGetValue(@int, ref num2))
					{
						using (MatchResultsReader matchResultsReader = fuzzyQuery.Match(dataReader))
						{
							while (matchResultsReader.Read())
							{
								if (!matchResultsReader.IsDBNull(count))
								{
									int int2 = matchResultsReader.GetInt32(count);
									int num3 = -1;
									if (dedupContext.RowIdToDedupId.TryGetValue(int2, ref num3) && num2 != num3)
									{
										long key = DeduplicationUtils.GetKey(num2, num3);
										if (!fastInt64HashSet.Contains(key) && !dedupContext.IsIncompatible(num2, num3))
										{
											float num4 = (float)matchResultsReader.GetDouble(num);
											FuzzyLookupMatch fuzzyLookupMatch;
											if (num2 < num3)
											{
												fuzzyLookupMatch = new FuzzyLookupMatch(num2, num3, num4);
											}
											else
											{
												fuzzyLookupMatch = new FuzzyLookupMatch(num3, num2, num4);
											}
											list2.Add(fuzzyLookupMatch);
											fastInt64HashSet.Add(key);
										}
									}
								}
							}
						}
					}
				}
			}
			GreedyMerger greedyMerger = this.CreateGreedyMerger(dedupContext);
			DuplicateGroupConverter duplicateGroupConverter = this.CreateDuplicateGroupConverter(dedupContext);
			List<DuplicateGroup> list3 = new List<DuplicateGroup>();
			HashSet<int> hashSet = new HashSet<int>();
			foreach (Cluster cluster in greedyMerger.ClusterMatches(list2))
			{
				foreach (FuzzyLookupMatch fuzzyLookupMatch2 in cluster.Matches)
				{
					hashSet.Add(fuzzyLookupMatch2.DedupId);
					hashSet.Add(fuzzyLookupMatch2.MatchDedupId);
				}
				DuplicateGroup duplicateGroup = duplicateGroupConverter.ToDuplicateGroup(cluster);
				list3.Add(duplicateGroup);
			}
			for (int j = 0; j <= dedupContext.MaxValueId; j++)
			{
				if (dedupContext.HasMultipleValues(j) && !hashSet.Contains(j))
				{
					DuplicateGroup duplicateGroupByCase = duplicateGroupConverter.GetDuplicateGroupByCase(j);
					list3.Add(duplicateGroupByCase);
				}
			}
			return list3;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004464 File Offset: 0x00002664
		private GreedyMerger CreateGreedyMerger(DedupContext context)
		{
			MergeClusterStrategy mergeClusterStrategy = MergeClusterStrategy.Weighted;
			return new GreedyMerger(context, mergeClusterStrategy);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000447C File Offset: 0x0000267C
		private DuplicateGroupConverter CreateDuplicateGroupConverter(DedupContext context)
		{
			RepresentativeChooser representativeChooser = new RepresentativeChooser(context);
			return new DuplicateGroupConverter(context, representativeChooser);
		}
	}
}

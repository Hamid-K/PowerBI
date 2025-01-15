using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.DataRefresh
{
	// Token: 0x0200021A RID: 538
	internal sealed class PartitionPolicyRangeMap
	{
		// Token: 0x06001E46 RID: 7750 RVA: 0x000CAECC File Offset: 0x000C90CC
		public PartitionPolicyRangeMap(Table table, DateTime effectiveTime, bool refreshPartitions, bool refreshAllExistingPartitions)
		{
			this.Table = table;
			this.PartitionsToDelete = new List<Partition>();
			this.PartitionPolicyRangesToCreate = new List<PolicyRangePartitionSource>();
			this.PartitionPolicyRangesToRefresh = new List<PolicyRangePartitionSource>();
			this.PartitionPolicyRangesToMerge = new List<IList<PolicyRangePartitionSource>>();
			this.OriginalRefreshBookmarks = new Dictionary<string, string>();
			this.Initialize(effectiveTime, refreshPartitions, refreshAllExistingPartitions);
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001E47 RID: 7751 RVA: 0x000CAF27 File Offset: 0x000C9127
		// (set) Token: 0x06001E48 RID: 7752 RVA: 0x000CAF2F File Offset: 0x000C912F
		public Table Table { get; private set; }

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001E49 RID: 7753 RVA: 0x000CAF38 File Offset: 0x000C9138
		// (set) Token: 0x06001E4A RID: 7754 RVA: 0x000CAF40 File Offset: 0x000C9140
		public IList<Partition> PartitionsToDelete { get; private set; }

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001E4B RID: 7755 RVA: 0x000CAF49 File Offset: 0x000C9149
		// (set) Token: 0x06001E4C RID: 7756 RVA: 0x000CAF51 File Offset: 0x000C9151
		public IList<PolicyRangePartitionSource> PartitionPolicyRangesToCreate { get; private set; }

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001E4D RID: 7757 RVA: 0x000CAF5A File Offset: 0x000C915A
		// (set) Token: 0x06001E4E RID: 7758 RVA: 0x000CAF62 File Offset: 0x000C9162
		public IList<PolicyRangePartitionSource> PartitionPolicyRangesToRefresh { get; private set; }

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001E4F RID: 7759 RVA: 0x000CAF6B File Offset: 0x000C916B
		// (set) Token: 0x06001E50 RID: 7760 RVA: 0x000CAF73 File Offset: 0x000C9173
		public IList<IList<PolicyRangePartitionSource>> PartitionPolicyRangesToMerge { get; private set; }

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001E51 RID: 7761 RVA: 0x000CAF7C File Offset: 0x000C917C
		// (set) Token: 0x06001E52 RID: 7762 RVA: 0x000CAF84 File Offset: 0x000C9184
		public PolicyRangePartitionSource DirectQueryPartitionToCreate { get; private set; }

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001E53 RID: 7763 RVA: 0x000CAF8D File Offset: 0x000C918D
		// (set) Token: 0x06001E54 RID: 7764 RVA: 0x000CAF95 File Offset: 0x000C9195
		public IDictionary<string, string> OriginalRefreshBookmarks { get; private set; }

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001E55 RID: 7765 RVA: 0x000CAF9E File Offset: 0x000C919E
		// (set) Token: 0x06001E56 RID: 7766 RVA: 0x000CAFA6 File Offset: 0x000C91A6
		public bool HasPollingExpression { get; private set; }

		// Token: 0x06001E57 RID: 7767 RVA: 0x000CAFB0 File Offset: 0x000C91B0
		private static IDictionary<PartitionPolicyRangeMap.PolicyRangeSpan, PolicyRangePartitionSource> BuildPolicyRangesMap(IEnumerable<PolicyRangePartitionSource> policyRanges)
		{
			Dictionary<PartitionPolicyRangeMap.PolicyRangeSpan, PolicyRangePartitionSource> dictionary = new Dictionary<PartitionPolicyRangeMap.PolicyRangeSpan, PolicyRangePartitionSource>();
			foreach (PolicyRangePartitionSource policyRangePartitionSource in policyRanges)
			{
				dictionary[new PartitionPolicyRangeMap.PolicyRangeSpan(policyRangePartitionSource.Start, policyRangePartitionSource.End)] = policyRangePartitionSource;
			}
			return dictionary;
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x000CB010 File Offset: 0x000C9210
		private static bool TryFindPartitionStartingFrom(IList<PolicyRangePartitionSource> sortedPolicyRanges, DateTime start, out PolicyRangePartitionSource result)
		{
			foreach (PolicyRangePartitionSource policyRangePartitionSource in sortedPolicyRanges)
			{
				int num = DateTime.Compare(policyRangePartitionSource.Start, start);
				if (num > 0)
				{
					break;
				}
				if (num == 0)
				{
					result = policyRangePartitionSource;
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x000CB074 File Offset: 0x000C9274
		private static bool TryFindPartitionEndingAt(IList<PolicyRangePartitionSource> sortedPolicyRanges, DateTime end, out PolicyRangePartitionSource result)
		{
			foreach (PolicyRangePartitionSource policyRangePartitionSource in sortedPolicyRanges)
			{
				int num = DateTime.Compare(policyRangePartitionSource.End, end);
				if (num > 0)
				{
					break;
				}
				if (num == 0)
				{
					result = policyRangePartitionSource;
					return true;
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x000CB0D8 File Offset: 0x000C92D8
		private static void SortAndValidateCurrentPartitioning(List<PolicyRangePartitionSource> currentPartitionPolicyRanges, PolicyRangePartitionSource currentDQPartitionSource)
		{
			currentPartitionPolicyRanges.Sort(PartitionPolicyRangeMap.rangeComparison);
			PolicyRangePartitionSource policyRangePartitionSource = null;
			foreach (PolicyRangePartitionSource policyRangePartitionSource2 in currentPartitionPolicyRanges)
			{
				if (policyRangePartitionSource == null)
				{
					policyRangePartitionSource = policyRangePartitionSource2;
				}
				else
				{
					policyRangePartitionSource = policyRangePartitionSource2;
				}
			}
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x000CB138 File Offset: 0x000C9338
		private static DateTime CalculateDQPartitionEnd(DateTime startTime)
		{
			return PropertyHelper.StartOfYear(startTime.AddYears(6));
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x000CB148 File Offset: 0x000C9348
		private void Initialize(DateTime effectiveTime, bool refreshPartitions, bool refreshAllExistingPartitions)
		{
			DateTime dateTime;
			List<PolicyRangePartitionSource> list;
			PolicyRangePartitionSource policyRangePartitionSource;
			this.GetCurrentPartitioning(out dateTime, out list, out policyRangePartitionSource);
			List<PolicyRangePartitionSource> list2;
			RefreshTimeLine refreshTimeLine;
			PolicyRangePartitionSource policyRangePartitionSource2;
			this.GenerateTargetPartitioning(effectiveTime, dateTime, out list2, out refreshTimeLine, out policyRangePartitionSource2);
			if (list.Count == 0)
			{
				using (List<PolicyRangePartitionSource>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PolicyRangePartitionSource policyRangePartitionSource3 = enumerator.Current;
						this.PartitionPolicyRangesToCreate.Add(policyRangePartitionSource3);
						this.PartitionPolicyRangesToRefresh.Add(policyRangePartitionSource3);
					}
					goto IL_00A8;
				}
			}
			this.HandleOutOfScopeCurrentPartitionPolicyRanges(list, refreshTimeLine);
			IDictionary<PartitionPolicyRangeMap.PolicyRangeSpan, PolicyRangePartitionSource> dictionary = PartitionPolicyRangeMap.BuildPolicyRangesMap(list);
			IDictionary<PartitionPolicyRangeMap.PolicyRangeSpan, PolicyRangePartitionSource> dictionary2 = PartitionPolicyRangeMap.BuildPolicyRangesMap(list2);
			this.HandlePartitionsInTargetIncrementalWindow(list, list2, refreshTimeLine, dictionary, dictionary2);
			this.HandlePartitionsStartingBetweenTails(list, list2, refreshTimeLine, dictionary, dictionary2, refreshPartitions);
			if (refreshAllExistingPartitions)
			{
				this.EnsureAllExistingPartitionsRefreshed(list);
			}
			IL_00A8:
			if (policyRangePartitionSource != null || policyRangePartitionSource2 != null)
			{
				this.HandleDQPartitions(policyRangePartitionSource, policyRangePartitionSource2);
			}
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x000CB220 File Offset: 0x000C9420
		private void EnsureAllExistingPartitionsRefreshed(List<PolicyRangePartitionSource> currentRanges)
		{
			foreach (PolicyRangePartitionSource policyRangePartitionSource in currentRanges)
			{
				if (!this.PartitionsToDelete.Contains(policyRangePartitionSource.Partition) && !this.PartitionPolicyRangesToRefresh.Contains(policyRangePartitionSource))
				{
					this.PartitionPolicyRangesToRefresh.Add(policyRangePartitionSource);
				}
			}
		}

		// Token: 0x06001E5E RID: 7774 RVA: 0x000CB294 File Offset: 0x000C9494
		private void GetCurrentPartitioning(out DateTime existingIncrementalHead, out List<PolicyRangePartitionSource> currentImportRanges, out PolicyRangePartitionSource currentDQRange)
		{
			currentImportRanges = new List<PolicyRangePartitionSource>();
			currentDQRange = null;
			if (this.Table.Partitions.Count == 0)
			{
				existingIncrementalHead = DateTime.MinValue;
				return;
			}
			foreach (Partition partition in this.Table.Partitions)
			{
				if (partition.SourceType == PartitionSourceType.PolicyRange)
				{
					PolicyRangePartitionSource policyRangePartitionSource = (PolicyRangePartitionSource)partition.Source;
					if (partition.Mode == ModeType.DirectQuery)
					{
						currentDQRange = policyRangePartitionSource;
					}
					else
					{
						currentImportRanges.Add(policyRangePartitionSource);
						this.OriginalRefreshBookmarks.Add(partition.Name, policyRangePartitionSource.RefreshBookmark);
					}
				}
				else
				{
					this.PartitionsToDelete.Add(partition);
				}
			}
			if (currentImportRanges.Count == 0)
			{
				existingIncrementalHead = DateTime.MinValue;
				return;
			}
			PartitionPolicyRangeMap.SortAndValidateCurrentPartitioning(currentImportRanges, currentDQRange);
			existingIncrementalHead = currentImportRanges[currentImportRanges.Count - 1].Start;
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x000CB390 File Offset: 0x000C9590
		private void GenerateTargetPartitioning(DateTime effectiveTime, DateTime existingIncrementalHead, out List<PolicyRangePartitionSource> targetImportRanges, out RefreshTimeLine refreshTimeLine, out PolicyRangePartitionSource targetDQRange)
		{
			this.HasPollingExpression = !string.IsNullOrEmpty(this.Table.RefreshPolicy.PollingExpression);
			targetImportRanges = new List<PolicyRangePartitionSource>();
			refreshTimeLine = new RefreshTimeLine(effectiveTime, this.Table.RefreshPolicy, existingIncrementalHead);
			DateTime dateTime = refreshTimeLine.RollingWindowTail;
			DateTime incrementalTail = refreshTimeLine.IncrementalTail;
			DateTime dateTime2 = PropertyHelper.SubtractGranularity(refreshTimeLine.IncrementalTail, this.Table.RefreshPolicy.IncrementalGranularity);
			RefreshGranularityType refreshGranularityType = this.Table.RefreshPolicy.RollingWindowGranularity;
			while (DateTime.Compare(dateTime, dateTime2) <= 0)
			{
				DateTime dateTime3 = PropertyHelper.AddGranularity(dateTime, refreshGranularityType);
				if (DateTime.Compare(dateTime3, incrementalTail) <= 0)
				{
					targetImportRanges.Add(new PolicyRangePartitionSource
					{
						Start = dateTime,
						End = dateTime3,
						Granularity = refreshGranularityType
					});
					dateTime = dateTime3;
				}
				else
				{
					refreshGranularityType = PropertyHelper.DeGranularity(refreshGranularityType);
					if (refreshGranularityType < this.Table.RefreshPolicy.IncrementalGranularity)
					{
						IL_012B:
						while (DateTime.Compare(dateTime, refreshTimeLine.IncrementalHead) <= 0)
						{
							DateTime dateTime4 = PropertyHelper.AddGranularity(dateTime, this.Table.RefreshPolicy.IncrementalGranularity);
							targetImportRanges.Add(new PolicyRangePartitionSource
							{
								Start = dateTime,
								End = dateTime4,
								Granularity = this.Table.RefreshPolicy.IncrementalGranularity
							});
							dateTime = dateTime4;
						}
						targetDQRange = null;
						if (this.Table.RefreshPolicy.Mode == RefreshPolicyMode.Hybrid)
						{
							targetDQRange = new PolicyRangePartitionSource
							{
								Start = dateTime,
								End = PartitionPolicyRangeMap.CalculateDQPartitionEnd(dateTime),
								Granularity = this.Table.RefreshPolicy.IncrementalGranularity
							};
						}
						return;
					}
				}
			}
			goto IL_012B;
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x000CB520 File Offset: 0x000C9720
		private void HandleOutOfScopeCurrentPartitionPolicyRanges(IList<PolicyRangePartitionSource> currentPartitionPolicyRanges, RefreshTimeLine refreshTimeLine)
		{
			for (int i = currentPartitionPolicyRanges.Count - 1; i >= 0; i--)
			{
				PolicyRangePartitionSource policyRangePartitionSource = currentPartitionPolicyRanges[i];
				if (DateTime.Compare(policyRangePartitionSource.Start, refreshTimeLine.RollingWindowTail) < 0 || DateTime.Compare(policyRangePartitionSource.Start, refreshTimeLine.IncrementalHead) > 0)
				{
					this.PartitionsToDelete.Add(policyRangePartitionSource.Partition);
					currentPartitionPolicyRanges.RemoveAt(i);
				}
			}
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x000CB588 File Offset: 0x000C9788
		private void HandlePartitionsInTargetIncrementalWindow(IList<PolicyRangePartitionSource> currentPartitionPolicyRanges, IList<PolicyRangePartitionSource> targetPartitionPolicyRanges, RefreshTimeLine refreshTimeLine, IDictionary<PartitionPolicyRangeMap.PolicyRangeSpan, PolicyRangePartitionSource> existingPartitionPolicyRangesMap, IDictionary<PartitionPolicyRangeMap.PolicyRangeSpan, PolicyRangePartitionSource> targetPartitionPolicyRangesMap)
		{
			foreach (PolicyRangePartitionSource policyRangePartitionSource in targetPartitionPolicyRanges)
			{
				if (DateTime.Compare(policyRangePartitionSource.Start, refreshTimeLine.IncrementalTail) >= 0)
				{
					PolicyRangePartitionSource policyRangePartitionSource2;
					if (!existingPartitionPolicyRangesMap.TryGetValue(new PartitionPolicyRangeMap.PolicyRangeSpan(policyRangePartitionSource.Start, policyRangePartitionSource.End), out policyRangePartitionSource2))
					{
						policyRangePartitionSource2 = policyRangePartitionSource;
						this.PartitionPolicyRangesToCreate.Add(policyRangePartitionSource2);
					}
					this.PartitionPolicyRangesToRefresh.Add(policyRangePartitionSource2);
				}
			}
			foreach (PolicyRangePartitionSource policyRangePartitionSource3 in currentPartitionPolicyRanges)
			{
				if (DateTime.Compare(policyRangePartitionSource3.Start, refreshTimeLine.IncrementalTail) >= 0 && !targetPartitionPolicyRangesMap.ContainsKey(new PartitionPolicyRangeMap.PolicyRangeSpan(policyRangePartitionSource3.Start, policyRangePartitionSource3.End)))
				{
					this.PartitionsToDelete.Add(policyRangePartitionSource3.Partition);
				}
			}
		}

		// Token: 0x06001E62 RID: 7778 RVA: 0x000CB684 File Offset: 0x000C9884
		private void HandlePartitionsStartingBetweenTails(IList<PolicyRangePartitionSource> currentPartitionPolicyRanges, IList<PolicyRangePartitionSource> targetPartitionPolicyRanges, RefreshTimeLine refreshTimeLine, IDictionary<PartitionPolicyRangeMap.PolicyRangeSpan, PolicyRangePartitionSource> existingPartitionPolicyRangesMap, IDictionary<PartitionPolicyRangeMap.PolicyRangeSpan, PolicyRangePartitionSource> targetPartitionPolicyRangesMap, bool refreshPartitions)
		{
			foreach (PolicyRangePartitionSource policyRangePartitionSource in targetPartitionPolicyRanges)
			{
				if (DateTime.Compare(policyRangePartitionSource.Start, refreshTimeLine.IncrementalTail) < 0 && !existingPartitionPolicyRangesMap.ContainsKey(new PartitionPolicyRangeMap.PolicyRangeSpan(policyRangePartitionSource.Start, policyRangePartitionSource.End)))
				{
					PolicyRangePartitionSource policyRangePartitionSource2;
					if (PartitionPolicyRangeMap.TryFindPartitionStartingFrom(currentPartitionPolicyRanges, policyRangePartitionSource.Start, out policyRangePartitionSource2))
					{
						PolicyRangePartitionSource policyRangePartitionSource3;
						if (DateTime.Compare(policyRangePartitionSource2.End, policyRangePartitionSource.End) > 0)
						{
							this.AddNonExistingParAndDeleteBiggerExisting(policyRangePartitionSource, policyRangePartitionSource2);
						}
						else if (!PartitionPolicyRangeMap.TryFindPartitionEndingAt(currentPartitionPolicyRanges, policyRangePartitionSource.End, out policyRangePartitionSource3))
						{
							this.AddNonExistingParAndDeleteCoverings(currentPartitionPolicyRanges, policyRangePartitionSource);
						}
						else
						{
							IList<PolicyRangePartitionSource> list;
							this.GetToMergeListOrRefreshCleared(currentPartitionPolicyRanges, policyRangePartitionSource2.Start, policyRangePartitionSource3.End, out list);
							if (refreshPartitions)
							{
								this.PartitionPolicyRangesToCreate.Add(policyRangePartitionSource);
								this.PartitionPolicyRangesToMerge.Add(list);
							}
							else if (list.All((PolicyRangePartitionSource range) => range.Partition.State == ObjectState.NoData))
							{
								foreach (PolicyRangePartitionSource policyRangePartitionSource4 in list)
								{
									this.PartitionPolicyRangesToRefresh.Remove(policyRangePartitionSource4);
									this.PartitionsToDelete.Add(policyRangePartitionSource4.Partition);
								}
								this.PartitionPolicyRangesToCreate.Add(policyRangePartitionSource);
							}
						}
					}
					else
					{
						this.AddNonExistingParAndDeleteCoverings(currentPartitionPolicyRanges, policyRangePartitionSource);
					}
				}
			}
		}

		// Token: 0x06001E63 RID: 7779 RVA: 0x000CB834 File Offset: 0x000C9A34
		private void AddNonExistingParAndDeleteBiggerExisting(PolicyRangePartitionSource policyRange, PolicyRangePartitionSource biggerPartitionWithSameStart)
		{
			this.PartitionPolicyRangesToCreate.Add(policyRange);
			this.PartitionPolicyRangesToRefresh.Add(policyRange);
			this.PartitionsToDelete.Add(biggerPartitionWithSameStart.Partition);
		}

		// Token: 0x06001E64 RID: 7780 RVA: 0x000CB860 File Offset: 0x000C9A60
		private void AddNonExistingParAndDeleteCoverings(IList<PolicyRangePartitionSource> currentPartitionPolicyRanges, PolicyRangePartitionSource targetPolicyRange)
		{
			this.PartitionPolicyRangesToCreate.Add(targetPolicyRange);
			this.PartitionPolicyRangesToRefresh.Add(targetPolicyRange);
			foreach (PolicyRangePartitionSource policyRangePartitionSource in currentPartitionPolicyRanges)
			{
				if (DateTime.Compare(policyRangePartitionSource.Start, targetPolicyRange.Start) >= 0 && DateTime.Compare(policyRangePartitionSource.End, targetPolicyRange.End) <= 0)
				{
					this.PartitionsToDelete.Add(policyRangePartitionSource.Partition);
				}
			}
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x000CB8F4 File Offset: 0x000C9AF4
		private void GetToMergeListOrRefreshCleared(IList<PolicyRangePartitionSource> currentPartitionPolicyRanges, DateTime start, DateTime end, out IList<PolicyRangePartitionSource> candidatesToMerge)
		{
			candidatesToMerge = new List<PolicyRangePartitionSource>();
			foreach (PolicyRangePartitionSource policyRangePartitionSource in currentPartitionPolicyRanges)
			{
				if (DateTime.Compare(policyRangePartitionSource.Start, start) >= 0 && DateTime.Compare(policyRangePartitionSource.End, end) <= 0)
				{
					if (policyRangePartitionSource.Partition.State == ObjectState.NoData)
					{
						this.PartitionPolicyRangesToRefresh.Add(policyRangePartitionSource);
					}
					candidatesToMerge.Add(policyRangePartitionSource);
				}
			}
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x000CB980 File Offset: 0x000C9B80
		private void HandleDQPartitions(PolicyRangePartitionSource currentDQRange, PolicyRangePartitionSource targetDQRange)
		{
			if (currentDQRange != null && targetDQRange != null && DateTime.Compare(currentDQRange.Start, targetDQRange.Start) == 0 && currentDQRange.Granularity == targetDQRange.Granularity)
			{
				return;
			}
			if (currentDQRange != null)
			{
				this.PartitionsToDelete.Add(currentDQRange.Partition);
			}
			this.DirectQueryPartitionToCreate = targetDQRange;
		}

		// Token: 0x040006F2 RID: 1778
		private static readonly Comparison<PolicyRangePartitionSource> rangeComparison = (PolicyRangePartitionSource p, PolicyRangePartitionSource q) => p.Start.CompareTo(q.Start);

		// Token: 0x02000448 RID: 1096
		private struct PolicyRangeSpan : IEquatable<PartitionPolicyRangeMap.PolicyRangeSpan>
		{
			// Token: 0x0600291C RID: 10524 RVA: 0x000F1147 File Offset: 0x000EF347
			public PolicyRangeSpan(DateTime start, DateTime end)
			{
				this.Start = start;
				this.End = end;
			}

			// Token: 0x170007FE RID: 2046
			// (get) Token: 0x0600291D RID: 10525 RVA: 0x000F1157 File Offset: 0x000EF357
			public DateTime Start { get; }

			// Token: 0x170007FF RID: 2047
			// (get) Token: 0x0600291E RID: 10526 RVA: 0x000F115F File Offset: 0x000EF35F
			public DateTime End { get; }

			// Token: 0x0600291F RID: 10527 RVA: 0x000F1168 File Offset: 0x000EF368
			public override int GetHashCode()
			{
				return (this.Start.GetHashCode() * 397) ^ this.End.GetHashCode();
			}

			// Token: 0x06002920 RID: 10528 RVA: 0x000F1198 File Offset: 0x000EF398
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				if (this == obj)
				{
					return true;
				}
				if (obj is PartitionPolicyRangeMap.PolicyRangeSpan)
				{
					PartitionPolicyRangeMap.PolicyRangeSpan policyRangeSpan = (PartitionPolicyRangeMap.PolicyRangeSpan)obj;
					return this.Equals(policyRangeSpan);
				}
				return false;
			}

			// Token: 0x06002921 RID: 10529 RVA: 0x000F11D4 File Offset: 0x000EF3D4
			public bool Equals(PartitionPolicyRangeMap.PolicyRangeSpan other)
			{
				return this.Start.Equals(other.Start) && this.End.Equals(other.End);
			}
		}
	}
}

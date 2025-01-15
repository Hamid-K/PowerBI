using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyClustering
{
	// Token: 0x02000008 RID: 8
	internal class DedupContext
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002397 File Offset: 0x00000597
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000239F File Offset: 0x0000059F
		internal DataTable DedupTable { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000023A8 File Offset: 0x000005A8
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000023B0 File Offset: 0x000005B0
		internal Dictionary<int, int> RowIdToDedupId { get; private set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000023B9 File Offset: 0x000005B9
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000023C1 File Offset: 0x000005C1
		internal List<List<int>> PositiveCstrtDedupIdGroups { get; private set; }

		// Token: 0x06000022 RID: 34 RVA: 0x000023CC File Offset: 0x000005CC
		internal DedupContext(DataTable input, FuzzyDedupEntry.DedupConstraints constraints)
		{
			this.AggrIdenticalRecords(input);
			this.RowIdToDedupId = new Dictionary<int, int>();
			this.valueToIdMap = new Dictionary<DedupRecord, int>();
			this.values = new List<DedupRecord>();
			this.counts = new List<int>();
			int num = 0;
			this.DedupIdToInputRows = new List<GroupWithRepresentative>();
			foreach (KeyValuePair<DedupRecord, List<int>> keyValuePair in this.identicalRecordGroups)
			{
				DedupRecord key = keyValuePair.Key;
				int count = keyValuePair.Value.Count;
				int num2 = Enumerable.First<int>(keyValuePair.Value);
				int num3;
				if (this.valueToIdMap.TryGetValue(key, ref num3))
				{
					List<int> list = this.counts;
					int num4 = num3;
					list[num4] += count;
					foreach (int num5 in keyValuePair.Value)
					{
						this.RowIdToDedupId[num5] = num3;
					}
					GroupWithRepresentative groupWithRepresentative = this.DedupIdToInputRows[num3];
					groupWithRepresentative.RowIds.AddRange(keyValuePair.Value);
					if (count > groupWithRepresentative.RepreCount)
					{
						groupWithRepresentative.RepreRowId = num2;
						groupWithRepresentative.RepreCount = count;
					}
				}
				else
				{
					this.valueToIdMap.Add(key, num);
					this.values.Add(key);
					this.counts.Add(count);
					foreach (int num6 in keyValuePair.Value)
					{
						this.RowIdToDedupId[num6] = num;
					}
					this.DedupIdToInputRows.Add(new GroupWithRepresentative(num2, count, keyValuePair.Value));
					num++;
				}
			}
			this.PrepDedupTable(input);
			if (constraints != null)
			{
				UnionMerge unionMerge = null;
				int count2 = this.values.Count;
				foreach (FuzzyDedupEntry.PositiveConstraint positiveConstraint in constraints.PositiveConstraints)
				{
					if (positiveConstraint != null)
					{
						int representId = positiveConstraint.RepresentId;
						int num7 = this.RowIdToDedupId[representId];
						foreach (int num8 in positiveConstraint.IdToBeCollapsed)
						{
							int num9 = this.RowIdToDedupId[num8];
							if (num9 != num7)
							{
								if (unionMerge == null)
								{
									unionMerge = new UnionMerge(count2);
								}
								unionMerge.Union(num7, num9);
							}
						}
					}
				}
				if (unionMerge != null)
				{
					this.PositiveCstrtDedupIdGroups = Enumerable.ToList<List<int>>(Enumerable.Select<IGrouping<int, int>, List<int>>(Enumerable.ToLookup<KeyValuePair<int, int>, int, int>(Enumerable.Where<KeyValuePair<int, int>>(Enumerable.Select<int, KeyValuePair<int, int>>(Enumerable.Range(0, this.values.Count), (int x) => new KeyValuePair<int, int>(unionMerge.GetRoot(x), x)), (KeyValuePair<int, int> x) => x.Key != x.Value), (KeyValuePair<int, int> x) => x.Key, (KeyValuePair<int, int> x) => x.Value), delegate(IGrouping<int, int> x)
					{
						List<int> list2 = new List<int>();
						list2.Add(x.Key);
						list2.AddRange(x);
						return list2;
					}));
				}
				foreach (FuzzyDedupEntry.NegativeConstraint negativeConstraint in constraints.NegativeConstraints)
				{
					int num10 = this.RowIdToDedupId[negativeConstraint.Id];
					foreach (int num11 in negativeConstraint.IncompatibleIds)
					{
						int num12 = this.RowIdToDedupId[num11];
						if (num10 == num12)
						{
							throw new ArgumentException(string.Format("Negative constraint ({0} <-> {1}) is a pair of identical values", negativeConstraint.Id, num11), "constraints");
						}
						if (unionMerge != null && unionMerge.GetRoot(num10) == unionMerge.GetRoot(num12))
						{
							throw new ArgumentException(string.Format("Negative constraint ({0} <-> {1}) conflicts with positive constraint", negativeConstraint.Id, num11), "constraints");
						}
					}
				}
				HashSet<Tuple<int, int>> hashSet = this.FlattenNegativeConstraints(constraints.NegativeConstraints);
				if (Enumerable.Any<Tuple<int, int>>(hashSet))
				{
					this.idToIncompatibleIdsLookup = Enumerable.ToLookup<Tuple<int, int>, int, int>(hashSet, (Tuple<int, int> x) => x.Item1, (Tuple<int, int> x) => x.Item2);
					this.incompatibleValues = new FastInt64HashSet();
					foreach (KeyValuePair<int, int> keyValuePair2 in Enumerable.SelectMany<IGrouping<int, int>, KeyValuePair<int, int>>(this.idToIncompatibleIdsLookup, (IGrouping<int, int> x) => Enumerable.Select<int, KeyValuePair<int, int>>(x, (int y) => new KeyValuePair<int, int>(x.Key, y))))
					{
						if (keyValuePair2.Key < keyValuePair2.Value)
						{
							this.incompatibleValues.Add(DeduplicationUtils.GetKey(keyValuePair2.Key, keyValuePair2.Value));
						}
					}
				}
			}
			this.maxValueId = this.values.Count - 1;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002A40 File Offset: 0x00000C40
		private HashSet<Tuple<int, int>> FlattenNegativeConstraints(IEnumerable<FuzzyDedupEntry.NegativeConstraint> negativeConstraints)
		{
			HashSet<Tuple<int, int>> hashSet = new HashSet<Tuple<int, int>>();
			foreach (FuzzyDedupEntry.NegativeConstraint negativeConstraint in negativeConstraints)
			{
				if (negativeConstraint != null)
				{
					int num = this.RowIdToDedupId[negativeConstraint.Id];
					foreach (int num2 in negativeConstraint.IncompatibleIds)
					{
						int num3 = this.RowIdToDedupId[num2];
						hashSet.Add(Tuple.Create<int, int>(num, num3));
						hashSet.Add(Tuple.Create<int, int>(num3, num));
					}
				}
			}
			return hashSet;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002B0C File Offset: 0x00000D0C
		public bool IsRepresentativeIdInConstraints(int valueId)
		{
			return this.representativeIdsInConstraints != null && this.representativeIdsInConstraints.Contains(valueId);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002B24 File Offset: 0x00000D24
		public int MaxValueId
		{
			get
			{
				return this.maxValueId;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002B2C File Offset: 0x00000D2C
		public int GetFrequency(int dedupId)
		{
			return this.counts[dedupId];
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B3A File Offset: 0x00000D3A
		public DedupRecord GetValue(int dedupId)
		{
			return this.values[dedupId];
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002B48 File Offset: 0x00000D48
		public bool HasMultipleValues(int valueId)
		{
			return this.DedupIdToInputRows[valueId].RowIds.Count > 1;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002B63 File Offset: 0x00000D63
		public bool HasIncompatibleValueIds(int valueId)
		{
			return this.idToIncompatibleIdsLookup != null && this.idToIncompatibleIdsLookup.Contains(valueId);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002B7B File Offset: 0x00000D7B
		public IEnumerable<int> GetIncompatibleValueIds(int valueId)
		{
			if (this.idToIncompatibleIdsLookup == null)
			{
				return Enumerable.Empty<int>();
			}
			return this.idToIncompatibleIdsLookup[valueId];
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002B98 File Offset: 0x00000D98
		public bool IsIncompatible(int valueId, int matchValueId)
		{
			if (this.incompatibleValues == null || valueId == matchValueId)
			{
				return false;
			}
			long num = ((valueId < matchValueId) ? DeduplicationUtils.GetKey(valueId, matchValueId) : DeduplicationUtils.GetKey(matchValueId, valueId));
			return this.incompatibleValues.Contains(num);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002BD4 File Offset: 0x00000DD4
		private void AggrIdenticalRecords(DataTable input)
		{
			this.identicalRecordGroups = new Dictionary<DedupRecord, List<int>>();
			int count = input.Columns.Count;
			foreach (object obj in input.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				DedupRecord dedupRecord = new DedupRecord(dataRow, 1, count);
				int num = (int)dataRow[0];
				List<int> list;
				if (this.identicalRecordGroups.TryGetValue(dedupRecord, ref list))
				{
					list.Add(num);
				}
				else
				{
					Dictionary<DedupRecord, List<int>> dictionary = this.identicalRecordGroups;
					DedupRecord dedupRecord2 = dedupRecord;
					List<int> list2 = new List<int>();
					list2.Add(num);
					dictionary[dedupRecord2] = list2;
				}
				dedupRecord.IncFreq();
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002C8C File Offset: 0x00000E8C
		private void PrepDedupTable(DataTable input)
		{
			HashSet<int> hashSet = new HashSet<int>();
			this.DedupTable = input.Clone();
			foreach (object obj in input.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				int num = this.RowIdToDedupId[(int)dataRow[0]];
				if (!hashSet.Contains(num))
				{
					hashSet.Add(num);
					this.DedupTable.ImportRow(dataRow);
				}
			}
		}

		// Token: 0x0400000C RID: 12
		private Dictionary<DedupRecord, List<int>> identicalRecordGroups;

		// Token: 0x0400000F RID: 15
		private Dictionary<DedupRecord, int> valueToIdMap;

		// Token: 0x04000010 RID: 16
		private List<DedupRecord> values;

		// Token: 0x04000011 RID: 17
		private List<int> counts;

		// Token: 0x04000012 RID: 18
		private int maxValueId;

		// Token: 0x04000013 RID: 19
		internal List<GroupWithRepresentative> DedupIdToInputRows;

		// Token: 0x04000015 RID: 21
		private HashSet<int> representativeIdsInConstraints;

		// Token: 0x04000016 RID: 22
		private FastInt64HashSet incompatibleValues;

		// Token: 0x04000017 RID: 23
		private ILookup<int, int> idToIncompatibleIdsLookup;
	}
}

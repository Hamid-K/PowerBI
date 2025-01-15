using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Split.Text.Semantics
{
	// Token: 0x02001389 RID: 5001
	public class MatchRecord
	{
		// Token: 0x06009B4D RID: 39757 RVA: 0x0020C382 File Offset: 0x0020A582
		public MatchRecord(IReadOnlyList<int> startIndexes, IReadOnlyList<int> endIndexes)
		{
			this.StartIndexes = startIndexes;
			this.EndIndexes = endIndexes;
			if (startIndexes.Count != endIndexes.Count)
			{
				throw new Exception("Start and end index list should be of the same length.");
			}
			this._hashCodeComputed = false;
		}

		// Token: 0x06009B4E RID: 39758 RVA: 0x0020C3B8 File Offset: 0x0020A5B8
		public MatchRecord(IEnumerable<Record<int, int>> matchPairs, bool inputIsSorted = false)
		{
			IEnumerable<Record<int, int>> enumerable2;
			if (!inputIsSorted)
			{
				IEnumerable<Record<int, int>> enumerable = matchPairs.OrderBy((Record<int, int> p) => p.Item1);
				enumerable2 = enumerable;
			}
			else
			{
				enumerable2 = matchPairs;
			}
			ICollection<Record<int, int>> collection = matchPairs as ICollection<Record<int, int>>;
			List<int> list;
			List<int> list2;
			if (collection != null)
			{
				list = new List<int>(collection.Count);
				list2 = new List<int>(collection.Count);
			}
			else
			{
				list = new List<int>();
				list2 = new List<int>();
			}
			foreach (Record<int, int> record in enumerable2)
			{
				list.Add(record.Item1);
				list2.Add(record.Item2);
			}
			this.StartIndexes = list;
			this.EndIndexes = list2;
			this._hashCodeComputed = false;
		}

		// Token: 0x06009B4F RID: 39759 RVA: 0x0020C490 File Offset: 0x0020A690
		public MatchRecord(IEnumerable<Match> matchCollection)
		{
			matchCollection = matchCollection.Collect<Match>().Memoize<Match>();
			List<int> list = new List<int>(matchCollection.Count<Match>());
			List<int> list2 = new List<int>(matchCollection.Count<Match>());
			foreach (Match match in matchCollection)
			{
				list.Add(match.Index);
				list2.Add(match.Index + match.Length);
			}
			this.StartIndexes = list;
			this.EndIndexes = list2;
			this._hashCodeComputed = false;
		}

		// Token: 0x17001AA5 RID: 6821
		// (get) Token: 0x06009B50 RID: 39760 RVA: 0x0020C530 File Offset: 0x0020A730
		public IReadOnlyList<int> StartIndexes { get; }

		// Token: 0x17001AA6 RID: 6822
		// (get) Token: 0x06009B51 RID: 39761 RVA: 0x0020C538 File Offset: 0x0020A738
		public IReadOnlyList<int> EndIndexes { get; }

		// Token: 0x17001AA7 RID: 6823
		// (get) Token: 0x06009B52 RID: 39762 RVA: 0x0020C540 File Offset: 0x0020A740
		public int NumMatches
		{
			get
			{
				return this.StartIndexes.Count;
			}
		}

		// Token: 0x06009B53 RID: 39763 RVA: 0x0020C550 File Offset: 0x0020A750
		public static MatchRecord DisjointUnion(MatchRecord m1, MatchRecord m2, out int[] matchOrdering)
		{
			int num = 0;
			int num2 = 0;
			int num3 = m1.NumMatches + m2.NumMatches;
			int[] array = new int[num3];
			int[] array2 = new int[num3];
			matchOrdering = new int[num3];
			for (int i = 0; i < num3; i++)
			{
				if (num == m1.NumMatches)
				{
					array[i] = m2.StartIndexes[num2];
					array2[i] = m2.EndIndexes[num2];
					matchOrdering[i] = 1;
					num2++;
				}
				else if (num2 == m2.NumMatches)
				{
					array[i] = m1.StartIndexes[num];
					array2[i] = m1.EndIndexes[num];
					matchOrdering[i] = 0;
					num++;
				}
				else
				{
					int num4 = m1.StartIndexes[num];
					int num5 = m1.EndIndexes[num];
					int num6 = m2.StartIndexes[num2];
					int num7 = m2.EndIndexes[num2];
					if (num4 == num6)
					{
						if (num4 == num5 && num6 != num7)
						{
							array[i] = num4;
							array2[i] = num5;
							matchOrdering[i] = 0;
							num++;
						}
						else
						{
							if (num4 == num5 || num6 != num7)
							{
								return null;
							}
							array[i] = num6;
							array2[i] = num7;
							matchOrdering[i] = 1;
							num2++;
						}
					}
					else if (num4 < num6)
					{
						if (num6 < num5)
						{
							return null;
						}
						array[i] = num4;
						array2[i] = num5;
						matchOrdering[i] = 0;
						num++;
					}
					else
					{
						if (num4 < num7)
						{
							return null;
						}
						array[i] = num6;
						array2[i] = num7;
						matchOrdering[i] = 1;
						num2++;
					}
				}
			}
			return new MatchRecord(array, array2);
		}

		// Token: 0x06009B54 RID: 39764 RVA: 0x0020C6E8 File Offset: 0x0020A8E8
		public static MatchRecord[] DisjointUnion(MatchRecord[] m1, MatchRecord[] m2)
		{
			List<MatchRecord> list = new List<MatchRecord>();
			int[] array = null;
			for (int i = 0; i < m1.Length; i++)
			{
				int[] array2;
				MatchRecord matchRecord = MatchRecord.DisjointUnion(m1[i], m2[i], out array2);
				if (matchRecord == null)
				{
					return null;
				}
				if (array == null)
				{
					array = array2;
				}
				else if (!array2.SequenceEqual(array))
				{
					return null;
				}
				list.Add(matchRecord);
			}
			return list.ToArray();
		}

		// Token: 0x06009B55 RID: 39765 RVA: 0x0020C746 File Offset: 0x0020A946
		public bool Equals(MatchRecord p)
		{
			return p != null && this.StartIndexes.SequenceEqual(p.StartIndexes) && this.EndIndexes.SequenceEqual(p.EndIndexes);
		}

		// Token: 0x06009B56 RID: 39766 RVA: 0x0020C774 File Offset: 0x0020A974
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			MatchRecord matchRecord = obj as MatchRecord;
			return matchRecord != null && this.Equals(matchRecord);
		}

		// Token: 0x06009B57 RID: 39767 RVA: 0x0020C79C File Offset: 0x0020A99C
		public override int GetHashCode()
		{
			if (this._hashCodeComputed)
			{
				return this._hashCode;
			}
			int num = 17;
			foreach (int num2 in this.StartIndexes)
			{
				num = num * 23 + num2;
			}
			foreach (int num3 in this.EndIndexes)
			{
				num = num * 23 + num3;
			}
			this._hashCode = num;
			this._hashCodeComputed = true;
			return num;
		}

		// Token: 0x06009B58 RID: 39768 RVA: 0x0020C848 File Offset: 0x0020AA48
		public static bool operator ==(MatchRecord a, MatchRecord b)
		{
			return a == b || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x06009B59 RID: 39769 RVA: 0x0020C85F File Offset: 0x0020AA5F
		public static bool operator !=(MatchRecord a, MatchRecord b)
		{
			return !(a == b);
		}

		// Token: 0x04003E00 RID: 15872
		private int _hashCode;

		// Token: 0x04003E01 RID: 15873
		private bool _hashCodeComputed;
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Split.Text.Semantics;

namespace Microsoft.ProgramSynthesis.Split.Text.Learning
{
	// Token: 0x02001399 RID: 5017
	public static class MatchRecordLearningExtensions
	{
		// Token: 0x06009BD6 RID: 39894 RVA: 0x0020ECB4 File Offset: 0x0020CEB4
		public static int NumNonZLDs(this MatchRecord m, HashSet<int> relevantDelimiterIndexes)
		{
			return relevantDelimiterIndexes.Count((int i) => m.IsNonZeroLengthSplit(i));
		}

		// Token: 0x06009BD7 RID: 39895 RVA: 0x0020ECE0 File Offset: 0x0020CEE0
		public static int NumDisjointNonZLDs(this MatchRecord m, HashSet<int> relevantDelimiterIndexes)
		{
			return relevantDelimiterIndexes.Count((int i) => m.IsDisjointNonZeroLengthSplit(i));
		}

		// Token: 0x06009BD8 RID: 39896 RVA: 0x0020ED0C File Offset: 0x0020CF0C
		public static int NumZLDs(this MatchRecord m, HashSet<int> relevantDelimiterIndexes)
		{
			return relevantDelimiterIndexes.Count((int i) => m.IsZeroLengthSplit(i));
		}

		// Token: 0x06009BD9 RID: 39897 RVA: 0x0020ED38 File Offset: 0x0020CF38
		public static int NumFields(this MatchRecord m)
		{
			int num = m.StartIndexes.Where((int start, int i) => start == m.EndIndexes[i]).Count<int>();
			if (num % 2 != 0)
			{
				throw new ArgumentException("Match records representing field extractions must contain an even number of zero-length splits");
			}
			return num / 2;
		}

		// Token: 0x06009BDA RID: 39898 RVA: 0x0020ED88 File Offset: 0x0020CF88
		public static int TotalFieldsSize(this MatchRecord m)
		{
			int[] array = m.StartIndexes.Where((int start, int i) => start == m.EndIndexes[i]).ToArray<int>();
			if (array.Length % 2 != 0)
			{
				throw new ArgumentException("Match records representing field extractions must contain an even number of zero-length splits");
			}
			int num = 0;
			for (int j = 1; j < array.Length; j += 2)
			{
				num += array[j] - array[j - 1];
			}
			return num;
		}

		// Token: 0x06009BDB RID: 39899 RVA: 0x0020EDF3 File Offset: 0x0020CFF3
		public static bool IsNonZeroLengthSplit(this MatchRecord m, int i)
		{
			return m.StartIndexes[i] != m.EndIndexes[i];
		}

		// Token: 0x06009BDC RID: 39900 RVA: 0x0020EE12 File Offset: 0x0020D012
		public static bool IsZeroLengthSplit(this MatchRecord m, int i)
		{
			return m.StartIndexes[i] == m.EndIndexes[i];
		}

		// Token: 0x06009BDD RID: 39901 RVA: 0x0020EE30 File Offset: 0x0020D030
		public static bool IsDisjointNonZeroLengthSplit(this MatchRecord m, int i)
		{
			int num = m.StartIndexes[i];
			return num != m.EndIndexes[i] && (i <= 0 || num != m.EndIndexes[i - 1] || m.StartIndexes[i - 1] == m.EndIndexes[i - 1]);
		}

		// Token: 0x06009BDE RID: 39902 RVA: 0x0020EE94 File Offset: 0x0020D094
		public static bool IsTrivialZeroLengthSplit(this MatchRecord m, int i, string s, HashSet<int> ignoreIndexes)
		{
			int num = m.StartIndexes[i];
			return num == m.EndIndexes[i] && (num == 0 || num == s.Length || (i > 0 && num == m.EndIndexes[i - 1] && !ignoreIndexes.Contains(i - 1)) || (i < m.NumMatches - 1 && num == m.StartIndexes[i + 1] && !ignoreIndexes.Contains(i + 1) && m.StartIndexes[i + 1] != m.EndIndexes[i + 1]));
		}
	}
}

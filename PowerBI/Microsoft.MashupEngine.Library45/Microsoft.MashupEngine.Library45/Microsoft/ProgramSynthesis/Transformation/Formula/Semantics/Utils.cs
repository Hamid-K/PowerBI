using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics
{
	// Token: 0x020015FE RID: 5630
	public static class Utils
	{
		// Token: 0x0600BB37 RID: 47927 RVA: 0x00284AAC File Offset: 0x00282CAC
		public static IEnumerable<T> Empty<T>()
		{
			yield break;
		}

		// Token: 0x0600BB38 RID: 47928 RVA: 0x00284AB8 File Offset: 0x00282CB8
		public static string ExpandWhitespace(string subject, string substring, int startIndex)
		{
			if (subject == substring)
			{
				return subject;
			}
			int num = startIndex;
			while (num > 0 && subject[num - 1].IsWhitespace())
			{
				num--;
			}
			int num2 = startIndex + substring.Length;
			int num3 = num2;
			while (num3 < subject.Length && subject[num3].IsWhitespace())
			{
				num3++;
			}
			if (num != startIndex || num3 != num2)
			{
				return subject.Slice(new int?(num), new int?(num3), 1);
			}
			return substring;
		}

		// Token: 0x0600BB39 RID: 47929 RVA: 0x00284B31 File Offset: 0x00282D31
		public static IEnumerable<int> IndexRange<T>(ICollection<T> subject)
		{
			return Utils.Range(0, subject.Count - 1);
		}

		// Token: 0x0600BB3A RID: 47930 RVA: 0x00284B41 File Offset: 0x00282D41
		public static IEnumerable<int> IndexRange<T>(T[] subject)
		{
			return Utils.Range(0, subject.Length - 1);
		}

		// Token: 0x0600BB3B RID: 47931 RVA: 0x00284B4E File Offset: 0x00282D4E
		public static IEnumerable<int> IndexRange(string subject)
		{
			return Utils.Range(0, subject.Length - 1);
		}

		// Token: 0x0600BB3C RID: 47932 RVA: 0x00284B5E File Offset: 0x00282D5E
		public static IEnumerable<int> IndexRange<T>(ReadOnlySpan<T> subject)
		{
			return Utils.Range(0, subject.Length - 1);
		}

		// Token: 0x0600BB3D RID: 47933 RVA: 0x00284B6F File Offset: 0x00282D6F
		public static IEnumerable<int> Range(int start, int end)
		{
			int num;
			for (int i = start; i <= end; i = num + 1)
			{
				yield return i;
				num = i;
			}
			yield break;
		}
	}
}

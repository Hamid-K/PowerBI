using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Autocompletion
{
	// Token: 0x02001E09 RID: 7689
	internal class EntropyBasedRanker : IRanker
	{
		// Token: 0x0601014A RID: 65866 RVA: 0x00373AC4 File Offset: 0x00371CC4
		public IList<CompletionResultWithIndex> Rank(IEnumerable<CompletionResultWithIndex> completionResults)
		{
			IEnumerable<CompletionResultWithIndex> enumerable = completionResults.ToList<CompletionResultWithIndex>();
			List<CompletionResultWithIndex> list = new List<CompletionResultWithIndex>();
			using (IEnumerator<IGrouping<int, CompletionResultWithIndex>> enumerator = (from c in enumerable
				group c by c.Index into g
				orderby g.Key
				select g).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IEnumerable<IGrouping<string, CompletionResultWithIndex>> enumerable2 = from c in enumerator.Current
						group c by c.Value.Value;
					list.AddRange(enumerable2.OrderByDescending(new Func<IGrouping<string, CompletionResultWithIndex>, double>(this.CalculateScore)).SelectMany((IGrouping<string, CompletionResultWithIndex> g) => g));
				}
			}
			return list;
		}

		// Token: 0x0601014B RID: 65867 RVA: 0x00373BBC File Offset: 0x00371DBC
		private double CalculateNormalizedEntropy(string s)
		{
			int len = s.Length;
			return (from c in s
				group c by c).Select(delegate(IGrouping<char, char> g)
			{
				double num = (double)g.Count<char>() / (double)len;
				return -(Math.Log(num) / Math.Log(2.0) * num);
			}).Sum() / (double)s.Length;
		}

		// Token: 0x0601014C RID: 65868 RVA: 0x00373C20 File Offset: 0x00371E20
		private double CalculateScore(IGrouping<string, CompletionResultWithIndex> completionGroup)
		{
			double num = this.CalculateNormalizedEntropy(completionGroup.Key);
			double num2 = Math.Pow(2.718281828459045, (double)(-(double)Math.Abs(8 - completionGroup.Key.Length)));
			double num3 = completionGroup.Max((CompletionResultWithIndex c) => c.Value.EntityScoreMultiplier * c.Value.RelativeScore);
			return (num + num2) * num3;
		}

		// Token: 0x040060DE RID: 24798
		private const int OptimalLength = 8;
	}
}

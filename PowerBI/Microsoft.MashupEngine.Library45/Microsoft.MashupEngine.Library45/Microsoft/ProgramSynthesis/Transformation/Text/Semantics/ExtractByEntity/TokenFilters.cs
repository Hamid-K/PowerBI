using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Semantics.ExtractByEntity
{
	// Token: 0x02001D77 RID: 7543
	internal static class TokenFilters
	{
		// Token: 0x0600FDAB RID: 64939 RVA: 0x00363196 File Offset: 0x00361396
		private static IEnumerable<EntityToken> GetUndominatedTokens(IReadOnlyList<EntityToken> tokens)
		{
			List<int>[] incomingPrecedenceEdges = (from _ in Enumerable.Range(0, tokens.Count)
				select new List<int>()).ToArray<List<int>>();
			List<int>[] outgoingPrecedenceEdges = (from _ in Enumerable.Range(0, tokens.Count)
				select new List<int>()).ToArray<List<int>>();
			PrecedenceBasedTokenComparer instance = PrecedenceBasedTokenComparer.Instance;
			for (int i = 0; i < tokens.Count; i++)
			{
				for (int j = i + 1; j < tokens.Count; j++)
				{
					int num = instance.Compare(tokens[i], tokens[j]);
					if (num > 0)
					{
						outgoingPrecedenceEdges[i].Add(j);
						incomingPrecedenceEdges[j].Add(i);
					}
					else if (num < 0)
					{
						outgoingPrecedenceEdges[j].Add(i);
						incomingPrecedenceEdges[i].Add(j);
					}
				}
			}
			HashSet<int> indicesToConsider = new HashSet<int>(Enumerable.Range(0, tokens.Count));
			Func<int, bool> <>9__2;
			while (indicesToConsider.Any<int>())
			{
				IEnumerable<int> enumerable = indicesToConsider;
				Func<int, bool> func;
				if ((func = <>9__2) == null)
				{
					func = (<>9__2 = (int idx) => incomingPrecedenceEdges[idx].Count == 0);
				}
				List<int> list = enumerable.Where(func).ToList<int>();
				if (list.Count == 0)
				{
					yield break;
				}
				foreach (int idx2 in list)
				{
					yield return tokens[idx2];
					indicesToConsider.Remove(idx2);
					List<KeyValuePair<int, int>> list2 = new List<KeyValuePair<int, int>>();
					List<KeyValuePair<int, int>> list3 = new List<KeyValuePair<int, int>>();
					foreach (int num2 in outgoingPrecedenceEdges[idx2])
					{
						indicesToConsider.Remove(num2);
						foreach (int num3 in outgoingPrecedenceEdges[num2])
						{
							list2.Add(new KeyValuePair<int, int>(num3, num2));
						}
						foreach (int num4 in incomingPrecedenceEdges[num2])
						{
							list3.Add(new KeyValuePair<int, int>(num4, num2));
						}
					}
					foreach (KeyValuePair<int, int> keyValuePair in list2)
					{
						incomingPrecedenceEdges[keyValuePair.Key].Remove(keyValuePair.Value);
					}
					foreach (KeyValuePair<int, int> keyValuePair2 in list3)
					{
						outgoingPrecedenceEdges[keyValuePair2.Key].Remove(keyValuePair2.Value);
					}
				}
				List<int>.Enumerator enumerator = default(List<int>.Enumerator);
			}
			yield break;
			yield break;
		}

		// Token: 0x0600FDAC RID: 64940 RVA: 0x003631A6 File Offset: 0x003613A6
		internal static IEnumerable<EntityToken> ResolveSubsumptionByPrecedence(IEnumerable<EntityToken> tokens)
		{
			List<EntityToken> sortedTokens = (from t in tokens
				orderby t.Start, t.End descending
				select t).ToList<EntityToken>();
			int idx = 0;
			while (idx < sortedTokens.Count)
			{
				List<EntityToken> list = new List<EntityToken>();
				List<EntityToken> list2 = sortedTokens;
				int num = idx;
				idx = num + 1;
				EntityToken entityToken = list2[num];
				list.Add(entityToken);
				int end = entityToken.End;
				while (idx < sortedTokens.Count && sortedTokens[idx].End <= end)
				{
					List<EntityToken> list3 = list;
					List<EntityToken> list4 = sortedTokens;
					num = idx;
					idx = num + 1;
					list3.Add(list4[num]);
				}
				list = (from t in list
					group t by t.GetType() into g
					select g.Distinct((EntityToken t) => new KeyValuePair<int, int>(t.Start, t.End))).SelectMany((IEnumerable<EntityToken> g) => g).ToList<EntityToken>();
				IEnumerable<EntityToken> undominatedTokens = TokenFilters.GetUndominatedTokens(list);
				foreach (EntityToken entityToken2 in undominatedTokens)
				{
					yield return entityToken2;
				}
				IEnumerator<EntityToken> enumerator = null;
			}
			yield break;
			yield break;
		}
	}
}

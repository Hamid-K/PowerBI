using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Features;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.VersionSpace
{
	// Token: 0x020002B6 RID: 694
	public static class ProgramSetUtils
	{
		// Token: 0x06000F21 RID: 3873 RVA: 0x0002BE50 File Offset: 0x0002A050
		public static ProgramSet NormalizedUnion(this IEnumerable<ProgramSet> collection)
		{
			if (collection == null)
			{
				return null;
			}
			ProgramSet[] array = (collection as ProgramSet[]) ?? collection.ToArray<ProgramSet>();
			if (array.Length == 0)
			{
				return null;
			}
			List<ProgramSet> list = new List<ProgramSet>(array.Length);
			Symbol symbol = null;
			foreach (ProgramSet programSet in array)
			{
				if (!ProgramSet.IsNullOrEmpty(programSet))
				{
					list.Add(programSet);
				}
				else if (symbol == null && programSet != null)
				{
					symbol = programSet.Symbol;
				}
			}
			if (list.Count == 0)
			{
				if (symbol != null)
				{
					return ProgramSet.Empty(symbol);
				}
				return null;
			}
			else
			{
				if (list.Count == 1)
				{
					return list[0];
				}
				symbol = symbol ?? list[0].Symbol;
				List<ProgramSet> list2 = list;
				List<ProgramSet> list3 = new List<ProgramSet>();
				bool flag;
				do
				{
					flag = false;
					foreach (ProgramSet programSet2 in list2)
					{
						UnionProgramSet unionProgramSet = programSet2 as UnionProgramSet;
						if (unionProgramSet == null)
						{
							list3.Add(programSet2);
						}
						else
						{
							list3.AddRange(unionProgramSet.UnionSpaces);
							flag = true;
						}
					}
					list2 = list3;
					list3 = new List<ProgramSet>();
				}
				while (flag);
				BigInteger bigInteger = list2.Aggregate(BigInteger.One, (BigInteger x, ProgramSet y) => x + y.Size);
				UnionProgramSet unionProgramSet2 = new UnionProgramSet(symbol, list2.ToArray());
				if (!(bigInteger >= 20L))
				{
					return ProgramSet.List(symbol, unionProgramSet2.RealizedPrograms.ToArray<ProgramNode>());
				}
				return unionProgramSet2;
			}
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x0002BFE0 File Offset: 0x0002A1E0
		public static ProgramSet Normalize(this ProgramSet set, LearningTask task)
		{
			if (set != null)
			{
				return set;
			}
			if (!task.RequiresPruning)
			{
				return ProgramSet.Empty(task.Symbol);
			}
			return PrunedProgramSet.Empty(task.Symbol);
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x0002C008 File Offset: 0x0002A208
		public static IEnumerable<T> TopK<T, TKey>(this IEnumerable<T> seq, Func<T, TKey> keySelector, int k, IComparer<TKey> comparer = null, List<T> prunedNodes = null)
		{
			comparer = comparer ?? Comparer<TKey>.Default;
			MultiValueDictionary<TKey, T> multiValueDictionary = seq.GroupBy(keySelector).ToMultiValueDictionary(null);
			SortedSet<TKey> sortedSet = new SortedSet<TKey>(comparer);
			int num = 0;
			foreach (KeyValuePair<TKey, IReadOnlyCollection<T>> keyValuePair in multiValueDictionary)
			{
				if (num++ < Math.Min(k, multiValueDictionary.Count))
				{
					sortedSet.Add(keyValuePair.Key);
				}
				else
				{
					TKey min = sortedSet.Min;
					if (comparer.Compare(keyValuePair.Key, min) <= 0)
					{
						if (prunedNodes != null)
						{
							prunedNodes.AddRange(multiValueDictionary[keyValuePair.Key]);
						}
					}
					else
					{
						sortedSet.Remove(min);
						sortedSet.Add(keyValuePair.Key);
						if (prunedNodes != null)
						{
							prunedNodes.AddRange(multiValueDictionary[min]);
						}
					}
				}
			}
			List<IReadOnlyCollection<T>> list = new List<IReadOnlyCollection<T>>(sortedSet.Count);
			foreach (TKey tkey in sortedSet)
			{
				list.Add(multiValueDictionary[tkey]);
			}
			list.Reverse();
			return list.SelectMany((IReadOnlyCollection<T> l) => l);
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x0002C178 File Offset: 0x0002A378
		public static ProgramSet AddConversionRules(this ProgramSet body, Symbol symbol)
		{
			return body.Symbol.ConversionRulesTo(symbol).Aggregate(body, (ProgramSet current, ConversionRule conversionRule) => new JoinProgramSet(conversionRule, new ProgramSet[] { current }));
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x0002C1AB File Offset: 0x0002A3AB
		public static JoinProgramSet AddConversionRules(this JoinProgramSet body, Symbol symbol)
		{
			return body.Symbol.ConversionRulesTo(symbol).Aggregate(body, (JoinProgramSet current, ConversionRule conversionRule) => new JoinProgramSet(conversionRule, new ProgramSet[] { current }));
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x0002C1E0 File Offset: 0x0002A3E0
		internal static IEnumerable<Func<ProgramNode, XAttribute>> GetAttributeCalculators(this IEnumerable<IFeature> featureCalculators, Dictionary<object, int> identityCache)
		{
			return featureCalculators.Select((IFeature f) => delegate(ProgramNode p)
			{
				object featureValue;
				try
				{
					featureValue = p.GetFeatureValue(f, null);
				}
				catch (FeatureUndefinedException ex)
				{
					if (f.Info.IsComplete || ex.Feature != f.Info)
					{
						throw;
					}
					return null;
				}
				if (featureValue != null)
				{
					return new XAttribute(f.Info.Name, featureValue.ToLiteral(identityCache));
				}
				return null;
			});
		}

		// Token: 0x020002B7 RID: 695
		public class ShatteringContext
		{
			// Token: 0x17000353 RID: 851
			// (get) Token: 0x06000F27 RID: 3879 RVA: 0x0002C20C File Offset: 0x0002A40C
			private int Buckets { get; }

			// Token: 0x17000354 RID: 852
			// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0002C214 File Offset: 0x0002A414
			private ulong[] HashCoefficients { get; }

			// Token: 0x17000355 RID: 853
			// (get) Token: 0x06000F29 RID: 3881 RVA: 0x0002C21C File Offset: 0x0002A41C
			private Dictionary<ProgramSet, Dictionary<int, ProgramSet>> Cache { get; }

			// Token: 0x06000F2A RID: 3882 RVA: 0x0002C224 File Offset: 0x0002A424
			public ShatteringContext(int buckets, Random random)
			{
				this.Buckets = buckets;
				this.HashCoefficients = new ulong[16];
				for (int i = 0; i < 16; i++)
				{
					this.HashCoefficients[i] = (ulong)((long)random.Next(int.MaxValue) + 1L);
				}
				this.Cache = new Dictionary<ProgramSet, Dictionary<int, ProgramSet>>();
			}

			// Token: 0x06000F2B RID: 3883 RVA: 0x0002C27C File Offset: 0x0002A47C
			public Dictionary<int, ProgramSet> ShatterChild(ProgramSet child)
			{
				Dictionary<int, ProgramSet> dictionary;
				if (this.Cache.TryGetValue(child, out dictionary))
				{
					return dictionary;
				}
				dictionary = child.Shatter(this);
				this.Cache[child] = dictionary;
				return dictionary;
			}

			// Token: 0x06000F2C RID: 3884 RVA: 0x0002C2B1 File Offset: 0x0002A4B1
			public int HashProgram(ProgramNode node)
			{
				if (node.Children.Length == 0)
				{
					return node.GetHashCode();
				}
				return this.UHash(node.GrammarRule.GetHashCode(), node.Children.Select((ProgramNode child) => this.HashProgram(child)));
			}

			// Token: 0x06000F2D RID: 3885 RVA: 0x0002C2EC File Offset: 0x0002A4EC
			public int UHash(int headHash, IEnumerable<int> tailHashes)
			{
				ulong num = (ulong)((long)headHash * (long)this.HashCoefficients[0]);
				int num2 = 1;
				foreach (int num3 in tailHashes)
				{
					num = (num + (ulong)((long)num3 * (long)this.HashCoefficients[num2++])) % 2147483647UL;
				}
				return (int)(num % (ulong)((long)this.Buckets));
			}

			// Token: 0x0400075A RID: 1882
			private const int NumCoefficients = 16;
		}
	}
}

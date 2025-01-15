using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.StdWitnessingPlans
{
	// Token: 0x0200072D RID: 1837
	internal class FilterIntWitnessingPlan : StdWitnessingPlan<FilterInt, SubsequenceSpec>
	{
		// Token: 0x06002798 RID: 10136 RVA: 0x0000E945 File Offset: 0x0000CB45
		[WitnessFunction(1)]
		internal static SubsequenceSpec WitnessList(FilterInt rule, SubsequenceSpec spec)
		{
			return spec;
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x000700B0 File Offset: 0x0006E2B0
		[WitnessFunction(0, DependsOnParameters = new int[] { 1 })]
		internal static DisjunctiveExamplesSpec WitnessSlice(FilterInt rule, SubsequenceSpec spec, ExampleSpec listValues)
		{
			List<Record<int, int>> initIterPairs = FilterIntWitnessingPlan.FindInitIterPairs(spec, listValues, false);
			if (initIterPairs.Count == 0)
			{
				return null;
			}
			return DisjunctiveExamplesSpec.From(spec.ProvidedInputs.ToDictionary((State s) => s, (State s) => initIterPairs.Cast<object>()));
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x0007011C File Offset: 0x0006E31C
		internal static List<Record<int, int>> FindInitIterPairs(SubsequenceSpec spec, ExampleSpec listValues, bool isPrefix)
		{
			HashSet<int> startIndices = new HashSet<int>();
			int? num = null;
			int num2 = int.MaxValue;
			foreach (KeyValuePair<State, object> keyValuePair in listValues.Examples)
			{
				State key = keyValuePair.Key;
				IEnumerable<object> enumerable = keyValuePair.Value.ToEnumerable<object>();
				IEnumerable<object> enumerable2 = spec.PositiveExamples[key];
				int? num3 = null;
				IEnumerator<object> enumerator2 = enumerable2.GetEnumerator();
				IEnumerator<object> enumerator3 = enumerable.GetEnumerator();
				int num4 = -1;
				while (enumerator2.MoveNext())
				{
					bool flag = false;
					int num5 = num4;
					while (enumerator3.MoveNext())
					{
						num4++;
						if (ValueEquality.Comparer.Equals(enumerator2.Current, enumerator3.Current))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return new List<Record<int, int>>();
					}
					if (num3 == null)
					{
						num3 = new int?(num4);
					}
					else if (num == null)
					{
						num = new int?(num4 - num5);
					}
					else if (!isPrefix)
					{
						num = new int?(MathUtils.GCD(num.Value, num4 - num5));
					}
					else if (num.Value != num4 - num5)
					{
						return new List<Record<int, int>>();
					}
				}
				if (num3 != null)
				{
					startIndices.Add(num3.Value);
					if (num == null)
					{
						while (enumerator3.MoveNext())
						{
							num4++;
						}
						int num6 = (isPrefix ? (num4 - num3.Value) : Math.Max(num3.Value, num4 - num3.Value));
						if (num6 == 0)
						{
							num6 = 1;
						}
						num2 = Math.Min(num2, num6);
					}
				}
			}
			if (startIndices.Count == 0)
			{
				return new List<Record<int, int>>();
			}
			IEnumerable<int> enumerable3 = ((num == null) ? Enumerable.Range(1, num2) : (isPrefix ? Seq.Of<int>(new int[] { num.Value }) : num.Value.GetDivisors()));
			if (!isPrefix)
			{
				int maxInit = startIndices.Min();
				List<Record<int, int>> list = new List<Record<int, int>>();
				using (IEnumerator<int> enumerator4 = enumerable3.GetEnumerator())
				{
					while (enumerator4.MoveNext())
					{
						int iter2 = enumerator4.Current;
						if (!startIndices.Any((int startIndex) => (startIndex - maxInit) % iter2 != 0))
						{
							for (int i = maxInit; i >= 0; i -= iter2)
							{
								list.Add(Record.Create<int, int>(i, iter2));
							}
						}
					}
				}
				return list;
			}
			if (startIndices.Count > 1)
			{
				return new List<Record<int, int>>();
			}
			return enumerable3.Select((int iter) => Record.Create<int, int>(startIndices.ElementAt(0), iter)).ToList<Record<int, int>>();
		}
	}
}

using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.StdWitnessingPlans
{
	// Token: 0x02000735 RID: 1845
	internal class FilterWitnessingPlanPrefix : StdWitnessingPlan<Filter, PrefixSpec>
	{
		// Token: 0x060027AE RID: 10158 RVA: 0x00070017 File Offset: 0x0006E217
		[WitnessFunction(1)]
		internal static SubsequenceSpec WitnessSet(Filter rule, PrefixSpec spec)
		{
			return new SubsequenceSpec(spec.PositiveExamples);
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x00070A28 File Offset: 0x0006EC28
		[WitnessFunction(0, DependsOnParameters = new int[] { 1 })]
		internal static ExampleSpec WitnessPredicate(Filter rule, PrefixSpec spec, ExampleSpec supersetValues)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in supersetValues.Examples)
			{
				State key = keyValuePair.Key;
				using (IEnumerator<object> enumerator2 = keyValuePair.Value.ToEnumerable<object>().GetEnumerator())
				{
					foreach (object obj in spec.PositiveExamples[key])
					{
						bool flag = false;
						List<object> list = new List<object>();
						while (!flag && enumerator2.MoveNext())
						{
							if (ValueEquality.Comparer.Equals(enumerator2.Current, obj))
							{
								flag = true;
							}
							else
							{
								list.Add(enumerator2.Current);
							}
						}
						if (!flag)
						{
							return null;
						}
						State state = key.WithFunctionalInput(obj, false);
						object obj2;
						if (dictionary.TryGetValue(state, out obj2) && !true.Equals(obj2))
						{
							return null;
						}
						dictionary[state] = true;
						foreach (object obj3 in list)
						{
							State state2 = key.WithFunctionalInput(obj3, false);
							if (dictionary.TryGetValue(state2, out obj2) && !false.Equals(obj2))
							{
								return null;
							}
							dictionary[state2] = false;
						}
					}
					HashSet<object> hashSet = spec.NegativeExamples[key].ConvertToHashSet<object>();
					while (enumerator2.MoveNext())
					{
						object obj4 = enumerator2.Current;
						if (hashSet.Contains(obj4))
						{
							State state3 = key.WithFunctionalInput(obj4, false);
							object obj5;
							if (dictionary.TryGetValue(state3, out obj5) && !false.Equals(obj5))
							{
								return null;
							}
							dictionary[state3] = false;
						}
					}
				}
			}
			return new ExampleSpec(dictionary);
		}
	}
}

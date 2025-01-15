using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive
{
	// Token: 0x02000725 RID: 1829
	public static class WitnessUtils
	{
		// Token: 0x06002784 RID: 10116 RVA: 0x0006FDCC File Offset: 0x0006DFCC
		public static DisjunctiveExamplesSpec WitnessGenericProperty<TObjectType, TPropertyType>(BlackBoxRule rule, int baseParamIndex, BooleanExampleSpec outerSpec, Func<TObjectType, TPropertyType> getValFunc) where TObjectType : class
		{
			return WitnessUtils.WitnessGenericProperty<TObjectType, TPropertyType>(rule, baseParamIndex, outerSpec, (TObjectType n) => new TPropertyType[] { getValFunc(n) });
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x0006FDFC File Offset: 0x0006DFFC
		public static DisjunctiveExamplesSpec WitnessGenericProperty<TObjectType, TPropertyType>(BlackBoxRule rule, int baseParamIndex, BooleanExampleSpec outerSpec, Func<TObjectType, IEnumerable<TPropertyType>> getValFunc) where TObjectType : class
		{
			Symbol symbol = rule.Body[baseParamIndex];
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			bool flag = outerSpec is BooleanHardNegativeSpec;
			HashSet<TPropertyType> hashSet = null;
			HashSet<TPropertyType> hashSet2 = new HashSet<TPropertyType>();
			ReadOnlyDictionary<State, bool> selection = outerSpec.Selection;
			foreach (State state in outerSpec.ProvidedInputs)
			{
				TObjectType tobjectType = state[symbol] as TObjectType;
				if (tobjectType == null)
				{
					return null;
				}
				if (selection[state])
				{
					if (hashSet == null)
					{
						hashSet = new HashSet<TPropertyType>(getValFunc(tobjectType));
					}
					else if (flag)
					{
						hashSet.UnionWith(getValFunc(tobjectType));
					}
					else
					{
						hashSet.IntersectWith(getValFunc(tobjectType));
					}
				}
				else
				{
					hashSet2.UnionWith(getValFunc(tobjectType));
				}
			}
			if (hashSet == null)
			{
				return null;
			}
			if (flag)
			{
				hashSet.ExceptWith(hashSet2);
			}
			if (hashSet.Count == 0)
			{
				return null;
			}
			foreach (State state2 in outerSpec.ProvidedInputs)
			{
				dictionary[state2] = hashSet.Cast<object>();
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}
	}
}

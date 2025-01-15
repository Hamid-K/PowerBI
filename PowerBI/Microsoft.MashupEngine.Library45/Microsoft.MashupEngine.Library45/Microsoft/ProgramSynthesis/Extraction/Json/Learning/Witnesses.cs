using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Learning
{
	// Token: 0x02000B86 RID: 2950
	public class Witnesses : DomainLearningLogic
	{
		// Token: 0x06004AED RID: 19181 RVA: 0x000EB5A8 File Offset: 0x000E97A8
		public Witnesses(Grammar grammar)
			: base(grammar)
		{
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x000EB5B4 File Offset: 0x000E97B4
		[WitnessFunction("SelectSequence", 1)]
		internal DisjunctiveExamplesSpec WitnessPathInSelectSequence(GrammarRule rule, SubsequenceSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.PositiveExamples)
			{
				IEnumerable<JsonRegion> enumerable = keyValuePair.Value.Cast<JsonRegion>();
				State key = keyValuePair.Key;
				JsonRegion jsonRegion = (JsonRegion)key[rule.Body[0]];
				HashSet<JPath> hashSet = null;
				foreach (JsonRegion jsonRegion2 in enumerable)
				{
					if (hashSet == null)
					{
						hashSet = Witnesses.LearnAllGeneralizedPaths(jsonRegion, jsonRegion2, true).ConvertToHashSet<JPath>();
					}
					else
					{
						hashSet.IntersectWith(Witnesses.LearnAllGeneralizedPaths(jsonRegion, jsonRegion2, true));
					}
				}
				if (hashSet == null)
				{
					return null;
				}
				dictionary[key] = hashSet;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x06004AEF RID: 19183 RVA: 0x000EB6B0 File Offset: 0x000E98B0
		[WitnessFunction("SelectRegion", 1)]
		internal DisjunctiveExamplesSpec WitnessPathInSelectRegion(GrammarRule rule, ExampleSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in spec.Examples)
			{
				JsonRegion jsonRegion = keyValuePair.Value as JsonRegion;
				if (jsonRegion == null)
				{
					return null;
				}
				State key = keyValuePair.Key;
				JsonRegion jsonRegion2 = (JsonRegion)key[rule.Body[0]];
				dictionary[key] = Witnesses.LearnAllGeneralizedPaths(jsonRegion2, jsonRegion, false);
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x000EB754 File Offset: 0x000E9954
		private static IEnumerable<JPath> LearnAllGeneralizedPaths(JsonRegion src, JsonRegion dst, bool learnSequence)
		{
			List<List<JPathStep>> list = new List<List<JPathStep>>
			{
				new List<JPathStep>()
			};
			JToken jtoken = dst.Token;
			JToken jtoken2 = dst.Token.Parent;
			while (jtoken2 != null && jtoken != src.Token)
			{
				if (!(jtoken2 is JProperty))
				{
					JArray jarray = jtoken2 as JArray;
					if (jarray == null)
					{
						if (!(jtoken2 is JObject))
						{
							goto IL_00FC;
						}
					}
					else
					{
						int num = jarray.IndexOf(jtoken);
						if (num == -1)
						{
							return null;
						}
						using (List<List<JPathStep>>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								List<JPathStep> list2 = enumerator.Current;
								if (learnSequence)
								{
									list2.Insert(0, new StarStep());
								}
								else
								{
									list2.Insert(0, new IndexStep(num));
								}
							}
							goto IL_00FC;
						}
					}
					JProperty jproperty = jtoken as JProperty;
					if (jproperty == null || jproperty.Parent != jtoken2)
					{
						return null;
					}
					foreach (List<JPathStep> list3 in list)
					{
						list3.Insert(0, new AccessStep(jproperty.Name));
					}
				}
				IL_00FC:
				jtoken = jtoken2;
				jtoken2 = jtoken2.Parent;
			}
			return list.Select((List<JPathStep> steps) => new JPath(steps)).ToArray<JPath>();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.ProgramSynthesis.Learning;
using Microsoft.ProgramSynthesis.Learning.Strategies.Deductive.RuleLearners;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Specifications;
using Microsoft.ProgramSynthesis.Transformation.Json.Build;
using Microsoft.ProgramSynthesis.Transformation.Json.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Json.Semantics;
using Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet;
using Microsoft.ProgramSynthesis.Transformation.Text;
using Microsoft.ProgramSynthesis.Transformation.Text.Description;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Json.Learning
{
	// Token: 0x02001A51 RID: 6737
	public class Witnesses : DomainLearningLogic
	{
		// Token: 0x0600DE22 RID: 56866 RVA: 0x002F28A8 File Offset: 0x002F0AA8
		public Witnesses(Grammar grammar)
			: base(grammar)
		{
			this._build = GrammarBuilders.Instance(grammar);
			Witnesses.Options options = new Witnesses.Options
			{
				AllowedTransformations = (TransformationKind.Constant | TransformationKind.Concat | TransformationKind.Substring | TransformationKind.WholeColumn | TransformationKind.CaseTransformation),
				ConcatLocation = ConcatLocation.AtTokenBoundaries,
				ForbidConstantProgram = true
			};
			this._externWitnessFunction = new Witnesses(Language.Grammar, new RankingScore(Language.Grammar, new RankingScoreModel(), false, null), null, options);
		}

		// Token: 0x0600DE23 RID: 56867 RVA: 0x002F2914 File Offset: 0x002F0B14
		[WitnessFunction("ConstValue", 0)]
		internal DisjunctiveExamplesSpec WitnessStrInConstValue(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue[] array = keyValuePair.Value.OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue>().ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue>();
				if (array.Length == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = array.Select((Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue v) => v.ToString(CultureInfo.InvariantCulture)).ToArray<string>();
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE24 RID: 56868 RVA: 0x002F29BC File Offset: 0x002F0BBC
		[WitnessFunction("Object", 0)]
		internal DisjunctiveExamplesSpec WitnessKeyValueInObject(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject[] array = (from obj in keyValuePair.Value.OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>()
					where obj.Count == 1
					select obj).ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>();
				if (array.Length == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = array.Select((Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject obj) => obj.First).ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>();
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE25 RID: 56869 RVA: 0x002F2A8C File Offset: 0x002F0C8C
		[WitnessFunction("Append", 0)]
		internal DisjunctiveExamplesSpec WitnessKeyValueInAppend(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject[] array = (from obj in keyValuePair.Value.OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>()
					where obj.Count > 0
					select obj).ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>();
				if (array.Length == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = array.Select((Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject obj) => obj.First).ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>();
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE26 RID: 56870 RVA: 0x002F2B5C File Offset: 0x002F0D5C
		[WitnessFunction("Append", 1)]
		internal DisjunctiveExamplesSpec WitnessObjectInAppend(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject[] array = (from obj in keyValuePair.Value.OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>()
					where obj.Count > 0
					select obj).ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>();
				if (array.Length == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = array.Select((Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject obj) => new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject(obj.First.AfterSelf())).ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>();
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE27 RID: 56871 RVA: 0x002F2C2C File Offset: 0x002F0E2C
		[WitnessFunction("FlattenObject", 1, DependsOnParameters = new int[] { 0 })]
		internal DisjunctiveExamplesSpec WitnessPathInFlattenObject(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec xSpec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in xSpec.Examples)
			{
				State key = keyValuePair.Key;
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken jtoken = keyValuePair.Value as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken;
				if (jtoken == null)
				{
					return null;
				}
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject[] array = (from <>h__TransparentIdentifier0 in (from example in spec.DisjunctiveExamples[key].OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>()
						select new
						{
							example = example,
							children = example.Children().OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>().ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>()
						}).Where(delegate(<>h__TransparentIdentifier0)
					{
						if (<>h__TransparentIdentifier0.children.Any((Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty c) => c.Name.Contains(".") || c.Name.Contains("[")))
						{
							return <>h__TransparentIdentifier0.children.All((Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty c) => c.Value is Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue);
						}
						return false;
					})
					select <>h__TransparentIdentifier0.example).ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>();
				if (array.Length == 0)
				{
					return null;
				}
				List<JPath> list = new List<JPath>();
				foreach (Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject jobject in jtoken.DescendantsAndSelf().OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>())
				{
					string[] array2 = (from p in jobject.Children().OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>()
						select p.Name).ToArray<string>();
					Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject[] array3 = array;
					for (int i = 0; i < array3.Length; i++)
					{
						Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject jobject2 = array3[i];
						string[] flattenKeys = (from e in jobject2.Children().OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>()
							select e.Name).ToArray<string>();
						if (!array2.Any((string k) => !flattenKeys.Any((string fk) => fk.StartsWith(k, StringComparison.Ordinal))) && Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken.DeepEquals(jobject2, Microsoft.ProgramSynthesis.Transformation.Json.Semantics.Semantics.FlattenObject(jobject)))
						{
							list.AddRange(jtoken.GetAllPathsTo(jobject));
						}
					}
				}
				if (list.Count == 0)
				{
					return null;
				}
				dictionary[key] = list;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE28 RID: 56872 RVA: 0x002F2E8C File Offset: 0x002F108C
		[WitnessFunction("Array", 0)]
		internal DisjunctiveExamplesSpec WitnessElementsInArray(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray[] array = keyValuePair.Value.OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>().ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>();
				if (array.Length == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = array;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE29 RID: 56873 RVA: 0x002F2F0C File Offset: 0x002F110C
		private static IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject> ToArrayCandidates(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken x)
		{
			foreach (Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject jobject in x.DescendantsAndSelf().OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject>())
			{
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty[] array = jobject.Children().OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>().ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>();
				if (array.Length >= 2)
				{
					if (array.Select((Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty c) => c.Value.Type).ConvertToHashSet<JTokenType>().Count == 1)
					{
						yield return jobject;
					}
				}
			}
			IEnumerator<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600DE2A RID: 56874 RVA: 0x002F2F1C File Offset: 0x002F111C
		[WitnessFunction("ToArray", 1, DependsOnParameters = new int[] { 0 })]
		internal DisjunctiveExamplesSpec WitnessPathInToArray(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec xSpec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in xSpec.Examples)
			{
				State key = keyValuePair.Key;
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken jtoken = keyValuePair.Value as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken;
				if (jtoken == null)
				{
					return null;
				}
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray[] array = spec.DisjunctiveExamples[key].OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>().ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>();
				if (array.Length == 0)
				{
					return null;
				}
				List<JPath> list = new List<JPath>();
				foreach (Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JObject jobject in Witnesses.ToArrayCandidates(jtoken))
				{
					Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						if (Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken.DeepEquals(array2[i], Microsoft.ProgramSynthesis.Transformation.Json.Semantics.Semantics.ToArray(jobject)))
						{
							list.AddRange(jtoken.GetAllPathsTo(jobject));
						}
					}
				}
				if (list.Count == 0)
				{
					return null;
				}
				dictionary[key] = list;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE2B RID: 56875 RVA: 0x002F3054 File Offset: 0x002F1254
		[WitnessFunction("Property", 0)]
		internal DisjunctiveExamplesSpec WitnessKeyInProperty(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				string[] array = (from prop in keyValuePair.Value.OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>()
					select prop.Name).ToArray<string>();
				if (array.Length == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = array;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE2C RID: 56876 RVA: 0x002F30F8 File Offset: 0x002F12F8
		[WitnessFunction("Property", 1)]
		internal DisjunctiveExamplesSpec WitnessValueInProperty(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken[] array = (from prop in keyValuePair.Value.OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>()
					select prop.Value).ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>();
				if (array.Length == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = array;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE2D RID: 56877 RVA: 0x002F319C File Offset: 0x002F139C
		[WitnessFunction("Transform", 1)]
		internal DisjunctiveExamplesSpec WitnessSelectArrayInTransform(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken jtoken = keyValuePair.Key[this._build.Symbol.x] as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken;
				if (jtoken == null)
				{
					return null;
				}
				HashSet<int> arraySizes = (from e in keyValuePair.Value.OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>()
					select e.Count).ConvertToHashSet<int>();
				IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray> enumerable = from a in jtoken.DescendantsAndSelf().OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>()
					where arraySizes.Contains(a.Count)
					select a;
				IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray> enumerable2 = from obj in Witnesses.ToArrayCandidates(jtoken)
					where arraySizes.Contains(obj.Count)
					select Microsoft.ProgramSynthesis.Transformation.Json.Semantics.Semantics.ToArray(obj);
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray[] array = enumerable.Union(enumerable2).ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>();
				if (array.Length == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = array;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE2E RID: 56878 RVA: 0x002F32FC File Offset: 0x002F14FC
		[WitnessFunction("TransformFlatten", 1)]
		internal DisjunctiveExamplesSpec WitnessSelectArrayInTransformFlatten(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken jtoken = keyValuePair.Key[this._build.Symbol.x] as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken;
				if (jtoken == null)
				{
					return null;
				}
				HashSet<int> eligibleArraySizes = keyValuePair.Value.OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>().SelectMany((Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray e) => Witnesses.GetDivisors(e.Count)).ConvertToHashSet<int>();
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray[] array = (from a in jtoken.DescendantsAndSelf().OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>()
					where eligibleArraySizes.Contains(a.Count)
					select a).ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>();
				IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray> enumerable = from obj in Witnesses.ToArrayCandidates(jtoken)
					where eligibleArraySizes.Contains(obj.Count)
					select Microsoft.ProgramSynthesis.Transformation.Json.Semantics.Semantics.ToArray(obj);
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray[] array2 = array.Union(enumerable).ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>();
				if (array2.Length == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = array2;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE2F RID: 56879 RVA: 0x002F3460 File Offset: 0x002F1660
		[WitnessFunction("TransformFlatten", 0, DependsOnParameters = new int[] { 1 })]
		internal ExampleSpec WitnessTokenInTransformFlatten(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec arraySpec)
		{
			Dictionary<State, object> dictionary = new Dictionary<State, object>();
			foreach (KeyValuePair<State, object> keyValuePair in arraySpec.Examples)
			{
				State key = keyValuePair.Key;
				object[] selectArray = keyValuePair.Value.ToEnumerable<object>().ToArray<object>();
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray jarray = spec.DisjunctiveExamples[key].OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray>().FirstOrDefault((Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray f) => f.Count % selectArray.Length == 0);
				if (jarray == null)
				{
					return null;
				}
				int num = jarray.Count / selectArray.Length;
				int num2 = 0;
				foreach (object obj in selectArray)
				{
					List<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken> list = new List<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>(num);
					int j = 0;
					while (j < num)
					{
						list.Add(jarray[num2]);
						j++;
						num2++;
					}
					State state = key.WithFunctionalInput(obj, false);
					dictionary[state] = new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JArray(list);
				}
			}
			return new ExampleSpec(dictionary);
		}

		// Token: 0x0600DE30 RID: 56880 RVA: 0x002F3594 File Offset: 0x002F1794
		[WitnessFunction("Key", 0)]
		internal DisjunctiveExamplesSpec WitnessPathInKey(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				string[] array = keyValuePair.Value.OfType<string>().ToArray<string>();
				if (array.Length == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = array.Select((string k) => new Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue(k)).ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue>();
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE31 RID: 56881 RVA: 0x002F363C File Offset: 0x002F183C
		[WitnessFunction("ConstKey", 0)]
		internal DisjunctiveExamplesSpec WitnessStrInConstKey(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				string[] array = keyValuePair.Value.OfType<string>().ToArray<string>();
				if (array.Length == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = array;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE32 RID: 56882 RVA: 0x002F36BC File Offset: 0x002F18BC
		[RuleLearner("SelectOrTransformValue")]
		internal Optional<ProgramSet> LearnObjectBodyLet(SynthesisEngine engine, GrammarRule rule, LearningTask<DisjunctiveExamplesSpec> task, CancellationToken cancel)
		{
			LearningTask learningTask = task.Clone(this._build.Symbol.selectValue, null);
			ProgramSetBuilder<selectValue> programSetBuilder = this._build.Set.Cast.selectValue(engine.Learn(learningTask, cancel));
			if (!ProgramSetBuilder.IsNullOrEmpty<selectValue>(programSetBuilder))
			{
				return this._build.Set.Join.SelectOrTransformValue(this._build.Set.UnnamedConversion.selectOrTransformValue_selectValue(programSetBuilder)).Set.Some<ProgramSet>();
			}
			LearningTask learningTask2 = task.Clone(this._build.Symbol.transformValue, null);
			ProgramSetBuilder<transformValue> programSetBuilder2 = this._build.Set.Cast.transformValue(engine.Learn(learningTask2, cancel));
			return this._build.Set.Join.SelectOrTransformValue(this._build.Set.UnnamedConversion.selectOrTransformValue_transformValue(programSetBuilder2)).Set.Some<ProgramSet>();
		}

		// Token: 0x0600DE33 RID: 56883 RVA: 0x002F37AC File Offset: 0x002F19AC
		[WitnessFunction("Value", 0)]
		internal DisjunctiveExamplesSpec WitnessSelectKeyInValue(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue[] array = keyValuePair.Value.OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue>().ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue>();
				if (array.Length == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = array.Select((Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue v) => v.ToString(CultureInfo.InvariantCulture)).ToArray<string>();
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE34 RID: 56884 RVA: 0x002F3854 File Offset: 0x002F1A54
		[WitnessFunction("TransformValue", 0)]
		internal DisjunctiveExamplesSpec WitnessSelectKeyInTransformValue(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue[] array = (from v in keyValuePair.Value.OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue>()
					where v.Type == JTokenType.String
					select v).ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue>();
				if (array.Length == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = array.Select((Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue v) => ValueSubstring.Create(v.ToString(CultureInfo.InvariantCulture), null, null, null, null)).ToArray<ValueSubstring>();
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE35 RID: 56885 RVA: 0x002F3924 File Offset: 0x002F1B24
		private static bool CanTransform(Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue node, ValueSubstring example)
		{
			string text = node.Value as string;
			string value = example.Value;
			if (text == null || value == null)
			{
				return false;
			}
			for (int i = 0; i < text.Length - 1; i++)
			{
				if (value.Contains(text.Substring(i, 2)))
				{
					return true;
				}
			}
			return true;
		}

		// Token: 0x0600DE36 RID: 56886 RVA: 0x002F3974 File Offset: 0x002F1B74
		[RuleLearner("TransformLet")]
		internal Optional<ProgramSet> LearnTransformLet(SynthesisEngine engine, LetRule rule, LearningTask<DisjunctiveExamplesSpec> task, CancellationToken cancel)
		{
			string[] array = (from input in task.Spec.ProvidedInputs
				let x = input[this._build.Symbol.x] as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken
				where x != null
				let values = task.Spec.DisjunctiveExamples[input].OfType<ValueSubstring>().ToList<ValueSubstring>()
				where values.Count > 0
				from node in x.DescendantsAndSelf().OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue>()
				where values.Any((ValueSubstring value) => Witnesses.CanTransform(node, value))
				select Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken.GetJPath(x, node).Serialize()).Distinct<string>().ToArray<string>();
			_LetB0 letB = this._build.Node.Rule.SelectStringValues(this._build.Node.Variable.x);
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in task.Spec.DisjunctiveExamples)
			{
				State key = keyValuePair.Key;
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken jtoken = key[this._build.Symbol.x] as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken;
				if (jtoken == null)
				{
					return OptionalUtils.Some((T)null);
				}
				JPathMapperRow jpathMapperRow = new JPathMapperRow(jtoken.Wrapped, array);
				State state = key.Bind(rule.Variable, jpathMapperRow);
				IEnumerable<object> enumerable;
				if (!dictionary.TryGetValue(state, out enumerable))
				{
					dictionary[state] = keyValuePair.Value;
				}
				else
				{
					IReadOnlyList<object> readOnlyList = enumerable.Intersect(keyValuePair.Value, ValueEquality.Comparer).ToList<object>();
					if (readOnlyList.IsEmpty<object>())
					{
						return OptionalUtils.Some((T)null);
					}
					dictionary[state] = readOnlyList;
				}
			}
			LearningTask learningTask = task.Clone(rule.LetBody, new DisjunctiveExamplesSpec(dictionary));
			ProgramSet programSet = engine.Learn(learningTask, cancel);
			ProgramSetBuilder<transformLet> programSetBuilder = this._build.Set.Join.TransformLet(ProgramSetBuilder.List<_LetB0>(new _LetB0[] { letB }), this._build.Set.Cast.transformString(programSet));
			return ((programSetBuilder != null) ? programSetBuilder.Set : null).Some<ProgramSet>();
		}

		// Token: 0x17002525 RID: 9509
		// (get) Token: 0x0600DE37 RID: 56887 RVA: 0x002F3C4C File Offset: 0x002F1E4C
		[ExternLearningLogicMapping("TransformString")]
		public DomainLearningLogic ExternWitnessFunction
		{
			get
			{
				return this._externWitnessFunction;
			}
		}

		// Token: 0x0600DE38 RID: 56888 RVA: 0x002F3C54 File Offset: 0x002F1E54
		[WitnessFunction("SelectObject", 1, DependsOnParameters = new int[] { 0 })]
		internal DisjunctiveExamplesSpec WitnessPathInSelectObject(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec xSpec)
		{
			return Witnesses.WitnessPath(JTokenType.Object, spec, xSpec);
		}

		// Token: 0x0600DE39 RID: 56889 RVA: 0x002F3C60 File Offset: 0x002F1E60
		[WitnessFunction("SelectProperty", 1, DependsOnParameters = new int[] { 0 })]
		internal DisjunctiveExamplesSpec WitnessPathInSelectProperty(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec xSpec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in xSpec.Examples)
			{
				State key = keyValuePair.Key;
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken x = keyValuePair.Value as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken;
				if (x == null)
				{
					return null;
				}
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty[] array = spec.DisjunctiveExamples[key].OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>().ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>();
				HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty> hashSet = new HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>();
				List<JPath> list = new List<JPath>();
				Func<<62da6841-b783-425d-9db2-c715c1c05351><>f__AnonymousType4<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty, Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>, IEnumerable<JPath>> <>9__3;
				for (Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken jtoken = x; jtoken != null; jtoken = jtoken.Parent)
				{
					HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty> descendants = jtoken.DescendantsAndSelf().OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>().ConvertToHashSet(IdentityEquality.Comparer);
					HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty> hashSet2 = new HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>(descendants);
					descendants.ExceptWith(hashSet);
					hashSet = hashSet2;
					var enumerable = from example in array
						from cand in descendants
						where Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken.DeepEquals(cand, example)
						select <>h__TransparentIdentifier0;
					var func;
					if ((func = <>9__3) == null)
					{
						func = (<>9__3 = <>h__TransparentIdentifier0 => x.GetAllPathsTo(<>h__TransparentIdentifier0.cand.Value));
					}
					list = enumerable.SelectMany(func, (<>h__TransparentIdentifier0, JPath path) => path).ToList<JPath>();
					if (list.Count != 0)
					{
						dictionary[key] = list;
						break;
					}
				}
				if (list.Count == 0)
				{
					return null;
				}
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE3A RID: 56890 RVA: 0x002F3E44 File Offset: 0x002F2044
		[WitnessFunction("SelectKey", 1, DependsOnParameters = new int[] { 0 })]
		internal DisjunctiveExamplesSpec WitnessPathInSelectKey(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec xSpec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, object> keyValuePair in xSpec.Examples)
			{
				State key = keyValuePair.Key;
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken x = keyValuePair.Value as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken;
				if (x == null)
				{
					return null;
				}
				string[] array = spec.DisjunctiveExamples[key].OfType<string>().ToArray<string>();
				HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty> hashSet = new HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>();
				List<JPath> list = new List<JPath>();
				Func<<62da6841-b783-425d-9db2-c715c1c05351><>f__AnonymousType4<string, Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>, IEnumerable<JPath>> <>9__3;
				for (Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken jtoken = x; jtoken != null; jtoken = jtoken.Parent)
				{
					HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty> descendants = jtoken.DescendantsAndSelf().OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>().ConvertToHashSet(IdentityEquality.Comparer);
					HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty> hashSet2 = new HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JProperty>(descendants);
					descendants.ExceptWith(hashSet);
					hashSet = hashSet2;
					var enumerable = from example in array
						from cand in descendants
						where cand.Name == example
						select <>h__TransparentIdentifier0;
					var func;
					if ((func = <>9__3) == null)
					{
						func = (<>9__3 = <>h__TransparentIdentifier0 => x.GetAllPathsTo(<>h__TransparentIdentifier0.cand.Value));
					}
					list = enumerable.SelectMany(func, (<>h__TransparentIdentifier0, JPath path) => path).ToList<JPath>();
					if (list.Count != 0)
					{
						dictionary[key] = list;
						break;
					}
				}
				if (list.Count == 0)
				{
					return null;
				}
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE3B RID: 56891 RVA: 0x002F4028 File Offset: 0x002F2228
		[WitnessFunction("SelectValue", 1, DependsOnParameters = new int[] { 0 })]
		internal DisjunctiveExamplesSpec WitnessPathInSelectValue(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec xSpec)
		{
			HashSet<JTokenType> hashSet = spec.DisjunctiveExamples.SelectMany(delegate(KeyValuePair<State, IEnumerable<object>> example)
			{
				KeyValuePair<State, IEnumerable<object>> keyValuePair = example;
				return keyValuePair.Value.OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>();
			}, (KeyValuePair<State, IEnumerable<object>> example, Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken obj) => obj.Type).ConvertToHashSet<JTokenType>();
			if (hashSet.Count == 1)
			{
				return Witnesses.WitnessPath(hashSet.Single<JTokenType>(), spec, xSpec);
			}
			return null;
		}

		// Token: 0x0600DE3C RID: 56892 RVA: 0x002F409C File Offset: 0x002F229C
		[WitnessFunction("ValueToString", 1, DependsOnParameters = new int[] { 0 })]
		internal DisjunctiveExamplesSpec WitnessPathInValueToString(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec xSpec)
		{
			return Witnesses.WitnessPathInConvertValue(spec, xSpec, null, new JTokenType?(JTokenType.String));
		}

		// Token: 0x0600DE3D RID: 56893 RVA: 0x002F40C0 File Offset: 0x002F22C0
		[WitnessFunction("ConvertValueTo", 1)]
		internal DisjunctiveExamplesSpec WitnessTypeInConvertValueTo(GrammarRule rule, DisjunctiveExamplesSpec spec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			foreach (KeyValuePair<State, IEnumerable<object>> keyValuePair in spec.DisjunctiveExamples)
			{
				List<object> list = Witnesses.AllowedConvertToTypes.Intersect(from e in keyValuePair.Value.OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue>()
					select e.Type).Cast<object>().ToList<object>();
				if (list.Count == 0)
				{
					return null;
				}
				dictionary[keyValuePair.Key] = list;
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE3E RID: 56894 RVA: 0x002F4178 File Offset: 0x002F2378
		[WitnessFunction("ConvertValueTo", 2, DependsOnParameters = new int[] { 0, 1 })]
		internal DisjunctiveExamplesSpec WitnessPathInConvertValueTo(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec xSpec, ExampleSpec typeSpec)
		{
			return Witnesses.WitnessPathInConvertValue(spec, xSpec, new JTokenType?(JTokenType.String), null);
		}

		// Token: 0x0600DE3F RID: 56895 RVA: 0x002F419C File Offset: 0x002F239C
		private static DisjunctiveExamplesSpec WitnessPathInConvertValue(DisjunctiveExamplesSpec spec, ExampleSpec xSpec, JTokenType? srcType, JTokenType? destType)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			Func<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue, bool> <>9__1;
			Func<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue, bool> <>9__2;
			foreach (KeyValuePair<State, object> keyValuePair in xSpec.Examples)
			{
				State key = keyValuePair.Key;
				object value = keyValuePair.Value;
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken x = value as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken;
				if (x == null)
				{
					return null;
				}
				IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue> enumerable = spec.DisjunctiveExamples[key].OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue>();
				Func<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue, bool> func;
				if ((func = <>9__1) == null)
				{
					func = (<>9__1 = (Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue e) => destType == null || e.Type == destType.Value);
				}
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue[] array = enumerable.Where(func).ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue>();
				if (array.Length == 0)
				{
					return null;
				}
				HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue> hashSet = new HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue>();
				List<JPath> list = new List<JPath>();
				Func<<62da6841-b783-425d-9db2-c715c1c05351><>f__AnonymousType4<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue, Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue>, IEnumerable<JPath>> <>9__6;
				for (Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken jtoken = x; jtoken != null; jtoken = jtoken.Parent)
				{
					Witnesses.<>c__DisplayClass33_2 CS$<>8__locals3 = new Witnesses.<>c__DisplayClass33_2();
					Witnesses.<>c__DisplayClass33_2 CS$<>8__locals4 = CS$<>8__locals3;
					IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue> enumerable2 = jtoken.DescendantsAndSelf().OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue>();
					Func<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue, bool> func2;
					if ((func2 = <>9__2) == null)
					{
						func2 = (<>9__2 = (Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue t) => srcType == null || t.Type == srcType.Value);
					}
					CS$<>8__locals4.descendants = enumerable2.Where(func2).ConvertToHashSet(IdentityEquality.Comparer);
					HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue> hashSet2 = new HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JValue>(CS$<>8__locals3.descendants);
					CS$<>8__locals3.descendants.ExceptWith(hashSet);
					hashSet = hashSet2;
					var enumerable3 = from example in array
						from cand in CS$<>8__locals3.descendants
						where Witnesses.<WitnessPathInConvertValue>g__ToString|33_0(cand.Value) == Witnesses.<WitnessPathInConvertValue>g__ToString|33_0(example.Value)
						select <>h__TransparentIdentifier0;
					var func3;
					if ((func3 = <>9__6) == null)
					{
						func3 = (<>9__6 = <>h__TransparentIdentifier0 => x.GetAllPathsTo(<>h__TransparentIdentifier0.cand));
					}
					list = enumerable3.SelectMany(func3, (<>h__TransparentIdentifier0, JPath path) => path).ToList<JPath>();
					if (list.Count != 0)
					{
						dictionary[key] = list;
						break;
					}
				}
				if (list.Count == 0)
				{
					return null;
				}
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE40 RID: 56896 RVA: 0x002F43F8 File Offset: 0x002F25F8
		[WitnessFunction("SelectArray", 1, DependsOnParameters = new int[] { 0 })]
		internal DisjunctiveExamplesSpec WitnessPathInSelectArray(GrammarRule rule, DisjunctiveExamplesSpec spec, ExampleSpec xSpec)
		{
			return Witnesses.WitnessPath(JTokenType.Array, spec, xSpec);
		}

		// Token: 0x0600DE41 RID: 56897 RVA: 0x002F4404 File Offset: 0x002F2604
		private static DisjunctiveExamplesSpec WitnessPath(JTokenType objectType, DisjunctiveExamplesSpec spec, ExampleSpec xSpec)
		{
			Dictionary<State, IEnumerable<object>> dictionary = new Dictionary<State, IEnumerable<object>>();
			Func<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken, bool> <>9__0;
			Func<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken, bool> <>9__1;
			foreach (KeyValuePair<State, object> keyValuePair in xSpec.Examples)
			{
				State key = keyValuePair.Key;
				object value = keyValuePair.Value;
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken x = value as Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken;
				if (x == null)
				{
					return null;
				}
				IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken> enumerable = spec.DisjunctiveExamples[key].OfType<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>();
				Func<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken, bool> func;
				if ((func = <>9__0) == null)
				{
					func = (<>9__0 = (Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken e) => e.Type == objectType);
				}
				Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken[] array = enumerable.Where(func).ToArray<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>();
				HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken> hashSet = new HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>();
				List<JPath> list = new List<JPath>();
				Func<<62da6841-b783-425d-9db2-c715c1c05351><>f__AnonymousType4<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken, Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>, IEnumerable<JPath>> <>9__5;
				for (Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken jtoken = x; jtoken != null; jtoken = jtoken.Parent)
				{
					Witnesses.<>c__DisplayClass35_2 CS$<>8__locals3 = new Witnesses.<>c__DisplayClass35_2();
					Witnesses.<>c__DisplayClass35_2 CS$<>8__locals4 = CS$<>8__locals3;
					IEnumerable<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken> enumerable2 = jtoken.DescendantsAndSelf();
					Func<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken, bool> func2;
					if ((func2 = <>9__1) == null)
					{
						func2 = (<>9__1 = (Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken o) => o.Type == objectType);
					}
					CS$<>8__locals4.descendants = enumerable2.Where(func2).ConvertToHashSet(IdentityEquality.Comparer);
					HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken> hashSet2 = new HashSet<Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken>(CS$<>8__locals3.descendants);
					CS$<>8__locals3.descendants.ExceptWith(hashSet);
					hashSet = hashSet2;
					var enumerable3 = from example in array
						from cand in CS$<>8__locals3.descendants
						where Microsoft.ProgramSynthesis.Transformation.Json.Semantics.WrappedJsonNet.JToken.DeepEquals(cand, example)
						select <>h__TransparentIdentifier0;
					var func3;
					if ((func3 = <>9__5) == null)
					{
						func3 = (<>9__5 = <>h__TransparentIdentifier0 => x.GetAllPathsTo(<>h__TransparentIdentifier0.cand));
					}
					list = enumerable3.SelectMany(func3, (<>h__TransparentIdentifier0, JPath path) => path).ToList<JPath>();
					if (list.Count != 0)
					{
						dictionary[key] = list;
						break;
					}
				}
				if (list.Count == 0)
				{
					return null;
				}
			}
			return DisjunctiveExamplesSpec.From(dictionary);
		}

		// Token: 0x0600DE42 RID: 56898 RVA: 0x002F4648 File Offset: 0x002F2848
		private static IEnumerable<int> GetDivisors(int n)
		{
			return from d in Enumerable.Range(2, n / 2)
				where n % d == 0
				select d;
		}

		// Token: 0x0600DE44 RID: 56900 RVA: 0x002F46A6 File Offset: 0x002F28A6
		[CompilerGenerated]
		internal static string <WitnessPathInConvertValue>g__ToString|33_0(object s)
		{
			return ((s != null) ? s.ToString() : null) ?? "null";
		}

		// Token: 0x04005444 RID: 21572
		private readonly GrammarBuilders _build;

		// Token: 0x04005445 RID: 21573
		private readonly DomainLearningLogic _externWitnessFunction;

		// Token: 0x04005446 RID: 21574
		private static readonly HashSet<JTokenType> AllowedConvertToTypes = new HashSet<JTokenType>
		{
			JTokenType.Integer,
			JTokenType.Boolean,
			JTokenType.Float
		};
	}
}

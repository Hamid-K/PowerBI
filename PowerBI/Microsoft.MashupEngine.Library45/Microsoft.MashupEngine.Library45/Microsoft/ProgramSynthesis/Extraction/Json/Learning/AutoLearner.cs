using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Learning
{
	// Token: 0x02000B74 RID: 2932
	[NullableContext(1)]
	[Nullable(0)]
	public class AutoLearner
	{
		// Token: 0x06004A6E RID: 19054 RVA: 0x000E9BA4 File Offset: 0x000E7DA4
		public AutoLearner(Grammar grammar, SynthesisOptions options)
		{
			this._build = GrammarBuilders.Instance(grammar);
			this._options = options;
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x000E9BE0 File Offset: 0x000E7DE0
		[return: Nullable(2)]
		public IReadOnlyList<output> Learn(IEnumerable<ParsedJson> jsons)
		{
			IReadOnlyList<output> readOnlyList2;
			try
			{
				IReadOnlyList<AutoLearner.SchemaElements> readOnlyList = this.LearnBodyFromConstraints(jsons);
				if (readOnlyList == null)
				{
					readOnlyList2 = null;
				}
				else
				{
					List<output> list = new List<output>();
					foreach (AutoLearner.SchemaElements schemaElements in readOnlyList)
					{
						ProgramNode programNode = this.CreateStructIfNeeded(schemaElements);
						list.Add(this.CreateConvertOutput(programNode));
					}
					if (list.Count != 0)
					{
						readOnlyList2 = list;
					}
					else
					{
						@struct @struct = this.CreateStruct(this._build.Node.Rule.Empty());
						readOnlyList2 = new output[] { this._build.Node.UnnamedConversion.output_struct(@struct) };
					}
				}
			}
			catch (AutoLearner.TooManyElementsException)
			{
				readOnlyList2 = null;
			}
			return readOnlyList2;
		}

		// Token: 0x06004A70 RID: 19056 RVA: 0x000E9CB8 File Offset: 0x000E7EB8
		private IReadOnlyList<AutoLearner.SchemaElements> LearnBodyFromConstraints(IEnumerable<ParsedJson> jsons)
		{
			List<JToken> list = new List<JToken>();
			bool flag = false;
			foreach (ParsedJson parsedJson in jsons)
			{
				if (parsedJson.IsDelimitedJson)
				{
					flag = true;
				}
				list.AddRange(parsedJson.Regions.Select((JsonRegion region) => region.Token));
			}
			this._joinArrays = new HashSet<JArray>();
			if (!flag)
			{
				if (list.All((JToken t) => t is JArray))
				{
					this._joinArrays = list.Cast<JArray>().ConvertToHashSet<JArray>();
				}
			}
			if (this._options.Auto)
			{
				this._joinArrays = AutoLearner.FindJoiningArrays(list);
			}
			if (this._options.JoinSingleTopArray)
			{
				IEnumerable<JToken> enumerable = list;
				Func<JToken, JArray> func;
				if ((func = AutoLearner.<>O.<0>__FindSingleTopArray) == null)
				{
					func = (AutoLearner.<>O.<0>__FindSingleTopArray = new Func<JToken, JArray>(AutoLearner.FindSingleTopArray));
				}
				HashSet<JArray> hashSet = enumerable.Select(func).ConvertToHashSet<JArray>();
				if (hashSet.All((JArray array) => array != null))
				{
					this._joinArrays = hashSet;
				}
			}
			IReadOnlyList<string[]> deletePaths = this._options.DeletePaths;
			if (deletePaths != null && deletePaths.Count > 0)
			{
				this._deleteTokens = AutoLearner.ConvertPathsToTokens(this._options.DeletePaths, list, false);
			}
			IReadOnlyList<string[]> splitArrayPaths = this._options.SplitArrayPaths;
			if (splitArrayPaths != null && splitArrayPaths.Count > 0)
			{
				this._splitArrays = AutoLearner.ConvertPathsToTokens(this._options.SplitArrayPaths, list, true).Cast<JArray>().ToList<JArray>();
			}
			IReadOnlyList<string[]> joinArrayPaths = this._options.JoinArrayPaths;
			if (joinArrayPaths != null && joinArrayPaths.Count > 0)
			{
				this._joinArrays.UnionWith(AutoLearner.ConvertPathsToTokens(this._options.JoinArrayPaths, list, true).Cast<JArray>());
			}
			string text = this._options.NamePrefix ?? string.Empty;
			return this.LearnFlatten(text, new List<JPathStep>(), list);
		}

		// Token: 0x06004A71 RID: 19057 RVA: 0x000E9ED4 File Offset: 0x000E80D4
		private static IReadOnlyList<JToken> ConvertPathsToTokens(IReadOnlyList<string[]> paths, IReadOnlyList<JToken> tokens, bool destTokenIsArray = false)
		{
			AutoLearner.<>c__DisplayClass11_0 CS$<>8__locals1 = new AutoLearner.<>c__DisplayClass11_0();
			CS$<>8__locals1.tokens = tokens;
			CS$<>8__locals1.destTokenIsArray = destTokenIsArray;
			return paths.Select(new Func<string[], JToken>(CS$<>8__locals1.<ConvertPathsToTokens>g__Convert|0)).ToList<JToken>();
		}

		// Token: 0x06004A72 RID: 19058 RVA: 0x000E9F0C File Offset: 0x000E810C
		internal static HashSet<JArray> FindJoiningArrays(IReadOnlyList<JToken> tokens)
		{
			Func<JToken, JArray> func;
			if ((func = AutoLearner.<>O.<0>__FindSingleTopArray) == null)
			{
				func = (AutoLearner.<>O.<0>__FindSingleTopArray = new Func<JToken, JArray>(AutoLearner.FindSingleTopArray));
			}
			List<JArray> list = tokens.Select(func).ToList<JArray>();
			if (!AutoLearner.<FindJoiningArrays>g__IsJoiningArrays|12_0(list, true))
			{
				return new HashSet<JArray>();
			}
			List<JArray> list2 = list.SelectMany(delegate(JArray array)
			{
				Func<JToken, JArray> func2;
				if ((func2 = AutoLearner.<>O.<0>__FindSingleTopArray) == null)
				{
					func2 = (AutoLearner.<>O.<0>__FindSingleTopArray = new Func<JToken, JArray>(AutoLearner.FindSingleTopArray));
				}
				return array.Select(func2);
			}).ToList<JArray>();
			if (AutoLearner.<FindJoiningArrays>g__IsJoiningArrays|12_0(list2, false))
			{
				return list.Concat(list2).ConvertToHashSet<JArray>();
			}
			return list.ConvertToHashSet<JArray>();
		}

		// Token: 0x06004A73 RID: 19059 RVA: 0x000E9F98 File Offset: 0x000E8198
		[return: Nullable(2)]
		internal static JArray FindSingleTopArray(JToken token)
		{
			JArray jarray = null;
			Queue<JToken> queue = new Queue<JToken>();
			queue.Enqueue(token);
			while (queue.Count > 0)
			{
				JToken jtoken = queue.Dequeue();
				JArray jarray2 = jtoken as JArray;
				if (jarray2 == null)
				{
					JObject jobject = jtoken as JObject;
					if (jobject != null)
					{
						foreach (JToken jtoken2 in jobject.PropertyValues())
						{
							queue.Enqueue(jtoken2);
						}
					}
				}
				else
				{
					if (jarray != null)
					{
						return null;
					}
					jarray = jarray2;
				}
			}
			return jarray;
		}

		// Token: 0x06004A74 RID: 19060 RVA: 0x000EA034 File Offset: 0x000E8234
		private IReadOnlyList<AutoLearner.SchemaElements> LearnFlatten(string name, IReadOnlyList<JPathStep> steps, IReadOnlyList<JToken> nodes)
		{
			if (nodes.Any(new Func<JToken, bool>(this._deleteTokens.Contains<JToken>)))
			{
				return new List<AutoLearner.SchemaElements>();
			}
			JTokenType[] array = nodes.Select((JToken n) => n.Type).Distinct<JTokenType>().ToArray<JTokenType>();
			if (array.Length > 1)
			{
				return this.CreateField(name, steps, true);
			}
			switch (array[0])
			{
			case JTokenType.Object:
				return this.LearnFlattenObject(name, steps, nodes.Cast<JObject>().ToArray<JObject>());
			case JTokenType.Array:
				return this.LearnFlattenArray(name, steps, nodes.Cast<JArray>().ToArray<JArray>());
			case JTokenType.Property:
			{
				string name2 = ((JProperty)nodes[0]).Name;
				string text = ((name == string.Empty) ? name2 : FormattableString.Invariant(FormattableStringFactory.Create("{0}.{1}", new object[] { name, name2 })));
				List<JPathStep> list = steps.Concat(new AccessStep[]
				{
					new AccessStep(name2)
				}).ToList<JPathStep>();
				List<JToken> list2 = nodes.Select((JToken n) => ((JProperty)n).Value).ToList<JToken>();
				return this.LearnFlatten(text, list, list2);
			}
			}
			return this.CreateField(name, steps, true);
		}

		// Token: 0x06004A75 RID: 19061 RVA: 0x000EA188 File Offset: 0x000E8388
		private IReadOnlyList<AutoLearner.SchemaElements> CreateField(string name, IReadOnlyList<JPathStep> steps, bool fixEmptyName = true)
		{
			if (fixEmptyName && string.IsNullOrWhiteSpace(name))
			{
				name = "column";
			}
			selectRegion selectRegion = this.CreateSelectRegion(new JPath(steps));
			return new AutoLearner.SchemaElements[]
			{
				new AutoLearner.SchemaElements(this.CreateField(name, selectRegion).Node)
			};
		}

		// Token: 0x06004A76 RID: 19062 RVA: 0x000EA1D4 File Offset: 0x000E83D4
		private IReadOnlyList<AutoLearner.SchemaElements> LearnFlattenObject(string name, IReadOnlyList<JPathStep> steps, IReadOnlyList<JObject> objects)
		{
			bool flag;
			if (objects.Count != 1 || !AutoLearner.HaveSameKeys(objects[0].Properties(), out flag))
			{
				OrderedDictionary orderedDictionary = AutoLearner.GroupPropertiesByKey(objects);
				List<IReadOnlyList<AutoLearner.SchemaElements>> list = new List<IReadOnlyList<AutoLearner.SchemaElements>>();
				foreach (object obj in orderedDictionary.Values)
				{
					List<JProperty> list2 = (List<JProperty>)obj;
					IReadOnlyList<AutoLearner.SchemaElements> readOnlyList = this.LearnFlatten(name, steps, list2);
					if (readOnlyList.Count > 0)
					{
						list.Add(readOnlyList);
					}
				}
				return this.EnumerateStructPrograms(list);
			}
			if (flag)
			{
				return (from children in this.LearnFlattenObject(name, new CurrentStep[]
					{
						new CurrentStep()
					}, (from property in objects[0].Properties()
						select property.Value).Cast<JObject>().ToList<JObject>())
					select new AutoLearner.SchemaElements(this.CreateObjectToSequenceNode(steps, children).Node)).ToList<AutoLearner.SchemaElements>();
			}
			return (from children in this.LearnFlattenObject(string.IsNullOrEmpty(name) ? "Value" : (name + ".Value"), new JPathStep[]
				{
					new CurrentStep(),
					new PropertyValueStep()
				}, (from property in objects[0].Properties()
					select property.Value).Cast<JObject>().ToList<JObject>())
				select new AutoLearner.SchemaElements(this.CreateSequenceNode(steps, new AutoLearner.SchemaElements(children.PrependItem(this.CreateField(string.IsNullOrEmpty(name) ? "Name" : (name + ".Name"), this.CreateSelectRegion(new JPath(new JPathStep[]
				{
					new PropertyKeyStep()
				}))).Node))).Node)).ToList<AutoLearner.SchemaElements>();
		}

		// Token: 0x06004A77 RID: 19063 RVA: 0x000EA3A4 File Offset: 0x000E85A4
		internal static OrderedDictionary GroupPropertiesByKey(IReadOnlyList<JObject> objects)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			foreach (JObject jobject in objects)
			{
				List<JProperty> list = jobject.Properties().ToList<JProperty>();
				for (int i = 0; i < list.Count; i++)
				{
					JProperty jproperty = list[i];
					if (orderedDictionary.Contains(jproperty.Name))
					{
						((List<JProperty>)orderedDictionary[jproperty.Name]).Add(jproperty);
					}
					else
					{
						int j;
						for (j = i + 1; j < list.Count; j++)
						{
							string nextPropName = list[j].Name;
							if (orderedDictionary.Contains(nextPropName))
							{
								int num = orderedDictionary.Keys.Cast<string>().TakeWhile((string key) => key != nextPropName).Count<string>();
								orderedDictionary.Insert(num, jproperty.Name, new List<JProperty> { jproperty });
								break;
							}
						}
						if (j == list.Count)
						{
							orderedDictionary.Add(jproperty.Name, new List<JProperty> { jproperty });
						}
					}
				}
			}
			return orderedDictionary;
		}

		// Token: 0x06004A78 RID: 19064 RVA: 0x000EA4FC File Offset: 0x000E86FC
		internal static bool HaveSameKeys(IEnumerable<JProperty> properties, out bool hasNameValue)
		{
			HashSet<string> hashSet = null;
			HashSet<string> hashSet2 = null;
			hasNameValue = false;
			int num = 0;
			using (IEnumerator<JProperty> enumerator = properties.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					JProperty property = enumerator.Current;
					num++;
					JObject value = property.Value as JObject;
					if (value == null)
					{
						return false;
					}
					if (hashSet == null || hashSet2 == null)
					{
						hashSet = AutoLearner.<HaveSameKeys>g__Keys|18_0(value).ConvertToHashSet<string>();
						hashSet2 = hashSet.Where(delegate(string key)
						{
							JToken jtoken = value[key];
							if (jtoken != null && jtoken.Type == JTokenType.String)
							{
								JToken jtoken2 = value[key];
								return ((jtoken2 != null) ? jtoken2.ToString() : null) == property.Name;
							}
							return false;
						}).ConvertToHashSet<string>();
					}
					else
					{
						if (!hashSet.SetEquals(AutoLearner.<HaveSameKeys>g__Keys|18_0(value)))
						{
							return false;
						}
						hashSet2.RemoveWhere(delegate(string key)
						{
							JToken jtoken3 = value[key];
							if (jtoken3 != null && jtoken3.Type == JTokenType.String)
							{
								JToken jtoken4 = value[key];
								return ((jtoken4 != null) ? jtoken4.ToString() : null) != property.Name;
							}
							return true;
						});
					}
				}
			}
			if (num <= 1)
			{
				return false;
			}
			if (hashSet == null || hashSet.Count <= 1)
			{
				return false;
			}
			hasNameValue = hashSet2.Any<string>();
			return true;
		}

		// Token: 0x06004A79 RID: 19065 RVA: 0x000EA608 File Offset: 0x000E8808
		private List<AutoLearner.SchemaElements> EnumerateStructPrograms(IReadOnlyList<IReadOnlyList<AutoLearner.SchemaElements>> schemaElementsList)
		{
			if (schemaElementsList.Count == 0)
			{
				return new List<AutoLearner.SchemaElements>();
			}
			if (schemaElementsList.Count > 1000)
			{
				throw new AutoLearner.TooManyElementsException();
			}
			IEnumerable<IEnumerable<ProgramNode>> enumerable = new IEnumerable<ProgramNode>[] { Enumerable.Empty<ProgramNode>() };
			IEnumerable<IEnumerable<ProgramNode>> enumerable2 = schemaElementsList.Aggregate(enumerable, (IEnumerable<IEnumerable<ProgramNode>> acc, IReadOnlyList<AutoLearner.SchemaElements> seq) => from accseq in acc
				from item in seq
				select accseq.Concat(item));
			List<AutoLearner.SchemaElements> list = new List<AutoLearner.SchemaElements>();
			foreach (IEnumerable<ProgramNode> enumerable3 in enumerable2)
			{
				AutoLearner.SchemaElements schemaElements = new AutoLearner.SchemaElements(enumerable3);
				list.Add((schemaElements.Count == 1) ? schemaElements : new AutoLearner.SchemaElements(this.CreateStructIfNeeded(schemaElements)));
			}
			return list;
		}

		// Token: 0x06004A7A RID: 19066 RVA: 0x000EA6C8 File Offset: 0x000E88C8
		private sequence CreateObjectToSequenceNode(IEnumerable<JPathStep> steps, AutoLearner.SchemaElements children)
		{
			ProgramNode programNode = this.CreateStructIfNeeded(children);
			selectSequence selectSequence = this.CreateSelectSequence(new JPath(steps.Concat(new JPathStep[]
			{
				new StarStep(),
				new PropertyValueStep()
			})));
			sequenceBody sequenceBody = this.CreateSequenceBody(programNode, selectSequence);
			return this._build.Node.Rule.DummySequence(sequenceBody);
		}

		// Token: 0x06004A7B RID: 19067 RVA: 0x000EA724 File Offset: 0x000E8924
		private sequence CreateSequenceNode(IEnumerable<JPathStep> steps, AutoLearner.SchemaElements children)
		{
			ProgramNode programNode = this.CreateStructIfNeeded(children);
			selectSequence selectSequence = this.CreateSelectSequence(new JPath(steps.Concat(new StarStep[]
			{
				new StarStep()
			})));
			sequenceBody sequenceBody = this.CreateSequenceBody(programNode, selectSequence);
			return this._build.Node.Rule.DummySequence(sequenceBody);
		}

		// Token: 0x06004A7C RID: 19068 RVA: 0x000EA778 File Offset: 0x000E8978
		internal static bool CanSplitArray(IReadOnlyList<JArray> arrays)
		{
			if (arrays.Count < 2)
			{
				return false;
			}
			int arrayLength = arrays.First<JArray>().Count;
			return arrayLength >= 1 && arrays.All(delegate(JArray array)
			{
				if (array.Count == arrayLength)
				{
					return array.All((JToken e) => e is JValue);
				}
				return false;
			});
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x000EA7C4 File Offset: 0x000E89C4
		private IReadOnlyList<AutoLearner.SchemaElements> LearnFlattenArray(string name, IReadOnlyList<JPathStep> steps, IReadOnlyList<JArray> arrays)
		{
			if (this._options.SplitTopArrays || (this._options.Auto && AutoLearner.CanSplitArray(arrays)) || arrays.Any(new Func<JArray, bool>(this._splitArrays.Contains<JArray>)))
			{
				List<ProgramNode> list = (from n in this.LearnSplitArrayToColumns(name, steps, arrays)
					select n.Node).ToList<ProgramNode>();
				if (list.Count == 0)
				{
					return new AutoLearner.SchemaElements[0];
				}
				return new AutoLearner.SchemaElements[]
				{
					new AutoLearner.SchemaElements(list)
				};
			}
			else
			{
				if (!arrays.Any(new Func<JArray, bool>(this._joinArrays.Contains)) && !this._options.JoinAllArrays)
				{
					return this.CreateField(name, steps, true);
				}
				List<JToken> list2 = arrays.SelectMany((JArray n) => n.Children().Where(delegate(JToken child)
				{
					JValue jvalue = child as JValue;
					return jvalue == null || jvalue.Value != null;
				})).ToList<JToken>();
				if (list2.Count == 0)
				{
					return new AutoLearner.SchemaElements[0];
				}
				return (from children in this.LearnFlatten(name, new CurrentStep[]
					{
						new CurrentStep()
					}, list2)
					select new AutoLearner.SchemaElements(this.CreateSequenceNode(steps, children).Node)).ToList<AutoLearner.SchemaElements>();
			}
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x000EA914 File Offset: 0x000E8B14
		private IReadOnlyList<@struct> LearnSplitArrayToColumns(string name, IReadOnlyList<JPathStep> steps, IEnumerable<JArray> arrays)
		{
			int num = arrays.Max((JArray e) => e.Count);
			List<@struct> list = new List<@struct>(num);
			string text = (string.IsNullOrWhiteSpace(name) ? string.Empty : FormattableString.Invariant(FormattableStringFactory.Create("{0}.", new object[] { name })));
			for (int i = 0; i < num; i++)
			{
				JPath jpath = new JPath(steps.Concat(new IndexStep[]
				{
					new IndexStep(i)
				}));
				@struct @struct = this.CreateField(FormattableString.Invariant(FormattableStringFactory.Create("{0}{1}", new object[] { text, i })), this.CreateSelectRegion(jpath));
				list.Add(@struct);
			}
			return list;
		}

		// Token: 0x06004A7F RID: 19071 RVA: 0x000EA9DC File Offset: 0x000E8BDC
		private ProgramNode CreateStructIfNeeded(AutoLearner.SchemaElements elements)
		{
			int count = elements.Count;
			if (count == 0)
			{
				throw new InvalidOperationException("SchemaElements may never be empty.");
			}
			if (count == 1)
			{
				return elements[0];
			}
			if (elements.Count > 1000)
			{
				throw new AutoLearner.TooManyElementsException();
			}
			structBodyRec structBodyRec = this.CreateToList(elements.Last<ProgramNode>());
			for (int i = elements.Count - 2; i >= 0; i--)
			{
				structBodyRec = this.CreateConcat(elements[i], structBodyRec);
			}
			return this.CreateStruct(structBodyRec).Node;
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x000EAA60 File Offset: 0x000E8C60
		private output CreateConvertOutput(ProgramNode programNode)
		{
			if (programNode.Symbol == this._build.Symbol.@struct)
			{
				return this._build.Node.UnnamedConversion.output_struct(this._build.Node.Cast.@struct(programNode));
			}
			if (programNode.Symbol == this._build.Symbol.sequence)
			{
				return this._build.Node.UnnamedConversion.output_sequence(this._build.Node.Cast.sequence(programNode));
			}
			throw new InvalidOperationException(FormattableString.Invariant(FormattableStringFactory.Create("Unknown grammar top level rule {0}", new object[] { programNode.GrammarRule.Head.Name })));
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x000EAB2C File Offset: 0x000E8D2C
		private @struct CreateStruct(structBodyRec structBodyRec)
		{
			return this._build.Node.Rule.Struct(this._build.Node.Variable.v, structBodyRec);
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x000EAB5C File Offset: 0x000E8D5C
		private @struct CreateField(string idValue, selectRegion selectRegion)
		{
			return this._build.Node.Rule.Field(this._build.Node.Variable.v, this._build.Node.Rule.id(idValue), selectRegion);
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x000EABAA File Offset: 0x000E8DAA
		private structBodyRec CreateConcat(ProgramNode output, structBodyRec structBodyNode)
		{
			return this._build.Node.Rule.Concat(this.CreateConvertOutput(output), structBodyNode);
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x000EABC9 File Offset: 0x000E8DC9
		private structBodyRec CreateToList(ProgramNode output)
		{
			return this._build.Node.Rule.ToList(this.CreateConvertOutput(output));
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x000EABE8 File Offset: 0x000E8DE8
		private sequenceBody CreateSequenceBody(ProgramNode wrapStruct, selectSequence selectSequence)
		{
			return this._build.Node.Rule.SequenceBody(this._build.Node.Rule.WrapStructLet(this._build.Node.Variable.x, this.CreateConvertOutput(wrapStruct)), selectSequence);
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x000EAC3C File Offset: 0x000E8E3C
		private selectSequence CreateSelectSequence(JPath jPath)
		{
			return this._build.Node.Rule.SelectSequence(this._build.Node.Variable.v, this._build.Node.Rule.path(jPath));
		}

		// Token: 0x06004A87 RID: 19079 RVA: 0x000EAC8C File Offset: 0x000E8E8C
		private selectRegion CreateSelectRegion(JPath jPath)
		{
			return this._build.Node.Rule.SelectRegion(this._build.Node.Variable.v, this._build.Node.Rule.path(jPath));
		}

		// Token: 0x06004A88 RID: 19080 RVA: 0x000EACDC File Offset: 0x000E8EDC
		[CompilerGenerated]
		internal static bool <FindJoiningArrays>g__IsJoiningArrays|12_0([Nullable(new byte[] { 1, 2 })] IReadOnlyList<JArray> arrays, bool isOuter = true)
		{
			if (arrays.Count > 0 && arrays.All((JArray array) => array != null && (isOuter || array.Count > 0)) && !AutoLearner.CanSplitArray(arrays))
			{
				return arrays.SelectMany((JArray array) => array.Select((JToken e) => e.Type)).Distinct<JTokenType>().Count<JTokenType>() <= 1;
			}
			return false;
		}

		// Token: 0x06004A89 RID: 19081 RVA: 0x000EAD52 File Offset: 0x000E8F52
		[CompilerGenerated]
		internal static IEnumerable<string> <HaveSameKeys>g__Keys|18_0(JObject obj)
		{
			return from prop in obj.Properties()
				select prop.Name;
		}

		// Token: 0x04002170 RID: 8560
		private const string EmptyArrayName = "column";

		// Token: 0x04002171 RID: 8561
		private const int SplitMinArrayLength = 1;

		// Token: 0x04002172 RID: 8562
		private const int SplitMinArrayInstanceCount = 2;

		// Token: 0x04002173 RID: 8563
		private readonly GrammarBuilders _build;

		// Token: 0x04002174 RID: 8564
		private readonly SynthesisOptions _options;

		// Token: 0x04002175 RID: 8565
		private HashSet<JArray> _joinArrays = new HashSet<JArray>();

		// Token: 0x04002176 RID: 8566
		private IReadOnlyList<JArray> _splitArrays = new List<JArray>();

		// Token: 0x04002177 RID: 8567
		private IReadOnlyList<JToken> _deleteTokens = new List<JToken>();

		// Token: 0x02000B75 RID: 2933
		[Nullable(new byte[] { 0, 1 })]
		private class SchemaElements : List<ProgramNode>
		{
			// Token: 0x06004A8A RID: 19082 RVA: 0x000EAD7E File Offset: 0x000E8F7E
			public SchemaElements(IEnumerable<ProgramNode> nodes)
				: base(nodes)
			{
			}

			// Token: 0x06004A8B RID: 19083 RVA: 0x000EAD87 File Offset: 0x000E8F87
			public SchemaElements(ProgramNode node)
			{
				base.Add(node);
			}
		}

		// Token: 0x02000B76 RID: 2934
		[NullableContext(0)]
		private class TooManyElementsException : Exception
		{
		}

		// Token: 0x02000B77 RID: 2935
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04002178 RID: 8568
			[Nullable(0)]
			public static Func<JToken, JArray> <0>__FindSingleTopArray;
		}
	}
}

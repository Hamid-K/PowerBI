using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Exceptions;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Learning.PowerQueryM
{
	// Token: 0x02000B93 RID: 2963
	public class PowerQueryMLearner : TargetLearner
	{
		// Token: 0x06004B52 RID: 19282 RVA: 0x000ED328 File Offset: 0x000EB528
		protected override TargetCode LearnImpl(IReadOnlyList<ParsedJson> jsons, SynthesisOptions options)
		{
			List<JToken> list = new List<JToken>();
			foreach (ParsedJson parsedJson in jsons)
			{
				if ((parsedJson.Errors & JsonErrors.Truncated & JsonErrors.MismatchedBrackets & JsonErrors.HasComment) != JsonErrors.None)
				{
					throw new InvalidInputException(FormattableString.Invariant(FormattableStringFactory.Create("Cannot generate PowerQuery M code for invalid JSON", Array.Empty<object>())));
				}
				list.AddRange(parsedJson.Regions.Select((JsonRegion r) => r.Token));
			}
			bool flag = jsons.Any((ParsedJson js) => js.IsDelimitedJson);
			this._builder = new PowerQueryMJsonCodeBuilder(TargetLearner.FileObjectId, flag, options.LocalizedPowerQueryMStrings, options.EscapePowerQueryM, options.ForbiddenMStepNames, options.SourceMStepName);
			Optional<JTokenType> optional = PowerQueryMLearner.SingleKindOrNone(list);
			if (!optional.HasValue)
			{
				return null;
			}
			HashSet<JArray> hashSet = AutoLearner.FindJoiningArrays(list);
			if (flag)
			{
				this.Explore(list, new string[] { "Column1" }, hashSet);
			}
			else
			{
				JTokenType value = optional.Value;
				if (value != JTokenType.Object)
				{
					if (value != JTokenType.Array)
					{
						return null;
					}
					this._builder.AddStep(MTableFunctionName.FromList, new string[] { "Splitter.SplitByNothing()", "null", "null", "ExtraValues.Error" }, null);
					List<JArray> list2 = list.Cast<JArray>().ToList<JArray>();
					hashSet.ExceptWith(list2);
					List<JToken> list3 = list2.SelectMany((JArray array) => array.Children()).ToList<JToken>();
					this.Explore(list3, new string[] { "Column1" }, hashSet);
				}
				else
				{
					IReadOnlyList<JObject> readOnlyList = list.Cast<JObject>().ToList<JObject>();
					IEnumerable<JProperty> enumerable = readOnlyList.OnlyOrDefault<JObject>().Properties();
					bool flag2;
					if (AutoLearner.HaveSameKeys(enumerable, out flag2))
					{
						IReadOnlyList<JObject> readOnlyList2 = enumerable.Select((JProperty prop) => (JObject)prop.Value).ToList<JObject>();
						if (flag2)
						{
							this._builder.AddStep("Record.ToList(" + this._builder.EscapedLastStepName + ")", options.LocalizedPowerQueryMStrings.ConvertedToList, false);
							this._builder.AddStep(MTableFunctionName.FromRecords, new string[0], null);
							this.ExploreObject(readOnlyList2, new string[0], hashSet, false);
						}
						else
						{
							this._builder.AddStep("Record.ToTable(" + this._builder.EscapedLastStepName + ")", options.LocalizedPowerQueryMStrings.ConvertedToTable, false);
							this.ExploreObject(readOnlyList2, new string[] { "Value" }, hashSet, true);
						}
					}
					else
					{
						this._builder.AddConvertListStep(MTableFunctionName.FromRecords);
						this.ExploreObject(readOnlyList, new string[0], hashSet, false);
					}
				}
			}
			return new TargetCode(null, this._builder.GetCode());
		}

		// Token: 0x06004B53 RID: 19283 RVA: 0x000ED63C File Offset: 0x000EB83C
		private void Explore(IReadOnlyList<JToken> tokens, IReadOnlyList<string> path, HashSet<JArray> joiningArrays)
		{
			Optional<JTokenType> optional = PowerQueryMLearner.SingleKindOrNone(tokens);
			if (!optional.HasValue)
			{
				return;
			}
			JTokenType value = optional.Value;
			if (value == JTokenType.Object)
			{
				List<JObject> list = tokens.Cast<JObject>().ToList<JObject>();
				this.ExploreObject(list, path, joiningArrays, true);
				return;
			}
			if (value == JTokenType.Array)
			{
				List<JArray> list2 = tokens.Cast<JArray>().ToList<JArray>();
				this.ExploreArray(list2, path, joiningArrays);
				return;
			}
		}

		// Token: 0x06004B54 RID: 19284 RVA: 0x000ED698 File Offset: 0x000EB898
		private void ExploreArray(IReadOnlyList<JArray> arrays, IReadOnlyList<string> path, HashSet<JArray> joiningArrays)
		{
			if (AutoLearner.CanSplitArray(arrays))
			{
				List<string> list = (from idx in Enumerable.Range(0, arrays.Max((JArray e) => e.Count))
					select this.GetColumnName(path.AppendItem(idx.ToString()).ToList<string>())).ToList<string>();
				this._builder.AddStep(MTableFunctionName.SplitColumn, new string[]
				{
					this.GetColumnName(path),
					"each _",
					this.GetCommaSepBracketList(list)
				}, null);
			}
			if (arrays.Any(new Func<JArray, bool>(joiningArrays.Contains)))
			{
				this._builder.AddStep(MTableFunctionName.ExpandListColumn, new string[] { this.GetColumnName(path) }, this._builder.GetExpandedStep(path));
				List<JToken> list2 = arrays.SelectMany((JArray array) => array.Children()).ToList<JToken>();
				this.Explore(list2, path, joiningArrays);
			}
		}

		// Token: 0x06004B55 RID: 19285 RVA: 0x000ED7C0 File Offset: 0x000EB9C0
		private void ExploreObject(IReadOnlyList<JObject> objects, IReadOnlyList<string> path, HashSet<JArray> joiningArrays, bool flattenCurrentObject = true)
		{
			OrderedDictionary orderedDictionary = AutoLearner.GroupPropertiesByKey(objects);
			if (flattenCurrentObject)
			{
				string columnName = this.GetColumnName(path);
				string expandedStep = this._builder.GetExpandedStep(path);
				List<string> list = orderedDictionary.Keys.Cast<string>().ToList<string>();
				List<string> list2 = list.Select((string f) => this._builder.EscapeString(f)).ToList<string>();
				if (path.Count == 1 && path[0] == "Column1")
				{
					path = new string[0];
				}
				List<string> list3 = list.Select((string f) => this.GetColumnName(path.AppendItem(f).ToList<string>())).ToList<string>();
				this._builder.AddStep(MTableFunctionName.ExpandRecordColumn, new string[]
				{
					columnName,
					this.GetCommaSepBracketList(list2),
					this.GetCommaSepBracketList(list3)
				}, expandedStep);
			}
			foreach (object obj in orderedDictionary)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (string)dictionaryEntry.Key;
				List<JToken> list4 = ((List<JProperty>)dictionaryEntry.Value).Select((JProperty v) => v.Value).ToList<JToken>();
				this.Explore(list4, path.AppendItem(text).ToList<string>(), joiningArrays);
			}
		}

		// Token: 0x06004B56 RID: 19286 RVA: 0x000ED95C File Offset: 0x000EBB5C
		private string GetColumnName(IReadOnlyList<string> p)
		{
			return this._builder.EscapeString(string.Join(".", p));
		}

		// Token: 0x06004B57 RID: 19287 RVA: 0x000ED974 File Offset: 0x000EBB74
		private string GetCommaSepBracketList(IReadOnlyList<string> names)
		{
			return "{" + string.Join(", ", names) + "}";
		}

		// Token: 0x06004B58 RID: 19288 RVA: 0x000ED990 File Offset: 0x000EBB90
		private static Optional<JTokenType> SingleKindOrNone(IEnumerable<JToken> tokens)
		{
			Optional<JTokenType> optional = Optional<JTokenType>.Nothing;
			foreach (JToken jtoken in tokens)
			{
				if (!optional.HasValue)
				{
					optional = jtoken.Type.Some<JTokenType>();
				}
				else if (optional.Value != jtoken.Type)
				{
					return Optional<JTokenType>.Nothing;
				}
			}
			return optional;
		}

		// Token: 0x040021EA RID: 8682
		private const string FirstColumnName = "Column1";

		// Token: 0x040021EB RID: 8683
		private PowerQueryMJsonCodeBuilder _builder;
	}
}

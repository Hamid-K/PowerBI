using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Exceptions;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Microsoft.ProgramSynthesis.Wrangling.Json.JpathStep;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Learning.Python
{
	// Token: 0x02000B8F RID: 2959
	public abstract class PythonLearner : TargetLearner
	{
		// Token: 0x06004B34 RID: 19252 RVA: 0x000ECC3C File Offset: 0x000EAE3C
		protected override TargetCode LearnImpl(IReadOnlyList<ParsedJson> jsons, SynthesisOptions options)
		{
			List<JToken> list = new List<JToken>();
			foreach (ParsedJson parsedJson in jsons)
			{
				if (!options.HandleInvalidJson && parsedJson.Errors != JsonErrors.None)
				{
					throw new InvalidInputException(FormattableString.Invariant(FormattableStringFactory.Create("Cannot generate code for invalid JSON", Array.Empty<object>())));
				}
				this.Errors = parsedJson.Errors;
				this.IsLineJson = parsedJson.IsDelimitedJson;
				if (!string.IsNullOrEmpty(parsedJson.StartDelimiter))
				{
					this.PrefixLength = parsedJson.StartDelimiter.Length;
				}
				if (!string.IsNullOrEmpty(parsedJson.EndDelimiter))
				{
					this.SuffixLength = parsedJson.EndDelimiter.Length;
				}
				list.AddRange(parsedJson.Regions.Select((JsonRegion r) => r.Token));
			}
			this.Explore(list);
			string text = this.GenerateSnippet();
			return new TargetCode(this.GenerateImport(), text);
		}

		// Token: 0x06004B35 RID: 19253 RVA: 0x000ECD54 File Offset: 0x000EAF54
		private string GenerateImport()
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			foreach (string text in this._importList.OrderBy((string v) => v))
			{
				codeBuilder.AppendLine(text);
			}
			foreach (KeyValuePair<string, HashSet<string>> keyValuePair in this._fromImportDictionary.OrderBy((KeyValuePair<string, HashSet<string>> kvp) => kvp.Key))
			{
				CodeBuilder codeBuilder2 = codeBuilder;
				string text2 = "from {0} import {1}";
				object[] array = new object[2];
				array[0] = keyValuePair.Key;
				array[1] = string.Join(", ", keyValuePair.Value.OrderBy((string v) => v));
				codeBuilder2.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create(text2, array)));
			}
			codeBuilder.AppendLine();
			codeBuilder.AppendLine();
			return codeBuilder.GetCode();
		}

		// Token: 0x06004B36 RID: 19254
		protected abstract void Explore(IReadOnlyList<JToken> tokens);

		// Token: 0x06004B37 RID: 19255
		protected abstract string GenerateSnippet();

		// Token: 0x06004B38 RID: 19256 RVA: 0x000ECE98 File Offset: 0x000EB098
		protected void Explore(IReadOnlyList<JToken> tokens, HashSet<JArray> joiningArrays, IReadOnlyList<string> path, IReadOnlyList<int> joiningIndices)
		{
			JTokenType[] array = tokens.Select((JToken n) => n.Type).Distinct<JTokenType>().ToArray<JTokenType>();
			if (array.Length > 1)
			{
				this.AddField(joiningArrays, path, joiningIndices);
				return;
			}
			JTokenType jtokenType = array[0];
			if (jtokenType != JTokenType.Object)
			{
				if (jtokenType != JTokenType.Array)
				{
					this.AddField(joiningArrays, path, joiningIndices);
					return;
				}
			}
			else
			{
				List<JObject> list = tokens.Cast<JObject>().ToList<JObject>();
				bool flag;
				if (list.Count == 1 && AutoLearner.HaveSameKeys(list[0].Properties(), out flag))
				{
					if (!flag)
					{
						this.AddField(joiningArrays, path.Concat(new string[]
						{
							new StarStep().ToString(),
							new PropertyKeyStep().ToString()
						}).ToList<string>(), joiningIndices);
					}
					this.Explore((from property in list[0].Properties()
						select property.Value).Cast<JObject>().ToList<JObject>(), joiningArrays, path.Concat(new string[]
					{
						new StarStep().ToString(),
						new PropertyValueStep().ToString()
					}).ToList<string>(), joiningIndices);
					return;
				}
				using (List<string>.Enumerator enumerator = list.SelectMany((JObject t) => from p in t.Properties()
					select p.Name).Distinct<string>().ToList<string>()
					.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						List<JToken> list2 = new List<JToken>();
						using (List<JObject>.Enumerator enumerator2 = list.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								JToken jtoken;
								if (enumerator2.Current.TryGetValue(text, out jtoken))
								{
									list2.Add(jtoken);
								}
							}
						}
						this.Explore(list2, joiningArrays, path.AppendItem(text).ToList<string>(), joiningIndices);
					}
					return;
				}
			}
			List<JArray> list3 = tokens.Cast<JArray>().ToList<JArray>();
			if (AutoLearner.CanSplitArray(list3))
			{
				this.AddSplitArray(list3, path, joiningIndices);
				return;
			}
			if (joiningArrays.Count == 0)
			{
				this.AddField(joiningArrays, path, joiningIndices);
				return;
			}
			this.ExploreArray(list3, joiningArrays, path, joiningIndices);
		}

		// Token: 0x06004B39 RID: 19257
		protected abstract void AddField(HashSet<JArray> joiningArrays, IReadOnlyList<string> path, IReadOnlyList<int> joiningIndices);

		// Token: 0x06004B3A RID: 19258
		protected abstract void AddSplitArray(IReadOnlyList<JArray> arrays, IReadOnlyList<string> path, IReadOnlyList<int> joiningIndices);

		// Token: 0x06004B3B RID: 19259
		protected abstract void ExploreArray(List<JArray> arrays, HashSet<JArray> joiningArrays, IReadOnlyList<string> path, IReadOnlyList<int> joiningIndices);

		// Token: 0x06004B3C RID: 19260 RVA: 0x000ED0F0 File Offset: 0x000EB2F0
		protected string GenerateSliceCode(bool stripTrailingNewLine = false)
		{
			string text = (stripTrailingNewLine ? ".rstrip('\\r\\n')" : string.Empty);
			if (this.PrefixLength > 0 && this.SuffixLength > 0)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}[{1}:-{2}]", new object[] { text, this.PrefixLength, this.SuffixLength }));
			}
			if (this.PrefixLength > 0)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}[{1}:]", new object[] { text, this.PrefixLength }));
			}
			if (this.SuffixLength > 0)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("{0}[:-{1}]", new object[] { text, this.SuffixLength }));
			}
			return string.Empty;
		}

		// Token: 0x06004B3D RID: 19261 RVA: 0x000ED1BC File Offset: 0x000EB3BC
		protected void AddFromImport(string from, string import)
		{
			this._fromImportDictionary.AddOrInsert(from, import);
		}

		// Token: 0x06004B3E RID: 19262 RVA: 0x000ED1CB File Offset: 0x000EB3CB
		protected void AddImport(string import)
		{
			this._importList.Add(import);
		}

		// Token: 0x040021DE RID: 8670
		private readonly Dictionary<string, HashSet<string>> _fromImportDictionary = new Dictionary<string, HashSet<string>>();

		// Token: 0x040021DF RID: 8671
		private readonly HashSet<string> _importList = new HashSet<string>();
	}
}

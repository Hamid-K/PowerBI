using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Learning.Python
{
	// Token: 0x02000B88 RID: 2952
	public class PandasLearner : PythonLearner
	{
		// Token: 0x06004AF4 RID: 19188 RVA: 0x000EB8D0 File Offset: 0x000E9AD0
		protected override void Explore(IReadOnlyList<JToken> tokens)
		{
			IReadOnlyList<JToken> readOnlyList = tokens;
			List<string> list = new List<string>();
			bool flag = readOnlyList.Count == tokens.Count;
			while (flag)
			{
				if (readOnlyList.All((JToken t) => t is JArray))
				{
					break;
				}
				List<JObject> list2 = readOnlyList.OfType<JObject>().ToList<JObject>();
				if (list2.Count != readOnlyList.Count)
				{
					flag = false;
					break;
				}
				string text = null;
				List<JToken> list3 = new List<JToken>();
				foreach (JObject jobject in list2)
				{
					if (jobject.Count != 1)
					{
						flag = false;
						break;
					}
					JProperty jproperty = jobject.Properties().OnlyOrDefault<JProperty>();
					if (text == null)
					{
						text = jproperty.Name;
					}
					else if (text != jproperty.Name)
					{
						flag = false;
						break;
					}
					list3.Add(jproperty.Value);
				}
				if (!flag || text == null)
				{
					break;
				}
				list.Add(text);
				readOnlyList = list3;
			}
			if (flag)
			{
				this._pathPrefix = list;
				tokens = readOnlyList;
				if (tokens.All((JToken t) => t is JArray))
				{
					this._isArrays = true;
					List<JArray> list4 = tokens.Cast<JArray>().ToList<JArray>();
					if (list4.All(delegate(JArray array)
					{
						if (array.Count > 0)
						{
							return array.Any((JToken e) => e is JValue);
						}
						return false;
					}))
					{
						this._isArrayOfValues = true;
					}
					else if (list4.All(delegate(JArray array)
					{
						if (array.Count > 0)
						{
							return array.All((JToken e) => e is JArray);
						}
						return false;
					}))
					{
						this._isArrayOfArrays = true;
					}
				}
			}
			HashSet<JArray> hashSet = AutoLearner.FindJoiningArrays(tokens);
			if (hashSet.Count > 0)
			{
				base.Explore(tokens, hashSet, new List<string>(), new List<int>());
			}
		}

		// Token: 0x06004AF5 RID: 19189 RVA: 0x000EBAC4 File Offset: 0x000E9CC4
		protected override void ExploreArray(List<JArray> arrays, HashSet<JArray> joiningArrays, IReadOnlyList<string> path, IReadOnlyList<int> joiningIndices)
		{
			if (arrays.Any(new Func<JArray, bool>(joiningArrays.Contains)))
			{
				joiningArrays = joiningArrays.Except(arrays).ConvertToHashSet<JArray>();
				if (joiningArrays.Count == 0)
				{
					bool flag = arrays.Any((JArray array) => array.Any(delegate(JToken e)
					{
						JObject jobject = e as JObject;
						if (jobject != null)
						{
							return jobject.PropertyValues().Any((JToken value) => value is JObject);
						}
						return false;
					}));
					this.SetArray(path, flag);
					path = new List<string>();
				}
			}
			List<JToken> list = arrays.SelectMany((JArray a) => a).ToList<JToken>();
			if (list.Count > 0)
			{
				base.Explore(list, joiningArrays, path, joiningIndices);
			}
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x000EBB70 File Offset: 0x000E9D70
		protected override void AddField(HashSet<JArray> joiningArrays, IReadOnlyList<string> path, IReadOnlyList<int> explodeIndices)
		{
			if (joiningArrays.Count > 0)
			{
				this._fieldPaths.Add(PandasLearner.ToPandasPath(path));
			}
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x000EBB8C File Offset: 0x000E9D8C
		private void SetArray(IReadOnlyList<string> path, bool hasNestedObjectChildren)
		{
			if (path.Count == 0)
			{
				return;
			}
			this._arrayColumn = PandasLearner.ToPrefix(path);
			this._recordPathCode = FormattableString.Invariant(FormattableStringFactory.Create("record_path={0},", new object[] { PandasLearner.ToPandasPath(path) }));
			this._recordPrefixCode = FormattableString.Invariant(FormattableStringFactory.Create("record_prefix={0}", new object[] { (this._arrayColumn + ".").ToLiteral(null) }));
			this._requireSecondNormalize = hasNestedObjectChildren;
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x000EBC0D File Offset: 0x000E9E0D
		protected override void AddSplitArray(IReadOnlyList<JArray> arrays, IReadOnlyList<string> path, IReadOnlyList<int> explodeIndices)
		{
			if (path.Count > 0)
			{
				this._splitArrayColumns.Add(PandasLearner.ToPrefix(path));
			}
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x000EBC2C File Offset: 0x000E9E2C
		protected override string GenerateSnippet()
		{
			base.AddImport("import json");
			if (this.Errors != JsonErrors.None)
			{
				base.AddImport("import collections");
				base.AddImport("import prose_semantics as ps");
			}
			base.AddFromImport("pandas.io.json", "json_normalize");
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			if (this.IsLineJson)
			{
				string text = "line" + base.GenerateSliceCode(true);
				string text2 = this.GenerateJsonLoad(text, false);
				codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("d = [{0} for line in {1} if line.strip()]", new object[]
				{
					text2,
					TargetLearner.FileObjectId
				})));
				if (this._isArrays)
				{
					base.AddImport("import pandas");
					codeBuilder.AppendLine("df = pandas.DataFrame(d)");
				}
				else
				{
					this.GenerateNormalizeCode(codeBuilder);
				}
			}
			else if (this._isArrayOfValues || this._isArrayOfArrays)
			{
				base.AddImport("import pandas");
				codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("d = {0}", new object[] { this.GenerateJsonLoad(TargetLearner.FileObjectId, true) })));
				codeBuilder.Append(FormattableString.Invariant(FormattableStringFactory.Create("df = pandas.DataFrame(data=d{0}){1}", new object[]
				{
					this.GenerateTopArraySelect(),
					this.GeneratePathPrefix()
				})));
			}
			else
			{
				if (this.PrefixLength == 0 && this.SuffixLength == 0)
				{
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("d = {0}", new object[] { this.GenerateJsonLoad("f", true) })));
				}
				else
				{
					string text3 = "f.read()" + base.GenerateSliceCode(false);
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("d = {0}", new object[] { this.GenerateJsonLoad(text3, false) })));
				}
				this.GenerateNormalizeCode(codeBuilder);
			}
			return codeBuilder.GetCode();
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x000EBDE8 File Offset: 0x000E9FE8
		private string GenerateJsonLoad(string variable, bool loadFile = true)
		{
			string text = (loadFile ? string.Empty : "s");
			if (this.Errors == JsonErrors.None)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("json.load{0}({1})", new object[] { text, variable }));
			}
			return FormattableString.Invariant(FormattableStringFactory.Create("ps.repair_load{0}({1}, collections.OrderedDict, True)", new object[] { text, variable }));
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x000EBE48 File Offset: 0x000EA048
		private string GenerateTopArraySelect()
		{
			if (this._pathPrefix.Count != 0)
			{
				return string.Join(string.Empty, this._pathPrefix.Select((string p) => FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[] { p.ToLiteral(null) }))));
			}
			return string.Empty;
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x000EBE9C File Offset: 0x000EA09C
		private string GeneratePathPrefix()
		{
			if (this._pathPrefix.Count != 0)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create(".add_prefix({0})", new object[] { (PandasLearner.ToPrefix(this._pathPrefix) + ".").ToLiteral(null) }));
			}
			return string.Empty;
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x000EBEF0 File Offset: 0x000EA0F0
		private void GenerateNormalizeCode(CodeBuilder code)
		{
			string text = FormattableString.Invariant(FormattableStringFactory.Create("d{0}", new object[] { this.GenerateTopArraySelect() }));
			string text2 = this.GeneratePathPrefix();
			string text3 = (string.IsNullOrEmpty(text2) ? null : FormattableString.Invariant(FormattableStringFactory.Create("df = df{0}", new object[] { text2 })));
			if (this._recordPathCode == null)
			{
				code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("df = json_normalize({0})", new object[] { text })));
				this.GenerateSplitArrayCode(code);
				if (!string.IsNullOrEmpty(text3))
				{
					code.AppendLine(text3);
				}
				return;
			}
			using (code.NewScope("df = json_normalize(", 1U))
			{
				code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0},", new object[] { text })));
				code.AppendLine(this._recordPathCode);
				if (this._fieldPaths.Count == 1)
				{
					code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("meta={0},", new object[] { this._fieldPaths.Single<string>() })));
				}
				else if (this._fieldPaths.Count > 1)
				{
					using (code.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("meta=[", Array.Empty<object>())), 1U))
					{
						for (int i = 0; i < this._fieldPaths.Count; i++)
						{
							code.AppendLine(this._fieldPaths[i] + ",");
						}
					}
					code.AppendLine("],");
				}
				code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0},", new object[] { this._recordPrefixCode })));
			}
			code.AppendLine(")");
			if (this._requireSecondNormalize)
			{
				code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("# flatten objects in \"{0}\"", new object[] { this._arrayColumn })));
				code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("df = json_normalize(df.to_dict(\"r\"))", Array.Empty<object>())));
			}
			this.GenerateSplitArrayCode(code);
			if (!string.IsNullOrEmpty(text3))
			{
				code.AppendLine(text3);
			}
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x000EC140 File Offset: 0x000EA340
		private void GenerateSplitArrayCode(CodeBuilder code)
		{
			if (this._splitArrayColumns.Count <= 0)
			{
				return;
			}
			using (code.NewScope("df = (", 1U))
			{
				code.Append("df");
				foreach (string text in this._splitArrayColumns)
				{
					string text2 = (string.IsNullOrEmpty(this._recordPrefixCode) ? text : (this._arrayColumn + "." + text));
					string text3 = (text2 + ".").ToLiteral(null);
					text2 = text2.ToLiteral(null);
					code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create(".drop({0}, 1)", new object[] { text2 })));
					using (code.NewScope(FormattableString.Invariant(FormattableStringFactory.Create(".join(", Array.Empty<object>())), 1U))
					{
						code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("df[{0}]", new object[] { text2 })));
						base.AddImport("import pandas as pd");
						code.AppendLine(".apply(lambda t: pd.Series(t))");
						code.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create(".add_prefix({0})", new object[] { text3 })));
					}
					code.AppendLine(")");
				}
			}
			code.AppendLine(")");
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x000EC2F4 File Offset: 0x000EA4F4
		private static string ToPandasPath(IEnumerable<string> path)
		{
			List<string> list = path.Select((string s) => s.ToLiteral(null)).ToList<string>();
			if (list.Count != 1)
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("[{0}]", new object[] { string.Join(", ", list) }));
			}
			return list.Single<string>();
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x000EC35F File Offset: 0x000EA55F
		private static string ToPrefix(IEnumerable<string> path)
		{
			return string.Join(".", path);
		}

		// Token: 0x040021B4 RID: 8628
		private readonly List<string> _fieldPaths = new List<string>();

		// Token: 0x040021B5 RID: 8629
		private readonly List<string> _splitArrayColumns = new List<string>();

		// Token: 0x040021B6 RID: 8630
		private string _arrayColumn;

		// Token: 0x040021B7 RID: 8631
		private bool _isArrays;

		// Token: 0x040021B8 RID: 8632
		private bool _isArrayOfArrays;

		// Token: 0x040021B9 RID: 8633
		private bool _isArrayOfValues;

		// Token: 0x040021BA RID: 8634
		private List<string> _pathPrefix = new List<string>();

		// Token: 0x040021BB RID: 8635
		private string _recordPathCode;

		// Token: 0x040021BC RID: 8636
		private string _recordPrefixCode;

		// Token: 0x040021BD RID: 8637
		private bool _requireSecondNormalize;
	}
}

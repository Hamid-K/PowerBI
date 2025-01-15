using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Wrangling.Json;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Learning.Python
{
	// Token: 0x02000B8A RID: 2954
	public class PySparkLearner : PythonLearner
	{
		// Token: 0x06004B10 RID: 19216 RVA: 0x000EC4A0 File Offset: 0x000EA6A0
		protected override TargetCode LearnImpl(IReadOnlyList<ParsedJson> jsons, SynthesisOptions options)
		{
			SynthesisOptions synthesisOptions = new SynthesisOptions(options)
			{
				HandleInvalidJson = false
			};
			return base.LearnImpl(jsons, synthesisOptions);
		}

		// Token: 0x06004B11 RID: 19217 RVA: 0x000EC4C4 File Offset: 0x000EA6C4
		protected override void Explore(IReadOnlyList<JToken> tokens)
		{
			HashSet<JArray> hashSet = AutoLearner.FindJoiningArrays(tokens);
			this._dfSelectList.NewSelect();
			base.Explore(tokens, hashSet, new List<string>(), new List<int>());
			while (this._dfSelectList.Context.Count > 0)
			{
				List<JArray> list;
				HashSet<JArray> hashSet2;
				IReadOnlyList<string> readOnlyList;
				this._dfSelectList.Context.Dequeue().Deconstruct(out list, out hashSet2, out readOnlyList);
				this._dfSelectList.NewSelect();
				this.ExploreArray(list, hashSet2, readOnlyList, new List<int>());
			}
		}

		// Token: 0x06004B12 RID: 19218 RVA: 0x000EC540 File Offset: 0x000EA740
		protected override void ExploreArray(List<JArray> arrays, HashSet<JArray> joiningArrays, IReadOnlyList<string> path, IReadOnlyList<int> explodeIndices)
		{
			if (this._dfSelectList.HasExplode)
			{
				this._dfSelectList.AddCol(PySparkLearner.ToExplodePathString(path, explodeIndices), PySparkLearner.ToAliasPathString(path), true);
				List<string> list = new List<string> { string.Join(PySparkLearner.Separator, path) };
				this._dfSelectList.Context.Enqueue(new Record<List<JArray>, HashSet<JArray>, IReadOnlyList<string>>(arrays, joiningArrays, list));
				return;
			}
			if (path.Count > 0)
			{
				base.AddFromImport("pyspark.sql.functions", "explode");
				string text = PySparkLearner.ToColPathString(path);
				if (arrays.All((JArray array) => array.All((JToken e) => e is JValue)))
				{
					this._dfSelectList.AddExplode(text, PySparkLearner.ToAliasPathString(path));
					return;
				}
				explodeIndices = explodeIndices.AppendItem(path.Count - 1).ToList<int>();
				string text2 = PySparkLearner.ToExplodePathString(path, explodeIndices);
				this._dfSelectList.AddExplode(text, text2);
				this._dfSelectList.AddDrop(text2);
			}
			List<JToken> list2 = arrays.SelectMany((JArray a) => a).ToList<JToken>();
			if (list2.Count > 0)
			{
				joiningArrays = joiningArrays.Except(arrays).ConvertToHashSet<JArray>();
				base.Explore(list2, joiningArrays, path, explodeIndices);
			}
		}

		// Token: 0x06004B13 RID: 19219 RVA: 0x000EC688 File Offset: 0x000EA888
		protected override void AddField(HashSet<JArray> joiningArrays, IReadOnlyList<string> path, IReadOnlyList<int> explodeIndices)
		{
			if (path.Count > 0)
			{
				string text = PySparkLearner.ToExplodePathString(path, explodeIndices);
				string text2 = PySparkLearner.ToAliasPathString(path);
				this._dfSelectList.AddCol(text, text2, false);
			}
		}

		// Token: 0x06004B14 RID: 19220 RVA: 0x000EC6BC File Offset: 0x000EA8BC
		protected override void AddSplitArray(IReadOnlyList<JArray> arrays, IReadOnlyList<string> path, IReadOnlyList<int> explodeIndices)
		{
			int count = arrays.First<JArray>().Count;
			string text = PySparkLearner.ToExplodePathString(path, explodeIndices);
			for (int i = 0; i < count; i++)
			{
				this._dfSelectList.AddCol(text, i, PySparkLearner.ToAliasPathString(path.AppendItem(i.ToString())));
			}
		}

		// Token: 0x06004B15 RID: 19221 RVA: 0x000EC708 File Offset: 0x000EA908
		private static string ToAliasPathString(IEnumerable<string> path)
		{
			return string.Join(PySparkLearner.Separator, path).ToLiteral(null);
		}

		// Token: 0x06004B16 RID: 19222 RVA: 0x000EC71B File Offset: 0x000EA91B
		private static string ToColPathString(IEnumerable<string> path)
		{
			return string.Join(".", path).ToLiteral(null);
		}

		// Token: 0x06004B17 RID: 19223 RVA: 0x000EC730 File Offset: 0x000EA930
		private static string ToExplodePathString(IEnumerable<string> path, IReadOnlyList<int> explodeIndices)
		{
			if (explodeIndices.Count == 0)
			{
				return PySparkLearner.ToColPathString(path);
			}
			List<string> list = new List<string>(path);
			foreach (int num in explodeIndices)
			{
				list[num] += PySparkLearner.ExplodeSuffix;
			}
			string text = string.Join(PySparkLearner.Separator, list.Take(explodeIndices.Last<int>() + 1));
			return PySparkLearner.ToColPathString(list.Skip(explodeIndices.Last<int>() + 1).PrependItem(text));
		}

		// Token: 0x06004B18 RID: 19224 RVA: 0x000EC7D4 File Offset: 0x000EA9D4
		protected override string GenerateSnippet()
		{
			base.AddFromImport("pyspark.sql", "SparkSession");
			base.AddFromImport("pyspark.sql.functions", "col");
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			List<PySparkLearner.DataFrameSelect> selectDropList = this._dfSelectList.SelectDropList;
			for (int i = 0; i < selectDropList.Count; i++)
			{
				List<string> list = selectDropList.Take(i).SelectMany((PySparkLearner.DataFrameSelect select) => from col in @select.ColExplodeList
					where !col.IsTemporary
					select FormattableString.Invariant(FormattableStringFactory.Create("col({0})", new object[] { col.Alias }))).Concat(selectDropList[i].ColExplodeList.Select((PySparkLearner.ColExplode col) => col.Code))
					.ToList<string>();
				string text = "df = df.select(";
				if (list.Count == 1)
				{
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0}{1})", new object[]
					{
						text,
						list.Single<string>()
					})));
				}
				else if (list.Count > 1)
				{
					using (codeBuilder.NewScope(FormattableString.Invariant(FormattableStringFactory.Create("{0}", new object[] { text })), 1U))
					{
						foreach (string text2 in list)
						{
							codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("{0},", new object[] { text2 })));
						}
					}
					codeBuilder.AppendLine(")");
				}
				if (i == selectDropList.Count - 1 && selectDropList[i].Drop != null)
				{
					codeBuilder.AppendLine(FormattableString.Invariant(FormattableStringFactory.Create("df = df.drop({0})", new object[] { selectDropList[i].Drop })));
				}
			}
			return codeBuilder.GetCode();
		}

		// Token: 0x040021CB RID: 8651
		private static readonly string Separator = "_";

		// Token: 0x040021CC RID: 8652
		private static readonly string ExplodeSuffix = "_explode";

		// Token: 0x040021CD RID: 8653
		private readonly PySparkLearner.DataFrameSelectList _dfSelectList = new PySparkLearner.DataFrameSelectList();

		// Token: 0x02000B8B RID: 2955
		private class DataFrameSelect
		{
			// Token: 0x17000D70 RID: 3440
			// (get) Token: 0x06004B1B RID: 19227 RVA: 0x000EC9F1 File Offset: 0x000EABF1
			// (set) Token: 0x06004B1C RID: 19228 RVA: 0x000EC9F9 File Offset: 0x000EABF9
			public string Drop { get; internal set; }

			// Token: 0x17000D71 RID: 3441
			// (get) Token: 0x06004B1D RID: 19229 RVA: 0x000ECA02 File Offset: 0x000EAC02
			public List<PySparkLearner.ColExplode> ColExplodeList { get; } = new List<PySparkLearner.ColExplode>();
		}

		// Token: 0x02000B8C RID: 2956
		private struct ColExplode
		{
			// Token: 0x06004B1F RID: 19231 RVA: 0x000ECA1D File Offset: 0x000EAC1D
			public ColExplode(string code, string alias, bool isTemporary)
			{
				this.Code = code;
				this.Alias = alias;
				this.IsTemporary = isTemporary;
			}

			// Token: 0x040021D0 RID: 8656
			public readonly string Code;

			// Token: 0x040021D1 RID: 8657
			public readonly string Alias;

			// Token: 0x040021D2 RID: 8658
			public readonly bool IsTemporary;
		}

		// Token: 0x02000B8D RID: 2957
		private class DataFrameSelectList
		{
			// Token: 0x17000D72 RID: 3442
			// (get) Token: 0x06004B20 RID: 19232 RVA: 0x000ECA34 File Offset: 0x000EAC34
			public List<PySparkLearner.DataFrameSelect> SelectDropList { get; } = new List<PySparkLearner.DataFrameSelect>();

			// Token: 0x17000D73 RID: 3443
			// (get) Token: 0x06004B21 RID: 19233 RVA: 0x000ECA3C File Offset: 0x000EAC3C
			public Queue<Record<List<JArray>, HashSet<JArray>, IReadOnlyList<string>>> Context { get; } = new Queue<Record<List<JArray>, HashSet<JArray>, IReadOnlyList<string>>>();

			// Token: 0x17000D74 RID: 3444
			// (get) Token: 0x06004B22 RID: 19234 RVA: 0x000ECA44 File Offset: 0x000EAC44
			// (set) Token: 0x06004B23 RID: 19235 RVA: 0x000ECA4C File Offset: 0x000EAC4C
			public bool HasExplode { get; private set; }

			// Token: 0x06004B24 RID: 19236 RVA: 0x000ECA55 File Offset: 0x000EAC55
			public void NewSelect()
			{
				this.SelectDropList.Add(new PySparkLearner.DataFrameSelect());
				this.HasExplode = false;
			}

			// Token: 0x06004B25 RID: 19237 RVA: 0x000ECA70 File Offset: 0x000EAC70
			public void AddCol(string col, string alias, bool isTemporary = false)
			{
				string text = ((col == alias) ? FormattableString.Invariant(FormattableStringFactory.Create("col({0})", new object[] { col })) : FormattableString.Invariant(FormattableStringFactory.Create("col({0}).alias({1})", new object[] { col, alias })));
				this.AddColExplode(text, alias, isTemporary);
			}

			// Token: 0x06004B26 RID: 19238 RVA: 0x000ECAC8 File Offset: 0x000EACC8
			public void AddCol(string col, int i, string alias)
			{
				this.AddColExplode(FormattableString.Invariant(FormattableStringFactory.Create("col({0})[{1}].alias({2})", new object[] { col, i, alias })), alias, false);
			}

			// Token: 0x06004B27 RID: 19239 RVA: 0x000ECAF8 File Offset: 0x000EACF8
			public void AddExplode(string col, string alias)
			{
				this.AddColExplode(FormattableString.Invariant(FormattableStringFactory.Create("explode({0}).alias({1})", new object[] { col, alias })), alias, true);
				this.HasExplode = true;
			}

			// Token: 0x06004B28 RID: 19240 RVA: 0x000ECB26 File Offset: 0x000EAD26
			private void AddColExplode(string colAlias, string alias, bool isTemporary = false)
			{
				this.SelectDropList.Last<PySparkLearner.DataFrameSelect>().ColExplodeList.Add(new PySparkLearner.ColExplode(colAlias, alias, isTemporary));
			}

			// Token: 0x06004B29 RID: 19241 RVA: 0x000ECB45 File Offset: 0x000EAD45
			public void AddDrop(string drop)
			{
				this.SelectDropList.Last<PySparkLearner.DataFrameSelect>().Drop = drop;
			}
		}
	}
}

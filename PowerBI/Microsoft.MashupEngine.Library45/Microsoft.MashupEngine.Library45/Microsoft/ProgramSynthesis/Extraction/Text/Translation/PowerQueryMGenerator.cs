using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Learning;
using Microsoft.ProgramSynthesis.Translation.PowerQuery;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Translation
{
	// Token: 0x02000F9C RID: 3996
	internal class PowerQueryMGenerator
	{
		// Token: 0x06006E85 RID: 28293 RVA: 0x00168C84 File Offset: 0x00166E84
		private PowerQueryMGenerator(ProgramNode node, string binaryContent, string encoding, ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escapeIdentifiers)
		{
			CodeFragmentCollector codeFragmentCollector = new CodeFragmentCollector();
			node.AcceptVisitor<object>(codeFragmentCollector);
			this._columnNames = codeFragmentCollector.ColumnNames;
			this._skipLines = codeFragmentCollector.SkipLines;
			this._recordGroup = codeFragmentCollector.Group;
			this._splits = codeFragmentCollector.Splits;
			this._trimExtracts = codeFragmentCollector.Extracts;
			this._localizedStrings = localizedStrings;
			this._codeBuilder = new PowerQueryMCodeBuilder(binaryContent, encoding, localizedStrings, escapeIdentifiers);
		}

		// Token: 0x06006E86 RID: 28294 RVA: 0x00168CFA File Offset: 0x00166EFA
		public static string Generate(ProgramNode node, string binaryContent, string encoding, ILocalizedPowerQueryMStrings localizedStrings, IEscapePowerQueryM escapeIdentifiers)
		{
			return new PowerQueryMGenerator(node, binaryContent, encoding, localizedStrings, escapeIdentifiers).Generate();
		}

		// Token: 0x06006E87 RID: 28295 RVA: 0x00168D0C File Offset: 0x00166F0C
		private string Generate()
		{
			if (this._skipLines > 0)
			{
				this._codeBuilder.AddStep(MTableFunctionName.Skip, new string[] { this._skipLines.ToString() }, null);
			}
			this._recordGroup.Switch(Language.Build, delegate(records_skip convert)
			{
			}, delegate(Select sel)
			{
				this._codeBuilder.AddStep(MTableFunctionName.SelectRows, new string[] { "each " + this.GenerateGroupPredicate(sel.re.Value) }, null);
			}, delegate(Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes.Group grp)
			{
				this.GenerateGroupRecord(this.GenerateGroupPredicate(grp.re.Value));
			}, delegate(MergeEvery merge)
			{
				this.GenerateGroupRecord(string.Format("Number.Mod({0}, {1}) = 0", this._codeBuilder.FormatFieldIdentifier("Index"), merge.k.Value));
			});
			int num = 1;
			foreach (Record<split, trimExtract> record in this._splits.ZipWith(this._trimExtracts))
			{
				split split;
				trimExtract trimExtract;
				record.Deconstruct(out split, out trimExtract);
				split split2 = split;
				trimExtract trimExtract2 = trimExtract;
				this.GenerateSplitSteps(split2, num);
				this.GenerateTrimExtract(trimExtract2, num);
				num++;
			}
			this.GenerateTrimExtract(this._trimExtracts.Last<trimExtract>(), num);
			List<Record<string, string>> list = (from i in Enumerable.Range(1, this._columnNames.Count)
				select "Column" + i.ToString()).ToList<string>().ZipWith(this._columnNames).Where2((string c1, string c2) => c1 != c2)
				.ToList<Record<string, string>>();
			if (list.Any<Record<string, string>>())
			{
				string text = string.Join(", ", list.Select2((string c1, string c2) => string.Concat(new string[]
				{
					"{",
					this._codeBuilder.EscapeString(c1),
					", ",
					this._codeBuilder.EscapeString(c2),
					"}"
				})));
				if (list.Count > 1)
				{
					text = "{" + text + "}";
				}
				this._codeBuilder.AddStep(MTableFunctionName.RenameColumns, new string[] { text }, null);
			}
			return this._codeBuilder.GetCode();
		}

		// Token: 0x06006E88 RID: 28296 RVA: 0x00168EFC File Offset: 0x001670FC
		private string GenerateGroupPredicate(Regex regex)
		{
			string text = this._codeBuilder.FormatFieldIdentifier("Column1");
			string text2;
			if (Witnesses.GroupRegexes.TryGetValue(regex, out text2))
			{
				return MTextFunctionName.PositionOfAny.Invoke(new string[] { text, text2 }) + " = 0";
			}
			string text3 = Regex.Unescape(regex.ToString().Substring(1));
			return MTextFunctionName.StartsWith.Invoke(new string[]
			{
				text,
				this._codeBuilder.EscapeString(text3)
			}) ?? "";
		}

		// Token: 0x06006E89 RID: 28297 RVA: 0x00168F8C File Offset: 0x0016718C
		private void GenerateGroupRecord(string predicate)
		{
			string text = this._codeBuilder.EscapeString("StartIndex");
			this._codeBuilder.AddStep(MTableFunctionName.AddIndexColumn, new string[]
			{
				this._codeBuilder.EscapeString("Index"),
				"0",
				"1"
			}, null);
			this._codeBuilder.AddStep(MTableFunctionName.AddColumn, new string[]
			{
				text,
				string.Concat(new string[]
				{
					"each if ",
					predicate,
					" then ",
					this._codeBuilder.FormatFieldIdentifier("Index"),
					" else null"
				})
			}, null);
			this._codeBuilder.AddStep(MTableFunctionName.FillDown, new string[] { "{" + text + "}" }, null);
			this._codeBuilder.AddStep(MTableFunctionName.Group, new string[]
			{
				"{" + text + "}",
				string.Concat(new string[]
				{
					"{",
					this._codeBuilder.EscapeString("Column1"),
					", each ",
					MTextFunctionName.Combine.Invoke(new string[]
					{
						this._codeBuilder.FormatFieldIdentifier("Column1"),
						this._codeBuilder.EscapeString("\n")
					}),
					", type table}"
				}),
				"GroupKind.Local"
			}, null);
			this._codeBuilder.AddStep(MTableFunctionName.RemoveColumns, new string[] { "{" + text + "}" }, null);
		}

		// Token: 0x06006E8A RID: 28298 RVA: 0x00169134 File Offset: 0x00167334
		private void GenerateTrimExtract(trimExtract trimExtract, int columnIndex)
		{
			Trim trim;
			if (trimExtract.Is_Trim(Language.Build, out trim))
			{
				this.GenerateExtract(trim.extract, columnIndex);
				this._codeBuilder.AddTransformStep(MTextFunctionName.Trim, new string[] { "_" }, columnIndex);
				return;
			}
			trimExtract_extract trimExtract_extract;
			if (trimExtract.Is_trimExtract_extract(Language.Build, out trimExtract_extract))
			{
				this.GenerateExtract(trimExtract_extract.extract, columnIndex);
				return;
			}
			throw new NotImplementedException(trimExtract.ToString());
		}

		// Token: 0x06006E8B RID: 28299 RVA: 0x001691B0 File Offset: 0x001673B0
		private void GenerateExtract(extract extract, int columnIndex)
		{
			extract.Switch(Language.Build, delegate(extract_row extract_row)
			{
			}, delegate(BetweenDelimiters betweenDelimiters)
			{
				if (betweenDelimiters.del1.Value.Value == null)
				{
					this._codeBuilder.AddTransformStep(MTextFunctionName.BeforeDelimiter, new string[]
					{
						"_",
						this._codeBuilder.EscapeString(betweenDelimiters.del2.Value.Value)
					}, columnIndex);
					return;
				}
				if (betweenDelimiters.del2.Value.Value == null)
				{
					this._codeBuilder.AddTransformStep(MTextFunctionName.AfterDelimiter, new string[]
					{
						"_",
						this._codeBuilder.EscapeString(betweenDelimiters.del1.Value.Value)
					}, columnIndex);
					return;
				}
				this._codeBuilder.AddTransformStep(MTextFunctionName.BetweenDelimiters, new string[]
				{
					"_",
					this._codeBuilder.EscapeString(betweenDelimiters.del1.Value.Value),
					this._codeBuilder.EscapeString(betweenDelimiters.del2.Value.Value)
				}, columnIndex);
			}, delegate(Substring substring)
			{
				this._codeBuilder.AddTransformStep(MTextFunctionName.Range, new string[]
				{
					"_",
					(substring.k1.Value - 1).ToString(),
					substring.k2.Value.ToString()
				}, columnIndex);
			}, delegate(Slice slice)
			{
				int value = slice.k1.Value;
				int value2 = slice.k2.Value;
				string text = ((value > 0) ? (value - 1).ToString() : string.Format("Text.Length(_) - {0}", -value - 1));
				string text2;
				if (Math.Sign(value) == Math.Sign(value2))
				{
					text2 = Math.Abs(value2 - value).ToString();
				}
				else if (value2 > 0)
				{
					text2 = string.Format("{0} - Text.Length(_)", value2 - value - 2);
				}
				else
				{
					text2 = string.Format("Text.Length(_) - {0}", value - value2 - 2);
				}
				if (slice.k1.Value == 1)
				{
					this._codeBuilder.AddTransformStep(MTextFunctionName.Start, new string[] { "_", text2 }, columnIndex);
					return;
				}
				if (slice.k2.Value == -1)
				{
					this._codeBuilder.AddTransformStep(MTextFunctionName.End, new string[] { "_", text }, columnIndex);
					return;
				}
				this._codeBuilder.AddTransformStep(MTextFunctionName.Range, new string[] { "_", text, text2 }, columnIndex);
			});
		}

		// Token: 0x06006E8C RID: 28300 RVA: 0x00169220 File Offset: 0x00167420
		private void GenerateSplitSteps(split splitNode, int columnIndex)
		{
			splitNode.Switch(Language.Build, delegate(SplitPosition splitPosition)
			{
				string text = ((splitPosition.k.Value > 0) ? string.Format("0, {0}", splitPosition.k.Value - 1) : string.Format("0, {0}", -splitPosition.k.Value + 1));
				this._codeBuilder.AddSplitStep(MSplitterFunctionName.SplitTextByPositions, new string[] { "{" + text + "}" }, columnIndex);
			}, delegate(SplitDelimiter splitDelimiter)
			{
				int num = Math.Abs(splitDelimiter.k.Value) - 1;
				if (num == 0)
				{
					List<string> list = new List<string>
					{
						"{" + this._codeBuilder.EscapeString(splitDelimiter.str.Value) + "}",
						"QuoteStyle.None"
					};
					if (splitDelimiter.k.Value < 0)
					{
						list.Add("RelativePosition.FromEnd");
					}
					this._codeBuilder.AddSplitStep(MSplitterFunctionName.SplitTextByEachDelimiter, list, columnIndex);
					return;
				}
				List<string> list2 = new List<string>
				{
					this._codeBuilder.EscapeString(splitDelimiter.str.Value),
					num.ToString()
				};
				if (splitDelimiter.k.Value < 0)
				{
					list2.Add("RelativePosition.FromEnd");
				}
				string text2 = string.Join(", ", list2.PrependItem(this._codeBuilder.FormatFieldIdentifier("Column" + columnIndex.ToString())));
				this._codeBuilder.AddStep(MTableFunctionName.AddColumn, new string[]
				{
					this._codeBuilder.EscapeString("Column" + (columnIndex + 1).ToString()),
					"each " + MTextFunctionName.AfterDelimiter.Invoke(new string[] { text2 }),
					"type text"
				}, "Inserted After Delimiter");
				this._codeBuilder.AddTransformStep(MTextFunctionName.BeforeDelimiter, list2.PrependItem("_").ToArray<string>(), columnIndex);
			});
		}

		// Token: 0x0400301B RID: 12315
		private const string FirstColumn = "Column1";

		// Token: 0x0400301C RID: 12316
		private const string IndexColumn = "Index";

		// Token: 0x0400301D RID: 12317
		private const string StartIndexColumn = "StartIndex";

		// Token: 0x0400301E RID: 12318
		private readonly PowerQueryMCodeBuilder _codeBuilder;

		// Token: 0x0400301F RID: 12319
		private ILocalizedPowerQueryMStrings _localizedStrings;

		// Token: 0x04003020 RID: 12320
		private readonly IReadOnlyList<string> _columnNames;

		// Token: 0x04003021 RID: 12321
		private readonly records _recordGroup;

		// Token: 0x04003022 RID: 12322
		private readonly int _skipLines;

		// Token: 0x04003023 RID: 12323
		private readonly IReadOnlyList<split> _splits;

		// Token: 0x04003024 RID: 12324
		private readonly IReadOnlyList<trimExtract> _trimExtracts;
	}
}

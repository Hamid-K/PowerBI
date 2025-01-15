using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.RuleNodeTypes;
using Microsoft.ProgramSynthesis.Extraction.Text.Build.UnnamedConversionNodeTypes;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Interactive;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Translation
{
	// Token: 0x02000FA1 RID: 4001
	internal static class PythonGenerator
	{
		// Token: 0x06006EA1 RID: 28321 RVA: 0x00169C78 File Offset: 0x00167E78
		public static string Generate(ProgramNode node, string s, bool readFile)
		{
			CodeFragmentCollector codeFragmentCollector = new CodeFragmentCollector();
			node.AcceptVisitor<object>(codeFragmentCollector);
			return PythonGenerator.Generate(s, readFile, codeFragmentCollector.ColumnNames, codeFragmentCollector.SkipLines, codeFragmentCollector.Group, codeFragmentCollector.Splits, codeFragmentCollector.Extracts);
		}

		// Token: 0x06006EA2 RID: 28322 RVA: 0x00169CB8 File Offset: 0x00167EB8
		private static string Generate(string s, bool readFile, IReadOnlyList<string> columnNames, int skipLines, records group, IReadOnlyList<split> splits, IReadOnlyList<trimExtract> trimExtracts)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			if (!readFile)
			{
				codeBuilder.AppendLine("import io");
			}
			codeBuilder.AppendLine("import pandas as pd");
			if (group.Is_Group(Language.Build) || group.Is_Select(Language.Build))
			{
				codeBuilder.AppendLine("import regex as re");
			}
			codeBuilder.AppendLine();
			codeBuilder.AppendLine();
			if (group.Is_Group(Language.Build) || group.Is_MergeEvery(Language.Build))
			{
				using (codeBuilder.NewScope("def parse_row(record):", 1U))
				{
					PythonGenerator.GenerateParseRow(codeBuilder, "record", splits, trimExtracts);
					codeBuilder.AppendLine();
					codeBuilder.AppendLine("return row");
				}
				codeBuilder.AppendLine();
			}
			using (codeBuilder.NewScope("def parse(" + s + "):", 1U))
			{
				if (readFile)
				{
					using (codeBuilder.NewScope("with open(" + s + ", 'r', encoding='utf-8') as f:", 1U))
					{
						PythonGenerator.GenerateBody(codeBuilder, columnNames, "f", skipLines, group, trimExtracts, splits);
						goto IL_0135;
					}
				}
				codeBuilder.AppendLine("f = io.StringIO(" + s + ", newline=None)");
				PythonGenerator.GenerateBody(codeBuilder, columnNames, "f", skipLines, group, trimExtracts, splits);
			}
			IL_0135:
			return codeBuilder.GetCode();
		}

		// Token: 0x06006EA3 RID: 28323 RVA: 0x00169E28 File Offset: 0x00168028
		private static void GenerateBody(CodeBuilder builder, IReadOnlyList<string> columnNames, string lines, int skipLines, records group, IReadOnlyList<trimExtract> trimExtracts, IReadOnlyList<split> splits)
		{
			group.Switch(Language.Build, delegate(records_skip skip)
			{
			}, delegate(Select sel)
			{
				builder.AppendLine("prefix_re = re.compile(" + sel.re.Value.ToString().ToPythonLiteral() + ")");
			}, delegate(Group grp)
			{
				builder.AppendLine("prefix_re = re.compile(" + grp.re.Value.ToString().ToPythonLiteral() + ")");
				builder.AppendLine("record = ''");
			}, delegate(MergeEvery merge)
			{
				builder.AppendLine("record = ''");
				builder.AppendLine("idx = 1");
			});
			if (skipLines > 0)
			{
				builder.AppendLine("skip = 0");
			}
			builder.AppendLine("table = []");
			using (builder.NewScope("for line in " + lines + ":", 1U))
			{
				if (skipLines > 0)
				{
					builder.AppendLine(string.Format("# skip {0} lines", skipLines));
					using (builder.NewScope(string.Format("if skip < {0}:", skipLines), 1U))
					{
						builder.AppendLine("skip += 1");
						builder.AppendLine("continue");
					}
					builder.AppendLine();
				}
				builder.AppendLine("line = line.rstrip('\\r\\n')");
				builder.AppendLine();
				group.Switch(Language.Build, delegate(records_skip skip)
				{
					PythonGenerator.GenerateLoopBody(builder, false, trimExtracts, splits);
				}, delegate(Select select)
				{
					PythonGenerator.GenerateLoopBody(builder, true, trimExtracts, splits);
				}, delegate(Group grp)
				{
					PythonGenerator.GenerateLoopBodyHavingGrouping(builder, grp);
				}, delegate(MergeEvery merge)
				{
					PythonGenerator.GenerateLoopBodyHavingGrouping(builder, merge);
				});
			}
			if (group.Is_Group(Language.Build) || group.Is_MergeEvery(Language.Build))
			{
				builder.AppendLine();
				using (builder.NewScope("if record:", 1U))
				{
					builder.AppendLine("table.append(parse_row(record))");
				}
			}
			builder.AppendLine();
			string text = string.Join(", ", columnNames.Select((string col) => "'" + col + "'"));
			builder.AppendLine("return pd.DataFrame(table, columns=[" + text + "])");
		}

		// Token: 0x06006EA4 RID: 28324 RVA: 0x0016A08C File Offset: 0x0016828C
		private static void GenerateParseRow(CodeBuilder builder, string record, IReadOnlyList<split> splits, IReadOnlyList<trimExtract> trimExtracts)
		{
			builder.AppendLine("# parse a " + record + " into a table row");
			builder.AppendLine("row = []");
			builder.AppendLine("rest = " + record);
			builder.AppendLine();
			for (int i = 0; i < splits.Count; i++)
			{
				builder.AppendLine(string.Format("# column {0}", i + 1));
				PythonGenerator.GenerateSplit(splits[i], "rest").ForEach(new Action<string>(builder.AppendLine));
				foreach (string text in PythonGenerator.GenerateTrimExtract(trimExtracts[i], "first"))
				{
					builder.AppendLine(text);
				}
				builder.AppendLine();
			}
			builder.AppendLine(string.Format("# column {0}", splits.Count + 1));
			foreach (string text2 in PythonGenerator.GenerateTrimExtract(trimExtracts.Last<trimExtract>(), "rest"))
			{
				builder.AppendLine(text2);
			}
		}

		// Token: 0x06006EA5 RID: 28325 RVA: 0x0016A1D4 File Offset: 0x001683D4
		private static void GenerateLoopBodyHavingGrouping(CodeBuilder builder, records group)
		{
			using (builder.NewScope("if not record:", 1U))
			{
				builder.AppendLine("record = line");
			}
			MergeEvery mergeEvery;
			string text;
			if (group.Is_MergeEvery(Language.Build, out mergeEvery))
			{
				text = string.Format("idx % {0} == 0", mergeEvery.k.Value);
			}
			else
			{
				if (!group.Is_Group(Language.Build))
				{
					throw new InvalidOperationException(string.Format("Unexpected type {0}", group.Node));
				}
				text = "prefix_re.match(line)";
			}
			using (builder.NewScope("elif " + text + ":", 1U))
			{
				builder.AppendLine("table.append(parse_row(record))");
				builder.AppendLine("record = line");
				if (group.Is_MergeEvery(Language.Build))
				{
					builder.AppendLine("idx = 1");
				}
			}
			using (builder.NewScope("else:", 1U))
			{
				builder.AppendLine("# add line to the record");
				builder.AppendLine("record += '\\n' + line");
				if (group.Is_MergeEvery(Language.Build))
				{
					builder.AppendLine("idx += 1");
				}
			}
		}

		// Token: 0x06006EA6 RID: 28326 RVA: 0x0016A328 File Offset: 0x00168528
		private static void GenerateLoopBody(CodeBuilder builder, bool hasSelectRegex, IReadOnlyList<trimExtract> trimExtracts, IReadOnlyList<split> splits)
		{
			if (hasSelectRegex)
			{
				using (builder.NewScope("if not prefix_re.match(line):", 1U))
				{
					builder.AppendLine("continue");
					builder.AppendLine();
				}
			}
			PythonGenerator.GenerateParseRow(builder, "line", splits, trimExtracts);
			builder.AppendLine();
			builder.AppendLine("# add row to table");
			builder.AppendLine("table.append(row)");
		}

		// Token: 0x06006EA7 RID: 28327 RVA: 0x0016A39C File Offset: 0x0016859C
		private static int PythonIndex(int index)
		{
			if (index <= 0)
			{
				return index + 1;
			}
			return index - 1;
		}

		// Token: 0x06006EA8 RID: 28328 RVA: 0x0016A3AC File Offset: 0x001685AC
		private static string[] GenerateSplit(split splitNode, string variable)
		{
			return splitNode.Switch<string[]>(Language.Build, delegate(SplitPosition splitPosition)
			{
				int num = PythonGenerator.PythonIndex(splitPosition.k.Value);
				return new string[] { string.Format("({0}, {1}) = {2}[:{3}], {4}[{5}:]", new object[] { "first", "rest", variable, num, variable, num }) };
			}, delegate(SplitDelimiter splitDelimiter)
			{
				string text = splitDelimiter.str.Value.ToPythonLiteral();
				int value = splitDelimiter.k.Value;
				if (Math.Abs(value) == 1)
				{
					return new string[]
					{
						string.Concat(new string[]
						{
							"tup = ",
							variable,
							".",
							(value > 0) ? string.Empty : "r",
							"split(",
							text,
							", maxsplit=1)"
						}),
						"first = tup[0]",
						"rest = tup[1] if len(tup) == 2 else None"
					};
				}
				return new string[]
				{
					string.Concat(new string[] { "groups = ", variable, ".split(", text, ")" }),
					string.Format("({0}, {1}) = {2}.join(groups[:{3}]), {4}.join(groups[{5}:])", new object[] { "first", "rest", text, value, text, value })
				};
			});
		}

		// Token: 0x06006EA9 RID: 28329 RVA: 0x0016A3EC File Offset: 0x001685EC
		private static IEnumerable<string> GenerateTrimExtract(trimExtract trimExtractNode, string variable)
		{
			return trimExtractNode.Switch<IEnumerable<string>>(Language.Build, (trimExtract_extract trimExtractConversion) => PythonGenerator.GenerateExtract(trimExtractConversion.extract, variable, false), (Trim trim) => PythonGenerator.GenerateExtract(trim.extract, variable, true));
		}

		// Token: 0x06006EAA RID: 28330 RVA: 0x0016A42C File Offset: 0x0016862C
		private static IEnumerable<string> GenerateExtract(extract ext, string variable, bool requireTrim)
		{
			string[] array = ext.Switch<string[]>(Language.Build, (extract_row extract_row) => new string[] { variable }, delegate(BetweenDelimiters betweenDelimiters)
			{
				List<string> list = new List<string>();
				string value = betweenDelimiters.del1.Value.Value;
				string value2 = betweenDelimiters.del2.Value.Value;
				string text2 = string.Empty;
				string text3 = string.Empty;
				if (value != null)
				{
					text3 = "start";
					list.Add(string.Format("{0} = {1}.find({2}) + {3}", new object[]
					{
						text3,
						variable,
						value.ToPythonLiteral(),
						value.Length
					}));
					text2 = text3 + " != -1";
				}
				string text4 = string.Empty;
				if (value2 != null)
				{
					text4 = "end";
					string text5 = ((text3 == string.Empty) ? string.Empty : (", " + text3));
					list.Add(string.Concat(new string[]
					{
						text4,
						" = ",
						variable,
						".find(",
						value2.ToPythonLiteral(),
						text5,
						")"
					}));
					if (text2 != string.Empty)
					{
						text2 += " and ";
					}
					text2 = text2 + text4 + " != -1";
				}
				list.Add(string.Concat(new string[] { variable, "[", text3, ":", text4, "] if ", text2, " else None" }));
				return list.ToArray();
			}, delegate(Substring substring)
			{
				int num = PythonGenerator.PythonIndex(substring.k1.Value);
				return new string[] { string.Format("{0}[{1}:{2}]", variable, num, num + substring.k2.Value) };
			}, (Slice slice) => new string[] { string.Format("{0}[{1}:{2}]", variable, PythonGenerator.PythonIndex(slice.k1.Value), PythonGenerator.PythonIndex(slice.k2.Value)) });
			string text = array[array.Length - 1];
			array[array.Length - 1] = "row.append(" + (requireTrim ? ("(" + text + ").strip()") : text) + ")";
			return array;
		}

		// Token: 0x04003031 RID: 12337
		private const string First = "first";

		// Token: 0x04003032 RID: 12338
		private const string Rest = "rest";

		// Token: 0x04003033 RID: 12339
		private const string Record = "record";
	}
}

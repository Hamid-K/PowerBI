using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning.Models;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.Utils.Text;
using Microsoft.ProgramSynthesis.Wrangling;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Session;
using Newtonsoft.Json.Linq;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x02001775 RID: 6005
	public static class ExampleExtension
	{
		// Token: 0x0600C711 RID: 50961 RVA: 0x002AC92C File Offset: 0x002AAB2C
		public static string Render(this IEnumerable<Example<IRow, object>> examples, IEnumerable<string> columnNames = null)
		{
			examples = examples.ToReadOnlyList<Example<IRow, object>>();
			columnNames = columnNames.ToReadOnlyList<string>();
			List<ColumnDetail> list = (from c in examples.InputColumnDetails(null)
				orderby c.Name
				select c).ToList<ColumnDetail>();
			List<ColumnDetail> resolvedColumns = ((!columnNames.Any<string>()) ? list : list.Where((ColumnDetail c) => columnNames.Contains(c.Name)).ToList<ColumnDetail>());
			TextTableBuilder textTableBuilder = TextTableBuilder.Create(TextTableBorder.None, null, null).AddIdentityColumn("", new int?(0), new int?(0)).AddStaticColumn(":", false, true, false);
			foreach (ColumnDetail columnDetail in resolvedColumns)
			{
				TextTableBuilder textTableBuilder2 = textTableBuilder;
				string name = columnDetail.Name;
				int num = 0;
				bool allNumber = columnDetail.AllNumber;
				textTableBuilder2.AddColumn(name, num, new int?(60), allNumber, null, null, null, null);
			}
			int num2 = list.Count - resolvedColumns.Count;
			if (num2 > 0)
			{
				textTableBuilder.AddColumn(string.Format("[{0:N0} more]", num2), 0, null, false, null, null, null, null);
			}
			return textTableBuilder.AddColumn("Output", 0, null, false, null, null, null, null).AddHeadingRow().AddDataRows(examples.Select((Example<IRow, object> example) => (from <>h__TransparentIdentifier0 in resolvedColumns.Select(delegate(ColumnDetail column)
				{
					object obj;
					return new
					{
						column = column,
						value = (example.Input.TryGetValue(column.Name, out obj) ? obj : null)
					};
				})
				select <>h__TransparentIdentifier0.value.ToCSharpPseudoLiteral()).AppendItem(example.Output.ToCSharpPseudoLiteral()).ToList<string>()).ToList<List<string>>(), null)
				.Render();
		}

		// Token: 0x0600C712 RID: 50962 RVA: 0x002ACB24 File Offset: 0x002AAD24
		public static string Render(this IEnumerable<IRow> rows, int startNumber = 1)
		{
			return rows.Select((IRow r) => r.ToString()).RenderNumbered(startNumber);
		}

		// Token: 0x0600C713 RID: 50963 RVA: 0x002ACB51 File Offset: 0x002AAD51
		public static string Render(this IEnumerable<SignificantInput<IRow>> rows)
		{
			if (rows == null)
			{
				return null;
			}
			return rows.Select((SignificantInput<IRow> row) => row.Input.ToString()).RenderNumbered(1);
		}

		// Token: 0x0600C714 RID: 50964 RVA: 0x002ACB83 File Offset: 0x002AAD83
		public static string Render(this IEnumerable<IOutputSuggestion> suggestions)
		{
			if (suggestions == null)
			{
				return null;
			}
			return suggestions.Select((IOutputSuggestion suggestion) => suggestion.Text).RenderNumbered(1);
		}

		// Token: 0x0600C715 RID: 50965 RVA: 0x002ACBB5 File Offset: 0x002AADB5
		public static IEnumerable<string> ValidColumnNames(this IEnumerable<Example<IRow, object>> examples)
		{
			return examples.Select((Example<IRow, object> e) => e.Input).ValidColumnNames();
		}

		// Token: 0x0600C716 RID: 50966 RVA: 0x002ACBE4 File Offset: 0x002AADE4
		public static IEnumerable<string> ValidColumnNames(this IEnumerable<IRow> rows)
		{
			return (from row in rows
				from columnName in row.ValidColumnNames()
				select columnName).Distinct<string>();
		}

		// Token: 0x0600C717 RID: 50967 RVA: 0x002ACC3A File Offset: 0x002AAE3A
		public static IEnumerable<string> ValidColumnNames(this Example<IRow, object> example)
		{
			return example.Input.ValidColumnNames();
		}

		// Token: 0x0600C718 RID: 50968 RVA: 0x002ACC47 File Offset: 0x002AAE47
		public static IEnumerable<string> ValidColumnNames(this IRow row)
		{
			IEnumerable<string> columnNames = row.ColumnNames;
			if (columnNames == null)
			{
				throw new Exception("Unable to determine column names for IRow.");
			}
			return columnNames;
		}

		// Token: 0x0600C719 RID: 50969 RVA: 0x002ACC5E File Offset: 0x002AAE5E
		public static IReadOnlyList<ColumnDetail> ColumnDetails(this IEnumerable<Example<IRow, object>> examples, IEnumerable<string> columnNames = null)
		{
			examples = examples.ToReadOnlyList<Example<IRow, object>>();
			return examples.InputColumnDetails(columnNames).Concat(examples.OutputColumnDetail().Yield<ColumnDetail>()).ToList<ColumnDetail>();
		}

		// Token: 0x0600C71A RID: 50970 RVA: 0x002ACC84 File Offset: 0x002AAE84
		public static IReadOnlyList<ColumnDetail> InputColumnDetails(this IEnumerable<Example<IRow, object>> examples, IEnumerable<string> columnNames = null)
		{
			return (from e in examples.ToReadOnlyList<Example<IRow, object>>()
				select e.Input).InputColumnDetails(columnNames);
		}

		// Token: 0x0600C71B RID: 50971 RVA: 0x002ACCB8 File Offset: 0x002AAEB8
		public static IReadOnlyList<ColumnDetail> InputColumnDetails(this IEnumerable<IRow> rows, IEnumerable<string> columnNames = null)
		{
			if (rows == null)
			{
				return new ColumnDetail[0];
			}
			IReadOnlyList<IRow> readOnlyList = rows.ToReadOnlyList<IRow>();
			List<ColumnDetail> list = new List<ColumnDetail>();
			if (columnNames == null)
			{
				columnNames = readOnlyList.ValidColumnNames();
			}
			using (IEnumerator<string> enumerator = columnNames.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string columnName = enumerator.Current;
					IReadOnlyList<object> readOnlyList2 = readOnlyList.Select(delegate(IRow r)
					{
						object obj;
						if (!r.TryGetValue(columnName, out obj))
						{
							return null;
						}
						return obj;
					}).ToList<object>();
					list.Add(ColumnDetail.Create(columnName, readOnlyList2));
				}
			}
			return list;
		}

		// Token: 0x0600C71C RID: 50972 RVA: 0x002ACD54 File Offset: 0x002AAF54
		public static ColumnDetail OutputColumnDetail(this IEnumerable<Example<IRow, object>> examples)
		{
			if (examples != null)
			{
				return ColumnDetail.Create("Output", examples.Select((Example<IRow, object> e) => e.Output));
			}
			return null;
		}

		// Token: 0x0600C71D RID: 50973 RVA: 0x002ACD8A File Offset: 0x002AAF8A
		public static IEnumerable<string> StringOutputs<TProgramInput, TProgramOutput>(this IEnumerable<Example<TProgramInput, TProgramOutput>> examples)
		{
			return examples.Select((Example<TProgramInput, TProgramOutput> e) => e.Output).OfType<string>();
		}

		// Token: 0x0600C71E RID: 50974 RVA: 0x002ACDB8 File Offset: 0x002AAFB8
		public static Dictionary<string, ColumnDetail> ToDictionary(this IEnumerable<ColumnDetail> details)
		{
			return details.ToDictionary((ColumnDetail item) => item.Name, (ColumnDetail item) => item);
		}

		// Token: 0x0600C71F RID: 50975 RVA: 0x002ACE09 File Offset: 0x002AB009
		public static JObject ToAnonymizedJson(this IEnumerable<Example<IRow, object>> examples)
		{
			if (examples != null)
			{
				return JObject.FromObject(new
				{
					Examples = examples.Select((Example<IRow, object> example) => example.ToAnonymizedJson())
				});
			}
			return null;
		}

		// Token: 0x0600C720 RID: 50976 RVA: 0x002ACE40 File Offset: 0x002AB040
		public static JObject ToAnonymizedJson(this Example<IRow, object> example)
		{
			if (example == null)
			{
				return null;
			}
			JObject jobject = JObject.FromObject(new
			{
				Input = example.Input.ToAnonymizedJson(),
				Output = ExampleExtension.ToAnonymizedValue(example.Output)
			});
			INumberedRow numberedRow = example.Input as INumberedRow;
			if (numberedRow != null)
			{
				jobject["RowNumber"] = numberedRow.RowNumber;
			}
			return jobject;
		}

		// Token: 0x0600C721 RID: 50977 RVA: 0x002ACEA0 File Offset: 0x002AB0A0
		public static JObject ToAnonymizedJson(this IRow row)
		{
			JObject jobject = new JObject();
			foreach (Record<int, string> record in row.ValidColumnNames().Enumerate<string>())
			{
				string text = string.Format("i{0}", record.Item1 + 1);
				object obj = row.Get(record.Item2);
				jobject[text] = ((obj == null) ? null : JToken.FromObject(ExampleExtension.ToAnonymizedValue(obj)));
			}
			INumberedRow numberedRow = row as INumberedRow;
			if (numberedRow != null)
			{
				jobject["RowNumber"] = numberedRow.RowNumber;
			}
			return jobject;
		}

		// Token: 0x0600C722 RID: 50978 RVA: 0x002ACF54 File Offset: 0x002AB154
		public static JObject ToJson(this IEnumerable<Example<IRow, object>> examples)
		{
			if (examples != null)
			{
				return JObject.FromObject(new
				{
					Examples = examples.Select((Example<IRow, object> example) => example.ToJson())
				});
			}
			return null;
		}

		// Token: 0x0600C723 RID: 50979 RVA: 0x002ACF8A File Offset: 0x002AB18A
		public static JObject ToJson(this Example<IRow, object> example)
		{
			return JObject.FromObject(new
			{
				Input = example.Input.ToJson(),
				Output = example.Output
			});
		}

		// Token: 0x0600C724 RID: 50980 RVA: 0x002ACFA8 File Offset: 0x002AB1A8
		public static JObject ToJson(this IRow row)
		{
			JObject jobject = new JObject();
			foreach (string text in row.ValidColumnNames())
			{
				object obj = row.Get(text);
				jobject[text] = ((obj == null) ? null : JToken.FromObject(row.Get(text)));
			}
			INumberedRow numberedRow = row as INumberedRow;
			if (numberedRow != null)
			{
				jobject["RowNumber"] = numberedRow.RowNumber;
			}
			return jobject;
		}

		// Token: 0x0600C725 RID: 50981 RVA: 0x002AD038 File Offset: 0x002AB238
		public static string ToTestCode(this IEnumerable<Example<IRow, object>> examples)
		{
			string newLine = Environment.NewLine;
			string text = newLine + " ".Repeat(8);
			string[] array = new string[5];
			array[0] = "new[] {";
			array[1] = text;
			array[2] = examples.Select((Example<IRow, object> e) => e.ToTestCode()).ToJoinString("," + text);
			array[3] = newLine;
			array[4] = "    }";
			return string.Concat(array);
		}

		// Token: 0x0600C726 RID: 50982 RVA: 0x002AD0B8 File Offset: 0x002AB2B8
		public static string ToTestCode(this Example<IRow, object> example)
		{
			return string.Concat(new string[]
			{
				"ToExample(new object[] { ",
				(from col in example.Input.ValidColumnNames()
					select example.Input.Get(col).ToCSharpLiteral()).ToJoinString(", "),
				" }, ",
				example.Output.ToCSharpLiteral(),
				")"
			});
		}

		// Token: 0x0600C727 RID: 50983 RVA: 0x002AD138 File Offset: 0x002AB338
		private static object ToAnonymizedValue(object value)
		{
			string text;
			if (!(value is int) && !(value is double) && !(value is decimal) && !(value is float) && !(value is uint) && !(value is long) && !(value is byte))
			{
				if (!(value is DateTime))
				{
					text = ((value != null) ? value.ToString().ToAnonymizedString() : null);
				}
				else
				{
					text = "@date";
				}
			}
			else
			{
				text = "#" + value.ToString().ToAnonymizedString();
			}
			return text;
		}
	}
}

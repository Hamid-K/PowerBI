using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001B0D RID: 6925
	internal class DropRowsSuggester : ISuggester
	{
		// Token: 0x0600E427 RID: 58407 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool CanBeDestructive()
		{
			return true;
		}

		// Token: 0x0600E428 RID: 58408 RVA: 0x00306211 File Offset: 0x00304411
		public bool CanSuggest(Operators allowedOperators)
		{
			return allowedOperators.HasFlag(Operators.DropEmptyRows) || allowedOperators.HasFlag(Operators.DropDuplicateRows);
		}

		// Token: 0x0600E429 RID: 58409 RVA: 0x00306244 File Offset: 0x00304444
		public ProgramSetBuilder<table> Suggest(GrammarBuilders build, Options options, Table<object> inputTable)
		{
			table table = build.Node.UnnamedConversion.table_inputTable(build.Node.Variable.inputTable);
			List<ProgramSetBuilder<table>> list = new List<ProgramSetBuilder<table>>();
			if (options.AllowedOperators.HasFlag(Operators.DropEmptyRows))
			{
				MissingValueType missingValueType = MissingValueType.NAValue;
				int num = inputTable.ColumnNames.Count<string>();
				int num2 = 0;
				double num3 = 0.9 * (double)num;
				foreach (IEnumerable<object> enumerable in inputTable.Rows)
				{
					int num4 = 0;
					int num5 = 0;
					int num6 = 0;
					int num7 = 0;
					foreach (object obj in enumerable)
					{
						if (obj == null || (obj is double && double.IsNaN((double)obj)))
						{
							num4++;
						}
						else
						{
							string text = obj as string;
							if (text != null)
							{
								double num8;
								if (string.IsNullOrEmpty(text))
								{
									num5++;
								}
								else if (string.IsNullOrWhiteSpace(text))
								{
									num6++;
								}
								else if (double.TryParse(text, out num8) && double.IsNaN(num8))
								{
									num7++;
								}
							}
						}
					}
					if ((double)(num4 + num5 + num6 + num7) >= num3)
					{
						if (num5 > 0)
						{
							missingValueType |= MissingValueType.EmptyString;
						}
						if (num6 > 0)
						{
							missingValueType |= MissingValueType.WhiteSpace;
						}
						if (num7 > 0)
						{
							missingValueType |= MissingValueType.NanString;
						}
						num2++;
					}
				}
				if ((double)num2 / (double)inputTable.Count<IEnumerable<object>>() >= 0.01)
				{
					list.Add(build.Set.Join.DropRows(table.ToProgramSet<table>(), build.Node.Rule.dropCondition(new MissingCondition(0.9, missingValueType)).ToProgramSet<dropCondition>()));
				}
			}
			if (options.AllowedOperators.HasFlag(Operators.DropDuplicateRows))
			{
				if ((double)(inputTable.Count<IEnumerable<object>>() - inputTable.Rows.Select((IEnumerable<object> row) => row.Select(delegate(object cell)
				{
					if (cell == null)
					{
						return null;
					}
					return cell.ToString();
				}).OrderDependentHashCode<string>()).Distinct<int>().Count<int>()) / (double)inputTable.Count<IEnumerable<object>>() >= 0.01)
				{
					list.Add(build.Set.Join.DropRows(table.ToProgramSet<table>(), build.Node.Rule.dropCondition(new DuplicateCondition()).ToProgramSet<dropCondition>()));
				}
			}
			return list.NormalizedUnion<table>();
		}

		// Token: 0x0400565E RID: 22110
		private const double MISSING_VALUE_FRACTION_THRESHOLD = 0.9;

		// Token: 0x0400565F RID: 22111
		private const double FRACTION_OF_ROWS_TO_BE_DROPPED_THRESHOLD = 0.01;
	}
}

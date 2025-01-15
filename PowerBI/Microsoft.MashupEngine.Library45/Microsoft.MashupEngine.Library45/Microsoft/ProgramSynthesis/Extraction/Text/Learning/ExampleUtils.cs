using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Text.Semantics;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Text.Learning
{
	// Token: 0x02000F4C RID: 3916
	internal static class ExampleUtils
	{
		// Token: 0x06006D21 RID: 27937 RVA: 0x00164392 File Offset: 0x00162592
		internal static IEnumerable<List<List<StringRegionCell>>> EnumerateExampleMappings(StringRegion inputRegion, ITable<ExampleCell> table)
		{
			int num = 0;
			bool flag = true;
			List<List<StringRegionCell>> rows;
			while (ExampleUtils.TryMapExample(inputRegion, table, num, flag, out rows))
			{
				yield return rows;
				if (rows[0][0].Value == null)
				{
					yield break;
				}
				num = (int)(rows[0][0].Start + 1U);
				flag = false;
			}
			yield break;
		}

		// Token: 0x06006D22 RID: 27938 RVA: 0x001643AC File Offset: 0x001625AC
		private static bool TryMapExample(StringRegion inputRegion, ITable<ExampleCell> table, int startIndex, bool throwException, out List<List<StringRegionCell>> rows)
		{
			int num = table.ColumnNames.Count<string>();
			int num2 = startIndex;
			int num3 = 0;
			rows = new List<List<StringRegionCell>>();
			foreach (IEnumerable<ExampleCell> enumerable in table)
			{
				List<StringRegionCell> list = new List<StringRegionCell>(num);
				int num4 = 0;
				foreach (ExampleCell exampleCell in enumerable)
				{
					StringRegionCell stringRegionCell;
					if (string.IsNullOrWhiteSpace(exampleCell.Value))
					{
						stringRegionCell = new StringRegionCell(null, exampleCell.IsUserSpecified);
					}
					else
					{
						stringRegionCell = ExampleUtils.CreateCellExample(inputRegion, exampleCell, num2, out num2);
						if (stringRegionCell == null)
						{
							if (throwException)
							{
								throw new ExampleNotFoundException(num3, num4, exampleCell.Value, string.Format("Example at row {0}, column {1} is not found in the input.", num3, num4));
							}
							return false;
						}
					}
					list.Add(stringRegionCell);
					num4++;
				}
				if (num4 != num)
				{
					if (throwException)
					{
						throw new TableNotRectangleException(num3, num4 - 1, null, string.Format("Row {0} contains {1} elements instead of {2}", num3, num4, num));
					}
					return false;
				}
				else
				{
					rows.Add(list);
					num3++;
					while ((long)num2 < (long)((ulong)inputRegion.End) && inputRegion.Source[num2] != '\n')
					{
						num2++;
					}
				}
			}
			return true;
		}

		// Token: 0x06006D23 RID: 27939 RVA: 0x00164540 File Offset: 0x00162740
		private static StringRegionCell CreateCellExample(StringRegion input, ExampleCell cellExample, int startIndex, out int endIndex)
		{
			string value = cellExample.Value;
			int num = input.Source.IndexOf(value, startIndex, StringComparison.Ordinal);
			if (num != -1)
			{
				endIndex = num + value.Length;
				return new StringRegionCell(input.Slice((uint)num, (uint)(num + value.Length)), cellExample.IsUserSpecified);
			}
			endIndex = startIndex;
			if (!cellExample.IsUserSpecified)
			{
				return new StringRegionCell(null, false);
			}
			return null;
		}
	}
}

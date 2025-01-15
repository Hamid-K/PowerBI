using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Transformation.Table.Build;
using Microsoft.ProgramSynthesis.Transformation.Table.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Meta;
using Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Util;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Semantics.Learning
{
	// Token: 0x02001B07 RID: 6919
	internal class DropIndexColumnSuggester : ColumnSuggester
	{
		// Token: 0x0600E413 RID: 58387 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool CanBeDestructive()
		{
			return true;
		}

		// Token: 0x0600E414 RID: 58388 RVA: 0x00305D81 File Offset: 0x00303F81
		public override bool CanSuggest(Operators allowedOperators)
		{
			return allowedOperators.HasFlag(Operators.DropIndexColumn);
		}

		// Token: 0x0600E415 RID: 58389 RVA: 0x00305D98 File Offset: 0x00303F98
		public override int GetHashCode(Options options, Table<object> inputTable, string sourceColumnName)
		{
			string[] array = new string[4];
			array[0] = inputTable.HashedColumn(sourceColumnName).ToString();
			array[1] = sourceColumnName;
			int num = 2;
			NumPrefixRowsMetadata numPrefixRowsMetadata = inputTable.Metadata.OfType<NumPrefixRowsMetadata>().FirstOrDefault<NumPrefixRowsMetadata>();
			array[num] = ((numPrefixRowsMetadata != null) ? numPrefixRowsMetadata.NumPrefixRows.ToString() : inputTable.Rows.Count<IEnumerable<object>>().ToString());
			array[3] = options.GetHashCode().ToString();
			return array.OrderDependentHashCode<string>();
		}

		// Token: 0x0600E416 RID: 58390 RVA: 0x00305E14 File Offset: 0x00304014
		public override ProgramSetBuilder<table> SuggestForColumn(GrammarBuilders build, Options options, Table<object> inputTable, int columnIdx, string columnName)
		{
			IEnumerable<object> enumerable = inputTable.Column(columnIdx);
			NumPrefixRowsMetadata numPrefixRowsMetadata = inputTable.Metadata.OfType<NumPrefixRowsMetadata>().FirstOrDefault<NumPrefixRowsMetadata>();
			int? num = ((numPrefixRowsMetadata != null) ? new int?(numPrefixRowsMetadata.NumPrefixRows) : null);
			IEnumerable<object> enumerable2;
			if (num != null)
			{
				int valueOrDefault = num.GetValueOrDefault();
				if (valueOrDefault > 0)
				{
					enumerable2 = enumerable.Take(valueOrDefault);
					goto IL_0052;
				}
			}
			enumerable2 = enumerable;
			IL_0052:
			enumerable = enumerable2;
			if (!enumerable.Skip(1).Any<object>() || !Utilities.IsInteger(enumerable, false))
			{
				return null;
			}
			if (enumerable.Distinct<object>().Count<object>() == enumerable.Count<object>())
			{
				IEnumerable<object> enumerable3 = enumerable;
				Func<object, long> func;
				if ((func = DropIndexColumnSuggester.<>O.<0>__ToInt64) == null)
				{
					func = (DropIndexColumnSuggester.<>O.<0>__ToInt64 = new Func<object, long>(Convert.ToInt64));
				}
				if (!(from s in enumerable3.Select(func)
					orderby s
					select s).Select((long v, int i) => v - (long)i).Distinct<long>().Skip(1)
					.Any<long>())
				{
					return build.Set.Join.DropColumn(build.Node.UnnamedConversion.table_inputTable(build.Node.Variable.inputTable).ToProgramSet<table>(), build.Node.Rule.sourceColumnName(columnName).ToProgramSet<sourceColumnName>(), build.Node.Rule.dropCondition(new DropCondition(DropReason.Index)).ToProgramSet<dropCondition>());
				}
			}
			return null;
		}

		// Token: 0x02001B08 RID: 6920
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04005655 RID: 22101
			public static Func<object, long> <0>__ToInt64;
		}
	}
}

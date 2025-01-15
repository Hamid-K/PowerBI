using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.DSQ
{
	// Token: 0x02000107 RID: 263
	internal static class DsqTransformBuilder
	{
		// Token: 0x060008AA RID: 2218 RVA: 0x00022C4E File Offset: 0x00020E4E
		internal static List<DataTransform> BuildTransforms(IReadOnlyList<IntermediateQueryTransform> transforms)
		{
			Func<IntermediateQueryTransform, DataTransform> func;
			if ((func = DsqTransformBuilder.<>O.<0>__BuildTransform) == null)
			{
				func = (DsqTransformBuilder.<>O.<0>__BuildTransform = new Func<IntermediateQueryTransform, DataTransform>(DsqTransformBuilder.BuildTransform));
			}
			return DsqGenerationUtils.BuildList<DataTransform, IntermediateQueryTransform>(transforms, func);
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00022C74 File Offset: 0x00020E74
		private static DataTransform BuildTransform(IntermediateQueryTransform transform)
		{
			return new DataTransform
			{
				Id = transform.Id,
				Algorithm = transform.Algorithm,
				Input = DsqTransformBuilder.BuildTransformInput(transform.Input),
				Output = DsqTransformBuilder.BuildTransformOutput(transform.Output)
			};
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00022CC8 File Offset: 0x00020EC8
		private static DataTransformInput BuildTransformInput(IntermediateQueryTransformInput input)
		{
			DataTransformInput dataTransformInput = new DataTransformInput();
			IReadOnlyList<IntermediateQueryTransformParameter> parameters = input.Parameters;
			Func<IntermediateQueryTransformParameter, DataTransformParameter> func;
			if ((func = DsqTransformBuilder.<>O.<1>__BuildTransformParameter) == null)
			{
				func = (DsqTransformBuilder.<>O.<1>__BuildTransformParameter = new Func<IntermediateQueryTransformParameter, DataTransformParameter>(DsqTransformBuilder.BuildTransformParameter));
			}
			dataTransformInput.Parameters = DsqGenerationUtils.BuildList<DataTransformParameter, IntermediateQueryTransformParameter>(parameters, func);
			dataTransformInput.Table = DsqTransformBuilder.BuildTransformTable(input.Table);
			return dataTransformInput;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00022D17 File Offset: 0x00020F17
		private static DataTransformOutput BuildTransformOutput(IntermediateQueryTransformOutput output)
		{
			return new DataTransformOutput
			{
				Table = DsqTransformBuilder.BuildTransformTable(output.Table)
			};
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x00022D2F File Offset: 0x00020F2F
		private static DataTransformParameter BuildTransformParameter(IntermediateQueryTransformParameter param)
		{
			return new DataTransformParameter
			{
				Id = param.Name,
				Value = param.Expression
			};
		}

		// Token: 0x060008AF RID: 2223 RVA: 0x00022D58 File Offset: 0x00020F58
		private static DataTransformTable BuildTransformTable(IntermediateQueryTransformTable table)
		{
			DataTransformTable dataTransformTable = new DataTransformTable();
			dataTransformTable.Id = table.Id;
			IReadOnlyList<IntermediateQueryTransformTableColumn> columns = table.Columns;
			Func<IntermediateQueryTransformTableColumn, DataTransformTableColumn> func;
			if ((func = DsqTransformBuilder.<>O.<2>__BuildTransformTableColumn) == null)
			{
				func = (DsqTransformBuilder.<>O.<2>__BuildTransformTableColumn = new Func<IntermediateQueryTransformTableColumn, DataTransformTableColumn>(DsqTransformBuilder.BuildTransformTableColumn));
			}
			dataTransformTable.Columns = DsqGenerationUtils.BuildList<DataTransformTableColumn, IntermediateQueryTransformTableColumn>(columns, func);
			return dataTransformTable;
		}

		// Token: 0x060008B0 RID: 2224 RVA: 0x00022D97 File Offset: 0x00020F97
		private static DataTransformTableColumn BuildTransformTableColumn(IntermediateQueryTransformTableColumn column)
		{
			return new DataTransformTableColumn
			{
				Id = column.Id,
				Role = column.Role,
				Value = column.Expression
			};
		}

		// Token: 0x0200016A RID: 362
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400057D RID: 1405
			public static Func<IntermediateQueryTransform, DataTransform> <0>__BuildTransform;

			// Token: 0x0400057E RID: 1406
			public static Func<IntermediateQueryTransformParameter, DataTransformParameter> <1>__BuildTransformParameter;

			// Token: 0x0400057F RID: 1407
			public static Func<IntermediateQueryTransformTableColumn, DataTransformTableColumn> <2>__BuildTransformTableColumn;
		}
	}
}

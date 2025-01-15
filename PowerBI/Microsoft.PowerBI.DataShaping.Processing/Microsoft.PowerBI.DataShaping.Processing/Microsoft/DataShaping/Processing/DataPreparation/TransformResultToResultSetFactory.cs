using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Processing.Analytics;
using Microsoft.DataShaping.Processing.QueryExecution;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.DataPreparation
{
	// Token: 0x02000089 RID: 137
	internal sealed class TransformResultToResultSetFactory
	{
		// Token: 0x0600038C RID: 908 RVA: 0x0000BD34 File Offset: 0x00009F34
		internal static IResultSet CreateResultSet(ISchemaRow schema, IEnumerable<IDataRow> rows, ResultTable outputTable)
		{
			int[] array;
			Type[] array2;
			if (TransformResultToResultSetFactory.ShouldRemap(schema, outputTable, out array, out array2))
			{
				return new MappingEnumerableToResultSetAdapter(rows, new ResultSetSchema(array2), array);
			}
			return new EnumerableToResultSetAdapter(rows, new ResultSetSchema(array2));
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000BD68 File Offset: 0x00009F68
		internal static bool ShouldRemap(ISchemaRow schema, ResultTable outputTable, out int[] fieldIdxToRowColumnMapping, out Type[] types)
		{
			bool flag = false;
			fieldIdxToRowColumnMapping = new int[outputTable.Fields.Count];
			types = new Type[outputTable.Fields.Count];
			for (int i = 0; i < outputTable.Fields.Count; i++)
			{
				Field field = outputTable.Fields[i];
				int num = -1;
				IColumn column = null;
				if (field.TargetRole != null)
				{
					schema.TryGetRoleColumn(field.TargetRole, out column, out num);
				}
				else if (field.DataField != null)
				{
					schema.TryGetColumn(field.DataField, out column, out num);
				}
				Contract.RetailAssert(column != null, "Did not find a column for the field {0}", field.Id);
				Contract.RetailAssert(num != -1, "Did not find a column index for the field {0}", field.Id);
				flag |= num != i;
				fieldIdxToRowColumnMapping[i] = num;
				types[i] = column.DataType.ToType();
			}
			return flag;
		}
	}
}

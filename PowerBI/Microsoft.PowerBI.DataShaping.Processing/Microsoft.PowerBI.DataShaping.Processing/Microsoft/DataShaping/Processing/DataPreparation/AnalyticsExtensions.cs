using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Microsoft.DataShaping.Processing.Analytics;
using Microsoft.DataShaping.Processing.QueryExecution;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.DataPreparation
{
	// Token: 0x02000082 RID: 130
	internal static class AnalyticsExtensions
	{
		// Token: 0x06000348 RID: 840 RVA: 0x0000AC95 File Offset: 0x00008E95
		internal static Column ToColumn(this Field field, string role, Type dataType)
		{
			return new Column(field.Id, dataType.ToDataType(), role, field.SortInformation.ToSortInformation());
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000ACB4 File Offset: 0x00008EB4
		internal static Microsoft.DataShaping.Processing.Analytics.SortInformation ToSortInformation(this Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortInformation sortInfo)
		{
			if (sortInfo == null)
			{
				return null;
			}
			return new Microsoft.DataShaping.Processing.Analytics.SortInformation(sortInfo.SortIndex, sortInfo.SortDirection.ToSortDirection());
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000ACD1 File Offset: 0x00008ED1
		internal static Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortInformation ToSortInformation(this ISortInformation sortInfo)
		{
			if (sortInfo == null)
			{
				return null;
			}
			return new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortInformation(sortInfo.SortIndex, sortInfo.SortDirection.ToSortDirection());
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000ACEE File Offset: 0x00008EEE
		internal static Microsoft.PowerBI.Analytics.Contracts.SortDirection ToSortDirection(this Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortDirection sortDirection)
		{
			if (sortDirection == Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortDirection.Ascending)
			{
				return Microsoft.PowerBI.Analytics.Contracts.SortDirection.Ascending;
			}
			if (sortDirection != Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortDirection.Descending)
			{
				Contract.RetailFail("Unexpected SortDirection {0}", sortDirection);
				throw new InvalidOperationException("Not hit");
			}
			return Microsoft.PowerBI.Analytics.Contracts.SortDirection.Descending;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000AD17 File Offset: 0x00008F17
		internal static Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortDirection ToSortDirection(this Microsoft.PowerBI.Analytics.Contracts.SortDirection sortDirection)
		{
			if (sortDirection == Microsoft.PowerBI.Analytics.Contracts.SortDirection.Ascending)
			{
				return Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortDirection.Ascending;
			}
			if (sortDirection != Microsoft.PowerBI.Analytics.Contracts.SortDirection.Descending)
			{
				Contract.RetailFail("Unexpected SortDirection {0}", sortDirection);
				throw new InvalidOperationException("Not hit");
			}
			return Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.SortDirection.Descending;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000AD40 File Offset: 0x00008F40
		internal static DataType ToDataType(this Type type)
		{
			if (type == typeof(bool))
			{
				return DataType.Boolean;
			}
			if (type == typeof(DateTime))
			{
				return DataType.DateTime;
			}
			if (type == typeof(decimal))
			{
				return DataType.Decimal;
			}
			if (type == typeof(double))
			{
				return DataType.Double;
			}
			if (type == typeof(long))
			{
				return DataType.Int64;
			}
			if (type == typeof(string))
			{
				return DataType.String;
			}
			if (type == typeof(object))
			{
				return DataType.Variant;
			}
			Contract.RetailFail("Unexpected Type {0}", type);
			throw new InvalidOperationException("Not hit");
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000ADF0 File Offset: 0x00008FF0
		internal static Type ToType(this DataType dataType)
		{
			switch (dataType)
			{
			case DataType.Boolean:
				return typeof(bool);
			case DataType.DateTime:
				return typeof(DateTime);
			case DataType.Decimal:
				return typeof(decimal);
			case DataType.Double:
				return typeof(double);
			case DataType.Int64:
				return typeof(long);
			case DataType.String:
				return typeof(string);
			case DataType.Variant:
				return typeof(object);
			default:
				Contract.RetailFail("Unexpected DataType {0}", dataType);
				throw new InvalidOperationException("Not hit");
			}
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000AE88 File Offset: 0x00009088
		internal static Microsoft.PowerBI.Analytics.Contracts.DataTransformParameter ToParameter(this Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransformParameter param, IExpressionEvaluator<object> evaluator)
		{
			return new Microsoft.PowerBI.Analytics.Contracts.DataTransformParameter(param.Name, evaluator.Evaluate(param.Value));
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000AEA4 File Offset: 0x000090A4
		internal static SchemaTransformContext ToSchemaTransformContext(this DataTransform dataTransform, ResultSetSchema resultSetSchema, IExpressionEvaluator<object> evaluator)
		{
			DataTransformInput input = dataTransform.Input;
			DataTransformTableSchema schema = input.Schema;
			ResultTable table = input.Table;
			Column[] array = new Column[table.Fields.Count];
			for (int i = 0; i < array.Length; i++)
			{
				Field field = table.Fields[i];
				DataTransformColumn dataTransformColumn = schema.Columns.FirstOrDefault((DataTransformColumn c) => c.Name == field.Id);
				string text = ((dataTransformColumn != null) ? dataTransformColumn.Role : null);
				array[i] = field.ToColumn(text, resultSetSchema.GetColumnType(i));
			}
			ISchemaRow schemaRow = new SchemaRow(array);
			ReadOnlyCollection<Microsoft.PowerBI.Analytics.Contracts.DataTransformParameter> readOnlyCollection = ((input.Parameters == null) ? Util.EmptyReadOnlyCollection<Microsoft.PowerBI.Analytics.Contracts.DataTransformParameter>() : input.Parameters.Select((Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.DataTransformParameter p) => p.ToParameter(evaluator)).ToReadOnlyCollection<Microsoft.PowerBI.Analytics.Contracts.DataTransformParameter>());
			return new SchemaTransformContext(schemaRow, SchemaRowFactory.Instance, readOnlyCollection);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000AF98 File Offset: 0x00009198
		internal static DataTransformExecutionContext ToDataTransformContext(this DataTransform dataTransform, IEnumerable<IDataRow> inputRows, CancellationToken cancelToken, ResultSetSchema resultSetSchema, SchemaTransformContext schemaContext = null, IExpressionEvaluator<object> evaluator = null)
		{
			if (schemaContext == null)
			{
				schemaContext = dataTransform.ToSchemaTransformContext(resultSetSchema, evaluator);
			}
			return new DataTransformExecutionContext(inputRows, DataRowFactory.Instance, schemaContext.Parameters, schemaContext.Schema, cancelToken);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000AFC4 File Offset: 0x000091C4
		internal static ResultSetSchema ToResultSetSchema(this ISchemaRow schemaRow)
		{
			int count = schemaRow.Count;
			Type[] array = new Type[count];
			for (int i = 0; i < count; i++)
			{
				DataType dataType = schemaRow.GetColumn(i).DataType;
				array[i] = dataType.ToType();
			}
			return new ResultSetSchema(array);
		}
	}
}

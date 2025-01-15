using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.In4.AutoInsight.ComputationLibraries;
using Microsoft.InfoNav.Analytics.Outliers;
using Microsoft.InfoNav.Common;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x0200001B RID: 27
	internal sealed class OutlierDetector : DataTransform
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00003049 File Offset: 0x00001249
		internal OutlierDetector(ServiceRuntimeContext context)
			: base(context)
		{
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003054 File Offset: 0x00001254
		public override SchemaTransformResult GetSchema(SchemaTransformContext context)
		{
			return this.ServiceRuntimeContext.TelemetryService.RunInActivity<SchemaTransformResult>("OutlierDetectionGetSchema", () => this.GetSchemaCore(context));
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003098 File Offset: 0x00001298
		public override Task<DataTransformResult> ExecuteAsync(DataTransformExecutionContext context)
		{
			return Task.FromResult<DataTransformResult>(this.ServiceRuntimeContext.TelemetryService.RunInActivity<DataTransformResult>("OutlierDetectionExecute", () => this.ExecuteCore(context)));
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000030E0 File Offset: 0x000012E0
		private SchemaTransformResult GetSchemaCore(SchemaTransformContext context)
		{
			int num;
			int num2;
			OutlierDetector.ValidateInputSchema(context.Schema, out num, out num2);
			IColumn column = context.Schema.CreateColumn(context.Schema.MakeUniqueName(OutlierConstants.OutputColumnName, null), DataType.Boolean, OutlierConstants.OutputRoleName, null);
			return new SchemaTransformResult(context.Schema.AddColumns(column.ArrayWrap<IColumn>()));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003138 File Offset: 0x00001338
		private DataTransformResult ExecuteCore(DataTransformExecutionContext context)
		{
			int num;
			int num2;
			OutlierDetector.ValidateInputSchema(context.InputSchema, out num, out num2);
			DataType dataType = context.InputSchema.GetColumn(num).DataType;
			List<OutlierDataRow> list = new List<OutlierDataRow>();
			double num3 = double.MinValue;
			foreach (IDataRow dataRow in context.InputRows)
			{
				double? num4 = OutlierDetector.ConvertToDouble(dataRow, dataType, num);
				double? asDouble = dataRow.GetAsDouble(num2);
				if (num4 == null || asDouble == null)
				{
					throw new TransformException("Blank values are not supported");
				}
				if (num3 > num4.Value)
				{
					throw new TransformException("x-values must be sorted ascending");
				}
				OutlierDataRow outlierDataRow = new OutlierDataRow(dataRow, num4.Value, asDouble.Value);
				list.Add(outlierDataRow);
				num3 = num4.Value;
			}
			int count = list.Count;
			double[] array = new double[count];
			double[] array2 = new double[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = list[i].XValue;
				array2[i] = list[i].YValue;
			}
			int num5 = (int)((double)count * OutlierConstants.OutlierRatioLimit) + 1;
			CompositeTimeSeriesAnalyzer compositeTimeSeriesAnalyzer = new CompositeTimeSeriesAnalyzer(array, array2, 1.0, new int?(num5));
			compositeTimeSeriesAnalyzer.DetectOutliers();
			ReadOnlySet<int> readOnlySet = compositeTimeSeriesAnalyzer.UserPerceivedOutliers.ToReadOnlySet(null);
			IDataRow[] array3 = new IDataRow[count];
			for (int j = 0; j < count; j++)
			{
				array3[j] = list[j].SourceDataRow.AddColumn(readOnlySet.Contains(j));
			}
			return new DataTransformResult(array3);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000032F0 File Offset: 0x000014F0
		private static void ValidateInputSchema(ISchemaRow inputSchema, out int xValueIndex, out int yValueIndex)
		{
			xValueIndex = inputSchema.ValidateRequiredRole(OutlierConstants.XInputRoleName, (DataType t) => t.IsNumeric() || t == DataType.DateTime);
			yValueIndex = inputSchema.ValidateRequiredRole(OutlierConstants.YInputRoleName, (DataType t) => t.IsNumeric());
			inputSchema.ValidateAbsentRole(OutlierConstants.OutputRoleName);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003360 File Offset: 0x00001560
		private static double? ConvertToDouble(IDataRow dataRow, DataType dataType, int columnIndex)
		{
			if (dataType != DataType.DateTime)
			{
				if (dataType - DataType.Decimal <= 2)
				{
					return dataRow.GetAsDouble(columnIndex);
				}
				throw new TransformException("Unexpected column data type");
			}
			else
			{
				object @object = dataRow.GetObject(columnIndex);
				if (!(@object is DateTime))
				{
					return null;
				}
				return new double?((double)((DateTime)@object).Ticks);
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000033B8 File Offset: 0x000015B8
		private static double GetNormalizationFactor(IReadOnlyList<OutlierDataRow> bufferedRows, out double normalizationOffset)
		{
			int count = bufferedRows.Count;
			if (count == 0)
			{
				normalizationOffset = 0.0;
				return 0.0;
			}
			normalizationOffset = bufferedRows[0].XValue;
			double num = bufferedRows[count - 1].XValue - normalizationOffset;
			if (num == 0.0)
			{
				return 0.0;
			}
			return (double)(count - 1) / num;
		}
	}
}

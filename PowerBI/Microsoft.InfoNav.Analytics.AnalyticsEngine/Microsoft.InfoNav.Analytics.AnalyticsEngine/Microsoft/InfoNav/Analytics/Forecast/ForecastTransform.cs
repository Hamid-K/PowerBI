using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.InfoNav.Analytics.TimeSeriesPreProcessing;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics.Forecast
{
	// Token: 0x02000033 RID: 51
	internal class ForecastTransform : DataTransform
	{
		// Token: 0x060000DF RID: 223 RVA: 0x000059D8 File Offset: 0x00003BD8
		internal ForecastTransform(ServiceRuntimeContext context, float defaultConfidence = 0.95f)
			: base(context)
		{
			this._defaultConfidence = defaultConfidence;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000059E8 File Offset: 0x00003BE8
		public override SchemaTransformResult GetSchema(SchemaTransformContext context)
		{
			return this.ServiceRuntimeContext.TelemetryService.RunInActivity<SchemaTransformResult>("ForecastGetSchema", () => ForecastTransform.GetSchemaCore(context));
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005A24 File Offset: 0x00003C24
		public override Task<DataTransformResult> ExecuteAsync(DataTransformExecutionContext context)
		{
			return Task.FromResult<DataTransformResult>(this.ServiceRuntimeContext.TelemetryService.RunInActivity<DataTransformResult>("ForecastExecute", delegate
			{
				ForecastStatistics forecastStatistics = new ForecastStatistics();
				Stopwatch stopwatch = new Stopwatch();
				stopwatch.Start();
				DataTransformResult dataTransformResult;
				try
				{
					dataTransformResult = this.ExecuteCore(context, forecastStatistics);
				}
				catch (TransformException ex)
				{
					forecastStatistics.Exception = ex;
					throw;
				}
				finally
				{
					stopwatch.Stop();
					forecastStatistics.Delay = new int?((int)stopwatch.Elapsed.TotalMilliseconds);
					forecastStatistics.FireTelemetryEvent(this.ServiceRuntimeContext.TelemetryService);
				}
				return dataTransformResult;
			}));
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005A6C File Offset: 0x00003C6C
		private DataTransformResult ExecuteCore(DataTransformExecutionContext context, ForecastStatistics forecastStatistics)
		{
			int num;
			int num2;
			TransformException ex;
			if (!ForecastTransform.ValidateInputSchema(context.InputSchema, out num, out num2, out ex))
			{
				List<IDataRow> list = new List<IDataRow>();
				foreach (IDataRow dataRow in context.InputRows)
				{
					list.Add(dataRow);
				}
				DataTransformMessage dataTransformMessage = new DataTransformMessage(ForecastErrorType.InvalidDataType.ToErrorCode(), DataTransformMessageSeverity.Warning, ex.Message);
				return DataTransformResultFactory.OriginalResultWithWarnings(list, DataPostprocessor.EmptyForecastResult, dataTransformMessage.ArrayWrap<DataTransformMessage>());
			}
			ForecastContext forecastContext = ForecastContext.Create(context, num, num2, this._defaultConfidence);
			DataPreprocessor dataPreprocessor = new DataPreprocessor(forecastStatistics);
			List<double> list3;
			List<double> list4;
			double num3;
			double num4;
			bool flag;
			List<IDataRow> list2 = dataPreprocessor.ReadAndValidateDataRows(forecastContext, out list3, out list4, out num3, out num4, out flag);
			DataTransformResult dataTransformResult;
			try
			{
				if (flag)
				{
					throw new TransformException("x-values must be not null", ForecastErrorType.XAxisIsNull.ToErrorCode(), ErrorSource.PowerBI);
				}
				if (list3.IsNullOrEmpty<double>() || list4.IsNullOrEmpty<double>())
				{
					throw new TransformException("Input data rows must not be empty", ForecastErrorType.DataIsTooSmall.ToErrorCode(), ErrorSource.PowerBI);
				}
				if (forecastContext.ParameterUnit.IsDate() && forecastContext.XColumnDataType != DataType.DateTime)
				{
					throw new TransformException("Forecast Unit is date time but the x axis is not date time", ForecastErrorType.Unexpected.ToErrorCode(), ErrorSource.PowerBI);
				}
				DataPreprocessResult dataPreprocessResult = dataPreprocessor.Preprocess(list2, list3, list4, num3, num4, forecastContext);
				ForecastResult forecastResult = ForecasterFactory.CreateForecaster(forecastStatistics).Forecast(forecastContext, dataPreprocessResult);
				DataPostprocessor dataPostprocessor = new DataPostprocessor(forecastContext, dataPreprocessResult);
				IReadOnlyList<DataTransformMessage> warnings = forecastStatistics.GetWarnings();
				dataTransformResult = dataPostprocessor.Postprocess(forecastResult, warnings);
			}
			catch (TransformException ex2)
			{
				DataTransformMessage dataTransformMessage2;
				if (string.IsNullOrWhiteSpace(ex2.ErrorCode))
				{
					dataTransformMessage2 = new DataTransformMessage(ForecastErrorType.Unexpected.ToErrorCode(), DataTransformMessageSeverity.Warning, ex2.Message);
				}
				else
				{
					dataTransformMessage2 = new DataTransformMessage(ex2.ErrorCode, DataTransformMessageSeverity.Warning, ex2.Message);
				}
				forecastStatistics.AddHandledError(ex2, null);
				dataTransformResult = DataTransformResultFactory.OriginalResultWithWarnings(list2, DataPostprocessor.EmptyForecastResult, dataTransformMessage2.ArrayWrap<DataTransformMessage>());
			}
			return dataTransformResult;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00005C40 File Offset: 0x00003E40
		private static SchemaTransformResult GetSchemaCore(SchemaTransformContext context)
		{
			ISchemaRow schema = context.Schema;
			int num;
			int num2;
			TransformException ex;
			ForecastTransform.ValidateInputSchema(schema, out num, out num2, out ex);
			DataType dataType = schema.GetColumn(num2).DataType;
			IColumn column = schema.CreateColumn(schema.MakeUniqueName("Forecast", null), dataType, "Forecast", null);
			IColumn column2 = schema.CreateColumn(schema.MakeUniqueName("LowerBound", null), dataType, "LowerBound", null);
			IColumn column3 = schema.CreateColumn(schema.MakeUniqueName("UpperBound", null), dataType, "UpperBound", null);
			return new SchemaTransformResult(schema.AddColumns(new IColumn[] { column, column2, column3 }));
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005CE0 File Offset: 0x00003EE0
		private static bool ValidateInputSchema(ISchemaRow inputSchema, out int xValueIndex, out int yValueIndex, out TransformException exception)
		{
			exception = null;
			xValueIndex = inputSchema.ValidateRequiredRole("X", null);
			yValueIndex = inputSchema.ValidateRequiredRole("Y", null);
			inputSchema.ValidateAbsentRole("Forecast");
			inputSchema.ValidateAbsentRole("LowerBound");
			inputSchema.ValidateAbsentRole("UpperBound");
			bool flag;
			try
			{
				inputSchema.ValidateColumnsHasValidType("X", xValueIndex.ArrayWrap<int>(), (DataType t) => t.IsSupportedXAxisType());
				inputSchema.ValidateColumnsHasValidType("Y", yValueIndex.ArrayWrap<int>(), (DataType t) => t.IsNumeric() || t == DataType.Variant);
				flag = true;
			}
			catch (TransformException ex)
			{
				exception = ex;
				flag = false;
			}
			return flag;
		}

		// Token: 0x04000113 RID: 275
		private readonly float _defaultConfidence;
	}
}

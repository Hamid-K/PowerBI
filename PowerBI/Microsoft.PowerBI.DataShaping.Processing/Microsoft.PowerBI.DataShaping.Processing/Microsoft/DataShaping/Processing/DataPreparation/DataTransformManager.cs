using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.Processing.Analytics;
using Microsoft.DataShaping.Processing.QueryExecution;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition;
using Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Expressions;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.PowerBI.Analytics.Contracts;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.Processing.DataPreparation
{
	// Token: 0x02000084 RID: 132
	internal sealed class DataTransformManager : IDataTransformManager
	{
		// Token: 0x06000355 RID: 853 RVA: 0x0000B0BA File Offset: 0x000092BA
		internal DataTransformManager(Microsoft.DataShaping.ServiceContracts.ITracer tracer, Microsoft.DataShaping.ServiceContracts.ITelemetryService telemetryService, IDataTransformPluginFactory transformFactory, IReadOnlyList<DataTransform> dataTransforms, CancellationToken cancelToken, IExpressionEvaluator<object> evaluator)
		{
			this._tracer = tracer;
			this._telemetryService = telemetryService;
			this._transformFactory = transformFactory;
			this._dataTransforms = dataTransforms;
			this._cancelToken = cancelToken;
			this._evaluator = evaluator;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000B0EF File Offset: 0x000092EF
		public int GetInputResultSetIndex(ResultTableLookupInfo tableLookupInfo)
		{
			return this._dataTransforms[tableLookupInfo.DataTransformIndex].Input.TableIndex;
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000B10C File Offset: 0x0000930C
		public async Task<IResultSet> GetResultSetAsync(IResultSet input, ResultTableLookupInfo tableLookupInfo)
		{
			ResultSetSchema schema = input.Schema;
			ResultSetEnumerable resultSetEnumerable = new ResultSetEnumerable(input);
			DataTransform dataTransform = this._dataTransforms[tableLookupInfo.DataTransformIndex];
			SchemaTransformContext schemaTransformContext = dataTransform.ToSchemaTransformContext(schema, this._evaluator);
			DataTransformExecutionContext dataTransformExecutionContext = dataTransform.ToDataTransformContext(resultSetEnumerable, this._cancelToken, schema, schemaTransformContext, null);
			Tuple<DataTransformResult, SchemaTransformResult> tuple = await this.ExecuteTransform(dataTransform, schemaTransformContext, dataTransformExecutionContext);
			this.ProcessMessages(tuple.Item1.Messages);
			return TransformResultToResultSetFactory.CreateResultSet(tuple.Item2.Schema, tuple.Item1.Rows, dataTransform.Output.Table);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000B15F File Offset: 0x0000935F
		public IReadOnlyList<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Message> GetMessages()
		{
			return this._messages;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000B168 File Offset: 0x00009368
		private async Task<Tuple<DataTransformResult, SchemaTransformResult>> ExecuteTransform(DataTransform dataTransform, SchemaTransformContext schemaTransformContext, DataTransformExecutionContext dataTransformContext)
		{
			DataTransformManager.<>c__DisplayClass11_0 CS$<>8__locals1 = new DataTransformManager.<>c__DisplayClass11_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.dataTransform = dataTransform;
			CS$<>8__locals1.schemaTransformContext = schemaTransformContext;
			CS$<>8__locals1.dataTransformContext = dataTransformContext;
			return await this._telemetryService.RunInActivity<Task<Tuple<DataTransformResult, SchemaTransformResult>>>(ActivityKind.DataTransformExecution, delegate
			{
				DataTransformManager.<>c__DisplayClass11_0.<<ExecuteTransform>b__0>d <<ExecuteTransform>b__0>d;
				<<ExecuteTransform>b__0>d.<>t__builder = AsyncTaskMethodBuilder<Tuple<DataTransformResult, SchemaTransformResult>>.Create();
				<<ExecuteTransform>b__0>d.<>4__this = CS$<>8__locals1;
				<<ExecuteTransform>b__0>d.<>1__state = -1;
				<<ExecuteTransform>b__0>d.<>t__builder.Start<DataTransformManager.<>c__DisplayClass11_0.<<ExecuteTransform>b__0>d>(ref <<ExecuteTransform>b__0>d);
				return <<ExecuteTransform>b__0>d.<>t__builder.Task;
			});
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000B1C4 File Offset: 0x000093C4
		private async Task<Tuple<DataTransformResult, SchemaTransformResult>> ExecuteTransformImpl(IDataTransformPlugin plugin, DataTransform dataTransform, SchemaTransformContext schemaTransformContext, DataTransformExecutionContext dataTransformContext)
		{
			DataTransformManager.<>c__DisplayClass12_0 CS$<>8__locals1 = new DataTransformManager.<>c__DisplayClass12_0();
			CS$<>8__locals1.schemaTransformContext = schemaTransformContext;
			CS$<>8__locals1.dataTransformContext = dataTransformContext;
			CS$<>8__locals1.schemaTransform = plugin.CreateMetadataTransform();
			Contract.RetailAssert(CS$<>8__locals1.schemaTransform != null, "Found null metadata transform for {0} aglorithm", dataTransform.Algorithm);
			SchemaTransformResult schemaResult = this._telemetryService.RunInActivity<SchemaTransformResult>(ActivityKind.TransformSchema, () => CS$<>8__locals1.schemaTransform.GetSchema(CS$<>8__locals1.schemaTransformContext));
			DataTransformManager.ValidateAndReconcileTransformOutput(dataTransform.Output, schemaResult, dataTransform.Algorithm);
			CS$<>8__locals1.transform = plugin.CreateDataTransform();
			Contract.RetailAssert(CS$<>8__locals1.transform != null, "Found null data transform for {0} aglorithm", dataTransform.Algorithm);
			TaskAwaiter<DataTransformResult> taskAwaiter = this._telemetryService.RunInActivity<Task<DataTransformResult>>(ActivityKind.TransformData, delegate
			{
				DataTransformManager.<>c__DisplayClass12_0.<<ExecuteTransformImpl>b__1>d <<ExecuteTransformImpl>b__1>d;
				<<ExecuteTransformImpl>b__1>d.<>t__builder = AsyncTaskMethodBuilder<DataTransformResult>.Create();
				<<ExecuteTransformImpl>b__1>d.<>4__this = CS$<>8__locals1;
				<<ExecuteTransformImpl>b__1>d.<>1__state = -1;
				<<ExecuteTransformImpl>b__1>d.<>t__builder.Start<DataTransformManager.<>c__DisplayClass12_0.<<ExecuteTransformImpl>b__1>d>(ref <<ExecuteTransformImpl>b__1>d);
				return <<ExecuteTransformImpl>b__1>d.<>t__builder.Task;
			}).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<DataTransformResult> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<DataTransformResult>);
			}
			return new Tuple<DataTransformResult, SchemaTransformResult>(taskAwaiter.GetResult(), schemaResult);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000B228 File Offset: 0x00009428
		private static void ValidateAndReconcileTransformOutput(DataTransformOutput transformOutput, SchemaTransformResult schemaResult, string transformAlgorithm)
		{
			ResultTable table = transformOutput.Table;
			ISchemaRow schema = schemaResult.Schema;
			foreach (Field field in table.Fields)
			{
				IColumn column;
				if (field.TargetRole != null)
				{
					int num;
					if (!schema.TryGetRoleColumn(field.TargetRole, out column, out num))
					{
						string text = StringUtil.FormatInvariant("DataTransform '{0}' did not return the expected Role column '{1}'.", new object[] { transformAlgorithm, field.TargetRole });
						throw new DataTransformException("DataTransformInvalidOutputError", text, null, Microsoft.PowerBI.Query.Contracts.ErrorSource.PowerBI);
					}
				}
				else
				{
					if (field.DataField == null)
					{
						Contract.RetailFail("Unexpected field that doesn't have a role or data field - id {0}.", field.Id);
						throw new InvalidOperationException();
					}
					int num;
					if (!schema.TryGetColumn(field.DataField, out column, out num))
					{
						string text2 = StringUtil.FormatInvariant("DataTransform '{0}' did not return the expected column '{1}'.", new object[] { transformAlgorithm, field.DataField });
						throw new DataTransformException("DataTransformInvalidOutputError", text2, null, Microsoft.PowerBI.Query.Contracts.ErrorSource.PowerBI);
					}
				}
				field.SortInformation = column.SortInformation.ToSortInformation();
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000B33C File Offset: 0x0000953C
		private static string GetErrorCode(Exception exception)
		{
			TransformException ex = exception as TransformException;
			if (ex == null)
			{
				return "UnexpectedDataTransformError";
			}
			if (ex.ErrorCode == null)
			{
				return "DataTransformError";
			}
			return ex.ErrorCode;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000B36D File Offset: 0x0000956D
		private static Microsoft.PowerBI.Query.Contracts.ErrorSource ToErrorSource(Microsoft.PowerBI.Analytics.Contracts.ErrorSource errorSource)
		{
			switch (errorSource)
			{
			case Microsoft.PowerBI.Analytics.Contracts.ErrorSource.PowerBI:
				return Microsoft.PowerBI.Query.Contracts.ErrorSource.PowerBI;
			case Microsoft.PowerBI.Analytics.Contracts.ErrorSource.External:
				return Microsoft.PowerBI.Query.Contracts.ErrorSource.External;
			case Microsoft.PowerBI.Analytics.Contracts.ErrorSource.User:
				return Microsoft.PowerBI.Query.Contracts.ErrorSource.User;
			default:
				Contract.RetailFail("Invalid ErrorSource " + errorSource.ToString());
				return Microsoft.PowerBI.Query.Contracts.ErrorSource.Unknown;
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000B3A8 File Offset: 0x000095A8
		private void ProcessMessages(IReadOnlyList<DataTransformMessage> transformMessages)
		{
			if (transformMessages.IsNullOrEmpty<DataTransformMessage>())
			{
				return;
			}
			if (this._messages == null)
			{
				this._messages = new List<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Message>();
			}
			foreach (DataTransformMessage dataTransformMessage in transformMessages)
			{
				this._messages.Add(new Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Message(dataTransformMessage.Code, dataTransformMessage.Severity.ToString(), dataTransformMessage.Message, null, null, null));
			}
		}

		// Token: 0x040001DD RID: 477
		private readonly Microsoft.DataShaping.ServiceContracts.ITracer _tracer;

		// Token: 0x040001DE RID: 478
		private readonly IDataTransformPluginFactory _transformFactory;

		// Token: 0x040001DF RID: 479
		private readonly Microsoft.DataShaping.ServiceContracts.ITelemetryService _telemetryService;

		// Token: 0x040001E0 RID: 480
		private readonly IReadOnlyList<DataTransform> _dataTransforms;

		// Token: 0x040001E1 RID: 481
		private readonly CancellationToken _cancelToken;

		// Token: 0x040001E2 RID: 482
		private readonly IExpressionEvaluator<object> _evaluator;

		// Token: 0x040001E3 RID: 483
		private List<Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition.Message> _messages;
	}
}

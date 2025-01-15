using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ProcessingRenderingCommon;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000800 RID: 2048
	internal abstract class RuntimeDataSet : RuntimeLiveQueryExecutor
	{
		// Token: 0x06007212 RID: 29202 RVA: 0x001D9C8C File Offset: 0x001D7E8C
		internal RuntimeDataSet(Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, DataSetInstance dataSetInstance, OnDemandProcessingContext odpContext, bool processRetrievedData)
			: base(dataSource, dataSet, odpContext)
		{
			this.m_dataSetInstance = dataSetInstance;
			this.m_processRetrievedData = processRetrievedData;
			if (this.m_odpContext.QueryRestartInfo == null)
			{
				this.m_restartPosition = null;
				return;
			}
			this.m_restartPosition = this.m_odpContext.QueryRestartInfo.GetRestartPositionForDataSet(this.m_dataSet);
		}

		// Token: 0x170026B6 RID: 9910
		// (get) Token: 0x06007213 RID: 29203 RVA: 0x001D9CEA File Offset: 0x001D7EEA
		internal virtual bool ProcessFromLiveDataReader
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170026B7 RID: 9911
		// (get) Token: 0x06007214 RID: 29204 RVA: 0x001D9CED File Offset: 0x001D7EED
		internal bool NoRows
		{
			get
			{
				return this.m_dataRowsRead <= 0;
			}
		}

		// Token: 0x170026B8 RID: 9912
		// (get) Token: 0x06007215 RID: 29205 RVA: 0x001D9CFB File Offset: 0x001D7EFB
		internal int NumRowsRead
		{
			get
			{
				return this.m_dataRowsRead;
			}
		}

		// Token: 0x170026B9 RID: 9913
		// (get) Token: 0x06007216 RID: 29206 RVA: 0x001D9D03 File Offset: 0x001D7F03
		internal bool UsedOnlyInParameters
		{
			get
			{
				return this.m_dataSet.UsedOnlyInParameters;
			}
		}

		// Token: 0x170026BA RID: 9914
		// (get) Token: 0x06007217 RID: 29207 RVA: 0x001D9D10 File Offset: 0x001D7F10
		protected virtual bool WritesDataChunk
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170026BB RID: 9915
		// (get) Token: 0x06007218 RID: 29208 RVA: 0x001D9D13 File Offset: 0x001D7F13
		protected bool ProcessRetrievedData
		{
			get
			{
				return this.m_processRetrievedData;
			}
		}

		// Token: 0x170026BC RID: 9916
		// (get) Token: 0x06007219 RID: 29209 RVA: 0x001D9D1B File Offset: 0x001D7F1B
		protected bool HasServerAggregateMetadata
		{
			get
			{
				return this.m_dataSet.HasAggregateIndicatorFields || (this.m_dataReader != null && this.m_dataReader.ReaderExtensionsSupported);
			}
		}

		// Token: 0x0600721A RID: 29210 RVA: 0x001D9D41 File Offset: 0x001D7F41
		internal void InitProcessingParams(IDbConnection conn, ReportProcessing.TransactionInfo transInfo)
		{
			this.m_dataSourceConnection = conn;
			this.m_transInfo = transInfo;
		}

		// Token: 0x0600721B RID: 29211 RVA: 0x001D9D54 File Offset: 0x001D7F54
		protected virtual void InitializeDataSet()
		{
			this.m_odpContext.EnsureCultureIsSetOnCurrentThread();
			if (DataSetValidator.LOCALE_SYSTEM_DEFAULT == this.m_dataSet.LCID)
			{
				if (this.m_odpContext.ShouldExecuteLiveQueries)
				{
					this.m_dataSet.LCID = DataSetValidator.LCIDfromRDLCollation(this.m_dataSet.Collation);
				}
				else
				{
					this.m_dataSet.LCID = this.m_dataSetInstance.LCID;
				}
			}
			this.m_isConnectionOwner = false;
			this.InitRuntime();
		}

		// Token: 0x0600721C RID: 29212 RVA: 0x001D9DCC File Offset: 0x001D7FCC
		private void InitRuntime()
		{
			Global.Tracer.Assert(this.m_odpContext.ReportObjectModel != null && this.m_odpContext.ReportRuntime != null);
			if (this.m_odpContext.ReportRuntime.ReportExprHost != null)
			{
				this.m_dataSet.SetExprHost(this.m_odpContext.ReportRuntime.ReportExprHost, this.m_odpContext.ReportObjectModel);
			}
		}

		// Token: 0x0600721D RID: 29213 RVA: 0x001D9E39 File Offset: 0x001D8039
		protected virtual void TeardownDataSet()
		{
			this.m_odpContext.CheckAndThrowIfAborted();
			if (this.NoRows)
			{
				this.m_dataSet.MarkDataRegionsAsNoRows();
			}
		}

		// Token: 0x0600721E RID: 29214 RVA: 0x001D9E59 File Offset: 0x001D8059
		protected virtual void FinalCleanup()
		{
			this.CleanupDataReader();
			base.CloseConnection();
			if (this.m_odpContext.ExecutionLogContext != null)
			{
				this.m_odpContext.ExecutionLogContext.AddDataSetMetrics(this.m_dataSet.Name, this.m_executionMetrics);
			}
		}

		// Token: 0x0600721F RID: 29215 RVA: 0x001D9E95 File Offset: 0x001D8095
		protected virtual void CleanupForException()
		{
			if (this.m_transInfo != null)
			{
				this.m_transInfo.RollbackRequired = true;
			}
		}

		// Token: 0x06007220 RID: 29216 RVA: 0x001D9EAC File Offset: 0x001D80AC
		protected virtual void CleanupDataReader()
		{
			this.m_executionMetrics.AddRowCount((long)this.m_dataRowsRead);
			if (this.m_odpContext.DataSetRetrievalComplete != null)
			{
				this.m_odpContext.DataSetRetrievalComplete[this.m_dataSet.IndexInCollection] = true;
			}
			if (this.m_dataSet.IsReferenceToSharedDataSet)
			{
				this.m_dataSetInstance.RecordSetSize = this.NumRowsRead;
			}
			this.CleanupCommandAndDataReader();
		}

		// Token: 0x06007221 RID: 29217
		protected abstract void InitializeBeforeProcessingRows(bool aReaderExtensionsSupported);

		// Token: 0x06007222 RID: 29218 RVA: 0x001D9F14 File Offset: 0x001D8114
		protected void PopulateFieldsWithReaderFlags()
		{
			if (this.m_dataReader != null)
			{
				this.m_odpContext.ReportObjectModel.FieldsImpl.ReaderExtensionsSupported = this.HasServerAggregateMetadata;
				this.m_odpContext.ReportObjectModel.FieldsImpl.ReaderFieldProperties = this.m_dataReader.ReaderFieldProperties;
			}
		}

		// Token: 0x170026BD RID: 9917
		// (get) Token: 0x06007223 RID: 29219 RVA: 0x001D9F64 File Offset: 0x001D8164
		protected virtual bool ShouldCancelCommandDuringCleanup
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007224 RID: 29220 RVA: 0x001D9F67 File Offset: 0x001D8167
		protected virtual void CleanupProcess()
		{
			if (!this.m_dataSet.IsReferenceToSharedDataSet)
			{
				this.CleanupCommandAndDataReader();
			}
		}

		// Token: 0x06007225 RID: 29221 RVA: 0x001D9F7C File Offset: 0x001D817C
		private void CleanupCommandAndDataReader()
		{
			if (this.m_dataSet.IsReferenceToSharedDataSet && this.ProcessFromLiveDataReader)
			{
				return;
			}
			try
			{
				if (this.ShouldCancelCommandDuringCleanup && !this.m_allDataRowsRead)
				{
					base.CancelCommand();
				}
				this.DisposeDataReader();
			}
			finally
			{
				base.DisposeCommand();
			}
		}

		// Token: 0x06007226 RID: 29222 RVA: 0x001D9FD8 File Offset: 0x001D81D8
		private void DisposeDataReader()
		{
			base.DisposeDataExtensionObject<IProcessingDataReader>(ref this.m_dataReader, "data reader", new DataProcessingMetrics.MetricType?(DataProcessingMetrics.MetricType.DisposeDataReader));
		}

		// Token: 0x06007227 RID: 29223
		protected abstract void ProcessExtendedPropertyMappings();

		// Token: 0x06007228 RID: 29224 RVA: 0x001D9FF1 File Offset: 0x001D81F1
		protected virtual void InitializeBeforeFirstRow(bool hasRows)
		{
			if (hasRows && !this.m_dataSet.IsReferenceToSharedDataSet)
			{
				this.MapExtendedProperties();
				this.ProcessExtendedPropertyMappings();
			}
		}

		// Token: 0x06007229 RID: 29225 RVA: 0x001DA010 File Offset: 0x001D8210
		protected Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow ReadOneRow(out int rowIndex)
		{
			if (this.m_dataRowsRead.IsMultipleOf(100))
			{
				Microsoft.ReportingServices.Diagnostics.ProcessingContext.DelayUntilResourcesAvailableBlocking();
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow recordRow = null;
			rowIndex = -1;
			if (this.m_allDataRowsRead)
			{
				return recordRow;
			}
			do
			{
				bool flag = this.m_dataReader != null && this.m_dataReader.GetNextRow();
				if (this.m_dataRowsRead == 0)
				{
					this.InitializeBeforeFirstRow(flag);
				}
				if (flag)
				{
					this.m_odpContext.CheckAndThrowIfAborted();
					recordRow = this.ReadRow();
					rowIndex = this.m_dataRowsRead;
					this.IncrementRowCounterAndTrace();
				}
				else
				{
					recordRow = null;
					this.m_allDataRowsRead = true;
				}
			}
			while (!this.m_allDataRowsRead && this.m_restartPosition != null && this.m_restartPosition.ShouldSkip(this.m_odpContext, recordRow));
			if (this.m_restartPosition != null)
			{
				this.m_restartPosition.DisableRowSkipping(recordRow);
			}
			return recordRow;
		}

		// Token: 0x0600722A RID: 29226 RVA: 0x001DA0D0 File Offset: 0x001D82D0
		protected void IncrementRowCounterAndTrace()
		{
			this.m_dataRowsRead++;
			if (Global.Tracer.TraceVerbose && this.m_dataRowsRead % 100000 == 0)
			{
				Global.Tracer.Trace(TraceLevel.Verbose, "Read data row: {0}", new object[] { this.m_dataRowsRead });
			}
		}

		// Token: 0x0600722B RID: 29227 RVA: 0x001DA12C File Offset: 0x001D832C
		private void MapExtendedProperties()
		{
			if (!this.m_dataReader.ReaderFieldProperties)
			{
				return;
			}
			int count = this.m_dataSet.Fields.Count;
			for (int i = 0; i < count; i++)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Field field = this.m_dataSet.Fields[i];
				if (!field.IsCalculatedField)
				{
					try
					{
						int propertyCount = this.m_dataReader.GetPropertyCount(i);
						List<int> list = new List<int>();
						List<string> list2 = new List<string>();
						for (int j = 0; j < propertyCount; j++)
						{
							string text = null;
							try
							{
								text = this.m_dataReader.GetPropertyName(i, j);
								list.Add(j);
								list2.Add(text);
							}
							catch (ReportProcessingException_FieldError reportProcessingException_FieldError)
							{
								this.m_odpContext.ErrorContext.Register(ProcessingErrorCode.rsErrorReadingFieldProperty, Severity.Warning, Microsoft.ReportingServices.ReportProcessing.ObjectType.DataSet, this.m_dataSet.Name, "FieldExtendedProperty", new string[] { field.Name, text, reportProcessingException_FieldError.Message });
							}
						}
						if (list.Count > 0)
						{
							if (this.m_dataSetInstance.FieldInfos == null)
							{
								this.m_dataSetInstance.FieldInfos = new FieldInfo[count];
							}
							this.m_dataSetInstance.FieldInfos[i] = new FieldInfo(list, list2);
						}
					}
					catch (ReportProcessingException_FieldError reportProcessingException_FieldError2)
					{
						this.HandleFieldError(reportProcessingException_FieldError2, i, field.Name);
					}
				}
			}
		}

		// Token: 0x0600722C RID: 29228 RVA: 0x001DA298 File Offset: 0x001D8498
		private Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow ReadRow()
		{
			int count = this.m_dataSet.Fields.Count;
			Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow recordRow = this.m_dataReader.GetUnderlyingRecordRowObject();
			if (recordRow != null)
			{
				return recordRow;
			}
			this.m_executionMetrics.StartTotalTimer();
			recordRow = this.ConstructRecordRow();
			this.m_executionMetrics.RecordTotalTimerMeasurement();
			return recordRow;
		}

		// Token: 0x0600722D RID: 29229 RVA: 0x001DA2E8 File Offset: 0x001D84E8
		private Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow ConstructRecordRow()
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow recordRow = new Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow();
			bool flag = this.m_dataReader.ReaderExtensionsSupported && !this.m_dataSet.HasAggregateIndicatorFields;
			bool flag2 = this.HasServerAggregateMetadata && (this.m_dataSet.InterpretSubtotalsAsDetails == Microsoft.ReportingServices.ReportIntermediateFormat.DataSet.TriState.False || (this.m_odpContext.IsSharedDataSetExecutionOnly && this.m_dataSet.InterpretSubtotalsAsDetails == Microsoft.ReportingServices.ReportIntermediateFormat.DataSet.TriState.Auto));
			Microsoft.ReportingServices.ReportIntermediateFormat.RecordField[] array = new Microsoft.ReportingServices.ReportIntermediateFormat.RecordField[this.m_dataSet.NonCalculatedFieldCount];
			recordRow.RecordFields = array;
			for (int i = 0; i < array.Length; i++)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Field field = this.m_dataSet.Fields[i];
				if (!this.m_dataSetInstance.IsFieldMissing(i))
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.RecordField recordField = new Microsoft.ReportingServices.ReportIntermediateFormat.RecordField();
					try
					{
						array[i] = recordField;
						recordField.FieldValue = this.m_dataReader.GetColumn(i);
						if (flag2)
						{
							if (flag)
							{
								recordField.IsAggregationField = this.m_dataReader.IsAggregationField(i);
							}
						}
						else
						{
							recordField.IsAggregationField = true;
						}
						recordField.FieldStatus = DataFieldStatus.None;
					}
					catch (ReportProcessingException_FieldError reportProcessingException_FieldError)
					{
						recordField = this.HandleFieldError(reportProcessingException_FieldError, i, field.Name);
						array[i] = recordField;
						if (recordField != null && !flag2)
						{
							recordField.IsAggregationField = true;
						}
					}
					this.ReadExtendedPropertiesForRecordField(i, field, recordField);
				}
				else
				{
					array[i] = null;
				}
			}
			if (flag2)
			{
				if (flag)
				{
					recordRow.IsAggregateRow = this.m_dataReader.IsAggregateRow;
					recordRow.AggregationFieldCount = this.m_dataReader.AggregationFieldCount;
				}
				else
				{
					this.PopulateServerAggregateInformationFromIndicatorFields(recordRow);
				}
			}
			else
			{
				recordRow.AggregationFieldCount = this.m_dataSet.Fields.Count;
			}
			return recordRow;
		}

		// Token: 0x0600722E RID: 29230 RVA: 0x001DA494 File Offset: 0x001D8694
		private void PopulateServerAggregateInformationFromIndicatorFields(Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow recordRow)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < recordRow.RecordFields.Length; i++)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.RecordField recordField = recordRow.RecordFields[i];
				Microsoft.ReportingServices.ReportIntermediateFormat.Field field = this.m_dataSet.Fields[i];
				if (recordField != null && field.HasAggregateIndicatorField)
				{
					num++;
					Microsoft.ReportingServices.ReportIntermediateFormat.Field field2 = this.m_dataSet.Fields[field.AggregateIndicatorFieldIndex];
					bool flag = false;
					bool flag2;
					if (field2.IsCalculatedField)
					{
						if (field2.Value.Type == Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo.Types.Constant)
						{
							flag = field2.Value.BoolValue;
							flag2 = false;
						}
						else
						{
							flag2 = !Microsoft.ReportingServices.RdlExpressions.ReportRuntime.TryProcessObjectToBoolean(field2.Value.LiteralInfo.Value, out flag);
						}
					}
					else
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.RecordField recordField2 = recordRow.RecordFields[field.AggregateIndicatorFieldIndex];
						flag2 = recordField2 == null || recordField2.FieldStatus != DataFieldStatus.None || !Microsoft.ReportingServices.RdlExpressions.ReportRuntime.TryProcessObjectToBoolean(recordField2.FieldValue, out flag);
					}
					if (flag2)
					{
						this.m_odpContext.ErrorContext.Register(ProcessingErrorCode.rsMissingOrInvalidAggregateIndicatorFieldValue, Severity.Warning, Microsoft.ReportingServices.ReportProcessing.ObjectType.Field, field2.Name, "AggregateIndicatorField", new string[]
						{
							this.m_dataSet.Name,
							field.Name
						});
					}
					else if (flag)
					{
						num2++;
						recordRow.IsAggregateRow = true;
					}
					recordField.IsAggregationField = !flag;
				}
			}
			recordRow.AggregationFieldCount = num - num2;
		}

		// Token: 0x0600722F RID: 29231 RVA: 0x001DA5F8 File Offset: 0x001D87F8
		private void ReadExtendedPropertiesForRecordField(int fieldIndex, Microsoft.ReportingServices.ReportIntermediateFormat.Field fieldDef, Microsoft.ReportingServices.ReportIntermediateFormat.RecordField field)
		{
			if (this.m_dataReader.ReaderFieldProperties && this.m_dataSetInstance.GetFieldPropertyCount(fieldIndex) > 0)
			{
				FieldInfo orCreateFieldInfo = this.m_dataSetInstance.GetOrCreateFieldInfo(fieldIndex);
				field.FieldPropertyValues = new List<object>(orCreateFieldInfo.PropertyCount);
				for (int i = 0; i < orCreateFieldInfo.PropertyCount; i++)
				{
					int num = orCreateFieldInfo.PropertyReaderIndices[i];
					string text = orCreateFieldInfo.PropertyNames[i];
					try
					{
						object propertyValue = this.m_dataReader.GetPropertyValue(fieldIndex, num);
						field.FieldPropertyValues.Add(propertyValue);
					}
					catch (ReportProcessingException_FieldError reportProcessingException_FieldError)
					{
						if (!orCreateFieldInfo.IsPropertyErrorRegistered(i))
						{
							this.m_odpContext.ErrorContext.Register(ProcessingErrorCode.rsErrorReadingFieldProperty, Severity.Warning, Microsoft.ReportingServices.ReportProcessing.ObjectType.DataSet, this.m_dataSet.Name, "FieldExtendedProperty", new string[] { fieldDef.Name, text, reportProcessingException_FieldError.Message });
							orCreateFieldInfo.SetPropertyErrorRegistered(i);
						}
						field.FieldPropertyValues.Add(null);
					}
				}
			}
		}

		// Token: 0x06007230 RID: 29232 RVA: 0x001DA70C File Offset: 0x001D890C
		private Microsoft.ReportingServices.ReportIntermediateFormat.RecordField HandleFieldError(ReportProcessingException_FieldError aException, int aFieldIndex, string aFieldName)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.RecordField recordField = null;
			bool flag = false;
			FieldInfo orCreateFieldInfo = this.m_dataSetInstance.GetOrCreateFieldInfo(aFieldIndex);
			if (this.m_dataRowsRead == 0 && DataFieldStatus.UnSupportedDataType != aException.Status && DataFieldStatus.Overflow != aException.Status)
			{
				orCreateFieldInfo.Missing = true;
				recordField = null;
				flag = true;
				this.m_odpContext.ErrorContext.Register(ProcessingErrorCode.rsMissingFieldInDataSet, Severity.Warning, Microsoft.ReportingServices.ReportProcessing.ObjectType.DataSet, this.m_dataSet.Name, "Field", new string[] { aFieldName });
			}
			if (!flag)
			{
				recordField = new Microsoft.ReportingServices.ReportIntermediateFormat.RecordField();
				recordField.FieldStatus = aException.Status;
				recordField.IsAggregationField = false;
				recordField.FieldValue = null;
			}
			if (!orCreateFieldInfo.ErrorRegistered)
			{
				orCreateFieldInfo.ErrorRegistered = true;
				if (DataFieldStatus.UnSupportedDataType == aException.Status)
				{
					if (!this.m_odpContext.ProcessReportParameters)
					{
						this.m_odpContext.ErrorSavingSnapshotData = true;
					}
					this.m_odpContext.ErrorContext.Register(ProcessingErrorCode.rsDataSetFieldTypeNotSupported, Severity.Warning, Microsoft.ReportingServices.ReportProcessing.ObjectType.DataSet, this.m_dataSet.Name, "Field", new string[] { aFieldName });
				}
				else
				{
					this.m_odpContext.ErrorContext.Register(ProcessingErrorCode.rsErrorReadingDataSetField, Severity.Warning, Microsoft.ReportingServices.ReportProcessing.ObjectType.DataSet, this.m_dataSet.Name, "Field", new string[] { aFieldName, aException.Message });
				}
			}
			return recordField;
		}

		// Token: 0x06007231 RID: 29233 RVA: 0x001DA84C File Offset: 0x001D8A4C
		protected void InitializeAndRunLiveQuery()
		{
			if (this.m_dataSourceConnection == null)
			{
				this.m_isConnectionOwner = true;
			}
			bool flag = this.RunDataSetQuery();
			this.InitializeToProcessData(flag);
		}

		// Token: 0x06007232 RID: 29234 RVA: 0x001DA876 File Offset: 0x001D8A76
		private void InitializeToProcessData(bool readerExtensionsSupported)
		{
			if (this.m_processRetrievedData)
			{
				this.InitializeBeforeProcessingRows(readerExtensionsSupported);
				this.m_odpContext.CheckAndThrowIfAborted();
			}
		}

		// Token: 0x06007233 RID: 29235 RVA: 0x001DA894 File Offset: 0x001D8A94
		protected void InitializeAndRunFromExistingQuery(ExecutedQuery query)
		{
			bool flag = this.RunFromExistingQuery(query);
			this.InitializeToProcessData(flag);
		}

		// Token: 0x06007234 RID: 29236 RVA: 0x001DA8B0 File Offset: 0x001D8AB0
		private bool RunFromExistingQuery(ExecutedQuery query)
		{
			if (this.m_dataSetInstance != null)
			{
				this.m_dataSetInstance.SetQueryExecutionTime(query.QueryExecutionTimestamp);
				this.m_dataSetInstance.CommandText = query.CommandText;
			}
			bool flag = this.TakeOwnershipFromExistingQuery(query);
			if (!this.m_odpContext.IsSharedDataSetExecutionOnly && this.m_dataSetInstance != null)
			{
				this.m_dataSetInstance.SaveCollationSettings(this.m_dataSet);
				this.UpdateReportOMDataSet();
			}
			return flag;
		}

		// Token: 0x06007235 RID: 29237 RVA: 0x001DA91C File Offset: 0x001D8B1C
		private bool TakeOwnershipFromExistingQuery(ExecutedQuery query)
		{
			IDataReader dataReader = null;
			bool flag;
			try
			{
				this.m_executionMetrics.Add(query.ExecutionMetrics);
				this.m_executionMetrics.CommandText = query.ExecutionMetrics.CommandText;
				query.ReleaseOwnership(ref this.m_command, ref this.m_commandWrappedForCancel, ref dataReader);
				this.ExtractRewrittenCommandText(this.m_command);
				this.StoreDataReader(dataReader, query.ErrorInspector);
				flag = RuntimeDataSet.ReaderExtensionsSupported(dataReader);
			}
			catch (RSException)
			{
				this.EagerInlineReaderCleanup(ref dataReader);
				throw;
			}
			catch (Exception ex)
			{
				if (AsynchronousExceptionDetection.IsStoppingException(ex))
				{
					throw;
				}
				this.EagerInlineReaderCleanup(ref dataReader);
				throw;
			}
			return flag;
		}

		// Token: 0x06007236 RID: 29238 RVA: 0x001DA9C4 File Offset: 0x001D8BC4
		private bool RunDataSetQuery()
		{
			bool flag = false;
			if (this.m_dataSetInstance != null)
			{
				this.m_dataSetInstance.SetQueryExecutionTime(DateTime.Now);
			}
			if (this.m_dataSet.Query == null)
			{
				return flag;
			}
			List<Microsoft.ReportingServices.ReportIntermediateFormat.ParameterValue> parameters = this.m_dataSet.Query.Parameters;
			object[] array = new object[(parameters == null) ? 0 : parameters.Count];
			for (int i = 0; i < array.Length; i++)
			{
				if (this.m_odpContext.IsSharedDataSetExecutionOnly)
				{
					DataSetParameterValue dataSetParameterValue = parameters[i] as DataSetParameterValue;
					if (!dataSetParameterValue.OmitFromQuery)
					{
						array[i] = this.m_odpContext.ReportObjectModel.ParametersImpl[dataSetParameterValue.UniqueName].Value;
					}
				}
				else
				{
					array[i] = parameters[i].EvaluateQueryParameterValue(this.m_odpContext, this.m_dataSet.ExprHost);
					foreach (object obj in this.m_odpContext.ExternalProcessingContext.Parameters)
					{
						ParameterInfo parameterInfo = (ParameterInfo)obj;
						if (parameterInfo.Name == parameters[i].Name)
						{
							parameters[i].UseAllValidValues = parameterInfo.UseAllValidValues;
							break;
						}
					}
				}
			}
			this.m_odpContext.CheckAndThrowIfAborted();
			this.m_executionMetrics.StartTotalTimer();
			try
			{
				flag = this.RunEmbeddedQuery(parameters, array);
			}
			finally
			{
				this.m_executionMetrics.RecordTotalTimerMeasurement();
			}
			if (!this.m_odpContext.IsSharedDataSetExecutionOnly && this.m_dataSetInstance != null)
			{
				this.m_dataSetInstance.SaveCollationSettings(this.m_dataSet);
				this.UpdateReportOMDataSet();
			}
			return flag;
		}

		// Token: 0x06007237 RID: 29239 RVA: 0x001DAB90 File Offset: 0x001D8D90
		private void UpdateReportOMDataSet()
		{
			((DataSetImpl)this.m_odpContext.ReportObjectModel.DataSetsImpl[this.m_dataSet.Name]).Update(this.m_dataSetInstance, this.m_odpContext.ExecutionTime);
		}

		// Token: 0x06007238 RID: 29240 RVA: 0x001DABD0 File Offset: 0x001D8DD0
		protected bool ProcessSharedDataSetReference()
		{
			DataSetInfo dataSetInfo = null;
			if (this.m_odpContext.SharedDataSetReferences != null)
			{
				if (Guid.Empty != this.m_dataSet.DataSetCore.CatalogID)
				{
					dataSetInfo = this.m_odpContext.SharedDataSetReferences.GetByID(this.m_dataSet.DataSetCore.CatalogID);
				}
				if (dataSetInfo == null)
				{
					dataSetInfo = this.m_odpContext.SharedDataSetReferences.GetByName(this.m_dataSet.DataSetCore.Name, this.m_odpContext.ReportContext);
				}
			}
			if (dataSetInfo == null)
			{
				throw new ReportProcessingException(ErrorCode.rsInvalidSharedDataSetReference, new object[]
				{
					this.m_dataSet.Name,
					this.m_dataSet.SharedDataSetQuery.SharedDataSetReference
				});
			}
			List<Microsoft.ReportingServices.ReportIntermediateFormat.ParameterValue> parameters = this.m_dataSet.SharedDataSetQuery.Parameters;
			SharedDataSetParameterNameMapper.MakeUnique(parameters);
			ParameterInfoCollection parameterInfoCollection = new ParameterInfoCollection();
			object[] array = new object[(parameters == null) ? 0 : parameters.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = parameters[i].EvaluateQueryParameterValue(this.m_odpContext, this.m_dataSet.ExprHost);
				if (this.m_dataSet.IsReferenceToSharedDataSet)
				{
					ParameterInfo parameterInfo = new ParameterInfo(parameters[i]);
					parameterInfo.Name = parameters[i].UniqueName;
					parameterInfo.SetValuesFromQueryParameter(array[i]);
					parameterInfo.DataType = DataType.Object;
					parameterInfoCollection.Add(parameterInfo);
				}
			}
			this.m_odpContext.CheckAndThrowIfAborted();
			this.m_executionMetrics.StartTotalTimer();
			try
			{
				this.GetSharedDataSetChunkAndProcess(true, dataSetInfo, parameterInfoCollection);
			}
			finally
			{
				this.m_executionMetrics.RecordTotalTimerMeasurement();
			}
			if (!this.m_odpContext.IsSharedDataSetExecutionOnly && this.m_dataSetInstance != null)
			{
				this.m_dataSetInstance.SaveCollationSettings(this.m_dataSet);
				this.UpdateReportOMDataSet();
			}
			return false;
		}

		// Token: 0x06007239 RID: 29241 RVA: 0x001DADA8 File Offset: 0x001D8FA8
		private void GetSharedDataSetChunkAndProcess(bool processAsIRowConsumer, DataSetInfo dataSetInfo, ParameterInfoCollection datasetParameterCollection)
		{
			Global.Tracer.Assert(this.m_odpContext.ExternalProcessingContext != null && this.m_odpContext.ExternalProcessingContext.DataSetExecute != null, "Missing handler for shared dataset reference execution");
			string text = null;
			if (!this.m_odpContext.ProcessReportParameters)
			{
				text = Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.GenerateDataChunkName(this.m_odpContext, this.m_dataSet.ID, this.m_odpContext.InSubreport);
			}
			IRowConsumer rowConsumer = (processAsIRowConsumer ? ((IRowConsumer)this) : null);
			bool flag = !processAsIRowConsumer || this.WritesDataChunk;
			this.m_odpContext.ExternalProcessingContext.DataSetExecute.Process(dataSetInfo, text, flag, rowConsumer, datasetParameterCollection, this.m_odpContext.ExternalProcessingContext);
			if (processAsIRowConsumer)
			{
				if (!this.m_odpContext.ProcessReportParameters)
				{
					this.m_odpContext.OdpMetadata.AddDataChunk(text, this.m_dataSetInstance);
					return;
				}
			}
			else
			{
				this.m_dataReader = new ProcessingDataReader(this.m_dataSetInstance, this.m_dataSet, this.m_odpContext, true);
			}
		}

		// Token: 0x0600723A RID: 29242 RVA: 0x001DAE9D File Offset: 0x001D909D
		private bool RunEmbeddedQuery(List<Microsoft.ReportingServices.ReportIntermediateFormat.ParameterValue> queryParams, object[] paramValues)
		{
			Global.Tracer.Assert(this.m_odpContext.StateManager.ExecutedQueryCache == null, "When query execution caching is enabled, new queries must not be run outside query prefetch.");
			return RuntimeDataSet.ReaderExtensionsSupported(base.RunLiveQuery(queryParams, paramValues));
		}

		// Token: 0x0600723B RID: 29243 RVA: 0x001DAED0 File Offset: 0x001D90D0
		protected override void StoreDataReader(IDataReader reader, DataSourceErrorInspector errorInspector)
		{
			bool flag = RuntimeDataSet.ReaderExtensionsSupported(reader);
			if (reader.FieldCount > 0 || this.m_odpContext.IsSharedDataSetExecutionOnly)
			{
				this.CreateProcessingDataReader(reader, errorInspector, flag);
				return;
			}
			this.EagerInlineReaderCleanup(ref reader);
			base.DisposeCommand();
		}

		// Token: 0x0600723C RID: 29244 RVA: 0x001DAF12 File Offset: 0x001D9112
		private static bool ReaderExtensionsSupported(IDataReader reader)
		{
			return reader is IDataReaderExtension;
		}

		// Token: 0x0600723D RID: 29245 RVA: 0x001DAF20 File Offset: 0x001D9120
		protected override void SetRestartPosition(IDbCommand command)
		{
			if (this.m_odpContext.StreamingMode && !(command is IRestartable))
			{
				throw new ReportProcessingException(ErrorCode.rsInvalidDataExtension);
			}
			try
			{
				if (this.m_restartPosition != null && command is IRestartable)
				{
					List<ScopeValueFieldName> queryRestartPosition = this.m_restartPosition.GetQueryRestartPosition(this.m_dataSet);
					if (queryRestartPosition != null)
					{
						IDataParameter[] array = ((IRestartable)command).StartAt(queryRestartPosition);
						if (this.m_odpContext.UseVerboseExecutionLogging)
						{
							this.m_executionMetrics.SetStartAtParameters(array);
						}
					}
				}
			}
			catch (Exception ex)
			{
				throw new ReportProcessingException(ErrorCode.rsErrorSettingStartAt, ex, new object[] { this.m_dataSet.Name });
			}
		}

		// Token: 0x0600723E RID: 29246 RVA: 0x001DAFCC File Offset: 0x001D91CC
		protected override void ExtractRewrittenCommandText(IDbCommand command)
		{
			if (command is IDbCommandRewriter && this.m_dataSetInstance != null)
			{
				this.m_dataSetInstance.RewrittenCommandText = ((IDbCommandRewriter)command).RewrittenCommandText;
			}
		}

		// Token: 0x0600723F RID: 29247 RVA: 0x001DAFF4 File Offset: 0x001D91F4
		protected override void StoreCommandText(string commandText)
		{
			this.m_dataSetInstance.CommandText = commandText;
		}

		// Token: 0x06007240 RID: 29248 RVA: 0x001DB004 File Offset: 0x001D9204
		private void CreateProcessingDataReader(IDataReader reader, DataSourceErrorInspector errorInspector, bool readerExtensionsSupportedLocal)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Field> fields = this.m_dataSet.Fields;
			int num = 0;
			if (fields != null)
			{
				if (this.m_odpContext.IsSharedDataSetExecutionOnly)
				{
					num = this.m_dataSet.Fields.Count;
				}
				else
				{
					num = this.m_dataSet.NonCalculatedFieldCount;
				}
			}
			string[] array = new string[num];
			string[] array2 = new string[num];
			for (int i = 0; i < num; i++)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Field field = fields[i];
				array[i] = field.DataField;
				array2[i] = field.Name;
			}
			this.m_executionMetrics.StartTimer(DataProcessingMetrics.MetricType.DataReaderMapping);
			this.m_dataReader = new ProcessingDataReader(this.m_odpContext, this.m_dataSetInstance, this.m_dataSet.Name, reader, readerExtensionsSupportedLocal || this.m_dataSet.HasAggregateIndicatorFields, array2, array, errorInspector);
			this.m_executionMetrics.RecordTimerMeasurement(DataProcessingMetrics.MetricType.DataReaderMapping);
		}

		// Token: 0x06007241 RID: 29249 RVA: 0x001DB0DC File Offset: 0x001D92DC
		protected override void EagerInlineReaderCleanup(ref IDataReader reader)
		{
			if (this.m_dataReader != null)
			{
				reader = null;
				this.DisposeDataReader();
				return;
			}
			base.DisposeDataExtensionObject<IDataReader>(ref reader, "data reader");
		}

		// Token: 0x06007242 RID: 29250 RVA: 0x001DB0FC File Offset: 0x001D92FC
		internal virtual void EraseDataChunk()
		{
		}

		// Token: 0x06007243 RID: 29251 RVA: 0x001DB0FE File Offset: 0x001D92FE
		protected static void EraseDataChunk(OnDemandProcessingContext odpContext, DataSetInstance dataSetInstance, ref Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.DataChunkWriter dataChunkWriter)
		{
			if (dataChunkWriter == null)
			{
				dataChunkWriter = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.DataChunkWriter(dataSetInstance, odpContext);
			}
			dataChunkWriter.CloseAndEraseChunk();
			dataChunkWriter = null;
		}

		// Token: 0x04003AA0 RID: 15008
		protected DataSetInstance m_dataSetInstance;

		// Token: 0x04003AA1 RID: 15009
		protected IProcessingDataReader m_dataReader;

		// Token: 0x04003AA2 RID: 15010
		protected int m_dataRowsRead;

		// Token: 0x04003AA3 RID: 15011
		private bool m_allDataRowsRead;

		// Token: 0x04003AA4 RID: 15012
		private readonly bool m_processRetrievedData = true;

		// Token: 0x04003AA5 RID: 15013
		private readonly DataSetQueryRestartPosition m_restartPosition;
	}
}

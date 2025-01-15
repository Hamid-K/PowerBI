using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel
{
	// Token: 0x020007AD RID: 1965
	internal sealed class FieldsContext
	{
		// Token: 0x06006F63 RID: 28515 RVA: 0x001D0C19 File Offset: 0x001CEE19
		internal FieldsContext(ObjectModelImpl reportOM)
			: this(reportOM, null)
		{
		}

		// Token: 0x06006F64 RID: 28516 RVA: 0x001D0C24 File Offset: 0x001CEE24
		internal FieldsContext(ObjectModelImpl reportOM, DataSetCore dataSet)
		{
			this.Initialize(reportOM, new FieldsImpl(reportOM), dataSet, null, null, true, false, DataFieldRow.UnInitializedStreamOffset);
		}

		// Token: 0x06006F65 RID: 28517 RVA: 0x001D0C50 File Offset: 0x001CEE50
		internal FieldsContext(ObjectModelImpl reportOM, DataSetCore dataSet, bool addRowIndex, bool noRows)
		{
			List<Field> fields = dataSet.Fields;
			int num = ((fields == null) ? 0 : fields.Count);
			FieldsImpl fieldsImpl = new FieldsImpl(reportOM, num, addRowIndex, noRows);
			this.Initialize(reportOM, fieldsImpl, dataSet, null, null, true, false, DataFieldRow.UnInitializedStreamOffset);
			for (int i = 0; i < num; i++)
			{
				Field field = fields[i];
				if (dataSet.ExprHost != null)
				{
					field.SetExprHost(dataSet.ExprHost, reportOM);
				}
				fieldsImpl.Add(field.Name, null);
			}
			if (addRowIndex)
			{
				fieldsImpl.AddRowIndexField();
			}
		}

		// Token: 0x06006F66 RID: 28518 RVA: 0x001D0CD6 File Offset: 0x001CEED6
		private void Initialize(ObjectModelImpl reportOM, FieldsImpl fields, DataSetCore dataSet, DataSetInstance dataSetInstance, ChunkManager.DataChunkReader dataReader, bool allFieldsCleared, bool pendingFieldValueUpdate, long lastRowOffset)
		{
			this.m_fields = fields;
			this.m_dataSet = dataSet;
			this.m_dataSetInstance = dataSetInstance;
			this.m_dataReader = dataReader;
			this.m_allFieldsCleared = allFieldsCleared;
			this.m_pendingFieldValueUpdate = pendingFieldValueUpdate;
			this.m_lastRowOffset = lastRowOffset;
			this.AttachToDataSetCache(reportOM);
		}

		// Token: 0x06006F67 RID: 28519 RVA: 0x001D0D15 File Offset: 0x001CEF15
		internal void AttachToDataSetCache(ObjectModelImpl reportOM)
		{
			if (this.m_dataSet != null && reportOM.UseDataSetFieldsCache)
			{
				this.m_dataSet.FieldsContext = this;
			}
		}

		// Token: 0x170025E9 RID: 9705
		// (get) Token: 0x06006F68 RID: 28520 RVA: 0x001D0D33 File Offset: 0x001CEF33
		internal bool AllFieldsCleared
		{
			get
			{
				return this.m_allFieldsCleared;
			}
		}

		// Token: 0x170025EA RID: 9706
		// (get) Token: 0x06006F69 RID: 28521 RVA: 0x001D0D3B File Offset: 0x001CEF3B
		internal bool PendingFieldValueUpdate
		{
			get
			{
				return this.m_pendingFieldValueUpdate;
			}
		}

		// Token: 0x170025EB RID: 9707
		// (get) Token: 0x06006F6A RID: 28522 RVA: 0x001D0D43 File Offset: 0x001CEF43
		internal long LastRowOffset
		{
			get
			{
				return this.m_lastRowOffset;
			}
		}

		// Token: 0x170025EC RID: 9708
		// (get) Token: 0x06006F6B RID: 28523 RVA: 0x001D0D4B File Offset: 0x001CEF4B
		internal DataSetCore DataSet
		{
			get
			{
				return this.m_dataSet;
			}
		}

		// Token: 0x170025ED RID: 9709
		// (get) Token: 0x06006F6C RID: 28524 RVA: 0x001D0D53 File Offset: 0x001CEF53
		internal DataSetInstance DataSetInstance
		{
			get
			{
				return this.m_dataSetInstance;
			}
		}

		// Token: 0x170025EE RID: 9710
		// (get) Token: 0x06006F6D RID: 28525 RVA: 0x001D0D5B File Offset: 0x001CEF5B
		internal ChunkManager.DataChunkReader DataReader
		{
			get
			{
				return this.m_dataReader;
			}
		}

		// Token: 0x170025EF RID: 9711
		// (get) Token: 0x06006F6E RID: 28526 RVA: 0x001D0D63 File Offset: 0x001CEF63
		internal FieldsImpl Fields
		{
			get
			{
				return this.m_fields;
			}
		}

		// Token: 0x06006F6F RID: 28527 RVA: 0x001D0D6B File Offset: 0x001CEF6B
		internal void ResetFieldFlags()
		{
			this.m_pendingFieldValueUpdate = false;
			this.m_allFieldsCleared = true;
		}

		// Token: 0x06006F70 RID: 28528 RVA: 0x001D0D7B File Offset: 0x001CEF7B
		internal void UpdateDataSetInfo(DataSetInstance dataSetInstance, ChunkManager.DataChunkReader dataChunkReader)
		{
			this.m_dataSetInstance = dataSetInstance;
			this.m_dataReader = dataChunkReader;
		}

		// Token: 0x06006F71 RID: 28529 RVA: 0x001D0D8C File Offset: 0x001CEF8C
		internal void CreateNoRows()
		{
			if (this.m_noRowsFields == null)
			{
				this.m_fields.SetFields(null, DataFieldRow.UnInitializedStreamOffset);
				this.m_noRowsFields = this.m_fields.GetAndSaveFields();
			}
			else
			{
				this.m_fields.SetFields(this.m_noRowsFields, DataFieldRow.UnInitializedStreamOffset);
			}
			this.ResetFieldFlags();
		}

		// Token: 0x06006F72 RID: 28530 RVA: 0x001D0DE4 File Offset: 0x001CEFE4
		internal void CreateNullFieldValues()
		{
			int count = this.m_fields.Count;
			for (int i = 0; i < count; i++)
			{
				FieldImpl fieldByIndex = this.m_fields.GetFieldByIndex(i);
				if (fieldByIndex != null)
				{
					fieldByIndex.UpdateValue(null, false, DataFieldStatus.None, null);
				}
			}
			this.ResetFieldFlags();
		}

		// Token: 0x06006F73 RID: 28531 RVA: 0x001D0E29 File Offset: 0x001CF029
		internal void PerformPendingFieldValueUpdate(ObjectModelImpl reportOM, bool useDataSetFieldsCache)
		{
			if (this.m_pendingFieldValueUpdate)
			{
				this.m_pendingFieldValueUpdate = false;
				this.UpdateFieldValues(reportOM, useDataSetFieldsCache, this.m_lastRowOffset);
			}
		}

		// Token: 0x06006F74 RID: 28532 RVA: 0x001D0E48 File Offset: 0x001CF048
		internal void RegisterOnDemandFieldValueUpdate(long firstRowOffsetInScope, DataSetInstance dataSetInstance, ChunkManager.DataChunkReader dataReader)
		{
			this.m_pendingFieldValueUpdate = true;
			this.m_lastRowOffset = firstRowOffsetInScope;
			this.m_dataSetInstance = dataSetInstance;
			this.m_dataReader = dataReader;
		}

		// Token: 0x06006F75 RID: 28533 RVA: 0x001D0E66 File Offset: 0x001CF066
		internal void UpdateFieldValues(ObjectModelImpl reportOM, bool useDataSetFieldsCache, long firstRowOffsetInScope)
		{
			if (this.m_dataReader.ReadOneRowAtPosition(firstRowOffsetInScope) || this.m_allFieldsCleared)
			{
				this.UpdateFieldValues(reportOM, useDataSetFieldsCache, true, this.m_dataReader.RecordRow, this.m_dataSetInstance, this.m_dataReader.ReaderExtensionsSupported);
			}
		}

		// Token: 0x06006F76 RID: 28534 RVA: 0x001D0EA4 File Offset: 0x001CF0A4
		internal void UpdateFieldValues(ObjectModelImpl reportOM, bool useDataSetFieldsCache, bool reuseFieldObjects, RecordRow row, DataSetInstance dataSetInstance, bool readerExtensionsSupported)
		{
			Global.Tracer.Assert(row != null, "Empty data row / no data reader");
			if (this.m_dataSetInstance != dataSetInstance)
			{
				this.m_dataSetInstance = dataSetInstance;
				this.m_dataSet = dataSetInstance.DataSetDef.DataSetCore;
				if (this.m_dataSet.FieldsContext != null && useDataSetFieldsCache)
				{
					this.m_fields = this.m_dataSet.FieldsContext.Fields;
				}
				else
				{
					reuseFieldObjects = false;
				}
				this.m_dataReader = null;
				this.m_lastRowOffset = DataFieldRow.UnInitializedStreamOffset;
				this.m_pendingFieldValueUpdate = false;
			}
			this.m_allFieldsCleared = false;
			FieldInfo[] fieldInfos = dataSetInstance.FieldInfos;
			if (this.m_fields.ReaderExtensionsSupported && this.m_dataSet.InterpretSubtotalsAsDetails == Microsoft.ReportingServices.ReportIntermediateFormat.DataSet.TriState.False)
			{
				this.m_fields.IsAggregateRow = row.IsAggregateRow;
				this.m_fields.AggregationFieldCount = row.AggregationFieldCount;
				if (!row.IsAggregateRow)
				{
					this.m_fields.AggregationFieldCountForDetailRow = row.AggregationFieldCount;
				}
			}
			int count = this.m_dataSet.Fields.Count;
			int num = row.RecordFields.Length;
			int i;
			for (i = 0; i < num; i++)
			{
				FieldImpl fieldImpl = (reuseFieldObjects ? this.m_fields.GetFieldByIndex(i) : null);
				Field field = this.m_dataSet.Fields[i];
				RecordField recordField = row.RecordFields[i];
				if (recordField == null)
				{
					if (!reuseFieldObjects || fieldImpl == null)
					{
						fieldImpl = new FieldImpl(reportOM, DataFieldStatus.IsMissing, null, field);
					}
					else
					{
						fieldImpl.UpdateValue(null, false, DataFieldStatus.IsMissing, null);
					}
				}
				else if (recordField.FieldStatus == DataFieldStatus.None)
				{
					if (!reuseFieldObjects || fieldImpl == null)
					{
						fieldImpl = new FieldImpl(reportOM, recordField.FieldValue, recordField.IsAggregationField, field);
					}
					else
					{
						fieldImpl.UpdateValue(recordField.FieldValue, recordField.IsAggregationField, DataFieldStatus.None, null);
					}
				}
				else if (!reuseFieldObjects || fieldImpl == null)
				{
					fieldImpl = new FieldImpl(reportOM, recordField.FieldStatus, ReportRuntime.GetErrorName(recordField.FieldStatus, null), field);
				}
				else
				{
					fieldImpl.UpdateValue(null, false, recordField.FieldStatus, ReportRuntime.GetErrorName(recordField.FieldStatus, null));
				}
				if (recordField != null && fieldInfos != null)
				{
					FieldInfo fieldInfo = fieldInfos[i];
					if (fieldInfo != null && fieldInfo.PropertyCount != 0 && recordField.FieldPropertyValues != null)
					{
						for (int j = 0; j < fieldInfo.PropertyCount; j++)
						{
							fieldImpl.SetProperty(fieldInfo.PropertyNames[j], recordField.FieldPropertyValues[j]);
						}
					}
				}
				this.m_fields[i] = fieldImpl;
			}
			if (i < count)
			{
				if (!reuseFieldObjects && reportOM.OdpContext.ReportRuntime.ReportExprHost != null)
				{
					this.m_dataSet.SetExprHost(reportOM.OdpContext.ReportRuntime.ReportExprHost, reportOM);
				}
				while (i < count)
				{
					Field field2 = this.m_dataSet.Fields[i];
					FieldImpl fieldImpl2 = (reuseFieldObjects ? this.m_fields.GetFieldByIndex(i) : null);
					if (reuseFieldObjects && fieldImpl2 != null)
					{
						if (!fieldImpl2.ResetCalculatedField())
						{
							this.CreateAndInitializeCalculatedFieldWrapper(reportOM, readerExtensionsSupported, this.m_dataSet, i, field2);
						}
					}
					else
					{
						this.CreateAndInitializeCalculatedFieldWrapper(reportOM, readerExtensionsSupported, this.m_dataSet, i, field2);
					}
					i++;
				}
			}
		}

		// Token: 0x06006F77 RID: 28535 RVA: 0x001D11B8 File Offset: 0x001CF3B8
		private void CreateAndInitializeCalculatedFieldWrapper(ObjectModelImpl reportOM, bool readerExtensionsSupported, DataSetCore dataSet, int fieldIndex, Field fieldDef)
		{
			CalculatedFieldWrapperImpl calculatedFieldWrapperImpl = new CalculatedFieldWrapperImpl(fieldDef, reportOM.OdpContext.ReportRuntime);
			bool flag = !readerExtensionsSupported;
			if (dataSet.InterpretSubtotalsAsDetails == Microsoft.ReportingServices.ReportIntermediateFormat.DataSet.TriState.True)
			{
				flag = true;
			}
			this.m_fields[fieldIndex] = new FieldImpl(reportOM, calculatedFieldWrapperImpl, flag, fieldDef);
			if (dataSet.ExprHost != null && fieldDef.ExprHost == null)
			{
				fieldDef.SetExprHost(dataSet.ExprHost, reportOM);
			}
		}

		// Token: 0x0400399F RID: 14751
		private FieldsImpl m_fields;

		// Token: 0x040039A0 RID: 14752
		private DataSetCore m_dataSet;

		// Token: 0x040039A1 RID: 14753
		private DataSetInstance m_dataSetInstance;

		// Token: 0x040039A2 RID: 14754
		private ChunkManager.DataChunkReader m_dataReader;

		// Token: 0x040039A3 RID: 14755
		private bool m_allFieldsCleared;

		// Token: 0x040039A4 RID: 14756
		private bool m_pendingFieldValueUpdate;

		// Token: 0x040039A5 RID: 14757
		private long m_lastRowOffset;

		// Token: 0x040039A6 RID: 14758
		private FieldImpl[] m_noRowsFields;
	}
}

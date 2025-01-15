using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000812 RID: 2066
	internal class RuntimePrefetchDataSet : RuntimeAtomicDataSet
	{
		// Token: 0x060072C6 RID: 29382 RVA: 0x001DD7F5 File Offset: 0x001DB9F5
		public RuntimePrefetchDataSet(DataSource dataSource, DataSet dataSet, DataSetInstance dataSetInstance, OnDemandProcessingContext processingContext, bool canWriteDataChunk, bool processRetrievedData)
			: base(dataSource, dataSet, dataSetInstance, processingContext, processRetrievedData)
		{
			this.m_canWriteDataChunk = canWriteDataChunk;
		}

		// Token: 0x170026DC RID: 9948
		// (get) Token: 0x060072C7 RID: 29383 RVA: 0x001DD80C File Offset: 0x001DBA0C
		protected override bool WritesDataChunk
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060072C8 RID: 29384 RVA: 0x001DD80F File Offset: 0x001DBA0F
		protected override void ProcessRow(RecordRow aRow, int rowNumber)
		{
			if (!this.m_dataSet.IsReferenceToSharedDataSet && this.m_canWriteDataChunk)
			{
				this.m_dataChunkWriter.WriteRecordRow(aRow);
			}
		}

		// Token: 0x060072C9 RID: 29385 RVA: 0x001DD832 File Offset: 0x001DBA32
		protected override void ProcessExtendedPropertyMappings()
		{
			if (!this.m_dataSet.IsReferenceToSharedDataSet)
			{
				this.m_recordSetInfo.PopulateExtendedFieldsProperties(this.m_dataSetInstance);
			}
		}

		// Token: 0x060072CA RID: 29386 RVA: 0x001DD854 File Offset: 0x001DBA54
		protected override void InitializeBeforeProcessingRows(bool aReaderExtensionsSupported)
		{
			if (!this.m_dataSet.IsReferenceToSharedDataSet)
			{
				if (this.m_dataReader != null)
				{
					this.m_recordSetInfo = this.m_dataReader.RecordSetInfo;
				}
				else
				{
					this.m_recordSetInfo = new RecordSetInfo(aReaderExtensionsSupported, this.m_odpContext.IsSharedDataSetExecutionOnly, this.m_dataSetInstance, this.m_odpContext.ExecutionTime);
				}
				if (this.m_canWriteDataChunk)
				{
					this.m_dataChunkWriter = new ChunkManager.DataChunkWriter(this.m_recordSetInfo, this.m_dataSetInstance, this.m_odpContext);
				}
			}
		}

		// Token: 0x060072CB RID: 29387 RVA: 0x001DD8D6 File Offset: 0x001DBAD6
		protected override void AllRowsRead()
		{
			this.m_dataSetInstance.RecordSetSize = base.NumRowsRead;
		}

		// Token: 0x060072CC RID: 29388 RVA: 0x001DD8E9 File Offset: 0x001DBAE9
		protected override void CleanupProcess()
		{
			base.CleanupProcess();
			if (this.m_dataChunkWriter != null)
			{
				this.m_dataChunkWriter.Close();
				this.m_dataChunkWriter = null;
			}
		}

		// Token: 0x060072CD RID: 29389 RVA: 0x001DD90B File Offset: 0x001DBB0B
		internal override void EraseDataChunk()
		{
			if (!this.m_dataSet.IsReferenceToSharedDataSet && this.m_canWriteDataChunk)
			{
				RuntimeDataSet.EraseDataChunk(this.m_odpContext, this.m_dataSetInstance, ref this.m_dataChunkWriter);
			}
		}

		// Token: 0x04003AD0 RID: 15056
		private ChunkManager.DataChunkWriter m_dataChunkWriter;

		// Token: 0x04003AD1 RID: 15057
		protected readonly bool m_canWriteDataChunk;

		// Token: 0x04003AD2 RID: 15058
		private RecordSetInfo m_recordSetInfo;
	}
}

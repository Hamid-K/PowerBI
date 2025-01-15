using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000814 RID: 2068
	internal sealed class RuntimeSharedDataSet : RuntimeParameterDataSet
	{
		// Token: 0x060072D8 RID: 29400 RVA: 0x001DDBB6 File Offset: 0x001DBDB6
		public RuntimeSharedDataSet(Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, DataSetInstance dataSetInstance, OnDemandProcessingContext processingContext)
			: base(dataSource, dataSet, dataSetInstance, processingContext, dataSet.DataSetCore.Filters != null || dataSet.DataSetCore.HasCalculatedFields(), null)
		{
			this.m_consumerRequest = this.m_odpContext.ExternalDataSetContext.ConsumerRequest;
		}

		// Token: 0x170026DD RID: 9949
		// (get) Token: 0x060072D9 RID: 29401 RVA: 0x001DDBF5 File Offset: 0x001DBDF5
		protected override bool WritesDataChunk
		{
			get
			{
				return this.m_odpContext.ExternalDataSetContext.MustCreateDataChunk;
			}
		}

		// Token: 0x060072DA RID: 29402 RVA: 0x001DDC07 File Offset: 0x001DBE07
		protected override void InitializeBeforeProcessingRows(bool aReaderExtensionsSupported)
		{
			base.InitializeBeforeProcessingRows(aReaderExtensionsSupported);
			if (this.WritesDataChunk)
			{
				this.m_dataChunkWriter = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.DataChunkWriter(this.m_dataReader.RecordSetInfo, this.m_dataSetInstance, this.m_odpContext);
			}
		}

		// Token: 0x060072DB RID: 29403 RVA: 0x001DDC3C File Offset: 0x001DBE3C
		protected override void InitializeBeforeFirstRow(bool hasRows)
		{
			base.InitializeBeforeFirstRow(hasRows);
			if (this.WritesDataChunk)
			{
				if (hasRows)
				{
					this.m_dataReader.RecordSetInfo.PopulateExtendedFieldsProperties(this.m_dataSetInstance);
				}
				this.m_dataChunkWriter.CreateDataChunkAndWriteHeader(this.m_dataReader.RecordSetInfo);
			}
			if (this.m_consumerRequest != null)
			{
				this.m_consumerRequest.SetProcessingDataReader(this.m_dataReader);
			}
		}

		// Token: 0x060072DC RID: 29404 RVA: 0x001DDCA0 File Offset: 0x001DBEA0
		protected override void ProcessRow(Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow row, int rowNumber)
		{
			if (this.m_mustEvaluateThroughReportObjectModel)
			{
				base.ProcessRow(row, rowNumber);
				return;
			}
			this.m_currentRow = row;
			this.PostFilterNextRow();
		}

		// Token: 0x060072DD RID: 29405 RVA: 0x001DDCC0 File Offset: 0x001DBEC0
		protected override void AllRowsRead()
		{
			this.m_dataSetInstance.RecordSetSize = base.NumRowsRead;
			base.AllRowsRead();
		}

		// Token: 0x060072DE RID: 29406 RVA: 0x001DDCD9 File Offset: 0x001DBED9
		internal override void EraseDataChunk()
		{
			if (this.WritesDataChunk)
			{
				RuntimeDataSet.EraseDataChunk(this.m_odpContext, this.m_dataSetInstance, ref this.m_dataChunkWriter);
			}
		}

		// Token: 0x060072DF RID: 29407 RVA: 0x001DDCFA File Offset: 0x001DBEFA
		protected override void FinalCleanup()
		{
			base.FinalCleanup();
			if (this.m_dataChunkWriter != null)
			{
				this.m_dataChunkWriter.Close();
				this.m_dataChunkWriter = null;
			}
		}

		// Token: 0x060072E0 RID: 29408 RVA: 0x001DDD1C File Offset: 0x001DBF1C
		public override void PostFilterNextRow()
		{
			if (this.m_mustEvaluateThroughReportObjectModel)
			{
				this.m_currentRow = new Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow(this.m_odpContext.ReportObjectModel.FieldsImpl, this.m_dataSet.DataSetCore.Fields.Count, this.m_dataSetInstance.FieldInfos);
			}
			if (this.WritesDataChunk)
			{
				this.m_dataChunkWriter.WriteRecordRow(this.m_currentRow);
			}
			if (this.m_consumerRequest != null)
			{
				this.m_consumerRequest.NextRow(this.m_currentRow);
			}
		}

		// Token: 0x04003AD7 RID: 15063
		private Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ChunkManager.DataChunkWriter m_dataChunkWriter;

		// Token: 0x04003AD8 RID: 15064
		private Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow m_currentRow;

		// Token: 0x04003AD9 RID: 15065
		private IRowConsumer m_consumerRequest;
	}
}

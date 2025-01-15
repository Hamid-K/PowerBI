using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008F1 RID: 2289
	internal sealed class RuntimeOnDemandDataSet : RuntimePrefetchDataSet
	{
		// Token: 0x06007DE7 RID: 32231 RVA: 0x00207FF6 File Offset: 0x002061F6
		public RuntimeOnDemandDataSet(Microsoft.ReportingServices.ReportIntermediateFormat.DataSource dataSource, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, DataSetInstance dataSetInstance, OnDemandProcessingContext odpContext, bool processFromLiveDataReader, bool generateGroupTree, bool canWriteDataChunk)
			: base(dataSource, dataSet, dataSetInstance, odpContext, canWriteDataChunk, true)
		{
			this.m_processFromLiveDataReader = processFromLiveDataReader;
			this.m_generateGroupTree = generateGroupTree;
		}

		// Token: 0x17002900 RID: 10496
		// (get) Token: 0x06007DE8 RID: 32232 RVA: 0x00208016 File Offset: 0x00206216
		internal IOnDemandScopeInstance GroupTreeRoot
		{
			get
			{
				return this.m_dataProcessingController.GroupTreeRoot;
			}
		}

		// Token: 0x17002901 RID: 10497
		// (get) Token: 0x06007DE9 RID: 32233 RVA: 0x00208023 File Offset: 0x00206223
		internal override bool ProcessFromLiveDataReader
		{
			get
			{
				return this.m_processFromLiveDataReader;
			}
		}

		// Token: 0x06007DEA RID: 32234 RVA: 0x0020802C File Offset: 0x0020622C
		protected override void InitializeBeforeProcessingRows(bool aReaderExtensionsSupported)
		{
			this.m_dataProcessingController = new DataProcessingController(this.m_odpContext, this.m_dataSet, this.m_dataSetInstance);
			if (this.m_processFromLiveDataReader)
			{
				base.InitializeBeforeProcessingRows(aReaderExtensionsSupported);
				this.m_odpContext.ClrCompareOptions = this.m_dataSet.GetCLRCompareOptions();
			}
			else
			{
				Global.Tracer.Assert(this.m_dataReader == null, "(null == m_dataReader)");
				if (!this.m_dataSetInstance.NoRows)
				{
					this.m_dataReader = new ProcessingDataReader(this.m_dataSetInstance, this.m_dataSet, this.m_odpContext, false);
				}
			}
			base.PopulateFieldsWithReaderFlags();
			this.m_dataProcessingController.InitializeDataProcessing();
		}

		// Token: 0x06007DEB RID: 32235 RVA: 0x002080D1 File Offset: 0x002062D1
		protected override void InitializeRowSourceAndProcessRows(ExecutedQuery existingQuery)
		{
			if (this.m_processFromLiveDataReader)
			{
				base.InitializeRowSourceAndProcessRows(existingQuery);
				return;
			}
			this.InitializeBeforeProcessingRows(false);
			this.m_odpContext.CheckAndThrowIfAborted();
			base.ProcessRows();
		}

		// Token: 0x06007DEC RID: 32236 RVA: 0x002080FB File Offset: 0x002062FB
		protected override void InitializeDataSet()
		{
			base.InitializeDataSet();
			this.m_originalTablixProcessingMode = new bool?(this.m_odpContext.IsTablixProcessingMode);
			this.m_odpContext.IsTablixProcessingMode = true;
			this.m_odpContext.SetComparisonInformation(this.m_dataSet.DataSetCore);
		}

		// Token: 0x06007DED RID: 32237 RVA: 0x0020813B File Offset: 0x0020633B
		protected override void CleanupDataReader()
		{
			if (this.m_processFromLiveDataReader)
			{
				base.CleanupDataReader();
			}
		}

		// Token: 0x06007DEE RID: 32238 RVA: 0x0020814C File Offset: 0x0020634C
		protected override void FinalCleanup()
		{
			base.FinalCleanup();
			if (this.m_generateGroupTree)
			{
				this.CleanupController();
			}
			if (this.m_originalTablixProcessingMode != null)
			{
				this.m_odpContext.IsTablixProcessingMode = this.m_originalTablixProcessingMode.Value;
			}
			if (this.m_dataSetInstance != null)
			{
				this.m_odpContext.SetTablixProcessingComplete(this.m_dataSet.IndexInCollection);
			}
		}

		// Token: 0x06007DEF RID: 32239 RVA: 0x002081AE File Offset: 0x002063AE
		protected override void AllRowsRead()
		{
			base.AllRowsRead();
			this.m_dataProcessingController.AllRowsRead();
			if (this.m_generateGroupTree)
			{
				this.m_dataProcessingController.GenerateGroupTree();
			}
		}

		// Token: 0x06007DF0 RID: 32240 RVA: 0x002081D4 File Offset: 0x002063D4
		protected override void CleanupForException()
		{
			base.CleanupForException();
			this.CleanupController();
		}

		// Token: 0x06007DF1 RID: 32241 RVA: 0x002081E2 File Offset: 0x002063E2
		private void CleanupController()
		{
			if (this.m_dataProcessingController != null)
			{
				this.m_dataProcessingController.TeardownDataProcessing();
			}
		}

		// Token: 0x06007DF2 RID: 32242 RVA: 0x002081F8 File Offset: 0x002063F8
		protected override void ProcessRow(Microsoft.ReportingServices.ReportIntermediateFormat.RecordRow row, int rowNumber)
		{
			if (this.m_processFromLiveDataReader && !this.m_dataSet.IsReferenceToSharedDataSet)
			{
				base.ProcessRow(row, rowNumber);
			}
			this.m_dataProcessingController.NextRow(row, rowNumber, this.m_processFromLiveDataReader && !this.m_canWriteDataChunk, base.HasServerAggregateMetadata);
		}

		// Token: 0x06007DF3 RID: 32243 RVA: 0x00208249 File Offset: 0x00206449
		protected override void ProcessExtendedPropertyMappings()
		{
			if (this.m_processFromLiveDataReader)
			{
				base.ProcessExtendedPropertyMappings();
			}
		}

		// Token: 0x06007DF4 RID: 32244 RVA: 0x00208259 File Offset: 0x00206459
		protected override void CleanupProcess()
		{
			if (this.m_processFromLiveDataReader)
			{
				base.CleanupProcess();
			}
		}

		// Token: 0x06007DF5 RID: 32245 RVA: 0x00208269 File Offset: 0x00206469
		internal override void EraseDataChunk()
		{
			if (this.m_processFromLiveDataReader)
			{
				base.EraseDataChunk();
			}
		}

		// Token: 0x04003E10 RID: 15888
		private bool? m_originalTablixProcessingMode;

		// Token: 0x04003E11 RID: 15889
		private bool m_processFromLiveDataReader;

		// Token: 0x04003E12 RID: 15890
		private readonly bool m_generateGroupTree;

		// Token: 0x04003E13 RID: 15891
		private DataProcessingController m_dataProcessingController;
	}
}

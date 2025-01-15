using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x0200012F RID: 303
	internal class SharedDataSetExecution : ISharedDataSet
	{
		// Token: 0x06000C23 RID: 3107 RVA: 0x0002D907 File Offset: 0x0002BB07
		public SharedDataSetExecution(IExecutionDataProvider service, ReportSnapshot targetSnapshot)
		{
			this.m_service = service;
			this.m_targetSnapshot = targetSnapshot;
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x0002D91D File Offset: 0x0002BB1D
		public SharedDataSetExecution(IExecutionDataProvider service, SnapshotManager targetSnapshotManager)
		{
			this.m_service = service;
			this.m_targetSnapshotManager = targetSnapshotManager;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x0002D933 File Offset: 0x0002BB33
		public void Process(DataSetInfo sharedDataSet, string targetChunkNameInReportSnapshot, bool originalRequestNeedsDataChunk, IRowConsumer originalRequest, ParameterInfoCollection dataSetParameterValues, ReportProcessingContext originalProcessingContext)
		{
			SharedDataExecutionInstance sharedDataExecutionInstance = this.CreateInstance();
			sharedDataExecutionInstance.Execute(sharedDataSet, targetChunkNameInReportSnapshot, dataSetParameterValues, originalProcessingContext, originalRequestNeedsDataChunk, originalRequest);
			if (sharedDataExecutionInstance.HasUserDependencies)
			{
				this.m_hasUserDependencies = true;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0002D958 File Offset: 0x0002BB58
		public bool HasUserDependencies
		{
			get
			{
				return this.m_hasUserDependencies;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (set) Token: 0x06000C27 RID: 3111 RVA: 0x0002D960 File Offset: 0x0002BB60
		public IChunkFactory TargetSnapshot
		{
			set
			{
				this.m_targetSnapshot = (ReportSnapshot)value;
			}
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0002D96E File Offset: 0x0002BB6E
		private SharedDataExecutionInstance CreateInstance()
		{
			if (this.m_targetSnapshotManager != null)
			{
				return new SharedDataExecutionInstance(this.m_service, this.m_targetSnapshotManager);
			}
			return new SharedDataExecutionInstance(this.m_service, this.m_targetSnapshot);
		}

		// Token: 0x040004F1 RID: 1265
		private IExecutionDataProvider m_service;

		// Token: 0x040004F2 RID: 1266
		private ReportSnapshot m_targetSnapshot;

		// Token: 0x040004F3 RID: 1267
		private SnapshotManager m_targetSnapshotManager;

		// Token: 0x040004F4 RID: 1268
		internal const string ShareDataSetChunkName = "SharedDataSet";

		// Token: 0x040004F5 RID: 1269
		private bool m_hasUserDependencies;
	}
}

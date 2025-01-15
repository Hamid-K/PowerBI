using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000821 RID: 2081
	internal abstract class OnDemandStateManager
	{
		// Token: 0x06007450 RID: 29776 RVA: 0x001E1E02 File Offset: 0x001E0002
		public OnDemandStateManager(OnDemandProcessingContext odpContext)
		{
			this.m_odpContext = odpContext;
		}

		// Token: 0x17002768 RID: 10088
		// (get) Token: 0x06007451 RID: 29777
		internal abstract IReportScopeInstance LastROMInstance { get; }

		// Token: 0x17002769 RID: 10089
		// (get) Token: 0x06007452 RID: 29778
		// (set) Token: 0x06007453 RID: 29779
		internal abstract IRIFReportScope LastTablixProcessingReportScope { get; set; }

		// Token: 0x1700276A RID: 10090
		// (get) Token: 0x06007454 RID: 29780
		// (set) Token: 0x06007455 RID: 29781
		internal abstract IInstancePath LastRIFObject { get; set; }

		// Token: 0x1700276B RID: 10091
		// (get) Token: 0x06007456 RID: 29782
		internal abstract QueryRestartInfo QueryRestartInfo { get; }

		// Token: 0x1700276C RID: 10092
		// (get) Token: 0x06007457 RID: 29783
		internal abstract ExecutedQueryCache ExecutedQueryCache { get; }

		// Token: 0x06007458 RID: 29784
		internal abstract ExecutedQueryCache SetupExecutedQueryCache();

		// Token: 0x06007459 RID: 29785
		internal abstract void ResetOnDemandState();

		// Token: 0x0600745A RID: 29786
		internal abstract int RecursiveLevel(string scopeName);

		// Token: 0x0600745B RID: 29787
		internal abstract bool InScope(string scopeName);

		// Token: 0x0600745C RID: 29788
		internal abstract Dictionary<string, object> GetCurrentSpecialGroupingValues();

		// Token: 0x0600745D RID: 29789
		internal abstract void RestoreContext(IInstancePath originalObject);

		// Token: 0x0600745E RID: 29790
		internal abstract void SetupContext(IInstancePath rifObject, IReportScopeInstance romInstance);

		// Token: 0x0600745F RID: 29791
		internal abstract void SetupContext(IInstancePath rifObject, IReportScopeInstance romInstance, int moveNextInstanceIndex);

		// Token: 0x06007460 RID: 29792
		internal abstract void BindNextMemberInstance(IInstancePath rifObject, IReportScopeInstance romInstance, int moveNextInstanceIndex);

		// Token: 0x06007461 RID: 29793
		internal abstract bool CalculateAggregate(string aggregateName);

		// Token: 0x06007462 RID: 29794
		internal abstract bool CalculateLookup(LookupInfo lookup);

		// Token: 0x06007463 RID: 29795
		internal abstract bool PrepareFieldsCollectionForDirectFields();

		// Token: 0x06007464 RID: 29796
		internal abstract void EvaluateScopedFieldReference(string scopeName, int fieldIndex, ref Microsoft.ReportingServices.RdlExpressions.VariantResult result);

		// Token: 0x06007465 RID: 29797
		internal abstract IRecordRowReader CreateSequentialDataReader(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, out Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance dataSetInstance);

		// Token: 0x06007466 RID: 29798
		internal abstract bool ShouldStopPipelineAdvance(bool rowAccepted);

		// Token: 0x06007467 RID: 29799
		internal abstract void CreatedScopeInstance(IRIFReportDataScope scope);

		// Token: 0x06007468 RID: 29800 RVA: 0x001E1E11 File Offset: 0x001E0011
		internal virtual void FreeResources()
		{
			this.ShutdownSequentialReadersAndIdcDataManagers();
		}

		// Token: 0x06007469 RID: 29801 RVA: 0x001E1E19 File Offset: 0x001E0019
		protected OnDemandProcessingContext GetOdpWorkerContextForTablixProcessing()
		{
			if (this.m_odpContext.IsPageHeaderFooter)
			{
				return this.m_odpContext.ParentContext;
			}
			return this.m_odpContext;
		}

		// Token: 0x0600746A RID: 29802 RVA: 0x001E1E3C File Offset: 0x001E003C
		protected void ShutdownSequentialReadersAndIdcDataManagers()
		{
			if (this.m_sequentialDataReadersAndIdcDataManagers != null)
			{
				for (int i = 0; i < this.m_sequentialDataReadersAndIdcDataManagers.Count; i++)
				{
					try
					{
						this.m_sequentialDataReadersAndIdcDataManagers[i].Dispose();
					}
					catch (ReportProcessingException ex)
					{
						if (ex.InnerException != null && AsynchronousExceptionDetection.IsStoppingException(ex.InnerException))
						{
							throw;
						}
						Global.Tracer.Trace(TraceLevel.Error, "Error cleaning up request: {0}", new object[] { ex });
					}
				}
				this.m_sequentialDataReadersAndIdcDataManagers = null;
				this.m_idcDataManagers = null;
			}
		}

		// Token: 0x0600746B RID: 29803 RVA: 0x001E1ED0 File Offset: 0x001E00D0
		protected void RegisterDisposableDataReaderOrIdcDataManager(IDisposable dataReaderOrIdcDataManager)
		{
			if (this.m_sequentialDataReadersAndIdcDataManagers == null)
			{
				this.m_sequentialDataReadersAndIdcDataManagers = new List<IDisposable>();
			}
			this.m_sequentialDataReadersAndIdcDataManagers.Add(dataReaderOrIdcDataManager);
		}

		// Token: 0x0600746C RID: 29804
		internal abstract bool CheckForPrematureServerAggregate(string aggregateName);

		// Token: 0x0600746D RID: 29805
		internal abstract bool ProcessOneRow(IRIFReportDataScope scope);

		// Token: 0x0600746E RID: 29806 RVA: 0x001E1EF4 File Offset: 0x001E00F4
		protected BaseIdcDataManager GetOrCreateIdcDataManager(IRIFReportDataScope scope)
		{
			BaseIdcDataManager baseIdcDataManager;
			if (!this.TryGetIdcDataManager(scope, out baseIdcDataManager))
			{
				if (scope.IsDataIntersectionScope)
				{
					baseIdcDataManager = new CellIdcDataManager(this.m_odpContext, scope);
				}
				else
				{
					baseIdcDataManager = new IdcDataManager(this.m_odpContext, scope);
				}
				this.RegisterDisposableDataReaderOrIdcDataManager(baseIdcDataManager);
				this.AddIdcDataManager(scope, baseIdcDataManager);
			}
			return baseIdcDataManager;
		}

		// Token: 0x0600746F RID: 29807 RVA: 0x001E1F40 File Offset: 0x001E0140
		private bool TryGetIdcDataManager(IRIFReportDataScope scope, out BaseIdcDataManager idcDataManager)
		{
			return this.TryGetIdcDataManager(scope.DataScopeInfo.DataPipelineID, out idcDataManager);
		}

		// Token: 0x06007470 RID: 29808 RVA: 0x001E1F54 File Offset: 0x001E0154
		protected bool TryGetNonStructuralIdcDataManager(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet targetDataSet, out NonStructuralIdcDataManager nsIdcDataManager)
		{
			BaseIdcDataManager baseIdcDataManager;
			if (this.TryGetIdcDataManager(targetDataSet.IndexInCollection, out baseIdcDataManager))
			{
				nsIdcDataManager = (NonStructuralIdcDataManager)baseIdcDataManager;
				return true;
			}
			nsIdcDataManager = null;
			return false;
		}

		// Token: 0x06007471 RID: 29809 RVA: 0x001E1F7F File Offset: 0x001E017F
		private bool TryGetIdcDataManager(int dataPipelineId, out BaseIdcDataManager idcDataManager)
		{
			if (this.m_idcDataManagers == null)
			{
				idcDataManager = null;
				return false;
			}
			idcDataManager = this.m_idcDataManagers[dataPipelineId];
			return idcDataManager != null;
		}

		// Token: 0x06007472 RID: 29810 RVA: 0x001E1F9D File Offset: 0x001E019D
		protected void AddNonStructuralIdcDataManager(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet targetDataSet, NonStructuralIdcDataManager idcDataManager)
		{
			this.AddIdcDataManager(targetDataSet.IndexInCollection, idcDataManager);
		}

		// Token: 0x06007473 RID: 29811 RVA: 0x001E1FAC File Offset: 0x001E01AC
		private void AddIdcDataManager(IRIFReportDataScope scope, BaseIdcDataManager idcDataManager)
		{
			this.AddIdcDataManager(scope.DataScopeInfo.DataPipelineID, idcDataManager);
		}

		// Token: 0x06007474 RID: 29812 RVA: 0x001E1FC0 File Offset: 0x001E01C0
		private void AddIdcDataManager(int dataPipelineId, BaseIdcDataManager idcDataManager)
		{
			if (this.m_idcDataManagers == null)
			{
				this.m_idcDataManagers = new BaseIdcDataManager[this.m_odpContext.ReportDefinition.DataPipelineCount];
			}
			this.m_idcDataManagers[dataPipelineId] = idcDataManager;
		}

		// Token: 0x06007475 RID: 29813 RVA: 0x001E1FF0 File Offset: 0x001E01F0
		internal BaseIdcDataManager GetIdcDataManager(IRIFReportDataScope scope)
		{
			BaseIdcDataManager baseIdcDataManager;
			if (!this.TryGetIdcDataManager(scope, out baseIdcDataManager))
			{
				Global.Tracer.Assert(false, "Missing expected IDCDataManager.");
			}
			return baseIdcDataManager;
		}

		// Token: 0x04003B48 RID: 15176
		protected readonly OnDemandProcessingContext m_odpContext;

		// Token: 0x04003B49 RID: 15177
		private List<IDisposable> m_sequentialDataReadersAndIdcDataManagers;

		// Token: 0x04003B4A RID: 15178
		private BaseIdcDataManager[] m_idcDataManagers;
	}
}

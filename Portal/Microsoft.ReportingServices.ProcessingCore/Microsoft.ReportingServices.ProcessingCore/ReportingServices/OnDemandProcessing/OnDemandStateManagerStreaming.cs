using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000824 RID: 2084
	internal sealed class OnDemandStateManagerStreaming : OnDemandStateManager
	{
		// Token: 0x060074B0 RID: 29872 RVA: 0x001E2FF8 File Offset: 0x001E11F8
		public OnDemandStateManagerStreaming(OnDemandProcessingContext odpContext)
			: base(odpContext)
		{
			this.m_queryRestartInfo = new QueryRestartInfo();
			if (this.m_odpContext.AbortInfo != null)
			{
				this.m_abortProcessor = new EventHandler(this.AbortHandler);
				this.m_odpContext.AbortInfo.ProcessingAbortEvent += this.m_abortProcessor;
			}
		}

		// Token: 0x17002777 RID: 10103
		// (get) Token: 0x060074B1 RID: 29873 RVA: 0x001E304C File Offset: 0x001E124C
		internal override IReportScopeInstance LastROMInstance
		{
			get
			{
				return this.m_lastROMInstance;
			}
		}

		// Token: 0x17002778 RID: 10104
		// (get) Token: 0x060074B2 RID: 29874 RVA: 0x001E3054 File Offset: 0x001E1254
		// (set) Token: 0x060074B3 RID: 29875 RVA: 0x001E305C File Offset: 0x001E125C
		internal override IRIFReportScope LastTablixProcessingReportScope
		{
			get
			{
				return this.m_lastRIFObject;
			}
			set
			{
				Global.Tracer.Assert(false, "Set LastTablixProcessingReportScope not supported in this execution mode");
				throw new NotImplementedException();
			}
		}

		// Token: 0x17002779 RID: 10105
		// (get) Token: 0x060074B4 RID: 29876 RVA: 0x001E3073 File Offset: 0x001E1273
		// (set) Token: 0x060074B5 RID: 29877 RVA: 0x001E307B File Offset: 0x001E127B
		internal override IInstancePath LastRIFObject
		{
			get
			{
				return this.m_lastRIFObject;
			}
			set
			{
				Global.Tracer.Assert(false, "Set LastRIFObject not supported in this execution mode");
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700277A RID: 10106
		// (get) Token: 0x060074B6 RID: 29878 RVA: 0x001E3092 File Offset: 0x001E1292
		internal override QueryRestartInfo QueryRestartInfo
		{
			get
			{
				return this.m_queryRestartInfo;
			}
		}

		// Token: 0x1700277B RID: 10107
		// (get) Token: 0x060074B7 RID: 29879 RVA: 0x001E309A File Offset: 0x001E129A
		internal override ExecutedQueryCache ExecutedQueryCache
		{
			get
			{
				return this.m_executedQueryCache;
			}
		}

		// Token: 0x060074B8 RID: 29880 RVA: 0x001E30A2 File Offset: 0x001E12A2
		internal override ExecutedQueryCache SetupExecutedQueryCache()
		{
			Global.Tracer.Assert(this.m_executedQueryCache == null, "Cannot SetupExecutedQueryCache twice");
			this.m_executedQueryCache = new ExecutedQueryCache();
			return this.ExecutedQueryCache;
		}

		// Token: 0x060074B9 RID: 29881 RVA: 0x001E30CD File Offset: 0x001E12CD
		internal override void ResetOnDemandState()
		{
		}

		// Token: 0x060074BA RID: 29882 RVA: 0x001E30CF File Offset: 0x001E12CF
		internal override int RecursiveLevel(string scopeName)
		{
			Global.Tracer.Assert(false, "The Level function is not supported in this execution mode.");
			throw new NotImplementedException();
		}

		// Token: 0x060074BB RID: 29883 RVA: 0x001E30E6 File Offset: 0x001E12E6
		internal override bool InScope(string scopeName)
		{
			Global.Tracer.Assert(false, "The InScope function is not supported in this execution mode.");
			throw new NotImplementedException();
		}

		// Token: 0x060074BC RID: 29884 RVA: 0x001E30FD File Offset: 0x001E12FD
		internal override Dictionary<string, object> GetCurrentSpecialGroupingValues()
		{
			Global.Tracer.Assert(false, "The CreateDrillthroughContext function is not supported in this execution mode.");
			throw new NotImplementedException();
		}

		// Token: 0x060074BD RID: 29885 RVA: 0x001E3114 File Offset: 0x001E1314
		internal override bool CalculateAggregate(string aggregateName)
		{
			Global.Tracer.Assert(!this.m_odpContext.IsPageHeaderFooter, "Not supported for page header/footer in streaming mode");
			OnDemandProcessingContext odpWorkerContextForTablixProcessing = base.GetOdpWorkerContextForTablixProcessing();
			Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo dataAggregateInfo;
			odpWorkerContextForTablixProcessing.ReportAggregates.TryGetValue(aggregateName, out dataAggregateInfo);
			if (dataAggregateInfo == null)
			{
				return false;
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = this.m_odpContext.ReportDefinition.MappingDataSetIndexToDataSet[dataAggregateInfo.DataSetIndexInCollection];
			if (!odpWorkerContextForTablixProcessing.IsTablixProcessingComplete(dataSet.IndexInCollection))
			{
				if (odpWorkerContextForTablixProcessing.IsTablixProcessingMode)
				{
					return false;
				}
				DataSetAggregateDataPipelineManager dataSetAggregateDataPipelineManager = new DataSetAggregateDataPipelineManager(odpWorkerContextForTablixProcessing, dataSet);
				odpWorkerContextForTablixProcessing.OnDemandProcessDataPipelineWithRestore(dataSetAggregateDataPipelineManager);
			}
			return true;
		}

		// Token: 0x060074BE RID: 29886 RVA: 0x001E319D File Offset: 0x001E139D
		internal override bool CalculateLookup(LookupInfo lookup)
		{
			Global.Tracer.Assert(false, "Lookup functions are not supported in this execution mode.");
			throw new NotImplementedException();
		}

		// Token: 0x060074BF RID: 29887 RVA: 0x001E31B4 File Offset: 0x001E13B4
		internal override bool PrepareFieldsCollectionForDirectFields()
		{
			Global.Tracer.Assert(false, "The fields collection should already be setup for Streaming ODP Mode");
			throw new NotImplementedException();
		}

		// Token: 0x060074C0 RID: 29888 RVA: 0x001E31CC File Offset: 0x001E13CC
		internal override void EvaluateScopedFieldReference(string scopeName, int fieldIndex, ref Microsoft.ReportingServices.RdlExpressions.VariantResult result)
		{
			Global.Tracer.Assert(this.m_lastRIFObject != null, "The RIF object for the current scope should be present.");
			try
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet;
				if (!this.m_odpContext.ReportDefinition.MappingNameToDataSet.TryGetValue(scopeName, out dataSet))
				{
					throw new ReportProcessingException_NonExistingScopeReference(scopeName);
				}
				NonStructuralIdcDataManager nonStructuralIdcDataManager;
				if (!base.TryGetNonStructuralIdcDataManager(dataSet, out nonStructuralIdcDataManager))
				{
					nonStructuralIdcDataManager = this.CreateNonStructuralIdcDataManager(scopeName, dataSet);
				}
				if (nonStructuralIdcDataManager.SourceDataScope.CurrentStreamingScopeInstance != nonStructuralIdcDataManager.LastParentScopeInstance)
				{
					nonStructuralIdcDataManager.RegisterActiveParent(nonStructuralIdcDataManager.SourceDataScope.CurrentStreamingScopeInstance);
					nonStructuralIdcDataManager.Advance();
				}
				else
				{
					nonStructuralIdcDataManager.SetupEnvironment();
				}
				this.m_odpContext.ReportRuntime.EvaluateSimpleFieldReference(fieldIndex, ref result);
			}
			finally
			{
				this.SetupEnvironment(this.m_lastRIFObject, this.m_lastOnDemandScopeInstance.Value(), this.m_lastOnDemandScopeInstance);
			}
		}

		// Token: 0x060074C1 RID: 29889 RVA: 0x001E329C File Offset: 0x001E149C
		private NonStructuralIdcDataManager CreateNonStructuralIdcDataManager(string scopeName, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet targetDataSet)
		{
			IRIFReportDataScope irifreportDataScope;
			if (!DataScopeInfo.TryGetInnermostParentScopeRelatedToTargetDataSet(targetDataSet, this.m_lastRIFObject, out irifreportDataScope))
			{
				throw new ReportProcessingException_InvalidScopeReference(scopeName);
			}
			NonStructuralIdcDataManager nonStructuralIdcDataManager = new NonStructuralIdcDataManager(this.m_odpContext, targetDataSet, irifreportDataScope);
			base.RegisterDisposableDataReaderOrIdcDataManager(nonStructuralIdcDataManager);
			base.AddNonStructuralIdcDataManager(targetDataSet, nonStructuralIdcDataManager);
			return nonStructuralIdcDataManager;
		}

		// Token: 0x060074C2 RID: 29890 RVA: 0x001E32DE File Offset: 0x001E14DE
		internal override void RestoreContext(IInstancePath originalObject)
		{
			if (originalObject == null || !this.m_odpContext.ReportRuntime.ContextUpdated)
			{
				return;
			}
			if (this.m_lastRIFObject != originalObject)
			{
				this.SetupObjectModels((IRIFReportDataScope)originalObject, -1);
			}
		}

		// Token: 0x060074C3 RID: 29891 RVA: 0x001E330C File Offset: 0x001E150C
		internal override void SetupContext(IInstancePath rifObject, IReportScopeInstance romInstance)
		{
			this.SetupContext(rifObject, romInstance, -1);
		}

		// Token: 0x060074C4 RID: 29892 RVA: 0x001E3318 File Offset: 0x001E1518
		internal override void SetupContext(IInstancePath rifObject, IReportScopeInstance romInstance, int moveNextInstanceIndex)
		{
			this.m_lastROMInstance = romInstance;
			IRIFReportDataScope irifreportDataScope = romInstance.ReportScope.RIFReportScope as IRIFReportDataScope;
			if (irifreportDataScope != null && (this.m_lastOnDemandScopeInstance == null || irifreportDataScope.CurrentStreamingScopeInstance != this.m_lastOnDemandScopeInstance))
			{
				this.SetupObjectModels(irifreportDataScope, moveNextInstanceIndex);
			}
		}

		// Token: 0x060074C5 RID: 29893 RVA: 0x001E3360 File Offset: 0x001E1560
		private void SetupObjectModels(IRIFReportDataScope reportDataScope, int moveNextInstanceIndex)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Report reportDefinition = this.m_odpContext.ReportDefinition;
			this.m_odpContext.EnsureCultureIsSetOnCurrentThread();
			this.EnsureScopeIsBound(reportDataScope);
			if (this.m_lastOnDemandScopeInstance != reportDataScope.CurrentStreamingScopeInstance)
			{
				this.SetupEnvironment(reportDataScope, reportDataScope.CurrentStreamingScopeInstance.Value(), reportDataScope.CurrentStreamingScopeInstance);
			}
		}

		// Token: 0x060074C6 RID: 29894 RVA: 0x001E33B1 File Offset: 0x001E15B1
		private void EnsureScopeIsBound(IRIFReportDataScope reportDataScope)
		{
			this.BindScopeToInstance(reportDataScope);
			if (!reportDataScope.IsBoundToStreamingScopeInstance && OnDemandStateManagerStreaming.CanBindOrProcessIndividually(reportDataScope) && this.TryProcessToNextScopeInstance(reportDataScope))
			{
				this.BindScopeToInstance(reportDataScope);
			}
			if (!reportDataScope.IsBoundToStreamingScopeInstance)
			{
				reportDataScope.BindToNoRowsScopeInstance(this.m_odpContext);
			}
		}

		// Token: 0x060074C7 RID: 29895 RVA: 0x001E33F0 File Offset: 0x001E15F0
		private void SetupEnvironment(IRIFReportDataScope reportDataScope, IOnDemandScopeInstance scopeInst, IReference<IOnDemandScopeInstance> scopeInstRef)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = reportDataScope.DataScopeInfo.DataSet;
			if (this.m_odpContext.CurrentDataSetIndex != dataSet.IndexInCollection)
			{
				this.m_odpContext.SetupFieldsForNewDataSet(dataSet, this.m_odpContext.GetDataSetInstance(dataSet), true, false);
			}
			scopeInst.SetupEnvironment();
			this.m_lastOnDemandScopeInstance = scopeInstRef;
			this.m_lastRIFObject = reportDataScope;
		}

		// Token: 0x060074C8 RID: 29896 RVA: 0x001E344C File Offset: 0x001E164C
		private void BindScopeToInstance(IRIFReportDataScope reportDataScope)
		{
			if (reportDataScope.IsBoundToStreamingScopeInstance)
			{
				return;
			}
			if (!reportDataScope.IsScope)
			{
				IRIFReportDataScope parentReportScope = reportDataScope.ParentReportScope;
				this.EnsureScopeIsBound(parentReportScope);
				reportDataScope.BindToStreamingScopeInstance(parentReportScope.CurrentStreamingScopeInstance);
				return;
			}
			switch (reportDataScope.InstancePathItem.Type)
			{
			case InstancePathItemType.DataRegion:
				break;
			case InstancePathItemType.SubReport:
				goto IL_025E;
			case InstancePathItemType.Cell:
			{
				if (!reportDataScope.IsDataIntersectionScope)
				{
					Global.Tracer.Assert(false, "Non-intersection cell scopes are not yet supported by streaming ODP.");
					return;
				}
				IRIFReportIntersectionScope irifreportIntersectionScope = (IRIFReportIntersectionScope)reportDataScope;
				IRIFReportDataScope parentRowReportScope = irifreportIntersectionScope.ParentRowReportScope;
				IReference<IOnDemandMemberInstance> reference;
				if (!this.TryBindParentScope<IOnDemandMemberInstance>(reportDataScope, parentRowReportScope, out reference))
				{
					return;
				}
				IRIFReportDataScope parentColumnReportScope = irifreportIntersectionScope.ParentColumnReportScope;
				IReference<IOnDemandMemberInstance> reference2;
				if (!this.TryBindParentScope<IOnDemandMemberInstance>(reportDataScope, parentColumnReportScope, out reference2))
				{
					return;
				}
				IReference<IOnDemandMemberInstance> reference3;
				IReference<IOnDemandMemberInstance> reference4;
				if (!irifreportIntersectionScope.IsColumnOuterGrouping)
				{
					reference3 = reference;
					reference4 = reference2;
				}
				else
				{
					reference3 = reference2;
					reference4 = reference;
				}
				this.CheckForPrematureScopeInstance(reportDataScope);
				IReference<IOnDemandScopeInstance> reference5;
				IOnDemandScopeInstance onDemandScopeInstance = SyntheticTriangulatedCellReference.GetCellInstance(reference3, reference4, out reference5);
				if (onDemandScopeInstance == null && irifreportIntersectionScope.DataScopeInfo.NeedsIDC && this.TryProcessToCreateCell(irifreportIntersectionScope, (RuntimeDataTablixGroupLeafObjReference)reference4, (RuntimeDataTablixGroupLeafObjReference)reference3))
				{
					onDemandScopeInstance = SyntheticTriangulatedCellReference.GetCellInstance(reference3, reference4, out reference5);
				}
				if (onDemandScopeInstance == null)
				{
					return;
				}
				if (reference5 == null)
				{
					irifreportIntersectionScope.BindToStreamingScopeInstance(reference3, reference4);
					this.SetupEnvironment(reportDataScope, onDemandScopeInstance, irifreportIntersectionScope.CurrentStreamingScopeInstance);
					return;
				}
				reportDataScope.BindToStreamingScopeInstance(reference5);
				return;
			}
			case InstancePathItemType.ColumnMemberInstanceIndexTopMost:
			case InstancePathItemType.ColumnMemberInstanceIndex:
			case InstancePathItemType.RowMemberInstanceIndex:
			{
				IRIFReportDataScope parentReportScope2 = reportDataScope.ParentReportScope;
				IReference<IOnDemandMemberOwnerInstance> reference6;
				if (!this.TryBindParentScope<IOnDemandMemberOwnerInstance>(reportDataScope, parentReportScope2, out reference6))
				{
					return;
				}
				this.CheckForPrematureScopeInstance(reportDataScope);
				using (reference6.PinValue())
				{
					IOnDemandMemberOwnerInstance onDemandMemberOwnerInstance = reference6.Value();
					Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode reportHierarchyNode = (Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode)reportDataScope;
					IOnDemandMemberInstanceReference onDemandMemberInstanceReference = onDemandMemberOwnerInstance.GetFirstMemberInstance(reportHierarchyNode);
					if (this.RequiresIdcProcessing(reportDataScope, onDemandMemberInstanceReference, (IReference<IOnDemandScopeInstance>)reference6))
					{
						onDemandMemberInstanceReference = onDemandMemberOwnerInstance.GetFirstMemberInstance(reportHierarchyNode);
					}
					reportDataScope.BindToStreamingScopeInstance(onDemandMemberInstanceReference);
					return;
				}
				break;
			}
			default:
				goto IL_025E;
			}
			IRIFReportDataScope parentReportScope3 = reportDataScope.ParentReportScope;
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = (Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)reportDataScope;
			if (parentReportScope3 == null)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = dataRegion.DataScopeInfo.DataSet;
				DataPipelineManager orCreatePipelineManager = this.GetOrCreatePipelineManager(dataSet, dataRegion);
				reportDataScope.BindToStreamingScopeInstance(orCreatePipelineManager.GroupTreeRoot.GetDataRegionInstance(dataRegion));
				return;
			}
			IReference<IOnDemandScopeInstance> reference7;
			if (!this.TryBindParentScope<IOnDemandScopeInstance>(reportDataScope, parentReportScope3, out reference7))
			{
				return;
			}
			this.CheckForPrematureScopeInstance(reportDataScope);
			using (reference7.PinValue())
			{
				IOnDemandScopeInstance onDemandScopeInstance2 = reference7.Value();
				IReference<IOnDemandScopeInstance> reference8 = onDemandScopeInstance2.GetDataRegionInstance(dataRegion);
				if (this.RequiresIdcProcessing(reportDataScope, reference8, reference7))
				{
					reference8 = onDemandScopeInstance2.GetDataRegionInstance(dataRegion);
				}
				reportDataScope.BindToStreamingScopeInstance(reference8);
				return;
			}
			IL_025E:
			Global.Tracer.Assert(false, "SetupObjectModels cannot handle IRIFReportDataScope of type: {0}", new object[] { Enum.GetName(typeof(InstancePathItemType), reportDataScope.InstancePathItem.Type) });
		}

		// Token: 0x060074C9 RID: 29897 RVA: 0x001E370C File Offset: 0x001E190C
		private bool RequiresIdcProcessing(IRIFReportDataScope reportDataScope, IReference<IOnDemandScopeInstance> scopeInstanceRef, IReference<IOnDemandScopeInstance> parentScopeInstanceRef)
		{
			if (reportDataScope.DataScopeInfo.NeedsIDC)
			{
				if (scopeInstanceRef == null)
				{
					this.RegisterParentForIdc(reportDataScope, parentScopeInstanceRef);
					return this.TryProcessToNextScopeInstance(reportDataScope);
				}
				IOnDemandScopeInstance onDemandScopeInstance = scopeInstanceRef.Value();
				if (onDemandScopeInstance.IsNoRows && onDemandScopeInstance.IsMostRecentlyCreatedScopeInstance)
				{
					this.RegisterParentForIdc(reportDataScope, parentScopeInstanceRef);
					return this.ProcessOneRow(reportDataScope);
				}
			}
			return false;
		}

		// Token: 0x060074CA RID: 29898 RVA: 0x001E3761 File Offset: 0x001E1961
		private void RegisterParentForIdc(IRIFReportDataScope reportDataScope, IReference<IOnDemandScopeInstance> parentScopeInstanceRef)
		{
			((IdcDataManager)base.GetOrCreateIdcDataManager(reportDataScope)).RegisterActiveParent(parentScopeInstanceRef);
		}

		// Token: 0x060074CB RID: 29899 RVA: 0x001E3778 File Offset: 0x001E1978
		internal override bool CheckForPrematureServerAggregate(string aggregateName)
		{
			IRIFReportDataScope irifreportDataScope = this.m_lastRIFObject;
			while (irifreportDataScope != null && !irifreportDataScope.IsScope)
			{
				irifreportDataScope = irifreportDataScope.ParentReportScope;
			}
			if (irifreportDataScope == null || !irifreportDataScope.IsBoundToStreamingScopeInstance)
			{
				return false;
			}
			if (OnDemandStateManagerStreaming.NeedsDataForServerAggregate(irifreportDataScope))
			{
				this.AdvanceDataPipeline(irifreportDataScope, OnDemandStateManagerStreaming.PipelineAdvanceMode.ToFulfillServerAggregate);
				this.SetupEnvironment(irifreportDataScope, irifreportDataScope.CurrentStreamingScopeInstance.Value(), irifreportDataScope.CurrentStreamingScopeInstance);
				return true;
			}
			return false;
		}

		// Token: 0x060074CC RID: 29900 RVA: 0x001E37DC File Offset: 0x001E19DC
		internal static bool NeedsDataForServerAggregate(IRIFReportDataScope reportDataScope)
		{
			IOnDemandScopeInstance onDemandScopeInstance = reportDataScope.CurrentStreamingScopeInstance.Value();
			return !onDemandScopeInstance.IsNoRows && onDemandScopeInstance.IsMostRecentlyCreatedScopeInstance && onDemandScopeInstance.HasUnProcessedServerAggregate;
		}

		// Token: 0x060074CD RID: 29901 RVA: 0x001E380D File Offset: 0x001E1A0D
		private void CheckForPrematureScopeInstance(IRIFReportDataScope reportDataScope)
		{
			if (!OnDemandStateManagerStreaming.CanBindOrProcessIndividually(reportDataScope) && !reportDataScope.DataScopeInfo.NeedsIDC && !reportDataScope.IsDataIntersectionScope && !DataScopeInfo.HasDecomposableAncestorWithNonLatestInstanceBinding(reportDataScope))
			{
				this.TryProcessToNextScopeInstance(reportDataScope);
			}
		}

		// Token: 0x060074CE RID: 29902 RVA: 0x001E383C File Offset: 0x001E1A3C
		private bool TryBindParentScope<T>(IRIFReportDataScope reportDataScope, IRIFReportDataScope parentReportDataScope, out IReference<T> parentScopeInst) where T : IOnDemandScopeInstance
		{
			this.EnsureScopeIsBound(parentReportDataScope);
			parentScopeInst = (IReference<T>)parentReportDataScope.CurrentStreamingScopeInstance;
			T t = parentScopeInst.Value();
			if (t.IsNoRows)
			{
				reportDataScope.BindToNoRowsScopeInstance(this.m_odpContext);
				return false;
			}
			return true;
		}

		// Token: 0x060074CF RID: 29903 RVA: 0x001E3884 File Offset: 0x001E1A84
		private DataPipelineManager GetOrCreatePipelineManager(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, IRIFReportDataScope targetScope)
		{
			if (this.m_pipelineManager != null)
			{
				if (this.m_pipelineManager.DataSetIndex == dataSet.IndexInCollection)
				{
					return this.m_pipelineManager;
				}
				if (this.m_odpContext.IsTablixProcessingComplete(dataSet.IndexInCollection))
				{
					Global.Tracer.Trace(TraceLevel.Verbose, "Performance: While rendering the report: '{0}' the data set {1} was processed multiple times due to rendering traversal order.", new object[]
					{
						this.m_odpContext.ReportContext.ItemPathAsString,
						dataSet.Name
					});
				}
				this.CleanupPipelineManager();
				base.ShutdownSequentialReadersAndIdcDataManagers();
			}
			if (dataSet.AllowIncrementalProcessing)
			{
				this.m_pipelineManager = new IncrementalDataPipelineManager(this.m_odpContext, dataSet);
			}
			else
			{
				this.m_pipelineManager = new StreamingAtomicDataPipelineManager(this.m_odpContext, dataSet);
			}
			this.m_pipelineThrottle = new OnDemandStateManagerStreaming.DataPipelineThrottle();
			this.m_pipelineThrottle.StartUsingContext(OnDemandStateManagerStreaming.PipelineAdvanceMode.ToStoppingScopeInstance, targetScope);
			this.m_pipelineManager.StartProcessing();
			this.m_pipelineThrottle.StopUsingContext();
			this.TryProcessToNextScopeInstance(targetScope);
			return this.m_pipelineManager;
		}

		// Token: 0x060074D0 RID: 29904 RVA: 0x001E396F File Offset: 0x001E1B6F
		private void CleanupPipelineManager()
		{
			if (this.m_pipelineManager != null)
			{
				this.m_pipelineManager.StopProcessing();
				this.m_pipelineManager = null;
			}
		}

		// Token: 0x060074D1 RID: 29905 RVA: 0x001E398C File Offset: 0x001E1B8C
		internal override IRecordRowReader CreateSequentialDataReader(Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, out Microsoft.ReportingServices.ReportIntermediateFormat.DataSetInstance dataSetInstance)
		{
			LiveRecordRowReader liveRecordRowReader = new LiveRecordRowReader(dataSet, this.m_odpContext);
			dataSetInstance = liveRecordRowReader.DataSetInstance;
			base.RegisterDisposableDataReaderOrIdcDataManager(liveRecordRowReader);
			return liveRecordRowReader;
		}

		// Token: 0x060074D2 RID: 29906 RVA: 0x001E39B6 File Offset: 0x001E1BB6
		private bool TryProcessToCreateCell(IRIFReportDataScope reportDataScope, RuntimeDataTablixGroupLeafObjReference columnGroupLeafRef, RuntimeDataTablixGroupLeafObjReference rowGroupLeafRef)
		{
			((CellIdcDataManager)base.GetOrCreateIdcDataManager(reportDataScope)).RegisterActiveIntersection(columnGroupLeafRef, rowGroupLeafRef);
			return this.TryProcessToNextScopeInstance(reportDataScope);
		}

		// Token: 0x060074D3 RID: 29907 RVA: 0x001E39D2 File Offset: 0x001E1BD2
		private bool TryProcessToNextScopeInstance(IRIFReportDataScope reportDataScope)
		{
			return this.AdvanceDataPipeline(reportDataScope, OnDemandStateManagerStreaming.PipelineAdvanceMode.ToStoppingScopeInstance);
		}

		// Token: 0x060074D4 RID: 29908 RVA: 0x001E39DC File Offset: 0x001E1BDC
		private bool AdvanceDataPipeline(IRIFReportDataScope reportDataScope, OnDemandStateManagerStreaming.PipelineAdvanceMode pipelineMode)
		{
			this.m_lastOnDemandScopeInstance = null;
			OnDemandStateManagerStreaming.DataPipelineThrottle dataPipelineThrottle = this.SetupUsablePipelineContextWithBackup();
			this.m_pipelineThrottle.StartUsingContext(pipelineMode, reportDataScope);
			bool isTablixProcessingMode = this.m_odpContext.IsTablixProcessingMode;
			bool flag = this.m_odpContext.ExecutionLogContext.TryStartTablixProcessingTimer();
			this.m_odpContext.IsTablixProcessingMode = true;
			if (reportDataScope.DataScopeInfo.DataPipelineID != this.m_pipelineManager.DataSetIndex)
			{
				this.m_odpContext.StateManager.GetIdcDataManager(reportDataScope).Advance();
			}
			else
			{
				this.m_pipelineManager.Advance();
			}
			this.m_odpContext.IsTablixProcessingMode = isTablixProcessingMode;
			if (flag)
			{
				this.m_odpContext.ExecutionLogContext.StopTablixProcessingTimer();
			}
			bool flag2 = this.m_pipelineThrottle.StopUsingContext();
			this.m_pipelineThrottle = dataPipelineThrottle;
			return flag2;
		}

		// Token: 0x060074D5 RID: 29909 RVA: 0x001E3A98 File Offset: 0x001E1C98
		private OnDemandStateManagerStreaming.DataPipelineThrottle SetupUsablePipelineContextWithBackup()
		{
			if (this.m_pipelineThrottle == null)
			{
				this.m_pipelineThrottle = new OnDemandStateManagerStreaming.DataPipelineThrottle();
			}
			OnDemandStateManagerStreaming.DataPipelineThrottle pipelineThrottle = this.m_pipelineThrottle;
			if (this.m_pipelineThrottle.InUse)
			{
				if (this.m_pipelineThrottle2 == null)
				{
					this.m_pipelineThrottle2 = new OnDemandStateManagerStreaming.DataPipelineThrottle();
				}
				if (this.m_pipelineThrottle2.InUse)
				{
					this.m_pipelineThrottle = new OnDemandStateManagerStreaming.DataPipelineThrottle();
					return pipelineThrottle;
				}
				this.m_pipelineThrottle = this.m_pipelineThrottle2;
			}
			return pipelineThrottle;
		}

		// Token: 0x060074D6 RID: 29910 RVA: 0x001E3B03 File Offset: 0x001E1D03
		internal override bool ProcessOneRow(IRIFReportDataScope scope)
		{
			return this.AdvanceDataPipeline(scope, OnDemandStateManagerStreaming.PipelineAdvanceMode.ByOneRow);
		}

		// Token: 0x060074D7 RID: 29911 RVA: 0x001E3B0D File Offset: 0x001E1D0D
		private void AbortHandler(object sender, EventArgs e)
		{
			if (this.m_pipelineManager != null)
			{
				this.m_pipelineManager.Abort();
			}
		}

		// Token: 0x060074D8 RID: 29912 RVA: 0x001E3B22 File Offset: 0x001E1D22
		internal override void FreeResources()
		{
			if (this.m_abortProcessor != null)
			{
				this.m_odpContext.AbortInfo.ProcessingAbortEvent -= this.m_abortProcessor;
				this.m_abortProcessor = null;
			}
			base.FreeResources();
			this.CleanupPipelineManager();
			this.CleanupQueryCache();
		}

		// Token: 0x060074D9 RID: 29913 RVA: 0x001E3B5B File Offset: 0x001E1D5B
		private void CleanupQueryCache()
		{
			if (this.m_executedQueryCache == null)
			{
				return;
			}
			this.m_executedQueryCache.Close();
		}

		// Token: 0x060074DA RID: 29914 RVA: 0x001E3B74 File Offset: 0x001E1D74
		internal override void BindNextMemberInstance(IInstancePath rifObject, IReportScopeInstance romInstance, int moveNextInstanceIndex)
		{
			IRIFReportDataScope irifreportDataScope = romInstance.ReportScope.RIFReportScope as IRIFReportDataScope;
			IReference<IOnDemandMemberInstance> reference = irifreportDataScope.CurrentStreamingScopeInstance as IReference<IOnDemandMemberInstance>;
			if (reference.Value().IsNoRows)
			{
				return;
			}
			IDisposable disposable = reference.PinValue();
			IOnDemandMemberInstance onDemandMemberInstance = reference.Value();
			irifreportDataScope.BindToStreamingScopeInstance(onDemandMemberInstance.GetNextMemberInstance());
			if (!irifreportDataScope.IsBoundToStreamingScopeInstance && OnDemandStateManagerStreaming.CanBindOrProcessIndividually(irifreportDataScope) && onDemandMemberInstance.IsMostRecentlyCreatedScopeInstance)
			{
				IdcDataManager idcDataManager = null;
				if (irifreportDataScope.DataScopeInfo.NeedsIDC)
				{
					idcDataManager = (IdcDataManager)base.GetIdcDataManager(irifreportDataScope);
					List<object> groupExprValues = onDemandMemberInstance.GroupExprValues;
					List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo> groupExpressions = ((Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode)irifreportDataScope).Grouping.GroupExpressions;
					idcDataManager.SetSkippingFilter(groupExpressions, groupExprValues);
				}
				if (this.TryProcessToNextScopeInstance(irifreportDataScope))
				{
					irifreportDataScope.BindToStreamingScopeInstance(onDemandMemberInstance.GetNextMemberInstance());
				}
				if (idcDataManager != null)
				{
					idcDataManager.ClearSkippingFilter();
				}
			}
			if (!irifreportDataScope.IsBoundToStreamingScopeInstance)
			{
				irifreportDataScope.BindToNoRowsScopeInstance(this.m_odpContext);
			}
			this.SetupEnvironment(irifreportDataScope, irifreportDataScope.CurrentStreamingScopeInstance.Value(), irifreportDataScope.CurrentStreamingScopeInstance);
			disposable.Dispose();
		}

		// Token: 0x060074DB RID: 29915 RVA: 0x001E3C73 File Offset: 0x001E1E73
		internal override bool ShouldStopPipelineAdvance(bool rowAccepted)
		{
			return this.m_pipelineThrottle.ShouldStopPipelineAdvance(rowAccepted);
		}

		// Token: 0x060074DC RID: 29916 RVA: 0x001E3C81 File Offset: 0x001E1E81
		internal override void CreatedScopeInstance(IRIFReportDataScope scope)
		{
			this.m_pipelineThrottle.CreatedScopeInstance(scope);
		}

		// Token: 0x060074DD RID: 29917 RVA: 0x001E3C8F File Offset: 0x001E1E8F
		internal static bool CanBindOrProcessIndividually(IRIFReportDataScope scope)
		{
			return scope.DataScopeInfo.IsDecomposable;
		}

		// Token: 0x04003B54 RID: 15188
		private IReportScopeInstance m_lastROMInstance;

		// Token: 0x04003B55 RID: 15189
		private IReference<IOnDemandScopeInstance> m_lastOnDemandScopeInstance;

		// Token: 0x04003B56 RID: 15190
		private IRIFReportDataScope m_lastRIFObject;

		// Token: 0x04003B57 RID: 15191
		private DataPipelineManager m_pipelineManager;

		// Token: 0x04003B58 RID: 15192
		private QueryRestartInfo m_queryRestartInfo;

		// Token: 0x04003B59 RID: 15193
		private ExecutedQueryCache m_executedQueryCache;

		// Token: 0x04003B5A RID: 15194
		private EventHandler m_abortProcessor;

		// Token: 0x04003B5B RID: 15195
		private OnDemandStateManagerStreaming.DataPipelineThrottle m_pipelineThrottle;

		// Token: 0x04003B5C RID: 15196
		private OnDemandStateManagerStreaming.DataPipelineThrottle m_pipelineThrottle2;

		// Token: 0x02000CFA RID: 3322
		private class DataPipelineThrottle
		{
			// Token: 0x06008E4F RID: 36431 RVA: 0x002447D0 File Offset: 0x002429D0
			internal bool ShouldStopPipelineAdvance(bool rowAccepted)
			{
				switch (this.m_pipelineMode)
				{
				case OnDemandStateManagerStreaming.PipelineAdvanceMode.ToStoppingScopeInstance:
					this.m_metStoppingCondition = rowAccepted && this.m_stoppingScopeInstanceCreated;
					break;
				case OnDemandStateManagerStreaming.PipelineAdvanceMode.ByOneRow:
					this.m_metStoppingCondition = rowAccepted;
					break;
				case OnDemandStateManagerStreaming.PipelineAdvanceMode.ToFulfillServerAggregate:
					this.m_metStoppingCondition = this.m_stoppingScopeInstanceCreated || !OnDemandStateManagerStreaming.NeedsDataForServerAggregate(this.m_targetScopeForDataProcessing);
					break;
				default:
					Global.Tracer.Assert(false, "Unknown pipeline mode: {0}", new object[] { this.m_pipelineMode });
					throw new InvalidOperationException();
				}
				return this.m_metStoppingCondition;
			}

			// Token: 0x06008E50 RID: 36432 RVA: 0x00244866 File Offset: 0x00242A66
			internal void CreatedScopeInstance(IRIFReportDataScope scope)
			{
				this.m_anyScopeInstanceCreated = true;
				if (OnDemandStateManagerStreaming.CanBindOrProcessIndividually(scope) && this.IsTargetScopeForDataProcessing(scope))
				{
					this.m_stoppingScopeInstanceCreated = true;
				}
			}

			// Token: 0x06008E51 RID: 36433 RVA: 0x00244887 File Offset: 0x00242A87
			private bool IsTargetScopeForDataProcessing(IRIFReportDataScope candidateScope)
			{
				return this.m_targetScopeForDataProcessing.IsSameOrChildScopeOf(candidateScope);
			}

			// Token: 0x17002BAD RID: 11181
			// (get) Token: 0x06008E52 RID: 36434 RVA: 0x00244895 File Offset: 0x00242A95
			public bool InUse
			{
				get
				{
					return this.m_inUse;
				}
			}

			// Token: 0x06008E53 RID: 36435 RVA: 0x0024489D File Offset: 0x00242A9D
			public void StartUsingContext(OnDemandStateManagerStreaming.PipelineAdvanceMode mode, IRIFReportDataScope targetScope)
			{
				this.m_inUse = true;
				this.m_metStoppingCondition = false;
				this.m_anyScopeInstanceCreated = false;
				this.m_stoppingScopeInstanceCreated = false;
				this.m_pipelineMode = mode;
				this.m_targetScopeForDataProcessing = targetScope;
			}

			// Token: 0x06008E54 RID: 36436 RVA: 0x002448C9 File Offset: 0x00242AC9
			public bool StopUsingContext()
			{
				this.m_inUse = false;
				if (this.m_pipelineMode == OnDemandStateManagerStreaming.PipelineAdvanceMode.ToStoppingScopeInstance)
				{
					return this.m_anyScopeInstanceCreated;
				}
				return this.m_metStoppingCondition;
			}

			// Token: 0x04004FFE RID: 20478
			private bool m_stoppingScopeInstanceCreated;

			// Token: 0x04004FFF RID: 20479
			private bool m_anyScopeInstanceCreated;

			// Token: 0x04005000 RID: 20480
			private OnDemandStateManagerStreaming.PipelineAdvanceMode m_pipelineMode;

			// Token: 0x04005001 RID: 20481
			private IRIFReportDataScope m_targetScopeForDataProcessing;

			// Token: 0x04005002 RID: 20482
			private bool m_inUse;

			// Token: 0x04005003 RID: 20483
			private bool m_metStoppingCondition;
		}

		// Token: 0x02000CFB RID: 3323
		private enum PipelineAdvanceMode
		{
			// Token: 0x04005005 RID: 20485
			ToStoppingScopeInstance,
			// Token: 0x04005006 RID: 20486
			ByOneRow,
			// Token: 0x04005007 RID: 20487
			ToFulfillServerAggregate
		}
	}
}

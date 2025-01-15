using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008F2 RID: 2290
	internal sealed class RuntimeOnDemandDataSetObj : ReportProcessing.IFilterOwner, IHierarchyObj, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IScope, ISelfReferential, IOnDemandScopeInstance
	{
		// Token: 0x06007DF6 RID: 32246 RVA: 0x0020827C File Offset: 0x0020647C
		public RuntimeOnDemandDataSetObj(OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet, DataSetInstance dataSetInstance)
		{
			this.m_odpContext = odpContext;
			this.m_dataSet = dataSet;
			this.m_dataSetInstance = dataSetInstance;
			this.m_odpContext.TablixProcessingScalabilityCache.GenerateFixedReference<RuntimeOnDemandDataSetObj>(this);
			UserSortFilterContext userSortFilterContext = odpContext.UserSortFilterContext;
			if (this.m_odpContext.IsSortFilterTarget(dataSet.IsSortFilterTarget, userSortFilterContext.CurrentContainingScope, this.SelfReference, ref this.m_userSortTargetInfo) && this.m_userSortTargetInfo.TargetForNonDetailSort)
			{
				this.m_dataRows = new ScalableList<DataFieldRow>(0, odpContext.TablixProcessingScalabilityCache, 100, 10);
			}
			if (!this.m_odpContext.StreamingMode)
			{
				this.CreateRuntimeStructure();
			}
			this.m_dataSet.SetupRuntimeEnvironment(this.m_odpContext);
			if (this.m_dataSet.Filters != null)
			{
				this.m_filters = new Filters(Filters.FilterTypes.DataSetFilter, (IReference<ReportProcessing.IFilterOwner>)this.SelfReference, this.m_dataSet.Filters, this.m_dataSet.ObjectType, this.m_dataSet.Name, this.m_odpContext, 0);
			}
			this.RegisterAggregates();
		}

		// Token: 0x06007DF7 RID: 32247 RVA: 0x00208382 File Offset: 0x00206582
		public void Initialize()
		{
			if (this.m_dataSet.LookupDestinationInfos != null)
			{
				this.m_lookupProcessor = new RuntimeLookupProcessing(this.m_odpContext, this.m_dataSet, this.m_dataSetInstance, this);
			}
		}

		// Token: 0x06007DF8 RID: 32248 RVA: 0x002083B0 File Offset: 0x002065B0
		public void NextRow()
		{
			if (this.m_odpContext.ReportObjectModel.FieldsImpl.IsAggregateRow)
			{
				this.NextAggregateRow();
				return;
			}
			bool flag = true;
			if (this.m_filters != null)
			{
				flag = this.m_filters.PassFilters(new DataFieldRow(this.m_odpContext.ReportObjectModel.FieldsImpl, false));
			}
			if (flag)
			{
				this.PostFilterNextRow();
			}
		}

		// Token: 0x06007DF9 RID: 32249 RVA: 0x00208410 File Offset: 0x00206610
		private void NextAggregateRow()
		{
			if (this.m_odpContext.ReportObjectModel.FieldsImpl.AggregationFieldCount == 0 && this.m_customAggregates != null)
			{
				for (int i = 0; i < this.m_customAggregates.Count; i++)
				{
					this.m_customAggregates[i].Update();
				}
			}
			if (this.m_userSortTargetInfo != null && this.m_userSortTargetInfo.SortTree != null)
			{
				if (this.m_userSortTargetInfo.AggregateRows == null)
				{
					this.m_userSortTargetInfo.AggregateRows = new List<AggregateRow>();
				}
				AggregateRow aggregateRow = new AggregateRow(this.m_odpContext.ReportObjectModel.FieldsImpl, true);
				this.m_userSortTargetInfo.AggregateRows.Add(aggregateRow);
				if (!this.m_userSortTargetInfo.TargetForNonDetailSort)
				{
					return;
				}
			}
			this.SendToInner();
		}

		// Token: 0x06007DFA RID: 32250 RVA: 0x002084D4 File Offset: 0x002066D4
		public void PostFilterNextRow()
		{
			if (this.m_nonCustomAggregates != null)
			{
				for (int i = 0; i < this.m_nonCustomAggregates.Count; i++)
				{
					this.m_nonCustomAggregates[i].Update();
				}
			}
			if (this.m_dataRows != null)
			{
				RuntimeDataTablixObj.SaveData(this.m_dataRows, this.m_odpContext);
			}
			if (this.m_firstNonAggregateRow)
			{
				this.m_firstNonAggregateRow = false;
				this.m_dataSetInstance.FirstRowOffset = this.m_odpContext.ReportObjectModel.FieldsImpl.StreamOffset;
			}
			if (this.m_lookupProcessor != null)
			{
				this.m_lookupProcessor.NextRow();
				return;
			}
			this.PostLookupNextRow();
		}

		// Token: 0x06007DFB RID: 32251 RVA: 0x00208572 File Offset: 0x00206772
		internal void PostLookupNextRow()
		{
			this.SendToInner();
		}

		// Token: 0x06007DFC RID: 32252 RVA: 0x0020857A File Offset: 0x0020677A
		private void SendToInner()
		{
			if (this.m_runtimeDataRegions == null)
			{
				this.CreateRuntimeStructure();
			}
			this.m_runtimeDataRegions.FirstPassNextDataRow(this.m_odpContext);
		}

		// Token: 0x06007DFD RID: 32253 RVA: 0x0020859B File Offset: 0x0020679B
		internal void CompleteLookupProcessing()
		{
			if (this.m_lookupProcessor != null)
			{
				this.m_lookupProcessor.CompleteLookupProcessing();
				this.m_lookupProcessor = null;
			}
		}

		// Token: 0x06007DFE RID: 32254 RVA: 0x002085B7 File Offset: 0x002067B7
		public void FinishReadingRows()
		{
			if (this.m_filters != null)
			{
				this.m_filters.FinishReadingRows();
			}
			this.m_odpContext.CheckAndThrowIfAborted();
			if (this.m_lookupProcessor != null && this.m_lookupProcessor.MustBufferAllRows)
			{
				this.m_lookupProcessor.FinishReadingRows();
			}
		}

		// Token: 0x06007DFF RID: 32255 RVA: 0x002085F7 File Offset: 0x002067F7
		public void SortAndFilter(AggregateUpdateContext aggContext)
		{
			if (this.m_runtimeDataRegions == null)
			{
				return;
			}
			this.m_runtimeDataRegions.SortAndFilter(aggContext);
		}

		// Token: 0x06007E00 RID: 32256 RVA: 0x0020860E File Offset: 0x0020680E
		public void EnterProcessUserSortPhase()
		{
			if (this.m_userSortTargetInfo != null)
			{
				this.m_userSortTargetInfo.EnterProcessUserSortPhase(this.m_odpContext);
			}
		}

		// Token: 0x06007E01 RID: 32257 RVA: 0x00208629 File Offset: 0x00206829
		public void LeaveProcessUserSortPhase()
		{
			if (this.m_userSortTargetInfo != null)
			{
				this.m_userSortTargetInfo.LeaveProcessUserSortPhase(this.m_odpContext);
			}
		}

		// Token: 0x06007E02 RID: 32258 RVA: 0x00208644 File Offset: 0x00206844
		public void CalculateRunningValues(Dictionary<string, IReference<RuntimeGroupRootObj>> groupCollection, AggregateUpdateContext aggContext)
		{
			if (this.m_runtimeDataRegions == null)
			{
				return;
			}
			this.m_runtimeDataRegions.CalculateRunningValues(groupCollection, null, aggContext);
		}

		// Token: 0x06007E03 RID: 32259 RVA: 0x0020865D File Offset: 0x0020685D
		public void CreateInstances()
		{
			if (this.m_runtimeDataRegions == null)
			{
				return;
			}
			this.m_runtimeDataRegions.CreateAllDataRegionInstances(this.m_odpContext.CurrentReportInstance, this.m_odpContext, this.m_selfReference);
		}

		// Token: 0x06007E04 RID: 32260 RVA: 0x0020868A File Offset: 0x0020688A
		public void Teardown()
		{
			this.m_selfReference = null;
		}

		// Token: 0x06007E05 RID: 32261 RVA: 0x00208693 File Offset: 0x00206893
		public IOnDemandMemberOwnerInstanceReference GetDataRegionInstance(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion rifDataRegion)
		{
			if (this.m_runtimeDataRegions == null)
			{
				return null;
			}
			return this.m_runtimeDataRegions.GetDataRegionObj(rifDataRegion);
		}

		// Token: 0x06007E06 RID: 32262 RVA: 0x002086AB File Offset: 0x002068AB
		public IReference<IDataCorrelation> GetIdcReceiver(IRIFReportDataScope scope)
		{
			return null;
		}

		// Token: 0x17002902 RID: 10498
		// (get) Token: 0x06007E07 RID: 32263 RVA: 0x002086AE File Offset: 0x002068AE
		public bool IsNoRows
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002903 RID: 10499
		// (get) Token: 0x06007E08 RID: 32264 RVA: 0x002086B1 File Offset: 0x002068B1
		public bool IsMostRecentlyCreatedScopeInstance
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002904 RID: 10500
		// (get) Token: 0x06007E09 RID: 32265 RVA: 0x002086B4 File Offset: 0x002068B4
		public bool HasUnProcessedServerAggregate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007E0A RID: 32266 RVA: 0x002086B7 File Offset: 0x002068B7
		private void CreateRuntimeStructure()
		{
			this.m_runtimeDataRegions = new RuntimeRICollection(this.m_selfReference, this.m_dataSet.DataRegions, this.m_odpContext, this.m_odpContext.ReportDefinition.MergeOnePass);
		}

		// Token: 0x06007E0B RID: 32267 RVA: 0x002086EC File Offset: 0x002068EC
		private void RegisterAggregates()
		{
			if (this.m_odpContext.InSubreport)
			{
				this.m_odpContext.ReportObjectModel.AggregatesImpl.ClearAll();
			}
			this.CreateAggregates(this.m_dataSet.Aggregates);
			this.CreateAggregates(this.m_dataSet.PostSortAggregates);
		}

		// Token: 0x06007E0C RID: 32268 RVA: 0x00208740 File Offset: 0x00206940
		private void CreateAggregates(List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo> aggDefs)
		{
			if (aggDefs != null && 0 < aggDefs.Count)
			{
				AggregatesImpl aggregatesImpl = this.m_odpContext.ReportObjectModel.AggregatesImpl;
				for (int i = 0; i < aggDefs.Count; i++)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo dataAggregateInfo = aggDefs[i];
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj dataAggregateObj = aggregatesImpl.GetAggregateObj(dataAggregateInfo.Name);
					if (dataAggregateObj == null)
					{
						dataAggregateObj = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj(dataAggregateInfo, this.m_odpContext);
						aggregatesImpl.Add(dataAggregateObj);
					}
					if (Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.Previous != dataAggregateInfo.AggregateType)
					{
						if (Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.Aggregate == dataAggregateInfo.AggregateType)
						{
							RuntimeDataRegionObj.AddAggregate(ref this.m_customAggregates, dataAggregateObj);
						}
						else
						{
							RuntimeDataRegionObj.AddAggregate(ref this.m_nonCustomAggregates, dataAggregateObj);
						}
					}
				}
			}
		}

		// Token: 0x17002905 RID: 10501
		// (get) Token: 0x06007E0D RID: 32269 RVA: 0x002087D8 File Offset: 0x002069D8
		IReference<IHierarchyObj> IHierarchyObj.HierarchyRoot
		{
			get
			{
				return this.m_selfReference;
			}
		}

		// Token: 0x17002906 RID: 10502
		// (get) Token: 0x06007E0E RID: 32270 RVA: 0x002087E0 File Offset: 0x002069E0
		OnDemandProcessingContext IHierarchyObj.OdpContext
		{
			get
			{
				return this.m_odpContext;
			}
		}

		// Token: 0x17002907 RID: 10503
		// (get) Token: 0x06007E0F RID: 32271 RVA: 0x002087E8 File Offset: 0x002069E8
		BTree IHierarchyObj.SortTree
		{
			get
			{
				if (this.m_userSortTargetInfo != null)
				{
					return this.m_userSortTargetInfo.SortTree;
				}
				return null;
			}
		}

		// Token: 0x17002908 RID: 10504
		// (get) Token: 0x06007E10 RID: 32272 RVA: 0x002087FF File Offset: 0x002069FF
		int IHierarchyObj.ExpressionIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002909 RID: 10505
		// (get) Token: 0x06007E11 RID: 32273 RVA: 0x00208802 File Offset: 0x00206A02
		List<int> IHierarchyObj.SortFilterInfoIndices
		{
			get
			{
				if (this.m_userSortTargetInfo != null)
				{
					return this.m_userSortTargetInfo.SortFilterInfoIndices;
				}
				return null;
			}
		}

		// Token: 0x1700290A RID: 10506
		// (get) Token: 0x06007E12 RID: 32274 RVA: 0x00208819 File Offset: 0x00206A19
		bool IHierarchyObj.IsDetail
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700290B RID: 10507
		// (get) Token: 0x06007E13 RID: 32275 RVA: 0x0020881C File Offset: 0x00206A1C
		bool IHierarchyObj.InDataRowSortPhase
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007E14 RID: 32276 RVA: 0x0020881F File Offset: 0x00206A1F
		IHierarchyObj IHierarchyObj.CreateHierarchyObjForSortTree()
		{
			return new RuntimeSortHierarchyObj(this, 1);
		}

		// Token: 0x06007E15 RID: 32277 RVA: 0x00208828 File Offset: 0x00206A28
		ProcessingMessageList IHierarchyObj.RegisterComparisonError(string propertyName)
		{
			return this.m_odpContext.RegisterComparisonErrorForSortFilterEvent(propertyName);
		}

		// Token: 0x06007E16 RID: 32278 RVA: 0x00208836 File Offset: 0x00206A36
		internal ProcessingMessageList RegisterSpatialElementComparisonError(string type)
		{
			this.m_odpContext.ErrorContext.Register(ProcessingErrorCode.rsCannotCompareSpatialType, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.DataSet, this.m_dataSet.Name, type, Array.Empty<string>());
			return this.m_odpContext.ErrorContext.Messages;
		}

		// Token: 0x06007E17 RID: 32279 RVA: 0x00208872 File Offset: 0x00206A72
		void IHierarchyObj.NextRow(IHierarchyObj owner)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007E18 RID: 32280 RVA: 0x0020887F File Offset: 0x00206A7F
		void IHierarchyObj.Traverse(ProcessingStages operation, ITraversalContext traversalContext)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007E19 RID: 32281 RVA: 0x0020888C File Offset: 0x00206A8C
		void IHierarchyObj.ReadRow()
		{
			this.SendToInner();
		}

		// Token: 0x06007E1A RID: 32282 RVA: 0x00208894 File Offset: 0x00206A94
		void IHierarchyObj.ProcessUserSort()
		{
			Global.Tracer.Assert(this.m_userSortTargetInfo != null, "(null != m_userSortTargetInfo)");
			this.m_odpContext.ProcessUserSortForTarget(this.SelfReference, ref this.m_dataRows, this.m_userSortTargetInfo.TargetForNonDetailSort);
			if (this.m_userSortTargetInfo.TargetForNonDetailSort)
			{
				this.m_userSortTargetInfo.ResetTargetForNonDetailSort();
				this.m_userSortTargetInfo.EnterProcessUserSortPhase(this.m_odpContext);
				this.CreateRuntimeStructure();
				this.m_userSortTargetInfo.SortTree.Traverse(ProcessingStages.UserSortFilter, true, null);
				this.m_userSortTargetInfo.SortTree.Dispose();
				this.m_userSortTargetInfo.SortTree = null;
				if (this.m_userSortTargetInfo.AggregateRows != null)
				{
					for (int i = 0; i < this.m_userSortTargetInfo.AggregateRows.Count; i++)
					{
						this.m_userSortTargetInfo.AggregateRows[i].SetFields(this.m_odpContext.ReportObjectModel.FieldsImpl);
						this.SendToInner();
					}
					this.m_userSortTargetInfo.AggregateRows = null;
				}
				this.m_userSortTargetInfo.LeaveProcessUserSortPhase(this.m_odpContext);
			}
		}

		// Token: 0x06007E1B RID: 32283 RVA: 0x002089AD File Offset: 0x00206BAD
		void IHierarchyObj.MarkSortInfoProcessed(List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo)
		{
			if (this.m_userSortTargetInfo != null)
			{
				this.m_userSortTargetInfo.MarkSortInfoProcessed(runtimeSortFilterInfo, this.SelfReference);
			}
		}

		// Token: 0x06007E1C RID: 32284 RVA: 0x002089C9 File Offset: 0x00206BC9
		void IHierarchyObj.AddSortInfoIndex(int sortInfoIndex, IReference<RuntimeSortFilterEventInfo> sortInfo)
		{
			if (this.m_userSortTargetInfo != null)
			{
				this.m_userSortTargetInfo.AddSortInfoIndex(sortInfoIndex, sortInfo);
			}
		}

		// Token: 0x06007E1D RID: 32285 RVA: 0x002089E0 File Offset: 0x00206BE0
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			Global.Tracer.Assert(false, "RuntimeOnDemandDataSetObj should not be serialized");
		}

		// Token: 0x06007E1E RID: 32286 RVA: 0x002089F2 File Offset: 0x00206BF2
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(false, "RuntimeOnDemandDataSetObj should not be de-serialized");
		}

		// Token: 0x06007E1F RID: 32287 RVA: 0x00208A04 File Offset: 0x00206C04
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "RuntimeOnDemandDataSetObj should not need references resolved");
		}

		// Token: 0x06007E20 RID: 32288 RVA: 0x00208A16 File Offset: 0x00206C16
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeOnDemandDataSetObj;
		}

		// Token: 0x1700290C RID: 10508
		// (get) Token: 0x06007E21 RID: 32289 RVA: 0x00208A1D File Offset: 0x00206C1D
		int IStorable.Size
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700290D RID: 10509
		// (get) Token: 0x06007E22 RID: 32290 RVA: 0x00208A20 File Offset: 0x00206C20
		public IReference<IHierarchyObj> SelfReference
		{
			get
			{
				return this.m_selfReference;
			}
		}

		// Token: 0x1700290E RID: 10510
		// (get) Token: 0x06007E23 RID: 32291 RVA: 0x00208A28 File Offset: 0x00206C28
		public int Depth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06007E24 RID: 32292 RVA: 0x00208A2B File Offset: 0x00206C2B
		public void SetReference(IReference selfRef)
		{
			this.m_selfReference = (RuntimeOnDemandDataSetObjReference)selfRef;
		}

		// Token: 0x1700290F RID: 10511
		// (get) Token: 0x06007E25 RID: 32293 RVA: 0x00208A39 File Offset: 0x00206C39
		bool IScope.TargetForNonDetailSort
		{
			get
			{
				return this.m_userSortTargetInfo != null && this.m_userSortTargetInfo.TargetForNonDetailSort;
			}
		}

		// Token: 0x17002910 RID: 10512
		// (get) Token: 0x06007E26 RID: 32294 RVA: 0x00208A50 File Offset: 0x00206C50
		int[] IScope.SortFilterExpressionScopeInfoIndices
		{
			get
			{
				if (this.m_sortFilterExpressionScopeInfoIndices == null)
				{
					this.m_sortFilterExpressionScopeInfoIndices = new int[this.m_odpContext.RuntimeSortFilterInfo.Count];
					for (int i = 0; i < this.m_odpContext.RuntimeSortFilterInfo.Count; i++)
					{
						this.m_sortFilterExpressionScopeInfoIndices[i] = -1;
					}
				}
				return this.m_sortFilterExpressionScopeInfoIndices;
			}
		}

		// Token: 0x17002911 RID: 10513
		// (get) Token: 0x06007E27 RID: 32295 RVA: 0x00208AAA File Offset: 0x00206CAA
		IRIFReportScope IScope.RIFReportScope
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06007E28 RID: 32296 RVA: 0x00208AAD File Offset: 0x00206CAD
		bool IScope.IsTargetForSort(int index, bool detailSort)
		{
			return this.m_userSortTargetInfo != null && this.m_userSortTargetInfo.IsTargetForSort(index, detailSort);
		}

		// Token: 0x06007E29 RID: 32297 RVA: 0x00208AC6 File Offset: 0x00206CC6
		bool IScope.InScope(string scope)
		{
			return ReportProcessing.CompareWithInvariantCulture(this.m_dataSet.Name, scope, false) == 0;
		}

		// Token: 0x06007E2A RID: 32298 RVA: 0x00208ADF File Offset: 0x00206CDF
		void IScope.ReadRow(DataActions dataAction, ITraversalContext context)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007E2B RID: 32299 RVA: 0x00208AEC File Offset: 0x00206CEC
		IReference<IScope> IScope.GetOuterScope(bool includeSubReportContainingScope)
		{
			if (includeSubReportContainingScope)
			{
				return this.m_odpContext.UserSortFilterContext.CurrentContainingScope;
			}
			return null;
		}

		// Token: 0x06007E2C RID: 32300 RVA: 0x00208B03 File Offset: 0x00206D03
		void IScope.CalculatePreviousAggregates()
		{
		}

		// Token: 0x06007E2D RID: 32301 RVA: 0x00208B05 File Offset: 0x00206D05
		string IScope.GetScopeName()
		{
			return this.m_dataSet.Name;
		}

		// Token: 0x06007E2E RID: 32302 RVA: 0x00208B12 File Offset: 0x00206D12
		int IScope.RecursiveLevel(string scope)
		{
			return 0;
		}

		// Token: 0x06007E2F RID: 32303 RVA: 0x00208B18 File Offset: 0x00206D18
		bool IScope.TargetScopeMatched(int index, bool detailSort)
		{
			if (this.m_odpContext.UserSortFilterContext.CurrentContainingScope != null)
			{
				return this.m_odpContext.UserSortFilterContext.CurrentContainingScope.Value().TargetScopeMatched(index, detailSort);
			}
			return this.m_odpContext.RuntimeSortFilterInfo != null;
		}

		// Token: 0x06007E30 RID: 32304 RVA: 0x00208B64 File Offset: 0x00206D64
		void IScope.GetScopeValues(IReference<IHierarchyObj> targetScopeObj, List<object>[] scopeValues, ref int index)
		{
			IReference<IScope> currentContainingScope = this.m_odpContext.UserSortFilterContext.CurrentContainingScope;
			if (currentContainingScope != null && (targetScopeObj == null || this != targetScopeObj.Value()))
			{
				currentContainingScope.Value().GetScopeValues(null, scopeValues, ref index);
			}
		}

		// Token: 0x06007E31 RID: 32305 RVA: 0x00208B9F File Offset: 0x00206D9F
		void IScope.GetGroupNameValuePairs(Dictionary<string, object> pairs)
		{
		}

		// Token: 0x06007E32 RID: 32306 RVA: 0x00208BA1 File Offset: 0x00206DA1
		public void SetupEnvironment()
		{
		}

		// Token: 0x06007E33 RID: 32307 RVA: 0x00208BA3 File Offset: 0x00206DA3
		void IScope.UpdateAggregates(AggregateUpdateContext context)
		{
		}

		// Token: 0x04003E14 RID: 15892
		private readonly OnDemandProcessingContext m_odpContext;

		// Token: 0x04003E15 RID: 15893
		private readonly Microsoft.ReportingServices.ReportIntermediateFormat.DataSet m_dataSet;

		// Token: 0x04003E16 RID: 15894
		private readonly DataSetInstance m_dataSetInstance;

		// Token: 0x04003E17 RID: 15895
		private ScalableList<DataFieldRow> m_dataRows;

		// Token: 0x04003E18 RID: 15896
		private RuntimeUserSortTargetInfo m_userSortTargetInfo;

		// Token: 0x04003E19 RID: 15897
		private RuntimeOnDemandDataSetObjReference m_selfReference;

		// Token: 0x04003E1A RID: 15898
		private int[] m_sortFilterExpressionScopeInfoIndices;

		// Token: 0x04003E1B RID: 15899
		private RuntimeRICollection m_runtimeDataRegions;

		// Token: 0x04003E1C RID: 15900
		private bool m_firstNonAggregateRow = true;

		// Token: 0x04003E1D RID: 15901
		private Filters m_filters;

		// Token: 0x04003E1E RID: 15902
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> m_nonCustomAggregates;

		// Token: 0x04003E1F RID: 15903
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> m_customAggregates;

		// Token: 0x04003E20 RID: 15904
		private RuntimeLookupProcessing m_lookupProcessor;
	}
}

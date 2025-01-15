using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008E5 RID: 2277
	[PersistedWithinRequestOnly]
	internal abstract class RuntimeDataTablixGroupLeafObj : RuntimeGroupLeafObj, ISortDataHolder, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IOnDemandMemberInstance, IOnDemandMemberOwnerInstance, IOnDemandScopeInstance
	{
		// Token: 0x06007C92 RID: 31890 RVA: 0x002010A1 File Offset: 0x001FF2A1
		internal RuntimeDataTablixGroupLeafObj()
		{
		}

		// Token: 0x06007C93 RID: 31891 RVA: 0x002010CC File Offset: 0x001FF2CC
		internal RuntimeDataTablixGroupLeafObj(RuntimeDataTablixGroupRootObjReference groupRootRef, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(groupRootRef, objectType)
		{
			using (groupRootRef.PinValue())
			{
				RuntimeDataTablixGroupRootObj runtimeDataTablixGroupRootObj = groupRootRef.Value();
				Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode hierarchyDef = runtimeDataTablixGroupRootObj.HierarchyDef;
				bool flag = false;
				bool flag2 = base.HandleSortFilterEvent(hierarchyDef.IsColumn);
				DataActions dataActions;
				this.ConstructorHelper(runtimeDataTablixGroupRootObj, this.DataRegionDef, out flag, out dataActions);
				this.InitializeGroupScopedItems(hierarchyDef, ref dataActions);
				if (!flag)
				{
					this.m_dataAction = dataActions;
				}
				if (flag2)
				{
					this.m_dataAction |= DataActions.UserSort;
				}
				if (this.m_dataAction != DataActions.None || this.DataRegionDef.IsMatrixIDC)
				{
					this.m_dataRows = new ScalableList<DataFieldRow>(this.m_depth + 1, this.m_odpContext.TablixProcessingScalabilityCache, 30);
				}
			}
			this.m_odpContext.CreatedScopeInstance(this.m_hierarchyDef);
			this.m_scopeInstanceNumber = RuntimeDataRegionObj.AssignScopeInstanceNumber(this.m_hierarchyDef.DataScopeInfo);
		}

		// Token: 0x06007C94 RID: 31892 RVA: 0x002011D8 File Offset: 0x001FF3D8
		protected virtual void InitializeGroupScopedItems(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode member, ref DataActions innerDataAction)
		{
		}

		// Token: 0x170028B1 RID: 10417
		// (get) Token: 0x06007C95 RID: 31893 RVA: 0x002011DA File Offset: 0x001FF3DA
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> CellPostSortAggregates
		{
			get
			{
				return this.m_cellPostSortAggregates;
			}
		}

		// Token: 0x170028B2 RID: 10418
		// (get) Token: 0x06007C96 RID: 31894 RVA: 0x002011E2 File Offset: 0x001FF3E2
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion DataRegionDef
		{
			get
			{
				return base.MemberDef.DataRegionDef;
			}
		}

		// Token: 0x170028B3 RID: 10419
		// (get) Token: 0x06007C97 RID: 31895 RVA: 0x002011EF File Offset: 0x001FF3EF
		internal int HeadingLevel
		{
			get
			{
				return ((RuntimeDataTablixGroupRootObj)this.m_hierarchyRoot.Value()).HeadingLevel;
			}
		}

		// Token: 0x170028B4 RID: 10420
		// (get) Token: 0x06007C98 RID: 31896 RVA: 0x00201206 File Offset: 0x001FF406
		internal int InstanceIndex
		{
			get
			{
				return this.m_instanceIndex;
			}
		}

		// Token: 0x170028B5 RID: 10421
		// (get) Token: 0x06007C99 RID: 31897 RVA: 0x0020120E File Offset: 0x001FF40E
		internal int GroupLeafIndex
		{
			get
			{
				return this.m_groupLeafIndex;
			}
		}

		// Token: 0x170028B6 RID: 10422
		// (get) Token: 0x06007C9A RID: 31898 RVA: 0x00201216 File Offset: 0x001FF416
		protected bool HasInnerStaticMembersInSameScope
		{
			get
			{
				return base.MemberDef.InnerStaticMembersInSameScope != null && base.MemberDef.InnerStaticMembersInSameScope.Count != 0;
			}
		}

		// Token: 0x170028B7 RID: 10423
		// (get) Token: 0x06007C9B RID: 31899 RVA: 0x0020123A File Offset: 0x001FF43A
		List<object> IOnDemandMemberInstance.GroupExprValues
		{
			get
			{
				return this.m_groupExprValues;
			}
		}

		// Token: 0x06007C9C RID: 31900 RVA: 0x00201244 File Offset: 0x001FF444
		void ISortDataHolder.NextRow()
		{
			if (this.m_detailRowCounter == 0)
			{
				this.NextRow();
			}
			else
			{
				if (this.m_detailSortAdditionalGroupLeafs == null)
				{
					this.m_detailSortAdditionalGroupLeafs = new List<IHierarchyObj>();
				}
				IHierarchyObj hierarchyObj = base.GroupRoot.Value().CreateDetailSortHierarchyObj(this);
				this.m_detailSortAdditionalGroupLeafs.Add(hierarchyObj);
				hierarchyObj.NextRow(this);
			}
			this.m_detailRowCounter++;
		}

		// Token: 0x06007C9D RID: 31901 RVA: 0x002012A8 File Offset: 0x001FF4A8
		void ISortDataHolder.Traverse(ProcessingStages operation, ITraversalContext traversalContext)
		{
			base.TablixProcessingMoveNext(operation);
			switch (operation)
			{
			case ProcessingStages.SortAndFilter:
				this.SortAndFilter((AggregateUpdateContext)traversalContext);
				goto IL_0075;
			case ProcessingStages.PreparePeerGroupRunningValues:
				this.PrepareCalculateRunningValues();
				goto IL_0075;
			case ProcessingStages.RunningValues:
				this.CalculateDetailSortRunningValues((AggregateUpdateContext)traversalContext);
				goto IL_0075;
			case ProcessingStages.UpdateAggregates:
				this.UpdateAggregates((AggregateUpdateContext)traversalContext);
				goto IL_0075;
			case ProcessingStages.CreateGroupTree:
				this.CreateInstance((CreateInstancesTraversalContext)traversalContext);
				goto IL_0075;
			}
			Global.Tracer.Assert(false);
			IL_0075:
			int num = ((this.m_detailSortAdditionalGroupLeafs == null) ? 0 : this.m_detailSortAdditionalGroupLeafs.Count);
			for (int i = 0; i < num; i++)
			{
				this.m_detailSortAdditionalGroupLeafs[i].Traverse(operation, traversalContext);
			}
		}

		// Token: 0x06007C9E RID: 31902 RVA: 0x00201360 File Offset: 0x001FF560
		private void CalculateDetailSortRunningValues(AggregateUpdateContext aggContext)
		{
			if (this.m_dataRows != null && (this.m_dataAction & DataActions.PostSortAggregates) != DataActions.None)
			{
				int count = this.m_dataRows.Count;
				using (this.m_hierarchyRoot.PinValue())
				{
					RuntimeDataTablixGroupRootObj runtimeDataTablixGroupRootObj = this.m_hierarchyRoot.Value() as RuntimeDataTablixGroupRootObj;
					if (runtimeDataTablixGroupRootObj.DetailDataRows == null)
					{
						runtimeDataTablixGroupRootObj.DetailDataRows = new ScalableList<DataFieldRow>(runtimeDataTablixGroupRootObj.Depth, this.m_odpContext.TablixProcessingScalabilityCache);
					}
					this.UpdateSortFilterInfo(runtimeDataTablixGroupRootObj, runtimeDataTablixGroupRootObj.HierarchyDef.IsColumn, runtimeDataTablixGroupRootObj.DetailDataRows.Count);
					runtimeDataTablixGroupRootObj.DetailDataRows.AddRange(this.m_dataRows);
				}
				this.CalculateRunningValues(aggContext);
			}
		}

		// Token: 0x06007C9F RID: 31903 RVA: 0x00201424 File Offset: 0x001FF624
		private void UpdateSortFilterInfo(RuntimeGroupRootObj detailRoot, bool isColumnAxis, int rootRowCount)
		{
			List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo = this.m_odpContext.RuntimeSortFilterInfo;
			if (runtimeSortFilterInfo == null || detailRoot.HierarchyDef.DataRegionDef.SortFilterSourceDetailScopeInfo == null)
			{
				return;
			}
			for (int i = 0; i < runtimeSortFilterInfo.Count; i++)
			{
				IReference<RuntimeSortFilterEventInfo> reference = runtimeSortFilterInfo[i];
				using (reference.PinValue())
				{
					RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = reference.Value();
					if (base.SelfReference == runtimeSortFilterEventInfo.GetEventSourceScope(isColumnAxis))
					{
						runtimeSortFilterEventInfo.UpdateEventSourceScope(isColumnAxis, detailRoot.SelfReference, rootRowCount);
					}
					if (runtimeSortFilterEventInfo.HasDetailScopeInfo)
					{
						runtimeSortFilterEventInfo.UpdateDetailScopeInfo(detailRoot, isColumnAxis, rootRowCount, base.SelfReference);
					}
				}
			}
		}

		// Token: 0x06007CA0 RID: 31904 RVA: 0x002014D4 File Offset: 0x001FF6D4
		protected void ConstructorHelper(RuntimeDataTablixGroupRootObj groupRoot, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef, out bool handleMyDataAction, out DataActions innerDataAction)
		{
			this.m_dataAction = groupRoot.DataAction;
			handleMyDataAction = false;
			if (this.m_hierarchyDef.DataScopeInfo != null && this.m_hierarchyDef.DataScopeInfo.HasAggregatesToUpdateAtRowScope)
			{
				this.m_dataAction |= DataActions.AggregatesOfAggregates;
				handleMyDataAction = true;
			}
			if (groupRoot.ProcessOutermostStaticCells)
			{
				List<int> outermostColumnIndexes = this.OutermostColumnIndexes;
				foreach (int num in this.OutermostRowIndexes)
				{
					foreach (int num2 in outermostColumnIndexes)
					{
						handleMyDataAction |= this.CreateCellAggregates(dataRegionDef, num, num2);
					}
				}
			}
			this.ConstructRuntimeStructure(ref handleMyDataAction, out innerDataAction);
			if (base.IsOuterGrouping)
			{
				RuntimeDataTablixObjReference runtimeDataTablixObjReference = (RuntimeDataTablixObjReference)dataRegionDef.RuntimeDataRegionObj;
				using (runtimeDataTablixObjReference.PinValue())
				{
					RuntimeDataTablixObj runtimeDataTablixObj = runtimeDataTablixObjReference.Value();
					int hierarchyDynamicIndex = groupRoot.HierarchyDef.HierarchyDynamicIndex;
					this.m_groupLeafIndex = runtimeDataTablixObj.OuterGroupingCounters[hierarchyDynamicIndex] + 1;
					runtimeDataTablixObj.OuterGroupingCounters[hierarchyDynamicIndex] = this.m_groupLeafIndex;
				}
				dataRegionDef.UpdateOuterGroupingIndexes(this.m_hierarchyRoot as RuntimeDataTablixGroupRootObjReference, this.m_groupLeafIndex);
			}
		}

		// Token: 0x170028B8 RID: 10424
		// (get) Token: 0x06007CA1 RID: 31905 RVA: 0x00201644 File Offset: 0x001FF844
		private List<int> OutermostRowIndexes
		{
			get
			{
				if (base.MemberDef.IsColumn)
				{
					return base.MemberDef.DataRegionDef.OutermostStaticRowIndexes;
				}
				return base.MemberDef.GetCellIndexes();
			}
		}

		// Token: 0x170028B9 RID: 10425
		// (get) Token: 0x06007CA2 RID: 31906 RVA: 0x0020166F File Offset: 0x001FF86F
		private List<int> OutermostColumnIndexes
		{
			get
			{
				if (base.MemberDef.IsColumn)
				{
					return base.MemberDef.GetCellIndexes();
				}
				return base.MemberDef.DataRegionDef.OutermostStaticColumnIndexes;
			}
		}

		// Token: 0x06007CA3 RID: 31907 RVA: 0x0020169C File Offset: 0x001FF89C
		private bool CreateCellAggregates(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef, int rowIndex, int colIndex)
		{
			bool flag = false;
			Cell cell = dataRegionDef.Rows[rowIndex].Cells[colIndex];
			if (cell != null && !cell.SimpleGroupTreeCell)
			{
				if (cell.AggregateIndexes != null)
				{
					RuntimeDataRegionObj.CreateAggregates(this.m_odpContext, dataRegionDef.CellAggregates, cell.AggregateIndexes, ref this.m_firstPassCellNonCustomAggs, ref this.m_firstPassCellCustomAggs);
				}
				if (cell.HasInnerGroupTreeHierarchy)
				{
					this.ConstructOutermostCellContents(cell);
				}
				if (cell.PostSortAggregateIndexes != null)
				{
					flag = true;
					RuntimeDataRegionObj.CreateAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, dataRegionDef.CellPostSortAggregates, cell.PostSortAggregateIndexes, ref this.m_postSortAggregates);
				}
			}
			return flag;
		}

		// Token: 0x06007CA4 RID: 31908
		protected abstract void ConstructOutermostCellContents(Cell cell);

		// Token: 0x06007CA5 RID: 31909 RVA: 0x00201730 File Offset: 0x001FF930
		protected override void ConstructRuntimeStructure(ref bool handleMyDataAction, out DataActions innerDataAction)
		{
			using (this.m_hierarchyRoot.PinValue())
			{
				RuntimeDataTablixGroupRootObj runtimeDataTablixGroupRootObj = this.m_hierarchyRoot.Value() as RuntimeDataTablixGroupRootObj;
				Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode hierarchyDef = runtimeDataTablixGroupRootObj.HierarchyDef;
				Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef = hierarchyDef.DataRegionDef;
				base.ConstructRuntimeStructure(ref handleMyDataAction, out innerDataAction);
				if (!base.IsOuterGrouping && (!hierarchyDef.HasInnerDynamic || runtimeDataTablixGroupRootObj.HasLeafCells))
				{
					if (this.m_cellsList == null)
					{
						this.m_cellsList = new RuntimeCells[dataRegionDef.OuterGroupingDynamicMemberCount];
						RuntimeDataRegionObj.CreateAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, dataRegionDef.CellPostSortAggregates, ref this.m_cellPostSortAggregates);
					}
					int outerGroupingDynamicMemberCount = dataRegionDef.OuterGroupingDynamicMemberCount;
					for (int i = 0; i < outerGroupingDynamicMemberCount; i++)
					{
						IReference<RuntimeDataTablixGroupRootObj> reference = dataRegionDef.CurrentOuterGroupRootObjs[i];
						if (reference == null)
						{
							break;
						}
						this.CreateRuntimeCells(reference.Value());
					}
				}
				int num = (hierarchyDef.HasInnerDynamic ? hierarchyDef.InnerDynamicMembers.Count : 0);
				this.m_hasInnerHierarchy = num > 0;
				this.m_memberObjs = new IReference<RuntimeMemberObj>[Math.Max(1, num)];
				if (num == 0)
				{
					IReference<RuntimeMemberObj> reference2 = RuntimeDataTablixMemberObj.CreateRuntimeMemberObject(this.m_selfReference, null, ref innerDataAction, runtimeDataTablixGroupRootObj.OdpContext, runtimeDataTablixGroupRootObj.InnerGroupings, hierarchyDef.InnerStaticMembersInSameScope, runtimeDataTablixGroupRootObj.OutermostStatics, runtimeDataTablixGroupRootObj.HeadingLevel + 1, base.ObjectType);
					this.m_memberObjs[0] = reference2;
				}
				else
				{
					for (int j = 0; j < num; j++)
					{
						IReference<RuntimeMemberObj> reference3 = RuntimeDataTablixMemberObj.CreateRuntimeMemberObject(this.m_selfReference, hierarchyDef.InnerDynamicMembers[j], ref innerDataAction, runtimeDataTablixGroupRootObj.OdpContext, runtimeDataTablixGroupRootObj.InnerGroupings, (j == 0) ? hierarchyDef.InnerStaticMembersInSameScope : null, runtimeDataTablixGroupRootObj.OutermostStatics, runtimeDataTablixGroupRootObj.HeadingLevel + 1, base.ObjectType);
						this.m_memberObjs[j] = reference3;
					}
				}
			}
		}

		// Token: 0x06007CA6 RID: 31910 RVA: 0x002018F4 File Offset: 0x001FFAF4
		private void CreateRuntimeCells(RuntimeDataTablixGroupRootObj outerGroupRoot)
		{
			if (outerGroupRoot == null || !outerGroupRoot.HasLeafCells)
			{
				return;
			}
			int hierarchyDynamicIndex = outerGroupRoot.HierarchyDef.HierarchyDynamicIndex;
			if (this.m_cellsList[hierarchyDynamicIndex] != null)
			{
				return;
			}
			this.m_cellsList[hierarchyDynamicIndex] = new RuntimeCells(base.Depth, this.m_odpContext.TablixProcessingScalabilityCache);
		}

		// Token: 0x06007CA7 RID: 31911
		internal abstract void CreateCell(RuntimeCells cellsCollection, int collectionKey, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode outerGroupingMember, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode innerGroupingMember, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef);

		// Token: 0x06007CA8 RID: 31912 RVA: 0x00201944 File Offset: 0x001FFB44
		internal override void NextRow()
		{
			DomainScopeContext domainScopeContext = base.OdpContext.DomainScopeContext;
			if (domainScopeContext != null)
			{
				DomainScopeContext.DomainScopeInfo currentDomainScope = domainScopeContext.CurrentDomainScope;
				if (currentDomainScope != null)
				{
					if (this.m_firstRow == null)
					{
						this.m_firstRow = currentDomainScope.CurrentRow;
					}
					return;
				}
			}
			if (base.IsOuterGrouping)
			{
				this.DataRegionDef.CurrentOuterGroupRoot = (RuntimeDataTablixGroupRootObjReference)this.m_hierarchyRoot;
				this.DataRegionDef.UpdateOuterGroupingIndexes((RuntimeDataTablixGroupRootObjReference)this.m_hierarchyRoot, this.m_groupLeafIndex);
			}
			if (base.IsOuterGrouping || !this.m_odpContext.PeerOuterGroupProcessing)
			{
				base.UpdateAggregateInfo();
			}
			if (base.IsOuterGrouping)
			{
				this.DataRegionDef.SaveOuterGroupingAggregateRowInfo(this.m_hierarchyDef.HierarchyDynamicIndex, this.m_odpContext);
			}
			if (base.IsOuterGrouping || !this.m_odpContext.PeerOuterGroupProcessing)
			{
				FieldsImpl fieldsImpl = this.m_odpContext.ReportObjectModel.FieldsImpl;
				if (fieldsImpl.AggregationFieldCount == 0 && fieldsImpl.ValidAggregateRow)
				{
					RuntimeDataRegionObj.UpdateAggregates(this.m_odpContext, this.m_firstPassCellCustomAggs, false);
				}
				if (!this.m_odpContext.ReportObjectModel.FieldsImpl.IsAggregateRow)
				{
					RuntimeDataRegionObj.UpdateAggregates(this.m_odpContext, this.m_firstPassCellNonCustomAggs, false);
				}
			}
			base.InternalNextRow();
		}

		// Token: 0x06007CA9 RID: 31913 RVA: 0x00201A70 File Offset: 0x001FFC70
		internal void PeerOuterGroupProcessCells()
		{
			Global.Tracer.Assert(!base.IsOuterGrouping && this.m_odpContext.PeerOuterGroupProcessing);
			DataScopeInfo dataScopeInfo = this.m_hierarchyDef.DataScopeInfo;
			if (dataScopeInfo != null && dataScopeInfo.DataSet != null && dataScopeInfo.DataSet.HasScopeWithCustomAggregates)
			{
				dataScopeInfo.ApplyGroupingFieldsForServerAggregates(this.m_odpContext.ReportObjectModel.FieldsImpl);
			}
			this.ProcessCells();
		}

		// Token: 0x06007CAA RID: 31914 RVA: 0x00201AE0 File Offset: 0x001FFCE0
		protected override void SendToInner()
		{
			RuntimeDataTablixGroupRootObjReference runtimeDataTablixGroupRootObjReference = (RuntimeDataTablixGroupRootObjReference)this.m_hierarchyRoot;
			if (base.IsOuterGrouping)
			{
				this.DataRegionDef.ResetOuterGroupingIndexesForOuterPeerGroup(runtimeDataTablixGroupRootObjReference.Value().HierarchyDef.HierarchyDynamicIndex);
				this.DataRegionDef.CurrentOuterGroupRoot = runtimeDataTablixGroupRootObjReference;
				this.DataRegionDef.UpdateOuterGroupingIndexes(runtimeDataTablixGroupRootObjReference, this.m_groupLeafIndex);
			}
			base.SendToInner();
			if (!this.DataRegionDef.IsMatrixIDC)
			{
				this.ProcessCells();
			}
			int num = ((this.m_memberObjs != null) ? this.m_memberObjs.Length : 0);
			if (num != 0)
			{
				bool peerOuterGroupProcessing = this.m_odpContext.PeerOuterGroupProcessing;
				AggregateRowInfo aggregateRowInfo = null;
				if (base.IsOuterGrouping || num > 1)
				{
					if (aggregateRowInfo == null)
					{
						aggregateRowInfo = new AggregateRowInfo();
					}
					aggregateRowInfo.SaveAggregateInfo(this.m_odpContext);
				}
				for (int i = 0; i < num; i++)
				{
					if (base.IsOuterGrouping)
					{
						if (i != 0)
						{
							this.m_odpContext.PeerOuterGroupProcessing = true;
						}
						this.DataRegionDef.SetDataTablixAggregateRowInfo(aggregateRowInfo);
					}
					IReference<RuntimeMemberObj> reference = this.m_memberObjs[i];
					using (reference.PinValue())
					{
						reference.Value().NextRow(base.IsOuterGrouping, this.m_odpContext);
					}
					if (aggregateRowInfo != null)
					{
						aggregateRowInfo.RestoreAggregateInfo(this.m_odpContext);
					}
				}
				this.m_odpContext.PeerOuterGroupProcessing = peerOuterGroupProcessing;
			}
		}

		// Token: 0x06007CAB RID: 31915 RVA: 0x00201C38 File Offset: 0x001FFE38
		internal RuntimeCell GetOrCreateCell(RuntimeDataTablixGroupLeafObj rowGroupLeaf)
		{
			Global.Tracer.Assert(!base.IsOuterGrouping, "(!IsOuterGrouping)");
			RuntimeCell runtimeCell = null;
			if (this.m_cellsList != null)
			{
				IReference<RuntimeDataTablixGroupRootObj> reference = (IReference<RuntimeDataTablixGroupRootObj>)rowGroupLeaf.HierarchyRoot;
				RuntimeDataTablixGroupRootObj runtimeDataTablixGroupRootObj = reference.Value();
				int hierarchyDynamicIndex = runtimeDataTablixGroupRootObj.HierarchyDef.HierarchyDynamicIndex;
				if (this.m_cellsList[hierarchyDynamicIndex] == null)
				{
					this.CreateRuntimeCells(runtimeDataTablixGroupRootObj);
				}
				RuntimeCells runtimeCells = this.m_cellsList[hierarchyDynamicIndex];
				if (runtimeCells != null)
				{
					IDisposable disposable;
					runtimeCell = runtimeCells.GetOrCreateCell(this.DataRegionDef, (RuntimeDataTablixGroupLeafObjReference)base.SelfReference, reference, rowGroupLeaf.GroupLeafIndex, out disposable);
				}
			}
			return runtimeCell;
		}

		// Token: 0x06007CAC RID: 31916 RVA: 0x00201CC8 File Offset: 0x001FFEC8
		private void ProcessCells()
		{
			if (this.m_cellsList != null)
			{
				Global.Tracer.Assert(!base.IsOuterGrouping, "(!IsOuterGrouping)");
				if (!this.m_odpContext.PeerOuterGroupProcessing)
				{
					IReference<RuntimeDataTablixObj> ownerDataTablix = this.GetOwnerDataTablix();
					using (ownerDataTablix.PinValue())
					{
						RuntimeDataTablixObj runtimeDataTablixObj = ownerDataTablix.Value();
						if (runtimeDataTablixObj.InnerGroupsWithCellsForOuterPeerGroupProcessing != null)
						{
							runtimeDataTablixObj.InnerGroupsWithCellsForOuterPeerGroupProcessing.Add((RuntimeDataTablixGroupLeafObjReference)this.m_selfReference);
						}
					}
				}
				int[] outerGroupingIndexes = this.DataRegionDef.OuterGroupingIndexes;
				for (int i = 0; i < outerGroupingIndexes.Length; i++)
				{
					IReference<RuntimeDataTablixGroupRootObj> reference = this.DataRegionDef.CurrentOuterGroupRootObjs[i];
					if (reference != null)
					{
						RuntimeDataTablixGroupRootObj runtimeDataTablixGroupRootObj = reference.Value();
						int hierarchyDynamicIndex = runtimeDataTablixGroupRootObj.HierarchyDef.HierarchyDynamicIndex;
						int num = outerGroupingIndexes[i];
						AggregateRowInfo aggregateRowInfo = AggregateRowInfo.CreateAndSaveAggregateInfo(this.m_odpContext);
						this.DataRegionDef.SetCellAggregateRowInfo(i, this.m_odpContext);
						if (this.m_cellsList[hierarchyDynamicIndex] == null)
						{
							this.CreateRuntimeCells(runtimeDataTablixGroupRootObj);
						}
						RuntimeCells runtimeCells = this.m_cellsList[hierarchyDynamicIndex];
						if (runtimeCells != null)
						{
							IDisposable disposable2;
							RuntimeCell runtimeCell = runtimeCells.GetAndPinCell(num, out disposable2);
							if (runtimeCell == null)
							{
								this.CreateCell(runtimeCells, num, runtimeDataTablixGroupRootObj.HierarchyDef, base.MemberDef, this.DataRegionDef);
								runtimeCell = runtimeCells.GetAndPinCell(num, out disposable2);
							}
							runtimeCell.NextRow();
							if (disposable2 != null)
							{
								disposable2.Dispose();
							}
						}
						aggregateRowInfo.RestoreAggregateInfo(this.m_odpContext);
					}
				}
			}
		}

		// Token: 0x06007CAD RID: 31917 RVA: 0x00201E48 File Offset: 0x00200048
		internal override void GetScopeValues(IReference<IHierarchyObj> targetScopeObj, List<object>[] scopeValues, ref int index)
		{
			if (base.GroupRoot.Value().IsDetailGroup && (targetScopeObj == null || this != targetScopeObj.Value()))
			{
				base.DetailGetScopeValues(this.OuterScope, targetScopeObj, scopeValues, ref index);
				return;
			}
			base.GetScopeValues(targetScopeObj, scopeValues, ref index);
		}

		// Token: 0x06007CAE RID: 31918 RVA: 0x00201E84 File Offset: 0x00200084
		internal override bool TargetScopeMatched(int index, bool detailSort)
		{
			if (base.GroupRoot.Value().IsDetailGroup)
			{
				return base.DetailTargetScopeMatched(base.MemberDef.DataRegionDef, this.OuterScope, base.MemberDef.IsColumn, index);
			}
			return (detailSort && base.GroupingDef.SortFilterScopeInfo == null) || (this.m_targetScopeMatched != null && this.m_targetScopeMatched[index]);
		}

		// Token: 0x06007CAF RID: 31919 RVA: 0x00201EF0 File Offset: 0x002000F0
		internal override bool SortAndFilter(AggregateUpdateContext aggContext)
		{
			this.SetupEnvironment();
			if (this.m_userSortTargetInfo != null)
			{
				this.m_userSortTargetInfo.EnterProcessUserSortPhase(this.m_odpContext);
			}
			AggregateUpdateQueue aggregateUpdateQueue = null;
			if (this.m_odpContext.HasSecondPassOperation(SecondPassOperations.FilteringOrAggregatesOrDomainScope))
			{
				aggregateUpdateQueue = RuntimeDataRegionObj.AggregateOfAggregatesStart(aggContext, this, this.m_hierarchyDef.DataScopeInfo, this.m_aggregatesOfAggregates, AggregateUpdateFlags.Both, false);
			}
			bool flag3;
			using (this.m_hierarchyRoot.PinValue())
			{
				RuntimeDataTablixGroupRootObj runtimeDataTablixGroupRootObj = (RuntimeDataTablixGroupRootObj)this.m_hierarchyRoot.Value();
				bool flag = false;
				int innerDomainScopeCount = runtimeDataTablixGroupRootObj.HierarchyDef.InnerDomainScopeCount;
				DomainScopeContext domainScopeContext = base.OdpContext.DomainScopeContext;
				if (this.m_odpContext.HasSecondPassOperation(SecondPassOperations.FilteringOrAggregatesOrDomainScope) && innerDomainScopeCount > 0)
				{
					domainScopeContext.AddDomainScopes(this.m_memberObjs, this.m_memberObjs.Length - innerDomainScopeCount);
				}
				this.TraverseStaticContents(ProcessingStages.SortAndFilter, aggContext);
				if (this.m_hasInnerHierarchy)
				{
					bool flag2 = true;
					for (int i = 0; i < this.m_memberObjs.Length; i++)
					{
						IReference<RuntimeMemberObj> reference = this.m_memberObjs[i];
						using (reference.PinValue())
						{
							RuntimeMemberObj runtimeMemberObj = reference.Value();
							if (runtimeMemberObj.SortAndFilter(aggContext))
							{
								flag2 = false;
							}
							else if (null == runtimeMemberObj.GroupRoot)
							{
								flag2 = false;
							}
							else if (runtimeMemberObj.GroupRoot.Value().HierarchyDef.InnerStaticMembersInSameScope != null)
							{
								flag2 = false;
							}
						}
					}
					if (flag2)
					{
						Global.Tracer.Assert((SecondPassOperations.FilteringOrAggregatesOrDomainScope & this.m_odpContext.SecondPassOperation) > SecondPassOperations.None, "(0 != (SecondPassOperations.Filtering & m_odpContext.SecondPassOperation))");
						Global.Tracer.Assert(runtimeDataTablixGroupRootObj.GroupFilters != null, "(null != groupRoot.GroupFilters)");
						runtimeDataTablixGroupRootObj.GroupFilters.FailFilters = true;
						flag = true;
					}
				}
				if (this.m_odpContext.HasSecondPassOperation(SecondPassOperations.FilteringOrAggregatesOrDomainScope))
				{
					RuntimeDataRegionObj.AggregatesOfAggregatesEnd(this, aggContext, aggregateUpdateQueue, this.m_hierarchyDef.DataScopeInfo, this.m_aggregatesOfAggregates, false);
				}
				flag3 = base.SortAndFilter(aggContext);
				if (this.m_odpContext.HasSecondPassOperation(SecondPassOperations.FilteringOrAggregatesOrDomainScope) && innerDomainScopeCount > 0)
				{
					domainScopeContext.RemoveDomainScopes(this.m_memberObjs, this.m_memberObjs.Length - innerDomainScopeCount);
				}
				if (flag)
				{
					runtimeDataTablixGroupRootObj.GroupFilters.FailFilters = false;
				}
			}
			if (this.m_userSortTargetInfo != null)
			{
				this.m_userSortTargetInfo.LeaveProcessUserSortPhase(this.m_odpContext);
			}
			return flag3;
		}

		// Token: 0x06007CB0 RID: 31920 RVA: 0x00202154 File Offset: 0x00200354
		internal override void PostFilterNextRow(AggregateUpdateContext context)
		{
			AggregateUpdateQueue aggregateUpdateQueue = null;
			if (this.m_odpContext.HasSecondPassOperation(SecondPassOperations.FilteringOrAggregatesOrDomainScope))
			{
				aggregateUpdateQueue = RuntimeDataRegionObj.AggregateOfAggregatesStart(context, this, this.m_hierarchyDef.DataScopeInfo, this.m_aggregatesOfAggregates, AggregateUpdateFlags.None, false);
			}
			this.TraverseCellList(ProcessingStages.SortAndFilter, context);
			if (this.m_odpContext.HasSecondPassOperation(SecondPassOperations.FilteringOrAggregatesOrDomainScope))
			{
				RuntimeDataRegionObj.AggregatesOfAggregatesEnd(this, context, aggregateUpdateQueue, this.m_hierarchyDef.DataScopeInfo, this.m_aggregatesOfAggregates, true);
			}
			base.PostFilterNextRow(context);
		}

		// Token: 0x06007CB1 RID: 31921 RVA: 0x002021C3 File Offset: 0x002003C3
		protected virtual void TraverseStaticContents(ProcessingStages operation, AggregateUpdateContext context)
		{
		}

		// Token: 0x06007CB2 RID: 31922 RVA: 0x002021C8 File Offset: 0x002003C8
		private void TraverseCellList(ProcessingStages operation, AggregateUpdateContext aggContext)
		{
			if (this.m_cellsList != null)
			{
				for (int i = 0; i < this.m_cellsList.Length; i++)
				{
					if (this.m_cellsList[i] != null)
					{
						if (operation != ProcessingStages.SortAndFilter)
						{
							if (operation != ProcessingStages.UpdateAggregates)
							{
								Global.Tracer.Assert(false, "Unknown operation in TraverseCellList");
							}
							else
							{
								this.m_cellsList[i].UpdateAggregates(aggContext);
							}
						}
						else
						{
							this.m_cellsList[i].SortAndFilter(aggContext);
						}
					}
				}
			}
		}

		// Token: 0x06007CB3 RID: 31923 RVA: 0x00202234 File Offset: 0x00200434
		public override void UpdateAggregates(AggregateUpdateContext aggContext)
		{
			this.SetupEnvironment();
			if (RuntimeDataRegionObj.UpdateAggregatesAtScope(aggContext, this, this.m_hierarchyDef.DataScopeInfo, AggregateUpdateFlags.Both, false))
			{
				this.TraverseStaticContents(ProcessingStages.UpdateAggregates, aggContext);
				if (this.m_hasInnerHierarchy)
				{
					for (int i = 0; i < this.m_memberObjs.Length; i++)
					{
						IReference<RuntimeMemberObj> reference = this.m_memberObjs[i];
						using (reference.PinValue())
						{
							reference.Value().UpdateAggregates(aggContext);
						}
					}
				}
				this.TraverseCellList(ProcessingStages.UpdateAggregates, aggContext);
			}
		}

		// Token: 0x06007CB4 RID: 31924 RVA: 0x002022C0 File Offset: 0x002004C0
		protected static void CalculateInnerRunningValues(IReference<RuntimeMemberObj>[] memberObjs, Dictionary<string, IReference<RuntimeGroupRootObj>> groupCol, IReference<RuntimeGroupRootObj> lastGroup, AggregateUpdateContext aggContext)
		{
			if (memberObjs != null)
			{
				foreach (IReference<RuntimeMemberObj> reference in memberObjs)
				{
					using (reference.PinValue())
					{
						reference.Value().CalculateRunningValues(groupCol, lastGroup, aggContext);
					}
				}
			}
		}

		// Token: 0x06007CB5 RID: 31925 RVA: 0x00202314 File Offset: 0x00200514
		protected override void PrepareCalculateRunningValues()
		{
			this.m_processHeading = true;
			if (this.m_hasInnerHierarchy)
			{
				for (int i = 0; i < this.m_memberObjs.Length; i++)
				{
					IReference<RuntimeMemberObj> reference = this.m_memberObjs[i];
					using (reference.PinValue())
					{
						reference.Value().PrepareCalculateRunningValues();
					}
				}
			}
		}

		// Token: 0x06007CB6 RID: 31926 RVA: 0x0020237C File Offset: 0x0020057C
		internal override void CalculateRunningValues(AggregateUpdateContext aggContext)
		{
			this.SetupEnvironment();
			AggregateUpdateQueue aggregateUpdateQueue = null;
			bool flag = false;
			DataActions dataActions = DataActions.PostSortAggregates;
			if (this.m_processHeading)
			{
				flag = FlagUtils.HasFlag(this.m_dataAction, DataActions.PostSortAggregates);
				aggregateUpdateQueue = RuntimeDataRegionObj.AggregateOfAggregatesStart(aggContext, this, this.m_hierarchyDef.DataScopeInfo, this.m_postSortAggregatesOfAggregates, flag ? AggregateUpdateFlags.ScopedAggregates : AggregateUpdateFlags.Both, false);
				if (flag && aggContext.LastScopeNeedsRowAggregateProcessing())
				{
					dataActions |= DataActions.PostSortAggregatesOfAggregates;
				}
			}
			bool isOuterGrouping = base.IsOuterGrouping;
			RuntimeDataTablixGroupRootObjReference runtimeDataTablixGroupRootObjReference = (RuntimeDataTablixGroupRootObjReference)this.m_hierarchyRoot;
			using (runtimeDataTablixGroupRootObjReference.PinValue())
			{
				RuntimeDataTablixGroupRootObj runtimeDataTablixGroupRootObj = runtimeDataTablixGroupRootObjReference.Value();
				Dictionary<string, IReference<RuntimeGroupRootObj>> groupCollection = runtimeDataTablixGroupRootObj.GroupCollection;
				if (this.m_processHeading)
				{
					if (flag)
					{
						base.ReadRows(dataActions, aggContext);
						if (isOuterGrouping)
						{
							this.m_dataRows = null;
						}
					}
					RuntimeDataTablixGroupLeafObj.CalculateInnerRunningValues(this.m_memberObjs, groupCollection, runtimeDataTablixGroupRootObjReference, aggContext);
				}
				else if (this.m_hasInnerHierarchy)
				{
					RuntimeDataTablixGroupLeafObj.CalculateInnerRunningValues(this.m_memberObjs, groupCollection, runtimeDataTablixGroupRootObjReference, aggContext);
				}
				RuntimeGroupRootObjReference runtimeGroupRootObjReference = runtimeDataTablixGroupRootObjReference;
				if (isOuterGrouping)
				{
					if (!this.m_hasInnerHierarchy || this.HasInnerStaticMembersInSameScope)
					{
						this.DataRegionDef.CurrentOuterGroupRoot = runtimeDataTablixGroupRootObjReference;
						this.DataRegionDef.OuterGroupingIndexes[runtimeDataTablixGroupRootObj.HierarchyDef.HierarchyDynamicIndex] = this.m_groupLeafIndex;
						RuntimeDataTablixGroupLeafObj.CalculateInnerRunningValues(runtimeDataTablixGroupRootObj.InnerGroupings, groupCollection, runtimeGroupRootObjReference, aggContext);
					}
				}
				else if (this.m_cellsList != null)
				{
					IReference<RuntimeDataTablixGroupRootObj> currentOuterGroupRoot = this.DataRegionDef.CurrentOuterGroupRoot;
					using (currentOuterGroupRoot.PinValue())
					{
						RuntimeDataTablixGroupRootObj runtimeDataTablixGroupRootObj2 = currentOuterGroupRoot.Value();
						int hierarchyDynamicIndex = runtimeDataTablixGroupRootObj2.HierarchyDef.HierarchyDynamicIndex;
						if (this.m_cellsList[hierarchyDynamicIndex] == null && this.m_odpContext.PeerOuterGroupProcessing)
						{
							this.CreateRuntimeCells(runtimeDataTablixGroupRootObj2);
						}
						RuntimeCells runtimeCells = this.m_cellsList[hierarchyDynamicIndex];
						if (runtimeCells != null)
						{
							this.DataRegionDef.ProcessCellRunningValues = true;
							runtimeCells.CalculateRunningValues(this.DataRegionDef, groupCollection, runtimeGroupRootObjReference, (RuntimeDataTablixGroupLeafObjReference)this.m_selfReference, aggContext);
							this.DataRegionDef.ProcessCellRunningValues = false;
						}
					}
				}
			}
			this.CalculateRunningValuesForStaticContents(aggContext);
			if (this.m_processHeading)
			{
				RuntimeDataRegionObj.AggregatesOfAggregatesEnd(this, aggContext, aggregateUpdateQueue, this.m_hierarchyDef.DataScopeInfo, this.m_postSortAggregatesOfAggregates, true);
				if (this.m_odpContext.HasPreviousAggregates)
				{
					base.CalculatePreviousAggregates(isOuterGrouping);
				}
			}
			this.StoreCalculatedRunningValues();
		}

		// Token: 0x06007CB7 RID: 31927 RVA: 0x002025DC File Offset: 0x002007DC
		protected virtual void CalculateRunningValuesForStaticContents(AggregateUpdateContext aggContext)
		{
		}

		// Token: 0x06007CB8 RID: 31928 RVA: 0x002025E0 File Offset: 0x002007E0
		protected void StoreCalculatedRunningValues()
		{
			if (this.m_processHeading)
			{
				using (this.m_hierarchyRoot.PinValue())
				{
					((RuntimeDataTablixGroupRootObj)this.m_hierarchyRoot.Value()).DoneReadingRows(ref this.m_runningValueValues, ref this.m_runningValueOfAggregateValues, ref this.m_cellRunningValueValues);
					this.m_processHeading = false;
				}
			}
			this.ResetScopedRunningValues();
		}

		// Token: 0x06007CB9 RID: 31929 RVA: 0x00202654 File Offset: 0x00200854
		protected override void ResetScopedRunningValues()
		{
			this.m_processHeading = false;
			base.ResetScopedRunningValues();
		}

		// Token: 0x06007CBA RID: 31930 RVA: 0x00202664 File Offset: 0x00200864
		public override void ReadRow(DataActions dataAction, ITraversalContext context)
		{
			if (DataActions.UserSort == dataAction)
			{
				RuntimeDataRegionObj.CommonFirstRow(this.m_odpContext, ref this.m_firstRowIsAggregate, ref this.m_firstRow);
				base.CommonNextRow(this.m_dataRows);
				return;
			}
			if (this.DataRegionDef.ProcessCellRunningValues)
			{
				if (FlagUtils.HasFlag(dataAction, DataActions.PostSortAggregates) && this.m_cellPostSortAggregates != null)
				{
					RuntimeDataRegionObj.UpdateAggregates(this.m_odpContext, this.m_cellPostSortAggregates, false);
				}
				using (this.m_hierarchyRoot.PinValue())
				{
					((IScope)this.m_hierarchyRoot.Value()).ReadRow(dataAction, context);
					return;
				}
			}
			base.ReadRow(dataAction, context);
		}

		// Token: 0x06007CBB RID: 31931 RVA: 0x0020270C File Offset: 0x0020090C
		public override void SetupEnvironment()
		{
			base.SetupEnvironment();
			this.SetupAggregateValues(this.m_firstPassCellNonCustomAggs, this.m_firstPassCellCustomAggs);
		}

		// Token: 0x06007CBC RID: 31932 RVA: 0x00202726 File Offset: 0x00200926
		private void SetupAggregateValues(List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> nonCustomAggCollection, List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> customAggCollection)
		{
			base.SetupAggregates(nonCustomAggCollection);
			base.SetupAggregates(customAggCollection);
		}

		// Token: 0x06007CBD RID: 31933 RVA: 0x00202736 File Offset: 0x00200936
		protected virtual void CreateInstanceHeadingContents()
		{
		}

		// Token: 0x06007CBE RID: 31934 RVA: 0x00202738 File Offset: 0x00200938
		internal override void CreateInstance(CreateInstancesTraversalContext traversalContext)
		{
			this.SetupEnvironment();
			RuntimeDataTablixGroupRootObjReference runtimeDataTablixGroupRootObjReference = (RuntimeDataTablixGroupRootObjReference)this.m_hierarchyRoot;
			using (runtimeDataTablixGroupRootObjReference.PinValue())
			{
				RuntimeDataTablixGroupRootObj runtimeDataTablixGroupRootObj = runtimeDataTablixGroupRootObjReference.Value();
				ScopeInstance scopeInstance = traversalContext.ParentInstance;
				base.SetupRunningValues(base.MemberDef.RunningValues, this.m_runningValueValues);
				if (base.MemberDef.DataScopeInfo != null)
				{
					base.SetupRunningValues(base.MemberDef.DataScopeInfo.RunningValuesOfAggregates, this.m_runningValueOfAggregateValues);
				}
				if (this.m_targetScopeMatched != null)
				{
					base.MemberDef.Grouping.SortFilterScopeMatched = this.m_targetScopeMatched;
				}
				IReference<RuntimeDataTablixGroupRootObj> reference;
				if (base.IsOuterGrouping)
				{
					reference = runtimeDataTablixGroupRootObjReference;
					this.DataRegionDef.CurrentOuterGroupRoot = reference;
					this.DataRegionDef.UpdateOuterGroupingIndexes(runtimeDataTablixGroupRootObjReference, this.m_groupLeafIndex);
					this.DataRegionDef.NewOuterCells();
				}
				else
				{
					reference = this.DataRegionDef.CurrentOuterGroupRoot;
				}
				bool flag = false;
				if (this.m_sequentialMemberIndexWithinScopeLevel == -1)
				{
					this.m_sequentialMemberIndexWithinScopeLevel = this.DataRegionDef.AddMemberInstance(base.MemberDef.IsColumn);
					this.m_memberInstance = DataRegionMemberInstance.CreateInstance((IMemberHierarchy)scopeInstance, this.m_odpContext, base.MemberDef, this.m_firstRow.StreamOffset, this.m_sequentialMemberIndexWithinScopeLevel, this.m_recursiveLevel, runtimeDataTablixGroupRootObj.IsDetailGroup ? null : this.m_groupExprValues, this.m_variableValues, out this.m_instanceIndex);
					flag = true;
					if (runtimeDataTablixGroupRootObj.HasParent)
					{
						runtimeDataTablixGroupRootObj.SetRecursiveParentIndex(this.m_instanceIndex, this.m_recursiveLevel);
						if (this.m_recursiveLevel > 0)
						{
							this.m_memberInstance.RecursiveParentIndex = runtimeDataTablixGroupRootObj.GetRecursiveParentIndex(this.m_recursiveLevel - 1);
						}
						this.m_memberInstance.HasRecursiveChildren = new bool?(this.m_firstChild != null || this.m_grouping != null);
					}
					scopeInstance = this.m_memberInstance;
					this.CreateInstanceHeadingContents();
				}
				runtimeDataTablixGroupRootObj.CurrentMemberIndexWithinScopeLevel = this.m_sequentialMemberIndexWithinScopeLevel;
				runtimeDataTablixGroupRootObj.CurrentMemberInstance = this.m_memberInstance;
				if (this.m_memberObjs != null)
				{
					for (int i = 0; i < this.m_memberObjs.Length; i++)
					{
						IReference<RuntimeMemberObj> reference2 = this.m_memberObjs[i];
						using (reference2.PinValue())
						{
							reference2.Value().CreateInstances(base.SelfReference, this.m_odpContext, runtimeDataTablixGroupRootObj.DataRegionInstance, base.IsOuterGrouping, reference, scopeInstance, traversalContext.InnerMembers, traversalContext.InnerGroupLeafRef);
						}
					}
				}
				if (flag)
				{
					this.m_memberInstance.InstanceComplete();
					this.m_memberInstance = null;
				}
			}
		}

		// Token: 0x06007CBF RID: 31935 RVA: 0x002029E4 File Offset: 0x00200BE4
		internal void CreateInnerGroupingsOrCells(DataRegionInstance dataRegionInstance, ScopeInstance parentInstance, IReference<RuntimeDataTablixGroupRootObj> currOuterGroupRoot, IReference<RuntimeMemberObj>[] innerMembers, IReference<RuntimeDataTablixGroupLeafObj> innerGroupLeafRef)
		{
			this.SetupEnvironment();
			if (innerMembers != null)
			{
				if (base.IsOuterGrouping)
				{
					innerGroupLeafRef = null;
				}
				else
				{
					innerGroupLeafRef = (IReference<RuntimeDataTablixGroupLeafObj>)base.SelfReference;
				}
				foreach (IReference<RuntimeMemberObj> reference in innerMembers)
				{
					using (reference.PinValue())
					{
						reference.Value().CreateInstances(base.SelfReference, this.m_odpContext, dataRegionInstance, !base.IsOuterGrouping, currOuterGroupRoot, dataRegionInstance, null, innerGroupLeafRef);
					}
				}
				return;
			}
			IDisposable disposable2 = null;
			RuntimeDataTablixGroupLeafObj runtimeDataTablixGroupLeafObj;
			if (base.IsOuterGrouping && innerGroupLeafRef != null)
			{
				disposable2 = innerGroupLeafRef.PinValue();
				runtimeDataTablixGroupLeafObj = innerGroupLeafRef.Value();
			}
			else
			{
				runtimeDataTablixGroupLeafObj = this;
			}
			if (currOuterGroupRoot != null)
			{
				runtimeDataTablixGroupLeafObj.CreateCellInstance(dataRegionInstance, currOuterGroupRoot);
			}
			else if (base.MemberDef.IsColumn)
			{
				runtimeDataTablixGroupLeafObj.CreateOutermostStatics(dataRegionInstance, this.m_sequentialMemberIndexWithinScopeLevel);
			}
			else
			{
				runtimeDataTablixGroupLeafObj.CreateOutermostStatics(this.m_memberInstance, 0);
			}
			if (disposable2 != null)
			{
				disposable2.Dispose();
			}
		}

		// Token: 0x06007CC0 RID: 31936 RVA: 0x00202AE0 File Offset: 0x00200CE0
		protected virtual void CreateCellInstance(DataRegionInstance dataRegionInstance, IReference<RuntimeDataTablixGroupRootObj> currOuterGroupRoot)
		{
			this.SetupEnvironment();
			int hierarchyDynamicIndex = currOuterGroupRoot.Value().HierarchyDef.HierarchyDynamicIndex;
			if (this.m_cellsList[hierarchyDynamicIndex] == null)
			{
				this.CreateRuntimeCells(currOuterGroupRoot.Value());
			}
			Global.Tracer.Assert(this.m_cellsList[hierarchyDynamicIndex] != null, "(null != m_cellsList[index])");
			dataRegionInstance.DataRegionDef.AddCell();
			IDisposable disposable;
			RuntimeCell orCreateCell = this.m_cellsList[hierarchyDynamicIndex].GetOrCreateCell(this.DataRegionDef, (RuntimeDataTablixGroupLeafObjReference)this.m_selfReference, currOuterGroupRoot, out disposable);
			if (orCreateCell != null)
			{
				if (base.MemberDef.IsColumn)
				{
					orCreateCell.CreateInstance(currOuterGroupRoot.Value().CurrentMemberInstance, this.m_sequentialMemberIndexWithinScopeLevel);
				}
				else
				{
					orCreateCell.CreateInstance(this.m_memberInstance, currOuterGroupRoot.Value().CurrentMemberIndexWithinScopeLevel);
				}
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06007CC1 RID: 31937 RVA: 0x00202BAC File Offset: 0x00200DAC
		internal void CreateStaticCells(DataRegionInstance dataRegionInstance, ScopeInstance parentInstance, IReference<RuntimeDataTablixGroupRootObj> currOuterGroupRoot, bool outerGroupings, List<int> staticLeafCellIndexes, IReference<RuntimeMemberObj>[] innerMembers, IReference<RuntimeDataTablixGroupLeafObj> innerGroupLeafRef)
		{
			if ((!base.IsOuterGrouping || outerGroupings) && (!base.IsOuterGrouping || innerMembers != null || innerGroupLeafRef != null))
			{
				this.CreateInnerGroupingsOrCells(dataRegionInstance, parentInstance, currOuterGroupRoot, innerMembers, innerGroupLeafRef);
				return;
			}
			if (base.MemberDef.IsColumn)
			{
				this.CreateOutermostStatics(dataRegionInstance, this.m_sequentialMemberIndexWithinScopeLevel);
				return;
			}
			this.CreateOutermostStatics(this.m_memberInstance, 0);
		}

		// Token: 0x06007CC2 RID: 31938 RVA: 0x00202C0C File Offset: 0x00200E0C
		protected void CreateOutermostStatics(IMemberHierarchy dataRegionOrRowMemberInstance, int columnMemberSequenceId)
		{
			this.SetupEnvironment();
			base.SetupRunningValues(base.MemberDef.RunningValues, this.m_runningValueValues);
			if (base.MemberDef.DataScopeInfo != null)
			{
				base.SetupRunningValues(base.MemberDef.DataScopeInfo.RunningValuesOfAggregates, this.m_runningValueOfAggregateValues);
			}
			using (this.m_hierarchyRoot.PinValue())
			{
				((RuntimeDataTablixGroupRootObj)this.m_hierarchyRoot.Value()).SetupCellRunningValues(this.m_cellRunningValueValues);
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef = this.DataRegionDef;
			long num = ((this.m_firstRow != null) ? this.m_firstRow.StreamOffset : 0L);
			dataRegionDef.AddCell();
			List<int> outermostColumnIndexes = this.OutermostColumnIndexes;
			foreach (int num2 in this.OutermostRowIndexes)
			{
				foreach (int num3 in outermostColumnIndexes)
				{
					Cell cell = dataRegionDef.Rows[num2].Cells[num3];
					if (cell != null && !cell.SimpleGroupTreeCell)
					{
						DataCellInstance dataCellInstance = DataCellInstance.CreateInstance(dataRegionOrRowMemberInstance, this.m_odpContext, cell, num, columnMemberSequenceId);
						this.CreateOutermostStaticCellContents(cell, dataCellInstance);
						dataCellInstance.InstanceComplete();
					}
				}
			}
		}

		// Token: 0x06007CC3 RID: 31939 RVA: 0x00202D94 File Offset: 0x00200F94
		protected virtual void CreateOutermostStaticCellContents(Cell cell, DataCellInstance cellInstance)
		{
		}

		// Token: 0x06007CC4 RID: 31940 RVA: 0x00202D96 File Offset: 0x00200F96
		internal bool GetCellTargetForNonDetailSort()
		{
			return ((RuntimeDataTablixGroupRootObj)this.m_hierarchyRoot.Value()).GetCellTargetForNonDetailSort();
		}

		// Token: 0x06007CC5 RID: 31941 RVA: 0x00202DAD File Offset: 0x00200FAD
		internal bool GetCellTargetForSort(int index, bool detailSort)
		{
			return ((RuntimeDataTablixGroupRootObj)this.m_hierarchyRoot.Value()).GetCellTargetForSort(index, detailSort);
		}

		// Token: 0x06007CC6 RID: 31942 RVA: 0x00202DC6 File Offset: 0x00200FC6
		internal bool NeedHandleCellSortFilterEvent()
		{
			return base.GroupingDef.SortFilterScopeMatched != null || base.GroupingDef.NeedScopeInfoForSortFilterExpression != null;
		}

		// Token: 0x06007CC7 RID: 31943 RVA: 0x00202DE8 File Offset: 0x00200FE8
		internal IReference<RuntimeDataTablixObj> GetOwnerDataTablix()
		{
			IReference<IScope> reference = this.OuterScope;
			while (!(reference is RuntimeDataTablixObjReference))
			{
				reference = reference.Value().GetOuterScope(false);
			}
			Global.Tracer.Assert(reference is RuntimeDataTablixObjReference, "(outerScopeRef is RuntimeDataTablixObjReference)");
			return (RuntimeDataTablixObjReference)reference;
		}

		// Token: 0x06007CC8 RID: 31944 RVA: 0x00202E34 File Offset: 0x00201034
		internal bool ReadStreamingModeIdcRowFromBufferOrDataSet(FieldsContext fieldsContext)
		{
			if (this.m_bufferIndex == -2)
			{
				return false;
			}
			this.m_bufferIndex++;
			if (this.m_bufferIndex < this.m_dataRows.Count)
			{
				this.m_dataRows[this.m_bufferIndex].RestoreDataSetAndSetFields(this.m_odpContext, fieldsContext);
				return true;
			}
			this.m_odpContext.StateManager.ProcessOneRow(base.GroupingDef.Owner);
			if (this.m_bufferIndex >= this.m_dataRows.Count)
			{
				this.m_bufferIndex = -2;
				return false;
			}
			this.m_dataRows[this.m_bufferIndex].SaveAggregateInfo(this.m_odpContext);
			return true;
		}

		// Token: 0x06007CC9 RID: 31945 RVA: 0x00202EE3 File Offset: 0x002010E3
		internal void PushBackStreamingModeIdcRowToBuffer()
		{
			if (this.m_bufferIndex != -2)
			{
				this.m_bufferIndex--;
			}
		}

		// Token: 0x06007CCA RID: 31946 RVA: 0x00202EFD File Offset: 0x002010FD
		internal void ResetStreamingModeIdcRowBuffer()
		{
			this.m_bufferIndex = -1;
		}

		// Token: 0x06007CCB RID: 31947 RVA: 0x00202F06 File Offset: 0x00201106
		public IOnDemandMemberInstanceReference GetFirstMemberInstance(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode rifMember)
		{
			return RuntimeDataRegionObj.GetFirstMemberInstance(rifMember, this.m_memberObjs);
		}

		// Token: 0x06007CCC RID: 31948 RVA: 0x00202F14 File Offset: 0x00201114
		public IOnDemandMemberInstanceReference GetNextMemberInstance()
		{
			IOnDemandMemberInstanceReference onDemandMemberInstanceReference = null;
			if (this.m_nextLeaf != null)
			{
				onDemandMemberInstanceReference = (IOnDemandMemberInstanceReference)this.m_nextLeaf;
			}
			return onDemandMemberInstanceReference;
		}

		// Token: 0x06007CCD RID: 31949 RVA: 0x00202F40 File Offset: 0x00201140
		public IOnDemandScopeInstance GetCellInstance(IOnDemandMemberInstanceReference outerGroupInstanceRef, out IReference<IOnDemandScopeInstance> cellScopeRef)
		{
			RuntimeCell runtimeCell = null;
			cellScopeRef = null;
			RuntimeDataTablixGroupLeafObj runtimeDataTablixGroupLeafObj = ((RuntimeDataTablixGroupLeafObjReference)outerGroupInstanceRef).Value();
			int hierarchyDynamicIndex = runtimeDataTablixGroupLeafObj.MemberDef.HierarchyDynamicIndex;
			RuntimeCells runtimeCells = this.m_cellsList[hierarchyDynamicIndex];
			if (runtimeCells != null)
			{
				RuntimeCellReference runtimeCellReference;
				runtimeCell = runtimeCells.GetCell(runtimeDataTablixGroupLeafObj.GroupLeafIndex, out runtimeCellReference);
				cellScopeRef = runtimeCellReference;
			}
			return runtimeCell;
		}

		// Token: 0x06007CCE RID: 31950 RVA: 0x00202F8A File Offset: 0x0020118A
		public IOnDemandMemberOwnerInstanceReference GetDataRegionInstance(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion rifDataRegion)
		{
			return this.GetNestedDataRegion(rifDataRegion);
		}

		// Token: 0x06007CCF RID: 31951
		internal abstract RuntimeDataTablixObjReference GetNestedDataRegion(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion rifDataRegion);

		// Token: 0x06007CD0 RID: 31952 RVA: 0x00202F94 File Offset: 0x00201194
		public IReference<IDataCorrelation> GetIdcReceiver(IRIFReportDataScope scope)
		{
			if (scope.IsGroup)
			{
				return RuntimeDataRegionObj.GetGroupRoot(scope as Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode, this.m_memberObjs);
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = scope as Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion;
			return this.GetNestedDataRegion(dataRegion);
		}

		// Token: 0x170028BA RID: 10426
		// (get) Token: 0x06007CD1 RID: 31953 RVA: 0x00202FC9 File Offset: 0x002011C9
		public bool IsNoRows
		{
			get
			{
				return this.m_firstRow == null;
			}
		}

		// Token: 0x170028BB RID: 10427
		// (get) Token: 0x06007CD2 RID: 31954 RVA: 0x00202FD4 File Offset: 0x002011D4
		public bool IsMostRecentlyCreatedScopeInstance
		{
			get
			{
				return this.m_hierarchyDef.DataScopeInfo.IsLastScopeInstanceNumber(this.m_scopeInstanceNumber);
			}
		}

		// Token: 0x170028BC RID: 10428
		// (get) Token: 0x06007CD3 RID: 31955 RVA: 0x00202FEC File Offset: 0x002011EC
		public bool HasUnProcessedServerAggregate
		{
			get
			{
				return this.m_customAggregates != null && this.m_customAggregates.Count > 0 && !this.m_hasProcessedAggregateRow;
			}
		}

		// Token: 0x06007CD4 RID: 31956 RVA: 0x00203010 File Offset: 0x00201210
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeDataTablixGroupLeafObj.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.CellRunningValueValues)
				{
					if (memberName == MemberName.CellPostSortAggregates)
					{
						writer.Write<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_cellPostSortAggregates);
						continue;
					}
					if (memberName == MemberName.RunningValueValues)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] array = this.m_runningValueValues;
						writer.Write(array);
						continue;
					}
					switch (memberName)
					{
					case MemberName.MemberObjs:
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] array = this.m_memberObjs;
						writer.Write(array);
						continue;
					}
					case MemberName.HasInnerHierarchy:
						writer.Write(this.m_hasInnerHierarchy);
						continue;
					case MemberName.FirstPassCellNonCustomAggs:
						writer.Write<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_firstPassCellNonCustomAggs);
						continue;
					case MemberName.FirstPassCellCustomAggs:
						writer.Write<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_firstPassCellCustomAggs);
						continue;
					case MemberName.CellsList:
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] array = this.m_cellsList;
						writer.Write(array);
						continue;
					}
					case MemberName.GroupLeafIndex:
						writer.Write(this.m_groupLeafIndex);
						continue;
					case MemberName.ProcessHeading:
						writer.Write(this.m_processHeading);
						continue;
					case MemberName.SequentialMemberIndexWithinScopeLevel:
						writer.Write(this.m_sequentialMemberIndexWithinScopeLevel);
						continue;
					case MemberName.CellRunningValueValues:
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] array = this.m_cellRunningValueValues;
						writer.Write(array);
						continue;
					}
					}
				}
				else
				{
					if (memberName == MemberName.InstanceIndex)
					{
						writer.Write(this.m_instanceIndex);
						continue;
					}
					if (memberName == MemberName.RunningValueOfAggregateValues)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] array = this.m_runningValueOfAggregateValues;
						writer.Write(array);
						continue;
					}
					if (memberName == MemberName.ScopeInstanceNumber)
					{
						writer.Write(this.m_scopeInstanceNumber);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007CD5 RID: 31957 RVA: 0x002031C8 File Offset: 0x002013C8
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeDataTablixGroupLeafObj.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.CellRunningValueValues)
				{
					if (memberName == MemberName.CellPostSortAggregates)
					{
						this.m_cellPostSortAggregates = reader.ReadListOfRIFObjects<List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>>();
						continue;
					}
					if (memberName == MemberName.RunningValueValues)
					{
						this.m_runningValueValues = reader.ReadArrayOfRIFObjects<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>();
						continue;
					}
					switch (memberName)
					{
					case MemberName.MemberObjs:
						this.m_memberObjs = reader.ReadArrayOfRIFObjects<IReference<RuntimeMemberObj>>();
						continue;
					case MemberName.HasInnerHierarchy:
						this.m_hasInnerHierarchy = reader.ReadBoolean();
						continue;
					case MemberName.FirstPassCellNonCustomAggs:
						this.m_firstPassCellNonCustomAggs = reader.ReadListOfRIFObjects<List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>>();
						continue;
					case MemberName.FirstPassCellCustomAggs:
						this.m_firstPassCellCustomAggs = reader.ReadListOfRIFObjects<List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>>();
						continue;
					case MemberName.CellsList:
						this.m_cellsList = reader.ReadArrayOfRIFObjects<RuntimeCells>();
						continue;
					case MemberName.GroupLeafIndex:
						this.m_groupLeafIndex = reader.ReadInt32();
						continue;
					case MemberName.ProcessHeading:
						this.m_processHeading = reader.ReadBoolean();
						continue;
					case MemberName.SequentialMemberIndexWithinScopeLevel:
						this.m_sequentialMemberIndexWithinScopeLevel = reader.ReadInt32();
						continue;
					case MemberName.CellRunningValueValues:
						this.m_cellRunningValueValues = reader.ReadArrayOfRIFObjects<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.InstanceIndex)
					{
						this.m_instanceIndex = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.RunningValueOfAggregateValues)
					{
						this.m_runningValueOfAggregateValues = reader.ReadArrayOfRIFObjects<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>();
						continue;
					}
					if (memberName == MemberName.ScopeInstanceNumber)
					{
						this.m_scopeInstanceNumber = reader.ReadInt64();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007CD6 RID: 31958 RVA: 0x00203374 File Offset: 0x00201574
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007CD7 RID: 31959 RVA: 0x0020337E File Offset: 0x0020157E
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupLeafObj;
		}

		// Token: 0x06007CD8 RID: 31960 RVA: 0x00203388 File Offset: 0x00201588
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeDataTablixGroupLeafObj.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupLeafObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupLeafObj, new List<MemberInfo>
				{
					new MemberInfo(MemberName.MemberObjs, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMemberObjReference),
					new MemberInfo(MemberName.HasInnerHierarchy, Token.Boolean),
					new MemberInfo(MemberName.FirstPassCellNonCustomAggs, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj),
					new MemberInfo(MemberName.FirstPassCellCustomAggs, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj),
					new MemberInfo(MemberName.CellsList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeCells),
					new MemberInfo(MemberName.CellPostSortAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj),
					new MemberInfo(MemberName.GroupLeafIndex, Token.Int32),
					new MemberInfo(MemberName.ProcessHeading, Token.Boolean),
					new MemberInfo(MemberName.SequentialMemberIndexWithinScopeLevel, Token.Int32),
					new MemberInfo(MemberName.RunningValueValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjResult),
					new MemberInfo(MemberName.CellRunningValueValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjResult),
					new MemberInfo(MemberName.InstanceIndex, Token.Int32),
					new MemberInfo(MemberName.RunningValueOfAggregateValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjResult),
					new MemberInfo(MemberName.ScopeInstanceNumber, Token.Int64)
				});
			}
			return RuntimeDataTablixGroupLeafObj.m_declaration;
		}

		// Token: 0x170028BD RID: 10429
		// (get) Token: 0x06007CD9 RID: 31961 RVA: 0x002034D8 File Offset: 0x002016D8
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.SizeOf<IReference<RuntimeMemberObj>>(this.m_memberObjs) + 1 + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_firstPassCellNonCustomAggs) + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_firstPassCellCustomAggs) + ItemSizes.SizeOf<RuntimeCells>(this.m_cellsList) + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_cellPostSortAggregates) + 4 + 1 + ItemSizes.ReferenceSize + 4 + 4 + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>(this.m_runningValueValues) + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>(this.m_runningValueOfAggregateValues) + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>(this.m_cellRunningValueValues) + 8;
			}
		}

		// Token: 0x04003DC2 RID: 15810
		protected IReference<RuntimeMemberObj>[] m_memberObjs;

		// Token: 0x04003DC3 RID: 15811
		protected bool m_hasInnerHierarchy;

		// Token: 0x04003DC4 RID: 15812
		protected List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> m_firstPassCellNonCustomAggs;

		// Token: 0x04003DC5 RID: 15813
		protected List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> m_firstPassCellCustomAggs;

		// Token: 0x04003DC6 RID: 15814
		protected RuntimeCells[] m_cellsList;

		// Token: 0x04003DC7 RID: 15815
		protected List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> m_cellPostSortAggregates;

		// Token: 0x04003DC8 RID: 15816
		protected int m_groupLeafIndex = -1;

		// Token: 0x04003DC9 RID: 15817
		protected bool m_processHeading = true;

		// Token: 0x04003DCA RID: 15818
		[NonSerialized]
		protected DataRegionMemberInstance m_memberInstance;

		// Token: 0x04003DCB RID: 15819
		protected int m_sequentialMemberIndexWithinScopeLevel = -1;

		// Token: 0x04003DCC RID: 15820
		protected Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[] m_runningValueValues;

		// Token: 0x04003DCD RID: 15821
		protected Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[] m_runningValueOfAggregateValues;

		// Token: 0x04003DCE RID: 15822
		protected Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[] m_cellRunningValueValues;

		// Token: 0x04003DCF RID: 15823
		protected int m_instanceIndex = -1;

		// Token: 0x04003DD0 RID: 15824
		private long m_scopeInstanceNumber;

		// Token: 0x04003DD1 RID: 15825
		[NonSerialized]
		private int m_bufferIndex = -1;

		// Token: 0x04003DD2 RID: 15826
		private const int BeforeFirstRowInBuffer = -1;

		// Token: 0x04003DD3 RID: 15827
		private const int AfterLastRowInBuffer = -2;

		// Token: 0x04003DD4 RID: 15828
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeDataTablixGroupLeafObj.GetDeclaration();
	}
}

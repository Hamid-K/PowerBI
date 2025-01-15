using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008E4 RID: 2276
	[PersistedWithinRequestOnly]
	public abstract class RuntimeGroupLeafObj : RuntimeGroupObj, IDataRowHolder
	{
		// Token: 0x06007C51 RID: 31825 RVA: 0x001FEF5F File Offset: 0x001FD15F
		protected RuntimeGroupLeafObj()
		{
		}

		// Token: 0x06007C52 RID: 31826 RVA: 0x001FEF68 File Offset: 0x001FD168
		protected RuntimeGroupLeafObj(RuntimeGroupRootObjReference groupRootRef, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(groupRootRef.Value().OdpContext, objectType, ((IScope)groupRootRef.Value()).Depth + 1)
		{
			RuntimeGroupRootObj runtimeGroupRootObj = groupRootRef.Value();
			this.m_hierarchyDef = runtimeGroupRootObj.HierarchyDef;
			this.m_hierarchyRoot = groupRootRef;
			Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = this.m_hierarchyDef.Grouping;
			RuntimeDataRegionObj.CreateAggregates(this.m_odpContext, grouping.Aggregates, ref this.m_nonCustomAggregates, ref this.m_customAggregates);
			RuntimeDataRegionObj.CreateAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, grouping.RecursiveAggregates, ref this.m_recursiveAggregates);
			RuntimeDataRegionObj.CreateAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, grouping.PostSortAggregates, ref this.m_postSortAggregates);
			if (this.m_hierarchyDef.DataScopeInfo != null)
			{
				DataScopeInfo dataScopeInfo = this.m_hierarchyDef.DataScopeInfo;
				RuntimeDataRegionObj.CreateAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, dataScopeInfo.AggregatesOfAggregates, ref this.m_aggregatesOfAggregates);
				RuntimeDataRegionObj.CreateAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, dataScopeInfo.PostSortAggregatesOfAggregates, ref this.m_postSortAggregatesOfAggregates);
			}
			if (runtimeGroupRootObj.SaveGroupExprValues)
			{
				this.m_groupExprValues = grouping.CurrentGroupExpressionValues;
			}
			RuntimeDataTablixGroupRootObj runtimeDataTablixGroupRootObj = runtimeGroupRootObj as RuntimeDataTablixGroupRootObj;
			this.m_isOuterGrouping = runtimeDataTablixGroupRootObj != null && runtimeDataTablixGroupRootObj.InnerGroupings != null;
		}

		// Token: 0x1700289C RID: 10396
		// (get) Token: 0x06007C53 RID: 31827 RVA: 0x001FF07C File Offset: 0x001FD27C
		internal bool IsOuterGrouping
		{
			get
			{
				return this.m_isOuterGrouping;
			}
		}

		// Token: 0x1700289D RID: 10397
		// (set) Token: 0x06007C54 RID: 31828 RVA: 0x001FF084 File Offset: 0x001FD284
		internal RuntimeGroupLeafObjReference NextLeaf
		{
			set
			{
				this.m_nextLeaf = value;
			}
		}

		// Token: 0x1700289E RID: 10398
		// (set) Token: 0x06007C55 RID: 31829 RVA: 0x001FF08D File Offset: 0x001FD28D
		internal RuntimeGroupLeafObjReference PrevLeaf
		{
			set
			{
				this.m_prevLeaf = value;
			}
		}

		// Token: 0x1700289F RID: 10399
		// (get) Token: 0x06007C56 RID: 31830 RVA: 0x001FF096 File Offset: 0x001FD296
		// (set) Token: 0x06007C57 RID: 31831 RVA: 0x001FF09E File Offset: 0x001FD29E
		internal IReference<RuntimeGroupObj> Parent
		{
			get
			{
				return this.m_parent;
			}
			set
			{
				this.m_parent = value;
			}
		}

		// Token: 0x170028A0 RID: 10400
		// (get) Token: 0x06007C58 RID: 31832 RVA: 0x001FF0A7 File Offset: 0x001FD2A7
		protected override IReference<IScope> OuterScope
		{
			get
			{
				return this.m_hierarchyRoot;
			}
		}

		// Token: 0x170028A1 RID: 10401
		// (get) Token: 0x06007C59 RID: 31833 RVA: 0x001FF0AF File Offset: 0x001FD2AF
		internal override int RecursiveLevel
		{
			get
			{
				return this.m_recursiveLevel;
			}
		}

		// Token: 0x170028A2 RID: 10402
		// (get) Token: 0x06007C5A RID: 31834 RVA: 0x001FF0B7 File Offset: 0x001FD2B7
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode MemberDef
		{
			get
			{
				return this.m_hierarchyDef;
			}
		}

		// Token: 0x170028A3 RID: 10403
		// (get) Token: 0x06007C5B RID: 31835 RVA: 0x001FF0BF File Offset: 0x001FD2BF
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Grouping GroupingDef
		{
			get
			{
				return this.m_hierarchyDef.Grouping;
			}
		}

		// Token: 0x170028A4 RID: 10404
		// (get) Token: 0x06007C5C RID: 31836 RVA: 0x001FF0CC File Offset: 0x001FD2CC
		protected override string ScopeName
		{
			get
			{
				return this.m_hierarchyDef.Grouping.Name;
			}
		}

		// Token: 0x170028A5 RID: 10405
		// (get) Token: 0x06007C5D RID: 31837 RVA: 0x001FF0DE File Offset: 0x001FD2DE
		protected override IReference<IHierarchyObj> HierarchyRoot
		{
			get
			{
				if (ProcessingStages.UserSortFilter == ((RuntimeGroupRootObj)this.m_hierarchyRoot.Value()).ProcessingStage)
				{
					return (RuntimeGroupLeafObjReference)this.m_selfReference;
				}
				return this.m_hierarchyRoot;
			}
		}

		// Token: 0x170028A6 RID: 10406
		// (get) Token: 0x06007C5E RID: 31838 RVA: 0x001FF10A File Offset: 0x001FD30A
		protected override BTree SortTree
		{
			get
			{
				if (ProcessingStages.UserSortFilter != ((RuntimeGroupRootObj)this.m_hierarchyRoot.Value()).ProcessingStage)
				{
					return this.m_grouping.Tree;
				}
				if (this.m_userSortTargetInfo != null)
				{
					return this.m_userSortTargetInfo.SortTree;
				}
				return null;
			}
		}

		// Token: 0x170028A7 RID: 10407
		// (get) Token: 0x06007C5F RID: 31839 RVA: 0x001FF145 File Offset: 0x001FD345
		protected override int ExpressionIndex
		{
			get
			{
				if (ProcessingStages.UserSortFilter == ((RuntimeGroupRootObj)this.m_hierarchyRoot.Value()).ProcessingStage)
				{
					return 0;
				}
				Global.Tracer.Assert(false);
				return -1;
			}
		}

		// Token: 0x170028A8 RID: 10408
		// (get) Token: 0x06007C60 RID: 31840 RVA: 0x001FF16D File Offset: 0x001FD36D
		protected override List<int> SortFilterInfoIndices
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

		// Token: 0x170028A9 RID: 10409
		// (get) Token: 0x06007C61 RID: 31841 RVA: 0x001FF184 File Offset: 0x001FD384
		protected RuntimeGroupRootObjReference GroupRoot
		{
			get
			{
				return this.m_hierarchyRoot as RuntimeGroupRootObjReference;
			}
		}

		// Token: 0x170028AA RID: 10410
		// (get) Token: 0x06007C62 RID: 31842 RVA: 0x001FF191 File Offset: 0x001FD391
		internal DataFieldRow FirstRow
		{
			get
			{
				return this.m_firstRow;
			}
		}

		// Token: 0x170028AB RID: 10411
		// (get) Token: 0x06007C63 RID: 31843 RVA: 0x001FF199 File Offset: 0x001FD399
		internal override bool TargetForNonDetailSort
		{
			get
			{
				return (this.m_userSortTargetInfo != null && this.m_userSortTargetInfo.TargetForNonDetailSort) || this.m_hierarchyRoot.Value().TargetForNonDetailSort;
			}
		}

		// Token: 0x170028AC RID: 10412
		// (get) Token: 0x06007C64 RID: 31844 RVA: 0x001FF1C4 File Offset: 0x001FD3C4
		protected override int[] SortFilterExpressionScopeInfoIndices
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

		// Token: 0x170028AD RID: 10413
		// (get) Token: 0x06007C65 RID: 31845 RVA: 0x001FF21E File Offset: 0x001FD41E
		internal override IRIFReportScope RIFReportScope
		{
			get
			{
				return this.m_hierarchyDef;
			}
		}

		// Token: 0x170028AE RID: 10414
		// (get) Token: 0x06007C66 RID: 31846 RVA: 0x001FF226 File Offset: 0x001FD426
		internal int DetailSortRowCounter
		{
			get
			{
				return this.m_detailRowCounter;
			}
		}

		// Token: 0x170028AF RID: 10415
		// (get) Token: 0x06007C67 RID: 31847 RVA: 0x001FF22E File Offset: 0x001FD42E
		internal ScalableList<DataFieldRow> DataRows
		{
			get
			{
				return this.m_dataRows;
			}
		}

		// Token: 0x06007C68 RID: 31848 RVA: 0x001FF236 File Offset: 0x001FD436
		internal override bool IsTargetForSort(int index, bool detailSort)
		{
			return (this.m_userSortTargetInfo != null && this.m_userSortTargetInfo.IsTargetForSort(index, detailSort)) || this.m_hierarchyRoot.Value().IsTargetForSort(index, detailSort);
		}

		// Token: 0x06007C69 RID: 31849 RVA: 0x001FF264 File Offset: 0x001FD464
		protected virtual void ConstructRuntimeStructure(ref bool handleMyDataAction, out DataActions innerDataAction)
		{
			if (this.m_postSortAggregates != null)
			{
				handleMyDataAction = true;
			}
			if (this.m_recursiveAggregates != null && (this.m_odpContext.SpecialRecursiveAggregates || this.MemberDef.HasInnerDynamic))
			{
				handleMyDataAction = true;
			}
			if (handleMyDataAction)
			{
				innerDataAction = DataActions.None;
				return;
			}
			innerDataAction = ((RuntimeGroupRootObj)this.m_hierarchyRoot.Value()).DataAction;
		}

		// Token: 0x06007C6A RID: 31850 RVA: 0x001FF2C0 File Offset: 0x001FD4C0
		protected bool HandleSortFilterEvent(bool isColumnAxis)
		{
			if (this.m_odpContext.RuntimeSortFilterInfo == null)
			{
				return false;
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.Grouping groupingDef = this.GroupingDef;
			if (groupingDef.IsDetail)
			{
				return false;
			}
			int count = this.m_odpContext.RuntimeSortFilterInfo.Count;
			if (groupingDef.SortFilterScopeMatched != null || groupingDef.NeedScopeInfoForSortFilterExpression != null)
			{
				this.m_targetScopeMatched = new bool[count];
				for (int i = 0; i < count; i++)
				{
					IReference<RuntimeSortFilterEventInfo> reference = this.m_odpContext.RuntimeSortFilterInfo[i];
					using (reference.PinValue())
					{
						RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = reference.Value();
						if (groupingDef.SortFilterScopeMatched != null && groupingDef.SortFilterScopeIndex != null && -1 != groupingDef.SortFilterScopeIndex[i])
						{
							this.m_targetScopeMatched[i] = groupingDef.SortFilterScopeMatched[i];
							if (this.m_targetScopeMatched[i])
							{
								if (groupingDef.IsSortFilterTarget != null && groupingDef.IsSortFilterTarget[i] && !this.m_hierarchyRoot.Value().TargetForNonDetailSort)
								{
									runtimeSortFilterEventInfo.EventTarget = (IReference<IHierarchyObj>)base.SelfReference;
									if (this.m_userSortTargetInfo == null)
									{
										this.m_userSortTargetInfo = new RuntimeUserSortTargetInfo((IReference<IHierarchyObj>)base.SelfReference, i, reference);
									}
									else
									{
										this.m_userSortTargetInfo.AddSortInfo((IReference<IHierarchyObj>)base.SelfReference, i, reference);
									}
								}
								Global.Tracer.Assert(runtimeSortFilterEventInfo.EventSource.ContainingScopes != null, "(null != sortFilterInfo.EventSource.ContainingScopes)");
								if (groupingDef == runtimeSortFilterEventInfo.EventSource.ContainingScopes.LastEntry && !runtimeSortFilterEventInfo.EventSource.IsTablixCellScope && !this.m_hierarchyRoot.Value().TargetForNonDetailSort)
								{
									runtimeSortFilterEventInfo.SetEventSourceScope(isColumnAxis, base.SelfReference, -1);
								}
							}
						}
						else
						{
							this.m_targetScopeMatched[i] = ((RuntimeGroupRootObj)this.m_hierarchyRoot.Value()).TargetScopeMatched(i, false);
						}
					}
				}
			}
			this.m_odpContext.RegisterSortFilterExpressionScope(this.m_hierarchyRoot, base.SelfReference, groupingDef.IsSortFilterExpressionScope);
			return this.m_userSortTargetInfo != null && this.m_userSortTargetInfo.TargetForNonDetailSort;
		}

		// Token: 0x06007C6B RID: 31851 RVA: 0x001FF4E4 File Offset: 0x001FD6E4
		internal override void GetScopeValues(IReference<IHierarchyObj> targetScopeObj, List<object>[] scopeValues, ref int index)
		{
			if (this.GroupingDef.IsDetail)
			{
				base.DetailGetScopeValues(this.GroupRoot.Value().GroupRootOuterScope, targetScopeObj, scopeValues, ref index);
				return;
			}
			if (targetScopeObj == null || this != targetScopeObj.Value())
			{
				using (this.m_hierarchyRoot.PinValue())
				{
					this.m_hierarchyRoot.Value().GetScopeValues(targetScopeObj, scopeValues, ref index);
				}
				Global.Tracer.Assert(this.m_groupExprValues != null, "(null != m_groupExprValues)");
				Global.Tracer.Assert(index < scopeValues.Length, "(index < scopeValues.Length)");
				int num = index;
				index = num + 1;
				scopeValues[num] = this.m_groupExprValues;
			}
		}

		// Token: 0x06007C6C RID: 31852 RVA: 0x001FF5A0 File Offset: 0x001FD7A0
		internal override bool TargetScopeMatched(int index, bool detailSort)
		{
			if (this.GroupingDef.IsDetail)
			{
				RuntimeGroupRootObj runtimeGroupRootObj = this.GroupRoot.Value();
				return base.DetailTargetScopeMatched(this.MemberDef.DataRegionDef, runtimeGroupRootObj.GroupRootOuterScope, runtimeGroupRootObj.HierarchyDef.IsColumn, index);
			}
			return (detailSort && this.GroupingDef.SortFilterScopeInfo == null) || (this.m_targetScopeMatched != null && this.m_targetScopeMatched[index]);
		}

		// Token: 0x06007C6D RID: 31853 RVA: 0x001FF610 File Offset: 0x001FD810
		protected void UpdateAggregateInfo()
		{
			FieldsImpl fieldsImpl = this.m_odpContext.ReportObjectModel.FieldsImpl;
			if (fieldsImpl.ValidAggregateRow)
			{
				int[] groupExpressionFieldIndices = this.GroupingDef.GetGroupExpressionFieldIndices();
				if (groupExpressionFieldIndices != null)
				{
					foreach (int num in groupExpressionFieldIndices)
					{
						if (num >= 0)
						{
							fieldsImpl.ConsumeAggregationField(num);
						}
					}
				}
				if (fieldsImpl.AggregationFieldCount == 0 && this.m_customAggregates != null && (this.IsOuterGrouping || !this.m_odpContext.PeerOuterGroupProcessing))
				{
					this.m_hasProcessedAggregateRow = true;
					RuntimeDataRegionObj.UpdateAggregates(this.m_odpContext, this.m_customAggregates, false);
				}
			}
		}

		// Token: 0x06007C6E RID: 31854 RVA: 0x001FF6A0 File Offset: 0x001FD8A0
		protected void InternalNextRow()
		{
			using (this.m_hierarchyRoot.PinValue())
			{
				RuntimeGroupRootObj runtimeGroupRootObj = this.m_hierarchyRoot.Value() as RuntimeGroupRootObj;
				ProcessingStages processingStage = runtimeGroupRootObj.ProcessingStage;
				runtimeGroupRootObj.ProcessingStage = ProcessingStages.UserSortFilter;
				if (runtimeGroupRootObj.IsDetailGroup)
				{
					base.DetailHandleSortFilterEvent(this.MemberDef.DataRegionDef, runtimeGroupRootObj.GroupRootOuterScope, runtimeGroupRootObj.HierarchyDef.IsColumn, this.m_odpContext.ReportObjectModel.FieldsImpl.GetRowIndex());
				}
				RuntimeDataRegionObj.CommonFirstRow(this.m_odpContext, ref this.m_firstRowIsAggregate, ref this.m_firstRow);
				if (this.IsOuterGrouping || !this.m_odpContext.PeerOuterGroupProcessing)
				{
					if (this.m_odpContext.ReportObjectModel.FieldsImpl.IsAggregateRow)
					{
						base.ScopeNextAggregateRow(this.m_userSortTargetInfo);
						if (this.MemberDef.DataRegionDef.IsMatrixIDC)
						{
							AggregateRow aggregateRow = new AggregateRow(this.m_odpContext.ReportObjectModel.FieldsImpl, true);
							aggregateRow.SaveAggregateInfo(this.m_odpContext);
							this.m_dataRows.Add(aggregateRow);
						}
					}
					else
					{
						base.ScopeNextNonAggregateRow(this.m_nonCustomAggregates, this.m_dataRows);
					}
				}
				else
				{
					this.SendToInner();
				}
				runtimeGroupRootObj.ProcessingStage = processingStage;
			}
		}

		// Token: 0x06007C6F RID: 31855 RVA: 0x001FF7F4 File Offset: 0x001FD9F4
		protected override void SendToInner()
		{
			using (this.m_hierarchyRoot.PinValue())
			{
				((RuntimeGroupRootObj)this.m_hierarchyRoot.Value()).ProcessingStage = ProcessingStages.Grouping;
			}
		}

		// Token: 0x06007C70 RID: 31856 RVA: 0x001FF840 File Offset: 0x001FDA40
		internal void RemoveFromParent(RuntimeGroupObjReference parentRef)
		{
			using (parentRef.PinValue())
			{
				RuntimeGroupObj runtimeGroupObj = parentRef.Value();
				if (null == this.m_prevLeaf)
				{
					runtimeGroupObj.FirstChild = this.m_nextLeaf;
				}
				else
				{
					using (this.m_prevLeaf.PinValue())
					{
						this.m_prevLeaf.Value().m_nextLeaf = this.m_nextLeaf;
					}
				}
				if (null == this.m_nextLeaf)
				{
					runtimeGroupObj.LastChild = this.m_prevLeaf;
				}
				else
				{
					using (this.m_nextLeaf.PinValue())
					{
						this.m_nextLeaf.Value().m_prevLeaf = this.m_prevLeaf;
					}
				}
			}
		}

		// Token: 0x06007C71 RID: 31857 RVA: 0x001FF924 File Offset: 0x001FDB24
		private IReference<RuntimeGroupLeafObj> Traverse(ProcessingStages operation, ITraversalContext traversalContext)
		{
			IReference<RuntimeGroupLeafObj> nextLeaf = this.m_nextLeaf;
			if (((RuntimeGroupRootObj)this.m_hierarchyRoot.Value()).HasParent)
			{
				this.m_recursiveLevel = this.m_parent.Value().RecursiveLevel + 1;
			}
			bool flag = this.IsSpecialFilteringPass(operation);
			if (flag)
			{
				this.m_lastChild = null;
				this.ProcessChildren(operation, traversalContext);
			}
			switch (operation)
			{
			case ProcessingStages.SortAndFilter:
				this.SortAndFilter((AggregateUpdateContext)traversalContext);
				break;
			case ProcessingStages.PreparePeerGroupRunningValues:
				this.PrepareCalculateRunningValues();
				break;
			case ProcessingStages.RunningValues:
			{
				bool flag2 = false;
				if (this.m_odpContext.HasPreviousAggregates)
				{
					flag2 = ((RuntimeGroupRootObj)this.m_hierarchyRoot.Value()).IsDetailGroup;
					if (this.m_groupExprValues != null && !flag2)
					{
						this.m_odpContext.GroupExpressionValues.AddRange(this.m_groupExprValues);
					}
				}
				this.CalculateRunningValues((AggregateUpdateContext)traversalContext);
				if (this.m_odpContext.HasPreviousAggregates && this.m_groupExprValues != null && !flag2)
				{
					this.m_odpContext.GroupExpressionValues.RemoveRange(this.m_odpContext.GroupExpressionValues.Count - this.m_groupExprValues.Count, this.m_groupExprValues.Count);
				}
				break;
			}
			case ProcessingStages.UpdateAggregates:
				this.UpdateAggregates((AggregateUpdateContext)traversalContext);
				break;
			case ProcessingStages.CreateGroupTree:
				this.CreateInstance((CreateInstancesTraversalContext)traversalContext);
				break;
			}
			if (!flag)
			{
				this.ProcessChildren(operation, traversalContext);
			}
			return nextLeaf;
		}

		// Token: 0x06007C72 RID: 31858 RVA: 0x001FFA8C File Offset: 0x001FDC8C
		internal void TraverseAllLeafNodes(ProcessingStages operation, ITraversalContext traversalContext)
		{
			IReference<RuntimeGroupLeafObj> reference = base.SelfReference as IReference<RuntimeGroupLeafObj>;
			while (reference != null)
			{
				this.TablixProcessingMoveNext(operation);
				using (reference.PinValue())
				{
					reference = reference.Value().Traverse(operation, traversalContext);
				}
			}
		}

		// Token: 0x06007C73 RID: 31859 RVA: 0x001FFAE4 File Offset: 0x001FDCE4
		protected void TablixProcessingMoveNext(ProcessingStages operation)
		{
			if (operation == ProcessingStages.CreateGroupTree && (this.m_odpContext.ReportDefinition.ReportOrDescendentHasUserSortFilter || this.m_odpContext.ReportDefinition.HasSubReports))
			{
				this.MemberDef.MoveNextForUserSort(this.m_odpContext);
			}
		}

		// Token: 0x06007C74 RID: 31860 RVA: 0x001FFB20 File Offset: 0x001FDD20
		private void ProcessChildren(ProcessingStages operation, ITraversalContext traversalContext)
		{
			if (null != this.m_firstChild || this.m_grouping != null)
			{
				using (this.m_hierarchyRoot.PinValue())
				{
					RuntimeGroupRootObj runtimeGroupRootObj = (RuntimeGroupRootObj)this.m_hierarchyRoot.Value();
					if (null != this.m_firstChild)
					{
						using (this.m_firstChild.PinValue())
						{
							this.m_firstChild.Value().TraverseAllLeafNodes(operation, traversalContext);
						}
						if (operation == ProcessingStages.SortAndFilter)
						{
							if ((SecondPassOperations.FilteringOrAggregatesOrDomainScope & this.m_odpContext.SecondPassOperation) != SecondPassOperations.None && runtimeGroupRootObj.HierarchyDef.Grouping.Filters != null)
							{
								if (null == this.m_lastChild)
								{
									this.m_firstChild = null;
								}
							}
							else if (this.m_grouping != null)
							{
								this.m_firstChild = null;
							}
						}
					}
					else if (this.m_grouping != null)
					{
						this.m_grouping.Traverse(operation, runtimeGroupRootObj.Expression.Direction, traversalContext);
					}
				}
			}
		}

		// Token: 0x06007C75 RID: 31861 RVA: 0x001FFC30 File Offset: 0x001FDE30
		private bool IsSpecialFilteringPass(ProcessingStages operation)
		{
			return ProcessingStages.SortAndFilter == operation && this.m_odpContext.SpecialRecursiveAggregates && (SecondPassOperations.FilteringOrAggregatesOrDomainScope & this.m_odpContext.SecondPassOperation) != SecondPassOperations.None;
		}

		// Token: 0x06007C76 RID: 31862 RVA: 0x001FFC58 File Offset: 0x001FDE58
		internal override bool SortAndFilter(AggregateUpdateContext aggContext)
		{
			bool flag = true;
			bool flag2 = false;
			using (this.m_hierarchyRoot.PinValue())
			{
				RuntimeGroupRootObj runtimeGroupRootObj = (RuntimeGroupRootObj)this.m_hierarchyRoot.Value();
				Global.Tracer.Assert(runtimeGroupRootObj != null, "(null != groupRoot)");
				if (this.MemberDef.Grouping.Variables != null)
				{
					ScopeInstance.ResetVariables(this.m_odpContext, this.MemberDef.Grouping.Variables);
					ScopeInstance.CalculateVariables(this.m_odpContext, this.MemberDef.Grouping.Variables, out this.m_variableValues);
				}
				if (runtimeGroupRootObj.ProcessSecondPassSorting && null != this.m_firstChild)
				{
					this.m_expression = runtimeGroupRootObj.Expression;
					this.m_grouping = new RuntimeGroupingObjTree(this, this.m_objectType);
				}
				this.m_lastChild = null;
				if (this.m_odpContext.HasSecondPassOperation(SecondPassOperations.FilteringOrAggregatesOrDomainScope))
				{
					if (this.m_odpContext.SpecialRecursiveAggregates && this.m_recursiveAggregates != null)
					{
						Global.Tracer.Assert(this.m_dataRows != null, "(null != m_dataRows)");
						this.ReadRows(false);
					}
					if (runtimeGroupRootObj.GroupFilters != null)
					{
						this.SetupEnvironment();
						flag = runtimeGroupRootObj.GroupFilters.PassFilters(base.SelfReference, out flag2);
					}
				}
				if (flag)
				{
					this.PostFilterNextRow(aggContext);
				}
				else if (!flag2)
				{
					this.FailFilter();
				}
			}
			return flag;
		}

		// Token: 0x06007C77 RID: 31863 RVA: 0x001FFDC4 File Offset: 0x001FDFC4
		internal void FailFilter()
		{
			RuntimeGroupLeafObjReference runtimeGroupLeafObjReference = null;
			bool flag = false;
			if (this.IsSpecialFilteringPass(ProcessingStages.SortAndFilter))
			{
				flag = true;
			}
			if (this.m_firstChild != null)
			{
				using (this.m_parent.PinValue())
				{
					RuntimeGroupObj runtimeGroupObj = this.m_parent.Value();
					RuntimeGroupLeafObjReference runtimeGroupLeafObjReference2 = this.m_firstChild;
					while (runtimeGroupLeafObjReference2 != null)
					{
						using (runtimeGroupLeafObjReference2.PinValue())
						{
							RuntimeGroupLeafObj runtimeGroupLeafObj = runtimeGroupLeafObjReference2.Value();
							runtimeGroupLeafObjReference = runtimeGroupLeafObj.m_nextLeaf;
							runtimeGroupLeafObj.m_parent = this.m_parent;
							if (flag)
							{
								runtimeGroupObj.AddChild(runtimeGroupLeafObjReference2);
							}
						}
						runtimeGroupLeafObjReference2 = runtimeGroupLeafObjReference;
					}
				}
			}
		}

		// Token: 0x06007C78 RID: 31864 RVA: 0x001FFE80 File Offset: 0x001FE080
		internal virtual void PostFilterNextRow(AggregateUpdateContext context)
		{
			using (this.m_hierarchyRoot.PinValue())
			{
				RuntimeGroupRootObj runtimeGroupRootObj = (RuntimeGroupRootObj)this.m_hierarchyRoot.Value();
				if ((SecondPassOperations.FilteringOrAggregatesOrDomainScope & this.m_odpContext.SecondPassOperation) != SecondPassOperations.None && this.m_dataRows != null && (this.m_dataAction & DataActions.RecursiveAggregates) != DataActions.None)
				{
					if (this.m_odpContext.SpecialRecursiveAggregates)
					{
						this.ReadRows(true);
					}
					else
					{
						this.ReadRows(DataActions.RecursiveAggregates, null);
					}
					base.ReleaseDataRows(DataActions.RecursiveAggregates, ref this.m_dataAction, ref this.m_dataRows);
				}
				bool processSecondPassSorting = runtimeGroupRootObj.ProcessSecondPassSorting;
				if (processSecondPassSorting)
				{
					this.SetupEnvironment();
				}
				if (processSecondPassSorting || runtimeGroupRootObj.GroupFilters != null)
				{
					this.m_nextLeaf = null;
					using (this.m_parent.PinValue())
					{
						this.m_parent.Value().InsertToSortTree((RuntimeGroupLeafObjReference)this.m_selfReference);
					}
				}
			}
		}

		// Token: 0x06007C79 RID: 31865
		protected abstract void PrepareCalculateRunningValues();

		// Token: 0x06007C7A RID: 31866 RVA: 0x001FFF78 File Offset: 0x001FE178
		internal override void CalculateRunningValues(AggregateUpdateContext aggContext)
		{
			this.ResetScopedRunningValues();
		}

		// Token: 0x06007C7B RID: 31867 RVA: 0x001FFF80 File Offset: 0x001FE180
		public override void ReadRow(DataActions dataAction, ITraversalContext context)
		{
			Global.Tracer.Assert(DataActions.UserSort != dataAction, "(DataActions.UserSort != dataAction)");
			if (FlagUtils.HasFlag(dataAction, DataActions.PostSortAggregatesOfAggregates))
			{
				((AggregateUpdateContext)context).UpdateAggregatesForRow();
			}
			if (FlagUtils.HasFlag(dataAction, DataActions.PostSortAggregates))
			{
				if (this.m_postSortAggregates != null)
				{
					RuntimeDataRegionObj.UpdateAggregates(this.m_odpContext, this.m_postSortAggregates, false);
				}
				Global.Tracer.Assert(null != this.m_hierarchyRoot, "(null != m_hierarchyRoot)");
				using (this.m_hierarchyRoot.PinValue())
				{
					((IScope)this.m_hierarchyRoot.Value()).ReadRow(DataActions.PostSortAggregates, context);
					return;
				}
			}
			if (FlagUtils.HasFlag(dataAction, DataActions.AggregatesOfAggregates))
			{
				((AggregateUpdateContext)context).UpdateAggregatesForRow();
				return;
			}
			Global.Tracer.Assert(DataActions.RecursiveAggregates == dataAction, "(DataActions.RecursiveAggregates == dataAction)");
			if (this.m_recursiveAggregates != null)
			{
				RuntimeDataRegionObj.UpdateAggregates(this.m_odpContext, this.m_recursiveAggregates, false);
			}
			using (this.m_parent.PinValue())
			{
				((IScope)this.m_parent.Value()).ReadRow(DataActions.RecursiveAggregates, context);
			}
		}

		// Token: 0x06007C7C RID: 31868 RVA: 0x002000A8 File Offset: 0x001FE2A8
		private void ReadRow(bool sendToParent)
		{
			if (!sendToParent)
			{
				Global.Tracer.Assert(this.m_recursiveAggregates != null, "(null != m_recursiveAggregates)");
				RuntimeDataRegionObj.UpdateAggregates(this.m_odpContext, this.m_recursiveAggregates, false);
				return;
			}
			using (this.m_parent.PinValue())
			{
				((IScope)this.m_parent.Value()).ReadRow(DataActions.RecursiveAggregates, null);
			}
		}

		// Token: 0x06007C7D RID: 31869 RVA: 0x00200120 File Offset: 0x001FE320
		public override void SetupEnvironment()
		{
			if (this.m_hierarchyDef.DataScopeInfo != null && this.m_hierarchyDef.DataScopeInfo.DataSet != null)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = this.m_hierarchyDef.DataScopeInfo.DataSet;
				base.SetupNewDataSet(dataSet);
				if (this.m_hierarchyDef.DataScopeInfo.DataSet.DataSetCore.FieldsContext != null)
				{
					this.m_odpContext.ReportObjectModel.RestoreFields(this.m_hierarchyDef.DataScopeInfo.DataSet.DataSetCore.FieldsContext);
				}
			}
			base.SetupEnvironment(this.m_nonCustomAggregates, this.m_customAggregates, this.m_firstRow);
			this.MemberDef.SetUserSortDetailRowIndex(this.m_odpContext);
			base.SetupAggregates(this.m_aggregatesOfAggregates);
			base.SetupAggregates(this.m_postSortAggregatesOfAggregates);
			base.SetupAggregates(this.m_recursiveAggregates);
			base.SetupAggregates(this.m_postSortAggregates);
			RuntimeGroupRootObj runtimeGroupRootObj = (RuntimeGroupRootObj)this.m_hierarchyRoot.Value();
			if (runtimeGroupRootObj.HasParent)
			{
				this.GroupingDef.RecursiveLevel = this.m_recursiveLevel;
			}
			if (runtimeGroupRootObj.SaveGroupExprValues)
			{
				this.GroupingDef.CurrentGroupExpressionValues = this.m_groupExprValues;
			}
			this.SetupGroupVariables();
		}

		// Token: 0x06007C7E RID: 31870 RVA: 0x0020024A File Offset: 0x001FE44A
		internal void SetupGroupVariables()
		{
			if (this.m_variableValues != null)
			{
				ScopeInstance.SetupVariables(this.m_odpContext, this.GroupingDef.Variables, this.m_variableValues);
			}
		}

		// Token: 0x06007C7F RID: 31871 RVA: 0x00200270 File Offset: 0x001FE470
		internal void CalculatePreviousAggregates(bool setupEnvironment)
		{
			if (setupEnvironment)
			{
				this.SetupEnvironment();
			}
			using (this.m_hierarchyRoot.PinValue())
			{
				((IScope)this.m_hierarchyRoot.Value()).CalculatePreviousAggregates();
			}
		}

		// Token: 0x06007C80 RID: 31872 RVA: 0x002002C0 File Offset: 0x001FE4C0
		internal override void CalculatePreviousAggregates()
		{
			this.CalculatePreviousAggregates(true);
		}

		// Token: 0x06007C81 RID: 31873 RVA: 0x002002CC File Offset: 0x001FE4CC
		public void ReadRows(DataActions action, ITraversalContext context)
		{
			if (this.m_dataRows != null)
			{
				for (int i = 0; i < this.m_dataRows.Count; i++)
				{
					this.m_dataRows[i].SetFields(this.m_odpContext.ReportObjectModel.FieldsImpl);
					this.ReadRow(action, context);
				}
			}
		}

		// Token: 0x06007C82 RID: 31874 RVA: 0x00200320 File Offset: 0x001FE520
		private void ReadRows(bool sendToParent)
		{
			for (int i = 0; i < this.m_dataRows.Count; i++)
			{
				this.m_dataRows[i].SetFields(this.m_odpContext.ReportObjectModel.FieldsImpl);
				this.ReadRow(sendToParent);
			}
		}

		// Token: 0x06007C83 RID: 31875 RVA: 0x0020036C File Offset: 0x001FE56C
		protected virtual void ResetScopedRunningValues()
		{
			using (this.m_hierarchyRoot.PinValue())
			{
				RuntimeGroupRootObj runtimeGroupRootObj = (RuntimeGroupRootObj)this.m_hierarchyRoot.Value();
				if (runtimeGroupRootObj.ScopedRunningValues != null)
				{
					AggregatesImpl aggregatesImpl = this.m_odpContext.ReportObjectModel.AggregatesImpl;
					foreach (string text in runtimeGroupRootObj.ScopedRunningValues)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj aggregateObj = aggregatesImpl.GetAggregateObj(text);
						Global.Tracer.Assert(aggregateObj != null, "Expected aggregate: {0} not in global collection", new object[] { text });
						aggregateObj.Init();
					}
				}
			}
		}

		// Token: 0x06007C84 RID: 31876 RVA: 0x00200438 File Offset: 0x001FE638
		internal override bool InScope(string scope)
		{
			bool flag2;
			using (this.m_hierarchyRoot.PinValue())
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = ((RuntimeGroupRootObj)this.m_hierarchyRoot.Value()).HierarchyDef.Grouping;
				if (grouping.ScopeNames == null)
				{
					bool flag;
					grouping.ScopeNames = base.GetScopeNames(base.SelfReference, scope, out flag);
					flag2 = flag;
				}
				else
				{
					flag2 = grouping.ScopeNames.Contains(scope);
				}
			}
			return flag2;
		}

		// Token: 0x06007C85 RID: 31877 RVA: 0x002004B8 File Offset: 0x001FE6B8
		protected override int GetRecursiveLevel(string scope)
		{
			if (scope == null)
			{
				return this.m_recursiveLevel;
			}
			int num2;
			using (this.m_hierarchyRoot.PinValue())
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = ((RuntimeGroupRootObj)this.m_hierarchyRoot.Value()).HierarchyDef.Grouping;
				if (grouping.ScopeNames == null)
				{
					int num;
					grouping.ScopeNames = base.GetScopeNames(base.SelfReference, scope, out num);
					num2 = num;
				}
				else
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping2 = grouping.ScopeNames[scope] as Microsoft.ReportingServices.ReportIntermediateFormat.Grouping;
					if (grouping2 != null)
					{
						num2 = grouping2.RecursiveLevel;
					}
					else
					{
						num2 = -1;
					}
				}
			}
			return num2;
		}

		// Token: 0x06007C86 RID: 31878 RVA: 0x00200558 File Offset: 0x001FE758
		protected override void ProcessUserSort()
		{
			using (this.m_hierarchyRoot.PinValue())
			{
				((RuntimeGroupRootObj)this.m_hierarchyRoot.Value()).ProcessingStage = ProcessingStages.UserSortFilter;
			}
			this.m_odpContext.ProcessUserSortForTarget((IReference<IHierarchyObj>)base.SelfReference, ref this.m_dataRows, this.m_userSortTargetInfo.TargetForNonDetailSort);
			if (this.m_userSortTargetInfo.TargetForNonDetailSort)
			{
				this.m_dataAction &= ~DataActions.UserSort;
				this.m_userSortTargetInfo.ResetTargetForNonDetailSort();
				this.m_userSortTargetInfo.EnterProcessUserSortPhase(this.m_odpContext);
				bool flag = false;
				DataActions dataActions;
				this.ConstructRuntimeStructure(ref flag, out dataActions);
				if (!flag)
				{
					Global.Tracer.Assert(dataActions == this.m_dataAction, "(innerDataAction == m_dataAction)");
				}
				if (this.m_dataAction != DataActions.None)
				{
					this.m_dataRows = new ScalableList<DataFieldRow>(base.Depth, this.m_odpContext.TablixProcessingScalabilityCache);
				}
				base.ScopeFinishSorting(ref this.m_firstRow, this.m_userSortTargetInfo);
				this.m_userSortTargetInfo.LeaveProcessUserSortPhase(this.m_odpContext);
			}
		}

		// Token: 0x06007C87 RID: 31879 RVA: 0x00200674 File Offset: 0x001FE874
		protected override void MarkSortInfoProcessed(List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo)
		{
			if (this.m_userSortTargetInfo != null)
			{
				this.m_userSortTargetInfo.MarkSortInfoProcessed(runtimeSortFilterInfo, (IReference<IHierarchyObj>)base.SelfReference);
			}
		}

		// Token: 0x06007C88 RID: 31880 RVA: 0x00200695 File Offset: 0x001FE895
		protected override void AddSortInfoIndex(int sortInfoIndex, IReference<RuntimeSortFilterEventInfo> sortInfo)
		{
			if (this.m_userSortTargetInfo != null)
			{
				this.m_userSortTargetInfo.AddSortInfoIndex(sortInfoIndex, sortInfo);
			}
		}

		// Token: 0x06007C89 RID: 31881 RVA: 0x002006AC File Offset: 0x001FE8AC
		public override IHierarchyObj CreateHierarchyObjForSortTree()
		{
			if (ProcessingStages.UserSortFilter == ((RuntimeGroupRootObj)this.m_hierarchyRoot.Value()).ProcessingStage)
			{
				return new RuntimeSortHierarchyObj(this, this.m_depth + 1);
			}
			return base.CreateHierarchyObjForSortTree();
		}

		// Token: 0x06007C8A RID: 31882 RVA: 0x002006DC File Offset: 0x001FE8DC
		protected override void GetGroupNameValuePairs(Dictionary<string, object> pairs)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = ((RuntimeGroupRootObj)this.m_hierarchyRoot.Value()).HierarchyDef.Grouping;
			if (grouping.ScopeNames == null)
			{
				grouping.ScopeNames = base.GetScopeNames(base.SelfReference, pairs);
				return;
			}
			foreach (object obj in grouping.ScopeNames.Values)
			{
				RuntimeDataRegionObj.AddGroupNameValuePair(this.m_odpContext, obj as Microsoft.ReportingServices.ReportIntermediateFormat.Grouping, pairs);
			}
		}

		// Token: 0x06007C8B RID: 31883 RVA: 0x00200754 File Offset: 0x001FE954
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeGroupLeafObj.m_declaration);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.HierarchyDef)
				{
					if (memberName <= MemberName.UserSortTargetInfo)
					{
						if (memberName == MemberName.Variables)
						{
							writer.WriteSerializableArray(this.m_variableValues);
							continue;
						}
						if (memberName == MemberName.RecursiveLevel)
						{
							writer.Write(this.m_recursiveLevel);
							continue;
						}
						switch (memberName)
						{
						case MemberName.FirstRow:
							writer.Write(this.m_firstRow);
							continue;
						case MemberName.FirstRowIsAggregate:
							writer.Write(this.m_firstRowIsAggregate);
							continue;
						case MemberName.SortFilterExpressionScopeInfoIndices:
							writer.Write(this.m_sortFilterExpressionScopeInfoIndices);
							continue;
						case MemberName.NonCustomAggregates:
							writer.Write<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_nonCustomAggregates);
							continue;
						case MemberName.CustomAggregates:
							writer.Write<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_customAggregates);
							continue;
						case MemberName.DataAction:
							writer.WriteEnum((int)this.m_dataAction);
							continue;
						case MemberName.UserSortTargetInfo:
							writer.Write(this.m_userSortTargetInfo);
							continue;
						}
					}
					else
					{
						switch (memberName)
						{
						case MemberName.Parent:
							writer.Write(this.m_parent);
							continue;
						case MemberName.PostSortAggregates:
							writer.Write<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_postSortAggregates);
							continue;
						case MemberName.RecursiveAggregates:
							writer.Write<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_recursiveAggregates);
							continue;
						default:
							if (memberName == MemberName.DataRows)
							{
								writer.Write(this.m_dataRows);
								continue;
							}
							if (memberName == MemberName.HierarchyDef)
							{
								int num = scalabilityCache.StoreStaticReference(this.m_hierarchyDef);
								writer.Write(num);
								continue;
							}
							break;
						}
					}
				}
				else if (memberName <= MemberName.DetailRowCounter)
				{
					switch (memberName)
					{
					case MemberName.NextLeaf:
						writer.Write(this.m_nextLeaf);
						continue;
					case MemberName.PrevLeaf:
						writer.Write(this.m_prevLeaf);
						continue;
					case MemberName.GroupExprValues:
						writer.WriteListOfVariant(this.m_groupExprValues);
						continue;
					case MemberName.TargetScopeMatched:
						writer.Write(this.m_targetScopeMatched);
						continue;
					default:
						if (memberName == MemberName.IsOuterGrouping)
						{
							writer.Write(this.m_isOuterGrouping);
							continue;
						}
						if (memberName == MemberName.DetailRowCounter)
						{
							writer.Write(this.m_detailRowCounter);
							continue;
						}
						break;
					}
				}
				else if (memberName <= MemberName.AggregatesOfAggregates)
				{
					if (memberName == MemberName.DetailSortAdditionalGroupLeafs)
					{
						writer.Write<IHierarchyObj>(this.m_detailSortAdditionalGroupLeafs);
						continue;
					}
					if (memberName == MemberName.AggregatesOfAggregates)
					{
						writer.Write(this.m_aggregatesOfAggregates);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.PostSortAggregatesOfAggregates)
					{
						writer.Write(this.m_postSortAggregatesOfAggregates);
						continue;
					}
					if (memberName == MemberName.HasProcessedAggregateRow)
					{
						writer.Write(this.m_hasProcessedAggregateRow);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007C8C RID: 31884 RVA: 0x00200A5C File Offset: 0x001FEC5C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeGroupLeafObj.m_declaration);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.HierarchyDef)
				{
					if (memberName <= MemberName.UserSortTargetInfo)
					{
						if (memberName == MemberName.Variables)
						{
							this.m_variableValues = reader.ReadSerializableArray();
							continue;
						}
						if (memberName == MemberName.RecursiveLevel)
						{
							this.m_recursiveLevel = reader.ReadInt32();
							continue;
						}
						switch (memberName)
						{
						case MemberName.FirstRow:
							this.m_firstRow = (DataFieldRow)reader.ReadRIFObject();
							continue;
						case MemberName.FirstRowIsAggregate:
							this.m_firstRowIsAggregate = reader.ReadBoolean();
							continue;
						case MemberName.SortFilterExpressionScopeInfoIndices:
							this.m_sortFilterExpressionScopeInfoIndices = reader.ReadInt32Array();
							continue;
						case MemberName.NonCustomAggregates:
							this.m_nonCustomAggregates = reader.ReadListOfRIFObjects<List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>>();
							continue;
						case MemberName.CustomAggregates:
							this.m_customAggregates = reader.ReadListOfRIFObjects<List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>>();
							continue;
						case MemberName.DataAction:
							this.m_dataAction = (DataActions)reader.ReadEnum();
							continue;
						case MemberName.UserSortTargetInfo:
							this.m_userSortTargetInfo = (RuntimeUserSortTargetInfo)reader.ReadRIFObject();
							continue;
						}
					}
					else
					{
						switch (memberName)
						{
						case MemberName.Parent:
							this.m_parent = (IReference<RuntimeGroupObj>)reader.ReadRIFObject();
							continue;
						case MemberName.PostSortAggregates:
							this.m_postSortAggregates = reader.ReadListOfRIFObjects<List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>>();
							continue;
						case MemberName.RecursiveAggregates:
							this.m_recursiveAggregates = reader.ReadListOfRIFObjects<List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>>();
							continue;
						default:
							if (memberName == MemberName.DataRows)
							{
								this.m_dataRows = reader.ReadRIFObject<ScalableList<DataFieldRow>>();
								continue;
							}
							if (memberName == MemberName.HierarchyDef)
							{
								int num = reader.ReadInt32();
								this.m_hierarchyDef = (Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode)scalabilityCache.FetchStaticReference(num);
								continue;
							}
							break;
						}
					}
				}
				else if (memberName <= MemberName.DetailRowCounter)
				{
					switch (memberName)
					{
					case MemberName.NextLeaf:
						this.m_nextLeaf = (RuntimeGroupLeafObjReference)reader.ReadRIFObject();
						continue;
					case MemberName.PrevLeaf:
						this.m_prevLeaf = (RuntimeGroupLeafObjReference)reader.ReadRIFObject();
						continue;
					case MemberName.GroupExprValues:
						this.m_groupExprValues = reader.ReadListOfVariant<List<object>>();
						continue;
					case MemberName.TargetScopeMatched:
						this.m_targetScopeMatched = reader.ReadBooleanArray();
						continue;
					default:
						if (memberName == MemberName.IsOuterGrouping)
						{
							this.m_isOuterGrouping = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.DetailRowCounter)
						{
							this.m_detailRowCounter = reader.ReadInt32();
							continue;
						}
						break;
					}
				}
				else if (memberName <= MemberName.AggregatesOfAggregates)
				{
					if (memberName == MemberName.DetailSortAdditionalGroupLeafs)
					{
						this.m_detailSortAdditionalGroupLeafs = reader.ReadListOfRIFObjects<List<IHierarchyObj>>();
						continue;
					}
					if (memberName == MemberName.AggregatesOfAggregates)
					{
						this.m_aggregatesOfAggregates = (BucketedDataAggregateObjs)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.PostSortAggregatesOfAggregates)
					{
						this.m_postSortAggregatesOfAggregates = (BucketedDataAggregateObjs)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.HasProcessedAggregateRow)
					{
						this.m_hasProcessedAggregateRow = reader.ReadBoolean();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007C8D RID: 31885 RVA: 0x00200D8C File Offset: 0x001FEF8C
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007C8E RID: 31886 RVA: 0x00200D96 File Offset: 0x001FEF96
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupLeafObj;
		}

		// Token: 0x06007C8F RID: 31887 RVA: 0x00200DA0 File Offset: 0x001FEFA0
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeGroupLeafObj.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupLeafObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupObj, new List<MemberInfo>
				{
					new MemberInfo(MemberName.NonCustomAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj),
					new MemberInfo(MemberName.CustomAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj),
					new MemberInfo(MemberName.FirstRow, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataFieldRow),
					new MemberInfo(MemberName.FirstRowIsAggregate, Token.Boolean),
					new MemberInfo(MemberName.NextLeaf, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupLeafObjReference),
					new MemberInfo(MemberName.PrevLeaf, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupLeafObjReference),
					new MemberInfo(MemberName.DataRows, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList),
					new MemberInfo(MemberName.Parent, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupObjReference),
					new MemberInfo(MemberName.RecursiveAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj),
					new MemberInfo(MemberName.PostSortAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj),
					new MemberInfo(MemberName.RecursiveLevel, Token.Int32),
					new MemberInfo(MemberName.GroupExprValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.VariantList),
					new MemberInfo(MemberName.TargetScopeMatched, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Boolean),
					new MemberInfo(MemberName.DataAction, Token.Enum),
					new MemberInfo(MemberName.UserSortTargetInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeUserSortTargetInfo),
					new MemberInfo(MemberName.SortFilterExpressionScopeInfoIndices, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Int32),
					new MemberInfo(MemberName.HierarchyDef, Token.Int32),
					new MemberInfo(MemberName.IsOuterGrouping, Token.Boolean),
					new MemberInfo(MemberName.Variables, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SerializableArray, Token.Serializable),
					new MemberInfo(MemberName.DetailRowCounter, Token.Int32),
					new MemberInfo(MemberName.DetailSortAdditionalGroupLeafs, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IHierarchyObj),
					new MemberInfo(MemberName.AggregatesOfAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BucketedDataAggregateObjs),
					new MemberInfo(MemberName.PostSortAggregatesOfAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BucketedDataAggregateObjs),
					new MemberInfo(MemberName.HasProcessedAggregateRow, Token.Boolean)
				});
			}
			return RuntimeGroupLeafObj.m_declaration;
		}

		// Token: 0x170028B0 RID: 10416
		// (get) Token: 0x06007C90 RID: 31888 RVA: 0x00200FA4 File Offset: 0x001FF1A4
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_nonCustomAggregates) + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_customAggregates) + ItemSizes.SizeOf(this.m_firstRow) + 1 + ItemSizes.SizeOf(this.m_nextLeaf) + ItemSizes.SizeOf(this.m_prevLeaf) + ItemSizes.SizeOf<DataFieldRow>(this.m_dataRows) + ItemSizes.SizeOf(this.m_parent) + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_recursiveAggregates) + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_postSortAggregates) + 4 + ItemSizes.SizeOf(this.m_groupExprValues) + ItemSizes.SizeOf(this.m_targetScopeMatched) + 4 + ItemSizes.SizeOf(this.m_userSortTargetInfo) + ItemSizes.SizeOf(this.m_sortFilterExpressionScopeInfoIndices) + 1 + ItemSizes.SizeOf(this.m_variableValues) + 4 + ItemSizes.SizeOf<IHierarchyObj>(this.m_detailSortAdditionalGroupLeafs) + ItemSizes.ReferenceSize + ItemSizes.SizeOf(this.m_aggregatesOfAggregates) + ItemSizes.SizeOf(this.m_postSortAggregatesOfAggregates) + 1;
			}
		}

		// Token: 0x04003DA9 RID: 15785
		protected List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> m_nonCustomAggregates;

		// Token: 0x04003DAA RID: 15786
		protected BucketedDataAggregateObjs m_aggregatesOfAggregates;

		// Token: 0x04003DAB RID: 15787
		protected BucketedDataAggregateObjs m_postSortAggregatesOfAggregates;

		// Token: 0x04003DAC RID: 15788
		protected List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> m_customAggregates;

		// Token: 0x04003DAD RID: 15789
		protected DataFieldRow m_firstRow;

		// Token: 0x04003DAE RID: 15790
		protected bool m_firstRowIsAggregate;

		// Token: 0x04003DAF RID: 15791
		protected RuntimeGroupLeafObjReference m_nextLeaf;

		// Token: 0x04003DB0 RID: 15792
		protected RuntimeGroupLeafObjReference m_prevLeaf;

		// Token: 0x04003DB1 RID: 15793
		protected ScalableList<DataFieldRow> m_dataRows;

		// Token: 0x04003DB2 RID: 15794
		protected IReference<RuntimeGroupObj> m_parent;

		// Token: 0x04003DB3 RID: 15795
		protected List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> m_recursiveAggregates;

		// Token: 0x04003DB4 RID: 15796
		protected List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> m_postSortAggregates;

		// Token: 0x04003DB5 RID: 15797
		protected int m_recursiveLevel;

		// Token: 0x04003DB6 RID: 15798
		protected List<object> m_groupExprValues;

		// Token: 0x04003DB7 RID: 15799
		protected bool[] m_targetScopeMatched;

		// Token: 0x04003DB8 RID: 15800
		protected DataActions m_dataAction;

		// Token: 0x04003DB9 RID: 15801
		protected RuntimeUserSortTargetInfo m_userSortTargetInfo;

		// Token: 0x04003DBA RID: 15802
		protected int[] m_sortFilterExpressionScopeInfoIndices;

		// Token: 0x04003DBB RID: 15803
		protected object[] m_variableValues;

		// Token: 0x04003DBC RID: 15804
		protected int m_detailRowCounter;

		// Token: 0x04003DBD RID: 15805
		protected List<IHierarchyObj> m_detailSortAdditionalGroupLeafs;

		// Token: 0x04003DBE RID: 15806
		protected Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode m_hierarchyDef;

		// Token: 0x04003DBF RID: 15807
		protected bool m_isOuterGrouping;

		// Token: 0x04003DC0 RID: 15808
		protected bool m_hasProcessedAggregateRow;

		// Token: 0x04003DC1 RID: 15809
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeGroupLeafObj.GetDeclaration();
	}
}

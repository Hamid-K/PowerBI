using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008EA RID: 2282
	[PersistedWithinRequestOnly]
	public abstract class RuntimeGroupRootObj : RuntimeGroupObj, ReportProcessing.IFilterOwner, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IDataCorrelation
	{
		// Token: 0x06007D11 RID: 32017 RVA: 0x00203C5D File Offset: 0x00201E5D
		protected RuntimeGroupRootObj()
		{
		}

		// Token: 0x06007D12 RID: 32018 RVA: 0x00203C74 File Offset: 0x00201E74
		protected RuntimeGroupRootObj(IReference<IScope> outerScope, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode hierarchyDef, DataActions dataAction, OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(odpContext, objectType, outerScope.Value().Depth + 1)
		{
			this.m_hierarchyRoot = (RuntimeHierarchyObjReference)this.m_selfReference;
			this.m_outerScope = outerScope;
			this.m_hierarchyDef = hierarchyDef;
			Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = hierarchyDef.Grouping;
			Global.Tracer.Assert(grouping != null, "(null != groupDef)");
			this.m_isDetailGroup = grouping.IsDetail;
			if (this.m_isDetailGroup)
			{
				this.m_expression = null;
			}
			else
			{
				this.m_expression = new RuntimeExpressionInfo(grouping.GroupExpressions, grouping.ExprHost, grouping.SortDirections, 0);
			}
			if (this.m_odpContext.RuntimeSortFilterInfo != null)
			{
				int count = this.m_odpContext.RuntimeSortFilterInfo.Count;
				using (outerScope.PinValue())
				{
					IScope scope = outerScope.Value();
					for (int i = 0; i < count; i++)
					{
						IReference<RuntimeSortFilterEventInfo> reference = this.m_odpContext.RuntimeSortFilterInfo[i];
						using (reference.PinValue())
						{
							RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = reference.Value();
							if (runtimeSortFilterEventInfo.EventSource.ContainingScopes == null || runtimeSortFilterEventInfo.EventSource.ContainingScopes.Count == 0 || runtimeSortFilterEventInfo.HasEventSourceScope || (this.m_isDetailGroup && runtimeSortFilterEventInfo.EventSource.IsSubReportTopLevelScope))
							{
								bool flag = false;
								if (this.m_isDetailGroup)
								{
									if (!scope.TargetForNonDetailSort && this.IsTargetForSort(i, true) && runtimeSortFilterEventInfo.EventTarget != base.SelfReference && scope.TargetScopeMatched(i, true))
									{
										flag = true;
										if (this.m_detailUserSortTargetInfo == null)
										{
											this.m_detailUserSortTargetInfo = new RuntimeUserSortTargetInfo((IReference<IHierarchyObj>)base.SelfReference, i, reference);
										}
										else
										{
											this.m_detailUserSortTargetInfo.AddSortInfo((IReference<IHierarchyObj>)base.SelfReference, i, reference);
										}
									}
								}
								else if (grouping.IsSortFilterExpressionScope != null)
								{
									flag = grouping.IsSortFilterExpressionScope[i] && this.m_odpContext.UserSortFilterContext.InProcessUserSortPhase(i) && this.TargetScopeMatched(i, false);
								}
								if (flag)
								{
									if (this.m_builtinSortOverridden == null)
									{
										this.m_builtinSortOverridden = new bool[count];
									}
									this.m_builtinSortOverridden[i] = true;
								}
							}
						}
					}
				}
			}
			if (this.m_detailUserSortTargetInfo != null)
			{
				this.m_groupingType = RuntimeGroupingObj.GroupingTypes.DetailUserSort;
			}
			else if (grouping.GroupAndSort && !this.BuiltinSortOverridden)
			{
				this.m_groupingType = RuntimeGroupingObj.GroupingTypes.Sort;
			}
			else if (grouping.IsDetail && grouping.Parent == null && !this.BuiltinSortOverridden)
			{
				this.m_groupingType = RuntimeGroupingObj.GroupingTypes.Detail;
			}
			else if (grouping.NaturalGroup)
			{
				this.m_groupingType = RuntimeGroupingObj.GroupingTypes.NaturalGroup;
			}
			else
			{
				this.m_groupingType = RuntimeGroupingObj.GroupingTypes.Hash;
			}
			this.m_grouping = RuntimeGroupingObj.CreateGroupingObj(this.m_groupingType, this, objectType);
			if (grouping.Filters == null)
			{
				this.m_dataAction = dataAction;
				this.m_outerDataAction = dataAction;
			}
			if (grouping.RecursiveAggregates != null)
			{
				this.m_dataAction |= DataActions.RecursiveAggregates;
			}
			if (grouping.PostSortAggregates != null)
			{
				this.m_dataAction |= DataActions.PostSortAggregates;
			}
			if (grouping.Parent != null)
			{
				this.m_parentExpression = new RuntimeExpressionInfo(grouping.Parent, grouping.ParentExprHost, null, 0);
			}
		}

		// Token: 0x170028BF RID: 10431
		// (get) Token: 0x06007D13 RID: 32019 RVA: 0x00203FD0 File Offset: 0x002021D0
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode HierarchyDef
		{
			get
			{
				return this.m_hierarchyDef;
			}
		}

		// Token: 0x170028C0 RID: 10432
		// (get) Token: 0x06007D14 RID: 32020 RVA: 0x00203FD8 File Offset: 0x002021D8
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo> GroupExpressions
		{
			get
			{
				return this.m_hierarchyDef.Grouping.GroupExpressions;
			}
		}

		// Token: 0x170028C1 RID: 10433
		// (get) Token: 0x06007D15 RID: 32021 RVA: 0x00203FEA File Offset: 0x002021EA
		internal GroupExprHost GroupExpressionHost
		{
			get
			{
				return this.m_hierarchyDef.Grouping.ExprHost;
			}
		}

		// Token: 0x170028C2 RID: 10434
		// (get) Token: 0x06007D16 RID: 32022 RVA: 0x00203FFC File Offset: 0x002021FC
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.ExpressionInfo> SortExpressions
		{
			get
			{
				if (this.m_hierarchyDef.Sorting != null && this.m_hierarchyDef.Sorting.ShouldApplySorting)
				{
					return this.m_hierarchyDef.Sorting.SortExpressions;
				}
				return null;
			}
		}

		// Token: 0x170028C3 RID: 10435
		// (get) Token: 0x06007D17 RID: 32023 RVA: 0x0020402F File Offset: 0x0020222F
		internal SortExprHost SortExpressionHost
		{
			get
			{
				return this.m_hierarchyDef.Sorting.ExprHost;
			}
		}

		// Token: 0x170028C4 RID: 10436
		// (get) Token: 0x06007D18 RID: 32024 RVA: 0x00204041 File Offset: 0x00202241
		internal List<bool> GroupDirections
		{
			get
			{
				return this.m_hierarchyDef.Grouping.SortDirections;
			}
		}

		// Token: 0x170028C5 RID: 10437
		// (get) Token: 0x06007D19 RID: 32025 RVA: 0x00204053 File Offset: 0x00202253
		internal List<bool> SortDirections
		{
			get
			{
				return this.m_hierarchyDef.Sorting.SortDirections;
			}
		}

		// Token: 0x170028C6 RID: 10438
		// (get) Token: 0x06007D1A RID: 32026 RVA: 0x00204065 File Offset: 0x00202265
		internal RuntimeExpressionInfo Expression
		{
			get
			{
				return this.m_expression;
			}
		}

		// Token: 0x170028C7 RID: 10439
		// (get) Token: 0x06007D1B RID: 32027 RVA: 0x0020406D File Offset: 0x0020226D
		internal List<string> ScopedRunningValues
		{
			get
			{
				return this.m_scopedRunningValues;
			}
		}

		// Token: 0x170028C8 RID: 10440
		// (get) Token: 0x06007D1C RID: 32028 RVA: 0x00204075 File Offset: 0x00202275
		internal Dictionary<string, IReference<RuntimeGroupRootObj>> GroupCollection
		{
			get
			{
				return this.m_groupCollection;
			}
		}

		// Token: 0x170028C9 RID: 10441
		// (get) Token: 0x06007D1D RID: 32029 RVA: 0x0020407D File Offset: 0x0020227D
		internal DataActions DataAction
		{
			get
			{
				return this.m_dataAction;
			}
		}

		// Token: 0x170028CA RID: 10442
		// (get) Token: 0x06007D1E RID: 32030 RVA: 0x00204085 File Offset: 0x00202285
		// (set) Token: 0x06007D1F RID: 32031 RVA: 0x0020408D File Offset: 0x0020228D
		internal ProcessingStages ProcessingStage
		{
			get
			{
				return this.m_processingStage;
			}
			set
			{
				this.m_processingStage = value;
			}
		}

		// Token: 0x170028CB RID: 10443
		// (get) Token: 0x06007D20 RID: 32032 RVA: 0x00204096 File Offset: 0x00202296
		internal DataRegionInstance DataRegionInstance
		{
			get
			{
				return this.m_hierarchyDef.DataRegionDef.CurrentDataRegionInstance;
			}
		}

		// Token: 0x170028CC RID: 10444
		// (get) Token: 0x06007D21 RID: 32033 RVA: 0x002040A8 File Offset: 0x002022A8
		internal RuntimeGroupingObj.GroupingTypes GroupingType
		{
			get
			{
				return this.m_groupingType;
			}
		}

		// Token: 0x170028CD RID: 10445
		// (get) Token: 0x06007D22 RID: 32034 RVA: 0x002040B0 File Offset: 0x002022B0
		internal Filters GroupFilters
		{
			get
			{
				return this.m_groupFilters;
			}
		}

		// Token: 0x170028CE RID: 10446
		// (get) Token: 0x06007D23 RID: 32035 RVA: 0x002040B8 File Offset: 0x002022B8
		internal bool HasParent
		{
			get
			{
				return this.m_parentExpression != null;
			}
		}

		// Token: 0x170028CF RID: 10447
		// (get) Token: 0x06007D24 RID: 32036 RVA: 0x002040C3 File Offset: 0x002022C3
		protected override IReference<IScope> OuterScope
		{
			get
			{
				return this.m_outerScope;
			}
		}

		// Token: 0x170028D0 RID: 10448
		// (get) Token: 0x06007D25 RID: 32037 RVA: 0x002040CB File Offset: 0x002022CB
		internal IReference<IScope> GroupRootOuterScope
		{
			get
			{
				return this.m_outerScope;
			}
		}

		// Token: 0x170028D1 RID: 10449
		// (get) Token: 0x06007D26 RID: 32038 RVA: 0x002040D3 File Offset: 0x002022D3
		internal bool SaveGroupExprValues
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170028D2 RID: 10450
		// (get) Token: 0x06007D27 RID: 32039 RVA: 0x002040D6 File Offset: 0x002022D6
		internal bool IsDetailGroup
		{
			get
			{
				return this.m_isDetailGroup;
			}
		}

		// Token: 0x170028D3 RID: 10451
		// (get) Token: 0x06007D28 RID: 32040 RVA: 0x002040DE File Offset: 0x002022DE
		protected override bool IsDetail
		{
			get
			{
				return this.m_isDetailGroup || base.IsDetail;
			}
		}

		// Token: 0x170028D4 RID: 10452
		// (get) Token: 0x06007D29 RID: 32041 RVA: 0x002040F0 File Offset: 0x002022F0
		protected override int ExpressionIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170028D5 RID: 10453
		// (get) Token: 0x06007D2A RID: 32042 RVA: 0x002040F3 File Offset: 0x002022F3
		protected override List<int> SortFilterInfoIndices
		{
			get
			{
				if (this.m_detailUserSortTargetInfo != null)
				{
					return this.m_detailUserSortTargetInfo.SortFilterInfoIndices;
				}
				return null;
			}
		}

		// Token: 0x170028D6 RID: 10454
		// (get) Token: 0x06007D2B RID: 32043 RVA: 0x0020410A File Offset: 0x0020230A
		internal BTree GroupOrDetailSortTree
		{
			get
			{
				return this.SortTree;
			}
		}

		// Token: 0x170028D7 RID: 10455
		// (get) Token: 0x06007D2C RID: 32044 RVA: 0x00204112 File Offset: 0x00202312
		protected override BTree SortTree
		{
			get
			{
				if (this.m_detailUserSortTargetInfo != null)
				{
					return this.m_detailUserSortTargetInfo.SortTree;
				}
				if (this.m_grouping != null)
				{
					return this.m_grouping.Tree;
				}
				return null;
			}
		}

		// Token: 0x170028D8 RID: 10456
		// (get) Token: 0x06007D2D RID: 32045 RVA: 0x00204140 File Offset: 0x00202340
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

		// Token: 0x170028D9 RID: 10457
		// (get) Token: 0x06007D2E RID: 32046 RVA: 0x0020419A File Offset: 0x0020239A
		internal override IRIFReportScope RIFReportScope
		{
			get
			{
				return this.m_hierarchyDef;
			}
		}

		// Token: 0x170028DA RID: 10458
		// (get) Token: 0x06007D2F RID: 32047 RVA: 0x002041A4 File Offset: 0x002023A4
		private bool BuiltinSortOverridden
		{
			get
			{
				if (this.m_detailUserSortTargetInfo != null && this.IsDetailGroup)
				{
					return true;
				}
				if (this.m_odpContext.RuntimeSortFilterInfo != null && this.m_builtinSortOverridden != null)
				{
					for (int i = 0; i < this.m_odpContext.RuntimeSortFilterInfo.Count; i++)
					{
						if (this.m_odpContext.UserSortFilterContext.InProcessUserSortPhase(i) && this.m_builtinSortOverridden[i])
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x170028DB RID: 10459
		// (get) Token: 0x06007D30 RID: 32048 RVA: 0x00204213 File Offset: 0x00202413
		internal bool ProcessSecondPassSorting
		{
			get
			{
				return (SecondPassOperations.Sorting & this.m_odpContext.SecondPassOperation) != SecondPassOperations.None && !this.BuiltinSortOverridden && this.HierarchyDef.Sorting != null && this.HierarchyDef.Sorting.ShouldApplySorting;
			}
		}

		// Token: 0x170028DC RID: 10460
		// (get) Token: 0x06007D31 RID: 32049 RVA: 0x0020424B File Offset: 0x0020244B
		// (set) Token: 0x06007D32 RID: 32050 RVA: 0x00204253 File Offset: 0x00202453
		internal ScalableList<DataFieldRow> DetailDataRows
		{
			get
			{
				return this.m_detailDataRows;
			}
			set
			{
				this.m_detailDataRows = value;
			}
		}

		// Token: 0x06007D33 RID: 32051 RVA: 0x0020425C File Offset: 0x0020245C
		internal override void GetScopeValues(IReference<IHierarchyObj> targetScopeObj, List<object>[] scopeValues, ref int index)
		{
			if (targetScopeObj == null || this != targetScopeObj.Value())
			{
				if (this.m_isDetailGroup)
				{
					base.DetailGetScopeValues(this.m_outerScope, targetScopeObj, scopeValues, ref index);
					return;
				}
				this.m_outerScope.Value().GetScopeValues(targetScopeObj, scopeValues, ref index);
			}
		}

		// Token: 0x06007D34 RID: 32052 RVA: 0x00204295 File Offset: 0x00202495
		internal override bool TargetScopeMatched(int index, bool detailSort)
		{
			if (this.m_isDetailGroup)
			{
				return base.DetailTargetScopeMatched(this.m_hierarchyDef.DataRegionDef, this.m_outerScope, this.m_hierarchyDef.IsColumn, index);
			}
			return this.m_outerScope.Value().TargetScopeMatched(index, detailSort);
		}

		// Token: 0x06007D35 RID: 32053
		protected abstract void UpdateDataRegionGroupRootInfo();

		// Token: 0x06007D36 RID: 32054 RVA: 0x002042D5 File Offset: 0x002024D5
		internal override void NextRow()
		{
			if (this.m_hierarchyDef.DataScopeInfo == null || !this.m_hierarchyDef.DataScopeInfo.NeedsIDC)
			{
				this.NextRegularRow();
			}
		}

		// Token: 0x06007D37 RID: 32055 RVA: 0x002042FD File Offset: 0x002024FD
		bool IDataCorrelation.NextCorrelatedRow()
		{
			return this.NextRegularRow();
		}

		// Token: 0x06007D38 RID: 32056 RVA: 0x00204308 File Offset: 0x00202508
		private bool NextRegularRow()
		{
			this.UpdateDataRegionGroupRootInfo();
			if (!this.ProcessThisRow())
			{
				return false;
			}
			DomainScopeContext domainScopeContext = base.OdpContext.DomainScopeContext;
			DomainScopeContext.DomainScopeInfo domainScopeInfo = null;
			if (domainScopeContext != null)
			{
				domainScopeInfo = domainScopeContext.CurrentDomainScope;
			}
			if (domainScopeInfo != null)
			{
				domainScopeInfo.MoveNext();
				this.m_currentGroupExprValue = domainScopeInfo.CurrentKey;
			}
			else if (this.m_expression != null)
			{
				this.m_currentGroupExprValue = this.EvaluateGroupExpression(this.m_expression, "Group");
			}
			else
			{
				this.m_currentGroupExprValue = this.m_odpContext.ReportObjectModel.FieldsImpl.GetRowIndex();
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = this.m_hierarchyDef.Grouping;
			if (this.SaveGroupExprValues)
			{
				grouping.CurrentGroupExpressionValues = new List<object>(1);
				grouping.CurrentGroupExpressionValues.Add(this.m_currentGroupExprValue);
			}
			if (this.m_isDetailGroup)
			{
				if (this.m_detailUserSortTargetInfo != null)
				{
					this.ProcessDetailSort();
				}
				else
				{
					this.m_grouping.NextRow(this.m_currentGroupExprValue, false, null);
				}
			}
			else
			{
				if (this.m_odpContext.RuntimeSortFilterInfo != null)
				{
					int count = this.m_odpContext.RuntimeSortFilterInfo.Count;
					if (grouping.SortFilterScopeMatched == null)
					{
						grouping.SortFilterScopeMatched = new bool[count];
					}
					for (int i = 0; i < count; i++)
					{
						grouping.SortFilterScopeMatched[i] = true;
					}
				}
				base.MatchSortFilterScope(this.m_outerScope, grouping, this.m_currentGroupExprValue, 0);
				object obj = null;
				bool flag = this.m_parentExpression != null;
				if (flag)
				{
					obj = this.EvaluateGroupExpression(this.m_parentExpression, "Parent");
				}
				this.m_grouping.NextRow(this.m_currentGroupExprValue, flag, obj);
			}
			if (domainScopeInfo != null)
			{
				domainScopeInfo.MovePrevious();
			}
			return true;
		}

		// Token: 0x06007D39 RID: 32057 RVA: 0x002044A0 File Offset: 0x002026A0
		protected void ProcessDetailSort()
		{
			if (this.m_detailUserSortTargetInfo != null && !this.m_detailUserSortTargetInfo.TargetForNonDetailSort)
			{
				IReference<RuntimeSortFilterEventInfo> reference = this.m_odpContext.RuntimeSortFilterInfo[this.m_detailUserSortTargetInfo.SortFilterInfoIndices[0]];
				object sortOrder;
				using (reference.PinValue())
				{
					sortOrder = reference.Value().GetSortOrder(this.m_odpContext.ReportRuntime);
				}
				this.m_detailUserSortTargetInfo.SortTree.NextRow(sortOrder, this);
			}
		}

		// Token: 0x06007D3A RID: 32058 RVA: 0x00204530 File Offset: 0x00202730
		internal IHierarchyObj CreateDetailSortHierarchyObj(RuntimeGroupLeafObj rootSortDetailLeafObj)
		{
			Global.Tracer.Assert(this.m_detailUserSortTargetInfo != null, "(null != m_detailUserSortTargetInfo)");
			return new RuntimeSortHierarchyObj(this, base.Depth);
		}

		// Token: 0x06007D3B RID: 32059 RVA: 0x00204556 File Offset: 0x00202756
		public override IHierarchyObj CreateHierarchyObjForSortTree()
		{
			if (this.m_detailUserSortTargetInfo != null)
			{
				return new RuntimeSortHierarchyObj(this, base.Depth);
			}
			return base.CreateHierarchyObjForSortTree();
		}

		// Token: 0x06007D3C RID: 32060 RVA: 0x00204573 File Offset: 0x00202773
		protected override void ProcessUserSort()
		{
			if (this.m_detailUserSortTargetInfo != null)
			{
				this.m_odpContext.ProcessUserSortForTarget((IReference<IHierarchyObj>)base.SelfReference, ref this.m_detailDataRows, this.m_detailUserSortTargetInfo.TargetForNonDetailSort);
				return;
			}
			base.ProcessUserSort();
		}

		// Token: 0x06007D3D RID: 32061 RVA: 0x002045AB File Offset: 0x002027AB
		protected override void MarkSortInfoProcessed(List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo)
		{
			if (this.m_detailUserSortTargetInfo != null)
			{
				this.m_detailUserSortTargetInfo.MarkSortInfoProcessed(runtimeSortFilterInfo, (IReference<IHierarchyObj>)base.SelfReference);
				return;
			}
			base.MarkSortInfoProcessed(runtimeSortFilterInfo);
		}

		// Token: 0x06007D3E RID: 32062 RVA: 0x002045D4 File Offset: 0x002027D4
		protected override void AddSortInfoIndex(int sortInfoIndex, IReference<RuntimeSortFilterEventInfo> sortInfo)
		{
			if (this.m_detailUserSortTargetInfo != null)
			{
				this.m_detailUserSortTargetInfo.AddSortInfoIndex(sortInfoIndex, sortInfo);
				return;
			}
			base.AddSortInfoIndex(sortInfoIndex, sortInfo);
		}

		// Token: 0x06007D3F RID: 32063 RVA: 0x002045F4 File Offset: 0x002027F4
		private object EvaluateGroupExpression(RuntimeExpressionInfo expression, string propertyName)
		{
			Global.Tracer.Assert(this.m_hierarchyDef.Grouping != null, "(null != m_hierarchyDef.Grouping)");
			return this.m_odpContext.ReportRuntime.EvaluateRuntimeExpression(expression, Microsoft.ReportingServices.ReportProcessing.ObjectType.Grouping, this.m_hierarchyDef.Grouping.Name, propertyName);
		}

		// Token: 0x06007D40 RID: 32064 RVA: 0x00204644 File Offset: 0x00202844
		protected bool ProcessThisRow()
		{
			FieldsImpl fieldsImpl = this.m_odpContext.ReportObjectModel.FieldsImpl;
			if (fieldsImpl.IsAggregateRow && 0 > fieldsImpl.AggregationFieldCount)
			{
				return false;
			}
			int[] groupExpressionFieldIndices = this.m_hierarchyDef.Grouping.GetGroupExpressionFieldIndices();
			if (groupExpressionFieldIndices == null)
			{
				fieldsImpl.ValidAggregateRow = false;
			}
			else
			{
				foreach (int num in groupExpressionFieldIndices)
				{
					if (-1 > num || (0 <= num && !fieldsImpl[num].IsAggregationField))
					{
						fieldsImpl.ValidAggregateRow = false;
					}
				}
			}
			return !fieldsImpl.IsAggregateRow || fieldsImpl.ValidAggregateRow;
		}

		// Token: 0x06007D41 RID: 32065 RVA: 0x002046D4 File Offset: 0x002028D4
		internal void AddChildWithNoParent(RuntimeGroupLeafObjReference child)
		{
			if (RuntimeGroupingObj.GroupingTypes.Sort == this.m_groupingType)
			{
				using (child.PinValue())
				{
					child.Value().Parent = (RuntimeGroupObjReference)this.m_selfReference;
					return;
				}
			}
			base.AddChild(child);
		}

		// Token: 0x06007D42 RID: 32066 RVA: 0x0020472C File Offset: 0x0020292C
		private bool DetermineTraversalDirection()
		{
			bool flag = true;
			if (this.m_detailUserSortTargetInfo != null && this.IsDetailGroup && this.GroupOrDetailSortTree != null)
			{
				flag = this.GetDetailSortDirection();
			}
			else if (this.m_expression != null)
			{
				flag = this.m_expression.Direction;
			}
			return flag;
		}

		// Token: 0x06007D43 RID: 32067 RVA: 0x00204774 File Offset: 0x00202974
		internal override bool SortAndFilter(AggregateUpdateContext aggContext)
		{
			if (this.m_odpContext.HasSecondPassOperation(SecondPassOperations.FilteringOrAggregatesOrDomainScope))
			{
				this.CopyDomainScopeGroupInstancesFromTarget();
			}
			RuntimeGroupingObj grouping = this.m_grouping;
			bool flag = this.DetermineTraversalDirection();
			bool flag2 = true;
			bool processSecondPassSorting = this.ProcessSecondPassSorting;
			bool flag3 = (SecondPassOperations.FilteringOrAggregatesOrDomainScope & this.m_odpContext.SecondPassOperation) != SecondPassOperations.None && (this.m_hierarchyDef.HasFilters || this.m_hierarchyDef.HasInnerFilters);
			if (processSecondPassSorting)
			{
				this.m_expression = new RuntimeExpressionInfo(this.m_hierarchyDef.Sorting.SortExpressions, this.m_hierarchyDef.Sorting.ExprHost, this.m_hierarchyDef.Sorting.SortDirections, 0);
				this.m_groupingType = RuntimeGroupingObj.GroupingTypes.Sort;
				this.m_grouping = new RuntimeGroupingObjTree(this, this.m_objectType);
			}
			else if (flag3)
			{
				this.m_groupingType = RuntimeGroupingObj.GroupingTypes.None;
				this.m_grouping = new RuntimeGroupingObjLinkedList(this, this.m_objectType);
			}
			if (flag3)
			{
				this.m_groupFilters = new Filters(Filters.FilterTypes.GroupFilter, (IReference<ReportProcessing.IFilterOwner>)base.SelfReference, this.m_hierarchyDef.Grouping.Filters, Microsoft.ReportingServices.ReportProcessing.ObjectType.Grouping, this.m_hierarchyDef.Grouping.Name, this.m_odpContext, base.Depth + 1);
			}
			this.m_processingStage = ProcessingStages.SortAndFilter;
			this.m_lastChild = null;
			grouping.Traverse(ProcessingStages.SortAndFilter, flag, aggContext);
			if (flag3)
			{
				this.m_groupFilters.FinishReadingGroups(aggContext);
				if (!processSecondPassSorting && null == this.m_lastChild)
				{
					if (this.m_firstChild != null)
					{
						this.m_firstChild.Free();
					}
					this.m_firstChild = null;
					flag2 = false;
				}
			}
			if (grouping != this.m_grouping)
			{
				grouping.Cleanup();
			}
			return flag2;
		}

		// Token: 0x06007D44 RID: 32068 RVA: 0x00204908 File Offset: 0x00202B08
		private void CopyDomainScopeGroupInstancesFromTarget()
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Grouping grouping = this.HierarchyDef.Grouping;
			if (grouping != null && grouping.DomainScope != null)
			{
				IReference<RuntimeGroupRootObj> reference;
				bool flag = base.OdpContext.DomainScopeContext.DomainScopes.TryGetValue(grouping.ScopeIDForDomainScope, out reference);
				Global.Tracer.Assert(flag, "DomainScopes should contain the target group root for the specified group");
				using (reference.PinValue())
				{
					RuntimeHierarchyObj runtimeHierarchyObj = reference.Value();
					this.ProcessingStage = ProcessingStages.Grouping;
					runtimeHierarchyObj.m_grouping.CopyDomainScopeGroupInstances(this);
					this.ProcessingStage = ProcessingStages.SortAndFilter;
				}
			}
		}

		// Token: 0x06007D45 RID: 32069 RVA: 0x0020499C File Offset: 0x00202B9C
		public override void UpdateAggregates(AggregateUpdateContext context)
		{
			this.m_grouping.Traverse(ProcessingStages.UpdateAggregates, this.DetermineTraversalDirection(), context);
		}

		// Token: 0x06007D46 RID: 32070 RVA: 0x002049B1 File Offset: 0x00202BB1
		void ReportProcessing.IFilterOwner.PostFilterNextRow()
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007D47 RID: 32071 RVA: 0x002049BE File Offset: 0x00202BBE
		internal virtual void AddScopedRunningValue(Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj runningValueObj)
		{
			if (this.m_scopedRunningValues == null)
			{
				this.m_scopedRunningValues = new List<string>();
			}
			if (!this.m_scopedRunningValues.Contains(runningValueObj.Name))
			{
				this.m_scopedRunningValues.Add(runningValueObj.Name);
			}
		}

		// Token: 0x06007D48 RID: 32072 RVA: 0x002049F7 File Offset: 0x00202BF7
		internal override void CalculateRunningValues(Dictionary<string, IReference<RuntimeGroupRootObj>> groupCol, IReference<RuntimeGroupRootObj> lastGroup, AggregateUpdateContext aggContext)
		{
			this.SetupRunningValues(groupCol);
		}

		// Token: 0x06007D49 RID: 32073 RVA: 0x00204A00 File Offset: 0x00202C00
		protected void SetupRunningValues(Dictionary<string, IReference<RuntimeGroupRootObj>> groupCol)
		{
			this.m_groupCollection = groupCol;
			if (this.m_hierarchyDef.Grouping.Name != null)
			{
				groupCol[this.m_hierarchyDef.Grouping.Name] = (RuntimeGroupRootObjReference)this.m_selfReference;
			}
		}

		// Token: 0x06007D4A RID: 32074 RVA: 0x00204A3C File Offset: 0x00202C3C
		protected void AddRunningValues(List<Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo> runningValues)
		{
			this.AddRunningValues(runningValues, ref this.m_runningValuesInGroup, ref this.m_previousValuesInGroup, this.m_groupCollection, false, false);
		}

		// Token: 0x06007D4B RID: 32075 RVA: 0x00204A5C File Offset: 0x00202C5C
		protected void AddRunningValuesOfAggregates()
		{
			if (this.m_hierarchyDef.DataScopeInfo == null)
			{
				return;
			}
			List<string> list = null;
			List<string> list2 = null;
			this.AddRunningValues(this.m_hierarchyDef.DataScopeInfo.RunningValuesOfAggregates, ref list, ref list2, this.m_groupCollection, false, false);
		}

		// Token: 0x06007D4C RID: 32076 RVA: 0x00204AA0 File Offset: 0x00202CA0
		protected bool AddRunningValues(List<Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo> runningValues, ref List<string> runningValuesInGroup, ref List<string> previousValuesInGroup, Dictionary<string, IReference<RuntimeGroupRootObj>> groupCollection, bool cellRunningValues, bool outermostStatics)
		{
			bool flag = false;
			if (runningValues == null || 0 >= runningValues.Count)
			{
				return flag;
			}
			if (runningValuesInGroup == null)
			{
				runningValuesInGroup = new List<string>();
			}
			if (previousValuesInGroup == null)
			{
				previousValuesInGroup = new List<string>();
			}
			if (cellRunningValues)
			{
				List<int> list = null;
				Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef = this.m_hierarchyDef.DataRegionDef;
				bool isColumn = this.m_hierarchyDef.IsColumn;
				RuntimeDataTablixGroupRootObj runtimeDataTablixGroupRootObj = this as RuntimeDataTablixGroupRootObj;
				List<int> list2;
				if (outermostStatics && ((runtimeDataTablixGroupRootObj != null && runtimeDataTablixGroupRootObj.InnerGroupings != null) || dataRegionDef.CurrentOuterGroupRoot == null))
				{
					if (isColumn)
					{
						list = this.m_hierarchyDef.GetCellIndexes();
						list2 = dataRegionDef.OutermostStaticRowIndexes;
					}
					else
					{
						list = dataRegionDef.OutermostStaticColumnIndexes;
						list2 = this.m_hierarchyDef.GetCellIndexes();
					}
				}
				else
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode hierarchyDef = dataRegionDef.CurrentOuterGroupRoot.Value().HierarchyDef;
					if (isColumn)
					{
						list = this.m_hierarchyDef.GetCellIndexes();
						if (outermostStatics)
						{
							list2 = dataRegionDef.OutermostStaticRowIndexes;
						}
						else
						{
							list2 = hierarchyDef.GetCellIndexes();
						}
					}
					else
					{
						list2 = this.m_hierarchyDef.GetCellIndexes();
						if (outermostStatics)
						{
							list = dataRegionDef.OutermostStaticColumnIndexes;
						}
						else
						{
							list = hierarchyDef.GetCellIndexes();
						}
					}
				}
				if (list2 == null || list == null)
				{
					goto IL_01ED;
				}
				using (List<int>.Enumerator enumerator = list2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int num = enumerator.Current;
						foreach (int num2 in list)
						{
							Cell cell = dataRegionDef.Rows[num].Cells[num2];
							if (cell.RunningValueIndexes != null)
							{
								flag = true;
								for (int i = 0; i < cell.RunningValueIndexes.Count; i++)
								{
									int num3 = cell.RunningValueIndexes[i];
									this.AddRunningValue(runningValues[num3], runningValuesInGroup, previousValuesInGroup, groupCollection);
								}
							}
						}
					}
					goto IL_01ED;
				}
			}
			flag = true;
			for (int j = 0; j < runningValues.Count; j++)
			{
				this.AddRunningValue(runningValues[j], runningValuesInGroup, previousValuesInGroup, groupCollection);
			}
			IL_01ED:
			if (previousValuesInGroup.Count == 0)
			{
				previousValuesInGroup = null;
			}
			if (runningValuesInGroup.Count == 0)
			{
				runningValuesInGroup = null;
			}
			return flag;
		}

		// Token: 0x06007D4D RID: 32077 RVA: 0x00204CD0 File Offset: 0x00202ED0
		private void AddRunningValue(Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo runningValue, List<string> runningValuesInGroup, List<string> previousValuesInGroup, Dictionary<string, IReference<RuntimeGroupRootObj>> groupCollection)
		{
			AggregatesImpl aggregatesImpl = this.m_odpContext.ReportObjectModel.AggregatesImpl;
			bool flag = runningValue.AggregateType == Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.Previous;
			List<string> list;
			if (flag)
			{
				list = previousValuesInGroup;
			}
			else
			{
				list = runningValuesInGroup;
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj dataAggregateObj = aggregatesImpl.GetAggregateObj(runningValue.Name);
			if (dataAggregateObj == null)
			{
				dataAggregateObj = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj(runningValue, this.m_odpContext);
				aggregatesImpl.Add(dataAggregateObj);
			}
			else if (flag && (runningValue.Scope == null || runningValue.IsScopedInEvaluationScope))
			{
				dataAggregateObj.Init();
			}
			if (runningValue.Scope != null)
			{
				IReference<RuntimeGroupRootObj> reference;
				if (groupCollection.TryGetValue(runningValue.Scope, out reference))
				{
					using (reference.PinValue())
					{
						reference.Value().AddScopedRunningValue(dataAggregateObj);
						goto IL_00B1;
					}
				}
				Global.Tracer.Assert(false, "RV with runtime scope escalation");
			}
			IL_00B1:
			if (!list.Contains(dataAggregateObj.Name))
			{
				list.Add(dataAggregateObj.Name);
			}
		}

		// Token: 0x06007D4E RID: 32078 RVA: 0x00204DB8 File Offset: 0x00202FB8
		internal override void CalculatePreviousAggregates()
		{
			if (this.m_previousValuesInGroup != null)
			{
				AggregatesImpl aggregatesImpl = this.m_odpContext.ReportObjectModel.AggregatesImpl;
				for (int i = 0; i < this.m_previousValuesInGroup.Count; i++)
				{
					string text = this.m_previousValuesInGroup[i];
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj aggregateObj = aggregatesImpl.GetAggregateObj(text);
					Global.Tracer.Assert(aggregateObj != null, "Missing expected previous aggregate: {0}", new object[] { text });
					aggregateObj.Update();
				}
			}
			if (this.m_outerScope != null && (this.m_outerDataAction & DataActions.PostSortAggregates) != DataActions.None)
			{
				using (this.m_outerScope.PinValue())
				{
					this.m_outerScope.Value().CalculatePreviousAggregates();
				}
			}
		}

		// Token: 0x06007D4F RID: 32079 RVA: 0x00204E7C File Offset: 0x0020307C
		public override void ReadRow(DataActions dataAction, ITraversalContext context)
		{
			if (DataActions.PostSortAggregates == dataAction && this.m_runningValuesInGroup != null)
			{
				AggregatesImpl aggregatesImpl = this.m_odpContext.ReportObjectModel.AggregatesImpl;
				for (int i = 0; i < this.m_runningValuesInGroup.Count; i++)
				{
					string text = this.m_runningValuesInGroup[i];
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj aggregateObj = aggregatesImpl.GetAggregateObj(text);
					Global.Tracer.Assert(aggregateObj != null, "Missing expected running value aggregate: {0}", new object[] { text });
					aggregateObj.Update();
				}
			}
			if (this.m_outerScope != null && (dataAction & this.m_outerDataAction) != DataActions.None)
			{
				using (this.m_outerScope.PinValue())
				{
					this.m_outerScope.Value().ReadRow(dataAction, context);
				}
			}
		}

		// Token: 0x06007D50 RID: 32080 RVA: 0x00204F44 File Offset: 0x00203144
		internal void CreateInstances(ScopeInstance parentInstance, IReference<RuntimeMemberObj>[] innerMembers, IReference<RuntimeDataTablixGroupLeafObj> innerGroupLeafRef)
		{
			CreateInstancesTraversalContext createInstancesTraversalContext = new CreateInstancesTraversalContext(parentInstance, innerMembers, innerGroupLeafRef);
			this.m_hierarchyDef.ResetInstancePathCascade();
			this.TraverseGroupOrSortTree(ProcessingStages.CreateGroupTree, createInstancesTraversalContext);
			if (this.m_detailDataRows != null)
			{
				this.m_detailDataRows.Dispose();
			}
			this.m_detailDataRows = null;
			this.m_detailUserSortTargetInfo = null;
		}

		// Token: 0x06007D51 RID: 32081 RVA: 0x00204F90 File Offset: 0x00203190
		protected void TraverseGroupOrSortTree(ProcessingStages operation, ITraversalContext traversalContext)
		{
			if (this.m_detailUserSortTargetInfo != null && this.m_groupingType != RuntimeGroupingObj.GroupingTypes.None)
			{
				this.m_detailUserSortTargetInfo.SortTree.Traverse(operation, this.GetDetailSortDirection(), traversalContext);
				return;
			}
			this.m_grouping.Traverse(operation, this.m_expression == null || this.m_expression.Direction, traversalContext);
		}

		// Token: 0x06007D52 RID: 32082 RVA: 0x00204FEC File Offset: 0x002031EC
		internal void TraverseLinkedGroupLeaves(ProcessingStages operation, bool ascending, ITraversalContext traversalContext)
		{
			if (null != this.m_firstChild)
			{
				using (this.m_firstChild.PinValue())
				{
					this.m_firstChild.Value().TraverseAllLeafNodes(operation, traversalContext);
				}
			}
		}

		// Token: 0x06007D53 RID: 32083 RVA: 0x00205044 File Offset: 0x00203244
		private bool GetDetailSortDirection()
		{
			int num = this.m_detailUserSortTargetInfo.SortFilterInfoIndices[0];
			return this.m_odpContext.RuntimeSortFilterInfo[num].Value().SortDirection;
		}

		// Token: 0x06007D54 RID: 32084 RVA: 0x00205080 File Offset: 0x00203280
		internal RuntimeGroupLeafObjReference CreateGroupLeaf()
		{
			RuntimeGroupLeafObj runtimeGroupLeafObj = null;
			Microsoft.ReportingServices.ReportProcessing.ObjectType objectType = base.ObjectType;
			if (objectType <= Microsoft.ReportingServices.ReportProcessing.ObjectType.CustomReportItem)
			{
				if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel && objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart && objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.CustomReportItem)
				{
					goto IL_007B;
				}
			}
			else
			{
				if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Tablix)
				{
					runtimeGroupLeafObj = new RuntimeTablixGroupLeafObj((RuntimeDataTablixGroupRootObjReference)base.SelfReference, base.ObjectType);
					goto IL_008B;
				}
				if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.MapDataRegion)
				{
					if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.DataShape)
					{
						goto IL_007B;
					}
					runtimeGroupLeafObj = new RuntimeDataShapeGroupLeafObj((RuntimeDataTablixGroupRootObjReference)base.SelfReference, base.ObjectType);
					goto IL_008B;
				}
			}
			runtimeGroupLeafObj = new RuntimeChartCriGroupLeafObj((RuntimeDataTablixGroupRootObjReference)base.SelfReference, base.ObjectType);
			goto IL_008B;
			IL_007B:
			Global.Tracer.Assert(false, "Invalid ObjectType");
			IL_008B:
			RuntimeGroupLeafObjReference runtimeGroupLeafObjReference = (RuntimeGroupLeafObjReference)runtimeGroupLeafObj.SelfReference;
			runtimeGroupLeafObjReference.UnPinValue();
			return runtimeGroupLeafObjReference;
		}

		// Token: 0x06007D55 RID: 32085 RVA: 0x0020512C File Offset: 0x0020332C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			writer.RegisterDeclaration(RuntimeGroupRootObj.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Filters)
				{
					if (memberName == MemberName.SortFilterExpressionScopeInfoIndices)
					{
						writer.Write(this.m_sortFilterExpressionScopeInfoIndices);
						continue;
					}
					switch (memberName)
					{
					case MemberName.OuterScope:
						writer.Write(this.m_outerScope);
						continue;
					case MemberName.NonCustomAggregates:
					case MemberName.CustomAggregates:
						break;
					case MemberName.DataAction:
						writer.WriteEnum((int)this.m_dataAction);
						continue;
					case MemberName.OuterDataAction:
						writer.WriteEnum((int)this.m_outerDataAction);
						continue;
					default:
						if (memberName == MemberName.Filters)
						{
							int num = scalabilityCache.StoreStaticReference(this.m_groupFilters);
							writer.Write(num);
							continue;
						}
						break;
					}
				}
				else if (memberName <= MemberName.HierarchyDef)
				{
					if (memberName == MemberName.SaveGroupExprValues)
					{
						writer.Write(this.m_saveGroupExprValues);
						continue;
					}
					switch (memberName)
					{
					case MemberName.RunningValuesInGroup:
						writer.WriteListOfPrimitives<string>(this.m_runningValuesInGroup);
						continue;
					case MemberName.PreviousValuesInGroup:
						writer.WriteListOfPrimitives<string>(this.m_previousValuesInGroup);
						continue;
					case MemberName.GroupCollection:
						writer.WriteStringRIFObjectDictionary<IReference<RuntimeGroupRootObj>>(this.m_groupCollection);
						continue;
					case MemberName.ProcessingStage:
						writer.WriteEnum((int)this.m_processingStage);
						continue;
					case MemberName.ScopedRunningValues:
						writer.WriteListOfPrimitives<string>(this.m_scopedRunningValues);
						continue;
					case MemberName.GroupingType:
						writer.WriteEnum((int)this.m_groupingType);
						continue;
					case MemberName.ParentExpression:
						writer.Write(this.m_parentExpression);
						continue;
					case MemberName.CurrentGroupExprValue:
						writer.Write(this.m_currentGroupExprValue);
						continue;
					case MemberName.BuiltinSortOverridden:
						writer.Write(this.m_builtinSortOverridden);
						continue;
					case MemberName.IsDetailGroup:
						writer.Write(this.m_isDetailGroup);
						continue;
					case MemberName.HierarchyDef:
					{
						int num2 = scalabilityCache.StoreStaticReference(this.m_hierarchyDef);
						writer.Write(num2);
						continue;
					}
					}
				}
				else
				{
					if (memberName == MemberName.DetailUserSortTargetInfo)
					{
						writer.Write(this.m_detailUserSortTargetInfo);
						continue;
					}
					if (memberName == MemberName.DetailRows)
					{
						writer.Write(this.m_detailDataRows);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007D56 RID: 32086 RVA: 0x00205380 File Offset: 0x00203580
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			reader.RegisterDeclaration(RuntimeGroupRootObj.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Filters)
				{
					if (memberName == MemberName.SortFilterExpressionScopeInfoIndices)
					{
						this.m_sortFilterExpressionScopeInfoIndices = reader.ReadInt32Array();
						continue;
					}
					switch (memberName)
					{
					case MemberName.OuterScope:
						this.m_outerScope = (IReference<IScope>)reader.ReadRIFObject();
						continue;
					case MemberName.NonCustomAggregates:
					case MemberName.CustomAggregates:
						break;
					case MemberName.DataAction:
						this.m_dataAction = (DataActions)reader.ReadEnum();
						continue;
					case MemberName.OuterDataAction:
						this.m_outerDataAction = (DataActions)reader.ReadEnum();
						continue;
					default:
						if (memberName == MemberName.Filters)
						{
							int num = reader.ReadInt32();
							this.m_groupFilters = (Filters)scalabilityCache.FetchStaticReference(num);
							continue;
						}
						break;
					}
				}
				else if (memberName <= MemberName.HierarchyDef)
				{
					if (memberName == MemberName.SaveGroupExprValues)
					{
						this.m_saveGroupExprValues = reader.ReadBoolean();
						continue;
					}
					switch (memberName)
					{
					case MemberName.RunningValuesInGroup:
						this.m_runningValuesInGroup = reader.ReadListOfPrimitives<string>();
						continue;
					case MemberName.PreviousValuesInGroup:
						this.m_previousValuesInGroup = reader.ReadListOfPrimitives<string>();
						continue;
					case MemberName.GroupCollection:
						this.m_groupCollection = reader.ReadStringRIFObjectDictionary<IReference<RuntimeGroupRootObj>>();
						continue;
					case MemberName.ProcessingStage:
						this.m_processingStage = (ProcessingStages)reader.ReadEnum();
						continue;
					case MemberName.ScopedRunningValues:
						this.m_scopedRunningValues = reader.ReadListOfPrimitives<string>();
						continue;
					case MemberName.GroupingType:
						this.m_groupingType = (RuntimeGroupingObj.GroupingTypes)reader.ReadEnum();
						continue;
					case MemberName.ParentExpression:
						this.m_parentExpression = (RuntimeExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.CurrentGroupExprValue:
						this.m_currentGroupExprValue = reader.ReadVariant();
						continue;
					case MemberName.BuiltinSortOverridden:
						this.m_builtinSortOverridden = reader.ReadBooleanArray();
						continue;
					case MemberName.IsDetailGroup:
						this.m_isDetailGroup = reader.ReadBoolean();
						continue;
					case MemberName.HierarchyDef:
					{
						int num2 = reader.ReadInt32();
						this.m_hierarchyDef = (Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode)scalabilityCache.FetchStaticReference(num2);
						continue;
					}
					}
				}
				else
				{
					if (memberName == MemberName.DetailUserSortTargetInfo)
					{
						this.m_detailUserSortTargetInfo = (RuntimeUserSortTargetInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.DetailRows)
					{
						this.m_detailDataRows = reader.ReadRIFObject<ScalableList<DataFieldRow>>();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007D57 RID: 32087 RVA: 0x002055ED File Offset: 0x002037ED
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007D58 RID: 32088 RVA: 0x002055EF File Offset: 0x002037EF
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupRootObj;
		}

		// Token: 0x06007D59 RID: 32089 RVA: 0x002055F8 File Offset: 0x002037F8
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeGroupRootObj.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupRootObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupObj, new List<MemberInfo>
				{
					new MemberInfo(MemberName.HierarchyDef, Token.Int32),
					new MemberInfo(MemberName.OuterScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IScopeReference),
					new MemberInfo(MemberName.ProcessingStage, Token.Enum),
					new MemberInfo(MemberName.ScopedRunningValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.String),
					new MemberInfo(MemberName.RunningValuesInGroup, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.String),
					new MemberInfo(MemberName.PreviousValuesInGroup, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.String),
					new MemberInfo(MemberName.GroupCollection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringRIFObjectDictionary, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupRootObjReference),
					new MemberInfo(MemberName.DataAction, Token.Enum),
					new MemberInfo(MemberName.OuterDataAction, Token.Enum),
					new MemberInfo(MemberName.GroupingType, Token.Enum),
					new MemberInfo(MemberName.Filters, Token.Int32),
					new MemberInfo(MemberName.ParentExpression, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeExpressionInfo),
					new MemberInfo(MemberName.CurrentGroupExprValue, Token.Object),
					new MemberInfo(MemberName.SaveGroupExprValues, Token.Boolean),
					new MemberInfo(MemberName.SortFilterExpressionScopeInfoIndices, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Int32),
					new MemberInfo(MemberName.BuiltinSortOverridden, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Boolean),
					new MemberInfo(MemberName.IsDetailGroup, Token.Boolean),
					new MemberInfo(MemberName.DetailUserSortTargetInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeUserSortTargetInfo),
					new MemberInfo(MemberName.DetailRows, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataFieldRow)
				});
			}
			return RuntimeGroupRootObj.m_declaration;
		}

		// Token: 0x06007D5A RID: 32090 RVA: 0x00205799 File Offset: 0x00203999
		public override void SetReference(IReference selfRef)
		{
			base.SetReference(selfRef);
			this.m_hierarchyRoot = (RuntimeGroupRootObjReference)selfRef;
		}

		// Token: 0x170028DD RID: 10461
		// (get) Token: 0x06007D5B RID: 32091 RVA: 0x002057B0 File Offset: 0x002039B0
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.ReferenceSize + ItemSizes.SizeOf(this.m_outerScope) + 4 + ItemSizes.SizeOf(this.m_scopedRunningValues) + ItemSizes.SizeOf(this.m_runningValuesInGroup) + ItemSizes.ReferenceSize + ItemSizes.SizeOf<string, IReference<RuntimeGroupRootObj>>(this.m_groupCollection) + 4 + 4 + 4 + ItemSizes.ReferenceSize + ItemSizes.SizeOf(this.m_parentExpression) + ItemSizes.SizeOf(this.m_currentGroupExprValue) + 1 + ItemSizes.SizeOf(this.m_sortFilterExpressionScopeInfoIndices) + ItemSizes.SizeOf(this.m_builtinSortOverridden) + ItemSizes.SizeOf(this.m_detailUserSortTargetInfo) + ItemSizes.SizeOf<DataFieldRow>(this.m_detailDataRows) + 1;
			}
		}

		// Token: 0x04003DDA RID: 15834
		protected Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode m_hierarchyDef;

		// Token: 0x04003DDB RID: 15835
		protected IReference<IScope> m_outerScope;

		// Token: 0x04003DDC RID: 15836
		private ProcessingStages m_processingStage = ProcessingStages.Grouping;

		// Token: 0x04003DDD RID: 15837
		protected List<string> m_scopedRunningValues;

		// Token: 0x04003DDE RID: 15838
		protected List<string> m_runningValuesInGroup;

		// Token: 0x04003DDF RID: 15839
		protected List<string> m_previousValuesInGroup;

		// Token: 0x04003DE0 RID: 15840
		protected Dictionary<string, IReference<RuntimeGroupRootObj>> m_groupCollection;

		// Token: 0x04003DE1 RID: 15841
		protected DataActions m_dataAction;

		// Token: 0x04003DE2 RID: 15842
		protected DataActions m_outerDataAction;

		// Token: 0x04003DE3 RID: 15843
		protected RuntimeGroupingObj.GroupingTypes m_groupingType;

		// Token: 0x04003DE4 RID: 15844
		[Reference]
		protected Filters m_groupFilters;

		// Token: 0x04003DE5 RID: 15845
		protected RuntimeExpressionInfo m_parentExpression;

		// Token: 0x04003DE6 RID: 15846
		protected object m_currentGroupExprValue;

		// Token: 0x04003DE7 RID: 15847
		protected bool m_saveGroupExprValues = true;

		// Token: 0x04003DE8 RID: 15848
		protected int[] m_sortFilterExpressionScopeInfoIndices;

		// Token: 0x04003DE9 RID: 15849
		private bool[] m_builtinSortOverridden;

		// Token: 0x04003DEA RID: 15850
		protected bool m_isDetailGroup;

		// Token: 0x04003DEB RID: 15851
		protected RuntimeUserSortTargetInfo m_detailUserSortTargetInfo;

		// Token: 0x04003DEC RID: 15852
		protected ScalableList<DataFieldRow> m_detailDataRows;

		// Token: 0x04003DED RID: 15853
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeGroupRootObj.GetDeclaration();
	}
}

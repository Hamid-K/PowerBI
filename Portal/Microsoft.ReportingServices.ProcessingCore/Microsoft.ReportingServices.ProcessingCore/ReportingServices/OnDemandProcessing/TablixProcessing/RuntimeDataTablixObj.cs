using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008DA RID: 2266
	[PersistedWithinRequestOnly]
	internal abstract class RuntimeDataTablixObj : RuntimeRDLDataRegionObj, IOnDemandMemberOwnerInstance, IOnDemandScopeInstance, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007BCE RID: 31694 RVA: 0x001FCCEE File Offset: 0x001FAEEE
		internal RuntimeDataTablixObj()
		{
		}

		// Token: 0x06007BCF RID: 31695 RVA: 0x001FCCF8 File Offset: 0x001FAEF8
		internal RuntimeDataTablixObj(IReference<IScope> outerScope, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataTablixDef, ref DataActions dataAction, OnDemandProcessingContext odpContext, bool onePassProcess, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(outerScope, dataTablixDef, ref dataAction, odpContext, onePassProcess, dataTablixDef.RunningValues, objectType, outerScope.Value().Depth + 1)
		{
			bool flag;
			DataActions dataActions;
			this.ConstructorHelper(ref dataAction, onePassProcess, out flag, out dataActions);
			this.m_innerDataAction = dataActions;
			DataActions dataActions2 = base.HandleSortFilterEvent();
			this.ConstructRuntimeStructure(ref dataActions, onePassProcess);
			this.HandleDataAction(flag, dataActions, dataActions2);
			this.m_odpContext.CreatedScopeInstance(this.m_dataRegionDef);
			this.m_scopeInstanceNumber = RuntimeDataRegionObj.AssignScopeInstanceNumber(this.m_dataRegionDef.DataScopeInfo);
		}

		// Token: 0x17002894 RID: 10388
		// (get) Token: 0x06007BD0 RID: 31696 RVA: 0x001FCD7C File Offset: 0x001FAF7C
		internal int[] OuterGroupingCounters
		{
			get
			{
				return this.m_outerGroupingCounters;
			}
		}

		// Token: 0x17002895 RID: 10389
		// (get) Token: 0x06007BD1 RID: 31697 RVA: 0x001FCD84 File Offset: 0x001FAF84
		internal List<IReference<RuntimeDataTablixGroupLeafObj>> InnerGroupsWithCellsForOuterPeerGroupProcessing
		{
			get
			{
				return this.m_innerGroupsWithCellsForOuterPeerGroupProcessing;
			}
		}

		// Token: 0x06007BD2 RID: 31698 RVA: 0x001FCD8C File Offset: 0x001FAF8C
		protected void ConstructorHelper(ref DataActions dataAction, bool onePassProcess, out bool handleMyDataAction, out DataActions innerDataAction)
		{
			innerDataAction = this.m_dataAction;
			handleMyDataAction = false;
			if (onePassProcess)
			{
				if (this.m_dataRegionDef.RunningValues != null && 0 < this.m_dataRegionDef.RunningValues.Count)
				{
					RuntimeDataRegionObj.CreateAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo>(this.m_odpContext, this.m_dataRegionDef.RunningValues, ref this.m_nonCustomAggregates);
				}
				RuntimeDataRegionObj.CreateAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, this.m_dataRegionDef.PostSortAggregates, ref this.m_nonCustomAggregates);
				RuntimeDataRegionObj.CreateAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, this.m_dataRegionDef.CellPostSortAggregates, ref this.m_nonCustomAggregates);
			}
			else
			{
				if (this.m_dataRegionDef.RunningValues != null && 0 < this.m_dataRegionDef.RunningValues.Count)
				{
					this.m_dataAction |= DataActions.PostSortAggregates;
				}
				if (this.m_dataRegionDef.PostSortAggregates != null && this.m_dataRegionDef.PostSortAggregates.Count != 0)
				{
					RuntimeDataRegionObj.CreateAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, this.m_dataRegionDef.PostSortAggregates, ref this.m_postSortAggregates);
					this.m_dataAction |= DataActions.PostSortAggregates;
					handleMyDataAction = true;
				}
				if (this.m_dataRegionDef.DataScopeInfo != null)
				{
					DataScopeInfo dataScopeInfo = this.m_dataRegionDef.DataScopeInfo;
					if (dataScopeInfo.PostSortAggregatesOfAggregates != null && !dataScopeInfo.PostSortAggregatesOfAggregates.IsEmpty)
					{
						RuntimeDataRegionObj.CreateAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, dataScopeInfo.PostSortAggregatesOfAggregates, ref this.m_postSortAggregatesOfAggregates);
					}
					if (dataScopeInfo.HasAggregatesToUpdateAtRowScope)
					{
						this.m_dataAction |= DataActions.AggregatesOfAggregates;
						handleMyDataAction = true;
					}
				}
				if (handleMyDataAction)
				{
					innerDataAction = DataActions.None;
				}
				else
				{
					innerDataAction = this.m_dataAction;
				}
			}
			this.m_inDataRowSortPhase = this.m_dataRegionDef.Sorting != null && this.m_dataRegionDef.Sorting.ShouldApplySorting;
			if (this.m_inDataRowSortPhase)
			{
				this.m_sortedDataRowTree = new BTree(this, this.m_odpContext, this.m_depth);
				this.m_dataRowSortExpression = new RuntimeExpressionInfo(this.m_dataRegionDef.Sorting.SortExpressions, this.m_dataRegionDef.Sorting.ExprHost, this.m_dataRegionDef.Sorting.SortDirections, 0);
				this.m_odpContext.AddSpecialDataRowSort((IReference<IDataRowSortOwner>)base.SelfReference);
			}
			this.m_dataRegionDef.ResetInstanceIndexes();
			this.m_outerGroupingCounters = new int[this.m_dataRegionDef.OuterGroupingDynamicMemberCount];
			for (int i = 0; i < this.m_outerGroupingCounters.Length; i++)
			{
				this.m_outerGroupingCounters[i] = -1;
			}
		}

		// Token: 0x06007BD3 RID: 31699 RVA: 0x001FCFE4 File Offset: 0x001FB1E4
		protected override void ConstructRuntimeStructure(ref DataActions innerDataAction, bool onePassProcess)
		{
			HierarchyNodeList hierarchyNodeList;
			HierarchyNodeList hierarchyNodeList2;
			this.CreateRuntimeMemberObjects(this.m_dataRegionDef.OuterMembers, this.m_dataRegionDef.InnerMembers, out hierarchyNodeList, out hierarchyNodeList2, ref innerDataAction);
			if (((hierarchyNodeList != null && hierarchyNodeList.Count != 0) || (hierarchyNodeList2 != null && hierarchyNodeList2.Count != 0)) && base.DataRegionDef.OutermostStaticColumnIndexes == null && base.DataRegionDef.OutermostStaticRowIndexes == null)
			{
				List<int> list = ((hierarchyNodeList2 != null) ? hierarchyNodeList2.LeafCellIndexes : null);
				List<int> list2 = ((hierarchyNodeList != null) ? hierarchyNodeList.LeafCellIndexes : null);
				if (base.DataRegionDef.ProcessingInnerGrouping == Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.ProcessingInnerGroupings.Column)
				{
					base.DataRegionDef.OutermostStaticColumnIndexes = list;
					base.DataRegionDef.OutermostStaticRowIndexes = list2;
					return;
				}
				base.DataRegionDef.OutermostStaticColumnIndexes = list2;
				base.DataRegionDef.OutermostStaticRowIndexes = list;
			}
		}

		// Token: 0x06007BD4 RID: 31700 RVA: 0x001FD0A4 File Offset: 0x001FB2A4
		private void CreateRuntimeMemberObjects(HierarchyNodeList outerMembers, HierarchyNodeList innerMembers, out HierarchyNodeList outerTopLevelStaticMembers, out HierarchyNodeList innerTopLevelStaticMembers, ref DataActions innerDataAction)
		{
			bool flag = false;
			HierarchyNodeList hierarchyNodeList = null;
			outerTopLevelStaticMembers = null;
			if (outerMembers != null)
			{
				flag = outerMembers.HasStaticLeafMembers;
				hierarchyNodeList = outerMembers.DynamicMembersAtScope;
				outerTopLevelStaticMembers = outerMembers.StaticMembersInSameScope;
			}
			bool flag2 = false;
			HierarchyNodeList hierarchyNodeList2 = null;
			innerTopLevelStaticMembers = null;
			if (innerMembers != null)
			{
				flag2 = innerMembers.HasStaticLeafMembers;
				hierarchyNodeList2 = innerMembers.DynamicMembersAtScope;
				innerTopLevelStaticMembers = innerMembers.StaticMembersInSameScope;
			}
			DataActions dataActions = DataActions.None;
			this.CreateTopLevelRuntimeGroupings(ref dataActions, ref this.m_innerGroupings, innerTopLevelStaticMembers, hierarchyNodeList2, null, flag);
			Global.Tracer.Assert(this.m_innerGroupings != null && this.m_innerGroupings.Length >= 1, "(null != m_innerGroupings && m_innerGroupings.Length >= 1)");
			this.CreateTopLevelRuntimeGroupings(ref innerDataAction, ref this.m_outerGroupings, outerTopLevelStaticMembers, hierarchyNodeList, this.m_innerGroupings, flag2);
		}

		// Token: 0x06007BD5 RID: 31701 RVA: 0x001FD14C File Offset: 0x001FB34C
		private void CreateTopLevelRuntimeGroupings(ref DataActions groupingDataAction, ref IReference<RuntimeMemberObj>[] groupings, HierarchyNodeList topLevelStaticMembers, HierarchyNodeList topLevelDynamicMembers, IReference<RuntimeMemberObj>[] innerGroupings, bool hasOppositeStaticLeafMembers)
		{
			int num = ((topLevelDynamicMembers != null) ? topLevelDynamicMembers.Count : 0);
			groupings = new IReference<RuntimeMemberObj>[Math.Max(1, num)];
			if (num == 0)
			{
				IReference<RuntimeMemberObj> reference = RuntimeDataTablixMemberObj.CreateRuntimeMemberObject(this.m_selfReference, null, ref groupingDataAction, this.m_odpContext, innerGroupings, topLevelStaticMembers, hasOppositeStaticLeafMembers, 0, base.ObjectType);
				groupings[0] = reference;
				return;
			}
			for (int i = 0; i < num; i++)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode reportHierarchyNode = topLevelDynamicMembers[i];
				IReference<RuntimeMemberObj> reference2 = RuntimeDataTablixMemberObj.CreateRuntimeMemberObject(this.m_selfReference, reportHierarchyNode, ref groupingDataAction, this.m_odpContext, innerGroupings, (i == 0) ? topLevelStaticMembers : null, hasOppositeStaticLeafMembers, 0, base.ObjectType);
				groupings[i] = reference2;
			}
		}

		// Token: 0x06007BD6 RID: 31702 RVA: 0x001FD1E4 File Offset: 0x001FB3E4
		protected void HandleDataAction(bool handleMyDataAction, DataActions innerDataAction, DataActions userSortDataAction)
		{
			if (!handleMyDataAction)
			{
				this.m_dataAction = innerDataAction;
			}
			this.m_dataAction |= userSortDataAction;
			if (this.m_dataAction != DataActions.None)
			{
				this.m_dataRows = new ScalableList<DataFieldRow>(this.m_depth + 1, this.m_odpContext.TablixProcessingScalabilityCache, 30);
			}
		}

		// Token: 0x06007BD7 RID: 31703 RVA: 0x001FD234 File Offset: 0x001FB434
		protected override void SendToInner()
		{
			bool peerOuterGroupProcessing = this.m_odpContext.PeerOuterGroupProcessing;
			this.m_dataRegionDef.RuntimeDataRegionObj = this.m_selfReference;
			int num = ((this.m_outerGroupings != null) ? this.m_outerGroupings.Length : 0);
			AggregateRowInfo aggregateRowInfo = AggregateRowInfo.CreateAndSaveAggregateInfo(this.m_odpContext);
			if (this.m_dataRegionDef.IsMatrixIDC)
			{
				if (this.m_innerGroupings != null)
				{
					this.ProcessInnerHierarchy(aggregateRowInfo);
				}
				for (int i = 0; i < num; i++)
				{
					this.ProcessOuterHierarchy(aggregateRowInfo, i);
				}
				return;
			}
			if (num == 0)
			{
				if (this.m_innerGroupings != null)
				{
					this.ProcessInnerHierarchy(aggregateRowInfo);
				}
			}
			else
			{
				if (this.m_innerGroupsWithCellsForOuterPeerGroupProcessing == null || !peerOuterGroupProcessing)
				{
					this.m_innerGroupsWithCellsForOuterPeerGroupProcessing = new List<IReference<RuntimeDataTablixGroupLeafObj>>();
				}
				for (int j = 0; j < num; j++)
				{
					this.ProcessOuterHierarchy(aggregateRowInfo, j);
					if (this.m_innerGroupings != null)
					{
						if (j == 0)
						{
							this.ProcessInnerHierarchy(aggregateRowInfo);
						}
						else
						{
							foreach (IReference<RuntimeDataTablixGroupLeafObj> reference in this.m_innerGroupsWithCellsForOuterPeerGroupProcessing)
							{
								using (reference.PinValue())
								{
									reference.Value().PeerOuterGroupProcessCells();
								}
								aggregateRowInfo.RestoreAggregateInfo(this.m_odpContext);
							}
						}
					}
				}
			}
			this.m_odpContext.PeerOuterGroupProcessing = peerOuterGroupProcessing;
		}

		// Token: 0x06007BD8 RID: 31704 RVA: 0x001FD39C File Offset: 0x001FB59C
		private void ProcessInnerHierarchy(AggregateRowInfo aggregateRowInfo)
		{
			for (int i = 0; i < this.m_innerGroupings.Length; i++)
			{
				IReference<RuntimeMemberObj> reference = this.m_innerGroupings[i];
				using (reference.PinValue())
				{
					reference.Value().NextRow(false, this.m_odpContext);
				}
				aggregateRowInfo.RestoreAggregateInfo(this.m_odpContext);
			}
		}

		// Token: 0x06007BD9 RID: 31705 RVA: 0x001FD408 File Offset: 0x001FB608
		private void ProcessOuterHierarchy(AggregateRowInfo aggregateRowInfo, int outerGroupingIndex)
		{
			this.m_odpContext.PeerOuterGroupProcessing = outerGroupingIndex != 0;
			this.m_dataRegionDef.ResetOuterGroupingIndexesForOuterPeerGroup(0);
			this.m_dataRegionDef.ResetOuterGroupingAggregateRowInfo();
			this.m_dataRegionDef.SetDataTablixAggregateRowInfo(aggregateRowInfo);
			IReference<RuntimeMemberObj> reference = this.m_outerGroupings[outerGroupingIndex];
			using (reference.PinValue())
			{
				reference.Value().NextRow(true, this.m_odpContext);
			}
			aggregateRowInfo.RestoreAggregateInfo(this.m_odpContext);
		}

		// Token: 0x06007BDA RID: 31706 RVA: 0x001FD494 File Offset: 0x001FB694
		internal override bool SortAndFilter(AggregateUpdateContext aggContext)
		{
			this.SetupEnvironment();
			if (this.m_userSortTargetInfo != null)
			{
				this.m_userSortTargetInfo.EnterProcessUserSortPhase(this.m_odpContext);
			}
			bool flag = base.DataRegionDef.ProcessingInnerGrouping == Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.ProcessingInnerGroupings.Column;
			IReference<RuntimeMemberObj>[] array = (flag ? this.m_outerGroupings : this.m_innerGroupings);
			IReference<RuntimeMemberObj>[] array2 = (flag ? this.m_innerGroupings : this.m_outerGroupings);
			int rowDomainScopeCount = base.DataRegionDef.RowDomainScopeCount;
			int columnDomainScopeCount = base.DataRegionDef.ColumnDomainScopeCount;
			DomainScopeContext domainScopeContext = base.OdpContext.DomainScopeContext;
			AggregateUpdateQueue aggregateUpdateQueue = null;
			if (this.m_odpContext.HasSecondPassOperation(SecondPassOperations.FilteringOrAggregatesOrDomainScope))
			{
				aggregateUpdateQueue = RuntimeDataRegionObj.AggregateOfAggregatesStart(aggContext, this, this.m_dataRegionDef.DataScopeInfo, this.m_aggregatesOfAggregates, AggregateUpdateFlags.Both, false);
				if (rowDomainScopeCount > 0)
				{
					domainScopeContext.AddDomainScopes(array, array.Length - rowDomainScopeCount);
				}
				if (columnDomainScopeCount > 0)
				{
					domainScopeContext.AddDomainScopes(array2, array2.Length - columnDomainScopeCount);
				}
			}
			this.Traverse(ProcessingStages.SortAndFilter, aggContext);
			base.SortAndFilter(aggContext);
			if (this.m_odpContext.HasSecondPassOperation(SecondPassOperations.FilteringOrAggregatesOrDomainScope))
			{
				RuntimeDataRegionObj.AggregatesOfAggregatesEnd(this, aggContext, aggregateUpdateQueue, this.m_dataRegionDef.DataScopeInfo, this.m_aggregatesOfAggregates, true);
				if (rowDomainScopeCount > 0)
				{
					domainScopeContext.RemoveDomainScopes(array, array.Length - rowDomainScopeCount);
				}
				if (columnDomainScopeCount > 0)
				{
					domainScopeContext.RemoveDomainScopes(array2, array2.Length - columnDomainScopeCount);
				}
			}
			if (this.m_userSortTargetInfo != null)
			{
				this.m_userSortTargetInfo.LeaveProcessUserSortPhase(this.m_odpContext);
			}
			return true;
		}

		// Token: 0x06007BDB RID: 31707 RVA: 0x001FD5DA File Offset: 0x001FB7DA
		public override void UpdateAggregates(AggregateUpdateContext context)
		{
			this.SetupEnvironment();
			if (RuntimeDataRegionObj.UpdateAggregatesAtScope(context, this, this.m_dataRegionDef.DataScopeInfo, AggregateUpdateFlags.Both, false))
			{
				this.Traverse(ProcessingStages.UpdateAggregates, context);
			}
		}

		// Token: 0x06007BDC RID: 31708 RVA: 0x001FD600 File Offset: 0x001FB800
		protected virtual void Traverse(ProcessingStages operation, ITraversalContext context)
		{
			bool flag = base.DataRegionDef.ProcessingInnerGrouping == Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.ProcessingInnerGroupings.Column;
			IReference<RuntimeMemberObj>[] array = (flag ? this.m_outerGroupings : this.m_innerGroupings);
			IReference<RuntimeMemberObj>[] array2 = (flag ? this.m_innerGroupings : this.m_outerGroupings);
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					this.TraverseMember(array[i], operation, context);
				}
			}
			if (array2 != null)
			{
				for (int j = 0; j < array2.Length; j++)
				{
					this.TraverseMember(array2[j], operation, context);
				}
			}
		}

		// Token: 0x06007BDD RID: 31709 RVA: 0x001FD678 File Offset: 0x001FB878
		private void TraverseMember(IReference<RuntimeMemberObj> memberRef, ProcessingStages operation, ITraversalContext context)
		{
			using (memberRef.PinValue())
			{
				if (operation != ProcessingStages.SortAndFilter)
				{
					if (operation != ProcessingStages.UpdateAggregates)
					{
						Global.Tracer.Assert(false, "Unknown ProcessingStage in TraverseMember");
					}
					else
					{
						memberRef.Value().UpdateAggregates((AggregateUpdateContext)context);
					}
				}
				else
				{
					memberRef.Value().SortAndFilter((AggregateUpdateContext)context);
				}
			}
		}

		// Token: 0x06007BDE RID: 31710 RVA: 0x001FD6EC File Offset: 0x001FB8EC
		internal override void CalculateRunningValues(Dictionary<string, IReference<RuntimeGroupRootObj>> groupCol, IReference<RuntimeGroupRootObj> lastGroup, AggregateUpdateContext aggContext)
		{
			if (this.m_dataRegionDef.RunningValues != null && this.m_runningValues == null && this.m_previousValues == null)
			{
				RuntimeDataTablixObj.AddRunningValues(this.m_odpContext, this.m_dataRegionDef.RunningValues, ref this.m_runningValues, ref this.m_previousValues, groupCol, lastGroup);
			}
			if (this.m_dataRegionDef.DataScopeInfo != null)
			{
				List<string> list = null;
				List<string> list2 = null;
				RuntimeDataTablixObj.AddRunningValues(this.m_odpContext, this.m_dataRegionDef.DataScopeInfo.RunningValuesOfAggregates, ref list, ref list2, groupCol, lastGroup);
			}
			bool flag = this.m_dataRows != null && FlagUtils.HasFlag(this.m_dataAction, DataActions.PostSortAggregates);
			AggregateUpdateQueue aggregateUpdateQueue = RuntimeDataRegionObj.AggregateOfAggregatesStart(aggContext, this, this.m_dataRegionDef.DataScopeInfo, this.m_postSortAggregatesOfAggregates, flag ? AggregateUpdateFlags.ScopedAggregates : AggregateUpdateFlags.Both, true);
			if (flag)
			{
				DataActions dataActions = DataActions.PostSortAggregates;
				if (aggContext.LastScopeNeedsRowAggregateProcessing())
				{
					dataActions |= DataActions.PostSortAggregatesOfAggregates;
				}
				base.ReadRows(dataActions, aggContext);
				this.m_dataRows = null;
			}
			int num = ((this.m_outerGroupings != null) ? this.m_outerGroupings.Length : 0);
			if (num == 0)
			{
				if (this.m_innerGroupings != null)
				{
					for (int i = 0; i < this.m_innerGroupings.Length; i++)
					{
						IReference<RuntimeMemberObj> reference = this.m_innerGroupings[i];
						using (reference.PinValue())
						{
							reference.Value().CalculateRunningValues(groupCol, lastGroup, aggContext);
						}
					}
				}
			}
			else
			{
				for (int j = 0; j < num; j++)
				{
					IReference<RuntimeMemberObj> reference2 = this.m_outerGroupings[j];
					bool flag2;
					using (reference2.PinValue())
					{
						RuntimeMemberObj runtimeMemberObj = reference2.Value();
						runtimeMemberObj.CalculateRunningValues(groupCol, lastGroup, aggContext);
						flag2 = runtimeMemberObj.GroupRoot == null;
					}
					if (flag2 && this.m_innerGroupings != null)
					{
						for (int k = 0; k < this.m_innerGroupings.Length; k++)
						{
							IReference<RuntimeMemberObj> reference3 = this.m_innerGroupings[k];
							using (reference3.PinValue())
							{
								RuntimeMemberObj runtimeMemberObj2 = reference3.Value();
								runtimeMemberObj2.PrepareCalculateRunningValues();
								runtimeMemberObj2.CalculateRunningValues(groupCol, lastGroup, aggContext);
							}
						}
					}
				}
			}
			this.CalculateRunningValuesForTopLevelStaticContents(groupCol, lastGroup, aggContext);
			RuntimeDataRegionObj.AggregatesOfAggregatesEnd(this, aggContext, aggregateUpdateQueue, this.m_dataRegionDef.DataScopeInfo, this.m_postSortAggregatesOfAggregates, true);
			this.CalculateDRPreviousAggregates();
			RuntimeRICollection.StoreRunningValues(this.m_odpContext.ReportObjectModel.AggregatesImpl, this.m_dataRegionDef.RunningValues, ref this.m_runningValueValues);
			if (this.m_dataRegionDef.DataScopeInfo != null)
			{
				RuntimeRICollection.StoreRunningValues(this.m_odpContext.ReportObjectModel.AggregatesImpl, this.m_dataRegionDef.DataScopeInfo.RunningValuesOfAggregates, ref this.m_runningValueOfAggregateValues);
			}
		}

		// Token: 0x06007BDF RID: 31711 RVA: 0x001FD998 File Offset: 0x001FBB98
		protected virtual void CalculateRunningValuesForTopLevelStaticContents(Dictionary<string, IReference<RuntimeGroupRootObj>> groupCol, IReference<RuntimeGroupRootObj> lastGroup, AggregateUpdateContext aggContext)
		{
		}

		// Token: 0x06007BE0 RID: 31712 RVA: 0x001FD99C File Offset: 0x001FBB9C
		internal static void AddRunningValues(OnDemandProcessingContext odpContext, List<Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo> runningValues, ref List<string> runningValuesInGroup, ref List<string> previousValuesInGroup, Dictionary<string, IReference<RuntimeGroupRootObj>> groupCollection, IReference<RuntimeGroupRootObj> lastGroup)
		{
			if (runningValues == null || 0 >= runningValues.Count)
			{
				return;
			}
			if (runningValuesInGroup == null)
			{
				runningValuesInGroup = new List<string>();
			}
			if (previousValuesInGroup == null)
			{
				previousValuesInGroup = new List<string>();
			}
			AggregatesImpl aggregatesImpl = odpContext.ReportObjectModel.AggregatesImpl;
			for (int i = 0; i < runningValues.Count; i++)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo runningValueInfo = runningValues[i];
				Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj dataAggregateObj = aggregatesImpl.GetAggregateObj(runningValueInfo.Name);
				if (dataAggregateObj == null)
				{
					dataAggregateObj = new Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj(runningValueInfo, odpContext);
					aggregatesImpl.Add(dataAggregateObj);
				}
				if (runningValueInfo.Scope != null)
				{
					IReference<RuntimeGroupRootObj> reference;
					if (groupCollection.TryGetValue(runningValueInfo.Scope, out reference))
					{
						using (reference.PinValue())
						{
							reference.Value().AddScopedRunningValue(dataAggregateObj);
							goto IL_00A5;
						}
					}
					Global.Tracer.Assert(false);
				}
				IL_00A5:
				if (runningValueInfo.AggregateType == Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo.AggregateTypes.Previous)
				{
					previousValuesInGroup.Add(dataAggregateObj.Name);
				}
				else
				{
					runningValuesInGroup.Add(dataAggregateObj.Name);
				}
			}
		}

		// Token: 0x06007BE1 RID: 31713 RVA: 0x001FDA94 File Offset: 0x001FBC94
		internal static void UpdateRunningValues(OnDemandProcessingContext odpContext, List<string> runningValueNames)
		{
			AggregatesImpl aggregatesImpl = odpContext.ReportObjectModel.AggregatesImpl;
			for (int i = 0; i < runningValueNames.Count; i++)
			{
				string text = runningValueNames[i];
				aggregatesImpl.GetAggregateObj(text).Update();
			}
		}

		// Token: 0x06007BE2 RID: 31714 RVA: 0x001FDAD2 File Offset: 0x001FBCD2
		internal static void SaveData(ScalableList<DataFieldRow> dataRows, OnDemandProcessingContext odpContext)
		{
			Global.Tracer.Assert(dataRows != null, "(null != dataRows)");
			dataRows.Add(RuntimeDataTablixObj.SaveData(odpContext));
		}

		// Token: 0x06007BE3 RID: 31715 RVA: 0x001FDAF3 File Offset: 0x001FBCF3
		internal static DataFieldRow SaveData(OnDemandProcessingContext odpContext)
		{
			return new DataFieldRow(odpContext.ReportObjectModel.FieldsImpl, true);
		}

		// Token: 0x06007BE4 RID: 31716 RVA: 0x001FDB08 File Offset: 0x001FBD08
		internal override void CalculatePreviousAggregates()
		{
			if (this.m_outerScope != null && (DataActions.PostSortAggregates & this.m_outerDataAction) != DataActions.None)
			{
				using (this.m_outerScope.PinValue())
				{
					this.m_outerScope.Value().CalculatePreviousAggregates();
				}
			}
		}

		// Token: 0x06007BE5 RID: 31717 RVA: 0x001FDB60 File Offset: 0x001FBD60
		private void CalculateDRPreviousAggregates()
		{
			this.SetupEnvironment();
			if (this.m_previousValues != null)
			{
				AggregatesImpl aggregatesImpl = this.m_odpContext.ReportObjectModel.AggregatesImpl;
				for (int i = 0; i < this.m_previousValues.Count; i++)
				{
					string text = this.m_previousValues[i];
					Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj aggregateObj = aggregatesImpl.GetAggregateObj(text);
					Global.Tracer.Assert(aggregateObj != null, "Missing expected previous aggregate: {0}", new object[] { text });
					aggregateObj.Update();
				}
			}
		}

		// Token: 0x06007BE6 RID: 31718 RVA: 0x001FDBDC File Offset: 0x001FBDDC
		public override void ReadRow(DataActions dataAction, ITraversalContext context)
		{
			if (DataActions.UserSort == dataAction)
			{
				RuntimeDataRegionObj.CommonFirstRow(this.m_odpContext, ref this.m_firstRowIsAggregate, ref this.m_firstRow);
				base.CommonNextRow(this.m_dataRows);
				return;
			}
			if (DataActions.AggregatesOfAggregates == dataAction)
			{
				((AggregateUpdateContext)context).UpdateAggregatesForRow();
				return;
			}
			if (FlagUtils.HasFlag(dataAction, DataActions.PostSortAggregatesOfAggregates))
			{
				((AggregateUpdateContext)context).UpdateAggregatesForRow();
			}
			if (!this.m_dataRegionDef.ProcessCellRunningValues)
			{
				if (FlagUtils.HasFlag(dataAction, DataActions.PostSortAggregates))
				{
					if (this.m_postSortAggregates != null)
					{
						RuntimeDataRegionObj.UpdateAggregates(this.m_odpContext, this.m_postSortAggregates, false);
					}
					if (this.m_runningValues != null)
					{
						RuntimeDataTablixObj.UpdateRunningValues(this.m_odpContext, this.m_runningValues);
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
		}

		// Token: 0x06007BE7 RID: 31719 RVA: 0x001FDCCC File Offset: 0x001FBECC
		public override void SetupEnvironment()
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataSet dataSet = this.m_dataRegionDef.GetDataSet(this.m_odpContext.ReportDefinition);
			base.SetupNewDataSet(dataSet);
			this.m_odpContext.ReportRuntime.CurrentScope = this;
			base.SetupEnvironment(this.m_dataRegionDef.RunningValues);
		}

		// Token: 0x06007BE8 RID: 31720 RVA: 0x001FDD1C File Offset: 0x001FBF1C
		internal void CreateOutermostStaticCells(DataRegionInstance dataRegionInstance, bool outerGroupings, IReference<RuntimeMemberObj>[] innerMembers, IReference<RuntimeDataTablixGroupLeafObj> innerGroupLeafRef)
		{
			if (innerMembers != null)
			{
				this.SetupEnvironment();
				foreach (IReference<RuntimeMemberObj> reference in innerMembers)
				{
					using (reference.PinValue())
					{
						reference.Value().CreateInstances(base.SelfReference, this.m_odpContext, dataRegionInstance, !outerGroupings, null, dataRegionInstance, null, innerGroupLeafRef);
					}
				}
				return;
			}
			if (base.DataRegionDef.OutermostStaticRowIndexes != null && base.DataRegionDef.OutermostStaticColumnIndexes != null)
			{
				this.m_dataRegionDef.AddCell();
			}
		}

		// Token: 0x06007BE9 RID: 31721 RVA: 0x001FDDB0 File Offset: 0x001FBFB0
		private bool OutermostSTCellTargetScopeMatched(int index, IReference<RuntimeSortFilterEventInfo> sortFilterInfo)
		{
			return true;
		}

		// Token: 0x06007BEA RID: 31722 RVA: 0x001FDDB3 File Offset: 0x001FBFB3
		internal override bool TargetScopeMatched(int index, bool detailSort)
		{
			return (!this.m_dataRegionDef.InOutermostStaticCells || this.OutermostSTCellTargetScopeMatched(index, this.m_odpContext.RuntimeSortFilterInfo[index])) && base.TargetScopeMatched(index, detailSort);
		}

		// Token: 0x06007BEB RID: 31723 RVA: 0x001FDDE8 File Offset: 0x001FBFE8
		internal void CreateInstances(DataRegionInstance dataRegionInstance)
		{
			this.m_dataRegionDef.ResetInstanceIndexes();
			this.m_innerGroupsWithCellsForOuterPeerGroupProcessing = null;
			this.m_dataRegionDef.CurrentDataRegionInstance = dataRegionInstance;
			dataRegionInstance.StoreAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, this.m_dataRegionDef.Aggregates);
			dataRegionInstance.StoreAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, this.m_dataRegionDef.PostSortAggregates);
			dataRegionInstance.StoreAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo>(this.m_odpContext, this.m_dataRegionDef.RunningValues);
			if (this.m_dataRegionDef.DataScopeInfo != null)
			{
				DataScopeInfo dataScopeInfo = this.m_dataRegionDef.DataScopeInfo;
				dataRegionInstance.StoreAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, dataScopeInfo.AggregatesOfAggregates);
				dataRegionInstance.StoreAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, dataScopeInfo.PostSortAggregatesOfAggregates);
				dataRegionInstance.StoreAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo>(this.m_odpContext, dataScopeInfo.RunningValuesOfAggregates);
			}
			if (this.m_firstRow != null)
			{
				dataRegionInstance.FirstRowOffset = this.m_firstRow.StreamOffset;
			}
			this.m_dataRegionDef.ResetInstancePathCascade();
			if (this.m_dataRegionDef.InScopeEventSources != null)
			{
				UserSortFilterContext.ProcessEventSources(this.m_odpContext, this, this.m_dataRegionDef.InScopeEventSources);
			}
			this.CreateDataRegionScopedInstance(dataRegionInstance);
			IReference<RuntimeMemberObj>[] array;
			IReference<RuntimeMemberObj>[] array2;
			bool flag;
			if (base.DataRegionDef.ProcessingInnerGrouping == Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.ProcessingInnerGroupings.Column)
			{
				array = this.m_outerGroupings;
				array2 = this.m_innerGroupings;
				flag = true;
			}
			else
			{
				array = this.m_innerGroupings;
				array2 = this.m_outerGroupings;
				flag = false;
			}
			foreach (IReference<RuntimeMemberObj> reference in array)
			{
				using (reference.PinValue())
				{
					reference.Value().CreateInstances(base.SelfReference, this.m_odpContext, dataRegionInstance, flag, null, dataRegionInstance, array2, null);
				}
			}
			this.m_dataRegionDef.ResetInstancePathCascade();
			this.m_dataRegionDef.ResetInstanceIndexes();
		}

		// Token: 0x06007BEC RID: 31724 RVA: 0x001FDFA0 File Offset: 0x001FC1A0
		protected virtual void CreateDataRegionScopedInstance(DataRegionInstance dataRegionInstance)
		{
		}

		// Token: 0x06007BED RID: 31725 RVA: 0x001FDFA4 File Offset: 0x001FC1A4
		public IOnDemandMemberInstanceReference GetFirstMemberInstance(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode rifMember)
		{
			IReference<RuntimeMemberObj>[] memberCollection = this.GetMemberCollection(rifMember);
			return RuntimeDataRegionObj.GetFirstMemberInstance(rifMember, memberCollection);
		}

		// Token: 0x06007BEE RID: 31726 RVA: 0x001FDFC0 File Offset: 0x001FC1C0
		private IReference<RuntimeMemberObj>[] GetMemberCollection(Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode rifMember)
		{
			IReference<RuntimeMemberObj>[] array;
			if (this.m_dataRegionDef.ProcessingInnerGrouping == Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.ProcessingInnerGroupings.Column)
			{
				if (rifMember.IsColumn)
				{
					array = this.m_innerGroupings;
				}
				else
				{
					array = this.m_outerGroupings;
				}
			}
			else if (rifMember.IsColumn)
			{
				array = this.m_outerGroupings;
			}
			else
			{
				array = this.m_innerGroupings;
			}
			return array;
		}

		// Token: 0x06007BEF RID: 31727 RVA: 0x001FE00D File Offset: 0x001FC20D
		public IOnDemandMemberOwnerInstanceReference GetDataRegionInstance(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion rifDataRegion)
		{
			return this.GetNestedDataRegion(rifDataRegion);
		}

		// Token: 0x06007BF0 RID: 31728
		internal abstract RuntimeDataTablixObjReference GetNestedDataRegion(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion rifDataRegion);

		// Token: 0x06007BF1 RID: 31729 RVA: 0x001FE018 File Offset: 0x001FC218
		public IReference<IDataCorrelation> GetIdcReceiver(IRIFReportDataScope scope)
		{
			if (scope.IsGroup)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode reportHierarchyNode = scope as Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode;
				IReference<RuntimeMemberObj>[] memberCollection = this.GetMemberCollection(reportHierarchyNode);
				return RuntimeDataRegionObj.GetGroupRoot(reportHierarchyNode, memberCollection);
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = scope as Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion;
			return this.GetNestedDataRegion(dataRegion);
		}

		// Token: 0x17002896 RID: 10390
		// (get) Token: 0x06007BF2 RID: 31730 RVA: 0x001FE052 File Offset: 0x001FC252
		public bool IsNoRows
		{
			get
			{
				return this.m_firstRow == null;
			}
		}

		// Token: 0x17002897 RID: 10391
		// (get) Token: 0x06007BF3 RID: 31731 RVA: 0x001FE05D File Offset: 0x001FC25D
		public bool IsMostRecentlyCreatedScopeInstance
		{
			get
			{
				return this.m_dataRegionDef.DataScopeInfo.IsLastScopeInstanceNumber(this.m_scopeInstanceNumber);
			}
		}

		// Token: 0x17002898 RID: 10392
		// (get) Token: 0x06007BF4 RID: 31732 RVA: 0x001FE075 File Offset: 0x001FC275
		public bool HasUnProcessedServerAggregate
		{
			get
			{
				return this.m_customAggregates != null && this.m_customAggregates.Count > 0 && !this.m_hasProcessedAggregateRow;
			}
		}

		// Token: 0x06007BF5 RID: 31733 RVA: 0x001FE098 File Offset: 0x001FC298
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeDataTablixObj.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.InnerGroupings)
				{
					switch (memberName)
					{
					case MemberName.OuterGroupingCounters:
						writer.Write(this.m_outerGroupingCounters);
						break;
					case MemberName.OuterGroupings:
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] array = this.m_outerGroupings;
						writer.Write(array);
						break;
					}
					case MemberName.InnerGroupsWithCellsForOuterPeerGroupProcessing:
						writer.Write<IReference<RuntimeDataTablixGroupLeafObj>>(this.m_innerGroupsWithCellsForOuterPeerGroupProcessing);
						break;
					default:
						if (memberName != MemberName.ScopeInstanceNumber)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_scopeInstanceNumber);
						}
						break;
					}
				}
				else
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] array = this.m_innerGroupings;
					writer.Write(array);
				}
			}
		}

		// Token: 0x06007BF6 RID: 31734 RVA: 0x001FE15C File Offset: 0x001FC35C
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeDataTablixObj.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.InnerGroupings)
				{
					switch (memberName)
					{
					case MemberName.OuterGroupingCounters:
						this.m_outerGroupingCounters = reader.ReadInt32Array();
						break;
					case MemberName.OuterGroupings:
						this.m_outerGroupings = reader.ReadArrayOfRIFObjects<IReference<RuntimeMemberObj>>();
						break;
					case MemberName.InnerGroupsWithCellsForOuterPeerGroupProcessing:
						this.m_innerGroupsWithCellsForOuterPeerGroupProcessing = reader.ReadListOfRIFObjects<List<IReference<RuntimeDataTablixGroupLeafObj>>>();
						break;
					default:
						if (memberName != MemberName.ScopeInstanceNumber)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_scopeInstanceNumber = reader.ReadInt64();
						}
						break;
					}
				}
				else
				{
					this.m_innerGroupings = reader.ReadArrayOfRIFObjects<IReference<RuntimeMemberObj>>();
				}
			}
		}

		// Token: 0x06007BF7 RID: 31735 RVA: 0x001FE21A File Offset: 0x001FC41A
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007BF8 RID: 31736 RVA: 0x001FE224 File Offset: 0x001FC424
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixObj;
		}

		// Token: 0x06007BF9 RID: 31737 RVA: 0x001FE228 File Offset: 0x001FC428
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeDataTablixObj.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeRDLDataRegionObj, new List<MemberInfo>
				{
					new MemberInfo(MemberName.OuterGroupingCounters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Int32),
					new MemberInfo(MemberName.OuterGroupings, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMemberObjReference),
					new MemberInfo(MemberName.InnerGroupings, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMemberObjReference),
					new MemberInfo(MemberName.InnerGroupsWithCellsForOuterPeerGroupProcessing, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupLeafObjReference),
					new MemberInfo(MemberName.ScopeInstanceNumber, Token.Int64)
				});
			}
			return RuntimeDataTablixObj.m_declaration;
		}

		// Token: 0x17002899 RID: 10393
		// (get) Token: 0x06007BFA RID: 31738 RVA: 0x001FE2C5 File Offset: 0x001FC4C5
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.SizeOf(this.m_outerGroupingCounters) + ItemSizes.SizeOf<IReference<RuntimeMemberObj>>(this.m_outerGroupings) + ItemSizes.SizeOf<IReference<RuntimeMemberObj>>(this.m_innerGroupings) + ItemSizes.SizeOf<IReference<RuntimeDataTablixGroupLeafObj>>(this.m_innerGroupsWithCellsForOuterPeerGroupProcessing) + 8;
			}
		}

		// Token: 0x04003D91 RID: 15761
		protected int[] m_outerGroupingCounters;

		// Token: 0x04003D92 RID: 15762
		protected IReference<RuntimeMemberObj>[] m_outerGroupings;

		// Token: 0x04003D93 RID: 15763
		protected IReference<RuntimeMemberObj>[] m_innerGroupings;

		// Token: 0x04003D94 RID: 15764
		protected List<IReference<RuntimeDataTablixGroupLeafObj>> m_innerGroupsWithCellsForOuterPeerGroupProcessing;

		// Token: 0x04003D95 RID: 15765
		private long m_scopeInstanceNumber;

		// Token: 0x04003D96 RID: 15766
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeDataTablixObj.GetDeclaration();
	}
}

using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008BD RID: 2237
	[PersistedWithinRequestOnly]
	internal abstract class RuntimeRDLDataRegionObj : RuntimeDataRegionObj, IHierarchyObj, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, ReportProcessing.IFilterOwner, IDataRowSortOwner, IDataRowHolder, IDataCorrelation
	{
		// Token: 0x06007A60 RID: 31328 RVA: 0x001F8205 File Offset: 0x001F6405
		internal RuntimeRDLDataRegionObj()
		{
		}

		// Token: 0x06007A61 RID: 31329 RVA: 0x001F8210 File Offset: 0x001F6410
		internal RuntimeRDLDataRegionObj(IReference<IScope> outerScope, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef, ref DataActions dataAction, OnDemandProcessingContext odpContext, bool onePassProcess, List<Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo> runningValues, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, int level)
			: base(odpContext, objectType, level)
		{
			this.m_dataRegionDef = dataRegionDef;
			this.m_outerScope = outerScope;
			RuntimeDataRegionObj.CreateAggregates(this.m_odpContext, dataRegionDef.Aggregates, ref this.m_nonCustomAggregates, ref this.m_customAggregates);
			if (dataRegionDef.DataScopeInfo != null)
			{
				RuntimeDataRegionObj.CreateAggregates<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateInfo>(this.m_odpContext, dataRegionDef.DataScopeInfo.AggregatesOfAggregates, ref this.m_aggregatesOfAggregates);
			}
			if (dataRegionDef.Filters != null)
			{
				this.m_filters = new Filters(Filters.FilterTypes.DataRegionFilter, (IReference<ReportProcessing.IFilterOwner>)base.SelfReference, dataRegionDef.Filters, dataRegionDef.ObjectType, dataRegionDef.Name, this.m_odpContext, level + 1);
				return;
			}
			this.m_outerDataAction = dataAction;
			this.m_dataAction = dataAction;
			dataAction = DataActions.None;
		}

		// Token: 0x17002849 RID: 10313
		// (get) Token: 0x06007A62 RID: 31330 RVA: 0x001F82C7 File Offset: 0x001F64C7
		protected override IReference<IScope> OuterScope
		{
			get
			{
				return this.m_outerScope;
			}
		}

		// Token: 0x1700284A RID: 10314
		// (get) Token: 0x06007A63 RID: 31331 RVA: 0x001F82CF File Offset: 0x001F64CF
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion DataRegionDef
		{
			get
			{
				return this.m_dataRegionDef;
			}
		}

		// Token: 0x1700284B RID: 10315
		// (get) Token: 0x06007A64 RID: 31332 RVA: 0x001F82D7 File Offset: 0x001F64D7
		protected override string ScopeName
		{
			get
			{
				return this.m_dataRegionDef.Name;
			}
		}

		// Token: 0x1700284C RID: 10316
		// (get) Token: 0x06007A65 RID: 31333 RVA: 0x001F82E4 File Offset: 0x001F64E4
		internal override bool TargetForNonDetailSort
		{
			get
			{
				return (this.m_userSortTargetInfo != null && this.m_userSortTargetInfo.TargetForNonDetailSort) || this.m_outerScope.Value().TargetForNonDetailSort;
			}
		}

		// Token: 0x1700284D RID: 10317
		// (get) Token: 0x06007A66 RID: 31334 RVA: 0x001F8310 File Offset: 0x001F6510
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

		// Token: 0x1700284E RID: 10318
		// (get) Token: 0x06007A67 RID: 31335 RVA: 0x001F836A File Offset: 0x001F656A
		internal override IRIFReportScope RIFReportScope
		{
			get
			{
				return this.m_dataRegionDef;
			}
		}

		// Token: 0x06007A68 RID: 31336 RVA: 0x001F8372 File Offset: 0x001F6572
		internal override bool IsTargetForSort(int index, bool detailSort)
		{
			return (this.m_userSortTargetInfo != null && this.m_userSortTargetInfo.IsTargetForSort(index, detailSort)) || this.m_outerScope.Value().IsTargetForSort(index, detailSort);
		}

		// Token: 0x1700284F RID: 10319
		// (get) Token: 0x06007A69 RID: 31337 RVA: 0x001F839F File Offset: 0x001F659F
		IReference<IHierarchyObj> IHierarchyObj.HierarchyRoot
		{
			get
			{
				return (IReference<IHierarchyObj>)this.m_selfReference;
			}
		}

		// Token: 0x17002850 RID: 10320
		// (get) Token: 0x06007A6A RID: 31338 RVA: 0x001F83AC File Offset: 0x001F65AC
		OnDemandProcessingContext IHierarchyObj.OdpContext
		{
			get
			{
				return this.m_odpContext;
			}
		}

		// Token: 0x17002851 RID: 10321
		// (get) Token: 0x06007A6B RID: 31339 RVA: 0x001F83B4 File Offset: 0x001F65B4
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

		// Token: 0x17002852 RID: 10322
		// (get) Token: 0x06007A6C RID: 31340 RVA: 0x001F83CB File Offset: 0x001F65CB
		int IHierarchyObj.ExpressionIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17002853 RID: 10323
		// (get) Token: 0x06007A6D RID: 31341 RVA: 0x001F83CE File Offset: 0x001F65CE
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

		// Token: 0x17002854 RID: 10324
		// (get) Token: 0x06007A6E RID: 31342 RVA: 0x001F83E5 File Offset: 0x001F65E5
		bool IHierarchyObj.IsDetail
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002855 RID: 10325
		// (get) Token: 0x06007A6F RID: 31343 RVA: 0x001F83E8 File Offset: 0x001F65E8
		bool IHierarchyObj.InDataRowSortPhase
		{
			get
			{
				return this.m_inDataRowSortPhase;
			}
		}

		// Token: 0x06007A70 RID: 31344 RVA: 0x001F83F0 File Offset: 0x001F65F0
		IHierarchyObj IHierarchyObj.CreateHierarchyObjForSortTree()
		{
			if (this.m_inDataRowSortPhase)
			{
				return new RuntimeDataRowSortHierarchyObj(this, base.Depth + 1);
			}
			return new RuntimeSortHierarchyObj(this, this.m_depth + 1);
		}

		// Token: 0x06007A71 RID: 31345 RVA: 0x001F8418 File Offset: 0x001F6618
		ProcessingMessageList IHierarchyObj.RegisterComparisonError(string propertyName)
		{
			if (this.m_inDataRowSortPhase)
			{
				this.m_odpContext.ErrorContext.Register(ProcessingErrorCode.rsComparisonError, Severity.Error, this.m_dataRegionDef.ObjectType, this.m_dataRegionDef.Name, propertyName, Array.Empty<string>());
				return this.m_odpContext.ErrorContext.Messages;
			}
			return this.m_odpContext.RegisterComparisonErrorForSortFilterEvent(propertyName);
		}

		// Token: 0x06007A72 RID: 31346 RVA: 0x001F847D File Offset: 0x001F667D
		void IHierarchyObj.NextRow(IHierarchyObj owner)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007A73 RID: 31347 RVA: 0x001F848A File Offset: 0x001F668A
		void IHierarchyObj.Traverse(ProcessingStages operation, ITraversalContext traversalContext)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007A74 RID: 31348 RVA: 0x001F8497 File Offset: 0x001F6697
		void IHierarchyObj.ReadRow()
		{
			this.ReadRow(DataActions.UserSort, null);
		}

		// Token: 0x06007A75 RID: 31349 RVA: 0x001F84A4 File Offset: 0x001F66A4
		void IHierarchyObj.ProcessUserSort()
		{
			this.m_odpContext.ProcessUserSortForTarget((RuntimeRDLDataRegionObjReference)base.SelfReference, ref this.m_dataRows, this.m_userSortTargetInfo.TargetForNonDetailSort);
			this.m_dataAction &= ~DataActions.UserSort;
			if (this.m_userSortTargetInfo.TargetForNonDetailSort)
			{
				this.m_userSortTargetInfo.ResetTargetForNonDetailSort();
				this.m_userSortTargetInfo.EnterProcessUserSortPhase(this.m_odpContext);
				DataActions innerDataAction = this.m_innerDataAction;
				this.ConstructRuntimeStructure(ref innerDataAction, this.m_odpContext.ReportDefinition.MergeOnePass);
				if (this.m_dataAction != DataActions.None)
				{
					this.m_dataRows = new ScalableList<DataFieldRow>(this.m_depth, this.m_odpContext.TablixProcessingScalabilityCache);
				}
				base.ScopeFinishSorting(ref this.m_firstRow, this.m_userSortTargetInfo);
				this.m_userSortTargetInfo.LeaveProcessUserSortPhase(this.m_odpContext);
			}
		}

		// Token: 0x06007A76 RID: 31350 RVA: 0x001F8579 File Offset: 0x001F6779
		void IHierarchyObj.MarkSortInfoProcessed(List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo)
		{
			if (this.m_userSortTargetInfo != null)
			{
				this.m_userSortTargetInfo.MarkSortInfoProcessed(runtimeSortFilterInfo, (IReference<IHierarchyObj>)base.SelfReference);
			}
		}

		// Token: 0x06007A77 RID: 31351 RVA: 0x001F859A File Offset: 0x001F679A
		void IHierarchyObj.AddSortInfoIndex(int sortInfoIndex, IReference<RuntimeSortFilterEventInfo> sortInfo)
		{
			if (this.m_userSortTargetInfo != null)
			{
				this.m_userSortTargetInfo.AddSortInfoIndex(sortInfoIndex, sortInfo);
			}
		}

		// Token: 0x06007A78 RID: 31352
		protected abstract void ConstructRuntimeStructure(ref DataActions innerDataAction, bool onePassProcess);

		// Token: 0x06007A79 RID: 31353 RVA: 0x001F85B4 File Offset: 0x001F67B4
		protected DataActions HandleSortFilterEvent()
		{
			DataActions dataActions = DataActions.None;
			if (this.m_odpContext.IsSortFilterTarget(this.DataRegionDef.IsSortFilterTarget, this.m_outerScope, (RuntimeRDLDataRegionObjReference)base.SelfReference, ref this.m_userSortTargetInfo) && this.m_userSortTargetInfo.TargetForNonDetailSort)
			{
				dataActions = DataActions.UserSort;
			}
			this.m_odpContext.RegisterSortFilterExpressionScope(this.m_outerScope, base.SelfReference, this.DataRegionDef.IsSortFilterExpressionScope);
			return dataActions;
		}

		// Token: 0x06007A7A RID: 31354 RVA: 0x001F8624 File Offset: 0x001F6824
		internal override bool TargetScopeMatched(int index, bool detailSort)
		{
			return this.m_outerScope.Value().TargetScopeMatched(index, detailSort);
		}

		// Token: 0x06007A7B RID: 31355 RVA: 0x001F8638 File Offset: 0x001F6838
		internal override void GetScopeValues(IReference<IHierarchyObj> targetScopeObj, List<object>[] scopeValues, ref int index)
		{
			if (targetScopeObj == null || this != targetScopeObj.Value())
			{
				this.m_outerScope.Value().GetScopeValues(targetScopeObj, scopeValues, ref index);
			}
		}

		// Token: 0x06007A7C RID: 31356 RVA: 0x001F8659 File Offset: 0x001F6859
		internal override void NextRow()
		{
			if (this.m_dataRegionDef.DataScopeInfo == null || !this.m_dataRegionDef.DataScopeInfo.NeedsIDC)
			{
				this.NextRegularRow();
			}
		}

		// Token: 0x06007A7D RID: 31357 RVA: 0x001F8681 File Offset: 0x001F6881
		bool IDataCorrelation.NextCorrelatedRow()
		{
			return this.NextRegularRow();
		}

		// Token: 0x06007A7E RID: 31358 RVA: 0x001F868C File Offset: 0x001F688C
		private bool NextRegularRow()
		{
			if (this.m_odpContext.ReportObjectModel.FieldsImpl.AggregationFieldCount == 0)
			{
				this.m_hasProcessedAggregateRow = true;
				RuntimeDataRegionObj.UpdateAggregates(this.m_odpContext, this.m_customAggregates, false);
			}
			if (this.m_odpContext.ReportObjectModel.FieldsImpl.IsAggregateRow)
			{
				base.ScopeNextAggregateRow(this.m_userSortTargetInfo);
				return false;
			}
			this.NextNonAggregateRow();
			return true;
		}

		// Token: 0x06007A7F RID: 31359 RVA: 0x001F86F8 File Offset: 0x001F68F8
		private void NextNonAggregateRow()
		{
			bool flag = true;
			if (this.m_filters != null)
			{
				flag = this.m_filters.PassFilters(new DataFieldRow(this.m_odpContext.ReportObjectModel.FieldsImpl, false));
			}
			if (flag)
			{
				((ReportProcessing.IFilterOwner)this).PostFilterNextRow();
			}
		}

		// Token: 0x06007A80 RID: 31360 RVA: 0x001F873C File Offset: 0x001F693C
		void ReportProcessing.IFilterOwner.PostFilterNextRow()
		{
			if (this.m_inDataRowSortPhase)
			{
				object obj = this.EvaluateDataRowSortExpression(this.m_dataRowSortExpression);
				this.m_sortedDataRowTree.NextRow(obj, this);
				return;
			}
			((IDataRowSortOwner)this).PostDataRowSortNextRow();
		}

		// Token: 0x06007A81 RID: 31361 RVA: 0x001F8772 File Offset: 0x001F6972
		public object EvaluateDataRowSortExpression(RuntimeExpressionInfo sortExpression)
		{
			return this.m_odpContext.ReportRuntime.EvaluateRuntimeExpression(sortExpression, base.ObjectType, this.m_dataRegionDef.Name, "Sort");
		}

		// Token: 0x06007A82 RID: 31362 RVA: 0x001F879B File Offset: 0x001F699B
		void IDataRowSortOwner.PostDataRowSortNextRow()
		{
			RuntimeDataRegionObj.CommonFirstRow(this.m_odpContext, ref this.m_firstRowIsAggregate, ref this.m_firstRow);
			base.ScopeNextNonAggregateRow(this.m_nonCustomAggregates, this.m_dataRows);
		}

		// Token: 0x06007A83 RID: 31363 RVA: 0x001F87C8 File Offset: 0x001F69C8
		void IDataRowSortOwner.DataRowSortTraverse()
		{
			try
			{
				ITraversalContext traversalContext = new DataRowSortOwnerTraversalContext(this);
				this.m_sortedDataRowTree.Traverse(ProcessingStages.Grouping, this.m_dataRowSortExpression.Direction, traversalContext);
			}
			finally
			{
				this.m_inDataRowSortPhase = false;
				this.m_sortedDataRowTree.Dispose();
				this.m_sortedDataRowTree = null;
				this.m_dataRowSortExpression = null;
			}
		}

		// Token: 0x17002856 RID: 10326
		// (get) Token: 0x06007A84 RID: 31364 RVA: 0x001F8828 File Offset: 0x001F6A28
		Microsoft.ReportingServices.ReportIntermediateFormat.Sorting IDataRowSortOwner.SortingDef
		{
			get
			{
				return this.m_dataRegionDef.Sorting;
			}
		}

		// Token: 0x17002857 RID: 10327
		// (get) Token: 0x06007A85 RID: 31365 RVA: 0x001F8835 File Offset: 0x001F6A35
		OnDemandProcessingContext IDataRowSortOwner.OdpContext
		{
			get
			{
				return this.m_odpContext;
			}
		}

		// Token: 0x06007A86 RID: 31366 RVA: 0x001F883D File Offset: 0x001F6A3D
		internal override bool SortAndFilter(AggregateUpdateContext aggContext)
		{
			if ((SecondPassOperations.FilteringOrAggregatesOrDomainScope & this.m_odpContext.SecondPassOperation) != SecondPassOperations.None && this.m_dataRows != null && (this.m_outerDataAction & DataActions.RecursiveAggregates) != DataActions.None)
			{
				this.ReadRows(DataActions.RecursiveAggregates, null);
				base.ReleaseDataRows(DataActions.RecursiveAggregates, ref this.m_dataAction, ref this.m_dataRows);
			}
			return true;
		}

		// Token: 0x06007A87 RID: 31367 RVA: 0x001F887C File Offset: 0x001F6A7C
		public void ReadRows(DataActions action, ITraversalContext context)
		{
			for (int i = 0; i < this.m_dataRows.Count; i++)
			{
				this.m_dataRows[i].SetFields(this.m_odpContext.ReportObjectModel.FieldsImpl);
				this.ReadRow(action, context);
			}
		}

		// Token: 0x06007A88 RID: 31368 RVA: 0x001F88C8 File Offset: 0x001F6AC8
		protected void SetupEnvironment(List<Microsoft.ReportingServices.ReportIntermediateFormat.RunningValueInfo> runningValues)
		{
			if (this.m_dataRegionDef.DataScopeInfo != null && this.m_dataRegionDef.DataScopeInfo.DataSet != null && this.m_dataRegionDef.DataScopeInfo.DataSet.DataSetCore.FieldsContext != null)
			{
				this.m_odpContext.ReportObjectModel.RestoreFields(this.m_dataRegionDef.DataScopeInfo.DataSet.DataSetCore.FieldsContext);
			}
			base.SetupEnvironment(this.m_nonCustomAggregates, this.m_customAggregates, this.m_firstRow);
			base.SetupAggregates(this.m_postSortAggregates);
			base.SetupRunningValues(runningValues, this.m_runningValueValues);
			base.SetupAggregates(this.m_aggregatesOfAggregates);
			base.SetupAggregates(this.m_postSortAggregatesOfAggregates);
			if (this.m_dataRegionDef.DataScopeInfo != null)
			{
				base.SetupRunningValues(this.m_dataRegionDef.DataScopeInfo.RunningValuesOfAggregates, this.m_runningValueOfAggregateValues);
			}
		}

		// Token: 0x06007A89 RID: 31369 RVA: 0x001F89AC File Offset: 0x001F6BAC
		internal override bool InScope(string scope)
		{
			return base.DataRegionInScope(this.DataRegionDef, scope);
		}

		// Token: 0x06007A8A RID: 31370 RVA: 0x001F89BB File Offset: 0x001F6BBB
		protected override int GetRecursiveLevel(string scope)
		{
			return base.DataRegionRecursiveLevel(this.DataRegionDef, scope);
		}

		// Token: 0x06007A8B RID: 31371 RVA: 0x001F89CA File Offset: 0x001F6BCA
		protected override void GetGroupNameValuePairs(Dictionary<string, object> pairs)
		{
			base.DataRegionGetGroupNameValuePairs(this.DataRegionDef, pairs);
		}

		// Token: 0x06007A8C RID: 31372 RVA: 0x001F89DC File Offset: 0x001F6BDC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeRDLDataRegionObj.m_declaration);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.DataRows)
				{
					if (memberName <= MemberName.RunningValues)
					{
						switch (memberName)
						{
						case MemberName.RunningValueValues:
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] array = this.m_runningValueValues;
							writer.Write(array);
							continue;
						}
						case MemberName.Innermost:
						case MemberName.NextCell:
						case MemberName.CellsWithSameScope:
						case MemberName.ReportItemColIndex:
							break;
						case MemberName.FirstRow:
							writer.Write(this.m_firstRow);
							continue;
						case MemberName.FirstRowIsAggregate:
							writer.Write(this.m_firstRowIsAggregate);
							continue;
						case MemberName.SortFilterExpressionScopeInfoIndices:
							writer.Write(this.m_sortFilterExpressionScopeInfoIndices);
							continue;
						case MemberName.OuterScope:
							writer.Write(this.m_outerScope);
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
						case MemberName.OuterDataAction:
							writer.WriteEnum((int)this.m_outerDataAction);
							continue;
						case MemberName.InnerDataAction:
							writer.WriteEnum((int)this.m_innerDataAction);
							continue;
						case MemberName.UserSortTargetInfo:
							writer.Write(this.m_userSortTargetInfo);
							continue;
						default:
							if (memberName == MemberName.DataRegionDef)
							{
								int num = scalabilityCache.StoreStaticReference(this.m_dataRegionDef);
								writer.Write(num);
								continue;
							}
							if (memberName == MemberName.RunningValues)
							{
								writer.WriteListOfPrimitives<string>(this.m_runningValues);
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.Filters)
						{
							int num2 = scalabilityCache.StoreStaticReference(this.m_filters);
							writer.Write(num2);
							continue;
						}
						if (memberName == MemberName.PostSortAggregates)
						{
							writer.Write<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_postSortAggregates);
							continue;
						}
						if (memberName == MemberName.DataRows)
						{
							writer.Write(this.m_dataRows);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.AggregatesOfAggregates)
				{
					if (memberName == MemberName.PreviousValues)
					{
						writer.WriteListOfPrimitives<string>(this.m_previousValues);
						continue;
					}
					switch (memberName)
					{
					case MemberName.InDataRowSortPhase:
						writer.Write(this.m_inDataRowSortPhase);
						continue;
					case MemberName.SortedDataRowTree:
						writer.Write(this.m_sortedDataRowTree);
						continue;
					case MemberName.DataRowSortExpression:
						writer.Write(this.m_dataRowSortExpression);
						continue;
					default:
						if (memberName == MemberName.AggregatesOfAggregates)
						{
							writer.Write(this.m_aggregatesOfAggregates);
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.PostSortAggregatesOfAggregates)
					{
						writer.Write(this.m_postSortAggregatesOfAggregates);
						continue;
					}
					if (memberName == MemberName.RunningValueOfAggregateValues)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable[] array = this.m_runningValueOfAggregateValues;
						writer.Write(array);
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

		// Token: 0x06007A8D RID: 31373 RVA: 0x001F8CDC File Offset: 0x001F6EDC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeRDLDataRegionObj.m_declaration);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.DataRows)
				{
					if (memberName <= MemberName.RunningValues)
					{
						switch (memberName)
						{
						case MemberName.RunningValueValues:
							this.m_runningValueValues = reader.ReadArrayOfRIFObjects<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>();
							continue;
						case MemberName.Innermost:
						case MemberName.NextCell:
						case MemberName.CellsWithSameScope:
						case MemberName.ReportItemColIndex:
							break;
						case MemberName.FirstRow:
							this.m_firstRow = (DataFieldRow)reader.ReadRIFObject();
							continue;
						case MemberName.FirstRowIsAggregate:
							this.m_firstRowIsAggregate = reader.ReadBoolean();
							continue;
						case MemberName.SortFilterExpressionScopeInfoIndices:
							this.m_sortFilterExpressionScopeInfoIndices = reader.ReadInt32Array();
							continue;
						case MemberName.OuterScope:
							this.m_outerScope = (IReference<IScope>)reader.ReadRIFObject();
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
						case MemberName.OuterDataAction:
							this.m_outerDataAction = (DataActions)reader.ReadEnum();
							continue;
						case MemberName.InnerDataAction:
							this.m_innerDataAction = (DataActions)reader.ReadEnum();
							continue;
						case MemberName.UserSortTargetInfo:
							this.m_userSortTargetInfo = (RuntimeUserSortTargetInfo)reader.ReadRIFObject();
							continue;
						default:
							if (memberName == MemberName.DataRegionDef)
							{
								int num = reader.ReadInt32();
								this.m_dataRegionDef = (Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)scalabilityCache.FetchStaticReference(num);
								continue;
							}
							if (memberName == MemberName.RunningValues)
							{
								this.m_runningValues = reader.ReadListOfPrimitives<string>();
								continue;
							}
							break;
						}
					}
					else
					{
						if (memberName == MemberName.Filters)
						{
							int num2 = reader.ReadInt32();
							this.m_filters = (Filters)scalabilityCache.FetchStaticReference(num2);
							continue;
						}
						if (memberName == MemberName.PostSortAggregates)
						{
							this.m_postSortAggregates = reader.ReadListOfRIFObjects<List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>>();
							continue;
						}
						if (memberName == MemberName.DataRows)
						{
							this.m_dataRows = reader.ReadRIFObject<ScalableList<DataFieldRow>>();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.AggregatesOfAggregates)
				{
					if (memberName == MemberName.PreviousValues)
					{
						this.m_previousValues = reader.ReadListOfPrimitives<string>();
						continue;
					}
					switch (memberName)
					{
					case MemberName.InDataRowSortPhase:
						this.m_inDataRowSortPhase = reader.ReadBoolean();
						continue;
					case MemberName.SortedDataRowTree:
						this.m_sortedDataRowTree = (BTree)reader.ReadRIFObject();
						continue;
					case MemberName.DataRowSortExpression:
						this.m_dataRowSortExpression = (RuntimeExpressionInfo)reader.ReadRIFObject();
						continue;
					default:
						if (memberName == MemberName.AggregatesOfAggregates)
						{
							this.m_aggregatesOfAggregates = (BucketedDataAggregateObjs)reader.ReadRIFObject();
							continue;
						}
						break;
					}
				}
				else
				{
					if (memberName == MemberName.PostSortAggregatesOfAggregates)
					{
						this.m_postSortAggregatesOfAggregates = (BucketedDataAggregateObjs)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.RunningValueOfAggregateValues)
					{
						this.m_runningValueOfAggregateValues = reader.ReadArrayOfRIFObjects<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>();
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

		// Token: 0x06007A8E RID: 31374 RVA: 0x001F9004 File Offset: 0x001F7204
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007A8F RID: 31375 RVA: 0x001F900E File Offset: 0x001F720E
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeRDLDataRegionObj;
		}

		// Token: 0x06007A90 RID: 31376 RVA: 0x001F9018 File Offset: 0x001F7218
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeRDLDataRegionObj.m_declaration == null)
			{
				RuntimeRDLDataRegionObj.m_declaration = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeRDLDataRegionObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataRegionObj, new List<MemberInfo>
				{
					new MemberInfo(MemberName.DataRegionDef, Token.Int32),
					new MemberInfo(MemberName.OuterScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IScopeReference),
					new MemberInfo(MemberName.FirstRow, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataFieldRow),
					new MemberInfo(MemberName.FirstRowIsAggregate, Token.Boolean),
					new MemberInfo(MemberName.Filters, Token.Int32),
					new MemberInfo(MemberName.NonCustomAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj),
					new MemberInfo(MemberName.CustomAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj),
					new MemberInfo(MemberName.DataAction, Token.Enum),
					new MemberInfo(MemberName.OuterDataAction, Token.Enum),
					new MemberInfo(MemberName.RunningValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.String),
					new MemberInfo(MemberName.PreviousValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.String),
					new MemberInfo(MemberName.RunningValueValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjResult),
					new MemberInfo(MemberName.PostSortAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObj),
					new MemberInfo(MemberName.DataRows, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList),
					new MemberInfo(MemberName.InnerDataAction, Token.Enum),
					new MemberInfo(MemberName.UserSortTargetInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeUserSortTargetInfo),
					new MemberInfo(MemberName.SortFilterExpressionScopeInfoIndices, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Int32),
					new MemberInfo(MemberName.InDataRowSortPhase, Token.Boolean),
					new MemberInfo(MemberName.SortedDataRowTree, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTree),
					new MemberInfo(MemberName.DataRowSortExpression, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeExpressionInfo),
					new MemberInfo(MemberName.AggregatesOfAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BucketedDataAggregateObjs),
					new MemberInfo(MemberName.PostSortAggregatesOfAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BucketedDataAggregateObjs),
					new MemberInfo(MemberName.RunningValueOfAggregateValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateObjResult),
					new MemberInfo(MemberName.HasProcessedAggregateRow, Token.Boolean)
				});
			}
			return RuntimeRDLDataRegionObj.m_declaration;
		}

		// Token: 0x17002858 RID: 10328
		// (get) Token: 0x06007A91 RID: 31377 RVA: 0x001F920C File Offset: 0x001F740C
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.SizeOf(this.m_outerScope) + ItemSizes.SizeOf(this.m_firstRow) + 1 + ItemSizes.ReferenceSize + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_nonCustomAggregates) + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_customAggregates) + 4 + 4 + ItemSizes.SizeOf(this.m_runningValues) + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>(this.m_runningValueValues) + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>(this.m_runningValueOfAggregateValues) + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj>(this.m_postSortAggregates) + ItemSizes.SizeOf<DataFieldRow>(this.m_dataRows) + 4 + ItemSizes.SizeOf(this.m_userSortTargetInfo) + ItemSizes.SizeOf(this.m_sortFilterExpressionScopeInfoIndices) + 1 + ItemSizes.SizeOf(this.m_sortedDataRowTree) + ItemSizes.SizeOf(this.m_dataRowSortExpression) + ItemSizes.SizeOf(this.m_aggregatesOfAggregates) + ItemSizes.SizeOf(this.m_postSortAggregatesOfAggregates) + 1;
			}
		}

		// Token: 0x04003D2B RID: 15659
		[StaticReference]
		protected Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion m_dataRegionDef;

		// Token: 0x04003D2C RID: 15660
		protected IReference<IScope> m_outerScope;

		// Token: 0x04003D2D RID: 15661
		protected DataFieldRow m_firstRow;

		// Token: 0x04003D2E RID: 15662
		protected bool m_firstRowIsAggregate;

		// Token: 0x04003D2F RID: 15663
		[StaticReference]
		protected Filters m_filters;

		// Token: 0x04003D30 RID: 15664
		protected List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> m_nonCustomAggregates;

		// Token: 0x04003D31 RID: 15665
		protected BucketedDataAggregateObjs m_aggregatesOfAggregates;

		// Token: 0x04003D32 RID: 15666
		protected List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> m_customAggregates;

		// Token: 0x04003D33 RID: 15667
		protected DataActions m_dataAction;

		// Token: 0x04003D34 RID: 15668
		protected DataActions m_outerDataAction;

		// Token: 0x04003D35 RID: 15669
		protected List<string> m_runningValues;

		// Token: 0x04003D36 RID: 15670
		protected List<string> m_previousValues;

		// Token: 0x04003D37 RID: 15671
		protected Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[] m_runningValueValues;

		// Token: 0x04003D38 RID: 15672
		protected Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[] m_runningValueOfAggregateValues;

		// Token: 0x04003D39 RID: 15673
		protected List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObj> m_postSortAggregates;

		// Token: 0x04003D3A RID: 15674
		protected BucketedDataAggregateObjs m_postSortAggregatesOfAggregates;

		// Token: 0x04003D3B RID: 15675
		protected ScalableList<DataFieldRow> m_dataRows;

		// Token: 0x04003D3C RID: 15676
		protected DataActions m_innerDataAction;

		// Token: 0x04003D3D RID: 15677
		protected RuntimeUserSortTargetInfo m_userSortTargetInfo;

		// Token: 0x04003D3E RID: 15678
		protected int[] m_sortFilterExpressionScopeInfoIndices;

		// Token: 0x04003D3F RID: 15679
		protected bool m_inDataRowSortPhase;

		// Token: 0x04003D40 RID: 15680
		protected BTree m_sortedDataRowTree;

		// Token: 0x04003D41 RID: 15681
		protected RuntimeExpressionInfo m_dataRowSortExpression;

		// Token: 0x04003D42 RID: 15682
		protected bool m_hasProcessedAggregateRow;

		// Token: 0x04003D43 RID: 15683
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeRDLDataRegionObj.GetDeclaration();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004B0 RID: 1200
	public class DataScopeInfo : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003BCC RID: 15308 RVA: 0x001027C3 File Offset: 0x001009C3
		public DataScopeInfo()
		{
		}

		// Token: 0x06003BCD RID: 15309 RVA: 0x001027D2 File Offset: 0x001009D2
		public DataScopeInfo(int scopeId)
		{
			this.m_scopeID = scopeId;
			this.m_runningValuesOfAggregates = new List<RunningValueInfo>();
			this.m_aggregatesOfAggregates = new BucketedDataAggregateInfos();
			this.m_postSortAggregatesOfAggregates = new BucketedDataAggregateInfos();
		}

		// Token: 0x170019B0 RID: 6576
		// (get) Token: 0x06003BCE RID: 15310 RVA: 0x00102809 File Offset: 0x00100A09
		// (set) Token: 0x06003BCF RID: 15311 RVA: 0x00102811 File Offset: 0x00100A11
		internal bool AggregatesSpanGroupFilter
		{
			get
			{
				return this.m_aggregatesSpanGroupFilter;
			}
			set
			{
				if (!this.m_aggregatesSpanGroupFilter)
				{
					this.m_aggregatesSpanGroupFilter = value;
				}
			}
		}

		// Token: 0x170019B1 RID: 6577
		// (get) Token: 0x06003BD0 RID: 15312 RVA: 0x00102822 File Offset: 0x00100A22
		// (set) Token: 0x06003BD1 RID: 15313 RVA: 0x0010282A File Offset: 0x00100A2A
		internal bool HasAggregatesToUpdateAtRowScope
		{
			get
			{
				return this.m_hasAggregatesToUpdateAtRowScope;
			}
			set
			{
				if (!this.m_hasAggregatesToUpdateAtRowScope)
				{
					this.m_hasAggregatesToUpdateAtRowScope = value;
				}
			}
		}

		// Token: 0x170019B2 RID: 6578
		// (get) Token: 0x06003BD2 RID: 15314 RVA: 0x0010283B File Offset: 0x00100A3B
		internal bool NeedsSeparateAofAPass
		{
			get
			{
				return this.AggregatesSpanGroupFilter;
			}
		}

		// Token: 0x170019B3 RID: 6579
		// (get) Token: 0x06003BD3 RID: 15315 RVA: 0x00102843 File Offset: 0x00100A43
		// (set) Token: 0x06003BD4 RID: 15316 RVA: 0x0010284B File Offset: 0x00100A4B
		internal BucketedDataAggregateInfos AggregatesOfAggregates
		{
			get
			{
				return this.m_aggregatesOfAggregates;
			}
			set
			{
				this.m_aggregatesOfAggregates = value;
			}
		}

		// Token: 0x170019B4 RID: 6580
		// (get) Token: 0x06003BD5 RID: 15317 RVA: 0x00102854 File Offset: 0x00100A54
		// (set) Token: 0x06003BD6 RID: 15318 RVA: 0x0010285C File Offset: 0x00100A5C
		internal BucketedDataAggregateInfos PostSortAggregatesOfAggregates
		{
			get
			{
				return this.m_postSortAggregatesOfAggregates;
			}
			set
			{
				this.m_postSortAggregatesOfAggregates = value;
			}
		}

		// Token: 0x170019B5 RID: 6581
		// (get) Token: 0x06003BD7 RID: 15319 RVA: 0x00102865 File Offset: 0x00100A65
		// (set) Token: 0x06003BD8 RID: 15320 RVA: 0x0010286D File Offset: 0x00100A6D
		internal List<RunningValueInfo> RunningValuesOfAggregates
		{
			get
			{
				return this.m_runningValuesOfAggregates;
			}
			set
			{
				this.m_runningValuesOfAggregates = value;
			}
		}

		// Token: 0x170019B6 RID: 6582
		// (get) Token: 0x06003BD9 RID: 15321 RVA: 0x00102876 File Offset: 0x00100A76
		// (set) Token: 0x06003BDA RID: 15322 RVA: 0x0010287E File Offset: 0x00100A7E
		internal int ScopeID
		{
			get
			{
				return this.m_scopeID;
			}
			set
			{
				this.m_scopeID = value;
			}
		}

		// Token: 0x170019B7 RID: 6583
		// (get) Token: 0x06003BDB RID: 15323 RVA: 0x00102887 File Offset: 0x00100A87
		internal DataSet DataSet
		{
			get
			{
				return this.m_dataSet;
			}
		}

		// Token: 0x170019B8 RID: 6584
		// (get) Token: 0x06003BDC RID: 15324 RVA: 0x0010288F File Offset: 0x00100A8F
		// (set) Token: 0x06003BDD RID: 15325 RVA: 0x00102897 File Offset: 0x00100A97
		internal int DataPipelineID
		{
			get
			{
				return this.m_dataPipelineID;
			}
			set
			{
				this.m_dataPipelineID = value;
			}
		}

		// Token: 0x170019B9 RID: 6585
		// (get) Token: 0x06003BDE RID: 15326 RVA: 0x001028A0 File Offset: 0x00100AA0
		internal bool HasAggregatesOrRunningValues
		{
			get
			{
				return DataScopeInfo.HasAggregates<DataAggregateInfo>(this.m_aggregatesOfAggregates) || DataScopeInfo.HasAggregates<DataAggregateInfo>(this.m_postSortAggregatesOfAggregates) || this.HasRunningValues;
			}
		}

		// Token: 0x170019BA RID: 6586
		// (get) Token: 0x06003BDF RID: 15327 RVA: 0x001028C4 File Offset: 0x00100AC4
		internal bool HasRunningValues
		{
			get
			{
				return this.m_runningValuesOfAggregates.Count > 0;
			}
		}

		// Token: 0x170019BB RID: 6587
		// (get) Token: 0x06003BE0 RID: 15328 RVA: 0x001028D4 File Offset: 0x00100AD4
		// (set) Token: 0x06003BE1 RID: 15329 RVA: 0x001028DC File Offset: 0x00100ADC
		internal bool IsDecomposable
		{
			get
			{
				return this.m_isDecomposable;
			}
			set
			{
				this.m_isDecomposable = value;
			}
		}

		// Token: 0x170019BC RID: 6588
		// (get) Token: 0x06003BE2 RID: 15330 RVA: 0x001028E5 File Offset: 0x00100AE5
		internal bool NeedsIDC
		{
			get
			{
				return this.m_joinInfo != null;
			}
		}

		// Token: 0x170019BD RID: 6589
		// (get) Token: 0x06003BE3 RID: 15331 RVA: 0x001028F0 File Offset: 0x00100AF0
		internal JoinInfo JoinInfo
		{
			get
			{
				return this.m_joinInfo;
			}
		}

		// Token: 0x170019BE RID: 6590
		// (get) Token: 0x06003BE4 RID: 15332 RVA: 0x001028F8 File Offset: 0x00100AF8
		internal List<int> GroupingFieldIndicesForServerAggregates
		{
			get
			{
				return this.m_groupingFieldIndicesForServerAggregates;
			}
		}

		// Token: 0x06003BE5 RID: 15333 RVA: 0x00102900 File Offset: 0x00100B00
		internal void ApplyGroupingFieldsForServerAggregates(FieldsImpl fields)
		{
			if (this.m_groupingFieldIndicesForServerAggregates == null)
			{
				return;
			}
			for (int i = 0; i < this.m_groupingFieldIndicesForServerAggregates.Count; i++)
			{
				fields.ConsumeAggregationField(this.m_groupingFieldIndicesForServerAggregates[i]);
			}
		}

		// Token: 0x06003BE6 RID: 15334 RVA: 0x0010293E File Offset: 0x00100B3E
		internal void SetRelationship(string dataSetName, IdcRelationship relationship)
		{
			this.m_dataSetName = dataSetName;
			if (relationship == null)
			{
				return;
			}
			this.m_joinInfo = new LinearJoinInfo(relationship);
		}

		// Token: 0x06003BE7 RID: 15335 RVA: 0x00102957 File Offset: 0x00100B57
		internal void SetRelationship(string dataSetName, List<IdcRelationship> relationships)
		{
			this.m_dataSetName = dataSetName;
			if (relationships == null)
			{
				return;
			}
			this.m_joinInfo = new IntersectJoinInfo(relationships);
		}

		// Token: 0x06003BE8 RID: 15336 RVA: 0x00102970 File Offset: 0x00100B70
		internal void ValidateScopeRulesForIdc(InitializationContext context, IRIFDataScope dataScope)
		{
			if (this.m_dataSet == null)
			{
				return;
			}
			if (this.NeedsIDC)
			{
				this.m_joinInfo.ValidateScopeRulesForIdcNaturalJoin(context, dataScope);
				context.EnsureDataSetUsedOnceForIdcUnderTopDataRegion(this.m_dataSet, dataScope);
			}
		}

		// Token: 0x06003BE9 RID: 15337 RVA: 0x001029A0 File Offset: 0x00100BA0
		internal void ValidateDataSetBindingAndRelationships(ScopeTree scopeTree, IRIFReportDataScope scope, ErrorContext errorContext)
		{
			if (this.m_dataSet != null)
			{
				return;
			}
			ParentDataSetContainer parentDataSetContainer = DataScopeInfo.DetermineParentDataSets(scopeTree, scope);
			if (this.m_dataSetName == null)
			{
				this.BindToParentDataSet(scopeTree, scope, errorContext, parentDataSetContainer);
			}
			else
			{
				this.BindToNamedDataSet(scopeTree, scope, errorContext, parentDataSetContainer);
			}
			if (this.m_dataSet != null)
			{
				if (scopeTree.GetParentDataRegion(scope) == null && this.m_joinInfo != null)
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidRelationshipTopLevelDataRegion, Severity.Error, scope.DataScopeObjectType, scope.Name, "Relationship", Array.Empty<string>());
					this.m_joinInfo = null;
				}
				if (parentDataSetContainer != null && this.m_joinInfo == null)
				{
					if (parentDataSetContainer.Count == 1)
					{
						this.m_joinInfo = new LinearJoinInfo();
					}
					else
					{
						this.m_joinInfo = new IntersectJoinInfo();
					}
				}
				if (this.m_joinInfo != null)
				{
					if (!this.m_joinInfo.ValidateRelationships(scopeTree, errorContext, this.m_dataSet, parentDataSetContainer, scope))
					{
						this.m_joinInfo = null;
					}
					if (this.m_joinInfo == null && this.m_dataSetName != null && this.m_dataSet != null && !DataSet.AreEqualById(parentDataSetContainer.ParentDataSet, this.m_dataSet))
					{
						this.UpdateDataSet(parentDataSetContainer.ParentDataSet, scope);
					}
				}
			}
		}

		// Token: 0x06003BEA RID: 15338 RVA: 0x00102AAC File Offset: 0x00100CAC
		private void BindToNamedDataSet(ScopeTree scopeTree, IRIFReportDataScope scope, ErrorContext errorContext, ParentDataSetContainer parentDataSets)
		{
			DataSet dataSet = scopeTree.GetDataSet(this.m_dataSetName);
			if (dataSet == null)
			{
				DataRegion parentDataRegion = scopeTree.GetParentDataRegion(scope);
				if (parentDataSets != null && parentDataSets.Count == 1 && scope is DataRegion && parentDataRegion != null)
				{
					this.UpdateDataSet(parentDataSets.ParentDataSet, scope);
					this.m_dataSetName = parentDataSets.ParentDataSet.Name;
					return;
				}
				errorContext.Register(ProcessingErrorCode.rsInvalidDataSetName, Severity.Error, scope.DataScopeObjectType, scope.Name, "DataSetName", new string[] { this.m_dataSetName });
				if (parentDataSets != null)
				{
					this.UpdateDataSet(parentDataSets.ParentDataSet, scope);
					return;
				}
			}
			else
			{
				this.UpdateDataSet(dataSet, scope);
			}
		}

		// Token: 0x06003BEB RID: 15339 RVA: 0x00102B50 File Offset: 0x00100D50
		private void BindToParentDataSet(ScopeTree scopeTree, IRIFReportDataScope scope, ErrorContext errorContext, ParentDataSetContainer parentDataSets)
		{
			if (parentDataSets == null)
			{
				if (scopeTree.GetParentDataRegion(scope) == null)
				{
					if (scopeTree.Report.MappingDataSetIndexToDataSet.Count == 0)
					{
						errorContext.Register(ProcessingErrorCode.rsDataRegionWithoutDataSet, Severity.Error, scope.DataScopeObjectType, scope.Name, null, Array.Empty<string>());
						return;
					}
					errorContext.Register(ProcessingErrorCode.rsMissingDataSetName, Severity.Error, scope.DataScopeObjectType, scope.Name, "DataSetName", Array.Empty<string>());
					return;
				}
			}
			else
			{
				if (parentDataSets.Count > 1 && !parentDataSets.AreAllSameDataSet())
				{
					DataRegion parentDataRegion = scopeTree.GetParentDataRegion(scope);
					IRIFDataScope parentRowScopeForIntersection = scopeTree.GetParentRowScopeForIntersection(scope);
					IRIFDataScope parentColumnScopeForIntersection = scopeTree.GetParentColumnScopeForIntersection(scope);
					errorContext.Register(ProcessingErrorCode.rsMissingIntersectionDataSetName, Severity.Error, scope.DataScopeObjectType, parentDataRegion.Name, "DataSetName", new string[]
					{
						parentDataRegion.ObjectType.ToString(),
						parentRowScopeForIntersection.Name,
						parentColumnScopeForIntersection.Name
					});
					return;
				}
				this.UpdateDataSet(parentDataSets.ParentDataSet, scope);
			}
		}

		// Token: 0x06003BEC RID: 15340 RVA: 0x00102C48 File Offset: 0x00100E48
		private static ParentDataSetContainer DetermineParentDataSets(ScopeTree scopeTree, IRIFReportDataScope scope)
		{
			if (scopeTree.IsIntersectionScope(scope))
			{
				IRIFDataScope parentRowScopeForIntersection = scopeTree.GetParentRowScopeForIntersection(scope);
				IRIFDataScope parentColumnScopeForIntersection = scopeTree.GetParentColumnScopeForIntersection(scope);
				return new ParentDataSetContainer(parentRowScopeForIntersection.DataScopeInfo.DataSet, parentColumnScopeForIntersection.DataScopeInfo.DataSet);
			}
			IRIFDataScope parentScope = scopeTree.GetParentScope(scope);
			DataSet dataSet;
			if (parentScope == null)
			{
				dataSet = scopeTree.GetDefaultTopLevelDataSet();
			}
			else
			{
				dataSet = parentScope.DataScopeInfo.DataSet;
			}
			if (dataSet == null)
			{
				return null;
			}
			return new ParentDataSetContainer(dataSet);
		}

		// Token: 0x06003BED RID: 15341 RVA: 0x00102CB4 File Offset: 0x00100EB4
		private void UpdateDataSet(DataSet targetDataSet, IRIFDataScope scope)
		{
			this.m_dataSet = targetDataSet;
			DataRegion dataRegion = scope as DataRegion;
			if (dataRegion != null && this.m_dataSet != null)
			{
				dataRegion.DataSetName = this.m_dataSet.Name;
			}
		}

		// Token: 0x06003BEE RID: 15342 RVA: 0x00102CEC File Offset: 0x00100EEC
		internal void ClearAggregatesIfEmpty()
		{
			Global.Tracer.Assert(this.m_aggregatesOfAggregates != null, "(null != m_aggregatesOfAggregates)");
			if (this.m_aggregatesOfAggregates.IsEmpty)
			{
				this.m_aggregatesOfAggregates = null;
			}
			Global.Tracer.Assert(this.m_postSortAggregatesOfAggregates != null, "(null != m_postSortAggregatesOfAggregates)");
			if (this.m_postSortAggregatesOfAggregates.IsEmpty)
			{
				this.m_postSortAggregatesOfAggregates = null;
			}
		}

		// Token: 0x06003BEF RID: 15343 RVA: 0x00102D51 File Offset: 0x00100F51
		internal void ClearRunningValuesIfEmpty()
		{
			Global.Tracer.Assert(this.m_runningValuesOfAggregates != null, "(null != m_runningValuesOfAggregates)");
			if (this.m_runningValuesOfAggregates.Count == 0)
			{
				this.m_runningValuesOfAggregates.Clear();
			}
		}

		// Token: 0x06003BF0 RID: 15344 RVA: 0x00102D84 File Offset: 0x00100F84
		public void MergeFrom(DataScopeInfo otherScope)
		{
			this.m_aggregatesSpanGroupFilter |= otherScope.m_aggregatesSpanGroupFilter;
			this.m_hasAggregatesToUpdateAtRowScope |= otherScope.m_hasAggregatesToUpdateAtRowScope;
			this.m_runningValuesOfAggregates.AddRange(otherScope.m_runningValuesOfAggregates);
			this.m_aggregatesOfAggregates.MergeFrom(otherScope.m_aggregatesOfAggregates);
			this.m_postSortAggregatesOfAggregates.MergeFrom(otherScope.m_postSortAggregatesOfAggregates);
		}

		// Token: 0x06003BF1 RID: 15345 RVA: 0x00102DEC File Offset: 0x00100FEC
		internal DataScopeInfo PublishClone(AutomaticSubtotalContext context, int scopeID)
		{
			DataScopeInfo dataScopeInfo = new DataScopeInfo(scopeID);
			dataScopeInfo.m_dataSetName = this.m_dataSetName;
			if (this.m_joinInfo != null)
			{
				dataScopeInfo.m_joinInfo = this.m_joinInfo.PublishClone(context);
			}
			return dataScopeInfo;
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x00102E27 File Offset: 0x00101027
		internal void Initialize(InitializationContext context, IRIFDataScope scope)
		{
			if (this.m_joinInfo != null)
			{
				this.m_joinInfo.Initialize(context);
				this.InjectAggregateIndicatorFieldJoinConditions(context, scope);
			}
			this.CaptureGroupingFieldsForServerAggregates(context, scope);
		}

		// Token: 0x06003BF3 RID: 15347 RVA: 0x00102E50 File Offset: 0x00101050
		private void CaptureGroupingFieldsForServerAggregates(InitializationContext context, IRIFDataScope scope)
		{
			if (this.m_groupingFieldIndicesForServerAggregates != null)
			{
				return;
			}
			this.m_groupingFieldIndicesForServerAggregates = new List<int>();
			if (this.m_dataSet == null)
			{
				return;
			}
			ScopeTree scopeTree = context.ScopeTree;
			if (scopeTree.IsIntersectionScope(scope))
			{
				this.AddGroupingFieldIndicesFromParentScope(context, scopeTree.GetParentRowScopeForIntersection(scope));
				this.AddGroupingFieldIndicesFromParentScope(context, scopeTree.GetParentColumnScopeForIntersection(scope));
				return;
			}
			IRIFDataScope parentScope = scopeTree.GetParentScope(scope);
			if (parentScope != null)
			{
				this.AddGroupingFieldIndicesFromParentScope(context, parentScope);
			}
			ReportHierarchyNode reportHierarchyNode = scope as ReportHierarchyNode;
			if (reportHierarchyNode != null && !reportHierarchyNode.IsStatic && !reportHierarchyNode.Grouping.IsDetail)
			{
				foreach (ExpressionInfo expressionInfo in reportHierarchyNode.Grouping.GroupExpressions)
				{
					if (expressionInfo.Type == ExpressionInfo.Types.Field)
					{
						this.m_groupingFieldIndicesForServerAggregates.Add(expressionInfo.FieldIndex);
					}
				}
			}
		}

		// Token: 0x06003BF4 RID: 15348 RVA: 0x00102F3C File Offset: 0x0010113C
		private void AddGroupingFieldIndicesFromParentScope(InitializationContext context, IRIFDataScope parentScope)
		{
			Global.Tracer.Assert(parentScope.DataScopeInfo.GroupingFieldIndicesForServerAggregates != null, "Grouping fields for parent should have been captured first.");
			if (DataSet.AreEqualById(parentScope.DataScopeInfo.DataSet, this.m_dataSet))
			{
				this.m_groupingFieldIndicesForServerAggregates.AddRange(parentScope.DataScopeInfo.GroupingFieldIndicesForServerAggregates);
				return;
			}
			if (this.m_joinInfo == null)
			{
				Global.Tracer.Assert(context.ErrorContext.HasError, "Missing expected error.");
				return;
			}
			this.m_joinInfo.AddMappedFieldIndices(parentScope.DataScopeInfo.GroupingFieldIndicesForServerAggregates, parentScope.DataScopeInfo.DataSet, this.m_dataSet, this.m_groupingFieldIndicesForServerAggregates);
		}

		// Token: 0x06003BF5 RID: 15349 RVA: 0x00102FE8 File Offset: 0x001011E8
		private void InjectAggregateIndicatorFieldJoinConditions(InitializationContext context, IRIFDataScope scope)
		{
			if (this.m_dataSet == null)
			{
				return;
			}
			if (this.m_joinInfo.Relationships == null)
			{
				return;
			}
			PublishingContextKind publishingContextKind = context.PublishingContext.PublishingContextKind;
			if (context.PublishingContext.PublishingContextKind == PublishingContextKind.DataShape)
			{
				return;
			}
			if (scope.DataScopeObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Grouping)
			{
				return;
			}
			int count = this.m_dataSet.Fields.Count;
			foreach (IdcRelationship idcRelationship in this.m_joinInfo.Relationships)
			{
				if (!idcRelationship.IsCrossJoin)
				{
					for (int i = 0; i < count; i++)
					{
						Field field = this.m_dataSet.Fields[i];
						if (field.AggregateIndicatorFieldIndex >= 0)
						{
							Field field2 = this.m_dataSet.Fields[field.AggregateIndicatorFieldIndex];
							if (!field2.IsCalculatedField)
							{
								idcRelationship.InsertAggregateIndicatorJoinCondition(field, i, field2, field.AggregateIndicatorFieldIndex, context);
							}
						}
					}
				}
			}
		}

		// Token: 0x06003BF6 RID: 15350 RVA: 0x001030F0 File Offset: 0x001012F0
		internal long AssignScopeInstanceNumber()
		{
			this.m_lastScopeInstanceNumber += 1L;
			return this.m_lastScopeInstanceNumber;
		}

		// Token: 0x06003BF7 RID: 15351 RVA: 0x00103107 File Offset: 0x00101307
		internal bool IsLastScopeInstanceNumber(long scopeInstanceNumber)
		{
			return this.m_lastScopeInstanceNumber == scopeInstanceNumber;
		}

		// Token: 0x06003BF8 RID: 15352 RVA: 0x00103112 File Offset: 0x00101312
		internal void ResetAggregates(AggregatesImpl reportOmAggregates)
		{
			reportOmAggregates.ResetAll<DataAggregateInfo>(this.m_aggregatesOfAggregates);
			reportOmAggregates.ResetAll<DataAggregateInfo>(this.m_postSortAggregatesOfAggregates);
			reportOmAggregates.ResetAll<RunningValueInfo>(this.m_runningValuesOfAggregates);
		}

		// Token: 0x06003BF9 RID: 15353 RVA: 0x00103138 File Offset: 0x00101338
		internal bool IsSameScope(DataScopeInfo candidateScopeInfo)
		{
			return this.m_scopeID == candidateScopeInfo.ScopeID;
		}

		// Token: 0x06003BFA RID: 15354 RVA: 0x00103148 File Offset: 0x00101348
		internal static bool IsSameOrChildScope(IRIFReportDataScope childCandidate, IRIFReportDataScope parentCandidate)
		{
			while (childCandidate != null)
			{
				if (childCandidate.DataScopeInfo.ScopeID == parentCandidate.DataScopeInfo.ScopeID)
				{
					return true;
				}
				if (childCandidate.IsDataIntersectionScope)
				{
					IRIFReportIntersectionScope irifreportIntersectionScope = (IRIFReportIntersectionScope)childCandidate;
					return DataScopeInfo.IsSameOrChildScope(irifreportIntersectionScope.ParentRowReportScope, parentCandidate) || DataScopeInfo.IsSameOrChildScope(irifreportIntersectionScope.ParentColumnReportScope, parentCandidate);
				}
				childCandidate = childCandidate.ParentReportScope;
			}
			return false;
		}

		// Token: 0x06003BFB RID: 15355 RVA: 0x001031A9 File Offset: 0x001013A9
		internal static bool IsChildScopeOf(IRIFReportDataScope childCandidate, IRIFReportDataScope parentCandidate)
		{
			return !childCandidate.DataScopeInfo.IsSameScope(parentCandidate.DataScopeInfo) && DataScopeInfo.IsSameOrChildScope(childCandidate, parentCandidate);
		}

		// Token: 0x06003BFC RID: 15356 RVA: 0x001031C8 File Offset: 0x001013C8
		internal static bool HasDecomposableAncestorWithNonLatestInstanceBinding(IRIFReportDataScope candidate)
		{
			while (candidate != null)
			{
				if (candidate.IsScope && candidate.DataScopeInfo.IsDecomposable && candidate.IsBoundToStreamingScopeInstance && !candidate.CurrentStreamingScopeInstance.Value().IsMostRecentlyCreatedScopeInstance)
				{
					return true;
				}
				if (candidate.IsDataIntersectionScope)
				{
					IRIFReportIntersectionScope irifreportIntersectionScope = (IRIFReportIntersectionScope)candidate;
					return DataScopeInfo.HasDecomposableAncestorWithNonLatestInstanceBinding(irifreportIntersectionScope.ParentRowReportScope) || DataScopeInfo.HasDecomposableAncestorWithNonLatestInstanceBinding(irifreportIntersectionScope.ParentColumnReportScope);
				}
				candidate = candidate.ParentReportScope;
			}
			return false;
		}

		// Token: 0x06003BFD RID: 15357 RVA: 0x00103240 File Offset: 0x00101440
		internal static bool TryGetInnermostParentScopeRelatedToTargetDataSet(DataSet targetDataSet, IRIFReportDataScope candidate, out IRIFReportDataScope targetScope)
		{
			while (candidate != null)
			{
				if (targetDataSet.HasDefaultRelationship(candidate.DataScopeInfo.DataSet))
				{
					targetScope = candidate;
					return true;
				}
				if (candidate.IsDataIntersectionScope)
				{
					IRIFReportIntersectionScope irifreportIntersectionScope = (IRIFReportIntersectionScope)candidate;
					return DataScopeInfo.TryGetInnermostParentScopeRelatedToTargetDataSet(targetDataSet, irifreportIntersectionScope.ParentRowReportScope, out targetScope) || DataScopeInfo.TryGetInnermostParentScopeRelatedToTargetDataSet(targetDataSet, irifreportIntersectionScope.ParentColumnReportScope, out targetScope);
				}
				candidate = candidate.ParentReportScope;
			}
			targetScope = null;
			return false;
		}

		// Token: 0x06003BFE RID: 15358 RVA: 0x001032A4 File Offset: 0x001014A4
		internal static bool HasAggregates<T>(List<T> aggs) where T : DataAggregateInfo
		{
			return aggs != null && aggs.Count > 0;
		}

		// Token: 0x06003BFF RID: 15359 RVA: 0x001032B4 File Offset: 0x001014B4
		internal static bool HasNonServerAggregates<T>(List<T> aggs) where T : DataAggregateInfo
		{
			if (aggs != null)
			{
				return aggs.Any((T agg) => agg.AggregateType != DataAggregateInfo.AggregateTypes.Aggregate);
			}
			return false;
		}

		// Token: 0x06003C00 RID: 15360 RVA: 0x001032E0 File Offset: 0x001014E0
		internal static bool HasAggregates<T>(BucketedAggregatesCollection<T> aggs) where T : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
		{
			return aggs != null && !aggs.IsEmpty;
		}

		// Token: 0x06003C01 RID: 15361 RVA: 0x001032F0 File Offset: 0x001014F0
		internal static bool ContainsServerAggregate<T>(List<T> aggs, string aggregateName) where T : DataAggregateInfo
		{
			return aggs != null && aggs.Any((T agg) => DataScopeInfo.IsTargetServerAggregate(agg, aggregateName));
		}

		// Token: 0x06003C02 RID: 15362 RVA: 0x00103321 File Offset: 0x00101521
		internal static bool IsTargetServerAggregate(DataAggregateInfo agg, string aggregateName)
		{
			return agg.AggregateType == DataAggregateInfo.AggregateTypes.Aggregate && (string.Equals(agg.Name, aggregateName, StringComparison.Ordinal) || (agg.DuplicateNames != null && ListUtils.ContainsWithOrdinalComparer(aggregateName, agg.DuplicateNames)));
		}

		// Token: 0x06003C03 RID: 15363 RVA: 0x00103358 File Offset: 0x00101558
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataScopeInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.AggregatesSpanGroupFilter, Token.Boolean),
				new MemberInfo(MemberName.AggregatesOfAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BucketedDataAggregateInfos),
				new MemberInfo(MemberName.PostSortAggregatesOfAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BucketedDataAggregateInfos),
				new MemberInfo(MemberName.RunningValuesOfAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RunningValueInfo),
				new MemberInfo(MemberName.ScopeID, Token.Int32),
				new MemberInfo(MemberName.HasAggregatesToUpdateAtRowScope, Token.Boolean),
				new MemberInfo(MemberName.IsDecomposable, Token.Boolean),
				new MemberInfo(MemberName.DataSet, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataSet, Token.Reference),
				new MemberInfo(MemberName.JoinInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.JoinInfo),
				new MemberInfo(MemberName.DataPipelineID, Token.Int32),
				new MemberInfo(MemberName.GroupingFieldIndicesForServerAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int32)
			});
		}

		// Token: 0x06003C04 RID: 15364 RVA: 0x00103464 File Offset: 0x00101664
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(DataScopeInfo.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.HasAggregatesToUpdateAtRowScope)
				{
					if (memberName <= MemberName.AggregatesSpanGroupFilter)
					{
						if (memberName == MemberName.DataSet)
						{
							writer.WriteReference(this.m_dataSet);
							continue;
						}
						switch (memberName)
						{
						case MemberName.AggregatesOfAggregates:
							writer.Write(this.m_aggregatesOfAggregates);
							continue;
						case MemberName.PostSortAggregatesOfAggregates:
							writer.Write(this.m_postSortAggregatesOfAggregates);
							continue;
						case MemberName.RunningValuesOfAggregates:
							writer.Write<RunningValueInfo>(this.m_runningValuesOfAggregates);
							continue;
						case MemberName.AggregatesSpanGroupFilter:
							writer.Write(this.m_aggregatesSpanGroupFilter);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ScopeID)
						{
							writer.Write(this.m_scopeID);
							continue;
						}
						if (memberName == MemberName.HasAggregatesToUpdateAtRowScope)
						{
							writer.Write(this.m_hasAggregatesToUpdateAtRowScope);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.JoinInfo)
				{
					if (memberName == MemberName.IsDecomposable)
					{
						writer.Write(this.m_isDecomposable);
						continue;
					}
					if (memberName == MemberName.JoinInfo)
					{
						writer.Write(this.m_joinInfo);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DataPipelineID)
					{
						writer.Write(this.m_dataPipelineID);
						continue;
					}
					if (memberName == MemberName.GroupingFieldIndicesForServerAggregates)
					{
						writer.WriteListOfPrimitives<int>(this.m_groupingFieldIndicesForServerAggregates);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003C05 RID: 15365 RVA: 0x001035F4 File Offset: 0x001017F4
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(DataScopeInfo.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.HasAggregatesToUpdateAtRowScope)
				{
					if (memberName <= MemberName.AggregatesSpanGroupFilter)
					{
						if (memberName == MemberName.DataSet)
						{
							this.m_dataSet = reader.ReadReference<DataSet>(this);
							continue;
						}
						switch (memberName)
						{
						case MemberName.AggregatesOfAggregates:
							this.m_aggregatesOfAggregates = (BucketedDataAggregateInfos)reader.ReadRIFObject();
							continue;
						case MemberName.PostSortAggregatesOfAggregates:
							this.m_postSortAggregatesOfAggregates = (BucketedDataAggregateInfos)reader.ReadRIFObject();
							continue;
						case MemberName.RunningValuesOfAggregates:
							this.m_runningValuesOfAggregates = reader.ReadGenericListOfRIFObjects<RunningValueInfo>();
							continue;
						case MemberName.AggregatesSpanGroupFilter:
							this.m_aggregatesSpanGroupFilter = reader.ReadBoolean();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ScopeID)
						{
							this.m_scopeID = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.HasAggregatesToUpdateAtRowScope)
						{
							this.m_hasAggregatesToUpdateAtRowScope = reader.ReadBoolean();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.JoinInfo)
				{
					if (memberName == MemberName.IsDecomposable)
					{
						this.m_isDecomposable = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.JoinInfo)
					{
						this.m_joinInfo = (JoinInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DataPipelineID)
					{
						this.m_dataPipelineID = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.GroupingFieldIndicesForServerAggregates)
					{
						this.m_groupingFieldIndicesForServerAggregates = reader.ReadListOfPrimitives<int>();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003C06 RID: 15366 RVA: 0x00103794 File Offset: 0x00101994
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(DataScopeInfo.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.DataSet)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						Global.Tracer.Assert(referenceableItems[memberReference.RefID] is DataSet);
						Global.Tracer.Assert(this.m_dataSet != (DataSet)referenceableItems[memberReference.RefID]);
						this.m_dataSet = (DataSet)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06003C07 RID: 15367 RVA: 0x00103884 File Offset: 0x00101A84
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataScopeInfo;
		}

		// Token: 0x04001C3E RID: 7230
		private bool m_aggregatesSpanGroupFilter;

		// Token: 0x04001C3F RID: 7231
		private bool m_hasAggregatesToUpdateAtRowScope;

		// Token: 0x04001C40 RID: 7232
		private BucketedDataAggregateInfos m_aggregatesOfAggregates;

		// Token: 0x04001C41 RID: 7233
		private BucketedDataAggregateInfos m_postSortAggregatesOfAggregates;

		// Token: 0x04001C42 RID: 7234
		private List<RunningValueInfo> m_runningValuesOfAggregates;

		// Token: 0x04001C43 RID: 7235
		private int m_scopeID;

		// Token: 0x04001C44 RID: 7236
		private int m_dataPipelineID = -1;

		// Token: 0x04001C45 RID: 7237
		[Reference]
		private DataSet m_dataSet;

		// Token: 0x04001C46 RID: 7238
		private bool m_isDecomposable;

		// Token: 0x04001C47 RID: 7239
		private JoinInfo m_joinInfo;

		// Token: 0x04001C48 RID: 7240
		private List<int> m_groupingFieldIndicesForServerAggregates;

		// Token: 0x04001C49 RID: 7241
		[NonSerialized]
		private string m_dataSetName;

		// Token: 0x04001C4A RID: 7242
		[NonSerialized]
		private long m_lastScopeInstanceNumber;

		// Token: 0x04001C4B RID: 7243
		[NonSerialized]
		internal const int DataPipelineIDUnassigned = -1;

		// Token: 0x04001C4C RID: 7244
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DataScopeInfo.GetDeclaration();
	}
}

using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200047A RID: 1146
	public abstract class Cell : IDOwner, IAggregateHolder, IRunningValueHolder, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IIndexedInCollection, IRIFReportScope, IInstancePath, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGloballyReferenceable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGlobalIDOwner, IRIFDataScope, IRIFReportIntersectionScope, IRIFReportDataScope
	{
		// Token: 0x060034B9 RID: 13497 RVA: 0x000E7AAC File Offset: 0x000E5CAC
		internal Cell()
		{
		}

		// Token: 0x060034BA RID: 13498 RVA: 0x000E7AD0 File Offset: 0x000E5CD0
		internal Cell(int id, DataRegion dataRegion)
			: base(id)
		{
			this.m_dataRegionDef = dataRegion;
			this.m_aggregates = new List<DataAggregateInfo>();
			this.m_postSortAggregates = new List<DataAggregateInfo>();
			this.m_runningValues = new List<RunningValueInfo>();
			this.m_dataScopeInfo = new DataScopeInfo(id);
		}

		// Token: 0x17001777 RID: 6007
		// (get) Token: 0x060034BB RID: 13499 RVA: 0x000E7B34 File Offset: 0x000E5D34
		string IRIFDataScope.Name
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001778 RID: 6008
		// (get) Token: 0x060034BC RID: 13500 RVA: 0x000E7B37 File Offset: 0x000E5D37
		public DataScopeInfo DataScopeInfo
		{
			get
			{
				return this.m_dataScopeInfo;
			}
		}

		// Token: 0x17001779 RID: 6009
		// (get) Token: 0x060034BD RID: 13501
		public abstract Microsoft.ReportingServices.ReportProcessing.ObjectType DataScopeObjectType { get; }

		// Token: 0x1700177A RID: 6010
		// (get) Token: 0x060034BE RID: 13502 RVA: 0x000E7B3F File Offset: 0x000E5D3F
		// (set) Token: 0x060034BF RID: 13503 RVA: 0x000E7B47 File Offset: 0x000E5D47
		internal DataScopeInfo CanonicalDataScopeInfo
		{
			get
			{
				return this.m_canonicalDataScopeInfo;
			}
			set
			{
				this.m_canonicalDataScopeInfo = value;
			}
		}

		// Token: 0x1700177B RID: 6011
		// (get) Token: 0x060034C0 RID: 13504 RVA: 0x000E7B50 File Offset: 0x000E5D50
		// (set) Token: 0x060034C1 RID: 13505 RVA: 0x000E7B58 File Offset: 0x000E5D58
		internal int ExpressionHostID
		{
			get
			{
				return this.m_exprHostID;
			}
			set
			{
				this.m_exprHostID = value;
			}
		}

		// Token: 0x1700177C RID: 6012
		// (get) Token: 0x060034C2 RID: 13506 RVA: 0x000E7B61 File Offset: 0x000E5D61
		internal int ParentRowMemberID
		{
			get
			{
				return this.m_parentRowID;
			}
		}

		// Token: 0x1700177D RID: 6013
		// (get) Token: 0x060034C3 RID: 13507 RVA: 0x000E7B69 File Offset: 0x000E5D69
		internal int ParentColumnMemberID
		{
			get
			{
				return this.m_parentColumnID;
			}
		}

		// Token: 0x1700177E RID: 6014
		// (get) Token: 0x060034C4 RID: 13508 RVA: 0x000E7B71 File Offset: 0x000E5D71
		internal DataRegion DataRegionDef
		{
			get
			{
				return this.m_dataRegionDef;
			}
		}

		// Token: 0x1700177F RID: 6015
		// (get) Token: 0x060034C5 RID: 13509 RVA: 0x000E7B79 File Offset: 0x000E5D79
		internal List<int> AggregateIndexes
		{
			get
			{
				return this.m_aggregateIndexes;
			}
		}

		// Token: 0x17001780 RID: 6016
		// (get) Token: 0x060034C6 RID: 13510 RVA: 0x000E7B81 File Offset: 0x000E5D81
		internal List<int> PostSortAggregateIndexes
		{
			get
			{
				return this.m_postSortAggregateIndexes;
			}
		}

		// Token: 0x17001781 RID: 6017
		// (get) Token: 0x060034C7 RID: 13511 RVA: 0x000E7B89 File Offset: 0x000E5D89
		internal List<int> RunningValueIndexes
		{
			get
			{
				return this.m_runningValueIndexes;
			}
		}

		// Token: 0x17001782 RID: 6018
		// (get) Token: 0x060034C8 RID: 13512 RVA: 0x000E7B91 File Offset: 0x000E5D91
		internal bool HasInnerGroupTreeHierarchy
		{
			get
			{
				return this.m_hasInnerGroupTreeHierarchy;
			}
		}

		// Token: 0x17001783 RID: 6019
		// (get) Token: 0x060034C9 RID: 13513 RVA: 0x000E7B9C File Offset: 0x000E5D9C
		internal bool SimpleGroupTreeCell
		{
			get
			{
				return !this.m_hasInnerGroupTreeHierarchy && this.m_aggregateIndexes == null && this.m_postSortAggregateIndexes == null && this.m_runningValueIndexes == null && (this.m_dataScopeInfo == null || !this.m_dataScopeInfo.HasAggregatesOrRunningValues) && !this.m_dataRegionDef.IsMatrixIDC;
			}
		}

		// Token: 0x17001784 RID: 6020
		// (get) Token: 0x060034CA RID: 13514 RVA: 0x000E7BEE File Offset: 0x000E5DEE
		internal List<DataAggregateInfo> Aggregates
		{
			get
			{
				return this.m_aggregates;
			}
		}

		// Token: 0x17001785 RID: 6021
		// (get) Token: 0x060034CB RID: 13515 RVA: 0x000E7BF6 File Offset: 0x000E5DF6
		internal List<DataAggregateInfo> PostSortAggregates
		{
			get
			{
				return this.m_postSortAggregates;
			}
		}

		// Token: 0x17001786 RID: 6022
		// (get) Token: 0x060034CC RID: 13516 RVA: 0x000E7BFE File Offset: 0x000E5DFE
		internal List<RunningValueInfo> RunningValues
		{
			get
			{
				return this.m_runningValues;
			}
		}

		// Token: 0x17001787 RID: 6023
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x000E7C08 File Offset: 0x000E5E08
		public override List<InstancePathItem> InstancePath
		{
			get
			{
				if (this.m_cachedInstancePath == null)
				{
					if (this.m_parentColumnIDOwner == null)
					{
						return base.InstancePath;
					}
					this.m_cachedInstancePath = InstancePathItem.CombineRowColPath(base.InstancePath, this.m_parentColumnIDOwner.InstancePath);
					this.m_cachedInstancePath.Add(base.InstancePathItem);
				}
				return this.m_cachedInstancePath;
			}
		}

		// Token: 0x060034CE RID: 13518 RVA: 0x000E7C5F File Offset: 0x000E5E5F
		protected override InstancePathItem CreateInstancePathItem()
		{
			return new InstancePathItem(InstancePathItemType.Cell, this.IndexInCollection);
		}

		// Token: 0x17001788 RID: 6024
		// (get) Token: 0x060034CF RID: 13519 RVA: 0x000E7C6D File Offset: 0x000E5E6D
		protected virtual bool IsDataRegionBodyCell
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001789 RID: 6025
		// (get) Token: 0x060034D0 RID: 13520 RVA: 0x000E7C70 File Offset: 0x000E5E70
		// (set) Token: 0x060034D1 RID: 13521 RVA: 0x000E7C78 File Offset: 0x000E5E78
		public int IndexInCollection
		{
			get
			{
				return this.m_indexInCollection;
			}
			set
			{
				this.m_indexInCollection = value;
			}
		}

		// Token: 0x1700178A RID: 6026
		// (get) Token: 0x060034D2 RID: 13522 RVA: 0x000E7C81 File Offset: 0x000E5E81
		public IndexedInCollectionType IndexedInCollectionType
		{
			get
			{
				return IndexedInCollectionType.Cell;
			}
		}

		// Token: 0x1700178B RID: 6027
		// (get) Token: 0x060034D3 RID: 13523 RVA: 0x000E7C84 File Offset: 0x000E5E84
		internal List<IInScopeEventSource> InScopeEventSources
		{
			get
			{
				return this.m_inScopeEventSources;
			}
		}

		// Token: 0x1700178C RID: 6028
		// (get) Token: 0x060034D4 RID: 13524 RVA: 0x000E7C8C File Offset: 0x000E5E8C
		internal bool InDynamicRowAndColumnContext
		{
			get
			{
				return this.m_inDynamicRowAndColumnContext;
			}
		}

		// Token: 0x1700178D RID: 6029
		// (get) Token: 0x060034D5 RID: 13525 RVA: 0x000E7C94 File Offset: 0x000E5E94
		internal virtual List<ReportItem> CellContentCollection
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700178E RID: 6030
		// (get) Token: 0x060034D6 RID: 13526 RVA: 0x000E7C97 File Offset: 0x000E5E97
		// (set) Token: 0x060034D7 RID: 13527 RVA: 0x000E7C9F File Offset: 0x000E5E9F
		bool IRIFReportScope.NeedToCacheDataRows
		{
			get
			{
				return this.m_needToCacheDataRows;
			}
			set
			{
				if (!this.m_needToCacheDataRows)
				{
					this.m_needToCacheDataRows = value;
				}
			}
		}

		// Token: 0x060034D8 RID: 13528 RVA: 0x000E7CB0 File Offset: 0x000E5EB0
		bool IRIFReportScope.VariableInScope(int sequenceIndex)
		{
			return SequenceIndex.GetBit(this.m_variablesInScope, sequenceIndex, true);
		}

		// Token: 0x060034D9 RID: 13529 RVA: 0x000E7CBF File Offset: 0x000E5EBF
		bool IRIFReportScope.TextboxInScope(int sequenceIndex)
		{
			return SequenceIndex.GetBit(this.m_textboxesInScope, sequenceIndex, true);
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x000E7CCE File Offset: 0x000E5ECE
		void IRIFReportScope.ResetTextBoxImpls(OnDemandProcessingContext context)
		{
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x000E7CD0 File Offset: 0x000E5ED0
		void IRIFReportScope.AddInScopeTextBox(TextBox textbox)
		{
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x000E7CD2 File Offset: 0x000E5ED2
		void IRIFReportScope.AddInScopeEventSource(IInScopeEventSource eventSource)
		{
			if (this.m_inScopeEventSources == null)
			{
				this.m_inScopeEventSources = new List<IInScopeEventSource>();
			}
			this.m_inScopeEventSources.Add(eventSource);
		}

		// Token: 0x1700178F RID: 6031
		// (get) Token: 0x060034DD RID: 13533 RVA: 0x000E7CF3 File Offset: 0x000E5EF3
		public bool IsDataIntersectionScope
		{
			get
			{
				return this.InDynamicRowAndColumnContext;
			}
		}

		// Token: 0x17001790 RID: 6032
		// (get) Token: 0x060034DE RID: 13534 RVA: 0x000E7CFB File Offset: 0x000E5EFB
		public bool IsScope
		{
			get
			{
				return this.IsDataIntersectionScope || (this.m_dataScopeInfo != null && this.m_dataScopeInfo.NeedsIDC);
			}
		}

		// Token: 0x17001791 RID: 6033
		// (get) Token: 0x060034DF RID: 13535 RVA: 0x000E7D1C File Offset: 0x000E5F1C
		public bool IsGroup
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001792 RID: 6034
		// (get) Token: 0x060034E0 RID: 13536 RVA: 0x000E7D1F File Offset: 0x000E5F1F
		public bool IsColumnOuterGrouping
		{
			get
			{
				return this.DataRegionDef.ProcessingInnerGrouping == DataRegion.ProcessingInnerGroupings.Row;
			}
		}

		// Token: 0x17001793 RID: 6035
		// (get) Token: 0x060034E1 RID: 13537 RVA: 0x000E7D30 File Offset: 0x000E5F30
		public IRIFReportDataScope ParentReportScope
		{
			get
			{
				if (this.IsDataIntersectionScope)
				{
					return null;
				}
				if (this.m_parentReportScope == null)
				{
					IRIFReportDataScope irifreportDataScope = IDOwner.FindReportDataScope(base.ParentInstancePath);
					IRIFReportDataScope irifreportDataScope2 = IDOwner.FindReportDataScope(this.m_parentColumnIDOwner);
					if (irifreportDataScope2 is ReportHierarchyNode)
					{
						this.m_parentReportScope = irifreportDataScope2;
					}
					else
					{
						this.m_parentReportScope = irifreportDataScope;
					}
				}
				return this.m_parentReportScope;
			}
		}

		// Token: 0x17001794 RID: 6036
		// (get) Token: 0x060034E2 RID: 13538 RVA: 0x000E7D85 File Offset: 0x000E5F85
		public IRIFReportDataScope ParentRowReportScope
		{
			get
			{
				if (this.IsDataIntersectionScope)
				{
					if (this.m_parentReportScope == null)
					{
						this.m_parentReportScope = IDOwner.FindReportDataScope(base.ParentInstancePath);
					}
					return this.m_parentReportScope;
				}
				return null;
			}
		}

		// Token: 0x17001795 RID: 6037
		// (get) Token: 0x060034E3 RID: 13539 RVA: 0x000E7DB0 File Offset: 0x000E5FB0
		public IRIFReportDataScope ParentColumnReportScope
		{
			get
			{
				if (this.IsDataIntersectionScope)
				{
					if (this.m_parentColumnReportScope == null)
					{
						this.m_parentColumnReportScope = IDOwner.FindReportDataScope(this.m_parentColumnIDOwner);
					}
					return this.m_parentColumnReportScope;
				}
				return null;
			}
		}

		// Token: 0x060034E4 RID: 13540 RVA: 0x000E7DDB File Offset: 0x000E5FDB
		public bool IsSameOrChildScopeOf(IRIFReportDataScope candidateScope)
		{
			return DataScopeInfo.IsSameOrChildScope(this, candidateScope);
		}

		// Token: 0x060034E5 RID: 13541 RVA: 0x000E7DE4 File Offset: 0x000E5FE4
		public bool IsChildScopeOf(IRIFReportDataScope candidateScope)
		{
			return DataScopeInfo.IsChildScopeOf(this, candidateScope);
		}

		// Token: 0x17001796 RID: 6038
		// (get) Token: 0x060034E6 RID: 13542 RVA: 0x000E7DED File Offset: 0x000E5FED
		public IReference<IOnDemandScopeInstance> CurrentStreamingScopeInstance
		{
			get
			{
				return this.m_currentStreamingScopeInstance;
			}
		}

		// Token: 0x060034E7 RID: 13543 RVA: 0x000E7DF5 File Offset: 0x000E5FF5
		public void BindToStreamingScopeInstance(IReference<IOnDemandMemberInstance> parentRowScopeInstance, IReference<IOnDemandMemberInstance> parentColumnScopeInstance)
		{
			if (this.m_cachedSyntheticCellReference == null)
			{
				this.m_cachedSyntheticCellReference = new SyntheticTriangulatedCellReference(parentRowScopeInstance, parentColumnScopeInstance);
			}
			else
			{
				this.m_cachedSyntheticCellReference.UpdateGroupLeafReferences(parentRowScopeInstance, parentColumnScopeInstance);
			}
			this.m_currentStreamingScopeInstance = this.m_cachedSyntheticCellReference;
		}

		// Token: 0x060034E8 RID: 13544 RVA: 0x000E7E28 File Offset: 0x000E6028
		public void ResetAggregates(AggregatesImpl reportOmAggregates)
		{
			this.ResetAggregates<DataAggregateInfo>(reportOmAggregates, this.m_dataRegionDef.CellAggregates, this.m_aggregateIndexes);
			this.ResetAggregates<DataAggregateInfo>(reportOmAggregates, this.m_dataRegionDef.CellPostSortAggregates, this.m_postSortAggregateIndexes);
			this.ResetAggregates<RunningValueInfo>(reportOmAggregates, this.m_dataRegionDef.CellRunningValues, this.m_runningValueIndexes);
			if (this.m_dataScopeInfo != null)
			{
				this.m_dataScopeInfo.ResetAggregates(reportOmAggregates);
			}
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x000E7E94 File Offset: 0x000E6094
		private void ResetAggregates<T>(AggregatesImpl reportOmAggregates, List<T> aggregateDefs, List<int> aggregateIndices) where T : DataAggregateInfo
		{
			if (aggregateDefs == null || aggregateIndices == null)
			{
				return;
			}
			for (int i = 0; i < aggregateIndices.Count; i++)
			{
				int num = aggregateIndices[i];
				reportOmAggregates.Reset(aggregateDefs[num]);
			}
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x000E7ED4 File Offset: 0x000E60D4
		public bool HasServerAggregate(string aggregateName)
		{
			if (this.m_aggregateIndexes == null)
			{
				return false;
			}
			foreach (int num in this.m_aggregateIndexes)
			{
				if (DataScopeInfo.IsTargetServerAggregate(this.m_dataRegionDef.CellAggregates[num], aggregateName))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000E7F4C File Offset: 0x000E614C
		public void BindToStreamingScopeInstance(IReference<IOnDemandScopeInstance> scopeInstance)
		{
			this.m_currentStreamingScopeInstance = scopeInstance;
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x000E7F58 File Offset: 0x000E6158
		public void BindToNoRowsScopeInstance(OnDemandProcessingContext odpContext)
		{
			if (this.m_cachedNoRowsStreamingScopeInstance == null)
			{
				StreamingNoRowsCellInstance streamingNoRowsCellInstance = new StreamingNoRowsCellInstance(odpContext, this);
				this.m_cachedNoRowsStreamingScopeInstance = new SyntheticOnDemandScopeInstanceReference(streamingNoRowsCellInstance);
			}
			this.m_currentStreamingScopeInstance = this.m_cachedNoRowsStreamingScopeInstance;
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000E7F8D File Offset: 0x000E618D
		public void ClearStreamingScopeInstanceBinding()
		{
			this.m_currentStreamingScopeInstance = null;
		}

		// Token: 0x17001797 RID: 6039
		// (get) Token: 0x060034EE RID: 13550 RVA: 0x000E7F96 File Offset: 0x000E6196
		public bool IsBoundToStreamingScopeInstance
		{
			get
			{
				return this.m_currentStreamingScopeInstance != null;
			}
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x000E7FA1 File Offset: 0x000E61A1
		internal void TraverseScopes(IRIFScopeVisitor visitor, int rowIndex, int colIndex)
		{
			visitor.PreVisit(this, rowIndex, colIndex);
			this.TraverseNestedScopes(visitor);
			visitor.PostVisit(this, rowIndex, colIndex);
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x000E7FBC File Offset: 0x000E61BC
		protected virtual void TraverseNestedScopes(IRIFScopeVisitor visitor)
		{
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000E7FBE File Offset: 0x000E61BE
		List<DataAggregateInfo> IAggregateHolder.GetAggregateList()
		{
			return this.m_aggregates;
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x000E7FC6 File Offset: 0x000E61C6
		List<DataAggregateInfo> IAggregateHolder.GetPostSortAggregateList()
		{
			return this.m_postSortAggregates;
		}

		// Token: 0x060034F3 RID: 13555 RVA: 0x000E7FCE File Offset: 0x000E61CE
		List<RunningValueInfo> IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		// Token: 0x060034F4 RID: 13556 RVA: 0x000E7FD8 File Offset: 0x000E61D8
		void IAggregateHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_aggregates != null, "(null != m_aggregates)");
			if (this.m_aggregates.Count == 0)
			{
				this.m_aggregates = null;
			}
			Global.Tracer.Assert(this.m_postSortAggregates != null, "(null != m_postSortAggregates)");
			if (this.m_postSortAggregates.Count == 0)
			{
				this.m_postSortAggregates = null;
			}
		}

		// Token: 0x060034F5 RID: 13557 RVA: 0x000E803D File Offset: 0x000E623D
		void IRunningValueHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_runningValues != null, "(null != m_runningValues)");
			if (this.m_runningValues.Count == 0)
			{
				this.m_runningValues = null;
			}
		}

		// Token: 0x060034F6 RID: 13558 RVA: 0x000E806C File Offset: 0x000E626C
		internal void GenerateAggregateIndexes(Dictionary<string, int> aggregateIndexMapping, Dictionary<string, int> postSortAggregateIndexMapping, Dictionary<string, int> runningValueIndexMapping)
		{
			if (this.m_aggregates != null)
			{
				Cell.GenerateAggregateIndexes<DataAggregateInfo>(this.m_aggregates, aggregateIndexMapping, ref this.m_aggregateIndexes);
			}
			if (this.m_postSortAggregates != null)
			{
				Cell.GenerateAggregateIndexes<DataAggregateInfo>(this.m_postSortAggregates, postSortAggregateIndexMapping, ref this.m_postSortAggregateIndexes);
			}
			if (this.m_runningValues != null)
			{
				Cell.GenerateAggregateIndexes<RunningValueInfo>(this.m_runningValues, runningValueIndexMapping, ref this.m_runningValueIndexes);
			}
		}

		// Token: 0x060034F7 RID: 13559 RVA: 0x000E80C8 File Offset: 0x000E62C8
		private static void GenerateAggregateIndexes<AggregateType>(List<AggregateType> cellAggregates, Dictionary<string, int> aggregateIndexMapping, ref List<int> aggregateIndexes) where AggregateType : DataAggregateInfo
		{
			int count = cellAggregates.Count;
			if (count == 0)
			{
				return;
			}
			aggregateIndexes = new List<int>();
			for (int i = 0; i < count; i++)
			{
				AggregateType aggregateType = cellAggregates[i];
				int num;
				if (aggregateIndexMapping.TryGetValue(aggregateType.Name, out num))
				{
					aggregateIndexes.Add(num);
				}
			}
		}

		// Token: 0x060034F8 RID: 13560 RVA: 0x000E8118 File Offset: 0x000E6318
		internal static bool ContainsInnerGroupTreeHierarchy(ReportItem cellContents)
		{
			return cellContents != null && cellContents.IsOrContainsDataRegionOrSubReport();
		}

		// Token: 0x060034F9 RID: 13561 RVA: 0x000E8128 File Offset: 0x000E6328
		internal void Initialize(int parentRowID, int parentColumnID, int rowindex, int colIndex, InitializationContext context)
		{
			bool flag = this.IsDataRegionBodyCell && context.IsDataRegionCellScope;
			if (flag)
			{
				context.RegisterIndividualCellScope(this);
				this.m_inDynamicRowAndColumnContext = context.IsDataRegionCellScope;
				if (this.DataScopeInfo.JoinInfo != null && this.DataScopeInfo.JoinInfo is IntersectJoinInfo)
				{
					this.m_dataRegionDef.IsMatrixIDC = true;
				}
			}
			else
			{
				context.RegisterNonScopeCell(this);
			}
			this.m_textboxesInScope = context.GetCurrentReferencableTextboxes();
			this.m_variablesInScope = context.GetCurrentReferencableVariables();
			this.m_parentRowID = parentRowID;
			this.m_parentColumnID = parentColumnID;
			context.SetIndexInCollection(this);
			this.StartExprHost(context);
			if (this.m_dataScopeInfo != null)
			{
				this.m_dataScopeInfo.Initialize(context, this);
			}
			this.InternalInitialize(parentRowID, parentColumnID, rowindex, colIndex, context);
			this.EndExprHost(context);
			this.m_dataScopeInfo.ValidateScopeRulesForIdc(context, this);
			if (context.EvaluateAtomicityCondition(this.HasAggregatesForAtomicityCheck(), this, AtomicityReason.Aggregates) || context.EvaluateAtomicityCondition(context.HasMultiplePeerChildScopes(this), this, AtomicityReason.PeerChildScopes))
			{
				context.FoundAtomicScope(this);
			}
			else
			{
				this.m_dataScopeInfo.IsDecomposable = true;
			}
			if (flag)
			{
				context.UnRegisterIndividualCellScope(this);
				return;
			}
			context.UnRegisterNonScopeCell(this);
		}

		// Token: 0x060034FA RID: 13562
		internal abstract void InternalInitialize(int parentRowID, int parentColumnID, int rowindex, int colIndex, InitializationContext context);

		// Token: 0x17001798 RID: 6040
		// (get) Token: 0x060034FB RID: 13563
		protected abstract Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode ExprHostDataRegionMode { get; }

		// Token: 0x060034FC RID: 13564 RVA: 0x000E8253 File Offset: 0x000E6453
		protected virtual void StartExprHost(InitializationContext context)
		{
			context.ExprHostBuilder.DataCellStart(this.ExprHostDataRegionMode);
		}

		// Token: 0x060034FD RID: 13565 RVA: 0x000E8267 File Offset: 0x000E6467
		protected virtual void EndExprHost(InitializationContext context)
		{
			this.m_exprHostID = context.ExprHostBuilder.DataCellEnd(this.ExprHostDataRegionMode);
		}

		// Token: 0x060034FE RID: 13566 RVA: 0x000E8281 File Offset: 0x000E6481
		private bool HasAggregatesForAtomicityCheck()
		{
			return DataScopeInfo.HasNonServerAggregates<DataAggregateInfo>(this.m_aggregates) || DataScopeInfo.HasAggregates<DataAggregateInfo>(this.m_postSortAggregates) || DataScopeInfo.HasAggregates<RunningValueInfo>(this.m_runningValues) || this.m_dataScopeInfo.HasAggregatesOrRunningValues;
		}

		// Token: 0x060034FF RID: 13567 RVA: 0x000E82B8 File Offset: 0x000E64B8
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Cell cell = (Cell)base.PublishClone(context);
			cell.m_aggregates = new List<DataAggregateInfo>();
			cell.m_postSortAggregates = new List<DataAggregateInfo>();
			cell.m_runningValues = new List<RunningValueInfo>();
			cell.m_dataScopeInfo = this.m_dataScopeInfo.PublishClone(context, cell.ID);
			context.AddAggregateHolder(cell);
			context.AddRunningValueHolder(cell);
			if (context.CurrentDataRegionClone != null)
			{
				cell.m_dataRegionDef = context.CurrentDataRegionClone;
			}
			return cell;
		}

		// Token: 0x06003500 RID: 13568 RVA: 0x000E8332 File Offset: 0x000E6532
		internal void BaseSetExprHost(CellExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			if (this.m_dataScopeInfo != null && this.m_dataScopeInfo.JoinInfo != null && exprHost.JoinConditionExprHostsRemotable != null)
			{
				this.m_dataScopeInfo.JoinInfo.SetJoinConditionExprHost(exprHost.JoinConditionExprHostsRemotable, reportObjectModel);
			}
		}

		// Token: 0x06003501 RID: 13569 RVA: 0x000E8368 File Offset: 0x000E6568
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Cell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IDOwner, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.ParentRowID, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IDOwner, Token.Reference),
				new MemberInfo(MemberName.ParentColumnID, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IDOwner, Token.Reference),
				new MemberInfo(MemberName.IndexInCollection, Token.Int32),
				new MemberInfo(MemberName.HasInnerGroupTreeHierarchy, Token.Boolean),
				new MemberInfo(MemberName.DataRegionDef, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegion, Token.Reference),
				new MemberInfo(MemberName.AggregateIndexes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int32),
				new MemberInfo(MemberName.PostSortAggregateIndexes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int32),
				new MemberInfo(MemberName.RunningValueIndexes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int32),
				new MemberInfo(MemberName.NeedToCacheDataRows, Token.Boolean),
				new MemberInfo(MemberName.InScopeEventSources, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IInScopeEventSource),
				new MemberInfo(MemberName.InDynamicRowAndColumnContext, Token.Boolean),
				new MemberInfo(MemberName.TextboxesInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Byte),
				new MemberInfo(MemberName.VariablesInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Byte),
				new MemberInfo(MemberName.DataScopeInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataScopeInfo)
			});
		}

		// Token: 0x06003502 RID: 13570 RVA: 0x000E84D0 File Offset: 0x000E66D0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Cell.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.HasInnerGroupTreeHierarchy)
				{
					if (memberName <= MemberName.ParentRowID)
					{
						if (memberName == MemberName.DataRegionDef)
						{
							Global.Tracer.Assert(this.m_dataRegionDef != null, "(null != m_dataRegionDef)");
							writer.WriteReference(this.m_dataRegionDef);
							continue;
						}
						if (memberName == MemberName.ExprHostID)
						{
							writer.Write(this.m_exprHostID);
							continue;
						}
						if (memberName == MemberName.ParentRowID)
						{
							writer.WriteReferenceID(this.m_parentRowID);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ParentColumnID)
						{
							writer.WriteReferenceID(this.m_parentColumnID);
							continue;
						}
						if (memberName == MemberName.IndexInCollection)
						{
							writer.Write(this.m_indexInCollection);
							continue;
						}
						if (memberName == MemberName.HasInnerGroupTreeHierarchy)
						{
							writer.Write(this.m_hasInnerGroupTreeHierarchy);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.InScopeEventSources)
				{
					switch (memberName)
					{
					case MemberName.AggregateIndexes:
						writer.WriteListOfPrimitives<int>(this.m_aggregateIndexes);
						continue;
					case MemberName.PostSortAggregateIndexes:
						writer.WriteListOfPrimitives<int>(this.m_postSortAggregateIndexes);
						continue;
					case MemberName.RunningValueIndexes:
						writer.WriteListOfPrimitives<int>(this.m_runningValueIndexes);
						continue;
					default:
						if (memberName == MemberName.NeedToCacheDataRows)
						{
							writer.Write(this.m_needToCacheDataRows);
							continue;
						}
						if (memberName == MemberName.InScopeEventSources)
						{
							writer.WriteListOfReferences(this.m_inScopeEventSources);
							continue;
						}
						break;
					}
				}
				else if (memberName <= MemberName.VariablesInScope)
				{
					if (memberName == MemberName.InDynamicRowAndColumnContext)
					{
						writer.Write(this.m_inDynamicRowAndColumnContext);
						continue;
					}
					if (memberName == MemberName.VariablesInScope)
					{
						writer.Write(this.m_variablesInScope);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.TextboxesInScope)
					{
						writer.Write(this.m_textboxesInScope);
						continue;
					}
					if (memberName == MemberName.DataScopeInfo)
					{
						writer.Write(this.m_dataScopeInfo);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003503 RID: 13571 RVA: 0x000E86FC File Offset: 0x000E68FC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Cell.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.HasInnerGroupTreeHierarchy)
				{
					if (memberName <= MemberName.ParentRowID)
					{
						if (memberName == MemberName.DataRegionDef)
						{
							this.m_dataRegionDef = reader.ReadReference<DataRegion>(this);
							continue;
						}
						if (memberName == MemberName.ExprHostID)
						{
							this.m_exprHostID = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.ParentRowID)
						{
							this.m_parentIDOwner = reader.ReadReference<IDOwner>(this);
							if (this.m_parentIDOwner != null)
							{
								this.m_parentRowID = this.m_parentIDOwner.ID;
								continue;
							}
							continue;
						}
					}
					else if (memberName != MemberName.ParentColumnID)
					{
						if (memberName == MemberName.IndexInCollection)
						{
							this.m_indexInCollection = reader.ReadInt32();
							continue;
						}
						if (memberName == MemberName.HasInnerGroupTreeHierarchy)
						{
							this.m_hasInnerGroupTreeHierarchy = reader.ReadBoolean();
							continue;
						}
					}
					else
					{
						this.m_parentColumnIDOwner = reader.ReadReference<IDOwner>(this);
						if (this.m_parentColumnIDOwner != null)
						{
							this.m_parentColumnID = this.m_parentColumnIDOwner.ID;
							continue;
						}
						continue;
					}
				}
				else if (memberName <= MemberName.InScopeEventSources)
				{
					switch (memberName)
					{
					case MemberName.AggregateIndexes:
						this.m_aggregateIndexes = reader.ReadListOfPrimitives<int>();
						continue;
					case MemberName.PostSortAggregateIndexes:
						this.m_postSortAggregateIndexes = reader.ReadListOfPrimitives<int>();
						continue;
					case MemberName.RunningValueIndexes:
						this.m_runningValueIndexes = reader.ReadListOfPrimitives<int>();
						continue;
					default:
						if (memberName == MemberName.NeedToCacheDataRows)
						{
							this.m_needToCacheDataRows = reader.ReadBoolean();
							continue;
						}
						if (memberName == MemberName.InScopeEventSources)
						{
							this.m_inScopeEventSources = reader.ReadGenericListOfReferences<IInScopeEventSource>(this);
							continue;
						}
						break;
					}
				}
				else if (memberName <= MemberName.VariablesInScope)
				{
					if (memberName == MemberName.InDynamicRowAndColumnContext)
					{
						this.m_inDynamicRowAndColumnContext = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.VariablesInScope)
					{
						this.m_variablesInScope = reader.ReadByteArray();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.TextboxesInScope)
					{
						this.m_textboxesInScope = reader.ReadByteArray();
						continue;
					}
					if (memberName == MemberName.DataScopeInfo)
					{
						this.m_dataScopeInfo = reader.ReadRIFObject<DataScopeInfo>();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003504 RID: 13572 RVA: 0x000E894C File Offset: 0x000E6B4C
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(Cell.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					MemberName memberName = memberReference.MemberName;
					if (memberName <= MemberName.ParentRowID)
					{
						if (memberName == MemberName.DataRegionDef)
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable;
							referenceableItems.TryGetValue(memberReference.RefID, out referenceable);
							Global.Tracer.Assert(referenceable != null && ((ReportItem)referenceable).IsDataRegion, "DataRegionDef");
							this.m_dataRegionDef = (DataRegion)referenceable;
							continue;
						}
						if (memberName == MemberName.ParentRowID)
						{
							Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID), "ParentRowID");
							this.m_parentIDOwner = (IDOwner)referenceableItems[memberReference.RefID];
							this.m_parentRowID = memberReference.RefID;
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.ParentColumnID)
						{
							Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID), "ParentColumnID");
							this.m_parentColumnIDOwner = (IDOwner)referenceableItems[memberReference.RefID];
							this.m_parentColumnID = memberReference.RefID;
							continue;
						}
						if (memberName == MemberName.InScopeEventSources)
						{
							Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable2;
							referenceableItems.TryGetValue(memberReference.RefID, out referenceable2);
							IInScopeEventSource inScopeEventSource = (IInScopeEventSource)referenceable2;
							if (this.m_inScopeEventSources == null)
							{
								this.m_inScopeEventSources = new List<IInScopeEventSource>();
							}
							this.m_inScopeEventSources.Add(inScopeEventSource);
							continue;
						}
					}
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06003505 RID: 13573 RVA: 0x000E8B08 File Offset: 0x000E6D08
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Cell;
		}

		// Token: 0x04001A16 RID: 6678
		protected int m_exprHostID = -1;

		// Token: 0x04001A17 RID: 6679
		protected int m_parentRowID = -1;

		// Token: 0x04001A18 RID: 6680
		protected int m_parentColumnID = -1;

		// Token: 0x04001A19 RID: 6681
		protected int m_indexInCollection = -1;

		// Token: 0x04001A1A RID: 6682
		protected bool m_hasInnerGroupTreeHierarchy;

		// Token: 0x04001A1B RID: 6683
		[Reference]
		protected DataRegion m_dataRegionDef;

		// Token: 0x04001A1C RID: 6684
		protected List<int> m_aggregateIndexes;

		// Token: 0x04001A1D RID: 6685
		protected List<int> m_postSortAggregateIndexes;

		// Token: 0x04001A1E RID: 6686
		protected List<int> m_runningValueIndexes;

		// Token: 0x04001A1F RID: 6687
		private bool m_needToCacheDataRows;

		// Token: 0x04001A20 RID: 6688
		private byte[] m_textboxesInScope;

		// Token: 0x04001A21 RID: 6689
		private byte[] m_variablesInScope;

		// Token: 0x04001A22 RID: 6690
		private List<IInScopeEventSource> m_inScopeEventSources;

		// Token: 0x04001A23 RID: 6691
		protected bool m_inDynamicRowAndColumnContext;

		// Token: 0x04001A24 RID: 6692
		protected DataScopeInfo m_dataScopeInfo;

		// Token: 0x04001A25 RID: 6693
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Cell.GetDeclaration();

		// Token: 0x04001A26 RID: 6694
		[NonSerialized]
		protected IDOwner m_parentColumnIDOwner;

		// Token: 0x04001A27 RID: 6695
		[NonSerialized]
		protected List<DataAggregateInfo> m_aggregates;

		// Token: 0x04001A28 RID: 6696
		[NonSerialized]
		protected List<DataAggregateInfo> m_postSortAggregates;

		// Token: 0x04001A29 RID: 6697
		[NonSerialized]
		protected List<RunningValueInfo> m_runningValues;

		// Token: 0x04001A2A RID: 6698
		[NonSerialized]
		protected DataScopeInfo m_canonicalDataScopeInfo;

		// Token: 0x04001A2B RID: 6699
		[NonSerialized]
		private IRIFReportDataScope m_parentReportScope;

		// Token: 0x04001A2C RID: 6700
		[NonSerialized]
		private IRIFReportDataScope m_parentColumnReportScope;

		// Token: 0x04001A2D RID: 6701
		[NonSerialized]
		private IReference<IOnDemandScopeInstance> m_currentStreamingScopeInstance;

		// Token: 0x04001A2E RID: 6702
		[NonSerialized]
		private SyntheticTriangulatedCellReference m_cachedSyntheticCellReference;

		// Token: 0x04001A2F RID: 6703
		[NonSerialized]
		private IReference<IOnDemandScopeInstance> m_cachedNoRowsStreamingScopeInstance;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004AE RID: 1198
	[Serializable]
	public abstract class DataRegion : Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem, IPageBreakOwner, IAggregateHolder, IRunningValueHolder, ISortFilterScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable, IIndexedInCollection, IRIFReportScope, IInstancePath, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGloballyReferenceable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IGlobalIDOwner, IRIFDataScope, IDomainScopeMemberCreator, IRIFReportDataScope
	{
		// Token: 0x06003AE8 RID: 15080 RVA: 0x000FF012 File Offset: 0x000FD212
		protected DataRegion(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x06003AE9 RID: 15081 RVA: 0x000FF030 File Offset: 0x000FD230
		protected DataRegion(int id, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem parent)
			: base(id, parent)
		{
			this.m_aggregates = new List<DataAggregateInfo>();
			this.m_postSortAggregates = new List<DataAggregateInfo>();
			this.m_runningValues = new List<RunningValueInfo>();
			this.m_cellRunningValues = new List<RunningValueInfo>();
			this.m_dataScopeInfo = new DataScopeInfo(id);
		}

		// Token: 0x1700195B RID: 6491
		// (get) Token: 0x06003AEA RID: 15082 RVA: 0x000FF092 File Offset: 0x000FD292
		string IRIFDataScope.Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x1700195C RID: 6492
		// (get) Token: 0x06003AEB RID: 15083 RVA: 0x000FF09A File Offset: 0x000FD29A
		Microsoft.ReportingServices.ReportProcessing.ObjectType IRIFDataScope.DataScopeObjectType
		{
			get
			{
				return this.ObjectType;
			}
		}

		// Token: 0x1700195D RID: 6493
		// (get) Token: 0x06003AED RID: 15085 RVA: 0x000FF0AB File Offset: 0x000FD2AB
		// (set) Token: 0x06003AEC RID: 15084 RVA: 0x000FF0A2 File Offset: 0x000FD2A2
		internal bool IsMatrixIDC
		{
			get
			{
				return this.m_isMatrixIDC;
			}
			set
			{
				this.m_isMatrixIDC = value;
			}
		}

		// Token: 0x1700195E RID: 6494
		// (get) Token: 0x06003AEE RID: 15086 RVA: 0x000FF0B3 File Offset: 0x000FD2B3
		internal override bool IsDataRegion
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700195F RID: 6495
		// (get) Token: 0x06003AEF RID: 15087
		internal abstract HierarchyNodeList ColumnMembers { get; }

		// Token: 0x17001960 RID: 6496
		// (get) Token: 0x06003AF0 RID: 15088
		internal abstract HierarchyNodeList RowMembers { get; }

		// Token: 0x17001961 RID: 6497
		// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x000FF0B6 File Offset: 0x000FD2B6
		internal HierarchyNodeList OuterMembers
		{
			get
			{
				if (this.m_processingInnerGrouping == Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.ProcessingInnerGroupings.Column)
				{
					return this.RowMembers;
				}
				return this.ColumnMembers;
			}
		}

		// Token: 0x17001962 RID: 6498
		// (get) Token: 0x06003AF2 RID: 15090 RVA: 0x000FF0CD File Offset: 0x000FD2CD
		internal HierarchyNodeList InnerMembers
		{
			get
			{
				if (this.m_processingInnerGrouping == Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.ProcessingInnerGroupings.Column)
				{
					return this.ColumnMembers;
				}
				return this.RowMembers;
			}
		}

		// Token: 0x17001963 RID: 6499
		// (get) Token: 0x06003AF3 RID: 15091
		internal abstract RowList Rows { get; }

		// Token: 0x17001964 RID: 6500
		// (get) Token: 0x06003AF4 RID: 15092 RVA: 0x000FF0E4 File Offset: 0x000FD2E4
		// (set) Token: 0x06003AF5 RID: 15093 RVA: 0x000FF0EC File Offset: 0x000FD2EC
		internal string DataSetName
		{
			get
			{
				return this.m_dataSetName;
			}
			set
			{
				this.m_dataSetName = value;
			}
		}

		// Token: 0x17001965 RID: 6501
		// (get) Token: 0x06003AF6 RID: 15094 RVA: 0x000FF0F5 File Offset: 0x000FD2F5
		// (set) Token: 0x06003AF7 RID: 15095 RVA: 0x000FF0FD File Offset: 0x000FD2FD
		internal bool NoRows
		{
			get
			{
				return this.m_noRows;
			}
			set
			{
				this.m_noRows = value;
			}
		}

		// Token: 0x17001966 RID: 6502
		// (get) Token: 0x06003AF8 RID: 15096 RVA: 0x000FF106 File Offset: 0x000FD306
		// (set) Token: 0x06003AF9 RID: 15097 RVA: 0x000FF10E File Offset: 0x000FD30E
		internal ExpressionInfo NoRowsMessage
		{
			get
			{
				return this.m_noRowsMessage;
			}
			set
			{
				this.m_noRowsMessage = value;
			}
		}

		// Token: 0x17001967 RID: 6503
		// (get) Token: 0x06003AFA RID: 15098 RVA: 0x000FF117 File Offset: 0x000FD317
		// (set) Token: 0x06003AFB RID: 15099 RVA: 0x000FF11F File Offset: 0x000FD31F
		internal int ColumnCount
		{
			get
			{
				return this.m_columnCount;
			}
			set
			{
				this.m_columnCount = value;
			}
		}

		// Token: 0x17001968 RID: 6504
		// (get) Token: 0x06003AFC RID: 15100 RVA: 0x000FF128 File Offset: 0x000FD328
		// (set) Token: 0x06003AFD RID: 15101 RVA: 0x000FF130 File Offset: 0x000FD330
		internal int RowCount
		{
			get
			{
				return this.m_rowCount;
			}
			set
			{
				this.m_rowCount = value;
			}
		}

		// Token: 0x17001969 RID: 6505
		// (get) Token: 0x06003AFE RID: 15102 RVA: 0x000FF139 File Offset: 0x000FD339
		// (set) Token: 0x06003AFF RID: 15103 RVA: 0x000FF141 File Offset: 0x000FD341
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.ProcessingInnerGroupings ProcessingInnerGrouping
		{
			get
			{
				return this.m_processingInnerGrouping;
			}
			set
			{
				this.m_processingInnerGrouping = value;
			}
		}

		// Token: 0x1700196A RID: 6506
		// (get) Token: 0x06003B00 RID: 15104 RVA: 0x000FF14A File Offset: 0x000FD34A
		// (set) Token: 0x06003B01 RID: 15105 RVA: 0x000FF152 File Offset: 0x000FD352
		internal List<int> RepeatSiblings
		{
			get
			{
				return this.m_repeatSiblings;
			}
			set
			{
				this.m_repeatSiblings = value;
			}
		}

		// Token: 0x1700196B RID: 6507
		// (get) Token: 0x06003B02 RID: 15106 RVA: 0x000FF15B File Offset: 0x000FD35B
		// (set) Token: 0x06003B03 RID: 15107 RVA: 0x000FF163 File Offset: 0x000FD363
		internal Sorting Sorting
		{
			get
			{
				return this.m_sorting;
			}
			set
			{
				this.m_sorting = value;
			}
		}

		// Token: 0x1700196C RID: 6508
		// (get) Token: 0x06003B04 RID: 15108 RVA: 0x000FF16C File Offset: 0x000FD36C
		// (set) Token: 0x06003B05 RID: 15109 RVA: 0x000FF174 File Offset: 0x000FD374
		internal List<Filter> Filters
		{
			get
			{
				return this.m_filters;
			}
			set
			{
				this.m_filters = value;
			}
		}

		// Token: 0x1700196D RID: 6509
		// (get) Token: 0x06003B06 RID: 15110 RVA: 0x000FF17D File Offset: 0x000FD37D
		internal bool HasFilters
		{
			get
			{
				return this.m_filters != null && this.m_filters.Count > 0;
			}
		}

		// Token: 0x1700196E RID: 6510
		// (get) Token: 0x06003B07 RID: 15111 RVA: 0x000FF197 File Offset: 0x000FD397
		// (set) Token: 0x06003B08 RID: 15112 RVA: 0x000FF19F File Offset: 0x000FD39F
		internal List<DataAggregateInfo> Aggregates
		{
			get
			{
				return this.m_aggregates;
			}
			set
			{
				this.m_aggregates = value;
			}
		}

		// Token: 0x1700196F RID: 6511
		// (get) Token: 0x06003B09 RID: 15113 RVA: 0x000FF1A8 File Offset: 0x000FD3A8
		// (set) Token: 0x06003B0A RID: 15114 RVA: 0x000FF1B0 File Offset: 0x000FD3B0
		internal List<DataAggregateInfo> PostSortAggregates
		{
			get
			{
				return this.m_postSortAggregates;
			}
			set
			{
				this.m_postSortAggregates = value;
			}
		}

		// Token: 0x17001970 RID: 6512
		// (get) Token: 0x06003B0B RID: 15115 RVA: 0x000FF1B9 File Offset: 0x000FD3B9
		// (set) Token: 0x06003B0C RID: 15116 RVA: 0x000FF1C1 File Offset: 0x000FD3C1
		internal List<RunningValueInfo> RunningValues
		{
			get
			{
				return this.m_runningValues;
			}
			set
			{
				this.m_runningValues = value;
			}
		}

		// Token: 0x17001971 RID: 6513
		// (get) Token: 0x06003B0D RID: 15117 RVA: 0x000FF1CA File Offset: 0x000FD3CA
		// (set) Token: 0x06003B0E RID: 15118 RVA: 0x000FF1D2 File Offset: 0x000FD3D2
		internal List<DataAggregateInfo> CellAggregates
		{
			get
			{
				return this.m_cellAggregates;
			}
			set
			{
				this.m_cellAggregates = value;
			}
		}

		// Token: 0x17001972 RID: 6514
		// (get) Token: 0x06003B0F RID: 15119 RVA: 0x000FF1DB File Offset: 0x000FD3DB
		// (set) Token: 0x06003B10 RID: 15120 RVA: 0x000FF1E3 File Offset: 0x000FD3E3
		internal List<DataAggregateInfo> CellPostSortAggregates
		{
			get
			{
				return this.m_cellPostSortAggregates;
			}
			set
			{
				this.m_cellPostSortAggregates = value;
			}
		}

		// Token: 0x17001973 RID: 6515
		// (get) Token: 0x06003B11 RID: 15121 RVA: 0x000FF1EC File Offset: 0x000FD3EC
		// (set) Token: 0x06003B12 RID: 15122 RVA: 0x000FF1F4 File Offset: 0x000FD3F4
		internal List<RunningValueInfo> CellRunningValues
		{
			get
			{
				return this.m_cellRunningValues;
			}
			set
			{
				this.m_cellRunningValues = value;
			}
		}

		// Token: 0x17001974 RID: 6516
		// (get) Token: 0x06003B13 RID: 15123 RVA: 0x000FF1FD File Offset: 0x000FD3FD
		// (set) Token: 0x06003B14 RID: 15124 RVA: 0x000FF205 File Offset: 0x000FD405
		internal List<ExpressionInfo> UserSortExpressions
		{
			get
			{
				return this.m_userSortExpressions;
			}
			set
			{
				this.m_userSortExpressions = value;
			}
		}

		// Token: 0x17001975 RID: 6517
		// (get) Token: 0x06003B15 RID: 15125 RVA: 0x000FF20E File Offset: 0x000FD40E
		// (set) Token: 0x06003B16 RID: 15126 RVA: 0x000FF216 File Offset: 0x000FD416
		internal InScopeSortFilterHashtable DetailSortFiltersInScope
		{
			get
			{
				return this.m_detailSortFiltersInScope;
			}
			set
			{
				this.m_detailSortFiltersInScope = value;
			}
		}

		// Token: 0x17001976 RID: 6518
		// (get) Token: 0x06003B17 RID: 15127 RVA: 0x000FF21F File Offset: 0x000FD41F
		// (set) Token: 0x06003B18 RID: 15128 RVA: 0x000FF227 File Offset: 0x000FD427
		internal Hashtable ScopeNames
		{
			get
			{
				return this.m_scopeNames;
			}
			set
			{
				this.m_scopeNames = value;
			}
		}

		// Token: 0x06003B19 RID: 15129 RVA: 0x000FF230 File Offset: 0x000FD430
		bool IRIFReportScope.VariableInScope(int sequenceIndex)
		{
			return SequenceIndex.GetBit(this.m_variablesInScope, sequenceIndex, true);
		}

		// Token: 0x06003B1A RID: 15130 RVA: 0x000FF23F File Offset: 0x000FD43F
		bool IRIFReportScope.TextboxInScope(int sequenceIndex)
		{
			return SequenceIndex.GetBit(this.m_textboxesInScope, sequenceIndex, true);
		}

		// Token: 0x17001977 RID: 6519
		// (get) Token: 0x06003B1B RID: 15131 RVA: 0x000FF24E File Offset: 0x000FD44E
		public IRIFReportDataScope ParentReportScope
		{
			get
			{
				if (!this.m_populatedParentReportScope)
				{
					this.m_parentReportScope = IDOwner.FindReportDataScope(base.ParentInstancePath);
					this.m_populatedParentReportScope = true;
				}
				return this.m_parentReportScope;
			}
		}

		// Token: 0x17001978 RID: 6520
		// (get) Token: 0x06003B1C RID: 15132 RVA: 0x000FF276 File Offset: 0x000FD476
		public bool IsDataIntersectionScope
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001979 RID: 6521
		// (get) Token: 0x06003B1D RID: 15133 RVA: 0x000FF279 File Offset: 0x000FD479
		public bool IsScope
		{
			get
			{
				return this.IsDataRegion;
			}
		}

		// Token: 0x1700197A RID: 6522
		// (get) Token: 0x06003B1E RID: 15134 RVA: 0x000FF281 File Offset: 0x000FD481
		public bool IsGroup
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700197B RID: 6523
		// (get) Token: 0x06003B1F RID: 15135 RVA: 0x000FF284 File Offset: 0x000FD484
		public virtual bool IsColumnGroupingSwitched
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x000FF287 File Offset: 0x000FD487
		public bool IsSameOrChildScopeOf(IRIFReportDataScope candidateScope)
		{
			return DataScopeInfo.IsSameOrChildScope(this, candidateScope);
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x000FF290 File Offset: 0x000FD490
		public bool IsChildScopeOf(IRIFReportDataScope candidateScope)
		{
			return DataScopeInfo.IsChildScopeOf(this, candidateScope);
		}

		// Token: 0x1700197C RID: 6524
		// (get) Token: 0x06003B22 RID: 15138 RVA: 0x000FF299 File Offset: 0x000FD499
		public IReference<IOnDemandScopeInstance> CurrentStreamingScopeInstance
		{
			get
			{
				return this.m_currentStreamingScopeInstance;
			}
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x000FF2A1 File Offset: 0x000FD4A1
		public void ResetAggregates(AggregatesImpl reportOmAggregates)
		{
			reportOmAggregates.ResetAll<DataAggregateInfo>(this.m_aggregates);
			reportOmAggregates.ResetAll<DataAggregateInfo>(this.m_postSortAggregates);
			reportOmAggregates.ResetAll<RunningValueInfo>(this.m_runningValues);
			if (this.m_dataScopeInfo != null)
			{
				this.m_dataScopeInfo.ResetAggregates(reportOmAggregates);
			}
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x000FF2DB File Offset: 0x000FD4DB
		public bool HasServerAggregate(string aggregateName)
		{
			return DataScopeInfo.ContainsServerAggregate<DataAggregateInfo>(this.m_aggregates, aggregateName);
		}

		// Token: 0x06003B25 RID: 15141 RVA: 0x000FF2E9 File Offset: 0x000FD4E9
		public void BindToStreamingScopeInstance(IReference<IOnDemandScopeInstance> scopeInstance)
		{
			this.m_currentStreamingScopeInstance = scopeInstance;
		}

		// Token: 0x06003B26 RID: 15142 RVA: 0x000FF2F4 File Offset: 0x000FD4F4
		public void BindToNoRowsScopeInstance(OnDemandProcessingContext odpContext)
		{
			if (this.m_cachedNoRowsStreamingScopeInstance == null)
			{
				StreamingNoRowsDataRegionInstance streamingNoRowsDataRegionInstance = new StreamingNoRowsDataRegionInstance(odpContext, this);
				this.m_cachedNoRowsStreamingScopeInstance = new SyntheticOnDemandMemberOwnerInstanceReference(streamingNoRowsDataRegionInstance);
			}
			this.m_currentStreamingScopeInstance = this.m_cachedNoRowsStreamingScopeInstance;
		}

		// Token: 0x06003B27 RID: 15143 RVA: 0x000FF329 File Offset: 0x000FD529
		public void ClearStreamingScopeInstanceBinding()
		{
			this.m_currentStreamingScopeInstance = null;
		}

		// Token: 0x1700197D RID: 6525
		// (get) Token: 0x06003B28 RID: 15144 RVA: 0x000FF332 File Offset: 0x000FD532
		public bool IsBoundToStreamingScopeInstance
		{
			get
			{
				return this.m_currentStreamingScopeInstance != null;
			}
		}

		// Token: 0x1700197E RID: 6526
		// (get) Token: 0x06003B29 RID: 15145 RVA: 0x000FF33D File Offset: 0x000FD53D
		// (set) Token: 0x06003B2A RID: 15146 RVA: 0x000FF345 File Offset: 0x000FD545
		internal RuntimeDataRegionObjReference RuntimeDataRegionObj
		{
			get
			{
				return this.m_runtimeDataRegionObj;
			}
			set
			{
				this.m_runtimeDataRegionObj = value;
			}
		}

		// Token: 0x1700197F RID: 6527
		// (get) Token: 0x06003B2B RID: 15147 RVA: 0x000FF34E File Offset: 0x000FD54E
		// (set) Token: 0x06003B2C RID: 15148 RVA: 0x000FF356 File Offset: 0x000FD556
		internal List<int> OutermostStaticColumnIndexes
		{
			get
			{
				return this.m_outermostStaticColumnIndexes;
			}
			set
			{
				this.m_outermostStaticColumnIndexes = value;
			}
		}

		// Token: 0x17001980 RID: 6528
		// (get) Token: 0x06003B2D RID: 15149 RVA: 0x000FF35F File Offset: 0x000FD55F
		// (set) Token: 0x06003B2E RID: 15150 RVA: 0x000FF367 File Offset: 0x000FD567
		internal List<int> OutermostStaticRowIndexes
		{
			get
			{
				return this.m_outermostStaticRowIndexes;
			}
			set
			{
				this.m_outermostStaticRowIndexes = value;
			}
		}

		// Token: 0x17001981 RID: 6529
		// (get) Token: 0x06003B2F RID: 15151 RVA: 0x000FF370 File Offset: 0x000FD570
		internal int CurrentCellInnerIndex
		{
			get
			{
				return this.m_currentCellInnerIndex;
			}
		}

		// Token: 0x17001982 RID: 6530
		// (get) Token: 0x06003B30 RID: 15152 RVA: 0x000FF378 File Offset: 0x000FD578
		// (set) Token: 0x06003B31 RID: 15153 RVA: 0x000FF380 File Offset: 0x000FD580
		internal IReference<RuntimeDataTablixGroupRootObj> CurrentOuterGroupRoot
		{
			get
			{
				return this.m_currentOuterGroupRoot;
			}
			set
			{
				this.m_currentOuterGroupRoot = value;
			}
		}

		// Token: 0x17001983 RID: 6531
		// (get) Token: 0x06003B32 RID: 15154 RVA: 0x000FF389 File Offset: 0x000FD589
		// (set) Token: 0x06003B33 RID: 15155 RVA: 0x000FF391 File Offset: 0x000FD591
		internal IReference<RuntimeDataTablixGroupRootObj>[] CurrentOuterGroupRootObjs
		{
			get
			{
				return this.m_currentOuterGroupRootObjs;
			}
			set
			{
				this.m_currentOuterGroupRootObjs = value;
			}
		}

		// Token: 0x17001984 RID: 6532
		// (get) Token: 0x06003B34 RID: 15156 RVA: 0x000FF39A File Offset: 0x000FD59A
		internal int[] OuterGroupingIndexes
		{
			get
			{
				return this.m_outerGroupingIndexes;
			}
		}

		// Token: 0x17001985 RID: 6533
		// (get) Token: 0x06003B35 RID: 15157 RVA: 0x000FF3A2 File Offset: 0x000FD5A2
		// (set) Token: 0x06003B36 RID: 15158 RVA: 0x000FF3AA File Offset: 0x000FD5AA
		internal bool InTablixCell
		{
			get
			{
				return this.m_inTablixCell;
			}
			set
			{
				this.m_inTablixCell = value;
			}
		}

		// Token: 0x17001986 RID: 6534
		// (get) Token: 0x06003B37 RID: 15159 RVA: 0x000FF3B3 File Offset: 0x000FD5B3
		// (set) Token: 0x06003B38 RID: 15160 RVA: 0x000FF3BB File Offset: 0x000FD5BB
		internal bool[] IsSortFilterTarget
		{
			get
			{
				return this.m_isSortFilterTarget;
			}
			set
			{
				this.m_isSortFilterTarget = value;
			}
		}

		// Token: 0x17001987 RID: 6535
		// (get) Token: 0x06003B39 RID: 15161 RVA: 0x000FF3C4 File Offset: 0x000FD5C4
		// (set) Token: 0x06003B3A RID: 15162 RVA: 0x000FF3CC File Offset: 0x000FD5CC
		internal bool[] IsSortFilterExpressionScope
		{
			get
			{
				return this.m_isSortFilterExpressionScope;
			}
			set
			{
				this.m_isSortFilterExpressionScope = value;
			}
		}

		// Token: 0x17001988 RID: 6536
		// (get) Token: 0x06003B3B RID: 15163 RVA: 0x000FF3D5 File Offset: 0x000FD5D5
		// (set) Token: 0x06003B3C RID: 15164 RVA: 0x000FF3DD File Offset: 0x000FD5DD
		internal int[] SortFilterSourceDetailScopeInfo
		{
			get
			{
				return this.m_sortFilterSourceDetailScopeInfo;
			}
			set
			{
				this.m_sortFilterSourceDetailScopeInfo = value;
			}
		}

		// Token: 0x17001989 RID: 6537
		// (get) Token: 0x06003B3D RID: 15165 RVA: 0x000FF3E6 File Offset: 0x000FD5E6
		// (set) Token: 0x06003B3E RID: 15166 RVA: 0x000FF3EE File Offset: 0x000FD5EE
		internal int CurrentColDetailIndex
		{
			get
			{
				return this.m_currentColDetailIndex;
			}
			set
			{
				this.m_currentColDetailIndex = value;
			}
		}

		// Token: 0x1700198A RID: 6538
		// (get) Token: 0x06003B3F RID: 15167 RVA: 0x000FF3F7 File Offset: 0x000FD5F7
		// (set) Token: 0x06003B40 RID: 15168 RVA: 0x000FF3FF File Offset: 0x000FD5FF
		internal int CurrentRowDetailIndex
		{
			get
			{
				return this.m_currentRowDetailIndex;
			}
			set
			{
				this.m_currentRowDetailIndex = value;
			}
		}

		// Token: 0x1700198B RID: 6539
		// (get) Token: 0x06003B41 RID: 15169 RVA: 0x000FF408 File Offset: 0x000FD608
		// (set) Token: 0x06003B42 RID: 15170 RVA: 0x000FF410 File Offset: 0x000FD610
		internal bool ProcessCellRunningValues
		{
			get
			{
				return this.m_processCellRunningValues;
			}
			set
			{
				this.m_processCellRunningValues = value;
			}
		}

		// Token: 0x1700198C RID: 6540
		// (get) Token: 0x06003B43 RID: 15171 RVA: 0x000FF419 File Offset: 0x000FD619
		// (set) Token: 0x06003B44 RID: 15172 RVA: 0x000FF421 File Offset: 0x000FD621
		internal bool ProcessOutermostStaticCellRunningValues
		{
			get
			{
				return this.m_processOutermostStaticCellRunningValues;
			}
			set
			{
				this.m_processOutermostStaticCellRunningValues = value;
			}
		}

		// Token: 0x1700198D RID: 6541
		// (get) Token: 0x06003B45 RID: 15173 RVA: 0x000FF42A File Offset: 0x000FD62A
		// (set) Token: 0x06003B46 RID: 15174 RVA: 0x000FF432 File Offset: 0x000FD632
		internal bool InOutermostStaticCells
		{
			get
			{
				return this.m_inOutermostStaticCells;
			}
			set
			{
				this.m_inOutermostStaticCells = value;
			}
		}

		// Token: 0x1700198E RID: 6542
		// (get) Token: 0x06003B47 RID: 15175 RVA: 0x000FF43B File Offset: 0x000FD63B
		int ISortFilterScope.ID
		{
			get
			{
				return this.m_ID;
			}
		}

		// Token: 0x1700198F RID: 6543
		// (get) Token: 0x06003B48 RID: 15176 RVA: 0x000FF443 File Offset: 0x000FD643
		string ISortFilterScope.ScopeName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17001990 RID: 6544
		// (get) Token: 0x06003B49 RID: 15177 RVA: 0x000FF44B File Offset: 0x000FD64B
		// (set) Token: 0x06003B4A RID: 15178 RVA: 0x000FF453 File Offset: 0x000FD653
		bool[] ISortFilterScope.IsSortFilterTarget
		{
			get
			{
				return this.m_isSortFilterTarget;
			}
			set
			{
				this.m_isSortFilterTarget = value;
			}
		}

		// Token: 0x17001991 RID: 6545
		// (get) Token: 0x06003B4B RID: 15179 RVA: 0x000FF45C File Offset: 0x000FD65C
		// (set) Token: 0x06003B4C RID: 15180 RVA: 0x000FF464 File Offset: 0x000FD664
		bool[] ISortFilterScope.IsSortFilterExpressionScope
		{
			get
			{
				return this.m_isSortFilterExpressionScope;
			}
			set
			{
				this.m_isSortFilterExpressionScope = value;
			}
		}

		// Token: 0x17001992 RID: 6546
		// (get) Token: 0x06003B4D RID: 15181 RVA: 0x000FF46D File Offset: 0x000FD66D
		// (set) Token: 0x06003B4E RID: 15182 RVA: 0x000FF475 File Offset: 0x000FD675
		List<ExpressionInfo> ISortFilterScope.UserSortExpressions
		{
			get
			{
				return this.m_userSortExpressions;
			}
			set
			{
				this.m_userSortExpressions = value;
			}
		}

		// Token: 0x17001993 RID: 6547
		// (get) Token: 0x06003B4F RID: 15183 RVA: 0x000FF47E File Offset: 0x000FD67E
		IndexedExprHost ISortFilterScope.UserSortExpressionsHost
		{
			get
			{
				return this.UserSortExpressionsHost;
			}
		}

		// Token: 0x17001994 RID: 6548
		// (get) Token: 0x06003B50 RID: 15184
		protected abstract IndexedExprHost UserSortExpressionsHost { get; }

		// Token: 0x17001995 RID: 6549
		// (get) Token: 0x06003B51 RID: 15185 RVA: 0x000FF486 File Offset: 0x000FD686
		// (set) Token: 0x06003B52 RID: 15186 RVA: 0x000FF48E File Offset: 0x000FD68E
		internal bool ColumnScopeFound
		{
			get
			{
				return this.m_columnScopeFound;
			}
			set
			{
				this.m_columnScopeFound = value;
			}
		}

		// Token: 0x17001996 RID: 6550
		// (get) Token: 0x06003B53 RID: 15187 RVA: 0x000FF497 File Offset: 0x000FD697
		// (set) Token: 0x06003B54 RID: 15188 RVA: 0x000FF49F File Offset: 0x000FD69F
		internal bool RowScopeFound
		{
			get
			{
				return this.m_rowScopeFound;
			}
			set
			{
				this.m_rowScopeFound = value;
			}
		}

		// Token: 0x17001997 RID: 6551
		// (get) Token: 0x06003B55 RID: 15189 RVA: 0x000FF4A8 File Offset: 0x000FD6A8
		// (set) Token: 0x06003B56 RID: 15190 RVA: 0x000FF4B0 File Offset: 0x000FD6B0
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

		// Token: 0x17001998 RID: 6552
		// (get) Token: 0x06003B57 RID: 15191 RVA: 0x000FF4B9 File Offset: 0x000FD6B9
		public IndexedInCollectionType IndexedInCollectionType
		{
			get
			{
				return IndexedInCollectionType.DataRegion;
			}
		}

		// Token: 0x17001999 RID: 6553
		// (get) Token: 0x06003B58 RID: 15192 RVA: 0x000FF4BC File Offset: 0x000FD6BC
		// (set) Token: 0x06003B59 RID: 15193 RVA: 0x000FF4C4 File Offset: 0x000FD6C4
		internal DataRegionInstance CurrentDataRegionInstance
		{
			get
			{
				return this.m_currentDataRegionInstance;
			}
			set
			{
				this.m_currentDataRegionInstance = value;
			}
		}

		// Token: 0x1700199A RID: 6554
		// (get) Token: 0x06003B5A RID: 15194 RVA: 0x000FF4CD File Offset: 0x000FD6CD
		internal List<IInScopeEventSource> InScopeEventSources
		{
			get
			{
				return this.m_inScopeEventSources;
			}
		}

		// Token: 0x1700199B RID: 6555
		// (get) Token: 0x06003B5B RID: 15195 RVA: 0x000FF4D5 File Offset: 0x000FD6D5
		internal int OuterGroupingMaximumDynamicLevel
		{
			get
			{
				return this.m_outerGroupingMaximumDynamicLevel;
			}
		}

		// Token: 0x1700199C RID: 6556
		// (get) Token: 0x06003B5C RID: 15196 RVA: 0x000FF4DD File Offset: 0x000FD6DD
		internal int OuterGroupingDynamicMemberCount
		{
			get
			{
				return this.m_outerGroupingDynamicMemberCount;
			}
		}

		// Token: 0x1700199D RID: 6557
		// (get) Token: 0x06003B5D RID: 15197 RVA: 0x000FF4E5 File Offset: 0x000FD6E5
		internal int OuterGroupingDynamicPathCount
		{
			get
			{
				return this.m_outerGroupingDynamicPathCount;
			}
		}

		// Token: 0x1700199E RID: 6558
		// (get) Token: 0x06003B5E RID: 15198 RVA: 0x000FF4ED File Offset: 0x000FD6ED
		// (set) Token: 0x06003B5F RID: 15199 RVA: 0x000FF4F5 File Offset: 0x000FD6F5
		internal InitializationContext.ScopeChainInfo ScopeChainInfo
		{
			get
			{
				return this.m_scopeChainInfo;
			}
			set
			{
				this.m_scopeChainInfo = value;
			}
		}

		// Token: 0x1700199F RID: 6559
		// (get) Token: 0x06003B60 RID: 15200 RVA: 0x000FF4FE File Offset: 0x000FD6FE
		internal int InnerGroupingMaximumDynamicLevel
		{
			get
			{
				return this.m_innerGroupingMaximumDynamicLevel;
			}
		}

		// Token: 0x170019A0 RID: 6560
		// (get) Token: 0x06003B61 RID: 15201 RVA: 0x000FF506 File Offset: 0x000FD706
		internal int InnerGroupingDynamicMemberCount
		{
			get
			{
				return this.m_innerGroupingDynamicMemberCount;
			}
		}

		// Token: 0x170019A1 RID: 6561
		// (get) Token: 0x06003B62 RID: 15202 RVA: 0x000FF50E File Offset: 0x000FD70E
		internal int InnerGroupingDynamicPathCount
		{
			get
			{
				return this.m_innerGroupingDynamicPathCount;
			}
		}

		// Token: 0x170019A2 RID: 6562
		// (get) Token: 0x06003B63 RID: 15203 RVA: 0x000FF518 File Offset: 0x000FD718
		internal int RowDomainScopeCount
		{
			get
			{
				if (this.RowMembers == null)
				{
					this.m_rowDomainScopeCount = new int?(0);
				}
				else if (this.m_rowDomainScopeCount == null)
				{
					this.m_rowDomainScopeCount = new int?(this.RowMembers.Count - this.RowMembers.OriginalNodeCount);
				}
				return this.m_rowDomainScopeCount.Value;
			}
		}

		// Token: 0x170019A3 RID: 6563
		// (get) Token: 0x06003B64 RID: 15204 RVA: 0x000FF578 File Offset: 0x000FD778
		internal int ColumnDomainScopeCount
		{
			get
			{
				if (this.ColumnMembers == null)
				{
					this.m_colDomainScopeCount = new int?(0);
				}
				else if (this.m_colDomainScopeCount == null)
				{
					this.m_colDomainScopeCount = new int?(this.ColumnMembers.Count - this.ColumnMembers.OriginalNodeCount);
				}
				return this.m_colDomainScopeCount.Value;
			}
		}

		// Token: 0x170019A4 RID: 6564
		// (get) Token: 0x06003B65 RID: 15205 RVA: 0x000FF5D5 File Offset: 0x000FD7D5
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> DataRegionScopedItemsForDataProcessing
		{
			get
			{
				if (this.m_dataRegionScopedItemsForDataProcessing == null)
				{
					this.m_dataRegionScopedItemsForDataProcessing = this.ComputeDataRegionScopedItemsForDataProcessing();
				}
				return this.m_dataRegionScopedItemsForDataProcessing;
			}
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x000FF5F4 File Offset: 0x000FD7F4
		protected virtual List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> ComputeDataRegionScopedItemsForDataProcessing()
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> list = null;
			if (this.OutermostStaticRowIndexes != null && this.OutermostStaticColumnIndexes != null)
			{
				foreach (int num in this.OutermostStaticRowIndexes)
				{
					foreach (int num2 in this.OutermostStaticColumnIndexes)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.MergeDataProcessingItems(this.Rows[num].Cells[num2], ref list);
					}
				}
			}
			if (this.OuterMembers != null)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.MergeDataProcessingItems(this.OuterMembers.StaticMembersInSameScope, ref list);
			}
			if (this.InnerMembers != null)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.MergeDataProcessingItems(this.InnerMembers.StaticMembersInSameScope, ref list);
			}
			return list;
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x000FF6E8 File Offset: 0x000FD8E8
		private static void MergeDataProcessingItems(HierarchyNodeList staticMembers, ref List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> results)
		{
			if (staticMembers == null)
			{
				return;
			}
			for (int i = 0; i < staticMembers.Count; i++)
			{
				RuntimeRICollection.MergeDataProcessingItems(staticMembers[i].MemberContentCollection, ref results);
			}
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x000FF71C File Offset: 0x000FD91C
		protected static void MergeDataProcessingItems(Cell rifCell, ref List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> results)
		{
			if (rifCell != null)
			{
				RuntimeRICollection.MergeDataProcessingItems(rifCell.CellContentCollection, ref results);
			}
		}

		// Token: 0x170019A5 RID: 6565
		// (get) Token: 0x06003B69 RID: 15209 RVA: 0x000FF72D File Offset: 0x000FD92D
		// (set) Token: 0x06003B6A RID: 15210 RVA: 0x000FF735 File Offset: 0x000FD935
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

		// Token: 0x06003B6B RID: 15211 RVA: 0x000FF746 File Offset: 0x000FD946
		void IRIFReportScope.AddInScopeTextBox(Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textbox)
		{
			this.AddInScopeTextBox(textbox);
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x000FF74F File Offset: 0x000FD94F
		protected virtual void AddInScopeTextBox(Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textbox)
		{
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x000FF751 File Offset: 0x000FD951
		void IRIFReportScope.ResetTextBoxImpls(OnDemandProcessingContext context)
		{
			this.ResetTextBoxImpls(context);
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x000FF75A File Offset: 0x000FD95A
		internal virtual void ResetTextBoxImpls(OnDemandProcessingContext context)
		{
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x000FF75C File Offset: 0x000FD95C
		void IRIFReportScope.AddInScopeEventSource(IInScopeEventSource eventSource)
		{
			if (this.m_inScopeEventSources == null)
			{
				this.m_inScopeEventSources = new List<IInScopeEventSource>();
			}
			this.m_inScopeEventSources.Add(eventSource);
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x000FF780 File Offset: 0x000FD980
		public virtual void CreateDomainScopeMember(ReportHierarchyNode parentNode, Grouping grouping, AutomaticSubtotalContext context)
		{
			ReportHierarchyNode reportHierarchyNode = this.CreateHierarchyNode(context.GenerateID());
			reportHierarchyNode.Grouping = grouping.CloneForDomainScope(context, reportHierarchyNode);
			bool flag = parentNode != null && parentNode.IsColumn;
			HierarchyNodeList hierarchyNodeList = ((parentNode != null) ? parentNode.InnerHierarchy : this.RowMembers);
			if (hierarchyNodeList == null)
			{
				return;
			}
			hierarchyNodeList.Add(reportHierarchyNode);
			reportHierarchyNode.IsColumn = flag;
			this.CreateDomainScopeRowsAndCells(context, reportHierarchyNode);
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x000FF7E4 File Offset: 0x000FD9E4
		protected virtual void CreateDomainScopeRowsAndCells(AutomaticSubtotalContext context, ReportHierarchyNode member)
		{
			if (!member.IsColumn)
			{
				Row row = this.CreateRow(context.GenerateID(), this.ColumnCount);
				for (int i = 0; i < this.ColumnCount; i++)
				{
					row.Cells.Add(this.CreateCell(context.GenerateID(), -1, i));
				}
				this.Rows.Insert(this.RowMembers.GetMemberIndex(member), row);
				this.RowCount++;
				return;
			}
			int memberIndex = this.ColumnMembers.GetMemberIndex(member);
			for (int j = 0; j < this.RowCount; j++)
			{
				this.Rows[j].Cells.Insert(memberIndex, this.CreateCell(context.GenerateID(), j, -1));
			}
			this.ColumnCount++;
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x000FF8B2 File Offset: 0x000FDAB2
		protected virtual ReportHierarchyNode CreateHierarchyNode(int id)
		{
			return null;
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x000FF8B5 File Offset: 0x000FDAB5
		protected virtual Row CreateRow(int id, int columnCount)
		{
			return null;
		}

		// Token: 0x06003B74 RID: 15220 RVA: 0x000FF8B8 File Offset: 0x000FDAB8
		protected virtual Cell CreateCell(int id, int rowIndex, int colIndex)
		{
			return null;
		}

		// Token: 0x170019A6 RID: 6566
		// (get) Token: 0x06003B75 RID: 15221 RVA: 0x000FF8BB File Offset: 0x000FDABB
		public DataScopeInfo DataScopeInfo
		{
			get
			{
				return this.m_dataScopeInfo;
			}
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x000FF8C4 File Offset: 0x000FDAC4
		internal override bool Initialize(InitializationContext context)
		{
			context.IsDataRegionScopedCell = true;
			base.Initialize(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context);
			}
			this.m_dataScopeInfo.ValidateScopeRulesForIdc(context, this);
			if (context.PublishingContext.PublishingVersioning.IsRdlFeatureRestricted(RdlFeatures.PeerGroups) && context.HasPeerGroups(this))
			{
				string text = "TablixMembers";
				if (this.ObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.DataShape)
				{
					text = "DataShapeMembers";
				}
				else if (this.ObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart)
				{
					text = "ChartMembers";
				}
				else if (this.ObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.CustomReportItem)
				{
					text = "DataMembers";
				}
				else if (this.ObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Map)
				{
					text = "MapMember";
				}
				else if (this.ObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel)
				{
					text = "GaugeMember";
				}
				context.ErrorContext.Register(ProcessingErrorCode.rsInvalidPeerGroupsNotSupported, Severity.Error, context.ObjectType, context.ObjectName, text, Array.Empty<string>());
			}
			if ((context.Location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				return false;
			}
			if (this.IsDataRegion)
			{
				this.m_dataScopeInfo.Initialize(context, this);
			}
			context.InitializeAbsolutePosition(this);
			context.UpdateTopLeftDataRegion(this);
			context.InAutoSubtotalClone = false;
			if (this.m_pageBreak != null)
			{
				this.m_pageBreak.Initialize(context);
			}
			if (this.m_pageName != null)
			{
				this.m_pageName.Initialize("PageName", context);
				context.ExprHostBuilder.PageName(this.m_pageName);
			}
			if (this.m_sorting != null)
			{
				this.m_sorting.Initialize(context);
			}
			if (this.m_filters != null)
			{
				for (int i = 0; i < this.m_filters.Count; i++)
				{
					this.m_filters[i].Initialize(context);
				}
			}
			if (this.m_noRowsMessage != null)
			{
				this.m_noRowsMessage.Initialize("NoRows", context);
				context.ExprHostBuilder.GenericNoRows(this.m_noRowsMessage);
			}
			if (this.m_userSortExpressions != null)
			{
				context.ExprHostBuilder.UserSortExpressionsStart();
				for (int j = 0; j < this.m_userSortExpressions.Count; j++)
				{
					ExpressionInfo expressionInfo = this.m_userSortExpressions[j];
					context.ExprHostBuilder.UserSortExpression(expressionInfo);
				}
				context.ExprHostBuilder.UserSortExpressionsEnd();
			}
			context.RegisterRunningValues(this.m_runningValues, this.m_dataScopeInfo.RunningValuesOfAggregates);
			context.IsTopLevelCellContents = false;
			this.InitializeCorner(context);
			context.ResetMemberAndCellIndexInCollectionTable();
			context.Location &= ~Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegionCellTopLevelItem;
			bool flag = this.InitializeRows(context);
			if (this.ValidateInnerStructure(context))
			{
				context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegionGroupHeader;
				bool flag2 = this.InitializeMembers(context);
				context.Location &= ~Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegionGroupHeader;
				if (flag2 && flag)
				{
					this.InitializeData(context);
					this.m_outerGroupingMaximumDynamicLevel = this.GetMaximumDynamicLevelAndAssignHierarchyIndexes(this.OuterMembers, 0, ref this.m_outerGroupingDynamicMemberCount, ref this.m_outerGroupingDynamicPathCount);
					this.m_innerGroupingMaximumDynamicLevel = this.GetMaximumDynamicLevelAndAssignHierarchyIndexes(this.InnerMembers, 0, ref this.m_innerGroupingDynamicMemberCount, ref this.m_innerGroupingDynamicPathCount);
				}
			}
			context.UnRegisterRunningValues(this.m_runningValues, this.m_dataScopeInfo.RunningValuesOfAggregates);
			if (this.IsDataRegion)
			{
				if (context.EvaluateAtomicityCondition(this.m_sorting != null && !this.m_sorting.NaturalSort, this, AtomicityReason.Sorts) || context.EvaluateAtomicityCondition(this.m_filters != null, this, AtomicityReason.Filters) || context.EvaluateAtomicityCondition(this.HasAggregatesForAtomicityCheck(), this, AtomicityReason.Aggregates) || context.EvaluateAtomicityCondition(context.HasMultiplePeerChildScopes(this), this, AtomicityReason.PeerChildScopes))
				{
					context.FoundAtomicScope(this);
				}
				else
				{
					this.m_dataScopeInfo.IsDecomposable = true;
				}
			}
			return false;
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x000FFC43 File Offset: 0x000FDE43
		private bool HasAggregatesForAtomicityCheck()
		{
			return DataScopeInfo.HasNonServerAggregates<DataAggregateInfo>(this.m_aggregates) || DataScopeInfo.HasAggregates<DataAggregateInfo>(this.m_postSortAggregates) || DataScopeInfo.HasAggregates<RunningValueInfo>(this.m_runningValues) || this.m_dataScopeInfo.HasAggregatesOrRunningValues;
		}

		// Token: 0x06003B78 RID: 15224 RVA: 0x000FFC7C File Offset: 0x000FDE7C
		private int GetMaximumDynamicLevelAndAssignHierarchyIndexes(HierarchyNodeList members, int parentDynamicLevels, ref int hierarchyDynamicIndex, ref int hierarchyPathIndex)
		{
			if (members == null)
			{
				return parentDynamicLevels;
			}
			int count = members.Count;
			int num = parentDynamicLevels;
			for (int i = 0; i < count; i++)
			{
				int num2 = parentDynamicLevels;
				ReportHierarchyNode reportHierarchyNode = members[i];
				if (!reportHierarchyNode.IsStatic)
				{
					ReportHierarchyNode reportHierarchyNode2 = reportHierarchyNode;
					int num3 = hierarchyDynamicIndex;
					hierarchyDynamicIndex = num3 + 1;
					reportHierarchyNode2.HierarchyDynamicIndex = num3;
					if (reportHierarchyNode.HasInnerDynamic)
					{
						reportHierarchyNode.HierarchyPathIndex = hierarchyPathIndex;
						num2 = this.GetMaximumDynamicLevelAndAssignHierarchyIndexes(reportHierarchyNode.InnerDynamicMembers, parentDynamicLevels + 1, ref hierarchyDynamicIndex, ref hierarchyPathIndex);
					}
					else
					{
						ReportHierarchyNode reportHierarchyNode3 = reportHierarchyNode;
						num3 = hierarchyPathIndex;
						hierarchyPathIndex = num3 + 1;
						reportHierarchyNode3.HierarchyPathIndex = num3;
						num2 = parentDynamicLevels + 1;
					}
				}
				else if (reportHierarchyNode.HasInnerDynamic)
				{
					num2 = this.GetMaximumDynamicLevelAndAssignHierarchyIndexes(reportHierarchyNode.InnerDynamicMembers, parentDynamicLevels, ref hierarchyDynamicIndex, ref hierarchyPathIndex);
				}
				num = Math.Max(num, num2);
			}
			return num;
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x000FFD38 File Offset: 0x000FDF38
		protected GroupingList GenerateUserSortGroupingList(bool rowIsInnerGrouping)
		{
			GroupingList groupingList = new GroupingList();
			HierarchyNodeList hierarchyNodeList = (rowIsInnerGrouping ? this.RowMembers : this.ColumnMembers);
			this.AddGroupsToList(hierarchyNodeList, groupingList);
			hierarchyNodeList = (rowIsInnerGrouping ? this.ColumnMembers : this.RowMembers);
			this.AddGroupsToList(hierarchyNodeList, groupingList);
			return groupingList;
		}

		// Token: 0x06003B7A RID: 15226 RVA: 0x000FFD80 File Offset: 0x000FDF80
		private void AddGroupsToList(HierarchyNodeList members, GroupingList groups)
		{
			foreach (object obj in members)
			{
				ReportHierarchyNode reportHierarchyNode = (ReportHierarchyNode)obj;
				if (reportHierarchyNode.Grouping != null)
				{
					groups.Add(reportHierarchyNode.Grouping);
				}
				if (reportHierarchyNode.InnerHierarchy != null)
				{
					this.AddGroupsToList(reportHierarchyNode.InnerHierarchy, groups);
				}
			}
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x000FFDF8 File Offset: 0x000FDFF8
		protected virtual bool InitializeRows(InitializationContext context)
		{
			bool flag = true;
			if (((this.ColumnMembers == null || this.RowMembers == null) && this.Rows != null) || (this.ColumnMembers != null && this.RowMembers != null && this.Rows == null) || (this.Rows != null && this.Rows.Count != this.m_rowCount))
			{
				context.ErrorContext.Register((context.ObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart) ? ProcessingErrorCode.rsWrongNumberOfChartSeries : ProcessingErrorCode.rsWrongNumberOfDataRows, Severity.Error, context.ObjectType, context.ObjectName, this.m_rowCount.ToString(CultureInfo.InvariantCulture), Array.Empty<string>());
				return false;
			}
			if (this.Rows != null)
			{
				for (int i = 0; i < this.Rows.Count; i++)
				{
					Row row = this.Rows[i];
					if (row == null || row.Cells == null || row.Cells.Count != this.m_columnCount)
					{
						context.ErrorContext.Register((context.ObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.CustomReportItem) ? ProcessingErrorCode.rsWrongNumberOfDataCellsInDataRow : ProcessingErrorCode.rsWrongNumberOfChartDataPointsInSeries, Severity.Error, context.ObjectType, context.ObjectName, (context.ObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.CustomReportItem) ? "DataCell" : "ChartDataPoint", new string[] { i.ToString(CultureInfo.CurrentCulture) });
						flag = false;
					}
					row.Initialize(context);
				}
			}
			return flag;
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x000FFF57 File Offset: 0x000FE157
		protected virtual void InitializeCorner(InitializationContext context)
		{
		}

		// Token: 0x06003B7D RID: 15229
		protected abstract bool ValidateInnerStructure(InitializationContext context);

		// Token: 0x06003B7E RID: 15230 RVA: 0x000FFF5C File Offset: 0x000FE15C
		protected virtual bool InitializeMembers(InitializationContext context)
		{
			bool flag = true;
			if (this.m_rowCount != 0 && this.m_columnCount != 0 && (this.Rows == null || this.Rows.Count == 0))
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsMissingDataCells, Severity.Error, context.ObjectType, context.ObjectName, "DataRows", Array.Empty<string>());
				flag = false;
			}
			context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InTablixColumnHierarchy;
			flag &= this.InitializeColumnMembers(context);
			context.ResetMemberAndCellIndexInCollectionTable();
			context.Location &= ~Microsoft.ReportingServices.ReportPublishing.LocationFlags.InTablixColumnHierarchy;
			context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InTablixRowHierarchy;
			flag &= this.InitializeRowMembers(context);
			context.ResetMemberAndCellIndexInCollectionTable();
			context.Location &= ~Microsoft.ReportingServices.ReportPublishing.LocationFlags.InTablixRowHierarchy;
			return flag;
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x0010002C File Offset: 0x000FE22C
		protected virtual bool InitializeColumnMembers(InitializationContext context)
		{
			HierarchyNodeList columnMembers = this.ColumnMembers;
			context.MemberCellIndex = 0;
			if (columnMembers == null || columnMembers.Count == 0)
			{
				return false;
			}
			foreach (object obj in columnMembers)
			{
				ReportHierarchyNode reportHierarchyNode = (ReportHierarchyNode)obj;
				context.InAutoSubtotalClone = reportHierarchyNode.IsAutoSubtotal;
				this.m_hasDynamicColumnMember |= reportHierarchyNode.Initialize(context);
			}
			if (columnMembers.Count == 1 && columnMembers[0].IsStatic)
			{
				context.SpecialTransferRunningValues(columnMembers[0].RunningValues, columnMembers[0].DataScopeInfo.RunningValuesOfAggregates);
			}
			return true;
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x001000F4 File Offset: 0x000FE2F4
		protected virtual bool InitializeRowMembers(InitializationContext context)
		{
			HierarchyNodeList rowMembers = this.RowMembers;
			context.MemberCellIndex = 0;
			if (rowMembers == null || rowMembers.Count == 0)
			{
				return false;
			}
			foreach (object obj in rowMembers)
			{
				ReportHierarchyNode reportHierarchyNode = (ReportHierarchyNode)obj;
				this.m_hasDynamicRowMember |= reportHierarchyNode.Initialize(context);
			}
			if (rowMembers.Count == 1 && rowMembers[0].IsStatic)
			{
				context.SpecialTransferRunningValues(rowMembers[0].RunningValues, rowMembers[0].DataScopeInfo.RunningValuesOfAggregates);
			}
			return true;
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x001001B0 File Offset: 0x000FE3B0
		protected virtual void InitializeData(InitializationContext context)
		{
			this.m_textboxesInScope = context.GetCurrentReferencableTextboxes();
			this.m_variablesInScope = context.GetCurrentReferencableVariables();
			if (context.ObjectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart)
			{
				context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegionCellTopLevelItem;
			}
			context.TablixName = this.m_name;
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = context.RegisterDataRegionCellScope(this, this.m_columnCount == 1 && this.ColumnMembers[0].Grouping == null, this.m_aggregates, this.m_postSortAggregates);
			int num = 0;
			for (int i = 0; i < this.RowMembers.Count; i++)
			{
				this.InitializeDataRows(ref num, this.RowMembers[i], context);
			}
			if (context.IsRunningValueDirectionColumn || (!this.m_hasDynamicRowMember && this.m_hasDynamicColumnMember))
			{
				this.m_processingInnerGrouping = Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.ProcessingInnerGroupings.Row;
			}
			if (this.IsColumnGroupingSwitched && this.m_hasDynamicColumnMember)
			{
				this.m_processingInnerGrouping = Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.ProcessingInnerGroupings.Row;
			}
			context.UnRegisterTablixCellScope(dataRegion);
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x001002A4 File Offset: 0x000FE4A4
		protected void InitializeDataRows(ref int index, ReportHierarchyNode member, InitializationContext context)
		{
			member.HierarchyParentGroups = context.GetContainingScopesInCurrentDataRegion();
			bool suspendErrors = context.ErrorContext.SuspendErrors;
			bool inRecursiveHierarchyRows = context.InRecursiveHierarchyRows;
			context.ErrorContext.SuspendErrors |= member.IsAutoSubtotal;
			context.InAutoSubtotalClone = member.IsAutoSubtotal;
			bool flag = member.PreInitializeDataMember(context);
			member.CaptureReferencableTextboxes(context);
			if (member.Grouping != null)
			{
				context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InGrouping;
				if (member.Grouping.IsDetail)
				{
					context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDetail;
				}
				context.IsDataRegionScopedCell = false;
				if (member.Grouping.Variables != null)
				{
					context.RegisterVariables(member.Grouping.Variables);
				}
				context.RegisterGroupingScopeForDataRegionCell(member);
				context.InRecursiveHierarchyRows = member.Grouping.Parent != null;
			}
			else if (member.IsNonToggleableHiddenMember)
			{
				context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InNonToggleableHiddenStaticTablixMember;
			}
			if (member.InnerHierarchy == null)
			{
				this.InitializeDataColumns(member.ID, index, context);
				index++;
			}
			else
			{
				HierarchyNodeList innerHierarchy = member.InnerHierarchy;
				for (int i = 0; i < innerHierarchy.Count; i++)
				{
					this.InitializeDataRows(ref index, innerHierarchy[i], context);
				}
			}
			member.PostInitializeDataMember(context, flag);
			if (member.Grouping != null)
			{
				context.UnRegisterGroupingScopeForDataRegionCell(member);
				if (member.Grouping.Variables != null)
				{
					context.UnregisterVariables(member.Grouping.Variables);
				}
			}
			context.InRecursiveHierarchyRows = inRecursiveHierarchyRows;
			context.ErrorContext.SuspendErrors = suspendErrors;
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x00100434 File Offset: 0x000FE634
		protected virtual void InitializeDataColumns(int parentRowID, int rowIndex, InitializationContext context)
		{
			int num = 0;
			for (int i = 0; i < this.ColumnMembers.Count; i++)
			{
				this.InitializeDataColumns(parentRowID, rowIndex, ref num, this.ColumnMembers[i], context);
			}
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x00100470 File Offset: 0x000FE670
		protected virtual void InitializeDataColumns(int parentRowID, int rowIndex, ref int columnIndex, ReportHierarchyNode member, InitializationContext context)
		{
			member.HierarchyParentGroups = context.GetContainingScopesInCurrentDataRegion();
			bool suspendErrors = context.ErrorContext.SuspendErrors;
			bool inRecursiveHierarchyColumns = context.InRecursiveHierarchyColumns;
			context.ErrorContext.SuspendErrors |= member.IsAutoSubtotal;
			context.InAutoSubtotalClone = member.IsAutoSubtotal;
			bool flag = member.PreInitializeDataMember(context);
			member.CaptureReferencableTextboxes(context);
			if (member.Grouping != null)
			{
				context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InGrouping;
				if (member.Grouping.IsDetail)
				{
					context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDetail;
				}
				context.IsDataRegionScopedCell = false;
				if (member.Grouping.Variables != null)
				{
					context.RegisterVariables(member.Grouping.Variables);
				}
				context.RegisterGroupingScopeForDataRegionCell(member);
				context.InRecursiveHierarchyColumns = member.Grouping.Parent != null;
			}
			else if (member.IsNonToggleableHiddenMember)
			{
				context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InNonToggleableHiddenStaticTablixMember;
			}
			if (member.InnerHierarchy == null)
			{
				context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InTablixCell;
				if (context.CellHasDynamicRowsAndColumns)
				{
					context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDynamicTablixCell;
				}
				if (this.Rows[rowIndex].Cells != null && rowIndex < this.Rows.Count && columnIndex < this.Rows[rowIndex].Cells.Count)
				{
					Cell cell = this.Rows[rowIndex].Cells[columnIndex];
					cell.Initialize(parentRowID, member.ID, rowIndex, columnIndex, context);
					if ((context.ObjectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Tablix || !context.HasUserSorts) && !context.IsDataRegionScopedCell)
					{
						this.CopyCellAggregates(cell);
					}
				}
				columnIndex++;
			}
			else
			{
				HierarchyNodeList innerHierarchy = member.InnerHierarchy;
				for (int i = 0; i < innerHierarchy.Count; i++)
				{
					this.InitializeDataColumns(parentRowID, rowIndex, ref columnIndex, innerHierarchy[i], context);
				}
			}
			member.PostInitializeDataMember(context, flag);
			if (member.Grouping != null)
			{
				context.UnRegisterGroupingScopeForDataRegionCell(member);
				if (member.Grouping.Variables != null)
				{
					context.UnregisterVariables(member.Grouping.Variables);
				}
			}
			context.InRecursiveHierarchyColumns = inRecursiveHierarchyColumns;
			context.ErrorContext.SuspendErrors = suspendErrors;
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x001006C8 File Offset: 0x000FE8C8
		internal override void InitializeRVDirectionDependentItems(InitializationContext context)
		{
			if (this.IsDataRegion && context.RegisterDataRegion(this))
			{
				context.IsDataRegionScopedCell = true;
				context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataSet | Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion;
				context.ObjectType = this.ObjectType;
				context.ObjectName = base.Name;
				context.RegisterRunningValues(this.m_runningValues, this.m_dataScopeInfo.RunningValuesOfAggregates);
				this.InitializeRVDirectionDependentItemsInCorner(context);
				Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = context.RegisterDataRegionCellScope(this, this.m_columnCount == 1 && this.ColumnMembers[0].Grouping == null, this.m_aggregates, this.m_postSortAggregates);
				context.Location &= ~Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegionCellTopLevelItem;
				this.InitializeRVDirectionDependentItems(context, false);
				this.InitializeRVDirectionDependentItems(context, true);
				int num = 0;
				int num2 = 0;
				this.InitializeRVDirectionDependentItems(ref num, ref num2, context, false, true);
				context.ProcessUserSortScopes(this.m_name);
				context.EventSourcesWithDetailSortExpressionInitialize(this.m_name);
				context.UnRegisterRunningValues(this.m_runningValues, this.m_dataScopeInfo.RunningValuesOfAggregates);
				context.UnRegisterTablixCellScope(dataRegion);
				context.UnRegisterDataRegion(this);
			}
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x001007EC File Offset: 0x000FE9EC
		private void InitializeRVDirectionDependentItems(InitializationContext context, bool traverseInner)
		{
			int num = 0;
			int num2 = 0;
			this.InitializeRVDirectionDependentItems(ref num, ref num2, context, traverseInner, false);
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x0010080C File Offset: 0x000FEA0C
		private void InitializeRVDirectionDependentItems(ref int outerIndex, ref int innerIndex, InitializationContext context, bool traverseInner, bool initializeCells)
		{
			HierarchyNodeList hierarchyNodeList;
			if (this.m_processingInnerGrouping == Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.ProcessingInnerGroupings.Column != traverseInner)
			{
				hierarchyNodeList = this.RowMembers;
			}
			else
			{
				hierarchyNodeList = this.ColumnMembers;
			}
			for (int i = 0; i < hierarchyNodeList.Count; i++)
			{
				this.InitializeRVDirectionDependentItems(ref outerIndex, ref innerIndex, hierarchyNodeList[i], context, traverseInner, initializeCells);
			}
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x0010085C File Offset: 0x000FEA5C
		private void InitializeRVDirectionDependentItems(ref int outerIndex, ref int innerIndex, ReportHierarchyNode member, InitializationContext context, bool traverseInner, bool initializeCells)
		{
			member.HierarchyParentGroups = context.GetContainingScopesInCurrentDataRegion();
			if (member.Grouping != null)
			{
				context.ObjectType = Microsoft.ReportingServices.ReportProcessing.ObjectType.Grouping;
				context.ObjectName = member.Grouping.Name;
				context.IsDataRegionScopedCell = false;
				context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InGrouping;
				List<ExpressionInfo> groupExpressions = member.Grouping.GroupExpressions;
				if (groupExpressions == null || groupExpressions.Count == 0)
				{
					context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDetail;
				}
				context.RegisterGroupingScopeForDataRegionCell(member);
				if (member.Grouping.Variables != null)
				{
					context.RegisterVariables(member.Grouping.Variables);
				}
			}
			if (!initializeCells)
			{
				member.InitializeRVDirectionDependentItems(context);
			}
			if (member.InnerHierarchy == null)
			{
				if (initializeCells)
				{
					if (traverseInner)
					{
						this.InitializeRVDirectionDependentItems(outerIndex, innerIndex, context);
						innerIndex++;
					}
					else
					{
						innerIndex = 0;
						this.InitializeRVDirectionDependentItems(ref outerIndex, ref innerIndex, context, true, initializeCells);
						outerIndex++;
					}
				}
			}
			else
			{
				HierarchyNodeList innerHierarchy = member.InnerHierarchy;
				for (int i = 0; i < innerHierarchy.Count; i++)
				{
					this.InitializeRVDirectionDependentItems(ref outerIndex, ref innerIndex, innerHierarchy[i], context, traverseInner, initializeCells);
				}
			}
			if (member.Grouping != null)
			{
				if (initializeCells)
				{
					context.ProcessUserSortScopes(member.Grouping.Name);
					context.EventSourcesWithDetailSortExpressionInitialize(member.Grouping.Name);
				}
				context.UnRegisterGroupingScopeForDataRegionCell(member);
				if (member.Grouping.Variables != null)
				{
					context.UnregisterVariables(member.Grouping.Variables);
				}
			}
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x001009CD File Offset: 0x000FEBCD
		protected virtual void InitializeRVDirectionDependentItemsInCorner(InitializationContext context)
		{
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x001009CF File Offset: 0x000FEBCF
		protected virtual void InitializeRVDirectionDependentItems(int outerIndex, int innerIndex, InitializationContext context)
		{
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x001009D4 File Offset: 0x000FEBD4
		internal override void DetermineGroupingExprValueCount(InitializationContext context, int groupingExprCount)
		{
			this.DetermineGroupingExprValueCountInCorner(context, groupingExprCount);
			int num = 0;
			int num2 = 0;
			this.DetermineGroupingExprValueCount(ref num, ref num2, context, false, groupingExprCount);
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x001009FC File Offset: 0x000FEBFC
		private void DetermineGroupingExprValueCount(ref int outerIndex, ref int innerIndex, InitializationContext context, bool traverseInner, int groupingExprCount)
		{
			HierarchyNodeList hierarchyNodeList;
			if (this.m_processingInnerGrouping == Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.ProcessingInnerGroupings.Column != traverseInner)
			{
				hierarchyNodeList = this.RowMembers;
			}
			else
			{
				hierarchyNodeList = this.ColumnMembers;
			}
			for (int i = 0; i < hierarchyNodeList.Count; i++)
			{
				this.DetermineGroupingExprValueCount(ref outerIndex, ref innerIndex, hierarchyNodeList[i], context, traverseInner, groupingExprCount);
			}
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x00100A4C File Offset: 0x000FEC4C
		private void DetermineGroupingExprValueCount(ref int outerIndex, ref int innerIndex, ReportHierarchyNode member, InitializationContext context, bool traverseInner, int groupingExprCount)
		{
			if (member.Grouping != null)
			{
				List<ExpressionInfo> groupExpressions = member.Grouping.GroupExpressions;
				if (groupExpressions != null)
				{
					groupingExprCount += groupExpressions.Count;
				}
				context.AddGroupingExprCountForGroup(member.Grouping.Name, groupingExprCount);
			}
			member.DetermineGroupingExprValueCount(context, groupingExprCount);
			if (member.InnerHierarchy != null)
			{
				HierarchyNodeList innerHierarchy = member.InnerHierarchy;
				for (int i = 0; i < innerHierarchy.Count; i++)
				{
					this.DetermineGroupingExprValueCount(ref outerIndex, ref innerIndex, innerHierarchy[i], context, traverseInner, groupingExprCount);
				}
				return;
			}
			if (traverseInner)
			{
				this.DetermineGroupingExprValueCount(outerIndex, innerIndex, context, groupingExprCount);
				innerIndex++;
				return;
			}
			innerIndex = 0;
			this.DetermineGroupingExprValueCount(ref outerIndex, ref innerIndex, context, true, groupingExprCount);
			outerIndex++;
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x00100AFE File Offset: 0x000FECFE
		protected virtual void DetermineGroupingExprValueCountInCorner(InitializationContext context, int groupingExprCount)
		{
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x00100B00 File Offset: 0x000FED00
		protected virtual void DetermineGroupingExprValueCount(int outerIndex, int innerIndex, InitializationContext context, int groupingExprCount)
		{
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x00100B02 File Offset: 0x000FED02
		protected void CopyCellAggregates(Cell cell)
		{
			this.CopyCellAggregates<DataAggregateInfo>(cell.Aggregates, ref this.m_cellAggregates);
			this.CopyCellAggregates<DataAggregateInfo>(cell.PostSortAggregates, ref this.m_cellPostSortAggregates);
			this.CopyCellAggregates<RunningValueInfo>(cell.RunningValues, ref this.m_cellRunningValues);
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x00100B3A File Offset: 0x000FED3A
		private void CopyCellAggregates<AggregateType>(List<AggregateType> aggregates, ref List<AggregateType> dataRegionCellAggregates) where AggregateType : DataAggregateInfo, new()
		{
			if (aggregates != null && aggregates.Count != 0)
			{
				if (dataRegionCellAggregates == null)
				{
					dataRegionCellAggregates = new List<AggregateType>();
				}
				dataRegionCellAggregates.AddRange(aggregates);
			}
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x00100B5C File Offset: 0x000FED5C
		internal Microsoft.ReportingServices.ReportIntermediateFormat.DataSet GetDataSet(Microsoft.ReportingServices.ReportIntermediateFormat.Report reportDefinition)
		{
			if (this.m_cachedDataSet == null)
			{
				Global.Tracer.Assert(reportDefinition != null, "(null != reportDefinition)");
				if (this.m_dataScopeInfo != null && this.m_dataScopeInfo.DataSet != null)
				{
					this.m_cachedDataSet = this.m_dataScopeInfo.DataSet;
				}
				else if (this.m_dataSetName == null)
				{
					this.m_dataSetName = reportDefinition.FirstDataSet.Name;
					this.m_cachedDataSet = reportDefinition.FirstDataSet;
				}
				else
				{
					this.m_cachedDataSet = reportDefinition.MappingNameToDataSet[this.m_dataSetName];
				}
			}
			return this.m_cachedDataSet;
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x00100BF0 File Offset: 0x000FEDF0
		List<DataAggregateInfo> IAggregateHolder.GetAggregateList()
		{
			return this.m_aggregates;
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x00100BF8 File Offset: 0x000FEDF8
		List<DataAggregateInfo> IAggregateHolder.GetPostSortAggregateList()
		{
			return this.m_postSortAggregates;
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x00100C00 File Offset: 0x000FEE00
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

		// Token: 0x06003B96 RID: 15254 RVA: 0x00100C65 File Offset: 0x000FEE65
		List<RunningValueInfo> IRunningValueHolder.GetRunningValueList()
		{
			return this.m_runningValues;
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x00100C6D File Offset: 0x000FEE6D
		void IRunningValueHolder.ClearIfEmpty()
		{
			if (this.m_runningValues != null && this.m_runningValues.Count == 0)
			{
				this.m_runningValues = null;
			}
			if (this.m_cellRunningValues != null && this.m_cellRunningValues.Count == 0)
			{
				this.m_cellRunningValues = null;
			}
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x00100CA8 File Offset: 0x000FEEA8
		internal void ConvertCellAggregatesToIndexes()
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
			Dictionary<string, int> dictionary3 = new Dictionary<string, int>();
			if (this.m_cellAggregates != null)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.GenerateAggregateIndexMapping<DataAggregateInfo>(this.m_cellAggregates, dictionary);
			}
			if (this.m_cellPostSortAggregates != null)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.GenerateAggregateIndexMapping<DataAggregateInfo>(this.m_cellPostSortAggregates, dictionary2);
			}
			if (this.m_cellRunningValues != null)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.GenerateAggregateIndexMapping<RunningValueInfo>(this.m_cellRunningValues, dictionary3);
			}
			int num = 0;
			while (num < this.m_rowCount && this.Rows.Count > num)
			{
				Row row = this.Rows[num];
				int num2 = 0;
				while (num2 < this.m_columnCount && row.Cells != null && row.Cells.Count > num2)
				{
					Cell cell = row.Cells[num2];
					if (cell != null)
					{
						cell.GenerateAggregateIndexes(dictionary, dictionary2, dictionary3);
					}
					num2++;
				}
				num++;
			}
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x00100D7C File Offset: 0x000FEF7C
		private static void GenerateAggregateIndexMapping<AggregateType>(List<AggregateType> cellAggregates, Dictionary<string, int> aggregateIndexes) where AggregateType : DataAggregateInfo
		{
			int count = cellAggregates.Count;
			for (int i = 0; i < count; i++)
			{
				AggregateType aggregateType = cellAggregates[i];
				aggregateIndexes.Add(aggregateType.Name, i);
				int num = ((aggregateType.DuplicateNames == null) ? 0 : aggregateType.DuplicateNames.Count);
				for (int j = 0; j < num; j++)
				{
					string text = aggregateType.DuplicateNames[j];
					if (!aggregateIndexes.ContainsKey(text))
					{
						aggregateIndexes.Add(text, i);
					}
				}
			}
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x00100E0E File Offset: 0x000FF00E
		protected override InstancePathItem CreateInstancePathItem()
		{
			if (this.IsDataRegion)
			{
				return new InstancePathItem(InstancePathItemType.DataRegion, this.IndexInCollection);
			}
			return new InstancePathItem();
		}

		// Token: 0x170019A7 RID: 6567
		// (get) Token: 0x06003B9B RID: 15259 RVA: 0x00100E2A File Offset: 0x000FF02A
		// (set) Token: 0x06003B9C RID: 15260 RVA: 0x00100E32 File Offset: 0x000FF032
		internal ExpressionInfo PageName
		{
			get
			{
				return this.m_pageName;
			}
			set
			{
				this.m_pageName = value;
			}
		}

		// Token: 0x170019A8 RID: 6568
		// (get) Token: 0x06003B9D RID: 15261 RVA: 0x00100E3B File Offset: 0x000FF03B
		// (set) Token: 0x06003B9E RID: 15262 RVA: 0x00100E43 File Offset: 0x000FF043
		internal PageBreak PageBreak
		{
			get
			{
				return this.m_pageBreak;
			}
			set
			{
				this.m_pageBreak = value;
			}
		}

		// Token: 0x170019A9 RID: 6569
		// (get) Token: 0x06003B9F RID: 15263 RVA: 0x00100E4C File Offset: 0x000FF04C
		// (set) Token: 0x06003BA0 RID: 15264 RVA: 0x00100E54 File Offset: 0x000FF054
		PageBreak IPageBreakOwner.PageBreak
		{
			get
			{
				return this.m_pageBreak;
			}
			set
			{
				this.m_pageBreak = value;
			}
		}

		// Token: 0x170019AA RID: 6570
		// (get) Token: 0x06003BA1 RID: 15265 RVA: 0x00100E5D File Offset: 0x000FF05D
		Microsoft.ReportingServices.ReportProcessing.ObjectType IPageBreakOwner.ObjectType
		{
			get
			{
				return this.ObjectType;
			}
		}

		// Token: 0x170019AB RID: 6571
		// (get) Token: 0x06003BA2 RID: 15266 RVA: 0x00100E65 File Offset: 0x000FF065
		string IPageBreakOwner.ObjectName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x170019AC RID: 6572
		// (get) Token: 0x06003BA3 RID: 15267 RVA: 0x00100E6D File Offset: 0x000FF06D
		IInstancePath IPageBreakOwner.InstancePath
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x00100E70 File Offset: 0x000FF070
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = (Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)base.PublishClone(context);
			dataRegion.m_dataScopeInfo = this.m_dataScopeInfo.PublishClone(context, dataRegion.ID);
			context.CurrentDataRegionClone = dataRegion;
			context.AddAggregateHolder(dataRegion);
			context.AddRunningValueHolder(dataRegion);
			if (this.m_dataSetName != null)
			{
				dataRegion.m_dataSetName = (string)this.m_dataSetName.Clone();
			}
			context.RegisterClonedScopeName(this.m_name, dataRegion.m_name);
			context.AddSortTarget(dataRegion.m_name, dataRegion);
			if (this.m_noRowsMessage != null)
			{
				dataRegion.m_noRowsMessage = (ExpressionInfo)this.m_noRowsMessage.PublishClone(context);
			}
			if (this.m_repeatSiblings != null)
			{
				dataRegion.m_repeatSiblings = new List<int>(this.m_repeatSiblings.Count);
				foreach (int num in this.m_repeatSiblings)
				{
					dataRegion.m_repeatSiblings.Add(num);
				}
			}
			if (this.m_sorting != null)
			{
				dataRegion.m_sorting = (Sorting)this.m_sorting.PublishClone(context);
			}
			if (this.m_filters != null)
			{
				dataRegion.m_filters = new List<Filter>(this.m_filters.Count);
				foreach (Filter filter in this.m_filters)
				{
					dataRegion.m_filters.Add((Filter)filter.PublishClone(context));
				}
			}
			if (this.m_pageBreak != null)
			{
				dataRegion.m_pageBreak = (PageBreak)this.m_pageBreak.PublishClone(context);
			}
			if (this.m_pageName != null)
			{
				dataRegion.m_pageName = (ExpressionInfo)this.m_pageName.PublishClone(context);
			}
			if (this.m_detailSortFiltersInScope != null)
			{
				dataRegion.m_detailSortFiltersInScope = new InScopeSortFilterHashtable(this.m_detailSortFiltersInScope.Count);
				foreach (object obj in this.m_detailSortFiltersInScope)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					List<int> list = (List<int>)dictionaryEntry.Value;
					List<int> list2 = new List<int>(list.Count);
					foreach (int num2 in list)
					{
						list2.Add(num2);
					}
					dataRegion.m_detailSortFiltersInScope.Add(dictionaryEntry.Key, list2);
				}
			}
			return dataRegion;
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x00101128 File Offset: 0x000FF328
		internal override void TraverseScopes(IRIFScopeVisitor visitor)
		{
			if (this.IsDataRegion)
			{
				visitor.PreVisit(this);
			}
			this.TraverseDataRegionLevelScopes(visitor);
			this.TraverseMembers(visitor, this.RowMembers);
			this.TraverseMembers(visitor, this.ColumnMembers);
			int num = 0;
			int num2 = 0;
			this.TraverseScopes(visitor, this.RowMembers, ref num, ref num2);
			if (this.IsDataRegion)
			{
				visitor.PostVisit(this);
			}
		}

		// Token: 0x06003BA6 RID: 15270 RVA: 0x0010118C File Offset: 0x000FF38C
		private void TraverseMembers(IRIFScopeVisitor visitor, HierarchyNodeList members)
		{
			if (members == null)
			{
				return;
			}
			foreach (object obj in members)
			{
				ReportHierarchyNode reportHierarchyNode = (ReportHierarchyNode)obj;
				this.TraversMembers(visitor, reportHierarchyNode);
			}
		}

		// Token: 0x06003BA7 RID: 15271 RVA: 0x001011E8 File Offset: 0x000FF3E8
		private void TraversMembers(IRIFScopeVisitor visitor, ReportHierarchyNode member)
		{
			if (!member.IsStatic)
			{
				visitor.PreVisit(member);
			}
			member.TraverseMemberScopes(visitor);
			this.TraverseMembers(visitor, member.InnerHierarchy);
			if (!member.IsStatic)
			{
				visitor.PostVisit(member);
			}
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x0010121C File Offset: 0x000FF41C
		protected virtual void TraverseDataRegionLevelScopes(IRIFScopeVisitor visitor)
		{
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x00101220 File Offset: 0x000FF420
		private void TraverseScopes(IRIFScopeVisitor visitor, HierarchyNodeList members, ref int rowCellIndex, ref int colCellIndex)
		{
			if (members == null)
			{
				return;
			}
			foreach (object obj in members)
			{
				ReportHierarchyNode reportHierarchyNode = (ReportHierarchyNode)obj;
				this.TraverseScopes(visitor, reportHierarchyNode, ref rowCellIndex, ref colCellIndex);
			}
		}

		// Token: 0x06003BAA RID: 15274 RVA: 0x0010127C File Offset: 0x000FF47C
		private void TraverseScopes(IRIFScopeVisitor visitor, ReportHierarchyNode member, ref int rowCellIndex, ref int colCellIndex)
		{
			if (member == null)
			{
				return;
			}
			if (!member.IsStatic)
			{
				visitor.PreVisit(member);
			}
			if (member.InnerHierarchy == null || member.InnerHierarchy.Count == 0)
			{
				if (member.IsColumn)
				{
					RowList rows = this.Rows;
					if (rows != null && rows.Count > rowCellIndex)
					{
						Row row = rows[rowCellIndex];
						if (row != null && row.Cells != null && row.Cells.Count > colCellIndex)
						{
							Cell cell = row.Cells[colCellIndex];
							this.TraverseScopes(visitor, cell, rowCellIndex, colCellIndex);
						}
					}
					colCellIndex++;
				}
				else
				{
					colCellIndex = 0;
					this.TraverseScopes(visitor, this.ColumnMembers, ref rowCellIndex, ref colCellIndex);
					rowCellIndex++;
				}
			}
			else
			{
				this.TraverseScopes(visitor, member.InnerHierarchy, ref rowCellIndex, ref colCellIndex);
			}
			if (!member.IsStatic)
			{
				visitor.PostVisit(member);
			}
		}

		// Token: 0x06003BAB RID: 15275 RVA: 0x00101355 File Offset: 0x000FF555
		protected void TraverseScopes(IRIFScopeVisitor visitor, Cell cell, int rowIndex, int colIndex)
		{
			if (cell == null)
			{
				return;
			}
			cell.TraverseScopes(visitor, rowIndex, colIndex);
		}

		// Token: 0x06003BAC RID: 15276 RVA: 0x00101368 File Offset: 0x000FF568
		protected void BuildAndSetupAxisScopeTreeForAutoSubtotals(ref AutomaticSubtotalContext context, ReportHierarchyNode member)
		{
			int startIndex = context.StartIndex;
			this.FindClonedScopesForAutoSubtotals(false, member, context.ScopeNamesToClone, ref startIndex);
		}

		// Token: 0x06003BAD RID: 15277 RVA: 0x0010138C File Offset: 0x000FF58C
		private void FindClonedScopesForAutoSubtotals(bool register, ReportHierarchyNode member, Dictionary<string, IRIFDataScope> scopesToClone, ref int memberCellIndex)
		{
			if (member == null)
			{
				return;
			}
			if (!member.IsStatic && register)
			{
				scopesToClone.Add(member.Grouping.Name, member);
			}
			TablixMember tablixMember = member as TablixMember;
			if (tablixMember != null && tablixMember.TablixHeader != null)
			{
				TablixHeader tablixHeader = tablixMember.TablixHeader;
				this.FindClonedScopesForAutoSubtotals(tablixHeader.CellContents, scopesToClone);
				this.FindClonedScopesForAutoSubtotals(tablixHeader.AltCellContents, scopesToClone);
			}
			if (member.InnerHierarchy == null || member.InnerHierarchy.Count == 0)
			{
				RowList rows = this.Rows;
				if (rows != null && this.ObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Tablix)
				{
					if (member.IsColumn)
					{
						using (IEnumerator enumerator = rows.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								Row row = (Row)obj;
								if (row != null && row.Cells != null && row.Cells.Count > memberCellIndex)
								{
									this.FindClonedScopesForAutoSubtotals((TablixCellBase)row.Cells[memberCellIndex], scopesToClone);
								}
							}
							goto IL_0161;
						}
					}
					if (rows.Count > memberCellIndex)
					{
						Row row2 = rows[memberCellIndex];
						if (row2 != null && row2.Cells != null)
						{
							foreach (object obj2 in row2.Cells)
							{
								TablixCellBase tablixCellBase = (TablixCellBase)obj2;
								this.FindClonedScopesForAutoSubtotals(tablixCellBase, scopesToClone);
							}
						}
					}
				}
				IL_0161:
				memberCellIndex++;
				return;
			}
			this.FindClonedScopesForAutoSubtotals(register, member.InnerHierarchy, scopesToClone, ref memberCellIndex);
		}

		// Token: 0x06003BAE RID: 15278 RVA: 0x00101530 File Offset: 0x000FF730
		private void FindClonedScopesForAutoSubtotals(TablixCellBase cell, Dictionary<string, IRIFDataScope> scopesToClone)
		{
			if (cell == null)
			{
				return;
			}
			this.FindClonedScopesForAutoSubtotals(cell.CellContents, scopesToClone);
			this.FindClonedScopesForAutoSubtotals(cell.AltCellContents, scopesToClone);
		}

		// Token: 0x06003BAF RID: 15279 RVA: 0x00101550 File Offset: 0x000FF750
		private void FindClonedScopesForAutoSubtotals(Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem item, Dictionary<string, IRIFDataScope> scopesToClone)
		{
			if (item == null)
			{
				return;
			}
			Microsoft.ReportingServices.ReportProcessing.ObjectType objectType = item.ObjectType;
			if (objectType <= Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel)
			{
				if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Rectangle)
				{
					goto IL_0143;
				}
				if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.GaugePanel)
				{
					return;
				}
			}
			else if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart && objectType - Microsoft.ReportingServices.ReportProcessing.ObjectType.CustomReportItem > 1)
			{
				if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Map)
				{
					return;
				}
				goto IL_00F4;
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion = (Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)item;
			scopesToClone.Add(dataRegion.Name, dataRegion);
			int num = 0;
			dataRegion.FindClonedScopesForAutoSubtotals(true, dataRegion.OuterMembers, scopesToClone, ref num);
			int num2 = 0;
			dataRegion.FindClonedScopesForAutoSubtotals(true, dataRegion.InnerMembers, scopesToClone, ref num2);
			Microsoft.ReportingServices.ReportIntermediateFormat.Tablix tablix = dataRegion as Microsoft.ReportingServices.ReportIntermediateFormat.Tablix;
			if (tablix == null || tablix.Corner == null)
			{
				return;
			}
			using (List<List<TablixCornerCell>>.Enumerator enumerator = tablix.Corner.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					List<TablixCornerCell> list = enumerator.Current;
					if (list != null)
					{
						foreach (TablixCornerCell tablixCornerCell in list)
						{
							dataRegion.FindClonedScopesForAutoSubtotals(tablixCornerCell, scopesToClone);
						}
					}
				}
				return;
			}
			IL_00F4:
			Map map = (Map)item;
			if (map.MapDataRegions == null)
			{
				return;
			}
			using (List<MapDataRegion>.Enumerator enumerator3 = map.MapDataRegions.GetEnumerator())
			{
				while (enumerator3.MoveNext())
				{
					MapDataRegion mapDataRegion = enumerator3.Current;
					this.FindClonedScopesForAutoSubtotals(mapDataRegion, scopesToClone);
				}
				return;
			}
			IL_0143:
			Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle rectangle = (Microsoft.ReportingServices.ReportIntermediateFormat.Rectangle)item;
			if (rectangle.ReportItems != null)
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem in rectangle.ReportItems)
				{
					this.FindClonedScopesForAutoSubtotals(reportItem, scopesToClone);
				}
			}
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x00101720 File Offset: 0x000FF920
		private void FindClonedScopesForAutoSubtotals(bool register, HierarchyNodeList members, Dictionary<string, IRIFDataScope> scopesToClone, ref int memberCellIndex)
		{
			if (members == null)
			{
				return;
			}
			foreach (object obj in members)
			{
				ReportHierarchyNode reportHierarchyNode = (ReportHierarchyNode)obj;
				this.FindClonedScopesForAutoSubtotals(register, reportHierarchyNode, scopesToClone, ref memberCellIndex);
			}
		}

		// Token: 0x06003BB1 RID: 15281 RVA: 0x0010177C File Offset: 0x000FF97C
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegion, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportItem, new List<MemberInfo>
			{
				new MemberInfo(MemberName.DataSetName, Token.String),
				new MemberInfo(MemberName.NoRowsMessage, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ColumnCount, Token.Int32),
				new MemberInfo(MemberName.RowCount, Token.Int32),
				new MemberInfo(MemberName.ProcessingInnerGrouping, Token.Enum),
				new MemberInfo(MemberName.RepeatSiblings, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int32),
				new MemberInfo(MemberName.Sorting, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Sorting),
				new MemberInfo(MemberName.Filters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Filter),
				new MemberInfo(MemberName.Aggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfo),
				new MemberInfo(MemberName.PostSortAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfo),
				new MemberInfo(MemberName.RunningValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RunningValueInfo),
				new MemberInfo(MemberName.CellAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfo),
				new MemberInfo(MemberName.CellPostSortAggregates, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataAggregateInfo),
				new MemberInfo(MemberName.CellRunningValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RunningValueInfo),
				new MemberInfo(MemberName.UserSortExpressions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DetailSortFiltersInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Int32PrimitiveListHashtable),
				new ReadOnlyMemberInfo(MemberName.PageBreakLocation, Token.Enum),
				new MemberInfo(MemberName.IndexInCollection, Token.Int32),
				new MemberInfo(MemberName.NeedToCacheDataRows, Token.Boolean),
				new MemberInfo(MemberName.InScopeEventSources, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IInScopeEventSource),
				new MemberInfo(MemberName.OuterGroupingMaximumDynamicLevel, Token.Int32),
				new MemberInfo(MemberName.OuterGroupingDynamicMemberCount, Token.Int32),
				new MemberInfo(MemberName.OuterGroupingDynamicPathCount, Token.Int32),
				new MemberInfo(MemberName.InnerGroupingMaximumDynamicLevel, Token.Int32),
				new MemberInfo(MemberName.InnerGroupingDynamicMemberCount, Token.Int32),
				new MemberInfo(MemberName.InnerGroupingDynamicPathCount, Token.Int32),
				new MemberInfo(MemberName.TextboxesInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Byte),
				new MemberInfo(MemberName.VariablesInScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveTypedArray, Token.Byte),
				new MemberInfo(MemberName.PageBreak, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PageBreak),
				new MemberInfo(MemberName.PageName, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DataScopeInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataScopeInfo),
				new MemberInfo(MemberName.RowDomainScopeCount, Token.Int32),
				new MemberInfo(MemberName.ColumnDomainScopeCount, Token.Int32),
				new MemberInfo(MemberName.IsMatrixIDC, Token.Boolean)
			});
		}

		// Token: 0x06003BB2 RID: 15282 RVA: 0x00101A64 File Offset: 0x000FFC64
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.IndexInCollection)
				{
					if (memberName <= MemberName.RunningValues)
					{
						if (memberName <= MemberName.Sorting)
						{
							switch (memberName)
							{
							case MemberName.CellAggregates:
								writer.Write<DataAggregateInfo>(this.m_cellAggregates);
								continue;
							case MemberName.CellPostSortAggregates:
								writer.Write<DataAggregateInfo>(this.m_cellPostSortAggregates);
								continue;
							case MemberName.CellRunningValues:
								writer.Write<RunningValueInfo>(this.m_cellRunningValues);
								continue;
							default:
								switch (memberName)
								{
								case MemberName.DataSetName:
									writer.Write(this.m_dataSetName);
									continue;
								case MemberName.RepeatSiblings:
									writer.WriteListOfPrimitives<int>(this.m_repeatSiblings);
									continue;
								case MemberName.Sorting:
									writer.Write(this.m_sorting);
									continue;
								}
								break;
							}
						}
						else
						{
							switch (memberName)
							{
							case MemberName.Aggregates:
								writer.Write<DataAggregateInfo>(this.m_aggregates);
								continue;
							case MemberName.GroupAndSort:
							case MemberName.SortExpressions:
								break;
							case MemberName.ColumnCount:
								writer.Write(this.m_columnCount);
								continue;
							case MemberName.RowCount:
								writer.Write(this.m_rowCount);
								continue;
							default:
								if (memberName == MemberName.ProcessingInnerGrouping)
								{
									writer.WriteEnum((int)this.m_processingInnerGrouping);
									continue;
								}
								if (memberName == MemberName.RunningValues)
								{
									writer.Write<RunningValueInfo>(this.m_runningValues);
									continue;
								}
								break;
							}
						}
					}
					else if (memberName <= MemberName.UserSortExpressions)
					{
						if (memberName == MemberName.Filters)
						{
							writer.Write<Filter>(this.m_filters);
							continue;
						}
						if (memberName == MemberName.PostSortAggregates)
						{
							writer.Write<DataAggregateInfo>(this.m_postSortAggregates);
							continue;
						}
						if (memberName == MemberName.UserSortExpressions)
						{
							writer.Write<ExpressionInfo>(this.m_userSortExpressions);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.DetailSortFiltersInScope)
						{
							writer.WriteInt32PrimitiveListHashtable<int>(this.m_detailSortFiltersInScope);
							continue;
						}
						if (memberName == MemberName.NoRowsMessage)
						{
							writer.Write(this.m_noRowsMessage);
							continue;
						}
						if (memberName == MemberName.IndexInCollection)
						{
							writer.Write(this.m_indexInCollection);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.TextboxesInScope)
				{
					if (memberName <= MemberName.OuterGroupingDynamicPathCount)
					{
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
						switch (memberName)
						{
						case MemberName.OuterGroupingMaximumDynamicLevel:
							writer.Write(this.m_outerGroupingMaximumDynamicLevel);
							continue;
						case MemberName.OuterGroupingDynamicMemberCount:
							writer.Write(this.m_outerGroupingDynamicMemberCount);
							continue;
						case MemberName.OuterGroupingDynamicPathCount:
							writer.Write(this.m_outerGroupingDynamicPathCount);
							continue;
						}
					}
					else
					{
						switch (memberName)
						{
						case MemberName.InnerGroupingMaximumDynamicLevel:
							writer.Write(this.m_innerGroupingMaximumDynamicLevel);
							continue;
						case MemberName.InnerGroupingDynamicMemberCount:
							writer.Write(this.m_innerGroupingDynamicMemberCount);
							continue;
						case MemberName.InnerGroupingDynamicPathCount:
							writer.Write(this.m_innerGroupingDynamicPathCount);
							continue;
						default:
							if (memberName == MemberName.VariablesInScope)
							{
								writer.Write(this.m_variablesInScope);
								continue;
							}
							if (memberName == MemberName.TextboxesInScope)
							{
								writer.Write(this.m_textboxesInScope);
								continue;
							}
							break;
						}
					}
				}
				else if (memberName <= MemberName.DataScopeInfo)
				{
					if (memberName == MemberName.PageBreak)
					{
						writer.Write(this.m_pageBreak);
						continue;
					}
					if (memberName == MemberName.PageName)
					{
						writer.Write(this.m_pageName);
						continue;
					}
					if (memberName == MemberName.DataScopeInfo)
					{
						writer.Write(this.m_dataScopeInfo);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.RowDomainScopeCount)
					{
						writer.Write(this.RowDomainScopeCount);
						continue;
					}
					if (memberName == MemberName.ColumnDomainScopeCount)
					{
						writer.Write(this.ColumnDomainScopeCount);
						continue;
					}
					if (memberName == MemberName.IsMatrixIDC)
					{
						writer.Write(this.m_isMatrixIDC);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003BB3 RID: 15283 RVA: 0x00101E98 File Offset: 0x00100098
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.IndexInCollection)
				{
					if (memberName <= MemberName.Filters)
					{
						if (memberName <= MemberName.RowCount)
						{
							switch (memberName)
							{
							case MemberName.CellAggregates:
								this.m_cellAggregates = reader.ReadGenericListOfRIFObjects<DataAggregateInfo>();
								continue;
							case MemberName.CellPostSortAggregates:
								this.m_cellPostSortAggregates = reader.ReadGenericListOfRIFObjects<DataAggregateInfo>();
								continue;
							case MemberName.CellRunningValues:
								this.m_cellRunningValues = reader.ReadGenericListOfRIFObjects<RunningValueInfo>();
								continue;
							default:
								switch (memberName)
								{
								case MemberName.DataSetName:
									this.m_dataSetName = reader.ReadString();
									continue;
								case MemberName.KeepTogether:
								case MemberName.Grouping:
									break;
								case MemberName.RepeatSiblings:
									this.m_repeatSiblings = reader.ReadListOfPrimitives<int>();
									continue;
								case MemberName.Sorting:
									this.m_sorting = (Sorting)reader.ReadRIFObject();
									continue;
								default:
									switch (memberName)
									{
									case MemberName.Aggregates:
										this.m_aggregates = reader.ReadGenericListOfRIFObjects<DataAggregateInfo>();
										continue;
									case MemberName.ColumnCount:
										this.m_columnCount = reader.ReadInt32();
										continue;
									case MemberName.RowCount:
										this.m_rowCount = reader.ReadInt32();
										continue;
									}
									break;
								}
								break;
							}
						}
						else
						{
							if (memberName == MemberName.ProcessingInnerGrouping)
							{
								this.m_processingInnerGrouping = (Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.ProcessingInnerGroupings)reader.ReadEnum();
								continue;
							}
							if (memberName == MemberName.RunningValues)
							{
								this.m_runningValues = reader.ReadGenericListOfRIFObjects<RunningValueInfo>();
								continue;
							}
							if (memberName == MemberName.Filters)
							{
								this.m_filters = reader.ReadGenericListOfRIFObjects<Filter>();
								continue;
							}
						}
					}
					else if (memberName <= MemberName.DetailSortFiltersInScope)
					{
						if (memberName == MemberName.PostSortAggregates)
						{
							this.m_postSortAggregates = reader.ReadGenericListOfRIFObjects<DataAggregateInfo>();
							continue;
						}
						if (memberName == MemberName.UserSortExpressions)
						{
							this.m_userSortExpressions = reader.ReadGenericListOfRIFObjects<ExpressionInfo>();
							continue;
						}
						if (memberName == MemberName.DetailSortFiltersInScope)
						{
							this.m_detailSortFiltersInScope = reader.ReadInt32PrimitiveListHashtable<InScopeSortFilterHashtable, int>();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.PageBreakLocation)
						{
							this.m_pageBreak = new PageBreak();
							this.m_pageBreak.BreakLocation = (PageBreakLocation)reader.ReadEnum();
							continue;
						}
						if (memberName == MemberName.NoRowsMessage)
						{
							this.m_noRowsMessage = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.IndexInCollection)
						{
							this.m_indexInCollection = reader.ReadInt32();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.TextboxesInScope)
				{
					if (memberName <= MemberName.OuterGroupingDynamicPathCount)
					{
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
						switch (memberName)
						{
						case MemberName.OuterGroupingMaximumDynamicLevel:
							this.m_outerGroupingMaximumDynamicLevel = reader.ReadInt32();
							continue;
						case MemberName.OuterGroupingDynamicMemberCount:
							this.m_outerGroupingDynamicMemberCount = reader.ReadInt32();
							continue;
						case MemberName.OuterGroupingDynamicPathCount:
							this.m_outerGroupingDynamicPathCount = reader.ReadInt32();
							continue;
						}
					}
					else
					{
						switch (memberName)
						{
						case MemberName.InnerGroupingMaximumDynamicLevel:
							this.m_innerGroupingMaximumDynamicLevel = reader.ReadInt32();
							continue;
						case MemberName.InnerGroupingDynamicMemberCount:
							this.m_innerGroupingDynamicMemberCount = reader.ReadInt32();
							continue;
						case MemberName.InnerGroupingDynamicPathCount:
							this.m_innerGroupingDynamicPathCount = reader.ReadInt32();
							continue;
						default:
							if (memberName == MemberName.VariablesInScope)
							{
								this.m_variablesInScope = reader.ReadByteArray();
								continue;
							}
							if (memberName == MemberName.TextboxesInScope)
							{
								this.m_textboxesInScope = reader.ReadByteArray();
								continue;
							}
							break;
						}
					}
				}
				else if (memberName <= MemberName.DataScopeInfo)
				{
					if (memberName == MemberName.PageBreak)
					{
						this.m_pageBreak = (PageBreak)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.PageName)
					{
						this.m_pageName = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.DataScopeInfo)
					{
						this.m_dataScopeInfo = (DataScopeInfo)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.RowDomainScopeCount)
					{
						this.m_rowDomainScopeCount = new int?(reader.ReadInt32());
						continue;
					}
					if (memberName == MemberName.ColumnDomainScopeCount)
					{
						this.m_colDomainScopeCount = new int?(reader.ReadInt32());
						continue;
					}
					if (memberName == MemberName.IsMatrixIDC)
					{
						this.m_isMatrixIDC = reader.ReadBoolean();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003BB4 RID: 15284 RVA: 0x00102320 File Offset: 0x00100520
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.InScopeEventSources)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable;
						referenceableItems.TryGetValue(memberReference.RefID, out referenceable);
						IInScopeEventSource inScopeEventSource = (IInScopeEventSource)referenceable;
						if (this.m_inScopeEventSources == null)
						{
							this.m_inScopeEventSources = new List<IInScopeEventSource>();
						}
						this.m_inScopeEventSources.Add(inScopeEventSource);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06003BB5 RID: 15285 RVA: 0x001023D4 File Offset: 0x001005D4
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataRegion;
		}

		// Token: 0x06003BB6 RID: 15286
		internal abstract object EvaluateNoRowsMessageExpression();

		// Token: 0x06003BB7 RID: 15287 RVA: 0x001023DB File Offset: 0x001005DB
		internal string EvaluateNoRowsMessage(DataRegionInstance romInstance, OnDemandProcessingContext odpContext)
		{
			odpContext.SetupContext(this, romInstance);
			return odpContext.ReportRuntime.EvaluateDataRegionNoRowsExpression(this, this.ObjectType, this.m_name, "NoRowsMessage");
		}

		// Token: 0x06003BB8 RID: 15288 RVA: 0x00102402 File Offset: 0x00100602
		internal string EvaluatePageName(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			context.SetupContext(this, romInstance);
			return context.ReportRuntime.EvaluateDataRegionPageNameExpression(this, this.m_pageName, this.ObjectType, base.Name);
		}

		// Token: 0x06003BB9 RID: 15289 RVA: 0x0010242C File Offset: 0x0010062C
		protected void DataRegionSetExprHost(ReportItemExprHost exprHost, SortExprHost sortExprHost, IList<FilterExprHost> FilterHostsRemotable, IndexedExprHost UserSortExpressionsHost, PageBreakExprHost pageBreakExprHost, IList<JoinConditionExprHost> joinConditionExprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null, "(exprHost != null)");
			base.ReportItemSetExprHost(exprHost, reportObjectModel);
			if (sortExprHost != null)
			{
				Global.Tracer.Assert(this.m_sorting != null, "(null != m_sorting)");
				this.m_sorting.SetExprHost(sortExprHost, reportObjectModel);
			}
			if (FilterHostsRemotable != null)
			{
				Global.Tracer.Assert(this.m_filters != null, "(m_filters != null)");
				int count = this.m_filters.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_filters[i].SetExprHost(FilterHostsRemotable, reportObjectModel);
				}
			}
			if (UserSortExpressionsHost != null)
			{
				UserSortExpressionsHost.SetReportObjectModel(reportObjectModel);
			}
			if (this.m_pageBreak != null && pageBreakExprHost != null)
			{
				this.m_pageBreak.SetExprHost(pageBreakExprHost, reportObjectModel);
			}
			if (this.m_dataScopeInfo != null && this.m_dataScopeInfo.JoinInfo != null && joinConditionExprHost != null)
			{
				this.m_dataScopeInfo.JoinInfo.SetJoinConditionExprHost(joinConditionExprHost, reportObjectModel);
			}
		}

		// Token: 0x06003BBA RID: 15290
		internal abstract void DataRegionContentsSetExprHost(ObjectModelImpl reportObjectModel, bool traverseDataRegions);

		// Token: 0x06003BBB RID: 15291 RVA: 0x0010251A File Offset: 0x0010071A
		internal void SaveOuterGroupingAggregateRowInfo(int dynamicIndex, OnDemandProcessingContext odpContext)
		{
			Global.Tracer.Assert(this.m_outerGroupingAggregateRowInfo != null, "(null != m_outerGroupingAggregateRowInfo)");
			if (this.m_outerGroupingAggregateRowInfo[dynamicIndex] == null)
			{
				this.m_outerGroupingAggregateRowInfo[dynamicIndex] = new AggregateRowInfo();
			}
			this.m_outerGroupingAggregateRowInfo[dynamicIndex].SaveAggregateInfo(odpContext);
		}

		// Token: 0x06003BBC RID: 15292 RVA: 0x00102559 File Offset: 0x00100759
		internal void SetDataTablixAggregateRowInfo(AggregateRowInfo aggregateRowInfo)
		{
			this.m_dataTablixAggregateRowInfo = aggregateRowInfo;
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x00102562 File Offset: 0x00100762
		internal void SetCellAggregateRowInfo(int dynamicIndex, OnDemandProcessingContext odpContext)
		{
			Global.Tracer.Assert(this.m_outerGroupingAggregateRowInfo != null && this.m_dataTablixAggregateRowInfo != null, "(null != m_outerGroupingAggregateRowInfo && null != m_dataTablixAggregateRowInfo)");
			this.m_dataTablixAggregateRowInfo.CombineAggregateInfo(odpContext, this.m_outerGroupingAggregateRowInfo[dynamicIndex]);
		}

		// Token: 0x06003BBE RID: 15294 RVA: 0x0010259C File Offset: 0x0010079C
		internal void ResetInstancePathCascade()
		{
			int num = ((this.RowMembers != null) ? this.RowMembers.Count : 0);
			for (int i = 0; i < num; i++)
			{
				this.RowMembers[i].ResetInstancePathCascade();
			}
			num = ((this.ColumnMembers != null) ? this.ColumnMembers.Count : 0);
			for (int j = 0; j < num; j++)
			{
				this.ColumnMembers[j].ResetInstancePathCascade();
			}
		}

		// Token: 0x06003BBF RID: 15295 RVA: 0x00102614 File Offset: 0x00100814
		internal void ResetInstanceIndexes()
		{
			this.m_currentCellInnerIndex = 0;
			this.m_sequentialColMemberInstanceIndex = 0;
			this.m_sequentialRowMemberInstanceIndex = 0;
			this.m_outerGroupingIndexes = new int[this.OuterGroupingDynamicMemberCount];
			this.m_currentOuterGroupRootObjs = new IReference<RuntimeDataTablixGroupRootObj>[this.OuterGroupingDynamicMemberCount];
			this.m_outerGroupingAggregateRowInfo = new AggregateRowInfo[this.OuterGroupingDynamicMemberCount];
			this.m_currentOuterGroupRoot = null;
		}

		// Token: 0x06003BC0 RID: 15296 RVA: 0x00102670 File Offset: 0x00100870
		internal void UpdateOuterGroupingIndexes(IReference<RuntimeDataTablixGroupRootObj> groupRoot, int groupLeafIndex)
		{
			int hierarchyDynamicIndex = groupRoot.Value().HierarchyDef.HierarchyDynamicIndex;
			this.m_currentOuterGroupRootObjs[hierarchyDynamicIndex] = groupRoot;
			this.m_outerGroupingIndexes[hierarchyDynamicIndex] = groupLeafIndex;
		}

		// Token: 0x06003BC1 RID: 15297 RVA: 0x001026A0 File Offset: 0x001008A0
		internal void ResetOuterGroupingIndexesForOuterPeerGroup(int index)
		{
			for (int i = index; i < this.OuterGroupingDynamicMemberCount; i++)
			{
				this.m_currentOuterGroupRootObjs[i] = null;
				this.m_outerGroupingIndexes[i] = 0;
			}
		}

		// Token: 0x06003BC2 RID: 15298 RVA: 0x001026D0 File Offset: 0x001008D0
		internal void ResetOuterGroupingAggregateRowInfo()
		{
			Global.Tracer.Assert(this.m_outerGroupingAggregateRowInfo != null, "(null != m_outerGroupingAggregateRowInfo)");
			for (int i = 0; i < this.m_outerGroupingAggregateRowInfo.Length; i++)
			{
				this.m_outerGroupingAggregateRowInfo[i] = null;
			}
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x00102714 File Offset: 0x00100914
		internal int AddMemberInstance(bool isColumn)
		{
			if (isColumn)
			{
				int num = this.m_sequentialColMemberInstanceIndex + 1;
				this.m_sequentialColMemberInstanceIndex = num;
				return num;
			}
			return this.m_sequentialRowMemberInstanceIndex;
		}

		// Token: 0x06003BC4 RID: 15300 RVA: 0x0010273C File Offset: 0x0010093C
		internal void AddCell()
		{
			this.m_currentCellInnerIndex++;
		}

		// Token: 0x06003BC5 RID: 15301 RVA: 0x0010274C File Offset: 0x0010094C
		internal void NewOuterCells()
		{
			if (0 < this.m_currentCellInnerIndex)
			{
				this.m_currentCellInnerIndex = 0;
			}
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x0010275E File Offset: 0x0010095E
		internal void ResetTopLevelDynamicMemberInstanceCount()
		{
			this.ResetTopLevelDynamicMemberInstanceCount(this.RowMembers);
			this.ResetTopLevelDynamicMemberInstanceCount(this.ColumnMembers);
		}

		// Token: 0x06003BC7 RID: 15303 RVA: 0x00102778 File Offset: 0x00100978
		private void ResetTopLevelDynamicMemberInstanceCount(HierarchyNodeList topLevelMembers)
		{
			if (topLevelMembers == null)
			{
				return;
			}
			int count = topLevelMembers.Count;
			for (int i = 0; i < count; i++)
			{
				if (!topLevelMembers[i].IsStatic)
				{
					topLevelMembers[i].InstanceCount = 0;
				}
			}
		}

		// Token: 0x04001BF9 RID: 7161
		protected string m_dataSetName;

		// Token: 0x04001BFA RID: 7162
		protected ExpressionInfo m_noRowsMessage;

		// Token: 0x04001BFB RID: 7163
		protected int m_columnCount;

		// Token: 0x04001BFC RID: 7164
		protected int m_rowCount;

		// Token: 0x04001BFD RID: 7165
		protected List<int> m_repeatSiblings;

		// Token: 0x04001BFE RID: 7166
		protected Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.ProcessingInnerGroupings m_processingInnerGrouping;

		// Token: 0x04001BFF RID: 7167
		protected Sorting m_sorting;

		// Token: 0x04001C00 RID: 7168
		protected List<Filter> m_filters;

		// Token: 0x04001C01 RID: 7169
		protected List<DataAggregateInfo> m_aggregates;

		// Token: 0x04001C02 RID: 7170
		protected List<DataAggregateInfo> m_postSortAggregates;

		// Token: 0x04001C03 RID: 7171
		protected List<RunningValueInfo> m_runningValues;

		// Token: 0x04001C04 RID: 7172
		protected List<DataAggregateInfo> m_cellAggregates;

		// Token: 0x04001C05 RID: 7173
		protected List<DataAggregateInfo> m_cellPostSortAggregates;

		// Token: 0x04001C06 RID: 7174
		protected List<RunningValueInfo> m_cellRunningValues;

		// Token: 0x04001C07 RID: 7175
		protected List<ExpressionInfo> m_userSortExpressions;

		// Token: 0x04001C08 RID: 7176
		private byte[] m_textboxesInScope;

		// Token: 0x04001C09 RID: 7177
		private byte[] m_variablesInScope;

		// Token: 0x04001C0A RID: 7178
		private bool m_needToCacheDataRows;

		// Token: 0x04001C0B RID: 7179
		private List<IInScopeEventSource> m_inScopeEventSources;

		// Token: 0x04001C0C RID: 7180
		protected InScopeSortFilterHashtable m_detailSortFiltersInScope;

		// Token: 0x04001C0D RID: 7181
		protected int m_indexInCollection = -1;

		// Token: 0x04001C0E RID: 7182
		protected int m_outerGroupingMaximumDynamicLevel;

		// Token: 0x04001C0F RID: 7183
		protected int m_outerGroupingDynamicMemberCount;

		// Token: 0x04001C10 RID: 7184
		protected int m_outerGroupingDynamicPathCount;

		// Token: 0x04001C11 RID: 7185
		protected int m_innerGroupingMaximumDynamicLevel;

		// Token: 0x04001C12 RID: 7186
		protected int m_innerGroupingDynamicMemberCount;

		// Token: 0x04001C13 RID: 7187
		protected int m_innerGroupingDynamicPathCount;

		// Token: 0x04001C14 RID: 7188
		protected PageBreak m_pageBreak;

		// Token: 0x04001C15 RID: 7189
		protected ExpressionInfo m_pageName;

		// Token: 0x04001C16 RID: 7190
		protected DataScopeInfo m_dataScopeInfo;

		// Token: 0x04001C17 RID: 7191
		private int? m_rowDomainScopeCount;

		// Token: 0x04001C18 RID: 7192
		private int? m_colDomainScopeCount;

		// Token: 0x04001C19 RID: 7193
		private bool m_isMatrixIDC;

		// Token: 0x04001C1A RID: 7194
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion.GetDeclaration();

		// Token: 0x04001C1B RID: 7195
		[NonSerialized]
		private bool m_rowScopeFound;

		// Token: 0x04001C1C RID: 7196
		[NonSerialized]
		private bool m_columnScopeFound;

		// Token: 0x04001C1D RID: 7197
		[NonSerialized]
		private bool m_hasDynamicColumnMember;

		// Token: 0x04001C1E RID: 7198
		[NonSerialized]
		private bool m_hasDynamicRowMember;

		// Token: 0x04001C1F RID: 7199
		[NonSerialized]
		private InitializationContext.ScopeChainInfo m_scopeChainInfo;

		// Token: 0x04001C20 RID: 7200
		[NonSerialized]
		protected Microsoft.ReportingServices.ReportIntermediateFormat.DataSet m_cachedDataSet;

		// Token: 0x04001C21 RID: 7201
		[NonSerialized]
		protected PageBreakStates m_pagebreakState;

		// Token: 0x04001C22 RID: 7202
		[NonSerialized]
		protected RuntimeDataRegionObjReference m_runtimeDataRegionObj;

		// Token: 0x04001C23 RID: 7203
		[NonSerialized]
		protected List<int> m_outermostStaticColumnIndexes;

		// Token: 0x04001C24 RID: 7204
		[NonSerialized]
		protected List<int> m_outermostStaticRowIndexes;

		// Token: 0x04001C25 RID: 7205
		[NonSerialized]
		protected int m_currentCellInnerIndex;

		// Token: 0x04001C26 RID: 7206
		[NonSerialized]
		protected int m_sequentialColMemberInstanceIndex;

		// Token: 0x04001C27 RID: 7207
		[NonSerialized]
		protected int m_sequentialRowMemberInstanceIndex;

		// Token: 0x04001C28 RID: 7208
		[NonSerialized]
		protected Hashtable m_scopeNames;

		// Token: 0x04001C29 RID: 7209
		[NonSerialized]
		protected bool m_inTablixCell;

		// Token: 0x04001C2A RID: 7210
		[NonSerialized]
		protected bool[] m_isSortFilterTarget;

		// Token: 0x04001C2B RID: 7211
		[NonSerialized]
		protected bool[] m_isSortFilterExpressionScope;

		// Token: 0x04001C2C RID: 7212
		[NonSerialized]
		protected int[] m_sortFilterSourceDetailScopeInfo;

		// Token: 0x04001C2D RID: 7213
		[NonSerialized]
		protected int m_currentColDetailIndex = -1;

		// Token: 0x04001C2E RID: 7214
		[NonSerialized]
		protected int m_currentRowDetailIndex = -1;

		// Token: 0x04001C2F RID: 7215
		[NonSerialized]
		protected bool m_noRows;

		// Token: 0x04001C30 RID: 7216
		[NonSerialized]
		protected bool m_processCellRunningValues;

		// Token: 0x04001C31 RID: 7217
		[NonSerialized]
		protected bool m_processOutermostStaticCellRunningValues;

		// Token: 0x04001C32 RID: 7218
		[NonSerialized]
		private bool m_inOutermostStaticCells;

		// Token: 0x04001C33 RID: 7219
		[NonSerialized]
		protected DataRegionInstance m_currentDataRegionInstance;

		// Token: 0x04001C34 RID: 7220
		[NonSerialized]
		protected AggregateRowInfo m_dataTablixAggregateRowInfo;

		// Token: 0x04001C35 RID: 7221
		[NonSerialized]
		protected AggregateRowInfo[] m_outerGroupingAggregateRowInfo;

		// Token: 0x04001C36 RID: 7222
		[NonSerialized]
		protected int[] m_outerGroupingIndexes;

		// Token: 0x04001C37 RID: 7223
		[NonSerialized]
		protected IReference<RuntimeDataTablixGroupRootObj>[] m_currentOuterGroupRootObjs;

		// Token: 0x04001C38 RID: 7224
		[NonSerialized]
		protected IReference<RuntimeDataTablixGroupRootObj> m_currentOuterGroupRoot;

		// Token: 0x04001C39 RID: 7225
		[NonSerialized]
		private bool m_populatedParentReportScope;

		// Token: 0x04001C3A RID: 7226
		[NonSerialized]
		private IRIFReportDataScope m_parentReportScope;

		// Token: 0x04001C3B RID: 7227
		[NonSerialized]
		private IReference<IOnDemandScopeInstance> m_currentStreamingScopeInstance;

		// Token: 0x04001C3C RID: 7228
		[NonSerialized]
		private IReference<IOnDemandScopeInstance> m_cachedNoRowsStreamingScopeInstance;

		// Token: 0x04001C3D RID: 7229
		[NonSerialized]
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> m_dataRegionScopedItemsForDataProcessing;

		// Token: 0x02000973 RID: 2419
		public enum ProcessingInnerGroupings
		{
			// Token: 0x040040D0 RID: 16592
			Column,
			// Token: 0x040040D1 RID: 16593
			Row
		}
	}
}

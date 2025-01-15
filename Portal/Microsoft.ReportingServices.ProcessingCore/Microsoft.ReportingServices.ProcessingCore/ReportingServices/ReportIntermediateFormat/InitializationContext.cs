using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200052E RID: 1326
	public struct InitializationContext
	{
		// Token: 0x06004760 RID: 18272 RVA: 0x0012ACD4 File Offset: 0x00128ED4
		internal InitializationContext(ICatalogItemContext reportContext, List<DataSet> datasets, ErrorContext errorContext, Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder exprHostBuilder, Report artificialReportContainerForCodeGeneration, Dictionary<string, ISortFilterScope> reportScopes, PublishingContextBase publishingContext)
		{
			this = new InitializationContext(reportContext, false, null, datasets, null, null, errorContext, exprHostBuilder, artificialReportContainerForCodeGeneration, null, reportScopes, false, false, 0, 0, 0, publishingContext, new ScopeTree(), true);
		}

		// Token: 0x06004761 RID: 18273 RVA: 0x0012AD04 File Offset: 0x00128F04
		internal InitializationContext(ICatalogItemContext reportContext, bool hasFilters, StringDictionary dataSources, List<DataSet> dataSets, ArrayList dynamicParameters, Hashtable dataSetQueryInfo, ErrorContext errorContext, Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder exprHostBuilder, Report report, CultureInfo reportLanguage, Dictionary<string, ISortFilterScope> reportScopes, bool hasUserSortPeerScopes, bool hasUserSort, int dataRegionCount, int textboxCount, int variableCount, PublishingContextBase publishingContext, ScopeTree scopeTree, bool isSharedDataSetContext)
		{
			Global.Tracer.Assert(errorContext != null, "(null != errorContext)");
			Global.Tracer.Assert(reportContext != null, "(null != reportContext)");
			this.m_publishingContext = publishingContext;
			this.m_reportContext = reportContext;
			this.m_location = Microsoft.ReportingServices.ReportPublishing.LocationFlags.None;
			this.m_objectType = Microsoft.ReportingServices.ReportProcessing.ObjectType.Report;
			this.m_objectName = null;
			this.m_tablixName = null;
			this.m_embeddedImages = report.EmbeddedImages;
			this.m_errorContext = errorContext;
			this.m_parameters = null;
			this.m_dynamicParameters = dynamicParameters;
			this.m_dataSetQueryInfo = dataSetQueryInfo;
			this.m_exprHostBuilder = exprHostBuilder;
			this.m_dataSources = dataSources;
			this.m_rowHeaderLevelSizeList = null;
			this.m_columnHeaderLevelSizeList = null;
			this.m_report = report;
			this.m_reportLanguage = reportLanguage;
			if (this.m_publishingContext.Configuration != null && this.m_publishingContext.Configuration.RdlSandboxing != null)
			{
				RdlSandboxValidator rdlSandboxValidator = new RdlSandboxValidator(this.m_publishingContext.Configuration.RdlSandboxing, report, this.m_publishingContext.CompilationTempAppDomain, errorContext);
				this.m_rdlSandboxExpressionValidator = rdlSandboxValidator.CreateExpressionValidator();
				this.m_rdlSandboxClassValidator = rdlSandboxValidator.CreateClassValidator();
			}
			else
			{
				this.m_rdlSandboxExpressionValidator = null;
				this.m_rdlSandboxClassValidator = null;
			}
			this.m_outerGroupName = null;
			this.m_currentGroupName = null;
			this.m_currentDataRegionName = null;
			this.m_runningValues = null;
			this.m_runningValuesOfAggregates = null;
			this.m_groupingScopesForRunningValues = new Hashtable();
			this.m_groupingScopesForRunningValuesInTablix = null;
			this.m_dataregionScopesForRunningValues = new Hashtable();
			this.m_scopeTree = scopeTree;
			this.m_hasFilters = hasFilters;
			this.m_currentScope = null;
			this.m_outermostDataregionScope = null;
			this.m_groupingScopes = new Hashtable();
			this.m_dataregionScopes = new Hashtable();
			this.m_datasetScopes = new Hashtable();
			this.m_numberOfDataSets = 0;
			this.m_oneDataSetName = null;
			this.m_activeDataSets = FunctionalList<DataSet>.Empty;
			this.m_activeScopeInfos = FunctionalList<InitializationContext.ScopeInfo>.Empty;
			this.m_fieldNameMap = new Hashtable();
			this.m_dataSetNameToDataRegionsMap = new Dictionary<string, List<DataRegion>>();
			this.m_isTopLevelCellContents = false;
			this.m_isDataRegionScopedCell = false;
			if (dataSets != null)
			{
				this.m_numberOfDataSets = dataSets.Count;
				this.m_oneDataSetName = ((1 == dataSets.Count) ? dataSets[0].Name : null);
				for (int i = 0; i < dataSets.Count; i++)
				{
					DataSet dataSet = dataSets[i];
					bool flag = this.m_datasetScopes.ContainsKey(dataSets[i].Name);
					this.m_datasetScopes[dataSets[i].Name] = new InitializationContext.ScopeInfo(true, dataSets[i].Aggregates, dataSets[i].PostSortAggregates, dataSets[i], flag);
					Hashtable hashtable = new Hashtable();
					if (dataSet.Fields != null)
					{
						for (int j = 0; j < dataSet.Fields.Count; j++)
						{
							hashtable[dataSet.Fields[j].Name] = j;
						}
					}
					this.m_fieldNameMap[dataSet.Name] = hashtable;
					if (!flag)
					{
						this.m_dataSetNameToDataRegionsMap.Add(dataSet.Name, dataSet.DataRegions);
					}
				}
			}
			if (report != null && report.Parameters != null)
			{
				this.m_parameters = new Hashtable();
				for (int k = 0; k < report.Parameters.Count; k++)
				{
					ParameterDef parameterDef = report.Parameters[k];
					if (parameterDef != null && !this.m_parameters.ContainsKey(parameterDef.Name))
					{
						this.m_parameters.Add(parameterDef.Name, parameterDef);
					}
				}
			}
			this.m_reportItemsInScope = new Dictionary<string, Pair<ReportItem, int>>();
			this.m_reportItemsInSection = new Dictionary<string, ReportItem>();
			this.m_variablesInScope = new Dictionary<string, Variable>();
			this.m_referencableVariables = new byte[(variableCount >> 3) + 1];
			this.m_referencableTextboxes = new byte[(textboxCount >> 3) + 1];
			this.m_referencableTextboxesInSection = new byte[(textboxCount >> 3) + 1];
			this.m_reportDataElementStyleAttribute = true;
			this.m_hasUserSorts = hasUserSort;
			this.m_hasUserSortPeerScopes = hasUserSortPeerScopes;
			this.m_lastPeerScopeId = 0;
			this.m_reportScopes = reportScopes;
			this.m_initializingUserSorts = false;
			if (hasUserSort || this.m_report.HasSubReports)
			{
				this.m_userSortExpressionScopes = new Dictionary<string, List<IInScopeEventSource>>();
				this.m_userSortEventSources = new Dictionary<string, List<IInScopeEventSource>>();
				this.m_peerScopes = (hasUserSortPeerScopes ? new Hashtable() : null);
				this.m_reportScopeDatasets = new Hashtable();
				this.m_detailSortExpressionScopeEventSources = new List<IInScopeEventSource>();
			}
			else
			{
				this.m_userSortExpressionScopes = null;
				this.m_userSortEventSources = null;
				this.m_peerScopes = null;
				this.m_reportScopeDatasets = null;
				this.m_detailSortExpressionScopeEventSources = null;
			}
			this.m_inAutoSubtotalClone = false;
			this.m_visibilityToggleInfos = new List<VisibilityToggleInfo>();
			this.m_toggleItems = new Dictionary<string, ToggleItemInfo>();
			this.m_visibilityContainmentInfos = new Stack<InitializationContext.VisibilityContainmentInfo>();
			this.m_memberCellIndex = null;
			this.m_indexInCollectionTableForDataRegions = new Dictionary<Hashtable, int>(InitializationContext.HashtableKeyComparer.Instance);
			this.m_indexInCollectionTableForSubReports = new Dictionary<Hashtable, int>(InitializationContext.HashtableKeyComparer.Instance);
			this.m_indexInCollectionTable = null;
			this.m_currentAbsoluteTop = 0.0;
			this.m_currentAbsoluteLeft = 0.0;
			this.m_hasPreviousAggregates = report.HasPreviousAggregates;
			this.m_axisGroupingScopesForRunningValues = null;
			this.m_groupingExprCountAtScope = new Dictionary<string, int>();
			this.m_lookupCompactionTable = new Dictionary<string, InitializationContext.LookupDestinationCompactionTable>();
			this.m_handledCellContents = new Holder<bool>();
			this.m_handledCellContents.Value = false;
			this.m_inRecursiveHierarchyColumns = false;
			this.m_inRecursiveHierarchyRows = false;
			this.m_naturalGroupExpressionsByDataSetName = new Dictionary<string, List<ExpressionInfo>>();
			this.m_naturalSortExpressionsByDataSetName = new Dictionary<string, List<ExpressionInfo>>();
			this.m_dataSetsForIdcInNestedDR = new Dictionary<int, IRIFDataScope>();
			this.m_dataSetsForIdc = new Dictionary<int, IRIFDataScope>();
			this.m_dataSetsForNonStructuralIdc = new Dictionary<int, IRIFDataScope>();
		}

		// Token: 0x06004762 RID: 18274 RVA: 0x0012B274 File Offset: 0x00129474
		internal void RSDRegisterDataSetParameters(DataSetCore sharedDataset)
		{
			this.m_parameters = new Hashtable();
			if (sharedDataset == null || sharedDataset.Query == null || sharedDataset.Query.Parameters == null)
			{
				return;
			}
			int count = sharedDataset.Query.Parameters.Count;
			for (int i = 0; i < count; i++)
			{
				DataSetParameterValue dataSetParameterValue = sharedDataset.Query.Parameters[i] as DataSetParameterValue;
				if (dataSetParameterValue != null && dataSetParameterValue.UniqueName != null && !this.m_parameters.ContainsKey(dataSetParameterValue.UniqueName))
				{
					this.m_parameters.Add(dataSetParameterValue.UniqueName, null);
				}
			}
		}

		// Token: 0x17001DA2 RID: 7586
		// (get) Token: 0x06004763 RID: 18275 RVA: 0x0012B309 File Offset: 0x00129509
		internal ICatalogItemContext ReportContext
		{
			get
			{
				return this.m_reportContext;
			}
		}

		// Token: 0x17001DA3 RID: 7587
		// (get) Token: 0x06004764 RID: 18276 RVA: 0x0012B311 File Offset: 0x00129511
		// (set) Token: 0x06004765 RID: 18277 RVA: 0x0012B319 File Offset: 0x00129519
		internal Microsoft.ReportingServices.ReportPublishing.LocationFlags Location
		{
			get
			{
				return this.m_location;
			}
			set
			{
				this.m_location = value;
			}
		}

		// Token: 0x17001DA4 RID: 7588
		// (get) Token: 0x06004766 RID: 18278 RVA: 0x0012B322 File Offset: 0x00129522
		// (set) Token: 0x06004767 RID: 18279 RVA: 0x0012B32A File Offset: 0x0012952A
		internal Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return this.m_objectType;
			}
			set
			{
				this.m_objectType = value;
			}
		}

		// Token: 0x17001DA5 RID: 7589
		// (get) Token: 0x06004768 RID: 18280 RVA: 0x0012B333 File Offset: 0x00129533
		// (set) Token: 0x06004769 RID: 18281 RVA: 0x0012B33B File Offset: 0x0012953B
		internal string ObjectName
		{
			get
			{
				return this.m_objectName;
			}
			set
			{
				this.m_objectName = value;
			}
		}

		// Token: 0x17001DA6 RID: 7590
		// (get) Token: 0x0600476A RID: 18282 RVA: 0x0012B344 File Offset: 0x00129544
		// (set) Token: 0x0600476B RID: 18283 RVA: 0x0012B34C File Offset: 0x0012954C
		internal bool IsTopLevelCellContents
		{
			get
			{
				return this.m_isTopLevelCellContents;
			}
			set
			{
				this.m_isTopLevelCellContents = value;
			}
		}

		// Token: 0x17001DA7 RID: 7591
		// (get) Token: 0x0600476C RID: 18284 RVA: 0x0012B355 File Offset: 0x00129555
		internal bool HasUserSorts
		{
			get
			{
				return this.m_hasUserSorts;
			}
		}

		// Token: 0x17001DA8 RID: 7592
		// (get) Token: 0x0600476D RID: 18285 RVA: 0x0012B35D File Offset: 0x0012955D
		// (set) Token: 0x0600476E RID: 18286 RVA: 0x0012B365 File Offset: 0x00129565
		internal bool InitializingUserSorts
		{
			get
			{
				return this.m_initializingUserSorts;
			}
			set
			{
				this.m_initializingUserSorts = value;
			}
		}

		// Token: 0x17001DA9 RID: 7593
		// (get) Token: 0x0600476F RID: 18287 RVA: 0x0012B36E File Offset: 0x0012956E
		internal bool IsDataRegionCellScope
		{
			get
			{
				return this.m_axisGroupingScopesForRunningValues.InCurrentDataRegionDynamicRow && this.m_axisGroupingScopesForRunningValues.InCurrentDataRegionDynamicColumn;
			}
		}

		// Token: 0x17001DAA RID: 7594
		// (get) Token: 0x06004770 RID: 18288 RVA: 0x0012B38A File Offset: 0x0012958A
		internal bool CellHasDynamicRowsAndColumns
		{
			get
			{
				return this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeName != null && this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeName != null;
			}
		}

		// Token: 0x17001DAB RID: 7595
		// (get) Token: 0x06004771 RID: 18289 RVA: 0x0012B3A9 File Offset: 0x001295A9
		// (set) Token: 0x06004772 RID: 18290 RVA: 0x0012B3B1 File Offset: 0x001295B1
		internal bool IsDataRegionScopedCell
		{
			get
			{
				return this.m_isDataRegionScopedCell;
			}
			set
			{
				this.m_isDataRegionScopedCell = value;
			}
		}

		// Token: 0x17001DAC RID: 7596
		// (get) Token: 0x06004773 RID: 18291 RVA: 0x0012B3BA File Offset: 0x001295BA
		// (set) Token: 0x06004774 RID: 18292 RVA: 0x0012B3C2 File Offset: 0x001295C2
		internal bool ReportDataElementStyleAttribute
		{
			get
			{
				return this.m_reportDataElementStyleAttribute;
			}
			set
			{
				this.m_reportDataElementStyleAttribute = value;
			}
		}

		// Token: 0x17001DAD RID: 7597
		// (get) Token: 0x06004775 RID: 18293 RVA: 0x0012B3CB File Offset: 0x001295CB
		// (set) Token: 0x06004776 RID: 18294 RVA: 0x0012B3D3 File Offset: 0x001295D3
		internal string TablixName
		{
			get
			{
				return this.m_tablixName;
			}
			set
			{
				this.m_tablixName = value;
			}
		}

		// Token: 0x17001DAE RID: 7598
		// (get) Token: 0x06004777 RID: 18295 RVA: 0x0012B3DC File Offset: 0x001295DC
		internal Dictionary<string, ImageInfo> EmbeddedImages
		{
			get
			{
				return this.m_embeddedImages;
			}
		}

		// Token: 0x17001DAF RID: 7599
		// (get) Token: 0x06004778 RID: 18296 RVA: 0x0012B3E4 File Offset: 0x001295E4
		internal ErrorContext ErrorContext
		{
			get
			{
				return this.m_errorContext;
			}
		}

		// Token: 0x17001DB0 RID: 7600
		// (get) Token: 0x06004779 RID: 18297 RVA: 0x0012B3EC File Offset: 0x001295EC
		internal Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder ExprHostBuilder
		{
			get
			{
				return this.m_exprHostBuilder;
			}
		}

		// Token: 0x17001DB1 RID: 7601
		// (get) Token: 0x0600477A RID: 18298 RVA: 0x0012B3F4 File Offset: 0x001295F4
		internal bool MergeOnePass
		{
			get
			{
				return this.m_report.MergeOnePass;
			}
		}

		// Token: 0x17001DB2 RID: 7602
		// (get) Token: 0x0600477B RID: 18299 RVA: 0x0012B401 File Offset: 0x00129601
		internal CultureInfo ReportLanguage
		{
			get
			{
				return this.m_reportLanguage;
			}
		}

		// Token: 0x17001DB3 RID: 7603
		// (get) Token: 0x0600477C RID: 18300 RVA: 0x0012B409 File Offset: 0x00129609
		// (set) Token: 0x0600477D RID: 18301 RVA: 0x0012B411 File Offset: 0x00129611
		internal IList<Pair<double, int>> ColumnHeaderLevelSizeList
		{
			get
			{
				return this.m_columnHeaderLevelSizeList;
			}
			set
			{
				this.m_columnHeaderLevelSizeList = value;
			}
		}

		// Token: 0x17001DB4 RID: 7604
		// (get) Token: 0x0600477E RID: 18302 RVA: 0x0012B41A File Offset: 0x0012961A
		// (set) Token: 0x0600477F RID: 18303 RVA: 0x0012B422 File Offset: 0x00129622
		internal IList<Pair<double, int>> RowHeaderLevelSizeList
		{
			get
			{
				return this.m_rowHeaderLevelSizeList;
			}
			set
			{
				this.m_rowHeaderLevelSizeList = value;
			}
		}

		// Token: 0x17001DB5 RID: 7605
		// (get) Token: 0x06004780 RID: 18304 RVA: 0x0012B42B File Offset: 0x0012962B
		// (set) Token: 0x06004781 RID: 18305 RVA: 0x0012B433 File Offset: 0x00129633
		internal bool InAutoSubtotalClone
		{
			get
			{
				return this.m_inAutoSubtotalClone;
			}
			set
			{
				this.m_inAutoSubtotalClone = value;
			}
		}

		// Token: 0x17001DB6 RID: 7606
		// (get) Token: 0x06004782 RID: 18306 RVA: 0x0012B43C File Offset: 0x0012963C
		// (set) Token: 0x06004783 RID: 18307 RVA: 0x0012B449 File Offset: 0x00129649
		internal int MemberCellIndex
		{
			get
			{
				return this.m_memberCellIndex.Value;
			}
			set
			{
				this.m_memberCellIndex.Value = value;
			}
		}

		// Token: 0x17001DB7 RID: 7607
		// (get) Token: 0x06004784 RID: 18308 RVA: 0x0012B457 File Offset: 0x00129657
		internal double CurrentAbsoluteTop
		{
			get
			{
				return this.m_currentAbsoluteTop;
			}
		}

		// Token: 0x17001DB8 RID: 7608
		// (get) Token: 0x06004785 RID: 18309 RVA: 0x0012B45F File Offset: 0x0012965F
		internal double CurrentAbsoluteLeft
		{
			get
			{
				return this.m_currentAbsoluteLeft;
			}
		}

		// Token: 0x17001DB9 RID: 7609
		// (get) Token: 0x06004786 RID: 18310 RVA: 0x0012B467 File Offset: 0x00129667
		internal bool HasPreviousAggregates
		{
			get
			{
				return this.m_hasPreviousAggregates;
			}
		}

		// Token: 0x17001DBA RID: 7610
		// (get) Token: 0x06004787 RID: 18311 RVA: 0x0012B46F File Offset: 0x0012966F
		internal Dictionary<string, int> GroupingExprCountAtScope
		{
			get
			{
				return this.m_groupingExprCountAtScope;
			}
		}

		// Token: 0x17001DBB RID: 7611
		// (get) Token: 0x06004788 RID: 18312 RVA: 0x0012B477 File Offset: 0x00129677
		internal bool IsRunningValueDirectionColumn
		{
			get
			{
				return this.m_groupingScopesForRunningValuesInTablix == null || this.m_groupingScopesForRunningValuesInTablix.IsRunningValueDirectionColumn;
			}
		}

		// Token: 0x17001DBC RID: 7612
		// (get) Token: 0x06004789 RID: 18313 RVA: 0x0012B48E File Offset: 0x0012968E
		internal bool HasLookups
		{
			get
			{
				return this.m_lookupCompactionTable.Count > 0;
			}
		}

		// Token: 0x17001DBD RID: 7613
		// (get) Token: 0x0600478A RID: 18314 RVA: 0x0012B49E File Offset: 0x0012969E
		internal Report Report
		{
			get
			{
				return this.m_report;
			}
		}

		// Token: 0x17001DBE RID: 7614
		// (get) Token: 0x0600478B RID: 18315 RVA: 0x0012B4A6 File Offset: 0x001296A6
		internal ScopeTree ScopeTree
		{
			get
			{
				return this.m_scopeTree;
			}
		}

		// Token: 0x17001DBF RID: 7615
		// (get) Token: 0x0600478C RID: 18316 RVA: 0x0012B4AE File Offset: 0x001296AE
		// (set) Token: 0x0600478D RID: 18317 RVA: 0x0012B4BB File Offset: 0x001296BB
		internal bool HandledCellContents
		{
			get
			{
				return this.m_handledCellContents.Value;
			}
			set
			{
				this.m_handledCellContents.Value = value;
			}
		}

		// Token: 0x17001DC0 RID: 7616
		// (get) Token: 0x0600478E RID: 18318 RVA: 0x0012B4C9 File Offset: 0x001296C9
		// (set) Token: 0x0600478F RID: 18319 RVA: 0x0012B4D1 File Offset: 0x001296D1
		internal bool InRecursiveHierarchyColumns
		{
			get
			{
				return this.m_inRecursiveHierarchyColumns;
			}
			set
			{
				this.m_inRecursiveHierarchyColumns = value;
			}
		}

		// Token: 0x17001DC1 RID: 7617
		// (get) Token: 0x06004790 RID: 18320 RVA: 0x0012B4DA File Offset: 0x001296DA
		// (set) Token: 0x06004791 RID: 18321 RVA: 0x0012B4E2 File Offset: 0x001296E2
		internal bool InRecursiveHierarchyRows
		{
			get
			{
				return this.m_inRecursiveHierarchyRows;
			}
			set
			{
				this.m_inRecursiveHierarchyRows = value;
			}
		}

		// Token: 0x17001DC2 RID: 7618
		// (get) Token: 0x06004792 RID: 18322 RVA: 0x0012B4EB File Offset: 0x001296EB
		internal PublishingContextBase PublishingContext
		{
			get
			{
				return this.m_publishingContext;
			}
		}

		// Token: 0x06004793 RID: 18323 RVA: 0x0012B4F4 File Offset: 0x001296F4
		internal void ValidateScopeRulesForNaturalGroup(ReportHierarchyNode member)
		{
			if (member.Grouping == null || !member.Grouping.NaturalGroup)
			{
				return;
			}
			ErrorContext errorContext = this.m_errorContext;
			List<ExpressionInfo> groupExpressions = new List<ExpressionInfo>();
			ScopeTree.DirectedScopeTreeVisitor directedScopeTreeVisitor = delegate(IRIFDataScope scope)
			{
				ReportHierarchyNode reportHierarchyNode = scope as ReportHierarchyNode;
				if (reportHierarchyNode != null && DataSet.AreEqualById(member.DataScopeInfo.DataSet, reportHierarchyNode.DataScopeInfo.DataSet))
				{
					if (!reportHierarchyNode.Grouping.NaturalGroup)
					{
						errorContext.Register(ProcessingErrorCode.rsInvalidGroupingContainerNotNaturalGroup, Severity.Warning, member.DataRegionDef.ObjectType, member.DataRegionDef.Name, "Grouping", new string[]
						{
							member.Grouping.Name,
							reportHierarchyNode.Grouping.Name
						});
						member.Grouping.NaturalGroup = false;
						return false;
					}
					InitializationContext.AppendExpressions(groupExpressions, reportHierarchyNode.Grouping.GroupExpressions);
				}
				return true;
			};
			this.ValidateNaturalGroupOrSortRulesForScopeTree(this.m_naturalGroupExpressionsByDataSetName, member, groupExpressions, directedScopeTreeVisitor, ProcessingErrorCode.rsConflictingNaturalGroupRequirements, "NaturalGroup");
		}

		// Token: 0x06004794 RID: 18324 RVA: 0x0012B578 File Offset: 0x00129778
		internal void ValidateScopeRulesForNaturalSort(ReportHierarchyNode member)
		{
			if (member.Grouping == null || member.Sorting == null || !member.Sorting.NaturalSort)
			{
				return;
			}
			ErrorContext errorContext = this.m_errorContext;
			List<ExpressionInfo> sortExpressions = new List<ExpressionInfo>();
			ScopeTree.DirectedScopeTreeVisitor directedScopeTreeVisitor = delegate(IRIFDataScope scope)
			{
				Sorting sorting = null;
				string text = null;
				ReportHierarchyNode reportHierarchyNode = scope as ReportHierarchyNode;
				DataRegion dataRegion = scope as DataRegion;
				if (reportHierarchyNode != null)
				{
					sorting = reportHierarchyNode.Sorting;
					text = reportHierarchyNode.Grouping.Name;
				}
				else if (dataRegion != null)
				{
					sorting = dataRegion.Sorting;
					text = dataRegion.Name;
				}
				if (sorting != null && member.DataScopeInfo.DataSet.ID == scope.DataScopeInfo.DataSet.ID)
				{
					if (!sorting.NaturalSort)
					{
						errorContext.Register(ProcessingErrorCode.rsInvalidSortingContainerNotNaturalSort, Severity.Warning, member.DataRegionDef.ObjectType, member.DataRegionDef.Name, "SortExpressions", new string[]
						{
							member.Grouping.Name,
							text
						});
						member.Sorting.NaturalSort = false;
						return false;
					}
					InitializationContext.AppendExpressions(sortExpressions, reportHierarchyNode.Sorting.SortExpressions);
				}
				return true;
			};
			this.ValidateNaturalGroupOrSortRulesForScopeTree(this.m_naturalSortExpressionsByDataSetName, member, sortExpressions, directedScopeTreeVisitor, ProcessingErrorCode.rsConflictingNaturalSortRequirements, "NaturalSort");
		}

		// Token: 0x06004795 RID: 18325 RVA: 0x0012B608 File Offset: 0x00129808
		internal void ValidateScopeRulesForIdcNaturalJoin(IRIFDataScope startScope)
		{
			ErrorContext errorContext = this.m_errorContext;
			ScopeTree.DirectedScopeTreeVisitor directedScopeTreeVisitor = delegate(IRIFDataScope scope)
			{
				if (scope.DataScopeInfo != null && scope.DataScopeInfo.JoinInfo != null)
				{
					scope.DataScopeInfo.JoinInfo.CheckContainerJoinForNaturalJoin(startScope, errorContext, scope);
				}
				ReportHierarchyNode reportHierarchyNode = scope as ReportHierarchyNode;
				if (reportHierarchyNode != null && !reportHierarchyNode.Grouping.NaturalGroup)
				{
					errorContext.Register(ProcessingErrorCode.rsInvalidRelationshipGroupingContainerNotNaturalGroup, Severity.Error, startScope.DataScopeObjectType, startScope.Name, "Grouping", new string[] { scope.Name });
					return false;
				}
				return true;
			};
			this.m_scopeTree.Traverse(directedScopeTreeVisitor, startScope);
		}

		// Token: 0x06004796 RID: 18326 RVA: 0x0012B650 File Offset: 0x00129850
		private void ValidateNaturalGroupOrSortRulesForScopeTree(Dictionary<string, List<ExpressionInfo>> expressionsByDataSetName, ReportHierarchyNode member, List<ExpressionInfo> expressions, ScopeTree.DirectedScopeTreeVisitor validator, ProcessingErrorCode conflictingNaturalRequirementErrorCode, string naturalElementName)
		{
			if (this.m_scopeTree.Traverse(validator, member))
			{
				string currentDataSetName = this.GetCurrentDataSetName();
				if (currentDataSetName != null)
				{
					List<ExpressionInfo> list;
					if (expressionsByDataSetName.TryGetValue(currentDataSetName, out list))
					{
						if (!ListUtils.AreSameOrSuffix<ExpressionInfo>(expressions, list, RdlExpressionComparer.Instance))
						{
							this.m_errorContext.Register(conflictingNaturalRequirementErrorCode, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.DataSet, currentDataSetName, naturalElementName, Array.Empty<string>());
						}
						if (list.Count > expressions.Count)
						{
							expressions = list;
						}
					}
					expressionsByDataSetName[currentDataSetName] = expressions;
				}
			}
		}

		// Token: 0x06004797 RID: 18327 RVA: 0x0012B6C2 File Offset: 0x001298C2
		private static void AppendExpressions(List<ExpressionInfo> allExpressions, List<ExpressionInfo> localExpressions)
		{
			if (localExpressions != null)
			{
				allExpressions.AddRange(localExpressions);
			}
		}

		// Token: 0x06004798 RID: 18328 RVA: 0x0012B6CE File Offset: 0x001298CE
		internal void EnsureDataSetUsedOnceForIdcUnderTopDataRegion(DataSet dataSet, IRIFDataScope currentScope)
		{
			if (!currentScope.DataScopeInfo.NeedsIDC)
			{
				return;
			}
			if (!this.IsErrorForDuplicateIdcDataSet(this.m_dataSetsForIdcInNestedDR, dataSet, currentScope, ProcessingErrorCode.rsInvalidRelationshipDataSetUsedMoreThanOnce))
			{
				this.IsErrorForDuplicateIdcDataSet(this.m_dataSetsForIdc, dataSet, currentScope, ProcessingErrorCode.rsInvalidRelationshipDataSet);
			}
		}

		// Token: 0x06004799 RID: 18329 RVA: 0x0012B708 File Offset: 0x00129908
		private bool IsErrorForDuplicateIdcDataSet(Dictionary<int, IRIFDataScope> mappingDataSetIdToFirstIdcScope, DataSet dataSet, IRIFDataScope currentScope, ProcessingErrorCode errorCode)
		{
			IRIFDataScope irifdataScope;
			if (mappingDataSetIdToFirstIdcScope.TryGetValue(dataSet.ID, out irifdataScope))
			{
				if (!this.IsOtherParentOfCurrentIntersection(irifdataScope, currentScope) && !this.IsOtherSameIntersectionScope(irifdataScope, currentScope))
				{
					this.m_errorContext.Register(errorCode, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.DataSet, dataSet.Name, currentScope.DataScopeObjectType.ToString(), new string[]
					{
						currentScope.Name,
						irifdataScope.DataScopeObjectType.ToString(),
						irifdataScope.Name
					});
				}
				return true;
			}
			mappingDataSetIdToFirstIdcScope.Add(dataSet.ID, currentScope);
			return false;
		}

		// Token: 0x0600479A RID: 18330 RVA: 0x0012B7A4 File Offset: 0x001299A4
		private bool IsOtherParentOfCurrentIntersection(IRIFDataScope otherScope, IRIFDataScope currentScope)
		{
			return this.m_scopeTree.IsIntersectionScope(currentScope) && (this.m_scopeTree.IsSameOrParentScope(otherScope, this.m_scopeTree.GetParentRowScopeForIntersection(currentScope)) || this.m_scopeTree.IsSameOrParentScope(otherScope, this.m_scopeTree.GetParentColumnScopeForIntersection(currentScope)));
		}

		// Token: 0x0600479B RID: 18331 RVA: 0x0012B7F8 File Offset: 0x001299F8
		private bool IsOtherSameIntersectionScope(IRIFDataScope otherScope, IRIFDataScope currentScope)
		{
			return this.m_scopeTree.IsIntersectionScope(currentScope) && this.m_scopeTree.IsIntersectionScope(otherScope) && ScopeTree.SameScope(this.m_scopeTree.GetParentRowScopeForIntersection(currentScope), this.m_scopeTree.GetParentRowScopeForIntersection(otherScope)) && ScopeTree.SameScope(this.m_scopeTree.GetParentColumnScopeForIntersection(currentScope), this.m_scopeTree.GetParentColumnScopeForIntersection(otherScope));
		}

		// Token: 0x0600479C RID: 18332 RVA: 0x0012B860 File Offset: 0x00129A60
		private string GetCurrentDataSetName()
		{
			DataSet first = this.m_activeDataSets.First;
			if (first != null)
			{
				return first.Name;
			}
			return null;
		}

		// Token: 0x0600479D RID: 18333 RVA: 0x0012B884 File Offset: 0x00129A84
		private int GetCurrentDataSetIndex()
		{
			DataSet first = this.m_activeDataSets.First;
			if (first != null)
			{
				return first.IndexInCollection;
			}
			return -1;
		}

		// Token: 0x0600479E RID: 18334 RVA: 0x0012B8A8 File Offset: 0x00129AA8
		private void RegisterDataSetScope(DataSet dataSet, List<DataAggregateInfo> scopeAggregates, List<DataAggregateInfo> scopePostSortAggregates, int datasetIndexInCollection)
		{
			Global.Tracer.Assert(dataSet != null);
			Global.Tracer.Assert(scopeAggregates != null);
			Global.Tracer.Assert(scopePostSortAggregates != null);
			this.m_currentScope = new InitializationContext.ScopeInfo(true, scopeAggregates, scopePostSortAggregates, dataSet);
			if (this.m_initializingUserSorts && !this.m_reportScopeDatasets.ContainsKey(dataSet.Name))
			{
				this.m_reportScopeDatasets.Add(dataSet.Name, dataSet);
			}
		}

		// Token: 0x0600479F RID: 18335 RVA: 0x0012B91B File Offset: 0x00129B1B
		private void UnRegisterDataSetScope(string scopeName)
		{
			Global.Tracer.Assert(scopeName != null);
			this.m_currentScope = null;
		}

		// Token: 0x060047A0 RID: 18336 RVA: 0x0012B934 File Offset: 0x00129B34
		private void RegisterDataRegionScope(DataRegion dataRegion)
		{
			Global.Tracer.Assert(dataRegion.Name != null);
			Global.Tracer.Assert(dataRegion.Aggregates != null);
			Global.Tracer.Assert(dataRegion.PostSortAggregates != null);
			this.m_currentDataRegionName = dataRegion.Name;
			this.m_dataregionScopesForRunningValues[dataRegion.Name] = this.m_currentGroupName;
			if (!this.m_initializingUserSorts)
			{
				this.SetIndexInCollection(dataRegion);
				this.m_memberCellIndex = new Holder<int>();
				this.m_indexInCollectionTableForDataRegions = new Dictionary<Hashtable, int>(InitializationContext.HashtableKeyComparer.Instance);
				this.m_indexInCollectionTableForSubReports = new Dictionary<Hashtable, int>(InitializationContext.HashtableKeyComparer.Instance);
				this.m_indexInCollectionTable = new Dictionary<Hashtable, int>(InitializationContext.HashtableKeyComparer.Instance);
			}
			else
			{
				this.m_reportScopeDatasets.Add(dataRegion.Name, this.GetDataSet());
			}
			this.m_currentScope = this.CreateScopeInfo(dataRegion, this.m_currentScope == null || this.m_currentScope.AllowCustomAggregates);
			if (this.m_axisGroupingScopesForRunningValues != null)
			{
				this.m_axisGroupingScopesForRunningValues = new InitializationContext.AxisGroupingScopesForRunningValues(this.m_axisGroupingScopesForRunningValues);
			}
			else
			{
				this.m_axisGroupingScopesForRunningValues = new InitializationContext.AxisGroupingScopesForRunningValues();
			}
			if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				this.m_outermostDataregionScope = this.m_currentScope;
			}
			this.m_dataregionScopes[dataRegion.Name] = this.m_currentScope;
		}

		// Token: 0x060047A1 RID: 18337 RVA: 0x0012BA76 File Offset: 0x00129C76
		private InitializationContext.ScopeInfo CreateScopeInfo(DataRegion dataRegion, bool allowCustomAggregates)
		{
			return new InitializationContext.ScopeInfo(allowCustomAggregates, dataRegion.Aggregates, dataRegion.PostSortAggregates, dataRegion);
		}

		// Token: 0x060047A2 RID: 18338 RVA: 0x0012BA8C File Offset: 0x00129C8C
		private void UnRegisterDataRegionScope(DataRegion dataRegion)
		{
			string name = dataRegion.Name;
			Global.Tracer.Assert(name != null);
			this.m_currentDataRegionName = null;
			this.m_dataregionScopesForRunningValues.Remove(name);
			this.m_currentScope = null;
			if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				this.m_outermostDataregionScope = null;
			}
			this.m_dataregionScopes.Remove(name);
			this.m_axisGroupingScopesForRunningValues = null;
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x0012BAEC File Offset: 0x00129CEC
		internal void RegisterGroupingScope(ReportHierarchyNode member)
		{
			this.RegisterGroupingScope(member, false);
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x0012BAF8 File Offset: 0x00129CF8
		private void RegisterGroupingScope(ReportHierarchyNode member, bool forTablixCell)
		{
			if (forTablixCell)
			{
				Global.Tracer.Assert(this.m_groupingScopesForRunningValuesInTablix != null, "(null != m_groupingScopesForRunningValuesInTablix)");
				Global.Tracer.Assert(this.m_groupingScopesForRunningValues != null, "(null != m_groupingScopesForRunningValues)");
			}
			DataSet dataSet = member.DataScopeInfo.DataSet;
			if (this.m_activeDataSets.First != dataSet)
			{
				this.RegisterDataSet(dataSet);
			}
			Grouping grouping = member.Grouping;
			this.m_outerGroupName = this.m_currentGroupName;
			this.m_currentGroupName = grouping.Name;
			string name = grouping.Name;
			this.m_groupingScopesForRunningValues[name] = null;
			if (member.IsColumn)
			{
				if (forTablixCell)
				{
					this.m_groupingScopesForRunningValuesInTablix.RegisterColumnGrouping(grouping);
				}
				this.m_axisGroupingScopesForRunningValues.RegisterColumnGrouping(grouping);
			}
			else
			{
				if (forTablixCell)
				{
					this.m_groupingScopesForRunningValuesInTablix.RegisterRowGrouping(grouping);
				}
				this.m_axisGroupingScopesForRunningValues.RegisterRowGrouping(grouping);
			}
			this.m_currentScope = this.CreateScopeInfo(member);
			this.m_groupingScopes[name] = this.m_currentScope;
			if (forTablixCell && this.m_initializingUserSorts && !this.m_reportScopeDatasets.ContainsKey(name))
			{
				this.m_reportScopeDatasets.Add(name, this.GetDataSet());
			}
			this.RegisterRunningValues(member.RunningValues, member.DataScopeInfo.RunningValuesOfAggregates);
		}

		// Token: 0x060047A5 RID: 18341 RVA: 0x0012BC2D File Offset: 0x00129E2D
		private InitializationContext.ScopeInfo CreateScopeInfo(ReportHierarchyNode member)
		{
			return this.CreateScopeInfo(member, (this.m_currentScope == null) ? member.Grouping.SimpleGroupExpressions : (member.Grouping.SimpleGroupExpressions && this.m_currentScope.AllowCustomAggregates));
		}

		// Token: 0x060047A6 RID: 18342 RVA: 0x0012BC66 File Offset: 0x00129E66
		private InitializationContext.ScopeInfo CreateScopeInfo(ReportHierarchyNode member, bool allowCustomAggregates)
		{
			return new InitializationContext.ScopeInfo(allowCustomAggregates, member.Grouping.Aggregates, member.Grouping.PostSortAggregates, member.Grouping.RecursiveAggregates, member.Grouping, member);
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x0012BC96 File Offset: 0x00129E96
		internal void UnRegisterGroupingScope(ReportHierarchyNode member)
		{
			this.UnRegisterGroupingScope(member, false);
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x0012BCA0 File Offset: 0x00129EA0
		private void UnRegisterGroupingScope(ReportHierarchyNode member, bool forTablixCell)
		{
			if (forTablixCell)
			{
				Global.Tracer.Assert(this.m_groupingScopesForRunningValuesInTablix != null, "(null != m_groupingScopesForRunningValuesInTablix)");
				Global.Tracer.Assert(this.m_groupingScopesForRunningValues != null, "(null != m_groupingScopesForRunningValues)");
			}
			Grouping grouping = member.Grouping;
			string name = grouping.Name;
			this.m_outerGroupName = null;
			this.m_currentGroupName = null;
			this.m_groupingScopesForRunningValues.Remove(name);
			if (member.IsColumn)
			{
				if (forTablixCell)
				{
					this.m_groupingScopesForRunningValuesInTablix.UnRegisterColumnGrouping(name);
				}
				this.m_axisGroupingScopesForRunningValues.UnregisterColumnGrouping(grouping);
			}
			else
			{
				if (forTablixCell)
				{
					this.m_groupingScopesForRunningValuesInTablix.UnRegisterRowGrouping(name);
				}
				this.m_axisGroupingScopesForRunningValues.UnregisterRowGrouping(grouping);
			}
			this.m_currentScope = null;
			this.m_groupingScopes.Remove(name);
			this.UnRegisterRunningValues(member.RunningValues, member.DataScopeInfo.RunningValuesOfAggregates);
			DataSet dataSet = member.DataScopeInfo.DataSet;
			this.UnRegisterDataSet(dataSet);
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x0012BD88 File Offset: 0x00129F88
		internal void ValidateHideDuplicateScope(string hideDuplicateScope, ReportItem reportItem)
		{
			if (hideDuplicateScope == null)
			{
				return;
			}
			bool flag = true;
			InitializationContext.ScopeInfo scopeInfo = null;
			if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDetail) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 && hideDuplicateScope.Equals(this.m_currentGroupName))
			{
				flag = false;
			}
			else if (this.m_groupingScopes.Contains(hideDuplicateScope))
			{
				scopeInfo = (InitializationContext.ScopeInfo)this.m_groupingScopes[hideDuplicateScope];
			}
			else if (!this.m_datasetScopes.ContainsKey(hideDuplicateScope))
			{
				flag = false;
			}
			if (flag)
			{
				if (scopeInfo != null)
				{
					Global.Tracer.Assert(scopeInfo.GroupingScope != null, "(null != scope.GroupingScope)");
					scopeInfo.GroupingScope.AddReportItemWithHideDuplicates(reportItem);
					return;
				}
			}
			else
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidHideDuplicateScope, Severity.Error, this.m_objectType, this.m_objectName, "HideDuplicates", new string[] { hideDuplicateScope });
			}
		}

		// Token: 0x060047AA RID: 18346 RVA: 0x0012BE41 File Offset: 0x0012A041
		internal void RegisterGroupingScopeForDataRegionCell(ReportHierarchyNode member)
		{
			this.RegisterGroupingScope(member, true);
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x0012BE4B File Offset: 0x0012A04B
		internal void UnRegisterGroupingScopeForDataRegionCell(ReportHierarchyNode member)
		{
			this.UnRegisterGroupingScope(member, true);
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x0012BE58 File Offset: 0x0012A058
		internal void RegisterIndividualCellScope(Cell cell)
		{
			DataSet dataSet = cell.DataScopeInfo.DataSet;
			this.RegisterDataSet(dataSet);
			this.m_currentScope = new InitializationContext.ScopeInfo(this.m_currentScope == null || this.m_currentScope.AllowCustomAggregates, cell.Aggregates, cell.PostSortAggregates, cell);
			this.m_runningValues = cell.RunningValues;
			this.m_runningValuesOfAggregates = cell.DataScopeInfo.RunningValuesOfAggregates;
			IRIFDataScope canonicalCellScope = this.m_scopeTree.GetCanonicalCellScope(cell);
			if (canonicalCellScope != null && cell != canonicalCellScope)
			{
				cell.DataScopeInfo.ScopeID = canonicalCellScope.DataScopeInfo.ScopeID;
			}
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x0012BEED File Offset: 0x0012A0ED
		internal void UnRegisterIndividualCellScope(Cell cell)
		{
			this.UnRegisterCell(cell);
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x0012BEF6 File Offset: 0x0012A0F6
		private void UnRegisterCell(Cell cell)
		{
			this.UnRegisterNonScopeCell(cell);
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x0012BF00 File Offset: 0x0012A100
		internal void RegisterNonScopeCell(Cell cell)
		{
			DataSet dataSet = cell.DataScopeInfo.DataSet;
			this.RegisterDataSet(dataSet);
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x0012BF20 File Offset: 0x0012A120
		internal void UnRegisterNonScopeCell(Cell cell)
		{
			DataSet dataSet = cell.DataScopeInfo.DataSet;
			this.UnRegisterDataSet(dataSet);
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x0012BF40 File Offset: 0x0012A140
		internal void FoundAtomicScope(IRIFDataScope scope)
		{
			this.MarkChildScopesWithAtomicParent(scope);
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x0012BF4C File Offset: 0x0012A14C
		private void MarkChildScopesWithAtomicParent(IRIFDataScope scope)
		{
			foreach (IRIFDataScope irifdataScope in this.m_scopeTree.GetChildScopes(scope))
			{
				if (irifdataScope.DataScopeInfo.IsDecomposable)
				{
					irifdataScope.DataScopeInfo.IsDecomposable = false;
					this.MarkChildScopesWithAtomicParent(irifdataScope);
				}
			}
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x0012BFB8 File Offset: 0x0012A1B8
		internal bool HasMultiplePeerChildScopes(IRIFDataScope scope)
		{
			int[] array = new int[this.m_report.MappingDataSetIndexToDataSet.Count];
			foreach (IRIFDataScope irifdataScope in this.m_scopeTree.GetChildScopes(scope))
			{
				if (this.IsOrHasSecondGroupBoundToDataSet(irifdataScope, array))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x0012C02C File Offset: 0x0012A22C
		private bool IsOrHasSecondGroupBoundToDataSet(IRIFDataScope scope, int[] dataSetGroupBindingCounts)
		{
			if (!(scope is ReportHierarchyNode))
			{
				foreach (IRIFDataScope irifdataScope in this.m_scopeTree.GetChildScopes(scope))
				{
					if (this.IsOrHasSecondGroupBoundToDataSet(irifdataScope, dataSetGroupBindingCounts))
					{
						return true;
					}
				}
				return false;
			}
			DataSet dataSet = scope.DataScopeInfo.DataSet;
			if (dataSet == null)
			{
				return false;
			}
			int num = dataSetGroupBindingCounts[dataSet.IndexInCollection];
			num++;
			dataSetGroupBindingCounts[dataSet.IndexInCollection] = num;
			return num >= 2;
		}

		// Token: 0x060047B5 RID: 18357 RVA: 0x0012C0C4 File Offset: 0x0012A2C4
		internal bool EvaluateAtomicityCondition(bool isAtomic, IRIFDataScope scope, AtomicityReason reason)
		{
			if (isAtomic && this.m_publishingContext.TraceAtomicScopes)
			{
				Global.Tracer.Trace(TraceLevel.Verbose, "Report {0} contains a scope '{1}' which uses or contains {2}.  This may prevent optimizations from being applied to parent or child scopes.", new object[]
				{
					this.m_reportContext.ItemPathAsString,
					this.m_scopeTree.GetScopeName(scope),
					reason.ToString()
				});
			}
			return isAtomic;
		}

		// Token: 0x060047B6 RID: 18358 RVA: 0x0012C128 File Offset: 0x0012A328
		internal bool IsAncestor(ReportHierarchyNode child, string parentName)
		{
			ISortFilterScope sortFilterScope;
			if (!this.m_reportScopes.TryGetValue(parentName, out sortFilterScope))
			{
				return false;
			}
			IRIFDataScope irifdataScope;
			if (sortFilterScope is Grouping)
			{
				irifdataScope = ((Grouping)sortFilterScope).Owner;
			}
			else
			{
				irifdataScope = (IRIFDataScope)sortFilterScope;
			}
			return irifdataScope != child && this.m_scopeTree.IsSameOrParentScope(irifdataScope, child);
		}

		// Token: 0x060047B7 RID: 18359 RVA: 0x0012C178 File Offset: 0x0012A378
		internal DataRegion RegisterDataRegionCellScope(DataRegion dataRegion, bool forceRows, List<DataAggregateInfo> scopeAggregates, List<DataAggregateInfo> scopePostSortAggregates)
		{
			Global.Tracer.Assert(scopeAggregates != null, "(null != scopeAggregates)");
			Global.Tracer.Assert(scopePostSortAggregates != null, "(null != scopePostSortAggregates)");
			DataRegion dataRegion2 = null;
			if (this.m_groupingScopesForRunningValuesInTablix == null)
			{
				this.m_groupingScopesForRunningValuesInTablix = new InitializationContext.GroupingScopesForTablix(forceRows, this.m_objectType, dataRegion);
			}
			else
			{
				dataRegion.ScopeChainInfo = this.m_groupingScopesForRunningValuesInTablix.GetScopeChainInfo();
				dataRegion2 = this.m_groupingScopesForRunningValuesInTablix.SetContainerScope(dataRegion);
			}
			this.m_currentScope = new InitializationContext.ScopeInfo(this.m_currentScope == null || this.m_currentScope.AllowCustomAggregates, scopeAggregates, scopePostSortAggregates, dataRegion);
			return dataRegion2;
		}

		// Token: 0x060047B8 RID: 18360 RVA: 0x0012C20F File Offset: 0x0012A40F
		internal void UnRegisterTablixCellScope(DataRegion dataRegion)
		{
			Global.Tracer.Assert(this.m_groupingScopesForRunningValuesInTablix != null, "(null != m_groupingScopesForRunningValuesInTablix)");
			if (dataRegion == null)
			{
				this.m_groupingScopesForRunningValuesInTablix = null;
				return;
			}
			this.m_groupingScopesForRunningValuesInTablix.SetContainerScope(dataRegion);
		}

		// Token: 0x060047B9 RID: 18361 RVA: 0x0012C241 File Offset: 0x0012A441
		internal void ResetMemberAndCellIndexInCollectionTable()
		{
			this.m_indexInCollectionTable.Clear();
		}

		// Token: 0x060047BA RID: 18362 RVA: 0x0012C250 File Offset: 0x0012A450
		internal void SetIndexInCollection(IIndexedInCollection indexedInCollection)
		{
			IndexedInCollectionType indexedInCollectionType = indexedInCollection.IndexedInCollectionType;
			Dictionary<Hashtable, int> dictionary;
			if (indexedInCollectionType != IndexedInCollectionType.DataRegion)
			{
				if (indexedInCollectionType != IndexedInCollectionType.SubReport)
				{
					dictionary = this.m_indexInCollectionTable;
				}
				else
				{
					dictionary = this.m_indexInCollectionTableForSubReports;
				}
			}
			else
			{
				dictionary = this.m_indexInCollectionTableForDataRegions;
			}
			Hashtable hashtable = (Hashtable)this.m_groupingScopes.Clone();
			int num;
			if (dictionary.TryGetValue(hashtable, out num))
			{
				num++;
				dictionary[hashtable] = num;
			}
			else
			{
				num = 0;
				dictionary.Add(hashtable, num);
			}
			indexedInCollection.IndexInCollection = num;
		}

		// Token: 0x060047BB RID: 18363 RVA: 0x0012C2C1 File Offset: 0x0012A4C1
		internal void RegisterPageSectionScope(Page rifPage, List<DataAggregateInfo> scopeAggregates)
		{
			Global.Tracer.Assert(scopeAggregates != null, "(null != scopeAggregates)");
			this.m_currentScope = new InitializationContext.ScopeInfo(false, scopeAggregates, rifPage);
		}

		// Token: 0x060047BC RID: 18364 RVA: 0x0012C2E4 File Offset: 0x0012A4E4
		internal void UnRegisterPageSectionScope()
		{
			this.m_currentScope = null;
		}

		// Token: 0x060047BD RID: 18365 RVA: 0x0012C2ED File Offset: 0x0012A4ED
		internal void RegisterRunningValues(List<RunningValueInfo> runningValues, List<RunningValueInfo> runningValuesOfAggregates)
		{
			Global.Tracer.Assert(runningValues != null, "(runningValues != null)");
			Global.Tracer.Assert(runningValuesOfAggregates != null, "(runningValuesOfAggregates != null)");
			this.m_runningValues = runningValues;
			this.m_runningValuesOfAggregates = runningValuesOfAggregates;
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x0012C324 File Offset: 0x0012A524
		internal void RegisterRunningValues(string groupName, List<RunningValueInfo> runningValues, List<RunningValueInfo> runningValuesOfAggregates)
		{
			Global.Tracer.Assert(runningValues != null, "(runningValues != null)");
			Global.Tracer.Assert(runningValuesOfAggregates != null, "(runningValuesOfAggregates != null)");
			this.m_groupingScopesForRunningValues[groupName] = null;
			this.m_runningValues = runningValues;
			this.m_runningValuesOfAggregates = runningValuesOfAggregates;
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x0012C374 File Offset: 0x0012A574
		internal void UnRegisterRunningValues(List<RunningValueInfo> runningValues, List<RunningValueInfo> runningValuesOfAggregates)
		{
			Global.Tracer.Assert(runningValues != null, "(runningValues != null)");
			Global.Tracer.Assert(runningValuesOfAggregates != null, "(runningValuesOfAggregates != null)");
			Global.Tracer.Assert(this.m_runningValues == runningValues);
			Global.Tracer.Assert(this.m_runningValuesOfAggregates == runningValuesOfAggregates);
			this.m_runningValues = null;
			this.m_runningValuesOfAggregates = null;
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x0012C3DC File Offset: 0x0012A5DC
		internal void TransferGroupExpressionRowNumbers(List<RunningValueInfo> rowNumbers)
		{
			if (rowNumbers == null)
			{
				return;
			}
			for (int i = rowNumbers.Count - 1; i >= 0; i--)
			{
				Global.Tracer.Assert((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InGrouping) > (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0, "(0 != (m_location & LocationFlags.InGrouping))");
				RunningValueInfo runningValueInfo = rowNumbers[i];
				Global.Tracer.Assert(runningValueInfo != null, "(null != rowNumber)");
				string scope = runningValueInfo.Scope;
				bool flag = true;
				InitializationContext.ScopeInfo scopeInfo = null;
				if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDynamicTablixCell) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
				{
					flag = false;
				}
				else if (scope == null)
				{
					if (this.m_outerGroupName != null)
					{
						flag = false;
					}
					else
					{
						scopeInfo = this.m_outermostDataregionScope;
					}
				}
				else if (this.m_outerGroupName == scope)
				{
					Global.Tracer.Assert(this.m_outerGroupName != null, "(null != m_outerGroupName)");
					scopeInfo = (InitializationContext.ScopeInfo)this.m_groupingScopes[this.m_outerGroupName];
				}
				else if (this.m_currentDataRegionName == scope)
				{
					Global.Tracer.Assert(this.m_currentDataRegionName != null, "(null != m_currentDataRegionName)");
					scopeInfo = (InitializationContext.ScopeInfo)this.m_dataregionScopes[this.m_currentDataRegionName];
				}
				else
				{
					flag = false;
				}
				if (!flag)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidGroupExpressionScope, Severity.Error, this.m_objectType, this.m_objectName, "GroupExpression", Array.Empty<string>());
				}
				else if (scopeInfo != null)
				{
					Global.Tracer.Assert(scopeInfo.Aggregates != null, "(null != destinationScope.Aggregates)");
					scopeInfo.Aggregates.Add(runningValueInfo);
				}
				rowNumbers.RemoveAt(i);
			}
		}

		// Token: 0x060047C1 RID: 18369 RVA: 0x0012C555 File Offset: 0x0012A755
		internal void TransferRunningValues(List<RunningValueInfo> runningValues, string propertyName)
		{
			this.TransferRunningValues(runningValues, this.m_objectType, this.m_objectName, propertyName);
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x0012C56C File Offset: 0x0012A76C
		internal void TransferRunningValues(List<RunningValueInfo> runningValues, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			if (runningValues == null)
			{
				return;
			}
			if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				return;
			}
			for (int i = runningValues.Count - 1; i >= 0; i--)
			{
				RunningValueInfo runningValueInfo = runningValues[i];
				Global.Tracer.Assert(runningValueInfo != null, "(null != runningValue)");
				bool flag = runningValueInfo.AggregateType == DataAggregateInfo.AggregateTypes.Previous;
				string scope = runningValueInfo.Scope;
				bool flag2 = true;
				string text = null;
				InitializationContext.ScopeInfo scopeInfo = null;
				List<DataAggregateInfo> list = null;
				List<RunningValueInfo> list2 = null;
				if (scope == null)
				{
					if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
					{
						flag2 = false;
					}
					else if (flag && (this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegionCellTopLevelItem) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
					{
						if (runningValueInfo.HasDirectFieldReferences)
						{
							if (this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeIsDetail && this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeIsDetail)
							{
								text = this.GetDataSetName();
								list2 = this.GetActiveRunningValuesCollection(runningValueInfo.IsAggregateOfAggregate, flag);
								runningValueInfo.IsScopedInEvaluationScope = true;
							}
							else
							{
								flag2 = false;
							}
						}
						else
						{
							text = this.GetDataSetName();
							list2 = this.GetActiveRunningValuesCollection(runningValueInfo.IsAggregateOfAggregate, flag);
							if (this.m_groupingScopesForRunningValuesInTablix.IsRunningValueDirectionColumn && this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeName != null)
							{
								runningValueInfo.Scope = this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeName;
								if (this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeIsDetail)
								{
									runningValueInfo.IsScopedInEvaluationScope = true;
								}
							}
							else if (this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeName != null)
							{
								runningValueInfo.Scope = this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeName;
								if (this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeIsDetail)
								{
									runningValueInfo.IsScopedInEvaluationScope = true;
								}
							}
						}
					}
					else if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InGrouping) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 || (this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDetail) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
					{
						text = this.GetDataSetName();
						list2 = this.GetActiveRunningValuesCollection(runningValueInfo.IsAggregateOfAggregate, flag);
						if (flag)
						{
							runningValueInfo.Scope = this.m_currentGroupName;
							runningValueInfo.IsScopedInEvaluationScope = true;
						}
					}
					else
					{
						text = this.GetDataSetName();
						if (text != null)
						{
							scopeInfo = (InitializationContext.ScopeInfo)this.m_datasetScopes[text];
							Global.Tracer.Assert(scopeInfo != null, "(null != destinationScope)");
							list = scopeInfo.Aggregates;
						}
					}
				}
				else if (this.m_groupingScopesForRunningValuesInTablix != null && this.m_groupingScopesForRunningValuesInTablix.ContainsScope(scope, this.m_errorContext, true, this.m_groupingScopes))
				{
					text = this.GetDataSetName();
					list2 = this.GetActiveRunningValuesCollection(runningValueInfo.IsAggregateOfAggregate, flag);
					Global.Tracer.Assert((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InTablixCell) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 || (this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegionCellTopLevelItem) > (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0);
					if (flag)
					{
						if (runningValueInfo.HasDirectFieldReferences && (!this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeIsDetail || !this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeIsDetail))
						{
							flag2 = false;
						}
						else if (((InitializationContext.ScopeInfo)this.m_groupingScopes[scope]).GroupingScope.Owner.IsColumn)
						{
							if (this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeIsDetail && scope == this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeName)
							{
								runningValueInfo.IsScopedInEvaluationScope = true;
							}
						}
						else if (this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeIsDetail && scope == this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeName)
						{
							runningValueInfo.IsScopedInEvaluationScope = true;
						}
					}
				}
				else if (this.m_groupingScopesForRunningValues.ContainsKey(scope))
				{
					Global.Tracer.Assert((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InGrouping) > (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0, "(0 != (m_location & LocationFlags.InGrouping))");
					if (flag && runningValueInfo.HasDirectFieldReferences && this.m_groupingScopesForRunningValuesInTablix != null && (!this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeIsDetail || !this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeIsDetail))
					{
						flag2 = false;
					}
					else if (this.m_groupingScopesForRunningValuesInTablix == null || this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeIsDetail || this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeIsDetail)
					{
						text = this.GetDataSetName();
						list2 = this.GetActiveRunningValuesCollection(runningValueInfo.IsAggregateOfAggregate, flag);
					}
					else
					{
						flag2 = false;
					}
				}
				else if (this.m_dataregionScopesForRunningValues.ContainsKey(scope))
				{
					Global.Tracer.Assert((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion) > (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0, "(0 != (m_location & LocationFlags.InDataRegion))");
					runningValueInfo.Scope = (string)this.m_dataregionScopesForRunningValues[scope];
					if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InGrouping) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 || (this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDetail) > (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 || flag)
					{
						text = this.GetDataSetName();
						list2 = this.GetActiveRunningValuesCollection(runningValueInfo.IsAggregateOfAggregate, flag);
						if (flag && runningValueInfo.Scope == null)
						{
							runningValueInfo = null;
							list2 = null;
						}
					}
					else
					{
						text = this.GetDataSetName();
						if (text != null)
						{
							scopeInfo = (InitializationContext.ScopeInfo)this.m_datasetScopes[text];
							Global.Tracer.Assert(scopeInfo != null, "(null != destinationScope)");
							list = scopeInfo.Aggregates;
						}
					}
				}
				else if (this.m_datasetScopes.ContainsKey(scope))
				{
					if (flag)
					{
						runningValueInfo = null;
						list2 = null;
					}
					else if (((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InGrouping) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 || (this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDetail) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0) && scope == this.GetDataSetName())
					{
						if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InTablixCell) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 && this.IsDataRegionCellScope)
						{
							flag2 = false;
						}
						else
						{
							text = scope;
							runningValueInfo.Scope = null;
							list2 = this.GetActiveRunningValuesCollection(runningValueInfo.IsAggregateOfAggregate, flag);
							runningValueInfo.IsScopedInEvaluationScope = true;
						}
					}
					else
					{
						text = scope;
						scopeInfo = (InitializationContext.ScopeInfo)this.m_datasetScopes[scope];
						Global.Tracer.Assert(scopeInfo != null, "(null != destinationScope)");
						list = scopeInfo.Aggregates;
					}
				}
				else
				{
					flag2 = false;
				}
				if (!flag2)
				{
					if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDynamicTablixCell) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
					{
						if (DataAggregateInfo.AggregateTypes.Previous == runningValueInfo.AggregateType)
						{
							this.m_errorContext.Register(ProcessingErrorCode.rsInvalidPreviousAggregateInTablixCell, Severity.Error, objectType, objectName, propertyName, new string[] { this.m_tablixName });
						}
						else
						{
							this.m_errorContext.Register(ProcessingErrorCode.rsInvalidScopeInTablix, Severity.Error, objectType, objectName, propertyName, new string[] { this.m_tablixName });
						}
					}
					else
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidAggregateScope, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
					}
				}
				else if (runningValueInfo != null)
				{
					if (scopeInfo == null)
					{
						scopeInfo = (InitializationContext.ScopeInfo)this.m_datasetScopes[this.GetCurrentDataSetName()];
					}
					runningValueInfo.DataSetIndexInCollection = scopeInfo.DataSetScope.IndexInCollection;
					this.RegisterDataSetLevelAggregateOrLookup(runningValueInfo.DataSetIndexInCollection);
					runningValueInfo.Initialize(this, text, objectType, objectName, propertyName);
					if (list != null)
					{
						list.Add(runningValueInfo);
					}
					else if (list2 != null)
					{
						Global.Tracer.Assert(runningValues != list2);
						if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 && !this.m_isDataRegionScopedCell)
						{
							string text2 = "";
							if (this.m_axisGroupingScopesForRunningValues.InCurrentDataRegionDynamicRow)
							{
								text2 = this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeName;
							}
							if (this.m_axisGroupingScopesForRunningValues.InCurrentDataRegionDynamicColumn)
							{
								text2 = text2 + "." + this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeName;
							}
							runningValueInfo.EvaluationScopeName = text2;
						}
						else if (this.m_currentScope.DataRegionScope != null)
						{
							runningValueInfo.EvaluationScopeName = this.m_currentScope.DataRegionScope.Name;
						}
						list2.Add(runningValueInfo);
						if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 && (!flag || runningValueInfo.HasDirectFieldReferences) && (this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 && !this.m_isDataRegionScopedCell)
						{
							if (this.m_axisGroupingScopesForRunningValues.InCurrentDataRegionDynamicRow)
							{
								((InitializationContext.ScopeInfo)this.m_groupingScopes[this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeName]).ReportScope.NeedToCacheDataRows = true;
							}
							if (this.m_axisGroupingScopesForRunningValues.InCurrentDataRegionDynamicColumn)
							{
								((InitializationContext.ScopeInfo)this.m_groupingScopes[this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeName]).ReportScope.NeedToCacheDataRows = true;
							}
							if (this.m_currentScope.ReportScope != null)
							{
								this.m_currentScope.ReportScope.NeedToCacheDataRows = true;
							}
						}
					}
					this.StoreAggregateScopeAndLocationInfo(runningValueInfo, this.m_currentScope, objectType, objectName, propertyName);
				}
				runningValues.RemoveAt(i);
			}
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x0012CD3C File Offset: 0x0012AF3C
		private List<RunningValueInfo> GetActiveRunningValuesCollection(bool isAggregateOfAggregates, bool isPrevious)
		{
			if (!isPrevious && isAggregateOfAggregates)
			{
				return this.m_runningValuesOfAggregates;
			}
			return this.m_runningValues;
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x0012CD53 File Offset: 0x0012AF53
		private void StoreAggregateScopeAndLocationInfo(DataAggregateInfo aggregate, InitializationContext.ScopeInfo destinationScope, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			if (destinationScope != null)
			{
				aggregate.EvaluationScope = destinationScope.DataScope;
			}
			aggregate.PublishingInfo.ObjectType = objectType;
			aggregate.PublishingInfo.ObjectName = objectName;
			aggregate.PublishingInfo.PropertyName = propertyName;
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x0012CD8A File Offset: 0x0012AF8A
		internal void SpecialTransferRunningValues(List<RunningValueInfo> runningValues, List<RunningValueInfo> runningValuesOfAggregates)
		{
			this.TransferRunningValueCollection(runningValues, this.m_runningValues);
			this.TransferRunningValueCollection(runningValuesOfAggregates, this.m_runningValuesOfAggregates);
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x0012CDA8 File Offset: 0x0012AFA8
		private void TransferRunningValueCollection(List<RunningValueInfo> runningValues, List<RunningValueInfo> destRunningValues)
		{
			if (runningValues == null)
			{
				return;
			}
			Global.Tracer.Assert(destRunningValues != null, "(null != m_runningValues)");
			for (int i = runningValues.Count - 1; i >= 0; i--)
			{
				Global.Tracer.Assert(runningValues != destRunningValues);
				destRunningValues.Add(runningValues[i]);
				runningValues.RemoveAt(i);
			}
		}

		// Token: 0x060047C7 RID: 18375 RVA: 0x0012CE04 File Offset: 0x0012B004
		internal void TransferLookups(List<LookupInfo> lookups, string propertyName)
		{
			this.TransferLookups(lookups, this.m_objectType, this.m_objectName, propertyName, this.GetDataSetName());
		}

		// Token: 0x060047C8 RID: 18376 RVA: 0x0012CE20 File Offset: 0x0012B020
		internal void TransferLookups(List<LookupInfo> lookups, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, string dataSetName)
		{
			if (lookups == null)
			{
				return;
			}
			for (int i = lookups.Count - 1; i >= 0; i--)
			{
				LookupInfo lookupInfo = lookups[i];
				Global.Tracer.Assert(lookupInfo != null, "(null != lookup)");
				LookupDestinationInfo destinationInfo = lookupInfo.DestinationInfo;
				Global.Tracer.Assert(destinationInfo != null, "(null != destinationInfo)");
				string scope = destinationInfo.Scope;
				if (string.IsNullOrEmpty(scope) || !this.m_datasetScopes.ContainsKey(scope))
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidLookupScope, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
				}
				else
				{
					lookupInfo.Initialize(this, scope, objectType, objectName, propertyName);
					DataSet dataSetScope = ((InitializationContext.ScopeInfo)this.m_datasetScopes[scope]).DataSetScope;
					InitializationContext.LookupDestinationCompactionTable lookupDestinationCompactionTable;
					if (!this.m_lookupCompactionTable.TryGetValue(scope, out lookupDestinationCompactionTable))
					{
						lookupDestinationCompactionTable = new InitializationContext.LookupDestinationCompactionTable();
						this.m_lookupCompactionTable[scope] = lookupDestinationCompactionTable;
					}
					destinationInfo.UsedInSameDataSetTablixProcessing = string.Equals(scope, dataSetName, StringComparison.Ordinal);
					string originalText = destinationInfo.DestinationExpr.OriginalText;
					int count;
					if (!lookupDestinationCompactionTable.TryGetValue(originalText, out count))
					{
						if (dataSetScope.LookupDestinationInfos == null)
						{
							dataSetScope.LookupDestinationInfos = new List<LookupDestinationInfo>();
						}
						destinationInfo.Initialize(this, scope, objectType, objectName, propertyName);
						count = dataSetScope.LookupDestinationInfos.Count;
						destinationInfo.IndexInCollection = count;
						dataSetScope.LookupDestinationInfos.Add(destinationInfo);
						lookupDestinationCompactionTable[originalText] = count;
					}
					else
					{
						LookupDestinationInfo lookupDestinationInfo = dataSetScope.LookupDestinationInfos[count];
						lookupDestinationInfo.IsMultiValue |= destinationInfo.IsMultiValue;
						lookupDestinationInfo.UsedInSameDataSetTablixProcessing |= destinationInfo.UsedInSameDataSetTablixProcessing;
					}
					lookupInfo.DestinationIndexInCollection = count;
					lookupInfo.DestinationInfo = null;
					lookupInfo.DataSetIndexInCollection = dataSetScope.IndexInCollection;
					if (dataSetScope.Lookups == null)
					{
						dataSetScope.Lookups = new List<LookupInfo>();
					}
					dataSetScope.Lookups.Add(lookupInfo);
					this.RegisterDataSetLevelAggregateOrLookup(dataSetScope.IndexInCollection);
				}
				lookups.RemoveAt(i);
			}
		}

		// Token: 0x060047C9 RID: 18377 RVA: 0x0012D00E File Offset: 0x0012B20E
		internal void TransferAggregates(List<DataAggregateInfo> aggregates, string propertyName)
		{
			this.TransferAggregates(aggregates, this.m_objectType, this.m_objectName, propertyName, false);
		}

		// Token: 0x060047CA RID: 18378 RVA: 0x0012D025 File Offset: 0x0012B225
		internal void TransferNestedAggregates(List<DataAggregateInfo> aggregates, string propertyName)
		{
			this.TransferAggregates(aggregates, this.m_objectType, this.m_objectName, propertyName, true);
		}

		// Token: 0x060047CB RID: 18379 RVA: 0x0012D03C File Offset: 0x0012B23C
		private void TransferAggregates(List<DataAggregateInfo> aggregates, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, bool isInAggregate)
		{
			if (aggregates == null)
			{
				return;
			}
			for (int i = aggregates.Count - 1; i >= 0; i--)
			{
				DataAggregateInfo dataAggregateInfo = aggregates[i];
				Global.Tracer.Assert(dataAggregateInfo != null, "(null != aggregate)");
				if (this.m_hasFilters && DataAggregateInfo.AggregateTypes.Aggregate == dataAggregateInfo.AggregateType)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsCustomAggregateAndFilter, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
				}
				string text;
				bool scope = dataAggregateInfo.GetScope(out text);
				bool flag = true;
				string text2 = null;
				InitializationContext.ScopeInfo scopeInfo = null;
				bool flag2 = false;
				if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 && this.m_numberOfDataSets == 0)
				{
					flag = false;
					flag2 = true;
				}
				else if (!scope)
				{
					text2 = this.GetDataSetName();
					if (Microsoft.ReportingServices.ReportPublishing.LocationFlags.None == this.m_location)
					{
						if (1 != this.m_numberOfDataSets)
						{
							flag = false;
							this.m_errorContext.Register(ProcessingErrorCode.rsMissingAggregateScope, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
						}
						else if (text2 != null)
						{
							scopeInfo = (InitializationContext.ScopeInfo)this.m_datasetScopes[text2];
						}
					}
					else if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
					{
						if (dataAggregateInfo.Expressions != null && dataAggregateInfo.Expressions.Length != 0)
						{
							ExpressionInfo expressionInfo = dataAggregateInfo.Expressions[0];
							Global.Tracer.Assert(expressionInfo != null, "(null != paramExpr)");
							if (expressionInfo.HasAnyFieldReferences)
							{
								flag = false;
								this.m_errorContext.Register(ProcessingErrorCode.rsMissingAggregateScopeInPageSection, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
							}
							else
							{
								scopeInfo = this.m_currentScope;
							}
						}
					}
					else
					{
						Global.Tracer.Assert((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataSet) > (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0);
						scopeInfo = this.m_currentScope;
					}
					if (scopeInfo != null && scopeInfo.DataSetScope != null)
					{
						scopeInfo.DataSetScope.UsedInAggregates = true;
						dataAggregateInfo.DataSetIndexInCollection = scopeInfo.DataSetScope.IndexInCollection;
						this.RegisterDataSetLevelAggregateOrLookup(dataAggregateInfo.DataSetIndexInCollection);
					}
				}
				else if (text == null)
				{
					flag = false;
				}
				else if (this.m_groupingScopes.ContainsKey(text))
				{
					Global.Tracer.Assert((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InGrouping) > (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0, "(0 != (m_location & LocationFlags.InGrouping))");
					text2 = this.GetDataSetName();
					scopeInfo = (InitializationContext.ScopeInfo)this.m_groupingScopes[text];
				}
				else if (this.m_dataregionScopes.ContainsKey(text))
				{
					Global.Tracer.Assert((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion) > (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0, "(0 != (m_location & LocationFlags.InDataRegion))");
					text2 = this.GetDataSetName();
					scopeInfo = (InitializationContext.ScopeInfo)this.m_dataregionScopes[text];
				}
				else if (this.m_datasetScopes.ContainsKey(text))
				{
					if (isInAggregate)
					{
						flag = false;
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidNestedDataSetAggregate, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
					}
					else if (dataAggregateInfo.IsAggregateOfAggregate)
					{
						flag = false;
						this.m_errorContext.Register(ProcessingErrorCode.rsDataSetAggregateOfAggregates, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
					}
					if (flag && (this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 && dataAggregateInfo.Expressions != null && dataAggregateInfo.Expressions.Length != 0)
					{
						ExpressionInfo expressionInfo2 = dataAggregateInfo.Expressions[0];
						Global.Tracer.Assert(expressionInfo2 != null, "(null != paramExpr)");
						List<string> referencedReportItems = expressionInfo2.ReferencedReportItems;
						if (referencedReportItems != null && referencedReportItems.Count > 0)
						{
							flag = false;
							this.m_errorContext.Register(ProcessingErrorCode.rsReportItemInScopedAggregate, Severity.Error, objectType, objectName, propertyName, new string[] { referencedReportItems[0] });
						}
						else if (expressionInfo2.ReferencedOverallPageGlobals)
						{
							flag = false;
							this.m_errorContext.Register(ProcessingErrorCode.rsOverallPageNumberInScopedAggregate, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
						}
						else if (expressionInfo2.ReferencedPageGlobals)
						{
							flag = false;
							this.m_errorContext.Register(ProcessingErrorCode.rsPageNumberInScopedAggregate, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
						}
					}
					if (flag)
					{
						text2 = text;
						scopeInfo = (InitializationContext.ScopeInfo)this.m_datasetScopes[text];
						if (!scopeInfo.IsDuplicateScope)
						{
							scopeInfo.DataSetScope.UsedInAggregates = true;
							dataAggregateInfo.DataSetIndexInCollection = scopeInfo.DataSetScope.IndexInCollection;
							this.RegisterDataSetLevelAggregateOrLookup(dataAggregateInfo.DataSetIndexInCollection);
						}
					}
				}
				else if (isInAggregate)
				{
					ISortFilterScope sortFilterScope;
					if (!this.m_reportScopes.TryGetValue(text, out sortFilterScope))
					{
						flag = false;
						flag2 = true;
					}
					else if (sortFilterScope is Grouping)
					{
						ReportHierarchyNode owner = ((Grouping)sortFilterScope).Owner;
						text2 = this.GetDataSetName();
						scopeInfo = this.CreateScopeInfo(owner, false);
					}
					else if (sortFilterScope is DataRegion)
					{
						text2 = this.GetDataSetName();
						scopeInfo = this.CreateScopeInfo((DataRegion)sortFilterScope, false);
					}
					else
					{
						flag = false;
						flag2 = true;
					}
				}
				else
				{
					flag = false;
					flag2 = true;
				}
				if (flag2)
				{
					ProcessingErrorCode processingErrorCode;
					if (isInAggregate)
					{
						processingErrorCode = ProcessingErrorCode.rsInvalidNestedAggregateScope;
					}
					else
					{
						processingErrorCode = ProcessingErrorCode.rsInvalidAggregateScope;
					}
					this.m_errorContext.Register(processingErrorCode, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
				}
				if (flag && scopeInfo != null)
				{
					if (scopeInfo.DataSetScope == null && dataAggregateInfo.DataSetIndexInCollection < 0)
					{
						dataAggregateInfo.DataSetIndexInCollection = this.GetCurrentDataSetIndex();
					}
					if (DataAggregateInfo.AggregateTypes.Aggregate == dataAggregateInfo.AggregateType && !scopeInfo.AllowCustomAggregates)
					{
						ProcessingErrorCode processingErrorCode2;
						if (isInAggregate)
						{
							processingErrorCode2 = ProcessingErrorCode.rsNestedCustomAggregate;
						}
						else
						{
							processingErrorCode2 = ProcessingErrorCode.rsInvalidCustomAggregateScope;
						}
						this.m_errorContext.Register(processingErrorCode2, Severity.Error, objectType, objectName, propertyName, Array.Empty<string>());
					}
					if (dataAggregateInfo.Expressions != null)
					{
						for (int j = 0; j < dataAggregateInfo.Expressions.Length; j++)
						{
							Global.Tracer.Assert(dataAggregateInfo.Expressions[j] != null, "(null != aggregate.Expressions[j])");
							dataAggregateInfo.Expressions[j].AggregateInitialize(text2, objectType, objectName, propertyName, this);
						}
					}
					if (DataAggregateInfo.AggregateTypes.Aggregate == dataAggregateInfo.AggregateType)
					{
						DataSet dataSet = this.GetDataSet(text2);
						if (dataSet != null)
						{
							dataSet.HasScopeWithCustomAggregates = true;
							if (dataSet.InterpretSubtotalsAsDetails == DataSet.TriState.Auto)
							{
								dataSet.InterpretSubtotalsAsDetails = DataSet.TriState.False;
							}
						}
					}
					List<DataAggregateInfo> list;
					if (dataAggregateInfo.Recursive)
					{
						if (scopeInfo.GroupingScope == null || scopeInfo.GroupingScope.Parent == null)
						{
							list = scopeInfo.Aggregates;
						}
						else
						{
							list = scopeInfo.RecursiveAggregates;
						}
					}
					else if (dataAggregateInfo.IsAggregateOfAggregate && scopeInfo.DataScopeInfo != null)
					{
						AggregateBucket<DataAggregateInfo> aggregateBucket;
						if (dataAggregateInfo.IsPostSortAggregate())
						{
							aggregateBucket = scopeInfo.DataScopeInfo.PostSortAggregatesOfAggregates.GetOrCreateBucket(0);
						}
						else
						{
							aggregateBucket = scopeInfo.DataScopeInfo.AggregatesOfAggregates.GetOrCreateBucket(dataAggregateInfo.PublishingInfo.AggregateOfAggregatesLevel);
						}
						list = aggregateBucket.Aggregates;
					}
					else if (scopeInfo.PostSortAggregates != null && dataAggregateInfo.IsPostSortAggregate())
					{
						list = scopeInfo.PostSortAggregates;
						if (scopeInfo.ReportScope != null)
						{
							scopeInfo.ReportScope.NeedToCacheDataRows = true;
						}
						if (this.m_groupingScopesForRunningValuesInTablix != null)
						{
							if (this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeName != null)
							{
								((InitializationContext.ScopeInfo)this.m_groupingScopes[this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeName]).ReportScope.NeedToCacheDataRows = true;
							}
							if (this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeName != null)
							{
								((InitializationContext.ScopeInfo)this.m_groupingScopes[this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeName]).ReportScope.NeedToCacheDataRows = true;
							}
						}
					}
					else
					{
						list = scopeInfo.Aggregates;
					}
					Global.Tracer.Assert(list != null, "(null != destinationAggregates)");
					Global.Tracer.Assert(aggregates != list);
					if (!scope && (this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
					{
						if (!this.m_isDataRegionScopedCell)
						{
							string text3 = "";
							if (this.m_axisGroupingScopesForRunningValues.InCurrentDataRegionDynamicRow)
							{
								text3 = this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeName;
							}
							if (this.m_axisGroupingScopesForRunningValues.InCurrentDataRegionDynamicColumn)
							{
								text3 = text3 + "." + this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeName;
							}
							dataAggregateInfo.EvaluationScopeName = text3;
						}
						else if (this.m_currentScope.DataRegionScope != null)
						{
							dataAggregateInfo.EvaluationScopeName = this.m_currentScope.DataRegionScope.Name;
						}
					}
					list.Add(dataAggregateInfo);
					this.StoreAggregateScopeAndLocationInfo(dataAggregateInfo, scopeInfo, objectType, objectName, propertyName);
				}
				aggregates.RemoveAt(i);
			}
		}

		// Token: 0x060047CC RID: 18380 RVA: 0x0012D7DD File Offset: 0x0012B9DD
		internal void RegisterReportSection(ReportSection sectionDef)
		{
			this.m_currentScope = new InitializationContext.ScopeInfo(false, null, sectionDef);
			this.m_reportItemsInSection = new Dictionary<string, ReportItem>();
			this.m_referencableTextboxesInSection = new byte[this.m_referencableTextboxesInSection.Length];
		}

		// Token: 0x060047CD RID: 18381 RVA: 0x0012D80B File Offset: 0x0012BA0B
		internal void UnRegisterReportSection()
		{
			this.m_currentScope = null;
			this.m_reportItemsInSection = null;
		}

		// Token: 0x060047CE RID: 18382 RVA: 0x0012D81C File Offset: 0x0012BA1C
		internal void InitializeParameters(List<ParameterDef> parameters, List<DataSet> dataSetList)
		{
			if (this.m_dynamicParameters == null || this.m_dynamicParameters.Count == 0)
			{
				return;
			}
			Hashtable hashtable = new Hashtable();
			int i = 0;
			for (int j = 0; j < this.m_dynamicParameters.Count; j++)
			{
				Microsoft.ReportingServices.ReportPublishing.DynamicParameter dynamicParameter = (Microsoft.ReportingServices.ReportPublishing.DynamicParameter)this.m_dynamicParameters[j];
				while (i < dynamicParameter.Index)
				{
					hashtable.Add(parameters[i].Name, i);
					i++;
				}
				this.InitializeParameter(parameters[dynamicParameter.Index], dynamicParameter, hashtable, dataSetList);
			}
		}

		// Token: 0x060047CF RID: 18383 RVA: 0x0012D8B0 File Offset: 0x0012BAB0
		private void InitializeParameter(ParameterDef parameter, Microsoft.ReportingServices.ReportPublishing.DynamicParameter dynamicParameter, Hashtable dependencies, List<DataSet> dataSetList)
		{
			Global.Tracer.Assert(dynamicParameter != null, "(null != dynamicParameter)");
			bool isComplex = dynamicParameter.IsComplex;
			Microsoft.ReportingServices.ReportPublishing.DataSetReference dataSetReference = dynamicParameter.ValidValueDataSet;
			if (dataSetReference != null)
			{
				this.InitializeParameterDataSource(parameter, dataSetReference, false, dependencies, ref isComplex, dataSetList);
			}
			dataSetReference = dynamicParameter.DefaultDataSet;
			if (dataSetReference != null)
			{
				this.InitializeParameterDataSource(parameter, dataSetReference, true, dependencies, ref isComplex, dataSetList);
			}
		}

		// Token: 0x060047D0 RID: 18384 RVA: 0x0012D90C File Offset: 0x0012BB0C
		private void InitializeParameterDataSource(ParameterDef parameter, Microsoft.ReportingServices.ReportPublishing.DataSetReference dataSetRef, bool isDefault, Hashtable dependencies, ref bool isComplex, List<DataSet> dataSetList)
		{
			ParameterDataSource parameterDataSource = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			PublishingDataSetInfo publishingDataSetInfo = (PublishingDataSetInfo)this.m_dataSetQueryInfo[dataSetRef.DataSet];
			if (publishingDataSetInfo == null)
			{
				if (isDefault)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidDefaultValueDataSetReference, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter, parameter.Name, "DataSetReference", new string[] { dataSetRef.DataSet });
				}
				else
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidValidValuesDataSetReference, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter, parameter.Name, "DataSetReference", new string[] { dataSetRef.DataSet });
				}
			}
			else
			{
				DataSet dataSet = dataSetList[publishingDataSetInfo.DataSetDefIndex];
				if (!dataSet.UsedInAggregates && !dataSet.HasLookups && !dataSet.UsedOnlyInParametersSet)
				{
					List<DataRegion> list;
					this.m_dataSetNameToDataRegionsMap.TryGetValue(dataSetRef.DataSet, out list);
					if (list == null || list.Count == 0)
					{
						dataSet.UsedOnlyInParameters = true;
					}
				}
				parameterDataSource = new ParameterDataSource(publishingDataSetInfo.DataSourceIndex, publishingDataSetInfo.DataSetIndex);
				Hashtable hashtable = (Hashtable)this.m_fieldNameMap[dataSetRef.DataSet];
				if (hashtable != null)
				{
					if (hashtable.ContainsKey(dataSetRef.ValueAlias))
					{
						parameterDataSource.ValueFieldIndex = (int)hashtable[dataSetRef.ValueAlias];
						if (parameterDataSource.ValueFieldIndex >= publishingDataSetInfo.CalculatedFieldIndex)
						{
							flag3 = true;
						}
						flag = true;
					}
					if (dataSetRef.LabelAlias != null)
					{
						if (hashtable.ContainsKey(dataSetRef.LabelAlias))
						{
							parameterDataSource.LabelFieldIndex = (int)hashtable[dataSetRef.LabelAlias];
							if (parameterDataSource.LabelFieldIndex >= publishingDataSetInfo.CalculatedFieldIndex)
							{
								flag3 = true;
							}
							flag2 = true;
						}
					}
					else
					{
						flag2 = true;
					}
				}
				else if (dataSetRef.LabelAlias == null)
				{
					flag2 = true;
				}
				if (!flag)
				{
					this.ErrorContext.Register(ProcessingErrorCode.rsInvalidDataSetReferenceField, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter, parameter.Name, "Field", new string[] { dataSetRef.ValueAlias, dataSetRef.DataSet });
				}
				if (!flag2)
				{
					this.ErrorContext.Register(ProcessingErrorCode.rsInvalidDataSetReferenceField, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter, parameter.Name, "Field", new string[] { dataSetRef.LabelAlias, dataSetRef.DataSet });
				}
				if (!isComplex)
				{
					if (publishingDataSetInfo.IsComplex || flag3)
					{
						isComplex = true;
						parameter.Dependencies = (Hashtable)dependencies.Clone();
					}
					else if (publishingDataSetInfo.ParameterNames != null && publishingDataSetInfo.ParameterNames.Count != 0)
					{
						Hashtable hashtable2 = parameter.Dependencies;
						if (hashtable2 == null)
						{
							hashtable2 = new Hashtable();
							parameter.Dependencies = hashtable2;
						}
						foreach (string text in publishingDataSetInfo.ParameterNames.Keys)
						{
							if (dependencies.ContainsKey(text))
							{
								if (!hashtable2.ContainsKey(text))
								{
									hashtable2.Add(text, dependencies[text]);
								}
							}
							else
							{
								this.ErrorContext.Register(ProcessingErrorCode.rsInvalidReportParameterDependency, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter, parameter.Name, "DataSetReference", new string[] { text });
							}
						}
					}
				}
			}
			if (isDefault)
			{
				parameter.DefaultDataSource = parameterDataSource;
				return;
			}
			parameter.ValidValuesDataSource = parameterDataSource;
		}

		// Token: 0x060047D1 RID: 18385 RVA: 0x0012DC30 File Offset: 0x0012BE30
		internal bool ValidateSliderLabelData(Tablix tablix, LabelData labelData)
		{
			if (labelData == null)
			{
				return true;
			}
			DataSet dataSet;
			if (labelData.DataSetName == null)
			{
				dataSet = this.GetDataSet();
			}
			else
			{
				dataSet = this.GetDataSet(labelData.DataSetName);
			}
			if (dataSet != null)
			{
				labelData.DataSetName = dataSet.Name;
				bool flag = true;
				if (labelData.Label != null)
				{
					flag &= this.ValidateSliderDataFieldReference(labelData.Label, "Label", tablix.ObjectType, tablix.Name, dataSet);
				}
				if (labelData.KeyFields != null)
				{
					foreach (string text in labelData.KeyFields)
					{
						flag &= this.ValidateSliderDataFieldReference(text, "Key", tablix.ObjectType, tablix.Name, dataSet);
					}
				}
				return flag;
			}
			this.ErrorContext.Register(ProcessingErrorCode.rsInvalidSliderDataSetReference, Severity.Error, tablix.ObjectType, tablix.Name, null, new string[] { labelData.DataSetName });
			return false;
		}

		// Token: 0x060047D2 RID: 18386 RVA: 0x0012DD34 File Offset: 0x0012BF34
		private bool ValidateSliderDataFieldReference(string fieldName, string propertyName, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, DataSet dataSet)
		{
			Hashtable hashtable = (Hashtable)this.m_fieldNameMap[dataSet.Name];
			if (hashtable == null || !hashtable.ContainsKey(fieldName))
			{
				this.ErrorContext.Register(ProcessingErrorCode.rsInvalidSliderDataSetReferenceField, Severity.Error, objectType, objectName, propertyName, new string[] { fieldName, dataSet.Name });
				return false;
			}
			return true;
		}

		// Token: 0x060047D3 RID: 18387 RVA: 0x0012DD94 File Offset: 0x0012BF94
		internal void RegisterDataSetLevelAggregateOrLookup(int referencedDataSetIndex)
		{
			if (this.GetCurrentDataSetIndex() == referencedDataSetIndex)
			{
				return;
			}
			if (-1 == this.GetCurrentDataSetIndex())
			{
				this.m_report.SetDatasetDependency(referencedDataSetIndex, referencedDataSetIndex, false);
				return;
			}
			DataSet dataSet = this.GetDataSet();
			if (dataSet != null && dataSet.DataSource != null)
			{
				this.m_report.SetDatasetDependency(this.GetCurrentDataSetIndex(), referencedDataSetIndex, false);
			}
		}

		// Token: 0x060047D4 RID: 18388 RVA: 0x0012DDE8 File Offset: 0x0012BFE8
		internal DataSet GetParentDataSet()
		{
			return this.m_activeDataSets.First;
		}

		// Token: 0x060047D5 RID: 18389 RVA: 0x0012DDF8 File Offset: 0x0012BFF8
		internal bool RegisterDataRegion(DataRegion dataRegion)
		{
			DataSet first = this.m_activeDataSets.First;
			DataSet dataSet = dataRegion.DataScopeInfo.DataSet;
			if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				this.m_dataSetsForIdcInNestedDR.Clear();
				if (!this.m_initializingUserSorts)
				{
					if (this.m_report.TopLevelDataRegions == null)
					{
						this.m_report.TopLevelDataRegions = new List<DataRegion>();
					}
					this.m_report.TopLevelDataRegions.Add(dataRegion);
				}
				if (dataRegion.DataScopeInfo.DataSet == null)
				{
					return false;
				}
				if (dataSet != null && !this.m_initializingUserSorts)
				{
					List<DataRegion> list;
					this.m_dataSetNameToDataRegionsMap.TryGetValue(dataSet.Name, out list);
					Global.Tracer.Assert(list != null, "(null != dataRegions)");
					list.Add(dataRegion);
				}
			}
			this.RegisterDataSet(dataSet);
			this.RegisterDataRegionScope(dataRegion);
			return true;
		}

		// Token: 0x060047D6 RID: 18390 RVA: 0x0012DEC4 File Offset: 0x0012C0C4
		internal void UnRegisterDataRegion(DataRegion dataRegion)
		{
			DataSet dataSet = dataRegion.DataScopeInfo.DataSet;
			this.UnRegisterDataSet(dataSet);
			this.UnRegisterDataRegionScope(dataRegion);
		}

		// Token: 0x060047D7 RID: 18391 RVA: 0x0012DEEC File Offset: 0x0012C0EC
		internal void RegisterDataSet(DataSet dataSet)
		{
			if (dataSet == null)
			{
				return;
			}
			DataSet first = this.m_activeDataSets.First;
			this.m_activeDataSets = this.m_activeDataSets.Add(dataSet);
			this.m_activeScopeInfos = this.m_activeScopeInfos.Add(this.m_currentScope);
			if (first != dataSet)
			{
				InitializationContext.ScopeInfo scopeInfo = (InitializationContext.ScopeInfo)this.m_datasetScopes[dataSet.Name];
				Global.Tracer.Assert(scopeInfo != null, "(null != dataSetScope)");
				this.RegisterDataSetScope(dataSet, scopeInfo.Aggregates, scopeInfo.PostSortAggregates, dataSet.IndexInCollection);
			}
		}

		// Token: 0x060047D8 RID: 18392 RVA: 0x0012DF7C File Offset: 0x0012C17C
		internal void UnRegisterDataSet(DataSet dataSet)
		{
			if (dataSet == null)
			{
				return;
			}
			DataSet first = this.m_activeDataSets.First;
			this.m_activeDataSets = this.m_activeDataSets.Rest;
			DataSet first2 = this.m_activeDataSets.First;
			if (first != first2)
			{
				this.UnRegisterDataSetScope(dataSet.Name);
			}
			this.m_currentScope = this.m_activeScopeInfos.First;
			this.m_activeScopeInfos = this.m_activeScopeInfos.Rest;
		}

		// Token: 0x060047D9 RID: 18393 RVA: 0x0012DFE8 File Offset: 0x0012C1E8
		private string GetDataSetName()
		{
			if (this.m_numberOfDataSets == 0)
			{
				return null;
			}
			if (1 == this.m_numberOfDataSets)
			{
				Global.Tracer.Assert(this.m_oneDataSetName != null);
				return this.m_oneDataSetName;
			}
			Global.Tracer.Assert(1 < this.m_numberOfDataSets);
			return this.GetCurrentDataSetName();
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x0012E03C File Offset: 0x0012C23C
		private DataSet GetDataSet()
		{
			string dataSetName = this.GetDataSetName();
			return this.GetDataSet(dataSetName);
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x0012E058 File Offset: 0x0012C258
		private DataSet GetDataSet(string dataSetName)
		{
			DataSet dataSet = null;
			if (this.m_numberOfDataSets > 0)
			{
				Global.Tracer.Assert(dataSetName != null, "DataSet name must not be null");
				if (!this.m_reportScopes.ContainsKey(dataSetName))
				{
					return null;
				}
				dataSet = this.m_reportScopes[dataSetName] as DataSet;
				Global.Tracer.Assert(dataSet != null, "DataSet {0} not found", new object[] { dataSetName });
			}
			return dataSet;
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x0012E0C4 File Offset: 0x0012C2C4
		internal void SetDataSetHasSubReports()
		{
			DataSet dataSet = this.GetDataSet();
			if (dataSet != null)
			{
				dataSet.HasSubReports = true;
			}
		}

		// Token: 0x060047DD RID: 18397 RVA: 0x0012E0E4 File Offset: 0x0012C2E4
		internal DataRegion GetCurrentDataRegion()
		{
			if (this.m_currentDataRegionName == null)
			{
				return null;
			}
			Global.Tracer.Assert(this.m_dataregionScopes.ContainsKey(this.m_currentDataRegionName));
			return ((InitializationContext.ScopeInfo)this.m_dataregionScopes[this.m_currentDataRegionName]).DataRegionScope;
		}

		// Token: 0x060047DE RID: 18398 RVA: 0x0012E134 File Offset: 0x0012C334
		private bool ValidateDataSetNameForTopLevelDataRegion(string dataSetName, bool registerError)
		{
			bool flag;
			if (this.m_numberOfDataSets == 0)
			{
				flag = dataSetName == null;
			}
			else if (1 == this.m_numberOfDataSets)
			{
				if (dataSetName == null)
				{
					dataSetName = this.m_oneDataSetName;
					flag = true;
				}
				else
				{
					flag = this.m_fieldNameMap.ContainsKey(dataSetName);
				}
			}
			else
			{
				Global.Tracer.Assert(1 < this.m_numberOfDataSets);
				flag = dataSetName != null && this.m_fieldNameMap.ContainsKey(dataSetName);
			}
			if (!flag && registerError)
			{
				if (dataSetName == null)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsMissingDataSetName, Severity.Error, this.m_objectType, this.m_objectName, "DataSetName", Array.Empty<string>());
				}
				else
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidDataSetName, Severity.Error, this.m_objectType, this.m_objectName, "DataSetName", new string[] { dataSetName });
				}
			}
			return flag;
		}

		// Token: 0x060047DF RID: 18399 RVA: 0x0012E1FE File Offset: 0x0012C3FE
		internal void CheckFieldReferences(List<string> fieldNames, string propertyName)
		{
			this.InternalCheckFieldReferences(fieldNames, this.GetDataSetName(), this.m_objectType, this.m_objectName, propertyName);
		}

		// Token: 0x060047E0 RID: 18400 RVA: 0x0012E21A File Offset: 0x0012C41A
		internal void AggregateCheckFieldReferences(List<string> fieldNames, string dataSetName, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			this.InternalCheckFieldReferences(fieldNames, dataSetName, objectType, objectName, propertyName);
		}

		// Token: 0x060047E1 RID: 18401 RVA: 0x0012E22C File Offset: 0x0012C42C
		private void InternalCheckFieldReferences(List<string> fieldNames, string dataSetName, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			if (fieldNames == null)
			{
				return;
			}
			for (int i = 0; i < fieldNames.Count; i++)
			{
				this.CheckFieldReference(fieldNames[i], dataSetName, objectType, objectName, propertyName);
			}
		}

		// Token: 0x060047E2 RID: 18402 RVA: 0x0012E264 File Offset: 0x0012C464
		private void CheckFieldReference(string fieldName, string dataSetName, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			Hashtable hashtable = null;
			if (dataSetName != null)
			{
				hashtable = (Hashtable)this.m_fieldNameMap[dataSetName];
			}
			if (this.m_numberOfDataSets == 0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsFieldReference, Severity.Error, objectType, objectName, propertyName, new string[] { fieldName });
				return;
			}
			Global.Tracer.Assert(1 <= this.m_numberOfDataSets, "Expected 1 or more data sets");
			if (dataSetName != null)
			{
				DataSet dataSet = this.GetDataSet(dataSetName);
				if (dataSet != null && !dataSet.UsedOnlyInParametersSet && (this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataSet) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
				{
					dataSet.UsedOnlyInParameters = false;
				}
			}
			if (hashtable == null && this.m_numberOfDataSets > 1 && (this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataSet) == (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsFieldReferenceAmbiguous, Severity.Error, objectType, objectName, propertyName, new string[] { fieldName });
				return;
			}
			if (hashtable == null || !hashtable.ContainsKey(fieldName))
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsFieldReference, Severity.Error, objectType, objectName, propertyName, new string[] { fieldName });
			}
		}

		// Token: 0x060047E3 RID: 18403 RVA: 0x0012E359 File Offset: 0x0012C559
		internal void FillInFieldIndex(ExpressionInfo exprInfo)
		{
			this.InternalFillInFieldIndex(exprInfo, this.GetDataSetName());
		}

		// Token: 0x060047E4 RID: 18404 RVA: 0x0012E368 File Offset: 0x0012C568
		internal void FillInFieldIndex(ExpressionInfo exprInfo, string dataSetName)
		{
			this.InternalFillInFieldIndex(exprInfo, dataSetName);
		}

		// Token: 0x060047E5 RID: 18405 RVA: 0x0012E372 File Offset: 0x0012C572
		private void InternalFillInFieldIndex(ExpressionInfo exprInfo, string dataSetName)
		{
			if (exprInfo == null || exprInfo.Type != ExpressionInfo.Types.Field)
			{
				return;
			}
			if (dataSetName == null)
			{
				return;
			}
			exprInfo.IntValue = this.GetIndexForDataSetField(dataSetName, exprInfo.StringValue);
		}

		// Token: 0x060047E6 RID: 18406 RVA: 0x0012E398 File Offset: 0x0012C598
		private int GetIndexForDataSetField(string dataSetName, string fieldName)
		{
			Hashtable hashtable = (Hashtable)this.m_fieldNameMap[dataSetName];
			int num = -1;
			if (hashtable != null && hashtable.ContainsKey(fieldName))
			{
				num = (int)hashtable[fieldName];
			}
			return num;
		}

		// Token: 0x060047E7 RID: 18407 RVA: 0x0012E3D4 File Offset: 0x0012C5D4
		internal void FillInTokenIndex(ExpressionInfo exprInfo)
		{
			if (exprInfo == null || exprInfo.Type != ExpressionInfo.Types.Token)
			{
				return;
			}
			string stringValue = exprInfo.StringValue;
			if (stringValue == null)
			{
				return;
			}
			DataSet dataSet = this.GetDataSet(stringValue);
			if (dataSet != null)
			{
				exprInfo.IntValue = dataSet.ID;
			}
		}

		// Token: 0x060047E8 RID: 18408 RVA: 0x0012E410 File Offset: 0x0012C610
		internal void CheckDataSetReference(List<string> referencedDataSets, string propertyName)
		{
			this.InternalCheckDataSetReference(referencedDataSets, this.m_objectType, this.m_objectName, propertyName);
		}

		// Token: 0x060047E9 RID: 18409 RVA: 0x0012E426 File Offset: 0x0012C626
		internal void AggregateCheckDataSetReference(List<string> referencedDataSets, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			this.InternalCheckDataSetReference(referencedDataSets, objectType, objectName, propertyName);
		}

		// Token: 0x060047EA RID: 18410 RVA: 0x0012E434 File Offset: 0x0012C634
		private void InternalCheckDataSetReference(List<string> dataSetNames, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			if (dataSetNames == null)
			{
				return;
			}
			if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				return;
			}
			for (int i = 0; i < dataSetNames.Count; i++)
			{
				DataSet dataSet = this.m_scopeTree.GetDataSet(dataSetNames[i]);
				if (dataSet == null)
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsDataSetReference, Severity.Error, objectType, objectName, propertyName, new string[] { dataSetNames[i] });
				}
				else if (dataSet.IsReferenceToSharedDataSet)
				{
					dataSet.UsedOnlyInParameters = false;
				}
			}
		}

		// Token: 0x060047EB RID: 18411 RVA: 0x0012E4B0 File Offset: 0x0012C6B0
		internal int ResolveScopedFieldReferenceToIndex(string scopeName, string fieldName)
		{
			DataSet targetDataSetForScopeReference = this.GetTargetDataSetForScopeReference(scopeName);
			if (targetDataSetForScopeReference != null)
			{
				return this.GetIndexForDataSetField(targetDataSetForScopeReference.Name, fieldName);
			}
			return -1;
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x0012E4D8 File Offset: 0x0012C6D8
		internal void CheckScopeReferences(List<ScopeReference> referencedScopes, string propertyName)
		{
			if (referencedScopes == null || referencedScopes.Count == 0)
			{
				return;
			}
			if (this.m_currentScope == null || this.m_currentScope.DataScope == null || this.m_currentScope.DataScope is DataSet)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidScopeCollectionReference, Severity.Error, this.m_objectType, this.m_objectName, propertyName, Array.Empty<string>());
				return;
			}
			IRIFDataScope dataScope = this.m_currentScope.DataScope;
			foreach (ScopeReference scopeReference in referencedScopes)
			{
				IRIFDataScope sourceScope = null;
				DataSet targetDataSet = this.GetTargetDataSetForScopeReference(scopeReference.ScopeName);
				if (targetDataSet != null)
				{
					ScopeTree.DirectedScopeTreeVisitor directedScopeTreeVisitor = delegate(IRIFDataScope scope)
					{
						if (scope.DataScopeInfo != null && scope.DataScopeInfo.DataSet != null && targetDataSet.HasDefaultRelationship(scope.DataScopeInfo.DataSet))
						{
							sourceScope = scope;
							return false;
						}
						return true;
					};
					this.m_scopeTree.Traverse(directedScopeTreeVisitor, dataScope);
				}
				if (sourceScope != null)
				{
					if (scopeReference.HasFieldName)
					{
						this.CheckFieldReference(scopeReference.FieldName, targetDataSet.Name, this.m_objectType, this.m_objectName, propertyName);
					}
					IRIFDataScope irifdataScope;
					if (this.m_dataSetsForNonStructuralIdc.TryGetValue(targetDataSet.IndexInCollection, out irifdataScope))
					{
						if (!sourceScope.DataScopeInfo.IsSameScope(irifdataScope.DataScopeInfo))
						{
							this.m_errorContext.Register(ProcessingErrorCode.rsScopeReferenceUsesDataSetMoreThanOnce, Severity.Error, this.m_objectType, this.m_objectName, propertyName, new string[] { targetDataSet.Name });
						}
					}
					else
					{
						this.m_dataSetsForNonStructuralIdc.Add(targetDataSet.IndexInCollection, sourceScope);
					}
				}
				else
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidScopeReference, Severity.Error, this.m_objectType, this.m_objectName, propertyName, new string[] { scopeReference.ScopeName });
				}
			}
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x0012E6C8 File Offset: 0x0012C8C8
		private DataSet GetTargetDataSetForScopeReference(string scopeName)
		{
			return this.m_scopeTree.GetDataSet(scopeName);
		}

		// Token: 0x060047EE RID: 18414 RVA: 0x0012E6D6 File Offset: 0x0012C8D6
		internal void CheckDataSourceReference(List<string> referencedDataSources, string propertyName)
		{
			this.InternalCheckDataSourceReference(referencedDataSources, this.m_objectType, this.m_objectName, propertyName);
		}

		// Token: 0x060047EF RID: 18415 RVA: 0x0012E6EC File Offset: 0x0012C8EC
		internal void AggregateCheckDataSourceReference(List<string> referencedDataSources, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			this.InternalCheckDataSourceReference(referencedDataSources, objectType, objectName, propertyName);
		}

		// Token: 0x060047F0 RID: 18416 RVA: 0x0012E6FC File Offset: 0x0012C8FC
		private void InternalCheckDataSourceReference(List<string> dataSourceNames, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			if (dataSourceNames == null)
			{
				return;
			}
			if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				return;
			}
			for (int i = 0; i < dataSourceNames.Count; i++)
			{
				if (!this.m_dataSources.ContainsKey(dataSourceNames[i]))
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsDataSourceReference, Severity.Error, objectType, objectName, propertyName, new string[] { dataSourceNames[i] });
				}
			}
		}

		// Token: 0x060047F1 RID: 18417 RVA: 0x0012E764 File Offset: 0x0012C964
		internal void RegisterGroupWithVariables(ReportHierarchyNode node)
		{
			if (node.Grouping != null && node.Grouping.Variables != null)
			{
				this.m_report.AddGroupWithVariables(node);
			}
		}

		// Token: 0x060047F2 RID: 18418 RVA: 0x0012E788 File Offset: 0x0012C988
		internal void RegisterVariables(List<Variable> variables)
		{
			foreach (Variable variable in variables)
			{
				this.RegisterVariable(variable);
			}
		}

		// Token: 0x060047F3 RID: 18419 RVA: 0x0012E7D8 File Offset: 0x0012C9D8
		internal void UnregisterVariables(List<Variable> variables)
		{
			foreach (Variable variable in variables)
			{
				this.UnregisterVariable(variable);
			}
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x0012E828 File Offset: 0x0012CA28
		internal void RegisterVariable(Variable variable)
		{
			if (!this.m_variablesInScope.ContainsKey(variable.Name))
			{
				this.m_variablesInScope.Add(variable.Name, variable);
				SequenceIndex.SetBit(ref this.m_referencableVariables, variable.SequenceID);
			}
		}

		// Token: 0x060047F5 RID: 18421 RVA: 0x0012E860 File Offset: 0x0012CA60
		internal void UnregisterVariable(Variable variable)
		{
			this.m_variablesInScope.Remove(variable.Name);
			SequenceIndex.ClearBit(ref this.m_referencableVariables, variable.SequenceID);
		}

		// Token: 0x060047F6 RID: 18422 RVA: 0x0012E885 File Offset: 0x0012CA85
		internal void CheckVariableReferences(List<string> referencedVariables, string propertyName)
		{
			this.InternalCheckVariableReferences(referencedVariables, this.m_objectType, this.m_objectName, propertyName);
		}

		// Token: 0x060047F7 RID: 18423 RVA: 0x0012E89B File Offset: 0x0012CA9B
		internal void AggregateCheckVariableReferences(List<string> referencedVariables, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			this.InternalCheckVariableReferences(referencedVariables, objectType, objectName, propertyName);
		}

		// Token: 0x060047F8 RID: 18424 RVA: 0x0012E8A8 File Offset: 0x0012CAA8
		private void InternalCheckVariableReferences(List<string> referencedVariables, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			if (referencedVariables == null || this.m_inAutoSubtotalClone)
			{
				return;
			}
			for (int i = 0; i < referencedVariables.Count; i++)
			{
				if (!this.m_variablesInScope.ContainsKey(referencedVariables[i]))
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidVariableReference, Severity.Error, objectType, objectName, propertyName, new string[]
					{
						"Variable",
						referencedVariables[i]
					});
				}
			}
		}

		// Token: 0x060047F9 RID: 18425 RVA: 0x0012E914 File Offset: 0x0012CB14
		internal void RegisterTextBoxInScope(TextBox textbox)
		{
			if (this.m_currentDataRegionName != null)
			{
				if (this.m_groupingScopesForRunningValuesInTablix == null || !this.m_groupingScopesForRunningValuesInTablix.ContainerName.Equals(this.m_currentDataRegionName))
				{
					this.m_currentScope.ReportScope.AddInScopeTextBox(textbox);
					return;
				}
				string currentColumnScopeName = this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeName;
				string currentRowScopeName = this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeName;
				if (currentColumnScopeName == null && currentRowScopeName == null)
				{
					((IRIFReportScope)this.m_groupingScopesForRunningValuesInTablix.ContainerScope).AddInScopeTextBox(textbox);
					return;
				}
				if (currentColumnScopeName != null)
				{
					((InitializationContext.ScopeInfo)this.m_groupingScopes[currentColumnScopeName]).ReportScope.AddInScopeTextBox(textbox);
				}
				if (currentRowScopeName != null)
				{
					((InitializationContext.ScopeInfo)this.m_groupingScopes[currentRowScopeName]).ReportScope.AddInScopeTextBox(textbox);
					return;
				}
			}
			else
			{
				Global.Tracer.Assert(this.m_currentScope != null, "Top level scope should have been setup as either Page or ReportSection");
				this.m_currentScope.ReportScope.AddInScopeTextBox(textbox);
			}
		}

		// Token: 0x060047FA RID: 18426 RVA: 0x0012E9FC File Offset: 0x0012CBFC
		internal void RegisterReportItem(ReportItem reportItem)
		{
			if (reportItem != null)
			{
				Pair<ReportItem, int> pair;
				if (this.m_reportItemsInScope.TryGetValue(reportItem.Name, out pair))
				{
					pair.Second++;
				}
				else
				{
					pair = new Pair<ReportItem, int>(reportItem, 1);
				}
				this.m_reportItemsInScope[reportItem.Name] = pair;
				this.m_reportItemsInSection[reportItem.Name] = reportItem;
				Microsoft.ReportingServices.ReportProcessing.ObjectType objectType = reportItem.ObjectType;
				if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Rectangle)
				{
					this.RegisterReportItems(((Rectangle)reportItem).ReportItems);
					return;
				}
				if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Textbox)
				{
					if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Tablix)
					{
						return;
					}
					this.RegisterReportItem((Tablix)reportItem);
					return;
				}
				else
				{
					int sequenceID = ((TextBox)reportItem).SequenceID;
					SequenceIndex.SetBit(ref this.m_referencableTextboxes, sequenceID);
					SequenceIndex.SetBit(ref this.m_referencableTextboxesInSection, sequenceID);
				}
			}
		}

		// Token: 0x060047FB RID: 18427 RVA: 0x0012EABC File Offset: 0x0012CCBC
		internal void RegisterReportItems(ReportItemCollection reportItems)
		{
			for (int i = 0; i < reportItems.Count; i++)
			{
				this.RegisterReportItem(reportItems[i]);
			}
		}

		// Token: 0x060047FC RID: 18428 RVA: 0x0012EAE8 File Offset: 0x0012CCE8
		private void RegisterReportItems(List<List<TablixCornerCell>> corner)
		{
			if (corner != null)
			{
				foreach (List<TablixCornerCell> list in corner)
				{
					foreach (TablixCornerCell tablixCornerCell in list)
					{
						this.RegisterReportItems(tablixCornerCell);
					}
				}
			}
		}

		// Token: 0x060047FD RID: 18429 RVA: 0x0012EB70 File Offset: 0x0012CD70
		internal void RegisterReportItems(TablixRowList rows)
		{
			foreach (object obj in rows)
			{
				foreach (object obj2 in ((TablixRow)obj).TablixCells)
				{
					TablixCell tablixCell = (TablixCell)obj2;
					this.RegisterReportItems(tablixCell);
				}
			}
		}

		// Token: 0x060047FE RID: 18430 RVA: 0x0012EC04 File Offset: 0x0012CE04
		private void RegisterReportItems(TablixCellBase cell)
		{
			if (cell.CellContents != null)
			{
				this.RegisterReportItem(cell.CellContents);
				if (cell.AltCellContents != null)
				{
					this.RegisterReportItem(cell.AltCellContents);
				}
			}
		}

		// Token: 0x060047FF RID: 18431 RVA: 0x0012EC30 File Offset: 0x0012CE30
		private void RegisterReportItem(Tablix tablix)
		{
			this.RegisterReportItems(tablix.Corner);
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			int num = 0;
			this.RegisterStaticMemberReportItems(tablix.TablixRowMembers, true, list, ref num);
			num = 0;
			this.RegisterStaticMemberReportItems(tablix.TablixColumnMembers, true, list2, ref num);
			if (tablix.TablixRows != null)
			{
				foreach (int num2 in list)
				{
					if (num2 < tablix.TablixRows.Count)
					{
						TablixRow tablixRow = tablix.TablixRows[num2];
						foreach (int num3 in list2)
						{
							if (num3 < tablixRow.TablixCells.Count)
							{
								TablixCell tablixCell = tablixRow.TablixCells[num3];
								this.RegisterReportItems(tablixCell);
							}
						}
					}
				}
			}
		}

		// Token: 0x06004800 RID: 18432 RVA: 0x0012ED40 File Offset: 0x0012CF40
		private void RegisterStaticMemberReportItems(TablixMemberList members, bool register, List<int> indexes, ref int index)
		{
			foreach (object obj in members)
			{
				TablixMember tablixMember = (TablixMember)obj;
				if (tablixMember.Grouping == null)
				{
					this.RegisterMemberReportItems(tablixMember, register, true, indexes, ref index);
				}
				else
				{
					this.RegisterMemberReportItems(tablixMember, false, true, indexes, ref index);
				}
			}
		}

		// Token: 0x06004801 RID: 18433 RVA: 0x0012EDB0 File Offset: 0x0012CFB0
		internal void RegisterMemberReportItems(TablixMember member, bool firstPass, bool restrictive)
		{
			int num = 0;
			this.RegisterMemberReportItems(member, true, false, null, ref num);
			if (firstPass)
			{
				this.HandleCellContents(member, true, restrictive);
			}
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x0012EDD7 File Offset: 0x0012CFD7
		internal void RegisterMemberReportItems(TablixMember member, bool firstPass)
		{
			this.RegisterMemberReportItems(member, firstPass, true);
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x0012EDE2 File Offset: 0x0012CFE2
		private void HandleCellContents(TablixMember member, bool register)
		{
			this.HandleCellContents(member, register, true);
		}

		// Token: 0x06004804 RID: 18436 RVA: 0x0012EDF0 File Offset: 0x0012CFF0
		private void HandleCellContents(TablixMember member, bool register, bool restrictive)
		{
			Tablix tablix = (Tablix)member.DataRegionDef;
			int num = 0;
			int num2 = tablix.RowCount - 1;
			int num3 = 0;
			int num4 = tablix.ColumnCount - 1;
			if (restrictive)
			{
				if (member.IsColumn)
				{
					num3 = member.CellStartIndex;
					num4 = member.CellEndIndex;
				}
				else
				{
					num = member.CellStartIndex;
					num2 = member.CellEndIndex;
				}
			}
			if (!restrictive && this.m_handledCellContents.Value)
			{
				if (register)
				{
					return;
				}
				if (member.IsColumn)
				{
					if (member.CellStartIndex < num4 && member.CellEndIndex < num4)
					{
						return;
					}
				}
				else if (member.CellStartIndex < num2 && member.CellEndIndex < num2)
				{
					return;
				}
			}
			if (tablix.TablixRows != null)
			{
				for (int i = num; i <= num2; i++)
				{
					if (i < tablix.TablixRows.Count)
					{
						TablixRow tablixRow = tablix.TablixRows[i];
						for (int j = num3; j <= num4; j++)
						{
							if (j < tablixRow.TablixCells.Count)
							{
								TablixCell tablixCell = tablixRow.TablixCells[j];
								if (tablixCell != null)
								{
									if (register)
									{
										this.RegisterReportItems(tablixCell);
									}
									else
									{
										this.UnRegisterReportItems(tablixCell);
									}
								}
							}
						}
					}
				}
				if (!restrictive)
				{
					this.m_handledCellContents.Value = register > false;
				}
			}
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x0012EF24 File Offset: 0x0012D124
		private void RegisterMemberReportItems(TablixMember member, bool register, bool registerStatic, List<int> indexes, ref int index)
		{
			if (register && (registerStatic || member.Grouping != null) && member.TablixHeader != null && member.TablixHeader.CellContents != null)
			{
				this.RegisterReportItem(member.TablixHeader.CellContents);
				if (member.TablixHeader.AltCellContents != null)
				{
					this.RegisterReportItem(member.TablixHeader.AltCellContents);
				}
			}
			if (member.SubMembers != null)
			{
				this.RegisterStaticMemberReportItems(member.SubMembers, register, indexes, ref index);
				return;
			}
			if (indexes != null)
			{
				if (register && member.Grouping == null)
				{
					indexes.Add(index);
				}
				index++;
			}
		}

		// Token: 0x06004806 RID: 18438 RVA: 0x0012EFC0 File Offset: 0x0012D1C0
		internal void UnRegisterReportItem(ReportItem reportItem)
		{
			if (reportItem != null)
			{
				Pair<ReportItem, int> pair = this.m_reportItemsInScope[reportItem.Name];
				pair.Second--;
				bool flag = pair.Second == 0;
				if (flag)
				{
					this.m_reportItemsInScope.Remove(reportItem.Name);
				}
				else
				{
					this.m_reportItemsInScope[reportItem.Name] = pair;
				}
				Microsoft.ReportingServices.ReportProcessing.ObjectType objectType = reportItem.ObjectType;
				if (objectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Rectangle)
				{
					this.UnRegisterReportItems(((Rectangle)reportItem).ReportItems);
					return;
				}
				if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Textbox)
				{
					if (objectType != Microsoft.ReportingServices.ReportProcessing.ObjectType.Tablix)
					{
						return;
					}
					this.UnRegisterReportItem((Tablix)reportItem);
					return;
				}
				else if (flag)
				{
					SequenceIndex.ClearBit(ref this.m_referencableTextboxes, ((TextBox)reportItem).SequenceID);
				}
			}
		}

		// Token: 0x06004807 RID: 18439 RVA: 0x0012F074 File Offset: 0x0012D274
		private void UnRegisterReportItems(List<List<TablixCornerCell>> corner)
		{
			if (corner != null)
			{
				foreach (List<TablixCornerCell> list in corner)
				{
					foreach (TablixCornerCell tablixCornerCell in list)
					{
						this.UnRegisterReportItems(tablixCornerCell);
					}
				}
			}
		}

		// Token: 0x06004808 RID: 18440 RVA: 0x0012F0FC File Offset: 0x0012D2FC
		internal void UnRegisterReportItems(TablixRowList rows)
		{
			foreach (object obj in rows)
			{
				foreach (object obj2 in ((TablixRow)obj).TablixCells)
				{
					TablixCell tablixCell = (TablixCell)obj2;
					this.UnRegisterReportItems(tablixCell);
				}
			}
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x0012F190 File Offset: 0x0012D390
		private void UnRegisterReportItems(TablixCellBase cell)
		{
			if (cell.CellContents != null)
			{
				this.UnRegisterReportItem(cell.CellContents);
				if (cell.AltCellContents != null)
				{
					this.UnRegisterReportItem(cell.AltCellContents);
				}
			}
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x0012F1BC File Offset: 0x0012D3BC
		internal void UnRegisterReportItem(Tablix tablix)
		{
			this.UnRegisterReportItems(tablix.Corner);
			List<int> list = new List<int>();
			List<int> list2 = new List<int>();
			int num = 0;
			this.UnRegisterStaticMemberReportItems(tablix.TablixRowMembers, true, list, ref num);
			num = 0;
			this.UnRegisterStaticMemberReportItems(tablix.TablixColumnMembers, true, list2, ref num);
			if (tablix.TablixRows != null)
			{
				foreach (int num2 in list)
				{
					if (num2 < tablix.TablixRows.Count)
					{
						TablixRow tablixRow = tablix.TablixRows[num2];
						foreach (int num3 in list2)
						{
							if (num3 < tablixRow.TablixCells.Count)
							{
								TablixCell tablixCell = tablixRow.TablixCells[num3];
								this.UnRegisterReportItems(tablixCell);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600480B RID: 18443 RVA: 0x0012F2CC File Offset: 0x0012D4CC
		private void UnRegisterStaticMemberReportItems(TablixMemberList members, bool unregister, List<int> indexes, ref int index)
		{
			foreach (object obj in members)
			{
				TablixMember tablixMember = (TablixMember)obj;
				if (tablixMember.Grouping == null)
				{
					this.UnRegisterMemberReportItems(tablixMember, unregister, true, indexes, ref index);
				}
				else
				{
					this.UnRegisterMemberReportItems(tablixMember, false, true, indexes, ref index);
				}
			}
		}

		// Token: 0x0600480C RID: 18444 RVA: 0x0012F33C File Offset: 0x0012D53C
		internal void UnRegisterMemberReportItems(TablixMember member, bool firstPass)
		{
			this.UnRegisterMemberReportItems(member, firstPass, true);
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x0012F348 File Offset: 0x0012D548
		internal void UnRegisterMemberReportItems(TablixMember member, bool firstPass, bool restrictive)
		{
			int num = 0;
			this.UnRegisterMemberReportItems(member, true, false, null, ref num);
			if (firstPass)
			{
				this.HandleCellContents(member, false, restrictive);
			}
		}

		// Token: 0x0600480E RID: 18446 RVA: 0x0012F370 File Offset: 0x0012D570
		private void UnRegisterMemberReportItems(TablixMember member, bool unregister, bool unregisterStatic, List<int> indexes, ref int index)
		{
			if (unregister && (unregisterStatic || member.Grouping != null) && member.TablixHeader != null && member.TablixHeader.CellContents != null)
			{
				this.UnRegisterReportItem(member.TablixHeader.CellContents);
				if (member.TablixHeader.AltCellContents != null)
				{
					this.UnRegisterReportItem(member.TablixHeader.AltCellContents);
				}
			}
			if (member.SubMembers != null)
			{
				this.UnRegisterStaticMemberReportItems(member.SubMembers, unregister, indexes, ref index);
				return;
			}
			if (indexes != null)
			{
				if (unregister && member.Grouping == null)
				{
					indexes.Add(index);
				}
				index++;
			}
		}

		// Token: 0x0600480F RID: 18447 RVA: 0x0012F40C File Offset: 0x0012D60C
		internal void UnRegisterReportItems(ReportItemCollection reportItems)
		{
			Global.Tracer.Assert(reportItems != null);
			for (int i = 0; i < reportItems.Count; i++)
			{
				this.UnRegisterReportItem(reportItems[i]);
			}
		}

		// Token: 0x06004810 RID: 18448 RVA: 0x0012F445 File Offset: 0x0012D645
		internal void CheckReportItemReferences(List<string> referencedReportItems, string propertyName)
		{
			this.InternalCheckReportItemReferences(referencedReportItems, this.m_objectType, this.m_objectName, propertyName);
		}

		// Token: 0x06004811 RID: 18449 RVA: 0x0012F45B File Offset: 0x0012D65B
		internal void AggregateCheckReportItemReferences(List<string> referencedReportItems, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			this.InternalCheckReportItemReferences(referencedReportItems, objectType, objectName, propertyName);
		}

		// Token: 0x06004812 RID: 18450 RVA: 0x0012F468 File Offset: 0x0012D668
		private void InternalCheckReportItemReferences(List<string> referencedReportItems, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			if (referencedReportItems == null)
			{
				return;
			}
			if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				for (int i = 0; i < referencedReportItems.Count; i++)
				{
					if (!this.m_reportItemsInSection.ContainsKey(referencedReportItems[i]))
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsReportItemReferenceInPageSection, Severity.Error, objectType, objectName, propertyName, new string[] { referencedReportItems[i] });
					}
				}
				return;
			}
			for (int j = 0; j < referencedReportItems.Count; j++)
			{
				if (!this.m_reportItemsInScope.ContainsKey(referencedReportItems[j]))
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsReportItemReference, Severity.Error, objectType, objectName, propertyName, new string[] { referencedReportItems[j] });
				}
			}
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x0012F51B File Offset: 0x0012D71B
		internal byte[] GetCurrentReferencableVariables()
		{
			if (this.m_referencableVariables == null)
			{
				return null;
			}
			return this.m_referencableVariables.Clone() as byte[];
		}

		// Token: 0x06004814 RID: 18452 RVA: 0x0012F537 File Offset: 0x0012D737
		internal byte[] GetCurrentReferencableTextboxes()
		{
			if (this.m_referencableTextboxes == null)
			{
				return null;
			}
			return this.m_referencableTextboxes.Clone() as byte[];
		}

		// Token: 0x06004815 RID: 18453 RVA: 0x0012F553 File Offset: 0x0012D753
		internal byte[] GetCurrentReferencableTextboxesInSection()
		{
			if (this.m_referencableTextboxesInSection == null)
			{
				return null;
			}
			return this.m_referencableTextboxesInSection.Clone() as byte[];
		}

		// Token: 0x06004816 RID: 18454 RVA: 0x0012F56F File Offset: 0x0012D76F
		internal void CheckReportParameterReferences(List<string> referencedParameters, string propertyName)
		{
			this.InternalCheckReportParameterReferences(referencedParameters, this.m_objectType, this.m_objectName, propertyName);
		}

		// Token: 0x06004817 RID: 18455 RVA: 0x0012F588 File Offset: 0x0012D788
		private void InternalCheckReportParameterReferences(List<string> referencedParameters, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			if (referencedParameters == null)
			{
				return;
			}
			for (int i = 0; i < referencedParameters.Count; i++)
			{
				if (this.m_parameters == null || !this.m_parameters.ContainsKey(referencedParameters[i]))
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsParameterReference, Severity.Error, objectType, objectName, propertyName, new string[] { referencedParameters[i] });
				}
			}
		}

		// Token: 0x06004818 RID: 18456 RVA: 0x0012F5EC File Offset: 0x0012D7EC
		internal VisibilityToggleInfo RegisterVisibilityToggle(Visibility visibility)
		{
			if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InPageSection) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				return null;
			}
			VisibilityToggleInfo visibilityToggleInfo = null;
			if (visibility.IsToggleReceiver)
			{
				visibilityToggleInfo = new VisibilityToggleInfo();
				visibilityToggleInfo.ObjectName = this.m_objectName;
				visibilityToggleInfo.ObjectType = this.m_objectType;
				visibilityToggleInfo.Visibility = visibility;
				visibilityToggleInfo.GroupName = this.m_currentGroupName;
				visibilityToggleInfo.GroupingSet = (Hashtable)this.m_groupingScopes.Clone();
				this.m_visibilityToggleInfos.Add(visibilityToggleInfo);
			}
			return visibilityToggleInfo;
		}

		// Token: 0x06004819 RID: 18457 RVA: 0x0012F664 File Offset: 0x0012D864
		internal bool RegisterVisibility(Visibility visibility, IVisibilityOwner owner)
		{
			IVisibilityOwner visibilityOwner;
			IVisibilityOwner visibilityOwner2;
			IVisibilityOwner visibilityOwner3;
			if (this.NeedVisibilityLink(visibility, owner, out visibilityOwner, out visibilityOwner2, out visibilityOwner3))
			{
				InitializationContext.VisibilityContainmentInfo visibilityContainmentInfo = new InitializationContext.VisibilityContainmentInfo();
				if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InTablixColumnHierarchy) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 && visibilityOwner3 != null)
				{
					owner.ContainingDynamicVisibility = visibilityOwner3;
				}
				else if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InTablixRowHierarchy) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 && visibilityOwner2 != null)
				{
					owner.ContainingDynamicVisibility = visibilityOwner2;
				}
				else if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InTablixCell) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0 && (visibilityOwner3 != null || visibilityOwner2 != null))
				{
					owner.ContainingDynamicColumnVisibility = visibilityOwner3;
					owner.ContainingDynamicRowVisibility = visibilityOwner2;
				}
				else if (visibilityOwner != null)
				{
					owner.ContainingDynamicVisibility = visibilityOwner;
				}
				if (owner.GetObjectType() == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixMember)
				{
					TablixMember tablixMember = (TablixMember)owner;
					if (!tablixMember.IsAutoSubtotal)
					{
						if (tablixMember.IsColumn)
						{
							visibilityContainmentInfo.ContainingColumnVisibility = owner;
							visibilityContainmentInfo.ContainingRowVisibility = visibilityOwner2;
						}
						else
						{
							visibilityContainmentInfo.ContainingRowVisibility = owner;
						}
					}
				}
				else
				{
					visibilityContainmentInfo.ContainingVisibility = owner;
				}
				this.m_visibilityContainmentInfos.Push(visibilityContainmentInfo);
				return true;
			}
			return false;
		}

		// Token: 0x0600481A RID: 18458 RVA: 0x0012F742 File Offset: 0x0012D942
		internal void UnRegisterVisibility(Visibility visibility, IVisibilityOwner owner)
		{
			this.m_visibilityContainmentInfos.Pop();
		}

		// Token: 0x0600481B RID: 18459 RVA: 0x0012F750 File Offset: 0x0012D950
		internal bool NeedVisibilityLink(Visibility visibility, IVisibilityOwner owner, out IVisibilityOwner outerContainer, out IVisibilityOwner outerRowContainer, out IVisibilityOwner outerColumnContainer)
		{
			outerContainer = null;
			outerRowContainer = null;
			outerColumnContainer = null;
			if (this.m_visibilityContainmentInfos.Count > 0)
			{
				InitializationContext.VisibilityContainmentInfo visibilityContainmentInfo = this.m_visibilityContainmentInfos.Peek();
				outerContainer = visibilityContainmentInfo.ContainingVisibility;
				outerRowContainer = visibilityContainmentInfo.ContainingRowVisibility;
				outerColumnContainer = visibilityContainmentInfo.ContainingColumnVisibility;
			}
			bool flag = outerRowContainer != null || outerColumnContainer != null;
			return (owner.GetObjectType() == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Tablix && flag) || owner.GetObjectType() == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TextBox || (visibility != null && (visibility.IsConditional || visibility.IsToggleReceiver));
		}

		// Token: 0x0600481C RID: 18460 RVA: 0x0012F7E0 File Offset: 0x0012D9E0
		internal void RegisterToggleItem(TextBox textbox)
		{
			ToggleItemInfo toggleItemInfo = new ToggleItemInfo();
			toggleItemInfo.Textbox = textbox;
			toggleItemInfo.GroupName = this.m_currentGroupName;
			toggleItemInfo.GroupingSet = (Hashtable)this.m_groupingScopes.Clone();
			this.m_toggleItems.Add(textbox.Name, toggleItemInfo);
		}

		// Token: 0x0600481D RID: 18461 RVA: 0x0012F830 File Offset: 0x0012DA30
		internal void ValidateToggleItems()
		{
			foreach (VisibilityToggleInfo visibilityToggleInfo in this.m_visibilityToggleInfos)
			{
				bool flag = false;
				ToggleItemInfo toggleItemInfo = null;
				if (this.m_toggleItems.ContainsKey(visibilityToggleInfo.Visibility.Toggle))
				{
					toggleItemInfo = this.m_toggleItems[visibilityToggleInfo.Visibility.Toggle];
					Hashtable groupingSet = toggleItemInfo.GroupingSet;
					Hashtable groupingSet2 = visibilityToggleInfo.GroupingSet;
					flag = this.ContainsSubsetOfKeys(groupingSet2, groupingSet);
					InitializationContext.ScopeInfo scopeInfo = null;
					if (visibilityToggleInfo.GroupName != null)
					{
						scopeInfo = (InitializationContext.ScopeInfo)groupingSet2[visibilityToggleInfo.GroupName];
					}
					if (!flag || (visibilityToggleInfo.IsTablixMember && scopeInfo != null && scopeInfo.GroupingScope.Parent != null && this.IsTargetVisibilityOnContainmentChain(toggleItemInfo.Textbox, visibilityToggleInfo.Visibility)))
					{
						if (visibilityToggleInfo.GroupName != null && toggleItemInfo.GroupName != null && visibilityToggleInfo.GroupName == toggleItemInfo.GroupName)
						{
							Global.Tracer.Assert(groupingSet.Contains(toggleItemInfo.GroupName), "(toggleItemGroupingSet.Contains(toggleItem.GroupName))");
							InitializationContext.ScopeInfo scopeInfo2 = (InitializationContext.ScopeInfo)groupingSet[toggleItemInfo.GroupName];
							if (scopeInfo2.GroupingScope.Parent != null)
							{
								flag = true;
								TextBox textbox = toggleItemInfo.Textbox;
								textbox.RecursiveSender = true;
								textbox.RecursiveMember = (TablixMember)scopeInfo2.ReportScope;
								Visibility visibility = visibilityToggleInfo.Visibility;
								visibility.RecursiveReceiver = true;
								if (scopeInfo != null)
								{
									visibility.RecursiveMember = (TablixMember)scopeInfo.ReportScope;
								}
							}
						}
					}
					else
					{
						for (ReportItem reportItem = toggleItemInfo.Textbox.Parent; reportItem != null; reportItem = reportItem.Parent)
						{
							if (reportItem.Visibility == visibilityToggleInfo.Visibility)
							{
								flag = false;
								break;
							}
						}
					}
				}
				if (flag)
				{
					TextBox textbox2 = toggleItemInfo.Textbox;
					textbox2.IsToggle = true;
					if (!visibilityToggleInfo.Visibility.RecursiveReceiver)
					{
						textbox2.HasNonRecursiveSender = true;
					}
					visibilityToggleInfo.Visibility.ToggleSender = textbox2;
					ReportItem reportItem2 = textbox2;
					do
					{
						reportItem2.Computed = true;
						reportItem2 = reportItem2.Parent;
					}
					while (reportItem2 is Rectangle);
				}
				else if (visibilityToggleInfo.Visibility.IsClone)
				{
					visibilityToggleInfo.Visibility.Toggle = null;
				}
				else
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidToggleItem, Severity.Error, visibilityToggleInfo.ObjectType, visibilityToggleInfo.ObjectName, "Item", new string[] { visibilityToggleInfo.Visibility.Toggle });
				}
			}
			this.m_toggleItems.Clear();
			this.m_visibilityToggleInfos.Clear();
		}

		// Token: 0x0600481E RID: 18462 RVA: 0x0012FADC File Offset: 0x0012DCDC
		private bool IsTargetVisibilityOnContainmentChain(IVisibilityOwner visibilityOwner, Visibility targetVisibility)
		{
			if (visibilityOwner.Visibility == targetVisibility)
			{
				return true;
			}
			if (visibilityOwner.ContainingDynamicVisibility != null)
			{
				return this.IsTargetVisibilityOnContainmentChain(visibilityOwner.ContainingDynamicVisibility, targetVisibility);
			}
			bool flag = false;
			if (visibilityOwner.ContainingDynamicRowVisibility != null)
			{
				flag = this.IsTargetVisibilityOnContainmentChain(visibilityOwner.ContainingDynamicRowVisibility, targetVisibility);
			}
			if (!flag && visibilityOwner.ContainingDynamicColumnVisibility != null)
			{
				flag = this.IsTargetVisibilityOnContainmentChain(visibilityOwner.ContainingDynamicColumnVisibility, targetVisibility);
			}
			return flag;
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x0012FB3C File Offset: 0x0012DD3C
		private bool ContainsSubsetOfKeys(Hashtable set, Hashtable subSet)
		{
			int num = 0;
			foreach (object obj in subSet.Keys)
			{
				if (!set.ContainsKey(obj))
				{
					return false;
				}
				num++;
			}
			return num == subSet.Keys.Count;
		}

		// Token: 0x06004820 RID: 18464 RVA: 0x0012FBB0 File Offset: 0x0012DDB0
		internal void ValidateHeaderSize(double size, int startLevel, int span, bool isColumnHierarchy, int cellIndex)
		{
			double num = Math.Round(this.GetHeaderSize(isColumnHierarchy, startLevel, span), 4);
			double num2 = Math.Round(size, 4);
			if (Microsoft.ReportingServices.ReportPublishing.Validator.CompareDoubles(num2, num) != 0)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidTablixHeaderSize, Severity.Error, this.m_objectType, this.m_objectName, isColumnHierarchy ? "TablixColumnHierarchy" : "TablixRowHierarchy", new string[]
				{
					"TablixHeader.Size",
					(cellIndex + 1).ToString(CultureInfo.InvariantCulture.NumberFormat),
					num.ToString(CultureInfo.InvariantCulture.NumberFormat),
					num2.ToString(CultureInfo.InvariantCulture.NumberFormat),
					isColumnHierarchy ? "TablixColumn" : "TablixRow"
				});
			}
		}

		// Token: 0x06004821 RID: 18465 RVA: 0x0012FC72 File Offset: 0x0012DE72
		internal double GetTotalHeaderSize(bool isColumnHierarchy, int span)
		{
			return this.GetHeaderSize(isColumnHierarchy, 0, span);
		}

		// Token: 0x06004822 RID: 18466 RVA: 0x0012FC80 File Offset: 0x0012DE80
		internal double GetHeaderSize(bool isColumnHierarchy, int startLevel, int span)
		{
			IList<Pair<double, int>> list;
			if (isColumnHierarchy)
			{
				Global.Tracer.Assert(this.m_columnHeaderLevelSizeList != null, "(m_columnHeaderLevelSizeList != null)");
				list = this.m_columnHeaderLevelSizeList;
			}
			else
			{
				Global.Tracer.Assert(this.m_rowHeaderLevelSizeList != null, "(m_rowHeaderLevelSizeList != null)");
				list = this.m_rowHeaderLevelSizeList;
			}
			return this.GetHeaderSize(list, startLevel, span);
		}

		// Token: 0x06004823 RID: 18467 RVA: 0x0012FCDC File Offset: 0x0012DEDC
		internal double GetHeaderSize(IList<Pair<double, int>> headerLevelSizeList, int startingLevel, int spans)
		{
			int num = startingLevel + spans;
			startingLevel = this.FindEntryForLevel(headerLevelSizeList, startingLevel);
			num = this.FindEntryForLevel(headerLevelSizeList, num);
			return headerLevelSizeList[num].First - headerLevelSizeList[startingLevel].First;
		}

		// Token: 0x06004824 RID: 18468 RVA: 0x0012FD1C File Offset: 0x0012DF1C
		private int FindEntryForLevel(IList<Pair<double, int>> headerLevelSizeList, int level)
		{
			if (level > 0)
			{
				int num = -1;
				for (int i = 0; i < headerLevelSizeList.Count; i++)
				{
					num += headerLevelSizeList[i].Second + 1;
					if (num >= level)
					{
						return i;
					}
				}
			}
			return 0;
		}

		// Token: 0x06004825 RID: 18469 RVA: 0x0012FD58 File Offset: 0x0012DF58
		internal double ValidateSize(string size, string propertyName)
		{
			double num;
			string text;
			Microsoft.ReportingServices.ReportPublishing.PublishingValidator.ValidateSize(size, this.m_objectType, this.m_objectName, propertyName, true, this.m_errorContext, out num, out text);
			return num;
		}

		// Token: 0x06004826 RID: 18470 RVA: 0x0012FD85 File Offset: 0x0012DF85
		internal double ValidateSize(ref string size, string propertyName)
		{
			return this.ValidateSize(ref size, true, propertyName);
		}

		// Token: 0x06004827 RID: 18471 RVA: 0x0012FD90 File Offset: 0x0012DF90
		internal double ValidateSize(ref string size, bool restrictMaxValue, string propertyName)
		{
			double num;
			string text;
			Microsoft.ReportingServices.ReportPublishing.PublishingValidator.ValidateSize(size, this.m_objectType, this.m_objectName, propertyName, restrictMaxValue, this.m_errorContext, out num, out text);
			size = text;
			return num;
		}

		// Token: 0x06004828 RID: 18472 RVA: 0x0012FDC4 File Offset: 0x0012DFC4
		internal void CheckInternationalSettings(Dictionary<string, AttributeInfo> styleAttributes)
		{
			if (styleAttributes == null || styleAttributes.Count == 0)
			{
				return;
			}
			CultureInfo cultureInfo = null;
			AttributeInfo attributeInfo;
			if (!styleAttributes.TryGetValue("Language", out attributeInfo))
			{
				cultureInfo = this.m_reportLanguage;
			}
			else if (!attributeInfo.IsExpression)
			{
				Microsoft.ReportingServices.ReportPublishing.PublishingValidator.ValidateLanguage(attributeInfo.Value, this.ObjectType, this.ObjectName, "Language", this.ErrorContext, out cultureInfo);
			}
			AttributeInfo attributeInfo2;
			if (cultureInfo != null && styleAttributes.TryGetValue("Calendar", out attributeInfo2) && !attributeInfo2.IsExpression)
			{
				Microsoft.ReportingServices.ReportPublishing.PublishingValidator.ValidateCalendar(cultureInfo, attributeInfo2.Value, this.ObjectType, this.ObjectName, "Calendar", this.ErrorContext);
			}
			if (styleAttributes.TryGetValue("NumeralLanguage", out attributeInfo))
			{
				if (attributeInfo.IsExpression)
				{
					cultureInfo = null;
				}
				else
				{
					Microsoft.ReportingServices.ReportPublishing.PublishingValidator.ValidateLanguage(attributeInfo.Value, this.ObjectType, this.ObjectName, "NumeralLanguage", this.ErrorContext, out cultureInfo);
				}
			}
			AttributeInfo attributeInfo3;
			if (cultureInfo != null && styleAttributes.TryGetValue("NumeralVariant", out attributeInfo3) && !attributeInfo3.IsExpression)
			{
				Microsoft.ReportingServices.ReportPublishing.PublishingValidator.ValidateNumeralVariant(cultureInfo, attributeInfo3.IntValue, this.ObjectType, this.ObjectName, "NumeralVariant", this.ErrorContext);
			}
			if (styleAttributes.TryGetValue("CurrencyLanguage", out attributeInfo) && !attributeInfo.IsExpression)
			{
				Microsoft.ReportingServices.ReportPublishing.PublishingValidator.ValidateLanguage(attributeInfo.Value, this.ObjectType, this.ObjectName, "CurrencyLanguage", this.ErrorContext, out cultureInfo);
			}
		}

		// Token: 0x06004829 RID: 18473 RVA: 0x0012FF1C File Offset: 0x0012E11C
		internal string GetCurrentScopeName()
		{
			Global.Tracer.Assert(this.m_currentScope != null, "Missing ScopeInfo for current scope.");
			if (this.m_currentScope.IsTopLevelScope)
			{
				return "0_ReportScope";
			}
			if (this.m_currentScope.GroupingScope != null)
			{
				return this.m_currentScope.GroupingScope.Name;
			}
			if (!this.m_isDataRegionScopedCell)
			{
				if (!this.m_groupingScopesForRunningValuesInTablix.IsRunningValueDirectionColumn)
				{
					if (this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeName != null)
					{
						return this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeName;
					}
					if (this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeName != null)
					{
						return this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeName;
					}
				}
				else
				{
					if (this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeName != null)
					{
						return this.m_groupingScopesForRunningValuesInTablix.CurrentRowScopeName;
					}
					if (this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeName != null)
					{
						return this.m_groupingScopesForRunningValuesInTablix.CurrentColumnScopeName;
					}
				}
			}
			return this.m_currentDataRegionName;
		}

		// Token: 0x0600482A RID: 18474 RVA: 0x0012FFF1 File Offset: 0x0012E1F1
		internal bool IsScope(string scope)
		{
			return scope != null && this.m_reportScopes.ContainsKey(scope);
		}

		// Token: 0x0600482B RID: 18475 RVA: 0x00130004 File Offset: 0x0012E204
		internal bool IsAncestorScope(string targetScope)
		{
			string dataSetName = this.GetDataSetName();
			return (dataSetName != null && ReportProcessing.CompareWithInvariantCulture(dataSetName, targetScope, false) == 0) || (this.m_dataregionScopes != null && this.m_dataregionScopes.ContainsKey(targetScope)) || (this.m_groupingScopesForRunningValuesInTablix != null && this.m_groupingScopesForRunningValuesInTablix.ContainsScope(targetScope));
		}

		// Token: 0x0600482C RID: 18476 RVA: 0x00130054 File Offset: 0x0012E254
		internal bool IsSameOrChildScope(string parentScope, string childScope)
		{
			if (parentScope == childScope)
			{
				return true;
			}
			if (this.m_datasetScopes.ContainsKey(parentScope))
			{
				if (((InitializationContext.ScopeInfo)this.m_datasetScopes[parentScope]).DataSetScope == this.m_reportScopeDatasets[childScope])
				{
					return true;
				}
			}
			else if (this.m_dataregionScopes.ContainsKey(parentScope))
			{
				ReportItem reportItem = null;
				ReportItem reportItem2 = null;
				if (this.m_dataregionScopes.ContainsKey(childScope))
				{
					reportItem2 = ((InitializationContext.ScopeInfo)this.m_dataregionScopes[childScope]).DataRegionScope;
					reportItem = ((InitializationContext.ScopeInfo)this.m_dataregionScopes[parentScope]).DataRegionScope;
				}
				else if (this.m_groupingScopes.ContainsKey(childScope))
				{
					reportItem = ((InitializationContext.ScopeInfo)this.m_dataregionScopes[parentScope]).DataRegionScope;
					reportItem2 = ((InitializationContext.ScopeInfo)this.m_groupingScopes[childScope]).GroupingScope.Owner.DataRegionDef;
					if (reportItem2 == reportItem)
					{
						return true;
					}
				}
				while (reportItem2.Parent != null)
				{
					if (reportItem2.Parent == reportItem)
					{
						return true;
					}
					reportItem2 = reportItem2.Parent;
				}
			}
			else if (this.m_groupingScopes.ContainsKey(parentScope))
			{
				if (this.m_dataregionScopes.ContainsKey(childScope))
				{
					ReportItem reportItem3 = ((InitializationContext.ScopeInfo)this.m_dataregionScopes[childScope]).DataRegionScope;
					ReportItem dataRegionDef = ((InitializationContext.ScopeInfo)this.m_groupingScopes[parentScope]).GroupingScope.Owner.DataRegionDef;
					while (reportItem3.Parent != null)
					{
						if (reportItem3.Parent == dataRegionDef)
						{
							return true;
						}
						reportItem3 = reportItem3.Parent;
					}
				}
				else if (this.m_groupingScopes.ContainsKey(childScope))
				{
					return this.GetScopeChainInfo().IsSameOrChildScope(parentScope, childScope);
				}
			}
			return false;
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x001301F3 File Offset: 0x0012E3F3
		internal bool IsCurrentScope(string targetScope)
		{
			return targetScope == this.GetCurrentScopeName();
		}

		// Token: 0x0600482E RID: 18478 RVA: 0x00130204 File Offset: 0x0012E404
		internal bool HasPeerGroups(DataRegion dataRegion)
		{
			bool flag;
			return this.HasPeerGroups(dataRegion.RowMembers, out flag) || this.HasPeerGroups(dataRegion.ColumnMembers, out flag);
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x00130234 File Offset: 0x0012E434
		private bool HasPeerGroups(HierarchyNodeList nodes, out bool hasGroup)
		{
			hasGroup = false;
			if (nodes != null)
			{
				foreach (object obj in nodes)
				{
					ReportHierarchyNode reportHierarchyNode = (ReportHierarchyNode)obj;
					bool flag = false;
					if (reportHierarchyNode.InnerHierarchy != null && this.HasPeerGroups(reportHierarchyNode.InnerHierarchy, out flag))
					{
						hasGroup = true;
						return true;
					}
					if (reportHierarchyNode.IsGroup || flag)
					{
						if (hasGroup)
						{
							return true;
						}
						hasGroup = true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x001302C8 File Offset: 0x0012E4C8
		internal bool IsPeerScope(string targetScope)
		{
			if (!this.m_hasUserSortPeerScopes)
			{
				return false;
			}
			string currentScopeName = this.GetCurrentScopeName();
			Global.Tracer.Assert(currentScopeName != null && this.m_peerScopes != null, "(null != currentScope && null != m_peerScopes)");
			object obj = this.m_peerScopes[currentScopeName];
			if (obj == null)
			{
				return false;
			}
			int num = (int)obj;
			obj = this.m_peerScopes[targetScope];
			if (obj == null)
			{
				return false;
			}
			int num2 = (int)obj;
			return num == num2;
		}

		// Token: 0x06004831 RID: 18481 RVA: 0x0013033E File Offset: 0x0012E53E
		internal bool IsReportTopLevelScope()
		{
			return this.m_currentScope == null || this.m_currentScope.IsTopLevelScope;
		}

		// Token: 0x06004832 RID: 18482 RVA: 0x00130355 File Offset: 0x0012E555
		internal ISortFilterScope GetSortFilterScope()
		{
			return this.GetSortFilterScope(this.GetCurrentScopeName());
		}

		// Token: 0x06004833 RID: 18483 RVA: 0x00130363 File Offset: 0x0012E563
		internal ISortFilterScope GetSortFilterScope(string scopeName)
		{
			Global.Tracer.Assert(scopeName != null && "0_ReportScope" != scopeName && this.m_reportScopes.ContainsKey(scopeName));
			return this.m_reportScopes[scopeName];
		}

		// Token: 0x06004834 RID: 18484 RVA: 0x0013039C File Offset: 0x0012E59C
		internal void RegisterPeerScopes(ReportItemCollection reportItems)
		{
			int num = this.m_lastPeerScopeId + 1;
			this.m_lastPeerScopeId = num;
			this.RegisterPeerScopes(reportItems, num, true);
		}

		// Token: 0x06004835 RID: 18485 RVA: 0x001303C4 File Offset: 0x0012E5C4
		private void RegisterPeerScopes(TablixMemberList members, int scopeID)
		{
			if (members == null)
			{
				return;
			}
			foreach (object obj in members)
			{
				TablixMember tablixMember = (TablixMember)obj;
				if (tablixMember.Grouping == null)
				{
					if (tablixMember.TablixHeader != null && tablixMember.TablixHeader.CellContents != null)
					{
						this.RegisterPeerScope(tablixMember.TablixHeader.CellContents, scopeID, false);
						if (tablixMember.TablixHeader.AltCellContents != null)
						{
							this.RegisterPeerScope(tablixMember.TablixHeader.AltCellContents, scopeID, false);
						}
					}
					if (tablixMember.SubMembers != null)
					{
						this.RegisterPeerScopes(tablixMember.SubMembers, scopeID);
					}
				}
			}
		}

		// Token: 0x06004836 RID: 18486 RVA: 0x0013047C File Offset: 0x0012E67C
		private void RegisterPeerScopes(List<List<TablixCornerCell>> cornerCells, int scopeID)
		{
			if (cornerCells == null)
			{
				return;
			}
			foreach (List<TablixCornerCell> list in cornerCells)
			{
				foreach (TablixCornerCell tablixCornerCell in list)
				{
					if (tablixCornerCell.CellContents != null)
					{
						this.RegisterPeerScope(tablixCornerCell.CellContents, scopeID, false);
						if (tablixCornerCell.AltCellContents != null)
						{
							this.RegisterPeerScope(tablixCornerCell.AltCellContents, scopeID, false);
						}
					}
				}
			}
		}

		// Token: 0x06004837 RID: 18487 RVA: 0x00130528 File Offset: 0x0012E728
		private void RegisterPeerScopes(ReportItemCollection reportItems, int scopeID, bool traverse)
		{
			if (reportItems == null || !this.m_hasUserSortPeerScopes)
			{
				return;
			}
			string currentScopeName = this.GetCurrentScopeName();
			if (this.m_peerScopes.ContainsKey(currentScopeName))
			{
				return;
			}
			this.InternalRegisterPeerScopes(reportItems, scopeID, traverse);
			if (!this.m_peerScopes.ContainsKey(currentScopeName))
			{
				this.m_peerScopes.Add(currentScopeName, scopeID);
			}
		}

		// Token: 0x06004838 RID: 18488 RVA: 0x00130580 File Offset: 0x0012E780
		private void InternalRegisterPeerScopes(ReportItemCollection reportItems, int scopeID, bool traverse)
		{
			if (reportItems == null)
			{
				return;
			}
			int count = reportItems.Count;
			for (int i = 0; i < count; i++)
			{
				ReportItem reportItem = reportItems[i];
				this.RegisterPeerScope(reportItem, scopeID, traverse);
			}
		}

		// Token: 0x06004839 RID: 18489 RVA: 0x001305B8 File Offset: 0x0012E7B8
		private void RegisterPeerScope(ReportItem item, int scopeID, bool traverse)
		{
			if (item is Rectangle)
			{
				this.InternalRegisterPeerScopes(((Rectangle)item).ReportItems, scopeID, traverse);
			}
			else if (item.IsDataRegion && !this.m_peerScopes.ContainsKey(item.Name))
			{
				this.m_peerScopes.Add(item.Name, scopeID);
			}
			if (traverse && item is Tablix)
			{
				this.RegisterPeerScopes(((Tablix)item).Corner, scopeID);
				this.RegisterPeerScopes(((Tablix)item).TablixColumnMembers, scopeID);
				this.RegisterPeerScopes(((Tablix)item).TablixRowMembers, scopeID);
			}
		}

		// Token: 0x0600483A RID: 18490 RVA: 0x00130658 File Offset: 0x0012E858
		private void RegisterUserSortInnerScope(IInScopeEventSource eventSource)
		{
			string sortExpressionScopeString = eventSource.UserSort.SortExpressionScopeString;
			if (this.m_groupingScopes.ContainsKey(sortExpressionScopeString) && ((InitializationContext.ScopeInfo)this.m_groupingScopes[sortExpressionScopeString]).GroupingScope.IsDetail)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidSortExpressionScope, Severity.Error, eventSource.ObjectType, eventSource.Name, "SortExpressionScope", new string[] { sortExpressionScopeString });
				eventSource.UserSort.SortExpressionScopeString = null;
				return;
			}
			List<IInScopeEventSource> list;
			if (this.m_userSortExpressionScopes.TryGetValue(sortExpressionScopeString, out list))
			{
				list.Add(eventSource);
				return;
			}
			ISortFilterScope sortFilterScope = null;
			if (this.m_reportScopes.TryGetValue(sortExpressionScopeString, out sortFilterScope) && sortFilterScope is Grouping && ((Grouping)sortFilterScope).DomainScope != null)
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsInvalidSortExpressionScopeDomainScope, Severity.Error, eventSource.ObjectType, eventSource.Name, "SortExpressionScope", new string[] { sortExpressionScopeString });
			}
			list = new List<IInScopeEventSource>();
			list.Add(eventSource);
			this.m_userSortExpressionScopes.Add(sortExpressionScopeString, list);
		}

		// Token: 0x0600483B RID: 18491 RVA: 0x0013075C File Offset: 0x0012E95C
		private void UnregisterUserSortInnerScope(string sortExpressionScopeString, IInScopeEventSource eventSource)
		{
			List<IInScopeEventSource> list;
			if (this.m_userSortExpressionScopes.TryGetValue(sortExpressionScopeString, out list))
			{
				list.Remove(eventSource);
			}
		}

		// Token: 0x0600483C RID: 18492 RVA: 0x00130784 File Offset: 0x0012E984
		internal void ProcessUserSortScopes(string scopeName)
		{
			if (!this.m_hasUserSorts)
			{
				return;
			}
			List<IInScopeEventSource> list;
			if (this.m_userSortExpressionScopes.TryGetValue(scopeName, out list))
			{
				for (int i = list.Count - 1; i >= 0; i--)
				{
					IInScopeEventSource inScopeEventSource = list[i];
					Global.Tracer.Assert(inScopeEventSource.UserSort != null, "(null != eventSource.UserSort)");
					if (this.m_groupingScopes.ContainsKey(scopeName) && ((InitializationContext.ScopeInfo)this.m_groupingScopes[scopeName]).GroupingScope.IsDetail)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsInvalidSortExpressionScope, Severity.Error, inScopeEventSource.ObjectType, inScopeEventSource.Name, "SortExpressionScope", new string[] { scopeName });
						inScopeEventSource.UserSort.SortExpressionScopeString = null;
					}
					else
					{
						inScopeEventSource.ScopeChainInfo = this.GetScopeChainInfo();
						inScopeEventSource.UserSort.SortExpressionScope = this.GetSortFilterScope(inScopeEventSource.UserSort.SortExpressionScopeString);
						this.InitializeSortExpression(inScopeEventSource, false);
					}
					list.RemoveAt(i);
				}
				this.m_userSortExpressionScopes.Remove(scopeName);
			}
			if (this.m_userSortEventSources.TryGetValue(scopeName, out list))
			{
				for (int j = list.Count - 1; j >= 0; j--)
				{
					IInScopeEventSource inScopeEventSource2 = list[j];
					Global.Tracer.Assert(inScopeEventSource2.UserSort != null, "(null != eventSource.UserSort)");
					if (inScopeEventSource2.UserSort.SortTarget != null)
					{
						inScopeEventSource2.UserSort.DataSet = (DataSet)this.m_reportScopeDatasets[inScopeEventSource2.UserSort.SortTarget.ScopeName];
						if (inScopeEventSource2.UserSort.SortExpressionScopeString != null)
						{
							if (inScopeEventSource2.UserSort.SortExpressionScope != null)
							{
								if (this.m_reportScopeDatasets[inScopeEventSource2.UserSort.SortExpressionScope.ScopeName] != inScopeEventSource2.UserSort.DataSet)
								{
									this.m_errorContext.Register(ProcessingErrorCode.rsInvalidExpressionScopeDataSet, Severity.Error, inScopeEventSource2.ObjectType, inScopeEventSource2.Name, "SortExpressionScope", new string[]
									{
										inScopeEventSource2.UserSort.SortExpressionScope.ScopeName,
										"SortTarget"
									});
								}
								else
								{
									InitializationContext.ScopeChainInfo scopeChainInfo = this.GetScopeChainInfo();
									Grouping grouping = null;
									if (scopeChainInfo != null)
									{
										grouping = scopeChainInfo.GetInnermostGrouping();
									}
									if (inScopeEventSource2.ScopeChainInfo != null)
									{
										inScopeEventSource2.UserSort.GroupsInSortTarget = inScopeEventSource2.ScopeChainInfo.GetGroupsFromCurrentTablixAxisToGrouping(grouping);
									}
									if (inScopeEventSource2.ContainingScopes != null)
									{
										int num = inScopeEventSource2.ContainingScopes.Count - 1;
										if (0 <= num && string.CompareOrdinal(inScopeEventSource2.UserSort.SortExpressionScopeString, inScopeEventSource2.ContainingScopes[num].Name) == 0)
										{
											this.m_errorContext.Register(ProcessingErrorCode.rsIneffectiveSortExpressionScope, Severity.Warning, inScopeEventSource2.ObjectType, inScopeEventSource2.Name, "SortExpressionScope", new string[] { inScopeEventSource2.UserSort.SortExpressionScopeString });
										}
									}
								}
							}
							else
							{
								this.m_errorContext.Register(ProcessingErrorCode.rsInvalidExpressionScope, Severity.Error, inScopeEventSource2.ObjectType, inScopeEventSource2.Name, "SortExpressionScope", new string[] { inScopeEventSource2.UserSort.SortExpressionScopeString });
							}
						}
						if (!this.m_errorContext.HasError)
						{
							this.AddToScopeSortFilterList(inScopeEventSource2);
						}
					}
					list.RemoveAt(j);
				}
				this.m_userSortEventSources.Remove(scopeName);
			}
		}

		// Token: 0x0600483D RID: 18493 RVA: 0x00130ACC File Offset: 0x0012ECCC
		internal void RegisterSortEventSource(IInScopeEventSource eventSource)
		{
			if (!this.m_hasUserSorts || eventSource == null || eventSource.UserSort == null)
			{
				return;
			}
			if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegionCellTopLevelItem) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				eventSource.IsTablixCellScope = this.IsDataRegionCellScope;
			}
			string sortExpressionScopeString = eventSource.UserSort.SortExpressionScopeString;
			if (sortExpressionScopeString != null && this.IsScope(sortExpressionScopeString))
			{
				this.RegisterUserSortInnerScope(eventSource);
			}
			string sortTargetString = eventSource.UserSort.SortTargetString;
			if (sortTargetString != null && this.IsScope(sortTargetString))
			{
				this.RegisterUserSortWithSortTarget(eventSource);
			}
		}

		// Token: 0x0600483E RID: 18494 RVA: 0x00130B48 File Offset: 0x0012ED48
		internal void ProcessSortEventSource(IInScopeEventSource eventSource)
		{
			if (!this.m_initializingUserSorts || !this.m_hasUserSorts || eventSource == null || eventSource.UserSort == null)
			{
				return;
			}
			this.AddEventSourceToScope(eventSource);
			GroupingList containingScopes = this.GetContainingScopes();
			for (int i = 0; i < containingScopes.Count; i++)
			{
				containingScopes[i].SaveGroupExprValues = true;
			}
			if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDetail) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				containingScopes.Add(null);
				this.SetDataSetDetailUserSortFilter();
			}
			eventSource.ContainingScopes = containingScopes;
			string sortExpressionScopeString = eventSource.UserSort.SortExpressionScopeString;
			if (sortExpressionScopeString == null)
			{
				this.EventSourceWithDetailSortExpressionAdd(eventSource);
			}
			else if (this.IsScope(sortExpressionScopeString))
			{
				if (this.IsCurrentScope(sortExpressionScopeString))
				{
					eventSource.UserSort.SortExpressionScope = this.GetSortFilterScope(sortExpressionScopeString);
					eventSource.ScopeChainInfo = this.GetScopeChainInfo();
					this.InitializeSortExpression(eventSource, false);
					this.UnregisterUserSortInnerScope(sortExpressionScopeString, eventSource);
				}
				else if (this.IsAncestorScope(sortExpressionScopeString))
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidExpressionScope, Severity.Error, this.m_objectType, this.m_objectName, "SortExpressionScope", new string[] { sortExpressionScopeString });
					this.UnregisterUserSortInnerScope(sortExpressionScopeString, eventSource);
				}
				else if (!this.m_scopeTree.IsSameOrProperParentScope(this.m_currentScope.DataScope, this.m_scopeTree.GetScopeByName(sortExpressionScopeString)))
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidExpressionScope, Severity.Warning, eventSource.ObjectType, eventSource.Name, "SortExpressionScope", new string[] { sortExpressionScopeString });
				}
			}
			else
			{
				this.m_errorContext.Register(ProcessingErrorCode.rsNonExistingScope, Severity.Error, this.m_objectType, this.m_objectName, "SortExpressionScope", new string[] { sortExpressionScopeString });
			}
			string sortTargetString = eventSource.UserSort.SortTargetString;
			if (sortTargetString != null)
			{
				if (!this.IsScope(sortTargetString))
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsNonExistingScope, Severity.Error, this.m_objectType, this.m_objectName, "SortTarget", new string[] { sortTargetString });
					return;
				}
				if (!this.IsCurrentScope(sortTargetString) && !this.IsAncestorScope(sortTargetString) && !this.IsPeerScope(sortTargetString))
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidTargetScope, Severity.Error, this.m_objectType, this.m_objectName, "SortTarget", new string[] { sortTargetString });
					this.UnregisterUserSortWithSortTarget(sortTargetString, eventSource);
					return;
				}
				bool flag = false;
				if (this.m_groupingScopesForRunningValuesInTablix != null && (this.m_groupingScopesForRunningValuesInTablix.HasRowColScopeConflict(eventSource.Scope, sortTargetString, out flag) || (flag && ((InitializationContext.ScopeInfo)this.m_groupingScopes[eventSource.Scope]).GroupingScope.Owner.DataRegionDef != ((InitializationContext.ScopeInfo)this.m_groupingScopes[sortTargetString]).GroupingScope.Owner.DataRegionDef)))
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidTargetScope, Severity.Error, this.m_objectType, this.m_objectName, "SortTarget", new string[] { sortTargetString });
					this.UnregisterUserSortWithSortTarget(sortTargetString, eventSource);
					return;
				}
			}
			else
			{
				if (this.IsReportTopLevelScope())
				{
					this.m_errorContext.Register(ProcessingErrorCode.rsInvalidOmittedTargetScope, Severity.Error, this.m_objectType, this.m_objectName, "SortTarget", Array.Empty<string>());
					return;
				}
				this.RegisterUserSortWithSortTarget(eventSource);
			}
		}

		// Token: 0x0600483F RID: 18495 RVA: 0x00130E5C File Offset: 0x0012F05C
		private void RegisterUserSortWithSortTarget(IInScopeEventSource eventSource)
		{
			string text = eventSource.UserSort.SortTargetString;
			if (text == null)
			{
				text = this.GetCurrentScopeName();
			}
			List<IInScopeEventSource> list;
			if (this.m_userSortEventSources.TryGetValue(text, out list))
			{
				Global.Tracer.Assert(!list.Contains(eventSource), "(false == registeredEventSources.Contains(eventSource))");
				list.Add(eventSource);
				return;
			}
			list = new List<IInScopeEventSource>();
			list.Add(eventSource);
			this.m_userSortEventSources.Add(text, list);
		}

		// Token: 0x06004840 RID: 18496 RVA: 0x00130ECC File Offset: 0x0012F0CC
		private void UnregisterUserSortWithSortTarget(string sortTarget, IInScopeEventSource eventSource)
		{
			List<IInScopeEventSource> list;
			if (this.m_userSortEventSources.TryGetValue(sortTarget, out list))
			{
				Global.Tracer.Assert(list.Contains(eventSource), "(registeredEventSources.Contains(eventSource))");
				list.Remove(eventSource);
			}
		}

		// Token: 0x06004841 RID: 18497 RVA: 0x00130F08 File Offset: 0x0012F108
		internal GroupingList GetContainingScopesInCurrentDataRegion()
		{
			InitializationContext.ScopeChainInfo scopeChainInfo = this.GetScopeChainInfo();
			if (scopeChainInfo != null)
			{
				return scopeChainInfo.GetGroupingListForContainingDataRegion();
			}
			return null;
		}

		// Token: 0x06004842 RID: 18498 RVA: 0x00130F28 File Offset: 0x0012F128
		internal GroupingList GetContainingScopes()
		{
			InitializationContext.ScopeChainInfo scopeChainInfo = this.GetScopeChainInfo();
			if (scopeChainInfo != null)
			{
				return scopeChainInfo.GetGroupingList();
			}
			return new GroupingList();
		}

		// Token: 0x06004843 RID: 18499 RVA: 0x00130F4B File Offset: 0x0012F14B
		private InitializationContext.ScopeChainInfo GetScopeChainInfo()
		{
			if (this.m_groupingScopesForRunningValuesInTablix != null)
			{
				return this.m_groupingScopesForRunningValuesInTablix.GetScopeChainInfo();
			}
			return null;
		}

		// Token: 0x06004844 RID: 18500 RVA: 0x00130F64 File Offset: 0x0012F164
		private void AddEventSourceToScope(IInScopeEventSource eventSource)
		{
			eventSource.Scope = this.GetCurrentScopeName();
			IRIFReportScope irifreportScope;
			if ((this.m_location & Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion) != (Microsoft.ReportingServices.ReportPublishing.LocationFlags)0)
			{
				if (eventSource.IsTablixCellScope)
				{
					irifreportScope = this.m_currentScope.ReportScope;
				}
				else if (this.m_currentScope.DataRegionScope != null || this.m_currentScope.GroupingScope != null)
				{
					irifreportScope = this.m_currentScope.ReportScope;
				}
				else
				{
					irifreportScope = ((InitializationContext.ScopeInfo)this.m_groupingScopes[eventSource.Scope]).ReportScope;
				}
			}
			else
			{
				irifreportScope = this.m_report;
			}
			irifreportScope.AddInScopeEventSource(eventSource);
			this.m_report.AddEventSource(eventSource);
		}

		// Token: 0x06004845 RID: 18501 RVA: 0x00131000 File Offset: 0x0012F200
		private void InitializeSortExpression(IInScopeEventSource eventSource, bool needsExplicitAggregateScope)
		{
			EndUserSort userSort = eventSource.UserSort;
			if (userSort != null && userSort.SortExpression != null)
			{
				bool flag = true;
				if (needsExplicitAggregateScope && userSort.SortExpression.Aggregates != null)
				{
					int count = userSort.SortExpression.Aggregates.Count;
					for (int i = 0; i < count; i++)
					{
						string text;
						if (!userSort.SortExpression.Aggregates[i].GetScope(out text))
						{
							flag = false;
							this.m_errorContext.Register(ProcessingErrorCode.rsInvalidOmittedExpressionScope, Severity.Error, this.m_objectType, this.m_objectName, "SortExpression", new string[] { "SortExpressionScope" });
						}
					}
				}
				if (flag)
				{
					userSort.SortExpression.Initialize("SortExpression", this);
				}
			}
		}

		// Token: 0x06004846 RID: 18502 RVA: 0x001310C0 File Offset: 0x0012F2C0
		private void AddToScopeSortFilterList(IInScopeEventSource eventSource)
		{
			List<int> peerSortFilters = eventSource.GetPeerSortFilters(true);
			Global.Tracer.Assert(peerSortFilters != null, "(null != peerSorts)");
			peerSortFilters.Add(eventSource.ID);
		}

		// Token: 0x06004847 RID: 18503 RVA: 0x001310F4 File Offset: 0x0012F2F4
		internal void SetDataSetDetailUserSortFilter()
		{
			DataSet dataSet = this.GetDataSet();
			if (dataSet != null)
			{
				dataSet.HasDetailUserSortFilter = true;
			}
		}

		// Token: 0x06004848 RID: 18504 RVA: 0x00131112 File Offset: 0x0012F312
		private void EventSourceWithDetailSortExpressionAdd(IInScopeEventSource eventSource)
		{
			Global.Tracer.Assert(this.m_detailSortExpressionScopeEventSources != null, "(null != m_detailSortExpressionScopeEventSources)");
			this.m_detailSortExpressionScopeEventSources.Add(eventSource);
		}

		// Token: 0x06004849 RID: 18505 RVA: 0x00131138 File Offset: 0x0012F338
		internal void EventSourcesWithDetailSortExpressionInitialize(string sortExpressionScope)
		{
			if (!this.m_hasUserSorts)
			{
				return;
			}
			Global.Tracer.Assert(this.m_detailSortExpressionScopeEventSources != null, "(null != m_detailSortExpressionScopeEventSources)");
			int count = this.m_detailSortExpressionScopeEventSources.Count;
			if (count == 0)
			{
				return;
			}
			for (int i = 0; i < count; i++)
			{
				IInScopeEventSource inScopeEventSource = this.m_detailSortExpressionScopeEventSources[i];
				inScopeEventSource.UserSort.SortExpressionScope = null;
				this.InitializeSortExpression(inScopeEventSource, true);
				if (sortExpressionScope != null && inScopeEventSource.ContainingScopes != null)
				{
					int num = inScopeEventSource.ContainingScopes.Count - 1;
					if (0 <= num && inScopeEventSource.ContainingScopes[num] == null)
					{
						this.m_errorContext.Register(ProcessingErrorCode.rsIneffectiveSortExpressionScope, Severity.Warning, inScopeEventSource.ObjectType, inScopeEventSource.Name, "SortExpressionScope", new string[] { sortExpressionScope });
					}
				}
			}
			this.m_detailSortExpressionScopeEventSources.Clear();
		}

		// Token: 0x0600484A RID: 18506 RVA: 0x00131203 File Offset: 0x0012F403
		internal void InitializeAbsolutePosition(ReportItem reportItem)
		{
			this.m_currentAbsoluteTop += reportItem.AbsoluteTopValue;
			this.m_currentAbsoluteLeft += reportItem.AbsoluteLeftValue;
		}

		// Token: 0x0600484B RID: 18507 RVA: 0x0013122B File Offset: 0x0012F42B
		internal void UpdateTopLeftDataRegion(DataRegion dataRegion)
		{
			this.m_report.UpdateTopLeftDataRegion(this, dataRegion);
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x0013123F File Offset: 0x0012F43F
		internal void AddGroupingExprCountForGroup(string scope, int groupingExprCount)
		{
			if (!this.m_groupingExprCountAtScope.ContainsKey(scope))
			{
				this.m_groupingExprCountAtScope.Add(scope, groupingExprCount);
			}
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x0013125C File Offset: 0x0012F45C
		internal void EnforceRdlSandboxContentRestrictions(CodeClass codeClass)
		{
			if (this.m_rdlSandboxClassValidator != null)
			{
				this.m_rdlSandboxClassValidator.Validate(codeClass);
			}
		}

		// Token: 0x0600484E RID: 18510 RVA: 0x00131272 File Offset: 0x0012F472
		internal void EnforceRdlSandboxContentRestrictions(ExpressionInfo expression, string propertyName)
		{
			this.EnforceRdlSandboxContentRestrictions(expression, this.ObjectType, this.ObjectName, propertyName);
		}

		// Token: 0x0600484F RID: 18511 RVA: 0x00131288 File Offset: 0x0012F488
		internal void EnforceRdlSandboxContentRestrictions(ExpressionInfo expression, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			if (this.m_rdlSandboxExpressionValidator != null)
			{
				this.m_rdlSandboxExpressionValidator.Validate(expression.OriginalText, objectType, objectName, propertyName);
			}
		}

		// Token: 0x04001FF1 RID: 8177
		private PublishingContextBase m_publishingContext;

		// Token: 0x04001FF2 RID: 8178
		private ICatalogItemContext m_reportContext;

		// Token: 0x04001FF3 RID: 8179
		private Microsoft.ReportingServices.ReportPublishing.LocationFlags m_location;

		// Token: 0x04001FF4 RID: 8180
		private Microsoft.ReportingServices.ReportProcessing.ObjectType m_objectType;

		// Token: 0x04001FF5 RID: 8181
		private string m_objectName;

		// Token: 0x04001FF6 RID: 8182
		private string m_tablixName;

		// Token: 0x04001FF7 RID: 8183
		private Dictionary<string, ImageInfo> m_embeddedImages;

		// Token: 0x04001FF8 RID: 8184
		private ErrorContext m_errorContext;

		// Token: 0x04001FF9 RID: 8185
		private Hashtable m_parameters;

		// Token: 0x04001FFA RID: 8186
		private ArrayList m_dynamicParameters;

		// Token: 0x04001FFB RID: 8187
		private Hashtable m_dataSetQueryInfo;

		// Token: 0x04001FFC RID: 8188
		private Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder m_exprHostBuilder;

		// Token: 0x04001FFD RID: 8189
		private Report m_report;

		// Token: 0x04001FFE RID: 8190
		private Dictionary<string, Variable> m_variablesInScope;

		// Token: 0x04001FFF RID: 8191
		private byte[] m_referencableTextboxes;

		// Token: 0x04002000 RID: 8192
		private byte[] m_referencableTextboxesInSection;

		// Token: 0x04002001 RID: 8193
		private byte[] m_referencableVariables;

		// Token: 0x04002002 RID: 8194
		private RdlSandboxValidator.ExpressionValidator m_rdlSandboxExpressionValidator;

		// Token: 0x04002003 RID: 8195
		private RdlSandboxValidator.ClassValidator m_rdlSandboxClassValidator;

		// Token: 0x04002004 RID: 8196
		private string m_outerGroupName;

		// Token: 0x04002005 RID: 8197
		private string m_currentGroupName;

		// Token: 0x04002006 RID: 8198
		private string m_currentDataRegionName;

		// Token: 0x04002007 RID: 8199
		private List<RunningValueInfo> m_runningValues;

		// Token: 0x04002008 RID: 8200
		private List<RunningValueInfo> m_runningValuesOfAggregates;

		// Token: 0x04002009 RID: 8201
		private Hashtable m_groupingScopesForRunningValues;

		// Token: 0x0400200A RID: 8202
		private InitializationContext.GroupingScopesForTablix m_groupingScopesForRunningValuesInTablix;

		// Token: 0x0400200B RID: 8203
		private Hashtable m_dataregionScopesForRunningValues;

		// Token: 0x0400200C RID: 8204
		private InitializationContext.AxisGroupingScopesForRunningValues m_axisGroupingScopesForRunningValues;

		// Token: 0x0400200D RID: 8205
		private Dictionary<string, int> m_groupingExprCountAtScope;

		// Token: 0x0400200E RID: 8206
		private bool m_hasFilters;

		// Token: 0x0400200F RID: 8207
		private InitializationContext.ScopeInfo m_currentScope;

		// Token: 0x04002010 RID: 8208
		private InitializationContext.ScopeInfo m_outermostDataregionScope;

		// Token: 0x04002011 RID: 8209
		private Hashtable m_groupingScopes;

		// Token: 0x04002012 RID: 8210
		private Hashtable m_dataregionScopes;

		// Token: 0x04002013 RID: 8211
		private Hashtable m_datasetScopes;

		// Token: 0x04002014 RID: 8212
		private ScopeTree m_scopeTree;

		// Token: 0x04002015 RID: 8213
		private Dictionary<int, IRIFDataScope> m_dataSetsForNonStructuralIdc;

		// Token: 0x04002016 RID: 8214
		private Dictionary<int, IRIFDataScope> m_dataSetsForIdcInNestedDR;

		// Token: 0x04002017 RID: 8215
		private Dictionary<int, IRIFDataScope> m_dataSetsForIdc;

		// Token: 0x04002018 RID: 8216
		private int m_numberOfDataSets;

		// Token: 0x04002019 RID: 8217
		private string m_oneDataSetName;

		// Token: 0x0400201A RID: 8218
		private FunctionalList<DataSet> m_activeDataSets;

		// Token: 0x0400201B RID: 8219
		private FunctionalList<InitializationContext.ScopeInfo> m_activeScopeInfos;

		// Token: 0x0400201C RID: 8220
		private Hashtable m_fieldNameMap;

		// Token: 0x0400201D RID: 8221
		private Dictionary<string, List<DataRegion>> m_dataSetNameToDataRegionsMap;

		// Token: 0x0400201E RID: 8222
		private Dictionary<string, InitializationContext.LookupDestinationCompactionTable> m_lookupCompactionTable;

		// Token: 0x0400201F RID: 8223
		private StringDictionary m_dataSources;

		// Token: 0x04002020 RID: 8224
		private Dictionary<string, Pair<ReportItem, int>> m_reportItemsInScope;

		// Token: 0x04002021 RID: 8225
		private Dictionary<string, ReportItem> m_reportItemsInSection;

		// Token: 0x04002022 RID: 8226
		private Holder<bool> m_handledCellContents;

		// Token: 0x04002023 RID: 8227
		private CultureInfo m_reportLanguage;

		// Token: 0x04002024 RID: 8228
		private bool m_reportDataElementStyleAttribute;

		// Token: 0x04002025 RID: 8229
		private bool m_hasUserSortPeerScopes;

		// Token: 0x04002026 RID: 8230
		private bool m_hasUserSorts;

		// Token: 0x04002027 RID: 8231
		private Dictionary<string, List<IInScopeEventSource>> m_userSortExpressionScopes;

		// Token: 0x04002028 RID: 8232
		private Dictionary<string, List<IInScopeEventSource>> m_userSortEventSources;

		// Token: 0x04002029 RID: 8233
		private Hashtable m_peerScopes;

		// Token: 0x0400202A RID: 8234
		private int m_lastPeerScopeId;

		// Token: 0x0400202B RID: 8235
		private Dictionary<string, ISortFilterScope> m_reportScopes;

		// Token: 0x0400202C RID: 8236
		private Hashtable m_reportScopeDatasets;

		// Token: 0x0400202D RID: 8237
		private bool m_initializingUserSorts;

		// Token: 0x0400202E RID: 8238
		private List<IInScopeEventSource> m_detailSortExpressionScopeEventSources;

		// Token: 0x0400202F RID: 8239
		private IList<Pair<double, int>> m_columnHeaderLevelSizeList;

		// Token: 0x04002030 RID: 8240
		private IList<Pair<double, int>> m_rowHeaderLevelSizeList;

		// Token: 0x04002031 RID: 8241
		private bool m_hasPreviousAggregates;

		// Token: 0x04002032 RID: 8242
		private bool m_inAutoSubtotalClone;

		// Token: 0x04002033 RID: 8243
		private List<VisibilityToggleInfo> m_visibilityToggleInfos;

		// Token: 0x04002034 RID: 8244
		private Dictionary<string, ToggleItemInfo> m_toggleItems;

		// Token: 0x04002035 RID: 8245
		private Stack<InitializationContext.VisibilityContainmentInfo> m_visibilityContainmentInfos;

		// Token: 0x04002036 RID: 8246
		private bool m_isTopLevelCellContents;

		// Token: 0x04002037 RID: 8247
		private bool m_isDataRegionScopedCell;

		// Token: 0x04002038 RID: 8248
		private bool m_inRecursiveHierarchyRows;

		// Token: 0x04002039 RID: 8249
		private bool m_inRecursiveHierarchyColumns;

		// Token: 0x0400203A RID: 8250
		private Holder<int> m_memberCellIndex;

		// Token: 0x0400203B RID: 8251
		private Dictionary<Hashtable, int> m_indexInCollectionTableForDataRegions;

		// Token: 0x0400203C RID: 8252
		private Dictionary<Hashtable, int> m_indexInCollectionTableForSubReports;

		// Token: 0x0400203D RID: 8253
		private Dictionary<Hashtable, int> m_indexInCollectionTable;

		// Token: 0x0400203E RID: 8254
		private Dictionary<string, List<ExpressionInfo>> m_naturalGroupExpressionsByDataSetName;

		// Token: 0x0400203F RID: 8255
		private Dictionary<string, List<ExpressionInfo>> m_naturalSortExpressionsByDataSetName;

		// Token: 0x04002040 RID: 8256
		private double m_currentAbsoluteTop;

		// Token: 0x04002041 RID: 8257
		private double m_currentAbsoluteLeft;

		// Token: 0x02000989 RID: 2441
		private enum GroupingType
		{
			// Token: 0x04004165 RID: 16741
			Normal,
			// Token: 0x04004166 RID: 16742
			TablixRow,
			// Token: 0x04004167 RID: 16743
			TablixColumn
		}

		// Token: 0x0200098A RID: 2442
		private sealed class VisibilityContainmentInfo
		{
			// Token: 0x0600808F RID: 32911 RVA: 0x002118A5 File Offset: 0x0020FAA5
			internal VisibilityContainmentInfo()
			{
			}

			// Token: 0x04004168 RID: 16744
			internal IVisibilityOwner ContainingVisibility;

			// Token: 0x04004169 RID: 16745
			internal IVisibilityOwner ContainingRowVisibility;

			// Token: 0x0400416A RID: 16746
			internal IVisibilityOwner ContainingColumnVisibility;
		}

		// Token: 0x0200098B RID: 2443
		private sealed class ScopeInfo
		{
			// Token: 0x06008090 RID: 32912 RVA: 0x002118AD File Offset: 0x0020FAAD
			internal ScopeInfo(bool allowCustomAggregates, List<DataAggregateInfo> aggregates, IRIFReportScope reportScope)
				: this(allowCustomAggregates, aggregates, null, null, null, reportScope)
			{
				this.m_isTopLevelScope = true;
			}

			// Token: 0x06008091 RID: 32913 RVA: 0x002118C2 File Offset: 0x0020FAC2
			internal ScopeInfo(bool allowCustomAggregates, List<DataAggregateInfo> aggregates, List<DataAggregateInfo> postSortAggregates, IRIFReportScope reportScope)
				: this(allowCustomAggregates, aggregates, postSortAggregates, null, null, reportScope)
			{
			}

			// Token: 0x06008092 RID: 32914 RVA: 0x002118D1 File Offset: 0x0020FAD1
			internal ScopeInfo(bool allowCustomAggregates, List<DataAggregateInfo> aggregates, List<DataAggregateInfo> postSortAggregates, DataRegion dataRegion)
				: this(allowCustomAggregates, aggregates, postSortAggregates, null, null, dataRegion)
			{
				this.m_dataRegionScope = dataRegion;
			}

			// Token: 0x06008093 RID: 32915 RVA: 0x002118E8 File Offset: 0x0020FAE8
			internal ScopeInfo(bool allowCustomAggregates, List<DataAggregateInfo> aggregates, List<DataAggregateInfo> postSortAggregates, DataSet dataset, bool duplicateScope)
				: this(allowCustomAggregates, aggregates, postSortAggregates, dataset)
			{
				this.m_duplicateScope = duplicateScope;
			}

			// Token: 0x06008094 RID: 32916 RVA: 0x002118FD File Offset: 0x0020FAFD
			internal ScopeInfo(bool allowCustomAggregates, List<DataAggregateInfo> aggregates, List<DataAggregateInfo> postSortAggregates, DataSet dataset)
				: this(allowCustomAggregates, aggregates, postSortAggregates, null, null, null)
			{
				this.m_dataSetScope = dataset;
			}

			// Token: 0x06008095 RID: 32917 RVA: 0x00211913 File Offset: 0x0020FB13
			internal ScopeInfo(bool allowCustomAggregates, List<DataAggregateInfo> aggregates, List<DataAggregateInfo> postSortAggregates, List<DataAggregateInfo> recursiveAggregates, Grouping groupingScope, IRIFReportScope reportScope)
			{
				this.m_allowCustomAggregates = allowCustomAggregates;
				this.m_aggregates = aggregates;
				this.m_postSortAggregates = postSortAggregates;
				this.m_recursiveAggregates = recursiveAggregates;
				this.m_reportScope = reportScope;
				this.m_groupingScope = groupingScope;
			}

			// Token: 0x17002995 RID: 10645
			// (get) Token: 0x06008096 RID: 32918 RVA: 0x00211948 File Offset: 0x0020FB48
			internal bool AllowCustomAggregates
			{
				get
				{
					return this.m_allowCustomAggregates;
				}
			}

			// Token: 0x17002996 RID: 10646
			// (get) Token: 0x06008097 RID: 32919 RVA: 0x00211950 File Offset: 0x0020FB50
			internal List<DataAggregateInfo> Aggregates
			{
				get
				{
					return this.m_aggregates;
				}
			}

			// Token: 0x17002997 RID: 10647
			// (get) Token: 0x06008098 RID: 32920 RVA: 0x00211958 File Offset: 0x0020FB58
			internal List<DataAggregateInfo> PostSortAggregates
			{
				get
				{
					return this.m_postSortAggregates;
				}
			}

			// Token: 0x17002998 RID: 10648
			// (get) Token: 0x06008099 RID: 32921 RVA: 0x00211960 File Offset: 0x0020FB60
			internal List<DataAggregateInfo> RecursiveAggregates
			{
				get
				{
					return this.m_recursiveAggregates;
				}
			}

			// Token: 0x17002999 RID: 10649
			// (get) Token: 0x0600809A RID: 32922 RVA: 0x00211968 File Offset: 0x0020FB68
			internal IRIFReportScope ReportScope
			{
				get
				{
					return this.m_reportScope;
				}
			}

			// Token: 0x1700299A RID: 10650
			// (get) Token: 0x0600809B RID: 32923 RVA: 0x00211970 File Offset: 0x0020FB70
			internal Grouping GroupingScope
			{
				get
				{
					return this.m_groupingScope;
				}
			}

			// Token: 0x1700299B RID: 10651
			// (get) Token: 0x0600809C RID: 32924 RVA: 0x00211978 File Offset: 0x0020FB78
			internal DataRegion DataRegionScope
			{
				get
				{
					return this.m_dataRegionScope;
				}
			}

			// Token: 0x1700299C RID: 10652
			// (get) Token: 0x0600809D RID: 32925 RVA: 0x00211980 File Offset: 0x0020FB80
			internal DataSet DataSetScope
			{
				get
				{
					return this.m_dataSetScope;
				}
			}

			// Token: 0x1700299D RID: 10653
			// (get) Token: 0x0600809E RID: 32926 RVA: 0x00211988 File Offset: 0x0020FB88
			internal IRIFDataScope DataScope
			{
				get
				{
					if (this.m_groupingScope != null)
					{
						return this.m_groupingScope.Owner;
					}
					if (this.m_dataRegionScope != null)
					{
						return this.m_dataRegionScope;
					}
					if (this.m_dataSetScope != null)
					{
						return this.m_dataSetScope;
					}
					return this.m_reportScope as IRIFDataScope;
				}
			}

			// Token: 0x1700299E RID: 10654
			// (get) Token: 0x0600809F RID: 32927 RVA: 0x002119C8 File Offset: 0x0020FBC8
			internal DataScopeInfo DataScopeInfo
			{
				get
				{
					IRIFDataScope dataScope = this.DataScope;
					if (dataScope != null)
					{
						return dataScope.DataScopeInfo;
					}
					return null;
				}
			}

			// Token: 0x1700299F RID: 10655
			// (get) Token: 0x060080A0 RID: 32928 RVA: 0x002119E7 File Offset: 0x0020FBE7
			internal bool IsTopLevelScope
			{
				get
				{
					return this.m_isTopLevelScope;
				}
			}

			// Token: 0x170029A0 RID: 10656
			// (get) Token: 0x060080A1 RID: 32929 RVA: 0x002119EF File Offset: 0x0020FBEF
			internal bool IsDuplicateScope
			{
				get
				{
					return this.m_duplicateScope;
				}
			}

			// Token: 0x0400416B RID: 16747
			private bool m_isTopLevelScope;

			// Token: 0x0400416C RID: 16748
			private bool m_allowCustomAggregates;

			// Token: 0x0400416D RID: 16749
			private List<DataAggregateInfo> m_aggregates;

			// Token: 0x0400416E RID: 16750
			private List<DataAggregateInfo> m_postSortAggregates;

			// Token: 0x0400416F RID: 16751
			private List<DataAggregateInfo> m_recursiveAggregates;

			// Token: 0x04004170 RID: 16752
			private bool m_duplicateScope;

			// Token: 0x04004171 RID: 16753
			private Grouping m_groupingScope;

			// Token: 0x04004172 RID: 16754
			private DataRegion m_dataRegionScope;

			// Token: 0x04004173 RID: 16755
			private DataSet m_dataSetScope;

			// Token: 0x04004174 RID: 16756
			private IRIFReportScope m_reportScope;
		}

		// Token: 0x0200098C RID: 2444
		internal class HashtableKeyComparer : IEqualityComparer<Hashtable>
		{
			// Token: 0x060080A2 RID: 32930 RVA: 0x002119F7 File Offset: 0x0020FBF7
			private HashtableKeyComparer()
			{
			}

			// Token: 0x170029A1 RID: 10657
			// (get) Token: 0x060080A3 RID: 32931 RVA: 0x002119FF File Offset: 0x0020FBFF
			internal static InitializationContext.HashtableKeyComparer Instance
			{
				get
				{
					if (InitializationContext.HashtableKeyComparer.m_Instance == null)
					{
						InitializationContext.HashtableKeyComparer.m_Instance = new InitializationContext.HashtableKeyComparer();
					}
					return InitializationContext.HashtableKeyComparer.m_Instance;
				}
			}

			// Token: 0x060080A4 RID: 32932 RVA: 0x00211A18 File Offset: 0x0020FC18
			public bool Equals(Hashtable x, Hashtable y)
			{
				if (x != y)
				{
					if (x.Count == y.Count)
					{
						using (IEnumerator enumerator = x.Keys.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								if (!y.ContainsKey(obj))
								{
									return false;
								}
							}
							return true;
						}
						return false;
					}
					return false;
				}
				return true;
			}

			// Token: 0x060080A5 RID: 32933 RVA: 0x00211A8C File Offset: 0x0020FC8C
			public int GetHashCode(Hashtable obj)
			{
				int num = obj.Count;
				foreach (object obj2 in obj.Keys)
				{
					num ^= obj2.GetHashCode();
				}
				return num;
			}

			// Token: 0x04004175 RID: 16757
			private static InitializationContext.HashtableKeyComparer m_Instance;
		}

		// Token: 0x0200098D RID: 2445
		private sealed class AxisGroupingScopesForRunningValues
		{
			// Token: 0x060080A6 RID: 32934 RVA: 0x00211AEC File Offset: 0x0020FCEC
			internal AxisGroupingScopesForRunningValues()
			{
			}

			// Token: 0x060080A7 RID: 32935 RVA: 0x00211AF4 File Offset: 0x0020FCF4
			internal AxisGroupingScopesForRunningValues(InitializationContext.AxisGroupingScopesForRunningValues axisGroupingScopesForRunningValues)
			{
				this.m_columnGroupExprCount = axisGroupingScopesForRunningValues.m_columnGroupExprCount;
				this.m_rowGroupExprCount = axisGroupingScopesForRunningValues.m_rowGroupExprCount;
				this.m_colDetailCount = axisGroupingScopesForRunningValues.m_colDetailCount;
				this.m_rowDetailCount = axisGroupingScopesForRunningValues.m_rowDetailCount;
				this.SetCountsForDataRegion();
			}

			// Token: 0x170029A2 RID: 10658
			// (get) Token: 0x060080A8 RID: 32936 RVA: 0x00211B32 File Offset: 0x0020FD32
			internal bool InCurrentDataRegionDynamicRow
			{
				get
				{
					return this.m_rowGroupExprCount > this.m_previousDRsRowGroupExprCount || this.m_rowDetailCount > this.m_previousDRsRowDetailCount;
				}
			}

			// Token: 0x170029A3 RID: 10659
			// (get) Token: 0x060080A9 RID: 32937 RVA: 0x00211B52 File Offset: 0x0020FD52
			internal bool InCurrentDataRegionDynamicColumn
			{
				get
				{
					return this.m_columnGroupExprCount > this.m_previousDRsColumnGroupExprCount || this.m_colDetailCount > this.m_previousDRsColumnDetailCount;
				}
			}

			// Token: 0x060080AA RID: 32938 RVA: 0x00211B72 File Offset: 0x0020FD72
			internal void RegisterColumnGrouping(Grouping grouping)
			{
				if (!grouping.IsDetail)
				{
					this.m_columnGroupExprCount += grouping.GroupExpressions.Count;
					return;
				}
				this.m_colDetailCount++;
			}

			// Token: 0x060080AB RID: 32939 RVA: 0x00211BA3 File Offset: 0x0020FDA3
			internal void RegisterRowGrouping(Grouping grouping)
			{
				if (!grouping.IsDetail)
				{
					this.m_rowGroupExprCount += grouping.GroupExpressions.Count;
					return;
				}
				this.m_rowDetailCount++;
			}

			// Token: 0x060080AC RID: 32940 RVA: 0x00211BD4 File Offset: 0x0020FDD4
			internal void UnregisterColumnGrouping(Grouping grouping)
			{
				if (!grouping.IsDetail)
				{
					this.m_columnGroupExprCount -= grouping.GroupExpressions.Count;
					return;
				}
				this.m_colDetailCount--;
			}

			// Token: 0x060080AD RID: 32941 RVA: 0x00211C05 File Offset: 0x0020FE05
			internal void UnregisterRowGrouping(Grouping grouping)
			{
				if (!grouping.IsDetail)
				{
					this.m_rowGroupExprCount -= grouping.GroupExpressions.Count;
					return;
				}
				this.m_rowDetailCount--;
			}

			// Token: 0x060080AE RID: 32942 RVA: 0x00211C36 File Offset: 0x0020FE36
			private void SetCountsForDataRegion()
			{
				this.m_previousDRsColumnGroupExprCount = this.m_columnGroupExprCount;
				this.m_previousDRsRowGroupExprCount = this.m_rowGroupExprCount;
				this.m_previousDRsColumnDetailCount = this.m_colDetailCount;
				this.m_previousDRsRowDetailCount = this.m_rowDetailCount;
			}

			// Token: 0x04004176 RID: 16758
			private int m_columnGroupExprCount;

			// Token: 0x04004177 RID: 16759
			private int m_rowGroupExprCount;

			// Token: 0x04004178 RID: 16760
			private int m_previousDRsColumnGroupExprCount;

			// Token: 0x04004179 RID: 16761
			private int m_previousDRsRowGroupExprCount;

			// Token: 0x0400417A RID: 16762
			private int m_rowDetailCount;

			// Token: 0x0400417B RID: 16763
			private int m_colDetailCount;

			// Token: 0x0400417C RID: 16764
			private int m_previousDRsColumnDetailCount;

			// Token: 0x0400417D RID: 16765
			private int m_previousDRsRowDetailCount;
		}

		// Token: 0x0200098E RID: 2446
		private sealed class GroupingScopesForTablix
		{
			// Token: 0x060080AF RID: 32943 RVA: 0x00211C68 File Offset: 0x0020FE68
			internal GroupingScopesForTablix(bool forceRows, Microsoft.ReportingServices.ReportProcessing.ObjectType containerType, DataRegion containerScope)
			{
				containerScope.RowScopeFound = forceRows;
				containerScope.ColumnScopeFound = false;
				this.m_containerType = containerType;
				this.m_rowScopes = new Hashtable();
				this.m_columnScopes = new Hashtable();
				this.m_rowScopeStack = FunctionalList<Grouping>.Empty;
				this.m_columnScopeStack = FunctionalList<Grouping>.Empty;
				this.m_containerScope = containerScope;
			}

			// Token: 0x170029A4 RID: 10660
			// (get) Token: 0x060080B0 RID: 32944 RVA: 0x00211CC3 File Offset: 0x0020FEC3
			internal bool CurrentRowScopeIsDetail
			{
				get
				{
					return this.m_rowScopeStack.Count == 0 || this.m_rowScopeStack.First.IsDetail;
				}
			}

			// Token: 0x170029A5 RID: 10661
			// (get) Token: 0x060080B1 RID: 32945 RVA: 0x00211CE4 File Offset: 0x0020FEE4
			internal bool CurrentColumnScopeIsDetail
			{
				get
				{
					return this.m_columnScopeStack.Count == 0 || this.m_columnScopeStack.First.IsDetail;
				}
			}

			// Token: 0x170029A6 RID: 10662
			// (get) Token: 0x060080B2 RID: 32946 RVA: 0x00211D05 File Offset: 0x0020FF05
			internal string CurrentRowScopeName
			{
				get
				{
					if (this.m_rowScopeStack.Count != 0)
					{
						return this.m_rowScopeStack.First.Name;
					}
					return null;
				}
			}

			// Token: 0x170029A7 RID: 10663
			// (get) Token: 0x060080B3 RID: 32947 RVA: 0x00211D26 File Offset: 0x0020FF26
			internal string CurrentColumnScopeName
			{
				get
				{
					if (this.m_columnScopeStack.Count != 0)
					{
						return this.m_columnScopeStack.First.Name;
					}
					return null;
				}
			}

			// Token: 0x170029A8 RID: 10664
			// (get) Token: 0x060080B4 RID: 32948 RVA: 0x00211D47 File Offset: 0x0020FF47
			internal DataRegion ContainerScope
			{
				get
				{
					return this.m_containerScope;
				}
			}

			// Token: 0x170029A9 RID: 10665
			// (get) Token: 0x060080B5 RID: 32949 RVA: 0x00211D4F File Offset: 0x0020FF4F
			internal string ContainerName
			{
				get
				{
					return this.m_containerScope.Name;
				}
			}

			// Token: 0x170029AA RID: 10666
			// (get) Token: 0x060080B6 RID: 32950 RVA: 0x00211D5C File Offset: 0x0020FF5C
			internal bool IsRunningValueDirectionColumn
			{
				get
				{
					return this.m_containerScope.ColumnScopeFound;
				}
			}

			// Token: 0x060080B7 RID: 32951 RVA: 0x00211D69 File Offset: 0x0020FF69
			internal InitializationContext.ScopeChainInfo GetScopeChainInfo()
			{
				return new InitializationContext.ScopeChainInfo(this.m_containerScope, this.m_rowScopeStack, this.m_columnScopeStack);
			}

			// Token: 0x060080B8 RID: 32952 RVA: 0x00211D82 File Offset: 0x0020FF82
			internal DataRegion SetContainerScope(DataRegion dataRegion)
			{
				DataRegion containerScope = this.m_containerScope;
				this.m_containerScope = dataRegion;
				return containerScope;
			}

			// Token: 0x060080B9 RID: 32953 RVA: 0x00211D91 File Offset: 0x0020FF91
			internal void RegisterRowGrouping(Grouping grouping)
			{
				if (!this.m_rowScopes.ContainsKey(grouping.Name))
				{
					this.m_rowScopes[grouping.Name] = null;
					this.m_rowScopeStack = this.m_rowScopeStack.Add(grouping);
				}
			}

			// Token: 0x060080BA RID: 32954 RVA: 0x00211DCA File Offset: 0x0020FFCA
			internal void UnRegisterRowGrouping(string groupName)
			{
				this.m_rowScopes.Remove(groupName);
				this.m_rowScopeStack = this.m_rowScopeStack.Rest;
			}

			// Token: 0x060080BB RID: 32955 RVA: 0x00211DE9 File Offset: 0x0020FFE9
			internal void RegisterColumnGrouping(Grouping grouping)
			{
				if (!this.m_columnScopes.ContainsKey(grouping.Name))
				{
					this.m_columnScopes[grouping.Name] = null;
					this.m_columnScopeStack = this.m_columnScopeStack.Add(grouping);
				}
			}

			// Token: 0x060080BC RID: 32956 RVA: 0x00211E22 File Offset: 0x00210022
			internal void UnRegisterColumnGrouping(string groupName)
			{
				this.m_columnScopes.Remove(groupName);
				this.m_columnScopeStack = this.m_columnScopeStack.Rest;
			}

			// Token: 0x060080BD RID: 32957 RVA: 0x00211E44 File Offset: 0x00210044
			private ProcessingErrorCode getErrorCode()
			{
				Microsoft.ReportingServices.ReportProcessing.ObjectType containerType = this.m_containerType;
				if (containerType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Chart)
				{
					return ProcessingErrorCode.rsConflictingRunningValueScopesInTablix;
				}
				if (containerType == Microsoft.ReportingServices.ReportProcessing.ObjectType.CustomReportItem)
				{
					return ProcessingErrorCode.rsConflictingRunningValueScopesInTablix;
				}
				if (containerType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Tablix)
				{
					return ProcessingErrorCode.rsConflictingRunningValueScopesInTablix;
				}
				Global.Tracer.Assert(false);
				return ProcessingErrorCode.rsConflictingRunningValueScopesInTablix;
			}

			// Token: 0x060080BE RID: 32958 RVA: 0x00211E80 File Offset: 0x00210080
			internal bool HasRowColScopeConflict(string textboxSortActionScope, string sortTargetScope, out bool bothGroups)
			{
				bothGroups = false;
				if (textboxSortActionScope != null && sortTargetScope != null)
				{
					if (this.m_rowScopes.ContainsKey(textboxSortActionScope))
					{
						if (this.m_rowScopes.ContainsKey(sortTargetScope))
						{
							bothGroups = true;
							return false;
						}
						if (this.m_columnScopes.ContainsKey(sortTargetScope))
						{
							bothGroups = true;
							return true;
						}
					}
					else if (this.m_columnScopes.ContainsKey(textboxSortActionScope))
					{
						if (this.m_columnScopes.ContainsKey(sortTargetScope))
						{
							bothGroups = true;
							return false;
						}
						if (this.m_rowScopes.ContainsKey(sortTargetScope))
						{
							bothGroups = true;
							return true;
						}
					}
				}
				return false;
			}

			// Token: 0x060080BF RID: 32959 RVA: 0x00211EFF File Offset: 0x002100FF
			internal bool ContainsScope(string scope)
			{
				return this.ContainsScope(scope, null, false, null);
			}

			// Token: 0x060080C0 RID: 32960 RVA: 0x00211F0C File Offset: 0x0021010C
			internal bool ContainsScope(string scope, ErrorContext errorContext, bool checkConflictingScope, Hashtable groupingScopes)
			{
				Global.Tracer.Assert(scope != null, "(null != scope)");
				if (this.m_rowScopes.ContainsKey(scope))
				{
					if (checkConflictingScope)
					{
						DataRegion dataRegionDef = ((InitializationContext.ScopeInfo)groupingScopes[scope]).GroupingScope.Owner.DataRegionDef;
						if (dataRegionDef.ColumnScopeFound)
						{
							errorContext.Register(this.getErrorCode(), Severity.Error, dataRegionDef.ObjectType, this.ContainerName, null, Array.Empty<string>());
						}
						dataRegionDef.RowScopeFound = true;
					}
					return true;
				}
				if (this.m_columnScopes.ContainsKey(scope))
				{
					if (checkConflictingScope)
					{
						DataRegion dataRegionDef2 = ((InitializationContext.ScopeInfo)groupingScopes[scope]).GroupingScope.Owner.DataRegionDef;
						if (dataRegionDef2.RowScopeFound)
						{
							errorContext.Register(this.getErrorCode(), Severity.Error, dataRegionDef2.ObjectType, this.ContainerName, null, Array.Empty<string>());
						}
						dataRegionDef2.ColumnScopeFound = true;
					}
					return true;
				}
				return false;
			}

			// Token: 0x0400417E RID: 16766
			private Microsoft.ReportingServices.ReportProcessing.ObjectType m_containerType;

			// Token: 0x0400417F RID: 16767
			private DataRegion m_containerScope;

			// Token: 0x04004180 RID: 16768
			private Hashtable m_rowScopes;

			// Token: 0x04004181 RID: 16769
			private Hashtable m_columnScopes;

			// Token: 0x04004182 RID: 16770
			private FunctionalList<Grouping> m_rowScopeStack;

			// Token: 0x04004183 RID: 16771
			private FunctionalList<Grouping> m_columnScopeStack;
		}

		// Token: 0x0200098F RID: 2447
		private sealed class LookupDestinationCompactionTable : Dictionary<string, int>
		{
			// Token: 0x060080C1 RID: 32961 RVA: 0x00211FEB File Offset: 0x002101EB
			internal LookupDestinationCompactionTable()
			{
			}
		}

		// Token: 0x02000990 RID: 2448
		public sealed class ScopeChainInfo
		{
			// Token: 0x060080C2 RID: 32962 RVA: 0x00211FF3 File Offset: 0x002101F3
			internal ScopeChainInfo(DataRegion containingDataRegion, FunctionalList<Grouping> rowGroupList, FunctionalList<Grouping> columnGroupList)
			{
				this.m_containingDataRegion = containingDataRegion;
				this.m_rowGroupList = rowGroupList;
				this.m_columnGroupList = columnGroupList;
			}

			// Token: 0x060080C3 RID: 32963 RVA: 0x00212010 File Offset: 0x00210210
			internal Grouping GetInnermostGrouping()
			{
				if (this.m_containingDataRegion.ProcessingInnerGrouping == DataRegion.ProcessingInnerGroupings.Column)
				{
					if (!this.m_columnGroupList.IsEmpty() && this.m_columnGroupList.First.Owner.DataRegionDef == this.m_containingDataRegion)
					{
						return this.m_columnGroupList.First;
					}
					if (!this.m_rowGroupList.IsEmpty() && this.m_rowGroupList.First.Owner.DataRegionDef == this.m_containingDataRegion)
					{
						return this.m_rowGroupList.First;
					}
				}
				else
				{
					if (!this.m_rowGroupList.IsEmpty() && this.m_rowGroupList.First.Owner.DataRegionDef == this.m_containingDataRegion)
					{
						return this.m_rowGroupList.First;
					}
					if (!this.m_columnGroupList.IsEmpty() && this.m_columnGroupList.First.Owner.DataRegionDef == this.m_containingDataRegion)
					{
						return this.m_columnGroupList.First;
					}
				}
				InitializationContext.ScopeChainInfo scopeChainInfo = this.m_containingDataRegion.ScopeChainInfo;
				if (scopeChainInfo != null)
				{
					return scopeChainInfo.GetInnermostGrouping();
				}
				return null;
			}

			// Token: 0x060080C4 RID: 32964 RVA: 0x0021211C File Offset: 0x0021031C
			internal GroupingList GetGroupingList()
			{
				GroupingList groupingList = new GroupingList();
				this.AddAllGroups(groupingList);
				groupingList.Reverse();
				return groupingList;
			}

			// Token: 0x060080C5 RID: 32965 RVA: 0x00212140 File Offset: 0x00210340
			private void AddAllGroups(GroupingList groups)
			{
				this.AddGroupsForContainingDataRegion(groups);
				InitializationContext.ScopeChainInfo scopeChainInfo = this.m_containingDataRegion.ScopeChainInfo;
				if (scopeChainInfo != null)
				{
					scopeChainInfo.AddAllGroups(groups);
				}
			}

			// Token: 0x060080C6 RID: 32966 RVA: 0x0021216C File Offset: 0x0021036C
			internal GroupingList GetGroupingListForContainingDataRegion()
			{
				GroupingList groupingList = new GroupingList();
				this.AddGroupsForContainingDataRegion(groupingList);
				groupingList.Reverse();
				return groupingList;
			}

			// Token: 0x060080C7 RID: 32967 RVA: 0x00212190 File Offset: 0x00210390
			internal GroupingList GetGroupsFromCurrentTablixAxisToGrouping(Grouping fromGroup)
			{
				Grouping innermostGrouping = this.GetInnermostGrouping();
				if (innermostGrouping != null)
				{
					return this.GetGroupsFromCurrentTablixAxisToGrouping(innermostGrouping.Owner.DataRegionDef, innermostGrouping.Owner.IsColumn, fromGroup);
				}
				return null;
			}

			// Token: 0x060080C8 RID: 32968 RVA: 0x002121C8 File Offset: 0x002103C8
			internal GroupingList GetGroupsFromCurrentTablixAxisToGrouping(DataRegion dataRegion, bool isColumn, Grouping fromGroup)
			{
				if (dataRegion == this.m_containingDataRegion)
				{
					GroupingList groupingList = new GroupingList();
					if (isColumn)
					{
						this.AddGroupsToList(this.m_columnGroupList, groupingList, fromGroup);
					}
					else
					{
						this.AddGroupsToList(this.m_rowGroupList, groupingList, fromGroup);
					}
					if (fromGroup == null || fromGroup.Owner.DataRegionDef != this.m_containingDataRegion)
					{
						InitializationContext.ScopeChainInfo scopeChainInfo = this.m_containingDataRegion.ScopeChainInfo;
						if (scopeChainInfo != null)
						{
							scopeChainInfo.GetGroupsFromCurrentTablixAxisToGrouping(groupingList, fromGroup);
						}
					}
					if (groupingList.Count > 0)
					{
						groupingList.Reverse();
						return groupingList;
					}
					return null;
				}
				else
				{
					InitializationContext.ScopeChainInfo scopeChainInfo2 = this.m_containingDataRegion.ScopeChainInfo;
					if (scopeChainInfo2 != null)
					{
						return scopeChainInfo2.GetGroupsFromCurrentTablixAxisToGrouping(dataRegion, isColumn, fromGroup);
					}
					return null;
				}
			}

			// Token: 0x060080C9 RID: 32969 RVA: 0x00212260 File Offset: 0x00210460
			internal void GetGroupsFromCurrentTablixAxisToGrouping(GroupingList groups, Grouping fromGroup)
			{
				bool flag = this.m_containingDataRegion.ProcessingInnerGrouping == DataRegion.ProcessingInnerGroupings.Column;
				bool flag2 = false;
				bool flag3 = false;
				if (fromGroup != null && fromGroup.Owner.DataRegionDef == this.m_containingDataRegion)
				{
					flag2 = true;
					if (flag == fromGroup.Owner.IsColumn)
					{
						flag3 = true;
					}
				}
				if (flag)
				{
					if (!flag3)
					{
						this.AddGroupsToList(this.m_rowGroupList, groups, fromGroup);
					}
					this.AddGroupsToList(this.m_columnGroupList, groups, fromGroup);
				}
				else
				{
					if (!flag3)
					{
						this.AddGroupsToList(this.m_columnGroupList, groups, fromGroup);
					}
					this.AddGroupsToList(this.m_rowGroupList, groups, fromGroup);
				}
				if (!flag2)
				{
					InitializationContext.ScopeChainInfo scopeChainInfo = this.m_containingDataRegion.ScopeChainInfo;
					if (scopeChainInfo != null)
					{
						scopeChainInfo.GetGroupsFromCurrentTablixAxisToGrouping(groups, fromGroup);
					}
				}
			}

			// Token: 0x060080CA RID: 32970 RVA: 0x00212308 File Offset: 0x00210508
			private void AddGroupsForContainingDataRegion(GroupingList groups)
			{
				if (this.m_containingDataRegion.ProcessingInnerGrouping == DataRegion.ProcessingInnerGroupings.Column)
				{
					this.AddGroupsToList(this.m_rowGroupList, groups, null);
					this.AddGroupsToList(this.m_columnGroupList, groups, null);
					return;
				}
				this.AddGroupsToList(this.m_columnGroupList, groups, null);
				this.AddGroupsToList(this.m_rowGroupList, groups, null);
			}

			// Token: 0x060080CB RID: 32971 RVA: 0x00212360 File Offset: 0x00210560
			private void AddGroupsToList(FunctionalList<Grouping> groupings, GroupingList containingGroups, Grouping fromGroup)
			{
				while (!groupings.IsEmpty())
				{
					Grouping first = groupings.First;
					groupings = groupings.Rest;
					if (first.Owner.DataRegionDef != this.m_containingDataRegion || first == fromGroup)
					{
						break;
					}
					containingGroups.Add(first);
				}
			}

			// Token: 0x060080CC RID: 32972 RVA: 0x002123A5 File Offset: 0x002105A5
			internal bool IsSameOrChildScope(string parentScope, string childScope)
			{
				return this.IsSameOrChildScope(parentScope, childScope, false);
			}

			// Token: 0x060080CD RID: 32973 RVA: 0x002123B0 File Offset: 0x002105B0
			private bool IsSameOrChildScope(string parentScope, string childScope, bool foundChild)
			{
				bool flag = this.m_containingDataRegion.ProcessingInnerGrouping == DataRegion.ProcessingInnerGroupings.Column;
				bool flag2 = false;
				if (flag)
				{
					foundChild = this.IsSameOrChildScope(this.m_rowGroupList, parentScope, childScope, foundChild, out flag2);
					if (!flag2)
					{
						foundChild = this.IsSameOrChildScope(this.m_columnGroupList, parentScope, childScope, foundChild, out flag2);
					}
				}
				else
				{
					foundChild = this.IsSameOrChildScope(this.m_columnGroupList, parentScope, childScope, foundChild, out flag2);
					if (!flag2)
					{
						foundChild = this.IsSameOrChildScope(this.m_rowGroupList, parentScope, childScope, foundChild, out flag2);
					}
				}
				if (!flag2)
				{
					InitializationContext.ScopeChainInfo scopeChainInfo = this.m_containingDataRegion.ScopeChainInfo;
					if (scopeChainInfo != null)
					{
						return scopeChainInfo.IsSameOrChildScope(parentScope, childScope, foundChild);
					}
				}
				return foundChild && flag2;
			}

			// Token: 0x060080CE RID: 32974 RVA: 0x00212444 File Offset: 0x00210644
			private bool IsSameOrChildScope(FunctionalList<Grouping> groupings, string parentScope, string childScope, bool foundChild, out bool foundParent)
			{
				foundParent = false;
				while (!groupings.IsEmpty())
				{
					Grouping first = groupings.First;
					groupings = groupings.Rest;
					if (first.Owner.DataRegionDef != this.m_containingDataRegion)
					{
						break;
					}
					if (first.Name == childScope)
					{
						foundChild = true;
					}
					if (first.Name == parentScope)
					{
						foundParent = true;
						break;
					}
				}
				return foundChild;
			}

			// Token: 0x04004184 RID: 16772
			private DataRegion m_containingDataRegion;

			// Token: 0x04004185 RID: 16773
			private FunctionalList<Grouping> m_rowGroupList;

			// Token: 0x04004186 RID: 16774
			private FunctionalList<Grouping> m_columnGroupList;
		}
	}
}

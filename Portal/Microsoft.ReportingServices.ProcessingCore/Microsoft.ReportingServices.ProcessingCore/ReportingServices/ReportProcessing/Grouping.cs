using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006EF RID: 1775
	[Serializable]
	internal sealed class Grouping : IAggregateHolder, ISortFilterScope
	{
		// Token: 0x060061AB RID: 25003 RVA: 0x00186E00 File Offset: 0x00185000
		internal Grouping(ConstructionPhase phase)
		{
			if (phase == ConstructionPhase.Publishing)
			{
				this.m_groupExpressions = new ExpressionInfoList();
				this.m_aggregates = new DataAggregateInfoList();
				this.m_postSortAggregates = new DataAggregateInfoList();
				this.m_recursiveAggregates = new DataAggregateInfoList();
			}
		}

		// Token: 0x1700226F RID: 8815
		// (get) Token: 0x060061AC RID: 25004 RVA: 0x00186E37 File Offset: 0x00185037
		// (set) Token: 0x060061AD RID: 25005 RVA: 0x00186E3F File Offset: 0x0018503F
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17002270 RID: 8816
		// (get) Token: 0x060061AE RID: 25006 RVA: 0x00186E48 File Offset: 0x00185048
		// (set) Token: 0x060061AF RID: 25007 RVA: 0x00186E50 File Offset: 0x00185050
		internal ExpressionInfo GroupLabel
		{
			get
			{
				return this.m_groupLabel;
			}
			set
			{
				this.m_groupLabel = value;
			}
		}

		// Token: 0x17002271 RID: 8817
		// (get) Token: 0x060061B0 RID: 25008 RVA: 0x00186E59 File Offset: 0x00185059
		// (set) Token: 0x060061B1 RID: 25009 RVA: 0x00186E61 File Offset: 0x00185061
		internal BoolList SortDirections
		{
			get
			{
				return this.m_sortDirections;
			}
			set
			{
				this.m_sortDirections = value;
			}
		}

		// Token: 0x17002272 RID: 8818
		// (get) Token: 0x060061B2 RID: 25010 RVA: 0x00186E6A File Offset: 0x0018506A
		// (set) Token: 0x060061B3 RID: 25011 RVA: 0x00186E72 File Offset: 0x00185072
		internal ExpressionInfoList GroupExpressions
		{
			get
			{
				return this.m_groupExpressions;
			}
			set
			{
				this.m_groupExpressions = value;
			}
		}

		// Token: 0x17002273 RID: 8819
		// (get) Token: 0x060061B4 RID: 25012 RVA: 0x00186E7B File Offset: 0x0018507B
		// (set) Token: 0x060061B5 RID: 25013 RVA: 0x00186E83 File Offset: 0x00185083
		internal bool PageBreakAtEnd
		{
			get
			{
				return this.m_pageBreakAtEnd;
			}
			set
			{
				this.m_pageBreakAtEnd = value;
			}
		}

		// Token: 0x17002274 RID: 8820
		// (get) Token: 0x060061B6 RID: 25014 RVA: 0x00186E8C File Offset: 0x0018508C
		// (set) Token: 0x060061B7 RID: 25015 RVA: 0x00186E94 File Offset: 0x00185094
		internal bool PageBreakAtStart
		{
			get
			{
				return this.m_pageBreakAtStart;
			}
			set
			{
				this.m_pageBreakAtStart = value;
			}
		}

		// Token: 0x17002275 RID: 8821
		// (get) Token: 0x060061B8 RID: 25016 RVA: 0x00186E9D File Offset: 0x0018509D
		// (set) Token: 0x060061B9 RID: 25017 RVA: 0x00186EA5 File Offset: 0x001850A5
		internal string Custom
		{
			get
			{
				return this.m_custom;
			}
			set
			{
				this.m_custom = value;
			}
		}

		// Token: 0x17002276 RID: 8822
		// (get) Token: 0x060061BA RID: 25018 RVA: 0x00186EAE File Offset: 0x001850AE
		// (set) Token: 0x060061BB RID: 25019 RVA: 0x00186EB6 File Offset: 0x001850B6
		internal DataAggregateInfoList Aggregates
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

		// Token: 0x17002277 RID: 8823
		// (get) Token: 0x060061BC RID: 25020 RVA: 0x00186EBF File Offset: 0x001850BF
		// (set) Token: 0x060061BD RID: 25021 RVA: 0x00186EC7 File Offset: 0x001850C7
		internal bool GroupAndSort
		{
			get
			{
				return this.m_groupAndSort;
			}
			set
			{
				this.m_groupAndSort = value;
			}
		}

		// Token: 0x17002278 RID: 8824
		// (get) Token: 0x060061BE RID: 25022 RVA: 0x00186ED0 File Offset: 0x001850D0
		// (set) Token: 0x060061BF RID: 25023 RVA: 0x00186ED8 File Offset: 0x001850D8
		internal FilterList Filters
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

		// Token: 0x17002279 RID: 8825
		// (get) Token: 0x060061C0 RID: 25024 RVA: 0x00186EE4 File Offset: 0x001850E4
		internal bool SimpleGroupExpressions
		{
			get
			{
				if (this.m_groupExpressions != null)
				{
					for (int i = 0; i < this.m_groupExpressions.Count; i++)
					{
						Global.Tracer.Assert(this.m_groupExpressions[i] != null);
						if (ExpressionInfo.Types.Field != this.m_groupExpressions[i].Type)
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		// Token: 0x1700227A RID: 8826
		// (get) Token: 0x060061C1 RID: 25025 RVA: 0x00186F3F File Offset: 0x0018513F
		// (set) Token: 0x060061C2 RID: 25026 RVA: 0x00186F47 File Offset: 0x00185147
		internal ReportItemList ReportItemsWithHideDuplicates
		{
			get
			{
				return this.m_reportItemsWithHideDuplicates;
			}
			set
			{
				this.m_reportItemsWithHideDuplicates = value;
			}
		}

		// Token: 0x1700227B RID: 8827
		// (get) Token: 0x060061C3 RID: 25027 RVA: 0x00186F50 File Offset: 0x00185150
		// (set) Token: 0x060061C4 RID: 25028 RVA: 0x00186F58 File Offset: 0x00185158
		internal ExpressionInfoList Parent
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

		// Token: 0x1700227C RID: 8828
		// (get) Token: 0x060061C5 RID: 25029 RVA: 0x00186F61 File Offset: 0x00185161
		internal IndexedExprHost ParentExprHost
		{
			get
			{
				if (this.m_exprHost == null)
				{
					return null;
				}
				return this.m_exprHost.ParentExpressionsHost;
			}
		}

		// Token: 0x1700227D RID: 8829
		// (get) Token: 0x060061C6 RID: 25030 RVA: 0x00186F78 File Offset: 0x00185178
		// (set) Token: 0x060061C7 RID: 25031 RVA: 0x00186F80 File Offset: 0x00185180
		internal DataAggregateInfoList RecursiveAggregates
		{
			get
			{
				return this.m_recursiveAggregates;
			}
			set
			{
				this.m_recursiveAggregates = value;
			}
		}

		// Token: 0x1700227E RID: 8830
		// (get) Token: 0x060061C8 RID: 25032 RVA: 0x00186F89 File Offset: 0x00185189
		// (set) Token: 0x060061C9 RID: 25033 RVA: 0x00186F91 File Offset: 0x00185191
		internal DataAggregateInfoList PostSortAggregates
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

		// Token: 0x1700227F RID: 8831
		// (get) Token: 0x060061CA RID: 25034 RVA: 0x00186F9A File Offset: 0x0018519A
		// (set) Token: 0x060061CB RID: 25035 RVA: 0x00186FA2 File Offset: 0x001851A2
		internal string DataElementName
		{
			get
			{
				return this.m_dataElementName;
			}
			set
			{
				this.m_dataElementName = value;
			}
		}

		// Token: 0x17002280 RID: 8832
		// (get) Token: 0x060061CC RID: 25036 RVA: 0x00186FAB File Offset: 0x001851AB
		// (set) Token: 0x060061CD RID: 25037 RVA: 0x00186FB3 File Offset: 0x001851B3
		internal string DataCollectionName
		{
			get
			{
				return this.m_dataCollectionName;
			}
			set
			{
				this.m_dataCollectionName = value;
			}
		}

		// Token: 0x17002281 RID: 8833
		// (get) Token: 0x060061CE RID: 25038 RVA: 0x00186FBC File Offset: 0x001851BC
		// (set) Token: 0x060061CF RID: 25039 RVA: 0x00186FC4 File Offset: 0x001851C4
		internal DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_dataElementOutput;
			}
			set
			{
				this.m_dataElementOutput = value;
			}
		}

		// Token: 0x17002282 RID: 8834
		// (get) Token: 0x060061D0 RID: 25040 RVA: 0x00186FCD File Offset: 0x001851CD
		// (set) Token: 0x060061D1 RID: 25041 RVA: 0x00186FD5 File Offset: 0x001851D5
		internal DataValueList CustomProperties
		{
			get
			{
				return this.m_customProperties;
			}
			set
			{
				this.m_customProperties = value;
			}
		}

		// Token: 0x17002283 RID: 8835
		// (get) Token: 0x060061D2 RID: 25042 RVA: 0x00186FDE File Offset: 0x001851DE
		// (set) Token: 0x060061D3 RID: 25043 RVA: 0x00186FE6 File Offset: 0x001851E6
		internal bool SaveGroupExprValues
		{
			get
			{
				return this.m_saveGroupExprValues;
			}
			set
			{
				this.m_saveGroupExprValues = value;
			}
		}

		// Token: 0x17002284 RID: 8836
		// (get) Token: 0x060061D4 RID: 25044 RVA: 0x00186FEF File Offset: 0x001851EF
		// (set) Token: 0x060061D5 RID: 25045 RVA: 0x00186FF7 File Offset: 0x001851F7
		internal ExpressionInfoList UserSortExpressions
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

		// Token: 0x17002285 RID: 8837
		// (get) Token: 0x060061D6 RID: 25046 RVA: 0x00187000 File Offset: 0x00185200
		// (set) Token: 0x060061D7 RID: 25047 RVA: 0x00187008 File Offset: 0x00185208
		internal InScopeSortFilterHashtable NonDetailSortFiltersInScope
		{
			get
			{
				return this.m_nonDetailSortFiltersInScope;
			}
			set
			{
				this.m_nonDetailSortFiltersInScope = value;
			}
		}

		// Token: 0x17002286 RID: 8838
		// (get) Token: 0x060061D8 RID: 25048 RVA: 0x00187011 File Offset: 0x00185211
		// (set) Token: 0x060061D9 RID: 25049 RVA: 0x00187019 File Offset: 0x00185219
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

		// Token: 0x17002287 RID: 8839
		// (get) Token: 0x060061DA RID: 25050 RVA: 0x00187022 File Offset: 0x00185222
		// (set) Token: 0x060061DB RID: 25051 RVA: 0x0018702A File Offset: 0x0018522A
		internal IntList HideDuplicatesReportItemIDs
		{
			get
			{
				return this.m_hideDuplicatesReportItemIDs;
			}
			set
			{
				this.m_hideDuplicatesReportItemIDs = value;
			}
		}

		// Token: 0x17002288 RID: 8840
		// (get) Token: 0x060061DC RID: 25052 RVA: 0x00187033 File Offset: 0x00185233
		internal GroupingExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17002289 RID: 8841
		// (get) Token: 0x060061DD RID: 25053 RVA: 0x0018703B File Offset: 0x0018523B
		// (set) Token: 0x060061DE RID: 25054 RVA: 0x00187043 File Offset: 0x00185243
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

		// Token: 0x1700228A RID: 8842
		// (get) Token: 0x060061DF RID: 25055 RVA: 0x0018704C File Offset: 0x0018524C
		// (set) Token: 0x060061E0 RID: 25056 RVA: 0x00187054 File Offset: 0x00185254
		internal bool InPivotCell
		{
			get
			{
				return this.m_inPivotCell;
			}
			set
			{
				this.m_inPivotCell = value;
			}
		}

		// Token: 0x1700228B RID: 8843
		// (get) Token: 0x060061E1 RID: 25057 RVA: 0x0018705D File Offset: 0x0018525D
		// (set) Token: 0x060061E2 RID: 25058 RVA: 0x00187065 File Offset: 0x00185265
		internal int RecursiveLevel
		{
			get
			{
				return this.m_recursiveLevel;
			}
			set
			{
				this.m_recursiveLevel = value;
			}
		}

		// Token: 0x1700228C RID: 8844
		// (get) Token: 0x060061E3 RID: 25059 RVA: 0x0018706E File Offset: 0x0018526E
		// (set) Token: 0x060061E4 RID: 25060 RVA: 0x00187076 File Offset: 0x00185276
		internal bool HasInnerFilters
		{
			get
			{
				return this.m_hasInnerFilters;
			}
			set
			{
				this.m_hasInnerFilters = value;
			}
		}

		// Token: 0x1700228D RID: 8845
		// (get) Token: 0x060061E5 RID: 25061 RVA: 0x0018707F File Offset: 0x0018527F
		// (set) Token: 0x060061E6 RID: 25062 RVA: 0x00187087 File Offset: 0x00185287
		internal VariantList CurrentGroupExpressionValues
		{
			get
			{
				return this.m_currentGroupExprValues;
			}
			set
			{
				this.m_currentGroupExprValues = value;
			}
		}

		// Token: 0x1700228E RID: 8846
		// (get) Token: 0x060061E7 RID: 25063 RVA: 0x00187090 File Offset: 0x00185290
		// (set) Token: 0x060061E8 RID: 25064 RVA: 0x00187098 File Offset: 0x00185298
		internal ReportHierarchyNode Owner
		{
			get
			{
				return this.m_owner;
			}
			set
			{
				this.m_owner = value;
			}
		}

		// Token: 0x1700228F RID: 8847
		// (get) Token: 0x060061E9 RID: 25065 RVA: 0x001870A1 File Offset: 0x001852A1
		// (set) Token: 0x060061EA RID: 25066 RVA: 0x001870A9 File Offset: 0x001852A9
		internal VariantList[] SortFilterScopeInfo
		{
			get
			{
				return this.m_sortFilterScopeInfo;
			}
			set
			{
				this.m_sortFilterScopeInfo = value;
			}
		}

		// Token: 0x17002290 RID: 8848
		// (get) Token: 0x060061EB RID: 25067 RVA: 0x001870B2 File Offset: 0x001852B2
		// (set) Token: 0x060061EC RID: 25068 RVA: 0x001870BA File Offset: 0x001852BA
		internal int[] SortFilterScopeIndex
		{
			get
			{
				return this.m_sortFilterScopeIndex;
			}
			set
			{
				this.m_sortFilterScopeIndex = value;
			}
		}

		// Token: 0x17002291 RID: 8849
		// (get) Token: 0x060061ED RID: 25069 RVA: 0x001870C3 File Offset: 0x001852C3
		// (set) Token: 0x060061EE RID: 25070 RVA: 0x001870CB File Offset: 0x001852CB
		internal bool[] NeedScopeInfoForSortFilterExpression
		{
			get
			{
				return this.m_needScopeInfoForSortFilterExpression;
			}
			set
			{
				this.m_needScopeInfoForSortFilterExpression = value;
			}
		}

		// Token: 0x17002292 RID: 8850
		// (get) Token: 0x060061EF RID: 25071 RVA: 0x001870D4 File Offset: 0x001852D4
		// (set) Token: 0x060061F0 RID: 25072 RVA: 0x001870DC File Offset: 0x001852DC
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

		// Token: 0x17002293 RID: 8851
		// (get) Token: 0x060061F1 RID: 25073 RVA: 0x001870E5 File Offset: 0x001852E5
		// (set) Token: 0x060061F2 RID: 25074 RVA: 0x001870ED File Offset: 0x001852ED
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

		// Token: 0x17002294 RID: 8852
		// (get) Token: 0x060061F3 RID: 25075 RVA: 0x001870F6 File Offset: 0x001852F6
		// (set) Token: 0x060061F4 RID: 25076 RVA: 0x001870FE File Offset: 0x001852FE
		internal bool[] SortFilterScopeMatched
		{
			get
			{
				return this.m_sortFilterScopeMatched;
			}
			set
			{
				this.m_sortFilterScopeMatched = value;
			}
		}

		// Token: 0x17002295 RID: 8853
		// (get) Token: 0x060061F5 RID: 25077 RVA: 0x00187107 File Offset: 0x00185307
		int ISortFilterScope.ID
		{
			get
			{
				Global.Tracer.Assert(this.m_owner != null);
				return this.m_owner.ID;
			}
		}

		// Token: 0x17002296 RID: 8854
		// (get) Token: 0x060061F6 RID: 25078 RVA: 0x00187127 File Offset: 0x00185327
		string ISortFilterScope.ScopeName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17002297 RID: 8855
		// (get) Token: 0x060061F7 RID: 25079 RVA: 0x0018712F File Offset: 0x0018532F
		// (set) Token: 0x060061F8 RID: 25080 RVA: 0x00187137 File Offset: 0x00185337
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

		// Token: 0x17002298 RID: 8856
		// (get) Token: 0x060061F9 RID: 25081 RVA: 0x00187140 File Offset: 0x00185340
		// (set) Token: 0x060061FA RID: 25082 RVA: 0x00187148 File Offset: 0x00185348
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

		// Token: 0x17002299 RID: 8857
		// (get) Token: 0x060061FB RID: 25083 RVA: 0x00187151 File Offset: 0x00185351
		// (set) Token: 0x060061FC RID: 25084 RVA: 0x00187159 File Offset: 0x00185359
		ExpressionInfoList ISortFilterScope.UserSortExpressions
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

		// Token: 0x1700229A RID: 8858
		// (get) Token: 0x060061FD RID: 25085 RVA: 0x00187162 File Offset: 0x00185362
		IndexedExprHost ISortFilterScope.UserSortExpressionsHost
		{
			get
			{
				if (this.m_exprHost == null)
				{
					return null;
				}
				return this.m_exprHost.UserSortExpressionsHost;
			}
		}

		// Token: 0x060061FE RID: 25086 RVA: 0x0018717C File Offset: 0x0018537C
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.GroupingStart(this.m_name);
			this.DataRendererInitialize(context);
			if (this.m_groupExpressions != null)
			{
				for (int i = 0; i < this.m_groupExpressions.Count; i++)
				{
					ExpressionInfo expressionInfo = this.m_groupExpressions[i];
					expressionInfo.GroupExpressionInitialize(context);
					context.ExprHostBuilder.GroupingExpression(expressionInfo);
				}
			}
			if (this.m_groupLabel != null)
			{
				this.m_groupLabel.Initialize("Label", context);
				context.ExprHostBuilder.GenericLabel(this.m_groupLabel);
			}
			if (this.m_filters != null)
			{
				for (int j = 0; j < this.m_filters.Count; j++)
				{
					this.m_filters[j].Initialize(context);
				}
			}
			if (this.m_parent != null)
			{
				context.ExprHostBuilder.GroupingParentExpressionsStart();
				for (int k = 0; k < this.m_parent.Count; k++)
				{
					ExpressionInfo expressionInfo2 = this.m_parent[k];
					expressionInfo2.GroupExpressionInitialize(context);
					context.ExprHostBuilder.GroupingParentExpression(expressionInfo2);
				}
				context.ExprHostBuilder.GroupingParentExpressionsEnd();
			}
			if (this.m_customProperties != null)
			{
				this.m_customProperties.Initialize(null, true, context);
			}
			if (this.m_userSortExpressions != null)
			{
				context.ExprHostBuilder.UserSortExpressionsStart();
				for (int l = 0; l < this.m_userSortExpressions.Count; l++)
				{
					ExpressionInfo expressionInfo3 = this.m_userSortExpressions[l];
					context.ExprHostBuilder.UserSortExpression(expressionInfo3);
				}
				context.ExprHostBuilder.UserSortExpressionsEnd();
			}
			context.ExprHostBuilder.GroupingEnd();
		}

		// Token: 0x060061FF RID: 25087 RVA: 0x0018730F File Offset: 0x0018550F
		DataAggregateInfoList[] IAggregateHolder.GetAggregateLists()
		{
			return new DataAggregateInfoList[] { this.m_aggregates };
		}

		// Token: 0x06006200 RID: 25088 RVA: 0x00187320 File Offset: 0x00185520
		DataAggregateInfoList[] IAggregateHolder.GetPostSortAggregateLists()
		{
			return new DataAggregateInfoList[] { this.m_postSortAggregates };
		}

		// Token: 0x06006201 RID: 25089 RVA: 0x00187334 File Offset: 0x00185534
		void IAggregateHolder.ClearIfEmpty()
		{
			Global.Tracer.Assert(this.m_aggregates != null);
			if (this.m_aggregates.Count == 0)
			{
				this.m_aggregates = null;
			}
			Global.Tracer.Assert(this.m_postSortAggregates != null);
			if (this.m_postSortAggregates.Count == 0)
			{
				this.m_postSortAggregates = null;
			}
			Global.Tracer.Assert(this.m_recursiveAggregates != null);
			if (this.m_recursiveAggregates.Count == 0)
			{
				this.m_recursiveAggregates = null;
			}
		}

		// Token: 0x06006202 RID: 25090 RVA: 0x001873B8 File Offset: 0x001855B8
		private void DataRendererInitialize(InitializationContext context)
		{
			CLSNameValidator.ValidateDataElementName(ref this.m_dataElementName, this.m_name, context.ObjectType, context.ObjectName, "DataElementName", context.ErrorContext);
			CLSNameValidator.ValidateDataElementName(ref this.m_dataCollectionName, this.m_dataElementName + "_Collection", context.ObjectType, context.ObjectName, "DataCollectionName", context.ErrorContext);
		}

		// Token: 0x06006203 RID: 25091 RVA: 0x00187427 File Offset: 0x00185627
		internal void AddReportItemWithHideDuplicates(Microsoft.ReportingServices.ReportProcessing.ReportItem reportItem)
		{
			if (this.m_reportItemsWithHideDuplicates == null)
			{
				this.m_reportItemsWithHideDuplicates = new ReportItemList();
			}
			this.m_reportItemsWithHideDuplicates.Add(reportItem);
		}

		// Token: 0x06006204 RID: 25092 RVA: 0x0018744C File Offset: 0x0018564C
		internal void SetExprHost(GroupingExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null && reportObjectModel != null);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			if (this.m_exprHost.FilterHostsRemotable != null)
			{
				Global.Tracer.Assert(this.m_filters != null);
				int count = this.m_filters.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_filters[i].SetExprHost(this.m_exprHost.FilterHostsRemotable, reportObjectModel);
				}
			}
			if (this.m_exprHost.ParentExpressionsHost != null)
			{
				this.m_exprHost.ParentExpressionsHost.SetReportObjectModel(reportObjectModel);
			}
			if (this.m_exprHost.CustomPropertyHostsRemotable != null)
			{
				Global.Tracer.Assert(this.m_customProperties != null);
				this.m_customProperties.SetExprHost(this.m_exprHost.CustomPropertyHostsRemotable, reportObjectModel);
			}
			if (this.m_exprHost.UserSortExpressionsHost != null)
			{
				this.m_exprHost.UserSortExpressionsHost.SetReportObjectModel(reportObjectModel);
			}
		}

		// Token: 0x06006205 RID: 25093 RVA: 0x00187548 File Offset: 0x00185748
		internal bool IsOnPathToSortFilterSource(int index)
		{
			return this.m_sortFilterScopeInfo != null && this.m_sortFilterScopeIndex != null && -1 != this.m_sortFilterScopeIndex[index];
		}

		// Token: 0x06006206 RID: 25094 RVA: 0x00187568 File Offset: 0x00185768
		internal int[] GetGroupExpressionFieldIndices()
		{
			if (this.m_groupExpressionFieldIndices == null)
			{
				Global.Tracer.Assert(this.m_groupExpressions != null && 0 < this.m_groupExpressions.Count);
				this.m_groupExpressionFieldIndices = new int[this.m_groupExpressions.Count];
				for (int i = 0; i < this.m_groupExpressions.Count; i++)
				{
					this.m_groupExpressionFieldIndices[i] = -2;
					ExpressionInfo expressionInfo = this.m_groupExpressions[i];
					if (expressionInfo.Type == ExpressionInfo.Types.Field)
					{
						this.m_groupExpressionFieldIndices[i] = expressionInfo.IntValue;
					}
					else if (expressionInfo.Type == ExpressionInfo.Types.Constant)
					{
						this.m_groupExpressionFieldIndices[i] = -1;
					}
				}
			}
			return this.m_groupExpressionFieldIndices;
		}

		// Token: 0x06006207 RID: 25095 RVA: 0x00187618 File Offset: 0x00185818
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Name, Token.String),
				new MemberInfo(MemberName.GroupExpressions, ObjectType.ExpressionInfoList),
				new MemberInfo(MemberName.GroupLabel, ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.SortDirections, ObjectType.BoolList),
				new MemberInfo(MemberName.PageBreakAtEnd, Token.Boolean),
				new MemberInfo(MemberName.PageBreakAtStart, Token.Boolean),
				new MemberInfo(MemberName.Custom, Token.String),
				new MemberInfo(MemberName.Aggregates, ObjectType.DataAggregateInfoList),
				new MemberInfo(MemberName.GroupAndSort, Token.Boolean),
				new MemberInfo(MemberName.Filters, ObjectType.FilterList),
				new MemberInfo(MemberName.ReportItemsWithHideDuplicates, Token.Reference, ObjectType.ReportItemList),
				new MemberInfo(MemberName.Parent, ObjectType.ExpressionInfoList),
				new MemberInfo(MemberName.RecursiveAggregates, ObjectType.DataAggregateInfoList),
				new MemberInfo(MemberName.PostSortAggregates, ObjectType.DataAggregateInfoList),
				new MemberInfo(MemberName.DataElementName, Token.String),
				new MemberInfo(MemberName.DataCollectionName, Token.String),
				new MemberInfo(MemberName.DataElementOutput, Token.Enum),
				new MemberInfo(MemberName.CustomProperties, ObjectType.DataValueList),
				new MemberInfo(MemberName.SaveGroupExprValues, Token.Boolean),
				new MemberInfo(MemberName.UserSortExpressions, ObjectType.ExpressionInfoList),
				new MemberInfo(MemberName.NonDetailSortFiltersInScope, ObjectType.InScopeSortFilterHashtable),
				new MemberInfo(MemberName.DetailSortFiltersInScope, ObjectType.InScopeSortFilterHashtable)
			});
		}

		// Token: 0x04003164 RID: 12644
		private string m_name;

		// Token: 0x04003165 RID: 12645
		private ExpressionInfoList m_groupExpressions;

		// Token: 0x04003166 RID: 12646
		private ExpressionInfo m_groupLabel;

		// Token: 0x04003167 RID: 12647
		private BoolList m_sortDirections;

		// Token: 0x04003168 RID: 12648
		private bool m_pageBreakAtEnd;

		// Token: 0x04003169 RID: 12649
		private bool m_pageBreakAtStart;

		// Token: 0x0400316A RID: 12650
		private string m_custom;

		// Token: 0x0400316B RID: 12651
		private DataAggregateInfoList m_aggregates;

		// Token: 0x0400316C RID: 12652
		private bool m_groupAndSort;

		// Token: 0x0400316D RID: 12653
		private FilterList m_filters;

		// Token: 0x0400316E RID: 12654
		[Reference]
		private ReportItemList m_reportItemsWithHideDuplicates;

		// Token: 0x0400316F RID: 12655
		private ExpressionInfoList m_parent;

		// Token: 0x04003170 RID: 12656
		private DataAggregateInfoList m_recursiveAggregates;

		// Token: 0x04003171 RID: 12657
		private DataAggregateInfoList m_postSortAggregates;

		// Token: 0x04003172 RID: 12658
		private string m_dataElementName;

		// Token: 0x04003173 RID: 12659
		private string m_dataCollectionName;

		// Token: 0x04003174 RID: 12660
		private DataElementOutputTypes m_dataElementOutput;

		// Token: 0x04003175 RID: 12661
		private DataValueList m_customProperties;

		// Token: 0x04003176 RID: 12662
		private bool m_saveGroupExprValues;

		// Token: 0x04003177 RID: 12663
		private ExpressionInfoList m_userSortExpressions;

		// Token: 0x04003178 RID: 12664
		private InScopeSortFilterHashtable m_nonDetailSortFiltersInScope;

		// Token: 0x04003179 RID: 12665
		private InScopeSortFilterHashtable m_detailSortFiltersInScope;

		// Token: 0x0400317A RID: 12666
		[NonSerialized]
		private IntList m_hideDuplicatesReportItemIDs;

		// Token: 0x0400317B RID: 12667
		[NonSerialized]
		private GroupingExprHost m_exprHost;

		// Token: 0x0400317C RID: 12668
		[NonSerialized]
		private Hashtable m_scopeNames;

		// Token: 0x0400317D RID: 12669
		[NonSerialized]
		private bool m_inPivotCell;

		// Token: 0x0400317E RID: 12670
		[NonSerialized]
		private int m_recursiveLevel;

		// Token: 0x0400317F RID: 12671
		[NonSerialized]
		private int[] m_groupExpressionFieldIndices;

		// Token: 0x04003180 RID: 12672
		[NonSerialized]
		private bool m_hasInnerFilters;

		// Token: 0x04003181 RID: 12673
		[NonSerialized]
		private VariantList m_currentGroupExprValues;

		// Token: 0x04003182 RID: 12674
		[NonSerialized]
		private ReportHierarchyNode m_owner;

		// Token: 0x04003183 RID: 12675
		[NonSerialized]
		private VariantList[] m_sortFilterScopeInfo;

		// Token: 0x04003184 RID: 12676
		[NonSerialized]
		private int[] m_sortFilterScopeIndex;

		// Token: 0x04003185 RID: 12677
		[NonSerialized]
		private bool[] m_needScopeInfoForSortFilterExpression;

		// Token: 0x04003186 RID: 12678
		[NonSerialized]
		private bool[] m_sortFilterScopeMatched;

		// Token: 0x04003187 RID: 12679
		[NonSerialized]
		private bool[] m_isSortFilterTarget;

		// Token: 0x04003188 RID: 12680
		[NonSerialized]
		private bool[] m_isSortFilterExpressionScope;
	}
}

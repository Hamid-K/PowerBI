using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006EA RID: 1770
	[Serializable]
	internal abstract class DataRegion : Microsoft.ReportingServices.ReportProcessing.ReportItem, IPageBreakItem, IAggregateHolder, ISortFilterScope
	{
		// Token: 0x060060E2 RID: 24802 RVA: 0x001858D8 File Offset: 0x00183AD8
		protected DataRegion(Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x060060E3 RID: 24803 RVA: 0x001858E8 File Offset: 0x00183AE8
		protected DataRegion(int id, Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(id, parent)
		{
			this.m_aggregates = new DataAggregateInfoList();
			this.m_postSortAggregates = new DataAggregateInfoList();
		}

		// Token: 0x1700221F RID: 8735
		// (get) Token: 0x060060E4 RID: 24804 RVA: 0x0018590F File Offset: 0x00183B0F
		// (set) Token: 0x060060E5 RID: 24805 RVA: 0x00185917 File Offset: 0x00183B17
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

		// Token: 0x17002220 RID: 8736
		// (get) Token: 0x060060E6 RID: 24806 RVA: 0x00185920 File Offset: 0x00183B20
		// (set) Token: 0x060060E7 RID: 24807 RVA: 0x00185928 File Offset: 0x00183B28
		internal ExpressionInfo NoRows
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

		// Token: 0x17002221 RID: 8737
		// (get) Token: 0x060060E8 RID: 24808 RVA: 0x00185931 File Offset: 0x00183B31
		// (set) Token: 0x060060E9 RID: 24809 RVA: 0x00185939 File Offset: 0x00183B39
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

		// Token: 0x17002222 RID: 8738
		// (get) Token: 0x060060EA RID: 24810 RVA: 0x00185942 File Offset: 0x00183B42
		// (set) Token: 0x060060EB RID: 24811 RVA: 0x0018594A File Offset: 0x00183B4A
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

		// Token: 0x17002223 RID: 8739
		// (get) Token: 0x060060EC RID: 24812 RVA: 0x00185953 File Offset: 0x00183B53
		// (set) Token: 0x060060ED RID: 24813 RVA: 0x0018595B File Offset: 0x00183B5B
		internal bool KeepTogether
		{
			get
			{
				return this.m_keepTogether;
			}
			set
			{
				this.m_keepTogether = value;
			}
		}

		// Token: 0x17002224 RID: 8740
		// (get) Token: 0x060060EE RID: 24814 RVA: 0x00185964 File Offset: 0x00183B64
		// (set) Token: 0x060060EF RID: 24815 RVA: 0x0018596C File Offset: 0x00183B6C
		internal IntList RepeatSiblings
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

		// Token: 0x17002225 RID: 8741
		// (get) Token: 0x060060F0 RID: 24816 RVA: 0x00185975 File Offset: 0x00183B75
		// (set) Token: 0x060060F1 RID: 24817 RVA: 0x0018597D File Offset: 0x00183B7D
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

		// Token: 0x17002226 RID: 8742
		// (get) Token: 0x060060F2 RID: 24818 RVA: 0x00185986 File Offset: 0x00183B86
		// (set) Token: 0x060060F3 RID: 24819 RVA: 0x0018598E File Offset: 0x00183B8E
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

		// Token: 0x17002227 RID: 8743
		// (get) Token: 0x060060F4 RID: 24820 RVA: 0x00185997 File Offset: 0x00183B97
		// (set) Token: 0x060060F5 RID: 24821 RVA: 0x0018599F File Offset: 0x00183B9F
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

		// Token: 0x17002228 RID: 8744
		// (get) Token: 0x060060F6 RID: 24822 RVA: 0x001859A8 File Offset: 0x00183BA8
		// (set) Token: 0x060060F7 RID: 24823 RVA: 0x001859B0 File Offset: 0x00183BB0
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

		// Token: 0x17002229 RID: 8745
		// (get) Token: 0x060060F8 RID: 24824 RVA: 0x001859B9 File Offset: 0x00183BB9
		// (set) Token: 0x060060F9 RID: 24825 RVA: 0x001859C1 File Offset: 0x00183BC1
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

		// Token: 0x1700222A RID: 8746
		// (get) Token: 0x060060FA RID: 24826 RVA: 0x001859CA File Offset: 0x00183BCA
		// (set) Token: 0x060060FB RID: 24827 RVA: 0x001859D2 File Offset: 0x00183BD2
		internal ReportProcessing.RuntimeDataRegionObj RuntimeDataRegionObj
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

		// Token: 0x1700222B RID: 8747
		// (get) Token: 0x060060FC RID: 24828 RVA: 0x001859DB File Offset: 0x00183BDB
		// (set) Token: 0x060060FD RID: 24829 RVA: 0x001859E3 File Offset: 0x00183BE3
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

		// Token: 0x1700222C RID: 8748
		// (get) Token: 0x060060FE RID: 24830 RVA: 0x001859EC File Offset: 0x00183BEC
		// (set) Token: 0x060060FF RID: 24831 RVA: 0x001859F4 File Offset: 0x00183BF4
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

		// Token: 0x1700222D RID: 8749
		// (get) Token: 0x06006100 RID: 24832 RVA: 0x001859FD File Offset: 0x00183BFD
		// (set) Token: 0x06006101 RID: 24833 RVA: 0x00185A05 File Offset: 0x00183C05
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

		// Token: 0x1700222E RID: 8750
		// (get) Token: 0x06006102 RID: 24834 RVA: 0x00185A0E File Offset: 0x00183C0E
		// (set) Token: 0x06006103 RID: 24835 RVA: 0x00185A16 File Offset: 0x00183C16
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

		// Token: 0x1700222F RID: 8751
		// (get) Token: 0x06006104 RID: 24836 RVA: 0x00185A1F File Offset: 0x00183C1F
		// (set) Token: 0x06006105 RID: 24837 RVA: 0x00185A27 File Offset: 0x00183C27
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

		// Token: 0x17002230 RID: 8752
		// (get) Token: 0x06006106 RID: 24838 RVA: 0x00185A30 File Offset: 0x00183C30
		// (set) Token: 0x06006107 RID: 24839 RVA: 0x00185A38 File Offset: 0x00183C38
		internal int CurrentDetailRowIndex
		{
			get
			{
				return this.m_currentDetailRowIndex;
			}
			set
			{
				this.m_currentDetailRowIndex = value;
			}
		}

		// Token: 0x17002231 RID: 8753
		// (get) Token: 0x06006108 RID: 24840
		protected abstract DataRegionExprHost DataRegionExprHost { get; }

		// Token: 0x17002232 RID: 8754
		// (get) Token: 0x06006109 RID: 24841 RVA: 0x00185A41 File Offset: 0x00183C41
		int ISortFilterScope.ID
		{
			get
			{
				return this.m_ID;
			}
		}

		// Token: 0x17002233 RID: 8755
		// (get) Token: 0x0600610A RID: 24842 RVA: 0x00185A49 File Offset: 0x00183C49
		string ISortFilterScope.ScopeName
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17002234 RID: 8756
		// (get) Token: 0x0600610B RID: 24843 RVA: 0x00185A51 File Offset: 0x00183C51
		// (set) Token: 0x0600610C RID: 24844 RVA: 0x00185A59 File Offset: 0x00183C59
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

		// Token: 0x17002235 RID: 8757
		// (get) Token: 0x0600610D RID: 24845 RVA: 0x00185A62 File Offset: 0x00183C62
		// (set) Token: 0x0600610E RID: 24846 RVA: 0x00185A6A File Offset: 0x00183C6A
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

		// Token: 0x17002236 RID: 8758
		// (get) Token: 0x0600610F RID: 24847 RVA: 0x00185A73 File Offset: 0x00183C73
		// (set) Token: 0x06006110 RID: 24848 RVA: 0x00185A7B File Offset: 0x00183C7B
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

		// Token: 0x17002237 RID: 8759
		// (get) Token: 0x06006111 RID: 24849 RVA: 0x00185A84 File Offset: 0x00183C84
		IndexedExprHost ISortFilterScope.UserSortExpressionsHost
		{
			get
			{
				if (this.DataRegionExprHost == null)
				{
					return null;
				}
				return this.DataRegionExprHost.UserSortExpressionsHost;
			}
		}

		// Token: 0x06006112 RID: 24850 RVA: 0x00185A9C File Offset: 0x00183C9C
		internal override bool Initialize(InitializationContext context)
		{
			base.Initialize(context);
			if (this.m_filters != null)
			{
				for (int i = 0; i < this.m_filters.Count; i++)
				{
					this.m_filters[i].Initialize(context);
				}
			}
			if (this.m_noRows != null)
			{
				this.m_noRows.Initialize("NoRows", context);
				context.ExprHostBuilder.GenericNoRows(this.m_noRows);
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
			return false;
		}

		// Token: 0x06006113 RID: 24851 RVA: 0x00185B5D File Offset: 0x00183D5D
		DataAggregateInfoList[] IAggregateHolder.GetAggregateLists()
		{
			return new DataAggregateInfoList[] { this.m_aggregates };
		}

		// Token: 0x06006114 RID: 24852 RVA: 0x00185B6E File Offset: 0x00183D6E
		DataAggregateInfoList[] IAggregateHolder.GetPostSortAggregateLists()
		{
			return new DataAggregateInfoList[] { this.m_postSortAggregates };
		}

		// Token: 0x06006115 RID: 24853 RVA: 0x00185B80 File Offset: 0x00183D80
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
		}

		// Token: 0x06006116 RID: 24854 RVA: 0x00185BDB File Offset: 0x00183DDB
		bool IPageBreakItem.IgnorePageBreaks()
		{
			if (this.m_pagebreakState == PageBreakStates.Unknown)
			{
				if (SharedHiddenState.Never != Visibility.GetSharedHidden(this.m_visibility))
				{
					this.m_pagebreakState = PageBreakStates.CanIgnore;
				}
				else
				{
					this.m_pagebreakState = PageBreakStates.CannotIgnore;
				}
			}
			return PageBreakStates.CanIgnore == this.m_pagebreakState;
		}

		// Token: 0x06006117 RID: 24855 RVA: 0x00185C0F File Offset: 0x00183E0F
		bool IPageBreakItem.HasPageBreaks(bool atStart)
		{
			return (atStart && this.m_pageBreakAtStart) || (!atStart && this.m_pageBreakAtEnd);
		}

		// Token: 0x06006118 RID: 24856 RVA: 0x00185C2C File Offset: 0x00183E2C
		protected void DataRegionSetExprHost(DataRegionExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null);
			base.ReportItemSetExprHost(exprHost, reportObjectModel);
			if (exprHost.FilterHostsRemotable != null)
			{
				Global.Tracer.Assert(this.m_filters != null);
				int count = this.m_filters.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_filters[i].SetExprHost(exprHost.FilterHostsRemotable, reportObjectModel);
				}
			}
			if (exprHost.UserSortExpressionsHost != null)
			{
				exprHost.UserSortExpressionsHost.SetReportObjectModel(reportObjectModel);
			}
		}

		// Token: 0x06006119 RID: 24857 RVA: 0x00185CB0 File Offset: 0x00183EB0
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItem, new MemberInfoList
			{
				new MemberInfo(MemberName.DataSetName, Token.String),
				new MemberInfo(MemberName.NoRows, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.PageBreakAtEnd, Token.Boolean),
				new MemberInfo(MemberName.PageBreakAtStart, Token.Boolean),
				new MemberInfo(MemberName.KeepTogether, Token.Boolean),
				new MemberInfo(MemberName.RepeatSiblings, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.IntList),
				new MemberInfo(MemberName.Filters, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.FilterList),
				new MemberInfo(MemberName.Aggregates, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataAggregateInfoList),
				new MemberInfo(MemberName.PostSortAggregates, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataAggregateInfoList),
				new MemberInfo(MemberName.UserSortExpressions, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfoList),
				new MemberInfo(MemberName.DetailSortFiltersInScope, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.InScopeSortFilterHashtable)
			});
		}

		// Token: 0x04003123 RID: 12579
		protected string m_dataSetName;

		// Token: 0x04003124 RID: 12580
		protected ExpressionInfo m_noRows;

		// Token: 0x04003125 RID: 12581
		protected bool m_pageBreakAtEnd;

		// Token: 0x04003126 RID: 12582
		protected bool m_pageBreakAtStart;

		// Token: 0x04003127 RID: 12583
		protected bool m_keepTogether;

		// Token: 0x04003128 RID: 12584
		protected IntList m_repeatSiblings;

		// Token: 0x04003129 RID: 12585
		protected FilterList m_filters;

		// Token: 0x0400312A RID: 12586
		protected DataAggregateInfoList m_aggregates;

		// Token: 0x0400312B RID: 12587
		protected DataAggregateInfoList m_postSortAggregates;

		// Token: 0x0400312C RID: 12588
		protected ExpressionInfoList m_userSortExpressions;

		// Token: 0x0400312D RID: 12589
		protected InScopeSortFilterHashtable m_detailSortFiltersInScope;

		// Token: 0x0400312E RID: 12590
		[NonSerialized]
		protected ReportProcessing.RuntimeDataRegionObj m_runtimeDataRegionObj;

		// Token: 0x0400312F RID: 12591
		[NonSerialized]
		protected PageBreakStates m_pagebreakState;

		// Token: 0x04003130 RID: 12592
		[NonSerialized]
		protected Hashtable m_scopeNames;

		// Token: 0x04003131 RID: 12593
		[NonSerialized]
		protected bool m_inPivotCell;

		// Token: 0x04003132 RID: 12594
		[NonSerialized]
		protected bool[] m_isSortFilterTarget;

		// Token: 0x04003133 RID: 12595
		[NonSerialized]
		protected bool[] m_isSortFilterExpressionScope;

		// Token: 0x04003134 RID: 12596
		[NonSerialized]
		protected int[] m_sortFilterSourceDetailScopeInfo;

		// Token: 0x04003135 RID: 12597
		[NonSerialized]
		protected int m_currentDetailRowIndex = -1;
	}
}

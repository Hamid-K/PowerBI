using System;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006E8 RID: 1768
	[Serializable]
	public sealed class SubReport : Microsoft.ReportingServices.ReportProcessing.ReportItem, IPageBreakItem
	{
		// Token: 0x060060A6 RID: 24742 RVA: 0x0018513E File Offset: 0x0018333E
		internal SubReport(Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x060060A7 RID: 24743 RVA: 0x00185147 File Offset: 0x00183347
		internal SubReport(int id, Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(id, parent)
		{
			this.m_parameters = new ParameterValueList();
		}

		// Token: 0x17002206 RID: 8710
		// (get) Token: 0x060060A8 RID: 24744 RVA: 0x0018515C File Offset: 0x0018335C
		internal override ObjectType ObjectType
		{
			get
			{
				return ObjectType.Subreport;
			}
		}

		// Token: 0x17002207 RID: 8711
		// (get) Token: 0x060060A9 RID: 24745 RVA: 0x0018515F File Offset: 0x0018335F
		// (set) Token: 0x060060AA RID: 24746 RVA: 0x00185167 File Offset: 0x00183367
		internal string ReportPath
		{
			get
			{
				return this.m_reportPath;
			}
			set
			{
				this.m_reportPath = value;
			}
		}

		// Token: 0x17002208 RID: 8712
		// (get) Token: 0x060060AB RID: 24747 RVA: 0x00185170 File Offset: 0x00183370
		// (set) Token: 0x060060AC RID: 24748 RVA: 0x00185178 File Offset: 0x00183378
		internal ParameterValueList Parameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x17002209 RID: 8713
		// (get) Token: 0x060060AD RID: 24749 RVA: 0x00185181 File Offset: 0x00183381
		// (set) Token: 0x060060AE RID: 24750 RVA: 0x00185189 File Offset: 0x00183389
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

		// Token: 0x1700220A RID: 8714
		// (get) Token: 0x060060AF RID: 24751 RVA: 0x00185192 File Offset: 0x00183392
		// (set) Token: 0x060060B0 RID: 24752 RVA: 0x0018519A File Offset: 0x0018339A
		internal bool MergeTransactions
		{
			get
			{
				return this.m_mergeTransactions;
			}
			set
			{
				this.m_mergeTransactions = value;
			}
		}

		// Token: 0x1700220B RID: 8715
		// (get) Token: 0x060060B1 RID: 24753 RVA: 0x001851A3 File Offset: 0x001833A3
		// (set) Token: 0x060060B2 RID: 24754 RVA: 0x001851AB File Offset: 0x001833AB
		internal GroupingList ContainingScopes
		{
			get
			{
				return this.m_containingScopes;
			}
			set
			{
				this.m_containingScopes = value;
			}
		}

		// Token: 0x1700220C RID: 8716
		// (get) Token: 0x060060B3 RID: 24755 RVA: 0x001851B4 File Offset: 0x001833B4
		// (set) Token: 0x060060B4 RID: 24756 RVA: 0x001851BC File Offset: 0x001833BC
		internal SubReport.Status RetrievalStatus
		{
			get
			{
				return this.m_status;
			}
			set
			{
				this.m_status = value;
			}
		}

		// Token: 0x1700220D RID: 8717
		// (get) Token: 0x060060B5 RID: 24757 RVA: 0x001851C5 File Offset: 0x001833C5
		// (set) Token: 0x060060B6 RID: 24758 RVA: 0x001851CD File Offset: 0x001833CD
		internal string ReportName
		{
			get
			{
				return this.m_reportName;
			}
			set
			{
				this.m_reportName = value;
			}
		}

		// Token: 0x1700220E RID: 8718
		// (get) Token: 0x060060B7 RID: 24759 RVA: 0x001851D6 File Offset: 0x001833D6
		// (set) Token: 0x060060B8 RID: 24760 RVA: 0x001851DE File Offset: 0x001833DE
		internal string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x1700220F RID: 8719
		// (get) Token: 0x060060B9 RID: 24761 RVA: 0x001851E7 File Offset: 0x001833E7
		// (set) Token: 0x060060BA RID: 24762 RVA: 0x001851EF File Offset: 0x001833EF
		internal Report Report
		{
			get
			{
				return this.m_report;
			}
			set
			{
				this.m_report = value;
			}
		}

		// Token: 0x17002210 RID: 8720
		// (get) Token: 0x060060BB RID: 24763 RVA: 0x001851F8 File Offset: 0x001833F8
		// (set) Token: 0x060060BC RID: 24764 RVA: 0x00185200 File Offset: 0x00183400
		internal string StringUri
		{
			get
			{
				return this.m_stringUri;
			}
			set
			{
				this.m_stringUri = value;
			}
		}

		// Token: 0x17002211 RID: 8721
		// (get) Token: 0x060060BD RID: 24765 RVA: 0x00185209 File Offset: 0x00183409
		// (set) Token: 0x060060BE RID: 24766 RVA: 0x00185211 File Offset: 0x00183411
		internal ICatalogItemContext ReportContext
		{
			get
			{
				return this.m_reportContext;
			}
			set
			{
				this.m_reportContext = value;
			}
		}

		// Token: 0x17002212 RID: 8722
		// (get) Token: 0x060060BF RID: 24767 RVA: 0x0018521A File Offset: 0x0018341A
		// (set) Token: 0x060060C0 RID: 24768 RVA: 0x00185222 File Offset: 0x00183422
		internal ParameterInfoCollection ParametersFromCatalog
		{
			get
			{
				return this.m_parametersFromCatalog;
			}
			set
			{
				this.m_parametersFromCatalog = value;
			}
		}

		// Token: 0x17002213 RID: 8723
		// (get) Token: 0x060060C1 RID: 24769 RVA: 0x0018522B File Offset: 0x0018342B
		internal Uri Uri
		{
			get
			{
				if (null == this.m_uri)
				{
					this.m_uri = new Uri(this.m_stringUri);
				}
				return this.m_uri;
			}
		}

		// Token: 0x17002214 RID: 8724
		// (get) Token: 0x060060C2 RID: 24770 RVA: 0x00185252 File Offset: 0x00183452
		internal SubreportExprHost SubReportExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17002215 RID: 8725
		// (get) Token: 0x060060C3 RID: 24771 RVA: 0x0018525A File Offset: 0x0018345A
		// (set) Token: 0x060060C4 RID: 24772 RVA: 0x00185262 File Offset: 0x00183462
		internal string SubReportScope
		{
			get
			{
				return this.m_subReportScope;
			}
			set
			{
				this.m_subReportScope = value;
			}
		}

		// Token: 0x17002216 RID: 8726
		// (get) Token: 0x060060C5 RID: 24773 RVA: 0x0018526B File Offset: 0x0018346B
		// (set) Token: 0x060060C6 RID: 24774 RVA: 0x00185273 File Offset: 0x00183473
		internal bool IsMatrixCellScope
		{
			get
			{
				return this.m_isMatrixCellScope;
			}
			set
			{
				this.m_isMatrixCellScope = value;
			}
		}

		// Token: 0x17002217 RID: 8727
		// (get) Token: 0x060060C7 RID: 24775 RVA: 0x0018527C File Offset: 0x0018347C
		// (set) Token: 0x060060C8 RID: 24776 RVA: 0x00185284 File Offset: 0x00183484
		internal bool IsDetailScope
		{
			get
			{
				return this.m_isDetailScope;
			}
			set
			{
				this.m_isDetailScope = value;
			}
		}

		// Token: 0x17002218 RID: 8728
		// (get) Token: 0x060060C9 RID: 24777 RVA: 0x0018528D File Offset: 0x0018348D
		// (set) Token: 0x060060CA RID: 24778 RVA: 0x00185295 File Offset: 0x00183495
		internal SubReportList DetailScopeSubReports
		{
			get
			{
				return this.m_detailScopeSubReports;
			}
			set
			{
				this.m_detailScopeSubReports = value;
			}
		}

		// Token: 0x17002219 RID: 8729
		// (get) Token: 0x060060CB RID: 24779 RVA: 0x0018529E File Offset: 0x0018349E
		// (set) Token: 0x060060CC RID: 24780 RVA: 0x001852A6 File Offset: 0x001834A6
		internal ScopeLookupTable DataSetUniqueNameMap
		{
			get
			{
				return this.m_dataSetUniqueNameMap;
			}
			set
			{
				this.m_dataSetUniqueNameMap = value;
			}
		}

		// Token: 0x1700221A RID: 8730
		// (get) Token: 0x060060CD RID: 24781 RVA: 0x001852AF File Offset: 0x001834AF
		internal bool SaveDataSetUniqueName
		{
			get
			{
				return this.m_saveDataSetUniqueName;
			}
		}

		// Token: 0x060060CE RID: 24782 RVA: 0x001852B8 File Offset: 0x001834B8
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			this.m_subReportScope = context.GetCurrentScope();
			if ((LocationFlags)0 < (context.Location & LocationFlags.InMatrixCellTopLevelItem))
			{
				this.m_isMatrixCellScope = true;
			}
			if ((LocationFlags)0 < (context.Location & LocationFlags.InDetail))
			{
				this.m_isDetailScope = true;
				context.SetDataSetDetailUserSortFilter();
			}
			context.ExprHostBuilder.SubreportStart(this.m_name);
			base.Initialize(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, false, false);
			}
			if (this.m_parameters != null)
			{
				for (int i = 0; i < this.m_parameters.Count; i++)
				{
					ParameterValue parameterValue = this.m_parameters[i];
					context.ExprHostBuilder.SubreportParameterStart();
					parameterValue.Initialize(context, false);
					parameterValue.ExprHostID = context.ExprHostBuilder.SubreportParameterEnd();
				}
			}
			if (this.m_noRows != null)
			{
				this.m_noRows.Initialize("NoRows", context);
				context.ExprHostBuilder.GenericNoRows(this.m_noRows);
			}
			base.ExprHostID = context.ExprHostBuilder.SubreportEnd();
			return false;
		}

		// Token: 0x060060CF RID: 24783 RVA: 0x001853DC File Offset: 0x001835DC
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.SubreportHostsRemotable[base.ExprHostID];
				base.ReportItemSetExprHost(this.m_exprHost, reportObjectModel);
				if (this.m_exprHost.ParameterHostsRemotable != null)
				{
					Global.Tracer.Assert(this.m_parameters != null);
					for (int i = this.m_parameters.Count - 1; i >= 0; i--)
					{
						this.m_parameters[i].SetExprHost(this.m_exprHost.ParameterHostsRemotable, reportObjectModel);
					}
				}
			}
		}

		// Token: 0x060060D0 RID: 24784 RVA: 0x00185482 File Offset: 0x00183682
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

		// Token: 0x060060D1 RID: 24785 RVA: 0x001854B6 File Offset: 0x001836B6
		bool IPageBreakItem.HasPageBreaks(bool atStart)
		{
			return false;
		}

		// Token: 0x060060D2 RID: 24786 RVA: 0x001854BC File Offset: 0x001836BC
		internal void UpdateSubReportScopes(UserSortFilterContext context)
		{
			if (this.m_containingScopes != null && 0 < this.m_containingScopes.Count && this.m_containingScopes.LastEntry == null)
			{
				if (context.DetailScopeSubReports != null)
				{
					this.m_detailScopeSubReports = context.DetailScopeSubReports.Clone();
				}
				else
				{
					this.m_detailScopeSubReports = new SubReportList();
				}
				this.m_detailScopeSubReports.Add(this);
			}
			else
			{
				this.m_detailScopeSubReports = context.DetailScopeSubReports;
			}
			if (context.ContainingScopes != null)
			{
				if (this.m_containingScopes != null && 0 < this.m_containingScopes.Count)
				{
					this.m_containingScopes.InsertRange(0, context.ContainingScopes);
					return;
				}
				this.m_containingScopes = context.ContainingScopes;
			}
		}

		// Token: 0x060060D3 RID: 24787 RVA: 0x0018556A File Offset: 0x0018376A
		internal void AddDataSetUniqueName(VariantList[] scopeValues, int subReportUniqueName)
		{
			if (this.m_dataSetUniqueNameMap == null)
			{
				this.m_dataSetUniqueNameMap = new ScopeLookupTable();
				this.m_saveDataSetUniqueName = true;
			}
			this.m_dataSetUniqueNameMap.Add(this.m_containingScopes, scopeValues, subReportUniqueName);
		}

		// Token: 0x060060D4 RID: 24788 RVA: 0x00185599 File Offset: 0x00183799
		internal int GetDataSetUniqueName(VariantList[] scopeValues)
		{
			Global.Tracer.Assert(this.m_dataSetUniqueNameMap != null);
			return this.m_dataSetUniqueNameMap.Lookup(this.m_containingScopes, scopeValues);
		}

		// Token: 0x060060D5 RID: 24789 RVA: 0x001855C0 File Offset: 0x001837C0
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItem, new MemberInfoList
			{
				new MemberInfo(MemberName.ReportPath, Token.String),
				new MemberInfo(MemberName.Parameters, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ParameterValueList),
				new MemberInfo(MemberName.NoRows, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.MergeTransactions, Token.Boolean),
				new MemberInfo(MemberName.ContainingScopes, Token.Reference, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.GroupingList),
				new MemberInfo(MemberName.IsMatrixCellScope, Token.Boolean),
				new MemberInfo(MemberName.DataSetUniqueNameMap, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ScopeLookupTable),
				new MemberInfo(MemberName.Status, Token.Enum),
				new MemberInfo(MemberName.ReportName, Token.String),
				new MemberInfo(MemberName.Description, Token.String),
				new MemberInfo(MemberName.Report, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.Report),
				new MemberInfo(MemberName.StringUri, Token.String),
				new MemberInfo(MemberName.ParametersFromCatalog, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ParameterInfoCollection)
			});
		}

		// Token: 0x04003109 RID: 12553
		internal const uint MaxSubReportLevel = 20U;

		// Token: 0x0400310A RID: 12554
		private string m_reportPath;

		// Token: 0x0400310B RID: 12555
		private ParameterValueList m_parameters;

		// Token: 0x0400310C RID: 12556
		private ExpressionInfo m_noRows;

		// Token: 0x0400310D RID: 12557
		private bool m_mergeTransactions;

		// Token: 0x0400310E RID: 12558
		[Reference]
		private GroupingList m_containingScopes;

		// Token: 0x0400310F RID: 12559
		private bool m_isMatrixCellScope;

		// Token: 0x04003110 RID: 12560
		private SubReport.Status m_status;

		// Token: 0x04003111 RID: 12561
		private string m_reportName;

		// Token: 0x04003112 RID: 12562
		private string m_description;

		// Token: 0x04003113 RID: 12563
		private Report m_report;

		// Token: 0x04003114 RID: 12564
		private string m_stringUri;

		// Token: 0x04003115 RID: 12565
		private ParameterInfoCollection m_parametersFromCatalog;

		// Token: 0x04003116 RID: 12566
		private ScopeLookupTable m_dataSetUniqueNameMap;

		// Token: 0x04003117 RID: 12567
		[NonSerialized]
		private string m_subReportScope;

		// Token: 0x04003118 RID: 12568
		[NonSerialized]
		private bool m_isDetailScope;

		// Token: 0x04003119 RID: 12569
		[NonSerialized]
		private PageBreakStates m_pagebreakState;

		// Token: 0x0400311A RID: 12570
		[NonSerialized]
		private SubreportExprHost m_exprHost;

		// Token: 0x0400311B RID: 12571
		[NonSerialized]
		private SubReportList m_detailScopeSubReports;

		// Token: 0x0400311C RID: 12572
		[NonSerialized]
		private bool m_saveDataSetUniqueName;

		// Token: 0x0400311D RID: 12573
		[NonSerialized]
		private Uri m_uri;

		// Token: 0x0400311E RID: 12574
		[NonSerialized]
		private ICatalogItemContext m_reportContext;

		// Token: 0x02000CC4 RID: 3268
		internal enum Status
		{
			// Token: 0x04004E8E RID: 20110
			NotRetrieved,
			// Token: 0x04004E8F RID: 20111
			Retrieved,
			// Token: 0x04004E90 RID: 20112
			RetrieveFailed,
			// Token: 0x04004E91 RID: 20113
			PreFetched
		}
	}
}

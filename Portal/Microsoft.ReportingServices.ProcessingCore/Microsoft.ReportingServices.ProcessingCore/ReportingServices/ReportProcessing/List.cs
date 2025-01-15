using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006EE RID: 1774
	[Serializable]
	internal sealed class List : DataRegion
	{
		// Token: 0x06006188 RID: 24968 RVA: 0x0018690B File Offset: 0x00184B0B
		internal List(Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x06006189 RID: 24969 RVA: 0x00186922 File Offset: 0x00184B22
		internal List(int id, int idForListContent, int idForReportItems, Microsoft.ReportingServices.ReportProcessing.ReportItem parent)
			: base(id, parent)
		{
			this.m_hierarchyDef = new ReportHierarchyNode(idForListContent, this);
			this.m_reportItems = new ReportItemCollection(idForReportItems, true);
		}

		// Token: 0x1700225F RID: 8799
		// (get) Token: 0x0600618A RID: 24970 RVA: 0x00186955 File Offset: 0x00184B55
		internal override ObjectType ObjectType
		{
			get
			{
				return ObjectType.List;
			}
		}

		// Token: 0x17002260 RID: 8800
		// (get) Token: 0x0600618B RID: 24971 RVA: 0x00186959 File Offset: 0x00184B59
		// (set) Token: 0x0600618C RID: 24972 RVA: 0x00186966 File Offset: 0x00184B66
		internal Grouping Grouping
		{
			get
			{
				return this.m_hierarchyDef.Grouping;
			}
			set
			{
				this.m_hierarchyDef.Grouping = value;
			}
		}

		// Token: 0x17002261 RID: 8801
		// (get) Token: 0x0600618D RID: 24973 RVA: 0x00186974 File Offset: 0x00184B74
		// (set) Token: 0x0600618E RID: 24974 RVA: 0x00186981 File Offset: 0x00184B81
		internal Sorting Sorting
		{
			get
			{
				return this.m_hierarchyDef.Sorting;
			}
			set
			{
				this.m_hierarchyDef.Sorting = value;
			}
		}

		// Token: 0x17002262 RID: 8802
		// (get) Token: 0x0600618F RID: 24975 RVA: 0x0018698F File Offset: 0x00184B8F
		// (set) Token: 0x06006190 RID: 24976 RVA: 0x00186997 File Offset: 0x00184B97
		internal ReportHierarchyNode HierarchyDef
		{
			get
			{
				return this.m_hierarchyDef;
			}
			set
			{
				this.m_hierarchyDef = value;
			}
		}

		// Token: 0x17002263 RID: 8803
		// (get) Token: 0x06006191 RID: 24977 RVA: 0x001869A0 File Offset: 0x00184BA0
		// (set) Token: 0x06006192 RID: 24978 RVA: 0x001869A8 File Offset: 0x00184BA8
		internal ReportItemCollection ReportItems
		{
			get
			{
				return this.m_reportItems;
			}
			set
			{
				this.m_reportItems = value;
			}
		}

		// Token: 0x17002264 RID: 8804
		// (get) Token: 0x06006193 RID: 24979 RVA: 0x001869B1 File Offset: 0x00184BB1
		// (set) Token: 0x06006194 RID: 24980 RVA: 0x001869B9 File Offset: 0x00184BB9
		internal bool FillPage
		{
			get
			{
				return this.m_fillPage;
			}
			set
			{
				this.m_fillPage = value;
			}
		}

		// Token: 0x17002265 RID: 8805
		// (get) Token: 0x06006195 RID: 24981 RVA: 0x001869C2 File Offset: 0x00184BC2
		internal int ListContentID
		{
			get
			{
				return this.m_hierarchyDef.ID;
			}
		}

		// Token: 0x17002266 RID: 8806
		// (get) Token: 0x06006196 RID: 24982 RVA: 0x001869CF File Offset: 0x00184BCF
		// (set) Token: 0x06006197 RID: 24983 RVA: 0x001869D7 File Offset: 0x00184BD7
		internal string DataInstanceName
		{
			get
			{
				return this.m_dataInstanceName;
			}
			set
			{
				this.m_dataInstanceName = value;
			}
		}

		// Token: 0x17002267 RID: 8807
		// (get) Token: 0x06006198 RID: 24984 RVA: 0x001869E0 File Offset: 0x00184BE0
		// (set) Token: 0x06006199 RID: 24985 RVA: 0x001869E8 File Offset: 0x00184BE8
		internal DataElementOutputTypes DataInstanceElementOutput
		{
			get
			{
				return this.m_dataInstanceElementOutput;
			}
			set
			{
				this.m_dataInstanceElementOutput = value;
			}
		}

		// Token: 0x17002268 RID: 8808
		// (get) Token: 0x0600619A RID: 24986 RVA: 0x001869F1 File Offset: 0x00184BF1
		// (set) Token: 0x0600619B RID: 24987 RVA: 0x001869F9 File Offset: 0x00184BF9
		internal bool IsListMostInner
		{
			get
			{
				return this.m_isListMostInner;
			}
			set
			{
				this.m_isListMostInner = value;
			}
		}

		// Token: 0x17002269 RID: 8809
		// (get) Token: 0x0600619C RID: 24988 RVA: 0x00186A02 File Offset: 0x00184C02
		internal bool PropagatedPageBreakAtStart
		{
			get
			{
				return this.Grouping != null && this.Grouping.PageBreakAtStart;
			}
		}

		// Token: 0x1700226A RID: 8810
		// (get) Token: 0x0600619D RID: 24989 RVA: 0x00186A19 File Offset: 0x00184C19
		internal bool PropagatedPageBreakAtEnd
		{
			get
			{
				return this.Grouping != null && this.Grouping.PageBreakAtEnd;
			}
		}

		// Token: 0x1700226B RID: 8811
		// (get) Token: 0x0600619E RID: 24990 RVA: 0x00186A30 File Offset: 0x00184C30
		internal ListExprHost ListExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x1700226C RID: 8812
		// (get) Token: 0x0600619F RID: 24991 RVA: 0x00186A38 File Offset: 0x00184C38
		// (set) Token: 0x060061A0 RID: 24992 RVA: 0x00186A40 File Offset: 0x00184C40
		internal int ContentStartPage
		{
			get
			{
				return this.m_ContentStartPage;
			}
			set
			{
				this.m_ContentStartPage = value;
			}
		}

		// Token: 0x1700226D RID: 8813
		// (get) Token: 0x060061A1 RID: 24993 RVA: 0x00186A49 File Offset: 0x00184C49
		// (set) Token: 0x060061A2 RID: 24994 RVA: 0x00186A51 File Offset: 0x00184C51
		internal int KeepWithChildFirstPage
		{
			get
			{
				return this.m_keepWithChildFirstPage;
			}
			set
			{
				this.m_keepWithChildFirstPage = value;
			}
		}

		// Token: 0x1700226E RID: 8814
		// (get) Token: 0x060061A3 RID: 24995 RVA: 0x00186A5A File Offset: 0x00184C5A
		protected override DataRegionExprHost DataRegionExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x060061A4 RID: 24996 RVA: 0x00186A62 File Offset: 0x00184C62
		internal override void CalculateSizes(double width, double height, InitializationContext context, bool overwrite)
		{
			base.CalculateSizes(width, height, context, overwrite);
			this.m_reportItems.CalculateSizes(context, false);
		}

		// Token: 0x060061A5 RID: 24997 RVA: 0x00186A7C File Offset: 0x00184C7C
		internal override bool Initialize(InitializationContext context)
		{
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			context.RegisterDataRegion(this);
			this.InternalInitialize(context);
			context.UnRegisterDataRegion(this);
			return false;
		}

		// Token: 0x060061A6 RID: 24998 RVA: 0x00186AB0 File Offset: 0x00184CB0
		private void InternalInitialize(InitializationContext context)
		{
			context.Location = context.Location | LocationFlags.InDataSet | LocationFlags.InDataRegion;
			context.ExprHostBuilder.ListStart(this.m_name);
			base.Initialize(context);
			context.Location &= ~LocationFlags.InMatrixCellTopLevelItem;
			if (this.Grouping != null)
			{
				context.Location |= LocationFlags.InGrouping;
			}
			else
			{
				context.Location |= LocationFlags.InDetail;
				context.DetailObjectType = ObjectType.List;
			}
			if (this.Grouping != null)
			{
				context.RegisterGroupingScope(this.Grouping.Name, this.Grouping.SimpleGroupExpressions, this.Grouping.Aggregates, this.Grouping.PostSortAggregates, this.Grouping.RecursiveAggregates, this.Grouping);
			}
			Global.Tracer.Assert(this.m_hierarchyDef != null);
			this.m_hierarchyDef.Initialize(context);
			context.RegisterRunningValues(this.m_reportItems.RunningValues);
			context.RegisterReportItems(this.m_reportItems);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, true, false);
			}
			this.m_reportItems.Initialize(context, false);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
			context.UnRegisterReportItems(this.m_reportItems);
			context.UnRegisterRunningValues(this.m_reportItems.RunningValues);
			if (this.Grouping != null)
			{
				context.UnRegisterGroupingScope(this.Grouping.Name);
			}
			base.ExprHostID = context.ExprHostBuilder.ListEnd();
		}

		// Token: 0x060061A7 RID: 24999 RVA: 0x00186C3D File Offset: 0x00184E3D
		protected override void DataRendererInitialize(InitializationContext context)
		{
			base.DataRendererInitialize(context);
			CLSNameValidator.ValidateDataElementName(ref this.m_dataInstanceName, "Item", context.ObjectType, context.ObjectName, "DataInstanceName", context.ErrorContext);
		}

		// Token: 0x060061A8 RID: 25000 RVA: 0x00186C74 File Offset: 0x00184E74
		internal override void RegisterReceiver(InitializationContext context)
		{
			context.RegisterReportItems(this.m_reportItems);
			if (this.m_visibility != null)
			{
				this.m_visibility.RegisterReceiver(context, true);
			}
			this.m_reportItems.RegisterReceiver(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
			context.UnRegisterReportItems(this.m_reportItems);
		}

		// Token: 0x060061A9 RID: 25001 RVA: 0x00186CD4 File Offset: 0x00184ED4
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (base.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null);
				this.m_exprHost = reportExprHost.ListHostsRemotable[base.ExprHostID];
				base.DataRegionSetExprHost(this.m_exprHost, reportObjectModel);
				if (this.m_exprHost.GroupingHost != null || this.m_exprHost.SortingHost != null)
				{
					Global.Tracer.Assert(this.m_hierarchyDef != null);
					this.m_hierarchyDef.ReportHierarchyNodeSetExprHost(this.m_exprHost.GroupingHost, this.m_exprHost.SortingHost, reportObjectModel);
				}
			}
		}

		// Token: 0x060061AA RID: 25002 RVA: 0x00186D74 File Offset: 0x00184F74
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.DataRegion, new MemberInfoList
			{
				new MemberInfo(MemberName.HierarchyDef, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportHierarchyNode),
				new MemberInfo(MemberName.ReportItems, Microsoft.ReportingServices.ReportProcessing.Persistence.ObjectType.ReportItemCollection),
				new MemberInfo(MemberName.FillPage, Token.Boolean),
				new MemberInfo(MemberName.DataInstanceName, Token.String),
				new MemberInfo(MemberName.DataInstanceElementOutput, Token.Enum),
				new MemberInfo(MemberName.IsListMostInner, Token.Boolean)
			});
		}

		// Token: 0x0400315B RID: 12635
		private ReportHierarchyNode m_hierarchyDef;

		// Token: 0x0400315C RID: 12636
		private ReportItemCollection m_reportItems;

		// Token: 0x0400315D RID: 12637
		private bool m_fillPage;

		// Token: 0x0400315E RID: 12638
		private string m_dataInstanceName;

		// Token: 0x0400315F RID: 12639
		private DataElementOutputTypes m_dataInstanceElementOutput;

		// Token: 0x04003160 RID: 12640
		private bool m_isListMostInner;

		// Token: 0x04003161 RID: 12641
		[NonSerialized]
		private ListExprHost m_exprHost;

		// Token: 0x04003162 RID: 12642
		[NonSerialized]
		private int m_ContentStartPage = -1;

		// Token: 0x04003163 RID: 12643
		[NonSerialized]
		private int m_keepWithChildFirstPage = -1;
	}
}

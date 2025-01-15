using System;
using Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006ED RID: 1773
	[Serializable]
	internal class ReportHierarchyNode : IDOwner, IPageBreakItem
	{
		// Token: 0x06006177 RID: 24951 RVA: 0x00186721 File Offset: 0x00184921
		internal ReportHierarchyNode()
		{
		}

		// Token: 0x06006178 RID: 24952 RVA: 0x00186729 File Offset: 0x00184929
		internal ReportHierarchyNode(int id, DataRegion dataRegionDef)
			: base(id)
		{
			this.m_dataRegionDef = dataRegionDef;
		}

		// Token: 0x1700225B RID: 8795
		// (get) Token: 0x06006179 RID: 24953 RVA: 0x00186739 File Offset: 0x00184939
		// (set) Token: 0x0600617A RID: 24954 RVA: 0x00186741 File Offset: 0x00184941
		internal Grouping Grouping
		{
			get
			{
				return this.m_grouping;
			}
			set
			{
				this.m_grouping = value;
				if (this.m_grouping != null)
				{
					this.m_grouping.Owner = this;
				}
			}
		}

		// Token: 0x1700225C RID: 8796
		// (get) Token: 0x0600617B RID: 24955 RVA: 0x0018675E File Offset: 0x0018495E
		// (set) Token: 0x0600617C RID: 24956 RVA: 0x00186766 File Offset: 0x00184966
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

		// Token: 0x1700225D RID: 8797
		// (get) Token: 0x0600617D RID: 24957 RVA: 0x0018676F File Offset: 0x0018496F
		// (set) Token: 0x0600617E RID: 24958 RVA: 0x00186777 File Offset: 0x00184977
		internal ReportHierarchyNode InnerHierarchy
		{
			get
			{
				return this.m_innerHierarchy;
			}
			set
			{
				this.m_innerHierarchy = value;
			}
		}

		// Token: 0x1700225E RID: 8798
		// (get) Token: 0x0600617F RID: 24959 RVA: 0x00186780 File Offset: 0x00184980
		// (set) Token: 0x06006180 RID: 24960 RVA: 0x00186788 File Offset: 0x00184988
		internal DataRegion DataRegionDef
		{
			get
			{
				return this.m_dataRegionDef;
			}
			set
			{
				this.m_dataRegionDef = value;
			}
		}

		// Token: 0x06006181 RID: 24961 RVA: 0x00186791 File Offset: 0x00184991
		internal void Initialize(InitializationContext context)
		{
			if (this.m_grouping != null)
			{
				this.m_grouping.Initialize(context);
			}
			if (this.m_sorting != null)
			{
				this.m_sorting.Initialize(context);
			}
		}

		// Token: 0x06006182 RID: 24962 RVA: 0x001867BB File Offset: 0x001849BB
		bool IPageBreakItem.IgnorePageBreaks()
		{
			return false;
		}

		// Token: 0x06006183 RID: 24963 RVA: 0x001867BE File Offset: 0x001849BE
		protected bool IgnorePageBreaks(Visibility visibility)
		{
			if (this.m_pagebreakState == PageBreakStates.Unknown)
			{
				if (SharedHiddenState.Never != Visibility.GetSharedHidden(visibility))
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

		// Token: 0x06006184 RID: 24964 RVA: 0x001867ED File Offset: 0x001849ED
		bool IPageBreakItem.HasPageBreaks(bool atStart)
		{
			return this.m_grouping != null && ((atStart && this.m_grouping.PageBreakAtStart) || (!atStart && this.m_grouping.PageBreakAtEnd));
		}

		// Token: 0x06006185 RID: 24965 RVA: 0x0018681C File Offset: 0x00184A1C
		protected void ReportHierarchyNodeSetExprHost(DynamicGroupExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(exprHost != null);
			this.m_exprHost = exprHost;
			this.m_exprHost.SetReportObjectModel(reportObjectModel);
			this.ReportHierarchyNodeSetExprHost(this.m_exprHost.GroupingHost, this.m_exprHost.SortingHost, reportObjectModel);
		}

		// Token: 0x06006186 RID: 24966 RVA: 0x0018685C File Offset: 0x00184A5C
		internal void ReportHierarchyNodeSetExprHost(GroupingExprHost groupingExprHost, SortingExprHost sortingExprHost, ObjectModelImpl reportObjectModel)
		{
			if (groupingExprHost != null)
			{
				Global.Tracer.Assert(this.m_grouping != null);
				this.m_grouping.SetExprHost(groupingExprHost, reportObjectModel);
			}
			if (sortingExprHost != null)
			{
				Global.Tracer.Assert(this.m_sorting != null);
				this.m_sorting.SetExprHost(sortingExprHost, reportObjectModel);
			}
		}

		// Token: 0x06006187 RID: 24967 RVA: 0x001868B0 File Offset: 0x00184AB0
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.IDOwner, new MemberInfoList
			{
				new MemberInfo(MemberName.Grouping, ObjectType.Grouping),
				new MemberInfo(MemberName.Sorting, ObjectType.Sorting),
				new MemberInfo(MemberName.InnerHierarchy, ObjectType.ReportHierarchyNode),
				new MemberInfo(MemberName.DataRegionDef, Token.Reference, ObjectType.DataRegion)
			});
		}

		// Token: 0x04003155 RID: 12629
		protected Grouping m_grouping;

		// Token: 0x04003156 RID: 12630
		protected Sorting m_sorting;

		// Token: 0x04003157 RID: 12631
		protected ReportHierarchyNode m_innerHierarchy;

		// Token: 0x04003158 RID: 12632
		[Reference]
		protected DataRegion m_dataRegionDef;

		// Token: 0x04003159 RID: 12633
		[NonSerialized]
		private PageBreakStates m_pagebreakState;

		// Token: 0x0400315A RID: 12634
		[NonSerialized]
		private DynamicGroupExprHost m_exprHost;
	}
}

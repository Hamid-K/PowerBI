using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000278 RID: 632
	internal sealed class ShimChartMember : Microsoft.ReportingServices.OnDemandReportRendering.ChartMember, IShimDataRegionMember
	{
		// Token: 0x06001893 RID: 6291 RVA: 0x000651D8 File Offset: 0x000633D8
		internal ShimChartMember(IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.OnDemandReportRendering.Chart owner, ShimChartMember parent, int parentCollectionIndex, bool isCategory, Microsoft.ReportingServices.ReportRendering.ChartMember staticOrSubtotal)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex)
		{
			this.m_definitionStartIndex = owner.GetCurrentMemberCellDefinitionIndex();
			this.m_isCategory = isCategory;
			this.m_staticOrSubtotal = staticOrSubtotal;
			this.GenerateInnerHierarchy(owner, parent, isCategory, staticOrSubtotal.Children);
			this.m_definitionEndIndex = owner.GetCurrentMemberCellDefinitionIndex();
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x00065238 File Offset: 0x00063438
		internal ShimChartMember(IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.OnDemandReportRendering.Chart owner, ShimChartMember parent, int parentCollectionIndex, bool isCategory, ShimRenderGroups renderGroups)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex)
		{
			this.m_definitionStartIndex = owner.GetCurrentMemberCellDefinitionIndex();
			this.m_isCategory = isCategory;
			this.m_group = new Group(owner, renderGroups);
			this.GenerateInnerHierarchy(owner, parent, isCategory, ((Microsoft.ReportingServices.ReportRendering.ChartMember)renderGroups[0]).Children);
			this.m_definitionEndIndex = owner.GetCurrentMemberCellDefinitionIndex();
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x000652A8 File Offset: 0x000634A8
		private void GenerateInnerHierarchy(Microsoft.ReportingServices.OnDemandReportRendering.Chart owner, ShimChartMember parent, bool isCategory, ChartMemberCollection children)
		{
			if (children != null)
			{
				this.m_children = new ShimChartMemberCollection(this, owner, isCategory, this, children);
				return;
			}
			owner.GetAndIncrementMemberCellDefinitionIndex();
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x000652C7 File Offset: 0x000634C7
		internal override string UniqueName
		{
			get
			{
				return this.ID;
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x000652CF File Offset: 0x000634CF
		public override string ID
		{
			get
			{
				if (this.IsStatic)
				{
					return base.DefinitionPath;
				}
				return ((Microsoft.ReportingServices.ReportRendering.ChartMember)this.m_group.CurrentShimRenderGroup).ID;
			}
		}

		// Token: 0x17000E16 RID: 3606
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x000652F8 File Offset: 0x000634F8
		public override ReportStringProperty Label
		{
			get
			{
				if (this.m_label == null)
				{
					Microsoft.ReportingServices.ReportProcessing.ExpressionInfo expressionInfo;
					if (this.IsStatic)
					{
						expressionInfo = this.m_staticOrSubtotal.LabelDefinition;
					}
					else
					{
						expressionInfo = ((Microsoft.ReportingServices.ReportRendering.ChartMember)this.m_group.CurrentShimRenderGroup).LabelDefinition;
					}
					this.m_label = new ReportStringProperty(expressionInfo);
				}
				return this.m_label;
			}
		}

		// Token: 0x17000E17 RID: 3607
		// (get) Token: 0x06001899 RID: 6297 RVA: 0x0006534D File Offset: 0x0006354D
		internal object LabelInstanceValue
		{
			get
			{
				if (this.IsStatic)
				{
					return this.m_staticOrSubtotal.LabelValue;
				}
				return ((Microsoft.ReportingServices.ReportRendering.ChartMember)this.m_group.CurrentShimRenderGroup).LabelValue;
			}
		}

		// Token: 0x17000E18 RID: 3608
		// (get) Token: 0x0600189A RID: 6298 RVA: 0x00065378 File Offset: 0x00063578
		public override string DataElementName
		{
			get
			{
				if (this.m_staticOrSubtotal != null)
				{
					return this.m_staticOrSubtotal.DataElementName;
				}
				if (this.m_group != null && this.m_group.CurrentShimRenderGroup != null)
				{
					return this.m_group.CurrentShimRenderGroup.DataCollectionName;
				}
				return null;
			}
		}

		// Token: 0x17000E19 RID: 3609
		// (get) Token: 0x0600189B RID: 6299 RVA: 0x000653B5 File Offset: 0x000635B5
		public override DataElementOutputTypes DataElementOutput
		{
			get
			{
				if (this.m_staticOrSubtotal != null)
				{
					return (DataElementOutputTypes)this.m_staticOrSubtotal.DataElementOutput;
				}
				return DataElementOutputTypes.Output;
			}
		}

		// Token: 0x17000E1A RID: 3610
		// (get) Token: 0x0600189C RID: 6300 RVA: 0x000653CC File Offset: 0x000635CC
		public override ChartMemberCollection Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x17000E1B RID: 3611
		// (get) Token: 0x0600189D RID: 6301 RVA: 0x000653D4 File Offset: 0x000635D4
		public override CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.m_customPropertyCollection == null)
				{
					if (this.m_group != null && this.m_group.CustomProperties != null)
					{
						this.m_customPropertyCollection = this.m_group.CustomProperties;
					}
					else
					{
						this.m_customPropertyCollection = new CustomPropertyCollection();
					}
				}
				return this.m_customPropertyCollection;
			}
		}

		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x00065422 File Offset: 0x00063622
		public override bool IsStatic
		{
			get
			{
				return this.m_staticOrSubtotal != null;
			}
		}

		// Token: 0x17000E1D RID: 3613
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x0006542D File Offset: 0x0006362D
		public override bool IsCategory
		{
			get
			{
				return this.m_isCategory;
			}
		}

		// Token: 0x17000E1E RID: 3614
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x00065435 File Offset: 0x00063635
		public override int SeriesSpan
		{
			get
			{
				if (this.m_isCategory)
				{
					return 1;
				}
				return this.m_definitionEndIndex - this.m_definitionStartIndex;
			}
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x0006544E File Offset: 0x0006364E
		public override int CategorySpan
		{
			get
			{
				if (this.m_isCategory)
				{
					return this.m_definitionEndIndex - this.m_definitionStartIndex;
				}
				return 1;
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x060018A2 RID: 6306 RVA: 0x00065467 File Offset: 0x00063667
		public override int MemberCellIndex
		{
			get
			{
				return this.m_definitionStartIndex;
			}
		}

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x060018A3 RID: 6307 RVA: 0x0006546F File Offset: 0x0006366F
		public override bool IsTotal
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x060018A4 RID: 6308 RVA: 0x00065472 File Offset: 0x00063672
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.ChartMember MemberDefinition
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x00065475 File Offset: 0x00063675
		internal override IReportScope ReportScope
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x060018A6 RID: 6310 RVA: 0x00065478 File Offset: 0x00063678
		internal override IRIFReportScope RIFReportScope
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x0006547B File Offset: 0x0006367B
		internal override IReportScopeInstance ReportScopeInstance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x00065480 File Offset: 0x00063680
		public override ChartMemberInstance Instance
		{
			get
			{
				if (base.OwnerChart.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					if (this.IsStatic)
					{
						this.m_instance = new ChartMemberInstance(base.OwnerChart, this);
					}
					else
					{
						ChartDynamicMemberInstance chartDynamicMemberInstance = new ChartDynamicMemberInstance(base.OwnerChart, this, new InternalShimDynamicMemberLogic(this));
						this.m_owner.RenderingContext.AddDynamicInstance(chartDynamicMemberInstance);
						this.m_instance = chartDynamicMemberInstance;
					}
				}
				return this.m_instance;
			}
		}

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x000654F6 File Offset: 0x000636F6
		internal int DefinitionStartIndex
		{
			get
			{
				return this.m_definitionStartIndex;
			}
		}

		// Token: 0x17000E28 RID: 3624
		// (get) Token: 0x060018AA RID: 6314 RVA: 0x000654FE File Offset: 0x000636FE
		internal int DefinitionEndIndex
		{
			get
			{
				return this.m_definitionEndIndex;
			}
		}

		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x00065506 File Offset: 0x00063706
		internal Microsoft.ReportingServices.ReportRendering.ChartMember CurrentRenderChartMember
		{
			get
			{
				if (this.m_staticOrSubtotal != null)
				{
					return this.m_staticOrSubtotal;
				}
				return this.m_group.CurrentShimRenderGroup as Microsoft.ReportingServices.ReportRendering.ChartMember;
			}
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x00065528 File Offset: 0x00063728
		internal bool SetNewContext(int index)
		{
			base.ResetContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_group == null)
			{
				return index <= 1;
			}
			if (base.OwnerChart.RenderChart.NoRows)
			{
				return false;
			}
			if (index < 0 || index >= this.m_group.RenderGroups.Count)
			{
				return false;
			}
			this.m_group.CurrentRenderGroupIndex = index;
			this.UpdateInnerContext(this.m_group.RenderGroups[index] as Microsoft.ReportingServices.ReportRendering.ChartMember);
			return true;
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x000655B4 File Offset: 0x000637B4
		internal override void ResetContext()
		{
			this.ResetContext(null, null);
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x000655C0 File Offset: 0x000637C0
		internal void ResetContext(Microsoft.ReportingServices.ReportRendering.ChartMember staticOrSubtotal, ShimRenderGroups renderGroups)
		{
			if (this.m_group != null)
			{
				this.m_group.CurrentRenderGroupIndex = -1;
				if (renderGroups != null)
				{
					this.m_group.RenderGroups = renderGroups;
				}
			}
			else if (staticOrSubtotal != null)
			{
				this.m_staticOrSubtotal = staticOrSubtotal;
			}
			Microsoft.ReportingServices.ReportRendering.ChartMember chartMember = (this.IsStatic ? this.m_staticOrSubtotal : (this.m_group.CurrentShimRenderGroup as Microsoft.ReportingServices.ReportRendering.ChartMember));
			this.UpdateInnerContext(chartMember);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00065624 File Offset: 0x00063824
		private void UpdateInnerContext(Microsoft.ReportingServices.ReportRendering.ChartMember currentRenderGroup)
		{
			if (this.m_children != null)
			{
				((ShimChartMemberCollection)this.m_children).ResetContext(currentRenderGroup.Children);
				return;
			}
			((ShimChartSeriesCollection)base.OwnerChart.ChartData.SeriesCollection).UpdateCells(this);
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x00065660 File Offset: 0x00063860
		bool IShimDataRegionMember.SetNewContext(int index)
		{
			return this.SetNewContext(index);
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x00065669 File Offset: 0x00063869
		void IShimDataRegionMember.ResetContext()
		{
			this.ResetContext();
		}

		// Token: 0x04000C7A RID: 3194
		private bool m_isCategory;

		// Token: 0x04000C7B RID: 3195
		private int m_definitionStartIndex = -1;

		// Token: 0x04000C7C RID: 3196
		private int m_definitionEndIndex = -1;

		// Token: 0x04000C7D RID: 3197
		private Microsoft.ReportingServices.ReportRendering.ChartMember m_staticOrSubtotal;
	}
}

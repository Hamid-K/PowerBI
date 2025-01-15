using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000277 RID: 631
	internal sealed class InternalChartMember : ChartMember
	{
		// Token: 0x0600187F RID: 6271 RVA: 0x00064E9B File Offset: 0x0006309B
		internal InternalChartMember(IReportScope reportScope, IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.OnDemandReportRendering.Chart owner, ChartMember parent, ChartMember memberDef, int parentCollectionIndex)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex)
		{
			this.m_memberDef = memberDef;
			if (this.m_memberDef.IsStatic)
			{
				this.m_reportScope = reportScope;
			}
			this.m_group = new Group(owner, this.m_memberDef, this);
		}

		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06001880 RID: 6272 RVA: 0x00064ED8 File Offset: 0x000630D8
		internal override string UniqueName
		{
			get
			{
				if (this.m_uniqueName == null)
				{
					this.m_uniqueName = this.m_memberDef.UniqueName;
				}
				return this.m_uniqueName;
			}
		}

		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06001881 RID: 6273 RVA: 0x00064EF9 File Offset: 0x000630F9
		public override string ID
		{
			get
			{
				return this.m_memberDef.RenderingModelID;
			}
		}

		// Token: 0x17000E04 RID: 3588
		// (get) Token: 0x06001882 RID: 6274 RVA: 0x00064F06 File Offset: 0x00063106
		public override ReportStringProperty Label
		{
			get
			{
				if (this.m_label == null)
				{
					this.m_label = new ReportStringProperty(this.m_memberDef.Label);
				}
				return this.m_label;
			}
		}

		// Token: 0x17000E05 RID: 3589
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x00064F2C File Offset: 0x0006312C
		public override string DataElementName
		{
			get
			{
				return this.m_memberDef.DataElementName;
			}
		}

		// Token: 0x17000E06 RID: 3590
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x00064F39 File Offset: 0x00063139
		public override DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_memberDef.DataElementOutput;
			}
		}

		// Token: 0x17000E07 RID: 3591
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x00064F48 File Offset: 0x00063148
		public override ChartMemberCollection Children
		{
			get
			{
				ChartMemberList chartMembers = this.m_memberDef.ChartMembers;
				if (chartMembers == null)
				{
					return null;
				}
				if (this.m_children == null)
				{
					this.m_children = new InternalChartMemberCollection(this, base.OwnerChart, this, chartMembers);
				}
				return this.m_children;
			}
		}

		// Token: 0x17000E08 RID: 3592
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x00064F88 File Offset: 0x00063188
		public override bool IsStatic
		{
			get
			{
				return this.m_memberDef.Grouping == null;
			}
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06001887 RID: 6279 RVA: 0x00064F9A File Offset: 0x0006319A
		public override bool IsCategory
		{
			get
			{
				return this.m_memberDef.IsColumn;
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06001888 RID: 6280 RVA: 0x00064FA7 File Offset: 0x000631A7
		public override int SeriesSpan
		{
			get
			{
				return this.m_memberDef.RowSpan;
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x00064FB4 File Offset: 0x000631B4
		public override int CategorySpan
		{
			get
			{
				return this.m_memberDef.ColSpan;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x00064FC1 File Offset: 0x000631C1
		public override int MemberCellIndex
		{
			get
			{
				return this.m_memberDef.MemberCellIndex;
			}
		}

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x00064FCE File Offset: 0x000631CE
		public override bool IsTotal
		{
			get
			{
				return this.m_memberDef.IsAutoSubtotal;
			}
		}

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x0600188C RID: 6284 RVA: 0x00064FDB File Offset: 0x000631DB
		internal override ChartMember MemberDefinition
		{
			get
			{
				return this.m_memberDef;
			}
		}

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x00064FE3 File Offset: 0x000631E3
		internal override IReportScope ReportScope
		{
			get
			{
				if (this.IsStatic)
				{
					return this.m_reportScope;
				}
				return this;
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x0600188E RID: 6286 RVA: 0x00064FF5 File Offset: 0x000631F5
		internal override IRIFReportScope RIFReportScope
		{
			get
			{
				if (this.IsStatic)
				{
					return this.m_reportScope.RIFReportScope;
				}
				return this.MemberDefinition;
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x00065011 File Offset: 0x00063211
		internal override IReportScopeInstance ReportScopeInstance
		{
			get
			{
				if (this.IsStatic)
				{
					return this.m_reportScope.ReportScopeInstance;
				}
				return (IReportScopeInstance)this.Instance;
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06001890 RID: 6288 RVA: 0x00065034 File Offset: 0x00063234
		public override CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.m_customPropertyCollection == null)
				{
					string text = ((this.m_memberDef.Grouping != null) ? this.m_memberDef.Grouping.Name : base.OwnerChart.Name);
					this.m_customPropertyCollection = new CustomPropertyCollection(this.ReportScope.ReportScopeInstance, base.OwnerChart.RenderingContext, null, this.m_memberDef, ObjectType.Chart, text);
					this.m_customPropertyCollectionReady = true;
				}
				else if (!this.m_customPropertyCollectionReady)
				{
					string text2 = ((this.m_memberDef.Grouping != null) ? this.m_memberDef.Grouping.Name : base.OwnerChart.Name);
					this.m_customPropertyCollection.UpdateCustomProperties(this.ReportScope.ReportScopeInstance, this.m_memberDef, base.OwnerChart.RenderingContext.OdpContext, ObjectType.Chart, text2);
					this.m_customPropertyCollectionReady = true;
				}
				return this.m_customPropertyCollection;
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x00065118 File Offset: 0x00063318
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
						ChartDynamicMemberInstance chartDynamicMemberInstance = new ChartDynamicMemberInstance(base.OwnerChart, this, this.BuildOdpMemberLogic(base.OwnerChart.RenderingContext.OdpContext));
						this.m_owner.RenderingContext.AddDynamicInstance(chartDynamicMemberInstance);
						this.m_instance = chartDynamicMemberInstance;
					}
				}
				return this.m_instance;
			}
		}

		// Token: 0x06001892 RID: 6290 RVA: 0x0006519E File Offset: 0x0006339E
		internal override void SetNewContext(bool fromMoveNext)
		{
			if (!fromMoveNext && this.m_instance != null && !this.IsStatic)
			{
				((IDynamicInstance)this.m_instance).ResetContext();
			}
			base.SetNewContext(fromMoveNext);
			this.m_customPropertyCollectionReady = false;
			this.m_uniqueName = null;
		}

		// Token: 0x04000C76 RID: 3190
		private ChartMember m_memberDef;

		// Token: 0x04000C77 RID: 3191
		private IReportScope m_reportScope;

		// Token: 0x04000C78 RID: 3192
		private bool m_customPropertyCollectionReady;

		// Token: 0x04000C79 RID: 3193
		private string m_uniqueName;
	}
}

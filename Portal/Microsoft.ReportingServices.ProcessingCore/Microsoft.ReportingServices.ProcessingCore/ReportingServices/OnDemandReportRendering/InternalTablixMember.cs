using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000373 RID: 883
	internal sealed class InternalTablixMember : TablixMember
	{
		// Token: 0x060021A8 RID: 8616 RVA: 0x000820B4 File Offset: 0x000802B4
		internal InternalTablixMember(IReportScope reportScope, IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.OnDemandReportRendering.Tablix owner, TablixMember parent, TablixMember memberDef, int index)
			: base(parentDefinitionPath, owner, parent, index)
		{
			if (memberDef.IsStatic)
			{
				this.m_reportScope = reportScope;
			}
			this.m_owner = owner;
			this.m_memberDef = memberDef;
			if (this.m_memberDef.Grouping != null)
			{
				this.m_group = new Group(base.OwnerTablix, this.m_memberDef, this);
			}
			this.m_memberDef.ROMScopeInstance = this.ReportScope.ReportScopeInstance;
			this.m_memberDef.ResetVisibilityComputationCache();
		}

		// Token: 0x170012F7 RID: 4855
		// (get) Token: 0x060021A9 RID: 8617 RVA: 0x00082132 File Offset: 0x00080332
		internal override string UniqueName
		{
			get
			{
				return this.m_memberDef.UniqueName;
			}
		}

		// Token: 0x170012F8 RID: 4856
		// (get) Token: 0x060021AA RID: 8618 RVA: 0x0008213F File Offset: 0x0008033F
		public override string ID
		{
			get
			{
				return this.m_memberDef.RenderingModelID;
			}
		}

		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x060021AB RID: 8619 RVA: 0x0008214C File Offset: 0x0008034C
		public override string DataElementName
		{
			get
			{
				return this.m_memberDef.DataElementName;
			}
		}

		// Token: 0x170012FA RID: 4858
		// (get) Token: 0x060021AC RID: 8620 RVA: 0x00082159 File Offset: 0x00080359
		public override DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_memberDef.DataElementOutput;
			}
		}

		// Token: 0x170012FB RID: 4859
		// (get) Token: 0x060021AD RID: 8621 RVA: 0x00082166 File Offset: 0x00080366
		public override TablixHeader TablixHeader
		{
			get
			{
				if (this.m_header == null && this.m_memberDef.TablixHeader != null)
				{
					this.m_header = new TablixHeader(base.OwnerTablix, this);
				}
				return this.m_header;
			}
		}

		// Token: 0x170012FC RID: 4860
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x00082198 File Offset: 0x00080398
		public override TablixMemberCollection Children
		{
			get
			{
				TablixMemberList subMembers = this.m_memberDef.SubMembers;
				if (subMembers == null)
				{
					return null;
				}
				if (this.m_children == null)
				{
					this.m_children = new InternalTablixMemberCollection(this, base.OwnerTablix, this, subMembers);
				}
				return this.m_children;
			}
		}

		// Token: 0x170012FD RID: 4861
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x000821D8 File Offset: 0x000803D8
		public override CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.m_customPropertyCollection == null)
				{
					string text = ((this.m_memberDef.Grouping != null) ? this.m_memberDef.Grouping.Name : base.OwnerTablix.Name);
					this.m_customPropertyCollection = new CustomPropertyCollection(this.ReportScope.ReportScopeInstance, base.OwnerTablix.RenderingContext, null, this.m_memberDef, ObjectType.Tablix, text);
					this.m_customPropertyCollectionReady = true;
				}
				else if (!this.m_customPropertyCollectionReady)
				{
					string text2 = ((this.m_memberDef.Grouping != null) ? this.m_memberDef.Grouping.Name : base.OwnerTablix.Name);
					this.m_customPropertyCollection.UpdateCustomProperties(this.ReportScope.ReportScopeInstance, this.m_memberDef, base.OwnerTablix.RenderingContext.OdpContext, ObjectType.Tablix, text2);
					this.m_customPropertyCollectionReady = true;
				}
				return this.m_customPropertyCollection;
			}
		}

		// Token: 0x170012FE RID: 4862
		// (get) Token: 0x060021B0 RID: 8624 RVA: 0x000822BB File Offset: 0x000804BB
		public override bool IsStatic
		{
			get
			{
				return this.m_memberDef.Grouping == null;
			}
		}

		// Token: 0x170012FF RID: 4863
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x000822CD File Offset: 0x000804CD
		public override bool IsColumn
		{
			get
			{
				return this.m_memberDef.IsColumn;
			}
		}

		// Token: 0x17001300 RID: 4864
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x000822DA File Offset: 0x000804DA
		internal override int RowSpan
		{
			get
			{
				return this.m_memberDef.RowSpan;
			}
		}

		// Token: 0x17001301 RID: 4865
		// (get) Token: 0x060021B3 RID: 8627 RVA: 0x000822E7 File Offset: 0x000804E7
		internal override int ColSpan
		{
			get
			{
				return this.m_memberDef.ColSpan;
			}
		}

		// Token: 0x17001302 RID: 4866
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x000822F4 File Offset: 0x000804F4
		public override int MemberCellIndex
		{
			get
			{
				return this.m_memberDef.MemberCellIndex;
			}
		}

		// Token: 0x17001303 RID: 4867
		// (get) Token: 0x060021B5 RID: 8629 RVA: 0x00082301 File Offset: 0x00080501
		public override bool IsTotal
		{
			get
			{
				return this.m_memberDef.IsAutoSubtotal;
			}
		}

		// Token: 0x17001304 RID: 4868
		// (get) Token: 0x060021B6 RID: 8630 RVA: 0x00082310 File Offset: 0x00080510
		internal override PageBreakLocation PropagatedGroupBreak
		{
			get
			{
				if (!this.IsStatic)
				{
					PageBreak pageBreak = this.m_group.PageBreak;
					if (pageBreak.Instance != null && !pageBreak.Instance.Disabled)
					{
						return pageBreak.BreakLocation;
					}
				}
				return PageBreakLocation.None;
			}
		}

		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x0008234E File Offset: 0x0008054E
		public override Microsoft.ReportingServices.OnDemandReportRendering.Visibility Visibility
		{
			get
			{
				if (this.m_visibility == null && this.m_memberDef.Visibility != null && !this.m_memberDef.IsAutoSubtotal)
				{
					this.m_visibility = new InternalTablixMemberVisibility(this);
				}
				return this.m_visibility;
			}
		}

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x00082384 File Offset: 0x00080584
		public override bool HideIfNoRows
		{
			get
			{
				return this.m_memberDef.HideIfNoRows;
			}
		}

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x00082391 File Offset: 0x00080591
		public override bool KeepTogether
		{
			get
			{
				return this.m_memberDef.KeepTogether;
			}
		}

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x060021BA RID: 8634 RVA: 0x0008239E File Offset: 0x0008059E
		internal override TablixMember MemberDefinition
		{
			get
			{
				return this.m_memberDef;
			}
		}

		// Token: 0x17001309 RID: 4873
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x000823A6 File Offset: 0x000805A6
		public override bool FixedData
		{
			get
			{
				return this.MemberDefinition.FixedData;
			}
		}

		// Token: 0x1700130A RID: 4874
		// (get) Token: 0x060021BC RID: 8636 RVA: 0x000823B3 File Offset: 0x000805B3
		public override KeepWithGroup KeepWithGroup
		{
			get
			{
				return this.MemberDefinition.KeepWithGroup;
			}
		}

		// Token: 0x1700130B RID: 4875
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x000823C0 File Offset: 0x000805C0
		public override bool RepeatOnNewPage
		{
			get
			{
				return this.MemberDefinition.RepeatOnNewPage;
			}
		}

		// Token: 0x1700130C RID: 4876
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x000823CD File Offset: 0x000805CD
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

		// Token: 0x1700130D RID: 4877
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x000823DF File Offset: 0x000805DF
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

		// Token: 0x1700130E RID: 4878
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x000823FB File Offset: 0x000805FB
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

		// Token: 0x1700130F RID: 4879
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x0008241C File Offset: 0x0008061C
		public override TablixMemberInstance Instance
		{
			get
			{
				if (base.OwnerTablix.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					if (this.IsStatic)
					{
						this.m_instance = new TablixMemberInstance(base.OwnerTablix, this);
					}
					else
					{
						TablixDynamicMemberInstance tablixDynamicMemberInstance = new TablixDynamicMemberInstance(base.OwnerTablix, this, this.BuildOdpMemberLogic(base.OwnerTablix.RenderingContext.OdpContext));
						this.m_owner.RenderingContext.AddDynamicInstance(tablixDynamicMemberInstance);
						this.m_instance = tablixDynamicMemberInstance;
					}
				}
				return this.m_instance;
			}
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x000824A4 File Offset: 0x000806A4
		internal override void SetNewContext(bool fromMoveNext)
		{
			if (!fromMoveNext && this.m_instance != null && !this.IsStatic)
			{
				((IDynamicInstance)this.m_instance).ResetContext();
			}
			base.SetNewContext(fromMoveNext);
			this.m_customPropertyCollectionReady = false;
			this.m_memberDef.ResetTextBoxImpls(this.m_owner.m_renderingContext.OdpContext);
		}

		// Token: 0x040010D8 RID: 4312
		private TablixMember m_memberDef;

		// Token: 0x040010D9 RID: 4313
		private bool m_customPropertyCollectionReady;

		// Token: 0x040010DA RID: 4314
		private IReportScope m_reportScope;
	}
}

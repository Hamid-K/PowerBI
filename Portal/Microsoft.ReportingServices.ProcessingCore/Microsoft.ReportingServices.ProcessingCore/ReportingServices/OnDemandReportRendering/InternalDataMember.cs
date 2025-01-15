using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200028F RID: 655
	internal sealed class InternalDataMember : DataMember
	{
		// Token: 0x0600195B RID: 6491 RVA: 0x000674F7 File Offset: 0x000656F7
		internal InternalDataMember(IReportScope reportScope, IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem owner, DataMember parent, DataMember memberDef, int parentCollectionIndex)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex)
		{
			this.m_memberDef = memberDef;
			if (this.m_memberDef.IsStatic)
			{
				this.m_reportScope = reportScope;
			}
			this.m_group = new Group(owner, this.m_memberDef, this);
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x0600195C RID: 6492 RVA: 0x00067534 File Offset: 0x00065734
		internal override string UniqueName
		{
			get
			{
				if (this.m_uniqueName != null)
				{
					this.m_uniqueName = this.m_memberDef.UniqueName;
				}
				return this.m_uniqueName;
			}
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x0600195D RID: 6493 RVA: 0x00067555 File Offset: 0x00065755
		public override int ColSpan
		{
			get
			{
				return this.m_memberDef.ColSpan;
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x0600195E RID: 6494 RVA: 0x00067564 File Offset: 0x00065764
		public override CustomPropertyCollection CustomProperties
		{
			get
			{
				if (this.m_customPropertyCollection == null)
				{
					string text = ((this.m_memberDef.Grouping != null) ? this.m_memberDef.Grouping.Name : base.OwnerCri.Name);
					this.m_customPropertyCollection = new CustomPropertyCollection(this.ReportScope.ReportScopeInstance, base.OwnerCri.RenderingContext, null, this.m_memberDef, ObjectType.CustomReportItem, text);
					this.m_customPropertyCollectionReady = true;
				}
				else if (!this.m_customPropertyCollectionReady)
				{
					string text2 = ((this.m_memberDef.Grouping != null) ? this.m_memberDef.Grouping.Name : base.OwnerCri.Name);
					this.m_customPropertyCollection.UpdateCustomProperties(this.ReportScope.ReportScopeInstance, this.m_memberDef, base.OwnerCri.RenderingContext.OdpContext, ObjectType.CustomReportItem, text2);
					this.m_customPropertyCollectionReady = true;
				}
				return this.m_customPropertyCollection;
			}
		}

		// Token: 0x17000E86 RID: 3718
		// (get) Token: 0x0600195F RID: 6495 RVA: 0x00067647 File Offset: 0x00065847
		public override string ID
		{
			get
			{
				return this.m_memberDef.RenderingModelID;
			}
		}

		// Token: 0x17000E87 RID: 3719
		// (get) Token: 0x06001960 RID: 6496 RVA: 0x00067654 File Offset: 0x00065854
		public override bool IsColumn
		{
			get
			{
				return this.m_memberDef.IsColumn;
			}
		}

		// Token: 0x17000E88 RID: 3720
		// (get) Token: 0x06001961 RID: 6497 RVA: 0x00067661 File Offset: 0x00065861
		public override bool IsStatic
		{
			get
			{
				return this.m_memberDef.Grouping == null;
			}
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x00067673 File Offset: 0x00065873
		public override int MemberCellIndex
		{
			get
			{
				return this.m_memberDef.MemberCellIndex;
			}
		}

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x00067680 File Offset: 0x00065880
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

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x00067692 File Offset: 0x00065892
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

		// Token: 0x17000E8C RID: 3724
		// (get) Token: 0x06001965 RID: 6501 RVA: 0x000676AE File Offset: 0x000658AE
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

		// Token: 0x17000E8D RID: 3725
		// (get) Token: 0x06001966 RID: 6502 RVA: 0x000676CF File Offset: 0x000658CF
		public override int RowSpan
		{
			get
			{
				return this.m_memberDef.RowSpan;
			}
		}

		// Token: 0x17000E8E RID: 3726
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x000676DC File Offset: 0x000658DC
		public override DataMemberInstance Instance
		{
			get
			{
				if (base.OwnerCri.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					if (this.IsStatic)
					{
						this.m_instance = new DataMemberInstance(base.OwnerCri, this);
					}
					else
					{
						DataDynamicMemberInstance dataDynamicMemberInstance = new DataDynamicMemberInstance(base.OwnerCri, this, this.BuildOdpMemberLogic(base.OwnerCri.RenderingContext.OdpContext));
						this.m_owner.RenderingContext.AddDynamicInstance(dataDynamicMemberInstance);
						this.m_instance = dataDynamicMemberInstance;
					}
				}
				return this.m_instance;
			}
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x06001968 RID: 6504 RVA: 0x00067762 File Offset: 0x00065962
		internal override DataMember MemberDefinition
		{
			get
			{
				return this.m_memberDef;
			}
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x0006776A File Offset: 0x0006596A
		internal override void SetNewContext(bool fromMoveNext)
		{
			if (!fromMoveNext && this.m_instance != null && !this.IsStatic)
			{
				((IDynamicInstance)this.m_instance).ResetContext();
			}
			base.SetNewContext(fromMoveNext);
			this.m_customPropertyCollectionReady = false;
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x000677A0 File Offset: 0x000659A0
		public override DataMemberCollection Children
		{
			get
			{
				DataMemberList subMembers = this.m_memberDef.SubMembers;
				if (subMembers == null)
				{
					return null;
				}
				if (this.m_children == null)
				{
					this.m_children = new InternalDataMemberCollection(this, base.OwnerCri, this, subMembers);
				}
				return this.m_children;
			}
		}

		// Token: 0x04000CB2 RID: 3250
		private DataMember m_memberDef;

		// Token: 0x04000CB3 RID: 3251
		private IReportScope m_reportScope;

		// Token: 0x04000CB4 RID: 3252
		private string m_uniqueName;

		// Token: 0x04000CB5 RID: 3253
		private bool m_customPropertyCollectionReady;
	}
}

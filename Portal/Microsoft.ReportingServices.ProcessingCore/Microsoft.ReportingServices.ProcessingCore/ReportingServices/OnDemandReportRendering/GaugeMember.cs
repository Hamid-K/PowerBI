using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200010A RID: 266
	public sealed class GaugeMember : DataRegionMember
	{
		// Token: 0x06000BC8 RID: 3016 RVA: 0x0003428E File Offset: 0x0003248E
		internal GaugeMember(IReportScope reportScope, IDefinitionPath parentDefinitionPath, GaugePanel owner, GaugeMember parent, GaugeMember memberDef)
			: base(parentDefinitionPath, owner, parent, 0)
		{
			this.m_memberDef = memberDef;
			if (this.m_memberDef.IsStatic)
			{
				this.m_reportScope = reportScope;
			}
			this.m_group = new Group(owner, this.m_memberDef, this);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x000342CA File Offset: 0x000324CA
		internal GaugeMember(IDefinitionPath parentDefinitionPath, GaugePanel owner, GaugeMember parent)
			: base(parentDefinitionPath, owner, parent, 0)
		{
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06000BCA RID: 3018 RVA: 0x000342D6 File Offset: 0x000324D6
		public GaugeMember Parent
		{
			get
			{
				return this.m_parent as GaugeMember;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x000342E3 File Offset: 0x000324E3
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

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x00034304 File Offset: 0x00032504
		public override string ID
		{
			get
			{
				return this.m_memberDef.RenderingModelID;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00034311 File Offset: 0x00032511
		public override bool IsStatic
		{
			get
			{
				return this.m_memberDef.Grouping == null;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x00034323 File Offset: 0x00032523
		public bool IsColumn
		{
			get
			{
				return this.m_memberDef.IsColumn;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x00034330 File Offset: 0x00032530
		public int RowSpan
		{
			get
			{
				return this.m_memberDef.RowSpan;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06000BD0 RID: 3024 RVA: 0x0003433D File Offset: 0x0003253D
		public int ColumnSpan
		{
			get
			{
				return this.m_memberDef.ColSpan;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06000BD1 RID: 3025 RVA: 0x0003434A File Offset: 0x0003254A
		public override int MemberCellIndex
		{
			get
			{
				return this.m_memberDef.MemberCellIndex;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x00034357 File Offset: 0x00032557
		internal GaugeMember MemberDefinition
		{
			get
			{
				return this.m_memberDef;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0003435F File Offset: 0x0003255F
		internal override ReportHierarchyNode DataRegionMemberDefinition
		{
			get
			{
				return this.MemberDefinition;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x00034367 File Offset: 0x00032567
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

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06000BD5 RID: 3029 RVA: 0x00034379 File Offset: 0x00032579
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

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00034395 File Offset: 0x00032595
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

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x000343B6 File Offset: 0x000325B6
		internal GaugePanel OwnerGaugePanel
		{
			get
			{
				return this.m_owner as GaugePanel;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06000BD8 RID: 3032 RVA: 0x000343C4 File Offset: 0x000325C4
		public GaugeMemberInstance Instance
		{
			get
			{
				if (this.OwnerGaugePanel.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					if (this.IsStatic)
					{
						this.m_instance = new GaugeMemberInstance(this.OwnerGaugePanel, this);
					}
					else
					{
						GaugeDynamicMemberInstance gaugeDynamicMemberInstance = new GaugeDynamicMemberInstance(this.OwnerGaugePanel, this, this.BuildOdpMemberLogic(this.OwnerGaugePanel.RenderingContext.OdpContext));
						this.m_owner.RenderingContext.AddDynamicInstance(gaugeDynamicMemberInstance);
						this.m_instance = gaugeDynamicMemberInstance;
					}
				}
				return this.m_instance;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0003444A File Offset: 0x0003264A
		public GaugeMember ChildGaugeMember
		{
			get
			{
				if (this.m_children != null && this.m_children.Count == 1)
				{
					return this.m_children[0];
				}
				return null;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x00034470 File Offset: 0x00032670
		internal override IDataRegionMemberCollection SubMembers
		{
			get
			{
				if (this.m_children == null && this.m_memberDef.InnerHierarchy != null)
				{
					GaugeMemberList gaugeMemberList = (GaugeMemberList)this.m_memberDef.InnerHierarchy;
					if (gaugeMemberList == null)
					{
						return null;
					}
					this.m_children = new GaugeMemberCollection(this, this.OwnerGaugePanel, this, gaugeMemberList);
				}
				return this.m_children;
			}
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x000344C2 File Offset: 0x000326C2
		internal override bool GetIsColumn()
		{
			return this.IsColumn;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x000344CA File Offset: 0x000326CA
		internal override void SetNewContext(bool fromMoveNext)
		{
			if (!fromMoveNext && this.m_instance != null && !this.IsStatic)
			{
				((IDynamicInstance)this.m_instance).ResetContext();
			}
			base.SetNewContext(fromMoveNext);
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000519 RID: 1305
		private GaugeMemberCollection m_children;

		// Token: 0x0400051A RID: 1306
		private GaugeMemberInstance m_instance;

		// Token: 0x0400051B RID: 1307
		private GaugeMember m_memberDef;

		// Token: 0x0400051C RID: 1308
		private IReportScope m_reportScope;

		// Token: 0x0400051D RID: 1309
		private string m_uniqueName;
	}
}

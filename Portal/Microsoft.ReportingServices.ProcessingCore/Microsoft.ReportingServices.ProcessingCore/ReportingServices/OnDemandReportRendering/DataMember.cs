using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200028D RID: 653
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class DataMember : DataRegionMember
	{
		// Token: 0x06001937 RID: 6455 RVA: 0x0006707D File Offset: 0x0006527D
		internal DataMember(IDefinitionPath parentDefinitionPath, CustomReportItem owner, DataMember parent, int parentCollectionIndex)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex)
		{
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06001938 RID: 6456 RVA: 0x0006708A File Offset: 0x0006528A
		public DataMember Parent
		{
			get
			{
				return this.m_parent as DataMember;
			}
		}

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06001939 RID: 6457 RVA: 0x00067097 File Offset: 0x00065297
		public virtual DataMemberCollection Children
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x0600193A RID: 6458
		public abstract bool IsColumn { get; }

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x0600193B RID: 6459
		public abstract int RowSpan { get; }

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x0600193C RID: 6460
		public abstract int ColSpan { get; }

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x0600193D RID: 6461
		internal abstract DataMember MemberDefinition { get; }

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x0600193E RID: 6462 RVA: 0x0006709F File Offset: 0x0006529F
		internal override ReportHierarchyNode DataRegionMemberDefinition
		{
			get
			{
				return this.MemberDefinition;
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x000670A7 File Offset: 0x000652A7
		internal CustomReportItem OwnerCri
		{
			get
			{
				return this.m_owner as CustomReportItem;
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06001940 RID: 6464
		public abstract DataMemberInstance Instance { get; }

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x000670B4 File Offset: 0x000652B4
		internal override IDataRegionMemberCollection SubMembers
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x06001942 RID: 6466 RVA: 0x000670BC File Offset: 0x000652BC
		internal override bool GetIsColumn()
		{
			return this.IsColumn;
		}

		// Token: 0x06001943 RID: 6467 RVA: 0x000670C4 File Offset: 0x000652C4
		internal override void SetNewContext(bool fromMoveNext)
		{
			base.SetNewContext(fromMoveNext);
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000CAA RID: 3242
		protected DataMemberCollection m_children;

		// Token: 0x04000CAB RID: 3243
		protected DataMemberInstance m_instance;
	}
}

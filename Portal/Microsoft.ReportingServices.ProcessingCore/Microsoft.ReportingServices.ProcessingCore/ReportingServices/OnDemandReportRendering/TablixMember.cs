using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000371 RID: 881
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class TablixMember : DataRegionMember
	{
		// Token: 0x06002188 RID: 8584 RVA: 0x00081DD2 File Offset: 0x0007FFD2
		internal TablixMember(IDefinitionPath parentDefinitionPath, Tablix owner, TablixMember parent, int parentCollectionIndex)
			: base(parentDefinitionPath, owner, parent, parentCollectionIndex)
		{
		}

		// Token: 0x170012DE RID: 4830
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x00081DDF File Offset: 0x0007FFDF
		public TablixMember Parent
		{
			get
			{
				return this.m_parent as TablixMember;
			}
		}

		// Token: 0x170012DF RID: 4831
		// (get) Token: 0x0600218A RID: 8586
		public abstract string DataElementName { get; }

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x0600218B RID: 8587
		public abstract DataElementOutputTypes DataElementOutput { get; }

		// Token: 0x170012E1 RID: 4833
		// (get) Token: 0x0600218C RID: 8588
		public abstract TablixHeader TablixHeader { get; }

		// Token: 0x170012E2 RID: 4834
		// (get) Token: 0x0600218D RID: 8589
		public abstract TablixMemberCollection Children { get; }

		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x0600218E RID: 8590
		public abstract bool FixedData { get; }

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x0600218F RID: 8591
		public abstract KeepWithGroup KeepWithGroup { get; }

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x06002190 RID: 8592
		public abstract bool RepeatOnNewPage { get; }

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x06002191 RID: 8593 RVA: 0x00081DEC File Offset: 0x0007FFEC
		public virtual bool KeepTogether
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x06002192 RID: 8594
		public abstract bool IsColumn { get; }

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06002193 RID: 8595
		internal abstract int RowSpan { get; }

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06002194 RID: 8596
		internal abstract int ColSpan { get; }

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06002195 RID: 8597
		public abstract bool IsTotal { get; }

		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06002196 RID: 8598
		internal abstract PageBreakLocation PropagatedGroupBreak { get; }

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06002197 RID: 8599
		public abstract Visibility Visibility { get; }

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x06002198 RID: 8600
		public abstract bool HideIfNoRows { get; }

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x06002199 RID: 8601
		internal abstract TablixMember MemberDefinition { get; }

		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x00081DEF File Offset: 0x0007FFEF
		internal override ReportHierarchyNode DataRegionMemberDefinition
		{
			get
			{
				return this.MemberDefinition;
			}
		}

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x00081DF7 File Offset: 0x0007FFF7
		internal Tablix OwnerTablix
		{
			get
			{
				return this.m_owner as Tablix;
			}
		}

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x0600219C RID: 8604
		public abstract TablixMemberInstance Instance { get; }

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x0600219D RID: 8605 RVA: 0x00081E04 File Offset: 0x00080004
		internal override IDataRegionMemberCollection SubMembers
		{
			get
			{
				return this.m_children;
			}
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x00081E0C File Offset: 0x0008000C
		internal override bool GetIsColumn()
		{
			return this.IsColumn;
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x00081E14 File Offset: 0x00080014
		internal override void SetNewContext(bool fromMoveNext)
		{
			base.SetNewContext(fromMoveNext);
			if (this.m_header != null)
			{
				this.m_header.SetNewContext();
			}
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			TablixMember memberDefinition = this.MemberDefinition;
			if (memberDefinition != null)
			{
				memberDefinition.ResetVisibilityComputationCache();
			}
		}

		// Token: 0x040010CF RID: 4303
		protected TablixMemberCollection m_children;

		// Token: 0x040010D0 RID: 4304
		protected Visibility m_visibility;

		// Token: 0x040010D1 RID: 4305
		protected TablixMemberInstance m_instance;

		// Token: 0x040010D2 RID: 4306
		protected TablixHeader m_header;
	}
}

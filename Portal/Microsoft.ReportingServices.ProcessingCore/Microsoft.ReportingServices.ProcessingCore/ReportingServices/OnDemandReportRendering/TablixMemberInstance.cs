using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000367 RID: 871
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class TablixMemberInstance : BaseInstance
	{
		// Token: 0x06002141 RID: 8513 RVA: 0x00080C4D File Offset: 0x0007EE4D
		internal TablixMemberInstance(Tablix owner, TablixMember memberDef)
			: base(memberDef.ReportScope)
		{
			this.m_owner = owner;
			this.m_memberDef = memberDef;
		}

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x00080C6C File Offset: 0x0007EE6C
		public virtual VisibilityInstance Visibility
		{
			get
			{
				if (this.m_visibility == null && this.m_memberDef.Visibility != null)
				{
					if (this.m_owner.IsOldSnapshot)
					{
						this.m_visibility = new ShimMemberVisibilityInstance((ShimMemberVisibility)this.m_memberDef.Visibility);
					}
					else
					{
						this.m_visibility = new InternalTablixMemberVisibilityInstance((InternalTablixMember)this.m_memberDef);
					}
				}
				return this.m_visibility;
			}
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x00080CD4 File Offset: 0x0007EED4
		protected override void ResetInstanceCache()
		{
			if (this.m_visibility != null)
			{
				this.m_visibility.SetNewContext();
			}
		}

		// Token: 0x040010BA RID: 4282
		protected Tablix m_owner;

		// Token: 0x040010BB RID: 4283
		protected TablixMember m_memberDef;

		// Token: 0x040010BC RID: 4284
		protected VisibilityInstance m_visibility;
	}
}

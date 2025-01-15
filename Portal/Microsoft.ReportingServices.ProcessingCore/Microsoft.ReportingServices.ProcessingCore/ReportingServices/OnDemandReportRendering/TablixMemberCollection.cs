using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200036B RID: 875
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class TablixMemberCollection : DataRegionMemberCollection<TablixMember>
	{
		// Token: 0x06002164 RID: 8548 RVA: 0x0008125F File Offset: 0x0007F45F
		internal TablixMemberCollection(IDefinitionPath parentDefinitionPath, Tablix owner)
			: base(parentDefinitionPath, owner)
		{
		}

		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06002165 RID: 8549 RVA: 0x00081269 File Offset: 0x0007F469
		public override string DefinitionPath
		{
			get
			{
				if (this.m_parentDefinitionPath is TablixMember)
				{
					return this.m_parentDefinitionPath.DefinitionPath + "xM";
				}
				return this.m_parentDefinitionPath.DefinitionPath;
			}
		}

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06002166 RID: 8550 RVA: 0x00081299 File Offset: 0x0007F499
		internal Tablix OwnerTablix
		{
			get
			{
				return this.m_owner as Tablix;
			}
		}

		// Token: 0x170012D1 RID: 4817
		// (get) Token: 0x06002167 RID: 8551 RVA: 0x000812A6 File Offset: 0x0007F4A6
		internal virtual double SizeDelta
		{
			get
			{
				return 0.0;
			}
		}
	}
}

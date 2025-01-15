using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000273 RID: 627
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ChartMemberCollection : DataRegionMemberCollection<ChartMember>
	{
		// Token: 0x06001862 RID: 6242 RVA: 0x00064ABD File Offset: 0x00062CBD
		internal ChartMemberCollection(IDefinitionPath parentDefinitionPath, Chart owner)
			: base(parentDefinitionPath, owner)
		{
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06001863 RID: 6243 RVA: 0x00064AC7 File Offset: 0x00062CC7
		public override string DefinitionPath
		{
			get
			{
				if (this.m_parentDefinitionPath is ChartMember)
				{
					return this.m_parentDefinitionPath.DefinitionPath + "xM";
				}
				return this.m_parentDefinitionPath.DefinitionPath;
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06001864 RID: 6244 RVA: 0x00064AF7 File Offset: 0x00062CF7
		internal Chart OwnerChart
		{
			get
			{
				return this.m_owner as Chart;
			}
		}
	}
}

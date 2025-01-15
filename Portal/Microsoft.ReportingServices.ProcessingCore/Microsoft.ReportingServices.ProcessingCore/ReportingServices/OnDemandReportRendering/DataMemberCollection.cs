using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200028A RID: 650
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class DataMemberCollection : DataRegionMemberCollection<DataMember>
	{
		// Token: 0x0600192C RID: 6444 RVA: 0x00066D6E File Offset: 0x00064F6E
		internal DataMemberCollection(IDefinitionPath parentDefinitionPath, CustomReportItem owner)
			: base(parentDefinitionPath, owner)
		{
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x0600192D RID: 6445 RVA: 0x00066D78 File Offset: 0x00064F78
		public override string DefinitionPath
		{
			get
			{
				if (this.m_parentDefinitionPath is DataMember)
				{
					return this.m_parentDefinitionPath.DefinitionPath + "xM";
				}
				return this.m_parentDefinitionPath.DefinitionPath;
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x0600192E RID: 6446 RVA: 0x00066DA8 File Offset: 0x00064FA8
		internal CustomReportItem OwnerCri
		{
			get
			{
				return this.m_owner as CustomReportItem;
			}
		}
	}
}

using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.Interfaces
{
	// Token: 0x0200002F RID: 47
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public class Extension
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00002076 File Offset: 0x00000276
		public Extension(string name, string localizedName, bool visible)
		{
			this.m_name = name;
			this.m_localizedName = localizedName;
			this.m_visible = visible;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002093 File Offset: 0x00000293
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000209B File Offset: 0x0000029B
		public string LocalizedName
		{
			get
			{
				return this.m_localizedName;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000020A3 File Offset: 0x000002A3
		public bool Visible
		{
			get
			{
				return this.m_visible;
			}
		}

		// Token: 0x04000178 RID: 376
		private string m_name;

		// Token: 0x04000179 RID: 377
		private string m_localizedName;

		// Token: 0x0400017A RID: 378
		private bool m_visible;
	}
}

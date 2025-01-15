using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000AE RID: 174
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct COAUTHINFO
	{
		// Token: 0x04000338 RID: 824
		public uint AuthnSvc;

		// Token: 0x04000339 RID: 825
		public uint AuthzSvc;

		// Token: 0x0400033A RID: 826
		public unsafe char* ServerPrincName;

		// Token: 0x0400033B RID: 827
		public uint AuthnLevel;

		// Token: 0x0400033C RID: 828
		public uint ImpersonationLevel;

		// Token: 0x0400033D RID: 829
		public unsafe COAUTHIDENTITY* AuthIdentityData;

		// Token: 0x0400033E RID: 830
		public uint Capabilities;
	}
}

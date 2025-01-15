using System;
using System.Security.Permissions;

namespace RSManagedCrypto
{
	// Token: 0x0200001F RID: 31
	[StrongNameIdentityPermission(SecurityAction.LinkDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public sealed class StoreHelper
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00007D50 File Offset: 0x00007150
		public static string GetKeyPath(Guid id)
		{
			return "Software\\Microsoft\\Microsoft SQL Server\\Reporting Services\\Service Applications\\" + id + "\\KeyTransform";
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00007D80 File Offset: 0x00007180
		public static string GetSecondEntropy()
		{
			return StoreHelper.SecondEntropy;
		}

		// Token: 0x04000059 RID: 89
		private static string SecondEntropy = "This-string-should-meet-any-password-standard-5-17-2010";
	}
}

using System;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200014B RID: 331
	public static class PublicClientApplicationExtensions
	{
		// Token: 0x0600109E RID: 4254 RVA: 0x0003B780 File Offset: 0x00039980
		public static bool IsProofOfPossessionSupportedByClient(this IPublicClientApplication app)
		{
			PublicClientApplication publicClientApplication = app as PublicClientApplication;
			return publicClientApplication != null && publicClientApplication.IsProofOfPossessionSupportedByClient();
		}
	}
}

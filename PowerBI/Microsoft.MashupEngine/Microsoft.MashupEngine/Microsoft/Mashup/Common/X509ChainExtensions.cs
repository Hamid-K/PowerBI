using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C39 RID: 7225
	public static class X509ChainExtensions
	{
		// Token: 0x0600B46E RID: 46190 RVA: 0x00249798 File Offset: 0x00247998
		public static void EnableRevocationChecks(this X509Chain chain, bool rejectOnUnknownRevocation)
		{
			chain.ChainPolicy.RevocationFlag = X509RevocationFlag.ExcludeRoot;
			chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;
			chain.ChainPolicy.UrlRetrievalTimeout = new TimeSpan(0, 0, 10);
			chain.ChainPolicy.VerificationTime = DateTime.Now;
			if (!rejectOnUnknownRevocation)
			{
				chain.ChainPolicy.VerificationFlags = X509VerificationFlags.IgnoreEndRevocationUnknown | X509VerificationFlags.IgnoreCtlSignerRevocationUnknown | X509VerificationFlags.IgnoreCertificateAuthorityRevocationUnknown;
			}
		}
	}
}

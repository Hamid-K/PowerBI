using System;
using System.Security.Cryptography.X509Certificates;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000175 RID: 373
	public static class OsCapabilitiesExtensions
	{
		// Token: 0x06001255 RID: 4693 RVA: 0x0003E9D0 File Offset: 0x0003CBD0
		public static bool IsSystemWebViewAvailable(this IPublicClientApplication publicClientApplication)
		{
			PublicClientApplication publicClientApplication2 = publicClientApplication as PublicClientApplication;
			if (publicClientApplication2 != null)
			{
				return publicClientApplication2.IsSystemWebViewAvailable;
			}
			throw new ArgumentException("This extension method is only available for the PublicClientApplication implementation of the IPublicClientApplication interface.");
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x0003E9F8 File Offset: 0x0003CBF8
		public static bool IsEmbeddedWebViewAvailable(this IPublicClientApplication publicClientApplication)
		{
			PublicClientApplication publicClientApplication2 = publicClientApplication as PublicClientApplication;
			if (publicClientApplication2 != null)
			{
				return publicClientApplication2.IsEmbeddedWebViewAvailable();
			}
			throw new ArgumentException("This extension method is only available for the PublicClientApplication implementation of the IPublicClientApplication interface.");
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0003EA20 File Offset: 0x0003CC20
		public static bool IsUserInteractive(this IPublicClientApplication publicClientApplication)
		{
			PublicClientApplication publicClientApplication2 = publicClientApplication as PublicClientApplication;
			if (publicClientApplication2 != null)
			{
				return publicClientApplication2.IsUserInteractive();
			}
			throw new ArgumentException("This extension method is only available for the PublicClientApplication implementation of the IPublicClientApplication interface.");
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0003EA48 File Offset: 0x0003CC48
		public static X509Certificate2 GetCertificate(this IConfidentialClientApplication confidentialClientApplication)
		{
			ConfidentialClientApplication confidentialClientApplication2 = confidentialClientApplication as ConfidentialClientApplication;
			if (confidentialClientApplication2 != null)
			{
				return confidentialClientApplication2.Certificate;
			}
			throw new ArgumentException("This extension method is only available for the ConfidentialClientApplication implementation of the IConfidentialClientApplication interface.");
		}
	}
}

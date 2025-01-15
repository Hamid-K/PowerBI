using System;
using System.Net;

namespace Microsoft.AnalysisServices.AzureClient
{
	// Token: 0x02000007 RID: 7
	[Obsolete("Deprecated!")]
	public static class SecurityProtocolTypeExtensions
	{
		// Token: 0x0600000A RID: 10 RVA: 0x000021CD File Offset: 0x000003CD
		public static void EnableStrongSecurityProtocol()
		{
			throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
		}

		// Token: 0x04000018 RID: 24
		public const SecurityProtocolType Tls12 = SecurityProtocolType.Tls12;

		// Token: 0x04000019 RID: 25
		public const SecurityProtocolType Tls11 = SecurityProtocolType.Tls11;
	}
}

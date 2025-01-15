using System;
using System.Net;

namespace Microsoft.AnalysisServices.Authentication
{
	// Token: 0x02000103 RID: 259
	[Obsolete("Deprecated!")]
	public static class SecurityProtocolTypeExtensions
	{
		// Token: 0x06000FD9 RID: 4057 RVA: 0x00037385 File Offset: 0x00035585
		public static void EnableStrongSecurityProtocol()
		{
			throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
		}

		// Token: 0x040008B4 RID: 2228
		public const SecurityProtocolType Tls12 = SecurityProtocolType.Tls12;

		// Token: 0x040008B5 RID: 2229
		public const SecurityProtocolType Tls11 = SecurityProtocolType.Tls11;
	}
}

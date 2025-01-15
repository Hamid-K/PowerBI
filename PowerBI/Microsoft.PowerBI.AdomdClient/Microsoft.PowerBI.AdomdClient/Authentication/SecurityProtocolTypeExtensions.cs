using System;
using System.Net;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x0200010E RID: 270
	[Obsolete("Deprecated!")]
	public static class SecurityProtocolTypeExtensions
	{
		// Token: 0x06000F3E RID: 3902 RVA: 0x00034751 File Offset: 0x00032951
		public static void EnableStrongSecurityProtocol()
		{
			throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
		}

		// Token: 0x040008EE RID: 2286
		public const SecurityProtocolType Tls12 = SecurityProtocolType.Tls12;

		// Token: 0x040008EF RID: 2287
		public const SecurityProtocolType Tls11 = SecurityProtocolType.Tls11;
	}
}

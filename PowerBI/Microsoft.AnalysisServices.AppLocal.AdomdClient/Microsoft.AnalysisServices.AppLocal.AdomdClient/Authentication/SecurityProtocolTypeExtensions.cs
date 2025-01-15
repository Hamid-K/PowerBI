using System;
using System.Net;

namespace Microsoft.AnalysisServices.AdomdClient.Authentication
{
	// Token: 0x0200010E RID: 270
	[Obsolete("Deprecated!")]
	public static class SecurityProtocolTypeExtensions
	{
		// Token: 0x06000F4B RID: 3915 RVA: 0x00034A81 File Offset: 0x00032C81
		public static void EnableStrongSecurityProtocol()
		{
			throw new NotSupportedException(RuntimeSR.Exception_DeprecatedAndNotSupportedFeature);
		}

		// Token: 0x040008FB RID: 2299
		public const SecurityProtocolType Tls12 = SecurityProtocolType.Tls12;

		// Token: 0x040008FC RID: 2300
		public const SecurityProtocolType Tls11 = SecurityProtocolType.Tls11;
	}
}

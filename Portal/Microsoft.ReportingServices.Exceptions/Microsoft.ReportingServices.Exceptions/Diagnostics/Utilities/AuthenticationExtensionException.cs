using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000A7 RID: 167
	[Serializable]
	internal sealed class AuthenticationExtensionException : RSException
	{
		// Token: 0x06000292 RID: 658 RVA: 0x00005378 File Offset: 0x00003578
		public AuthenticationExtensionException(Exception innerException, string parameterName)
			: base(ErrorCode.rsAuthorizationTokenInvalidOrExpired, ErrorStringsWrapper.rsAuthenticationExtensionError(parameterName), innerException, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000053A1 File Offset: 0x000035A1
		private AuthenticationExtensionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

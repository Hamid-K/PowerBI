using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B2 RID: 178
	[Serializable]
	internal sealed class AuthorizationExtensionException : RSException
	{
		// Token: 0x060002AD RID: 685 RVA: 0x00005675 File Offset: 0x00003875
		public AuthorizationExtensionException(Exception innerException)
			: this(innerException, null)
		{
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000567F File Offset: 0x0000387F
		public AuthorizationExtensionException(Exception innerException, string additionalTraceMessage)
			: base(ErrorCode.rsAuthorizationExtensionError, ErrorStringsWrapper.rsAuthorizationExtensionError, innerException, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, additionalTraceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000056A7 File Offset: 0x000038A7
		private AuthorizationExtensionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

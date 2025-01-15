using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000C3 RID: 195
	internal sealed class SecureStoreUnsupportedSharePointVersionException : ReportCatalogException
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x000059CF File Offset: 0x00003BCF
		public SecureStoreUnsupportedSharePointVersionException()
			: base(ErrorCode.rsSecureStoreUnsupportedSharePointVersion, ErrorStringsWrapper.rsSecureStoreUnsupportedSharePointVersion, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000059E8 File Offset: 0x00003BE8
		private SecureStoreUnsupportedSharePointVersionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

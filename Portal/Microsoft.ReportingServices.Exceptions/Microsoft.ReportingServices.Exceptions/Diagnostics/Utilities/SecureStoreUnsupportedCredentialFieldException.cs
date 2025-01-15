using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000C2 RID: 194
	internal sealed class SecureStoreUnsupportedCredentialFieldException : ReportCatalogException
	{
		// Token: 0x060002CE RID: 718 RVA: 0x000059AB File Offset: 0x00003BAB
		public SecureStoreUnsupportedCredentialFieldException(string appId)
			: base(ErrorCode.rsSecureStoreUnsupportedCredentialField, ErrorStringsWrapper.rsSecureStoreUnsupportedCredentialField(appId), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000059C5 File Offset: 0x00003BC5
		private SecureStoreUnsupportedCredentialFieldException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

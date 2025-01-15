using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000C1 RID: 193
	internal sealed class SecureStoreAmbiguousCredentialFieldsException : ReportCatalogException
	{
		// Token: 0x060002CC RID: 716 RVA: 0x00005987 File Offset: 0x00003B87
		public SecureStoreAmbiguousCredentialFieldsException(string appId)
			: base(ErrorCode.rsSecureStoreAmbiguousCredentialFields, ErrorStringsWrapper.rsSecureStoreAmbiguousCredentialFields(appId), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000059A1 File Offset: 0x00003BA1
		private SecureStoreAmbiguousCredentialFieldsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

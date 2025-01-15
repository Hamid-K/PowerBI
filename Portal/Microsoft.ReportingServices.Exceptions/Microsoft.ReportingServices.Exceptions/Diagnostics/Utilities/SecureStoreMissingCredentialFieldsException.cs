using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000C0 RID: 192
	internal sealed class SecureStoreMissingCredentialFieldsException : ReportCatalogException
	{
		// Token: 0x060002CA RID: 714 RVA: 0x00005963 File Offset: 0x00003B63
		public SecureStoreMissingCredentialFieldsException(string appId)
			: base(ErrorCode.rsSecureStoreMissingCredentialFields, ErrorStringsWrapper.rsSecureStoreMissingCredentialFields(appId), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000597D File Offset: 0x00003B7D
		private SecureStoreMissingCredentialFieldsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

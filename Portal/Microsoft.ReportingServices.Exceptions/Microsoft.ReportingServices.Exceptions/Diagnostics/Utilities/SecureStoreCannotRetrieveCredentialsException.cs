using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000BF RID: 191
	internal sealed class SecureStoreCannotRetrieveCredentialsException : ReportCatalogException
	{
		// Token: 0x060002C8 RID: 712 RVA: 0x0000593A File Offset: 0x00003B3A
		public SecureStoreCannotRetrieveCredentialsException(Exception innerException)
			: base(ErrorCode.rsSecureStoreCannotRetrieveCredentials, ErrorStringsWrapper.rsSecureStoreCannotRetrieveCredentials(innerException.Message), innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00005959 File Offset: 0x00003B59
		private SecureStoreCannotRetrieveCredentialsException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

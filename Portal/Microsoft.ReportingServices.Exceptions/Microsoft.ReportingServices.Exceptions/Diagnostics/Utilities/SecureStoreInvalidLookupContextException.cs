using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000BD RID: 189
	internal sealed class SecureStoreInvalidLookupContextException : ReportCatalogException
	{
		// Token: 0x060002C4 RID: 708 RVA: 0x000058FC File Offset: 0x00003AFC
		public SecureStoreInvalidLookupContextException()
			: base(ErrorCode.rsSecureStoreInvalidLookupContext, ErrorStringsWrapper.rsSecureStoreInvalidLookupContext, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00005915 File Offset: 0x00003B15
		private SecureStoreInvalidLookupContextException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

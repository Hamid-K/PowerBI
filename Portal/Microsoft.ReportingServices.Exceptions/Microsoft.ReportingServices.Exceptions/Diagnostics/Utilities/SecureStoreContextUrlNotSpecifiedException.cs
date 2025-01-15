using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000BC RID: 188
	internal sealed class SecureStoreContextUrlNotSpecifiedException : ReportCatalogException
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x000058D9 File Offset: 0x00003AD9
		public SecureStoreContextUrlNotSpecifiedException()
			: base(ErrorCode.rsSecureStoreContextUrlNotSpecified, ErrorStringsWrapper.rsSecureStoreContextUrlNotSpecified, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000058F2 File Offset: 0x00003AF2
		private SecureStoreContextUrlNotSpecifiedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

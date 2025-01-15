using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000092 RID: 146
	[Serializable]
	internal sealed class AccessDeniedToSecureDataException : ReportCatalogException
	{
		// Token: 0x06000261 RID: 609 RVA: 0x00004F81 File Offset: 0x00003181
		public AccessDeniedToSecureDataException()
			: base(ErrorCode.rsAccessDeniedToSecureData, ErrorStringsWrapper.rsAccessDeniedToSecureData, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00004F97 File Offset: 0x00003197
		private AccessDeniedToSecureDataException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

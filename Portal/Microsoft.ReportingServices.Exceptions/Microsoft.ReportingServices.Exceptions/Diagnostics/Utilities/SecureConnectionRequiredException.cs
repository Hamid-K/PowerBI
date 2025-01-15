using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000078 RID: 120
	[Serializable]
	internal sealed class SecureConnectionRequiredException : ReportCatalogException
	{
		// Token: 0x06000227 RID: 551 RVA: 0x00004B1C File Offset: 0x00002D1C
		public SecureConnectionRequiredException()
			: base(ErrorCode.rsSecureConnectionRequired, ErrorStringsWrapper.rsSecureConnectionRequired, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00004B32 File Offset: 0x00002D32
		private SecureConnectionRequiredException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	internal sealed class AccessDeniedException : ReportCatalogException
	{
		// Token: 0x0600016C RID: 364 RVA: 0x00003EF6 File Offset: 0x000020F6
		public AccessDeniedException(string userName, ErrorCode errorCode = ErrorCode.rsAccessDenied)
			: base(errorCode, ErrorStringsWrapper.rsAccessDenied(userName), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00003F0C File Offset: 0x0000210C
		private AccessDeniedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200005D RID: 93
	[Serializable]
	internal sealed class GetExternalImagesFailureException : ReportCatalogException
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x00004748 File Offset: 0x00002948
		public GetExternalImagesFailureException(string message, ErrorCode errorCode)
			: this(message, errorCode, null)
		{
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00004753 File Offset: 0x00002953
		public GetExternalImagesFailureException(string message, ErrorCode errorCode, Exception innerException)
			: base(errorCode, message, innerException, null, Array.Empty<object>())
		{
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00004764 File Offset: 0x00002964
		private GetExternalImagesFailureException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	internal sealed class NotYetSupportedException : ReportCatalogException
	{
		// Token: 0x06000139 RID: 313 RVA: 0x00003B7B File Offset: 0x00001D7B
		public NotYetSupportedException()
			: base(ErrorCode.rsNotSupported, ErrorStringsWrapper.rsNotSupported, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00003B91 File Offset: 0x00001D91
		private NotYetSupportedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

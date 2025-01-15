using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000074 RID: 116
	[Serializable]
	internal sealed class ModelIDMismatchException : ReportCatalogException
	{
		// Token: 0x0600021F RID: 543 RVA: 0x00004A9C File Offset: 0x00002C9C
		public ModelIDMismatchException()
			: base(ErrorCode.rsModelIDMismatch, ErrorStringsWrapper.rsModelIDMismatch, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00004AB2 File Offset: 0x00002CB2
		private ModelIDMismatchException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200002A RID: 42
	[Serializable]
	internal sealed class StreamNotFoundException : ReportCatalogException
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00004020 File Offset: 0x00002220
		public StreamNotFoundException(string streamId)
			: base(ErrorCode.rsStreamNotFound, ErrorStringsWrapper.rsStreamNotFound, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00004036 File Offset: 0x00002236
		private StreamNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

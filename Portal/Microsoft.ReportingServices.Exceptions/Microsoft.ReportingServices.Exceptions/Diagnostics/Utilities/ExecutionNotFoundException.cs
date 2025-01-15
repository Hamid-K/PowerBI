using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x0200002C RID: 44
	[Serializable]
	internal sealed class ExecutionNotFoundException : ReportCatalogException
	{
		// Token: 0x06000182 RID: 386 RVA: 0x00004060 File Offset: 0x00002260
		public ExecutionNotFoundException(string ExecutionID)
			: base(ErrorCode.rsExecutionNotFound, ErrorStringsWrapper.rsExecutionNotFound(ExecutionID), null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00004077 File Offset: 0x00002277
		private ExecutionNotFoundException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

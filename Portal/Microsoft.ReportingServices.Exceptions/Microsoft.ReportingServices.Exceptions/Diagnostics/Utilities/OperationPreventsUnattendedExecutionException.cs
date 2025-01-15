using System;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	internal sealed class OperationPreventsUnattendedExecutionException : ReportCatalogException
	{
		// Token: 0x06000192 RID: 402 RVA: 0x00004165 File Offset: 0x00002365
		public OperationPreventsUnattendedExecutionException()
			: base(ErrorCode.rsOperationPreventsUnattendedExecution, ErrorStringsWrapper.rsOperationPreventsUnattendedExecution, null, null, Array.Empty<object>())
		{
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000417B File Offset: 0x0000237B
		private OperationPreventsUnattendedExecutionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

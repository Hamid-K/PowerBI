using System;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B8 RID: 184
	internal sealed class ComponentPublishingError : RSException
	{
		// Token: 0x060002BB RID: 699 RVA: 0x0000581B File Offset: 0x00003A1B
		public ComponentPublishingError(Exception innerException)
			: base(ErrorCode.rsComponentPublishingError, ErrorStringsWrapper.rsComponentPublishingError, innerException, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null, Array.Empty<object>())
		{
		}
	}
}

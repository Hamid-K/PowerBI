using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000172 RID: 370
	internal sealed class PowerPointExportException : RSException
	{
		// Token: 0x06000DA0 RID: 3488 RVA: 0x00031A24 File Offset: 0x0002FC24
		internal PowerPointExportException(string message, Exception innerException)
			: base(ErrorCode.rrRenderingError, message, innerException, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null, null)
		{
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x00031A44 File Offset: 0x0002FC44
		internal PowerPointExportException(string message)
			: this(message, null)
		{
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x00031A4E File Offset: 0x0002FC4E
		internal PowerPointExportException(ErrorCode code, Exception e)
			: base(code, e.Message, null, RSTrace.IsTraceInitialized ? RSTrace.CatalogTrace : null, null, null)
		{
		}
	}
}

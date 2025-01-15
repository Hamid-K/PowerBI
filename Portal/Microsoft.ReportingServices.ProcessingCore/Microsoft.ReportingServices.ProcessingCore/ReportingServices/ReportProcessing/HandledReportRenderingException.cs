using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000632 RID: 1586
	[Serializable]
	internal sealed class HandledReportRenderingException : RSException
	{
		// Token: 0x060056FA RID: 22266 RVA: 0x0016ED29 File Offset: 0x0016CF29
		private HandledReportRenderingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060056FB RID: 22267 RVA: 0x0016ED33 File Offset: 0x0016CF33
		internal HandledReportRenderingException(ReportRenderingException innerException)
			: base(innerException.ErrorCode, innerException.Message, innerException, Global.RenderingTracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x060056FC RID: 22268 RVA: 0x0016ED53 File Offset: 0x0016CF53
		internal HandledReportRenderingException(ErrorCode errCode, string message)
			: base(errCode, message, null, Global.RenderingTracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x060056FD RID: 22269 RVA: 0x0016ED69 File Offset: 0x0016CF69
		internal HandledReportRenderingException(ErrorCode errCode, Exception innerException)
			: base(errCode, innerException.Message, innerException, Global.RenderingTracer, null, Array.Empty<object>())
		{
		}
	}
}

using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000631 RID: 1585
	[Serializable]
	internal sealed class UnhandledReportRenderingException : RSException
	{
		// Token: 0x060056F7 RID: 22263 RVA: 0x0016ECE1 File Offset: 0x0016CEE1
		private UnhandledReportRenderingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060056F8 RID: 22264 RVA: 0x0016ECEB File Offset: 0x0016CEEB
		internal UnhandledReportRenderingException(ReportRenderingException innerException)
			: base(innerException.ErrorCode, innerException.Message, innerException, Global.RenderingTracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x060056F9 RID: 22265 RVA: 0x0016ED0B File Offset: 0x0016CF0B
		internal UnhandledReportRenderingException(Exception innerException)
			: base(ErrorCode.rrRenderingError, RenderErrors.rrRenderingError, innerException, Global.RenderingTracer, null, Array.Empty<object>())
		{
		}
	}
}

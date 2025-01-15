using System;
using System.Globalization;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000633 RID: 1587
	[Serializable]
	internal sealed class RenderingObjectModelException : RSException
	{
		// Token: 0x060056FE RID: 22270 RVA: 0x0016ED84 File Offset: 0x0016CF84
		private RenderingObjectModelException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060056FF RID: 22271 RVA: 0x0016ED8E File Offset: 0x0016CF8E
		internal RenderingObjectModelException(string LocalizedErrorMessage)
			: base(ErrorCode.rrRenderingError, LocalizedErrorMessage, null, Global.RenderingTracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x06005700 RID: 22272 RVA: 0x0016EDA8 File Offset: 0x0016CFA8
		internal RenderingObjectModelException(Exception innerException)
			: base(ErrorCode.rrRenderingError, innerException.Message, innerException, Global.RenderingTracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x06005701 RID: 22273 RVA: 0x0016EDC7 File Offset: 0x0016CFC7
		internal RenderingObjectModelException(ProcessingErrorCode errCode)
			: base(ErrorCode.rrRenderingError, RPResWrapper.Keys.GetString(errCode.ToString()), null, Global.RenderingTracer, null, Array.Empty<object>())
		{
			this.m_processingErrorCode = errCode;
		}

		// Token: 0x06005702 RID: 22274 RVA: 0x0016EDF9 File Offset: 0x0016CFF9
		internal RenderingObjectModelException(ProcessingErrorCode errCode, params object[] arguments)
			: base(ErrorCode.rrRenderingError, string.Format(CultureInfo.CurrentCulture, RPResWrapper.Keys.GetString(errCode.ToString()), arguments), null, Global.RenderingTracer, null, Array.Empty<object>())
		{
			this.m_processingErrorCode = errCode;
		}

		// Token: 0x06005703 RID: 22275 RVA: 0x0016EE36 File Offset: 0x0016D036
		internal RenderingObjectModelException(ErrorCode code, params object[] arguments)
			: base(code, string.Format(CultureInfo.CurrentCulture, RPResWrapper.Keys.GetString(code.ToString()), arguments), null, Global.Tracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x06005704 RID: 22276 RVA: 0x0016EE68 File Offset: 0x0016D068
		internal RenderingObjectModelException(ErrorCode code, Exception innerException, params object[] arguments)
			: base(code, string.Format(CultureInfo.CurrentCulture, RPResWrapper.Keys.GetString(code.ToString()), arguments), innerException, Global.Tracer, null, Array.Empty<object>())
		{
		}

		// Token: 0x17001FB2 RID: 8114
		// (get) Token: 0x06005705 RID: 22277 RVA: 0x0016EE9A File Offset: 0x0016D09A
		internal ProcessingErrorCode ProcessingErrorCode
		{
			get
			{
				return this.m_processingErrorCode;
			}
		}

		// Token: 0x04002DE6 RID: 11750
		private ProcessingErrorCode m_processingErrorCode;
	}
}

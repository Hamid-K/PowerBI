using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200007C RID: 124
	[Serializable]
	public abstract class ReportProcessingExceptionBase : RSException
	{
		// Token: 0x0600035E RID: 862 RVA: 0x0000B0D7 File Offset: 0x000092D7
		protected ReportProcessingExceptionBase(ErrorCode errorCode, string localizedMessage, Exception innerException, RSTrace tracer, string additionalTraceMessage, params object[] exceptionData)
			: base(errorCode, localizedMessage, innerException, tracer, additionalTraceMessage, exceptionData)
		{
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000B0E8 File Offset: 0x000092E8
		protected ReportProcessingExceptionBase(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

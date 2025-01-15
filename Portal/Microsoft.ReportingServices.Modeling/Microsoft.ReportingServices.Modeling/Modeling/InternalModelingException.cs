using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	internal class InternalModelingException : RSException
	{
		// Token: 0x06000312 RID: 786 RVA: 0x0000AC82 File Offset: 0x00008E82
		internal InternalModelingException(string traceMessage)
			: this(traceMessage, null)
		{
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000AC8C File Offset: 0x00008E8C
		internal InternalModelingException(string traceMessage, Exception inner)
			: base(ErrorCode.rsInternalError, SRErrors.InternalError, inner, Internal.GetTracer(), traceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000ACA7 File Offset: 0x00008EA7
		protected InternalModelingException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

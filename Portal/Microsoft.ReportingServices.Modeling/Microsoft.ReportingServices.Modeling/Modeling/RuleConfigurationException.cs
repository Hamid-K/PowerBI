using System;
using System.Runtime.Serialization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Modeling
{
	// Token: 0x0200004E RID: 78
	[Serializable]
	internal class RuleConfigurationException : RSException
	{
		// Token: 0x0600032D RID: 813 RVA: 0x0000B074 File Offset: 0x00009274
		internal RuleConfigurationException(string traceMessage)
			: this(traceMessage, null)
		{
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000B07E File Offset: 0x0000927E
		internal RuleConfigurationException(string traceMessage, Exception inner)
			: base(ErrorCode.rsInternalError, SRErrors.InternalError, inner, Internal.GetTracer(), traceMessage, Array.Empty<object>())
		{
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000B099 File Offset: 0x00009299
		protected RuleConfigurationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

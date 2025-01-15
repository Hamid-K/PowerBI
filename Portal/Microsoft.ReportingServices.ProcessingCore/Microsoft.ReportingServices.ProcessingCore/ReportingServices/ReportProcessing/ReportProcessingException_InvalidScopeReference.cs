using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005EC RID: 1516
	[Serializable]
	internal sealed class ReportProcessingException_InvalidScopeReference : Exception
	{
		// Token: 0x0600541B RID: 21531 RVA: 0x00161A8C File Offset: 0x0015FC8C
		internal ReportProcessingException_InvalidScopeReference(string scopeName)
			: base(string.Format(CultureInfo.CurrentCulture, RPResWrapper.rsInvalidRuntimeScopeReference(scopeName), Array.Empty<object>()))
		{
		}

		// Token: 0x0600541C RID: 21532 RVA: 0x00161AA9 File Offset: 0x0015FCA9
		private ReportProcessingException_InvalidScopeReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005EF RID: 1519
	[Serializable]
	internal sealed class ReportProcessingException_NonExistingLookupReference : Exception
	{
		// Token: 0x06005421 RID: 21537 RVA: 0x00161B01 File Offset: 0x0015FD01
		internal ReportProcessingException_NonExistingLookupReference()
			: base(string.Format(CultureInfo.CurrentCulture, RPRes.rsNonExistingLookupReference, Array.Empty<object>()))
		{
		}

		// Token: 0x06005422 RID: 21538 RVA: 0x00161B1D File Offset: 0x0015FD1D
		internal ReportProcessingException_NonExistingLookupReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

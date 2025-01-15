using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005E7 RID: 1511
	[Serializable]
	internal sealed class ReportProcessingException_NonExistingReportItemReference : Exception
	{
		// Token: 0x06005411 RID: 21521 RVA: 0x001619C9 File Offset: 0x0015FBC9
		internal ReportProcessingException_NonExistingReportItemReference(string itemName)
			: base(string.Format(CultureInfo.CurrentCulture, RPResWrapper.rsNonExistingReportItemReference(itemName), Array.Empty<object>()))
		{
		}

		// Token: 0x06005412 RID: 21522 RVA: 0x001619E6 File Offset: 0x0015FBE6
		private ReportProcessingException_NonExistingReportItemReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

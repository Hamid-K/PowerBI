using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005ED RID: 1517
	[Serializable]
	internal sealed class ReportProcessingException_NonExistingGlobalReference : Exception
	{
		// Token: 0x0600541D RID: 21533 RVA: 0x00161AB3 File Offset: 0x0015FCB3
		internal ReportProcessingException_NonExistingGlobalReference(string globalName)
			: base(string.Format(CultureInfo.CurrentCulture, RPResWrapper.rsNonExistingGlobalReference(globalName), Array.Empty<object>()))
		{
		}

		// Token: 0x0600541E RID: 21534 RVA: 0x00161AD0 File Offset: 0x0015FCD0
		internal ReportProcessingException_NonExistingGlobalReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

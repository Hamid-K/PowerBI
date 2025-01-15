using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005EE RID: 1518
	[Serializable]
	internal sealed class ReportProcessingException_NonExistingUserReference : Exception
	{
		// Token: 0x0600541F RID: 21535 RVA: 0x00161ADA File Offset: 0x0015FCDA
		internal ReportProcessingException_NonExistingUserReference(string propName)
			: base(string.Format(CultureInfo.CurrentCulture, RPResWrapper.rsNonExistingUserReference(propName), Array.Empty<object>()))
		{
		}

		// Token: 0x06005420 RID: 21536 RVA: 0x00161AF7 File Offset: 0x0015FCF7
		internal ReportProcessingException_NonExistingUserReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

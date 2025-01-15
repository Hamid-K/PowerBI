using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005EB RID: 1515
	[Serializable]
	internal sealed class ReportProcessingException_NonExistingScopeReference : Exception
	{
		// Token: 0x06005419 RID: 21529 RVA: 0x00161A65 File Offset: 0x0015FC65
		internal ReportProcessingException_NonExistingScopeReference(string scopeName)
			: base(string.Format(CultureInfo.CurrentCulture, RPResWrapper.rsNonExistingScopeReference(scopeName), Array.Empty<object>()))
		{
		}

		// Token: 0x0600541A RID: 21530 RVA: 0x00161A82 File Offset: 0x0015FC82
		private ReportProcessingException_NonExistingScopeReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}

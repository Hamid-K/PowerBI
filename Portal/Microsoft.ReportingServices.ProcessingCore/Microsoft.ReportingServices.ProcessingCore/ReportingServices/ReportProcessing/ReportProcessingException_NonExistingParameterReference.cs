using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005E6 RID: 1510
	[Serializable]
	internal sealed class ReportProcessingException_NonExistingParameterReference : Exception
	{
		// Token: 0x0600540F RID: 21519 RVA: 0x001619A2 File Offset: 0x0015FBA2
		internal ReportProcessingException_NonExistingParameterReference(string paramName)
			: base(string.Format(CultureInfo.CurrentCulture, RPResWrapper.rsNonExistingParameterReference(paramName), Array.Empty<object>()))
		{
		}

		// Token: 0x06005410 RID: 21520 RVA: 0x001619BF File Offset: 0x0015FBBF
		private ReportProcessingException_NonExistingParameterReference(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
